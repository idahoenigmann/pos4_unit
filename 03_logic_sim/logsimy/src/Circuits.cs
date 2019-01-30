using System;
using i15013.logsimy.variables;
using i15013.logsimy.gates.propositional;

namespace i15013.logsimy.circuits {
    public class Circuits {
        public static void and3 (){
            Variable i0 = new Variable(false, "i0");
            Variable i1 = new Variable(false, "i1");
            Variable i2 = new Variable(false, "i2");
            
            AndGate and1 = new AndGate("and1");
            AndGate and2 = new AndGate("and2");

            Utilities.connect(i0, and1.i0);
            Utilities.connect(i1, and1.i1);
            Utilities.connect(i2, and2.i1);
            
            Utilities.connect(and1.o, and2.i0);

            Utilities.enable_logging(and2.o);

            i0.Value = true;
            i1.Value = true;
            i2.Value = true;
        }
    }
}