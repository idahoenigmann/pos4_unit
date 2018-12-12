using i15013.elispy;

namespace i15013.transpiler {
    public interface CodeGenerator {
        void visit(SexpSymbol sexpsym);
        void visit(SexpString sexpstr);
        void visit(SexpInteger sexpint);
        void visit(SexpList sexplst);
    }

    public class CSharpGenerator {
        
    }
}