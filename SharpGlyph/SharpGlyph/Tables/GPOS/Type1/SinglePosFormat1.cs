using System;
namespace SharpGlyph {
	public class SinglePosFormat1 {
		/// <summary>
		/// Format identifier: format = 1.
		/// </summary>
		public ushort posFormat;

		/// <summary>
		/// Offset to Coverage table,
		/// from beginning of SinglePos subtable.
		/// </summary>
		public ushort coverageOffset;

		/// <summary>
		/// Defines the types of data in the ValueRecord.
		/// </summary>
		public ushort valueFormat;

		/// <summary>
		/// Defines positioning value(s)
		/// — applied to all glyphs in the Coverage table.
		/// </summary>
		public ValueRecord valueRecord;

		public static SinglePosFormat1 Read(BinaryReaderFont reader) {
			return new SinglePosFormat1 {
				posFormat = reader.ReadUInt16(),
				coverageOffset = reader.ReadUInt16(),
				valueFormat = reader.ReadUInt16(),
			};
		}
	}
}
