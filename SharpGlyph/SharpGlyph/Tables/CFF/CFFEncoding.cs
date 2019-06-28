using System;
namespace SharpGlyph {
	public class CFFEncoding {
		/// <summary>
		/// =0.
		/// </summary>
		public byte format;

		/// <summary>
		/// Number of encoded glyphs.
		/// </summary>
		public byte nCodes;

		/// <summary>
		/// Code array.
		/// </summary>
		public byte[] code;
	}
}
