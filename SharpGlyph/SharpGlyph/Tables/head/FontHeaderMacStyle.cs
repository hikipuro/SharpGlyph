using System;
namespace SharpGlyph {
	/// <summary>
	/// FontHeader macStyle value.
	/// </summary>
	[Flags]
	public enum FontHeaderMacStyle : ushort {
		/// <summary>
		/// Bit 0: Bold (if set to 1).
		/// </summary>
		Bold = 0x0001,

		/// <summary>
		/// Bit 1: Italic (if set to 1).
		/// </summary>
		Italic = 0x0002,

		/// <summary>
		/// Bit 2: Underline (if set to 1).
		/// </summary>
		Underline = 0x0004,

		/// <summary>
		/// Bit 3: Outline (if set to 1).
		/// </summary>
		Outline = 0x0008,

		/// <summary>
		/// Bit 4: Shadow (if set to 1).
		/// </summary>
		Shadow = 0x0010,

		/// <summary>
		/// Bit 5: Condensed (if set to 1).
		/// </summary>
		Condensed = 0x0020,

		/// <summary>
		/// Bit 6: Extended (if set to 1).
		/// </summary>
		Extended = 0x0040,

		Reserved7 = 0x0080,
		Reserved8 = 0x0100,
		Reserved9 = 0x0200,
		Reserved10 = 0x0400,
		Reserved11 = 0x0800,
		Reserved12 = 0x1000,
		Reserved13 = 0x2000,
		Reserved14 = 0x4000,
		Reserved15 = 0x8000
	}
}
