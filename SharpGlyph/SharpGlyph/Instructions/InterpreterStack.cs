using System;
namespace SharpGlyph {
	public class InterpreterStack {
		protected int[] data;
		protected int index;

		public int Length {
			get { return data.Length; }
		}

		public int Depth {
			get { return index; }
		}

		public InterpreterStack() {
			data = new int[0];
			index = 0;
		}

		public void Init(int maxDepth) {
			if (data == null || data.Length != maxDepth) {
				data = new int[maxDepth];
			}
			index = 0;
		}

		public void Push(byte value) {
			data[index] = value;
			index++;
		}

		public void Push(ushort value) {
			int n = value;
			if ((n & 0x8000) > 0) {
				n = (int)(n | 0xFFFF0000);
			}
			data[index] = n;
			index++;
		}

		public void Push(int value) {
			data[index] = value;
			index++;
		}

		public void Push(bool value) {
			data[index] = value == true ? 1 : 0;
			index++;
		}

		public void PushBytes(InterpreterStream stream, int count) {
			for (int i = 0; i < count; i++) {
				data[index++] = stream.Next();
			}
		}

		public void PushWords(InterpreterStream stream, int count) {
			for (int i = 0; i < count; i++) {
				data[index++] = stream.NextWord();
			}
		}

		//public void Push(float value) {
		//data[index] = (int)(value * 64);
		//index++;
		//}

		public void PushVector(float value) {
			data[index + 1] = (int)(Sin(value) * 0x4000);
			data[index] = (int)(Cos(value) * 0x4000);
			index += 2;
		}

		public int Pop() {
			int value = data[index - 1];
			index--;
			return value;
		}

		public float PopF2Dot14() {
			float value = data[index - 1] & 0xFFFF;
			index--;
			return value / 0x4000;
		}

		public float PopF26Dot6() {
			float value = data[index - 1];
			index--;
			return value / 0x40;
		}

		public float PopVector() {
			int x = data[index - 2];
			int y = data[index - 1];
			index -= 2;
			return (float)Math.Atan2(y, x);
		}

		public int Peek() {
			return data[index - 1];
		}

		public void Reset() {
			index = 0;
		}

		public void Dup() {
			data[index] = data[index - 1];
			index++;
		}

		public void Swap() {
			int top = data[index - 1];
			data[index - 1] = data[index - 2];
			data[index - 2] = top;
		}

		public void PushDepth() {
			data[index] = index;
			index++;
		}

		public void PushCopy(int i) {
			if (i <= 0) {
				return;
			}
			data[index] = data[index - i];
			index++;
		}

		public void MoveToTop(int i) {
			if (i <= 1) {
				return;
			}
			int item = data[index - i];
			int end = index - 1;
			for (int n = index - i; n < end; n++) {
				data[n] = data[n + 1];
			}
			data[index - 1] = item;
		}

		public void Roll() {
			int a = data[index - 1];
			data[index - 1] = data[index - 3];
			data[index - 3] = data[index - 2];
			data[index - 2] = a;
		}

		public void LT() {
			int e2 = data[index - 1];
			int e1 = data[index - 2];
			data[index - 2] = e1 < e2 ? 1 : 0;
			index--;
		}

		public void LTEQ() {
			int e2 = data[index - 1];
			int e1 = data[index - 2];
			data[index - 2] = e1 <= e2 ? 1 : 0;
			index--;
		}

		public void GT() {
			int e2 = data[index - 1];
			int e1 = data[index - 2];
			data[index - 2] = e1 > e2 ? 1 : 0;
			index--;
		}

		public void GTEQ() {
			int e2 = data[index - 1];
			int e1 = data[index - 2];
			data[index - 2] = e1 >= e2 ? 1 : 0;
			index--;
		}

		public void EQ() {
			int e1 = data[index - 1];
			int e2 = data[index - 2];
			data[index - 2] = e1 == e2 ? 1 : 0;
			index--;
		}

		public void NEQ() {
			int e1 = data[index - 1];
			int e2 = data[index - 2];
			data[index - 2] = e1 != e2 ? 1 : 0;
			index--;
		}

		public void And() {
			if (data[index - 1] != 0
			&&  data[index - 2] != 0) {
				data[index - 2] = 1;
			} else {
				data[index - 2] = 0;
			}
			index--;
		}

		public void Or() {
			if (data[index - 1] != 0
			||  data[index - 2] != 0) {
				data[index - 2] = 1;
			} else {
				data[index - 2] = 0;
			}
			index--;
		}

