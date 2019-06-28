using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureList {
		/// <summary>
		/// Number of FeatureRecords in this table.
		/// </summary>
		public ushort featureCount;
		
		/// <summary>
		/// Array of FeatureRecords — zero-based (first feature has FeatureIndex = 0),
		/// listed alphabetically by feature tag.
		/// </summary>
		public FeatureRecord[] featureRecords;

		public static FeatureList Read(BinaryReaderFont reader) {
			return new FeatureList {
				featureCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"featureCount\": {0},\n", featureCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
