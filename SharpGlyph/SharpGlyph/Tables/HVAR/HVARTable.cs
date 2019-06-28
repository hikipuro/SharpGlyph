using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Horizontal Metrics Variations Table (HVAR).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class HVARTable : Table {
		public const string Tag = "HVAR";

		/// <summary>
		/// Major version number of the horizontal metrics variations table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the horizontal metrics variations table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Offset in bytes from the start of this table to the item variation store table.
		/// </summary>
		public uint itemVariationStoreOffset;

		/// <summary>
		/// Offset in bytes from the start of this table to
		/// the delta-set index mapping for advance widths (may be NULL).
		/// </summary>
		public uint advanceWidthMappingOffset;

		/// <summary>
		/// Offset in bytes from the start of this table to
		/// the delta-set index mapping for left side bearings (may be NULL).
		/// </summary>
		public uint lsbMappingOffset;

		/// <summary>
		/// Offset in bytes from the start of this table to
		/// the delta-set index mapping for right side bearings (may be NULL).
		/// </summary>
		public uint rsbMappingOffset;

		public static HVARTable Read(BinaryReaderFont reader) {
			return new HVARTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				itemVariationStoreOffset = reader.ReadUInt32(),
				advanceWidthMappingOffset = reader.ReadUInt32(),
				lsbMappingOffset = reader.ReadUInt32(),
				rsbMappingOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"itemVariationStoreOffset\": {0},\n", itemVariationStoreOffset);
			builder.AppendFormat("\t\"advanceWidthMappingOffset\": {0},\n", advanceWidthMappingOffset);
			builder.AppendFormat("\t\"lsbMappingOffset\": {0},\n", lsbMappingOffset);
			builder.AppendFormat("\t\"rsbMappingOffset\": {0},\n", rsbMappingOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
