using System;
using System.Text;

namespace SharpGlyph {
	public class LangTagRecord {
		/// <summary>
		/// Language-tag string length (in bytes).
		/// </summary>
		public ushort length;

		/// <summary>
		/// Language-tag string offset from start of storage area (in bytes).
		/// </summary>
		public ushort offset;

		public string text;
		
		public static LangTagRecord[] ReadArray(BinaryReaderFont reader, int count) {
			LangTagRecord[] array = new LangTagRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}
		
		public static LangTagRecord Read(BinaryReaderFont reader) {
			LangTagRecord value = new LangTagRecord {
				length = reader.ReadUInt16(),
				offset = reader.ReadUInt16()
			};
			return value;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("\"LangTagRecord\": {");
			builder.AppendFormat("\t\"length\": {0},\n", length);
			builder.AppendFormat("\t\"offset\": {0},\n", offset);
			builder.AppendFormat("\t\"text\": {0},\n", text);
			builder.AppendLine("}");
			return builder.ToString();
		}
	}
}
