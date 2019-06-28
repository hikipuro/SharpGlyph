using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFRange2 {
		/// <summary>
		/// First glyph in range.
		/// </summary>
		public ushort first;

		/// <summary>
		/// Glyphs left in range (excluding first).
		/// </summary>
		public ushort nLeft;

		public static CFFRange2[] ReadArray(BinaryReaderFont reader, int count) {
			List<CFFRange2> list = new List<CFFRange2>();
			for (int i = 1; i < count; i++) {
				CFFRange2 item = Read(reader);
				list.Add(item);
				i += item.nLeft + 1;
			}
			return list.ToArray();
		}

		public static CFFRange2 Read(BinaryReaderFont reader) {
			return new CFFRange2 {
				first = reader.ReadUInt16(),
				nLeft = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"first\": {0},\n", first);
			builder.AppendFormat("\t\"nLeft\": {0}\n", nLeft);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
