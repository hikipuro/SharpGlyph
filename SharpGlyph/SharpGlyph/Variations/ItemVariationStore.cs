using System;
namespace SharpGlyph {
	public class ItemVariationStore {
		/// <summary>
		/// Format — set to 1.
		/// </summary>
		public ushort format;

		/// <summary>
		/// Offset in bytes from the start of the item
		/// variation store to the variation region list.
		/// </summary>
		public uint variationRegionListOffset;

		/// <summary>
		/// The number of item variation data subtables.
		/// </summary>
		public ushort itemVariationDataCount;

		/// <summary>
		/// Offsets in bytes from the start of the item
		/// variation store to each item variation data subtable.
		/// </summary>
		public uint[] itemVariationDataOffsets;

		public static ItemVariationStore Read(BinaryReaderFont reader) {
			return new ItemVariationStore {
				format = reader.ReadUInt16(),
				variationRegionListOffset = reader.ReadUInt32(),
				itemVariationDataCount = reader.ReadUInt16()
			};
		}
	}
}
