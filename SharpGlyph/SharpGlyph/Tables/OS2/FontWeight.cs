using System;
namespace SharpGlyph {
	public class FontWeight {
		public static string ToName(ushort weight) {
			switch (weight) {
				case 100: return "Thin";
				case 200: return "Extra-light (Ultra-light)";
				case 300: return "Light";
				case 400: return "Normal (Regular)";
				case 500: return "Medium";
				case 600: return "Semi-bold (Demi-bold)";
				case 700: return "Bold";
				case 800: return "Extra-bold (Ultra-bold)";
				case 900: return "Black (Heavy)";
			}
			return weight.ToString();
		}
	}
}
