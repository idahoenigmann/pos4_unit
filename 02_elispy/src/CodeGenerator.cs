using System;
using System.IO;
using i15013.elispy;
using i15013.lexer;

namespace i15013.transpiler {
    public interface CodeGenerator {
		void generateCode(StreamWriter sw, SexpsParser parser, string source);
    }
    
    public class TranspilerException : Exception {
        public TranspilerException() : base() { }
        public TranspilerException(string message) : base(message) { }

        public TranspilerException(string message, Exception inner) : base(
            message, inner) { }
    }
}