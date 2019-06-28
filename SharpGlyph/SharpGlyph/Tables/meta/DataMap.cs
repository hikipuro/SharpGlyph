using System;
using System.Text;

namespace SharpGlyph {
	public class DataMap {
		/// <summary>
		/// A tag indicating the type of metadata.
		/// </summary>
		public string tag;

		/// <summary>
		/// Offset in bytes from the beginning of the metadata table to the data for this tag.
		/// </summary>
		public uint dataOffset;

		/// <summary>
		/// Length of the data, in bytes.
		/// The data is not required to be padded to any byte boundary.
		/// </summary>
		public uint dataLength;

		public byte[] data;

		public static DataMap[] ReadArray(BinaryReaderFont reader, uint count) {
			DataMap[] array = new DataMap[count];
			for (uint i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static DataMap Read(BinaryReaderFont reader) {
			return new DataMap {
				tag = reader.ReadTag(),
				dataOffset = reader.ReadUInt32(),
				dataLength = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"tag\": \"{0}\",\n", tag);
			builder.AppendFormat("\t\"dataOffset\": 0x{0:X8},\n", dataOffset);
			builder.AppendFormat("\t\"dataLength\": 0x{0:X8}\n", dataLength);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
