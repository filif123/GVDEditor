using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Vyrobca tabule, ktory sa definuje v katalogovej tabuli
    /// </summary>
    public sealed class TableManufacturer
    {
#pragma warning disable 1591
        public static readonly TableManufacturer ELEN = new("ELEN");
        public static readonly TableManufacturer ELEN10 = new("ELEN10");
        public static readonly TableManufacturer ELENOLD = new("ELENOLD");
        public static readonly TableManufacturer ELEN16 = new("ELEN16");
        public static readonly TableManufacturer ELEN16Kam = new("ELENKam16");
        public static readonly TableManufacturer LCD1 = new("LCD1");
        public static readonly TableManufacturer ELEKTROCAS = new("Elektrocas");
        public static readonly TableManufacturer Pragotron = new("Pragotron");
        public static readonly TableManufacturer ERS = new("ERS");
        public static readonly TableManufacturer FERS = new("FERS");
#pragma warning restore 1591

        private TableManufacturer(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Nazov typu tabule
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Odkaz na seba, pouzivane pre DataSource
        /// </summary>
        public TableManufacturer This => this;

        /// <summary>
        ///     Konvertuje textove vyjadrenie nazvu typu tabule na objekt. V pripade, ak sa to nepodari, metoda vrati <see langword="null"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TableManufacturer Parse(string s)
        {
            return s switch
            {
                "ELEN" => ELEN,
                "ELEN10" => ELEN10,
                "ELENOLD" => ELENOLD,
                "ELEN16" => ELEN16,
                "ELEN16Kam" => ELEN16Kam,
                "Elektrocas" => ELEKTROCAS,
                "LCD1" => LCD1,
                "Pragotron" => Pragotron,
                "ERS" => ERS,
                "FERS" => FERS,
                _ => null
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Vrati zoznam vsetkych moznych typov tabuli
        /// </summary>
        /// <returns></returns>
        public static List<TableManufacturer> GetValues()
        {
            var types = new List<TableManufacturer>
            {
                ELEN,
                ELEN10,
                ELENOLD,
                ELEN16,
                ELEN16Kam,
                ELEKTROCAS,
                LCD1,
                Pragotron,
                ERS,
                FERS
            };
            return types;
        }
    }
}