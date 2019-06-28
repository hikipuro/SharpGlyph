using System;
namespace SharpGlyph {
	public class ItemVariationData {
		/// <summary>
		/// The number of delta sets for distinct items.
		/// </summary>
		public ushort itemCount;

		/// <summary>
		/// The number of deltas in each delta set
		/// that use a 16-bit representation.
		/// Must be less than or equal to regionIndexCount.
		/// </summary>
		public ushort shortDeltaCount;

		/// <summary>
		/// The number of variation regions referenced.
		/// </summary>
		public ushort regionIndexCount;

		/// <summary>
		/// Array of indices into the variation region list
		/// for the regions referenced by this item
		/// variation data table.
		/// </summary>
		public ushort[] regionIndexes;

		/// <summary>
		/// Delta-set rows.
		/// </summary>
		public DeltaSet[] deltaSets;

		public static ItemVariationData Read(BinaryReaderFont reader) {
			return new ItemVariationData {
				itemCount = reader.ReadUInt16(),
				shortDeltaCount = reader.ReadUInt16(),
				regionIndexCount = reader.ReadUInt16()
			};
		}
	}
}
