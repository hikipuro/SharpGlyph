using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class LongHorMetric {
		public static readonly int ByteSize = 4;

		/// <summary>
		/// Advance width, in font design units.
		/// </summary>
		public ushort advanceWidth;

		/// <summary>
		/// Glyph left side bearing, in font design units.
		/// </summary>
		public short lsb;

		public static List<LongHorMetric> ReadList(BinaryReaderFont reader, int count) {
			List<LongHorMetric> array = new List<LongHorMetric>();
			for (int i = 0; i < count; i++) {
				array.Add(Read(reader));
			}
			return array;
		}

		/*
		public static LongHorMetric[] ReadArray(BinaryReaderFont reader, int count) {
			LongHorMetric[] array = new LongHorMetric[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}
		*/

		public static LongHorMetric Read(BinaryReaderFont reader) {
			return new LongHorMetric {
				advanceWidth = reader.ReadUInt16(),
				lsb = reader.ReadInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"advanceWidth\": {0},\n", advanceWidth);
			builder.AppendFormat("\t\"lsb\": {0},\n", lsb);
			builder.AppendLine("}");
			return builder.ToString();
		}
	}
}
