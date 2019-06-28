using System;
using System.Text;

namespace SharpGlyph {
	public class AxisTable {
		/// <summary>
		/// Offset to BaseTagList table,
		/// from beginning of Axis table (may be NULL).
		/// </summary>
		public ushort baseTagListOffset;
		
		/// <summary>
		/// Offset to BaseScriptList table,
		/// from beginning of Axis table.
		/// </summary>
		public ushort baseScriptListOffset;

		public BaseTagList baseTagList;
		public BaseScriptList baseScriptList;

		public static AxisTable Read(BinaryReaderFont reader) {
			long position = reader.Position;
			AxisTable value = new AxisTable {
				baseTagListOffset = reader.ReadUInt16(),
				baseScriptListOffset = reader.ReadUInt16()
			};
			if (value.baseTagListOffset != 0) {
				reader.Position = position + value.baseTagListOffset;
				value.baseTagList = BaseTagList.Read(reader);
			}
			if (value.baseScriptListOffset != 0) {
				reader.Position = position + value.baseScriptListOffset;
				value.baseScriptList = BaseScriptList.Read(reader);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseTagListOffset\": {0},\n", baseTagListOffset);
			builder.AppendFormat("\t\"baseScriptListOffset\": {0}\n", baseScriptListOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
