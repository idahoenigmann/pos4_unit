using System;

namespace i15013.logsimy
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable v1 = new Variable(false, "v1");
            Variable v2 = new Variable(false, "v2");

            v1.notify += (v => v2.value = v);
            v1.set_value(true);
            
            Console.WriteLine(v2.value);
        }
    }
}
