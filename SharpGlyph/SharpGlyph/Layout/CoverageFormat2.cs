using System;
using System.Text;

namespace SharpGlyph {
	public class CoverageFormat2 {
		/// <summary>
		/// Format identifier — format = 2.
		/// </summary>
		public ushort coverageFormat;
		
		/// <summary>
		/// Number of RangeRecords.
		/// </summary>
		public ushort rangeCount;
		
		/// <summary>
		/// Array of glyph ranges — ordered by startGlyphID.
		/// </summary>
		public RangeRecord[] rangeRecords;

		public static CoverageFormat2 Read(BinaryReaderFont reader) {
			return new CoverageFormat2 {
				coverageFormat = reader.ReadUInt16(),
				rangeCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"coverageFormat\": {0},\n", coverageFormat);
			builder.AppendFormat("\t\"rangeCount\": {0},\n", rangeCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
