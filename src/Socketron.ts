import { EventEmitter } from "events";
import * as net from "net";
import * as Electron from "electron";
import { ipcMain, ipcRenderer } from "electron";

module Config {
	export const IpcEventPrefix = "_Socketron.";
	export const MaxDataSize = 512 * 1024; // 512 KB
	export const Encoding = "utf8";
}

enum DataType {
	Null = 0,
	Log,
	Run,
	Import,
	Command
}

enum ClientState {
	Type = 0,
	Length,
	Data
}

class Client {
	public socket: net.Socket = null;
	public data: Buffer = null;
	public dataType: DataType = DataType.Null;
	public dataLength: number = 0;
	public dataOffset: number = 0;
	public state: ClientState = ClientState.Type;

	public get id(): string {
		return this.socket.remoteAddress +
			":" + this.socket.remotePort;
	}

	constructor() {
		this.data = Buffer.alloc(0);
	}

	public getString(): string {
		return this.data.toString(
			Config.Encoding,
			this.dataOffset,
			this.dataOffset + this.dataLength
		);
	}
}

class Clients {
	protected _clients: Client[] = [];

	public add(client: Client): void {
		this._clients.push(client);
	}

	public remove(client: Client): void {
		const index = this._clients.indexOf(client);
		this._clients.splice(index, 1);
	}

	public broadcast(message: string, sender: Client = null): void {
		this._clients.forEach((client: Client) => {
			if (client === sender) {
				return;
			}
			client.socket.write(message);
		});
	}

	public findClientById(id: string): Client {
		const length = this._clients.length;
		for (let i = 0; i < length; i++) {
			const client: Client = this._clients[i];
			if (client.id == id) {
				return client;
			}
		}
		return null;
	}
}

export class Socketron {
	public browserWindow: Electron.BrowserWindow = null;
	protected static _instance: Socketron = null;
	protected _server: net.Server = null;
	protected _clients: Clients = new Clients();

	protected get isRenderer(): boolean {
		return process.type === "renderer";
	}

	public static broadcast(message: string): void {
		if (Socketron._instance == null) {
			return;
		}
		Socketron._instance.broadcast(message);
	}

	constructor() {
		if (Socketron._instance == null) {
			Socketron._instance = this;
		}
		this._addIpcEventsMain();
		this._addIpcEventsRenderer();
	}

	public startServer(port: number = 3000): void {
		if (this.isRenderer) {
			return;
		}
		this._server = net.createServer((socket: net.Socket) => {
			this._ipcLog("server-> tcp server created");
			const client = new Client();
			client.socket = socket;
			this._clients.add(client);

			socket.on("error", (err: Error) => {
				this._ipcError("socket error", err);
				this._clients.remove(client);
			});
			socket.on("timeout", () => {
				this._ipcWarn("socket timeout");
			});
			socket.on("data", (data: Buffer) => {
				this._onReceiveData(client, data);
			});
			socket.on("close", () => {
				this._ipcLog("server-> client closed connection");
			});
			socket.on("end", () => {
				this._clients.remove(client);
			});
		});
		this._server.listen(port);
	}

	public quit(): void {
		if (this.isRenderer) {
			this._ipcSend("quit");
			return;
		}
		Electron.app.quit();
	}

	public broadcast(message: string, sender: Client = null): void {
		if (this.isRenderer) {
			this._ipcSend("broadcast", message, sender);
			return;
		}
		this._clients.broadcast(message, sender);
	}

	public send(client: Client|string, buffer: Buffer): void {
		if (this.isRenderer) {
			this._ipcSend("send", client, buffer);
			return;
		}
		if (client instanceof Client) {
			client.socket.write(buffer);
		}
	}
	
	protected _addIpcEventsMain(): void {
		if (this.isRenderer) {
			return;
		}
		ipcMain.on(this._ipcEventId("broadcast"), (e: EventEmitter, ...args: any[]) => {
			this.broadcast.apply(this, args);
		});
		ipcMain.on(this._ipcEventId("send"), (e: EventEmitter, clientId: string, buffer: Buffer) => {
			const client = this._clients.findClientById(clientId);
			if (client != null) {
				this.send(client, buffer);
			}
		});
		ipcMain.on(this._ipcEventId("quit"), (e: EventEmitter) => {
			Electron.app.quit();
		});
	}

