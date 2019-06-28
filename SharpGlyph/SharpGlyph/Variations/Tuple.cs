using System;
namespace SharpGlyph {
	public class Tuple {
		/// <summary>
		/// Coordinate array specifying a position within
		/// the font’s variation space.
		/// The number of elements must match the axisCount
		/// specified in the 'fvar' table.
		/// </summary>
		public float[] coordinates;

		public static Tuple Read(BinaryReaderFont reader) {
			return new Tuple();
		}
	}
}
