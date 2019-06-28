using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Glyph Variations Table (gvar).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class GvarTable : Table {
		public const string Tag = "gvar";

		/// <summary>
		/// Major version number of the glyph variations table.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the glyph variations table.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// The number of variation axes for this font.
		/// This must be the same number as axisCount in the 'fvar' table.
		/// </summary>
		public ushort axisCount;

		/// <summary>
		/// The number of shared tuple records.
		/// </summary>
		public ushort sharedTupleCount;

		/// <summary>
		/// Offset from the start of this table to the shared tuple records.
		/// </summary>
		public uint sharedTuplesOffset;

		/// <summary>
		/// The number of glyphs in this font.
		/// This must match the number of glyphs stored elsewhere in the font.
		/// </summary>
		public ushort glyphCount;

		/// <summary>
		/// Bit-field that gives the format of the offset array that follows.
		/// If bit 0 is clear, the offsets are uint16;
		/// if bit 0 is set, the offsets are uint32.
		/// </summary>
		public ushort flags;

		/// <summary>
		/// Offset from the start of this table to the array of GlyphVariationData tables.
		/// </summary>
		public uint glyphVariationDataArrayOffset;

		/// <summary>
		/// Offsets from the start of the GlyphVariationData array to each GlyphVariationData table.
		/// </summary>
		public uint[] glyphVariationDataOffsets;

		public static GvarTable Read(BinaryReaderFont reader) {
			return new GvarTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				axisCount = reader.ReadUInt16(),
				sharedTupleCount = reader.ReadUInt16(),
				sharedTuplesOffset = reader.ReadUInt32(),
				glyphCount = reader.ReadUInt16(),
				flags = reader.ReadUInt16(),
				glyphVariationDataArrayOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"axisCount\": {0},\n", axisCount);
			builder.AppendFormat("\t\"sharedTupleCount\": {0},\n", sharedTupleCount);
			builder.AppendFormat("\t\"sharedTuplesOffset\": {0},\n", sharedTuplesOffset);
			builder.AppendFormat("\t\"glyphCount\": {0},\n", glyphCount);
			builder.AppendFormat("\t\"flags\": {0},\n", flags);
			builder.AppendFormat("\t\"glyphVariationDataArrayOffset\": {0},\n", glyphVariationDataArrayOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
