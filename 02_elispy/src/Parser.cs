using System;
using System.Collections.Generic;
using i15013.lexer;

namespace i15013.elispy {
    public interface IParser {
        List<Sexp> parse(string source);
    }

    /*
     * Atom::= INTEGER | STRING | SYMBOL
     * List::= ( {Sexp} )
     * Sexp::= '? Atom | List | Sexp
     * Program::= {Sexp}
     */
    
    public class SexpsParser {
        public SexpsParser(ILexer lexer) {
            this.lexer = lexer;
        }

        public List<Sexp> parse(string source) {
            foreach (Token token in lexer.tokenize(source)) {
                Console.WriteLine(token);
            }
            return new List<Sexp>();
        }

        private ILexer lexer;
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}