using System;
using System.Collections.Generic;

namespace i15013.elispy {
    public interface IParser {
        void parse(string source);
    }

    /*
     * Atom::= INTEGER | STRING | SYMBOL
     * List::= ( {Atom} )
     * Sexp::= ' Atom | List | Sexp
     * Program::= {Sexp}
     */
    
    public class SexpsParser {
        public SexpsParser() {
            
        }
    }
}