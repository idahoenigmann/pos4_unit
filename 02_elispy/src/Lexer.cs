using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace i15013.lexer {
    public class Definition {
        public string type { get; }
        public Regex regex { get; }
        public bool is_ignored { get; }

        public Definition(string type, string regex, bool is_ignored) {
            this.type = type;
            this.regex = new Regex(regex, RegexOptions.Compiled);
            this.is_ignored = is_ignored;
        }
    }

    public struct Position {
        public int idx { get; set; }
        public int line_number { get; set; }
        public int column_number { get; set; }

        public Position(int idx, int line_number, int column_number) {
            this.idx = idx;
            this.line_number = line_number;
            this.column_number = column_number;
        }

        public override string ToString() {
            return $"{line_number}, {column_number}";
        }
    }

    public struct Token {
        public string type { get; private set; }
        public string value { get; private set; }
        public Position position { get; private set; }

        public Token(string type, string value, Position position) {
            this.value = value.Replace(@"""", "");
            this.position = position;
            this.type = type;
        }

        public override string ToString() {
            return $"{type}: {value} at {position}";
        }
    }

    public class LexerException : Exception {
        public LexerException() : base() { }
        public LexerException(string message) : base(message) { }

        public LexerException(string message, System.Exception inner) : base(
            message, inner) { }
    }

    public interface ILexer {
        void add_definition(Definition def);
        IEnumerable<Token> tokenize(string source);
    }

    public class Lexer : ILexer {
        public void add_definition(Definition def) {
            definitions.Add(def);
        }

        public IEnumerable<Token> tokenize(string source) {
            int idx = 0;
            int row = 0;
            int col = 0;
            Position position = new Position(0, 0, 0);

            bool ignore = false;
            bool found_def = false;
            
            while (idx < source.Length)
            {
                string type = "";
                string value = "";
                
                position.idx = idx;
                position.line_number = row;
                position.column_number = col;

                foreach (Definition def in definitions) {
                    ignore = false;
                    found_def = false;
                    Match match = def.regex.Match(source, idx);
                    if (match.Success && match.Index == idx) {
                        type = def.type;
                        value = source.Substring(match.Index, match.Length);
                        idx = match.Index + match.Length;
                        col = col + match.Length;

                        if (def.is_ignored) {
                            ignore = true;
                        }

                        found_def = true;
                        break;
                    }
                }

                if (!found_def) {
                    string message = $"Invalid Token at ({row}, {col})\n" + source;
                    for (int i = 0; i < row; i++) {
                        message = message.Substring('\n');
                    }

                    message += "\n";
                    
                    for (int i = 0; i < col; i++) {
                        message += " ";
                    }

                    message += "^";

                    throw new LexerException(message);
                }
                
                if (value.Contains("\n")) {
                    row++;
                    col = 0;
                }

                if (!ignore) {
                    yield return new Token(type, value, position);
                }
            }
        }

        private static List<Definition> definitions = new List<Definition>();
    }
}