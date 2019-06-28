using System;
using System.Text;

namespace SharpGlyph {
	public class ClassDefFormat2 {
		/// <summary>
		/// Format identifier — format = 2.
		/// </summary>
		public ushort classFormat;
		
		/// <summary>
		/// Number of ClassRangeRecords.
		/// </summary>
		public ushort classRangeCount;
		
		/// <summary>
		/// Array of ClassRangeRecords — ordered by startGlyphID.
		/// </summary>
		public ClassRangeRecord[] classRangeRecords;

		public static ClassDefFormat2 Read(BinaryReaderFont reader) {
			return new ClassDefFormat2 {
				classFormat = reader.ReadUInt16(),
				classRangeCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"classFormat\": {0},\n", classFormat);
			builder.AppendFormat("\t\"classRangeCount\": {0},\n", classRangeCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
