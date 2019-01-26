using System;
using System.Collections.Generic;
using i15013.logics.lukasiewicz3;
 
namespace i15013.logics_printer.lukasiewicz3 {
    static public class Printer {
        static string boolToString(bool? b) {
            if (b is null) {
                return "½";
            }

            return System.Convert.ToInt32(b.Value).ToString();
        }
        
        static public void print_table(Func<bool?, bool?, bool?> op) {
            bool?[] array = new bool?[3] {false, null, true};

            foreach (bool? a in array) {
                foreach (bool? b in array) {
                    Console.WriteLine($" {boolToString(a)} | {boolToString(b)} |   {boolToString(op(a, b))}");
                }
            }
        }
        
        static public void print_table(Func<bool?, bool?> op) {
            bool?[] array = new bool?[3] {false, null, true};

            foreach (bool? a in array) {
                Console.WriteLine($" {boolToString(a)} |   {boolToString(op(a))}");
            }
        }
    }
    
    class Program {
        static public void print_lukasiewicz3_logic() {
            Console.WriteLine("Lukasiewicz L3 Logic");
            Console.WriteLine("===================\n");

            propositional_logic.Printer.print_head("&", 2);
            Printer.print_table(Operators.conj);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("|", 2);
            Printer.print_table(Operators.disj);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("^", 2);
            Printer.print_table(Operators.anti);
            Console.WriteLine("");
            
            propositional_logic.Printer.print_head("!", 1);
            Printer.print_table(Operators.neg);
            Console.WriteLine("");

            propositional_logic.Printer.print_head("->", 2);
            Printer.print_table(Operators.imp);
            Console.WriteLine("");

            
            propositional_logic.Printer.print_head("<->", 2);
            Printer.print_table(Operators.equiv);
            Console.WriteLine("");
        }
    }
}