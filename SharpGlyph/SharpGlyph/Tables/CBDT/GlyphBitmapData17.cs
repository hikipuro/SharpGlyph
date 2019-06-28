using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 17: small metrics, PNG image data.
	/// </summary>
	public class GlyphBitmapData17 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public SmallGlyphMetrics glyphMetrics;

		/// <summary>
		/// Length of data in bytes.
		/// </summary>
		public uint dataLen;

		/// <summary>
		/// Raw PNG data.
		/// </summary>
		public byte[] data;

		public GlyphBitmapData17() {
			format = 17;
		}

		public static GlyphBitmapData17 Read(BinaryReaderFont reader) {
			GlyphBitmapData17 value = new GlyphBitmapData17 {
				glyphMetrics = SmallGlyphMetrics.Read(reader),
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
