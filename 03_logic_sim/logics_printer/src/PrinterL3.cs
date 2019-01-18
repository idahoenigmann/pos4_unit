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
            var dict = new Dictionary<string, bool?>();
            dict.Add("0", false);
            dict.Add("½", null);
            dict.Add("1", true);

            foreach (KeyValuePair<string, bool?> a in dict) {
                foreach (KeyValuePair<string, bool?> b in dict) {
                    Console.WriteLine($" {a.Key} | {b.Key} |   {boolToString(op(a.Value, b.Value))}");
                }
            }
        }
        
        static public void print_table(Func<bool?, bool?> op) {
            var dict = new Dictionary<string, bool?>();
            dict.Add("0", false);
            dict.Add("½", null);
            dict.Add("1", true);
            
            foreach (KeyValuePair<string, bool?> a in dict) {
                Console.WriteLine($" {a.Key} |   {boolToString(op(a.Value))}");
            }
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
        static public void print_lukasiewicz3_logic() {
            Console.WriteLine("Lukasiewicz L3 Logic");
            Console.WriteLine("===================\n");

            propositional_logic.Printer.print_head("&", 2);
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
    }
}