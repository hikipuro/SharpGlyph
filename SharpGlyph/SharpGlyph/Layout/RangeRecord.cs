using System;
using System.Text;

namespace SharpGlyph {
	public class RangeRecord {
		/// <summary>
		/// First glyph ID in the range.
		/// </summary>
		public ushort startGlyphID;
		
		/// <summary>
		/// Last glyph ID in the range.
		/// </summary>
		public ushort endGlyphID;
		
		/// <summary>
		/// Coverage Index of first glyph ID in range.
		/// </summary>
		public ushort startCoverageIndex;

		public static RangeRecord[] ReadArray(BinaryReaderFont reader, int count) {
			RangeRecord[] array = new RangeRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static RangeRecord Read(BinaryReaderFont reader) {
			return new RangeRecord {
				startGlyphID = reader.ReadUInt16(),
				endGlyphID = reader.ReadUInt16(),
				startCoverageIndex = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startGlyphID\": {0},\n", startGlyphID);
			builder.AppendFormat("\t\"endGlyphID\": {0},\n", endGlyphID);
			builder.AppendFormat("\t\"startCoverageIndex\": {0},\n", startCoverageIndex);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
