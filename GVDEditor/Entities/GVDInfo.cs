using System;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Informácie o grafikone
    /// </summary>
    public sealed class GVDInfo
    {
        /// <summary>
        ///     Kategória
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        ///     Podkategória
        /// </summary>
        public int Subcat { get; set; }

        /// <summary>
        ///     Reprezentuje stanicu, pre ktorú bol grafikon robený
        /// </summary>
        public Station ThisStation { get; set; }

        /// <summary>
        ///     Počet vlakov
        /// </summary>
        public int TrainCount { get; set; }

        /// <summary>
        ///     Začiatok platnosti rozvrhu vlakov
        /// </summary>
        public DateTime StartValidTimeTable { get; set; }

        /// <summary>
        ///     Koniec platnosti rozvrhu vlakov
        /// </summary>
        public DateTime EndValidTimeTable { get; set; }

        /// <summary>
        ///     Začiatok platnosti dát
        /// </summary>
        public DateTime StartValidData { get; set; }

        /// <summary>
        ///     Koniec platnosti dát
        /// </summary>
        public DateTime EndValidData { get; set; }

        /// <summary>
        ///     Dátum vytvorenia/úpravy grafikonu
        /// </summary>
        public DateTime CreateData { get; set; }

        /// <summary>
        ///     TTIndex
        /// </summary>
        public int TTIndex { get; set; }

        /// <summary>
        ///     VLIndex
        /// </summary>
        public int VLIndex { get; set; }

        /// <summary>
        ///     STIndex
        /// </summary>
        public int STIndex { get; set; }

        /// <summary>
        ///     IsRegionText
        /// </summary>
        public bool IsRegionText { get; set; }

        /// <summary>
        ///     OnlyCityVLIndex
        /// </summary>
        public int OnlyCityVLIndex { get; set; }

        private bool Equals(GVDInfo other)
        {
            return Category == other.Category && Subcat == other.Subcat && Equals(ThisStation, other.ThisStation) &&
                   TrainCount == other.TrainCount && StartValidTimeTable.Equals(other.StartValidTimeTable) &&
                   EndValidTimeTable.Equals(other.EndValidTimeTable) && StartValidData.Equals(other.StartValidData) &&
                   EndValidData.Equals(other.EndValidData) && CreateData.Equals(other.CreateData) &&
                   TTIndex == other.TTIndex && VLIndex == other.VLIndex && STIndex == other.STIndex &&
                   IsRegionText == other.IsRegionText && OnlyCityVLIndex == other.OnlyCityVLIndex;
        }

        /// <summary>
        ///     Porovna GVDInfo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GVDInfo) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Category;
                hashCode = (hashCode * 397) ^ Subcat;
                hashCode = (hashCode * 397) ^ (ThisStation != null ? ThisStation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ TrainCount;
                hashCode = (hashCode * 397) ^ StartValidTimeTable.GetHashCode();
                hashCode = (hashCode * 397) ^ EndValidTimeTable.GetHashCode();
                hashCode = (hashCode * 397) ^ StartValidData.GetHashCode();
                hashCode = (hashCode * 397) ^ EndValidData.GetHashCode();
                hashCode = (hashCode * 397) ^ CreateData.GetHashCode();
                hashCode = (hashCode * 397) ^ TTIndex;
                hashCode = (hashCode * 397) ^ VLIndex;
                hashCode = (hashCode * 397) ^ STIndex;
                hashCode = (hashCode * 397) ^ IsRegionText.GetHashCode();
                hashCode = (hashCode * 397) ^ OnlyCityVLIndex;
                return hashCode;
            }
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:GVDEditor.Entities.GVDInfo" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(GVDInfo left, GVDInfo right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:GVDEditor.Entities.GVDInfo" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(GVDInfo left, GVDInfo right)
        {
            return !Equals(left, right);
        }
    }
}