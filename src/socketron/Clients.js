class Clients {
	constructor() {
		this._clients = [];
		this._clientIds = {};
	}

	add(client) {
		this._clients.push(client);
		this._clientIds[client.id] = client;
	}

	remove(client) {
		const index = this._clients.indexOf(client);
		this._clients.splice(index, 1);
		this._clientIds[client.id] = null;
	}
	
	broadcast(message, sender = null) {
		this._clients.forEach((client) => {
			if (client === sender) {
				return;
			}
			client.socket.write(message);
		});
	}

	findClientById(id) {
		return this._clientIds[id];
	}
}

module.exports = Clients;
