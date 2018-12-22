using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using i15013.elispy;

namespace i15013.transpiler {
    public class CSharpGenerator : CodeGenerator {
        public void generateCode(StreamWriter sw, SexpsParser parser, string source) {
            string sof = "using System;\n" +
                         "using System.Diagnostics;\n" +
                         "using System.Collections.Generic;\n\n" +
                         "class Program {\n" +
                         tab +
                         "public static string shell_exec(string cmd) {\n" +
                         tab + tab +
                         "var escaped_args = cmd.Replace(\"\\\"\", \"\\\\\\\"\");\n" +
                         tab + tab + "var process = new Process() {\n" +
                         tab + tab + tab +
                         "StartInfo = new ProcessStartInfo {\n" +
                         tab + tab + tab + tab +
                         "FileName = \"/bin/bash\",\n" +
                         tab + tab + tab + tab +
                         "Arguments = $\"-c \\\"{escaped_args}\\\"\",\n" +
                         tab + tab + tab + tab +
                         "RedirectStandardOutput = true,\n" +
                         tab + tab + tab + tab + "UseShellExecute = false,\n" +
                         tab + tab + tab + tab + "CreateNoWindow = true\n" +
                         tab + tab + tab + "}\n" +
                         tab + tab + "};\n\n" +
                         tab + tab + "process.Start();\n" +
                         tab + tab + "string result = process.StandardOutput.ReadToEnd();\n" +
                         tab + tab + "process.WaitForExit();\n\n" +
                         tab + tab + "if (process.ExitCode != 0)\n" +
                         tab + tab + tab + "throw new InvalidOperationException(\"Process exited.\");\n" +
            tab + tab + "return result;\n" +
                tab + "}\n\n" +
				tab + "public static void Main() {\n";
            sw.Write(sof);

            addSymbols(sw);

            foreach(Sexp sexp in parser.parse(source)) {
                sw.WriteLine(tab + tab + toCSharp(sexp) + ";");
            }

			string eof = tab + "}\n" +
				"}\n";

			
			sw.Write(eof);
		}

        public void addSymbols(StreamWriter sw) {
            sw.WriteLine(tab + tab + "bool t = true;\n" + tab + tab +
                         "bool nil = false;\n");
        }

        public static string toCSharp(Sexp sexp) {
            if (sexp is SexpAtom) {
                if (sexp.is_quoted) {
                    return ((SexpAtom) sexp).value;
                }
                return sexp.ToString();
            }
            else if (sexp is SexpList) {
                if (sexp.is_quoted) {
                    string res = "new List<dynamic>(new dynamic[]{";
                    foreach (Sexp s in ((SexpList) sexp).terms) {
                        res += s + " ,";
                    }

                    res = res.Substring(0, res.Length - 2);
                    res += "})";
                    return res;
                }
                else {
                    List<Sexp> listSexps = ((SexpList) sexp).terms;
                    if (listSexps.Count > 0) {
                        return ctx.functab[((SexpAtom) (listSexps[0])).value]
                            .toCS(listSexps.GetRange(1, listSexps.Count-1));
                    }
                    else {
                        return "new List<dynamic>()";
                    }
                }
            }

            return "";
        }

        public StringBuilder code { get; private set; }
		private string tab = "    ";
        private static Context ctx = new Context();
        static public List<string> vars = new List<String>();
    }

    
}