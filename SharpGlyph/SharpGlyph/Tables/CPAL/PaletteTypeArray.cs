using System;
using System.Text;

namespace SharpGlyph {
	public class PaletteTypeArray {
		/// <summary>
		/// Array of 32-bit flag fields that describe properties of each palette.
		/// </summary>
		public uint[] paletteTypes;

		public static PaletteTypeArray Read(BinaryReaderFont reader) {
			return new PaletteTypeArray();
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
