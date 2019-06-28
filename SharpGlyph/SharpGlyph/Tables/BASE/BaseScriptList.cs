using System;
using System.Text;

namespace SharpGlyph {
	public class BaseScriptList {
		/// <summary>
		/// Number of BaseScriptRecords defined.
		/// </summary>
		public ushort baseScriptCount;

		/// <summary>
		/// Array of BaseScriptRecords, in alphabetical order by baseScriptTag.
		/// </summary>
		public BaseScriptRecord[] baseScriptRecords;

		public static BaseScriptList Read(BinaryReaderFont reader) {
			long position = reader.Position;
			BaseScriptList value = new BaseScriptList {
				baseScriptCount = reader.ReadUInt16()
			};
			if (value.baseScriptCount != 0) {
				value.baseScriptRecords = BaseScriptRecord.ReadArray(
					reader, value.baseScriptCount, position
				);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseScriptCount\": {0},\n", baseScriptCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
