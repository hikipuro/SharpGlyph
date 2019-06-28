using System;
namespace SharpGlyph {
	public class GaspRange {
		/// <summary>
		/// Upper limit of range, in PPEM.
		/// </summary>
		public ushort rangeMaxPPEM;

		/// <summary>
		/// Flags describing desired rasterizer behavior.
		/// </summary>
		public ushort rangeGaspBehavior;

		public static GaspRange[] ReadArray(BinaryReaderFont reader, int count) {
			GaspRange[] array = new GaspRange[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static GaspRange Read(BinaryReaderFont reader) {
			return new GaspRange {
				rangeMaxPPEM = reader.ReadUInt16(),
				rangeGaspBehavior = reader.ReadUInt16()
			};
		}
	}
}
