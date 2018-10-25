using elispy;
using SymTab = System.Collections.Generic.Dictionary<string, elispy.Sexp>;
//using FuncTab = System.Collections.Generic.Dictionary<string, elispy.SexpFunction>;

namespace i15013.elispy {
    public class Context {
        public SymTab symtab;
        //public FuncTab functab;

        public Context() {
            
        }
    }
}