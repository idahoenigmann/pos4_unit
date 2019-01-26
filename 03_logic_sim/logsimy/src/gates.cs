using System;
using i15013.logsimy.variables;
using i15013.logics.propositional;

namespace i15013.logsimy.gates.propositional {
    public class AndGate : Observer {
        public string name { get; }
        public Variable i0;
        public Variable i1;
        public Variable o;

        public AndGate(string name) {
            this.name = name;
            
            i0 = new Variable(false, name + ".i0");
            i1 = new Variable(false, name + ".i1");
            o = new Variable(false, name + ".o");
            
            Utilities.inform(i0, this);
            Utilities.inform(i1, this);
        }

        public void update(Variable v, NotificationReason r) {
            if (r != NotificationReason.reset) {
                o.value = Operators.conj(i0.value, i1.value);
            }
        }

        public void reset(Variable v, NotificationReason r) {
            o.value = false;
        }
    }
}