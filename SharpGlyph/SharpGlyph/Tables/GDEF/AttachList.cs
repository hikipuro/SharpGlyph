using System;
namespace SharpGlyph {
	public class AttachList {
		/// <summary>
		/// Offset to Coverage table - from beginning of AttachList table.
		/// </summary>
		public ushort coverageOffset;

		/// <summary>
		/// Number of glyphs with attachment points.
		/// </summary>
		public ushort glyphCount;

		/// <summary>
		/// Array of offsets to AttachPoint tables-from beginning
		/// of AttachList table-in Coverage Index order.
		/// </summary>
		public ushort[] attachPointOffsets;

		public static AttachList Read(BinaryReaderFont reader) {
			return new AttachList {
				coverageOffset = reader.ReadUInt16(),
				glyphCount = reader.ReadUInt16()
			};
		}
	}
}
