using System.Windows.Forms;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Reprezentuje klavesovu skratku pouzivanu na hlavnej pracovnej ploche programu
    /// </summary>
    public class CommandShortcut
    {
        /// <summary>
        ///     Názov použitia klávesovej skratky
        /// </summary>
        [XmlIgnore]
        public string Name { get; set; }

        /// <summary>
        ///     Klávesová skratka ako štruktúra <see cref="ShortcutName" />
        /// </summary>
        [XmlIgnore]
        public ShortcutName Shortcut { get; set; }

        /// <summary>
        ///     XML reprezentácia klávesovej skratky
        /// </summary>
        [XmlAttribute("sc")]
        public string ShortcutXML
        {
            get => XMLEnum<Shortcut>.EnumToString(Shortcut.ThisShortcut);
            set => Shortcut = new ShortcutName(XMLEnum<Shortcut>.StringToEnum(value));
        }

        /// <summary>
        ///     Konstuktor
        /// </summary>
        public CommandShortcut() { }

        /// <summary>
        ///     Konstuktor
        /// </summary>
        /// <param name="shortcut"></param>
        public CommandShortcut(ShortcutName shortcut)
        {
            Shortcut = shortcut;
        }
    }

    /// <summary>
    ///     Štruktúra <see cref="Shortcut" /> pridaná o predefinovanie metódy <see cref="ToString" />, ktorá vracia
    ///     reprezentáciu klávesovej skratky vo formáte: X+Y.
    ///     Ak je <see cref="ThisShortcut" /> <see cref="Shortcut.None" />, vráti "(Žiadna)".
    /// </summary>
    public readonly struct ShortcutName
    {
        /// <summary>
        ///     Klávesová skratka ako štruktúra
        /// </summary>
        public readonly Shortcut ThisShortcut;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="shortcut"></param>
        public ShortcutName(Shortcut shortcut)
        {
            ThisShortcut = shortcut;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (ThisShortcut == Shortcut.None) return "(Žiadna)";
            return new KeysConverter().ConvertToString((Keys) ThisShortcut) ?? string.Empty;
        }
    }
}