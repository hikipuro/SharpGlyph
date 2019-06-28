using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// PCL 5 Table (pclt).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class PcltTable : Table {
		public const string Tag = "pclt";

		/// <summary>
		/// The current PCLT table version is 1.0.
		/// </summary>
		public ushort MajorVersion;

		/// <summary>
		/// The current PCLT table version is 1.0.
		/// </summary>
		public ushort MinorVersion;
		public uint FontNumber;

		/// <summary>
		/// Width of the space in FUnits.
		/// </summary>
		public ushort Pitch;

		/// <summary>
		/// Height of the optical line describing
		/// the height of the lowercase x in FUnits. 
		/// </summary>
		public ushort xHeight;
		public ushort Style;
		public ushort TypeFamily;

		/// <summary>
		/// Height of the optical line describing
		/// the top of the uppercase H in FUnits. 
		/// </summary>
		public ushort CapHeight;
		public ushort SymbolSet;
		public sbyte[] Typeface;
		public sbyte[] CharacterComplement;
		public sbyte[] FileName;
		public sbyte StrokeWeight;
		public sbyte WidthType;
		public byte SerifStyle;
		public byte Reserved;

		public static PcltTable Read(BinaryReaderFont reader) {
			return new PcltTable {
				MajorVersion = reader.ReadUInt16(),
				MinorVersion = reader.ReadUInt16(),
				FontNumber = reader.ReadUInt32(),
				Pitch = reader.ReadUInt16(),
				xHeight = reader.ReadUInt16(),
				Style = reader.ReadUInt16(),
				TypeFamily = reader.ReadUInt16(),
				CapHeight = reader.ReadUInt16(),
				SymbolSet = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"MajorVersion\": {0},\n", MajorVersion);
			builder.AppendFormat("\t\"MinorVersion\": {0},\n", MinorVersion);
			builder.AppendFormat("\t\"FontNumber\": {0},\n", FontNumber);
			builder.AppendFormat("\t\"Pitch\": {0},\n", Pitch);
			builder.AppendFormat("\t\"xHeight\": {0},\n", xHeight);
			builder.AppendFormat("\t\"Style\": {0},\n", Style);
			builder.AppendFormat("\t\"TypeFamily\": {0},\n", TypeFamily);
			builder.AppendFormat("\t\"CapHeight\": {0},\n", CapHeight);
			builder.AppendFormat("\t\"SymbolSet\": {0},\n", SymbolSet);
			builder.AppendFormat("\t\"StrokeWeight\": {0},\n", StrokeWeight);
			builder.AppendFormat("\t\"WidthType\": {0},\n", WidthType);
			builder.AppendFormat("\t\"SerifStyle\": {0},\n", SerifStyle);
			builder.AppendFormat("\t\"Reserved\": {0},\n", Reserved);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
