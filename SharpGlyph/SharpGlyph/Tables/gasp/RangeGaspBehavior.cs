using System;
namespace SharpGlyph {
	[Flags]
	public enum RangeGaspBehavior : ushort {
		/// <summary>
		/// Use gridfitting.
		/// </summary>
		GASP_GRIDFIT = 0x0001,

		/// <summary>
		/// Use grayscale rendering.
		/// </summary>
		GASP_DOGRAY = 0x0002,

		/// <summary>
		/// Use gridfitting with ClearType symmetric smoothing
		/// Only supported in version 1 'gasp'.
		/// </summary>
		GASP_SYMMETRIC_GRIDFIT = 0x0004,

		/// <summary>
		/// Use smoothing along multiple axes with ClearType
		/// Only supported in version 1 'gasp',
		/// </summary>
		GASP_SYMMETRIC_SMOOTHING = 0x0008
	}
}
