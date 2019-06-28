using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData5 : GlyphBitmapData {
		/// <summary>
		/// Bit-aligned bitmap data.
		/// </summary>
		public byte[] imageData;

		public GlyphBitmapData5() {
			format = 5;
		}

		public static GlyphBitmapData5 Read(BinaryReaderFont reader, int byteSize) {
			GlyphBitmapData5 value = new GlyphBitmapData5();
			value.imageData = reader.ReadBytes(byteSize);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"imageData.Length\": {0}\n", imageData.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
