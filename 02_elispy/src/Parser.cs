using System;
using System.Collections.Generic;
using i15013.lexer;

namespace i15013.elispy {
    public interface IParser {
        List<Sexp> parse(string source);
    }

    /*
     * program -> sexp | sexp program
     * sexp -> atom | 'sexp | ( list )
     * list -> sexp list | e
     * atom -> INTEGER | STRING | SYMBOL
     */
    
    public class SexpsParser : IParser{
        public SexpsParser(ILexer lexer, Context ctx) {
            this.lexer = lexer;
            this.ctx = ctx;
        }

        public List<Sexp> parse(string source) {
            return program(source);
        }

        public static void test() {
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
            
            Console.WriteLine("----------------------------");
            
            SexpsParser sexpsParser = new SexpsParser(new SexpsLexer(), new Context());
            
            foreach (Sexp sexp in sexpsParser.parse("(+ 1 2 name \"abc def\")\n" +
                                                    "'(+ 1 2)\n" +
                                                    "( + 1 2 )")) {
                Console.WriteLine(sexp);
            }
            
            /*foreach (Sexp sexp in sexpsParser.parse("'(+ 1 2)")) {
                Console.WriteLine(sexp);
            }
            
            foreach (Sexp sexp in sexpsParser.parse("( + 1 2 )")) {
                Console.WriteLine(sexp);
            }*/
            
            
        }

        private Token getSym() {
            Token token = tokens[0];
            tokens.RemoveAt(0);
            return token;
        }

        private List<Sexp> program(string source) {
            List<Sexp> list = new List<Sexp>();
            
            foreach (Token token in lexer.tokenize(source)) {
                tokens.Add(token);
                
            }

            while (tokens.Count > 0) {
                list.Add(sexp(getSym()));
                Console.WriteLine("----------------------------");
            }
            
            return list;
        }

        private Sexp sexp(Token token) {
            Console.WriteLine(token);
            
            if (token.type == Tokens.QUOTE) {
                Sexp s = sexp(getSym());
                s.is_quoted = true;
                Console.WriteLine("return quoted sexp");
                return s;
            }
            if (token.type == Tokens.LPAREN) {
                Sexp sexp = list(getSym());
                Console.WriteLine("return list");
                return sexp;
            }
            Console.WriteLine("return atom");
            return atom(token);
        }

        private SexpAtom atom(Token token) {
            Console.WriteLine(token);
            
            if (token.type == Tokens.INTEGER) {
                Console.WriteLine("return Integer");
                return new SexpInteger(Int32.Parse(token.value), token.position);
            }
            
            if (token.type == Tokens.SYMBOL) {
                Console.WriteLine("return Symbol");
                return new SexpSymbol(token.value, token.position);
            }
            
            if (token.type == Tokens.STRING) {
                Console.WriteLine("return String");
                return new SexpString(token.value, token.position);
            }
            throw new ParserException($"Unrecognized symbol '{token.value}' " +
                               $"at (index={token.position.idx}, " +
                                      $"line={token.position.line_number}, " +
                                      $"column={token.position.column_number})");
        }

        private SexpList list(Token token) {
            Console.WriteLine(token);
            
            SexpList sexpList = new SexpList(token.position);
            
            while (token.type != Tokens.RPAREN) {
                sexpList.add_term(sexp(token));
                token = getSym();

            }
            Console.WriteLine("return list");
            return sexpList;
        }
        
        private ILexer lexer;
        private Context ctx;
        private List<Token> tokens = new List<Token>();
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}