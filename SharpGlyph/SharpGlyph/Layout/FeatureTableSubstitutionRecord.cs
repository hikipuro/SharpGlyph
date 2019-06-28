using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureTableSubstitutionRecord {
		/// <summary>
		/// The feature table index to match.
		/// </summary>
		public ushort featureIndex;
		
		/// <summary>
		/// Offset to an alternate feature table,
		/// from start of the FeatureTableSubstitution table.
		/// </summary>
		public uint alternateFeatureTable;

		public static FeatureTableSubstitutionRecord Read(BinaryReaderFont reader) {
			return new FeatureTableSubstitutionRecord {
				featureIndex = reader.ReadUInt16(),
				alternateFeatureTable = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"featureIndex\": {0},\n", featureIndex);
			builder.AppendFormat("\t\"alternateFeatureTable\": {0},\n", alternateFeatureTable);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
