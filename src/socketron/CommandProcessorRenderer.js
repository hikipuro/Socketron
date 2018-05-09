const { EventEmitter } = require("events");
const SocketronData = require("./SocketronData");

class RendererContext extends EventEmitter {
	constructor() {
		super();
		this.clientId = "";
		this.require = require;
		this._objects = [];
		this._freeIndices = [];
		this._index = 0;
	}
	
	emit(eventName, ...args) {
		super.emit("emit", this.clientId, eventName, args);
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

class CommandProcessorRenderer extends EventEmitter {
	constructor() {
		super();
		this.socketron = null;
		this.context = new RendererContext();
		this.context.on("emit", (clientId, eventName, args) => {
			this.socketron.emitToClient(clientId, eventName, args);
		});
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
			this.context.clientId = clientId;
			let result = func[1].apply(func[0], data.args);
			this._sendCallback(data.sequenceId, clientId, result);
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
		const func = Function(script);
		const result = func.call(this.context);
		return result;
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

module.exports = CommandProcessorRenderer;
