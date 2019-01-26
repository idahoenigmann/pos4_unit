using System;
using i15013.logsimy.variables;
using i15013.logsimy.gates.propositional;

namespace i15013.logsimy
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Variable v1 = new Variable(false, "v1");
            Variable v2 = new Variable(false, "v2");

            Utilities.connect(v1, v2);
            Utilities.enable_logging(v2);
            v1.value = true;
            
            //Console.WriteLine(v2.value);
            */
            
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

            i0.value = true;
            i1.value = true;
            i2.value = true;
        }
    }
}
