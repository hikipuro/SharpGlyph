using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// OS/2 table (OS/2).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class OS2Table : Table {
		public const string Tag = "OS/2";

		/// <summary>
		/// OS/2 table version number.
		/// <para>The version number for the OS/2 table: 0x0000 to 0x0005.</para>
		/// </summary>
		public ushort version;
		
		/// <summary>
		/// Average weighted escapement.
		/// </summary>
		public short xAvgCharWidth;
		
		/// <summary>
		/// Weight class.
		/// </summary>
		public ushort usWeightClass;
		
		/// <summary>
		/// Width class.
		/// </summary>
		public ushort usWidthClass;
		
		/// <summary>
		/// Type flags.
		/// </summary>
		public ushort fsType;
		
		/// <summary>
		/// Subscript horizontal font size.
		/// </summary>
		public short ySubscriptXSize;
		
		/// <summary>
		/// Subscript vertical font size.
		/// </summary>
		public short ySubscriptYSize;
		
		/// <summary>
		/// Subscript x offset.
		/// </summary>
		public short ySubscriptXOffset;
		
		/// <summary>
		/// Subscript y offset.
		/// </summary>
		public short ySubscriptYOffset;
		
		/// <summary>
		/// Superscript horizontal font size.
		/// </summary>
		public short ySuperscriptXSize;
		
		/// <summary>
		/// Superscript vertical font size.
		/// </summary>
		public short ySuperscriptYSize;
		
		/// <summary>
		/// Superscript x offset.
		/// </summary>
		public short ySuperscriptXOffset;
		
		/// <summary>
		/// Superscript y offset.
		/// </summary>
		public short ySuperscriptYOffset;
		
		/// <summary>
		/// Strikeout size.
		/// </summary>
		public short yStrikeoutSize;
		
		/// <summary>
		/// Strikeout position.
		/// </summary>
		public short yStrikeoutPosition;
		
		/// <summary>
		/// Font-family class and subclass.
		/// </summary>
		public short sFamilyClass;
		
		/// <summary>
		/// PANOSE classification number.
		/// </summary>
		public byte[] panose;
		
		/// <summary>
		/// Unicode Character Range (Bits 0–31).
		/// </summary>
		public uint ulUnicodeRange1;
		
		/// <summary>
		/// Unicode Character Range (Bits 32–63).
		/// </summary>
		public uint ulUnicodeRange2;
		
		/// <summary>
		/// Unicode Character Range (Bits 64–95).
		/// </summary>
		public uint ulUnicodeRange3;
		
		/// <summary>
		/// Unicode Character Range (Bits 96–127).
		/// </summary>
		public uint ulUnicodeRange4;
		
		/// <summary>
		/// Font Vendor Identification.
		/// </summary>
		public string achVendID;
		
		/// <summary>
		/// Font selection flags.
		/// </summary>
		public ushort fsSelection;
		
		/// <summary>
		/// Minimum Unicode index (character code) in this font.
		/// </summary>
		public ushort usFirstCharIndex;
		
		/// <summary>
		/// Maximum Unicode index (character code) in this font.
		/// </summary>
		public ushort usLastCharIndex;
		
		/// <summary>
		/// Typographic ascender for this font.
		/// </summary>
		public short sTypoAscender;
		
		/// <summary>
		/// Typographic descender for this font. 
		/// </summary>
		public short sTypoDescender;
		
		/// <summary>
		/// Typographic line gap for this font.
		/// </summary>
		public short sTypoLineGap;
		
		/// <summary>
		/// “Windows ascender” metric.
		/// </summary>
		public ushort usWinAscent;
		
		/// <summary>
		/// “Windows descender” metric.
		/// </summary>
		public ushort usWinDescent;

		/// <summary>
		/// [Version 1]
		/// Code Page Character Range (Bits 0–31).
		/// </summary>
		public uint ulCodePageRange1;
		
		/// <summary>
		/// [Version 1]
		/// Code Page Character Range (Bits 32–63).
		/// </summary>
		public uint ulCodePageRange2;
		
		/// <summary>
		/// [Version 4]
		/// </summary>
		public short sxHeight;
		
		/// <summary>
		/// [Version 4]
		/// </summary>
		public short sCapHeight;
		
		/// <summary>
		/// [Version 4]
		/// </summary>
		public ushort usDefaultChar;
		
		/// <summary>
		/// [Version 4]
		/// </summary>
		public ushort usBreakChar;
		
		/// <summary>
		/// [Version 4]
		/// </summary>
		public ushort usMaxContext;
		
		/// <summary>
		/// [Version 5]
		/// </summary>
		public ushort usLowerOpticalPointSize;
		
		/// <summary>
		/// [Version 5]
		/// </summary>
		public ushort usUpperOpticalPointSize;
		
		public static OS2Table Read(BinaryReaderFont reader) {
			OS2Table value = new OS2Table {
				version = reader.ReadUInt16(),
				xAvgCharWidth = reader.ReadInt16(),
				usWeightClass = reader.ReadUInt16(),
				usWidthClass = reader.ReadUInt16(),
				fsType = reader.ReadUInt16(),
				ySubscriptXSize = reader.ReadInt16(),
				ySubscriptYSize = reader.ReadInt16(),
				ySubscriptXOffset = reader.ReadInt16(),
				ySubscriptYOffset = reader.ReadInt16(),
				ySuperscriptXSize = reader.ReadInt16(),
				ySuperscriptYSize = reader.ReadInt16(),
				ySuperscriptXOffset = reader.ReadInt16(),
				ySuperscriptYOffset = reader.ReadInt16(),
				yStrikeoutSize = reader.ReadInt16(),
				yStrikeoutPosition = reader.ReadInt16(),
				sFamilyClass = reader.ReadInt16(),
				panose = reader.ReadBytes(10),
				ulUnicodeRange1 = reader.ReadUInt32(),
				ulUnicodeRange2 = reader.ReadUInt32(),
				ulUnicodeRange3 = reader.ReadUInt32(),
				ulUnicodeRange4 = reader.ReadUInt32(),
				achVendID = reader.ReadTag(),
				fsSelection = reader.ReadUInt16(),
				usFirstCharIndex = reader.ReadUInt16(),
				usLastCharIndex = reader.ReadUInt16(),
				sTypoAscender = reader.ReadInt16(),
				sTypoDescender = reader.ReadInt16(),
				sTypoLineGap = reader.ReadInt16(),
				usWinAscent = reader.ReadUInt16(),
				usWinDescent = reader.ReadUInt16()
			};
			if (value.version >= 1) {
				value.ulCodePageRange1 = reader.ReadUInt32();
				value.ulCodePageRange2 = reader.ReadUInt32();
			}
			if (value.version >= 4) {
				value.sxHeight = reader.ReadInt16();
				value.sCapHeight = reader.ReadInt16();
				value.usDefaultChar = reader.ReadUInt16();
				value.usBreakChar = reader.ReadUInt16();
				value.usMaxContext = reader.ReadUInt16();
			}
			if (value.version >= 5) {
				value.usLowerOpticalPointSize = reader.ReadUInt16();
				value.usUpperOpticalPointSize = reader.ReadUInt16();
			}
			return value;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"xAvgCharWidth\": {0},\n", xAvgCharWidth);
			builder.AppendFormat("\t\"usWeightClass\": {0},\n", usWeightClass);
			builder.AppendFormat("\t\"usWidthClass\": {0},\n", usWidthClass);
			builder.AppendFormat("\t\"fsType\": 0x{0:X4},\n", fsType);
			builder.AppendFormat("\t\"ySubscriptXSize\": {0},\n", ySubscriptXSize);
			builder.AppendFormat("\t\"ySubscriptYSize\": {0},\n", ySubscriptYSize);
			builder.AppendFormat("\t\"ySubscriptXOffset\": {0},\n", ySubscriptXOffset);
			builder.AppendFormat("\t\"ySubscriptYOffset\": {0},\n", ySubscriptYOffset);
			builder.AppendFormat("\t\"ySuperscriptXSize\": {0},\n", ySuperscriptXSize);
			builder.AppendFormat("\t\"ySuperscriptYSize\": {0},\n", ySuperscriptYSize);
			builder.AppendFormat("\t\"ySuperscriptXOffset\": {0},\n", ySuperscriptXOffset);
			builder.AppendFormat("\t\"ySuperscriptYOffset\": {0},\n", ySuperscriptYOffset);
			builder.AppendFormat("\t\"yStrikeoutSize\": {0},\n", yStrikeoutSize);
			builder.AppendFormat("\t\"yStrikeoutPosition\": {0},\n", yStrikeoutPosition);
			builder.AppendFormat("\t\"sFamilyClass\": {0},\n", sFamilyClass);
			builder.AppendFormat("\t\"panose[0]\": {0},\n", panose[0]);
			builder.AppendFormat("\t\"ulUnicodeRange1\": {0},\n", ulUnicodeRange1);
			builder.AppendFormat("\t\"ulUnicodeRange2\": {0},\n", ulUnicodeRange2);
			builder.AppendFormat("\t\"ulUnicodeRange3\": {0},\n", ulUnicodeRange3);
			builder.AppendFormat("\t\"ulUnicodeRange4\": {0},\n", ulUnicodeRange4);
			builder.AppendFormat("\t\"achVendID\": {0},\n", TypographyVendors.ToName(achVendID));
			builder.AppendFormat("\t\"fsSelection\": {0},\n", fsSelection);
			builder.AppendFormat("\t\"usFirstCharIndex\": {0},\n", usFirstCharIndex);
			builder.AppendFormat("\t\"usLastCharIndex\": {0},\n", usLastCharIndex);
			builder.AppendFormat("\t\"sTypoAscender\": {0},\n", sTypoAscender);
			builder.AppendFormat("\t\"sTypoDescender\": {0},\n", sTypoDescender);
			builder.AppendFormat("\t\"sTypoLineGap\": {0},\n", sTypoLineGap);
			builder.AppendFormat("\t\"usWinAscent\": {0},\n", usWinAscent);
			builder.AppendFormat("\t\"usWinDescent\": {0},\n", usWinDescent);
			if (version >= 1) {
				builder.AppendFormat("\t\"ulCodePageRange1\": {0},\n", ulCodePageRange1);
				builder.AppendFormat("\t\"ulCodePageRange2\": {0},\n", ulCodePageRange2);
			}
			if (version >= 4) {
				builder.AppendFormat("\t\"sxHeight\": {0},\n", sxHeight);
				builder.AppendFormat("\t\"sCapHeight\": {0},\n", sCapHeight);
				builder.AppendFormat("\t\"usDefaultChar\": {0},\n", usDefaultChar);
				builder.AppendFormat("\t\"usBreakChar\": {0},\n", usBreakChar);
				builder.AppendFormat("\t\"usMaxContext\": {0},\n", usMaxContext);
			}
			if (version >= 5) {
				builder.AppendFormat("\t\"usLowerOpticalPointSize\": {0},\n", usLowerOpticalPointSize);
				builder.AppendFormat("\t\"usUpperOpticalPointSize\": {0},\n", usUpperOpticalPointSize);
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
