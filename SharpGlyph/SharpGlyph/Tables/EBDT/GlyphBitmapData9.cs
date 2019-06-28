using System.Text;

namespace SharpGlyph {
	public class GlyphBitmapData9 : GlyphBitmapData {
		/// <summary>
		/// Metrics information for the glyph.
		/// </summary>
		public BigGlyphMetrics bigMetrics;

		/// <summary>
		/// Number of components.
		/// </summary>
		public ushort numComponents;

		/// <summary>
		/// Array of EbdtComponent records.
		/// </summary>
		public EbdtComponent[] components;

		public GlyphBitmapData9() {
			format = 9;
		}

		public static GlyphBitmapData9 Read(BinaryReaderFont reader) {
			GlyphBitmapData9 value = new GlyphBitmapData9 {
				bigMetrics = BigGlyphMetrics.Read(reader),
				numComponents = reader.ReadUInt16()
			};
			value.components = EbdtComponent.ReadArray(reader, value.numComponents);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"bigMetrics\": {0},\n", bigMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"numComponents\": {0}\n", numComponents);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
