using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using GVDEditor.Tools;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Konfiguracny subor
    /// </summary>
    [XmlRoot("CONFIG")]
    public class Config
    {
        /// <summary>
        ///     Jazyk aplikacie
        /// </summary>
        public enum AppLanguage
        {
            /// <summary>
            ///     Slovencina
            /// </summary>
            [XmlEnum(Name = "SK")] SK,

            /// <summary>
            ///     Cestina
            /// </summary>
            [XmlEnum(Name = "CZ")] CZ
        }

        /// <summary>
        ///     Typ debuggovania
        /// </summary>
        public enum DebugMode
        {
            /// <summary>
            ///     Normalne chybove hlasky
            /// </summary>
            [XmlEnum(Name = "0")] ONLY_MESSAGE,

            /// <summary>
            ///     Zobrazovanie detailnejsich chyb. hlasok
            /// </summary>
            [XmlEnum(Name = "1")] DETAILED_INFO,

            /// <summary>
            ///     Bez chybovych hlasok (spadnutie programu)
            /// </summary>
            [XmlEnum(Name = "2")] APP_CRASH
        }

        /// <summary>
        ///     Typ zobrazenia MenuStrip alebo ToolStrip na pracovnej ploche programu
        /// </summary>
        public enum DesktopMenu
        {
            /// <summary>
            ///     ToolStrip aj MenuStrip
            /// </summary>
            [XmlEnum(Name = "0")] TS_MS,

            /// <summary>
            ///     Len ToolStrip
            /// </summary>
            [XmlEnum(Name = "1")] TS_ONLY,

            /// <summary>
            ///     Len MenuStrip
            /// </summary>
            [XmlEnum(Name = "2")] MS_ONLY
        }

        /// <summary>
        ///     Povolit pouzivanie automatickej spravy variant vlaku
        /// </summary>
        [XmlElement("AutoVariant")] 
        [DefaultValue(false)]
        public bool AutoVariant;

        /// <summary>
        ///     Povoli/zakaze kontrolovanie spravnosti zadanej varianty vlaku
        /// </summary>
        [XmlElement("DisableVariantChk")]
        [DefaultValue(false)]
        public bool DisableVariantCheck;

        /// <summary>
        ///     Zobrazenie menu v FMain
        /// </summary>
        [XmlElement("DesktopMenu")]
        public DesktopMenu DesktopMenuMode = DesktopMenu.TS_MS;

        /// <summary>
        ///     Automaticky generovat texty do tabul pri ukladani do suborov
        /// </summary>
        [XmlElement("AutoTableText")]
        [DefaultValue(false)]
        public bool AutoTableText;

        /// <summary>
        ///     True ak logovat chyby a vynimky, inak False
        /// </summary>
        [XmlElement("LoggingError")]
        [DefaultValue(true)]
        public bool LoggingError = true;

        /// <summary>
        ///     Povolit alebo zakazat viacero instancii tohto programu
        /// </summary>
        [XmlElement("MoreInstances")]
        [DefaultValue(false)]
        public bool MoreInstance;

        /// <summary>
        ///     Pouzivat klasicky dizajn komponetov v GUI
        /// </summary>
        [XmlElement("ClassicGUI")]
        [DefaultValue(false)]
        public bool ClassicGUI;

        /// <summary>
        ///     True ak logovat informacie o aplikacii, inak False
        /// </summary>
        [XmlElement("LoggingInfo")]
        [DefaultValue(true)]
        public bool LoggingInfo = true;

        /// <summary>
        ///     Klávesové skratky pre akcie na pracovnej ploche programu
        /// </summary>
        [XmlElement("Shortcuts")] 
        public AppShortcuts Shortcuts = new();

        /// <summary>
        ///     Nastavi jazyk pouzivatelskeho rozhrania (GUI)
        /// </summary>
        [XmlElement("Language")]
        public AppLanguage Language = AppLanguage.SK;

        /// <summary>
        ///     Nastavi jazyk generovania datumovych obmedzeni
        /// </summary>
        [XmlElement("DateRemLocate")]
        public AppLanguage DateRemLocate = AppLanguage.SK;

        /// <summary>
        ///     Mod zobrazovania chybovych hlasok v GUI
        /// </summary>
        [XmlElement("DebugModeGUI")]
        public DebugMode DebugModeGUI = DebugMode.ONLY_MESSAGE;

        /// <summary>
        ///     Stlpce zobrazujuce sa v tabulke na pracovnej ploche programu
        /// </summary>
        [XmlElement("DesktopCols")] 
        public DesktopColumns DesktopCols = new();

        /// <summary>
        ///     Nastavenia pisiem pre jednotlive komponenty GUI
        /// </summary>
        [XmlElement("ControlFonts")]
        public ControlFonts Fonts = new();

        /// <summary>
        ///     Ci sa ma zobrazovat stavovy riadok na pracovnej ploche programu
        /// </summary>
        [XmlElement("ShowStateRow")]
        [DefaultValue(true)]
        public bool ShowStateRow = true;

        /// <summary>
        ///     Ci sa maju zobrazovat hlavicky riadkov v tabulke na pracovnej ploche programu
        /// </summary>
        [XmlElement("ShowRowsHeader")]
        [DefaultValue(true)]
        public bool ShowRowsHeader = true;

        /// <summary>
        ///     Ci sa ma zobrazovat datum v stavovom riadku na pracovnej ploche programu
        /// </summary>
        [XmlElement("ShowDateTimeInStateRow")]
        [DefaultValue(true)]
        public bool ShowDateTimeInStateRow = true;

        /// <summary>
        ///     Ci sa ma prisposobit posledny stlpec v tabulke na pracovnej ploche programu
        /// </summary>
        [XmlElement("FitLastColumn")]
        [DefaultValue(true)]
        public bool FitLastColumn = true;

        /// <summary>
        ///     Nastavi cas medzi zvukmi pri prehravani
        /// </summary>
        [XmlElement("PlayerWordPause")]
        [DefaultValue(0)]
        public int PlayerWordPause;

        /*/// <summary>
        /// Povoli/zakaze upravu datovych obmedzeni variant podla hlavnej varianty
        /// </summary>
        [XmlElement("EditVariantsDaterem")] public bool EditVariantsDaterem;*/

        /// <summary>
        ///     konfiguracia spustania INISSu z tohto programu
        /// </summary>
        [XmlElement("StartupINISSConfig")] 
        public StartupINISS StartupINISSConfig = new() {CmdArgs = "", RunAsAdmin = false};

        /// <summary>
        ///     Nacitava data z konfiguracneho suboru
        /// </summary>
        /// <param name="sPathFName">cesta k suboru</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Ak konfiguacny subor obsahoval nelatnu hodnotu</exception>
        public static Config ReadData(string sPathFName)
        {
            Config config = null;
            try
            {
                var text = File.ReadAllText(sPathFName, Encoding.GetEncoding(1250));
                if (!string.IsNullOrEmpty(text))
                {
                    config = (Config) XMLSerialization.Deserialize(text, typeof(Config));

                    if (config.PlayerWordPause > 10000 || config.PlayerWordPause < 0)
                    {
                        var error =
                            $"Chyba v konfiguračnom súbore: Nastavenie {nameof(PlayerWordPause)} obsahuje nesprávnu hodnotu {config.PlayerWordPause}.";
                        Log.Error(error);
                        throw new ArgumentException(error);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }

            if (config == null)
            {
                config = new Config();
                WriteData(sPathFName, config);
            }

            return config;
        }

        /// <summary>
        ///     Zapise data do konfiguracneho suboru
        /// </summary>
        /// <param name="sFName">cesta k suboru</param>
        /// <param name="oObject">data</param>
        public static void WriteData(string sFName, object oObject)
        {
            XMLSerialization.SerializeToFile(sFName, RuntimeHelpers.GetObjectValue(oObject));
        }

        private static DesktopColumns SetDesktopColumnsSettingsDefault()
        {
            var cols = new DesktopColumns
            {
                Number = new DesktopColumn {Order = 0, Visible = true, MinWidth = 60},
                Type = new DesktopColumn {Order = 1, Visible = true, MinWidth = 40},
                Name = new DesktopColumn {Order = 2, Visible = true, MinWidth = 100},
                LinkaPrichod = new DesktopColumn {Order = 3, Visible = false, MinWidth = 50},
                LinkaOdchod = new DesktopColumn {Order = 4, Visible = false, MinWidth = 50},
                Routing = new DesktopColumn {Order = 5, Visible = true, MinWidth = 100},
                Prichod = new DesktopColumn {Order = 6, Visible = true, MinWidth = 60},
                Odchod = new DesktopColumn {Order = 7, Visible = true, MinWidth = 60},
                VychodziaStanica = new DesktopColumn {Order = 8, Visible = true, MinWidth = 120},
                KonecnaStanica = new DesktopColumn {Order = 9, Visible = true, MinWidth = 120},
                DateRem = new DesktopColumn {Order = 10, Visible = true, MinWidth = 300},
                Track = new DesktopColumn {Order = 11, Visible = true, MinWidth = 100},
                Operator = new DesktopColumn {Order = 12, Visible = true, MinWidth = 50},
                OtherBtn = new DesktopColumn {Order = 13, Visible = true, MinWidth = 50}
            };
            return cols;
        }

        /// <summary>
        ///     Vrati predvolene nastavenia pre klavesove skratky pracovnej plochy programu
        /// </summary>
        /// <returns></returns>
        public static AppShortcuts SetShortcutsSettingsDefault()
        {
            var shortcuts = new AppShortcuts
            {
                NewGVD = new CommandShortcut(new ShortcutName(Shortcut.None)),
                OpenGVD = new CommandShortcut(new ShortcutName(Shortcut.CtrlO)),
                ImportGVD = new CommandShortcut(new ShortcutName(Shortcut.CtrlI)),
                ImportData = new CommandShortcut(new ShortcutName(Shortcut.None)),
                Save = new CommandShortcut(new ShortcutName(Shortcut.CtrlS)),
                AddTrain = new CommandShortcut(new ShortcutName(Shortcut.Ins)),
                EditTrain = new CommandShortcut(new ShortcutName(Shortcut.ShiftIns)),
                DeleteTrains = new CommandShortcut(new ShortcutName(Shortcut.Del)),
                DuplicateTrain = new CommandShortcut(new ShortcutName(Shortcut.CtrlD)),
                LocalSettings = new CommandShortcut(new ShortcutName(Shortcut.CtrlL)),
                GlobalSettings = new CommandShortcut(new ShortcutName(Shortcut.CtrlG)),
                AppSettings = new CommandShortcut(new ShortcutName(Shortcut.CtrlP)),
                InfoApp = new CommandShortcut(new ShortcutName(Shortcut.F6)),
                UpdateNotes = new CommandShortcut(new ShortcutName(Shortcut.None)),
                RunINISS = new CommandShortcut(new ShortcutName(Shortcut.F5)),
                KillINISS = new CommandShortcut(new ShortcutName(Shortcut.ShiftF5))
            };

            return shortcuts;
        }

        /// <summary>
        ///     Vrati predvolene nastavenia pre pisma komponentov v GUI
        /// </summary>
        /// <returns></returns>
        public static ControlFonts SetAppFontsSettingsDefault()
        {
            var fonts = new ControlFonts
            {
                Labels = new AppFont {Font = SystemFonts.DefaultFont},
                Buttons = new AppFont {Font = SystemFonts.DefaultFont},
                ColsHeader = new AppFont {Font = SystemFonts.MenuFont},
                TableCells = new AppFont {Font = SystemFonts.DefaultFont},
                Menu = new AppFont {Font = SystemFonts.MenuFont},
                StateRow = new AppFont {Font = new Font(SystemFonts.MenuFont.FontFamily, 10, FontStyle.Bold)}
            };

            return fonts;
        }
    }
}