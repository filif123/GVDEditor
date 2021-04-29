using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda definujuca farby pre ovladacie prvky GUI
    /// </summary>
    public class ControlsColorScheme
    {
        /// <summary>
        ///     Styl panel
        /// </summary>
        [XmlElement("Panel")] 
        public ColorSetting Panel = new() {BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText, DisableFontEdit = true};

        /// <summary>
        ///     Styl pre tlacidla
        /// </summary>
        [XmlElement("Button")]
        public ColorSetting Button = new() { BackColor = SystemColors.ButtonFace, ForeColor = SystemColors.ControlText, DisableFontEdit = true};

        /// <summary>
        ///     Styl pre všetky štítky
        /// </summary>
        [XmlElement("Label")] 
        public ColorSetting Label = new() {BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText, DisableFontEdit = true};

        /// <summary>
        ///     Styl pre boxy - ComboBox, ListBox, ...
        /// </summary>
        [XmlElement("Box")] 
        public ColorSetting Box = new() { BackColor = SystemColors.ControlLightLight, ForeColor = SystemColors.ControlText, DisableFontEdit = true};

        /// <summary>
        ///     Farba okrajov ovladacich prvkov (nastavovat iba ForeColor)
        /// </summary>
        [XmlElement("Border")] 
        public ColorSetting Border = new() {ForeColor = SystemColors.WindowFrame, DisableFontEdit = true, DisableBackColorEdit = true};

        /// <summary>
        ///     Farba značiek - pouzite ako značka vo vnutri RadioButton a CheckBox (nastavovat iba ForeColor)
        /// </summary>
        [XmlElement("Mark")] 
        public ColorSetting Mark = new() {ForeColor = SystemColors.ControlText, DisableFontEdit = true, DisableBackColorEdit = true};

        /// <summary>
        ///     Styl pre oznacenie prave aktivneho ovladacieho prvku resp. jeho casti
        /// </summary>
        [XmlElement("Highlight")] 
        public ColorSetting Highlight = new() {BackColor = SystemColors.Highlight, ForeColor = SystemColors.ControlText, DisableFontEdit = true};
    }
}