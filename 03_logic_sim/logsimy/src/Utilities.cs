using System;

namespace i15013.logsimy.variables {
    public class Utilities {
        public static void connect(Variable v1, Variable v2) {
            v1.notify += ((v, r) => v2.value = v.value);
        }

		public static void enable_logging(Variable v) {
			v.notify += ((w, r) => Console.WriteLine(v.name + ": " + v.value + " (" + r + ")"));
		}
    }
}