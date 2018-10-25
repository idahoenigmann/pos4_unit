using System.Data;
using i15013.elispy;
using i15013.lexer;

namespace elispy {
    public class Sexp {
        protected bool is_quoted;
        protected Position? position;

        protected Sexp(Position? position) {
            //this.position = position;
        }

        public Sexp eval(Context ctx) {
            return this;
        }

        public string ToString() {
            return "";
        }

        public static void operator_int (Sexp sexp) {
            
        }

        public static void operator_string(Sexp sexp) {
            
        }

        public static void operator_Sexp(int i) {
            
        }
        
        
    }
    
    public class SexpList : Sexp {
        
    }
    
    public class SexpAtom : Sexp{
        
    }

    public class SexpSymbol : SexpAtom {
        
    }

    public class SexpString : SexpAtom {
        
    }

    public class SexpInteger : SexpAtom {
        
    }
}