const { EventEmitter } = require("events");
const net = require("net");
const Electron = require("electron");
const { ipcMain, ipcRenderer } = Electron;

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
	Null: 0,
	Log: 1,
	Run: 2,
	ImportScript: 3,
	Command: 4,
	AppendStyle: 5,
	Callback: 6,
	ID: 7,
	Return: 8
}

const ReadState = {
	Type: 0,
	Sequence: 1,
	Length: 2,
	Data: 3,
}

class Packet {
	constructor() {
		this.data = Buffer.alloc(0);
		this.dataType = DataType.Null;
		this.sequenceId = 0;
		this.dataLength = 0;
		this.dataOffset = 0;
		this.state = ReadState.Type;
	}

	static createData(dataType, sequenceId, data) {
		const buffer = new Buffer(7 + data.length);
		buffer.writeUInt8(dataType, 0);
		buffer.writeUInt16LE(sequenceId, 1);
		buffer.writeUInt32LE(data.length, 3);
		buffer.write(data, 7);
		return buffer;
	}

	static createKeyValueData(dataType, sequenceId, key, value) {
		const data = key + "," + value;
		const buffer = new Buffer(7 + data.length);
		buffer.writeUInt8(dataType, 0);
		buffer.writeUInt16LE(sequenceId, 1);
		buffer.writeUInt32LE(data.length, 3);
		buffer.write(data, 7);
		return buffer;
	}

	static fromJson(json) {
		let packet = new Packet();
		packet.data = json.data;
		packet.dataType = json.dataType;
		packet.sequenceId = json.sequenceId;
		packet.dataLength = json.dataLength;
		packet.dataOffset = json.dataOffset;
		packet.state = json.state;
		return packet;
	}

	clear() {
		this.data.clear();
		this.dataType = DataType.Null;
		this.sequenceId = 0;
		this.dataLength = 0;
		this.dataOffset = 0;
		this.state = ReadState.Type;
	}

	clone() {
		let packet = new Packet();
		packet.data = this.data;
		packet.dataType = this.dataType;
		packet.sequenceId = this.sequenceId;
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
		});
		socket.on("end", () => {
			this._clients.remove(client);
		});

		let data = Packet.createData(DataType.ID, 0, client.id);
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
			case ReadState.Sequence:
				if (remain < 2) {
					return;
				}
				break;
			case ReadState.Length:
				if (remain < 4) {
					return;
				}
				break;
			case ReadState.Data:
				if (remain < packet.dataLength) {
					return;
				}
				break;
		}

		switch (packet.state) {
			case ReadState.Type:
				packet.dataType = packet.data[offset];
				packet.dataOffset += 1;
				packet.state = ReadState.Sequence;
				break;
			case ReadState.Sequence:
				packet.sequenceId = packet.data.readUInt16LE(offset);
				packet.dataOffset += 2;
				packet.state = ReadState.Length;
				break;
			case ReadState.Length:
				packet.dataLength = packet.data.readUInt32LE(offset);
				if (packet.dataLength > Config.MaxDataSize) {
					this.emit("error", "incorrect data size (size > max data size)", client.id);
					//client.socket.destroy();
					client.socket.end();
					return;
				}
				packet.dataOffset += 4;
				packet.state = ReadState.Data;
				break;
			case ReadState.Data:
				this.emit("data", client, packet.clone());
				packet.data = packet.data.slice(offset + packet.dataLength);
				packet.dataOffset = 0;
				packet.state = ReadState.Type;
				break;
		}

		this._onData(client, null);
	}
}

class SocketronNode {
	constructor() {
		this.browserWindow = null;
		this._server = new SocketronServer();
		this._server.on("log", (...args) => {
			this._ipcLog.apply(this, args);
		});
		this._server.on("error", (err) => {
			this._ipcError("socket error");
		});
		this._server.on("timeout", () => {
			this._ipcWarn("socket timeout");
		});
		this._server.on("data", this._onData.bind(this));
		this._addIpcEvents();
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
		//this._ipcLog("sequence: " + packet.sequenceId);
		const message = packet.getStringData();
		let data = null;
		switch (packet.dataType) {
			case DataType.Log:
				this._ipcLog(message);
				data = Packet.createData(DataType.Callback, packet.sequenceId, "ok");
				client.write(data);
				break;
			case DataType.Run:
				this._ipcSend("run", message);
				data = Packet.createData(DataType.Callback, packet.sequenceId, "ok");
				client.write(data);
				break;
			case DataType.ImportScript:
				this._ipcSend("import", packet, client.id);
				break;
			case DataType.Command:
				this._ipcSend("command", message);
				break;
			case DataType.AppendStyle:
				this._ipcSend("appendStyle", packet, client.id);
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
				const data = Packet.createKeyValueData(DataType.Return, 0, key, value);
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
}

class SocketronRenderer {
	constructor() {
		if (SocketronRenderer._instance == null) {
			SocketronRenderer._instance = this;
		}
		this._addIpcEvents();
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
		this._addIpcEvent("run", (e, script) => {
			console.log(script);
			//eval(script);
			Function(script)();
		});
		this._addIpcEvent("import", (e, packet, clientId) => {
			packet = Packet.fromJson(packet);
			const url = packet.getStringData();
			console.log(url);
			const script = document.createElement("script");
			document.head.appendChild(script);
			script.addEventListener("load", () => {
				const data = Packet.createData(DataType.Callback, packet.sequenceId, url);
				this.send(clientId, data);
			});
			script.setAttribute("src", url);
		});
		this._addIpcEvent("command", (e, command, ...args) => {
			switch (command) {
				case "quit":
					this.quit();
					break;
				case "reload":
					location.reload();
					break;
			}
		});
		this._addIpcEvent("appendStyle", (e, packet, clientId) => {
			packet = Packet.fromJson(packet);
			const style = packet.getStringData();
			console.log(style);
			const element = document.createElement("style");
			document.head.appendChild(element);
			element.addEventListener("load", () => {
				const data = Packet.createData(DataType.Callback, packet.sequenceId, style);
				this.send(clientId, data);
			});
			element.innerHTML = style;
		});
	}

	_ipcSend(channel, ...args) {
		ipcRenderer.send(this._ipcEventId(channel), ...args);
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
