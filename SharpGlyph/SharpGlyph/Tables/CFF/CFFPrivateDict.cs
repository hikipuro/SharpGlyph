using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFPrivateDict {
		/// <summary>
		/// Value: 6. [delta]
		/// </summary>
		public CFFArray BlueValues;

		/// <summary>
		/// Value: 7. [delta]
		/// </summary>
		public CFFArray OtherBlues;

		/// <summary>
		/// Value: 8. [delta]
		/// </summary>
		public CFFArray FamilyBlues;

		/// <summary>
		/// Value: 9. [delta]
		/// </summary>
		public CFFArray FamilyOtherBlues;

		/// <summary>
		/// Value: 12 9. [number]
		/// </summary>
		public double BlueScale;

		/// <summary>
		/// Value: 12 10. [number]
		/// </summary>
		public double BlueShift;

		/// <summary>
		/// Value: 12 11. [number]
		/// </summary>
		public double BlueFuzz;

		/// <summary>
		/// Value: 10. [number]
		/// </summary>
		public double StdHW;

		/// <summary>
		/// Value: 11. [number]
		/// </summary>
		public double StdVW;

		/// <summary>
		/// Value: 12 12. [delta]
		/// </summary>
		public CFFArray StemSnapH;

		/// <summary>
		/// Value: 12 13. [delta]
		/// </summary>
		public CFFArray StemSnapV;

		/// <summary>
		/// Value: 12 14. [boolean]
		/// </summary>
		public int ForceBold;

		/// <summary>
		/// Value: 12 17. [number]
		/// </summary>
		public double LanguageGroup;

		/// <summary>
		/// Value: 12 18. [number]
		/// </summary>
		public double ExpansionFactor;

		/// <summary>
		/// Value: 12 19. [number]
		/// </summary>
		public double initialRandomSeed;

		/// <summary>
		/// Value: 19. [number]
		/// </summary>
		public double Subrs;

		/// <summary>
		/// Value: 20. [number]
		/// </summary>
		public double defaultWidthX;

		/// <summary>
		/// Value: 21. [number]
		/// </summary>
		public double nominalWidthX;

		public CFFPrivateDict() {
			BlueValues = new CFFArray();
			OtherBlues = new CFFArray();
			FamilyBlues = new CFFArray();
			FamilyOtherBlues = new CFFArray();
			BlueScale = 0.039625;
			BlueShift = 7;
			BlueFuzz = 1;
			StdHW = 0;
			StdVW = 0;
			StemSnapH = new CFFArray();
			StemSnapV = new CFFArray();
			ForceBold = 0;
			LanguageGroup = 0;
			ExpansionFactor = 0.06;
			initialRandomSeed = 0;
			Subrs = 0;
			defaultWidthX = 0;
			nominalWidthX = 0;
		}

		public static CFFPrivateDict Read(BinaryReaderFont reader, int length) {
			CFFPrivateDict value = new CFFPrivateDict();
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
				case 6:
					BlueValues = CFFArray.Create(list);
					break;
				case 7:
					OtherBlues = CFFArray.Create(list);
					break;
				case 8:
					FamilyBlues = CFFArray.Create(list);
					break;
				case 9:
					FamilyOtherBlues = CFFArray.Create(list);
					break;
				case 10:
					StdHW = list[0];
					break;
				case 11:
					StdVW = list[0];
					break;
				case 12:
					SetValue12(list, key1);
					break;
				case 19:
					Subrs = list[0];
					break;
				case 20:
					defaultWidthX = list[0];
					break;
				case 21:
					nominalWidthX = list[0];
					break;
			}
		}

		protected void SetValue12(List<double> list, byte key1) {
			switch (key1) {
				case 9:
					BlueScale = list[0];
					break;
				case 10:
					BlueShift = list[0];
					break;
				case 11:
					BlueFuzz = list[0];
					break;
				case 12:
					StemSnapH = CFFArray.Create(list);
					break;
				case 13:
					StemSnapV = CFFArray.Create(list);
					break;
				case 14:
					ForceBold = (int)list[0];
					break;
				case 17:
					LanguageGroup = list[0];
					break;
				case 18:
					ExpansionFactor = list[0];
					break;
				case 19:
					initialRandomSeed = list[0];
					break;
			}
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"BlueValues\": {0},\n", BlueValues.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"OtherBlues\": {0},\n", OtherBlues.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"FamilyBlues\": {0},\n", FamilyBlues.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"FamilyOtherBlues\": {0},\n", FamilyOtherBlues.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"BlueScale\": {0},\n", BlueScale);
			builder.AppendFormat("\t\"BlueShift\": {0},\n", BlueShift);
			builder.AppendFormat("\t\"BlueFuzz\": {0},\n", BlueFuzz);
			builder.AppendFormat("\t\"StdHW\": {0},\n", StdHW);
			builder.AppendFormat("\t\"StdVW\": {0},\n", StdVW);
			builder.AppendFormat("\t\"StemSnapH\": {0},\n", StemSnapH.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"StemSnapV\": {0},\n", StemSnapV.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"ForceBold\": {0},\n", ForceBold);
			builder.AppendFormat("\t\"LanguageGroup\": {0},\n", LanguageGroup);
			builder.AppendFormat("\t\"ExpansionFactor\": {0},\n", ExpansionFactor);
			builder.AppendFormat("\t\"initialRandomSeed\": {0},\n", initialRandomSeed);
			builder.AppendFormat("\t\"Subrs\": {0},\n", Subrs);
			builder.AppendFormat("\t\"defaultWidthX\": {0},\n", defaultWidthX);
			builder.AppendFormat("\t\"nominalWidthX\": {0},\n", nominalWidthX);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
