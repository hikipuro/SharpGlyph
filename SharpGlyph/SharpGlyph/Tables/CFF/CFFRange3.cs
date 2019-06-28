using System;
using System.Text;

namespace SharpGlyph {
	public class CFFRange3 {
		/// <summary>
		/// First glyph index in range.
		/// </summary>
		public ushort first;

		/// <summary>
		/// FD index for all glyphs in range.
		/// </summary>
		public byte fd;

		public static CFFRange3[] ReadArray(BinaryReaderFont reader, int count) {
			CFFRange3[] array = new CFFRange3[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static CFFRange3 Read(BinaryReaderFont reader) {
			return new CFFRange3 {
				first = reader.ReadUInt16(),
				fd = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"first\": {0},\n", first);
			builder.AppendFormat("\t\"fd\": {0}\n", fd);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
