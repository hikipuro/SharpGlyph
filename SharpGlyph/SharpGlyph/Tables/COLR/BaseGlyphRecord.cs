using System;
using System.Text;

namespace SharpGlyph {
	public class BaseGlyphRecord {
		/// <summary>
		/// Glyph ID of reference glyph.
		/// <para>This glyph is for reference only and is not rendered for color.</para>
		/// </summary>
		public ushort gID;

		/// <summary>
		/// Index (from beginning of the Layer Records) to the layer record.
		/// <para>There will be numLayers consecutive entries for this base glyph.</para>
		/// </summary>
		public ushort firstLayerIndex;

		/// <summary>
		/// Number of color layers associated with this glyph.
		/// </summary>
		public ushort numLayers;

		public static BaseGlyphRecord Read(BinaryReaderFont reader) {
			return new BaseGlyphRecord {
				gID = reader.ReadUInt16(),
				firstLayerIndex = reader.ReadUInt16(),
				numLayers = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"gID\": {0},\n", gID);
			builder.AppendFormat("\t\"firstLayerIndex\": {0},\n", firstLayerIndex);
			builder.AppendFormat("\t\"numLayers\": {0},\n", numLayers);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
