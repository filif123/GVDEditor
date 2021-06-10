using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Trieda zastesujuca texty na tabuliach
    /// </summary>
    public sealed class TableText : ITable
    {
        /// <summary>
        ///     Kluc typu textu na tabuliach
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     Nazov typu textu na tabuliach
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Realizacie tohto typu textu na tabuliach
        /// </summary>
        public List<TableTextRealization> Realizations { get; set; }

        /// <summary>
        ///     Vlaky (<see cref="TableTrain"/>), ktore maju tento typ textu na tabuliach
        /// </summary>
        public List<TableTrain> Trains { get; set; }

        /// <summary>
        ///     Komentar ku danemu typu textu na tabuliach
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public TableText()
        {
            Realizations = new List<TableTextRealization>();
            Trains = new List<TableTrain>();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}
