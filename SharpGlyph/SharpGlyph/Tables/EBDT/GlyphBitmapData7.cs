using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData7 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public BigGlyphMetrics bigMetrics;

		/// <summary>
		/// Bit-aligned bitmap data.
		/// </summary>
		public byte[] imageData;

		public GlyphBitmapData7() {
			format = 7;
		}

		public static GlyphBitmapData7 Read(BinaryReaderFont reader, int byteSize) {
			GlyphBitmapData7 value = new GlyphBitmapData7 {
				bigMetrics = BigGlyphMetrics.Read(reader)
			};
			byteSize -= BigGlyphMetrics.ByteSize;
			value.imageData = reader.ReadBytes(byteSize);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"bigMetrics\": {0},\n", bigMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"imageData.Length\": {0}\n", imageData.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
