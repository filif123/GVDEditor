using System;
using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda reprezentujúca nastavenie písmapre určitu komponentu programu
    /// </summary>
    public class AppFont
    {
        /// <summary>
        ///     Názov použitia písma pre program
        /// </summary>
        [XmlIgnore]
        public string Name { get; set; }

        /// <summary>
        ///     Písmo
        /// </summary>
        [XmlIgnore]
        public Font Font { get; set; }

        /// <summary>
        ///     Príkad pre vizualizáciu písma
        /// </summary>
        [XmlIgnore]
        public string Example => "OK1932Šč./jkl";

        /// <summary>
        ///     Textový komentár k vlastnostiam tohto písma
        /// </summary>
        [XmlIgnore]
        public string FontDescription =>
            $"{Font.Name}, {Math.Round(Font.SizeInPoints)}{(Font.Bold ? ", tučné " : "")}{(Font.Italic ? ", kurzíva " : "")}{(Font.Strikeout ? ", prečiarknuté " : "")}{(Font.Underline ? ",podčiarkuté " : "")}";

        /// <summary>
        ///     XML reprezentácia písma
        /// </summary>
        [XmlElement(Type = typeof(XMLFont), ElementName = "f")]
        public XMLFont FontXML
        {
            get => XMLFont.FromFont(Font);
            set => Font = XMLFont.ToFont(value);
        }

        /// <summary>
        ///     Returns Font object from this AppFont instance.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static implicit operator Font (AppFont f)
        {
            return f.Font;
        }
    }
}