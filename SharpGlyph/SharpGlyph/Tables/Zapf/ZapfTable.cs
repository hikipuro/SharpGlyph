using System;
namespace SharpGlyph {
	/// <summary>
	/// [Apple]
	/// Information about the individual glyphs in the font (Zapf).
	/// <para>A text string which the glyph represents</para>
	/// <para>A set of identifiers</para>
	/// <para>A set of font features</para>
	/// <para>A set of glyph collections</para>
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class ZapfTable : Table {
		/// <summary>
		/// Set to 2.
		/// </summary>
		public ushort version;

		/// <summary>
		/// 0.
		/// </summary>
		public ushort unused;

		/// <summary>
		/// Offset from start of table to start of extra info space
		/// (added to groupOffset and featOffset in GlyphInfo).
		/// </summary>
		public uint extraInfo;
	}
}
