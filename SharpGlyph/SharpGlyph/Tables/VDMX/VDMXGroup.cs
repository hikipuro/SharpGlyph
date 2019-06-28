using System;
namespace SharpGlyph {
	public class VDMXGroup {
		/// <summary>
		/// Number of height records in this group.
		/// </summary>
		public ushort recs;

		/// <summary>
		/// Starting yPelHeight.
		/// </summary>
		public byte startsz;

		/// <summary>
		/// Ending yPelHeight.
		/// </summary>
		public byte endsz;

		/// <summary>
		/// The VDMX records.
		/// </summary>
		public VTable[] entry;

		public static VDMXGroup Read(BinaryReaderFont reader) {
			return new VDMXGroup {
				recs = reader.ReadUInt16(),
				startsz = reader.ReadByte(),
				endsz = reader.ReadByte()
			};
		}
	}
}
