using System;
using i15013.logsimy.variables;
using i15013.logics.propositional;

namespace i15013.logsimy.gates.propositional {
    public class AndGate : Gate2 {
        public AndGate(string name) : base(name, Operators.conj) {}
    }

    public abstract class Gate2 : Observer {
        public string name { get; }
        public Variable i0;
        public Variable i1;
        public Variable o;
        protected BooleanOperator2 op;
        
        public Gate2(string name, BooleanOperator2 op) {
            this.name = name;
            this.op = op;
            
            i0 = new Variable(false, name + ".i0");
            i1 = new Variable(false, name + ".i1");
            o = new Variable(false, name + ".o");
            
            Utilities.inform(i0, this);
            Utilities.inform(i1, this);
        }
        
        public void reset(Variable v, NotificationReason r) {
            o.value = false;
        }
        
        public void update(Variable v, NotificationReason r) {
            if (r != NotificationReason.reset) {
                o.value = op(i0.value, i1.value);
            }
        }
    }
    
    public abstract class Gate1 : Observer {
        public string name { get; }
        public Variable i;
        public Variable o;
        
        public Gate1(string name, BooleanOperator1 op) {
            this.name = name;
            
            i = new Variable(false, name + ".i");
            o = new Variable(false, name + ".o");
            
            Utilities.inform(i, this);
        }
        
        public void reset(Variable v, NotificationReason r) {
            o.value = false;
        }
        
        public abstract void update(Variable v, NotificationReason r);
    }
    
    public delegate bool BooleanOperator1(bool a);
    public delegate bool BooleanOperator2(bool a, bool b);
}