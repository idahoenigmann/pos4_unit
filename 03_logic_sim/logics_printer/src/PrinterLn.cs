using System;
using i15013.logics.lukasiewiczn;
 
namespace i15013.logics_printer.lukasiewiczn {
    static public class Printer {
        static public void print_table(Func<double, double, double> op, int n) {
            double nd = Convert.ToDouble(n);
            for (double a = 0; a <= 1; a += 1 / (nd - 1)) {
                for (double b = 0; b <= 1; b += 1 / (nd - 1)) {
                    Console.WriteLine($" {a:0.000} | {b:0.000} |   {(op(a, b)):0.000}");
                }
            }
        }

        static public void print_table(Func<double, double> op, int n) {
            double nd = Convert.ToDouble(n);
            for (double a = 0; a <= 1; a += 1 / (nd - 1)) {
                Console.WriteLine($" {a:0.000} |   {(op(a)):0.000}");
            }
        }
    }

    class Program {
        static public void print_lukasiewiczn_logic() {
            Console.WriteLine("Lukasiewicz L4");
            Console.WriteLine("===================\n");

            propositional_logic.Printer.print_head("&", 2);
            Printer.print_table(Operators.conj, 4);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("|", 2);
            Printer.print_table(Operators.disj, 4);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("^", 2);
            Printer.print_table(Operators.anti, 4);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("!", 1);
            Printer.print_table(Operators.neg, 4);
            Console.WriteLine("");

            propositional_logic.Printer.print_head("->", 2);
            Printer.print_table(Operators.imp, 4);
            Console.WriteLine("");

            
            propositional_logic.Printer.print_head("<->", 2);
            Printer.print_table(Operators.equiv, 4);
            Console.WriteLine("");
        }
    }
}