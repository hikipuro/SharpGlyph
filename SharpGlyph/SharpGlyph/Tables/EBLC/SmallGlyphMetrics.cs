using System;
using System.Text;

namespace SharpGlyph {
	public class SmallGlyphMetrics {
		public static readonly int ByteSize = 5;

		public byte height;
		public byte width;
		public sbyte bearingX;
		public sbyte bearingY;
		public byte advance;

		public static SmallGlyphMetrics Read(BinaryReaderFont reader) {
			return new SmallGlyphMetrics {
				height = reader.ReadByte(),
				width = reader.ReadByte(),
				bearingX = reader.ReadSByte(),
				bearingY = reader.ReadSByte(),
				advance = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"height\": {0},\n", height);
			builder.AppendFormat("\t\"width\": {0},\n", width);
			builder.AppendFormat("\t\"bearingX\": {0},\n", bearingX);
			builder.AppendFormat("\t\"bearingY\": {0},\n", bearingY);
			builder.AppendFormat("\t\"advance\": {0},\n", advance);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
