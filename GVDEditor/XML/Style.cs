using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda definujuca farby a pisma pre viacere prvky programu
    /// </summary>
    public class Style
    {
        /// <summary>
        ///     Nazov stylu
        /// </summary>
        [XmlAttribute("name")] 
        public string Name;

        /// <summary>
        ///     Ci ovladacie prvky v GUI pouzivaju predvoleny vzhlad
        /// </summary>
        [XmlElement("DefaultStyle")] 
        [DefaultValue(true)]
        public bool ControlsDefaultStyle;

        /// <summary>
        ///     Ci okna v GUI maju mat tmavy TitleBar (funguje len vo Windows 10)
        /// </summary>
        [XmlElement("DarkTitlebar")]
        [DefaultValue(false)]
        public bool DarkTitleBar;

        /// <summary>
        ///     Ci ovladacie prvky v GUI maju mat tmavy ScrollBar (funguje len vo Windows 10)
        /// </summary>
        [XmlElement("DarkScrollBar")]
        [DefaultValue(false)]
        public bool DarkScrollBar;

        /// <summary>
        ///     Farebna schema pre ovladacie prvky v GUI
        /// </summary>
        [XmlElement("ControlsColorScheme")] 
        public ControlsColorScheme ControlsColorScheme = new();

        /// <summary>
        ///     Farebna schema pre druh vlaku zobrazujuceho sa v tabulke na pracovnej ploche programu
        /// </summary>
        [XmlElement("TrainType")]
        public TrainTypeColumnScheme TrainTypeColumnScheme = new();

        /// <summary>
        ///     Farebna schéma pre textový editor TabTab
        /// </summary>
        [XmlElement("TabTabEditor")]
        public TabTabEditorScheme TabTabEditorScheme = new();

        /// <summary>
        ///     Konstructor
        /// </summary>
        public Style()
        {
            ControlsDefaultStyle = true;
            DarkScrollBar = false;
            DarkTitleBar = false;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre editor TabTab na predvolene hodnoty
        /// </summary>
        public static TabTabEditorScheme SetTabTabEditorSchemeDefault()
        {
            var scheme = new TabTabEditorScheme
            {
                Default = new ColorSetting {ForeColor = Color.Black, Bold = false, DisableBackColorEdit = true},
                Function = new ColorSetting {ForeColor = Color.Blue, Bold = false, DisableBackColorEdit = true},
                Identifier = new ColorSetting {ForeColor = Color.Teal, Bold = false, DisableBackColorEdit = true},
                Number = new ColorSetting {ForeColor = Color.Purple, Bold = false, DisableBackColorEdit = true},
                String = new ColorSetting {ForeColor = Color.Red, Bold = false, DisableBackColorEdit = true},
                Comment = new ColorSetting {ForeColor = Color.Green, Bold = false, DisableBackColorEdit = true},
                Var = new ColorSetting {ForeColor = Color.DarkSlateGray, Bold = false, DisableBackColorEdit = true},
                Event = new ColorSetting {ForeColor = Color.SaddleBrown, Bold = false, DisableBackColorEdit = true},
                OnNewLine = new ColorSetting {ForeColor = Color.OrangeRed, Bold = false, DisableBackColorEdit = true},
                Operator = new ColorSetting {ForeColor = Color.Black, Bold = false, DisableBackColorEdit = true},
                Constant = new ColorSetting {ForeColor = Color.DimGray, Bold = false, DisableBackColorEdit = true},
                SelBraces = new ColorSetting {ForeColor = Color.LightGray, BackColor = Color.BlueViolet, Bold = false},
                SelBraceBad = new ColorSetting {ForeColor = Color.Red, BackColor = Color.LightGray, Bold = false},
                Font = new Font("Consolas", 10)
            };

            return scheme;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre stlpec Typ vlaku na predvolene hodnoty
        /// </summary>
        public static TrainTypeColumnScheme SetTrainTypeColumnSchemeDefault()
        {
            var scheme = new TrainTypeColumnScheme
            {
                Os = new ColorSetting {ForeColor = Color.Black, BackColor = Color.White, Bold = false},
                R = new ColorSetting {ForeColor = Color.Red, BackColor = Color.White, Bold = true},
                X = new ColorSetting {ForeColor = Color.Green, BackColor = Color.White, Bold = true},
                Sl = new ColorSetting {ForeColor = Color.OrangeRed, BackColor = Color.White, Bold = false},
                Font = new Font("Segoe UI", 9)
            };

            return scheme;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre ovladacie prvky na predvolene hodnoty
        /// </summary>
        public static ControlsColorScheme SetDefaultControlsScheme()
        {
            var scheme = new ControlsColorScheme
            {
                Panel = new ColorSetting { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText, DisableFontEdit = true },
                Button = new ColorSetting { BackColor = SystemColors.ButtonFace, ForeColor = SystemColors.ControlText, DisableFontEdit = true },
                Label = new ColorSetting { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText, DisableFontEdit = true },
                Box = new ColorSetting { BackColor = SystemColors.ControlLightLight, ForeColor = SystemColors.ControlText, DisableFontEdit = true },
                Border = new ColorSetting { ForeColor = SystemColors.InactiveBorder, DisableFontEdit = true, DisableBackColorEdit = true },
                Mark = new ColorSetting { ForeColor = SystemColors.ControlText, DisableFontEdit = true, DisableBackColorEdit = true },
                Highlight = new ColorSetting { BackColor = SystemColors.Highlight, ForeColor = SystemColors.ControlText, DisableFontEdit = true }
            };

            return scheme;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre ovladacie prvky na predvolene hodnoty pre tmavy rezim
        /// </summary>
        public static ControlsColorScheme SetDefaultDarkControlsScheme()
        {
            var scheme = new ControlsColorScheme
            {
                Panel = new ColorSetting {BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, DisableFontEdit = true},
                Button = new ColorSetting {BackColor = Color.FromArgb(51, 51, 55), ForeColor = Color.White, DisableFontEdit = true},
                Label = new ColorSetting {BackColor = Color.FromArgb(51, 51, 55), ForeColor = Color.White, DisableFontEdit = true},
                Box = new ColorSetting {BackColor = Color.FromArgb(37, 37, 38), ForeColor = Color.White, DisableFontEdit = true},
                Border = new ColorSetting {ForeColor = Color.FromArgb(62, 62, 66), DisableFontEdit = true, DisableBackColorEdit = true},
                Mark = new ColorSetting {ForeColor = Color.FromArgb(165, 165, 165), DisableFontEdit = true, DisableBackColorEdit = true},
                Highlight = new ColorSetting {BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, DisableFontEdit = true}
            };

            return scheme;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre stlpec Typ vlaku na predvolene hodnoty pre tmavy rezim
        /// </summary>
        public static TrainTypeColumnScheme SetTrainTypeColumnSchemeDark(Color back)
        {
            var scheme = new TrainTypeColumnScheme
            {
                Os = new ColorSetting { ForeColor = Color.White, BackColor = back, Bold = false },
                R = new ColorSetting { ForeColor = Color.Red, BackColor = back, Bold = true },
                X = new ColorSetting { ForeColor = Color.Green, BackColor = back, Bold = true },
                Sl = new ColorSetting { ForeColor = Color.OrangeRed, BackColor = back, Bold = false },
                Font = new Font("Segoe UI", 9)
            };

            return scheme;
        }

        /// <summary>
        ///     Nastavi nastavenia farieb pre editor TabTab na predvolene hodnoty pre tmavy rezim
        /// </summary>
        public static TabTabEditorScheme SetTabTabEditorSchemeDark()
        {
            var scheme = new TabTabEditorScheme
            {
                Default = new ColorSetting { ForeColor = Color.White, Bold = false, DisableBackColorEdit = true },
                Function = new ColorSetting { ForeColor = Color.DodgerBlue, Bold = false, DisableBackColorEdit = true },
                Identifier = new ColorSetting { ForeColor = Color.LightGreen, Bold = false, DisableBackColorEdit = true },
                Number = new ColorSetting { ForeColor = Color.PaleGoldenrod, Bold = false, DisableBackColorEdit = true },
                String = new ColorSetting { ForeColor = Color.Coral, Bold = false, DisableBackColorEdit = true },
                Comment = new ColorSetting { ForeColor = Color.Green, Bold = false, DisableBackColorEdit = true },
                Var = new ColorSetting { ForeColor = Color.Aquamarine, Bold = false, DisableBackColorEdit = true },
                Event = new ColorSetting { ForeColor = Color.Goldenrod, Bold = false, DisableBackColorEdit = true },
                OnNewLine = new ColorSetting { ForeColor = Color.OrangeRed, Bold = false, DisableBackColorEdit = true },
                Operator = new ColorSetting { ForeColor = Color.White, Bold = false, DisableBackColorEdit = true },
                Constant = new ColorSetting { ForeColor = Color.LightSlateGray, Bold = false, DisableBackColorEdit = true },
                SelBraces = new ColorSetting { ForeColor = Color.LightGray, BackColor = Color.BlueViolet, Bold = false },
                SelBraceBad = new ColorSetting { ForeColor = Color.Red, BackColor = Color.LightGray, Bold = false },
                Font = new Font("Consolas", 10)
            };

            return scheme;
        }
    }
}