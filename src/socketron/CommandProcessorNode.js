const { EventEmitter } = require("events");

class CommandProcessorNode extends EventEmitter {
	constructor() {
		super();
		this.socketron = null;
		this._client = null;
		this._data = null;
	}

	run(data, client) {
		const func = this._getFunction(data.func);
		if (func == null) {
			const message = "function not found: " + data.func;
			console.error(message);
			client.sendError(data, message);
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
					client.sendCallback(data, func[1]);
				} catch (e) {
					console.error(e);
					client.sendError(data, e.stack);
				}
				return;
			}
			client.sendCallback(data, func[1]);
			return;
		}
		this._client = client;
		this._data = data;
		try {
			let result = func[1].apply(func[0], data.args);
			client.sendCallback(data, result);
		} catch (e) {
			console.error(e);
			client.sendError(data, data.args + "\r\n" + e.stack);
		}
		this._client = null;
		this._data = null;
	}

	executeJavaScript(script) {
		if (script == null) {
			return;
		}
		const start = Date.now();
		console.log("----- executeJavaScript -----");
		console.log(script);
		const client = this._client;
		const func = Function(script);
		const result = func.call(client.context);
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
}

module.exports = CommandProcessorNode;
