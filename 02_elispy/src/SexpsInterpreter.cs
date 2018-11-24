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

            Context context = new Context {
                symtab = {["a"] = new SexpInteger(1, new Position(0, 0, 0))}
            };

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

        public void test_repl() {
            
            Console.WriteLine(@"   _______        _     _____            _ " + "\n" +
                              @"  |__   __|      | |   |  __ \          | |" + "\n" +
                              @"     | | ___  ___| |_  | |__) |___ _ __ | |" + "\n" +
                              @"     | |/ _ \/ __| __| |  _  // _ \ '_ \| |" + "\n" +
                              @"     | |  __/\__ \ |_  | | \ \  __/ |_) | |" + "\n" +
                              @"     |_|\___||___/\__| |_|  \_\___| .__/|_|" + "\n" +
                              @"                                  | |      " + "\n" +
                              @"                                  |_|      ");
            
            Context context = new Context();

            string[] inputs =
                {"(+)", "(+ 1)", "(+ 1 2)", "(+ 1 2 3)", "(+ 1 (+ 2 3))"};
            int[] i_res = {0, 1, 3, 6, 6};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != i_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(-)", "(- 1)", "(- 5 3)", "(- 51 4 3 2)"};
            i_res = new []{0, -1, 2, 42};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != i_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(*)", "(* 4)", "(* 4 3)", "(* 7 3 2)"};
            i_res = new []{1, 4, 12, 42};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != i_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }

            inputs = new []{"(/ 2)", "(/ 6 3)", "(/ 252 3 2)"};
            i_res = new []{0, 2, 42};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != i_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(< 3 2)", "(< 2 3)", "(< 3 3)"};
            string[] s_res = {"nil", "t", "nil"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(<= 3 2)", "(<= 2 3)", "(<= 3 3)"};
            s_res = new []{"nil", "t", "t"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(= 3 2)", "(= 2 3)", "(= 3 3)"};
            s_res = new []{"nil", "nil", "t"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(>= 3 2)", "(>= 2 3)", "(>= 3 3)"};
            s_res = new []{"t", "nil", "t"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(> 3 2)", "(> 2 3)", "(> 3 3)"};
            s_res = new []{"t", "nil", "nil"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context) != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(first '(1 2 3))", "(first ())", "(first nil)"};
            s_res = new []{"1", "nil", "nil"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }

            inputs = new []{"(rest ())", "(rest '(1))", "(rest '(1 2 3))"};
            s_res = new []{"nil", "nil", "(2 3)"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(cons 1 ())", "(cons 1 '(2 3))", "(cons '(1 2) '(3 4))"};
            s_res = new []{"(1)", "(1 2 3)", "((1 2) 3 4)"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(equal 1 1)", "(equal 1 ())"};
            s_res = new []{"t", "nil"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(null 1)", "(null nil)"};
            s_res = new []{"nil", "t"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(progn)", "(progn 1)", "(progn 1 (+ 1 1) (- 43 1))"};
            s_res = new []{"nil", "1", "42"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(princ 1)", "(princ \"abc\")", "(princ 'a)", "(setq a 42)", "(princ a)", "(princ '(1 2 3))"};
            s_res = new []{"1", "\"abc\"", "a", "42", "42", "(1 2 3)"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(and)", "(and t)", "(and nil)", "(and 1 2 3)", "(and 1 nil a)"};
            s_res = new []{"t", "t", "nil", "3", "nil"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
            }
            
            inputs = new []{"(or)", "(or 1)", "(or 1 2 3)", "(or nil nil 3 a)"};
            s_res = new []{"nil", "1", "1", "3"};
            for (int i=0; i < inputs.Length; i++) {
                if (parser.parse(inputs[i])[0].eval(context).ToString() != s_res[i]) {
                    Console.WriteLine(inputs[i]);
                }
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
                sexp.eval(context);
            }
        }
        
        private SexpsParser parser;
    }
    
    public class InterpreterException : Exception {
        public InterpreterException() : base() { }
        public InterpreterException(string message) : base(message) { }

        public InterpreterException(string message, Exception inner) : base(
            message, inner) { }
    }
}