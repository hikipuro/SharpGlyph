using System;
namespace SharpGlyph {
	/// <summary>
	/// Bitmap location table (bloc).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class BlocTable : Table {
		public uint version;
		public uint numSizes;
		public BitmapSizeTable[] bitmapSizeTable;
	}
}
