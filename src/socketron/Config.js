class Config {
	static get Name() {
		return "Socketron";
	}
	static get IpcEventPrefix() {
		return "_Socketron.";
	}
	static get MaxDataSize() {
		// 512 KB
		return 512 * 1024;
	}
	static get Encoding() {
		return "utf8";
	}
}

module.exports = Config;
