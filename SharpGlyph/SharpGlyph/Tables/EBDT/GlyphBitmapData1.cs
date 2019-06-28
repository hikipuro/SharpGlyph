using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData1 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public SmallGlyphMetrics smallMetrics;

		/// <summary>
		/// Byte-aligned bitmap data.
		/// </summary>
		public byte[] imageData;

		public GlyphBitmapData1() {
			format = 1;
		}

		public static GlyphBitmapData1 Read(BinaryReaderFont reader, int byteSize) {
			GlyphBitmapData1 value = new GlyphBitmapData1 {
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
