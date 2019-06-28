using System;
using System.Text;

namespace SharpGlyph {
	public class SegmentMaps {
		/// <summary>
		/// The number of correspondence pairs for this axis.
		/// </summary>
		public ushort positionMapCount;

		/// <summary>
		/// The array of axis value map records for this axis.
		/// </summary>
		public AxisValueMap[] axisValueMaps;

		public static SegmentMaps[] ReadArray(BinaryReaderFont reader, int count) {
			SegmentMaps[] array = new SegmentMaps[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static SegmentMaps Read(BinaryReaderFont reader) {
			SegmentMaps value = new SegmentMaps {
				positionMapCount = reader.ReadUInt16()
			};
			value.axisValueMaps = AxisValueMap.ReadArray(reader, value.positionMapCount);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"positionMapCount\": {0},\n", positionMapCount);
			builder.AppendFormat("\t\"axisValueMaps.Length\": {0},\n", axisValueMaps.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
