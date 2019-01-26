namespace i15013.logsimy.variables {
    public sealed class Variable {
        public string name { get; set; }
        public bool value;

        public void set_value(bool value) {
            if (notify != null) {
                notify(value);
            }
        }
        
        public bool get_value() {
            return value;
        }

        public Variable(bool value, string name = "") {
            this.value = value;
            this.name = name;
        }

        public delegate void NotifyHandler(bool value);

        public event NotifyHandler notify;
    }
}