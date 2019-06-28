using System;
namespace SharpGlyph {
	public class CFFCharset {
		public byte format;

		public static CFFCharset Read(BinaryReaderFont reader, int count) {
			CFFCharset charset = null;
			byte format = reader.PeekByte();
			switch (format) {
				case 0:
					charset = CFFCharset0.Read(reader);
					break;
				case 1:
					charset = CFFCharset1.Read(reader, count);
					break;
				case 2:
					charset = CFFCharset2.Read(reader, count);
					break;
			}
			return charset;
		}
	}
}
