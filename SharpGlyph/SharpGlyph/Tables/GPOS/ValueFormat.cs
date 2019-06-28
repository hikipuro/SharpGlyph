using System;
namespace SharpGlyph {
	[Flags]
	public enum ValueFormat : ushort {
		X_PLACEMENT = 0x0001,
		Y_PLACEMENT = 0x0002,
		X_ADVANCE = 0x0004,
		Y_ADVANCE = 0x0008,
		X_PLACEMENT_DEVICE = 0x0010,
		Y_PLACEMENT_DEVICE = 0x0020,
		X_ADVANCE_DEVICE = 0x0040,
		Y_ADVANCE_DEVICE = 0x0080
	}
}
