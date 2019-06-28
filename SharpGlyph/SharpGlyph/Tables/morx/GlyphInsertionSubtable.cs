using System;
namespace SharpGlyph {
	public class GlyphInsertionSubtable {
		/// <summary>
		/// Extended state table header.
		/// </summary>
		public STXHeader stateHeader;

		/// <summary>
		/// Byte offset from stateHeader to the start
		/// of the insertion glyph table.
		/// </summary>
		public uint insertionActionOffset;

		/// <summary>
		/// Zero-based index of the new state.
		/// </summary>
		public ushort newState;

		/// <summary>
		/// The action flags.
		/// </summary>
		public ushort flags;

		/// <summary>
		/// Zero-based index into the insertion glyph table.
		/// <para>
		/// The number of glyphs to be inserted is contained
		/// in the currentInsertCount field in the flags.
		/// A value of 0xFFFF indicates no insertion is to be done.
		/// </para>
		/// </summary>
		public ushort currentInsertIndex;

		/// <summary>
		/// Zero-based index into the insertion glyph table.
		/// <para>
		/// The number of glyphs to be inserted is contained
		/// in the markedInsertCount field in the flags.
		/// A value of 0xFFFF indicates no insertion is to be done.
		/// </para>
		/// </summary>
		public ushort markedInsertIndex;
	}
}
