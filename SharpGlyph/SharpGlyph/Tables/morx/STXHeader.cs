using System;
using System.Text;

namespace SharpGlyph {
	public class STXHeader {
		/// <summary>
		/// Number of classes, which is the number of 16-bit entry
		/// indices in a single line in the state array.
		/// </summary>
		public uint nClasses;

		/// <summary>
		/// Offset from the start of this state table header
		/// to the start of the class table.
		/// </summary>
		public uint classTableOffset;

		/// <summary>
		/// Offset from the start of this state table header
		/// to the start of the state array.
		/// </summary>
		public uint stateArrayOffset;

		/// <summary>
		/// Offset from the start of this state table header
		/// to the start of the entry table.
		/// </summary>
		public uint entryTableOffset;

		public static STXHeader Read(BinaryReaderFont reader, TableRecord record) {
			return new STXHeader {
				nClasses = reader.ReadUInt32(),
				classTableOffset = reader.ReadUInt32(),
				stateArrayOffset = reader.ReadUInt32(),
				entryTableOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"nClasses\": {0},\n", nClasses);
			builder.AppendFormat("\t\"classTableOffset\": 0x{0:X8},\n", classTableOffset);
			builder.AppendFormat("\t\"stateArrayOffset\": 0x{0:X8},\n", stateArrayOffset);
			builder.AppendFormat("\t\"entryTableOffset\": 0x{0:X8},\n", entryTableOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
