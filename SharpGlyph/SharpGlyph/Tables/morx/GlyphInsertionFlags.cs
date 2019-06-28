using System;
namespace SharpGlyph {
	[Flags]
	public enum GlyphInsertionFlags : ushort {
		setMark = 0x8000,
		dontAdvance = 0x4000,
		currentIsKashidaLike = 0x2000,
		markedIsKashidaLike = 0x1000,
		currentInsertBefore = 0x0800,
		markedInsertBefore = 0x0400,
		currentInsertCount = 0x03E0,
		markedInsertCount = 0x001F
	}
}
