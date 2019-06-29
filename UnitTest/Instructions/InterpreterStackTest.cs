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
			Assert.AreEqual(0, stack.Count);
			stack.Init(10);
			Assert.AreEqual(10, stack.Count);
			stack.Init(1);
			Assert.AreEqual(1, stack.Count);
			stack.Init(0);
			Assert.AreEqual(0, stack.Count);
		}

		[Test()]
		public void Depth() {
			InterpreterStack stack = new InterpreterStack();
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, stack.Count);
			try {
				stack.Push(1);
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
			stack.Init(10);
			Assert.AreEqual(10, stack.Count);
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
	}
}
