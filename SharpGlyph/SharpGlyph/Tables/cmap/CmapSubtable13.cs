using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 13: Many-to-one range mappings.
	/// </summary>
	public class CmapSubtable13 : CmapSubtable {
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
		/// Number of character codes covered.
		/// </summary>
		public uint numGroups;

		/// <summary>
		/// Array of glyph indices for the character codes covered.
		/// </summary>
		public ConstantMapGroup[] glyphs;
		
		public static new CmapSubtable13 Read(BinaryReaderFont reader) {
			CmapSubtable13 value = new CmapSubtable13 {
				format = reader.ReadUInt16(),
				reserved = reader.ReadUInt16(),
				length = reader.ReadUInt32(),
				language = reader.ReadUInt32(),
				numGroups = reader.ReadUInt32()
			};
			value.glyphs = ConstantMapGroup.ReadArray(reader, value.numGroups);
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			for (int i = 0; i < numGroups; i++) {
				ConstantMapGroup glyph = glyphs[i];
				ushort id = (ushort)glyph.glyphID;
				int endCharCode = (int)glyph.endCharCode;
				for (int j = (int)glyph.startCharCode; j <= endCharCode; j++) {
					table.Add(j, id);
				}
			}
			return table;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"reserved\": {0},\n", reserved);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.AppendFormat("\t\"numGroups\": \"{0}\",\n", numGroups);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
