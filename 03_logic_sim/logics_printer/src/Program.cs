using System;
using i15013.logics.propositional;

namespace i15013.logics_printer
{
    static class Printer {
        static public void print_table(Func<bool, bool, bool> op) {
            Console.WriteLine($" 0 | 0 |   {System.Convert.ToInt32(op(false, false))}");
            Console.WriteLine($" 0 | 1 |   {System.Convert.ToInt32(op(false, true))}");
            Console.WriteLine($" 1 | 0 |   {System.Convert.ToInt32(op(true, false))}");
            Console.WriteLine($" 1 | 1 |   {System.Convert.ToInt32(op(true, true))}");
        }
        
        static public void print_table(Func<bool, bool> op) {
            Console.WriteLine($" 0 | 0 |   {System.Convert.ToInt32(op(false, false))}");
            Console.WriteLine($" 0 | 1 |   {System.Convert.ToInt32(op(false, true))}");
            Console.WriteLine($" 1 | 0 |   {System.Convert.ToInt32(op(true, false))}");
            Console.WriteLine($" 1 | 1 |   {System.Convert.ToInt32(op(true, true))}");
        }
        
        static public void print_head(string sym) {
            Console.WriteLine($" a | b | a {sym} b");
            Console.WriteLine("---------------");

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Propositional Logic");
            Console.WriteLine("===================\n");

            Printer.print_head("&");
            Printer.print_table(Operators.conj);
            Console.WriteLine("");
            
            Printer.print_head("|");
            Printer.print_table(Operators.disj);
            Console.WriteLine("");
            
            Printer.print_head("^");
            Printer.print_table(Operators.anti);
            Console.WriteLine("");

        }
    }
}
