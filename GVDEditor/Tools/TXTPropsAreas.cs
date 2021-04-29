using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GVDEditor.Tools
{
    internal class TXTPropsAreas
    {
        private readonly Dictionary<string, string> Areas;
        private readonly string Filename;

        public TXTPropsAreas(string file, bool write = false)
        {
            Filename = file;
            Areas = new Dictionary<string, string>();

            if (!write) LoadFromFile(file);
        }

        public string Get(string area)
        {
            return Areas.ContainsKey(area) ? Areas[area] : null;
        }

        public IEnumerable<string> GetAreas()
        {
            return Areas.Keys.ToList();
        }

        public void Set(string area, object value)
        {
            if (Areas.ContainsKey(area))
                Areas[area] = value.ToString();
            else
                Areas.Add(area, value.ToString());
        }

        public void Save()
        { 
            var file = new StreamWriter(Filename, false, GlobData.AnsiEncoding);

            foreach (var area in Areas.Keys)
            {
                file.WriteLine("[" + area + "]");
                file.WriteLine(Areas[area]);
                file.WriteLine(Environment.NewLine);
            }

            file.Close();
        }

        private void LoadFromFile(string file)
        {
            var actualArea = "";
            var sb = new StringBuilder();

            foreach (var line in File.ReadAllLines(file, GlobData.AnsiEncoding))
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (sb.Length != 0 || !string.IsNullOrEmpty(actualArea)) Areas.Add(actualArea, sb.ToString());

                    sb = new StringBuilder();
                    var s = line.Replace("[", "");
                    actualArea = s.Replace("]", "");
                }

                if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && !line.StartsWith("[") && !line.EndsWith("]")) 
                    sb.AppendLine(line);
            }

            Areas.Add(actualArea, sb.ToString());
        }
    }
}