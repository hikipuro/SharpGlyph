using System;
namespace SharpGlyph {
	public class BitmapScale {
		/// <summary>
		/// line metrics.
		/// </summary>
		public SbitLineMetrics hori;

		/// <summary>
		/// line metrics.
		/// </summary>
		public SbitLineMetrics vert;

		/// <summary>
		/// target horizontal pixels per Em.
		/// </summary>
		public byte ppemX;

		/// <summary>
		/// target vertical pixels per Em.
		/// </summary>
		public byte ppemY;

		/// <summary>
		/// use bitmaps of this size.
		/// </summary>
		public byte substitutePpemX;

		/// <summary>
		/// use bitmaps of this size.
		/// </summary>
		public byte substitutePpemY;

		public static BitmapScale Read(BinaryReaderFont reader) {
			return new BitmapScale {
				hori = SbitLineMetrics.Read(reader),
				vert = SbitLineMetrics.Read(reader),
				ppemX = reader.ReadByte(),
				ppemY = reader.ReadByte(),
				substitutePpemX = reader.ReadByte(),
				substitutePpemY = reader.ReadByte()
			};
		}
	}
}
