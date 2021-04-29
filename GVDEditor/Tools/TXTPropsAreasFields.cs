using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GVDEditor.Tools
{
    internal class TXTPropsAreasFields
    {
        private const string EX_MESSAGE = "Súbor {0}: V oblasti {1} sa očakávala vlastnosť {2}.";

        private readonly Dictionary<string, Dictionary<string, string>> Areas;
        private readonly Dictionary<string, string> AreasComments;
        private readonly string Filename;

        public TXTPropsAreasFields(string file, bool write = false)
        {
            Filename = file;
            Areas = new Dictionary<string, Dictionary<string, string>>();
            AreasComments = new Dictionary<string, string>();

            if (!write) LoadFromFile(file);
        }

        public string Get(string area, string field, string defValue)
        {
            return Get(area, field, false) ?? defValue;
        }

        public string Get(string area, string field, bool nullSensitive = true)
        {
            if (nullSensitive)
                return Areas.ContainsKey(area) && Areas[area].ContainsKey(field)
                    ? Areas[area][field]
                    : throw new ArgumentNullException("", string.Format(EX_MESSAGE, Filename, area, field));

            return Areas.ContainsKey(area) && Areas[area].ContainsKey(field) ? Areas[area][field] : null;
        }

        public Dictionary<string, string> Get(string area)
        {
            return Areas.ContainsKey(area) ? Areas[area] : null;
        }

        public string GetComment(string area)
        {
            return AreasComments.ContainsKey(area) ? AreasComments[area] : "";
        }

        public Dictionary<string, Dictionary<string, string>> GetAll()
        {
            return Areas;
        }

        public List<string> GetAreas()
        {
            return Areas.Keys.ToList();
        }

        public void Set(string area, string field, object value, WriteType type = WriteType.WRITE_NUM)
        {
            if (type != WriteType.WRITE_NUM)
            {
                if (value == null)
                {
                    if (type == WriteType.WRITE_STRING_ANSI || type == WriteType.WRITE_STRING_UTF)
                        throw new ArgumentNullException("", string.Format(EX_MESSAGE, Filename, area, field));

                    value = "".ToQuotedString();
                }
                else
                {
                    var vals = value as string;

                    if (type == WriteType.WRITE_STRING_ANSI || type == WriteType.WRITE_STRING_ANSI_NULLABLE)
                        value = vals.ToQuotedString().UTFtoANSI();
                    else
                        value = vals.ToQuotedString();
                }
            }

            if (Areas.ContainsKey(area))
            {
                if (!Areas[area].ContainsKey(field))
                    Areas[area].Add(field, value.ToString());
                else
                    Areas[area][field] = value.ToString();
            }
            else
            {
                var keys = new Dictionary<string, string> {{field, value.ToString()}};
                Areas.Add(area, keys);
            }
        }

        public void Set(string area, Dictionary<string, object> fields)
        {
            foreach (var field in fields.Keys)
                if (Areas.ContainsKey(area))
                {
                    if (!Areas[area].ContainsKey(field))
                        Areas[area].Add(field, fields[field].ToString());
                    else
                        Areas[area][field] = fields[field].ToString();
                }
                else
                {
                    var keys = new Dictionary<string, string>
                    {
                        {field, fields[field].ToString()}
                    };
                    Areas.Add(area, keys);
                }
        }

        public void SetComment(string area, string comment)
        {
            if (AreasComments.ContainsKey(area))
                AreasComments[area] = comment;
            else
                AreasComments.Add(area, comment);
        }

        public void Save()
        {
            var file = new StreamWriter(Filename, false, Encoding.GetEncoding(1250));

            foreach (var area in Areas.Keys)
            {
                file.WriteLine("[" + area + "]");
                if (AreasComments.ContainsKey(area))
                {
                    var lines = AreasComments[area].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    for (var i = 0; i < lines.Length; i++)
                        if (!string.IsNullOrEmpty(lines[i]))
                        {
                            lines[i] = lines[i].Insert(0, ";");
                            file.WriteLine(lines[i]);
                        }
                }

                foreach (var field in Areas[area].Keys)
                    if (!string.IsNullOrWhiteSpace(Areas[area][field]))
                        file.WriteLine(field + "=" + Areas[area][field]);


                file.WriteLine(Environment.NewLine);
            }

            file.Close();
        }

        private void LoadFromFile(string file)
        {
            var comments = new StringBuilder();
            var actualArea = "";
            var propInArea = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(file, Encoding.GetEncoding(1250)))
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && !line.StartsWith("#") &&
                    !line.StartsWith("'") && line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (propInArea.Count != 0 || !string.IsNullOrEmpty(actualArea))
                    {
                        Areas.Add(actualArea, propInArea);
                        AreasComments.Add(actualArea, comments.ToString());
                    }

                    propInArea = new Dictionary<string, string>();
                    var s = line.Replace("[", "");
                    actualArea = s.Replace("]", "");
                    comments = new StringBuilder();
                }
                else if (!string.IsNullOrEmpty(line) && line.StartsWith(";"))
                {
                    comments.AppendLine(line.Substring(1));
                }

                if (!string.IsNullOrEmpty(line) && !line.StartsWith(";") && !line.StartsWith("#") &&
                    !line.StartsWith("'") && line.Contains('='))
                {
                    var index = line.IndexOf('=');
                    var key = line.Substring(0, index).Trim();
                    var value = line.Substring(index + 1).Trim();

                    if (value.StartsWith("\"") && value.EndsWith("\"") ||
                        value.StartsWith("'") && value.EndsWith("'"))
                        value = value.Substring(1, value.Length - 2);

                    try
                    {
                        propInArea.Add(key, value);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            Areas.Add(actualArea, propInArea);
            AreasComments.Add(actualArea, comments.ToString());
        }
    }

    /// <summary>
    ///     Typ zapisu prvku do slovnika
    /// </summary>
    public enum WriteType
    {
        /// <summary>
        ///     Zapis cisla (hodnota bez uvodzoviek)
        /// </summary>
        WRITE_NUM,

        /// <summary>
        ///     Zapis retazca UTF8 (v pripade ze je prvok <see langword="null" />, vyhodi vynimku)
        /// </summary>
        WRITE_STRING_UTF,

        /// <summary>
        ///     Zapis retazca UTF8 (v pripade ze je prvok <see langword="null" />, vlozi do slovnika "")
        /// </summary>
        WRITE_STRING_UTF_NULLABLE,

        /// <summary>
        ///     Zapis retazca ANSI (v pripade ze je prvok <see langword="null" />, vyhodi vynimku)
        /// </summary>
        WRITE_STRING_ANSI,

        /// <summary>
        ///     Zapis retazca ANSI (v pripade ze je prvok <see langword="null" />, vlozi do slovnika "")
        /// </summary>
        WRITE_STRING_ANSI_NULLABLE
    }
}