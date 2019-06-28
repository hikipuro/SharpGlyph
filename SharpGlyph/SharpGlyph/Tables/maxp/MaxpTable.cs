using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Maximum Profile (maxp).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class MaxpTable : Table {
		public const string Tag = "maxp";

		/// <summary>
		/// [Version 0.5]
		/// <para>0x00005000 for version 0.5</para>
		/// <para>0x00010000 for version 1.0</para>
		/// (Note the difference in the representation of a non-zero fractional part, in Fixed numbers.)
		/// </summary>
		public uint version;
		
		/// <summary>
		/// [Version 0.5]
		/// The number of glyphs in the font.
		/// </summary>
		public ushort numGlyphs;

		/// <summary>
		/// [Version 1]
		/// Maximum points in a non-composite glyph.
		/// </summary>
		public ushort maxPoints;
		
		/// <summary>
		/// [Version 1]
		/// Maximum contours in a non-composite glyph.
		/// </summary>
		public ushort maxContours;
		
		/// <summary>
		/// [Version 1]
		/// Maximum points in a composite glyph.
		/// </summary>
		public ushort maxCompositePoints;
		
		/// <summary>
		/// [Version 1]
		/// Maximum contours in a composite glyph.
		/// </summary>
		public ushort maxCompositeContours;
		
		/// <summary>
		/// [Version 1]
		/// 1 if instructions do not use the twilight zone (Z0),
		/// or 2 if instructions do use Z0; should be set to 2 in most cases.
		/// </summary>
		public ushort maxZones;
		
		/// <summary>
		/// [Version 1]
		/// Maximum points used in Z0.
		/// </summary>
		public ushort maxTwilightPoints;
		
		/// <summary>
		/// [Version 1]
		/// Number of Storage Area locations.
		/// </summary>
		public ushort maxStorage;
		
		/// <summary>
		/// [Version 1]
		/// Number of FDEFs, equal to the highest function number + 1.
		/// </summary>
		public ushort maxFunctionDefs;
		
		/// <summary>
		/// [Version 1]
		/// Number of IDEFs.
		/// </summary>
		public ushort maxInstructionDefs;
		
		/// <summary>
		/// [Version 1]
		/// Maximum stack depth across Font Program ('fpgm' table),
		/// CVT Program ('prep' table) and all glyph instructions (in the 'glyf' table).
		/// </summary>
		public ushort maxStackElements;
		
		/// <summary>
		/// [Version 1]
		/// Maximum byte count for glyph instructions.
		/// </summary>
		public ushort maxSizeOfInstructions;
		
		/// <summary>
		/// [Version 1]
		/// Maximum number of components referenced at “top level” for any composite glyph.
		/// </summary>
		public ushort maxComponentElements;
		
		/// <summary>
		/// [Version 1]
		/// Maximum levels of recursion; 1 for simple components.
		/// </summary>
		public ushort maxComponentDepth;
		
		public static MaxpTable Read(BinaryReaderFont reader) {
			MaxpTable value = new MaxpTable {
				version = reader.ReadFixed(),
				numGlyphs = reader.ReadUInt16()
			};
			if (value.version >= 0x10000) {
				value.maxPoints = reader.ReadUInt16();
				value.maxContours = reader.ReadUInt16();
				value.maxCompositePoints = reader.ReadUInt16();
				value.maxCompositeContours = reader.ReadUInt16();
				value.maxZones = reader.ReadUInt16();
				value.maxTwilightPoints = reader.ReadUInt16();
				value.maxStorage = reader.ReadUInt16();
				value.maxFunctionDefs = reader.ReadUInt16();
				value.maxInstructionDefs = reader.ReadUInt16();
				value.maxStackElements = reader.ReadUInt16();
				value.maxSizeOfInstructions = reader.ReadUInt16();
				value.maxComponentElements = reader.ReadUInt16();
				value.maxComponentDepth = reader.ReadUInt16();
			}
			return value;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": 0x{0:X8},\n", version);
			builder.AppendFormat("\t\"numGlyphs\": {0},\n", numGlyphs);
			if (version >= 0x10000) {
				builder.AppendFormat("\t\"maxPoints\": {0},\n", maxPoints);
				builder.AppendFormat("\t\"maxContours\": {0},\n", maxContours);
				builder.AppendFormat("\t\"maxCompositePoints\": {0},\n", maxCompositePoints);
				builder.AppendFormat("\t\"maxCompositeContours\": {0},\n", maxCompositeContours);
				builder.AppendFormat("\t\"maxZones\": {0},\n", maxZones);
				builder.AppendFormat("\t\"maxTwilightPoints\": {0},\n", maxTwilightPoints);
				builder.AppendFormat("\t\"maxStorage\": {0},\n", maxStorage);
				builder.AppendFormat("\t\"maxFunctionDefs\": {0},\n", maxFunctionDefs);
				builder.AppendFormat("\t\"maxInstructionDefs\": {0},\n", maxInstructionDefs);
				builder.AppendFormat("\t\"maxStackElements\": {0},\n", maxStackElements);
				builder.AppendFormat("\t\"maxSizeOfInstructions\": {0},\n", maxSizeOfInstructions);
				builder.AppendFormat("\t\"maxComponentElements\": {0},\n", maxComponentElements);
				builder.AppendFormat("\t\"maxComponentDepth\": {0},\n", maxComponentDepth);
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
