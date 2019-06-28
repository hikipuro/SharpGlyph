using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Compact Font Format table (CFF).
	/// <para>CFF Outline</para>
	/// </summary>
	//[CFFOutline]
	public class CFFTable : Table {
		public const string Tag = "CFF ";

		/// <summary>
		/// Format major version (starting at 1).
		/// </summary>
		public byte major;

		/// <summary>
		/// Format minor version (starting at 0).
		/// </summary>
		public byte minor;

		/// <summary>
		/// Header size (bytes).
		/// </summary>
		public byte hdrSize;

		/// <summary>
		/// Absolute offset (0) size.
		/// </summary>
		public byte offSize;

		public string[] names;
		public CFFTopDict topDict;
		public CFFPrivateDict privateDict;
		public string[] strings;
		public byte[][] globalSubr;
		public byte[][] localSubr;
		public CFFCharset charsets;
		public CFFIndex CharStringsIndex;
		public byte[][] charStrings;
		public CFFFDSelect fdSelect;

		protected long position;
		protected string filePath;
		protected CFFCharString charString;

		public CFFTable() {
			charString = new CFFCharString();
		}

		public static CFFTable Read(BinaryReaderFont reader) {
			long position = reader.Position;
			CFFTable value = new CFFTable();
			value.position = reader.Position;
			value.filePath = reader.FilePath;
			value.major = reader.ReadByte();
			value.minor = reader.ReadByte();
			value.hdrSize = reader.ReadByte();
			value.offSize = reader.ReadByte();

			reader.Position = position + value.hdrSize;
			value.ReadName(reader);
			value.ReadTopDict(reader);
			value.ReadString(reader);
			value.ReadGlobalSubr(reader);

			CFFTopDict topDict = value.topDict;
			//if (topDict.CharStrings > 0) {
			//	reader.Position = position + topDict.CharStrings;
			//	value.ReadCharStrings(reader);
			//}

			if (topDict.FDSelect > 0) {
				reader.Position = position + topDict.FDSelect;
				value.fdSelect = CFFFDSelect.Read(reader);
			}

			if (topDict.charset > 0) {
				reader.Position = position + topDict.charset;
				//value.charsets = CFFCharset.Read(reader, value.CharStringsIndex.count);
			}

			if (topDict.Private.values[0] > 0) {
				reader.Position = position + (int)topDict.Private.values[1];
				value.ReadPrivate(reader);
			}

			//Console.WriteLine("Charset: " + value.charsets);
			//Console.WriteLine("Docode: {0}", CFFCharString.Decode(value.charStrings[0]));

			//index = CFFIndex.Read(reader);
			//Console.WriteLine("index ::4 " + index);
			return value;
		}

		public GraphicsPath GetGlyph(int glyphId, bool hasWidth) {
			if (glyphId < 0) {
				return null;
			}
			if (File.Exists(filePath) == false) {
				return null;
			}
			byte[] cs = null;
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + topDict.CharStrings;
				CFFIndex index = CFFIndex.Read(reader);
				//Console.WriteLine("CharStrings index: {0}", index);
				long offsetStart = reader.Position;
				int offset0 = 0;
				int offset1 = 0;
				index.ReadOffset(reader, glyphId, out offset0, out offset1);
				int byteSize = offset1 - offset0;
				if (byteSize <= 0) {
					return null;
				}
				//Console.WriteLine("offset0: {0}, {1}", offset0, offset1);
				//Console.WriteLine("lastItem: {0}", offsetStart);
				offsetStart += (index.count + 1) * index.offSize;
				//Console.WriteLine("lastItem: {0}", offsetStart);
				reader.Position = offsetStart + offset0 - 1;
				cs = reader.ReadBytes(byteSize);
			}
			if (cs == null) {
				return null;
			}
			//Console.WriteLine("Docode: {0}", CFFCharString.Decode(charString));
			return charString.CreateGlyph(cs, hasWidth);
		}

		public string GetString(int index) {
			index -= 391;
			if (index < 0 || index >= strings.Length) {
				return string.Empty;
			}
			return strings[index];
		}

		protected void ReadName(BinaryReaderFont reader) {
			CFFIndex index = CFFIndex.Read(reader);
			int[] offset = index.ReadAllOffsets(reader);
			names = new string[index.count];
			int length = offset.Length - 1;
			for (int i = 0; i < length; i++) {
				int byteSize = offset[i + 1] - offset[i];
				names[i] = reader.ReadString(byteSize);
			}
		}

		protected void ReadTopDict(BinaryReaderFont reader) {
			CFFIndex index = CFFIndex.Read(reader);
			//Console.WriteLine("topDict: {0}", index);
			int[] offset = index.ReadAllOffsets(reader);
			int length = offset[1] - offset[0];
			topDict = CFFTopDict.Read(reader, length);
		}

		protected void ReadString(BinaryReaderFont reader) {
			CFFIndex index = CFFIndex.Read(reader);
			int[] offset = index.ReadAllOffsets(reader);
			//Console.WriteLine("string index: {0}", index);
			strings = new string[index.count];
			int length = offset.Length - 1;
			for (int i = 0; i < length; i++) {
				int byteSize = offset[i + 1] - offset[i];
				//value.strings[i] = value.ConvertString(reader.ReadBytes(length));
				strings[i] = reader.ReadString(byteSize);
			}
		}

		protected void ReadGlobalSubr(BinaryReaderFont reader) {
			CFFIndex index = CFFIndex.Read(reader);
			int[] offset = index.ReadAllOffsets(reader);
			//Console.WriteLine("global subr index: {0}", index);
			globalSubr = new byte[index.count][];
			int length = offset.Length - 1;
			for (int i = 0; i < length; i++) {
				int byteSize = offset[i + 1] - offset[i];
				globalSubr[i] = reader.ReadBytes(byteSize);
			}
			charString.globalSubr = globalSubr;
		}

		protected void ReadCharStrings(BinaryReaderFont reader) {
			/*
			CFFIndex index = CFFIndex.Read(reader);
			int[] offset = index.offset;
			//Console.WriteLine("CharStrings index: {0}", index);
			charStrings = new byte[index.count][];
			int length = offset.Length - 1;
			for (int i = 0; i < length; i++) {
				int byteSize = offset[i + 1] - offset[i];
				charStrings[i] = reader.ReadBytes(byteSize);
			}
			CharStringsIndex = index;
			*/
		}

		protected void ReadPrivate(BinaryReaderFont reader) {
			long start = reader.Position;
			//Console.WriteLine("private dict: {0}", topDict.Private);
			privateDict = CFFPrivateDict.Read(reader, (int)topDict.Private.values[0]);
			//Console.WriteLine("private dict: {0}", privateDict);
			//reader.Position = position + (int)topDict.Private.values[1];
			//CFFIndex index = CFFIndex.Read(reader);
			//int[] offset = index.offset;
			//Console.WriteLine("local subr index: {0}", index);

			if (privateDict.Subrs > 0) {
				reader.Position = start + (int)privateDict.Subrs;
				CFFIndex index = CFFIndex.Read(reader);
				int[] offset = index.ReadAllOffsets(reader);
				//Console.WriteLine("local subr index: {0}", index);
				localSubr = new byte[index.count][];
				for (int i = 0; i < offset.Length - 1; i++) {
					int length = offset[i + 1] - offset[i];
					localSubr[i] = reader.ReadBytes(length);
				}
			}
			charString.localSubr = localSubr;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"major\": {0},\n", major);
			builder.AppendFormat("\t\"minor\": {0},\n", minor);
			builder.AppendFormat("\t\"hdrSize\": {0},\n", hdrSize);
			builder.AppendFormat("\t\"offSize\": {0},\n", offSize);
			builder.AppendLine("\t\"names\": [");
			for (int i = 0; i < names.Length; i++) {
				builder.AppendFormat("\t\t\"{0}\",\n", names[i]);
			}
			if (names.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.AppendFormat("\t\"topDict\": {0},\n", topDict.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"strings\": [\n");
			for (int i = 0; i < strings.Length; i++) {
				//builder.AppendFormat("\t\t\"{0}\",\n", strings[i]);
			}
			if (strings.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendFormat("\t]\n");
			builder.Append("}");
			return builder.ToString();
		}

		public string ConvertString(byte[] bytes) {
			char[] charset = {
				// 0x00 (0)
				'\0', ' ', '!', '"', '#', '$', '%', '&',
				'’', '(', ')', '*', '+', ',', '-', '.',
				// 0x10 (16)
				'/', '0', '1', '2', '3', '4', '5', '6',
				'7', '8', '9', ':', ';', '<', '=', '>',
				// 0x20 (32)
				'?', '@', 'A', 'B', 'C', 'D', 'E', 'F',
				'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
				// 0x30 (48)
				'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
				'W', 'X', 'Y', 'Z', '[', '\\', ']', '^',
				// 0x40 (64)
				'_', '‘', 'a', 'b', 'c', 'd', 'e', 'f',
				'h', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
				// 0x50 (80)
				'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
				'w', 'x', 'y', 'z', '{', '|', '}', '~',
				// 0x60 (96)
				'¡', '¢', '£', '⁄', '¥', 'ƒ', '§', '¤',
				'\'', '“', '«', '‹', '›', 'ﬁ', 'ﬂ', '–',
				// 0x70 (112)
				'†', '‡', '·', '¶', '•', '‚', '„', '”',
				'»', '…', '‰', '¿', '`', '´', 'ˆ', '˜',
				// 0x80 (128)
				'¯', '˘', '˙', '¨', '˚', '¸', '˝', '˛',
				'ˇ', '—', 'Æ', 'ª', 'Ł', 'Ø', 'Œ', 'º',
				// 0x90 (144)
				'æ', 'ı', 'ł', 'ø', 'œ', 'ß', '¹', '¬',
				'µ', '™', 'Ð', '½', '±', 'Þ', '¼', '÷',
				// 0xA0 (160)
				'¦', '°', 'þ', '¾', '²', '®', '−', 'ð',
				'×', '³', '©', 'Á', 'Â', 'Ä', 'À', 'Å',
				// 0xB0 (176)
				'Ã', 'Ç', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î',
				'Ï', 'Ì', 'Ñ', 'Ó', 'Ô', 'Ö', 'Ò', 'Õ',
				// 0xC0 (192)
				'Š', 'Ú', 'Û', 'Ü', 'Ù', 'Ý', 'Ÿ', 'Ž',
				'á', 'â', 'ä', 'à', 'å', 'ã', 'ç', 'é',
				// 0xD0 (208)
				'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ñ',
				'ó', 'ô', 'ö', 'ò', 'õ', 'š', 'ú', 'û',
				// 0xE0 (224)
				'ü', 'ù', 'ý', 'ÿ', 'ž', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',
				// 0xF0 (240)
				' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',
				' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '
			};
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++) {
				builder.Append(charset[bytes[i]]);
			}
			return builder.ToString();
		}
	}
}
