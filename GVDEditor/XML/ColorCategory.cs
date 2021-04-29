using System.Collections.Generic;
using System.Drawing;

namespace GVDEditor.XML
{
    internal class ColorCategory
    {
        public string Name { get; set; }
        public Font Font { get; set; }
        public bool DisableFontEdit { get; set; }
        public List<ColorSetting> Settings { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}