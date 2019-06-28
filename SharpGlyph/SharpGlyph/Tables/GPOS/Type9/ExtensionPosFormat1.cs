using System;
namespace SharpGlyph {
	public class ExtensionPosFormat1 {
		/// <summary>
		/// Format identifier: format = 1.
		/// </summary>
		public ushort posFormat;

		/// <summary>
		/// Lookup type of subtable referenced
		/// by extensionOffset (i.e. the extension subtable).
		/// </summary>
		public ushort extensionLookupType;

		/// <summary>
		/// Offset to the extension subtable,
		/// of lookup type extensionLookupType,
		/// relative to the start of the ExtensionPosFormat1 subtable.
		/// </summary>
		public uint extensionOffset;
	}
}
