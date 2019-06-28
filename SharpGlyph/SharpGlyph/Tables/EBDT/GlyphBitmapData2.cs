using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData2 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public SmallGlyphMetrics smallMetrics;

		/// <summary>
		/// Bit-aligned bitmap data.
		/// </summary>
		public byte[] imageData;

		public GlyphBitmapData2() {
			format = 2;
		}

		public static GlyphBitmapData2 Read(BinaryReaderFont reader, int byteSize) {
			GlyphBitmapData2 value = new GlyphBitmapData2 {
				smallMetrics = SmallGlyphMetrics.Read(reader)
			};
			byteSize -= SmallGlyphMetrics.ByteSize;
			value.imageData = reader.ReadBytes(byteSize);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"smallMetrics\": {0},\n", smallMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"imageData.Length\": {0}\n", imageData.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
