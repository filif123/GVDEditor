using System.Collections.Generic;
using System.Linq;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Trieda reprezentujuca kolaj
    /// </summary>
    public sealed class Track
    {

        /// <summary>
        ///     Predvolená koľaj
        /// </summary>
        public static readonly Track None = new("K", "K", "Nedefinovaná", Platform.None, "", "Nedefinovaná");
        
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Track()
        {
            Tabule = new List<TableLogical>();
        }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Track(string key,string name,string fullname, Platform nastupiste, string sound,string trackName)
        {
            Tabule = new List<TableLogical>();

            Key = key;
            Name = name;
            FullName = fullname;
            Nastupiste = nastupiste;
            SoundName = sound;
            TrackName = trackName;
        }

        /// <summary>
        ///     Identifikátor kolaje
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     Názov kolaje
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Cely názov kolaje
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Nastupiste kolaje
        /// </summary>
        public Platform Nastupiste { get; set; }

        /// <summary>
        ///     Názov kolaje
        /// </summary>
        public string TrackName { get; set; }

        /// <summary>
        ///     Nazov zvuku kolaje
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        ///     Logicke tabule nachadzajuce sa na tejto kolaji
        /// </summary>
        public List<TableLogical> Tabule { get; }

        /// <summary>
        ///     Odkaz na seba, pouzite pre DataSource
        /// </summary>
        public Track This => this;

        /// <summary>
        ///     Cely názov nástupista - len pre potreby GUI
        /// </summary>
        public string NastupisteCelyNazov => Nastupiste.FullName;

        /// <summary>
        ///     Vyhlada kolaj z listu podla zadaneho identifikatora kolaje
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Track GetTrackFromId(IEnumerable<Track> tracks, string id)
        {
            return tracks.FirstOrDefault(kolaj => kolaj.Key == id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Track track &&
                   Key == track.Key &&
                   Name == track.Name &&
                   FullName == track.FullName &&
                   EqualityComparer<Platform>.Default.Equals(Nastupiste, track.Nastupiste) &&
                   TrackName == track.TrackName &&
                   SoundName == track.SoundName &&
                   NastupisteCelyNazov == track.NastupisteCelyNazov;
        }

        /// <summary>
        ///     Porovna identifikatory kolaji
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsKeys(Track other)
        {
            return other.Key == Key;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hashCode = 1716701147;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Platform>.Default.GetHashCode(Nastupiste);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TrackName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SoundName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NastupisteCelyNazov);
            return hashCode;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Track left, Track right)
        {
            return EqualityComparer<Track>.Default.Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Track left, Track right)
        {
            return !(left == right);
        }
    }
}