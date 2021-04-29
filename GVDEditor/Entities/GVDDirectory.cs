namespace GVDEditor.Entities
{
    /// <summary>
    ///     Spojenie vlastností <see cref="DirList" /> a <see cref="GVDInfo" /> s ďalšími vlastnosťami
    ///     (používa sa len pre potreby GUI)
    /// </summary>
    public sealed class GVDDirectory
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="period">obdobie platnosti ako text (napr. 2020/2021)</param>
        /// <param name="dir">informácie o priečinku s grafikonom</param>
        /// <param name="gvd">informácie grafiku</param>
        public GVDDirectory(string period, DirList dir, GVDInfo gvd)
        {
            Period = period;
            Dir = dir;
            GVD = gvd;
        }

        /// <summary>
        ///     Obdobie
        /// </summary>
        public string Period { get; }

        /// <summary>
        ///     Formátovaný reťazec obdobia (názov stanice + obdobie)
        /// </summary>
        public string PeriodFormatted => GVD.ThisStation.Name + " " + Period;

        /// <summary>
        ///     Priečinok s dátami grafikonu
        /// </summary>
        public DirList Dir { get; }

        /// <summary>
        ///     Informácie o grafikone
        /// </summary>
        public GVDInfo GVD { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Period;
        }
    }
}