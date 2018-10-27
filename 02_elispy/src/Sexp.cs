using System;
using System.Collections.Generic;
using System.Reflection;
using i15013.lexer;

namespace i15013.elispy {
    public abstract class Sexp {
        public bool is_quoted = false;
        protected Position? position;

        protected Sexp(Position? position) {
            this.position = position;
        }

        public Sexp() {
            
        }

        public abstract Sexp eval(Context ctx = null);

        public abstract override string ToString();

        public static explicit operator int (Sexp sexp) {
            return 0;
        }

        public static explicit operator string(Sexp sexp) {
            return "";
        }

        public static implicit operator Sexp(int i) {
            return new SexpString("");
        }

        public static implicit operator Sexp(string s) {
            return new SexpString("");
        }
        
        public static bool operator==(Sexp lhs, Sexp rhs) {
            return lhs.Equals(rhs);
        }

        public static bool operator!=(Sexp lhs, Sexp rhs) {
            return !lhs.Equals(rhs);
        }

        public override int GetHashCode() {
            return 0;
        }

        public override bool Equals(Object obj) {
            if (obj is Sexp) {
                return this.Equals(obj);
            }
            return false;
        }

        public bool Equals(Sexp sexp) {
            return this.Equals(sexp);
        }

        public static explicit operator bool(Sexp sexp) {
            return true;
        }

        public abstract bool is_null();
    }
    
    public class SexpList : Sexp {
        List<Sexp> terms;

        public SexpList(Position? position) {
            this.position = position;
            this.terms = new List<Sexp>();
        }

        public SexpList(List<Sexp> terms, Position? position) {
            this.terms = terms;
            this.position = position;
        }

        public void add_term(Sexp term) {
            terms.Add(term);
        }

        public override Sexp eval(Context ctx = null) {
            return terms[0];
        }

        public override string ToString() {
            if (terms.Count == 0) {
                return "nil";
            }
            string str = "";

            if (is_quoted) {
                str += "'";
            }
            
            str += "(";
            foreach (Sexp sexp in terms) {
                str += sexp + " ";
            }

            str = str.Remove(str.Length - 1);
            str += ")";
            return str;
        }

        public new int GetHashCode() {
            return 0;
        }

        public new bool Equals(Object o) {
            if (o is SexpList) {

                SexpList sexpList = (SexpList) o;
                
                for (int i=0; i<terms.Count; i++) {
                    if (!terms[i].Equals(sexpList.terms[i])) {
                        return false;
                    }
                }

                return true;
            }
            
            return false;
        }

        public new bool Equals(Sexp sexp) {
            if (sexp is SexpList) {

                SexpList sexpList = (SexpList) sexp;
                
                for (int i=0; i<terms.Count; i++) {
                    if (!terms[i].Equals(sexpList.terms[i])) {
                        return false;
                    }
                }

                return true;
            }
            
            return false;
        }

        public override bool is_null() {
            return true;
        }
    }
    
    public abstract class SexpAtom : Sexp {
        protected dynamic value;

        public SexpAtom(dynamic value, Position? position) {
            this.value = value;
            this.position = position;
        }

        public SexpAtom() {
            
        }

        public override Sexp eval(Context ctx = null) {
            return new SexpString("");
        }

        public new int GetHashCode() {
            return 0;
        }

        public new bool Equals(Object o) {
            return true;
        }

        public new bool Equals(Sexp sexp) {
            return true;
        }

        public override bool is_null() {
            return true;
        }
    }

    public class SexpSymbol : SexpAtom {
        public SexpSymbol(string name, Position? position) {
            value = name;
            this.position = position;
        }

        public new Sexp eval(Context ctx = null) {
            return new SexpString("");
        }

        public override string ToString() {
            if (is_quoted) {
                return "'" + value;
            }
            
            return value;
        }

        public override bool is_null() {
            return true;
        }
    }

    public class SexpString : SexpAtom {
        public SexpString(string value) {
            this.value = value;
        }

        public override string ToString() {
            if (is_quoted) {
                return "'\"" + value + "\"";
            }
            
            return "\"" + value + "\"";
        }
    }

    public class SexpInteger : SexpAtom {
        public SexpInteger(int value) {
            this.value = value;
        }

        public override string ToString() {
            if (is_quoted) {
                return "'" + value.ToString();
            }
            
            return value.ToString();
        }
    }
}