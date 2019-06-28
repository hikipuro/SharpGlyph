using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Glyph Definition Table (GDEF).
	/// <para>Advanced Typographic Table</para>
	/// </summary>
	//[AdvancedTypographicTable]
	public class GDEFTable : Table {
		public const string Tag = "GDEF";

		/// <summary>
		/// [Version 1.0]
		/// Major version of the GDEF table.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// [Version 1.0]
		/// Minor version of the GDEF table.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// [Version 1.0]
		/// Offset to class definition table for glyph type,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public ushort glyphClassDefOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to attachment point list table,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public ushort attachListOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to ligature caret list table,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public ushort ligCaretListOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to class definition table for mark attachment type,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public ushort markAttachClassDefOffset;

		/// <summary>
		/// [Version 1.2]
		/// Offset to the table of mark glyph set definitions,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public ushort markGlyphSetsDefOffset;

		/// <summary>
		/// [Version 1.3]
		/// Offset to the Item Variation Store table,
		/// from beginning of GDEF header (may be NULL).
		/// </summary>
		public uint itemVarStoreOffset;

		public static GDEFTable Read(BinaryReaderFont reader) {
			return new GDEFTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				glyphClassDefOffset = reader.ReadUInt16(),
				attachListOffset = reader.ReadUInt16(),
				ligCaretListOffset = reader.ReadUInt16(),
				markAttachClassDefOffset = reader.ReadUInt16(),
				markGlyphSetsDefOffset = reader.ReadUInt16(),
				itemVarStoreOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"glyphClassDefOffset\": {0},\n", glyphClassDefOffset);
			builder.AppendFormat("\t\"attachListOffset\": {0},\n", attachListOffset);
			builder.AppendFormat("\t\"ligCaretListOffset\": {0},\n", ligCaretListOffset);
			builder.AppendFormat("\t\"markAttachClassDefOffset\": {0},\n", markAttachClassDefOffset);
			builder.AppendFormat("\t\"markGlyphSetsDefOffset\": {0},\n", markGlyphSetsDefOffset);
			builder.AppendFormat("\t\"itemVarStoreOffset\": {0},\n", itemVarStoreOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
