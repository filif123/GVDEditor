using System.Drawing;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Informácie o priečinku s grafikonom
    /// </summary>
    public sealed class DirList
    {
        /// <summary>
        ///     Názov priečinka
        /// </summary>
        public string DirName { get; set; }

        /// <summary>
        ///     Celá cesta k priečinku
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        ///     Port pre vzdialené ovládanie tabúľ
        /// </summary>
        public int? TablePort { get; set; }

        /// <summary>
        ///     Port pre zabezpečenie vzdialeného hlásenia
        /// </summary>
        public int? HlaseniePort { get; set; }

        /// <summary>
        ///     Farba zobrazujúca v INISS ako farba pozadia vlaku na pracovnej ploche ako odlíšenie od vlakov iných staníc
        /// </summary>
        public Color? Farba { get; set; }
    }
}