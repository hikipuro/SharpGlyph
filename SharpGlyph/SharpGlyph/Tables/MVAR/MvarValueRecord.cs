using System;
namespace SharpGlyph {
	public class MvarValueRecord {
		/// <summary>
		/// Four-byte tag identifying a font-wide measure.
		/// </summary>
		public string valueTag;

		/// <summary>
		/// A delta-set outer index
		/// — used to select an item variation data
		/// subtable within the item variation store.
		/// </summary>
		public ushort deltaSetOuterIndex;

		/// <summary>
		/// A delta-set inner index
		/// — used to select a delta-set row within
		/// an item variation data subtable.
		/// </summary>
		public ushort deltaSetInnerIndex;

		public static MvarValueRecord Read(BinaryReaderFont reader) {
			return new MvarValueRecord {
				valueTag = reader.ReadTag(),
				deltaSetOuterIndex = reader.ReadUInt16(),
				deltaSetInnerIndex = reader.ReadUInt16()
			};
		}
	}
}
