const { EventEmitter } = require("events");
const net = require("net");
const Electron = require("electron");
const { ipcMain, ipcRenderer, dialog } = Electron;

class Config {
	static get Name() {
		return "Socketron";
	}
	static get IpcEventPrefix() {
		return "_Socketron.";
	}
	static get MaxDataSize() {
		// 512 KB
		return 512 * 1024;
	}
	static get Encoding() {
		return "utf8";
	}
}

const DataType = {
	Text: 0
}

const ReadState = {
	Type: 0,
	TextLength: 1,
	Command: 2
}

class Command {
	constructor() {
		this.sequenceId = null;
		this.type = "";
		this.function = "";
		this.data = "";
	}

	static fromJson(json) {
		data = JSON.parse(json);
		let command = new Command();
		command.sequenceId = data.sequenceId;
		command.type = data.type;
		command.function = data.function;
		command.data = data.data;
	}

	toJson() {
		return JSON.stringify(this);
	}
}

class Packet {
	constructor() {
		this.data = Buffer.alloc(0);
		this.dataType = DataType.Text;
		this.dataLength = 0;
		this.dataOffset = 0;
		this.state = ReadState.Type;
	}
	
	static createTextData(command) {
		const data = Buffer.from(command.toJson(), "utf8");
		let buffer = new Buffer(3);
		buffer.writeUInt8(0, 0);
		buffer.writeUInt16LE(data.length, 1);
		buffer = Buffer.concat([buffer, data]);
		return buffer;
	}

	clone() {
		let packet = new Packet();
		packet.data = this.data;
		packet.dataType = this.dataType;
		packet.dataLength = this.dataLength;
		packet.dataOffset = this.dataOffset;
		packet.state = this.state;
		return packet;
	}
	
	getStringData() {
		return this.data.toString(
			Config.Encoding,
			this.dataOffset,
			this.dataOffset + this.dataLength
		);
	}
}

class Client {
	constructor(socket) {
		this.socket = socket;
		this.packet = new Packet();
	}

	get id() {
		return this.socket.remoteAddress +
			":" + this.socket.remotePort;
	}

	write(buffer) {
		this.socket.write(buffer);
	}
}

class Clients {
	constructor() {
		this._clients = [];
	}

	add(client) {
		this._clients.push(client);
	}

	remove(client) {
		const index = this._clients.indexOf(client);
		this._clients.splice(index, 1);
	}

	broadcast(message, sender = null) {
		this._clients.forEach((client) => {
			if (client === sender) {
				return;
			}
			client.socket.write(message);
		});
	}

	findClientById(id) {
		const length = this._clients.length;
		for (let i = 0; i < length; i++) {
			const client = this._clients[i];
			if (client.id == id) {
				return client;
			}
		}
		return null;
	}
}

class SocketronServer extends EventEmitter {
	constructor() {
		super();
		this._server = null;
		this._clients = new Clients();
	}

	listen(port) {
		this._server = net.createServer(
			this._connectionListener.bind(this)
		);
		this._server.listen(port);
	}
	
	broadcast(message, sender = null) {
		this._clients.broadcast(message, sender);
	}
	
	findClientById(id) {
		return this._clients.findClientById(id);
	}

	_connectionListener(socket) {
		const client = new Client(socket);
		this._clients.add(client);
		this.emit("log", "client connected", client.id);

		socket.on("error", (err) => {
			this.emit("error", err);
			this._clients.remove(client);
		});
		socket.on("timeout", () => {
			this.emit("timeout");
		});
		socket.on("data", (data) => {
			this._onData(client, data);
		});
		socket.on("close", () => {
			this.emit("log", "client closed", client.id);
			this._clients.remove(client);
		});
		socket.on("end", () => {
			this._clients.remove(client);
		});

		let command = new Command();
		command.sequenceId = null;
		command.type = "";
		command.function = "id";
		command.data = client.id;
		let data = Packet.createTextData(command);
		client.write(data);
	}

