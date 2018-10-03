using System;

namespace i15013
{
    class Program
    {
        static void Main(string[] args) {
            usage();
        }
        
        static void usage(string message = null) {
            Console.WriteLine("usage: elispy [--help|-h|-s] [FILE]\n" +
                              "Executes the \"elispy\" expressions contained " +
                              "in FILE otherwise the REPL will be started\n\n" +
                              "--help|-h ... Help!\n" +
                              "-g ... generate C# code; only valid if FILE " +
                              "is provided\n" +
                              "FILE ... file name or - (stdin). If FILE is" +
                              "missing start the REPL");

            if (message != null) {
                Console.WriteLine("\n" + message);
            }
            Environment.Exit(1);
        }

    }
}
