const Payload = require("./Payload");
const SocketronData = require("./SocketronData");

class ClientContext {
	constructor(client) {
		this.client = client;
		this.require = require;
		this._objects = [];
		this._freeIndices = [];
		this._index = 0;
		this._dataCache = {
			emit: new SocketronData({
				status: "ok",
				func: "event",
				args: {
					name: null,
					args: null
				}
			})
		};
	}
	
	emit(eventName, ...args) {
		const data = this._dataCache.emit;
		data.args.name = eventName;
		data.args.args = args;
		this.client.writeTextData(data);
	}

	addObject(obj) {
		if (obj == null) {
			return 0;
		}
		const index = this._getNextIndex();
		this._objects[index] = obj;
		return index;
	}

	removeObject(index) {
		if (this._objects[index] == null) {
			return;
		}
		this._objects[index] = undefined;
		this._freeIndices.push(index);
	}

	getObject(index) {
		return this._objects[index];
	}

	_getNextIndex() {
		if (this._freeIndices.length <= 0) {
			return ++this._index;
		}
		return this._freeIndices.pop();
	}
}

class Client {
	constructor(socket) {
		this.socket = socket;
		this.payload = new Payload();
		this.context = new ClientContext(this);
		this._dataCache = {
			callback: new SocketronData({
				status: "ok",
				func: "callback"
			}),
			error: new SocketronData({
				status: "error",
				func: "callback"
			}),
			emit: new SocketronData({
				status: "ok",
				func: "event",
				args: {
					name: null,
					args: null
				}
			})
		};
	}

	get id() {
		return this.socket.remoteAddress +
			":" + this.socket.remotePort;
	}

	close() {
		this.context = null;
		this.socket.destroy();
	}

	write(buffer) {
		this.socket.write(buffer);
	}

	writeTextData(data) {
		this.socket.write(data.toBuffer());
	}
	
	emit(eventName, args) {
		const data = this._dataCache.emit;
		data.args.name = eventName;
		data.args.args = args;
		this.writeTextData(data);
	}
	
	sendCallback(data, args) {
		if (data.sequenceId == null) {
			return;
		}
		const data2 = this._dataCache.callback;
		data2.sequenceId = data.sequenceId;
		data2.args = args;
		this.writeTextData(data2);
	}
	
	sendError(data, args) {
		const data2 = this._dataCache.error;
		data2.sequenceId = data.sequenceId;
		data2.args = args;
		this.writeTextData(data2);
	}
}

module.exports = Client;
