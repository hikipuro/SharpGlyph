using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 18: big metrics, PNG image data.
	/// </summary>
	public class GlyphBitmapData18 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public BigGlyphMetrics glyphMetrics;

		/// <summary>
		/// Length of data in bytes.
		/// </summary>
		public uint dataLen;

		/// <summary>
		/// Raw PNG data.
		/// </summary>
		public byte[] data;

		public GlyphBitmapData18() {
			format = 18;
		}

		public static GlyphBitmapData18 Read(BinaryReaderFont reader) {
			GlyphBitmapData18 value = new GlyphBitmapData18 {
				glyphMetrics = BigGlyphMetrics.Read(reader),
				dataLen = reader.ReadUInt32()
			};
			value.data = reader.ReadBytes((int)value.dataLen);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"glyphMetrics\": {0},\n", glyphMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"dataLen\": {0}\n", dataLen);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
