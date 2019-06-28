using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Glyph Positioning Table (GPOS).
	/// <para>Advanced Typographic Table</para>
	/// </summary>
	//[AdvancedTypographicTable]
	public class GPOSTable : Table {
		public const string Tag = "GPOS";

		/// <summary>
		/// [Version 1.0]
		/// Major version of the GPOS table.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// [Version 1.0]
		/// Minor version of the GPOS table.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// [Version 1.0]
		/// Offset to ScriptList table, from beginning of GPOS table.
		/// </summary>
		public ushort scriptListOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to FeatureList table, from beginning of GPOS table.
		/// </summary>
		public ushort featureListOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to LookupList table, from beginning of GPOS table.
		/// </summary>
		public ushort lookupListOffset;

		/// <summary>
		/// [Version 1.1]
		/// Offset to FeatureVariations table, from beginning of GPOS table (may be NULL).
		/// </summary>
		public uint featureVariationsOffset;

		public static GPOSTable Read(BinaryReaderFont reader) {
			return new GPOSTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				scriptListOffset = reader.ReadUInt16(),
				featureListOffset = reader.ReadUInt16(),
				lookupListOffset = reader.ReadUInt16(),
				featureVariationsOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"scriptListOffset\": {0},\n", scriptListOffset);
			builder.AppendFormat("\t\"featureListOffset\": {0},\n", featureListOffset);
			builder.AppendFormat("\t\"lookupListOffset\": {0},\n", lookupListOffset);
			builder.AppendFormat("\t\"featureVariationsOffset\": {0},\n", featureVariationsOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
