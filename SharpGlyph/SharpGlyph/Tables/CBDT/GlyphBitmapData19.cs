using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 19: metrics in CBLC table, PNG image data.
	/// </summary>
	public class GlyphBitmapData19 : GlyphBitmapData {
		/// <summary>
		/// Length of data in bytes.
		/// </summary>
		public uint dataLen;

		/// <summary>
		/// Raw PNG data.
		/// </summary>
		public byte[] data;

		public GlyphBitmapData19() {
			format = 19;
		}

		public static GlyphBitmapData19 Read(BinaryReaderFont reader) {
			GlyphBitmapData19 value = new GlyphBitmapData19 {
				dataLen = reader.ReadUInt32()
			};
			value.data = reader.ReadBytes((int)value.dataLen);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"dataLen\": {0}\n", dataLen);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
