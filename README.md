# Socketron

## Introducing

This library is intended for some kind of use cases:

* Add the control interfaces on your already created apps.
* Create the electron app from scratch, if you want to write besides JavaScript.

Currentry, this library has only C# interface.

## Current status

Under construction (not stable).

Please don't use at the production code.

## How to use

* Download the entire this repository.
* $ npm i
* $ node_modules/.bin/electron .
  * windows: > node_modules\\.bin\\electron .
  * if you use the vscode: type F5 key intead
* Open "interfaces/cs/Socketron.sln"
* Run on the VisualStudio to see the example

## Mechanisms

Socketron runs like the Adobe AIR or X window system.
It has a server and wait the client connection.

The socketron client library try to connect the server via socket. After connected, communicate between server and client that uses the JSON text.

The JSON text includes some informations, e.g. JavaScript function name, arguments, environment selector (browser or renderer).

The server simply evaluate the functions and arguments in the received JSON text that is implemented in the JavaScript Function constructor (same the eval() method).

If specefied environment selector in the JSON text, send the text to the electron "renderer" process from node "browser" process as needed.

The client does not have any JavaScript object instances. That instances only have the Node.js side. When client is disconnected, all instances is removed from the Node.js.

In other words, the client is only wrapped JavaScript code in the client side programming languages. For example, console.log() is called in the client then the client creates pieces of JavaScript code, and send it.

The piece is like next: "this.getObject([number]).log('test')"

The server immediately evaluate the piece of code when received.

## Limitations

* Slow: This library runs like the interpreters that can not run native speed.
