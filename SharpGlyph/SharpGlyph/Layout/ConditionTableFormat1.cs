using System;
using System.Text;

namespace SharpGlyph {
	public class ConditionTableFormat1 {
		/// <summary>
		/// Format, = 1.
		/// </summary>
		public ushort Format;
		
		/// <summary>
		/// Index (zero-based) for the variation axis within the 'fvar' table.
		/// </summary>
		public ushort AxisIndex;
		
		/// <summary>
		/// Minimum value of the font variation instances that satisfy this condition.
		/// </summary>
		public ushort FilterRangeMinValue;
		
		/// <summary>
		/// Maximum value of the font variation instances that satisfy this condition.
		/// </summary>
		public ushort FilterRangeMaxValue;

		public static ConditionTableFormat1 Read(BinaryReaderFont reader) {
			return new ConditionTableFormat1 {
				Format = reader.ReadUInt16(),
				AxisIndex = reader.ReadUInt16(),
				FilterRangeMinValue = reader.ReadUInt16(),
				FilterRangeMaxValue = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"Format\": {0},\n", Format);
			builder.AppendFormat("\t\"AxisIndex\": {0},\n", AxisIndex);
			builder.AppendFormat("\t\"FilterRangeMinValue\": {0},\n", FilterRangeMinValue);
			builder.AppendFormat("\t\"FilterRangeMaxValue\": {0},\n", FilterRangeMaxValue);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
