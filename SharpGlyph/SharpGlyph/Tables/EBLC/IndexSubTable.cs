using System;
namespace SharpGlyph {
	public class IndexSubTable {
		/// <summary>
		/// Header info.
		/// </summary>
		public IndexSubHeader header;

		protected long position;
		//protected string filePath;

		public static IndexSubTable Read(BinaryReaderFont reader, ushort firstGlyphIndex, ushort lastGlyphIndex) {
			ushort format = reader.PeekUInt16();
			switch (format) {
				case 1:
					return IndexSubTable1.Read(reader, lastGlyphIndex - firstGlyphIndex + 1);
				case 2:
					return IndexSubTable2.Read(reader);
				case 3:
					return IndexSubTable3.Read(reader, lastGlyphIndex - firstGlyphIndex + 1);
				case 4:
					return IndexSubTable4.Read(reader);
				case 5:
					return IndexSubTable5.Read(reader);
			}
			return null;
		}

		public BigGlyphMetrics GetBigGlyphMetrics() {
			if (header == null) {
				return null;
			}
			switch (header.indexFormat) {
				case 2:
					return (this as IndexSubTable2).bigMetrics;
				case 5:
					return (this as IndexSubTable5).bigMetrics;
			}
			return null;
		}

		public virtual GlyphBitmapData ReadBitmapData(BinaryReaderFont reader, int glyphId, int index) {
			return null;
		}

		public virtual GlyphBitmapData[] ReadGlyphBitmapData(BinaryReaderFont reader, int count) {
			return null;
		}
	}
}
