using System;
using System.Collections;
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

        public virtual bool Equals(Object obj) {
            Sexp sexp = obj as Sexp;
            if (sexp is null) return false;
            return eval() == sexp.eval();
        }

        public virtual bool Equals(Sexp sexp) {
            return eval() == sexp.eval();
        }

        public static explicit operator bool(Sexp sexp) {
            return true;
        }

        public abstract bool is_null();
    }
    
    public class SexpList : Sexp {
        public List<Sexp> terms;

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
                return this;
            }

            try {
                SexpFunction sexpFunction =
                    ctx.functab[((SexpAtom) terms[0]).value];

                return sexpFunction.call(terms.GetRange(1, terms.Count - 1),
                    ctx);
            }
            catch (ArgumentException e) {
                throw new InterpreterException(
                    $"method {terms[0]} at ({position}) received invalid argument.",
                    e);
            }
            catch (ConstraintException e) {
                throw new InterpreterException(
                    $"method {terms[0]} at ({position}) received too many or too few arguments.",
                    e);
            }
            catch (KeyNotFoundException) {
                throw new InterpreterException(
                    $"First item must be a function, but got \"{terms[0]}\" at ({position})");
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

        public override bool Equals(Object o) {
            SexpList sexpList = o as SexpList;
            if (sexpList is null) {
                if (terms.Count == 0 && ((SexpAtom)o).value == "nil") {
                    return true;
                }
                return false;
            }

            if (sexpList.terms.Count != terms.Count) {
                return false;
            }
            
            for (int i=0; i<terms.Count; i++) {
                if (!terms[i].Equals(sexpList.terms[i])) {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(Sexp sexp) {
            SexpList sexpList = sexp as SexpList;
            if (sexpList is null) {
                if (terms.Count == 0 && ((SexpAtom)sexp).value == "nil") {
                    return true;
                }
                return false;
            }

            if (sexpList.terms.Count != terms.Count) {
                return false;
            }
            
            for (int i=0; i<terms.Count; i++) {
                if (!terms[i].Equals(sexpList.terms[i])) {
                    return false;
                }
            }

            return true;
        }

        public override bool is_null() {
            return false;
        }
    }
    
    public abstract class SexpAtom : Sexp {
        public dynamic value { get; protected set; }

        public SexpAtom(dynamic value, Position? position = null) {
            this.value = value;
            this.position = position;
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

        public override bool Equals(Object o) {
            SexpAtom sexpAtom = o as SexpAtom;
            if (sexpAtom is null) {
                return false;
            }
            return sexpAtom.value == value;
        }

        public override bool Equals(Sexp sexp) {
            SexpAtom sexpAtom = sexp as SexpAtom;
            if (sexpAtom is null) {
                return false;
            }
            return sexpAtom.value == value;
        }

        public override bool is_null() {
            return true;
        }
    }

    public class SexpSymbol : SexpAtom {
        public SexpSymbol(string name, Position? position = null) : base(name,
            position) { }

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
        public SexpString(string value, Position? position = null) : base(value, position) {}

        public override string ToString() {
            return base.ToString() + "\"" + value + "\"";
        }
    }

    public class SexpInteger : SexpAtom {
        public SexpInteger(int value, Position? position = null) : base(value, position) {}

        public override string ToString() {           
            return base.ToString() + value.ToString();
        }
    }
}