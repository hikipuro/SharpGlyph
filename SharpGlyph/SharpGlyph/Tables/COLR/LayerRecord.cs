using System;
using System.Text;

namespace SharpGlyph {
	public class LayerRecord {
		/// <summary>
		/// Glyph ID of layer glyph (must be in z-order from bottom to top).
		/// </summary>
		public ushort gID;

		/// <summary>
		/// Index value to use with a selected color palette. 
		/// </summary>
		public ushort paletteIndex;

		public static LayerRecord Read(BinaryReaderFont reader) {
			return new LayerRecord {
				gID = reader.ReadUInt16(),
				paletteIndex = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"gID\": {0},\n", gID);
			builder.AppendFormat("\t\"paletteIndex\": {0},\n", paletteIndex);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
