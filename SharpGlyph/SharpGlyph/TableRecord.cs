using System.Text;

namespace SharpGlyph {
	public class TableRecord {
		/// <summary>
		/// Table identifier.
		/// </summary>
		public string tableTag;

		/// <summary>
		/// CheckSum for this table.
		/// </summary>
		public uint checkSum;

		/// <summary>
		/// Offset from beginning of TrueType font file.
		/// </summary>
		public uint offset;

		/// <summary>
		/// Length of this table.
		/// </summary>
		public uint length;

		public static TableRecord[] ReadArray(BinaryReaderFont reader, int count) {
			TableRecord[] array = new TableRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static TableRecord Read(BinaryReaderFont reader) {
			return new TableRecord {
				tableTag = reader.ReadTag(),
				checkSum = reader.ReadUInt32(),
				offset = reader.ReadUInt32(),
				length = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"tableTag\": \"{0}\",\n", tableTag);
			builder.AppendFormat("\t\"checkSum\": 0x{0:X8},\n", checkSum);
			builder.AppendFormat("\t\"offset\": 0x{0:X8},\n", offset);
			builder.AppendFormat("\t\"length\": 0x{0:X8}\n", length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
