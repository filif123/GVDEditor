using System;
using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda opisujúca font, serializovateľná do XML
    /// </summary>
    public class XMLFont
    {
        /// <summary>
        ///     Štýl písma
        /// </summary>
        [Flags]
        public enum FontStyle
        {
            /// <summary>
            ///     Obyčajné písmo
            /// </summary>
            [XmlEnum(Name = "r")] Regular = 0,

            /// <summary>
            ///     Tučné písmo
            /// </summary>
            [XmlEnum(Name = "b")] Bold = 1,

            /// <summary>
            ///     Kurzíva
            /// </summary>
            [XmlEnum(Name = "i")] Italic = 2,

            /// <summary>
            ///     Podčiarnutý text
            /// </summary>
            [XmlEnum(Name = "u")] Underline = 4,

            /// <summary>
            ///     Prečiarknutý text
            /// </summary>
            [XmlEnum(Name = "s")] Strikeout = 8
        }

        /// <summary>
        ///     Jednotka veľkosti písma
        /// </summary>
        public enum GraphicsUnit
        {
            /// <summary>Specifies the world coordinate system unit as the unit of measure.</summary>
            [XmlEnum(Name = "world")] World,

            /// <summary>
            ///     Specifies the unit of measure of the display device. Typically pixels for video displays, and 1/100 inch for
            ///     printers.
            /// </summary>
            [XmlEnum(Name = "display")] Display,

            /// <summary>Specifies a device pixel as the unit of measure.</summary>
            [XmlEnum(Name = "px")] Pixel,

            /// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
            [XmlEnum(Name = "pt")] Point,

            /// <summary>Specifies the inch as the unit of measure.</summary>
            [XmlEnum(Name = "inch")] Inch,

            /// <summary>Specifies the document unit (1/300 inch) as the unit of measure.</summary>
            [XmlEnum(Name = "doc")] Document,

            /// <summary>Specifies the millimeter as the unit of measure.</summary>
            [XmlEnum(Name = "mm")] Millimeter
        }

        [XmlIgnore] private readonly Font font_;

        private XMLFont()
        {
        }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="f">Font</param>
        public XMLFont(Font f)
        {
            font_ = f;

            FontFamily = f.FontFamily.Name;
            Unit = (GraphicsUnit) (int) f.Unit;
            Size = f.Size;
            Style = (FontStyle) (int) f.Style;
        }

        /// <summary>
        ///     Rodina písma
        /// </summary>
        [XmlAttribute("family")]
        public string FontFamily { get; set; }

        /// <summary>
        ///     Jednotka veľkosti písma
        /// </summary>
        [XmlAttribute("gunit")]
        public GraphicsUnit Unit { get; set; }

        /// <summary>
        ///     Veľkosť písma
        /// </summary>
        [XmlAttribute("size")]
        public float Size { get; set; }

        /// <summary>
        ///     Štýl písma
        /// </summary>
        [XmlAttribute("style")]
        public FontStyle Style { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator Font(XMLFont x)
        {
            return x.font_;
        }

        /// <summary>
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static implicit operator XMLFont(Font f)
        {
            return new(f);
        }

        /// <summary>
        ///     Konvertuje System.Drawing.Font na XMLFont
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static XMLFont FromFont(Font font)
        {
            return new(font);
        }

        /// <summary>
        ///     Konvertuje XMLFont na System.Drawing.Font
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Font ToFont(XMLFont x)
        {
            return new(x.FontFamily, x.Size, (System.Drawing.FontStyle) (int) x.Style,
                (System.Drawing.GraphicsUnit) (int) x.Unit);
        }
    }
}