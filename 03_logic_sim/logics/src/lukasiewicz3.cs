using System;

namespace i15013.logics.lukasiewicz3 {
    public static class Operators {
        public static bool? conj(bool? a, bool? b) {
            try {
                if (!a | !b) {
                    return false;
                }
            }
            catch {
                return null;
            }
            return true;
        }

        public static bool? disj(bool? a, bool? b) {
            try {
                if (a) {
                    return true;
                }
            }
            catch {
                if (b) return true;
                return null;
            }
            
            try {
                if (b) {
                    return true;
                }
            }
            catch {
                if (a) return true;
                return null;
            }
            
            return false;
        }

        public static bool? neg(bool? a) {
            try {
                if a return false;
                return true;
            }
            catch {
                return null;
            }
        }

        public static bool? anti(bool? a, bool? b) {
            return (Math.min(Math.Max(a, b), 1 - (Math.min(a, b))));
        }

        public static bool? imp(bool? a, bool? b) {
            return Math.min(1, 1 + b - a);
        }

        public static bool? equiv(bool? a, bool? b) {
            return 1 - Math.Abs(a - b);
        }
    }
}