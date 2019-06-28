using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class CFFArray {
		public double[] values;

		public static CFFArray Create(List<double> list) {
			return new CFFArray(
				list.ToArray()
			);
		}

		public CFFArray() {
			values = new double[0];
		}

		public CFFArray(double[] values) {
			this.values = values;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			if (values == null) {
				builder.Append("[]");
				return builder.ToString();
			}
			builder.AppendLine("[");
			for (int i = 0; i < values.Length; i++) {
				builder.AppendFormat("\t{0},\n", values[i]);
			}
			if (values.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.Append("]");
			return builder.ToString();
		}
	}
}
