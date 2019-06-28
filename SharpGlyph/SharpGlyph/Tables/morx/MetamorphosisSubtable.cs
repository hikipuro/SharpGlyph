using System;
namespace SharpGlyph {
	public class MetamorphosisSubtable {
		/// <summary>
		/// Total subtable length, including this header.
		/// </summary>
		public uint length;

		/// <summary>
		/// Coverage flags and subtable type.
		/// </summary>
		public uint coverage;

		/// <summary>
		/// The 32-bit mask identifying which subtable this is
		/// (the subtable being executed if the AND of this
		/// value and the processed defaultFlags is nonzero).
		/// </summary>
		public uint subFeatureFlags;
	}
}
