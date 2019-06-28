using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Font Variations Table (fvar).
	/// <para>OpenType Font Variation</para>
	/// </summary>
	//[OpenTypeFontVariation]
	public class FvarTable : Table {
		public const string Tag = "fvar";

		/// <summary>
		/// Major version number of the font variations table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the font variations table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Offset in bytes from the beginning of the table
		/// to the start of the VariationAxisRecord array.
		/// </summary>
		public ushort axesArrayOffset;

		/// <summary>
		/// This field is permanently reserved. Set to 2.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// The number of variation axes in the font
		/// (the number of records in the axes array).
		/// </summary>
		public ushort axisCount;

		/// <summary>
		/// The size in bytes of each VariationAxisRecord
		/// — set to 20 (0x0014) for this version.
		/// </summary>
		public ushort axisSize;

		/// <summary>
		/// The number of named instances defined in the font
		/// (the number of records in the instances array).
		/// </summary>
		public ushort instanceCount;

		/// <summary>
		/// The size in bytes of each InstanceRecord
		/// — set to either axisCount * sizeof(Fixed) + 4,
		/// or to axisCount * sizeof(Fixed) + 6.
		/// </summary>
		public ushort instanceSize;

		/// <summary>
		/// The variation axis array.
		/// </summary>
		public VariationAxisRecord[] axes;

		/// <summary>
		/// The named instance array.
		/// </summary>
		public InstanceRecord[] instances;

		public static FvarTable Read(BinaryReaderFont reader) {
			FvarTable value = new FvarTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				axesArrayOffset = reader.ReadUInt16(),
				reserved = reader.ReadUInt16(),
				axisCount = reader.ReadUInt16(),
				axisSize = reader.ReadUInt16(),
				instanceCount = reader.ReadUInt16(),
				instanceSize = reader.ReadUInt16(),
			};
			value.axes = VariationAxisRecord.ReadArray(reader, value.axisCount);
			value.instances = InstanceRecord.ReadArray(reader, value.instanceCount);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"axesArrayOffset\": {0},\n", axesArrayOffset);
			builder.AppendFormat("\t\"reserved\": {0},\n", reserved);
			builder.AppendFormat("\t\"axisCount\": {0},\n", axisCount);
			builder.AppendFormat("\t\"axisSize\": {0},\n", axisSize);
			builder.AppendFormat("\t\"instanceCount\": {0},\n", instanceCount);
			builder.AppendFormat("\t\"instanceSize\": {0},\n", instanceSize);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
