const path = require("path");
const Config = require("./Config");
const SocketronNode = require("./SocketronNode");
const SocketronRenderer = require("./SocketronRenderer");

Config.path = __filename;
let Socketron = SocketronNode;
if (process.type === "renderer") {
	Socketron = SocketronRenderer;
	const socketron = new Socketron();
	/*
	window._socketron = new Socketron();
	if (typeof module == "object") {
		window.Socketron = Socketron;
	}
	//*/
}

module.exports = Socketron;
