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
            usage();
            parse_argv(args);
        }

        static void usage()
        {
            Console.WriteLine("usage: report [--help|-h|-s] [FILE]");
            Console.WriteLine("Print a sales statistics report ordered by product and salesclerk.\n");
            Console.WriteLine("  --help|-h ... Help!");
            Console.WriteLine("  -s ... sort it before producing the report");
            Console.WriteLine("FILE ... file name or - (stdin). If FILE is missing read from stdin");
            
            Environment.Exit(1);
        }

        static FileInformation parse_argv(string[] argv)
        {
            FileInformation fileInformation = new FileInformation();
            return fileInformation;
        }
    }
}
