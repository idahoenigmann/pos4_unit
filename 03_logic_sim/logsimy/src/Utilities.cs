namespace i15013.logsimy.variables {
    public class Utilities {
        public static void connect(Variable v1, Variable v2) {
            v1.notify += (v => v2.value = v);
        }
    }
}