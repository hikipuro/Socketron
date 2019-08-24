# Socketron

## Introducing

This library is intended for some kind of use cases:

* Add external control interfaces on your already created electron apps.
* Create electron app from scratch, if you want to write besides JavaScript.

Currentry, this library has only C# interface.

## Current status

Under construction (not stable).

Please don't use at production code.

## How to use

* Download entire this repository.
* $ npm i
* $ node_modules/.bin/electron .
  * windows: > node_modules\\.bin\\electron .
  * if you use vscode: type F5 key intead
* Open "interfaces/cs/Socketron.sln"
* Run on Visual Studio

## Mechanisms

Socketron runs like Adobe AIR or X window system.
It has a server and wait client connections.

Socketron client library try to connect the server via socket. After connected, communicate between server and client that uses JSON text.

JSON text includes some informations, e.g. JavaScript function name, arguments, environment selector (browser or renderer).

Server simply evaluate functions and arguments in the received JSON text that is implemented in JavaScript Function constructor (same eval() method).

If specefied environment selector in the JSON text, send text to electron's "renderer" process from node "browser" process as needed.

Client does not have any JavaScript object instances. That instances only have Node.js side. When client is disconnected, all instances is removed from Node.js.

In other words, client is only wrapped JavaScript code in client side programming languages. For example, console.log() is called in client then client creates pieces of JavaScript code, and send it.

Piece is like next: "this.getObject([number]).log('test')"

Server immediately evaluate the piece of code when received.

## Limitations

* Slow: This library runs like interpreters that can not run native speed.
