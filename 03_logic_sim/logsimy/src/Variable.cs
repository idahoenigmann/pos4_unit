using System;

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
		private bool _value;
        public bool Value {
			get {
				return _value;
			}
			set {
				if (value == _value) return;
				_value = value;
				if (notify != null) {
					notify(this, NotificationReason.changed);
				}
			}
		}

        public Variable(bool value, string name = "") {
            _value = value;
            this.name = name;
        }

		public void reset(bool v=false) {
			//if (v == _value) return;
			_value = v;
			if (notify != null) {
				notify(this, NotificationReason.reset);
			}
		}

        public delegate void NotifyHandler(Variable v, NotificationReason r);

        public event NotifyHandler notify;
    }
}