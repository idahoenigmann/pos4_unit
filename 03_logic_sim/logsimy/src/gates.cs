using System;
using i15013.logsimy.variables;
using i15013.logics.propositional;

namespace i15013.logsimy.gates.propositional {
    public class AndGate : Observer {
        public string name { get; }
        public Variable i0 = new Variable(false, "AndGate.i0");
        public Variable i1 = new Variable(false, "AndGate.i1");
        public Variable o = new Variable(false, "AndGate.o");

        public AndGate(string name) {
            this.name = name;
        }

        public void update(Variable v, NotificationReason r) {
            if (r != NotificationReason.reset) {
                o.value = Operators.conj(i0.value, i1.value);
            }
        }
    }
}