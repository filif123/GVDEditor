namespace GVDEditor.Entities
{
    /// <summary>
    ///     Nástupište na stanici
    /// </summary>
    public sealed class Platform
    {
        /// <summary>
        ///     Predvolené nástupište
        /// </summary>
        public static readonly Platform None = new("N","Nedefinované","");
        
        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="key">kľúč nástupišťa</param>
        /// <param name="fullName">celý názov nástupišťa</param>
        /// <param name="soundName">názov zvuku nástupišťa (bez prípony)</param>
        public Platform(string key, string fullName, string soundName)
        {
            Key = key;
            FullName = fullName;
            SoundName = soundName;
        }

        /// <summary>
        ///     Kľúč nástupišťa
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     Celý názov nástupišťa
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Názov zvuku nástupišťa (bez prípony)
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        ///     This
        /// </summary>
        public Platform This => this;

        /// <summary>
        ///     Porovná nástupišťa (porovná iba ich kľúče)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsKeys(Platform other)
        {
            return other.Key == Key;
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return Key;
        }

        /// <summary>
        ///     Porovná nástupišťa vo všetkých vlastnostiach
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Platform nast && nast.Key == Key && nast.FullName == FullName && nast.SoundName == SoundName;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Key != null ? Key.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SoundName != null ? SoundName.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:GVDEditor.Entities.Platform" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Platform left, Platform right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:GVDEditor.Entities.Platform" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Platform left, Platform right)
        {
            return !Equals(left, right);
        }
    }
}