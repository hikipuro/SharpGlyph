using System;
namespace SharpGlyph {
	public class VTable {
		/// <summary>
		/// yPelHeight to which values apply.
		/// </summary>
		public ushort yPelHeight;

		/// <summary>
		/// Maximum value (in pels) for this yPelHeight.
		/// </summary>
		public short yMax;

		/// <summary>
		/// Minimum value (in pels) for this yPelHeight.
		/// </summary>
		public short yMin;

		public static VTable Read(BinaryReaderFont reader) {
			return new VTable {
				yPelHeight = reader.ReadUInt16(),
				yMax = reader.ReadInt16(),
				yMin = reader.ReadInt16()
			};
		}
	}
}
