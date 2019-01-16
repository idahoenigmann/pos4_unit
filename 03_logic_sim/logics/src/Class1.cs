using System;

namespace i15013.logics.propositional {
    public static class Operators {
        public static bool conj(bool a, bool b) => a & b;
        public static bool disj(bool a, bool b) => a | b;
        public static bool neg(bool a) => !a;
        public static bool anti(bool a, bool b) => a ^ b;
        public static bool imp(bool a, bool b) => !a | b;
        public static bool equiv(bool a, bool b) => a == b;
    }
}
