using System;
using System.Text;

namespace SharpGlyph {
	public class Feature {
		/// <summary>
		/// = NULL (reserved for offset to FeatureParams).
		/// </summary>
		public ushort featureParams;
		
		/// <summary>
		/// Number of LookupList indices for this feature.
		/// </summary>
		public ushort lookupIndexCount;
		
		/// <summary>
		/// Array of indices into the LookupList — zero-based (first lookup is LookupListIndex = 0).
		/// </summary>
		public ushort[] lookupListIndices;

		public static Feature Read(BinaryReaderFont reader) {
			return new Feature {
				featureParams = reader.ReadUInt16(),
				lookupIndexCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"featureParams\": {0},\n", featureParams);
			builder.AppendFormat("\t\"lookupIndexCount\": {0},\n", lookupIndexCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
