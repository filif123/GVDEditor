using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Typ písma pre tabule
    /// </summary>
    public sealed class TableFontType
    {
        #region VALUES

        /// <summary>
        ///     Obyčajné písmo
        /// </summary>
        public static readonly TableFontType None = new("", "Normálne");

        /// <summary>
        ///     Tučné písmo
        /// </summary>
        public static readonly TableFontType Bold = new("Tučný", "Tučné písmo");

        /// <summary>
        ///     Šikmé písmo (kurzíva)
        /// </summary>
        public static readonly TableFontType Italics = new("Šikmý", "Šikmé písmo");

        /// <summary>
        ///     Tučné a šikmé písmo
        /// </summary>
        public static readonly TableFontType BoldItalics = new("Šikmý a tučný", "Šikmé a tučné písmo");

        #endregion

        private TableFontType(string key, string name)
        {
            Key = key;
            Name = name;
        }

        /// <summary>
        ///     Kľúč typu písma
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Naźov typu písma
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     This
        /// </summary>
        public TableFontType This => this;

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TableFontType Parse(string type)
        {
            return type switch
            {
                "" => None,
                "Tučný" => Bold,
                "Šikmý" => Italics,
                "Šikmý a tučný" => BoldItalics,
                _ => null
            };
        }

        /// <summary>
        ///     Vráti všetky možné typy písma pre tabule
        /// </summary>
        /// <returns></returns>
        public static List<TableFontType> GetValues()
        {
            return new() {None, Bold, Italics, BoldItalics};
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}