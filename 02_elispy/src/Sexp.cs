using System;
using System.Collections.Generic;
using System.Data;
using i15013.lexer;

namespace i15013.elispy {
    public abstract class Sexp {
        public bool is_quoted = false;
        protected Position? position;

        protected Sexp(Position? position = null) {
            this.position = position;
        }

        public Sexp() {
            
        }

        public abstract Sexp eval(Context ctx = null);

        public override string ToString() {    //can not be virtual because override
            if (is_quoted) {
                return "'";
            }

            return "";
        }

        public static explicit operator int (Sexp sexp) {
            SexpInteger sexpInteger = sexp as SexpInteger;
            if (sexpInteger is null) {
                throw new ArgumentException($"\"{sexp}\" is not a valid argument");
            }
            return sexpInteger.value;
        }

        public static explicit operator string(Sexp sexp) {
            SexpString sexpString = sexp as SexpString;
            if (sexpString == null) {
                throw new ArgumentException($"\"{sexp}\" is not a valid argument");
            }
            return sexpString.value;
        }

        public static implicit operator Sexp(int i) {
            return new SexpInteger(i);
        }

        public static implicit operator Sexp(string s) {
            return new SexpString(s);
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

        public SexpList(Position? position = null) {
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
            if (is_quoted) {
                return new SexpList(terms, position);
            }

            if (terms.Count == 0) {
                return new SexpSymbol("nil", position);
            }

            try {
                SexpSymbol sexpSymbol = terms[0] as SexpSymbol;

                if (sexpSymbol is null) {
                    throw new InterpreterException(
                        $"First item must be a symbol, but got \"{terms[0]}\" at ({position})");
                }

                SexpFunction sexpFunction = ctx.symtab[sexpSymbol.value];

                return sexpFunction.call(terms.GetRange(1, terms.Count - 1),
                    ctx);
            }
            catch (ArgumentException e) {
                throw new InterpreterException(
                    $"method {terms[0]} at ({position}) received invalid argument.",
                    e);
            }
            catch (ConstraintException e) {
                throw new InterpreterException($"method {terms[0]} at ({position}) received too many or too few arguments.", e);
            }
        }

        public override string ToString() {
            if (terms.Count == 0) {
                return "nil";
            }
            string str = base.ToString();
            
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
        public dynamic value { get; protected set; }

        public SexpAtom(dynamic value, Position? position = null) {
            this.value = value;
            this.position = position;
        }

        public SexpAtom() {
            
        }

        public override Sexp eval(Context ctx = null) {
            if (is_quoted) {
                if (GetType() == typeof(SexpString)) {
                    return new SexpString(value, position);
                }
                if (GetType() == typeof(SexpInteger)) {
                    return new SexpInteger(value, position);
                }
            }
            return this;
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
        public SexpSymbol(string name, Position? position = null) {
            value = name;
            this.position = position;
        }

        public override Sexp eval(Context ctx = null) {
            if (is_quoted) {
                return new SexpSymbol(value, position);
            }
            try {
                return ctx.symtab[value];
            }
            catch (KeyNotFoundException) {
                throw new InterpreterException($"Symbol \"{this.value}\" not defined at ({position.ToString()})");
            } catch (NullReferenceException) {
                throw new InterpreterException($"Symbol \"{this.value}\" not defined at ({position.ToString()})");
            }
        }

        public override string ToString() {           
            return base.ToString() + value;
        }

        public override bool is_null() {
            return true;
        }
    }

    public class SexpString : SexpAtom {
        public SexpString(string value, Position? position = null) {
            this.value = value;
            this.position = position;
        }

        public override string ToString() {
            return base.ToString() + "\"" + value + "\"";
        }
    }

    public class SexpInteger : SexpAtom {
        public SexpInteger(int value, Position? position = null) {
            this.value = value;
            this.position = position;
        }

        public override string ToString() {           
            return base.ToString() + value.ToString();
        }
    }
}