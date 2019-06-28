using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData8 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public SmallGlyphMetrics smallMetrics;

		/// <summary>
		/// Pad to 16-bit boundary.
		/// </summary>
		public byte pad;

		/// <summary>
		/// Number of components.
		/// </summary>
		public ushort numComponents;

		/// <summary>
		/// Array of EbdtComponent records.
		/// </summary>
		public EbdtComponent[] components;

		public GlyphBitmapData8() {
			format = 8;
		}

		public static GlyphBitmapData8 Read(BinaryReaderFont reader) {
			GlyphBitmapData8 value = new GlyphBitmapData8 {
				smallMetrics = SmallGlyphMetrics.Read(reader),
				pad = reader.ReadByte(),
				numComponents = reader.ReadUInt16()
			};
			value.components = EbdtComponent.ReadArray(reader, value.numComponents);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"smallMetrics\": {0},\n", smallMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"pad\": {0}\n", pad);
			builder.AppendFormat("\t\"numComponents\": {0}\n", numComponents);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
