using System;
using NUnit.Framework;
using SharpGlyph;

namespace UnitTest.Instructions {
	[TestFixture]
	public class InterpreterStreamTest {
		[Test]
		public void Constructor() {
			InterpreterStream stream = new InterpreterStream();
		}

		[Test]
		public void Length() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Length);
			stream.Push(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60 });
			Assert.AreEqual(6, stream.Length);
			stream.Push(new byte[] { 0x10, 0x20, 0x30, 0x40 });
			Assert.AreEqual(4, stream.Length);
			stream.Pop();
			Assert.AreEqual(6, stream.Length);
			stream.Push(new byte[] { 0x10, 0x20 });
			Assert.AreEqual(2, stream.Length);
			stream.Pop();
			Assert.AreEqual(6, stream.Length);
			stream.Pop();
			Assert.AreEqual(0, stream.Length);
		}

		[Test]
		public void Position() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Position);
			stream.Push(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60 });
			Assert.AreEqual(0, stream.Position);
			stream.Next();
			Assert.AreEqual(1, stream.Position);
			stream.NextWord();
			Assert.AreEqual(3, stream.Position);
		}

		[Test]
		public void Depth() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Depth);
		}

		[Test]
		public void Clear() {
			InterpreterStream stream = new InterpreterStream();
			stream.Clear();
			Assert.AreEqual(0, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(0, stream.Depth);
		}

		[Test]
		public void Push() {
			InterpreterStream stream = new InterpreterStream();
			stream.Push(new byte[] { 0x10, 0x20 });
			Assert.AreEqual(2, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(1, stream.Depth);
			stream.Push(new byte[] { 0x30, 0x40, 0x50 });
			Assert.AreEqual(3, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(2, stream.Depth);
		}

		[Test]
		public void PushCount() {
			InterpreterStream stream = new InterpreterStream();
			stream.Push(new byte[] { 0x10, 0x20 }, 0);
			Assert.AreEqual(0, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(0, stream.Depth);
			stream.Push(new byte[] { 0x10, 0x20 }, 5);
			Assert.AreEqual(2, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(5, stream.Depth);
		}

		[Test]
		public void Pop() {
			InterpreterStream stream = new InterpreterStream();
			try {
				stream.Pop();
				Assert.Fail();
			} catch (InvalidOperationException) {
			}
			stream.Push(new byte[] { 0x10, 0x20 });
			Assert.AreEqual(2, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(1, stream.Depth);
			stream.Push(new byte[] { 0x30, 0x40, 0x50 });
			Assert.AreEqual(3, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(2, stream.Depth);
			stream.Pop();
			Assert.AreEqual(2, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(1, stream.Depth);
			stream.Pop();
			Assert.AreEqual(0, stream.Length);
			Assert.AreEqual(0, stream.Position);
			Assert.AreEqual(0, stream.Depth);
			try {
				stream.Pop();
				Assert.Fail();
			} catch (InvalidOperationException) {
			}
		}

		[Test]
		public void HasNext() {
			InterpreterStream stream = new InterpreterStream();
			Assert.IsFalse(stream.HasNext());
			stream.Push(new byte[] { 0x10, 0x20 });
			Assert.IsTrue(stream.HasNext());
			stream.Next();
			Assert.IsTrue(stream.HasNext());
			stream.Next();
			Assert.IsFalse(stream.HasNext());
		}

		[Test]
		public void Next() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Next());
			stream.Push(new byte[] { 0x10, 0x20 });
			Assert.AreEqual(0x10, stream.Next());
			Assert.AreEqual(0x20, stream.Next());
			Assert.IsFalse(stream.HasNext());
			Assert.AreEqual(0, stream.Next());
			stream.Pop();
			Assert.IsFalse(stream.HasNext());
			Assert.AreEqual(0, stream.Next());
		}

		[Test]
		public void NextWord() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Next());
			stream.Push(new byte[] { 0x10, 0x20, 0xFF, 0xFF, 0x80, 0x00 });
			Assert.IsTrue(stream.HasNext());
			Assert.AreEqual(0x1020, stream.NextWord());
			Assert.IsTrue(stream.HasNext());
			Assert.AreEqual(-1, stream.NextWord());
			Assert.IsTrue(stream.HasNext());
			Assert.AreEqual(-0x8000, stream.NextWord());
			Assert.IsFalse(stream.HasNext());
			Assert.AreEqual(0, stream.NextWord());
		}

		[Test]
		public void JMPR() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Next());
			stream.Push(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60 });
			stream.JMPR(2);
			Assert.IsTrue(stream.HasNext());
			Assert.AreEqual(0x30, stream.Next());
			Assert.IsTrue(stream.HasNext());
			stream.JMPR(-3);
			Assert.AreEqual(0x10, stream.Next());
		}

		[Test]
		public void JROT() {
			InterpreterStream stream = new InterpreterStream();
			Assert.AreEqual(0, stream.Next());
			stream.Push(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60 });
			stream.JMPR(2);
			Assert.IsTrue(stream.HasNext());
			Assert.AreEqual(0x30, stream.Next());
			Assert.IsTrue(stream.HasNext());
			stream.JMPR(-3);
			Assert.AreEqual(0x10, stream.Next());
		}
	}
}