		public void Not() {
			int e = data[index - 1];
			data[index - 1] = e == 0 ? 1 : 0;
		}

		public void Odd(RoundState roundState) {
			int e1 = data[index - 1];
			e1 = RoundValue(e1, roundState);
			data[index - 1] = (e1 & 0x40) > 0 ? 1 : 0;
		}

		public void Even(RoundState roundState) {
			int e1 = data[index - 1];
			e1 = RoundValue(e1, roundState);
			data[index - 1] = (e1 & 0x40) == 0 ? 1 : 0;
		}

		public void Add() {
			data[index - 2] += data[index - 1];
			index--;
		}

		public void Sub() {
			data[index - 2] -= data[index - 1];
			index--;
		}

		public void Mul() {
			long n2 = data[index - 2];
			n2 *= data[index - 1];
			data[index - 2] = (int)(n2 >> 6);
			index--;
		}

		public void Div() {
			long n2 = data[index - 2];
			n2 <<= 6;
			int n1 = data[index - 1];
			if (n1 == 0) {
				data[index - 2] = 0;
			} else {
				data[index - 2] = (int)(n2 / data[index - 1]);
			}
			index--;
		}

		public void Abs() {
			int value = data[index - 1];
			data[index - 1] = value < 0 ? -value : value;
		}

		public void Neg() {
			data[index - 1] *= -1;
		}

		public void Floor() {
			data[index - 1] = (int)(data[index - 1] & 0xFFFFFFC0);
		}

		public void Ceil() {
			int n = data[index - 1];
			if ((n & 0x3F) > 0) {
				data[index - 1] = (int)((n & 0xFFFFFFC0) + 0x40);
			}
		}

		public void Max() {
			int value0 = data[index - 2];
			int value1 = data[index - 1];
			data[index - 2] = value0 > value1 ? value0 : value1;
			index--;
		}

		public void Min() {
			int value0 = data[index - 2];
			int value1 = data[index - 1];
			data[index - 2] = value0 < value1 ? value0 : value1;
			index--;
		}

		public int RoundValue(int f26d6, RoundState roundState) {
			if (roundState == RoundState.Off) {
				// Round Off
				return f26d6;
			}
			switch (roundState) {
				case RoundState.HalfGrid:
					f26d6 &= 0x7FFFFFC0;
					f26d6 |= 0x20;
					break;
				case RoundState.Grid:
					if ((f26d6 & 0x20) > 0) {
						f26d6 &= 0x7FFFFFC0;
						f26d6 += 0x40;
					} else {
						f26d6 &= 0x7FFFFFC0;
					}
					break;
				case RoundState.DoubleGrid:
					f26d6 &= 0x7FFFFFE0;
					break;
				case RoundState.DownToGrid:
					f26d6 &= 0x7FFFFFC0;
					break;
				case RoundState.UpToGrid:
					if ((f26d6 & 0x3F) > 0) {
						f26d6 &= 0x7FFFFFC0;
						f26d6 += 0x40;
					} else {
						f26d6 &= 0x7FFFFFC0;
					}
					break;
			}
			return f26d6;
		}

		public void Round(GraphicsState state) {
			float value = data[index - 1];
			value /= 64;
			Point2D p = new Point2D(value, value);
			float d = p.GetLength(state.projection_vector);
			if (state.round_state == RoundState.Off) {
				// Round Off
				data[index - 1] = (int)(d * 64);
				return;
			}
			//uint f26d6 = (uint)data[index - 1];
			data[index - 1] = RoundValue((int)(d * 64), state.round_state);
		}

		public void Round(GraphicsState state, byte ab) {
			float value = data[index - 1];
			value /= 64;
			Point2D p = new Point2D(value, value);
			float d = p.GetLength(state.projection_vector);
			if (ab == 0) { // Gray
				data[index - 1] = (int)(d * 64);
				return;
			}
			if (ab == 1) { // Black
				d += 0.5f;
			}
			if (ab == 2) { // White
				d -= 0.5f;
			}
			//uint f26d6 = (uint)data[index - 1];
			data[index - 1] = RoundValue((int)(d * 64), state.round_state);
		}

		protected float Sin(float value) {
			return (float)Math.Sin(value);
		}

		protected float Cos(float value) {
			return (float)Math.Cos(value);
		}

		protected float Sqrt(float value) {
			return (float)Math.Sqrt(value);
		}
	}
}
