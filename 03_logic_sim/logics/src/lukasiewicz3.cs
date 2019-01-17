using System;

namespace i15013.logics.lukasiewicz3 {
    public static class Operators {
        public static bool? conj(bool? a, bool? b) {
            if (a is null & b is null) {
				return null;
			}
			if (a is null) {
				return b.Value ? null : b;
			}
			if (b is null) {
				return a.Value ? null : a;
			}
			return a.Value & b.Value;
        }

        public static bool? disj(bool? a, bool? b) {
            if (a is null & b is null) {
				return null;
			}
			if (a is null) {
				return b.Value ? b : null;
			}
			if (b is null) {
				return a.Value ? a : null;
			}
			return a.Value | b.Value;
        }

        public static bool? neg(bool? a) {
			if (a is null) {
				return null;
			}
			return !a.Value;
        }

        public static bool? anti(bool? a, bool? b) {
            return false;
        }

        public static bool? imp(bool? a, bool? b) {
            return false;
        }

        public static bool? equiv(bool? a, bool? b) {
            if (a is null & b is null) {
				return true;
			}
			if (a is null) {
				return false;
			}
			if (b is null) {
				return false;
			}
			return a.Value == b.Value;
        }
    }
}