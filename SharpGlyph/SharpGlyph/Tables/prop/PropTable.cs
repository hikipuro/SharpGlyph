using System;
namespace SharpGlyph {
	/// <summary>
	/// glyph properties table (prop).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class PropTable : Table {
		public uint version;
		public ushort format;
		public ushort defaultProperties;
		public uint lookupData;
	}
}