	_onData(client, data) {
		const packet = client.packet;
		if (data != null) {
			packet.data = Buffer.concat([packet.data, data]);
		}
		const offset = packet.dataOffset;
		const remain = packet.data.length - offset;

		switch (packet.state) {
			case ReadState.Type:
				if (remain < 1) {
					return;
				}
				break;
			case ReadState.TextLength:
				if (remain < 2) {
					return;
				}
				break;
			case ReadState.Command:
				if (remain < packet.dataLength) {
					return;
				}
				break;
		}

		switch (packet.state) {
			case ReadState.Type:
				packet.dataType = packet.data[offset];
				packet.dataOffset += 1;
				packet.state = ReadState.TextLength;
				break;
			case ReadState.TextLength:
				packet.dataLength = packet.data.readUInt16LE(offset);
				packet.dataOffset += 2;
				packet.state = ReadState.Command;
				break;
			case ReadState.Command:
				this.emit("data", client, packet.clone());
				packet.data = packet.data.slice(offset + packet.dataLength);
				packet.dataOffset = 0;
				packet.state = ReadState.Type;
				break;
		}

		this._onData(client, null);
	}
}

class CommandProcessorNode extends EventEmitter {
	constructor() {
		super();
	}

	run(command, client) {
		const func = this[command.function];
		if (func != null) {
			func.apply(this, [command, client]);
		}
	}

	showOpenDialog(command, client) {
		const options = JSON.parse(command.data);
		const path = dialog.showOpenDialog(options);
		this._sendCallback(command, client, JSON.stringify(path));
	}
	
	_sendCallback(command, client, data) {
		this.emit("callback", command, client, data);
	}
}

class SocketronNode {
	constructor() {
		this.browserWindow = null;
		this._server = new SocketronServer();
		this._server.on("log", (...args) => {
			console.log.apply(this, args);
			this._ipcLog.apply(this, args);
		});
		this._server.on("error", (err) => {
			console.error.apply(this, ["socket error", err]);
			this._ipcError("socket error");
		});
		this._server.on("timeout", () => {
			this._ipcWarn("socket timeout");
		});
		this._server.on("data", this._onData.bind(this));
		this._addIpcEvents();
		
		this._processor = new CommandProcessorNode();
		this._processor.on("callback", this._sendCallback.bind(this));
	}

	listen(port = 3000) {
		this._server.listen(port);
	}

	broadcast(message, sender = null) {
		this._server.broadcast(message, sender);
	}

	send(client, buffer) {
		client.socket.write(buffer);
	}

	quit() {
		Electron.app.quit();
	}

	_onData(client, packet) {
		// text data
		if (packet.dataType == 0) {
			const commandText = packet.getStringData();
			const command = JSON.parse(commandText);

			switch (command.type) {
				case "browser":
					this._ipcSend("command", command, client.id);
					break;
				case "node":
					this._processor.run(command, client);
					break;
			}
		}
	}

	_ipcEventId(id) {
		return Config.IpcEventPrefix + id;
	}
	
	_addIpcEvent(channel, handler) {
		channel = this._ipcEventId(channel);
		ipcMain.on(channel, handler);
	}

	_addIpcEvents() {
		this._addIpcEvent("broadcast", (e, ...args) => {
			this.broadcast.apply(this, args);
		});
		this._addIpcEvent("send", (e, clientId, buffer) => {
			const client = this._server.findClientById(clientId);
			if (client != null) {
				client.write(buffer);
			}
		});
		this._addIpcEvent("return", (e, clientId, key, value) => {
			const client = this._server.findClientById(clientId);
			if (client != null) {
				let command = new Command();
				command.sequenceId = null;
				command.type = "";
				command.function = "return";
				command.data = key + "," + value;
				const data = Packet.createTextData(command);
				client.write(data);
			}
		});
		this._addIpcEvent("quit", (e) => {
			this.quit();
		});
	}
	
	_ipcSend(channel, ...args) {
		if (this.browserWindow == null) {
			return;
		}
		if (this.browserWindow.isDestroyed()) {
			return;
		}
		const webContents = this.browserWindow.webContents;
		webContents.send(this._ipcEventId(channel), ...args);
	}

	_ipcLog(...args) {
		this._ipcSend("log", ...args);
	}

	_ipcInfo(message, ...args) {
		this._ipcSend("info", message, ...args);
	}

	_ipcWarn(message, ...args) {
		this._ipcSend("warn", message, ...args);
	}

	_ipcError(message, ...args) {
		this._ipcSend("error", message, ...args);
	}

	_sendCallback(command, client, data) {
		if (command.sequenceId == null) {
			return;
		}
		this._ipcLog(data);
		let newCommand = new Command();
		newCommand.sequenceId = command.sequenceId;
		newCommand.type = "";
		newCommand.function = "callback";
		newCommand.data = data;
		const newData = Packet.createTextData(newCommand);
		client.write(newData);
	}
}

