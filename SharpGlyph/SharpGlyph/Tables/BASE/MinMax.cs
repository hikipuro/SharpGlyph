using System;
using System.Text;

namespace SharpGlyph {
	public class MinMax {
		/// <summary>
		/// Offset to BaseCoord table that defines
		/// the minimum extent value, from
		/// the beginning of MinMax table (may be NULL).
		/// </summary>
		public ushort minCoord;

		/// <summary>
		/// Offset to BaseCoord table that defines
		/// maximum extent value, from
		/// the beginning of MinMax table (may be NULL).
		/// </summary>
		public ushort maxCoord;

		/// <summary>
		/// Number of FeatMinMaxRecords — may be zero (0).
		/// </summary>
		public ushort featMinMaxCount;

		/// <summary>
		/// Array of FeatMinMaxRecords,
		/// in alphabetical order by featureTableTag.
		/// </summary>
		public FeatMinMaxRecord[] featMinMaxRecords;

		public static MinMax Read(BinaryReaderFont reader) {
			MinMax value = new MinMax {
				minCoord = reader.ReadUInt16(),
				maxCoord = reader.ReadUInt16(),
				featMinMaxCount = reader.ReadUInt16()
			};
			if (value.featMinMaxCount != 0) {
				value.featMinMaxRecords = FeatMinMaxRecord.ReadArray(
					reader, value.featMinMaxCount
				);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"minCoord\": {0},\n", minCoord);
			builder.AppendFormat("\t\"maxCoord\": {0}\n", maxCoord);
			builder.AppendFormat("\t\"featMinMaxCount\": {0},\n", featMinMaxCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