	protected _addIpcEventsRenderer(): void {
		if (!this.isRenderer) {
			return;
		}
		ipcRenderer.on(this._ipcEventId("log"), (e: EventEmitter, ...args: any[]) => {
			const header = Socketron.name + ":";
			console.log.apply(this, [header].concat(args));
		});
		ipcRenderer.on(this._ipcEventId("info"), (e: EventEmitter, ...args: any[]) => {
			const header = Socketron.name + ":";
			console.info.apply(this, [header].concat(args));
		});
		ipcRenderer.on(this._ipcEventId("warn"), (e: EventEmitter, ...args: any[]) => {
			const header = Socketron.name + ":";
			console.warn.apply(this, [header].concat(args));
		});
		ipcRenderer.on(this._ipcEventId("error"), (e: EventEmitter, ...args: any[]) => {
			const header = Socketron.name + ":";
			console.error.apply(this, [header].concat(args));
		});
		ipcRenderer.on(this._ipcEventId("run"), (e: EventEmitter, script: string) => {
			console.log(script);
			//eval(script);
			Function(script)();
		});
		ipcRenderer.on(this._ipcEventId("import"), (e: EventEmitter, clientId: string, url: string) => {
			console.log(url);
			const script = document.createElement("script");
			script.addEventListener("load", () => {
				document.addEventListener("DOMContentLoaded", () => {
					console.log("document.ready ###");
				});

				let buffer = Buffer.alloc(1 + url.length + 1);
				buffer.writeUInt8(DataType.Import, 0);
				buffer.write(url + "\n", 1);
				this.send(clientId, buffer);
			});
			script.setAttribute("src", url);
			document.head.appendChild(script);
		});
		ipcRenderer.on(this._ipcEventId("command"), (e: EventEmitter, command: string, ...args: any[]) => {
			switch (command) {
				case "quit":
					this.quit();
					break;
				case "reload":
					location.reload();
					break;
			}
		});
	}

	protected _ipcSend(channel: string, ...args: any[]): void {
		if (this.isRenderer) {
			ipcRenderer.send(this._ipcEventId(channel), ...args);
			return;
		}

		if (this.browserWindow == null) {
			return;
		}
		if (this.browserWindow.isDestroyed()) {
			return;
		}
		const webContents = this.browserWindow.webContents;
		webContents.send(this._ipcEventId(channel), ...args);
	}

	protected _ipcLog(...args: any[]): void {
		this._ipcSend("log", ...args);
	}

	protected _ipcInfo(message: string, ...args: any[]): void {
		this._ipcSend("info", message, ...args);
	}

	protected _ipcWarn(message: string, ...args: any[]): void {
		this._ipcSend("warn", message, ...args);
	}

	protected _ipcError(message: string, ...args: any[]): void {
		this._ipcSend("error", message, ...args);
	}

	protected _ipcEventId(id: string): string {
		return Config.IpcEventPrefix + id;
	}

	protected _onReceiveData(client: Client, data: Buffer): void {
		//this._ipcLog("server-> " + data + " from ", socket.remoteAddress + ":" + socket.remotePort);
		//this._ipcLog(data[0]);

		if (data != null) {
			client.data = Buffer.concat([client.data, data]);
		}
		const offset = client.dataOffset;
		const remain = client.data.length - offset;

		//this._ipcLog("ClientState: " + client.state);
		//this._ipcLog("offset: ", offset, ", remain: ", remain);
		
		switch (client.state) {
			case ClientState.Type:
				if (remain < 1) {
					return;
				}
				break;
			case ClientState.Length:
				if (remain < 4) {
					return;
				}
				break;
			case ClientState.Data:
				if (remain < client.dataLength) {
					return;
				}
				break;
		}

		switch (client.state) {
			case ClientState.Type:
				client.dataType = client.data[offset];
				client.dataOffset += 1;
				client.state = ClientState.Length;
				break;
			case ClientState.Length:
				client.dataLength = client.data.readUInt32LE(offset);
				if (client.dataLength > Config.MaxDataSize) {
					this._ipcWarn(
						"reached max data size", 
						client.socket.remoteAddress + ":" + client.socket.remotePort
					);
					//client.socket.destroy();
					client.socket.end();
					return;
				}
				client.dataOffset += 4;
				client.state = ClientState.Data;
				break;
			case ClientState.Data:
				this._onData(client);
				client.data = client.data.slice(offset + client.dataLength);
				client.dataOffset = 0;
				client.state = ClientState.Type;
				break;
		}

		this._onReceiveData(client, null);
	}

	protected _onData(client: Client): void {
		const message = client.getString();
		switch (client.dataType) {
			case DataType.Log:
				this._ipcLog(message);
				//const socket = client.socket;
				//socket.write("server -> Repeating: " + message + "\n");
				break;
			case DataType.Run:
				this._ipcSend("run", message);
				break;
			case DataType.Import:
				this._ipcSend("import", client.id, message);
				break;
			case DataType.Command:
				this._ipcSend("command", message);
				break;
		}
	}
}
