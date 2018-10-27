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

        private List<Sexp> program() {
            List<Sexp> list = new List<Sexp>();
            while (idx < source.Length) {
                list.Add(sexp());
            }
            return list;
        }

        private Sexp sexp() {
            while (source[idx] == ' ' || source[idx] == '\n') {
                idx++;
            }
            
            if (source[idx] == '\'') {
                idx++;
                return sexp();
            }
            if (source[idx] == '(') {
                idx++;
                Sexp sexp = list();
                if (source[idx] == ')') {
                    idx++;
                } else {
                    throw new ParserException();
                }

                return sexp;
            }

            return atom();
        }

        private SexpAtom atom() {
            while (source[idx] == ' ' || source[idx] == '\n') {
                idx++;
            }
            
            Match match = new Regex(@"\d+", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpInteger sexpInteger = new SexpInteger(Int32.Parse(source.Substring(idx, match.Length)));
                idx += match.Length;
                return sexpInteger;
            }
            match = new Regex(@"([\w-[0-9]]\w*)|[+\*-]|<=|==|>=|<|>", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpSymbol sexpSymbol = new SexpSymbol(source.Substring(idx, match.Length), null);
                idx += match.Length;
                return sexpSymbol;
            }
            match = new Regex(@""".*""", RegexOptions.Compiled).Match(source, idx);
            if (match.Success && match.Index == idx) {
                SexpString sexpString = new SexpString(source.Substring(idx, match.Length));
                idx += match.Length;
                return sexpString;
            }
            throw new ParserException();
        }

        private SexpList list() {
            SexpList sexpList = new SexpList(null);
            while (source[idx] != ')') {
                while (source[idx] == ' ' || source[idx] == '\n') {
                    idx++;
                }
                
                if (source[idx] == '\'') {
                    idx++;
                    sexpList.add_term(sexp());
                }

                if (source[idx] == '(') {
                    idx++;
                    Sexp sexp = list();
                    if (source[idx] == ')') {
                        idx++;
                    }
                    else {
                        throw new ParserException();
                    }

                    sexpList.add_term(sexp);
                }

                sexpList.add_term(atom());
            }

            return sexpList;
        }
        
        private ILexer lexer;
        private Context ctx;
        private string source;
        private int idx = 0;
    }
    
    public class ParserException : Exception {
        public ParserException() : base() { }
        public ParserException(string message) : base(message) { }

        public ParserException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}