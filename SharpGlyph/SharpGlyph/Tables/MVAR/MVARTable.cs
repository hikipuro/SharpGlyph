using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Metrics Variations Table (MVAR).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class MVARTable : Table {
		public const string Tag = "MVAR";

		/// <summary>
		/// Major version number of the metrics variations table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the metrics variations table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Not used; set to 0.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// The size in bytes of each value record — must be greater than zero.
		/// </summary>
		public ushort valueRecordSize;

		/// <summary>
		/// The number of value records — may be zero.
		/// </summary>
		public ushort valueRecordCount;

		/// <summary>
		/// Offset in bytes from the start of this table
		/// to the item variation store table.
		/// If valueRecordCount is zero, set to zero;
		/// if valueRecordCount is greater than zero,
		/// must be greater than zero.
		/// </summary>
		public ushort itemVariationStoreOffset;

		/// <summary>
		/// Array of value records that identify target items
		/// and the associated delta-set index for each.
		/// The valueTag records must be in binary order
		/// of their valueTag field.
		/// </summary>
		public MvarValueRecord[] valueRecords;

		public static MVARTable Read(BinaryReaderFont reader) {
			return new MVARTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				reserved = reader.ReadUInt16(),
				valueRecordSize = reader.ReadUInt16(),
				valueRecordCount = reader.ReadUInt16(),
				itemVariationStoreOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"reserved\": {0},\n", reserved);
			builder.AppendFormat("\t\"valueRecordSize\": {0},\n", valueRecordSize);
			builder.AppendFormat("\t\"valueRecordCount\": {0},\n", valueRecordCount);
			builder.AppendFormat("\t\"itemVariationStoreOffset\": {0},\n", itemVariationStoreOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
