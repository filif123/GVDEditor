using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda pre ulozenie CSV riadku
    /// </summary>
    public class CSVRow : List<string>
    {
        /// <summary>
        ///     Nespracovany text riadku
        /// </summary>
        public string LineText { get; set; }

        /// <inheritdoc />
        public CSVRow()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1" /> class that is empty and has the default initial capacity.</summary>
        public CSVRow(int initCount) : base(initCount)
        {
        }
    }

    /// <summary>
    ///     Trieda pre ulozenie dat do textoveho suboru
    /// </summary>
    public class CSVFileWriter : StreamWriter
    {
        /// <inheritdoc />
        public CSVFileWriter(string filename) : base(filename, false, GlobData.AnsiEncoding)
        {
        }

        /// <summary>
        ///     Zapise jeden riadok do suboru
        /// </summary>
        /// <param name="row">Riadok k zapisaniu</param>
        public void WriteRow(CSVRow row)
        {
            var builder = new StringBuilder();
            var firstColumn = true;
            foreach (var value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(',');

                builder.Append(value);
                firstColumn = false;
            }

            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }

        /// <summary>
        ///     Zapise komentar do suboru
        /// </summary>
        /// <param name="row">Komentar k zapisaniu</param>
        /// <param name="commentIndicator">Indikator komentaru, ktory sa zadava do suboru pred komentar</param>
        public void WriteComment(string row, char commentIndicator = ';')
        {
            WriteLine(commentIndicator + row);
        }
    }

    /// <summary>
    ///     Trieda pre citanie dat z textoveho suboru
    /// </summary>
    public class CSVFileReader : StreamReader
    {
        /// <inheritdoc />
        public CSVFileReader(string filename) : base(filename, GlobData.AnsiEncoding)
        {
        }

        /// <summary>
        ///     Precita jeden riadok zo suboru
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public ReadStartChar ReadRow(CSVRow row)
        {
            row.LineText = ReadLine();

            if (row.LineText == null) return ReadStartChar.EOF;
            if (string.Equals(row.LineText, string.Empty)) return ReadStartChar.EMPTY;
            if (row.LineText.StartsWith(";")) return ReadStartChar.SEMICOLON;
            if (row.LineText.StartsWith("/")) return ReadStartChar.SLASH;

            var pos = 0;
            var rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    var start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }

                        pos++;
                    }

                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    var start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }

            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            return ReadStartChar.NON_EMPTY;
        }
    }

    /// <summary>
    ///     Typ znaku na zaciatku riadku
    /// </summary>
    public enum ReadStartChar
    {
        /// <summary>
        ///     Neprazdny riadok
        /// </summary>
        NON_EMPTY,
        
        /// <summary>
        ///     Prazdny riadok
        /// </summary>
        EMPTY,

        /// <summary>
        ///     Bodkociarka
        /// </summary>
        SEMICOLON,

        /// <summary>
        ///     #
        /// </summary>
        SLASH,

        /// <summary>
        ///     Znak konca suboru
        /// </summary>
        EOF
    }
}