using TabTabCompiler.Tools;

namespace TabTabCompiler.Components
{
    public enum OutputType
    {
        NONE,
        HINT,
        WARNING,
        ERROR
    }

    public abstract class CodeOutput
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        protected CodeOutput(int position, int line, int column, int length, string text)
        {
            Line = line;
            Position = position;
            Length = length;
            Text = text;
            Column = column;
        }

        public abstract OutputType Type { get; }

        public int Line { get; }

        public int Position { get; }

        public int Column { get; }

        public int Length { get; }

        public string Text { get; set; }
    }

    public class ErrorOutput : CodeOutput
    {
        public override OutputType Type => OutputType.ERROR;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ErrorOutput(int position, int line, int column, int length, string text) : base(position, line, column, length, text)
        {
        }

        public ErrorOutput(MyStringReader strReader, string text) : base(strReader.Position, strReader.Row, strReader.Column, strReader.Length, text)
        {
        }

        public ErrorOutput(TokenInfo info, string text) : this(info.Position, info.Row, info.Column, info.Length, text)
        {
        }
    }

    public class WarningOutput : CodeOutput
    {
        public override OutputType Type => OutputType.WARNING;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WarningOutput(int position, int line, int column, int length, string text) : base(position, line, column, length, text)
        {
        }
    }

    public class HintOutput : CodeOutput
    {
        public override OutputType Type => OutputType.HINT;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public HintOutput(int position, int line, int column, int length, string text) : base(position, line, column, length, text)
        {
        }
    }
}
