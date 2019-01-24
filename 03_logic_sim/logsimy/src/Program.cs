using System;

namespace i15013.logsimy
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable v1 = new Variable(false, "v1");
            Variable v2 = new Variable(false, "v2");

            variables.Utilities.connect(v1, v2);
            
            Console.WriteLine(v2.value);
        }
    }
}
