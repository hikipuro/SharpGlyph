using System;
namespace SharpGlyph {
	public class InterpreterStack {
		protected int[] data;
		protected int index;

		public int Count {
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
			data[index] = (int)(Cos(value) * 0x4000f);
			data[index + 1] = (int)(Sin(value) * 0x4000f);
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
			index -= 2;
			float x = (float)(data[index] & 0xFFFF) / 0x4000f;
			float y = (float)(data[index + 1] & 0xFFFF) / 0x4000f;
			return (float)Math.Atan2(y, x);
		}

		public int Peek() {
			return data[index - 1];
		}

		public void Clear() {
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
			data[index] = data[index - i];
			index++;
		}

		public void MoveToTop(int i) {
			int item = data[index - i];
			int end = index - 1;
			for (int n = index - i; n < end; n++) {
				data[n] = data[n + 1];
			}
			data[index] = item;
		}

		public void Roll() {
			int a = data[index - 1];
			data[index - 1] = data[index - 3];
			data[index - 3] = data[index - 2];
			data[index - 2] = a;
		}

		public void LT() {
			index--;
			int e2 = data[index];
			int e1 = data[index - 1];
			if (e1 < e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void LTEQ() {
			index--;
			int e2 = data[index];
			int e1 = data[index - 1];
			if (e1 <= e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void GT() {
			index--;
			int e2 = (int)data[index];
			int e1 = (int)data[index - 1];
			if (e1 > e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void GTEQ() {
			index--;
			int e2 = (int)data[index];
			int e1 = (int)data[index - 1];
			if (e1 >= e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void Equal() {
			index--;
			int e1 = data[index];
			int e2 = data[index - 1];
			if (e1 == e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void NotEqual() {
			index--;
			int e1 = data[index];
			int e2 = data[index - 1];
			if (e1 != e2) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void And() {
			index--;
			int e1 = data[index];
			int e2 = data[index - 1];
			if (e1 != 0 && e2 != 0) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void Or() {
			index--;
			int e1 = data[index];
			int e2 = data[index - 1];
			if (e1 != 0 || e2 != 0) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void Not() {
			int e = data[index - 1];
			if (e == 0) {
				data[index - 1] = 1;
			} else {
				data[index - 1] = 0;
			}
		}

		public void Odd(int roundState) {
			int e1 = data[index - 1];
			e1 = RoundValue(e1, roundState);
			data[index - 1] = (e1 & 0x40) > 0 ? 1 : 0;
		}

		public void Even(int roundState) {
			int e1 = data[index - 1];
			e1 = RoundValue(e1, roundState);
			data[index - 1] = (e1 & 0x40) == 0 ? 1 : 0;
		}

		public void Add() {
			index--;
			data[index - 1] += data[index];
		}

		public void Sub() {
			index--;
			data[index - 1] -= data[index];
		}

		public void Mul() {
			index--;
			long n2 = data[index - 1];
			n2 *= data[index];
			data[index - 1] = (int)(n2 >> 6);
		}

		public void Div() {
			index--;
			long n2 = data[index - 1];
			n2 <<= 6;
			data[index - 1] = (int)(n2 / data[index]);
		}

		public void Abs() {
			data[index - 1] = Math.Abs(data[index - 1]);
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
			index--;
			data[index - 1] = Math.Max(data[index - 1], data[index]);
		}

		public void Min() {
			index--;
			data[index - 1] = Math.Min(data[index - 1], data[index]);
		}

		public int RoundValue(int f26d6, int roundState) {
			if (roundState == 5) {
				// Round Off
				return f26d6;
			}
			switch (roundState) {
				case 0: // Round To Half Grid
					f26d6 &= 0x7FFFFFC0;
					f26d6 |= 0x20;
					break;
				case 1: // Round To Grid
					if ((f26d6 & 0x20) > 0) {
						f26d6 &= 0x7FFFFFC0;
						f26d6 += 0x40;
					} else {
						f26d6 &= 0x7FFFFFC0;
					}
					break;
				case 2: // Round To Double Grid
					f26d6 &= 0x7FFFFFE0;
					break;
				case 3: // Round Down To Grid
					f26d6 &= 0x7FFFFFC0;
					break;
				case 4: // Round Up To Grid
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
			float x = value * Cos(state.projection_vector);
			float y = value * Sin(state.projection_vector);
			float sign = x < 0 || y < 0 ? -1 : 1;
			value = sign * Sqrt(x * x + y * y);
			if (state.round_state == 5) {
				// Round Off
				data[index - 1] = (int)(value * 64);
				return;
			}
			//uint f26d6 = (uint)data[index - 1];
			data[index - 1] = RoundValue((int)(value * 64), state.round_state);
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
