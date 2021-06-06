using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Zarovnanie tabule
    /// </summary>
    public sealed class TableAlign
    {
        #region VALUES
        
        /// <summary>
        ///     Zarovnanie vľavo
        /// </summary>
        public static readonly TableAlign Left = new(0, "Vľavo");

        /// <summary>
        ///     Zarovnanie na stred
        /// </summary>
        public static readonly TableAlign Center = new(1, "Do stredu");

        /// <summary>
        ///     Zarovnanie vpravo
        /// </summary>
        public static readonly TableAlign Right = new(2, "Vpravo");
        
        #endregion

        private TableAlign(int id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        ///     Identifikátor zarovnania
        /// </summary>
        public int ID { get; }

        /// <summary>
        ///     Názov zarovnania
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Konveruje identifikátor tabule ako <see cref="int" /> na <see cref="TableAlign" />
        /// </summary>
        /// <param name="s"></param>
        /// <returns><see cref="TableAlign" /> alebo <see langword="null" /> ak sa nenašla žiadna zhoda</returns>
        public static TableAlign Parse(int s)
        {
            return s switch
            {
                0 => Left,
                1 => Center,
                2 => Right,
                _ => null
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Vráti všetky dostupné zarovnania tabule
        /// </summary>
        /// <returns>zoznam možných zarovnaní obsahu tabule</returns>
        public static List<TableAlign> GetValues()
        {
            var types = new List<TableAlign>
            {
                Left,
                Center,
                Right
            };
            return types;
        }
    }
}