using System;
using SymTab = System.Collections.Generic.Dictionary<string, i15013.elispy.Sexp>;
//using FuncTab = System.Collections.Generic.Dictionary<string, elispy.SexpFunction>;

namespace i15013.elispy {
    public class Context {
        public SymTab symtab = new SymTab();
        //public FuncTab functab;

        public Context() {
            
        }
    }
}