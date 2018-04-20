﻿using System;
using System.Text;

namespace Socketron {
	internal class DataCreator {
		public Encoding Encoding = Encoding.UTF8;
		public ushort _sequenceId = 0;

		public ushort SequenceId {
			get { return _sequenceId; }
		}

		public byte[] Create(DataType type, byte[] bytes) {
			_sequenceId++;
			if (_sequenceId >= ushort.MaxValue) {
				_sequenceId = 0;
			}

			uint length = (uint)bytes.Length;
			byte[] data = new byte[length + 7];
			WriteUint8(data, 0, (byte)type);
			WriteUint16LE(data, 1, _sequenceId);
			WriteUint32LE(data, 3, length);
			Array.Copy(bytes, 0, data, 7, length);
			return data;
		}

		public byte[] Create(DataType type, string message) {
			byte[] bytes = Encoding.GetBytes(message);
			return Create(type, bytes);
		}

		void WriteUint8(byte[] dest, int index, byte data) {
			dest[index] = data;
		}

		void WriteUint16LE(byte[] dest, int index, uint data) {
			dest[index + 0] = (byte)(data & 0xFF);
			dest[index + 1] = (byte)((data >> 8) & 0xFF);
		}

		void WriteUint32LE(byte[] dest, int index, uint data) {
			dest[index + 0] = (byte)(data & 0xFF);
			dest[index + 1] = (byte)((data >> 8) & 0xFF);
			dest[index + 2] = (byte)((data >> 16) & 0xFF);
			dest[index + 3] = (byte)((data >> 24) & 0xFF);
		}
	}
}