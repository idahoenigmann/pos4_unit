using SymTab = System.Collections.Generic.Dictionary<string, i15013.elispy.Sexp>;
using FuncTab = System.Collections.Generic.Dictionary<string, i15013.elispy.SexpFunction>;

namespace i15013.elispy {
    public class Context {
        public SymTab symtab = new SymTab();
        public FuncTab functab = new FuncTab();

        public Context() {
            symtab["nil"] = new SexpList();
            symtab["t"] = new SexpSymbol("t");
            
            functab["+"] = new AddSexpFunction("+");
            functab["-"] = new SubSexpFunction("-");
            functab["*"] = new MulSexpFunction("*");
            functab["/"] = new DivSexpFunction("/");
            functab["<"] = new LessThanSexpFunction("<");
            functab["<="] = new LessThanOrEqualsSexpFunction("<=");
            functab["="] = new EqualsSexpFunction("=");
            functab[">="] = new GreaterThanOrEqualsSexpFunction(">=");
            functab[">"] = new GreaterThanSexpFunction(">");
            functab["first"] = new FirstSexpFunction("first");
            functab["rest"] = new RestSexpFunction("rest");
            functab["cons"] = new ConsSexpFunction("cons");
            functab["equal"] = new EqualSexpFunction("equal");
            functab["setq"] = new SetqSexpFunction("setq");
            functab["null"] = new NullSexpFunction("null");
            functab["if"] = new IfSexpFunction("if");
            functab["progn"] = new PrognSexpFunction("progn");
            functab["princ"] = new PrincSexpFunction("princ");
            functab["while"] = new WhileSexpFunction("while");
            functab["shell"] = new ShellSexpFunction("shell");
            functab["not"] = new NotSexpFunction("not");
            functab["and"] = new AndSexpFunction("and");
            functab["or"] = new OrSexpFunction("or");
        }
    }
}