using System;
using System.Text;

namespace SharpGlyph {
	public class VariationSelector {
		/// <summary>
		/// Variation selector.
		/// </summary>
		public int varSelector;
		
		/// <summary>
		/// Offset from the start of the format 14 subtable to Default UVS Table. May be 0.
		/// </summary>
		public uint defaultUVSOffset;
		
		/// <summary>
		/// Offset from the start of the format 14 subtable to Non-Default UVS Table. May be 0.
		/// </summary>
		public uint nonDefaultUVSOffset;

		public DefaultUVS defaultUVS;
		public NonDefaultUVS nonDefaultUVS;

		public static VariationSelector[] ReadArray(BinaryReaderFont reader, uint count, long start) {
			VariationSelector[] array = new VariationSelector[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader, start);
			}
			return array;
		}

		public static VariationSelector Read(BinaryReaderFont reader, long start) {
			VariationSelector value = new VariationSelector {
				varSelector = reader.ReadUInt24(),
				defaultUVSOffset = reader.ReadUInt32(),
				nonDefaultUVSOffset = reader.ReadUInt32()
			};
			long position = reader.Position;
			if (value.defaultUVSOffset != 0) {
				reader.Position = start + value.defaultUVSOffset;
				value.defaultUVS = DefaultUVS.Read(reader);
			}
			if (value.nonDefaultUVSOffset != 0) {
				reader.Position = start + value.nonDefaultUVSOffset;
				value.nonDefaultUVS = NonDefaultUVS.Read(reader);
			}
			reader.Position = position;
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"varSelector\": 0x{0:X8},\n", varSelector);
			builder.AppendFormat("\t\"defaultUVSOffset\": 0x{0:X8},\n", defaultUVSOffset);
			builder.AppendFormat("\t\"nonDefaultUVSOffset\": 0x{0:X8}\n", nonDefaultUVSOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
