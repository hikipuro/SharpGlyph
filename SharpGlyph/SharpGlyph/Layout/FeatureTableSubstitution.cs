using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureTableSubstitution {
		/// <summary>
		/// Major version of the feature table substitution table — set to 1.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version of the feature table substitution table — set to 0.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// Number of feature table substitution records.
		/// </summary>
		public ushort substitutionCount;
		
		/// <summary>
		/// Array of feature table substitution records.
		/// </summary>
		public FeatureTableSubstitutionRecord[] substitutions;

		public static FeatureTableSubstitution Read(BinaryReaderFont reader) {
			return new FeatureTableSubstitution {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				substitutionCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"substitutionCount\": {0},\n", substitutionCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
