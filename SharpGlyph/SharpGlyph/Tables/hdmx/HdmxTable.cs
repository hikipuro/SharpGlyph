using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Horizontal Device Metrics (hdmx).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class HdmxTable : Table {
		public const string Tag = "hdmx";

		/// <summary>
		/// Table version number (0).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of device records.
		/// </summary>
		public short numRecords;

		/// <summary>
		/// Size of a device record, 32-bit aligned.
		/// </summary>
		public int sizeDeviceRecord;

		/// <summary>
		/// Array of device records.
		/// </summary>
		public DeviceRecord[] records;

		public static HdmxTable Read(BinaryReaderFont reader) {
			return new HdmxTable {
				version = reader.ReadUInt16(),
				numRecords = reader.ReadInt16(),
				sizeDeviceRecord = reader.ReadInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numRecords\": {0},\n", numRecords);
			builder.AppendFormat("\t\"sizeDeviceRecord\": {0},\n", sizeDeviceRecord);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
