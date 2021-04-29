using System;
using System.Drawing;
using GVDEditor.Properties;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Trieda reprezentujúca smerovanie vlaku
    /// </summary>
    public sealed class Routing
    {
        #region VALUES

        /// <summary>
        ///     Vlak končí v stanici
        /// </summary>
        public static readonly Routing Konciaci = new("Končiaci", "->|", Resources.konecna_st, "K");

        /// <summary>
        ///     Vlak prechádza stanicou
        /// </summary>
        public static readonly Routing Prechadzajuci = new("Prechádzajúci", "<->", Resources.prechadza_st, "P");

        /// <summary>
        ///     Vlak vychádza zo stanice
        /// </summary>
        public static readonly Routing Vychadzajuci = new("Vychadzajúci", "|->", Resources.vychodzia_st, "V");

        #endregion

        private Routing(string name, string symbol, Bitmap bitmap, string charSymbol)
        {
            Name = name;
            Symbol = symbol;
            Image = bitmap;
            CharSymbol = charSymbol;
        }

        /// <summary>
        ///     Názov smerovania
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Symbol smerovania ako text
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        ///     Označenie smerovania ako znak
        /// </summary>
        public string CharSymbol { get; }

        /// <summary>
        ///     Symbol smerovania ako obrázok
        /// </summary>
        public Bitmap Image { get; }

        /// <summary>
        ///     Konvertuje označenie smerovania na objekt
        /// </summary>
        /// <param name="s">označenie smerovania</param>
        /// <returns><see cref="Routing" />, ak nenájde zhodu, vráti <see langword="null" /></returns>
        public static Routing Parse(string s)
        {
            return s switch
            {
                "V" => Vychadzajuci,
                "P" => Prechadzajuci,
                "K" => Konciaci,
                _ => null
            };
        }

        /// <summary>
        ///     Konvertuje označenie smerovania na objekt a vráti, či sa operácia podarila
        /// </summary>
        /// <param name="s">označenie smerovania</param>
        /// <param name="exit">výsledné smerovanie</param>
        /// <returns><see langword="true" /> ak sa operácia podarila, inak vráti <see langword="false" /></returns>
        public static bool TryParse(string s, out Routing exit)
        {
            switch (s)
            {
                case "V":
                    exit = Vychadzajuci;
                    return true;
                case "P":
                    exit = Prechadzajuci;
                    return true;
                case "K":
                    exit = Konciaci;
                    return true;
                default:
                    exit = null;
                    return false;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object s1)
        {
            if (s1 is Routing sm) return CharSymbol == sm.CharSymbol;

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Symbol != null ? Symbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CharSymbol != null ? CharSymbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Image != null ? Image.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:GVDEditor.Entities.Routing" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Routing left, Routing right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:GVDEditor.Entities.Routing" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Routing left, Routing right)
        {
            return !Equals(left, right);
        }
    }
}