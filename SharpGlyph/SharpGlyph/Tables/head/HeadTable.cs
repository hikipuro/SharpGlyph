using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Font Header Table (head).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class HeadTable : Table {
		public const string Tag = "head";
		public const uint MagicNumber = 0x5F0F3CF5;
		
		/// <summary>
		/// Major version number of the font header table — set to 1.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version number of the font header table — set to 0.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// Set by font manufacturer.
		/// </summary>
		public uint fontRevision;
		
		/// <summary>
		/// To compute: set it to 0, sum the entire font as uint32, then store 0xB1B0AFBA - sum.
		/// If the font is used as a component in a font collection file,
		/// the value of this field will be invalidated by changes to the file structure
		/// and font table directory, and must be ignored.
		/// </summary>
		public uint checkSumAdjustment;
		
		/// <summary>
		/// Set to 0x5F0F3CF5.
		/// </summary>
		public uint magicNumber;

		/// <summary>
		/// <para>Bit 0: Baseline for font at y=0;</para>
		/// <para>Bit 1: Left sidebearing point at x=0
		/// (relevant only for TrueType rasterizers);</para>
		/// <para>Bit 2: Instructions may depend on point size;</para>
		/// <para>Bit 3: Force ppem to integer values for all internal
		/// scaler math; may use fractional ppem sizes if this bit is clear;</para>
		/// <para>Bit 4: Instructions may alter advance width
		/// (the advance widths might not scale linearly);</para>
		/// <para>Bit 5: This bit is not used in OpenType,
		/// and should not be set in order to ensure compatible
		/// behavior on all platforms. If set, it may result in
		/// different behavior for vertical layout in some platforms.
		/// (See Apple’s specification for details regarding behavior in Apple platforms.)</para>
		/// <para>Bits 6–10: These bits are not used in Opentype
		/// and should always be cleared.
		/// (See Apple’s specification for details regarding
		/// legacy used in Apple platforms.)</para>
		/// <para>Bit 11: Font data is “lossless” as a result
		/// of having been subjected to optimizing transformation
		/// and/or compression (such as e.g. compression mechanisms
		/// defined by ISO/IEC 14496-18, MicroType Express, WOFF
		/// 2.0 or similar) where the original font functionality
		/// and features are retained but the binary compatibility
		/// between input and output font files is not guaranteed.
		/// As a result of the applied transform,
		/// the DSIG table may also be invalidated.</para>
		/// <para>Bit 12: Font converted (produce compatible metrics)</para>
		/// <para>Bit 13: Font optimized for ClearType™. Note,
		/// fonts that rely on embedded bitmaps (EBDT) for
		/// rendering should not be considered optimized for ClearType,
		/// and therefore should keep this bit cleared.</para>
		/// <para>Bit 14: Last Resort font. If set, indicates that
		/// the glyphs encoded in the 'cmap' subtables are simply
		/// generic symbolic representations of code point ranges
		/// and don’t truly represent support for those code points.
		/// If unset, indicates that the glyphs encoded in the 'cmap'
		/// subtables represent proper support for those code points.</para>
		/// <para>Bit 15: Reserved, set to 0</para>
		/// </summary>
		public FontHeaderFlags flags;

		/// <summary>
		/// Set to a value from 16 to 16384. Any value in this range is valid.
		/// In fonts that have TrueType outlines, a power of 2 is recommended
		/// as this allows performance optimizations in some rasterizers.
		/// <para>
		/// [Apple] range from 64 to 16384.
		/// </para>
		/// </summary>
		public ushort unitsPerEm;
		
		/// <summary>
		/// Number of seconds since 12:00 midnight that started
		/// January 1st 1904 in GMT/UTC time zone. 64-bit integer.
		/// </summary>
		public DateTime created;
		
		/// <summary>
		/// Number of seconds since 12:00 midnight that started
		/// January 1st 1904 in GMT/UTC time zone. 64-bit integer.
		/// </summary>
		public DateTime modified;
		
		/// <summary>
		/// For all glyph bounding boxes.
		/// </summary>
		public short xMin;
		
		/// <summary>
		/// For all glyph bounding boxes.
		/// </summary>
		public short yMin;
		
		/// <summary>
		/// For all glyph bounding boxes.
		/// </summary>
		public short xMax;
		
		/// <summary>
		/// For all glyph bounding boxes.
		/// </summary>
		public short yMax;

		/// <summary>
		/// <para>Bit 0: Bold (if set to 1)</para>
		/// <para>Bit 1: Italic (if set to 1)</para>
		/// <para>Bit 2: Underline (if set to 1)</para>
		/// <para>Bit 3: Outline (if set to 1)</para>
		/// <para>Bit 4: Shadow (if set to 1)</para>
		/// <para>Bit 5: Condensed (if set to 1)</para>
		/// <para>Bit 6: Extended (if set to 1)</para>
		/// <para>Bits 7–15: Reserved (set to 0)</para>
		/// </summary>
		public FontHeaderMacStyle macStyle;
		
		/// <summary>
		/// Smallest readable size in pixels.
		/// </summary>
		public ushort lowestRecPPEM;

		/// <summary>
		/// Deprecated (Set to 2).
		/// <para>0: Fully mixed directional glyphs;</para>
		/// <para>1: Only strongly left to right;</para>
		/// <para>2: Like 1 but also contains neutrals;</para>
		/// <para>-1: Only strongly right to left;</para>
		/// <para>-2: Like -1 but also contains neutrals.</para>
		/// (A neutral character has no inherent directionality;
		/// it is not a character with zero (0) width.
		/// Spaces and punctuation are examples of neutral characters.
		/// Non-neutral characters are those with inherent directionality.
		/// For example, Roman letters (left-to-right) and Arabic letters (right-to-left)
		/// have directionality. In a “normal” Roman font where spaces and punctuation are present,
		/// the font direction hints should be set to two (2).)
		/// </summary>
		public short fontDirectionHint;
		
		/// <summary>
		/// 0 for short offsets (Offset16),
		/// 1 for long (Offset32).
		/// </summary>
		public short indexToLocFormat;
		
		/// <summary>
		/// 0 for current format.
		/// </summary>
		public short glyphDataFormat;
		
		public static HeadTable Read(BinaryReaderFont reader) {
			return new HeadTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				fontRevision = reader.ReadFixed(),
				checkSumAdjustment = reader.ReadUInt32(),
				magicNumber = reader.ReadUInt32(),
				flags = (FontHeaderFlags)reader.ReadUInt16(),
				unitsPerEm = reader.ReadUInt16(),
				created = reader.ReadDateTime(),
				modified = reader.ReadDateTime(),
				xMin = reader.ReadInt16(),
				yMin = reader.ReadInt16(),
				xMax = reader.ReadInt16(),
				yMax = reader.ReadInt16(),
				macStyle = (FontHeaderMacStyle)reader.ReadUInt16(),
				lowestRecPPEM = reader.ReadUInt16(),
				fontDirectionHint = reader.ReadInt16(),
				indexToLocFormat = reader.ReadInt16(),
				glyphDataFormat = reader.ReadInt16()
			};
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"fontRevision\": 0x{0:X8},\n", fontRevision);
			builder.AppendFormat("\t\"checkSumAdjustment\": 0x{0:X8},\n", checkSumAdjustment);
			builder.AppendFormat("\t\"magicNumber\": 0x{0:X8},\n", magicNumber);
			builder.AppendFormat("\t\"flags\": \"{0}\",\n", flags);
			builder.AppendFormat("\t\"unitsPerEm\": {0},\n", unitsPerEm);
			builder.AppendFormat("\t\"created\": {0},\n", created);
			builder.AppendFormat("\t\"modified\": {0},\n", modified);
			builder.AppendFormat("\t\"xMin\": {0},\n", xMin);
			builder.AppendFormat("\t\"yMin\": {0},\n", yMin);
			builder.AppendFormat("\t\"xMax\": {0},\n", xMax);
			builder.AppendFormat("\t\"yMax\": {0},\n", yMax);
			builder.AppendFormat("\t\"macStyle\": {0},\n", macStyle);
			builder.AppendFormat("\t\"lowestRecPPEM\": {0},\n", lowestRecPPEM);
			builder.AppendFormat("\t\"fontDirectionHint\": {0},\n", fontDirectionHint);
			builder.AppendFormat("\t\"indexToLocFormat\": {0},\n", indexToLocFormat);
			builder.AppendFormat("\t\"glyphDataFormat\": {0},\n", glyphDataFormat);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
