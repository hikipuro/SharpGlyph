using System;
using System.Text;

namespace SharpGlyph {
	public class IndexSubHeader {
		/// <summary>
		/// Format of this IndexSubTable.
		/// </summary>
		public ushort indexFormat;

		/// <summary>
		/// Format of EBDT image data.
		/// </summary>
		public ushort imageFormat;

		/// <summary>
		/// Offset to image data in EBDT table.
		/// </summary>
		public uint imageDataOffset;

		public static IndexSubHeader Read(BinaryReaderFont reader) {
			return new IndexSubHeader {
				indexFormat = reader.ReadUInt16(),
				imageFormat = reader.ReadUInt16(),
				imageDataOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"indexFormat\": {0},\n", indexFormat);
			builder.AppendFormat("\t\"imageFormat\": {0},\n", imageFormat);
			builder.AppendFormat("\t\"imageDataOffset\": {0}\n", imageDataOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
