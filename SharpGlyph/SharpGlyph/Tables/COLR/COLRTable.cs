using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Color Table (COLR).
	/// <para>Color Font</para>
	/// </summary>
	//[ColorFont]
	public class COLRTable : Table {
		public const string Tag = "COLR";

		/// <summary>
		/// Table version number (starts at 0).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of Base Glyph Records.
		/// </summary>
		public ushort numBaseGlyphRecords;

		/// <summary>
		/// Offset (from beginning of COLR table) to Base Glyph records.
		/// </summary>
		public uint baseGlyphRecordsOffset;

		/// <summary>
		/// Offset (from beginning of COLR table) to Layer Records.
		/// </summary>
		public uint layerRecordsOffset;

		/// <summary>
		/// Number of Layer Records.
		/// </summary>
		public ushort numLayerRecords;

		public static COLRTable Read(BinaryReaderFont reader) {
			return new COLRTable {
				version = reader.ReadUInt16(),
				numBaseGlyphRecords = reader.ReadUInt16(),
				baseGlyphRecordsOffset = reader.ReadUInt32(),
				layerRecordsOffset = reader.ReadUInt32(),
				numLayerRecords = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numBaseGlyphRecords\": {0},\n", numBaseGlyphRecords);
			builder.AppendFormat("\t\"baseGlyphRecordsOffset\": {0},\n", baseGlyphRecordsOffset);
			builder.AppendFormat("\t\"layerRecordsOffset\": {0},\n", layerRecordsOffset);
			builder.AppendFormat("\t\"numLayerRecords\": {0},\n", numLayerRecords);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
