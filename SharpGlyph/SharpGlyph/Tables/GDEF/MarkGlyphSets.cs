using System;
namespace SharpGlyph {
	public class MarkGlyphSets {
		/// <summary>
		/// Format identifier == 1.
		/// </summary>
		public ushort markGlyphSetTableFormat;

		/// <summary>
		/// Number of mark glyph sets defined.
		/// </summary>
		public ushort markGlyphSetCount;

		/// <summary>
		/// Array of offsets to mark glyph set coverage tables.
		/// </summary>
		public uint[] coverageOffsets;

		public static MarkGlyphSets Read(BinaryReaderFont reader) {
			return new MarkGlyphSets {
				markGlyphSetTableFormat = reader.ReadUInt16(),
				markGlyphSetCount = reader.ReadUInt16()
			};
		}
	}
}
