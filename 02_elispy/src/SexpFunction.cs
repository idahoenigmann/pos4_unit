using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace i15013.elispy {
    public abstract class SexpFunction : SexpSymbol {
        //protected string name;

        protected SexpFunction(string name) : base(name) {}

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
            int res = 0;
            foreach (Sexp sexp in args) {
                res += (int) sexp.eval(ctx);
            }
            return res;
        }
    }

    public class SubSexpFunction : BuiltInSexpFunction {
        public SubSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count == 0) {
                return 0;
            }

            if (args.Count == 1) {
                return -(int)args[0].eval(ctx);
            }
            
            int res = (int)args[0].eval();
            for (int i=1; i < args.Count; i++) {
                res -= (int) args[i].eval(ctx);
            }
            return res;
        }
    }
    
    public class MulSexpFunction : BuiltInSexpFunction {
        public MulSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            int res = 1;
            foreach (Sexp sexp in args) {
                res *= (int) sexp.eval(ctx);
            }
            return res;
        }
    }
    
    public class DivSexpFunction : BuiltInSexpFunction {
        public DivSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count <= 1) {
                return 0;
            }
            
            int res = (int)args[0].eval();
            for (int i=1; i < args.Count; i++) {
                res /= (int) args[i].eval(ctx);
            }
            return res;
        }
    }
    
    public class LessThanSexpFunction : BuiltInSexpFunction {
        public LessThanSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval() < (int)args[1].eval()) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
    }
    
    public class LessThanOrEqualsSexpFunction : BuiltInSexpFunction {
        public LessThanOrEqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval() <= (int)args[1].eval()) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
    }
    
    public class EqualsSexpFunction : BuiltInSexpFunction {
        public EqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval() == (int)args[1].eval()) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
    }
    
    public class GreaterThanOrEqualsSexpFunction : BuiltInSexpFunction {
        public GreaterThanOrEqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval() >= (int)args[1].eval()) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
    }
    
    public class GreaterThanSexpFunction : BuiltInSexpFunction {
        public GreaterThanSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval() > (int)args[1].eval()) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
    }
    
    public class FirstSexpFunction : BuiltInSexpFunction {
        public FirstSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }
            
            if (!(args[0].eval(new Context()) is SexpList sexpList)) {
                if (args[0].eval(new Context()).Equals(new SexpSymbol("nil"))) {
                    return new SexpSymbol("nil");
                }
                
                throw new ArgumentException();
            }
            return sexpList.terms[0].eval(ctx);
            
        }
    }
}