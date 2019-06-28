using System;
namespace SharpGlyph {
	public class ContextualGlyphSubstitutionSubtable {
		public STXHeader stxHeader;
		public uint substitutionTable;

		public ushort newState;
		public ushort flags;
		public ushort markIndex;
		public ushort currentIndex;
	}
}
