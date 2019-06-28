using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Style Attributes Table (STAT).
	/// <para>OpenType Font Variation, OpenType Table</para>
	/// </summary>
	//[OpenTypeFontVariation]
	//[OpenTypeTable]
	public class STATTable : Table {
		public const string Tag = "STAT";

		/// <summary>
		/// Major version number of the style attributes table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the style attributes table — set to 2.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// The size in bytes of each axis record.
		/// </summary>
		public ushort designAxisSize;

		/// <summary>
		/// The number of design axis records.
		/// </summary>
		/// <remarks>
		/// In a font with an 'fvar' table, this value must be
		/// greater than or equal to the axisCount value in
		/// the 'fvar' table.
		/// In all fonts, must be greater than zero
		/// if axisValueCount is greater than zero.
		/// </remarks>
		public ushort designAxisCount;

		/// <summary>
		/// Offset in bytes from the beginning of the STAT table
		/// to the start of the design axes array.
		/// </summary>
		/// <remarks>
		/// If designAxisCount is zero, set to zero;
		/// if designAxisCount is greater than zero,
		/// must be greater than zero.
		/// </remarks>
		public uint designAxesOffset;

		/// <summary>
		/// The number of axis value tables.
		/// </summary>
		public ushort axisValueCount;

		/// <summary>
		/// Offset in bytes from the beginning of the STAT table
		/// to the start of the design axes value offsets array.
		/// </summary>
		/// <remarks>
		/// If axisValueCount is zero, set to zero;
		/// if axisValueCount is greater than zero,
		/// must be greater than zero.
		/// </remarks>
		public uint offsetToAxisValueOffsets;

		/// <summary>
		/// Name ID used as fallback when projection of names
		/// into a particular font model produces a subfamily
		/// name containing only elidable elements.
		/// </summary>
		public ushort elidedFallbackNameID;

		public static STATTable Read(BinaryReaderFont reader) {
			return new STATTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				designAxisSize = reader.ReadUInt16(),
				designAxisCount = reader.ReadUInt16(),
				designAxesOffset = reader.ReadUInt32(),
				axisValueCount = reader.ReadUInt16(),
				offsetToAxisValueOffsets = reader.ReadUInt32(),
				elidedFallbackNameID = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"designAxisSize\": {0},\n", designAxisSize);
			builder.AppendFormat("\t\"designAxisCount\": {0},\n", designAxisCount);
			builder.AppendFormat("\t\"designAxesOffset\": {0},\n", designAxesOffset);
			builder.AppendFormat("\t\"axisValueCount\": {0},\n", axisValueCount);
			builder.AppendFormat("\t\"offsetToAxisValueOffsets\": {0},\n", offsetToAxisValueOffsets);
			builder.AppendFormat("\t\"elidedFallbackNameID\": {0},\n", elidedFallbackNameID);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
