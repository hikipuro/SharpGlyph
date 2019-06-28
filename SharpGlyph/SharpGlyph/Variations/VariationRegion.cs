using System;
namespace SharpGlyph {
	public class VariationRegion {
		/// <summary>
		/// Array of region axis coordinates records,
		/// in the order of axes given in the 'fvar' table.
		/// </summary>
		public RegionAxisCoordinates[] regionAxes;
	}
}
