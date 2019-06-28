using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureVariations {
		/// <summary>
		/// Major version of the FeatureVariations table — set to 1.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version of the FeatureVariations table — set to 0.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// Number of feature variation records.
		/// </summary>
		public uint featureVariationRecordCount;
		
		/// <summary>
		/// Array of feature variation records.
		/// </summary>
		public FeatureVariationRecord[] featureVariationRecords;

		public static FeatureVariations Read(BinaryReaderFont reader) {
			return new FeatureVariations {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				featureVariationRecordCount = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"featureVariationRecordCount\": {0},\n", featureVariationRecordCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
