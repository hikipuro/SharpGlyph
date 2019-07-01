using System;
using NUnit.Framework;
using SharpGlyph;

namespace UnitTest.Instructions {
	[TestFixture()]
	public class InterpreterStackTest {
		[Test()]
		public void Constructor() {
			InterpreterStack stack = new InterpreterStack();
		}

		[Test()]
		public void Init() {
			InterpreterStack stack = new InterpreterStack();
			Assert.AreEqual(0, stack.Length);
			stack.Init(10);
			Assert.AreEqual(10, stack.Length);
			stack.Push(1);
			stack.Init(1);
			Assert.AreEqual(1, stack.Length);
			try {
				stack.Pop();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Init(0);
			Assert.AreEqual(0, stack.Length);
			try {
				stack.Push(1);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
		}

		[Test()]
		public void Depth() {
			InterpreterStack stack = new InterpreterStack();
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, stack.Length);
			try {
				stack.Push(1);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Init(10);
			Assert.AreEqual(10, stack.Length);
			stack.Push(1);
			Assert.AreEqual(1, stack.Depth);
			stack.Push(2);
			Assert.AreEqual(2, stack.Depth);
			stack.Pop();
			Assert.AreEqual(1, stack.Depth);
			stack.Pop();
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Push() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(1);
			stack.Push((byte)10);
			value = stack.Pop();
			Assert.AreEqual(10, value);
			stack.Push((ushort)15);
			value = stack.Pop();
			Assert.AreEqual(15, value);
			stack.Push(20);
			value = stack.Pop();
			Assert.AreEqual(20, value);
			stack.Push(true);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(false);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push((ushort)0xFFFF);
			value = stack.Pop();
			Assert.AreEqual(-1, value);
			stack.Push(1);
			try {
				stack.Push(1);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
		}

		[Test()]
		public void PushBytes() {
			InterpreterStack stack = new InterpreterStack();
			InterpreterStream stream = new InterpreterStream();
			stream.Push(new byte[] { 1, 2, 3, 4, 5 });
			int value;
			stack.Init(10);
			stack.PushBytes(stream, 3);
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(3, value);
			value = stack.Pop();
			Assert.AreEqual(2, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void PushWords() {
			InterpreterStack stack = new InterpreterStack();
			InterpreterStream stream = new InterpreterStream();
			stream.Push(new byte[] { 0, 1, 0, 2, 3, 0, 0xFF, 0xFF });
			int value;
			stack.Init(10);
			stack.PushWords(stream, 4);
			Assert.AreEqual(4, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(-1, value);
			value = stack.Pop();
			Assert.AreEqual(0x300, value);
			value = stack.Pop();
			Assert.AreEqual(2, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void PushVector() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.PushVector(0);
			Assert.AreEqual(2, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			value = stack.Pop();
			Assert.AreEqual(0x4000, value);
			Assert.AreEqual(0, stack.Depth);
			stack.PushVector((float)(Math.PI / 2));
			value = stack.Pop();
			Assert.AreEqual(0x4000, value);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.PushVector((float)(Math.PI));
			value = stack.Pop();
			Assert.AreEqual(0, value);
			value = stack.Pop();
			Assert.AreEqual(-0x4000, value);
			stack.PushVector((float)(Math.PI * 1.5));
			value = stack.Pop();
			Assert.AreEqual(-0x4000, value);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.PushVector((float)(Math.PI * 2));
			value = stack.Pop();
			Assert.AreEqual(0, value);
			value = stack.Pop();
			Assert.AreEqual(0x4000, value);
			stack.PushVector((float)(Math.PI / 4));
			value = stack.Pop();
			Assert.AreEqual(0x2D41, value);
			value = stack.Pop();
			Assert.AreEqual(0x2D41, value);
		}

		[Test()]
		public void Pop() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				value = stack.Pop();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			try {
				value = stack.Pop();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void PopF2Dot14() {
			InterpreterStack stack = new InterpreterStack();
			float value;
			stack.Init(10);
			try {
				value = stack.PopF2Dot14();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x4000);
			value = stack.PopF2Dot14();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x7000);
			value = stack.PopF2Dot14();
			Assert.AreEqual(1.75f, value);
			stack.Push(0xF000);
			value = stack.PopF2Dot14();
			Assert.AreEqual(3.75f, value);
			stack.Push(0xF800);
			value = stack.PopF2Dot14();
			Assert.AreEqual(3.875f, value);
			stack.Push(0xFC00);
			value = stack.PopF2Dot14();
			Assert.AreEqual(3.9375f, value);
			stack.Push(0xFE00);
			value = stack.PopF2Dot14();
			Assert.AreEqual(3.96875f, value);
			stack.Push(0xFF00);
			value = stack.PopF2Dot14();
			Assert.AreEqual(3.984375f, value);
			try {
				value = stack.PopF2Dot14();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void PopF26Dot6() {
			InterpreterStack stack = new InterpreterStack();
			float value;
			stack.Init(10);
			try {
				value = stack.PopF26Dot6();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x40);
			value = stack.PopF26Dot6();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x70);
			value = stack.PopF26Dot6();
			Assert.AreEqual(1.75f, value);
			stack.Push(0xF0);
			value = stack.PopF26Dot6();
			Assert.AreEqual(3.75f, value);
			stack.Push(0xF8);
			value = stack.PopF26Dot6();
			Assert.AreEqual(3.875f, value);
			stack.Push(0xFC);
			value = stack.PopF26Dot6();
			Assert.AreEqual(3.9375f, value);
			stack.Push(0xFE);
			value = stack.PopF26Dot6();
			Assert.AreEqual(3.96875f, value);
			stack.Push(0xFF);
			value = stack.PopF26Dot6();
			Assert.AreEqual(3.984375f, value);
			try {
				value = stack.PopF26Dot6();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void PopVector() {
			InterpreterStack stack = new InterpreterStack();
			float value;
			stack.Init(10);
			try {
				value = stack.PopVector();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1 * 0x4000);
			try {
				value = stack.PopVector();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(1, stack.Depth);
			stack.Push(1 * 0x4000);
			value = stack.PopVector();
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 4), value);
			stack.Push(1 * 0x4000);
			stack.Push(-1 * 0x4000);
			value = stack.PopVector();
			Assert.AreEqual((float)(-Math.PI / 4), value);
		}

		[Test()]
		public void Peek() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				value = stack.Peek();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			value = stack.Peek();
			Assert.AreEqual(1, value);
			value = stack.Peek();
			Assert.AreEqual(1, value);
			Assert.AreEqual(1, stack.Depth);
		}

		[Test()]
		public void Reset() {
			InterpreterStack stack = new InterpreterStack();
			stack.Init(10);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			Assert.AreEqual(1, stack.Depth);
			stack.Reset();
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(10, stack.Length);
			try {
				stack.Pop();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
		}

		[Test()]
		public void Dup() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(2, stack.Depth);
			stack.Dup();
			Assert.AreEqual(3, stack.Depth);
			stack.Dup();
			Assert.AreEqual(4, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(123, value);
		}

		[Test()]
		public void Swap() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(3, stack.Depth);
			stack.Swap();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
		}

		[Test()]
		public void PushDepth() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.PushDepth();
			Assert.AreEqual(1, stack.Depth);
			stack.Push(12);
			stack.PushDepth();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(2, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void PushCopy() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.Push(1);
			stack.Push(12);
			stack.Push(123);
			stack.PushCopy(1);
			Assert.AreEqual(4, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			stack.PushCopy(2);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			stack.PushCopy(3);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			try {
				stack.PushCopy(1);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void MoveToTop() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.Push(1);
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.MoveToTop(3);
			Assert.AreEqual(4, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Roll() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			stack.Push(1);
			try {
				stack.Roll();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.Roll();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Roll();
			Assert.AreEqual(4, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void LT() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.LT();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.LT();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.LT();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.LT();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.LT();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void LTEQ() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.LTEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.LTEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.LTEQ();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.LTEQ();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.LTEQ();
			value = stack.Pop();
			Assert.AreEqual(1, value);
		}

		[Test()]
		public void GT() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.GT();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.GT();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.GT();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.GT();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.GT();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void GTEQ() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.GTEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.GTEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.GTEQ();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.GTEQ();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.GTEQ();
			value = stack.Pop();
			Assert.AreEqual(1, value);
		}

		[Test()]
		public void EQ() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.EQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.EQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.EQ();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.EQ();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.EQ();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(0);
			stack.Push(0);
			stack.EQ();
			value = stack.Pop();
			Assert.AreEqual(1, value);
		}

		[Test()]
		public void NEQ() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.NEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.NEQ();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.NEQ();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.NEQ();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(123);
			stack.Push(123);
			stack.NEQ();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(0);
			stack.Push(0);
			stack.NEQ();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void And() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.And();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.And();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.And();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.And();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			stack.Push(1);
			stack.And();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(0);
			stack.Push(0);
			stack.And();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(0);
			stack.Push(1);
			stack.And();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(1);
			stack.Push(0);
			stack.And();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void Or() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Or();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			try {
				stack.Or();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Or();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Or();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			stack.Push(1);
			stack.Or();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(0);
			stack.Push(0);
			stack.Or();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(0);
			stack.Push(1);
			stack.Or();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(1);
			stack.Push(0);
			stack.Or();
			value = stack.Pop();
			Assert.AreEqual(1, value);
		}

		[Test()]
		public void Not() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Not();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			Assert.AreEqual(1, stack.Depth);
			stack.Not();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(0);
			stack.Not();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(123);
			stack.Not();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(-123);
			stack.Not();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Odd() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Odd(RoundState.Off);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1 * 64);
			stack.Odd(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(2 * 64);
			stack.Odd(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(1 * 32);
			stack.Odd(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			// TODO: add another roundState test
		}

		[Test()]
		public void Even() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Even(RoundState.Off);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1 * 64);
			stack.Even(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(2 * 64);
			stack.Even(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(1 * 32);
			stack.Even(RoundState.Off);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			// TODO: add another roundState test
		}

		[Test()]
		public void Add() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Add();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Add();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Add();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1357, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Sub() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Sub();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Sub();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Sub();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(123 - 1234, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Mul() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Mul();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Mul();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Mul();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(123 * 1234 / 64, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Div() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Div();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Div();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Div();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual((123 * 64) / 1234, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Abs() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Abs();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			Assert.AreEqual(1, stack.Depth);
			stack.Abs();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(-1);
			stack.Abs();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(0);
			stack.Abs();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void Neg() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Neg();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			Assert.AreEqual(1, stack.Depth);
			stack.Neg();
			Assert.AreEqual(1, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(-1, value);
			stack.Push(-1);
			stack.Neg();
			value = stack.Pop();
			Assert.AreEqual(1, value);
			stack.Push(0);
			stack.Neg();
			value = stack.Pop();
			Assert.AreEqual(0, value);
		}

		[Test()]
		public void Floor() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Floor();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			stack.Floor();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(0);
			stack.Floor();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(3 << 5);
			stack.Floor();
			value = stack.Pop();
			Assert.AreEqual(1 << 6, value);
			stack.Push(0x7F);
			stack.Floor();
			value = stack.Pop();
			Assert.AreEqual(1 << 6, value);
			stack.Push(0xFF);
			stack.Floor();
			value = stack.Pop();
			Assert.AreEqual(3 << 6, value);
		}

		[Test()]
		public void Ceil() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Ceil();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			Assert.AreEqual(0, stack.Depth);
			stack.Push(1);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(1 << 6, value);
			stack.Push(0);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(0, value);
			stack.Push(3 << 5);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(2 << 6, value);
			stack.Push(0x7F);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(2 << 6, value);
			stack.Push(0xFF);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(4 << 6, value);
			stack.Push(0x41);
			stack.Ceil();
			value = stack.Pop();
			Assert.AreEqual(2 << 6, value);
		}

		[Test()]
		public void Max() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Max();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Max();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Max();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(1234, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void Min() {
			InterpreterStack stack = new InterpreterStack();
			int value;
			stack.Init(10);
			try {
				stack.Min();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(1);
			try {
				stack.Min();
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Push(12);
			stack.Push(123);
			stack.Push(1234);
			Assert.AreEqual(4, stack.Depth);
			stack.Min();
			Assert.AreEqual(3, stack.Depth);
			value = stack.Pop();
			Assert.AreEqual(123, value);
			value = stack.Pop();
			Assert.AreEqual(12, value);
			value = stack.Pop();
			Assert.AreEqual(1, value);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void RoundValue() {
			Assert.Fail();
		}

		[Test()]
		public void Round() {
			Assert.Fail();
		}

	}
}
