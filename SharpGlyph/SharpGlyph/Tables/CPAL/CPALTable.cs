using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Color Palette Table (CPAL).
	/// <para>Color Font</para>
	/// </summary>
	//[ColorFont]
	public class CPALTable : Table {
		public const string Tag = "CPAL";
		
		/// <summary>
		/// Table version number (0 or 1).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of palette entries in each palette.
		/// </summary>
		public ushort numPaletteEntries;
		
		/// <summary>
		/// Number of palettes in the table.
		/// </summary>
		public ushort numPalettes;
		
		/// <summary>
		/// Total number of color records, combined for all palettes.
		/// </summary>
		public ushort numColorRecords;
		
		/// <summary>
		/// Offset from the beginning of CPAL table to the first ColorRecord.
		/// </summary>
		public uint offsetFirstColorRecord;
		
		/// <summary>
		/// Index of each palette’s first color record in the combined color record array.
		/// </summary>
		public ushort[] colorRecordIndices;
		
		/// <summary>
		/// [Version 1]
		/// Offset from the beginning of CPAL table to the Palette Type Array.
		/// Set to 0 if no array is provided.
		/// </summary>
		public uint offsetPaletteTypeArray;
		
		/// <summary>
		/// [Version 1]
		/// Offset from the beginning of CPAL table to the Palette Labels Array.
		/// Set to 0 if no array is provided.
		/// </summary>
		public uint offsetPaletteLabelArray;
		
		/// <summary>
		/// [Version 1]
		/// Offset from the beginning of CPAL table to the Palette Entry Label Array.
		/// Set to 0 if no array is provided.
		/// </summary>
		public uint offsetPaletteEntryLabelArray;

		public static CPALTable Read(BinaryReaderFont reader) {
			CPALTable value = new CPALTable {
				version = reader.ReadUInt16(),
				numPaletteEntries = reader.ReadUInt16(),
				numPalettes = reader.ReadUInt16(),
				numColorRecords = reader.ReadUInt16(),
				offsetFirstColorRecord = reader.ReadUInt32()
			};
			//value.colorRecordIndices;
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numPaletteEntries\": {0},\n", numPaletteEntries);
			builder.AppendFormat("\t\"numPalettes\": {0},\n", numPalettes);
			builder.AppendFormat("\t\"numColorRecords\": {0},\n", numColorRecords);
			builder.AppendFormat("\t\"offsetFirstColorRecord\": {0},\n", offsetFirstColorRecord);
			builder.AppendFormat("\t\"offsetPaletteTypeArray\": {0},\n", offsetPaletteTypeArray);
			builder.AppendFormat("\t\"offsetPaletteLabelArray\": {0},\n", offsetPaletteLabelArray);
			builder.AppendFormat("\t\"offsetPaletteEntryLabelArray\": {0},\n", offsetPaletteEntryLabelArray);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
