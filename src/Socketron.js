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
	CommandLength: 1,
	Command: 2
}

class SocketronData {
	constructor(options = null) {
		this.sequenceId = null;
		this.status = null;
		this.type = "";
		this.function = "";
		this.command = "";

		if (options != null) {
			Object.assign(this, options);
		}
	}

	static fromJson(json) {
		data = JSON.parse(json);
		return Object.assign(new SocketronData(), data);
	}
	
	toBuffer(type = DataType.Text) {
		const buffer = Buffer.from(this.toJson(), Config.Encoding);
		let header = new Buffer(3);
		header.writeUInt8(type, 0);
		header.writeUInt16LE(buffer.length, 1);
		return Buffer.concat([header, buffer]);
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

	writeTextData(data) {
		this.write(data.toBuffer());
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

		let data = new SocketronData({
			sequenceId: null,
			status: "ok",
			type: "",
			function: "id",
			command: client.id
		});
		client.writeTextData(data);
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
			case ReadState.CommandLength:
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
				packet.state = ReadState.CommandLength;
				break;
			case ReadState.CommandLength:
				packet.dataLength = packet.data.readUInt16LE(offset);
				packet.dataOffset += 2;
				packet.state = ReadState.Command;
				break;
			case ReadState.Command:
				if (packet.dataType === DataType.Text) {
					const text = packet.getStringData();
					this.emit("data", client, JSON.parse(text));
				}
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
		this.process = process;
		this.dialog = dialog;
	}

	run(data, client) {
		const func = this._getFunction(data.function);
		if (func != null) {
			func.apply(this, [data, client]);
		}
	}

	executeJavaScript(data, client) {
		const script = data.command;
		console.log(script);
		//eval(script);
		Function(script)();
		this._sendCallback(data, client);
	}

	showOpenDialog(data, client) {
		const options = JSON.parse(data.command);
		const path = dialog.showOpenDialog(options);
		this._sendCallback(data, client, JSON.stringify(path));
	}

	_getFunction(name) {
		if (name == null) {
			return null;
		}
		let names = name.split(".");
		if (names.length <= 0) {
			return null;
		}
		let reference = this;
		const length = names.length;
		for (let i = 0; i < length; i++) {
			reference = reference[names[i]];
			if (reference == null) {
				return null;
			}
		}
		return reference;
	}
	
	_sendCallback(data, client, command) {
		this.emit("callback", data, client, command);
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

	_onData(client, data) {
		if (data.type == null) {
			this._ipcSend("data", data, client.id);
			return;
		}
		switch (data.type) {
			case "renderer":
				this._ipcSend("data", data, client.id);
				break;
			case "browser":
				this._processor.run(data, client);
				break;
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
				let data = new SocketronData({
					sequenceId: null,
					status: "ok",
					type: "",
					function: "return",
					command: key + "," + value
				});
				client.writeTextData(data);
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

	_sendCallback(data, client, command) {
		if (data.sequenceId == null) {
			return;
		}
		this._ipcLog(data);
		let newData = new SocketronData({
			sequenceId: data.sequenceId,
			status: "ok",
			type: "",
			function: "callback",
			command: command
		});
		client.writeTextData(newData);
	}
}

class CommandProcessorRenderer extends EventEmitter {
	constructor() {
		super();
	}

	run(data, clientId) {
		const func = this[data.function];
		if (func != null) {
			func.apply(this, [data, clientId]);
		}
	}

	executeJavaScript(data, clientId) {
		const script = data.command;
		console.log(script);
		//eval(script);
		Function(script)();
		this._sendCallback(data, clientId);
	}

	insertJavaScript(data, clientId) {
		const url = data.command;
		console.log(url);
		const script = document.createElement("script");
		document.head.appendChild(script);
		script.addEventListener("load", () => {
			this._sendCallback(data, clientId);
		});
		script.setAttribute("src", url);
	}

	insertCSS(data, clientId) {
		const style = data.command;
		console.log(style);
		const element = document.createElement("style");
		document.head.appendChild(element);
		element.addEventListener("load", () => {
			this._sendCallback(data, clientId);
		});
		element.innerHTML = style;
	}

	command(data, clientId) {
		switch (data.command) {
			case "quit":
				this.quit();
				break;
			case "reload":
				location.reload();
				break;
		}
		this._sendCallback(data, clientId);
	}
	
	_sendCallback(data, clientId) {
		this.emit("callback", data, clientId);
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
		this._addIpcEvent("data", this._onData.bind(this));
	}

	_onData(e, data, clientId) {
		switch (data.function) {
			case "console.log":
				console.log("[" + clientId + "] " + data.command);
				this._sendCallback(data, clientId);
				break;
			default:
				this._processor.run(data, clientId);
				break;
		}
	}

	_ipcSend(channel, ...args) {
		ipcRenderer.send(this._ipcEventId(channel), ...args);
	}

	_sendCallback(data, clientId) {
		if (data.sequenceId == null) {
			return;
		}
		let newData = new SocketronData({
			sequenceId: data.sequenceId,
			status: "ok",
			type: "",
			function: "callback",
			command: null
		});
		this.send(clientId, newData.toBuffer());
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
