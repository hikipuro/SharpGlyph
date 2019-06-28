using System;
using System.Collections.Generic;
using System.IO;

namespace SharpGlyph {
	public class FontParser {
		public static Font[] Parse(string path) {
			Font[] fonts = null;
			if (File.Exists(path) == false) {
				return fonts;
			}
			using (Stream stream = File.OpenRead(path))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.FilePath = path;
				string tag = reader.ReadTag();
				stream.Position = 0;
				switch (tag) {
					case TTCHeader.Tag:
						fonts = ParseTTC(reader);
						break;
					default:
						fonts = new Font[] { Font.Read(reader) };
						break;
				}
			}
			return fonts;
		}

		protected static Font[] ParseTTC(BinaryReaderFont reader) {
			List<Font> fonts = new List<Font>();
			TTCHeader ttcHeader = TTCHeader.Read(reader);
			//Console.WriteLine(ttcHeader);
			for (int i = 0; i < ttcHeader.numFonts; i++) {
				uint fontOffset = ttcHeader.offsetTable[i];
				reader.Position = fontOffset;
				Font font = Font.Read(reader);
				fonts.Add(font);
			}
			return fonts.ToArray();
		}
	}
}
