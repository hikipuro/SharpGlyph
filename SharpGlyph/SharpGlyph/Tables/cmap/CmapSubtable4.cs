using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 4: Segment mapping to delta values.
	/// </summary>
	public class CmapSubtable4 : CmapSubtable {
		/// <summary>
		/// This is the length in bytes of the subtable.
		/// </summary>
		public ushort length;
		
		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public ushort language;

		/// <summary>
		/// 2 × segCount.
		/// </summary>
		public ushort segCountX2;

		/// <summary>
		/// 2 × (2**floor(log2(segCount))).
		/// </summary>
		public ushort searchRange;

		/// <summary>
		/// log2(searchRange/2).
		/// </summary>
		public ushort entrySelector;

		/// <summary>
		/// 2 × segCount - searchRange.
		/// </summary>
		public ushort rangeShift;

		/// <summary>
		/// End characterCode for each segment, last=0xFFFF.
		/// </summary>
		//public ushort[] endCode;

		/// <summary>
		/// Set to 0.
		/// </summary>
		//public ushort reservedPad;

		/// <summary>
		/// Start character code for each segment.
		/// </summary>
		//public ushort[] startCode;

		/// <summary>
		/// Delta for all character codes in segment.
		/// </summary>
		//public ushort[] idDelta;

		/// <summary>
		/// Offsets into glyphIdArray or 0.
		/// </summary>
		//public ushort[] idRangeOffset;

		/// <summary>
		/// Glyph index array (arbitrary length).
		/// </summary>
		//public ushort[] glyphIdArray;

		//CharToGlyphTable table;

		public static new CmapSubtable4 Read(BinaryReaderFont reader) {
			CmapSubtable4 value = new CmapSubtable4();
			value.filePath = reader.FilePath;
			value.format = reader.ReadUInt16();
			value.length = reader.ReadUInt16();
			value.language = reader.ReadUInt16();
			value.segCountX2 = reader.ReadUInt16();
			value.searchRange = reader.ReadUInt16();
			value.entrySelector = reader.ReadUInt16();
			value.rangeShift = reader.ReadUInt16();
			value.position = reader.Position;

			/*
			int segCount = value.segCountX2 / 2;
			value.endCode = reader.ReadUInt16Array(segCount);
			value.reservedPad = reader.ReadUInt16();
			value.startCode = reader.ReadUInt16Array(segCount);
			value.idDelta = reader.ReadUInt16Array(segCount);
			//long position = reader.Position;
			value.position = reader.Position;
			value.idRangeOffset = reader.ReadUInt16Array(segCount);
			*/

			/*
			value.table = new CharToGlyphTable();

			for (int i = 0; i < segCount; i++) {
				int offset = value.idRangeOffset[i];
				if (offset == 0) {
					int delta = value.idDelta[i];
					int end = value.endCode[i];
					for (int j = value.startCode[i]; j <= end; j++) {
						value.table.Add(j, (ushort)(j + delta));
					}
				} else {
					int delta = value.idDelta[i];
					int start = value.startCode[i];
					int end = value.endCode[i];
					for (int j = start; j <= end; j++) {
						reader.Position = position + offset + (j - start + i) * 2;
						int glyphId = reader.ReadUInt16();
						glyphId += delta;
						value.table.Add(j, (ushort)glyphId);
					}
				}
			}
			*/
			return value;
		}

		public override int GetGlyphId(int charCode) {
			if (File.Exists(filePath) == false) {
				return 0;
			}
			int segCount = segCountX2 / 2;
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position;
				int index = -1;
				for (int i = 0; i < segCount; i++) {
					ushort end = reader.ReadUInt16();
					//System.Console.WriteLine("end: {0}", end);
					if (end >= charCode) {
						index = i;
						break;
					}
				}
				if (index < 0) {
					return 0;
				}
				long pos = position + index * 2 + 2 + segCountX2;
				reader.Position = pos;
				ushort start = reader.ReadUInt16();
				if (start > charCode) {
					return 0;
				}
				pos += segCountX2;
				reader.Position = pos;
				ushort delta = reader.ReadUInt16();
				pos += segCountX2;
				reader.Position = pos;
				ushort offset = reader.ReadUInt16();
				if (offset == 0) {
					return (ushort)(charCode + delta);
				}
				reader.Position = position + offset + (charCode - start + index) * 2;
				return (ushort)(reader.ReadUInt16() + delta);
			}
			/*
			int segCount = segCountX2 / 2;
			int index = -1;
			for (int i = 0; i < segCount; i++) {
				if (endCode[i] >= charCode) {
					index = i;
					break;
				}
			}
			if (index < 0) {
				return 0;
			}
			if (startCode[index] > charCode) {
				return 0;
			}
			int offset = idRangeOffset[index];
			if (offset == 0) {
				return (ushort)(charCode + idDelta[index]);
			}
			if (File.Exists(filePath) == false) {
				return 0;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + offset + (charCode - startCode[index] + index) * 2;
				return (ushort)(reader.ReadUInt16() + idDelta[index]);
			}
			*/
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			//return table;
			return null;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": {0},\n", length);
			builder.AppendFormat("\t\"language\": {0},\n", language);
			builder.AppendFormat("\t\"segCountX2\": {0},\n", segCountX2);
			builder.AppendFormat("\t\"searchRange\": {0},\n", searchRange);
			builder.AppendFormat("\t\"entrySelector\": {0},\n", entrySelector);
			builder.AppendFormat("\t\"rangeShift\": {0},\n", rangeShift);
			/*
			builder.AppendLine("\t\"endCode\": [");
			for (int i = 0; i < endCode.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X4},\n", endCode[i]);
			}
			if (endCode.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendFormat("\t\"reservedPad\": {0},\n", reservedPad);
			builder.AppendLine("\t\"startCode\": [");
			for (int i = 0; i < startCode.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X4},\n", startCode[i]);
			}
			if (startCode.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendLine("\t\"idDelta\": [");
			for (int i = 0; i < idDelta.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X4},\n", idDelta[i]);
			}
			if (idDelta.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t\"idRangeOffset\": [");
			for (int i = 0; i < idRangeOffset.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X4},\n", idRangeOffset[i]);
			}
			if (idRangeOffset.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
