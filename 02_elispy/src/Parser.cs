using System;
using System.Collections.Generic;
using i15013.lexer;

namespace i15013.elispy {
    public interface IParser {
        void parse(string source);
    }

    /*
     * Atom::= INTEGER | STRING | SYMBOL
     * List::= ( {Atom} )
     * Sexp::= ' Atom | List | Sexp
     * Program::= {Sexp}
     */
    
    public class SexpsParser {
        public SexpsParser(ILexer lexer) {
            foreach (Token token in lexer.tokenize("(+ 1 2 \"abc def\")")) {
                Console.WriteLine(token);
            }
        }
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}