using System;
using i15013.logsimy.variables;

namespace i15013.logsimy
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable v1 = new Variable(false, "v1");
            Variable v2 = new Variable(false, "v2");

            Utilities.connect(v1, v2);
            Utilities.enable_logging(v2);
            v1.value = true;
            
            //Console.WriteLine(v2.value);
        }
    }
}
