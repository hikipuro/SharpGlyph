using System;
using System.Text;

namespace SharpGlyph {
	public class BaseScript {
		/// <summary>
		/// Offset to BaseValues table, from beginning of BaseScript table (may be NULL).
		/// </summary>
		public ushort baseValuesOffset;

		/// <summary>
		/// Offset to MinMax table, from beginning of BaseScript table (may be NULL).
		/// </summary>
		public ushort defaultMinMaxOffset;

		/// <summary>
		/// Number of BaseLangSysRecords defined — may be zero (0).
		/// </summary>
		public ushort baseLangSysCount;

		/// <summary>
		/// Array of BaseLangSysRecords, in alphabetical order by BaseLangSysTag.
		/// </summary>
		public BaseLangSysRecord[] baseLangSysRecords;

		public BaseValues baseValues;
		public MinMax defaultMinMax;

		public static BaseScript Read(BinaryReaderFont reader) {
			long position = reader.Position;
			BaseScript value = new BaseScript {
				baseValuesOffset = reader.ReadUInt16(),
				defaultMinMaxOffset = reader.ReadUInt16(),
				baseLangSysCount = reader.ReadUInt16(),
			};
			if (value.baseLangSysCount != 0) {
				value.baseLangSysRecords = BaseLangSysRecord.ReadArray(
					reader, value.baseLangSysCount
				);
			}
			if (value.baseValuesOffset != 0) {
				reader.Position = position + value.baseValuesOffset;
				value.baseValues = BaseValues.Read(reader);
			}
			if (value.defaultMinMaxOffset != 0) {
				reader.Position = position + value.defaultMinMaxOffset;
				value.defaultMinMax = MinMax.Read(reader);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseValuesOffset\": {0},\n", baseValuesOffset);
			builder.AppendFormat("\t\"defaultMinMaxOffset\": {0},\n", defaultMinMaxOffset);
			builder.AppendFormat("\t\"baseLangSysCount\": {0}\n", baseLangSysCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
