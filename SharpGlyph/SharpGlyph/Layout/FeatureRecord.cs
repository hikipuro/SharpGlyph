using System;
using System.Text;

namespace SharpGlyph {
	public class FeatureRecord {
		/// <summary>
		/// 4-byte feature identification tag.
		/// </summary>
		public string featureTag;
		
		/// <summary>
		/// Offset to Feature table, from beginning of FeatureList.
		/// </summary>
		public ushort featureOffset;

		public static FeatureRecord Read(BinaryReaderFont reader) {
			return new FeatureRecord {
				featureTag = reader.ReadTag(),
				featureOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"featureTag\": \"{0}\",\n", featureTag);
			builder.AppendFormat("\t\"featureOffset\": {0},\n", featureOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
