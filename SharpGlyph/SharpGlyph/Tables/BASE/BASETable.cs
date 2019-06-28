using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Baseline table (BASE).
	/// <para>Advanced Typographic Table</para>
	/// </summary>
	//[AdvancedTypographicTable]
	public class BASETable : Table {
		public const string Tag = "BASE";

		/// <summary>
		/// [Version 1.0]
		/// Major version of the BASE table.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// [Version 1.0]
		/// Minor version of the BASE table.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// [Version 1.0]
		/// Offset to horizontal Axis table,
		/// from beginning of BASE table (may be NULL).
		/// </summary>
		public ushort horizAxisOffset;

		/// <summary>
		/// [Version 1.0]
		/// Offset to vertical Axis table,
		/// from beginning of BASE table (may be NULL).
		/// </summary>
		public ushort vertAxisOffset;
		
		/// <summary>
		/// [Version 1.1]
		/// Offset to Item Variation Store table,
		/// from beginning of BASE table (may be null).
		/// </summary>
		public uint itemVarStoreOffset;

		public AxisTable horizAxis;
		public AxisTable vertAxis;
		//public VariationStore itemVarStore;

		public static BASETable Read(BinaryReaderFont reader) {
			long position = reader.Position;
			BASETable value = new BASETable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				horizAxisOffset = reader.ReadUInt16(),
				vertAxisOffset = reader.ReadUInt16()
			};
			if (value.majorVersion == 1 && value.minorVersion == 1) {
				value.itemVarStoreOffset = reader.ReadUInt32();
			}
			if (value.horizAxisOffset != 0) {
				reader.Position = position + value.horizAxisOffset;
				value.horizAxis = AxisTable.Read(reader);
			}
			if (value.vertAxisOffset != 0) {
				reader.Position = position + value.vertAxisOffset;
				value.vertAxis = AxisTable.Read(reader);
			}
			if (value.itemVarStoreOffset != 0) {
				reader.Position = position + value.itemVarStoreOffset;
				//value.itemVarStore = 
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"horizAxisOffset\": {0},\n", horizAxisOffset);
			builder.AppendFormat("\t\"vertAxisOffset\": {0},\n", vertAxisOffset);
			if (majorVersion == 1 && minorVersion == 1) {
				builder.AppendFormat("\t\"itemVarStoreOffset\": {0},\n", itemVarStoreOffset);
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
