using System;
namespace SharpGlyph {
	public class GlyphDescription {
		public static GlyphDescription Read(BinaryReaderFont reader, Glyph glyph) {
			if (glyph.numberOfContours >= 0) {
				return SimpleGlyph.Read(reader, glyph);
			}
			return CompositeGlyph.Read(reader, glyph);
		}
	}
}
