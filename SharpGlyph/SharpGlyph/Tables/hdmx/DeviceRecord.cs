using System;
namespace SharpGlyph {
	public class DeviceRecord {
		/// <summary>
		/// Pixel size for following widths (as ppem).
		/// </summary>
		public byte pixelSize;

		/// <summary>
		/// Maximum width.
		/// </summary>
		public byte maxWidth;

		/// <summary>
		/// Array of widths (numGlyphs is from the 'maxp' table).
		/// </summary>
		public byte[] widths;
	}
}
