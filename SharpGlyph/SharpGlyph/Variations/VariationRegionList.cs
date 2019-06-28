using System;
namespace SharpGlyph {
	public class VariationRegionList {
		/// <summary>
		/// The number of variation axes for this font.
		/// This must be the same number as axisCount in the 'fvar' table.
		/// </summary>
		public ushort axisCount;

		/// <summary>
		/// The number of variation region tables in the variation region list.
		/// </summary>
		public ushort regionCount;

		/// <summary>
		/// Array of variation regions.
		/// </summary>
		public VariationRegion[] variationRegions;

		public static VariationRegionList Read(BinaryReaderFont reader) {
			return new VariationRegionList {
				axisCount = reader.ReadUInt16(),
				regionCount = reader.ReadUInt16()
			};
		}
	}
}
