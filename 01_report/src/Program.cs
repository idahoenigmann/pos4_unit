using System;
using System.Collections.Generic;

namespace i15013
{
    struct FileInformation
    {
        public string filename;
        public bool sorted;
    }

    struct Record
    {
        public string article;
        public string salesClerk;
        public double price;
        public int quantity;
        public int month;

        public new string ToString()
        {
            return article + ", " + salesClerk + ", " + price + ", " + quantity + ", " + month;
        }
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
            List<Record> list = new List<Record>();
            try
            {
                string[] fileContent = System.IO.File.ReadAllLines(fileInformation.filename);
                int i = 0;
                foreach (var line in fileContent)
                {
                    i++;
                    String[] lineArray = line.Split(',');
                    
                    Record record = new Record();
                    record.article = lineArray[0];
                    record.salesClerk = lineArray[1];
                    if (! double.TryParse(lineArray[2], out record.price))
                    {
                        Console.WriteLine(string.Format("Error in Line {0}. Price: {1}.", i, lineArray[2]));
                        Environment.Exit(3);
                    }

                    if (!int.TryParse(lineArray[3], out record.quantity))
                    {
                        Console.WriteLine(string.Format("Error in Line {0}. Quantity: {1}.", i, lineArray[3]));
                        Environment.Exit(3);
                    }

                    if (!int.TryParse(lineArray[4], out record.month))
                    {
                        Console.WriteLine(string.Format("Error in Line {0}. Month: {1}.", i, lineArray[4]));
                        Environment.Exit(3);
                    }

                    list.Add(record);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine(string.Format("File '{0}' not found!", fileInformation.filename));
                Environment.Exit(2);
            }

            foreach (var record in list)
            {
                Console.WriteLine(record.ToString());
            }
        }
    }
}
