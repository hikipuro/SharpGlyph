using System;
namespace SharpGlyph {
	public class VariationAxisRecord {
		/// <summary>
		/// Tag identifying the design variation for the axis.
		/// </summary>
		public string axisTag;

		/// <summary>
		/// The minimum coordinate value for the axis.
		/// </summary>
		public uint minValue;

		/// <summary>
		/// The default coordinate value for the axis.
		/// </summary>
		public uint defaultValue;

		/// <summary>
		/// The maximum coordinate value for the axis.
		/// </summary>
		public uint maxValue;

		/// <summary>
		/// Axis qualifiers.
		/// </summary>
		public ushort flags;

		/// <summary>
		/// The name ID for entries in the 'name' table
		/// that provide a display name for this axis.
		/// </summary>
		public ushort axisNameID;

		public static VariationAxisRecord[] ReadArray(BinaryReaderFont reader, int count) {
			VariationAxisRecord[] array = new VariationAxisRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static VariationAxisRecord Read(BinaryReaderFont reader) {
			return new VariationAxisRecord {
				axisTag = reader.ReadTag(),
				minValue = reader.ReadUInt32(),
				defaultValue = reader.ReadUInt32(),
				maxValue = reader.ReadUInt32(),
				flags = reader.ReadUInt16(),
				axisNameID = reader.ReadUInt16()
			};
		}
	}
}
