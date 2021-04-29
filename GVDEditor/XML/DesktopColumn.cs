using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Reprezentuje jeden stlpec, ktory sa sa moze vyskytovat tabulke DataGridView na pracovnej ploche
    /// </summary>
    public class DesktopColumn
    {
        /// <summary>
        ///     Identifikator stlpca
        /// </summary>
        [XmlIgnore] public int Id { get; set; }

        /// <summary>
        ///     Nazov stlpca, ktory sa zobrazuje v hlavicke stlpca
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [XmlIgnore] public string Name { get; set; }

        /// <summary>
        ///     Aktualne poradove cislo umiestnenia tohto stlpca v tabulke
        /// </summary>
        [XmlAttribute("o")] public int Order { get; set; }

        /// <summary>
        ///     Urcuje, ci sa ma stlpec zobrazovat pouzivatelovi v tabulke
        /// </summary>
        [XmlAttribute("v")] public bool Visible { get; set; }

        /// <summary>
        ///     Urcuje minimalnu sirku stlpca (pouzivatel potom nemoze zmensit stlpec pod tuto hodnotu)
        /// </summary>
        [XmlAttribute("mw")] public int MinWidth { get; set; }
    }
}