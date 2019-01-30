using System;
using i15013.logsimy.variables;
using i15013.logics.propositional;

namespace i15013.logsimy.gates.propositional {
    public class AndGate : Gate2 {
        public AndGate(string name) : base(name, Operators.conj) {}
    }
    
    public class OrGate : Gate2 {
        public OrGate(string name) : base(name, Operators.disj) {}
    }
    
    public class XOrGate : Gate2 {
        public XOrGate(string name) : base(name, Operators.anti) {}
    }
    
    public class NegGate : Gate1 {
        public NegGate(string name) : base(name, Operators.neg) {}
    }
    
    public class XNOrGate : Gate2 {
        public XNOrGate(string name) : base(name, Operators.equiv) {}
    }
    
    public class NAndGate : Gate2 {
        public NAndGate(string name) : base(name, (x, y) => Operators.neg(Operators.conj(x, y))) {}
    }
    
    public class NOrGate : Gate2 {
        public NOrGate(string name) : base(name, (x, y) => Operators.neg(Operators.disj(x, y))) {}
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
        
        public void reset(bool v=false) {
            i0.reset(v);
            i1.reset(v);
            o.reset(v);
        }
        
        public void update(Variable v, NotificationReason r) {
            if (r != NotificationReason.reset) {
                o.Value = op(i0.Value, i1.Value);
            }
        }
    }
    
    public abstract class Gate1 : Observer {
        public string name { get; }
        public Variable i;
        public Variable o;
        protected BooleanOperator1 op;
        
        public Gate1(string name, BooleanOperator1 op) {
            this.name = name;
            this.op = op;
            
            i = new Variable(false, name + ".i");
            o = new Variable(false, name + ".o");
            
            Utilities.inform(i, this);
        }
        
        public void reset(bool v=false) {
            i.reset(v);
            o.reset(v);
        }
        
        public void update(Variable v, NotificationReason r) {
            if (r != NotificationReason.reset) {
                o.Value = op(i.Value);
            }
        }
    }
    
    public delegate bool BooleanOperator1(bool a);
    public delegate bool BooleanOperator2(bool a, bool b);
}