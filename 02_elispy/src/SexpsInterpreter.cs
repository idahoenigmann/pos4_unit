using System;
using System.Linq;
using i15013.lexer;

namespace i15013.elispy {
    public class SexpsInterpreter {
        public SexpsInterpreter(SexpsParser parser) {
            this.parser = parser;
        }

        public void test() {
            Console.WriteLine(@" _______        _     _____       _                           _            " + "\n" +
                              @"|__   __|      | |   |_   _|     | |                         | |           " + "\n" +
                              @"   | | ___  ___| |_    | |  _ __ | |_ ___ _ __ _ __  _ __ ___| |_ ___ _ __ " + "\n" +
                              @"   | |/ _ \/ __| __|   | | | '_ \| __/ _ \ '__| '_ \| '__/ _ \ __/ _ \ '__|" + "\n" +
                              @"   | |  __/\__ \ |_   _| |_| | | | ||  __/ |  | |_) | | |  __/ ||  __/ |   " + "\n" +
                              @"   |_|\___||___/\__| |_____|_| |_|\__\___|_|  | .__/|_|  \___|\__\___|_|   " + "\n" +
                              @"                                              | |                          " + "\n" +
                              @"                                              |_|                          ");
            
            Context context = new Context();

            context.symtab["a"] = new SexpInteger(1, new Position(0, 0, 0));

            foreach (var x in context.symtab) {
                Console.WriteLine(x);
            }
            
            Console.WriteLine(parser.parse("1")[0].eval(context));
            Console.WriteLine(parser.parse("\"abc\"")[0].eval(context));
            Console.WriteLine(parser.parse("'1")[0].eval(context));
            Console.WriteLine(parser.parse("'\"abc\"")[0].eval(context));
            Console.WriteLine(parser.parse("'a")[0].eval(context));
            Console.WriteLine(parser.parse("a")[0].eval(context));
            Console.WriteLine(parser.parse("'(1 2 3)")[0].eval(context));
            Console.WriteLine(parser.parse("nil")[0].eval(context));
            try {
                Console.WriteLine(parser.parse("(1 2 3)")[0].eval(context));
            }
            catch (InterpreterException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(parser.parse("(+)")[0].eval(context));
            Console.WriteLine(parser.parse("(+ 1)")[0].eval(context));
            Console.WriteLine(parser.parse("(+ 1 2)")[0].eval(context));
            Console.WriteLine(parser.parse("(+ 1 2 3)")[0].eval(context));
            Console.WriteLine(parser.parse("(+ 1 (+ 2 3))")[0].eval(context));
            try {
                Console.WriteLine(parser.parse("(+ 1 \"a\")")[0]
                    .eval(context));
            }
            catch (InterpreterException e) {
                Console.WriteLine(e.Message + " ---> " + e.InnerException.Message);
            }

        }

        public void repl() {
            Context context = new Context();
            
            while (true) {
                Console.Write("elispy> ");
                string input = Console.ReadLine();
                if (input is null) {    //input stream stopped (e.g. ctrl-d)
                    break;
                }

                input = input.Trim();    //remove unnecessary chars from input
                input = new string(input.Where(c => !char.IsControl(c)).ToArray());

                if (input == "") {    //empty string does not need to be interpreted
                    continue;
                }
                
                try {
                    foreach (Sexp sexp in parser.parse(input)) {
                        Console.WriteLine(sexp.eval(context));
                    }
                }
                catch (InterpreterException e) {
                    Console.WriteLine(e.Message);
                }
                catch (ParserException e) {
                    Console.WriteLine(e.Message);
                }
                catch (LexerException e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void repl_stdin_file(string input) {
            Context context = new Context();
            foreach (Sexp sexp in parser.parse(input)) {
                Console.WriteLine(sexp.eval(context));
            }
        }
        
        private SexpsParser parser;
    }
    
    public class InterpreterException : Exception {
        public InterpreterException() : base() { }
        public InterpreterException(string message) : base(message) { }

        public InterpreterException(string message, System.Exception inner) : base(
            message, inner) { }
    }
}