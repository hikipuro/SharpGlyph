namespace SharpGlyph {
	public enum PaletteType : uint {
		/// <summary>
		/// Bit 0: palette is appropriate to use when displaying the font on a light background such as white.
		/// </summary>
		USABLE_WITH_LIGHT_BACKGROUND = 0x0001,
		
		/// <summary>
		/// Bit 1: palette is appropriate to use when displaying the font on a dark background such as black.
		/// </summary>
		USABLE_WITH_DARK_BACKGROUND = 0x0002,
		
		/// <summary>
		/// Reserved for future use — set to 0.
		/// </summary>
		Reserved = 0xFFFC
	}
}
