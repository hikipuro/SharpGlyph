using System;
namespace SharpGlyph {
	public class AxisRecord {
		/// <summary>
		/// A tag identifying the axis of design variation.
		/// </summary>
		public string axisTag;

		/// <summary>
		/// The name ID for entries in the 'name' table
		/// that provide a display string for this axis.
		/// </summary>
		public ushort axisNameID;

		/// <summary>
		/// A value that applications can use to determine
		/// primary sorting of face names, or for ordering
		/// of descriptors when composing family or face names.
		/// </summary>
		public ushort axisOrdering;

		public static AxisRecord Read(BinaryReaderFont reader) {
			return new AxisRecord {
				axisTag = reader.ReadTag(),
				axisNameID = reader.ReadUInt16(),
				axisOrdering = reader.ReadUInt16()
			};
		}
	}
}
