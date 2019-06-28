using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 14: Unicode Variation Sequences.
	/// </summary>
	public class CmapSubtable14 : CmapSubtable {
		/// <summary>
		/// Byte length of this subtable (including this header).
		/// </summary>
		public uint length;

		/// <summary>
		/// Number of variation Selector Records.
		/// </summary>
		public uint numVarSelectorRecords;

		/// <summary>
		/// Array of VariationSelector records.
		/// </summary>
		public VariationSelector[] varSelector;

		public Dictionary<int, VariationSelector> varSelectorTable;
		
		public static new CmapSubtable14 Read(BinaryReaderFont reader) {
			long start = reader.Position;
			CmapSubtable14 value = new CmapSubtable14();
			value.format = reader.ReadUInt16();
			value.length = reader.ReadUInt32();
			value.numVarSelectorRecords = reader.ReadUInt32();
			/*
			value.varSelector = VariationSelector.ReadArray(
				reader, value.numVarSelectorRecords, start
			);
			value.varSelectorTable = new Dictionary<int, VariationSelector>();
			for (int i = 0; i < value.varSelector.Length; i++) {
				VariationSelector selector = value.varSelector[i];
				value.varSelectorTable.Add(
					selector.varSelector,
					selector
				);
			}
			*/
			return value;
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			return table;
		}

		public uint FindGlyphId(int codePoint, int varSelector) {
			VariationSelector selector = FindVarSelector(varSelector);
			if (selector == null) {
				return 0;
			}
			NonDefaultUVS uvs = selector.nonDefaultUVS;
			return uvs.FindGlyphId(codePoint);
		}

		public VariationSelector FindVarSelector(int varSelector) {
			if (varSelectorTable.ContainsKey(varSelector) == false) {
				return null;
			}
			return varSelectorTable[varSelector];
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"numVarSelectorRecords\": \"{0}\",\n", numVarSelectorRecords);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
