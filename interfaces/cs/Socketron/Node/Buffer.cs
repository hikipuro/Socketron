using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Buffer object of the Node API.
	/// <para>
	/// Buffer instances will be created in the remote memory.
	/// LocalBuffer.From() static method can be used
	/// when you should copy a Buffer object to the local memory.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Buffer : JSObject, IDisposable {
		public class Encodings {
			public const string ascii = "ascii";
			public const string utf8 = "utf8";
			public const string utf16le = "utf16le";
			public const string ucs2 = "ucs2";
			public const string base64 = "base64";
			public const string latin1 = "latin1";
			public const string binary = "binary";
			public const string hex = "hex";
		}

		public class constants {
			public static long MAX_LENGTH {
				get {
					string script = "return this.require('buffer').constants.MAX_LENGTH;";
					return SocketronClient.ExecuteBlocking<long>(script);
				}
			}
			public static long MAX_STRING_LENGTH {
				get {
					string script = "return this.require('buffer').constants.MAX_STRING_LENGTH;";
					return SocketronClient.ExecuteBlocking<long>(script);
				}
			}
		}

		public Buffer() {
		}

		public Buffer(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		public byte this[int index] {
			get {
				string script = ScriptBuilder.Build(
					"return {0}[{1}];",
					Script.GetObject(API.id),
					index
				);
				return API._ExecuteBlocking<byte>(script);
			}
			set {
				string script = ScriptBuilder.Build(
					"{0}[{1}] = {2};",
					Script.GetObject(API.id),
					index,
					value
				);
				API.ExecuteJavaScript(script);
			}
		}

		public static Buffer alloc(long size) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.alloc({0});",
					"return {1};"
				),
				size,
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer alloc(long size, string fill, string encoding = "utf8") {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.alloc({0},{1},{2});",
					"return {3};"
				),
				size,
				fill.Escape(),
				encoding.Escape(),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer allocUnsafe(long size) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.allocUnsafe({0});",
					"return {1};"
				),
				size,
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer allocUnsafeSlow(long size) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.allocUnsafeSlow({0});",
					"return {1};"
				),
				size,
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static long byteLength(string value, string encoding = "utf8") {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return Buffer.byteLength({0},{1});"
				),
				value.Escape(),
				encoding
			);
			return SocketronClient.ExecuteBlocking<long>(script);
		}

		public static long byteLength(Buffer value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return Buffer.byteLength({0});"
				),
				Script.GetObject(value.API.id)
			);
			return SocketronClient.ExecuteBlocking<long>(script);
		}

		public static long compare(Buffer buf1, Buffer buf2) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return Buffer.compare({0},{1});"
				),
				Script.GetObject(buf1.API.id),
				Script.GetObject(buf2.API.id)
			);
			return SocketronClient.ExecuteBlocking<long>(script);
		}

		public static Buffer concat(params Buffer[] list) {
			if (list == null) {
				return null;
			}
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.concat([{0}]);",
					"return {1};"
				),
				Script.GetObjectList(list),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer from(params object[] array) {
			if (array == null) {
				return null;
			}
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.from([{0}]);",
					"return {1};"
				),
				Script.GetObjectList(array),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer from(Buffer buffer) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.from({0});",
					"return {1};"
				),
				Script.GetObject(buffer.API.id),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer from(LocalBuffer buffer) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.from({0});",
					"return {1};"
				),
				buffer.Stringify(),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static Buffer from(string str, string encoding = "utf8") {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = Buffer.from({0},{1});",
					"return {2};"
				),
				str.Escape(),
				encoding.Escape(),
				Script.AddObject("buf")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new Buffer(client, result);
		}

		public static bool isBuffer(JSObject obj) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return Buffer.isBuffer({0});"
				),
				Script.GetObject(obj.API.id)
			);
			return SocketronClient.ExecuteBlocking<bool>(script);
		}

		public static bool isEncoding(string encoding) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return Buffer.isEncoding({0});"
				),
				encoding.Escape()
			);
			return SocketronClient.ExecuteBlocking<bool>(script);
		}

		public static int poolSize {
			get {
				string script = "return Buffer.poolSize;";
				return SocketronClient.ExecuteBlocking<int>(script);
			}
		}

		/*
		public Buffer buffer {
			get {
				// TODO: implement this
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var b = {0}.buffer;",
						"return {1};"
					),
					Script.GetObject(id),
					Script.AddObject("b")
				);
				int result = API.ExecuteJavaScriptBlocking<int>(script);
				return new Buffer(_socketron, result);
			}
		}
		//*/

		public int compare(Buffer target) {
			string script = ScriptBuilder.Build(
				"return {0}.compare({1});",
				Script.GetObject(API.id),
				Script.GetObject(target.API.id)
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int copy(Buffer target) {
			string script = ScriptBuilder.Build(
				"return {0}.copy({1});",
				Script.GetObject(API.id),
				Script.GetObject(target.API.id)
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void entries() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public bool equals(Buffer otherBuffer) {
			string script = ScriptBuilder.Build(
				"return {0}.equals({1});",
				Script.GetObject(API.id),
				Script.GetObject(otherBuffer.API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public Buffer fill(string value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.fill({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				value.Escape(),
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public bool includes(string value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.includes({1});"
				),
				Script.GetObject(API.id),
				value.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public int indexOf(string value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.indexOf({1});"
				),
				Script.GetObject(API.id),
				value.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void keys() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public int lastIndexOf(string value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.lastIndexOf({1});"
				),
				Script.GetObject(API.id),
				value.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int length {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.length;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<int>(script);
			}
		}

		public Buffer parent {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var b = {0}.parent;",
						"return {1};"
					),
					Script.GetObject(API.id),
					Script.AddObject("b")
				);
				int result = API._ExecuteBlocking<int>(script);
				return new Buffer(API.client, result);
			}
		}

		public double readDoubleBE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readDoubleBE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<double>(script);
		}

		public double readDoubleLE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readDoubleLE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<double>(script);
		}

		public float readFloatBE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readFloatBE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<float>(script);
		}

		public float readFloatLE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readFloatLE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<float>(script);
		}

		public sbyte readInt8(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readFloatLE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<sbyte>(script);
		}

		public short readInt16BE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readInt16BE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<short>(script);
		}

		public short readInt16LE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readInt16LE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<short>(script);
		}

		public int readInt32BE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readInt32BE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int readInt32LE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readInt32LE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public long readIntBE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readIntBE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<long>(script);
		}

		public long readIntLE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readIntLE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<long>(script);
		}

		public byte readUInt8(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUInt8({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<byte>(script);
		}

		public ushort readUInt16BE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUInt16BE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<ushort>(script);
		}

		public ushort readUInt16LE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUInt16LE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<ushort>(script);
		}

		public uint readUInt32BE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUInt32BE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<uint>(script);
		}

		public uint readUInt32LE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUInt32LE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<uint>(script);
		}

		public ulong readUIntBE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUIntBE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<ulong>(script);
		}

		public ulong readUIntLE(int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.readUIntLE({1},{2});",
				Script.GetObject(API.id),
				offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<ulong>(script);
		}

		public Buffer slice() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.slice();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public Buffer slice(int start) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.slice({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				start,
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public Buffer slice(int start, int end) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.slice({1},{2});",
					"return {3};"
				),
				Script.GetObject(API.id),
				start,
				end,
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public Buffer swap16() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.swap16();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public Buffer swap32() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.swap32();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public Buffer swap64() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var b = {0}.swap64();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("b")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		public JsonObject toJSON() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.toJSON();"
				),
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		public string toString(string encoding = "utf8", int start = 0) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.toString({1},{2});"
				),
				Script.GetObject(API.id),
				encoding.Escape(),
				start
			);
			return API._ExecuteBlocking<string>(script);
		}

		public string toString(string encoding, int start, int end) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.toString({1},{2});"
				),
				Script.GetObject(API.id),
				encoding.Escape(),
				start,
				end
			);
			return API._ExecuteBlocking<string>(script);
		}

		public void values() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public int write(string str, int offset = 0) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.write({1},{2});"
				),
				Script.GetObject(API.id),
				str.Escape(),
				offset
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int write(string str, int offset, int length, string encoding = "utf8") {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.write({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				str.Escape(),
				offset,
				length,
				encoding.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeDoubleBE(double value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeDoubleBE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeDoubleLE(double value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeDoubleLE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeFloatBE(float value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeFloatBE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeFloatLE(float value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeFloatLE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeInt8(int value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeInt8({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeInt16BE(int value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeInt16BE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeInt16LE(int value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeInt16LE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeInt32BE(int value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeInt32BE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeInt32LE(int value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeInt32LE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeIntBE(long value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeIntBE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeIntLE(long value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeIntLE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUInt8(uint value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUInt8({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUInt16BE(uint value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUInt16BE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUInt16LE(uint value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUInt16LE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUInt32BE(uint value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUInt32BE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUInt32LE(uint value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUInt32LE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUIntBE(ulong value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUIntBE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public int writeUIntLE(ulong value, int offset, bool noAssert = false) {
			string script = ScriptBuilder.Build(
				"return {0}.writeUIntLE({1},{2},{3});",
				Script.GetObject(API.id),
				value, offset, noAssert.Escape()
			);
			return API._ExecuteBlocking<int>(script);
		}

		public static int INSPECT_MAX_BYTES {
			get {
				string script = "return this.require('buffer').INSPECT_MAX_BYTES;";
				return SocketronClient.ExecuteBlocking<int>(script);
			}
		}

		public static int kMaxLength {
			get {
				string script = "return this.require('buffer').kMaxLength;";
				return SocketronClient.ExecuteBlocking<int>(script);
			}
		}

		public static Buffer transcode(Buffer source, string fromEnc, string toEnc) {
			if (source == null) {
				return null;
			}
			string script = ScriptBuilder.Build(
				"return this.require('buffer').transcode({0},{1},{2});",
				Script.GetObject(source.API.id),
				fromEnc.Escape(),
				toEnc.Escape()
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new Buffer(SocketronClient.GetCurrent(), result);
		}
	}
}
