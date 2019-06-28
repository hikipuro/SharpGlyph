using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFROS {
		public int Registry;
		public int Ordering;
		public int Supplement;

		public static CFFROS Create(List<double> list) {
			return new CFFROS {
				Registry = (int)list[0],
				Ordering = (int)list[1],
				Supplement = (int)list[2]
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"Registry\": {0},\n", Registry);
			builder.AppendFormat("\t\"Ordering\": {0},\n", Ordering);
			builder.AppendFormat("\t\"Supplement\": {0}\n", Supplement);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
