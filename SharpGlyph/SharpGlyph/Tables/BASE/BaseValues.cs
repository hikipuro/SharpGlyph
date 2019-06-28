using System;
using System.Text;

namespace SharpGlyph {
	public class BaseValues {
		/// <summary>
		/// Index number of default baseline for this script
		/// — equals index position of baseline tag in
		/// baselineTags array of the BaseTagList.
		/// </summary>
		public ushort defaultBaselineIndex;

		/// <summary>
		/// Number of BaseCoord tables defined
		/// — should equal baseTagCount in the BaseTagList.
		/// </summary>
		public ushort baseCoordCount;

		/// <summary>
		/// Array of offsets to BaseCoord tables,
		/// from beginning of BaseValues table
		/// — order matches baselineTags array in the BaseTagList.
		/// </summary>
		public ushort[] baseCoords;

		public static BaseValues Read(BinaryReaderFont reader) {
			BaseValues value = new BaseValues {
				defaultBaselineIndex = reader.ReadUInt16(),
				baseCoordCount = reader.ReadUInt16()
			};
			if (value.baseCoordCount != 0) {
				value.baseCoords = reader.ReadUInt16Array(
					value.baseCoordCount
				);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"defaultBaselineIndex\": {0},\n", defaultBaselineIndex);
			builder.AppendFormat("\t\"baseCoordCount\": {0},\n", baseCoordCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
