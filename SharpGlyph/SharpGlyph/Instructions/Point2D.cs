﻿using System;

namespace SharpGlyph {
	public class Point2D {
		public static readonly float PI = (float)Math.PI;
		public static readonly Point2D Empty = new Point2D(0, 0);

		public float x;
		public float y;

		public Point2D(float x, float y) {
			this.x = x;
			this.y = y;
		}

		public float GetAngle(Point2D p) {
			return (float)Math.Atan2(p.y - y, p.x - x);
		}

		public float GetLength(float vector) {
			if (vector.Equals(0f)) {
				return Sqrt(x * x + y * y);
			}
			float cos = Cos(-vector);
			float sin = Sin(-vector);
			float dx = x * cos - y * sin;
			float dy = y * cos + x * sin;
			float sign = dx < 0 || dy < 0 ? -1 : 1;
			return sign * Sqrt(dx * dx + dy * dy);
		}

		protected static float Sin(float value) {
			return (float)Math.Sin(value);
		}

		protected static float Cos(float value) {
			return (float)Math.Cos(value);
		}

		protected static float Sqrt(float value) {
			if (value.Equals(0f)) {
				return 0f;
			}
			return (float)Math.Sqrt(value);
		}
	}
}
