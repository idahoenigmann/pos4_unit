using System;

namespace i15013.logsimy.variables {
    public class Utilities {
        public static void connect(Variable v1, Variable v2) {
            v1.notify += ((v, r) => v2.Value = v.Value);
        }

		public static void enable_logging(Variable v) {
			v.notify += ((w, r) => Console.WriteLine(v.name + ": " + v.Value + " (" + r + ")"));
		}

		public static void inform(Variable src, Observer obs) {
			src.notify += obs.update;
		}
    }
}