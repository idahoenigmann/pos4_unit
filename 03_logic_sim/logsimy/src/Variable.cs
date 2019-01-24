namespace i15013.logsimy {
    public class Variable {
        public string name { get; set; }
        public bool value { get; set; }

        public Variable(bool value, string name = "") {
            this.value = value;
            this.name = name;
        }
    }
}