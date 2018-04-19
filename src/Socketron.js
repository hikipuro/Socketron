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
	Import: 3,
	Command: 4,
}

const ClientState = {
	Type: 0,
	Length: 1,
	Data: 2,
}

class Client {
	constructor(socket) {
		this.socket = socket;
		this.data = Buffer.alloc(0);
		this.dataType = DataType.Null;
		this.dataLength = 0;
		this.dataOffset = 0;
		this.state = ClientState.Type;
	}

	get id() {
		return this.socket.remoteAddress +
			":" + this.socket.remotePort;
	}

	getStringData() {
		return this.data.toString(
			Config.Encoding,
			this.dataOffset,
			this.dataOffset + this.dataLength
		);
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
	}

	_onData(client, data) {
		if (data != null) {
			client.data = Buffer.concat([client.data, data]);
		}
		const offset = client.dataOffset;
		const remain = client.data.length - offset;

		switch (client.state) {
			case ClientState.Type:
				if (remain < 1) {
					return;
				}
				break;
			case ClientState.Length:
				if (remain < 4) {
					return;
				}
				break;
			case ClientState.Data:
				if (remain < client.dataLength) {
					return;
				}
				break;
		}

		switch (client.state) {
			case ClientState.Type:
				client.dataType = client.data[offset];
				client.dataOffset += 1;
				client.state = ClientState.Length;
				break;
			case ClientState.Length:
				client.dataLength = client.data.readUInt32LE(offset);
				if (client.dataLength > Config.MaxDataSize) {
					this.emit("error", "incorrect data size (size > max data size)", client.id);
					//client.socket.destroy();
					client.socket.end();
					return;
				}
				client.dataOffset += 4;
				client.state = ClientState.Data;
				break;
			case ClientState.Data:
				this.emit("data", client);
				client.data = client.data.slice(offset + client.dataLength);
				client.dataOffset = 0;
				client.state = ClientState.Type;
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

	_onData(client) {
		const message = client.getStringData();
		switch (client.dataType) {
			case DataType.Log:
				this._ipcLog(message);
				break;
			case DataType.Run:
				this._ipcSend("run", message);
				break;
			case DataType.Import:
				this._ipcSend("import", client.id, message);
				break;
			case DataType.Command:
				this._ipcSend("command", message);
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
				this.send(client, buffer);
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

	broadcast(message, sender = null) {
		this._ipcSend("broadcast", message, sender);
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
		this._addIpcEvent("import", (e, clientId, url) => {
			console.log(url);
			const script = document.createElement("script");
			document.head.appendChild(script);
			script.addEventListener("load", () => {
				let buffer = Buffer.alloc(1 + url.length + 1);
				buffer.writeUInt8(DataType.Import, 0);
				buffer.write(url + "\n", 1);
				this.send(clientId, buffer);
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
	}

	_ipcSend(channel, ...args) {
		ipcRenderer.send(this._ipcEventId(channel), ...args);
	}
}

let Socketron = SocketronNode;
if (process.type === "renderer") {
	Socketron = SocketronRenderer;
}
module.exports = Socketron;
