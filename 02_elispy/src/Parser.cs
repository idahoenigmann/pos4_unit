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
    
    public class SexpsParser : IParser{
        public SexpsParser(ILexer lexer, Context ctx) {
            this.lexer = lexer;
            this.ctx = ctx;
        }

        public List<Sexp> parse(string source) {
            foreach (Token token in lexer.tokenize(source)) {
                Console.WriteLine(token);
            }
            return new List<Sexp>();
        }

        public void test() {
            Console.WriteLine(new SexpSymbol("a", null).ToString());
            Console.WriteLine(new SexpInteger(1).ToString());
            Console.WriteLine(new SexpString("abc").ToString());

            Sexp s = new SexpSymbol("a", null);
            s.is_quoted = true;
            Console.WriteLine(s.ToString());
            
            Console.WriteLine(new SexpList(new List<Sexp>(), null).ToString());

            List<Sexp> list = new List<Sexp>();
            list.Add(new SexpInteger(1));
            list.Add(new SexpString("abc"));
            
            SexpList sexpList = new SexpList(list, null);
            sexpList.is_quoted = true;
            
            Console.WriteLine(sexpList.ToString());
        }
        
        private ILexer lexer;
        private Context ctx;
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}