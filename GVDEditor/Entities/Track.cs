﻿namespace GVDEditor.Entities;

/// <summary>
///     Trieda reprezentujuca kolaj v stanici.
/// </summary>
public sealed record Track()
{
    /// <summary>
    ///     Predvolená koľaj - ziadna.
    /// </summary>
    public static readonly Track None = new("K", "K", "Nedefinovaná", Platform.None, "", "Nedefinovaná");

    /// <summary>
    ///     Vytvori novu instanciu triedy typu <see cref="Track"/> so zadanymi vlastnostami.
    /// </summary>
    public Track(string key, string name, string fullname, Platform platform, string sound, string trackName) : this()
    {
        Key = key;
        Name = name;
        FullName = fullname;
        Platform = platform;
        SoundName = sound;
        TrackName = trackName;
    }

    /// <summary>
    ///     Identifikátor kolaje.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Názov kolaje.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Cely názov kolaje.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    ///     Nastupiste kolaje.
    /// </summary>
    public Platform Platform { get; set; }

    /// <summary>
    ///     Názov kolaje.
    /// </summary>
    public string TrackName { get; set; }

    /// <summary>
    ///     Nazov zvuku kolaje.
    /// </summary>
    public string SoundName { get; set; }

    /// <summary>
    ///     Logicke tabule nachadzajuce sa na tejto kolaji.
    /// </summary>
    public List<TableLogical> Tables { get; } = new();

    /// <summary>
    ///     Odkaz na seba, pouzite pre DataSource.
    /// </summary>
    public Track This => this;

    /// <summary>
    ///     Vyhlada kolaj z listu podla zadaneho identifikatora kolaje.
    /// </summary>
    /// <param name="tracks"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Track GetFromID(IEnumerable<Track> tracks, string id) => tracks.FirstOrDefault(kolaj => kolaj.Key == id);

    /// <summary>
    ///     Porovna identifikatory kolaji
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool EqualsKeys(Track other) => other.Key == Key;

    /// <inheritdoc />
    public override string ToString() => Key;
}