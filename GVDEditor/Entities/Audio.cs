namespace GVDEditor.Entities
{
    /// <summary>
    ///     Audio linka
    /// </summary>
    public sealed class Audio
    {
        /// <summary>
        ///     Stanica audio linky (použije sa len identifikátor stanice)
        /// </summary>
        public Station Station { get; set; }

        /// <summary>
        ///     Názov audio linky
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Skrátený názov
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///     Názov fronty
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        ///     Mixer
        /// </summary>
        public string Mixer { get; set; }

        /// <summary>
        ///     Zvukova karta
        /// </summary>
        public string SoundCard { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}