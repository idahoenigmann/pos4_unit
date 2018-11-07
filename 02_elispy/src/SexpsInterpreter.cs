using System;
using i15013.lexer;

namespace i15013.elispy {
    public class SexpsInterpreter {
        public SexpsInterpreter(SexpsParser parser) {
            this.parser = parser;
        }

        public void test() {
            Console.WriteLine();
        }

        private SexpsParser parser;
    }
}