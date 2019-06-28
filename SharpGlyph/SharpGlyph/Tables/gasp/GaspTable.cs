using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Grid-fitting and Scan-conversion Procedure Table (gasp).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class GaspTable : Table {
		public const string Tag = "gasp";

		/// <summary>
		/// Version number (set to 1).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of records to follow.
		/// </summary>
		public ushort numRanges;

		/// <summary>
		/// Sorted by ppem.
		/// </summary>
		public GaspRange[] gaspRanges;

		public static GaspTable Read(BinaryReaderFont reader) {
			GaspTable value = new GaspTable {
				version = reader.ReadUInt16(),
				numRanges = reader.ReadUInt16()
			};
			value.gaspRanges = GaspRange.ReadArray(reader, value.numRanges);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numRanges\": {0},\n", numRanges);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
