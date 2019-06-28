using System;
namespace SharpGlyph {
	public class AnchorFormat3 {
		/// <summary>
		/// Format identifier, = 3.
		/// </summary>
		public ushort anchorFormat;

		/// <summary>
		/// Horizontal value, in design units.
		/// </summary>
		public short xCoordinate;

		/// <summary>
		/// Vertical value, in design units.
		/// </summary>
		public short yCoordinate;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font) for X coordinate,
		/// from beginning of Anchor table (may be NULL).
		/// </summary>
		public ushort xDeviceOffset;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / VariationIndex table (variable font) for Y coordinate,
		/// from beginning of Anchor table (may be NULL).
		/// </summary>
		public ushort yDeviceOffset;

		public static AnchorFormat3 Read(BinaryReaderFont reader) {
			return new AnchorFormat3 {
				anchorFormat = reader.ReadUInt16(),
				xCoordinate = reader.ReadInt16(),
				yCoordinate = reader.ReadInt16(),
				xDeviceOffset = reader.ReadUInt16(),
				yDeviceOffset = reader.ReadUInt16()
			};
		}
	}
}
