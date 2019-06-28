using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData6 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public BigGlyphMetrics bigMetrics;

		/// <summary>
		/// Byte-aligned bitmap data.
		/// </summary>
		public byte[] imageData;

		public GlyphBitmapData6() {
			format = 6;
		}

		public static GlyphBitmapData6 Read(BinaryReaderFont reader, int byteSize) {
			GlyphBitmapData6 value = new GlyphBitmapData6 {
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
