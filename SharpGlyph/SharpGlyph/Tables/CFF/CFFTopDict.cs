using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFTopDict {
		/// <summary>
		/// Value: 0. [SID]
		/// </summary>
		public ushort version;

		/// <summary>
		/// Value: 1. [SID]
		/// </summary>
		public ushort Notice;

		/// <summary>
		/// Value: 12 0. [SID]
		/// </summary>
		public ushort Copyright;

		/// <summary>
		/// Value: 2. [SID]
		/// </summary>
		public ushort FullName;

		/// <summary>
		/// Value: 3. [SID]
		/// </summary>
		public ushort FamilyName;

		/// <summary>
		/// Value: 4. [SID]
		/// </summary>
		public ushort Weight;

		/// <summary>
		/// Value: 12 1. [boolean]
		/// </summary>
		public int isFixedPitch;

		/// <summary>
		/// Value: 12 2. [number]
		/// </summary>
		public double ItalicAngle;

		/// <summary>
		/// Value: 12 3. [number]
		/// </summary>
		public double UnderlinePosition;

		/// <summary>
		/// Value: 12 4. [number]
		/// </summary>
		public double UnderlineThickness;

		/// <summary>
		/// Value: 12 5. [number]
		/// </summary>
		public double PaintType;

		/// <summary>
		/// Value: 12 6. [number]
		/// </summary>
		public double CharstringType;

		/// <summary>
		/// Value: 12 7. [array]
		/// </summary>
		public CFFArray FontMatrix;

		/// <summary>
		/// Value: 13. [number]
		/// </summary>
		public int UniqueID;

		/// <summary>
		/// Value: 5. [array]
		/// </summary>
		public CFFArray FontBBox;

		/// <summary>
		/// Value: 12 8. [number]
		/// </summary>
		public int StrokeWidth;

		/// <summary>
		/// Value: 14. [array]
		/// </summary>
		public CFFArray XUID;

		/// <summary>
		/// Value: 15. [number]
		/// <para>charset offset (0).</para>
		/// </summary>
		public int charset;

		/// <summary>
		/// Value: 16. [number]
		/// <para>encoding offset (0).</para>
		/// </summary>
		public int Encoding;

		/// <summary>
		/// Value: 17. [number]
		/// <para>CharStrings offset (0).</para>
		/// </summary>
		public int CharStrings;

		/// <summary>
		/// Value: 18. [number number]
		/// <para>Private DICT size and offset (0).</para>
		/// </summary>
		public CFFArray Private = new CFFArray();

		/// <summary>
		/// Value: 12 20. [number]
		/// <para>synthetic base font index.</para>
		/// </summary>
		public int SyntheticBase;

		/// <summary>
		/// Value: 12 21. [SID]
		/// <para>embedded PostScript language code.</para>
		/// </summary>
		public ushort PostScript;

		/// <summary>
		/// Value: 12 22. [SID]
		/// <para>(added as needed by Adobe-based technology)</para>
		/// </summary>
		public ushort BaseFontName;

		/// <summary>
		/// Value: 12 23. [delta]
		/// <para>(added as needed by Adobe-based technology)</para>
		/// </summary>
		public CFFArray BaseFontBlend;

		/// <summary>
		/// Value: 12 30. [SID SID number]
		/// <para>Registry Ordering Supplement.</para>
		/// </summary>
		public CFFROS ROS = new CFFROS();

		/// <summary>
		/// Value: 12 31. [number]
		/// </summary>
		public double CIDFontVersion;

		/// <summary>
		/// Value: 12 32. [number]
		/// </summary>
		public double CIDFontRevision;

		/// <summary>
		/// Value: 12 33. [number]
		/// </summary>
		public double CIDFontType;

		/// <summary>
		/// Value: 12 34. [number]
		/// </summary>
		public double CIDCount;

		/// <summary>
		/// Value: 12 35. [number]
		/// </summary>
		public double UIDBase;

		/// <summary>
		/// Value: 12 36. [number]
		/// <para>Font DICT (FD) INDEX offset (0).</para>
		/// </summary>
		public double FDArray;

		/// <summary>
		/// Value: 12 37. [number]
		/// <para>FDSelect offset (0).</para>
		/// </summary>
		public int FDSelect;

		/// <summary>
		/// Value: 12 38. [SID]
		/// <para>FD FontName.</para>
		/// </summary>
		public ushort FontName;

		public CFFTopDict() {
			const int DefaultSID = 0;
			version = DefaultSID;
			Notice = DefaultSID;
			Copyright = DefaultSID;
			FullName = DefaultSID;
			FamilyName = DefaultSID;
			Weight = DefaultSID;
			isFixedPitch = 0;
			ItalicAngle = 0;
			UnderlinePosition = -100;
			UnderlineThickness = 50;
			PaintType = 0;
			CharstringType = 2;
			FontMatrix = new CFFArray(
				new double[] { 0.001, 0, 0, 0.001, 0, 0 }
			);
			UniqueID = DefaultSID;
			FontBBox = new CFFArray(
				new double[] { 0, 0, 0, 0 }
			);
			StrokeWidth = 0;
			XUID = new CFFArray();
			charset = 0;
			Encoding = 0;
			CharStrings = 0;
			Private = new CFFArray(
				new double[] { 0, 0 }
			);
			SyntheticBase = 0;
			PostScript = DefaultSID;
			BaseFontName = DefaultSID;
			BaseFontBlend = new CFFArray();
			ROS = new CFFROS();
			CIDFontVersion = 0;
			CIDFontRevision = 0;
			CIDFontType = 0;
			CIDCount = 8720;
			UIDBase = 0;
			FDArray = 0;
			FDSelect = 0;
			FontName = DefaultSID;
		}

		public static CFFTopDict Read(BinaryReaderFont reader, int length) {
			CFFTopDict value = new CFFTopDict();
			long start = reader.Position;
			List<double> list = new List<double>();
			byte key0 = 0;
			byte key1 = 0;
			while ((reader.Position - start) < length) {
				byte n = reader.PeekByte();
				if (n <= 21) {
					key0 = reader.ReadByte();
					if (key0 == 12) {
						key1 = reader.ReadByte();
					}
					//Console.WriteLine("key0: " + key0);
					//if (key1 != 0) {
					//	Console.WriteLine("key1: " + key1);
					//}
					value.SetValue(list, key0, key1);
					key1 = 0;
					list.Clear();
				} else {
					list.Add(reader.ReadCFFNumber());
				}
			}
			return value;
		}

		protected void SetValue(List<double> list, byte key0, byte key1) {
			switch (key0) {
				case 0:
					version = (ushort)list[0];
					break;
				case 1:
					Notice = (ushort)list[0];
					break;
				case 2:
					FullName = (ushort)list[0];
					break;
				case 3:
					FamilyName = (ushort)list[0];
					break;
				case 4:
					Weight = (ushort)list[0];
					break;
				case 5:
					FontBBox = CFFArray.Create(list);
					break;
				case 12:
					SetValue12(list, key1);
					break;
				case 13:
					UniqueID = (int)list[0];
					break;
				case 14:
					XUID = CFFArray.Create(list);
					break;
				case 15:
					charset = (int)list[0];
					break;
				case 16:
					Encoding = (int)list[0];
					break;
				case 17:
					CharStrings = (int)list[0];
					break;
				case 18:
					Private = CFFArray.Create(list);
					break;
			}
		}

		protected void SetValue12(List<double> list, byte key1) {
			switch (key1) {
				case 0:
					Copyright = (ushort)list[0];
					break;
				case 1:
					isFixedPitch = (int)list[0];
					break;
				case 2:
					ItalicAngle = list[0];
					break;
				case 3:
					UnderlinePosition = list[0];
					break;
				case 4:
					UnderlineThickness = list[0];
					break;
				case 5:
					PaintType = list[0];
					break;
				case 6:
					CharstringType = list[0];
					break;
				case 7:
					FontMatrix = CFFArray.Create(list);
					break;
				case 8:
					StrokeWidth = (int)list[0];
					break;
				case 20:
					SyntheticBase = (int)list[0];
					break;
				case 21:
					PostScript = (ushort)list[0];
					break;
				case 22:
					BaseFontName = (ushort)list[0];
					break;
				case 23:
					BaseFontBlend = CFFArray.Create(list);
					break;
				case 30:
					ROS = CFFROS.Create(list);
					break;
				case 31:
					CIDFontVersion = list[0];
					break;
				case 32:
					CIDFontRevision = list[0];
					break;
				case 33:
					CIDFontType = list[0];
					break;
				case 34:
					CIDCount = list[0];
					break;
				case 35:
					UIDBase = list[0];
					break;
				case 36:
					FDArray = list[0];
					break;
				case 37:
					FDSelect = (int)list[0];
					break;
				case 38:
					FontName = (ushort)list[0];
					break;
			}
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"Notice\": {0},\n", Notice);
			builder.AppendFormat("\t\"Copyright\": {0},\n", Copyright);
			builder.AppendFormat("\t\"FullName\": {0},\n", FullName);
			builder.AppendFormat("\t\"FamilyName\": {0},\n", FamilyName);
			builder.AppendFormat("\t\"Weight\": {0},\n", Weight);
			builder.AppendFormat("\t\"isFixedPitch\": {0},\n", isFixedPitch);
			builder.AppendFormat("\t\"ItalicAngle\": {0},\n", ItalicAngle);
			builder.AppendFormat("\t\"UnderlinePosition\": {0},\n", UnderlinePosition);
			builder.AppendFormat("\t\"UnderlineThickness\": {0},\n", UnderlineThickness);
			builder.AppendFormat("\t\"PaintType\": {0},\n", PaintType);
			builder.AppendFormat("\t\"CharstringType\": {0},\n", CharstringType);
			builder.AppendFormat("\t\"FontMatrix\": {0},\n", FontMatrix.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"UniqueID\": {0},\n", UniqueID);
			builder.AppendFormat("\t\"FontBBox\": {0},\n", FontBBox.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"StrokeWidth\": {0},\n", StrokeWidth);
			builder.AppendFormat("\t\"XUID\": {0},\n", XUID.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"charset\": {0},\n", charset);
			builder.AppendFormat("\t\"Encoding\": {0},\n", Encoding);
			builder.AppendFormat("\t\"CharStrings\": {0},\n", CharStrings);
			builder.AppendFormat("\t\"Private\": {0},\n", Private.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"SyntheticBase\": {0},\n", SyntheticBase);
			builder.AppendFormat("\t\"PostScript\": {0},\n", PostScript);
			builder.AppendFormat("\t\"BaseFontName\": {0},\n", BaseFontName);
			builder.AppendFormat("\t\"BaseFontBlend\": {0},\n", BaseFontBlend.ToString().Replace("\n", "\n\t"));

			builder.AppendFormat("\t\"ROS\": {0},\n", ROS.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"CIDFontVersion\": {0},\n", CIDFontVersion);
			builder.AppendFormat("\t\"CIDFontRevision\": {0},\n", CIDFontRevision);
			builder.AppendFormat("\t\"CIDFontType\": {0},\n", CIDFontType);
			builder.AppendFormat("\t\"CIDCount\": {0},\n", CIDCount);
			builder.AppendFormat("\t\"UIDBase\": {0},\n", UIDBase);
			builder.AppendFormat("\t\"FDArray\": {0},\n", FDArray);
			builder.AppendFormat("\t\"FDSelect\": {0},\n", FDSelect);
			builder.AppendFormat("\t\"FontName\": {0}\n", FontName);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
