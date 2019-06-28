using System;
using System.Text;

namespace SharpGlyph {
	public class MetamorphosisChains {
		/// <summary>
		/// The default specification for subtables.
		/// </summary>
		public uint defaultFlags;

		/// <summary>
		/// Total byte count, including this header; must be a multiple of 4.
		/// </summary>
		public uint chainLength;

		/// <summary>
		/// Number of feature subtable entries.
		/// </summary>
		public uint nFeatureEntries;

		/// <summary>
		/// The number of subtables in the chain.
		/// </summary>
		public uint nSubtables;


		/// <summary>
		/// The type of feature.
		/// </summary>
		public ushort featureType;

		/// <summary>
		/// The feature's setting (aka selector).
		/// </summary>
		public ushort featureSetting;

		/// <summary>
		/// Flags for the settings that this feature and setting enables.
		/// </summary>
		public uint enableFlags;

		/// <summary>
		/// Complement of flags for the settings that this feature and setting disable.
		/// </summary>
		public uint disableFlags;

		public static MetamorphosisChains Read(BinaryReaderFont reader, TableRecord record) {
			MetamorphosisChains value = new MetamorphosisChains {
				defaultFlags = reader.ReadUInt32(),
				chainLength = reader.ReadUInt32(),
				nFeatureEntries = reader.ReadUInt32(),
				nSubtables = reader.ReadUInt32()
			};
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"defaultFlags\": {0},\n", defaultFlags);
			builder.AppendFormat("\t\"chainLength\": {0},\n", chainLength);
			builder.AppendFormat("\t\"nFeatureEntries\": {0},\n", nFeatureEntries);
			builder.AppendFormat("\t\"nSubtables\": {0},\n", nSubtables);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
