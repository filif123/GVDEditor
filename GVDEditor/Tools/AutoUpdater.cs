using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda zisťujúca aktuálnosť spustenej verzie programu
    /// </summary>
    internal static class AutoUpdater
    {
        /// <summary>
        ///     Zistí, či je aktuálne spustená verzia programu aktuálna. V prípade nedostupnosti zdroja vráti
        ///     <see langword="false" />.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool UpdateAvailable(out string version)
        {
            Version thisVersion = Utils.GetAppVersion();

            var allVersions = GetAllVersions();
            foreach (Version v in allVersions)
                if (v > thisVersion)
                {
                    version = v.ToString();
                    return true;
                }

            version = null;
            return false;
        }

        /// <summary>
        ///     Vráti, zoznam všetkých verzií zo servera. V prípade nedostupnosti zdroja vráti prázdny zoznam.
        /// </summary>
        /// <returns></returns>
        private static List<Version> GetAllVersions()
        {
            var allversions = new List<Version>();

            try
            {
                var web = new WebClient();
                Stream stream = web.OpenRead("http://iniss.6f.sk/gvdeditor-updater/update.txt");
                using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
                var text = reader.ReadToEnd();
                var csv = new CsvStringReader(text);

                for (var i = 0; i < csv.RowCount; i++)
                {
                    var version = new Version(csv[i, 0]);
                    allversions.Add(version);
                }
            }
            catch
            {
                //ignored
            }

            return allversions;
        }
    }
}