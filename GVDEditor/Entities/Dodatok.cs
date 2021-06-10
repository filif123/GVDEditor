using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GVDEditor.Tools;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Dodatkové hlásenie
    /// </summary>
    public sealed class Dodatok
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Dodatok()
        {
            ChosenReports = new List<ChosenReportType>();
        }

        /// <summary>
        ///     Fyzický zvuk
        /// </summary>
        public FyzZvuk Sound { get; set; }

        /// <summary>
        ///     Názov dodatkového hlásenia (zvyčajne Dxxxx, x - 0-9)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     V akých reportoch sa má dodatok hlásiť
        /// </summary>
        public List<ChosenReportType> ChosenReports { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Konveruje binárne pole na dodatkové hlásenie zo všetkými dátami
        /// </summary>
        /// <param name="sound">fyzický zvuk</param>
        /// <param name="nums">binárne pole vo forme reťazca</param>
        /// <param name="reportTypes">dostupné typy reportov</param>
        /// <param name="reportVariants">dostupné varianty reportov</param>
        /// <param name="smerovanie">smerovanie vlaku</param>
        /// <returns>dodatkové hlásenie</returns>
        /// <exception cref="FormatException"></exception>
        public static Dodatok NumsToDodatok(FyzZvuk sound, string nums, List<ReportType> reportTypes, List<ReportVariant> reportVariants, Routing smerovanie)
        {
            var dodatok = new Dodatok {Sound = sound, Name = sound.Name.Replace("D", "")};

            if (!Utils.IsInt(nums)) throw new FormatException("Pole dodatku neobsahuje iba čísla.");

            var numChars = nums.ToCharArray();

            foreach (var c in numChars)
                if (!c.Equals('0') && !c.Equals('1'))
                    throw new FormatException("Pole dodatku musí obsahovať iba číslice 0 a 1.");

            var countVariants = reportVariants.Count;
            List<ReportType> rightTypes;

            if (smerovanie == Routing.Vychadzajuci)
                rightTypes = reportTypes.Where(t => t.BaseTrain).ToList();
            else if (smerovanie == Routing.Prechadzajuci)
                rightTypes = reportTypes.Where(t => t.PassThrough).ToList();
            else
                rightTypes = reportTypes.Where(t => t.TerminateTrain).ToList();

            var rightMapLength = rightTypes.Count * countVariants;

            if (rightMapLength != numChars.Length)
                throw new FormatException($"Mapa dodatku má nesprávnu dĺžku ({numChars.Length}). Mala by mať dĺžku {rightMapLength}.");

            var ch = 0;
            for (var i = 0; i < rightTypes.Count; i++)
            {
                var vars = new List<ReportVariant>();
                for (var j = 0; j < countVariants; j++)
                {
                    if (numChars[ch].Equals('1')) vars.Add(reportVariants[j]);
                    ch++;
                }

                dodatok.ChosenReports.Add(new ChosenReportType {Type = reportTypes[i], Variants = vars});
            }

            return dodatok;
        }

        /// <summary>
        ///     Konveruje dodatkové hlásenie na binárne pole
        /// </summary>
        /// <param name="dodatok">dodatkové hlásenie</param>
        /// <param name="reportTypes">dostupné typy reportov</param>
        /// <param name="reportVariants">dostupné varianty reportov</param>
        /// <returns>binárne pole vo forme reťazca</returns>
        public static string DodatokToNums(Dodatok dodatok, List<ReportType> reportTypes,
            List<ReportVariant> reportVariants)
        {
            var nums = new StringBuilder();

            for (var i = 0; i < reportTypes.Count; i++)
                if (i < dodatok.ChosenReports.Count && dodatok.ChosenReports[i].Type == reportTypes[i])
                    foreach (var variant in reportVariants)
                        nums.Append(dodatok.ChosenReports[i].Variants.Contains(variant) ? '1' : '0');
                else
                    for (var j = 0; j < reportVariants.Count; j++)
                        nums.Append('0');

            return nums.ToString();
        }
    }
}