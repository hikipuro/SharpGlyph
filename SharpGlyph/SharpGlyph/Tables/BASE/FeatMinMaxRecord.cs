using System;
using System.Text;

namespace SharpGlyph {
	public class FeatMinMaxRecord {
		/// <summary>
		/// 4-byte feature identification tag
		/// — must match feature tag in FeatureList.
		/// </summary>
		public string featureTableTag;

		/// <summary>
		/// Offset to BaseCoord table that defines
		/// the minimum extent value, from
		/// beginning of MinMax table (may be NULL).
		/// </summary>
		public ushort minCoord;

		/// <summary>
		/// Offset to BaseCoord table that defines
		/// the maximum extent value, from
		/// beginning of MinMax table (may be NULL).
		/// </summary>
		public ushort maxCoord;

		public static FeatMinMaxRecord[] ReadArray(BinaryReaderFont reader, int count) {
			FeatMinMaxRecord[] array = new FeatMinMaxRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static FeatMinMaxRecord Read(BinaryReaderFont reader) {
			return new FeatMinMaxRecord {
				featureTableTag = reader.ReadTag(),
				minCoord = reader.ReadUInt16(),
				maxCoord = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"featureTableTag\": \"{0}\",\n", featureTableTag);
			builder.AppendFormat("\t\"minCoord\": {0},\n", minCoord);
			builder.AppendFormat("\t\"maxCoord\": {0}\n", maxCoord);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
