using System;
using System.Text;

namespace SharpGlyph {
	public class CoverageFormat1 {
		/// <summary>
		/// Format identifier — format = 1.
		/// </summary>
		public ushort coverageFormat;
		
		/// <summary>
		/// Number of glyphs in the glyph array.
		/// </summary>
		public ushort glyphCount;
		
		/// <summary>
		/// Array of glyph IDs — in numerical order.
		/// </summary>
		public ushort[] glyphArray;

		public static CoverageFormat1 Read(BinaryReaderFont reader) {
			return new CoverageFormat1 {
				coverageFormat = reader.ReadUInt16(),
				glyphCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"coverageFormat\": {0},\n", coverageFormat);
			builder.AppendFormat("\t\"glyphCount\": {0},\n", glyphCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
