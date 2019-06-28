using System;
using System.Text;

namespace SharpGlyph {
	public class CFFCharset2 : CFFCharset {
		public CFFRange2[] Range2;

		public static new CFFCharset2 Read(BinaryReaderFont reader, int count) {
			CFFCharset2 value = new CFFCharset2 {
				format = reader.ReadByte()
			};
			value.Range2 = CFFRange2.ReadArray(reader, count);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"Range2.Length\": {0},\n", Range2.Length);
			builder.AppendLine("\t\"Range2\": [");
			for (int i = 0; i < Range2.Length; i++) {
				string range2 = Range2[i].ToString();
				builder.AppendFormat("\t\t{0},\n", range2.Replace("\n", "\n\t\t"));
			}
			if (Range2.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
