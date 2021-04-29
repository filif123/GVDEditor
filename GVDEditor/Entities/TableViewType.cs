using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Druh tabule
    /// </summary>
    public sealed class TableViewType
    {
        /// <summary>
        ///     Odchodová tabuľa
        /// </summary>
        public static readonly TableViewType Odchodova = new("Tabule_Odjezdova", "Odchodová");

        /// <summary>
        ///     Príchodová tabuľa
        /// </summary>
        public static readonly TableViewType Prichodova = new("Tabule_Prijezdova", "Prichodová");

        /// <summary>
        ///     Nástupištná tabuľa
        /// </summary>
        public static readonly TableViewType Nastupistna = new("Tabule_Nastupistni", "Nástupištná");

        /// <summary>
        ///     Podchodová tabuľa
        /// </summary>
        public static readonly TableViewType Podchodova = new("Tabule_Podchodova", "Podchodová");

        /// <summary>
        ///     Reklamná tabuľa
        /// </summary>
        public static readonly TableViewType Reklamna = new("Tabule_Reklamni", "Reklamná");

        /// <summary>
        ///     Iný druh tabule
        /// </summary>
        public static readonly TableViewType Ina = new("Tabule_JinyDruh", "Iná");

        private TableViewType(string key, string name)
        {
            Key = key;
            Name = name;
        }

        /// <summary>
        ///     Kľúč druhu tabule
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Názov druhu tabule
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Tento objekt (použitie pre GUI)
        /// </summary>
        public TableViewType This => this;

        /// <summary>
        ///     Konveruje kľúč druhu tabule na objekt
        /// </summary>
        /// <param name="s">kľúč druhu tabule</param>
        /// <returns>druh tabule alebo <see langword="null" /> ak sa nenašla zhoda</returns>
        public static TableViewType Parse(string s)
        {
            return s switch
            {
                "Tabule_Odjezdova" => Odchodova,
                "Tabule_Prijezdova" => Prichodova,
                "Tabule_Nastupistni" => Nastupistna,
                "Tabule_Podchodova" => Podchodova,
                "Tabule_Reklamni" => Reklamna,
                "Tabule_JinyDruh" => Ina,
                _ => null
            };
        }

        /// <summary>
        ///     Vrati zoznam vsetkych TableViewType
        /// </summary>
        /// <returns></returns>
        public static List<TableViewType> GetValues()
        {
            var types = new List<TableViewType>
            {
                Odchodova,
                Prichodova,
                Nastupistna,
                Podchodova,
                Reklamna,
                Ina
            };
            return types;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is TableViewType typ) return Key == typ.Key;

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Key != null ? Key.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:GVDEditor.Entities.TableViewType" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(TableViewType left, TableViewType right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:GVDEditor.Entities.TableViewType" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(TableViewType left, TableViewType right)
        {
            return !Equals(left, right);
        }
    }
}