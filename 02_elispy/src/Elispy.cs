using System;
using i15013.lexer;
using i15013.elispy;

namespace i15013
{
    struct FileInformation {
        public string filename;
        public bool g;
    }
    
    class Program
    {
        static void Main(string[] args) {
            FileInformation fileInformation = parse_argv(args);

            SexpsLexer.test();
        }
        
        public static void usage(string message = null) {
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

        private static FileInformation parse_argv(string[] args) {
            FileInformation fileInformation = new FileInformation();
            fileInformation.filename = "-";
            fileInformation.g = false;
            switch (args.Length) {
                case 0:
                    break;
                case 1 when args[0] == "--help" | args[0] == "-h":
                    usage();
                    break;
                case 1 when args[0] == "-g":
                    fileInformation.g = true;
                    break;
                case 1:
                    fileInformation.filename = args[0];
                    break;
                case 2 when args[0] != "-g":
                    usage($"illegal argument {args[0]}");
                    break;
                case 2:
                    fileInformation.g = true;
                    fileInformation.filename = args[1];
                    break;
                default:
                    usage();
                    break;
            }
            return fileInformation;
        }
    }
}
