using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// CVT Variations Table (cvar).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class CvarTable : Table {
		public const string Tag = "cvar";

		/// <summary>
		/// Major version number of the CVT variations table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the CVT variations table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// A packed field.
		/// <para>
		/// The high 4 bits are flags, and the low 12 bits are
		/// the number of tuple-variation data tables.
		/// The count can be any number between 1 and 4095.
		/// </para>
		/// </summary>
		public ushort tupleVariationCount;

		/// <summary>
		/// Offset from the start of the 'cvar' table to the serialized data.
		/// </summary>
		public ushort dataOffset;

		/// <summary>
		/// Array of tuple variation headers.
		/// </summary>
		public TupleVariationHeader[] tupleVariationHeaders;

		public static CvarTable Read(BinaryReaderFont reader) {
			CvarTable value = new CvarTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				tupleVariationCount = reader.ReadUInt16(),
				dataOffset = reader.ReadUInt16()
			};
			//value.tupleVariationHeaders = TupleVariationHeader.ReadArray();
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"tupleVariationCount\": {0},\n", tupleVariationCount);
			builder.AppendFormat("\t\"dataOffset\": {0},\n", dataOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
