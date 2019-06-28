using System;
using System.Text;

namespace SharpGlyph {
	public class OffsetTable {
		public static readonly uint Version1 = 0x00010000;
		public static readonly uint VersionOTTO = 0x4F54544F;

		/// <summary>
		/// 0x00010000 or 0x4F54544F ('OTTO').
		/// </summary>
		public uint sfntVersion;

		/// <summary>
		/// Number of tables.
		/// </summary>
		public ushort numTables;

		/// <summary>
		/// (Maximum power of 2 &lt;= numTables) x 16.
		/// </summary>
		public ushort searchRange;

		/// <summary>
		/// Log2(maximum power of 2 &lt;= numTables).
		/// </summary>
		public ushort entrySelector;

		/// <summary>
		/// NumTables x 16-searchRange.
		/// </summary>
		public ushort rangeShift;

		public static OffsetTable Read(BinaryReaderFont reader) {
			OffsetTable value = new OffsetTable();
			value.sfntVersion = reader.ReadUInt32();
			if (value.sfntVersion != Version1 && value.sfntVersion != VersionOTTO) {
				throw new Exception(
					string.Format("Parse error: sfntVersion is not valid. 0x{0:X8}", value.sfntVersion)
				);
			}
			value.numTables = reader.ReadUInt16();
			value.searchRange = reader.ReadUInt16();
			if (MaximumPowerOf2(value.numTables) * 16 != value.searchRange) {
				throw new Exception(
					string.Format("Parse error: searchRange is not valid. {0}", value.searchRange)
				);
			}
			value.entrySelector = reader.ReadUInt16();
			if ((ushort)Math.Log(MaximumPowerOf2(value.numTables), 2) != value.entrySelector) {
				throw new Exception(
					string.Format("Parse error: entrySelector is not valid. {0}", value.entrySelector)
				);
			}
			value.rangeShift = reader.ReadUInt16();
			if (value.numTables * 16 - value.searchRange != value.rangeShift) {
				/*throw new Exception(
					string.Format("Parse error: rangeShift is not valid. {0}", value.rangeShift)
				);*/
			}
			return value;
		}

		protected static bool IsPowerOf2(int n) {
			if (n == 0) {
				return false;
			}
			while (n != 1) {
				if (n % 2 != 0) {
					return false;
				}
				n = n / 2;
			}
			return true;
		}

		protected static int MaximumPowerOf2(int n) {
			if (IsPowerOf2(n)) {
				return n;
			}
			n--;
			n |= n >> 1;
			n |= n >> 2;
			n |= n >> 4;
			n |= n >> 8;
			n |= n >> 16;
			n++;
			return n >> 1;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"sfntVersion\": 0x{0:X8},\n", sfntVersion);
			builder.AppendFormat("\t\"numTables\": {0},\n", numTables);
			builder.AppendFormat("\t\"searchRange\": {0},\n", searchRange);
			builder.AppendFormat("\t\"entrySelector\": {0},\n", entrySelector);
			builder.AppendFormat("\t\"rangeShift\": {0}\n", rangeShift);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
