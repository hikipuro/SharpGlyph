using System;
namespace SharpGlyph {
	public class MarkArray {
		/// <summary>
		/// Number of MarkRecords.
		/// </summary>
		public ushort markCount;

		/// <summary>
		/// Array of MarkRecords, ordered by corresponding
		/// glyphs in the associated mark Coverage table.
		/// </summary>
		public MarkRecord[] markRecords;

		public static MarkArray Read(BinaryReaderFont reader) {
			return new MarkArray {
				markCount = reader.ReadUInt16()
			};
		}
	}
}
