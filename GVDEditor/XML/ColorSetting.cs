using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda reprezentujúca nastavenia farieb a písma pre GUI
    /// </summary>
    public class ColorSetting
    {
        /// <summary>
        ///     Farba pozadia
        /// </summary>
        [XmlIgnore] public Color BackColor;

        /// <summary>
        ///     Nastavi, aby pouzivatel nemohol menit farbu pozadia - BackColor
        /// </summary>
        [XmlIgnore] public bool DisableBackColorEdit;

        /// <summary>
        ///     Nastavi, aby pouzivatel nemohol menit pismo
        /// </summary>
        [XmlIgnore] public bool DisableFontEdit;

        /// <summary>
        ///     Použiť hrubý rez písma
        /// </summary>
        [XmlAttribute("bold")] public bool Bold;

        /// <summary>
        ///     Farba popredia
        /// </summary>
        [XmlIgnore] public Color ForeColor;

        /// <summary>
        ///     Názov nastavenia, ktoré sa bude zobrazovať v GUI
        ///     Lokalizovateľná
        /// </summary>
        [XmlIgnore] [Localizable(true)] public string Name;

        /// <summary>
        ///     Farba pozadia vo formáte XML atribútu
        /// </summary>
        [XmlAttribute("b")]
        [DefaultValue("#00000000")]
        public string ColorBackAttr
        {
            get => XMLColor.FromColor(BackColor);
            set => BackColor = XMLColor.ToColor(value);
        }

        /// <summary>
        ///     Farba popredia vo formáte XML atribútu
        /// </summary>
        [XmlAttribute("f")]
        public string ColorForeAttr
        {
            get => XMLColor.FromColor(ForeColor);
            set => ForeColor = XMLColor.ToColor(value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Skopíruje nastavenie
        /// </summary>
        /// <param name="obj">pôvodné nastavenie</param>
        /// <returns></returns>
        public static ColorSetting Copy(ColorSetting obj)
        {
            return new()
            {
                BackColor = obj.BackColor,
                ForeColor = obj.ForeColor,
                DisableBackColorEdit = obj.DisableBackColorEdit,
                DisableFontEdit = obj.DisableFontEdit,
                Bold = obj.Bold,
                ColorBackAttr = obj.ColorBackAttr,
                ColorForeAttr = obj.ColorForeAttr,
                Name = obj.Name
            };
        }
    }
}