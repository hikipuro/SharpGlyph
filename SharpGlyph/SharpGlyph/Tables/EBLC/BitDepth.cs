using System;
namespace SharpGlyph {
	[System.Flags]
	public enum BitDepth : byte {
		/// <summary>
		/// Bit 0: black/white.
		/// </summary>
		BlackWhite = 0x01,

		/// <summary>
		/// Bit 1: 4 levels of gray.
		/// </summary>
		Gray4 = 0x02,

		/// <summary>
		/// Bit 2: 16 levels of gray.
		/// </summary>
		Gray16 = 0x04,

		/// <summary>
		/// Bit 3: 256 levels of gray.
		/// </summary>
		Gray256 = 0x08,
		Reserved4 = 0x10,
		Reserved5 = 0x20,
		Reserved6 = 0x40,
		Reserved7 = 0x80
	}
}
