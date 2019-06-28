using System.Collections.Generic;

namespace SharpGlyph {
	public class CharToGlyphTable : Dictionary<int, ushort> {
		public uint GetGlyphId(int charCode) {
			if (ContainsKey(charCode) == false) {
				return 0;
			}
			return this[charCode];
		}
	}
}
