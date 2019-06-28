using System;
using System.Text;

namespace SharpGlyph {
	public class PaletteEntryLabelArray {
		/// <summary>
		/// Array of 'name' table IDs (typically in the font-specific name ID range)
		/// that specify user interface strings associated with each palette entry,
		/// e.g. “Outline”, “Fill”. This set of palette entry labels applies
		/// to all palettes in the font. Use 0xFFFF if no name ID is provided
		/// for a particular palette entry.
		/// </summary>
		public ushort[] paletteEntryLabels;

		public static PaletteEntryLabelArray Read(BinaryReaderFont reader) {
			return new PaletteEntryLabelArray();
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
