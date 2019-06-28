using System;
using System.IO;
using System.Text;

namespace SharpGlyph {
	public class BinaryReaderFont : BinaryReader {
		protected static readonly DateTime dateOrigin =
			new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public string FilePath = string.Empty;
		
		public BinaryReaderFont(Stream input)
			: base(input) { }

		public BinaryReaderFont(Stream input, Encoding encoding)
			: base(input, encoding) { }

		//public BinaryReaderFont(Stream input, Encoding encoding, bool leaveOpen)
		//	: base(input, encoding, leaveOpen) { }

		public long Position {
			get { return BaseStream.Position; }
			set { BaseStream.Position = value; }
		}

		public byte PeekByte() {
			byte value = base.ReadByte();
			BaseStream.Position--;
			return value;
		}

		public ushort PeekUInt16() {
			ushort value = ReadUInt16();
			BaseStream.Position -= 2;
			return value;
		}

		public override char ReadChar() {
			char value = base.ReadChar();
			return (char)(value << 8 | ((value >> 8) & 0xFF));
		}

		public override double ReadDouble() {
			byte[] data = base.ReadBytes(8);
			Array.Reverse(data);
			return BitConverter.ToDouble(data, 0);
		}

		public override short ReadInt16() {
			short value = base.ReadInt16();
			return (short)(value << 8 | ((value >> 8) & 0xFF));
		}

		public override int ReadInt32() {
			int value = base.ReadInt32();
			return (value << 24) |
				((value << 8) & 0xFF0000) |
				((value >> 8) & 0xFF00) |
				((value >> 24) & 0xFF);
		}

		public override long ReadInt64() {
			byte[] data = base.ReadBytes(8);
			Array.Reverse(data);
			return BitConverter.ToInt64(data, 0);
		}

		public override float ReadSingle() {
			byte[] data = base.ReadBytes(4);
			Array.Reverse(data);
			return BitConverter.ToSingle(data, 0);
		}

		public override ushort ReadUInt16() {
			ushort value = base.ReadUInt16();
			return (ushort)(value << 8 | value >> 8);
		}

		public int ReadUInt24() {
			byte[] data = base.ReadBytes(3);
			return data[2] |
				(data[1] << 8) |
				(data[0] << 16);
		}

		public override uint ReadUInt32() {
			uint value = base.ReadUInt32();
			return (value << 24) |
				((value << 8) & 0xFF0000u) |
				((value >> 8) & 0xFF00u) |
				(value >> 24);
		}

		public override ulong ReadUInt64() {
			byte[] data = base.ReadBytes(8);
			Array.Reverse(data);
			return BitConverter.ToUInt64(data, 0);
		}
		
		public uint ReadFixed() {
			return ReadUInt32();
		}

		public short ReadFWORD() {
			return ReadInt16();
		}

		public ushort ReadUFWORD() {
			return ReadUInt16();
		}

		public DateTime ReadDateTime() {
			ulong offset = ReadUInt64();
			return dateOrigin.AddSeconds(offset);
		}
		
		public string ReadTag() {
			byte[] data = base.ReadBytes(4);
			return Encoding.ASCII.GetString(data);
		}

		public short[] ReadInt16Array(int count) {
			short[] array = new short[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadInt16();
			}
			return array;
		}

		public ushort[] ReadUInt16Array(int count) {
			ushort[] array = new ushort[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadUInt16();
			}
			return array;
		}

		public int[] ReadUInt24Array(int count) {
			int[] array = new int[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadUInt24();
			}
			return array;
		}

		public int[] ReadInt32Array(int count) {
			int[] array = new int[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadInt32();
			}
			return array;
		}

		public uint[] ReadUInt32Array(int count) {
			uint[] array = new uint[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadUInt32();
			}
			return array;
		}

		public string[] ReadTagArray(int count) {
			string[] array = new string[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadTag();
			}
			return array;
		}

		public float ReadF2DOT14() {
			float value = ReadUInt16();
			return value / 0x4000;
		}
		
