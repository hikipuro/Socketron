﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Socketron {
	/// <summary>
	/// Local buffer object.
	/// <para>
	/// This object can be used in two ways:
	/// 1. Create a binary object in the local memory.
	/// 2. Exchange a Buffer object of the Node API between local and remote.
	/// </para>
	/// </summary>
	public class LocalBuffer {
		// TODO: implement Buffer methods
		protected MemoryStream _data;

		public LocalBuffer() {
			_data = new MemoryStream(1024);
		}

		public static LocalBuffer Alloc(long size) {
			LocalBuffer buffer = new LocalBuffer();
			buffer._data.SetLength(size);
			return buffer;
		}

		public static LocalBuffer From(object obj) {
			return From(new JsonObject(obj));
		}

		public static LocalBuffer From(JsonObject json) {
			if (json == null) {
				return null;
			}
			if (json["type"] as string != "Buffer") {
				return null;
			}
			object[] data = json["data"] as object[];
			LocalBuffer buffer = new LocalBuffer();
			foreach (object item in data) {
				buffer.WriteUInt8((byte)(int)item);
			}
			return buffer;
		}

		public static LocalBuffer From(Buffer buffer) {
			if (buffer == null) {
				return null;
			}
			return From(buffer.toJSON());
		}

		public static LocalBuffer From(string str, Encoding encoding = null) {
			LocalBuffer buffer = new LocalBuffer();
			buffer.Write(str, encoding);
			return buffer;
		}

		public byte this[uint i] {
			get { return _data.GetBuffer()[i]; }
		}

		public int Length {
			get { return (int)_data.Length; }
		}

		public void Dispose() {
			_data.Dispose();
		}

		public void Save(string path) {
			byte[] bytes = ToByteArray();
			File.WriteAllBytes(path, bytes);
		}

		public void Write(byte[] bytes) {
			_data.Write(bytes, 0, bytes.Length);
		}

		public void Write(byte[] bytes, int offset, int length) {
			_data.Write(bytes, offset, length);
		}

		public void Write(LocalBuffer buffer) {
			byte[] bytes = buffer._data.GetBuffer();
			_data.Write(bytes, 0, bytes.Length);
		}

		public void Write(string str, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			byte[] bytes = encoding.GetBytes(str);
			Write(bytes, 0, bytes.Length);
		}

		public void WriteUInt8(byte value) {
			_data.WriteByte(value);
		}

		public void WriteUInt16LE(ushort value) {
			_data.WriteByte((byte)(value & 0xFF));
			_data.WriteByte((byte)(value >> 8 & 0xFF));
		}

		public void WriteUInt32LE(uint value) {
			_data.WriteByte((byte)(value & 0xFF));
			_data.WriteByte((byte)(value >> 8 & 0xFF));
			_data.WriteByte((byte)(value >> 16 & 0xFF));
			_data.WriteByte((byte)(value >> 24 & 0xFF));
		}

		public byte ReadUInt8(uint offset) {
			return _data.GetBuffer()[offset];
		}

		public ushort ReadUInt16LE(uint offset) {
			byte[] buffer = _data.GetBuffer();
			ushort result = buffer[offset];
			result |= (ushort)(buffer[offset + 1] << 8);
			return result;
		}

		public uint ReadUInt32LE(uint offset) {
			byte[] buffer = _data.GetBuffer();
			uint result = buffer[offset];
			result |= (uint)(buffer[offset + 1] << 8);
			result |= (uint)(buffer[offset + 2] << 16);
			result |= (uint)(buffer[offset + 3] << 24);
			return result;
		}

		public LocalBuffer Slice(uint offset) {
			uint length = (uint)_data.Length - offset;
			byte[] data = new byte[length];
			long position = _data.Position;
			_data.Position = offset;
			_data.Read(data, 0, (int)length);
			_data.Position = position;

			LocalBuffer buffer = new LocalBuffer();
			buffer.Write(data);
			return buffer;
		}

		public Buffer ToBuffer() {
			return Buffer.from(this);
		}

		public byte[] ToByteArray() {
			byte[] bytes = new byte[Length];
			long position = _data.Position;
			_data.Position = 0;
			_data.Read(bytes, 0, Length);
			_data.Position = position;
			return bytes;
		}

		public string ToString(Encoding encoding, int start, int end) {
			return encoding.GetString(
				_data.GetBuffer(), start, end - start
			);
		}

		public string Stringify() {
			byte[] bytes = ToByteArray();
			JsonObject json = new JsonObject() {
				["type"] = "Buffer",
				["data"] = new List<byte>(bytes)
			};
			return json.Stringify();
		}
	}
}
