using System;
namespace SharpGlyph {
	public class AttachPoint {
		/// <summary>
		/// Number of attachment points on this glyph.
		/// </summary>
		public ushort pointCount;

		/// <summary>
		/// Array of contour point indices -in increasing numerical order.
		/// </summary>
		public ushort[] pointIndices;

		public static AttachPoint Read(BinaryReaderFont reader) {
			return new AttachPoint {
				pointCount = reader.ReadUInt16()
			};
		}
	}
}
