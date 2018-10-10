using System;
using i15013.lexer;

namespace i15013.elispy {
    public class SexpsLexer : Lexer {
        public SexpsLexer() {
            add_definition(new Definition("LPAREN", @"\(", false));
            add_definition(new Definition("RPAREN", @"\)", false));
            add_definition(new Definition("SYMBOL", @"([\w-[0-9]]\w*)|[+\*-]|[<]|[<=]|[==]|[>]|[>=]", false));
            add_definition(new Definition("INTEGER", @"\d+", false));
            add_definition(new Definition("STRING", @""".*""", false));
            add_definition(new Definition("SPACE", @"\t|\r|\v|\n|\s", true));
            add_definition(new Definition("QUOTE", @"'", false));
        }
    }
}