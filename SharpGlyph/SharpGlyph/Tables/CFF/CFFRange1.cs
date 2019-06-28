using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFRange1 {
		/// <summary>
		/// First code in range.
		/// </summary>
		public ushort first;

		/// <summary>
		/// Codes left in range (excluding first).
		/// </summary>
		public byte nLeft;

		public static CFFRange1[] ReadArray(BinaryReaderFont reader, int count) {
			List<CFFRange1> list = new List<CFFRange1>();
			for (int i = 1; i < count; i++) {
				CFFRange1 item = Read(reader);
				list.Add(item);
				i += item.nLeft + 1;
			}
			return list.ToArray();
		}

		public static CFFRange1 Read(BinaryReaderFont reader) {
			return new CFFRange1 {
				first = reader.ReadUInt16(),
				nLeft = reader.ReadByte()
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
