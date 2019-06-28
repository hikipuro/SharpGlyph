using System;
namespace SharpGlyph {
	public class RatioRange {
		/// <summary>
		/// Character set.
		/// </summary>
		public byte bCharSet;

		/// <summary>
		/// Value to use for x-Ratio.
		/// </summary>
		public byte xRatio;

		/// <summary>
		/// Starting y-Ratio value.
		/// </summary>
		public byte yStartRatio;

		/// <summary>
		/// Ending y-Ratio value.
		/// </summary>
		public byte yEndRatio;

		public static RatioRange Read(BinaryReaderFont reader) {
			return new RatioRange {
				bCharSet = reader.ReadByte(),
				xRatio = reader.ReadByte(),
				yStartRatio = reader.ReadByte(),
				yEndRatio = reader.ReadByte()
			};
		}
	}
}
