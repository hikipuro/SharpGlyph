using System;
using System.Text;

namespace SharpGlyph {
	public class Script {
		/// <summary>
		/// Offset to default LangSys table, from beginning of Script table — may be NULL.
		/// </summary>
		public ushort defaultLangSys;
		
		/// <summary>
		/// Number of LangSysRecords for this script — excluding the default LangSys.
		/// </summary>
		public ushort langSysCount;
		
		/// <summary>
		/// Array of LangSysRecords, listed alphabetically by LangSys tag.
		/// </summary>
		public LangSysRecord[] langSysRecords;

		public static Script Read(BinaryReaderFont reader) {
			return new Script {
				defaultLangSys = reader.ReadUInt16(),
				langSysCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"defaultLangSys\": {0},\n", defaultLangSys);
			builder.AppendFormat("\t\"langSysCount\": {0},\n", langSysCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
