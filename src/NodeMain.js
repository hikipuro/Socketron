const Electron = require("electron");
const BrowserWindow = Electron.BrowserWindow;
const MainWindow = require("./MainWindow");
const Socketron = require("./Socketron");

class NodeMain {
	constructor(app) {
		this._app = app;
		this._addAppEvents();
	}
	
	_addAppEvents() {
		const app = this._app;
		app.once("ready", this._onReady);
		app.once("window-all-closed", this._onWindowAllClosed);
	}

	_removeAppEvents() {
		const app = this._app;
		app.removeAllListeners("ready");
		app.removeAllListeners("window-all-closed");
	}

	_onReady() {
		this._mainWindow = new MainWindow();
		let browserWindow = this._mainWindow.browserWindow;
		browserWindow.once("ready-to-show", () => {
			this._mainWindow.show();
		});
		browserWindow.once("closed", () => {
			this._mainWindow.destroy();
		});

		this._socketron = new Socketron();
		this._socketron.browserWindow = this._mainWindow.browserWindow;
		this._socketron.listen();
		this._socketron.exports.test = {
			testFunc: (a, b) => {
				console.log("testFunc", a, b);
			}
		}
	}

	get _onWindowAllClosed() {
		return () => {
			this._removeAppEvents();
			this._app.quit();
		};
	}
}

const nodeMain = new NodeMain(Electron.app);
