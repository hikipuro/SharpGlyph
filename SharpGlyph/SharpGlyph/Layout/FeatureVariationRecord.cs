using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureVariationRecord {
		/// <summary>
		/// Offset to a condition set table,
		/// from beginning of FeatureVariations table.
		/// </summary>
		public uint conditionSetOffset;
		
		/// <summary>
		/// Offset to a feature table substitution table,
		/// from beginning of the FeatureVariations table.
		/// </summary>
		public uint featureTableSubstitutionOffset;

		public static FeatureVariationRecord Read(BinaryReaderFont reader) {
			return new FeatureVariationRecord {
				conditionSetOffset = reader.ReadUInt32(),
				featureTableSubstitutionOffset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"conditionSetOffset\": {0},\n", conditionSetOffset);
			builder.AppendFormat("\t\"featureTableSubstitutionOffset\": {0},\n", featureTableSubstitutionOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
