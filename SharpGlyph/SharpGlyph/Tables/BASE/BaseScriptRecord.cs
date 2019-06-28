using System;
using System.Text;

namespace SharpGlyph {
	public class BaseScriptRecord {
		/// <summary>
		/// 4-byte script identification tag.
		/// </summary>
		public string baseScriptTag;

		/// <summary>
		/// Offset to BaseScript table, from beginning of BaseScriptList.
		/// </summary>
		public ushort baseScriptOffset;

		public BaseScript baseScript;

		public static BaseScriptRecord[] ReadArray(BinaryReaderFont reader, int count, long start) {
			BaseScriptRecord[] array = new BaseScriptRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader, start);
			}
			return array;
		}

		public static BaseScriptRecord Read(BinaryReaderFont reader, long start) {
			BaseScriptRecord value = new BaseScriptRecord {
				baseScriptTag = reader.ReadTag(),
				baseScriptOffset = reader.ReadUInt16()
			};
			long position = reader.Position;
			if (value.baseScriptOffset != 0) {
				reader.Position = start + value.baseScriptOffset;
				value.baseScript = BaseScript.Read(reader);
			}
			reader.Position = position;
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseScriptCount\": \"{0}\",\n", baseScriptTag);
			builder.AppendFormat("\t\"baseScriptOffset\": {0}\n", baseScriptOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
