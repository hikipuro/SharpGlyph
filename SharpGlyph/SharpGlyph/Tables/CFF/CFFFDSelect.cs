using System;
namespace SharpGlyph {
	public class CFFFDSelect {
		public byte format;

		public static CFFFDSelect Read(BinaryReaderFont reader) {
			CFFFDSelect fdSelect = null;
			byte format = reader.PeekByte();
			switch (format) {
				case 0:
					fdSelect = CFFFDSelect0.Read(reader);
					break;
				case 3:
					fdSelect = CFFFDSelect3.Read(reader);
					break;
			}
			return fdSelect;
		}
	}
}
