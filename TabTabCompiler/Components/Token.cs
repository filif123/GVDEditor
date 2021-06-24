using System.Collections.Generic;

namespace TabTabCompiler.Components
{
    public enum TokenType
    {
        NONE,
        OPEN_PAREN,
        CLOSE_PAREN,
        STRING_LITERAL,
        OPERATOR,
        FONTSET_START,
        FONTSET_END,
        KEYWORD,
        IDENTIFIER,
        NEW_LINE_CONDITION,
        VIEW_VALUES,
        IGNORE_CASE,
        NUMBER,
        COMMA,
        EVENT_SWITCH,
        EVENT_VYLUKA,
        EVENT_ODKLON,
        EVENT_MERGE,
        EOF
    }

    public abstract class Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        protected Token(string text, TokenInfo info)
        {
            DelimitersBefore = new List<Trivia>();
            DelimitersAfter = new List<Trivia>();
            Text = text;
            Info = info;
        }

        public List<Trivia> DelimitersBefore { get; }

        public List<Trivia> DelimitersAfter { get; }

        public string Text { get; }

        public abstract TokenType Type { get; }

        public TokenInfo Info { get; }

        public virtual int FullLength
        {
            get
            {
                int len = 0;
                foreach (var trivia in DelimitersBefore)
                {
                    len += trivia.Length;
                }

                foreach (var trivia in DelimitersAfter)
                {
                    len += trivia.Length;
                }

                len += Text.Length;
                return len;
            }
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Type}: {Text}";
        }
    }

    public class StringLiteralToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public StringLiteralToken(string text, TokenInfo info) : base(text, info)
        {
        }

        public override TokenType Type => TokenType.STRING_LITERAL;
    }

    public class OperatorToken : Token
    {

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public OperatorToken(OperatorType type, TokenInfo info) : base(AsString(type), info)
        {
            Operator = type;
        }

        public static string AsString(OperatorType type)
        {
            switch (type)
            {
                case OperatorType.ASSIGMENT:
                    return "=";
                case OperatorType.PLUS:
                    return "+";
                case OperatorType.MINUS:
                    return "-";
                case OperatorType.EQUALS:
                    return "==";
                case OperatorType.NOT_EQUALS:
                    return "!=";
                case OperatorType.GREATER_THAN:
                    return ">";
                case OperatorType.SMALLER_THAN:
                    return "<";
                case OperatorType.GREATER_EQUALS_THAN:
                    return ">=";
                case OperatorType.SMALLER_EQUALS_THAN:
                    return "<=";
                case OperatorType.MULTIPLICATION:
                    return "*";
                case OperatorType.DIVISION:
                    return "/";
                case OperatorType.AND:
                    return "&&";
                case OperatorType.OR:
                    return "||";
                case OperatorType.XOR:
                    return "^";
                case OperatorType.AND_LITERAL:
                    return "AND";
                case OperatorType.OR_LITERAL:
                    return "OR";
                case OperatorType.NOT:
                    return "!";
                default:
                    return null;
            }
        }

        public static OperatorType GetOperator(string code, out int length)
        {
            if (code.Contains("=="))
            {
                length = 2;
                return OperatorType.EQUALS;
            }
            if (code.Contains("!="))
            {
                length = 2;
                return OperatorType.NOT_EQUALS;
            }
            if (code.Contains(">="))
            {
                length = 2;
                return OperatorType.GREATER_EQUALS_THAN;
            }
            if (code.Contains("<="))
            {
                length = 2;
                return OperatorType.SMALLER_EQUALS_THAN;
            }
            if (code.Contains("="))
            {
                length = 1;
                return OperatorType.ASSIGMENT;
            }
            if (code.Contains("+"))
            {
                length = 1;
                return OperatorType.PLUS;
            }
            if (code.Contains("-"))
            {
                length = 1;
                return OperatorType.MINUS;
            }
            if (code.Contains(">"))
            {
                length = 1;
                return OperatorType.GREATER_THAN;
            }
            if (code.Contains("<"))
            {
                length = 1;
                return OperatorType.SMALLER_THAN;
            }
            if (code.Contains("*"))
            {
                length = 1;
                return OperatorType.MULTIPLICATION;
            }
            if (code.Contains("/"))
            {
                length = 1;
                return OperatorType.DIVISION;
            }
            if (code.Contains("&&"))
            {
                length = 2;
                return OperatorType.AND;
            }
            if (code.Contains("||"))
            {
                length = 2;
                return OperatorType.OR;
            }
            if (code.Contains("^"))
            {
                length = 1;
                return OperatorType.XOR;
            }
            if (code.Contains("AND"))
            {
                length = 3;
                return OperatorType.AND_LITERAL;
            }
            if (code.Contains("OR"))
            {
                length = 2;
                return OperatorType.OR_LITERAL;
            }
            if (code.Contains("!"))
            {
                length = 1;
                return OperatorType.NOT;
            }

            length = 0;
            return OperatorType.NONE;
        }

        public override TokenType Type => TokenType.OPERATOR;

        public OperatorType Operator { get; }
    }

    public class IdentifierToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public IdentifierToken(string text, TokenInfo info) : base(text, info)
        {
        }

        public override TokenType Type => TokenType.IDENTIFIER;
    }

    public class NewLineConditionToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NewLineConditionToken(TokenInfo info) : base("\\", info)
        {
        }

        public override TokenType Type => TokenType.NEW_LINE_CONDITION;
    }

    public class ViewValuesToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ViewValuesToken(TokenInfo info) : base("ViewValues", info)
        {
        }

        public override TokenType Type => TokenType.VIEW_VALUES;
    }

    public class CommaToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CommaToken(TokenInfo info) : base(",", info)
        {
        }

        public override TokenType Type => TokenType.COMMA;
    }

    public class IgnoreCaseToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public IgnoreCaseToken(TokenInfo info) : base("IgnoreCase", info)
        {
        }

        public override TokenType Type => TokenType.IGNORE_CASE;
    }

    public class NumberLiteralToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NumberLiteralToken(string number, TokenInfo info) : base(number, info)
        {
        }

        public override TokenType Type => TokenType.NUMBER;
    }

    public class EventSwitchToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EventSwitchToken(TokenInfo info) : base("#SWITCH", info)
        {
        }

        public override TokenType Type => TokenType.EVENT_SWITCH;
    }

    public class EventVylukaToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EventVylukaToken(TokenInfo info) : base("#VYLUKA", info)
        {
        }

        public override TokenType Type => TokenType.EVENT_VYLUKA;
    }

    public class EventOdklonToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EventOdklonToken(TokenInfo info) : base("#ODKLON", info)
        {
        }

        public override TokenType Type => TokenType.EVENT_ODKLON;
    }

    public class EventMergeToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EventMergeToken(TokenInfo info) : base("#MERGE", info)
        {
        }

        public override TokenType Type => TokenType.EVENT_MERGE;
    }

    public class OpenParenToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public OpenParenToken(TokenInfo info) : base("(", info)
        {
        }

        public override TokenType Type => TokenType.OPEN_PAREN;
    }

    public class CloseParenToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CloseParenToken(TokenInfo info) : base(")", info)
        {
        }

        public override TokenType Type => TokenType.CLOSE_PAREN;
    }

    public class StartFontSetToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public StartFontSetToken(TokenInfo info) : base("{", info)
        {
        }

        public override TokenType Type => TokenType.FONTSET_START;
    }

    public class EndFontSetToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EndFontSetToken(TokenInfo info) : base("}", info)
        {
        }

        public override TokenType Type => TokenType.FONTSET_END;
    }

    public class EndOfTheFileToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EndOfTheFileToken(TokenInfo info) : base("",info)
        {
        }

        public override TokenType Type => TokenType.EOF;
    }
}