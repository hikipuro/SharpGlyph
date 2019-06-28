using System;
using System.Text;

namespace SharpGlyph {
	public class BaseTagList {
		/// <summary>
		/// Number of baseline identification tags in this text direction — may be zero (0).
		/// </summary>
		public ushort baseTagCount;
		
		/// <summary>
		/// Array of 4-byte baseline identification tags — must be in alphabetical order.
		/// </summary>
		public string[] baselineTags;

		public static BaseTagList Read(BinaryReaderFont reader) {
			BaseTagList value = new BaseTagList {
				baseTagCount = reader.ReadUInt16()
			};
			if (value.baseTagCount != 0) {
				value.baselineTags = reader.ReadTagArray(value.baseTagCount);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseTagCount\": {0},\n", baseTagCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
