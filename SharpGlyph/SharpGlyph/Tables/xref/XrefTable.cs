using System;
namespace SharpGlyph {
	/// <summary>
	/// cross-reference table (xref).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class XrefTable : Table {
		public uint version;
		public uint flags;
		public uint numEntries;
		public uint stringOffset;
	}
}
