using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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
        protected BuiltInSexpFunction(string name) : base(name) {
        }

        public abstract string toCS(List<String> list);
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

        public override string toCS(List<String> list) {
			if (list.Count == 0) {
				return "0";
			}
			string res = "";
			foreach (String s in list) {
				res += s + " + ";
			}
            return res.Substring(0, res.Length - 3);
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
            
            int res = (int)args[0].eval(ctx);
            for (int i=1; i < args.Count; i++) {
                res -= (int) args[i].eval(ctx);
            }
            return res;
        }
        public override string toCS(List<String> list) {
			if (list.Count == 0) {
				return "0";
			} else if (list.Count == 1) {
				return "0 - " + list[0];
			}
			string res = "";
			foreach (String s in list) {
				res += s + " - ";
			}
            return res.Substring(0, res.Length - 3);
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
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class DivSexpFunction : BuiltInSexpFunction {
        public DivSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count <= 1) {
                return 0;
            }
            
            int res = (int)args[0].eval(ctx);
            for (int i=1; i < args.Count; i++) {
                res /= (int) args[i].eval(ctx);
            }
            return res;
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class LessThanSexpFunction : BuiltInSexpFunction {
        public LessThanSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval(ctx) < (int)args[1].eval(ctx)) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }

        public override string toCS(List<String> list) {
			return list[0].ToString() + " < " + list[1].ToString();
		}
    }
    
    public class LessThanOrEqualsSexpFunction : BuiltInSexpFunction {
        public LessThanOrEqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval(ctx) <= (int)args[1].eval(ctx)) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class EqualsSexpFunction : BuiltInSexpFunction {
        public EqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval(ctx) == (int)args[1].eval(ctx)) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class GreaterThanOrEqualsSexpFunction : BuiltInSexpFunction {
        public GreaterThanOrEqualsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval(ctx) >= (int)args[1].eval(ctx)) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class GreaterThanSexpFunction : BuiltInSexpFunction {
        public GreaterThanSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((int)args[0].eval(ctx) > (int)args[1].eval(ctx)) {
                return new SexpSymbol("t");
            }
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class FirstSexpFunction : BuiltInSexpFunction {
        public FirstSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }
            
            if (!(args[0].eval(ctx) is SexpList sexpList)) {
                throw new ArgumentException();
            }

            if (sexpList.terms.Count == 0) {
                return new SexpList();
            }
            return sexpList.terms[0].eval(ctx);
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class RestSexpFunction : BuiltInSexpFunction {
        public RestSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }
            
            if (!(args[0].eval(ctx) is SexpList sexpList)) {
                throw new ArgumentException();
            }
            if (sexpList.terms.Count > 0) {
                sexpList.terms.RemoveAt(0);
            }
            return sexpList;

        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class ConsSexpFunction : BuiltInSexpFunction {
        public ConsSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }
            
            if (!(args[1].eval(ctx) is SexpList sexpList)) {
                throw new ArgumentException();
            }
            
            sexpList.terms.Insert(0, args[0].eval(ctx));
            return sexpList;

        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class EqualSexpFunction : BuiltInSexpFunction {
        public EqualSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            return args[0] == args[1] ? new SexpSymbol("t") : new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class SetqSexpFunction : BuiltInSexpFunction {
        public SetqSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if (!(args[0] is SexpSymbol symbol)) throw new ArgumentException();

            if (ctx.symtab.ContainsKey(symbol.value)) {
                ctx.symtab[symbol.value] = args[1].eval(ctx);
            }
            else {
                ctx.symtab.Add(symbol.value, args[1].eval(ctx));
            }

            return args[1].eval(ctx);
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class NullSexpFunction : BuiltInSexpFunction {
        public NullSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            return args[0].eval(ctx).is_null() ? new SexpSymbol("t") : new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class IfSexpFunction : BuiltInSexpFunction {
        public IfSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count < 2 || args.Count > 3) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((bool) args[0].eval(ctx)) {
                return args[1].eval(ctx);
            }

            if (args.Count == 3) {
                return args[2].eval(ctx);
            }
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class PrognSexpFunction : BuiltInSexpFunction {
        public PrognSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {

            Sexp res = new SexpSymbol("nil");

            foreach (var arg in args) {
                res = arg.eval(ctx);
            }
            
            return res;
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class PrincSexpFunction : BuiltInSexpFunction {
        public PrincSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            Sexp res = args[0].eval(ctx);
            Console.WriteLine(res);
            return res;
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class WhileSexpFunction : BuiltInSexpFunction {
        public WhileSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 2) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            Sexp res = new SexpSymbol("nil");

            while ((bool)args[0].eval(ctx)) {
                res = args[1].eval(ctx);
            }
            return res;
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class ShellSexpFunction : BuiltInSexpFunction {
        public ShellSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            string cmd = (string)args[0].eval(ctx);
            var escaped_args = cmd.Replace("\"", "\\\"");
            var process = new Process() {
                StartInfo = new ProcessStartInfo {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escaped_args}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            
            if (process.ExitCode != 0)
                throw new InvalidOperationException($"Process exited with {process.ExitCode}");
            return new SexpString(result);

        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class NotSexpFunction : BuiltInSexpFunction {
        public NotSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {
            if (args.Count != 1) {
                throw new ConstraintException("Too many or too few argument given.");
            }

            if ((bool)args[0].eval(ctx)) return new SexpSymbol("nil");
            return new SexpSymbol("t");
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class AndSexpFunction : BuiltInSexpFunction {
        public AndSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {

            Sexp res = new SexpSymbol("t");

            foreach (Sexp sexp in args) {
                res = sexp.eval(ctx);
                if (!(bool)res.eval(ctx)) return new SexpSymbol("nil");
            }
            
            return res;
        }
        public override string toCS(List<String> list) { return ""; }
    }
    
    public class OrSexpFunction : BuiltInSexpFunction {
        public OrSexpFunction(string name) : base(name) {}

        public override Sexp call(List<Sexp> args, Context ctx) {

            foreach (Sexp sexp in args) {
                if ((bool) sexp.eval(ctx)) {
                    return sexp.eval(ctx);
                }
            }
            
            return new SexpSymbol("nil");
        }
        public override string toCS(List<String> list) { return ""; }
    }
}