using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Vertical Metrics Variations Table (VVAR).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class VVARTable : Table {
		public const string Tag = "VVAR";
		
		/// <summary>
		/// Major version number of the metrics variations table — set to 1.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version number of the metrics variations table — set to 0.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// Offset in bytes from the start of this table to the item variation store table.
		/// </summary>
		public uint itemVariationStoreOffset;
		
		/// <summary>
		/// Offset in bytes from the start of this table
		/// to the delta-set index mapping for advance heights (may be NULL).
		/// </summary>
		public uint advanceHeightMappingOffset;
		
		/// <summary>
		/// Offset in bytes from the start of this table
		/// to the delta-set index mapping for top side bearings (may be NULL).
		/// </summary>
		public uint tsbMappingOffset;
		
		/// <summary>
		/// Offset in bytes from the start of this table
		/// to the delta-set index mapping for bottom side bearings (may be NULL).
		/// </summary>
		public uint bsbMappingOffset;
		
		/// <summary>
		/// Offset in bytes from the start of this table
		/// to the delta-set index mapping for Y coordinates of vertical origins (may be NULL).
		/// </summary>
		public uint vOrgMappingOffset;

		public static VVARTable Read(BinaryReaderFont reader) {
			return new VVARTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				itemVariationStoreOffset = reader.ReadUInt32(),
				advanceHeightMappingOffset = reader.ReadUInt32(),
				tsbMappingOffset = reader.ReadUInt32(),
				bsbMappingOffset = reader.ReadUInt32(),
				vOrgMappingOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"itemVariationStoreOffset\": 0x{0:X8},\n", itemVariationStoreOffset);
			builder.AppendFormat("\t\"advanceHeightMappingOffset\": 0x{0:X8},\n", advanceHeightMappingOffset);
			builder.AppendFormat("\t\"tsbMappingOffset\": 0x{0:X8},\n", tsbMappingOffset);
			builder.AppendFormat("\t\"bsbMappingOffset\": 0x{0:X8},\n", bsbMappingOffset);
			builder.AppendFormat("\t\"vOrgMappingOffset\": 0x{0:X8}\n", vOrgMappingOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
