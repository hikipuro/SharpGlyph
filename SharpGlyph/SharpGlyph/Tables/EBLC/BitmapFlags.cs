using System;
namespace SharpGlyph {
	[System.Flags]
	public enum BitmapFlags : byte {
		/// <summary>
		/// Bit 0: Horizontal.
		/// </summary>
		HORIZONTAL_METRICS = 0x01,

		/// <summary>
		/// Bit 1: Vertical.
		/// </summary>
		VERTICAL_METRICS = 0x02,
		Reserved2 = 0x04,
		Reserved3 = 0x08,
		Reserved4 = 0x10,
		Reserved5 = 0x20,
		Reserved6 = 0x40,
		Reserved7 = 0x80
	}
}
