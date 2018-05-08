const Config = require("./Config");
const { DataType } = require("./Common");

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
	
	toBuffer() {
		const buffer = Buffer.from(this.toJson(), Config.Encoding);
		if (buffer.length <= 0xFFFF) {
			let header = new Buffer(3);
			header.writeUInt8(DataType.Text16, 0);
			header.writeUInt16LE(buffer.length, 1);
			return Buffer.concat([header, buffer]);
		} else {
			let header = new Buffer(5);
			header.writeUInt8(DataType.Text32, 0);
			header.writeUInt32LE(buffer.length, 1);
			return Buffer.concat([header, buffer]);
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
