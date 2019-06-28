using System;
namespace SharpGlyph {
	public class JstfScriptRecord {
		/// <summary>
		/// 4-byte JstfScript identification.
		/// </summary>
		public string jstfScriptTag;

		/// <summary>
		/// Offset to JstfScript table, from beginning of JSTF Header.
		/// </summary>
		public ushort jstfScriptOffset;
	}
}
