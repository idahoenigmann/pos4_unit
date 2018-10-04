using System;
using System.Text.RegularExpressions;

namespace i15013.lexer {
    public class Definition {
        private string type { get; set; }
        private Regex regex { get; set; }
        private bool is_ignored { get; set; }

        public Definition(string type, string regex, bool is_ignored) {
            this.type = type;
            this.regex = new Regex(regex, RegexOptions.Compiled);
            this.is_ignored = is_ignored;
        }
    }

    public struct Position {
        private int idx { get; set; };
        private int line_number { get; set; };
        private int column_number { get; set; };

        public Position(int idx, int line_number, int column_number) {
            this.idx = idx;
            this.line_number = line_number;
            this.column_number = column_number;
        }

        public override string ToString() {
            return $"{line_number}, {column_number}";
        }
    }
}