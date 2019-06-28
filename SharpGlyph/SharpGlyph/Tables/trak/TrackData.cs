using System;
namespace SharpGlyph {
	public class TrackData {
		/// <summary>
		/// Number of separate tracks included in this table.
		/// </summary>
		public ushort nTracks;

		/// <summary>
		/// Number of point sizes included in this table.
		/// </summary>
		public ushort nSizes;

		/// <summary>
		/// Offset from start of the tracking table
		/// to the start of the size subtable.
		/// </summary>
		public uint sizeTableOffset;

		/// <summary>
		/// Array[nTracks] of TrackTableEntry records.
		/// </summary>
		public TrackTableEntry[] trackTable;

		/// <summary>
		/// Array[nSizes] of size values.
		/// </summary>
		public uint[] sizeTable;
	}
}
