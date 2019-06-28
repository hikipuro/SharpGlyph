using System;
using System.Text;

namespace SharpGlyph {
	public class ClassDefFormat1 {
		/// <summary>
		/// Format identifier — format = 1.
		/// </summary>
		public ushort classFormat;
		
		/// <summary>
		/// First glyph ID of the classValueArray.
		/// </summary>
		public ushort startGlyphID;
		
		/// <summary>
		/// Size of the classValueArray.
		/// </summary>
		public ushort glyphCount;
		
		/// <summary>
		/// Array of Class Values — one per glyph ID.
		/// </summary>
		public ushort[] classValueArray;

		public static ClassDefFormat1 Read(BinaryReaderFont reader) {
			return new ClassDefFormat1 {
				classFormat = reader.ReadUInt16(),
				startGlyphID = reader.ReadUInt16(),
				glyphCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"classFormat\": {0},\n", classFormat);
			builder.AppendFormat("\t\"startGlyphID\": {0},\n", startGlyphID);
			builder.AppendFormat("\t\"glyphCount\": {0},\n", glyphCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
