const electron = require("electron");
const { ipcMain } = electron;

const Config = require("./Config");
const SocketronData = require("./SocketronData");
const SocketronServer = require("./SocketronServer");
const CommandProcessorNode = require("./CommandProcessorNode");

class SocketronNode {
	constructor() {
		this.config = Config;
		this.browserWindow = null;
		
		this._callbackData = new SocketronData({
			status: "ok",
			func: "callback"
		});
		this._createServer();
		this._createProcessor();
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

	_createServer() {
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
	}

	_createProcessor() {
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
	
	_sendErrorCallback(data, client, args) {
		//if (data.sequenceId == null) {
		//	return;
		//}
		let newData = new SocketronData({
			sequenceId: data.sequenceId,
			status: "error",
			func: "callback",
			args: args
		});
		client.writeTextData(newData);
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

	_addIpcEvent(channel, handler) {
		channel = Config.IpcEventPrefix + channel;
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
			this.emitToClient(clientId, eventName, args);
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
		webContents.send(Config.IpcEventPrefix + channel, ...args);
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
}

module.exports = SocketronNode;
