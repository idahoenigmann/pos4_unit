using System;
using i15013.logics.propositional;
using i15013.logics_printer.lukasiewicz3;
using i15013.logics_printer.lukasiewiczn;

namespace i15013.logics_printer.propositional_logic {
    static public class Printer {
        static public void print_table(Func<bool, bool, bool> op) {
            Console.WriteLine($" 0 | 0 |   {System.Convert.ToInt32(op(false, false))}");
            Console.WriteLine($" 0 | 1 |   {System.Convert.ToInt32(op(false, true))}");
            Console.WriteLine($" 1 | 0 |   {System.Convert.ToInt32(op(true, false))}");
            Console.WriteLine($" 1 | 1 |   {System.Convert.ToInt32(op(true, true))}");
        }
        
        static public void print_table(Func<bool, bool> op) {
            Console.WriteLine($" 0 |   {System.Convert.ToInt32(op(false))}");
            Console.WriteLine($" 1 |   {System.Convert.ToInt32(op(true))}");
        }
        
        static public void print_head(string sym, int args) {
            if (args == 2) {
                Console.WriteLine($" a | b | a {sym} b");
                Console.WriteLine("---------------");
            } else if (args == 1) {
                Console.WriteLine($" a |  {sym}a");
                Console.WriteLine("-------------");
            }
        }
    }
    
    class Program {
        static void print_propositional_logic() {
            Console.WriteLine("Propositional Logic");
            Console.WriteLine("===================\n");

            Printer.print_head("&", 2);
            Printer.print_table(Operators.conj);
            Console.WriteLine("");
            
            Printer.print_head("|", 2);
            Printer.print_table(Operators.disj);
            Console.WriteLine("");
            
            Printer.print_head("^", 2);
            Printer.print_table(Operators.anti);
            Console.WriteLine("");
            
            Printer.print_head("!", 1);
            Printer.print_table(Operators.neg);
            Console.WriteLine("");

            Printer.print_head("->", 2);
            Printer.print_table(Operators.imp);
            Console.WriteLine("");

            
            Printer.print_head("<->", 2);
            Printer.print_table(Operators.equiv);
            Console.WriteLine("");
        }
        
        static void Main(string[] args) {
            Program.print_propositional_logic();
            i15013.logics_printer.lukasiewicz3.Program.print_lukasiewicz3_logic();
			i15013.logics_printer.lukasiewiczn.Program.print_lukasiewiczn_logic();
        }
    }
}