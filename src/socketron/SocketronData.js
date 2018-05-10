const Config = require("./Config");
const { DataType } = require("./Common");

class SocketronData {
	constructor(options = null) {
		this.sequenceId = null;
		this.status = null;
		this.type = "";
		this.func = "";
		this.args = null;
		this.webContents = null;

		if (options != null) {
			Object.assign(this, options);
		}
	}

	static fromJson(json) {
		data = JSON.parse(json);
		return Object.assign(new SocketronData(), data);
	}
	
	toBuffer() {
		const text = this.toJson();
		const length = Buffer.byteLength(text, Config.encoding);
		//const buffer = Buffer.from(this.toJson(), Config.encoding);
		if (length <= 0xFFFF) {
			let data = Buffer.allocUnsafe(3 + length);
			data.writeUInt8(DataType.Text16, 0);
			data.writeUInt16LE(length, 1);
			data.write(text, 3);
			return data;
		} else {
			let data = Buffer.allocUnsafe(5 + length);
			data.writeUInt8(DataType.Text32, 0);
			data.writeUInt32LE(length, 1);
			data.write(text, 5);
			return data;
		}
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

module.exports = SocketronData;
