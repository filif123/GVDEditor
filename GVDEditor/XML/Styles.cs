using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using GVDEditor.Tools;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Trieda reprezentujuca zoznam stylov definovanych pre GUI programu
    /// </summary>
    [XmlRoot("STYLES")]
    public class Styles
    {
        /// <summary>
        ///     Predvoleny nazov predvoleneho stylu
        /// </summary>
        public const string DEFAULT_STYLE_NAME = "Default";

        /// <summary>
        ///     Predvoleny nazov tmaveho stylu
        /// </summary>
        public const string DARK_STYLE_NAME = "Dark";

        /// <summary>
        ///     Aktualne pouzivany styl v programe
        /// </summary>
        [XmlElement("using")]
        [DefaultValue(0)]
        public int UsingStyleID;

        /// <summary>
        ///     Zoznam vsetkych stylov s vlastnostami
        /// </summary>
        [XmlElement("styles")]
        public List<Style> StyleList;

        /// <summary>
        ///     Indexer pre jednoduchsi vyber z listu stylov
        /// </summary>
        /// <param name="index">index prvku</param>
        public Style this[int index]
        {
            get => StyleList[index];
            set => StyleList[index] = value;
        }

        /// <summary>
        ///     Indexer pre jednoduchsi vyber z listu stylov. Vrati styl podla nazvu stylu
        /// </summary>
        /// <param name="index"></param>
        public Style this[string index] => StyleList.First(i => i.Name == index);

        /// <summary>
        ///     Vrati pocet stylov v zozname
        /// </summary>
        public int Count => StyleList.Count;

        /// <summary>
        ///     Prida styl do zoznamu stylov
        /// </summary>
        /// <param name="style"></param>
        public void Add(Style style)
        {
            StyleList.Add(style);
        }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Styles()
        {
            StyleList = new List<Style>();
            UsingStyleID = 0;
        }

        /// <summary>
        ///     Nacitava data z konfiguracneho suboru
        /// </summary>
        /// <param name="sPathFName">cesta k suboru</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Ak konfiguacny subor obsahoval nelatnu hodnotu</exception>
        public static Styles ReadData(string sPathFName)
        {
            Styles styles = null;
            try
            {
                var text = File.ReadAllText(sPathFName, Encoding.GetEncoding(1250));
                if (!string.IsNullOrEmpty(text))
                {
                    styles = (Styles)XMLSerialization.Deserialize(text, typeof(Styles));

                    List<string> ids = new List<string>(styles.StyleList.Count);
                    ids.AddRange(styles.StyleList.Select(style => style.Name));

                    var query = ids.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

                    if (query.Count != 0)
                    {
                        var error = $"Chyba v súbore štýlov: Štýl {query[0]} je zadefinovaný viackrát.";
                        Log.Error(error);
                        throw new ArgumentException(error);
                    }

                    bool rewrite = false;
                    if (!ids.Contains(DEFAULT_STYLE_NAME))
                    {
                        styles.StyleList.Insert(0, SetDefaultStyle());
                        rewrite = true;
                    }

                    if (!ids.Contains(DARK_STYLE_NAME))
                    {
                        styles.StyleList.Insert(1, SetDarkStyle());
                        rewrite = true;
                    }

                    if (rewrite)
                    {
                        WriteData(sPathFName, styles);
                    }

                    if (styles.UsingStyleID < 0 || styles.UsingStyleID > styles.StyleList.Count - 1)
                    {
                        var error =
                            $"Chyba v súbore štýlov: Nastavenie {nameof(UsingStyleID)} obsahuje nesprávnu hodnotu {styles.UsingStyleID}.";
                        Log.Error(error);
                        throw new ArgumentException(error);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }

            if (styles == null)
            {
                styles = new Styles();
                styles.StyleList.Insert(0, SetDefaultStyle());
                styles.StyleList.Insert(1, SetDarkStyle());
                WriteData(sPathFName, styles);
            }

            return styles;
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

        /// <summary>
        ///     Nastav predvoleny styl
        /// </summary>
        /// <returns>predvoleny predvoleny styl</returns>
        public static Style SetDefaultStyle()
        {
            var style = new Style {Name = DEFAULT_STYLE_NAME};
            return style;
        }

        /// <summary>
        ///     Nastav tmavy styl
        /// </summary>
        /// <returns>predvoleny tmavy styl</returns>
        public static Style SetDarkStyle()
        {
            Style style = SetDefaultStyle();
            style.Name = DARK_STYLE_NAME;
            style.ControlsDefaultStyle = false;
            style.DarkScrollBar = true;
            style.DarkTitleBar = true;
            style.ControlsColorScheme = Style.SetDefaultDarkControlsScheme();
            style.TabTabEditorScheme = Style.SetTabTabEditorSchemeDark();
            style.TrainTypeColumnScheme = Style.SetTrainTypeColumnSchemeDark(style.ControlsColorScheme.Box.BackColor);
            return style;
        }
    }
}
