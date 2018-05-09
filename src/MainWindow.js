const path = require("path");
const url = require("url");
const fs = require("fs");
const electron = require("electron");
const ipcMain = electron.ipcMain;

class Config {
	static get Title() {
		return "Socketron";
	}
	static get Content() {
		return "./html/index.html";
	}
	static get DevTools() {
		return true;
	}
}

class MainWindow {
	constructor() {
		this.browserWindow = null;
		this._initWindow();
	}

	show() {
		this.browserWindow.show();
		if (Config.DevTools) {
			this.browserWindow.webContents.openDevTools();
		}
	}

	destroy() {
		this.browserWindow.destroy();
	}

	_initWindow() {
		let preload = path.join(__dirname, "socketron/Socketron.js");
		this.browserWindow = new electron.BrowserWindow({
			title: Config.Title,
			useContentSize: true,
			//width: this._config.window.width,
			//height: this._config.window.height,
			//x: this._config.window.x,
			//y: this._config.window.y,
			acceptFirstMouse: true,
			show: false,
			webPreferences: {
				nodeIntegration: false,
				preload: preload
			}
		});

		//console.log(preload);

		this.browserWindow.loadURL(url.format({
			pathname: path.join(__dirname, Config.Content),
			protocol: "file:",
			slashes: true,
		}));

		const webContents = this.browserWindow.webContents;
		/*
		webContents.on("did-finish-load", () => {
			//webContents.executeJavaScript("console.log('did-finish-load: " + webContents.getURL() + "')");
			fs.readFile("./src/Socketron.js", "utf8", function (err, text) {
				if (err) {
					console.error(err);
					return;
				}
				webContents.executeJavaScript(text);
			});
		});
		//*/
	}
}

module.exports = MainWindow;
