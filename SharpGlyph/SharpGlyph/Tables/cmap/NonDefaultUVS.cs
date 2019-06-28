using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class NonDefaultUVS {
		/// <summary>
		/// Number of UVS Mappings.
		/// </summary>
		public uint numUVSMappings;
		
		/// <summary>
		/// Array of UVSMapping records.
		/// </summary>
		public UVSMapping[] uvsMappings;

		public Dictionary<int, uint> uvsMappingsTable;

		public static NonDefaultUVS Read(BinaryReaderFont reader) {
			NonDefaultUVS value = new NonDefaultUVS {
				numUVSMappings = reader.ReadUInt32()
			};
			value.uvsMappings = UVSMapping.ReadArray(reader, value.numUVSMappings);
			value.uvsMappingsTable = new Dictionary<int, uint>();
			for (int i = 0; i < value.uvsMappings.Length; i++) {
				UVSMapping mapping = value.uvsMappings[i];
				value.uvsMappingsTable.Add(
					mapping.unicodeValue,
					mapping.glyphID
				);
			}
			return value;
		}

		public uint FindGlyphId(int codePoint) {
			if (uvsMappingsTable.ContainsKey(codePoint) == false) {
				return 0;
			}
			return uvsMappingsTable[codePoint];
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"numUVSMappings\": {0},\n", numUVSMappings);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
