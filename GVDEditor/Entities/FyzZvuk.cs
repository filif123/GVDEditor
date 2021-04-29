namespace GVDEditor.Entities
{
    /// <summary>
    ///     Trieda reprezentujúca fyzický zvuk zo zvukovej banky
    /// </summary>
    public sealed class FyzZvuk
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="name"></param>
        /// <param name="nazevZvukuPro"></param>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        /// <param name="language"></param>
        public FyzZvuk(FyzZvukDir dir, string name, string nazevZvukuPro, string fileName, string text, Language language)
        {
            Dir = dir;
            Name = name;
            NazevZvukuPro = nazevZvukuPro;
            FileName = fileName;
            Text = text;
            Language = language;
        }

        /// <summary>
        ///     Priečinok, do ktorého fyzický zvuk patrí
        /// </summary>
        public FyzZvukDir Dir { get; }

        /// <summary>
        ///     Názov zvuku
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Názov zvuku (alternatívna forma)
        /// </summary>
        public string NazevZvukuPro { get; }

        /// <summary>
        ///     Názov súboru so zvukom (vrátane prípony)
        /// </summary>
        public string FileName { get; }

        /// <summary>
        ///     Text hlásenia
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     Jazyk hlásenia
        /// </summary>
        public Language Language { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}