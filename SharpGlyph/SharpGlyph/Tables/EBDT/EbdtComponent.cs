using System;
namespace SharpGlyph {
	public class EbdtComponent {
		/// <summary>
		/// Component glyph ID.
		/// </summary>
		public ushort glyphID;

		/// <summary>
		/// Position of component left.
		/// </summary>
		public sbyte xOffset;

		/// <summary>
		/// Position of component top.
		/// </summary>
		public sbyte yOffset;

		public static EbdtComponent[] ReadArray(BinaryReaderFont reader, int count) {
			EbdtComponent[] array = new EbdtComponent[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static EbdtComponent Read(BinaryReaderFont reader) {
			return new EbdtComponent {
				glyphID = reader.ReadUInt16(),
				xOffset = reader.ReadSByte(),
				yOffset = reader.ReadSByte()
			};
		}
	}
}
