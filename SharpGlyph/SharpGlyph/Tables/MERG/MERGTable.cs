using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Merge Table (MERG).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class MERGTable : Table {
		public const string Tag = "MERG";

		/// <summary>
		/// Version number of the merge table — set to 0.
		/// </summary>
		public ushort version;

		/// <summary>
		/// The number of merge classes.
		/// </summary>
		public ushort mergeClassCount;

		/// <summary>
		/// Offset to the array of merge-entry data.
		/// </summary>
		public ushort mergeDataOffset;

		/// <summary>
		/// The number of class definition tables.
		/// </summary>
		public ushort classDefCount;

		/// <summary>
		/// Offset to an array of offsets to class definition tables
		/// — in bytes from the start of the MERG table.
		/// </summary>
		public ushort offsetToClassDefOffsets;

		public static MERGTable Read(BinaryReaderFont reader) {
			return new MERGTable {
				version = reader.ReadUInt16(),
				mergeClassCount = reader.ReadUInt16(),
				mergeDataOffset = reader.ReadUInt16(),
				classDefCount = reader.ReadUInt16(),
				offsetToClassDefOffsets = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"mergeClassCount\": {0},\n", mergeClassCount);
			builder.AppendFormat("\t\"mergeDataOffset\": {0},\n", mergeDataOffset);
			builder.AppendFormat("\t\"classDefCount\": {0},\n", classDefCount);
			builder.AppendFormat("\t\"offsetToClassDefOffsets\": {0},\n", offsetToClassDefOffsets);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
