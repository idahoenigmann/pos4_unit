using System;

namespace i15013.logics.lukasiewiczn {
    public class Operators {
        public static double conj(double op1, double op2) {
            return Math.Min(op1, op2);
        }
        
        public static double disj(double op1, double op2) {
            return Math.Max(op1, op2);
        }
        
        public static double neg(double op1) {
            return 1 - op1;
        }
        
        public static double anti(double op1, double op2) {
            return Operators.conj(Operators.disj(op1, op2), Operators.neg(Operators.conj(op1, op2)));
            //return disj(conj(neg(op1), op2), conj(op1, neg(op2)));
        }
        
        public static double imp(double op1, double op2) {
            return Math.Min(1, 1 + op2 - op1);
        }
        
        public static double equiv(double op1, double op2) {
            return 1 - Math.Abs(op1 - op2);
        }
    }
}