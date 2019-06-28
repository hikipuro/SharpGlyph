namespace SharpGlyph {
	[System.Flags]
	public enum SimpleGlyphFlags : byte {
		/// <summary>
		/// Bit 0: If set, the point is on the curve; otherwise, it is off the curve.
		/// </summary>
		ON_CURVE_POINT = 0x01,
		
		/// <summary>
		/// Bit 1: If set, the corresponding x-coordinate is 1 byte long.
		/// If not set, it is two bytes long. For the sign of this value,
		/// see the description of the X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR flag.
		/// </summary>
		X_SHORT_VECTOR = 0x02,
		
		/// <summary>
		/// Bit 2: If set, the corresponding y-coordinate is 1 byte long.
		/// If not set, it is two bytes long. For the sign of this value,
		/// see the description of the Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR flag.
		/// </summary>
		Y_SHORT_VECTOR = 0x04,
		
		/// <summary>
		/// Bit 3: If set, the next byte (read as unsigned) specifies
		/// the number of additional times this flag byte is to be repeated
		/// in the logical flags array — that is, the number of additional
		/// logical flag entries inserted after this entry.
		/// (In the expanded logical array, this bit is ignored.)
		/// In this way, the number of flags listed can be smaller than
		/// the number of points in the glyph description.
		/// </summary>
		REPEAT_FLAG = 0x08,
		
		/// <summary>
		/// Bit 4: This flag has two meanings, depending on how the X_SHORT_VECTOR flag is set.
		/// If X_SHORT_VECTOR is set, this bit describes the sign of the value,
		/// with 1 equalling positive and 0 negative.
		/// If X_SHORT_VECTOR is not set and this bit is set,
		/// then the current x-coordinate is the same as the previous x-coordinate.
		/// If X_SHORT_VECTOR is not set and this bit is also not set,
		/// the current x-coordinate is a signed 16-bit delta vector.
		/// </summary>
		X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR = 0x10,
		
		
		/// <summary>
		/// Bit 5: This flag has two meanings, depending on how the Y_SHORT_VECTOR flag is set.
		/// If Y_SHORT_VECTOR is set, this bit describes the sign of the value,
		/// with 1 equalling positive and 0 negative.
		/// If Y_SHORT_VECTOR is not set and this bit is set,
		/// then the current y-coordinate is the same as the previous y-coordinate.
		/// If Y_SHORT_VECTOR is not set and this bit is also not set,
		/// the current y-coordinate is a signed 16-bit delta vector.
		/// </summary>
		Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR = 0x20,
		
		/// <summary>
		/// Bit 6: If set, contours in the glyph description may overlap.
		/// Use of this flag is not required in OpenType — that is,
		/// it is valid to have contours overlap without having this flag set.
		/// It may affect behaviors in some platforms, however.
		/// (See the discussion of “Overlapping contours” in Apple’s specification
		/// for details regarding behavior in Apple platforms.)
		/// When used, it must be set on the first flag byte for the glyph.
		/// </summary>
		OVERLAP_SIMPLE = 0x40,
		
		/// <summary>
		/// Bit 7 is reserved: set to zero.
		/// </summary>
		Reserved = 0x80
	}
}
