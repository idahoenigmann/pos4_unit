using System;
using i15013.logsimy.variables;

namespace i15013.logsimy.gates.propositional {
    public class AndGate {
        public string name { get; }
        public Variable i0 = new Variable(false, "AndGate.i0");
        public Variable i1 = new Variable(false, "AndGate.i1");
        public Variable o = new Variable(false, "AndGate.o");

        public AndGate(string name) {
            this.name = name;
        }
    }
}