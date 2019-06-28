using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Digital Signature Table (DSIG).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class DSIGTable : Table {
		public const string Tag = "DSIG";

		/// <summary>
		/// Version number of the DSIG table (0x00000001).
		/// </summary>
		public uint version;

		/// <summary>
		/// Number of signatures in the table.
		/// </summary>
		public ushort numSignatures;

		/// <summary>
		/// permission flags Bit 0: cannot be resigned Bits 1-7: Reserved (Set to 0).
		/// </summary>
		public ushort flags;

		/// <summary>
		/// Array of signature records.
		/// </summary>
		public SignatureRecord[] signatureRecords;

		public static DSIGTable Read(BinaryReaderFont reader) {
			DSIGTable value = new DSIGTable {
				version = reader.ReadUInt32(),
				numSignatures = reader.ReadUInt16(),
				flags = reader.ReadUInt16()
			};
			value.signatureRecords = SignatureRecord.ReadArray(
				reader, value.numSignatures
			);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numSignatures\": {0},\n", numSignatures);
			builder.AppendFormat("\t\"flags\": {0},\n", flags);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
