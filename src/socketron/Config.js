class Config {
	constructor() {
		this.path = "";
		this.name = "Socketron";
		this.ipcEventPrefix = "_Socketron.";
		// 512 KB
		this.maxDataSize = 512 * 1024;
		this.encoding = "utf8";
	}
}

module.exports = new Config();
