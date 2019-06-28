using System;
namespace SharpGlyph {
	public class EncodingID {
		public class Unicode {
			public static readonly ushort Unicode_1 = 0;
			public static readonly ushort Unicode_1_1 = 1;
			public static readonly ushort ISO_IEC_10646 = 2;
			public static readonly ushort Unicode_2_BMP = 3;
			public static readonly ushort Unicode_2_Full = 4;
			public static readonly ushort UnicodeVariation = 5;
			public static readonly ushort UnicodeFull = 6;

			public static string ToName(ushort id) {
				switch (id) {
					case 0: return "Unicode 1.0 semantics";
					case 1: return "Unicode 1.1 semantics";
					case 2: return "ISO/IEC 10646 semantics";
					case 3: return "Unicode 2.0 and onwards semantics, Unicode BMP only";
					case 4: return "Unicode 2.0 and onwards semantics, Unicode full repertoire";
					case 5: return "Unicode Variation Sequences";
					case 6: return "Unicode full repertoire";
				}
				return id.ToString("X4");
			}
		}

		public class Macintosh {
			public static readonly ushort Roman = 0;
			public static readonly ushort Japanese = 1;
			public static readonly ushort ChineseTraditional = 2;
			public static readonly ushort Korean = 3;
			public static readonly ushort Arabic = 4;
			public static readonly ushort Hebrew = 5;
			public static readonly ushort Greek = 6;
			public static readonly ushort Russian = 7;
			public static readonly ushort RSymbol = 8;
			public static readonly ushort Devanagari = 9;
			public static readonly ushort Gurmukhi = 10;
			public static readonly ushort Gujarati = 11;
			public static readonly ushort Oriya = 12;
			public static readonly ushort Bengali = 13;
			public static readonly ushort Tamil = 14;
			public static readonly ushort Telugu = 15;
			public static readonly ushort Kannada = 16;
			public static readonly ushort Malayalam = 17;
			public static readonly ushort Sinhalese = 18;
			public static readonly ushort Burmese = 19;
			public static readonly ushort Khmer = 20;
			public static readonly ushort Thai = 21;
			public static readonly ushort Laotian = 22;
			public static readonly ushort Georgian = 23;
			public static readonly ushort Armenian = 24;
			public static readonly ushort ChineseSimplified = 25;
			public static readonly ushort Tibetan = 26;
			public static readonly ushort Mongolian = 27;
			public static readonly ushort Geez = 28;
			public static readonly ushort Slavic = 29;
			public static readonly ushort Vietnamese = 30;
			public static readonly ushort Sindhi = 31;
			public static readonly ushort Uninterpreted = 32;

			public static string ToName(ushort id) {
				switch (id) {
					case 0: return "Roman";
					case 1: return "Japanese";
					case 2: return "Chinese (Traditional)";
					case 3: return "Korean";
					case 4: return "Arabic";
					case 5: return "Hebrew";
					case 6: return "Greek";
					case 7: return "Russian";
					case 8: return "RSymbol";
					case 9: return "Devanagari";
					case 10: return "Gurmukhi";
					case 11: return "Gujarati";
					case 12: return "Oriya";
					case 13: return "Bengali";
					case 14: return "Tamil";
					case 15: return "Telugu";
					case 16: return "Kannada";
					case 17: return "Malayalam";
					case 18: return "Sinhalese";
					case 19: return "Burmese";
					case 20: return "Khmer";
					case 21: return "Thai";
					case 22: return "Laotian";
					case 23: return "Georgian";
					case 24: return "Armenian";
					case 25: return "Chinese (Simplified)";
					case 26: return "Tibetan";
					case 27: return "Mongolian";
					case 28: return "Geez";
					case 29: return "Slavic";
					case 30: return "Vietnamese";
					case 31: return "Sindhi";
					case 32: return "Uninterpreted";
				}
				return id.ToString("X4");
			}
		}

		//[System.Obsolete]
		public class ISO {
			public static readonly ushort ASCII_7bit = 0;
			public static readonly ushort ISO_10646 = 1;
			public static readonly ushort ISO_8859_1 = 2;

			public static string ToName(ushort id) {
				switch (id) {
					case 0: return "7-bit ASCII";
					case 1: return "ISO 10646";
					case 2: return "ISO 8859-1";
				}
				return id.ToString("X4");
			}
		}

		public class Windows {
			public static readonly ushort Symbol = 0;
			public static readonly ushort UnicodeBMP = 1;
			public static readonly ushort ShiftJIS = 2;
			public static readonly ushort PRC = 3;
			public static readonly ushort Big5 = 4;
			public static readonly ushort Wansung = 5;
			public static readonly ushort Johab = 6;
			public static readonly ushort UnicodeFullRepertoire = 10;

			public static string ToName(ushort id) {
				switch (id) {
					case 0: return "Symbol";
					case 1: return "Unicode BMP";
					case 2: return "ShiftJIS";
					case 3: return "PRC";
					case 4: return "Big5";
					case 5: return "Wansung";
					case 6: return "Johab";
					case 7: return "Reserved";
					case 8: return "Reserved";
					case 9: return "Reserved";
					case 10: return "Unicode full repertoire";
				}
				return id.ToString("X4");
			}
		}

		public class Custom {
			public static string ToName(ushort id) {
				if (0 <= id && 255 >= id) {
					return "OTF Windows NT compatibility mapping";
				}
				return id.ToString("X4");
			}
		}

		public static string ToName(PlatformID platformID, ushort encodingID) {
			switch (platformID) {
				case PlatformID.Unicode:
					return Unicode.ToName(encodingID);
				case PlatformID.Macintosh:
					return Macintosh.ToName(encodingID);
				case PlatformID.ISO:
					return ISO.ToName(encodingID);
				case PlatformID.Windows:
					return Windows.ToName(encodingID);
				case PlatformID.Custom:
					return Custom.ToName(encodingID);
				
			}
			return encodingID.ToString("X4");
		}
	}
}
