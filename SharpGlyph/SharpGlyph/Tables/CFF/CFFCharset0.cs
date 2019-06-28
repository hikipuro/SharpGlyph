using System;
using System.Text;

namespace SharpGlyph {
	public class CFFCharset0 : CFFCharset {
		public ushort[] glyph;

		public static CFFCharset0 Read(BinaryReaderFont reader) {
			return new CFFCharset0 {
				format = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
