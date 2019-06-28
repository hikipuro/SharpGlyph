using System;
using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 12: Segmented coverage.
	/// </summary>
	public class CmapSubtable12 : CmapSubtable {
		/// <summary>
		/// Reserved; set to 0.
		/// </summary>
		public ushort reserved;

		/// <summary>
		/// Byte length of this subtable (including the header).
		/// </summary>
		public uint length;

		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public uint language;

		/// <summary>
		/// Number of groupings which follow.
		/// </summary>
		public uint numGroups;

		/// <summary>
		/// Array of SequentialMapGroup records.
		/// </summary>
		//public SequentialMapGroup[] groups;
		
		public static new CmapSubtable12 Read(BinaryReaderFont reader) {
			CmapSubtable12 value = new CmapSubtable12();
			value.filePath = reader.FilePath;
			value.format = reader.ReadUInt16();
			value.reserved = reader.ReadUInt16();
			value.length = reader.ReadUInt32();
			value.language = reader.ReadUInt32();
			value.numGroups = reader.ReadUInt32();
			value.position = reader.Position;
			//value.groups = SequentialMapGroup.ReadArray(reader, value.numGroups);
			return value;
		}

		public override int GetGlyphId(int charCode) {
			if (File.Exists(filePath) == false) {
				return 0;
			}
			SequentialMapGroup group = null;
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				int range = (int)(numGroups / 2);
				int index = range;
				//int count = (int)Math.Log(index, 2);
				//Console.WriteLine();
				//Console.WriteLine("charCode: {0:X}", charCode);
				while (range >= 1) {
					if (range > 1) {
						range /= 2;
					}
					reader.Position = position + index * SequentialMapGroup.ByteSize;
					SequentialMapGroup g = SequentialMapGroup.Read(reader);
					//Console.WriteLine("group: {0}, {1}, {2}", g, index, numGroups);
					if (g.endCharCode < charCode) {
						index += range;
						if (index >= numGroups) {
							break;
						}
						if (range == 1) {
							reader.Position = position + index * SequentialMapGroup.ByteSize;
							g = SequentialMapGroup.Read(reader);
							if (g.startCharCode > charCode) {
								break;
							}
						}
						continue;
					}
					if (g.startCharCode > charCode) {
						index -= range;
						if (index < 0) {
							break;
						}
						continue;
					}
					if (g.endCharCode >= charCode && g.startCharCode <= charCode) {
						group = g;
						break;
					}
					break;
				}
			}
			/*
			for (int i = 0; i < groups.Length; i++) {
				SequentialMapGroup g = groups[i];
				if (g.endCharCode >= charCode && g.startCharCode <= charCode) {
					group = g;
					break;
				}
			}
			*/
			if (group == null) {
				return 0;
			}
			return (ushort)(group.startGlyphID + (charCode - group.startCharCode));

			/*
			if (File.Exists(filePath) == false) {
				return 0;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + charCode;
				return reader.ReadByte();
			}
			*/
		}

		/*
		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			for (int i = 0; i < numGroups; i++) {
				SequentialMapGroup group = groups[i];
				ushort id = (ushort)group.startGlyphID;
				int endCharCode = (int)group.endCharCode;
				for (int j = (int)group.startCharCode; j <= endCharCode; j++) {
					table.Add(j, id++);
				}
			}
			return table;
		}
		*/

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"reserved\": \"{0}\",\n", reserved);
			builder.AppendFormat("\t\"length\": \"{0}\",\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.AppendFormat("\t\"numGroups\": \"{0}\",\n", numGroups);
			builder.AppendLine("\t\"groups\": [");
			//for (int i = 0; i < numGroups; i++) {
			//	string group = groups[i].ToString();
			//	builder.AppendFormat("\t\t{0},\n", group.Replace("\n", "\n\t\t"));
			//}
			//if (numGroups > 0) {
			//	builder.Remove(builder.Length - 2, 1);
			//}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
