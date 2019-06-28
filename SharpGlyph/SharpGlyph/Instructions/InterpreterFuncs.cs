using System;
using System.Collections.Generic;

namespace SharpGlyph {
	public class InterpreterFuncs {
		protected Dictionary<int, byte[]> funcs;
		protected Dictionary<int, byte[]> insts;

		public InterpreterFuncs() {
			funcs = new Dictionary<int, byte[]>();
			insts = new Dictionary<int, byte[]>();
		}

		public int GetFuncCount() {
			return funcs.Count;
		}

		public int GetInstCount() {
			return insts.Count;
		}

		public void FDEF(int index, byte[] data) {
			if (funcs.ContainsKey(index)) {
				funcs[index] = data;
				return;
			}
			funcs.Add(index, data);
		}

		public byte[] CALL(int index) {
			if (funcs.ContainsKey(index)) {
				//Console.WriteLine("##### Call: " + index);
				//Console.WriteLine("Decode:\n{0}", Interpreter.Decode(funcs[index]));
				return funcs[index];
			}
			throw new InvalidOperationException(
				"Call invalid function: " + index
			);
			//return null;
		}

		public void IDEF(int index, byte[] data) {
			if (insts.ContainsKey(index)) {
				insts[index] = data;
				return;
			}
			insts.Add(index, data);
		}

		public byte[] ICALL(int index) {
			if (insts.ContainsKey(index)) {
				return insts[index];
			}
			return null;
		}
	}
}
