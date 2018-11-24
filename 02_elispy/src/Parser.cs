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

        public void test() {
            Console.WriteLine("" +
             @" _______        _     _____                         "+"\n"+
             @"|__   __|      | |   |  __ \                        "+"\n"+
             @"   | | ___  ___| |_  | |__) |_ _ _ __ ___  ___ _ __ "+"\n"+
             @"   | |/ _ \/ __| __| |  ___/ _` | '__/ __|/ _ \ '__|"+"\n"+
             @"   | |  __/\__ \ |_  | |  | (_| | |  \__ \  __/ |   "+"\n"+
             @"   |_|\___||___/\__| |_|   \__,_|_|  |___/\___|_|   "+"\n");
            Console.WriteLine("a as SexpSymbol: " + new SexpSymbol("a"));
            Console.WriteLine("1 as SexpInteger: " + new SexpInteger(1));
            Console.WriteLine("abc as SexpString: " + new SexpString("abc"));

            Sexp s = new SexpSymbol("a");
            s.is_quoted = true;
            Console.WriteLine("a as quoted SexpSymbol: " + s);
            
            Console.WriteLine("empty list as SexpList: " + new SexpList(new List<Sexp>(), null));

            List<Sexp> list = new List<Sexp>();
            list.Add(new SexpInteger(1));
            list.Add(new SexpString("abc"));
            
            SexpList sexpList = new SexpList(list, null);
            sexpList.is_quoted = true;
            
            Console.WriteLine("1 and abc as quoted SexpList: " + sexpList);
            
            Console.WriteLine("\n+ 1 2 name \"abc def\")\\n'(+ 1 2)\\n( + 1 2 )\t parsed is:");
            
            foreach (Sexp sexp in parse("(+ 1 2 name \"abc def\")\n" +
                                                    "'(+ 1 2)\n" +
                                                    "( + 1 2 )\n")) {
                Console.WriteLine("\t" + sexp);
            }
        }

        private Token getSym() {
            Token token = tokens[0];
            tokens.RemoveAt(0);
            return token;
        }

        private List<Sexp> program(string source) {
            List<Sexp> list = new List<Sexp>();

            try {
                foreach (Token token in lexer.tokenize(source)) {
                    tokens.Add(token);
                }
            }
            catch (LexerException lexerException) {
                throw new ParserException("Unrecognized symbol", lexerException);
            }

            while (tokens.Count > 0) {
                list.Add(sexp(getSym()));
            }
            
            return list;
        }

        private Sexp sexp(Token token) {
            
            if (token.type == Tokens.QUOTE) {
                Sexp s = sexp(getSym());
                s.is_quoted = true;
                return s;
            }
            if (token.type == Tokens.LPAREN) {
                try {
                    Sexp sexp = list(getSym());
                    return sexp;
                }
                catch (ArgumentOutOfRangeException) {
                    throw new ParserException(
                        $"Opening \'(\' but EOF at (index={token.position.idx}, line={token.position.line_number}, column={token.position.column_number})");
                }
            }
            return atom(token);
        }

        private SexpAtom atom(Token token) {
            
            if (token.type == Tokens.INTEGER) {
                return new SexpInteger(Int32.Parse(token.value), token.position);
            }
            
            if (token.type == Tokens.SYMBOL) {
                return new SexpSymbol(token.value, token.position);
            }
            
            if (token.type == Tokens.STRING) {
                return new SexpString(token.value, token.position);
            }
            throw new ParserException($"Unrecognized symbol '{token.value}' " +
                               $"at (index={token.position.idx}, " +
                                      $"line={token.position.line_number}, " +
                                      $"column={token.position.column_number})");
        }

        private SexpList list(Token token) {
            
            SexpList sexpList = new SexpList(token.position);
            try {
                while (token.type != Tokens.RPAREN) {
                    sexpList.add_term(sexp(token));
                    token = getSym();

                }
            }
            catch (ArgumentOutOfRangeException) {
                throw new ParserException(
                    $"Missing \')\' or EOF at (index={token.position.idx}, line={token.position.line_number}, column={token.position.column_number})");
            }

            return sexpList;
        }
        
        private ILexer lexer;
        private Context ctx;
        private List<Token> tokens = new List<Token>();
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, Exception inner) : base(
            message, inner) { }
    }
}