using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 8: mixed 16-bit and 32-bit coverage.
	/// </summary>
	public class CmapSubtable8 : CmapSubtable {
		/// <summary>
		/// Reserved; set to 0.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// Byte length of this subtable (including the header).
		/// </summary>
		public uint length;

		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public uint language;

		/// <summary>
		/// Tightly packed array of bits (8K bytes total)
		/// indicating whether the particular 16-bit (index)
		/// value is the start of a 32-bit character code.
		/// </summary>
		public byte[] is32;

		/// <summary>
		/// Number of groupings which follow.
		/// </summary>
		public uint numGroups;

		/// <summary>
		/// Array of SequentialMapGroup records.
		/// </summary>
		public SequentialMapGroup[] groups;

		public static new CmapSubtable8 Read(BinaryReaderFont reader) {
			CmapSubtable8 value = new CmapSubtable8 {
				format = reader.ReadUInt16(),
				reserved = reader.ReadUInt16(),
				length = reader.ReadUInt32(),
				language = reader.ReadUInt32(),
				is32 = reader.ReadBytes(8192),
				numGroups = reader.ReadUInt32()
			};
			value.groups = SequentialMapGroup.ReadArray(reader, value.numGroups);
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			for (int i = 0; i < numGroups; i++) {
				SequentialMapGroup group = groups[i];
				ushort glyphId = (ushort)group.startGlyphID;
				int endCharCode = (int)group.endCharCode;
				for (int n = (int)group.startCharCode; n <= endCharCode; n++) {
					table.Add(n, glyphId++);
				}
			}
			return table;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"reserved\": \"{0}\",\n", reserved);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.AppendFormat("\t\"numGroups\": \"{0}\",\n", numGroups);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
