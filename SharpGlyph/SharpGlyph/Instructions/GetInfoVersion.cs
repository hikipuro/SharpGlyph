using System;
namespace SharpGlyph {
	public class GetInfoVersion {
		public static string ToName(int version) {
			switch (version) {
				case 1: return "Macintosh System 6 INIT";
				case 2: return "Macintosh System 7";
				case 3: return "16-bit Windows 3.1";
				case 4: return "Macintosh System 6.2";
				case 5: return "Kirin Printer";
				case 6: return "Macintosh System 7.1";
				case 7: return "Macintosh QuickDraw GX";
				case 33: return "32-bit version, new scan-convertor";
				case 34: return "Font smoothing, new SCANTYPE";
				case 35: return "Composite scaling changes";
				case 36: return "\"Classic\" ClearType (Windows CE)";
				case 37: return "ClearType";
				case 38: return "Subpixel Positioned ClearType";
				case 39: return "Subpixel Positioned ClearType flag";
				case 40: return "Symmetric ClearType flag, ClearType Gray (and flag)";
				case 41: return "OpenType Font Variations";
				case 42: return "GETVARIATION support";
			}
			return version.ToString();
		}
	}
}
