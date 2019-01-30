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

		public static void flip_flop() {
			Variable i0 = new Variable(false, "i0");
			Variable i1 = new Variable(false, "i1");
			Variable o0 = new Variable(false, "o0");
			Variable o1 = new Variable(false, "o1");

			NOrGate nor1 = new NOrGate("nor1");
			NOrGate nor2 = new NOrGate("nor2");

			Utilities.connect(i0, nor1.i0);
			Utilities.connect(i1, nor2.i1);
			Utilities.connect(nor1.o, nor2.i0);
			Utilities.connect(nor2.o, nor1.i1);
			Utilities.connect(nor1.o, o0);
			Utilities.connect(nor2.o, o1);

			Utilities.enable_logging(nor2.o);

			i0.reset();
            i1.reset();
            o0.reset();
            o1.reset();

			nor1.reset(true);
			nor2.reset(false);

			nor2.i0.Value = true;
			
			Console.WriteLine("start flip-flopping");

			for (int i=0; i < 5; i++) {
				Console.WriteLine("set: true");
                i0.Value = true;
				Console.WriteLine("set: false");
                i0.Value = false;
				Console.WriteLine("reset: true");
                i1.Value = true;
				Console.WriteLine("reset: false");
                i1.Value = false;
			}
		}
    }
}