		public string ReadString(int count) {
			count = count > 0 ? count : 0;
			byte[] data = base.ReadBytes(count);
			return Encoding.ASCII.GetString(data);
		}
		
		public string ReadString(int count, Encoding encoding) {
			count = count > 0 ? count : 0;
			byte[] data = base.ReadBytes(count);
			return encoding.GetString(data);
		}

		public string ReadPascalString() {
			byte count = ReadByte();
			byte[] data = base.ReadBytes(count);
			return Encoding.ASCII.GetString(data);
		}

		public string[] ReadPascalStringArray(int count) {
			string[] array = new string[count];
			for (int i = 0; i < count; i++) {
				array[i] = ReadPascalString();
			}
			return array;
		}

		public int ReadCFFInt() {
			byte b0 = ReadByte();
			byte b1, b2, b3, b4;
			if (b0 >= 32 && b0 <= 246) {
				return b0 - 139;
			}
			if (b0 >= 247 && b0 <= 250) {
				b1 = ReadByte();
				return (b0 - 247) * 256 + b1 + 108;
			}
			if (b0 >= 251 && b0 <= 254) {
				b1 = ReadByte();
				return -(b0 - 251) * 256 - b1 - 108;
			}
			if (b0 == 28) {
				b1 = ReadByte();
				b2 = ReadByte();
				return b1 << 8 | b2;
			}
			if (b0 == 29) {
				b1 = ReadByte();
				b2 = ReadByte();
				b3 = ReadByte();
				b4 = ReadByte();
				return b1 << 24 | b2 << 16 | b3 << 8 | b4;
			}
			return b0;
		}

		public double ReadCFFNumber() {
			byte b0 = ReadByte();
			byte b1, b2, b3, b4;
			if (b0 >= 32 && b0 <= 246) {
				return b0 - 139;
			}
			if (b0 >= 247 && b0 <= 250) {
				b1 = ReadByte();
				return (b0 - 247) * 256 + b1 + 108;
			}
			if (b0 >= 251 && b0 <= 254) {
				b1 = ReadByte();
				return -(b0 - 251) * 256 - b1 - 108;
			}
			if (b0 == 28) {
				b1 = ReadByte();
				b2 = ReadByte();
				return b1 << 8 | b2;
			}
			if (b0 == 29) {
				b1 = ReadByte();
				b2 = ReadByte();
				b3 = ReadByte();
				b4 = ReadByte();
				return b1 << 24 | b2 << 16 | b3 << 8 | b4;
			}
			if (b0 == 30) {
				string[] nibbles = {
					"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
					".", "E", "E-", "<reserved>", "-", "end",
				};
				StringBuilder data = new StringBuilder();
				while (true) {
					byte b = ReadByte();
					int n = b >> 4;
					if (n == 0xF) {
						break;
					}
					data.Append(nibbles[n]);
					n = b & 0xF;
					if (n == 0xF) {
						break;
					}
					data.Append(nibbles[n]);
				}
				return double.Parse(data.ToString());
			}
			return b0;
		}

		public bool TableChecksum(TableRecord record) {
			if (record == null) {
				return false;
			}
			BaseStream.Position = record.offset;
			uint length = ((record.length + 3u) & ~3u) >> 2;
			//Console.WriteLine(length.ToString("X"));
			uint sum = 0;
			for (int i = 0; i < length; i++) {
				sum += ReadUInt32();
			}
			//Console.WriteLine("record.length - length * 4 " + (record.length - length * 4));
			if (record.tableTag == "head") {
				BaseStream.Position = record.offset + 8;
				uint checkSumAdjustment = ReadUInt32();
				sum -= checkSumAdjustment;
				//if (sum != checkSumAdjustment) {
				//	Console.WriteLine("incorrect " + sum.ToString("X") + ", " + checkSumAdjustment.ToString("X"));
				//}
				//return sum == checkSumAdjustment;
			}
			if (sum != record.checkSum) {
				Console.WriteLine("incorrect " + sum.ToString("X") + ", " + record.checkSum.ToString("X"));
			}
			return sum == record.checkSum;
		}
	}
}
