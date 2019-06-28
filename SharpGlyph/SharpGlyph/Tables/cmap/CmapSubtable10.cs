using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 10: Trimmed array.
	/// </summary>
	public class CmapSubtable10 : CmapSubtable {
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
		/// First character code covered.
		/// </summary>
		public uint startCharCode;

		/// <summary>
		/// Number of character codes covered.
		/// </summary>
		public uint numChars;

		/// <summary>
		/// Array of glyph indices for the character codes covered.
		/// </summary>
		public ushort[] glyphs;

		public static new CmapSubtable10 Read(BinaryReaderFont reader) {
			CmapSubtable10 value = new CmapSubtable10();
			value.format = reader.ReadUInt16();
			value.reserved = reader.ReadUInt16();
			value.length = reader.ReadUInt32();
			value.language = reader.ReadUInt32();
			value.startCharCode = reader.ReadUInt32();
			value.numChars = reader.ReadUInt32();
			value.glyphs = reader.ReadUInt16Array((int)value.numChars);
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			int charCode = (int)startCharCode;
			for (int i = 0; i < numChars; i++) {
				table.Add(charCode + i, glyphs[i]);
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
			builder.AppendFormat("\t\"startCharCode\": \"{0}\",\n", startCharCode);
			builder.AppendFormat("\t\"numChars\": \"{0}\",\n", numChars);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
