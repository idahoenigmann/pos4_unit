using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using i15013.elispy;

namespace i15013.transpiler {
    public class CSharpGenerator : CodeGenerator {
        public void generateCode(StreamWriter sw, SexpsParser parser, string source) {
        	if (File.Exists("sof.txt")) {
				sw.Write(File.ReadAllText("sof.txt"));
			} else {
				throw new TranspilerException("sof.txt was deleted! code can not" +
 				" be generated without this file.");
			}

            foreach(Sexp sexp in parser.parse(source)) {
                sw.WriteLine(tab + tab + toCSharp(sexp) + ";");
            }
			
			if (uses_shell) {
				if (File.Exists("eof_with_shell.txt")) {
					sw.Write(File.ReadAllText("eof_with_shell.txt"));
				} else {
					throw new TranspilerException("eof_with_shell.txt was deleted! code can not" +
 					" be generated without this file.");
				}
			} else {
				if (File.Exists("eof.txt")) {
					sw.Write(File.ReadAllText("eof.txt"));
				} else {
					throw new TranspilerException("eof.txt was deleted! code can not" +
 					" be generated without this file.");
				}
			}
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
		static public bool uses_shell = false;
    }

	
    
}