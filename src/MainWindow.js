const path = require("path");
const url = require("url");
const Electron = require("electron");
const ipcMain = Electron.ipcMain;

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
		this.browserWindow = new Electron.BrowserWindow({
			title: Config.Title,
			useContentSize: true,
			//width: this._config.window.width,
			//height: this._config.window.height,
			//x: this._config.window.x,
			//y: this._config.window.y,
			acceptFirstMouse: true,
			show: false,
			webPreferences: {
				//nodeIntegration: false
			}
		});

		this.browserWindow.loadURL(url.format({
			pathname: path.join(__dirname, Config.Content),
			protocol: "file:",
			slashes: true,
		}));
	}
}

module.exports = MainWindow;
