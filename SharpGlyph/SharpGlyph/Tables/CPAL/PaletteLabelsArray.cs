using System;
using System.Text;

namespace SharpGlyph {
	public class PaletteLabelsArray {
		/// <summary>
		/// Array of 'name' table IDs (typically in the font-specific name ID range)
		/// that specify user interface strings associated with each palette
		/// Use 0xFFFF if no name ID is provided for a particular palette.
		/// </summary>
		public ushort[] paletteLabels;

		public static PaletteLabelsArray Read(BinaryReaderFont reader) {
			return new PaletteLabelsArray();
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
