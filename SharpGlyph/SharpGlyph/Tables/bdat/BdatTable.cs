using System;
namespace SharpGlyph {
	/// <summary>
	/// Bitmap Data Table (bdat).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class BdatTable : Table {
		public uint version;
		public byte[] glyphbitmap;
	}
}
