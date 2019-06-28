using System;
using System.Text;

namespace SharpGlyph {
	public class CFFFDSelect0 : CFFFDSelect {
		/// <summary>
		/// FD selector array.
		/// </summary>
		public byte[] fds;

		public static new CFFFDSelect0 Read(BinaryReaderFont reader) {
			CFFFDSelect0 value = new CFFFDSelect0 {
				format = reader.ReadByte()
			};
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
