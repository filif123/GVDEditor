using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GVDEditor.Tools
{
    internal static class AppRegistry
    {
        public static List<string> GetRecentDirs()
        {
            var key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{Application.ProductName}");
            var dirs = new HashSet<string>();

            if (key?.GetValue("RecentDirs") != null)
            {
                var dirsString = key.GetValue("RecentDirs").ToString();
                var dirsArray = dirsString.Split(';');

                foreach (var dir in dirsArray)
                    if (!string.IsNullOrEmpty(dir))
                        dirs.Add(dir);
            }

            return dirs.ToList();
        }

        public static void SetNewRecentDir(string path)
        {
            var key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.ProductName}");

            if (key != null)
            {
                var sb = new StringBuilder();
                var dirs = GetRecentDirs();
                foreach (var dir in dirs)
                {
                    if (dir == path) return;

                    sb.Append(";");
                    sb.Append(dir);
                }

                sb.Append(";");
                sb.Append(path);

                key.SetValue("RecentDirs", sb.ToString());
            }
        }

        public static string[] GetINISSRegisters()
        {
            var bit64 = Environment.Is64BitOperatingSystem;
            var key = Registry.LocalMachine.OpenSubKey(bit64 ? @"SOFTWARE\WOW6432Node\CHAPS" : @"SOFTWARE\CHAPS");
            List<string> res = new List<string> {""};
            if (key is not null)
            {
                res.AddRange(key.GetSubKeyNames());
            }

            return res.ToArray();
        }
    }
}