using System;
namespace SharpGlyph {
	public class ValueRecord {
		/// <summary>
		/// Horizontal adjustment for placement, in design units.
		/// </summary>
		public short xPlacement;

		/// <summary>
		/// Vertical adjustment for placement, in design units.
		/// </summary>
		public short yPlacement;

		/// <summary>
		/// Horizontal adjustment for advance, in design units
		/// — only used for horizontal layout.
		/// </summary>
		public short xAdvance;

		/// <summary>
		/// Vertical adjustment for advance, in design units
		/// — only used for vertical layout.
		/// </summary>
		public short yAdvance;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font)
		/// for horizontal placement, from beginning of
		/// the immediate parent table (SinglePos or
		/// PairPosFormat2 lookup subtable,
		/// PairSet table within a PairPosFormat1
		/// lookup subtable) — may be NULL.
		/// </summary>
		public ushort xPlaDeviceOffset;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font)
		/// for vertical placement, from beginning of
		/// the immediate parent table (SinglePos or
		/// PairPosFormat2 lookup subtable,
		/// PairSet table within a PairPosFormat1
		/// lookup subtable) — may be NULL.
		/// </summary>
		public ushort yPlaDeviceOffset;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font)
		/// for horizontal advance, from beginning of
		/// the immediate parent table (SinglePos or
		/// PairPosFormat2 lookup subtable,
		/// PairSet table within a PairPosFormat1
		/// lookup subtable) — may be NULL.
		/// </summary>
		public ushort xAdvDeviceOffset;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font)
		/// for vertical advance, from beginning of
		/// the immediate parent table (SinglePos or
		/// PairPosFormat2 lookup subtable,
		/// PairSet table within a PairPosFormat1
		/// lookup subtable) — may be NULL.
		/// </summary>
		public ushort yAdvDeviceOffset;

		public static ValueRecord Read(BinaryReaderFont reader) {
			return new ValueRecord {
				xPlacement = reader.ReadInt16(),
				yPlacement = reader.ReadInt16(),
				xAdvance = reader.ReadInt16(),
				yAdvance = reader.ReadInt16(),
				xPlaDeviceOffset = reader.ReadUInt16(),
				yPlaDeviceOffset = reader.ReadUInt16(),
				xAdvDeviceOffset = reader.ReadUInt16(),
				yAdvDeviceOffset = reader.ReadUInt16()
			};
		}
	}
}
