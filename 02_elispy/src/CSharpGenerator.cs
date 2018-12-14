using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using i15013.elispy;

namespace i15013.transpiler {
    public class CSharpGenerator : CodeGenerator {
        public void generateCode(StreamWriter sw, SexpsParser parser, string source) {
			string sof = "using System;\n" +
				"using System.Collections.Generic;\n\n" +
				"class Program {\n" +
				tab + "public static void Main() {\n";
            sw.Write(sof);

            foreach(Sexp sexp in parser.parse(source)) {
                sw.WriteLine(tab + tab + toCSharp(sexp) + ";");
            }

			string eof = tab + "}\n" +
				"}\n";

			
			sw.Write(eof);
		}

        public string toCSharp(Sexp sexp) {
            if (sexp is SexpAtom) {
                return sexp.ToString();
            } else if (sexp is SexpList) {
                if (sexp.is_quoted) {
                    string res = "new List<dynamic>(new dynamic[]{";
                    foreach(Sexp s in ((SexpList)sexp).terms) {
                        res += s + " ,";
                    }
                    res = res.Substring(0, res.Length-2);
                    res += "})";
                    return res;
                } else if (sexp is SexpFunction) {
                    return ((BuiltInSexpFunction)sexp).toCS();
                }
            }
            return "";
        }

        public StringBuilder code { get; private set; }
		private string tab = "    ";
    }

    
}