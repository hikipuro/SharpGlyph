using System;
using System.Text;

namespace SharpGlyph {
	public class UVSMapping {
		/// <summary>
		/// Base Unicode value of the UVS.
		/// </summary>
		public int unicodeValue;
		
		/// <summary>
		/// Glyph ID of the UVS.
		/// </summary>
		public ushort glyphID;

		public static UVSMapping[] ReadArray(BinaryReaderFont reader, uint count) {
			UVSMapping[] array = new UVSMapping[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static UVSMapping Read(BinaryReaderFont reader) {
			return new UVSMapping {
				unicodeValue = reader.ReadUInt24(),
				glyphID = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"unicodeValue\": {0},\n", unicodeValue);
			builder.AppendFormat("\t\"glyphID\": {0}\n", glyphID);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
