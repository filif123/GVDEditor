using TabTabCompiler.Tools;

namespace TabTabCompiler.Components
{
    public readonly struct TokenInfo
    {
        public int Position { get; }

        public int Row { get; }

        public int Column { get; }

        public int Length { get; }

        public TokenInfo(MyStringReader reader)
        {
            Position = reader.Position;
            Row = reader.Row;
            Column = reader.Column;
            Length = reader.Length;
        }

        public static implicit operator TokenInfo(MyStringReader reader)
        {
            return new(reader);
        }
    }
}
