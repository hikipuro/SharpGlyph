using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Vertical Device Metrics (VDMX).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class VDMXTable : Table {
		public const string Tag = "VDMX";

		/// <summary>
		/// Version number (0 or 1).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of VDMX groups present.
		/// </summary>
		public ushort numRecs;

		/// <summary>
		/// Number of aspect ratio groupings.
		/// </summary>
		public ushort numRatios;

		/// <summary>
		/// Ratio record array.
		/// </summary>
		public RatioRange[] ratRange;

		/// <summary>
		/// Offset from start of this table to the VDMXGroup table
		/// for a corresponding RatioRange record.
		/// </summary>
		public ushort[] offset;

		public static VDMXTable Read(BinaryReaderFont reader) {
			return new VDMXTable {
				version = reader.ReadUInt16(),
				numRecs = reader.ReadUInt16(),
				numRatios = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numRecs\": {0},\n", numRecs);
			builder.AppendFormat("\t\"numRatios\": {0},\n", numRatios);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
