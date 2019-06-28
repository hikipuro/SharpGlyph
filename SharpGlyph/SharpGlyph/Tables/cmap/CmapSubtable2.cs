using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 2: High-byte mapping through table.
	/// </summary>
	public class CmapSubtable2 : CmapSubtable {
		/// <summary>
		/// This is the length in bytes of the subtable.
		/// </summary>
		public ushort length;

		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public ushort language;
		
		/// <summary>
		/// Array that maps high bytes to subHeaders: value is subHeader index × 8.
		/// </summary>
		public ushort[] subHeaderKeys;
		
		/// <summary>
		/// Variable-length array of SubHeader records.
		/// </summary>
		public SubHeader[] subHeaders;
		
		/// <summary>
		/// Variable-length array containing subarrays used for
		/// mapping the low byte of 2-byte characters.
		/// </summary>
		public ushort[] glyphIndexArray;

		public static new CmapSubtable2 Read(BinaryReaderFont reader) {
			CmapSubtable2 value = new CmapSubtable2 {
				format = reader.ReadUInt16(),
				length = reader.ReadUInt16(),
				language = reader.ReadUInt16()
			};
			value.subHeaderKeys = reader.ReadUInt16Array(256);
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			return table;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
