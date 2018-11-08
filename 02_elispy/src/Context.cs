using System;
using System.Collections.Generic;
using SymTab = System.Collections.Generic.Dictionary<string, i15013.elispy.Sexp>;
using FuncTab = System.Collections.Generic.Dictionary<string, i15013.elispy.SexpFunction>;

namespace i15013.elispy {
    public class Context {
        public SymTab symtab = new SymTab();
        public FuncTab functab;

        public Context() {
            symtab["nil"] = new SexpList();
            symtab["t"] = new SexpSymbol("t");
        }
    }
}