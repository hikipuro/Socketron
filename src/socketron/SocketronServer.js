const net = require("net");
const { EventEmitter } = require("events");

const { DataType, ReadState } = require("./Common");
const Client = require("./Client");
const Clients = require("./Clients");
const SocketronData = require("./SocketronData");

class SocketronServer extends EventEmitter {
	constructor() {
		super();
		this._server = null;
		this._clients = new Clients();
	}

	listen(port) {
		this._server = net.createServer(
			this._onConnect.bind(this)
		);
		this._server.listen(port);
	}
	
	broadcast(message, sender = null) {
		this._clients.broadcast(message, sender);
	}
	
	findClientById(id) {
		return this._clients.findClientById(id);
	}

	_onConnect(socket) {
		const client = new Client(socket);
		this._clients.add(client);
		this.emit("log", client, "client connected");

		socket.on("error", (err) => {
			this.emit("error", client, err);
			this._clients.remove(client);
			client.close();
		});
		socket.on("timeout", () => {
			this.emit("timeout", client);
			client.close();
		});
		socket.on("data", (data) => {
			this._onData(client, data);
		});
		socket.on("close", () => {
			this.emit("log", client, "client closed");
			this._clients.remove(client);
			client.close();
		});
		socket.on("end", () => {
			this._clients.remove(client);
			client.close();
		});
		this.emit("connect", client);
	}

	_onData(client, data) {
		const payload = client.payload;
		if (data != null) {
			//console.log(typeof data);
			payload.data = Buffer.concat([payload.data, data]);
		}
		const offset = payload.dataOffset;
		const remain = payload.data.length - offset;

		switch (payload.state) {
			case ReadState.Type:
				if (remain < 1) {
					return;
				}
				break;
			case ReadState.CommandLength:
				switch (payload.dataType) {
					case DataType.Text16:
						if (remain < 2) {
							return;
						}
						break;
					case DataType.Text32:
						if (remain < 4) {
							return;
						}
						break;
				}
				break;
			case ReadState.Command:
				if (remain < payload.dataLength) {
					return;
				}
				break;
		}

		switch (payload.state) {
			case ReadState.Type:
				payload.dataType = payload.data[offset];
				payload.dataOffset += 1;
				payload.state = ReadState.CommandLength;
				break;
			case ReadState.CommandLength:
				switch (payload.dataType) {
					case DataType.Text16:
						payload.dataLength = payload.data.readUInt16LE(offset);
						payload.dataOffset += 2;
						break;
					case DataType.Text32:
						payload.dataLength = payload.data.readUInt32LE(offset);
						payload.dataOffset += 4;
						break;
				}
				payload.state = ReadState.Command;
				break;
			case ReadState.Command:
				if (payload.dataType === DataType.Text16) {
					const text = payload.getStringData();
					this.emit("data", client, JSON.parse(text));
				}
				if (payload.dataType === DataType.Text32) {
					const text = payload.getStringData();
					this.emit("data", client, JSON.parse(text));
				}
				var newData = payload.data.slice(offset + payload.dataLength);
				//delete payload.data;
				payload.data = newData;
				payload.dataOffset = 0;
				payload.state = ReadState.Type;
				break;
		}

		this._onData(client, null);
	}
}

module.exports = SocketronServer;
