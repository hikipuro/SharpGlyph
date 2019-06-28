using System;
namespace SharpGlyph {
	/// <summary>
	/// Tracking Table (trak).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class TrakTable : Table {
		/// <summary>
		/// Version number of the tracking table
		/// (0x00010000 for the current version).
		/// </summary>
		public uint version;

		/// <summary>
		/// Format of the tracking table (set to 0).
		/// </summary>
		public ushort format;

		/// <summary>
		/// Offset from start of tracking table
		/// to TrackData for horizontal text (or 0 if none).
		/// </summary>
		public ushort horizOffset;

		/// <summary>
		/// Offset from start of tracking table
		/// to TrackData for vertical text (or 0 if none).
		/// </summary>
		public ushort vertOffset;

		/// <summary>
		/// Reserved. Set to 0.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// TrackData for horizontal text (if present).
		/// </summary>
		public TrackData horizData;

		/// <summary>
		/// TrackData for vertical text (if present).
		/// </summary>
		public TrackData vertData;
	}
}
