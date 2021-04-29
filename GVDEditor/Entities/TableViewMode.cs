using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Definuje mód zobrazenia udaju na tabuli
    /// </summary>
    public sealed class TableViewMode
    {
#pragma warning disable 1591
        public static readonly TableViewMode Nothing = new("LVM_Nothing", "Prázdna tabuľa");
        public static readonly TableViewMode Vlak = new("LVM_Vlak", "Vlak bez meškania");
        public static readonly TableViewMode VlakZmeskanyPrichod = new("LVM_VlakZpozdenyPrijezd", "Na príchode meškajúci vlak");
        public static readonly TableViewMode VlakZmeskanyOdchod = new("LVM_VlakZpozdenyOdjezd", "Na odchode meškajúci vlak");
        public static readonly TableViewMode VlakZmeskany = new("LVM_VlakZpozdeny", "Na príchode i odchode meškajúci vlak");
        public static readonly TableViewMode Text = new("LVM_Text", "Text");
#pragma warning restore 1591

        private TableViewMode(string key, string name)
        {
            Key = key;
            Name = name;
        }

        /// <summary>
        ///     Kluc modu zobrazenia
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Nazov modu zobrazenia
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Odkaz na seba, pre potreby DataSource
        /// </summary>
        public TableViewMode This => this;

        /// <summary>
        ///     Skonvertuje mód zobrazenia podla kluca na objekt
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TableViewMode Parse(string s)
        {
            return s switch
            {
                "LVM_Nothing" => Nothing,
                "LVM_Vlak" => Vlak,
                "LVM_VlakZpozdenyPrijezd" => VlakZmeskanyPrichod,
                "LVM_VlakZpozdenyOdjezd" => VlakZmeskanyOdchod,
                "LVM_VlakZpozdeny" => VlakZmeskany,
                "LVM_Text" => Text,
                _ => null
            };
        }

        /// <summary>
        ///     Vrati vsetky mozne tz
        /// </summary>
        /// <returns></returns>
        public static List<TableViewMode> GetValues()
        {
            var modes = new List<TableViewMode>
            {
                Nothing,
                Vlak,
                VlakZmeskanyPrichod,
                VlakZmeskanyOdchod,
                VlakZmeskany,
                Text
            };
            return modes;
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}