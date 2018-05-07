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

module.exports = SocketronData;
