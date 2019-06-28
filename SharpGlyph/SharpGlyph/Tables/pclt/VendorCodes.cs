using System;
namespace SharpGlyph {
	public class VendorCodes {
		public static string ToName(int code) {
			switch (code) {
				case 1: return "Agfa Corporation";
				case 2: return "Bitstream Inc.";
				case 3: return "Linotype Company";
				case 4: return "Monotype Typography Ltd.";
				case 5: return "Adobe Systems";
				case 6: return "font repackagers";
				case 7: return "vendors of unique typefaces";
			}
			return "reserved";
		}
	}
}
