using System;
using System.Collections.Generic;
using i15013.elispy;
using i15013.lexer;

namespace i15013.elispy {
    public abstract class Sexp {
        protected bool is_quoted;
        protected Position? position;

        protected Sexp(Position? position) {
        }

        public Sexp() {
            
        }

        public abstract Sexp eval(Context ctx = null);

        public override string ToString() {
            return "";
        }

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
            return true;
        }

        public static bool operator!=(Sexp lhs, Sexp rhs) {
            return true;
        }

        public override int GetHashCode() {
            return 0;
        }

        public override bool Equals(Object obj) {
            return true;
        }

        public bool Equals(Sexp sexp) {
            return true;
        }

        public static explicit operator bool(Sexp sexp) {
            return true;
        }

        public abstract bool is_null();
    }
    
    public class SexpList : Sexp {
        List<Sexp> terms;

        public SexpList(Position? position) {
            
        }

        public SexpList(List<Sexp> terms, Position? position) {
            
        }

        public void add_term(Sexp term) {
            
        }

        public override Sexp eval(Context ctx = null) {
            return terms[0];
        }

        public new string ToString() {
            return "";
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
    
    public abstract class SexpAtom : Sexp {
        dynamic value;

        public SexpAtom(dynamic value, Position? position) {
            
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
            
        }

        public new Sexp eval(Context ctx = null) {
            return new SexpString("");
        }

        public new string ToString() {
            return "";
        }

        public override bool is_null() {
            return true;
        }
    }

    public class SexpString : SexpAtom {
        public SexpString(string value) {
            
        }

        public new string ToString() {
            return "";
        }
    }

    public class SexpInteger : SexpAtom {
        public SexpInteger(int value) {
            
        }

        public new string ToString() {
            return "";
        }
    }
}