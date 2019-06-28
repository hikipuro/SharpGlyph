using System;
using System.Text;

namespace SharpGlyph {
	public class ScriptRecord {
		/// <summary>
		/// 4-byte script tag identifier.
		/// </summary>
		public string scriptTag;
		
		/// <summary>
		/// Offset to Script table, from beginning of ScriptList.
		/// </summary>
		public ushort scriptOffset;

		public static ScriptRecord Read(BinaryReaderFont reader) {
			return new ScriptRecord {
				scriptTag = reader.ReadTag(),
				scriptOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"scriptTag\": \"{0}\",\n", scriptTag);
			builder.AppendFormat("\t\"scriptOffset\": {0},\n", scriptOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
