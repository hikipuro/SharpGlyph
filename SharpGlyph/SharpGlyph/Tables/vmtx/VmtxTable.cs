using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Vertical Metrics Table (vmtx).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class VmtxTable : Table {
		public const string Tag = "vmtx";
		
		/// <summary>
		/// The advance height of the glyph.
		/// Unsigned integer in FUnits.
		/// </summary>
		public ushort advanceHeight;
		
		/// <summary>
		/// The top sidebearing of the glyph.
		/// Signed integer in FUnits.
		/// </summary>
		public short[] topSideBearing;

		public static VmtxTable Read(BinaryReaderFont reader) {
			return new VmtxTable {
				advanceHeight = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"advanceHeight\": {0},\n", advanceHeight);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
