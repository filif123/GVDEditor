using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Definuje sposob zadavania informacii do sekcie katalogovej tabule
    /// </summary>
    public sealed class TableDivType
    {
        #region VALUES

        /// <summary>
        ///     Zadanie ľub. textu (bez TAB1 a TAB2)
        /// </summary>
        public static readonly TableDivType Free = new(0, "0: Zadanie ľub. textu (bez TAB1 a TAB2)");

        /// <summary>
        ///     Výber z TabTab (TAB1 povinné)
        /// </summary>
        public static readonly TableDivType Table = new(1, "1: Výber z TabTab (TAB1 povinné)");

        /// <summary>
        ///     ? (TAB1 a TAB2 povinné)
        /// </summary>
        public static readonly TableDivType TableTime = new(2, "2: ? (TAB1 a TAB2 povinné)");

        /// <summary>
        ///     Zadanie ľub. textu (TAB1 povinné)
        /// </summary>
        public static readonly TableDivType Translate = new(3, "3: Zadanie ľub. textu (TAB1 povinné)");

        /// <summary>
        ///     ? (TAB1 povinné)
        /// </summary>
        public static readonly TableDivType Char = new(4, "4: ? (TAB1 povinné)");

        #endregion

        private TableDivType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     ID typu
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Nazov typu
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Skonvertuje ID DivType na objekt
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TableDivType Parse(int s)
        {
            return s switch
            {
                0 => Free,
                1 => Table,
                2 => TableTime,
                3 => Translate,
                4 => Char,
                _ => null
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Vrati všetky typy zadania do tabule
        /// </summary>
        /// <returns></returns>
        public static List<TableDivType> GetValues()
        {
            var types = new List<TableDivType>
            {
                Free,
                Table,
                TableTime,
                Translate,
                Char
            };
            return types;
        }
    }
}