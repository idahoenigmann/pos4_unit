using System;

namespace i15013
{
    struct FileInformation
    {
        public string filename;
        public bool sorted;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            process(parse_argv(args));
        }

        static void usage(string message = null)
        {
            Console.WriteLine("usage: report [--help|-h|-s] [FILE]");
            Console.WriteLine("Print a sales statistics report ordered by product and salesclerk.\n");
            Console.WriteLine("  --help|-h ... Help!");
            Console.WriteLine("  -s ... sort it before producing the report");
            Console.WriteLine("FILE ... file name or - (stdin). If FILE is missing read from stdin");

            if (message != null)
            {
                Console.WriteLine("\n" + message);
            }
            
            Environment.Exit(1);
        }

        static FileInformation parse_argv(string[] argv)
        {
            FileInformation fileInformation = new FileInformation();

            if (argv.Length >= 3)
            {
                usage("No more optioins or parameters allowed!\nAdditional argument was: '" + argv[2] + "'");
            }

            for (int i = 0; i < argv.Length; i++)
            {
                if (argv[i] == "-h" || argv[i] == "--help")
                {
                    usage();
                }
                else if (argv[i] == "-s")
                {
                    fileInformation.sorted = true;
                }
                else
                {
                    if (i == argv.Length - 1)
                    {
                        if (argv[i].StartsWith("-"))
                        {
                            usage("No more options allowed!\nAdditional option was: '" + argv[i] + "'");
                        }
                        fileInformation.filename = argv[i];
                    }
                    else
                    {
                        usage("Unknown option: " + argv[i]);
                    }
                }
            }

            if (fileInformation.filename == null)
            {
                fileInformation.filename = "-";
            }
            
            return fileInformation;
        }

        static void process(FileInformation fileInformation)
        {
            string fileContent = "";
            try
            {
                fileContent = System.IO.File.ReadAllText(fileInformation.filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine(string.Format("File '{0}' not found!", fileInformation.filename));
                Environment.Exit(2);
            }

            Console.WriteLine(fileContent);
        }

    }
}
