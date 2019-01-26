namespace i15013.logsimy.variables {
	public enum NotificationReason {
		changed,
		reset
	}

	public interface Observer {
    	void update(Variable v, NotificationReason r);
	}
	
    public sealed class Variable {
        public string name { get; set; }
        public bool value {
			get {
				return value;
			}
			set {
				if (value == this.value) return;
				if (notify != null) {
					notify(this, NotificationReason.changed);
				}
				this.value = value;
			}
		}

        public Variable(bool value, string name = "") {
            this.value = value;
            this.name = name;
        }

		public void reset(bool v=false) {
			if (v == this.value) return;
			if (notify != null) {
				notify(this, NotificationReason.reset);
			}
			this.value = v;	
		}

        public delegate void NotifyHandler(Variable v, NotificationReason r);

        public event NotifyHandler notify;
    }
}