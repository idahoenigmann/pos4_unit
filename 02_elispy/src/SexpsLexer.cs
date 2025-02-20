using System;
using i15013.lexer;

namespace i15013.elispy {
    public class SexpsLexer : Lexer {
        public SexpsLexer() {
            add_definition(new Definition(Tokens.LPAREN, @"\(", false));
            add_definition(new Definition(Tokens.RPAREN, @"\)", false));
            add_definition(new Definition(Tokens.SYMBOL, @"([\w-[0-9]]\w*)|[+/*-]|<=|>=|<|>|=", false));
            add_definition(new Definition(Tokens.INTEGER, @"\d+", false));
            add_definition(new Definition(Tokens.STRING, @""".[^""]*""", false));
            add_definition(new Definition(Tokens.SPACE, @"\t|\r|\v|\n|\s", true));
            add_definition(new Definition(Tokens.QUOTE, @"'", false));
        }

        public void test() {
            Console.WriteLine(@"" +
            @"  _______        _     _                        "+"\n"+
            @" |__   __|      | |   | |                       "+"\n"+         
            @"    | | ___  ___| |_  | |     _____  _____ _ __ "+"\n"+
            @"    | |/ _ \/ __| __| | |    / _ \ \/ / _ \ '__|"+"\n"+
            @"    | |  __/\__ \ |_  | |___|  __/>  <  __/ |   "+"\n"+
            @"    |_|\___||___/\__| |______\___/_/\_\___|_|   "+"\n");
            
            
            foreach (Token token in tokenize("(+ 1 2 \"abc def\")")) {
                Console.WriteLine(token);
            }
            
            Console.WriteLine();
            
            foreach (Token token in tokenize("'(+ 1 2)")) {
                Console.WriteLine(token);
            }
            
            Console.WriteLine();
            
            foreach (Token token in tokenize("(name _n_ame _ < <= == > >=)")) {
                Console.WriteLine(token);
            }

            Console.WriteLine();
            
            foreach (Token token in tokenize("(- 1 \n 2 \r\n 3)")) {
                Console.WriteLine(token);
            }
            
            Console.WriteLine();
            
            foreach (Token token in tokenize("(+ 1 \n   3)")) {
                Console.WriteLine(token);
            }
            
            Console.WriteLine();
            
            foreach (Token token in tokenize("(+ 1 \r\n   3)")) {
                Console.WriteLine(token);
            }
            
            Console.WriteLine();

            try {
                foreach (Token token in tokenize("1.5")) {
                    Console.WriteLine(token);
                }
            }
            catch (LexerException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static class Tokens {
        public const string LPAREN = "LPAREN";
        public const string RPAREN = "RPAREN";
        public const string SYMBOL = "SYMBOL";
        public const string INTEGER = "INTEGER";
        public const string STRING = "STRING";
        public const string SPACE = "SPACE";
        public const string QUOTE = "QUOTE";
    }
}