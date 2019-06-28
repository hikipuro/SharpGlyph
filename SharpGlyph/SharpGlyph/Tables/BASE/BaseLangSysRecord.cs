using System;
using System.Text;

namespace SharpGlyph {
	public class BaseLangSysRecord {
		/// <summary>
		/// 4-byte language system identification tag.
		/// </summary>
		public string baseLangSysTag;

		/// <summary>
		/// Offset to MinMax table, from beginning of BaseScript table.
		/// </summary>
		public ushort minMaxOffset;

		public static BaseLangSysRecord[] ReadArray(BinaryReaderFont reader, int count) {
			BaseLangSysRecord[] array = new BaseLangSysRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static BaseLangSysRecord Read(BinaryReaderFont reader) {
			return new BaseLangSysRecord {
				baseLangSysTag = reader.ReadTag(),
				minMaxOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseLangSysTag\": \"{0}\",\n", baseLangSysTag);
			builder.AppendFormat("\t\"minMaxOffset\": {0}\n", minMaxOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
