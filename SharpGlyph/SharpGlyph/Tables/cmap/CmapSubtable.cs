namespace SharpGlyph {
	public class CmapSubtable {
		/// <summary>
		/// Format number (0, 2, 4, 6, 8, 10, 12, 13 or 14).
		/// </summary>
		public ushort format;

		protected long position;
		protected string filePath;

		public static CmapSubtable Read(BinaryReaderFont reader) {
			switch (reader.PeekUInt16()) {
				case 0:
					return CmapSubtable0.Read(reader);
				case 2:
					return CmapSubtable2.Read(reader);
				case 4:
					return CmapSubtable4.Read(reader);
				case 6:
					return CmapSubtable6.Read(reader);
				case 8:
					return CmapSubtable8.Read(reader);
				case 10:
					return CmapSubtable10.Read(reader);
				case 12:
					return CmapSubtable12.Read(reader);
				case 13:
					return CmapSubtable13.Read(reader);
				case 14:
					return CmapSubtable14.Read(reader);
			}
			return null;
		}

		public virtual int GetGlyphId(int charCode) {
			return 0;
		}

		public virtual CharToGlyphTable CreateCharToGlyphTable() {
			return null;
		}
	}
}
