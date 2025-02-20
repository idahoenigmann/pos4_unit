﻿using System;
using System.Collections.Generic;

namespace i15013 {
    struct FileInformation {
        public string filename;
        public bool sorted;
    }

    struct Record {
        public string article;
        public string salesClerk;
        public double price;
        public int quantity;
        public int month;

        public override string ToString() {
            return $"{article}, {salesClerk}, {price}, {quantity}, {month}";
        }
    }
    
    class Program {
        static void Main(string[] args) {
            control_break(process(parse_argv(args)));
        }

        static void usage(string message = null) {
            Console.WriteLine("usage: report [--help|-h|-s] [FILE]\n" +
            "Print a sales statistics report ordered by product and " +
            "salesclerk.\n  --help|-h ... Help!\n" +
            "  -s ... sort it before producing the report\n" +
            "FILE ... file name or - (stdin). If FILE is missing read " +
            "from stdin");

            if (message != null) {
                Console.WriteLine("\n" + message);
            }
            Environment.Exit(1);
        }

        static FileInformation parse_argv(string[] argv) {
            FileInformation fileInformation = new FileInformation();

            if (argv.Length < 1 | argv.Length >= 3) {
                usage();
            }

            for (int i = 0; i < argv.Length; i++) {
                if (argv[i] == "-h" || argv[i] == "--help") {
                    usage();
                } else if (argv[i] == "-s") {
                    fileInformation.sorted = true;
                } else {
                    if (i == argv.Length - 1) {
                        if (argv[i].StartsWith("-")) {
                            usage("No more options allowed!\nAdditional" +
                                  "option was: '" + argv[i] + "'");
                        }
                        fileInformation.filename = argv[i];
                    } else {
                        usage("Unknown option: " + argv[i]);
                    }
                }
            }

            if (fileInformation.filename == null) {
                fileInformation.filename = "-";
            }
            
            return fileInformation;
        }

        static List<Record> process(FileInformation fileInformation) {
            List<Record> list = new List<Record>();
            try {
                string[] fileContent =
                    System.IO.File.ReadAllLines(fileInformation.filename);
                int i = 0;
                foreach (var line in fileContent) {
                    i++;
                    String[] lineArray = line.Split(',');
                    
                    Record record = new Record();
                    record.article = lineArray[0];
                    record.salesClerk = lineArray[1];
                    if (! double.TryParse(lineArray[2], out record.price)) {
                        Console.WriteLine(string.Format("Error in Line {0}." +
                                          "Price: {1}.", i, lineArray[2]));
                        Environment.Exit(3);
                    }

                    if (!int.TryParse(lineArray[3], out record.quantity)) {
                        Console.WriteLine(string.Format("Error in Line {0}." +
                                          "Quantity: {1}.", i, lineArray[3]));
                        Environment.Exit(3);
                    }

                    if (!int.TryParse(lineArray[4], out record.month)) {
                        Console.WriteLine(string.Format("Error in Line {0}." +
                                          "Month: {1}.", i, lineArray[4]));
                        Environment.Exit(3);
                    }

                    list.Add(record);
                }
            } catch (System.IO.FileNotFoundException) {
                Console.WriteLine(string.Format("File '{0}' not found!",
                    fileInformation.filename));
                Environment.Exit(2);
            }

            if (fileInformation.sorted) {
                list.Sort((x, y) =>
                    string.Compare(x.salesClerk, y.salesClerk));
                list.Sort((x, y) => string.Compare(x.article, y.article));

                string[] s_list = new string[list.Count];
                int j = 0;

                foreach (var record in list) {
                    s_list[j] = record.ToString();
                    j++;
                }

                System.IO.File.WriteAllLines(
                    fileInformation.filename + ".sort", s_list);
            }

            return list;
        }

        static void control_break(List<Record> list) {
            Console.WriteLine("### Umsatzstatistik nach Produkten und " +
                              "Verkaeufern ###");
            
            Console.WriteLine($"{"Produkt",-17}{"Verkaeufer", -10}" +
                              $"{"Umsatzsumme", 15}");

            double total_sales = 0.0;
            IEnumerator<Record> rec_iter=list.GetEnumerator();
            rec_iter.MoveNext();
            bool last_elem = false;
            
            while (!last_elem) {
                double article_sales = 0.0;
                Console.WriteLine();
                string curr_article = rec_iter.Current.article;
                
                while (curr_article == rec_iter.Current.article) {
                    double salesclerk_sales = 0.0;
                    string curr_salesclerk = rec_iter.Current.salesClerk;

                    while (curr_article == rec_iter.Current.article &
                           curr_salesclerk == rec_iter.Current.salesClerk) {

                        salesclerk_sales +=
                            rec_iter.Current.price * rec_iter.Current.quantity;
                        article_sales +=
                            rec_iter.Current.price * rec_iter.Current.quantity;
                        total_sales +=
                            rec_iter.Current.price * rec_iter.Current.quantity;
                        last_elem = !rec_iter.MoveNext();
                    }
                    Console.WriteLine($"{curr_article, -17}" +
                                      $"{curr_salesclerk, -10}" +
                                      $"{salesclerk_sales, 15} *");
                }
                Console.WriteLine($"{curr_article,-17}" +
                                  $"{article_sales, 25} **");
            }
            Console.WriteLine($"\nGesamtumsatz{total_sales, 30} ***");
        }
    }
}
