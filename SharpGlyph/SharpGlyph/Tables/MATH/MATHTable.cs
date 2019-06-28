using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// The mathematical typesetting table (MATH).
	/// <para>Advanced Typographic Table</para>
	/// </summary>
	//[AdvancedTypographicTable]
	public class MATHTable : Table {
		public const string Tag = "MATH";

		/// <summary>
		/// Major version of the MATH table, = 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version of the MATH table, = 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Offset to MathConstants table
		/// - from the beginning of MATH table.
		/// </summary>
		public ushort mathConstantsOffset;

		/// <summary>
		/// Offset to MathGlyphInfo table
		/// - from the beginning of MATH table.
		/// </summary>
		public ushort mathGlyphInfoOffset;

		/// <summary>
		/// Offset to MathVariants table
		/// - from the beginning of MATH table.
		/// </summary>
		public ushort mathVariantsOffset;

		public static MATHTable Read(BinaryReaderFont reader) {
			return new MATHTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				mathConstantsOffset = reader.ReadUInt16(),
				mathGlyphInfoOffset = reader.ReadUInt16(),
				mathVariantsOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"mathConstantsOffset\": {0},\n", mathConstantsOffset);
			builder.AppendFormat("\t\"mathGlyphInfoOffset\": {0},\n", mathGlyphInfoOffset);
			builder.AppendFormat("\t\"mathVariantsOffset\": {0},\n", mathVariantsOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
