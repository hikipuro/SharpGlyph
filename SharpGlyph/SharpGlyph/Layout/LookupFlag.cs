using System;
namespace SharpGlyph {
	[System.Flags]
	public enum LookupFlag {
		/// <summary>
		/// This bit relates only to the correct processing
		/// of the cursive attachment lookup type (GPOS lookup type 3).
		/// When this bit is set, the last glyph in a given sequence
		/// to which the cursive attachment lookup is applied,
		/// will be positioned on the baseline.
		/// Note: Setting of this bit is not intended to be used
		/// by operating systems or applications to determine text direction.
		/// </summary>
		rightToLeft = 0x0001,
		
		/// <summary>
		/// If set, skips over base glyphs.
		/// </summary>
		ignoreBaseGlyphs = 0x0002,
		
		/// <summary>
		/// If set, skips over ligatures.
		/// </summary>
		ignoreLigatures = 0x0004,
		
		/// <summary>
		/// If set, skips over all combining marks.
		/// </summary>
		ignoreMarks = 0x0008,
		
		/// <summary>
		/// If set, indicates that the lookup table structure is
		/// followed by a MarkFilteringSet field.
		/// The layout engine skips over all mark glyphs not
		/// in the mark filtering set indicated.
		/// </summary>
		useMarkFilteringSet = 0x0010,
		
		/// <summary>
		/// For future use (Set to zero).
		/// </summary>
		reserved = 0x00E0,
		
		/// <summary>
		/// If not zero, skips over all marks of attachment
		/// type different from specified..
		/// </summary>
		markAttachmentType = 0xFF00,
	}
}
