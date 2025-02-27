﻿using System;
using System.IO;
using i15013.elispy;
using i15013.transpiler;

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

            SexpsLexer lexer = new SexpsLexer();
            SexpsParser parser = new SexpsParser(lexer, new Context());
            SexpsInterpreter interpreter = new SexpsInterpreter(parser);

            //lexer.test();
            //parser.test();
            //interpreter.test();
            //interpreter.test_repl();
            if (fileInformation.g) {
                StreamWriter sw;
                if (fileInformation.filename == "-") {
                    sw = new StreamWriter(Console.OpenStandardOutput());
                    
                }
                else {
                    int idx = fileInformation.filename.LastIndexOf(".");
                    string csFile = (idx < 0)
                        ? fileInformation.filename
                        : fileInformation.filename.Remove(idx,
                            fileInformation.filename.Length - idx);
                    sw = new StreamWriter(csFile + ".cs");
                }
                sw.AutoFlush = true;
                CSharpGenerator cSharpGenerator = new CSharpGenerator();
                cSharpGenerator.generateCode(sw, new SexpsParser(new SexpsLexer(), new Context()), File.ReadAllText(fileInformation.filename));
            }
            else {
                if (fileInformation.filename == "") {
                    interpreter.repl();
                }
                else if (fileInformation.filename == "-") {
                    interpreter.repl_stdin_file(Console.In.ReadToEnd());
                }
                else {
                    string input = "";
                    try {
                        input = File.ReadAllText(fileInformation.filename);
                    }
                    catch (FileNotFoundException) {
                        usage(
                            $"Can not open file \"{fileInformation.filename}\".");
                    }
                    interpreter.repl_stdin_file(input);
                }
            }

        }
        
        private static void usage(string message = null) {
            Console.WriteLine("usage: elispy [--help|-h|-g] [FILE]\n" +
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
            FileInformation fileInformation = new FileInformation {
                filename = "", g = false
            };
            switch (args.Length) {
                case 0:
                    break;
                case 1 when args[0] == "--help" | args[0] == "-h":
                    usage();
                    break;
                case 1 when args[0] == "-g":
                    usage("No filename given but code generation requested!");
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