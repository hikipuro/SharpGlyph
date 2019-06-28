using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 6: Trimmed table mapping.
	/// </summary>
	public class CmapSubtable6 : CmapSubtable {
		/// <summary>
		/// This is the length in bytes of the subtable.
		/// </summary>
		public ushort length;

		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public ushort language;

		/// <summary>
		/// First character code of subrange.
		/// </summary>
		public ushort firstCode;

		/// <summary>
		/// Number of character codes in subrange.
		/// </summary>
		public ushort entryCount;

		/// <summary>
		/// Array of glyph index values for character codes in the range.
		/// </summary>
		public ushort[] glyphIdArray;

		public static new CmapSubtable6 Read(BinaryReaderFont reader) {
			CmapSubtable6 value = new CmapSubtable6 {
				format = reader.ReadUInt16(),
				length = reader.ReadUInt16(),
				language = reader.ReadUInt16(),
				firstCode = reader.ReadUInt16(),
				entryCount = reader.ReadUInt16()
			};
			value.glyphIdArray = reader.ReadUInt16Array(value.entryCount);
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			for (int i = 0; i < entryCount; i++) {
				table.Add(i + firstCode, glyphIdArray[i]);
			}
			return table;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.AppendFormat("\t\"firstCode\": \"{0}\",\n", firstCode);
			builder.AppendFormat("\t\"entryCount\": \"{0}\",\n", entryCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
