using System;
using System.Text;

namespace SharpGlyph {
	public class CFFIndex {
		/// <summary>
		/// Number of objects stored in INDEX.
		/// </summary>
		public ushort count;

		/// <summary>
		/// Offset array element size.
		/// </summary>
		public byte offSize;

		/// <summary>
		/// Offset array (from byte preceding object data).
		/// </summary>
		//public int[] offset;

		protected long position;

		public static CFFIndex Read(BinaryReaderFont reader) {
			CFFIndex value = new CFFIndex();
			value.count = reader.ReadUInt16();
			if (value.count == 0) {
				//value.offset = new int[0];
				return value;
			}
			value.offSize = reader.ReadByte();
			value.position = reader.Position;

			/*
			switch (value.offSize) {
				case 1:
					value.offset = Array.ConvertAll(
						reader.ReadBytes(value.count + 1),
						new Converter<byte, int>((a) => {
							return a;
						})
					);
					break;
				case 2:
					value.offset = Array.ConvertAll(
						reader.ReadUInt16Array(value.count + 1),
						new Converter<ushort, int>((a) => {
							return a;
						})
					);
					break;
				case 3:
					value.offset = reader.ReadUInt24Array(value.count + 1);
					break;
				case 4:
					value.offset = reader.ReadInt32Array(value.count + 1);
					break;
			}
			*/
			//value.offset = reader.ReadBytes(value.offSize + 1);
			//for (int i = 0; i < value.offset.Length; i += 2) {
			//	int length = value.offset[i + 1] - value.offset[i];
			//	value.data = reader.ReadBytes(length);
			//}
			return value;
		}

		public int[] ReadAllOffsets(BinaryReaderFont reader) {
			if (count == 0) {
				return new int[0];
			}
			switch (offSize) {
				case 1:
					return Array.ConvertAll(
						reader.ReadBytes(count + 1),
						new Converter<byte, int>((a) => {
							return a;
						})
					);
				case 2:
					return Array.ConvertAll(
						reader.ReadUInt16Array(count + 1),
						new Converter<ushort, int>((a) => {
							return a;
						})
					);
				case 3:
					return reader.ReadUInt24Array(count + 1);
				case 4:
					return reader.ReadInt32Array(count + 1);
			}
			return new int[0];
		}

		public void ReadOffset(BinaryReaderFont reader, int index, out int offset0, out int offset1) {
			if (index < 0 || index >= count - 1) {
				offset0 = 0;
				offset1 = 0;
				return;
			}
			switch (offSize) {
				case 1:
					reader.Position = position + index;
					offset0 = reader.ReadByte();
					offset1 = reader.ReadByte();
					return;
				case 2:
					reader.Position = position + index * 2;
					offset0 = reader.ReadUInt16();
					offset1 = reader.ReadUInt16();
					return;
				case 3:
					reader.Position = position + index * 3;
					offset0 = reader.ReadUInt24();
					offset1 = reader.ReadUInt24();
					return;
				case 4:
					reader.Position = position + index * 4;
					offset0 = reader.ReadInt32();
					offset1 = reader.ReadInt32();
					return;
			}
			offset0 = 0;
			offset1 = 0;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"count\": {0},\n", count);
			builder.AppendFormat("\t\"offSize\": {0},\n", offSize);
			/*
			builder.AppendLine("\t\"offset\": [");
			if (offset != null) {
				for (int i = 0; i < offset.Length; i++) {
					builder.AppendFormat("\t\t{0},\n", offset[i]);
				}
				if (offset.Length > 0) {
					builder.Remove(builder.Length - 2, 1);
				}
			}
			builder.AppendLine("\t]");
			*/
			/*
			builder.AppendLine("\t\"data\": [");
			for (int i = 0; i < data.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X2},\n", data[i]);
			}
			if (data.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
