using System;
namespace SharpGlyph {
	public class TrackTableEntry {
		/// <summary>
		/// Track value for this record.
		/// </summary>
		public uint track;

		/// <summary>
		/// The 'name' table index for this track
		/// (a short word or phrase like "loose" or "very tight").
		/// NameIndex has a value greater than 255 and less than 32768.
		/// </summary>
		public ushort nameIndex;

		/// <summary>
		/// Offset from start of tracking table to per-size
		/// tracking values for this track.
		/// </summary>
		public ushort offset;
	}
}
