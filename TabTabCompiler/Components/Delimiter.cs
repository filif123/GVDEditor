namespace TabTabCompiler.Components
{

    public enum DelimiterType
    {
        SPACE,
        TAB,
        EOL,
        EOF
    }

    public enum TriviaType
    {
        DELIMITER,
        COMMENT
    }

    public abstract class Trivia
    {
        public abstract TriviaType TriviaType { get; }

        public int Length { get; protected set; }
    }

    public class Comment : Trivia
    {
        public override TriviaType TriviaType => TriviaType.COMMENT;

        public string Text { get; }

        public Comment(string text)
        {
            Text = text;
            Length = text.Length;
        }
    }

    public abstract class Delimiter : Trivia
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        protected Delimiter(int length)
        {
            Length = length;
        }

        public abstract DelimiterType Type { get; }

        public override TriviaType TriviaType => TriviaType.DELIMITER;
    }

    public class SpaceDelimiter : Delimiter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public SpaceDelimiter(int length) : base(length)
        {
        }

        public override DelimiterType Type => DelimiterType.SPACE;
    }

    public class TabDelimiter : Delimiter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TabDelimiter(int length) : base(length)
        {
        }

        public override DelimiterType Type => DelimiterType.TAB;
    }

    public class EOLDelimiter : Delimiter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EOLDelimiter(string eof) : base(eof.Length)
        {
        }

        public override DelimiterType Type => DelimiterType.EOL;
    }

    public class EOFDelimiter : Delimiter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EOFDelimiter() : base(0)
        {
        }

        public override DelimiterType Type => DelimiterType.EOF;
    }
}