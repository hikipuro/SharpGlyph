﻿using System;
using System.Drawing;

namespace SharpGlyph {
	public class GraphicsState {
		public static readonly float YAxis = (float)(Math.PI / 2);
		public bool auto_flip;
		/// <summary>
		/// F26Dot6
		/// </summary>
		public uint control_value_cut_in;
		public int delta_base;
		public int delta_shift;
		public float dual_projection_vectors;
		public float freedom_vector;
		public int zp0;
		public int zp1;
		public int zp2;
		public int instruction_control;
		public int loop;
		/// <summary>
		/// F26Dot6
		/// </summary>
		public uint minimum_distance;
		public float projection_vector;
		public int round_state;
		public int rp0;
		public int rp1;
		public int rp2;
		public bool scan_control;
		/// <summary>
		/// F26Dot6
		/// </summary>
		public uint singe_width_cut_in;
		public int single_width_value;

		public GraphicsState() {
			Reset();
		}

		public void Reset() {
			auto_flip = true;
			control_value_cut_in = 0x44;
			delta_base = 9;
			delta_shift = 3;
			// dual_projection_vectors
			freedom_vector = 0;
			zp0 = 1;
			zp1 = 1;
			zp2 = 1;
			instruction_control = 0;
			loop = 1;
			minimum_distance = 1;
			projection_vector = 0;
			round_state = 1;
			rp0 = 0;
			rp1 = 0;
			rp2 = 0;
			scan_control = false;
			singe_width_cut_in = 0;
			single_width_value = 0;
		}

		public void SetProjectionVector(PointF a, PointF b) {
			projection_vector = (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
		}

		public void SetProjectionVectorY(PointF a, PointF b) {
			projection_vector = (float)(Math.Atan2(b.Y - a.Y, b.X - a.X) + Math.PI / 2);
		}

		public void SetFreedomVector(PointF a, PointF b) {
			freedom_vector = (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
		}

		public void SetFreedomVectorY(PointF a, PointF b) {
			freedom_vector = (float)(Math.Atan2(b.Y - a.Y, b.X - a.X) + Math.PI / 2);
		}

		public void SetDualProjectionVectors(PointF a, PointF b) {
			dual_projection_vectors = (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
		}

		public void SetDualProjectionVectorsY(PointF a, PointF b) {
			dual_projection_vectors = (float)(Math.Atan2(b.Y - a.Y, b.X - a.X) + Math.PI / 2);
		}
	}
}
