using System;
using System.Collections.Generic;

namespace SharpGlyph {
	public class CFFStream {
		Stack<byte[]> streams;
		Stack<int> pcs;
		byte[] stream;
		int pc;

		public int Length {
			get {
				if (stream == null) {
					return 0;
				}
				return stream.Length;
			}
		}

		public int Position {
			get { return pc; }
		}

		public int Depth {
			get { return streams.Count; }
		}

		public CFFStream() {
			streams = new Stack<byte[]>(10);
			pcs = new Stack<int>(10);
			stream = null;
			pc = 0;
		}

		public void Clear() {
			streams.Clear();
			pcs.Clear();
		}

		public void Push(byte[] bytes) {
			streams.Push(stream);
			pcs.Push(pc);
			stream = bytes;
			pc = 0;
		}

		public void Pop() {
			stream = streams.Pop();
			pc = pcs.Pop();
		}

		public bool HasNext() {
			return pc + 1 < stream.Length;
		}

		public byte ReadByte() {
			if (pc >= stream.Length) {
				return 0;
			}
			return stream[pc++];
		}

		public byte[] ReadBytes(int count) {
			byte[] data = new byte[count];
			for (int i = 0; i < count; i++) {
				data[i] = ReadByte();
			}
			return data;
		}

		public short ReadInt16() {
			return (short)(
				(ReadByte() << 8) |
				ReadByte()
			);
		}

		public int ReadInt32() {
			return (
				(ReadByte() << 24) |
				(ReadByte() << 16) |
				(ReadByte() << 8) |
				ReadByte()
			);
		}
	}
}
