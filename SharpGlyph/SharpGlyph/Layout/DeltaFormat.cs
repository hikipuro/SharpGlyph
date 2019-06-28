using System;
namespace SharpGlyph {
	[System.Flags]
	public enum DeltaFormat {
		/// <summary>
		/// Signed 2-bit value, 8 values per uint16.
		/// </summary>
		LOCAL_2_BIT_DELTAS = 0x0001,
		
		/// <summary>
		/// Signed 4-bit value, 4 values per uint16.
		/// </summary>
		LOCAL_4_BIT_DELTAS = 0x0002,
		
		/// <summary>
		/// Signed 8-bit value, 2 values per uint16.
		/// </summary>
		LOCAL_8_BIT_DELTAS = 0x0003,
		
		/// <summary>
		/// VariationIndex table, contains a delta-set index pair.
		/// </summary>
		VARIATION_INDEX = 0x8000,
		
		/// <summary>
		/// For future use — set to 0.
		/// </summary>
		Reserved = 0x7FFC
	}
}
