using System;
namespace SharpGlyph {
	/// <summary>
	/// FontHeader flags value.
	/// </summary>
	[Flags]
	public enum FontHeaderFlags : ushort {
		/// <summary>
		/// Bit 0: y value of 0 specifies baseline.
		/// </summary>
		BaseLineY0 = 0x0001,

		/// <summary>
		/// Bit 1: x position of left most black bit is LSB.
		/// </summary>
		LeftSidebearingX0 = 0x0002,

		/// <summary>
		/// Bit 2: scaled point size and actual point size will differ
		/// (i.e. 24 point glyph differs from 12 point glyph scaled by factor of 2).
		/// </summary>
		DependOnPointSize = 0x0004,

		/// <summary>
		/// Bit 3: use integer scaling instead of fractional.
		/// </summary>
		ForcePpemToInteger = 0x0008,

		/// <summary>
		/// Bit 4: Instructions may alter advance width
		/// (the advance widths might not scale linearly);
		/// </summary>
		AlterAdvanceWidth = 0x0010,

		/// <summary>
		/// <para>
		/// [Apple]
		/// Bit 5: This bit should be set in fonts that are intended to
		/// e laid out vertically, and in which the glyphs
		/// have been drawn such that an x-coordinate of 0.
		/// </para>
		/// </summary>
		Vertical = 0x0020,

		/// <summary>
		/// Bit 6: This bit must be set to zero.
		/// </summary>
		NotUsed6 = 0x0040,

		/// <summary>
		/// [Apple]
		/// Bit 7: This bit should be set if the font requires layout
		/// for correct linguistic rendering (e.g. Arabic fonts).
		/// </summary>
		RequiresLayout = 0x0080,

		/// <summary>
		/// [Apple]
		/// Bit 8: This bit should be set for an AAT font which has one or more.
		/// metamorphosis effects designated as happening by default.
		/// </summary>
		AATFont = 0x0100,

		/// <summary>
		/// [Apple]
		/// Bit 9: This bit should be set if the font contains any strong right-to-left glyphs.
		/// </summary>
		StrongRightToLeft = 0x0200,

		/// <summary>
		/// [Apple]
		/// Bit 10: This bit should be set if the font contains Indic-style rearrangement effects.
		/// </summary>
		IndicStyle = 0x0400,

		/// <summary>
		/// Bit 11: Font data is “lossless” as a result
		/// of having been subjected to optimizing transformation
		/// and/or compression (such as e.g. compression mechanisms
		/// defined by ISO/IEC 14496-18, MicroType Express, WOFF
		/// 2.0 or similar) where the original font functionality
		/// and features are retained but the binary compatibility
		/// between input and output font files is not guaranteed.
		/// As a result of the applied transform,
		/// the DSIG table may also be invalidated.
		/// </summary>
		Lossless = 0x0800,

		/// <summary>
		/// Bit 12: Font converted (produce compatible metrics).
		/// </summary>
		Converted = 0x1000,

		/// <summary>
		/// Bit 13: Font optimized for ClearType™. Note,
		/// fonts that rely on embedded bitmaps (EBDT) for
		/// rendering should not be considered optimized for ClearType,
		/// and therefore should keep this bit cleared.
		/// </summary>
		OptimizedForClearType = 0x2000,

		/// <summary>
		/// Bit 14: This bit should be set if the glyphs in the font are simply
		/// generic symbols for code point ranges, such as for a last resort font.
		/// </summary>
		LastResort = 0x4000,

		/// <summary>
		/// Bit 15: Reserved, set to 0.
		/// </summary>
		Reserved15 = 0x8000
	}
}
