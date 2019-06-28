using System;
namespace SharpGlyph {
	/// <summary>
	/// Anchor Point Table (ankr).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class AnkrTable : Table {
		public ushort version;
		public ushort flags;
		public uint lookupTableOffset;
		public uint glyphDataTableOffset;
	}
}
