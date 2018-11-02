using System;
using System.Collections.Generic;
using i15013.lexer;
using System.Text.RegularExpressions;

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
            this.source = source;
            return program();
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

        private char getSym() {
            return source[idx];
        }

        private bool checkSym(char c) {
            removeWhitespace();

            if (getSym() == c) {
                idx++;
                col++;
                return true;
            }

            return false;
        }

        private void removeWhitespace() {
            while (getSym() == ' ' || getSym() == '\n') {    //ignore whitespace
                idx++;
                col++;
                if (getSym() == '\n') {
                    line++;
                    col = 0;
                }
            }
        }

        private List<Sexp> program() {
            List<Sexp> list = new List<Sexp>();
            while (idx < source.Length) {    //while there still are symbols to check
                list.Add(sexp());
            }
            return list;
        }

        private Sexp sexp() {
            if (checkSym('\'')) {
                Sexp s = sexp();
                s.is_quoted = true;
                return s;
            }
            if (checkSym('(')) {
                Sexp sexp = list();
                return sexp;
            }
            return atom();
        }

        private SexpAtom atom() {
            removeWhitespace();
            
            Match match = new Regex(@"\d+", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpInteger sexpInteger = new SexpInteger(Int32.Parse(source.Substring(idx, match.Length)), new Position(idx, line, col));
                idx += match.Length;
                col += match.Length;
                return sexpInteger;
            }
            match = new Regex(@"([\w-[0-9]]\w*)|[+\*-]|<=|==|>=|<|>", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpSymbol sexpSymbol = new SexpSymbol(source.Substring(idx, match.Length), new Position(idx, line, col));
                idx += match.Length;
                col += match.Length;
                return sexpSymbol;
            }
            match = new Regex(@""".*""", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpString sexpString = new SexpString(source.Substring(idx+1, match.Length-2), new Position(idx, line, col));
                idx += match.Length;
                col += match.Length;
                return sexpString;
            }
            throw new ParserException($"Unrecognized symbol '{getSym()}' " +
                               $"at (index={idx}, line={line}, column={col})");
        }

        private SexpList list() {
            SexpList sexpList = new SexpList(new Position(idx, line, col));
            
            if (idx >= source.Length) {
                throw new ParserException($"Opening '(' but EOF at (index={idx}, line={line}, column={col})");
            }
            
            while (!checkSym(')')) {
                
                sexpList.add_term(sexp());
                
                if (idx >= source.Length) {
                    throw new ParserException($"Opening '(' but EOF at (index={idx}, line={line}, column={col})");
                }
            }
            return sexpList;
        }
        
        private ILexer lexer;
        private Context ctx;
        private string source;
        private int idx = 0;
        private int line = 0;
        private int col = 0; 
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}