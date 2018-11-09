using System;
using System.Collections.Generic;

namespace i15013.elispy {
    public abstract class SexpFunction {
        protected string name;

        protected SexpFunction(string name) {
            this.name = name;
        }

        public abstract Sexp call(List<Sexp> args, Context ctx);

        public override String ToString() {
            return "";
        }
    }

    public abstract class BuiltInSexpFunction : SexpFunction {
        public BuiltInSexpFunction(string name) : base(name) {
        }
    }
    
    public class AddSexpFunction : BuiltInSexpFunction {
        public AddSexpFunction(string name) : base(name) {}
        
        public override Sexp call(List<Sexp> args, Context ctx) {
            return new SexpInteger(1);
        }
    }

    public class SubSexpFunction : BuiltInSexpFunction {
        public SubSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            return new SexpInteger(1);
        }
    }
}