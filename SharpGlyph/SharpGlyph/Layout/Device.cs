using System;
using System.Text;

namespace SharpGlyph {
	public class Device {
		/// <summary>
		/// Smallest size to correct, in ppem.
		/// </summary>
		public ushort startSize;
		
		/// <summary>
		/// Largest size to correct, in ppem.
		/// </summary>
		public ushort endSize;
		
		/// <summary>
		/// Format of deltaValue array data: 0x0001, 0x0002, or 0x0003.
		/// </summary>
		public ushort deltaFormat;
		
		/// <summary>
		/// Array of compressed data.
		/// </summary>
		public ushort[] deltaValue;

		public static Device Read(BinaryReaderFont reader) {
			return new Device {
				startSize = reader.ReadUInt16(),
				endSize = reader.ReadUInt16(),
				deltaFormat = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startSize\": {0},\n", startSize);
			builder.AppendFormat("\t\"endSize\": {0},\n", endSize);
			builder.AppendFormat("\t\"deltaFormat\": {0},\n", deltaFormat);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
