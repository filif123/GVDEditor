using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Typ priecinka v zvukovej banke
    /// </summary>
    public sealed class FyzZvukDirType
    {
        #region VALUES

#pragma warning disable 1591
        public static readonly FyzZvukDirType UNCATEGORIZED = new("Nezaradené", "");
        public static readonly FyzZvukDirType C1 = new("C1", "hodiny");
        public static readonly FyzZvukDirType C2 = new("C2", "minúty, napr.: 2 minúty.");
        public static readonly FyzZvukDirType C3 = new("C3", "minúty, napr.: 2 minúty,");
        public static readonly FyzZvukDirType C9 = new("C9", "minúty pri meškaní");
        public static readonly FyzZvukDirType CISLO_ = new("CISLO_", "číslo s čiarkou, napr.: šesť,");
        public static readonly FyzZvukDirType CISLO1 = new("CISLO1", "číslo, napr.: šesť");
        public static readonly FyzZvukDirType CISLO2 = new("CISLO2", "číslo s predponami, napr.: 01");
        public static readonly FyzZvukDirType CISLO3 = new("CISLO3", "číslo na konci vety, napr.: šesť.");
        public static readonly FyzZvukDirType CISLO4 = new("CISLO4", "číslo so spojkou \"a\",napr.: a šesť");
        public static readonly FyzZvukDirType CISLO5 = new("CISLO5", "číslo so spojkou \"a\" s čiarkou,napr.: a šesť,");
        public static readonly FyzZvukDirType CISLO6 = new("CISLO6", "číslo so spojkou \"a\" na konci vety,napr.: a šesť.");
        public static readonly FyzZvukDirType CISLO7 = new("CISLO7", "číslo so spojkou \"až\",napr.: až šesť");
        public static readonly FyzZvukDirType CISLO8 = new("CISLO8", "číslo so spojkou \"až\",napr.: až šesť,");
        public static readonly FyzZvukDirType CISLO9 = new("CISLO9", "číslo so spojkou \"až\" na konci vety,napr.: až šesť.");
        public static readonly FyzZvukDirType DODATKY = new("DODATKY", "dodatkové hlásenia");
        public static readonly FyzZvukDirType DOPRAVCA = new("DOPRAVCA/O1", "názvy dopravcov");
        public static readonly FyzZvukDirType DZ = new("DZ", "dôvody meškania");
        public static readonly FyzZvukDirType K1 = new("K1", "názov koľaje na konci vety, napr.: koľaj dva.");
        public static readonly FyzZvukDirType K2 = new("K2", "názov koľaje s čiarkov, napr.: koľaj dva,");
        public static readonly FyzZvukDirType K3 = new("K3", "názov koľaje s \"Po\", napr.: Po koľaji dva");
        public static readonly FyzZvukDirType K4 = new("K4", "názov koľaje  s predložkou \"pri\", napr.: pri koľaji dva");
        public static readonly FyzZvukDirType LINKA = new("LINKA", "názvy liniek");
        public static readonly FyzZvukDirType N1 = new("N1", "názov nástupišta s predložkou \"k\", napr.: k nástupišťu dva,");
        public static readonly FyzZvukDirType N2 = new("N2", "názov nástupišta s predložkou \"pri\", napr.: pri nástupišti dva,");
        public static readonly FyzZvukDirType N3 = new("N3", "názov nástupišta s predložkou \"na\", napr.: na nástupišTE dva,");
        public static readonly FyzZvukDirType N4 = new("N4", "názov nástupišta s predložkou \"Pri\", napr.: Pri nástupišti jedna");
        public static readonly FyzZvukDirType N5 = new("N5", "názov nástupišta s predložkou \"na\", napr.: na nástupišTI dva,");
        public static readonly FyzZvukDirType N10 = new("N10", "názov koľaje s \"na\" na konci vety (bez nástupíšť), napr.: na koľaj dva.");
        public static readonly FyzZvukDirType N11 = new("N11", "názov koľaje s \"na\" s čiarkou (bez nástupíšť), napr.: na koľaj dva,");
        public static readonly FyzZvukDirType N12 = new("N12", "názov koľaje s \"Pri\" na začiatku vety (bez nástupíšť), napr.: Pri koľaji dva");
        public static readonly FyzZvukDirType N13 = new("N13", "názov koľaje s \"na\" na konci vety (bez nástupíšť), napr.: na koľaJI dva.");
        public static readonly FyzZvukDirType POZ1 = new("Poz1", "poznámky k vlaku, napr.: na konci vlaku");
        public static readonly FyzZvukDirType POZ2 = new("Poz2", "poznámky k vlaku s čiarkou, napr.: na konci vlaku,");
        public static readonly FyzZvukDirType POZ3 = new("Poz3", "poznámky k vlaku na konci vety, napr.: na konci vlaku.");
        public static readonly FyzZvukDirType POZ7 = new("Poz7", "poznámky k vlaku, napr.: na konci vlaku");
        public static readonly FyzZvukDirType R1 = new("R1", "názov stanice s čiarkou, napr.: Abda,");
        public static readonly FyzZvukDirType R2 = new("R2", "názov stanice so spojkou \"a\" s čiarkou, napr.: a Abda,");
        public static readonly FyzZvukDirType R3 = new("R3", "názov stanice napr.: Abda");
        public static readonly FyzZvukDirType R4 = new("R4", "názov stanice na konci vety.: Abda.");
        public static readonly FyzZvukDirType RAZENI = new("ŘAZENÍ", "celé hlásenie o radení");
        public static readonly FyzZvukDirType REKLAMA = new("REKLAMA", "reklamné hlásenia");
        public static readonly FyzZvukDirType SLOVA = new("SLOVA", "slová");
        public static readonly FyzZvukDirType V1 = new("V1", "druh vlaku, napr.: osobný vlak");
        public static readonly FyzZvukDirType V2 = new("V2", "druh vlaku s predložkou \"do\", napr.: do osobného vlaku");
        public static readonly FyzZvukDirType V4 = new("V4", "druh vlaku na začiatku vety, napr.: Osobný vlak");
        public static readonly FyzZvukDirType V8 = new("V8", "názvy vlakov");
        public static readonly FyzZvukDirType V14 = new("V14", "druh vlaku s príponami na zač. vety");
        public static readonly FyzZvukDirType VETY = new("VETY", "ucelené vetné hlásenia");
        public static readonly FyzZvukDirType VLAKNUM = new("VLAKNUM", "čísla použ. na číslovanie vlakov");
        public static readonly FyzZvukDirType VOZY1 = new("VOZY1", "vlastnosti vozňa, napr.: reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY1M = new("VOZY1M", "vlastnosti vozňov (množ.č), napr.: reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY2 = new("VOZY2", "vlastnosti vozňa s čiarkou, napr.: reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY2M = new("VOZY2M", "vlastnosti vozňov s čiarkou (množ.č), napr.: reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY3 = new("VOZY3", "vlastnosti vozňa na konci vety, napr.: reštauračný vozeň.");
        public static readonly FyzZvukDirType VOZY3M = new("VOZY3M", "vlastnosti vozňov na konci vety (množ.č), napr.: reštauračný vozeň.");
        public static readonly FyzZvukDirType VOZY4 = new("VOZY4", "vlastnosti vozňa so spojkou \"a\", napr.: a reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY4M = new("VOZY4M", "vlastnosti vozňov so spojkou \"a\" (množ.č), napr.: a reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY5 = new("VOZY5", "vlastnosti vozňa so spojkou \"a\" s čiarkou, napr.: a reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY5M = new("VOZY5M", "vlastnosti vozňov so spojkou \"a\" s čiarkou (množ.č), napr.: a reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY6 = new("VOZY6", "vlastnosti vozňa so spojkou \"a\" na konci vety, napr.: a reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY6M = new("VOZY6M", "vlastnosti vozňov so spojkou \"a\" na konci vety (množ.č), napr.: a reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY7 = new("VOZY7", "vlastnosti vozňa, napr.: reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY7M = new("VOZY7M", "vlastnosti vozňov (množ.č), napr.: reštauračný vozeň");
        public static readonly FyzZvukDirType VOZY8 = new("VOZY8", "vlastnosti vozňa s čiarkou, napr.: reštauračný vozeň,");
        public static readonly FyzZvukDirType VOZY8M = new("VOZY8M", "vlastnosti vozňov s čiarkou (množ.č), napr.: reštauračný vozeň,");
        public static readonly FyzZvukDirType ZNELKY = new("ZNELKY", "znelky");
#pragma warning restore 1591

        #endregion

        private FyzZvukDirType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        ///     Názov priečinka
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Popis priečinka
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Tento objekt (používané pre GUI)
        /// </summary>
        public FyzZvukDirType This => this;

        /// <summary>
        ///     Vrati vsetky typy priecinkov v zvukovej banke
        /// </summary>
        /// <returns></returns>
        public static List<FyzZvukDirType> GetValues()
        {
            return new()
            {
                UNCATEGORIZED,
                C1, C2, C3, C9,
                CISLO_, CISLO1, CISLO2, CISLO3, CISLO4, CISLO5, CISLO6, CISLO7, CISLO8, CISLO9,
                DODATKY,
                DOPRAVCA,
                DZ,
                K1, K2, K3, K4,
                LINKA,
                N1, N2, N3, N4, N5, N10, N11, N12, N13,
                POZ1, POZ2, POZ3, POZ7,
                R1, R2, R3, R4,
                RAZENI,
                REKLAMA,
                SLOVA,
                V1, V2, V4, V8, V14,
                VETY,
                VLAKNUM,
                VOZY1, VOZY1M,
                VOZY2, VOZY2M,
                VOZY3, VOZY3M,
                VOZY4, VOZY4M,
                VOZY5, VOZY5M,
                VOZY6, VOZY6M,
                VOZY7, VOZY7M,
                VOZY8, VOZY8M,
                ZNELKY
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }

        /// <summary>
        ///     Prevedie názov priečinka na jeho typ, ak je názov priečinka neznámy, vráti UNCATEGORIZED
        /// </summary>
        /// <param name="name">názov priečinka</param>
        /// <returns>typ priečinka</returns>
        public static FyzZvukDirType Parse(string name)
        {
            switch (name.ToUpper())
            {
                case "C1": return C1;
                case "C2": return C2;
                case "C3": return C3;
                case "C9": return C9;
                case "CISLO_": return CISLO_;
                case "CISLO1": return CISLO1;
                case "CISLO2": return CISLO2;
                case "CISLO3": return CISLO3;
                case "CISLO4": return CISLO4;
                case "CISLO5": return CISLO5;
                case "CISLO6": return CISLO6;
                case "CISLO7": return CISLO7;
                case "CISLO8": return CISLO8;
                case "CISLO9": return CISLO9;
                case "DODATKY": return DODATKY;
                case "DOPRAVCA":
                case "O1": return DOPRAVCA;
                case "DZ": return DZ;
                case "K1": return K1;
                case "K2": return K2;
                case "K3": return K3;
                case "K4": return K4;
                case "LINKA": return LINKA;
                case "N1": return N1;
                case "N2": return N2;
                case "N3": return N3;
                case "N4": return N4;
                case "N5": return N5;
                case "N10": return N10;
                case "N11": return N11;
                case "N12": return N12;
                case "N13": return N13;
                case "POZ1": return POZ1;
                case "POZ2": return POZ2;
                case "POZ3": return POZ3;
                case "POZ7": return POZ7;
                case "R1": return R1;
                case "R2": return R2;
                case "R3": return R3;
                case "R4": return R4;
                case "ŘAZENÍ":
                case "RAZENI": return RAZENI;
                case "REKLAMA": return REKLAMA;
                case "SLOVA": return SLOVA;
                case "V1": return V1;
                case "V2": return V2;
                case "V4": return V4;
                case "V8": return V8;
                case "V14": return V14;
                case "VETY": return VETY;
                case "VLAKNUM": return VLAKNUM;
                case "VOZY1": return VOZY1;
                case "VOZY1M": return VOZY1M;
                case "VOZY2": return VOZY2;
                case "VOZY2M": return VOZY2M;
                case "VOZY3": return VOZY3;
                case "VOZY3M": return VOZY3M;
                case "VOZY4": return VOZY4;
                case "VOZY4M": return VOZY4M;
                case "VOZY5": return VOZY5;
                case "VOZY5M": return VOZY5M;
                case "VOZY6": return VOZY6;
                case "VOZY6M": return VOZY6M;
                case "VOZY7": return VOZY7;
                case "VOZY7M": return VOZY7M;
                case "VOZY8": return VOZY8;
                case "VOZY8M": return VOZY8M;
                case "ZNELKY": return ZNELKY;
                default: return UNCATEGORIZED;
            }
        }
    }
}