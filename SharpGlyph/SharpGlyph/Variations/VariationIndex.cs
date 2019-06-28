using System;
namespace SharpGlyph {
	public class VariationIndex {
		/// <summary>
		/// A delta-set outer index — used to select an item
		/// variation data subtable within the item variation store.
		/// </summary>
		public ushort deltaSetOuterIndex;
		
		/// <summary>
		/// A delta-set inner index — used to select a delta-set
		/// row within an item variation data subtable..
		/// </summary>
		public ushort deltaSetInnerIndex;
		
		/// <summary>
		/// Format, = 0x8000.
		/// </summary>
		public ushort deltaFormat;

		public static VariationIndex Read(BinaryReaderFont reader) {
			return new VariationIndex {
				deltaSetOuterIndex = reader.ReadUInt16(),
				deltaSetInnerIndex = reader.ReadUInt16(),
				deltaFormat = reader.ReadUInt16()
			};
		}
	}
}
