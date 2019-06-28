using System;
namespace SharpGlyph {
	public class AnchorFormat2 {
		/// <summary>
		/// Format identifier, = 2.
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
		/// Index to glyph contour point.
		/// </summary>
		public ushort anchorPoint;

		public static AnchorFormat2 Read(BinaryReaderFont reader) {
			return new AnchorFormat2 {
				anchorFormat = reader.ReadUInt16(),
				xCoordinate = reader.ReadInt16(),
				yCoordinate = reader.ReadInt16(),
				anchorPoint = reader.ReadUInt16()
			};
		}
	}
}
