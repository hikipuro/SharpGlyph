using System;
namespace SharpGlyph {
	public class SVGDocumentList {
		/// <summary>
		/// Number of SVG document records.
		/// Must be non-zero.
		/// </summary>
		public ushort numEntries;

		/// <summary>
		/// Array of SVG document records.
		/// </summary>
		public SVGDocumentRecord[] documentRecords;
	}
}
