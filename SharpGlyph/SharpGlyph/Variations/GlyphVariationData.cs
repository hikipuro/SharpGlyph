using System;
namespace SharpGlyph {
	public class GlyphVariationData {
		/// <summary>
		/// A packed field.
		/// <para>
		/// The high 4 bits are flags (see below),
		/// and the low 12 bits are the number of
		/// tuple variation tables for this glyph.
		/// The count can be any number between 1 and 4095.
		/// </para>
		/// </summary>
		public ushort tupleVariationCount;

		/// <summary>
		/// Offset from the start of
		/// the GlyphVariationData table to the serialized data.
		/// </summary>
		public ushort dataOffset;

		/// <summary>
		/// Array of tuple variation headers.
		/// </summary>
		public TupleVariationHeader[] tupleVariationHeaders;

		public static GlyphVariationData Read(BinaryReaderFont reader) {
			return new GlyphVariationData {
				tupleVariationCount = reader.ReadUInt16(),
				dataOffset = reader.ReadUInt16()
			};
		}
	}
}
