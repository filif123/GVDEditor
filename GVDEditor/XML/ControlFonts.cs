using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Obsahuje zoznam všetkých nastaviteľných komponentov, pre ktoré sa nastavuje ich písmo
    /// </summary>
    public class ControlFonts
    {
        /// <summary>
        ///     Nastavenie písma pre Labels
        /// </summary>
        [XmlElement("Labels")] public AppFont Labels = new (){ Font = SystemFonts.DefaultFont };

        /// <summary>
        ///     Nastavenie písma pre Buttons
        /// </summary>
        [XmlElement("Buttons")] public AppFont Buttons = new() { Font = SystemFonts.DefaultFont };

        /// <summary>
        ///     Nastavenie písma pre Menu
        /// </summary>
        [XmlElement("Menu")] public AppFont Menu = new() { Font = SystemFonts.MenuFont };

        /// <summary>
        ///     Nastavenie písma pre hlavičku śtĺpcov v DataGridView
        /// </summary>
        [XmlElement("ColsHeaders")] public AppFont ColsHeader = new() { Font = SystemFonts.MenuFont };


        /// <summary>
        ///     Nastavenie písma pre obsah v DataGridView
        /// </summary>
        [XmlElement("TableCells")] public AppFont TableCells = new() { Font = SystemFonts.DefaultFont };

        /// <summary>
        ///     Nastavenie písma pre stavový riadok v dolnej časti pracovnej plochy programu
        /// </summary>
        [XmlElement("StateRow")] public AppFont StateRow = new() { Font = new Font(SystemFonts.MenuFont.FontFamily, 10, FontStyle.Bold) };


        /// <summary>
        ///     Vráti zoznam všetkých nastaviteľných komponentov, pre ktoré sa nastavuje ich písmo
        /// </summary>
        /// <returns></returns>
        public List<AppFont> GetValues()
        {
            return new() {Labels, Buttons, Menu, ColsHeader, TableCells, StateRow};
        }
    }
}