class CommandProcessorRenderer extends EventEmitter {
	constructor() {
		super();
	}

	run(command, clientId) {
		const func = this[command.function];
		if (func != null) {
			func.apply(this, [command, clientId]);
		}
	}

	executeJavaScript(command, clientId) {
		const script = command.data;
		console.log(script);
		//eval(script);
		Function(script)();
		this._sendCallback(command, clientId);
	}

	insertJavaScript(command, clientId) {
		const url = command.data;
		console.log(url);
		const script = document.createElement("script");
		document.head.appendChild(script);
		script.addEventListener("load", () => {
			this._sendCallback(command, clientId);
		});
		script.setAttribute("src", url);
	}

	insertCSS(command, clientId) {
		const style = command.data;
		console.log(style);
		const element = document.createElement("style");
		document.head.appendChild(element);
		element.addEventListener("load", () => {
			this._sendCallback(command, clientId);
		});
		element.innerHTML = style;
	}

	command(command, clientId) {
		switch (command.data) {
			case "quit":
				this.quit();
				break;
			case "reload":
				location.reload();
				break;
		}
		this._sendCallback(command, clientId);
	}
	
	_sendCallback(command, clientId) {
		this.emit("callback", command, clientId);
	}
}

class SocketronRenderer {
	constructor() {
		if (SocketronRenderer._instance == null) {
			SocketronRenderer._instance = this;
		}
		this._addIpcEvents();
		
		this._processor = new CommandProcessorRenderer();
		this._processor.on("callback", this._sendCallback.bind(this));
	}

	static broadcast(message) {
		const socketron = SocketronRenderer._instance;
		if (socketron == null) {
			return;
		}
		socketron.broadcast(message);
	}

	static return(cliendId, key, value) {
		const socketron = SocketronRenderer._instance;
		if (socketron == null) {
			return;
		}
		socketron._ipcSend("return", cliendId, key, value);
	}

	broadcast(message, sender = null) {
		const data = Packet.createData(DataType.Log, 0, message);
		this._ipcSend("broadcast", data, sender);
	}

	send(client, buffer) {
		this._ipcSend("send", client, buffer);
	}

	quit() {
		this._ipcSend("quit");
	}
	
	_ipcEventId(id) {
		return Config.IpcEventPrefix + id;
	}
	
	_addIpcEvent(channel, handler) {
		channel = this._ipcEventId(channel);
		ipcRenderer.on(channel, handler);
	}
	
	_addIpcEvents() {
		this._addIpcEvent("log", (e, ...args) => {
			const header = Config.Name + ":";
			console.log.apply(this, [header].concat(args));
		});
		this._addIpcEvent("info", (e, ...args) => {
			const header = Config.Name + ":";
			console.info.apply(this, [header].concat(args));
		});
		this._addIpcEvent("warn", (e, ...args) => {
			const header = Config.Name + ":";
			console.warn.apply(this, [header].concat(args));
		});
		this._addIpcEvent("error", (e, ...args) => {
			const header = Config.Name + ":";
			console.error.apply(this, [header].concat(args));
		});
		this._addIpcEvent("command", (e, command, clientId) => {
			this._onCommand(command, clientId);
		});
	}

	_onCommand(command, clientId) {
		switch (command.function) {
			case "console.log":
				console.log("[" + clientId + "] " + command.data);
				this._sendCallback(command, clientId);
				break;
			default:
				this._processor.run(command, clientId);
				break;
		}
	}

	_ipcSend(channel, ...args) {
		ipcRenderer.send(this._ipcEventId(channel), ...args);
	}

	_sendCallback(command, clientId) {
		if (command.sequenceId == null) {
			return;
		}
		let newCommand = new Command();
		newCommand.sequenceId = command.sequenceId;
		newCommand.type = "";
		newCommand.function = "callback";
		newCommand.data = "ok";
		const data = Packet.createTextData(newCommand);
		this.send(clientId, data);
	}
}

let Socketron = SocketronNode;
if (process.type === "renderer") {
	Socketron = SocketronRenderer;
	window._socketron = new Socketron();
	if (typeof module == "object") {
		window.Socketron = Socketron;
	}
}

module.exports = Socketron;
