using System;
using System.Text;

namespace SharpGlyph {
	public class CFFCharset1 : CFFCharset {
		public CFFRange1[] Range1;

		public static new CFFCharset1 Read(BinaryReaderFont reader, int count) {
			CFFCharset1 value = new CFFCharset1 {
				format = reader.ReadByte()
			};
			value.Range1 = CFFRange1.ReadArray(reader, count);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
