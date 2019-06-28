using System;
namespace SharpGlyph {
	public class MarkRecord {
		/// <summary>
		/// Class defined for the associated mark.
		/// </summary>
		public ushort markClass;

		/// <summary>
		/// Offset to Anchor table, from beginning of MarkArray table.
		/// </summary>
		public ushort markAnchorOffset;

		public static MarkRecord Read(BinaryReaderFont reader) {
			return new MarkRecord {
				markClass = reader.ReadUInt16(),
				markAnchorOffset = reader.ReadUInt16()
			};
		}
	}
}
