using System;
namespace SharpGlyph {
	public class GlyphBitmapData {
		public int format;

		public static GlyphBitmapData[] ReadArray(BinaryReaderFont reader, ushort imageFormat, int byteSize, int count) {
			GlyphBitmapData[] array = new GlyphBitmapData[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader, imageFormat, byteSize);
			}
			return array;
		}

		public static GlyphBitmapData Read(BinaryReaderFont reader, ushort imageFormat, int byteSize) {
			switch (imageFormat) {
				case 1:
					return GlyphBitmapData1.Read(reader, byteSize);
				case 2:
					return GlyphBitmapData2.Read(reader, byteSize);
				case 5:
					return GlyphBitmapData5.Read(reader, byteSize);
				case 6:
					return GlyphBitmapData6.Read(reader, byteSize);
				case 7:
					return GlyphBitmapData7.Read(reader, byteSize);
				case 8:
					return GlyphBitmapData8.Read(reader);
				case 9:
					return GlyphBitmapData9.Read(reader);
				case 17:
					return GlyphBitmapData17.Read(reader);
				case 18:
					return GlyphBitmapData18.Read(reader);
				case 19:
					return GlyphBitmapData19.Read(reader);
			}
			return null;
		}
	}
}
