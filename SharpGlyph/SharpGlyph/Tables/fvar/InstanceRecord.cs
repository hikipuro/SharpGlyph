using System;
namespace SharpGlyph {
	public class InstanceRecord {
		/// <summary>
		/// The name ID for entries in the 'name' table
		/// that provide subfamily names for this instance.
		/// </summary>
		public ushort subfamilyNameID;

		/// <summary>
		/// Reserved for future use — set to 0.
		/// </summary>
		public ushort flags;

		/// <summary>
		/// The coordinates array for this instance.
		/// </summary>
		public Tuple coordinates;

		/// <summary>
		/// Optional.
		/// The name ID for entries in the 'name' table
		/// that provide PostScript names for this instance.
		/// </summary>
		public ushort postScriptNameID;

		public static InstanceRecord[] ReadArray(BinaryReaderFont reader, int count) {
			InstanceRecord[] array = new InstanceRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static InstanceRecord Read(BinaryReaderFont reader) {
			return new InstanceRecord {
				subfamilyNameID = reader.ReadUInt16(),
				flags = reader.ReadUInt16(),
				coordinates = Tuple.Read(reader),
				postScriptNameID = reader.ReadUInt16()
			};
		}
	}
}
