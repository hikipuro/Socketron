const Payload = require("./Payload");
const SocketronData = require("./SocketronData");

class Client {
	constructor(socket) {
		this.socket = socket;
		this.payload = new Payload();
		this.context = {
			require: require,
			emit: (eventName, ...args) => {
				this.emit(eventName, args);
			},
			addObject: (obj) => {
				return this.addObjectReference(obj);
			},
			removeObject: (index) => {
				this.removeObjectReference(index);
			},
			getObject: (index) => {
				return this.getObjectReference(index);
			}
		};
		this._objRefs = [];
		this._objRefsIndex = 0;
		this._callbackData = new SocketronData({
			status: "ok",
			func: "callback"
		});
		this._errorData = new SocketronData({
			status: "error",
			func: "callback"
		});
		this._emitData = new SocketronData({
			status: "ok",
			func: "event",
			args: {
				name: null,
				args: null
			}
		});
	}

	get id() {
		return this.socket.remoteAddress +
			":" + this.socket.remotePort;
	}

	close() {
		this._objRefs = [];
		this._objRefsIndex = 0;
		this.socket.destroy();
	}

	write(buffer) {
		this.socket.write(buffer);
	}

	writeTextData(data) {
		this.socket.write(data.toBuffer());
	}
	
	addObjectReference(obj) {
		if (obj == null) {
			return 0;
		}
		const index = ++this._objRefsIndex;
		this._objRefs[index] = obj;
		return index;
	}

	removeObjectReference(index) {
		this._objRefs[index] = undefined;
	}

	getObjectReference(index) {
		return this._objRefs[index];
	}
	
	emit(eventName, args) {
		const data = this._emitData;
		data.args.name = eventName;
		data.args.args = args;
		this.writeTextData(data);
	}
	
	sendCallback(data, args) {
		if (data.sequenceId == null) {
			return;
		}
		const callbackData = this._callbackData;
		callbackData.sequenceId = data.sequenceId;
		callbackData.args = args;
		this.writeTextData(callbackData);
	}
	
	sendError(data, args) {
		const callbackData = this._errorData;
		callbackData.sequenceId = data.sequenceId;
		callbackData.args = args;
		this.writeTextData(callbackData);
	}
}

module.exports = Client;
