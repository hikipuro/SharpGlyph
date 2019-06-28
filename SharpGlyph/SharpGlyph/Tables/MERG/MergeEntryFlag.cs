using System;
namespace SharpGlyph {
	[Flags]
	public enum MergeEntryFlag : byte {
		/// <summary>
		/// Merge glyphs, for LTR visual order.
		/// </summary>
		MERGE_LTR = 0x01,

		/// <summary>
		/// Group glyphs, for LTR visual order.
		/// </summary>
		GROUP_LTR = 0x02,

		/// <summary>
		/// Second glyph is subordinate to the first glyph, for LTR visual order.
		/// </summary>
		SECOND_IS_SUBORDINATE_LTR = 0x04,

		/// <summary>
		/// Flag reserved for future use — set to 0.
		/// </summary>
		Reserved0 = 0x08,

		/// <summary>
		/// Merge glyphs, for RTL visual order.
		/// </summary>
		MERGE_RTL = 0x10,

		/// <summary>
		/// Group glyphs, for RTL visual order.
		/// </summary>
		GROUP_RTL = 0x20,

		/// <summary>
		/// Second glyph is subordinate to the first glyph, for RTL visual order.
		/// </summary>
		SECOND_IS_SUBORDINATE_RTL = 0x40,

		/// <summary>
		/// Flag reserved for future use — set to 0.
		/// </summary>
		Reserved1 = 0x80
	}
}
