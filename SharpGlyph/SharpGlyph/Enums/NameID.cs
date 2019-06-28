namespace SharpGlyph {
	public class NameID {
		public static string ToName(ushort id) {
			switch (id) {
				case 0: return "Copyright notice";
				case 1: return "Font Family name";
				case 2: return "Font Subfamily name";
				case 3: return "Unique font identifier";
				case 4: return "Full font name";
				case 5: return "Version string";
				case 6: return "PostScript name";
				case 7: return "Trademark";
				case 8: return "Manufacturer Name";
				case 9: return "Designer";
				case 10: return "Description";
				case 11: return "URL Vendor";
				case 12: return "URL Designer";
				case 13: return "License Description";
				case 14: return "License Info URL";
				case 15: return "Reserved";
				case 16: return "Typographic Family name";
				case 17: return "Typographic Subfamily name";
				case 18: return "Compatible Full (Macintosh only)";
				case 19: return "Sample text";
				case 20: return "PostScript CID findfont name";
				case 21: return "WWS Family Name";
				case 22: return "WWS Subfamily Name";
				case 23: return "Light Background Palette";
				case 24: return "Dark Background Palette";
				case 25: return "Variations PostScript Name Prefix";
			}
			return id.ToString("X4");
		}
	}
}
