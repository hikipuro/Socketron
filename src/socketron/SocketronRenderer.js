const electron = require("electron");
const { ipcRenderer } = electron;

const Config = require("./Config");
const SocketronData = require("./SocketronData");
const CommandProcessorRenderer = require("./CommandProcessorRenderer");

class SocketronRenderer {
	constructor() {
		this.config = ipcRenderer.sendSync("_Socketron.initRenderer");
		this._createProcessor();
		this._addIpcEvents();
	}

	send(client, buffer) {
		this._ipcSend("send", client, buffer);
	}

	emitToClient(clientId, eventName, args) {
		this._ipcSend("event", clientId, eventName, args);
	}
	
	_createProcessor() {
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
	
	_ipcEventId(id) {
		return this.config.ipcEventPrefix + id;
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
		channel = this.config.ipcEventPrefix + channel;
		ipcRenderer.send(channel, ...args);
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

module.exports = SocketronRenderer;
