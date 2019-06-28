using System;
using System.Text;

namespace SharpGlyph {
	public class LangSys {
		/// <summary>
		/// = NULL (reserved for an offset to a reordering table).
		/// </summary>
		public ushort lookupOrder;
		
		/// <summary>
		/// Index of a feature required for this language system;
		/// if no required features = 0xFFFF.
		/// </summary>
		public ushort requiredFeatureIndex;
		
		/// <summary>
		/// Number of feature index values for this language system — excludes the required feature.
		/// </summary>
		public ushort featureIndexCount;
		
		/// <summary>
		/// Array of indices into the FeatureList, in arbitrary order.
		/// </summary>
		public ushort[] featureIndices;

		public static LangSys Read(BinaryReaderFont reader) {
			return new LangSys {
				lookupOrder = reader.ReadUInt16(),
				requiredFeatureIndex = reader.ReadUInt16(),
				featureIndexCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"lookupOrder\": {0},\n", lookupOrder);
			builder.AppendFormat("\t\"requiredFeatureIndex\": {0},\n", requiredFeatureIndex);
			builder.AppendFormat("\t\"featureIndexCount\": {0},\n", featureIndexCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
