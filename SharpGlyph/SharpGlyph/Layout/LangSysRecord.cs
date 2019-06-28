using System;
using System.Text;

namespace SharpGlyph {
	public class LangSysRecord {
		/// <summary>
		/// 4-byte LangSysTag identifier.
		/// </summary>
		public string langSysTag;
		
		/// <summary>
		/// Offset to LangSys table, from beginning of Script table.
		/// </summary>
		public ushort langSysOffset;

		public static LangSysRecord Read(BinaryReaderFont reader) {
			return new LangSysRecord {
				langSysTag = reader.ReadTag(),
				langSysOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"langSysTag\": \"{0}\",\n", langSysTag);
			builder.AppendFormat("\t\"langSysOffset\": {0},\n", langSysOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
