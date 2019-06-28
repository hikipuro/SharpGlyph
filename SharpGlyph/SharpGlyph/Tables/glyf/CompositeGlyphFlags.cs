using System;
namespace SharpGlyph {
	[System.Flags]
	public enum CompositeGlyphFlags : ushort {
		/// <summary>
		/// Bit 0: If this is set, the arguments are 16-bit (uint16 or int16);
		/// otherwise, they are bytes (uint8 or int8).
		/// </summary>
		ARG_1_AND_2_ARE_WORDS = 0x0001,

		/// <summary>
		/// Bit 1: If this is set, the arguments are signed xy values;
		/// otherwise, they are unsigned point numbers.
		/// </summary>
		ARGS_ARE_XY_VALUES = 0x0002,

		/// <summary>
		/// Bit 2: For the xy values if the preceding is true.
		/// </summary>
		ROUND_XY_TO_GRID = 0x0004,

		/// <summary>
		/// Bit 3: This indicates that there is a simple scale
		/// for the component. Otherwise, scale = 1.0.
		/// </summary>
		WE_HAVE_A_SCALE = 0x0008,

		Reserved4 = 0x0010,

		/// <summary>
		/// Bit 5: Indicates at least one more glyph after this one.
		/// </summary>
		MORE_COMPONENTS = 0x0020,

		/// <summary>
		/// Bit 6: The x direction will use a different scale from the y direction.
		/// </summary>
		WE_HAVE_AN_X_AND_Y_SCALE = 0x0040,

		/// <summary>
		/// Bit 7: There is a 2 by 2 transformation that will be used to scale the component.
		/// </summary>
		WE_HAVE_A_TWO_BY_TWO = 0x0080,

		/// <summary>
		/// Bit 8: Following the last component are instructions for the composite character.
		/// </summary>
		WE_HAVE_INSTRUCTIONS = 0x0100,

		/// <summary>
		/// Bit 9: If set, this forces the aw and lsb (and rsb)
		/// for the composite to be equal to those from this original glyph.
		/// This works for hinted and unhinted characters.
		/// </summary>
		USE_MY_METRICS = 0x0200,

		/// <summary>
		/// Bit 10: If set, the components of the compound glyph overlap.
		/// Use of this flag is not required in OpenType — that is,
		/// it is valid to have components overlap without having
		/// this flag set. It may affect behaviors in some platforms, however. 
		/// </summary>
		OVERLAP_COMPOUND = 0x0400,

		/// <summary>
		/// Bit 11: The composite is designed to have the component offset scaled.
		/// </summary>
		SCALED_COMPONENT_OFFSET = 0x0800,

		/// <summary>
		/// Bit 12: The composite is designed not to have the component offset scaled.
		/// </summary>
		UNSCALED_COMPONENT_OFFSET = 0x1000,

		Reserved13 = 0x2000,
		Reserved14 = 0x4000,
		Reserved15 = 0x8000
	}
}
