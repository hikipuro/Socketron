const Config = require("./Config");
const { DataType, ReadState } = require("./Common");

class Payload {
	constructor() {
		this.data = Buffer.alloc(0);
		this.dataType = DataType.Text16;
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

module.exports = Payload;
