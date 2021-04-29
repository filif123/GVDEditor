using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GVDEditor.Tools
{
    internal class TXTProps
    {
        private const string EX_MESSAGE = "Súbor {0}: Očakávala sa vlastnosť {1}.";
        
        private readonly Dictionary<string, string> List;
        private readonly string Filename;

        public TXTProps(string file, bool write = false)
        {
            Filename = file;
            List = new Dictionary<string, string>();

            if (!write) LoadFromFile(file);
        }

        public string Get(string field, string defValue)
        {
            return Get(field, false) ?? defValue;
        }

        public string Get(string field, bool nullSensitive = true)
        {
            if (nullSensitive)
                return List.ContainsKey(field) ? List[field] : throw new ArgumentNullException("", string.Format(EX_MESSAGE, Filename, field));

            return List.ContainsKey(field) ? List[field] : null;
        }

        public void Set(string field, object value)
        {
            if (!List.ContainsKey(field))
                List.Add(field, value.ToString());
            else
                List[field] = value.ToString();
        }

        public void Save()
        {
            var file = new StreamWriter(Filename, false, GlobData.AnsiEncoding);

            foreach (var prop in List.Keys.ToArray())
                if (!string.IsNullOrWhiteSpace(List[prop]))
                    file.WriteLine(prop + "=" + List[prop]);

            file.Close();
        }

        private void LoadFromFile(string file)
        {
            foreach (var line in File.ReadAllLines(file, GlobData.AnsiEncoding))
                if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && !line.StartsWith("#") && !line.StartsWith("'") && line.Contains('='))
                {
                    var index = line.IndexOf('=');
                    var key = line.Substring(0, index).Trim();
                    var value = line.Substring(index + 1).Trim();

                    if (value.StartsWith("\"") && value.EndsWith("\"") ||
                        value.StartsWith("'") && value.EndsWith("'"))
                        value = value.Substring(1, value.Length - 2);

                    try
                    {
                        List.Add(key, value);
                    }
                    catch
                    {
                        // ignored
                    }
                }
        }
    }
}