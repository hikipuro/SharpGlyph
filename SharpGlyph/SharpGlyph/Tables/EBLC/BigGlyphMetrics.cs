using System.Text;

namespace SharpGlyph {
	public class BigGlyphMetrics {
		public static readonly int ByteSize = 8;

		public byte height;
		public byte width;
		public sbyte horiBearingX;
		public sbyte horiBearingY;
		public byte horiAdvance;
		public sbyte vertBearingX;
		public sbyte vertBearingY;
		public byte vertAdvance;

		public static BigGlyphMetrics Read(BinaryReaderFont reader) {
			return new BigGlyphMetrics {
				height = reader.ReadByte(),
				width = reader.ReadByte(),
				horiBearingX = reader.ReadSByte(),
				horiBearingY = reader.ReadSByte(),
				horiAdvance = reader.ReadByte(),
				vertBearingX = reader.ReadSByte(),
				vertBearingY = reader.ReadSByte(),
				vertAdvance = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"height\": {0},\n", height);
			builder.AppendFormat("\t\"width\": {0},\n", width);
			builder.AppendFormat("\t\"horiBearingX\": {0},\n", horiBearingX);
			builder.AppendFormat("\t\"horiBearingY\": {0},\n", horiBearingY);
			builder.AppendFormat("\t\"horiAdvance\": {0},\n", horiAdvance);
			builder.AppendFormat("\t\"vertBearingX\": {0},\n", vertBearingX);
			builder.AppendFormat("\t\"vertBearingY\": {0},\n", vertBearingY);
			builder.AppendFormat("\t\"vertAdvance\": {0}\n", vertAdvance);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
