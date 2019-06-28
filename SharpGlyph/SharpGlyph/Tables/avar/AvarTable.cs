using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Axis Variations Table (avar).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class AvarTable : Table {
		public const string Tag = "avar";

		/// <summary>
		/// Major version number of the axis variations table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the axis variations table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Permanently reserved; set to zero.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// The number of variation axes for this font.
		/// This must be the same number as axisCount in the 'fvar' table.
		/// </summary>
		public ushort axisCount;

		/// <summary>
		/// The segment maps array — one segment map for each axis,
		/// in the order of axes specified in the 'fvar' table.
		/// </summary>
		public SegmentMaps[] axisSegmentMaps;

		public static AvarTable Read(BinaryReaderFont reader) {
			AvarTable value = new AvarTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				reserved = reader.ReadUInt16(),
				axisCount = reader.ReadUInt16()
			};
			value.axisSegmentMaps = SegmentMaps.ReadArray(reader, value.axisCount);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"reserved\": {0},\n", reserved);
			builder.AppendFormat("\t\"axisCount\": {0},\n", axisCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
