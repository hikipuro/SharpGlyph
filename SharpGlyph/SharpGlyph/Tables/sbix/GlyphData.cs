using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// The data for each glyph includes a header and the actual, embedded graphic data.
	/// </summary>
	public class GlyphData {
		/// <summary>
		/// The horizontal (x-axis) offset from the left edge of the graphic to the glyph’s origin.
		/// That is, the x-coordinate of the point on the baseline at the left edge of the glyph.
		/// </summary>
		public short originOffsetX;

		/// <summary>
		/// The vertical (y-axis) offset from the bottom edge of the graphic to the glyph’s origin.
		/// That is, the y-coordinate of the point on the baseline at the left edge of the glyph.
		/// </summary>
		public short originOffsetY;

		/// <summary>
		/// Indicates the format of the embedded graphic data:
		/// one of 'jpg ', 'png ' or 'tiff', or the special format 'dupe'.
		/// </summary>
		public string graphicType;

		/// <summary>
		/// The actual embedded graphic data.
		/// The total length is inferred from sequential entries in the glyphDataOffsets array
		/// and the fixed size (8 bytes) of the preceding fields.
		/// </summary>
		public byte[] data;

		public static GlyphData Read(BinaryReaderFont reader, uint dataLength) {
			return new GlyphData {
				originOffsetX = reader.ReadInt16(),
				originOffsetY = reader.ReadInt16(),
				graphicType = reader.ReadTag(),
				data = reader.ReadBytes((int)dataLength)
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"originOffsetX\": {0},\n", originOffsetX);
			builder.AppendFormat("\t\"originOffsetY\": {0},\n", originOffsetY);
			builder.AppendFormat("\t\"graphicType\": \"{0}\",\n", graphicType);
			builder.AppendFormat("\t\"dataLength\": {0},\n", data.Length);
			builder.Append("\t\"data: [");
			int length = Math.Min(10, data.Length);
			for (int i = 0; i < length; i++) {
				builder.AppendFormat("{0:X2}, ", data[i]);
			}
			if (length > 0) {
				builder.Remove(builder.Length - 2, 2);
			}
			builder.AppendLine("]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
