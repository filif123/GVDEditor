using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TabTabCompiler.Components.AST;
using TabTabCompiler.Tools;

namespace TabTabCompiler.Components
{
    public enum ParamType
    {
        None,
        String,
        Int,
        Priznak,
        TrainType,
        TrainState
    }

    public enum ReturnType
    {
        Bool,
        Int
    }

    public sealed class Function : Statement
    {
        public static readonly Function Operator = new(ParamType.String,"OPERATOR","Zistí, či sa dopravca daného vlaku rovná zadanému názvu dopravcu.");
        public static readonly Function CVlaku = new("CVLAKU", "Vracia číslo daného vlaku.");
        //????public static readonly Function Pozice = new(ParamType.TrainState, "POZICE", "Vracia pozíciu vlaku v stanici (vychádza, prechádza, končí).");
        public static readonly Function Typ = new(ParamType.TrainType, "TYP", "Zistí, či sa typ vlaku rovná zadanému typu vlaku.");
        public static readonly Function Priznak = new(ParamType.Priznak, "PRIZNAK", "Zistí, či má daný vlak príznak rovný zadanému príznaku.");
        public static readonly Function Stav = new(ParamType.TrainState, "STAV", "Zistí, či sa stav vlaku rovná zadanému stavu vlaku.");
        public static readonly Function NaklTyp = new("NAKLTYP", "Vracia, či je vlak nákladného typu.");
        public static readonly Function VylukaTu = new("VYLUKAZDE", "Vracia, či má vlak v tejto stanici výluku.");
        public static readonly Function Odklon = new("ODKLON", "Vracia, či má vlak odklonenú trasu.");
        public static readonly Function MeskaniePrichod = new("ZPOZDENIPRIJ", "...");
        public static readonly Function MeskanieOdchod = new("ZPOZDENIODJ", "...");
        public static readonly Function Meskanie = new("ZPOZDENI", "ZPOZDENI", "Vracia, či má vlak nejaké meškanie.");
        public static readonly Function DobaPobytu = new("DOBAPOBYTU", "..."); //?
        public static readonly Function PlanDobaPobytu = new("PLANDOBAPOBYTU", "..."); //?
        public static readonly Function ZaujmovaStanica = new("ZAJMOSTANICE", "Zistí, či sa záujmová stanica rovná zadanenému ID stanice.", "HOMESTATION");
        public static readonly Function VychodziaStanica = new("VYCHSTANICE", "Zistí, či sa východzia stanica vlaku rovná zadanenému ID stanice.", "BASESTATION");
        public static readonly Function KonecnaStanica = new("CILSTANICE", "Zistí, či sa konečná stanica vlaku rovná zadanenému ID stanice.", "ENDSTATION");
        public static readonly Function Miestne = new("MISTNI", "..."); //?
        public static readonly Function Cudzie = new("CIZI", "..."); //?
        public static readonly Function ZoSmeru = new("ZESMERU", "Vráti, či vlak začína v zadanej stanici.","", ParamType.Int);
        public static readonly Function DoSmeru = new("DOSMERU", "Vráti, či vlak končí v zadanej stanici.","", ParamType.Int);
        public static readonly Function KolajPrichod = new("KOLEJPRIJ", "...","",ParamType.String);
        public static readonly Function KolajOdchod = new("KOLEJODJ", "...", "", ParamType.String);
        public static readonly Function DatumPrichod = new("DATUMPRIJ", "..."); //?
        public static readonly Function DatumOdchod = new("DATUMODJ", "..."); //?
        public static readonly Function CasPrichod = new("CASPRIJ", "..."); //?
        public static readonly Function CasOdchod = new("CASODJ", "..."); //?
        public static readonly Function IndCat6 = new("INDCAT6", "..."); //?
        public static readonly Function IndCat8 = new("INDCAT8", "..."); //?
        public static readonly Function Date = new("DATE", "..."); //?
        public static readonly Function Time = new("TIME", "..."); //?


        public string Name { get; }

        public string AltName { get; }

        public string Description { get; }

        public ParamType ParameterType { get; }

        public ReturnType ReturnType { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        private Function(ParamType paramType, string name, string desc = "", string altName = null, ReturnType rettype = ReturnType.Bool)
        {
            Name = name;
            ParameterType = paramType;
            AltName = altName;
            ReturnType = rettype;
            Description = desc;
        }

        private Function(string name, string desc, string alt = null, ParamType param = ParamType.None)
        {
            Name = name;
            Description = desc;
            ParameterType = param;
            AltName = alt;
            ReturnType = ReturnType.Bool;
        }

        /// <summary>
        ///     Vrati vsetky funkcie (Public static fields)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Function> GetValues()
        {
            return typeof(Function).GetFields(BindingFlags.Public |
                                              BindingFlags.Static |
                                              BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<Function>().ToList();
        }

        /// <summary>
        ///     Pokusi sa konvertovat retazec na funkciu podla identifikatora
        /// </summary>
        /// <param name="name">vstupny retazec</param>
        /// <returns>prvok enumeracie</returns>
        public static Function Parse(string name)
        {
            foreach (var val in GetValues())
            {
                if (val.Name.EqualsIgnoreCase(name) || val.AltName.EqualsIgnoreCase(name))
                {
                    return val;
                }
            }

            return null;
        }
    }
}
