using System;
namespace SharpGlyph {
	public class AnchorFormat1 {
		/// <summary>
		/// Format identifier, = 1.
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

		public static AnchorFormat1 Read(BinaryReaderFont reader) {
			return new AnchorFormat1 {
				anchorFormat = reader.ReadUInt16(),
				xCoordinate = reader.ReadInt16(),
				yCoordinate = reader.ReadInt16()
			};
		}
	}
}
