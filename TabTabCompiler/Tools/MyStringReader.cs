using System.Text;

namespace TabTabCompiler.Tools
{
    public class MyStringReader
    {
        private readonly string text;

        public int Position { get; private set; }

        public int Row { get; private set; }

        public int Column { get; private set; }

        public int Length => text.Length;

        private bool newLine;

        public MyStringReader(string s)
        {
            text = s;
            Position = -1;
        }

        /// <summary>Reads the next character from the input string and advances the character position by one character.</summary>
        /// <returns>The next character from the underlying string, or -1 if no more characters are available.</returns>
        /// <exception cref="T:System.ObjectDisposedException">The current reader is closed.</exception>
        public int Read()
        {
            if (Position + 1 >= Length)
            {
                return -1;
            }

            var result = text[++Position];
            
            if (result == '\n')
            {
                Row++;
                newLine = true;
            }
            else
            {
                Column++;

                if (newLine)
                {
                    Column = 0;
                    newLine = false;
                }
            }
            return result;
        }

        /// <summary>Reads a line of characters from the current string and returns the data as a string.</summary>
        /// <returns>The next line from the current string, or <see langword="null" /> if the end of the string is reached.</returns>
        /// <exception cref="T:System.ObjectDisposedException">The current reader is closed.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        public string ReadLine()
        {
            var sb = new StringBuilder();
            int i = Position;
            while (i + 1 >= text.Length || text[i] == '\n')
            {
                sb.Append(text[i]);
            }

            var str = sb.ToString();
            str = str.Replace("\r", "");
            Position += str.Length;
            Row++;
            return str;
        }

        public int PeekNext()
        {
            if (Position + 1 >= Length)
            {
                return -1;
            }

            var result = text[Position + 1];
            return result;
        }

        public string PeekNext(int length)
        {
            int i = 0;
            var builder = new StringBuilder(length);
            while (i < length)
            {
                int resi = PeekNext();
                if (resi == -1)
                {
                    Position -= i;
                    return builder.ToString();
                }

                builder.Append((char) resi);
                i++;
                Position++;
            }

            Position -= i;
            return builder.ToString();
        }

        public string Read(int length)
        {
            int i = 0;
            var builder = new StringBuilder(length);
            while (i < length)
            {
                int resi = Read();
                if (resi == -1)
                {
                    return builder.ToString();
                }

                builder.Append((char)resi);
                i++;
            }

            return builder.ToString();
        }
    }
}
