using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Typ stĺpca pre imporovanie dát
    /// </summary>
    public sealed class ImportTrainColumnType
    {
        #region VALUES

#pragma warning disable 1591
        public static readonly ImportTrainColumnType None = new("-");
        public static readonly ImportTrainColumnType Number = new("Číslo");
        public static readonly ImportTrainColumnType Type = new("Typ");
        public static readonly ImportTrainColumnType Variant = new("Varianta");
        public static readonly ImportTrainColumnType Name = new("Názov");
        public static readonly ImportTrainColumnType Prichod = new("Príchod");
        public static readonly ImportTrainColumnType Odchod = new("Odchod");
        public static readonly ImportTrainColumnType DateRemText = new("Dátum. obm. (text)");
        public static readonly ImportTrainColumnType DateRemBitArray = new("Dátum. obm. (bitarray)");
        public static readonly ImportTrainColumnType PlatnostOd = new("Platnosť od");
        public static readonly ImportTrainColumnType PlatnostDo = new("Platnosť do");
        public static readonly ImportTrainColumnType DopravcaId = new("Dopravca (ID)");
        public static readonly ImportTrainColumnType DopravcaName = new("Dopravca (Názov)");
        public static readonly ImportTrainColumnType Track = new("Koľaj");
        public static readonly ImportTrainColumnType Languages = new("Jazyky");
        public static readonly ImportTrainColumnType LinkaOdchod = new("Linka (odchod)");
        public static readonly ImportTrainColumnType LinkaPrichod = new("Linka (príchod)");
        public static readonly ImportTrainColumnType Routing = new("Smerovanie");
        public static readonly ImportTrainColumnType AllStationsID = new("Všetky stanice (ID stanice)");
        public static readonly ImportTrainColumnType StationsShortID = new("Stanice (krátke hlásenie) (ID stanice)");
        public static readonly ImportTrainColumnType StationsLongID = new("Stanice (dlhé hlásenie) (ID stanice)");
        public static readonly ImportTrainColumnType AllStationsName = new("Všetky stanice (stanice stanice)");
        public static readonly ImportTrainColumnType StationsShortName = new("Stanice (krátke hlásenie) (názov stanice)");
        public static readonly ImportTrainColumnType StationsLongName = new("Stanice (dlhé hlásenie) (názov stanice)");
        public static readonly ImportTrainColumnType Attributes = new("Vlastnosti vlaku");
#pragma warning restore 1591

        #endregion

        private ImportTrainColumnType(string name)
        {
            ColumnName = name;
        }

        /// <summary>
        ///     Názov stĺpca
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        ///     Vráti všetky dostupné typy stĺpcov pre importovanie dát
        /// </summary>
        /// <returns>všetky dostupné typy stĺpcov</returns>
        public static List<ImportTrainColumnType> GetValues()
        {
            return new()
            {
                Number,
                Type,
                Variant,
                Name,
                Prichod, Odchod,
                DateRemText, DateRemBitArray,
                PlatnostOd, PlatnostDo,
                DopravcaId, DopravcaName,
                Track,
                Languages,
                LinkaOdchod, LinkaPrichod,
                Routing,
                AllStationsID, StationsLongID, StationsShortID,
                AllStationsName, StationsLongName, StationsShortName,
                Attributes
            };
        }

        /// <summary>
        ///     Vráti všetky požadované typy stĺpcov pre importovanie dát
        /// </summary>
        /// <returns>všetky požadované typy stĺpcov</returns>
        public static List<ImportTrainColumnType> GetRequiredValues()
        {
            return new()
            {
                Number,
                Type,
                Variant,
                Prichod, Odchod,
                Track
            };
        }

        /// <summary>
        ///     Konvertuje názov stĺpca na objekt
        /// </summary>
        /// <param name="name">názov stĺpca ako reťazec</param>
        /// <returns>objekt <see cref="ImportTrainColumnType" /> alebo <see cref="None" /> ak sa nenašla žiadna zhoda</returns>
        public static ImportTrainColumnType ParseColumnName(string name)
        {
            if (string.IsNullOrEmpty(name)) return None;
            switch (name.ToLower().Replace("-", ""))
            {
                case "číslo":
                case "cislo":
                case "number":
                    return Number;
                case "typ":
                case "type":
                    return Type;
                case "variant":
                case "varianta":
                    return Variant;
                case "názov":
                case "nazov":
                case "název":
                case "nazev":
                case "názov vlaku":
                case "nazov vlaku":
                case "název vlaku":
                case "nazev vlaku":
                case "name":
                    return Name;
                case "príchod":
                case "příjezd":
                case "prichod":
                case "arrival":
                    return Prichod;
                case "odchod":
                case "odjezd":
                case "departure":
                    return Odchod;
                case "daterem":
                case "datum. obm.":
                case "dátum. obm.":
                case "datum. om.":
                case "dátumové obmedzenie":
                case "datumové omezení":
                    return DateRemText;
                case "platnosť od":
                case "platnost od":
                    return PlatnostOd;
                case "platnosť do":
                case "platnost do":
                    return PlatnostDo;
                case "dopravca id":
                case "dopravce id":
                case "id dopravca":
                case "id dopravce":
                    return DopravcaId;
                case "dopravca":
                case "dopravce":
                case "název dopravce":
                case "názov dopravcu":
                    return DopravcaName;
                case "kolaj":
                case "kolej":
                case "track":
                    return Track;
                case "jazyky":
                case "languages":
                case "jazyk":
                case "language":
                    return Languages;
                case "linka odchod":
                case "linka odjezd":
                    return LinkaOdchod;
                case "linka príchod":
                case "linka příjezd":
                case "linka prijezd":
                case "linka prichod":
                    return LinkaPrichod;
                case "routing":
                case "smerovanie":
                case "směrování":
                case "smerovani":
                    return Routing;
                case "trasy":
                case "všetky stanice":
                case "všechny stanice":
                case "vsetky stanice":
                case "vsechny stanice":
                case "all stations":
                    return AllStationsID;
                case "stahlasb":
                case "stanice kratke hlasenie":
                case "stanice krátke hlásenie":
                case "stanice kratke hlaseni":
                case "stanice krátke hlášeni":
                case "stanice kratke":
                case "stanice krátke":
                    return StationsShortID;
                case "stahlasc":
                case "stanice dlhe hlasenie":
                case "stanice dlhé hlásenie":
                case "stanice dlouhe hlaseni":
                case "stanice dlhé hlášeni":
                case "stanice dlhe":
                case "stanice dlhé":
                case "stanice dlouhe":
                case "stanice dlouhé":
                    return StationsLongID;
                case "attributes":
                case "atributy":
                case "atribúty":
                    return Attributes;
                default:
                    return None;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ColumnName;
        }
    }
}