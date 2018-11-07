using System;
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
            
            Console.WriteLine(parser.parse("1")[0].eval());
            Console.WriteLine(parser.parse("\"abc\"")[0].eval());
            Console.WriteLine(parser.parse("'1")[0].eval());
            Console.WriteLine(parser.parse("'\"abc\"")[0].eval());
            Console.WriteLine(parser.parse("'a")[0].eval());
        }

        private SexpsParser parser;
    }
}