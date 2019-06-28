using System;
using System.Text;

namespace SharpGlyph {
	public class ClassRangeRecord {
		/// <summary>
		/// First glyph ID in the range.
		/// </summary>
		public ushort startGlyphID;
		
		/// <summary>
		/// Last glyph ID in the range.
		/// </summary>
		public ushort endGlyphID;
		
		/// <summary>
		/// Applied to all glyphs in the range.
		/// </summary>
		public ushort @class;

		public static ClassRangeRecord Read(BinaryReaderFont reader) {
			return new ClassRangeRecord {
				startGlyphID = reader.ReadUInt16(),
				endGlyphID = reader.ReadUInt16(),
				@class = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startGlyphID\": {0},\n", startGlyphID);
			builder.AppendFormat("\t\"endGlyphID\": {0},\n", endGlyphID);
			builder.AppendFormat("\t\"@class\": {0},\n", @class);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
