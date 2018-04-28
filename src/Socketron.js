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
		this.func = "";
		this.args = null;

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
		return JSON.stringify(this, (key, value) => {
			if (value == null || value === "") {
				return undefined;
			}
			return value;
		});
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
		this.socket.write(data.toBuffer());
	}
}

class Clients {
	constructor() {
		this._clients = [];
		this._clientIds = {};
	}

	add(client) {
		this._clients.push(client);
		this._clientIds[client.id] = client;
	}

	remove(client) {
		const index = this._clients.indexOf(client);
		this._clients.splice(index, 1);
		this._clientIds[client.id] = null;
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
		return this._clientIds[id];
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
			this._onConnect.bind(this)
		);
		this._server.listen(port);
	}
	
	broadcast(message, sender = null) {
		this._clients.broadcast(message, sender);
	}
	
	findClientById(id) {
		return this._clients.findClientById(id);
	}

	_onConnect(socket) {
		const client = new Client(socket);
		this._clients.add(client);
		this.emit("log", client, "client connected");

		socket.on("error", (err) => {
			this.emit("error", client, err);
			this._clients.remove(client);
		});
		socket.on("timeout", () => {
			this.emit("timeout", client);
		});
		socket.on("data", (data) => {
			this._onData(client, data);
		});
		socket.on("close", () => {
			this.emit("log", client, "client closed");
			this._clients.remove(client);
		});
		socket.on("end", () => {
			this._clients.remove(client);
		});

		let data = new SocketronData({
			status: "ok",
			func: "id",
			args: client.id
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
		this.socketron = null;
		this.exports = {};
		this.electron = Electron;
		this.process = process;
		this.dialog = dialog;
		this._client = null;
		this._callbackList = {
			BrowserWindow: [],
			webContents: []
		};
	}

	run(data, client) {
		const func = this._getFunction(data.func);
		if (func == null) {
			this._sendError(data, client, "function not found: " + data.func);
			return;
		}
		const funcType = typeof func[1];
		if (funcType !== "function") {
			if (funcType === "object") {
				/*
				const obj = {};
				for (var i in func[1]) {
					obj[i] = func[1][i];
				}
				*/
				try {
					this._sendCallback(data, client, func[1]);
				} catch (e) {
					console.error(e);
					this._sendError(data, client, e.stack);
				}
				return;
			}
			this._sendCallback(data, client, func[1]);
			return;
		}
		this._client = client;
		try {
			let result = func[1].apply(func[0], data.args);
			if (result != null) {
				this._sendCallback(data, client, result);
			}
		} catch (e) {
			console.error(e);
			this._sendError(data, client, e.stack);
		}
		this._client = null;
	}

	executeJavaScript(script) {
		if (script == null) {
			return;
		}
		console.log(script);
		const start = Date.now();
		const clientId = this._client.id;
		const funcs = [
			"var socketron = this.socketron;",
			"var electron = this.electron;",
			"var emit = function (eventName, ...args) {",
				"socketron.emitToClient(",
					"'" + clientId + "',",
					"eventName, args",
				");",
			"};"
		];
		script = funcs.join("") + script;
		//eval(script);
		const result = Function(script).apply(this);
		console.log("time: " + (Date.now() - start));
		return result;
	}

	_getFunction(name) {
		if (name == null) {
			return null;
		}
		let names = name.split(".");
		if (names.length <= 0) {
			return null;
		}
		let parent = null;
		let reference = this;
		const length = names.length;
		for (let i = 0; i < length; i++) {
			parent = reference;
			reference = reference[names[i]];
			if (reference == null) {
				return null;
			}
		}
		return [parent, reference];
	}

	_addClientEventListener(className, callbackId, func) {
		this._callbackList[className][callbackId] = func;
	}

	_removeClientEventListener(className, callbackId) {
		this._callbackList[className][callbackId] = null;
	}
	
	_sendCallback(data, client, args) {
		if (data.sequenceId == null) {
			return;
		}
		this.socketron._sendCallback(data, client, args);
		//this.emit("callback", data, client, args);
	}
	
	_sendError(data, client, message) {
		this.emit("error", data, client, message);
	}
}

class SocketronNode {
	constructor() {
		this.browserWindow = null;
		
		this._callbackData = new SocketronData({
			status: "ok",
			func: "callback"
		});

		this._server = new SocketronServer();
		this._server.on("log", (client, message) => {
			const args = [client.id, message];
			console.log.apply(this, args);
			this._ipcLog.apply(this, args);
		});
		this._server.on("error", (client, err) => {
			const args = [client.id, "socket error", err];
			console.error.apply(this, args);
			this._ipcError.apply(this, args);
		});
		this._server.on("timeout", (client) => {
			const args = [client.id, "socket timeout"];
			console.warn.apply(this, args);
			this._ipcWarn.apply(this, args);
		});
		this._server.on("data", this._onData.bind(this));
		this._addIpcEvents();
		
		this._processor = new CommandProcessorNode();
		this._processor.socketron = this;
		this._processor.on("error", (data, client, message) => {
			this._ipcError(message);

			if (data.sequenceId == null) {
				return;
			}
			let newData = new SocketronData({
				sequenceId: data.sequenceId,
				status: "error",
				func: "callback",
				args: message
			});
			client.writeTextData(newData);
		});
		this._processor.on("callback", this._sendCallback.bind(this));
	}

	get exports() {
		return this._processor.exports;
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
	
	emitToClient(clientId, eventName, args) {
		const client = this._server.findClientById(clientId);
		if (client == null) {
			return;
		}
		let data = new SocketronData({
			status: "ok",
			func: "event",
			args: {
				name: eventName,
				args: args
			}
		});
		client.writeTextData(data);
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
		this._addIpcEvent("event", (e, clientId, eventName, args) => {
			const client = this._server.findClientById(clientId);
			if (client == null) {
				return;
			}
			let data = new SocketronData({
				status: "ok",
				func: "event",
				args: {}
			});
			data.args[eventName] = args;
			client.writeTextData(data);
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
		let data = new SocketronData({
			type: "renderer",
			func: "console.log",
			args: args
		});
		this._ipcSend("data", data);
	}

	_ipcInfo(...args) {
		let data = new SocketronData({
			type: "renderer",
			func: "console.info",
			args: args
		});
		this._ipcSend("data", data);
	}

	_ipcWarn(...args) {
		let data = new SocketronData({
			type: "renderer",
			func: "console.warn",
			args: args
		});
		this._ipcSend("data", data);
	}

	_ipcError(...args) {
		let data = new SocketronData({
			type: "renderer",
			func: "console.error",
			args: args
		});
		this._ipcSend("data", data);
	}

	_sendCallback(data, client, args) {
		if (data.sequenceId == null) {
			return;
		}
		//this._ipcLog(data);
		/*
		let newData = new SocketronData({
			sequenceId: data.sequenceId,
			status: "ok",
			func: "callback",
			args: args
		});
		//*/
		const callbackData = this._callbackData;
		callbackData.sequenceId = data.sequenceId;
		callbackData.args = args;
		client.writeTextData(callbackData);
	}
}

class CommandProcessorRenderer extends EventEmitter {
	constructor() {
		super();
		this.socketron = null;
		this.exports = {};
		this.window = window;
		this.console = console;
		this._data = null;
		this._clientId = null;
	}

	run(data, clientId) {
		const func = this._getFunction(data.func);
		if (func == null) {
			this._sendCallback(data.sequenceId, clientId, "function not found: " + data.func);
			return;
		}
		const funcType = typeof func[1];
		if (funcType !== "function") {
			if (funcType === "object") {
				/*
				const obj = {};
				for (var i in func[1]) {
					obj[i] = func[1][i];
				}
				*/
				try {
					this._sendCallback(data.sequenceId, clientId, [obj]);
				} catch (e) {
					console.error(e);
					this._sendError(data.sequenceId, clientId, e.stack);
				}
				return;
			}
			this._sendCallback(data.sequenceId, clientId, [func[1]]);
			return;
		}
		
		this._data = data;
		this._clientId = clientId;
		try {
			let result = func[1].apply(func[0], data.args);
			if (result != null) {
				this._sendCallback(data.sequenceId, clientId, result);
			}
		} catch (e) {
			console.error(e);
			this._sendError(data.sequenceId, clientId, e.stack);
		}
		this._data = null;
		this._clientId = null;
	}

	executeJavaScript(script) {
		if (script == null) {
			return;
		}
		//console.log(script);

		const clientId = this._clientId;
		const funcs = [
			"var socketron = this.socketron;",
			"var electron = this.electron;",
			"var emit = function (eventName, ...args) {",
				"socketron.emitToClient(",
					"'" + clientId + "',",
					"eventName, args",
				");",
			"};"
		];
		script = funcs.join("") + script;
		//eval(script);
		return Function(script).apply(this);
	}

	insertJavaScript(url) {
		if (url == null) {
			return;
		}
		const data = this._data;
		const clientId = this._clientId;
		console.log(url);
		const script = document.createElement("script");
		document.head.appendChild(script);
		script.addEventListener("load", () => {
			this._sendCallback(data.sequenceId, clientId);
		});
		script.setAttribute("src", url);
	}

	insertCSS(style) {
		if (style == null) {
			return;
		}
		const data = this._data;
		const clientId = this._clientId;
		console.log(style);
		const element = document.createElement("style");
		document.head.appendChild(element);
		element.addEventListener("load", () => {
			this._sendCallback(data.sequenceId, clientId);
		});
		element.innerHTML = style;
	}

	_getFunction(name) {
		if (name == null) {
			return null;
		}
		let names = name.split(".");
		if (names.length <= 0) {
			return null;
		}
		let parent = null;
		let reference = this;
		const length = names.length;
		for (let i = 0; i < length; i++) {
			parent = reference;
			reference = reference[names[i]];
			if (reference == null) {
				return null;
			}
		}
		return [parent, reference];
	}
	
	_sendCallback(sequenceId, clientId, args) {
		this.emit("callback", sequenceId, clientId, args);
	}
	
	_sendError(sequenceId, clientId, message) {
		this.emit("error", sequenceId, clientId, message);
	}
}

class SocketronRenderer {
	constructor() {
		this._addIpcEvents();
		
		this._processor = new CommandProcessorRenderer();
		this._processor.socketron = this;
		this._processor.on("error", (sequenceId, clientId, message) => {
			console.error(message);
			
			if (sequenceId == null) {
				return;
			}
			let newData = new SocketronData({
				sequenceId: sequenceId,
				status: "error",
				func: "callback",
				args: message
			});
			this.send(clientId, newData.toBuffer());
		});
		this._processor.on("callback", this._sendCallback.bind(this));
	}

	get exports() {
		return this._processor.exports;
	}

	broadcast(message, sender = null) {
		const data = Packet.createData(DataType.Log, 0, message);
		this._ipcSend("broadcast", data, sender);
	}

	send(client, buffer) {
		this._ipcSend("send", client, buffer);
	}

	emitToClient(clientId, eventName, args) {
		this._ipcSend("event", clientId, eventName, args);
	}
	
	_ipcEventId(id) {
		return Config.IpcEventPrefix + id;
	}
	
	_addIpcEvent(channel, handler) {
		channel = this._ipcEventId(channel);
		ipcRenderer.on(channel, handler);
	}
	
	_addIpcEvents() {
		this._addIpcEvent("data", this._onData.bind(this));
	}

	_onData(e, data, clientId) {
		this._processor.run(data, clientId);
	}

	_ipcSend(channel, ...args) {
		ipcRenderer.send(this._ipcEventId(channel), ...args);
	}

	_sendCallback(sequenceId, clientId, args) {
		if (sequenceId == null) {
			return;
		}
		let newData = new SocketronData({
			sequenceId: sequenceId,
			status: "ok",
			func: "callback",
			args: args
		});
		this.send(clientId, newData.toBuffer());
	}
}

let Socketron = SocketronNode;
if (process.type === "renderer") {
	Socketron = SocketronRenderer;
	const socketron = new Socketron();
	/*
	window._socketron = new Socketron();
	if (typeof module == "object") {
		window.Socketron = Socketron;
	}
	*/
}

module.exports = Socketron;
