﻿using System.Xml.Serialization;
using ToolsCore.XML;

namespace GVDEditor.XML;

/// <summary>
///     Konfiguracny subor pre program GVDEditor.
/// </summary>
[XmlRoot("CONFIG")]
public record GVDEditorConfig() : ConfigBase
{
    /// <summary>
    ///     Povolit pouzivanie automatickej spravy variant vlaku.
    /// </summary>
    [XmlElement("AutoVariant"), DefaultValue(false)]
    public bool AutoVariant { get; set; }

    /// <summary>
    ///     Nastavi jazyk generovania datumovych obmedzeni.
    /// </summary>
    [XmlElement("DateRemLocate"), DefaultValue(0)] 
    [Description("Nastavi jazyk generovania datumovych obmedzeni.")]
    public AppLanguage DateLimitLocate { get; set; } = AppLanguage.Slovak;

    /// <summary>
    ///     Povoli/zakaze kontrolovanie ci je aktualizacia programu dostupna.
    /// </summary>
    [XmlElement("DisableVariantChk"), DefaultValue(false)]
    public bool DisableVariantCheck { get; set; }

    /// <summary>
    ///     Automaticky generovat texty do tabul pri ukladani do suborov.
    /// </summary>
    [XmlElement("AutoTableText"), DefaultValue(false)]
    public bool AutoTableText { get; set; }

    /// <summary>
    ///     Nastavi cas v milisekundach medzi zvukmi pri prehravani.
    /// </summary>
    [XmlElement("PlayerSoundsOffset"), DefaultValue(0)]
    public int PlayerSoundsOffset { get; set; }

    /// <summary>
    ///     Stlpce zobrazujuce sa v tabulke na pracovnej ploche programu.
    /// </summary>
    [XmlElement("DesktopCols")] 
    public DesktopColumns DesktopCols { get; set; } = new();

    /// <summary>
    ///     Klávesové skratky pre akcie na pracovnej ploche programu.
    /// </summary>
    [XmlElement("Shortcuts")] 
    public AppShortcuts Shortcuts { get; set; } = new();

    /// <summary>
    ///     konfiguracia spustania INISSu z tohto programu.
    /// </summary>
    [XmlElement("StartupINISSConfig")] 
    public StartupINISS StartupINISSConfig { get; set; } = new() { CmdArgs = "", RunAsAdmin = false };

    /// <inheritdoc />
    public override string LinkAppSettingsGuide => LinkConsts.LINK_APP_SETTINGS;

    protected GVDEditorConfig(GVDEditorConfig original) : base(original)
    {
        AutoVariant = original.AutoVariant;
        DateLimitLocate = original.DateLimitLocate;
        DisableVariantCheck = original.DisableVariantCheck;
        AutoTableText = original.AutoTableText;
        PlayerSoundsOffset = original.PlayerSoundsOffset;
        DesktopCols = original.DesktopCols with { };
        Shortcuts = original.Shortcuts with { };
        StartupINISSConfig = original.StartupINISSConfig with { };
    }
}