using System;
namespace SharpGlyph {
	public class SVGDocumentRecord {
		/// <summary>
		/// The first glyph ID for the range covered by this record.
		/// </summary>
		public ushort startGlyphID;

		/// <summary>
		/// The last glyph ID for the range covered by this record.
		/// </summary>
		public ushort endGlyphID;

		/// <summary>
		/// Offset from the beginning of the SVGDocumentList to an SVG document.
		/// Must be non-zero.
		/// </summary>
		public uint svgDocOffset;

		/// <summary>
		/// Length of the SVG document data.
		/// Must be non-zero.
		/// </summary>
		public uint svgDocLength;
	}
}
