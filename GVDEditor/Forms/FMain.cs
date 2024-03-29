﻿using System.Text.RegularExpressions;
using ExControls;
using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;
using Microsoft.VisualBasic.FileIO;
using ToolsCore;
using ToolsCore.Entities;
using ToolsCore.Forms;
using ToolsCore.Tools;
using ToolsCore.XML;
using AppRegistry = ToolsCore.Tools.AppRegistry;

namespace GVDEditor.Forms;

/// <summary>
///     Hlavný formulár
/// </summary>
public partial class FMain : Form
{
    /// <summary>
    ///     Dostupné stanice.
    /// </summary>
    public static BindingList<string> Stanice { get; } = new();

    /// <summary>
    ///     Všetky dostupne priečinky s grafikonmi.
    /// </summary>
    public static BindingList<GVDDirectory> ObdobiaList { get; } = new();

    private readonly List<GVDDirectory> _gvdDirs = new();
    private Process _actualINISSProcess;
    private bool _error;
    private bool _dataSaved = true;
    private string _lastINISSStart;
    private GVDDirectory _newDir;
    private bool _prechod;
    private GVDDirectory _previousSelectedGVD;
    private bool _removingGVD;
    private FWait _waitForm;

    /// <summary>
    ///     Vytvori nový formulár typu <see cref="FMain"/>.
    /// </summary>
    public FMain()
    {
        InitializeComponent();

        mainMenu.Renderer = new ToolStripProfessionalRenderer(new FormUtils.LightColorTable());

        tsslSelTrainName.Font = GlobData.Config.Fonts.StateRow.Font;
        tsslSelTrainVariants.Font = GlobData.Config.Fonts.StateRow.Font;
        tsslTrainCount.Font = GlobData.Config.Fonts.StateRow.Font;
        tsslTrainCountWithVariants.Font = GlobData.Config.Fonts.StateRow.Font;

        dgvTrains.RowHeadersVisible = GlobData.Config.ShowRowsHeader;

        if (!GlobData.PrivateFeatures)
        {
            tsmiImportELIS.Visible = false;
            tsmimImportELIS.Visible = false;
        }

        SetShortcuts();
        SetColumns();
        SetColumnsAutoWidth();

        switch (GlobData.Config.DesktopMenuMode)
        {
            case DesktopMenu.MsTs:
                mainMenu.Visible = true;
                toolMenu.Visible = true;
                mainMenu.Items.Remove(tscbObdobie);
                mainMenu.Items.Remove(tscbStanica);
                break;
            case DesktopMenu.MsOnly:
                mainMenu.Visible = true;
                toolMenu.Visible = false;
                mainMenu.Items.Add(tscbObdobie);
                mainMenu.Items.Add(tscbStanica);
                break;
            case DesktopMenu.TsOnly:
                mainMenu.Visible = false;
                toolMenu.Visible = true;
                mainMenu.Items.Remove(tscbObdobie);
                mainMenu.Items.Remove(tscbStanica);
                break;
        }

        smerovanieDataGridViewTextBoxColumn.DefaultCellStyle.NullValue = new Bitmap(1, 1);
        backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
        backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

        var recentDirs = AppRegistry.GetOpenedProjects();

        foreach (var dir in recentDirs)
        {
            var itemA = new ToolStripMenuItem(dir.Path);
            var itemB = new ToolStripMenuItem(dir.Path);

            itemA.Click += RecentDirsClick;
            itemB.Click += RecentDirsClick;
            tsmiRecent.DropDownItems.Add(itemA);
            tssbRecentDirs.DropDownItems.Add(itemB);
        }

        if (recentDirs.Length == 0 || recentDirs[0].Path == "")
        {
            tsmiRecent.Enabled = false;
            tssbRecentDirs.Enabled = false;
        }
        else
        {
            tsmiRecent.Enabled = true;
            tssbRecentDirs.Enabled = true;
        }

        if (tscbStanica.ComboBox != null) tscbStanica.ComboBox.DataSource = Stanice;
        if (tscbObdobie.ComboBox != null) tscbObdobie.ComboBox.DataSource = ObdobiaList;

        this.ApplyThemeAndFonts();
        if (GlobData.UsingStyle.HighlightStatusBar)
        {
            statusStrip.BackColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            tsslSelTrainName.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslSelTrainVariants.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslTrainCount.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslTrainCountWithVariants.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
        }
    }

    private void FMain_Load(object sender, EventArgs e)
    {
        if (GlobData.Config.Startup == StartupType.LastProject)
        {
            var path = AppRegistry.GetLastProject();
            if (!string.IsNullOrWhiteSpace(path)) 
                OpenRecentProject(path);
        }
    }

    private bool DataSaved
    {
        get => _dataSaved;
        set
        {
            if(value == _dataSaved)
                return;

            _dataSaved = value;
            if (_dataSaved)
                Text = Text.Replace("*", "");
            else
                Text = Application.ProductName + @" - *" + GlobData.INISSDir;
        } 
    }

    protected override CreateParams CreateParams
    {
        get
        {
            var handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
            return handleParam;
        }
    }

    private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
            try
            {
                ProccessData((PathAndGVD)e.Argument);
            }
            catch (Exception exception)
            {
                Log.Exception(exception);
                Program.MainForm.Invoke(delegate()
                {
                    if (GlobData.Config.DebugModeGUI == DebugMode.OnlyMessage)
                        FError.ShowError(exception.Message);
                    if (GlobData.Config.DebugModeGUI == DebugMode.DetailInfo)
                        FError.ShowError(exception.ToString());
                }
                );

                _error = true;
                return;
            }
        else
            ProccessData((PathAndGVD)e.Argument);

        _error = false;
    }

    private void ProccessData(PathAndGVD pathgvd)
    {
        GlobData.CustomStations = new ExBindingList<Station>(TxtParser.ReadCustomStations(pathgvd.Path, pathgvd.Gvd));

        var (tabtabs, catalogs, physicals, logicals) = TxtParser.ReadTables(pathgvd.Path);
        GlobData.TabTabs = new ExBindingList<TableTabTab>(tabtabs);
        GlobData.TableCatalogs = new ExBindingList<TableCatalog>(catalogs);
        GlobData.TablePhysicals = new ExBindingList<TablePhysical>(physicals);
        GlobData.TableLogicals = new ExBindingList<TableLogical>(logicals);

        var operators = TxtParser.ReadOperators(pathgvd.Path);
        GlobData.Operators = new ExBindingList<Operator>(operators)
        {
            FireEventOnSort = true
        };

        var tracks = TxtParser.ReadTracks(pathgvd.Path);
        GlobData.Tracks = new ExBindingList<Track>(tracks)
        {
            FireEventOnSort = true
        };

        var nastupistia = new HashSet<Platform>();
        foreach (var kolaj in GlobData.Tracks)
            nastupistia.Add(kolaj.Platform);
        GlobData.Platforms = new ExBindingList<Platform>(nastupistia.ToList());

        (GlobData.ReportVariants,GlobData.ReportTypes,GlobData.LocalLanguages) = TxtParser.ReadLocalCategori(pathgvd.Path);
        InitDruhyReportov();

        var allSounds = new List<FyzSound>();
        foreach (var language in GlobData.LocalLanguages)
            allSounds.AddRange(language.IsBasic ? GlobData.Sounds : RawBankParser.ReadFyzZvukFile(GlobData.RawBankDir, language));

        try { GlobData.Radenia = TxtParser.ReadRazeni1(pathgvd.Path, allSounds); }catch (FileNotFoundException) { }

        GlobData.Trains = new ExBindingList<Train>(TxtParser.ReadTrains(pathgvd.Path));

        GlobData.TableTexts = new ExBindingList<TableText>(TxtParser.ReadTTexts(pathgvd.Path, GlobData.Trains));
        GlobData.TableFonts = new ExBindingList<TableFont>(TxtParser.ReadTableFonts(pathgvd.Path));
    }

    private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        _waitForm.Close();

        if (!_error)
        {
            Kolaj.DataSource = GlobData.Tracks;
            Dopravca.DataSource = GlobData.Operators;

            _prechod = true;
            dgvTrains.DataSource = GlobData.Trains;
            _prechod = false;

            SetColumnsAutoWidth();

            //sfunkčniť tlačidlá v tool stripe
            tsbAddTrain.Enabled = true;
            tsbCopyTrain.Enabled = true;
            tsbEditTrain.Enabled = true;
            tsbDeleteTrain.Enabled = true;
            tsbSave.Enabled = true;
            tsmiSave.Enabled = true;
            tsmiAnalyze.Enabled = true;
            tsbAnalyze.Enabled = true;
            tsbStanica.Enabled = true;
            tsbGlobalSettings.Enabled = true;
            tsmiUpravit.Enabled = true;
            tsmimAddTrain.Enabled = true;
            tsmimEditTrain.Enabled = true;
            tsmiDeleteTrain.Enabled = true;
            tsmiDuplikovat.Enabled = true;
            tsmiVlastnostiStanice.Enabled = true;
            tsmiGlobalSettings.Enabled = true;
            tsbAddGVD.Enabled = true;
            tsmiNew.Enabled = true;
            tsmiImport.Enabled = true;
            tsbImport.Enabled = true;
            ChangeEnableMenuItemsGSettings(true);
            ChangeEnableMenuItemsLSettings(true);
        }
        else
        {
            _prechod = true;
            GlobData.Trains.Clear();
            _prechod = false;

            //znefunkcnit tlacidla v tool stripe
            tsbAddTrain.Enabled = false;
            tsbCopyTrain.Enabled = false;
            tsbEditTrain.Enabled = false;
            tsbDeleteTrain.Enabled = false;
            tsbSave.Enabled = false;
            tsbStanica.Enabled = false;
            tsbGlobalSettings.Enabled = false;
            tsmiUpravit.Enabled = false;
            tsmimAddTrain.Enabled = false;
            tsmimEditTrain.Enabled = false;
            tsmiDeleteTrain.Enabled = false;
            tsmiDuplikovat.Enabled = false;
            tsmiVlastnostiStanice.Enabled = false;
            tsmiGlobalSettings.Enabled = false;
            tsmiSave.Enabled = false;
            tsbSave.Enabled = false;
            tsmiAnalyze.Enabled = false;
            tsbAnalyze.Enabled = false;
            tsbAddGVD.Enabled = false;
            tsmiNew.Enabled = false;
            tsmiImport.Enabled = false;
            tsbImport.Enabled = false;
            ChangeEnableMenuItemsGSettings(false);
            ChangeEnableMenuItemsLSettings(false);
        }
    }

    private void DoSave()
    {
        if (GlobData.Config.DebugModeGUI == DebugMode.AppCrash)
            DoSaveInternal();
        else
            try
            {
                DoSaveInternal();
            }
            catch (Exception e)
            {
                var monly = GlobData.Config.DebugModeGUI == DebugMode.OnlyMessage;
                Utils.ShowError(monly ? e.Message : e.ToString());
            }

        void DoSaveInternal()
        {
            var dir = _previousSelectedGVD;

            if (GlobData.Config.AutoTableText) 
                GenerateTableTextWhileSaving(dir);

            TxtParser.WriteTrains(dir.Dir.FullPath, GlobData.Trains, dir.GVD, GlobData.ReportVariants);
            TxtParser.WriteRazeni1(dir.Dir.FullPath, GlobData.Radenia, GlobData.LocalLanguages);
            
            TxtParser.WriteTables(dir.Dir.FullPath, GlobData.TabTabs, GlobData.TableCatalogs, GlobData.TablePhysicals, GlobData.TableLogicals);
            TxtParser.WriteTTexts(dir.Dir.FullPath, GlobData.TableTexts);
            TxtParser.WriteTracks(dir.Dir.FullPath, GlobData.Tracks);
            TxtParser.WriteInfoGVD(dir.Dir.FullPath, dir.GVD);
            TxtParser.WriteOperators(dir.Dir.FullPath, GlobData.Operators);
            TxtParser.WriteModeTabs(dir.Dir.FullPath, GlobData.TableFonts, GlobData.TableFontDir);
            TxtParser.WriteLocalCategori(dir.Dir.FullPath, GlobData.ReportVariants, GlobData.ReportTypes, GlobData.LocalLanguages);
            TxtParser.WriteCustomStations(dir.Dir.FullPath, GlobData.CustomStations, dir.GVD);

            DataSaved = true;
        }
    }

    private void ShowAnalyzeGVD()
    {
        var fan = new FAnalyzer(tscbObdobie.SelectedItem as GVDDirectory);
        fan.ShowDialog();
    }

    private void ShowNewGVD()
    {
        var nsf = new FNewGrafikon();
        var result = nsf.ShowDialog();
        if (result == DialogResult.OK)
        {
            var gvd = nsf.GvdInfo;
            var dir = nsf.NewDir;

            if (Stanice.Count == 0 || ObdobiaList.Count == 0)
            {
                //sfunkcnit tlacidla v toolstripe
                tsbAddGVD.Enabled = true;
                tsbAddTrain.Enabled = true;
                tsbCopyTrain.Enabled = true;
                tsbEditTrain.Enabled = true;
                tsbDeleteTrain.Enabled = true;
                tsbSave.Enabled = true;
                tsbStanica.Enabled = true;
                tscbStanica.Enabled = true;
                tscbObdobie.Enabled = true;
                tsbGlobalSettings.Enabled = true;
                tsmiNew.Enabled = true;
                tsmiImport.Enabled = true;
                tsbImport.Enabled = true;
                tsmiUpravit.Enabled = true;
                tsmimAddTrain.Enabled = true;
                tsmimEditTrain.Enabled = true;
                tsmiDeleteTrain.Enabled = true;
                tsmiDuplikovat.Enabled = true;
                tsmiVlastnostiStanice.Enabled = true;
                tsmiGlobalSettings.Enabled = true;
                tsmiSave.Enabled = true;
                tsbSave.Enabled = true;
                tsmiAnalyze.Enabled = true;
                tsbAnalyze.Enabled = true;
                tssbStartINISS.Enabled = true;
                tsmimStartINISS.Enabled = true;
                ChangeEnableMenuItemsGSettings(true);
                ChangeEnableMenuItemsLSettings(true);
            }

            if (!Stanice.Contains(gvd.ThisStation.Name)) Stanice.Add(gvd.ThisStation.Name);

            if ((string)tscbStanica.ComboBox.SelectedItem == gvd.ThisStation.Name)
                ObdobiaList.Add(new GVDDirectory(dir, gvd));

            var dirs = TxtParser.ReadDirList();
            dirs.Add(dir);
            TxtParser.WriteDirList(dirs);
            Directory.CreateDirectory(dir.FullPath);
            TxtParser.WriteInfoGVD(dir.FullPath, gvd);

            var dgyv = new GVDDirectory(dir, gvd);
            _gvdDirs.Add(dgyv);
            _newDir = dgyv;

            tscbStanica.ComboBox.SelectedItem = null;
            tscbStanica.ComboBox.SelectedItem = gvd.ThisStation.Name;
        }
    }

    private void ShowImportData()
    {
        var fid = new FImportData(((GVDDirectory)tscbObdobie.ComboBox.SelectedItem).GVD);
        var result = fid.ShowDialog();
        if (result == DialogResult.OK) GlobData.Trains.ResetBindings();
    }

    private void ShowImportELIS()
    {
        var fimport = new FELISOptions();
        var result = fimport.ShowDialog();
        if (result == DialogResult.OK)
        {
            var data = fimport.ResultOptions;
            data.GVDInfo = ((GVDDirectory)tscbObdobie.ComboBox.SelectedItem).GVD;
            data.Track = GlobData.Tracks.FirstOrDefault();
            data.DefTrains = GlobData.Trains.ToList();

            _error = false;
            _waitForm = new FWait("Prebieha importovanie údajov...");
            _waitForm.Show(this);
            if (!bWorkerELIS.IsBusy) bWorkerELIS.RunWorkerAsync(data);
        }
    }

    private void ShowImportGVD()
    {
        var dialog = new ExFolderBrowserDialog { Description = "Vyberte priečinok obsahujúci grafikon" };
        if (dialog.ShowDialog(this) == DialogResult.Cancel) 
            return;

        var selectedPath = dialog.SelectedPath;

        var dirname = Utils.GetDirectoryName(selectedPath);

        var newDirPath = Utils.CombinePath(GlobData.DataDir, dirname);

        if (string.IsNullOrEmpty(dirname))
        {
            Utils.ShowError(Resources.FMain_Názov_priečinka_je_prázdny);
            DialogResult = DialogResult.None;
            return;
        }

        if (Directory.Exists(newDirPath) || GlobData.GVDDirs.Count(d => d.DirName == dirname) != 0)
        {
            Utils.ShowError(Resources.Priečinok_s_týmto_názvom_už_existuje__Zmeňte_jeho_názov);
            DialogResult = DialogResult.None;
            return;
        }

        if (selectedPath != newDirPath) Utils.CopyDirectory(selectedPath, newDirPath);

        try
        {
            var gvd = TxtParser.ReadInfoGVD(newDirPath);
            var dirList = new DirList { DirName = dirname, FullPath = selectedPath };
            var dgyv = new GVDDirectory(dirList, gvd);

            var dirs = TxtParser.ReadDirList();
            dirs.Add(dirList);
            TxtParser.WriteDirList(dirs);

            GlobData.GVDDirs.Add(dirList);

            if (!Stanice.Contains(gvd.ThisStation.Name)) Stanice.Add(gvd.ThisStation.Name);

            if ((string)tscbStanica.ComboBox.SelectedItem == gvd.ThisStation.Name) ObdobiaList.Add(dgyv);

            _gvdDirs.Add(dgyv);
            GlobData.GVDDirs.Add(dgyv.Dir);
        }
        catch (Exception e)
        {
            Utils.ShowError($@"{Resources.FMain_Priečinok_neobsahuje_všetky_potrebné_dáta} {e.Message}");
            Log.Exception(e);
        }
    }

    private void ShowAppSettings()
    {
        var form = new FAppSettings(GlobData.Config, GlobData.Styles);
        var result = form.ShowDialog();
        if (result == DialogResult.OK)
        {
            UpdateMainUI();
        }
    }

    private void UpdateMainUI()
    {
        var menu = GlobData.Config.DesktopMenuMode;
        mainMenu.Visible = menu is DesktopMenu.MsTs or DesktopMenu.MsOnly;
        toolMenu.Visible = menu is DesktopMenu.MsTs or DesktopMenu.TsOnly;
        dgvTrains.RowHeadersVisible = GlobData.Config.ShowRowsHeader;

        this.ApplyThemeAndFonts();
        SetColumns();
        SetColumnsAutoWidth();
        SetShortcuts();
        Refresh();
        AppInit.MsgBoxStyleInit(GlobData.UsingStyle, GlobData.Config);
        if (GlobData.UsingStyle.HighlightStatusBar)
        {
            statusStrip.BackColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            tsslSelTrainName.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslSelTrainVariants.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslTrainCount.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
            tsslTrainCountWithVariants.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Highlight.ForeColor;
        }
    }

    private void ShowInfoApp()
    {
        var form = new FAboutApp(Resources.AboutAppDescription, Resources.gvd);
        form.ShowDialog(this);
    }

    //TODO prerobiť
    private void ShowLocalSettings(int startIndex = -1)
    {
        var dir = (GVDDirectory)tscbObdobie.ComboBox.SelectedItem;
        var svform = new FLocalSettings(dir, startIndex);
        var result = svform.ShowDialog();
        if (result == DialogResult.OK)
        {
            if (dir.GVD != svform.ThisDir.GVD) 
                Stanice.ResetBindings();

            GlobData.TableFontDir = svform.FontDir;
            DataSaved = false;
            GlobData.Trains.ResetBindings();
        }
    }

    private void ShowGlobalSettings(int startIndex = -1)
    {
        var gf = new FGlobalSettings(_gvdDirs.ToList(), startIndex);
        var result = gf.ShowDialog();
        if (result == DialogResult.OK)
        {
            var dirlist = gf.Grafikony.Select(gvd => gvd.Dir).ToList();

            GlobData.GVDDirs = dirlist;
            TxtParser.WriteDirList(dirlist);
            TxtParser.WriteTrainTypes(GlobData.TrainsTypes);
            TxtParser.WriteZpozdeni(GlobData.Delays);
            TxtParser.WriteAudio(GlobData.Audios);
            TxtParser.WriteLanguages(GlobData.Languages.ToList());
            GlobData.LocalLanguages = GlobData.Languages.ToList(); //TODO prerobit
            
            GlobData.Trains.ResetBindings();

            _removingGVD = true;
            foreach (var gvd in gf.RemovedGVDs)
            {
                if (_gvdDirs.Count == 1)
                {
                    tscbObdobie.ComboBox.SelectedItem = null;

                    //znefunkcnit niektore tlacidla tlacidla v toolstripe kvoli nevybratemu ziadnemu grafikonu
                    tsbAddGVD.Enabled = true;
                    tssbStartINISS.Enabled = true;
                    tsmimStartINISS.Enabled = true;
                    tsbAddTrain.Enabled = false;
                    tsbCopyTrain.Enabled = false;
                    tsbEditTrain.Enabled = false;
                    tsbDeleteTrain.Enabled = false;
                    tsbSave.Enabled = false;
                    tsbStanica.Enabled = false;
                    tsbGlobalSettings.Enabled = false;
                    tsmiNew.Enabled = true;
                    tsmiImport.Enabled = false;
                    tsbImport.Enabled = false;
                    tsmiUpravit.Enabled = false;
                    tsmimAddTrain.Enabled = false;
                    tsmimEditTrain.Enabled = false;
                    tsmiDeleteTrain.Enabled = false;
                    tsmiDuplikovat.Enabled = false;
                    tsmiVlastnostiStanice.Enabled = false;
                    tsmiGlobalSettings.Enabled = false;
                    tsmiSave.Enabled = false;
                    tsbSave.Enabled = false;
                    tsmiAnalyze.Enabled = false;
                    tsbAnalyze.Enabled = false;
                    tscbStanica.Enabled = false;
                    tscbObdobie.Enabled = false;
                    ChangeEnableMenuItemsGSettings(false);
                    ChangeEnableMenuItemsLSettings(false);

                    _prechod = true;
                    GlobData.Trains.Clear();
                    _prechod = false;
                }

                _gvdDirs.Remove(gvd);
                ObdobiaList.Remove(gvd);

                GlobData.GVDDirs.Remove(gvd.Dir);

                TxtParser.WriteDirList(GlobData.GVDDirs);

                try
                {
                    FileSystem.DeleteDirectory(gvd.Dir.FullPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            _removingGVD = false;
        }
    }

    private void ShowEditTrain(Train train, int row, bool copy = false)
    {
        var eform = new FEditTrain(train, row, ((GVDDirectory)tscbObdobie.ComboBox.SelectedItem).GVD, copy);
        var result = eform.ShowDialog();
        if (result == DialogResult.OK)
        {
            if (train == null || row == GlobData.Trains.Count)
            {
                GlobData.Trains.Add(eform.ThisTrain);
                DataSaved = false;
            }
            else
            {
                GlobData.Trains.ResetBindings();
                DataSaved = false;
            }
        }
    }

    private void ShowOpenDir()
    {
        var dialog = new ExFolderBrowserDialog { Description = "Vyberte priečinok s INISS.exe" };
        if (dialog.ShowDialog(this) == DialogResult.Cancel) 
            return;

        var selectedPath = dialog.SelectedPath;

        _prechod = true;
        GlobData.Trains.Clear();
        _prechod = false;

        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
            try
            {
                GlobData.PrepareGlobalData(selectedPath);
            }
            catch (Exception e)
            {
                Log.Exception(e);

                switch (GlobData.Config.DebugModeGUI)
                {
                    case DebugMode.OnlyMessage:
                        FError.ShowError(e.Message);
                        break;
                    case DebugMode.DetailInfo:
                        FError.ShowError(e.ToString());
                        break;
                }

                return;
            }
        else
            GlobData.PrepareGlobalData(selectedPath);

        if (InitializeDataList()) InitializeGUI();
    }

    private void ShowStartupINISSSettings()
    {
        var form = new FAppSettings(GlobData.Config, GlobData.Styles);
        form.PreselectMenuItem("pStartupIniss");
        if (form.ShowDialog() == DialogResult.OK)
        {
            UpdateMainUI();
        }
    }

    private void DoDeleteTrains()
    {
        if (dgvTrains.SelectedRows.Count > 0)
        {
            foreach (DataGridViewRow row in dgvTrains.SelectedRows)
            {
                CheckAutoTrainVariants(row.Index);
                if (!GlobData.Config.AutoTableText)
                    DeleteTTexts(row.DataBoundItem as Train);

                GlobData.Trains.RemoveAt(row.Index);
            }

            GlobData.Trains.ResetBindings();

            DataSaved = false;
        }
    }

    private static bool InitializeDataList()
    {
        try
        {
            var stanice = new HashSet<string>();
            var obdobiaList = new List<GVDDirectory>();
            var dirsInData = TxtParser.ReadDirList();
            foreach (var dir in dirsInData)
            {
                GVDInfo gvd;
                if (GlobData.Config.DebugModeGUI == DebugMode.AppCrash)
                {
                    gvd = TxtParser.ReadInfoGVD(dir.FullPath);
                    stanice.Add(gvd.ThisStation.Name);
                    obdobiaList.Add(new GVDDirectory(dir, gvd));
                }
                else
                {
                    try
                    {
                        gvd = TxtParser.ReadInfoGVD(dir.FullPath);
                        stanice.Add(gvd.ThisStation.Name);
                        obdobiaList.Add(new GVDDirectory(dir, gvd));
                    }
                    catch (Exception e)
                    {
                        FError.ShowError(GlobData.Config.DebugModeGUI == DebugMode.DetailInfo ? e.ToString() : e.Message);
                        Log.Exception(e);
                    }
                }
            }

            Stanice.Clear();
            foreach (var st in stanice) Stanice.Add(st);

            ObdobiaList.Clear();
            foreach (var obd in obdobiaList) ObdobiaList.Add(obd);
        }
        catch (DirectoryNotFoundException)
        {
            Utils.ShowError(Resources.FMain_Priečinok_neobsahuje_všetky_potrebné_dáta);
            return false;
        }

        AppRegistry.SetUsageOfProject(GlobData.INISSDir);
        AppRegistry.SetLastProject(GlobData.INISSDir);
        return true;
    }

    private void InitializeGUI()
    {
        //vlozit stanice a obdobia do combo boxov v tool stripe
        _gvdDirs.Clear();
        _gvdDirs.AddRange(ObdobiaList);

        tscbStanica.ComboBox.SelectedItem = null;
        tscbObdobie.ComboBox.SelectedItem = null;

        //vybrat prvy item 
        tscbStanica.ComboBox.SelectedItem = Stanice.FirstOrDefault();
        tscbObdobie.ComboBox.SelectedItem = ObdobiaList.FirstOrDefault();

        //premenovat form podla aktualne otvoreneho priecinka
        Text = Application.ProductName + @" - " + GlobData.INISSDir;


        if (Stanice.Count == 0 || ObdobiaList.Count == 0)
        {
            //znefunkcnit niektore tlacidla tlacidla v toolstripe kvoli nevybratemu ziadnemu grafikonu

            tsbAddGVD.Enabled = true;
            tssbStartINISS.Enabled = true;
            tsmimStartINISS.Enabled = true;
            tsbAddTrain.Enabled = false;
            tsbCopyTrain.Enabled = false;
            tsbEditTrain.Enabled = false;
            tsbDeleteTrain.Enabled = false;
            tsbSave.Enabled = false;
            tsbStanica.Enabled = false;
            tsbGlobalSettings.Enabled = false;
            tsmiNew.Enabled = true;
            tsmiImport.Enabled = false;
            tsbImport.Enabled = false;
            tsmiUpravit.Enabled = false;
            tsmimAddTrain.Enabled = false;
            tsmimEditTrain.Enabled = false;
            tsmiDeleteTrain.Enabled = false;
            tsmiDuplikovat.Enabled = false;
            tsmiVlastnostiStanice.Enabled = false;
            tsmiGlobalSettings.Enabled = false;
            tsmiSave.Enabled = false;
            tsbSave.Enabled = false;
            tsmiAnalyze.Enabled = false;
            tsbAnalyze.Enabled = false;
            tscbStanica.Enabled = false;
            tscbObdobie.Enabled = false;
            ChangeEnableMenuItemsGSettings(false);
            ChangeEnableMenuItemsLSettings(false);
        }
        else
        {
            //sfunkcnit tlacidla v toolstripe

            tsbAddGVD.Enabled = true;
            tsbAddTrain.Enabled = true;
            tsbCopyTrain.Enabled = true;
            tsbEditTrain.Enabled = true;
            tsbDeleteTrain.Enabled = true;
            tsmiSave.Enabled = true;
            tsbSave.Enabled = true;
            tsmiAnalyze.Enabled = true;
            tsbAnalyze.Enabled = true;
            tsbStanica.Enabled = true;
            tscbStanica.Enabled = true;
            tscbObdobie.Enabled = true;
            tsbGlobalSettings.Enabled = true;
            tsmiNew.Enabled = true;
            tsmiImport.Enabled = true;
            tsbImport.Enabled = true;
            tsmiUpravit.Enabled = true;
            tsmiUpravit.Enabled = true;
            tsmimAddTrain.Enabled = true;
            tsmimEditTrain.Enabled = true;
            tsmiDeleteTrain.Enabled = true;
            tsmiDuplikovat.Enabled = true;
            tsmiVlastnostiStanice.Enabled = true;
            tsmiGlobalSettings.Enabled = true;
            tssbStartINISS.Enabled = true;
            tsmimStartINISS.Enabled = true;
            ChangeEnableMenuItemsGSettings(true);
            ChangeEnableMenuItemsLSettings(true);
        }

        DataSaved = true;

        foreach (var file in GlobData.INISSExeFiles)
        {
            ToolStripItem item1 = new ToolStripMenuItem(file);
            item1.Click += InissStartItemOnClick;
            tssbStartINISS.DropDownItems.Add(item1);

            ToolStripItem item2 = new ToolStripMenuItem(file);
            item2.Click += InissStartItemOnClick;
            tsmiRun.DropDownItems.Add(item2);
        }

        if (GlobData.INISSExeFiles.Count > 0) tsmimStartINISS.Enabled = true;

        foreach (ToolStripItem item in tssbStartINISS.DropDownItems) item.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;

        foreach (ToolStripItem item in tsmiRun.DropDownItems) item.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;
    }

    private void InissStartItemOnClick(object sender, EventArgs e)
    {
        if (sender is ToolStripItem tsmi)
        {
            if (_actualINISSProcess is not null && _actualINISSProcess.HasExited)
            {
                _actualINISSProcess.Close();
                _actualINISSProcess = null;
            }

            if (_actualINISSProcess == null)
            {
                var path = Utils.CombinePath(GlobData.INISSDir, tsmi.Text);
                ExecuteINISS(path);
                _lastINISSStart = path;
            }
            else
            {
                var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
                if (result == DialogResult.Yes) _actualINISSProcess.Kill();
            }
        }
    }

    private void RecentDirsClick(object sender, EventArgs e)
    {
        var menuItem = (ToolStripMenuItem)sender;
        OpenRecentProject(menuItem.Text);
    }

    private void OpenRecentProject(string fullPath)
    {
        _prechod = true;
        GlobData.Trains.Clear();
        _prechod = false;

        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
            try
            {
                GlobData.PrepareGlobalData(fullPath);
            }
            catch (Exception exception)
            {
                Log.Exception(exception);

                switch (GlobData.Config.DebugModeGUI)
                {
                    case DebugMode.OnlyMessage:
                        FError.ShowError(exception.Message);
                        break;
                    case DebugMode.DetailInfo:
                        FError.ShowError(exception.ToString());
                        break;
                }

                return;
            }
        else
            GlobData.PrepareGlobalData(fullPath);

        if (InitializeDataList()) 
            InitializeGUI();
    }

    private void tsbInformation_Click(object sender, EventArgs e)
        => ShowInfoApp();

    private void tscbStanica_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tscbStanica.ComboBox.SelectedItem != null)
        {
            var dirs = new List<GVDDirectory>();
            var stanica = tscbStanica.ComboBox.SelectedItem.ToString();
            foreach (var gvdDir in _gvdDirs)
                if (stanica == gvdDir.GVD.ThisStation.Name)
                    dirs.Add(gvdDir);

            ObdobiaList.Clear();

            foreach (var dir in dirs) ObdobiaList.Add(dir);

            if (dirs.Count != 0 && _newDir == null)
            {
                tscbObdobie.ComboBox.SelectedItem = null;
                tscbObdobie.ComboBox.SelectedIndex = 0;
            }
            else if (_newDir != null)
            {
                tscbObdobie.ComboBox.SelectedItem = null;
                tscbObdobie.ComboBox.SelectedItem = _newDir;
            }

            _previousSelectedGVD ??= (GVDDirectory)tscbObdobie.ComboBox.SelectedItem;
        }
    }

    private void tscbObdobie_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_removingGVD)
            return;

        var dir = (GVDDirectory)tscbObdobie.ComboBox.SelectedItem;

        if (dir == null) return;

        if (!string.IsNullOrEmpty(GlobData.INISSDir) && !DataSaved)
        {
            var result = Utils.ShowQuestion(Resources.FMain_Save_Changes, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    DoSave();
                    break;
                case DialogResult.No:
                    DataSaved = true;
                    break;
                default:
                    return;
            }
        }

        _previousSelectedGVD = dir;

        //ak sa jedna o novy grafikon
        if (Equals(dir, _newDir))
        {
            GlobData.Tracks = new ExBindingList<Track> { Track.None };
            GlobData.Platforms = new ExBindingList<Platform> { Platform.None };

            GlobData.Operators = new ExBindingList<Operator> { Operator.None };

            GlobData.TabTabs = new ExBindingList<TableTabTab>();
            GlobData.TableCatalogs = new ExBindingList<TableCatalog>();
            GlobData.TablePhysicals = new ExBindingList<TablePhysical>();
            GlobData.TableLogicals = new ExBindingList<TableLogical>();
            GlobData.TableTexts = new ExBindingList<TableText>();
            GlobData.TableFonts = new ExBindingList<TableFont>();

            GlobData.CustomStations = new ExBindingList<Station>();

            GlobData.Radenia = new List<Radenie>();

            GlobData.ReportTypes = ReportType.GetDefaultValuesSK();
            GlobData.ReportVariants = ReportVariant.GetDefaultValues();

            _prechod = true;
            GlobData.Trains.Clear();
            _prechod = false;

            TxtParser.WriteTrains(dir.Dir.FullPath, GlobData.Trains.ToList(), dir.GVD, GlobData.ReportVariants);

            TxtParser.WriteTables(dir.Dir.FullPath, GlobData.TabTabs, GlobData.TableCatalogs, GlobData.TablePhysicals, GlobData.TableLogicals);
            TxtParser.WriteTTexts(dir.Dir.FullPath, GlobData.TableTexts);

            TxtParser.WriteTracks(dir.Dir.FullPath, GlobData.Tracks);

            TxtParser.WriteInfoGVD(dir.Dir.FullPath, dir.GVD);

            TxtParser.WriteOperators(dir.Dir.FullPath, GlobData.Operators);

            TxtParser.WriteModeTabs(dir.Dir.FullPath, GlobData.TableFonts, GlobData.TableFontDir);

            TxtParser.WriteStateDgm(dir.Dir.FullPath);

            TxtParser.WriteLocalCategori(dir.Dir.FullPath, GlobData.ReportVariants, GlobData.ReportTypes, GlobData.Languages);

            TxtParser.WriteRazeniDefault(dir.Dir.FullPath);
            TxtParser.WriteRazeni1Default(dir.Dir.FullPath);

            _newDir = null;
            return;
        }

        _prechod = true;
        dgvTrains.DataSource = null;
        GlobData.Trains.Clear();
        _prechod = false;

        _error = false;
        _waitForm = new FWait();
        _waitForm.Show(this);
        if (!backgroundWorker1.IsBusy)
            backgroundWorker1.RunWorkerAsync(new PathAndGVD { Path = dir.Dir.FullPath, Gvd = dir.GVD });
    }

    private static void InitDruhyReportov()
    {
        GlobData.ReportTypesV.Clear();
        GlobData.ReportTypesP.Clear();
        GlobData.ReportTypesK.Clear();

        foreach (var reportType in GlobData.ReportTypes)
        {
            if (reportType.BaseTrain) GlobData.ReportTypesV.Add(reportType);
            if (reportType.PassThrough) GlobData.ReportTypesP.Add(reportType);
            if (reportType.TerminateTrain) GlobData.ReportTypesK.Add(reportType);
        }
    }

    private void dgvTrains_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex != -1 && dgvTrains.Columns[e.ColumnIndex].Name == "Ostatne")
            if (dgvTrains.CurrentRow != null && e.RowIndex != -1 && GlobData.Trains[e.RowIndex] != new Train())
                ShowEditTrain(GlobData.Trains[e.RowIndex], e.RowIndex);

        if (e.RowIndex != -1)
        {
            tsslSelTrainName.Visible = true;
            tsslSelTrainVariants.Visible = true;
            tsslSelTrainName.Text = $@"{GlobData.Trains[e.RowIndex].Type} {GlobData.Trains[e.RowIndex].Number}";
            tsslSelTrainVariants.Text = CountSelTrainVariants(GlobData.Trains[e.RowIndex]).ToString();
        }
    }

    private static int CountSelTrainVariants(Train train)
    {
        var count = 0;
        foreach (var t in GlobData.Trains)
            if (t.NumberVariant.Number == train.Number)
                count++;
        return count;
    }

    private void dgvTrains_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        if (dgvTrains.Columns[e.ColumnIndex].Name == @"Kolaj" && e.RowIndex != -1 && e.RowIndex < GlobData.Trains.Count)
            GlobData.Trains[e.RowIndex].Track = GlobData.Tracks[0];
        else if (dgvTrains.Columns[e.ColumnIndex].Name == @"Dopravca" && e.RowIndex != -1 &&
                 e.RowIndex < GlobData.Trains.Count)
            GlobData.Trains[e.RowIndex].Operator = GlobData.Operators[0];
        else
            Utils.ShowError(Resources.FMain_dgvTrains_DataError_Tabuľka_obsahuje_nesprávny_údaj + e.Exception.Message);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!string.IsNullOrEmpty(GlobData.INISSDir) && !DataSaved)
        {
            var result = Utils.ShowQuestion(Resources.FMain_Save_Changes, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    DoSave();
                    e.Cancel = false;
                    break;
                case DialogResult.No:
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }
    }

    private void tsbSave_Click(object sender, EventArgs e) => DoSave();

    private void tsbOpen_Click(object sender, EventArgs e) => ShowOpenDir();

    private void tsmiImportData_Click(object sender, EventArgs e) => ShowImportData();

    private void tsmiImportGVD_Click(object sender, EventArgs e) => ShowImportGVD();

    private void tsmiImportELIS_Click(object sender, EventArgs e) => ShowImportELIS();

    private void tsmimImportELIS_Click(object sender, EventArgs e) => ShowImportELIS();

    private void tsmimImportData_Click(object sender, EventArgs e) => ShowImportData();

    private void tsmimImportGVD_Click(object sender, EventArgs e) => ShowImportGVD();

    private void tsbAnalyze_Click(object sender, EventArgs e) => ShowAnalyzeGVD();

    private void tsbAddTrain_Click(object sender, EventArgs e) => ShowEditTrain(null, GlobData.Trains.Count);

    private void tsbCopyTrain_Click(object sender, EventArgs e)
    {
        if (dgvTrains.SelectedRows.Count > 0)
        {
            var index = dgvTrains.SelectedRows[0].Index;
            ShowEditTrain(GlobData.Trains[index], GlobData.Trains.Count, true);
        }
    }

    private void tsbEditTrain_Click(object sender, EventArgs e)
    {
        if (dgvTrains.SelectedRows.Count > 0)
        {
            var index = dgvTrains.SelectedRows[0].Index;
            ShowEditTrain(GlobData.Trains[index], index);
        }
    }

    private void tsbDeleteTrain_Click(object sender, EventArgs e) => DoDeleteTrains();

    private void tsbLocalSettings_Click(object sender, EventArgs e) => ShowLocalSettings();

    private void tsbAddGVD_Click(object sender, EventArgs e) => ShowNewGVD();

    private void dgvTrains_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Delete) DoDeleteTrains();
    }

    private void dgvTrains_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (dgvTrains.Columns[e.ColumnIndex].Name == @"cisloDataGridViewTextBoxColumn" &&
            (string)e.FormattedValue == "") e.Cancel = true;

        if (dgvTrains.Columns[e.ColumnIndex].Name == @"odchodDataGridViewTextBoxColumn" &&
            (string)e.FormattedValue == "" &&
            dgvTrains.Rows[e.RowIndex].Cells[@"prichodDataGridViewTextBoxColumn"].Value == null)
            e.Cancel = true;
        else if (dgvTrains.Columns[e.ColumnIndex].Name == @"prichodDataGridViewTextBoxColumn" &&
                 (string)e.FormattedValue == "" &&
                 dgvTrains.Rows[e.RowIndex].Cells[@"odchodDataGridViewTextBoxColumn"].Value == null) e.Cancel = true;

        if (dgvTrains.Columns[e.ColumnIndex].Name == @"DatumoveObmedzenieText")
        {
            var value = e.FormattedValue as string;
            var thistrain = GlobData.Trains[e.RowIndex];
            var dateRemThis = new DateLimit(thistrain.ZaciatokPlatnosti, thistrain.KoniecPlatnosti,
                insertMarks: false);
            try
            {
                dateRemThis.TextToBitArray(value);
            }
            catch (Exception exception)
            {
                Utils.ShowError(exception.Message);
                e.Cancel = true;
                return;
            }

            var i = 0;
            foreach (var train in GlobData.Trains)
            {
                if (Train.IsSameVariant(train, thistrain) && i != e.RowIndex && 
                    train.ZaciatokPlatnosti == thistrain.ZaciatokPlatnosti && 
                    train.KoniecPlatnosti == thistrain.KoniecPlatnosti && 
                    dateRemThis.Overlap(thistrain.DateLimitText, train.DateLimitText))
                {
                    var obmand = dateRemThis.TextAnd(train.DateLimitText, thistrain.DateLimitText);
                    var result = MessageBox.Show(string.Format(Resources.FEditTrain_DateRem_zasahuje_do_ineho_vlaku, train.Type,
                        train.Number, train.Name, obmand), Resources.RWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var result2 = FDateLimitEdit.SetDateLimit(this, thistrain.ZaciatokPlatnosti, thistrain.KoniecPlatnosti, train,
                            true, train.DateLimitText);
                        if (result2 == DialogResult.OK) 
                            train.DateLimitText = FDateLimitEdit.Result;
                    }
                }

                i++;
            }
        }
    }

    private void tsbGlobalSettings_Click(object sender, EventArgs e) => ShowGlobalSettings();

    private void tsmiNew_Click(object sender, EventArgs e) => ShowNewGVD();

    private void tsmiOpen_Click(object sender, EventArgs e) => ShowOpenDir();

    private void tsmiSave_Click(object sender, EventArgs e) => DoSave();

    private void tsmiAnalyze_Click(object sender, EventArgs e) => ShowAnalyzeGVD();

    private void tsmimAddTrain_Click(object sender, EventArgs e) => ShowEditTrain(null, GlobData.Trains.Count);

    private void tsmimEditTrain_Click(object sender, EventArgs e)
    {
        if (dgvTrains.SelectedRows.Count > 0)
        {
            var index = dgvTrains.SelectedRows[0].Index;
            ShowEditTrain(GlobData.Trains[index], index);
        }
    }

    private void tsmiDeleteTrain_Click(object sender, EventArgs e) => DoDeleteTrains();

    private void tsmiDuplikovat_Click(object sender, EventArgs e)
    {
        if (dgvTrains.SelectedRows.Count > 0)
        {
            var index = dgvTrains.SelectedRows[0].Index;
            ShowEditTrain(GlobData.Trains[index], GlobData.Trains.Count, true);
        }
    }

    private void tsmiLocalSettings_Click(object sender, EventArgs e) => ShowLocalSettings();

    private void tsmiGlobalSettings_Click(object sender, EventArgs e) => ShowGlobalSettings();

    private void tsmiAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();

    private void tsmiInformation_Click(object sender, EventArgs e) => ShowInfoApp();

    private void tsmiChangelog_Click(object sender, EventArgs e) => Process.Start(LinkConsts.LINK_NEWS);

    private void dgvTrains_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (GlobData.Trains.Count != 0)
        {
            for (var i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                GlobData.Trains[i].ID = i + 1;

                if (GlobData.Trains[i].Routing == null)
                {
                    var vlak = GlobData.Trains[i];
                    throw new ArgumentNullException(
                        $"Vlak {vlak.Type} {vlak.NumberVariant} {vlak.Name} nemá definované smerovanie.");
                }

                if (GlobData.Trains[i].Routing == Routing.Prechadzajuci)
                {
                    dgvTrains.Rows[i].Cells[@"odchodDataGridViewTextBoxColumn"].ReadOnly = false;
                    dgvTrains.Rows[i].Cells[@"prichodDataGridViewTextBoxColumn"].ReadOnly = false;
                }
                else if (GlobData.Trains[i].Routing == Routing.Vychadzajuci)
                {
                    dgvTrains.Rows[i].Cells[@"odchodDataGridViewTextBoxColumn"].ReadOnly = false;
                    dgvTrains.Rows[i].Cells[@"prichodDataGridViewTextBoxColumn"].ReadOnly = true;
                }
                else
                {
                    dgvTrains.Rows[i].Cells[@"odchodDataGridViewTextBoxColumn"].ReadOnly = true;
                    dgvTrains.Rows[i].Cells[@"prichodDataGridViewTextBoxColumn"].ReadOnly = false;
                }
            }

            tsslTrainCountWithVariants.Text = dgvTrains.Rows.Count.ToString();
            tsslTrainCount.Text = $@"({CountTrainVariants()})";
        }
    }

    private void dgvTrains_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        for (var i = 1; i <= GlobData.Trains.Count; i++) GlobData.Trains[i - 1].ID = i;

        if (!_prechod) DataSaved = false;

        tsslTrainCountWithVariants.Text = dgvTrains.Rows.Count.ToString();
        tsslTrainCount.Text = $@"({CountTrainVariants()})";

        if (GlobData.Trains.Count == 0)
        {
            tsslSelTrainName.Visible = false;
            tsslSelTrainVariants.Visible = false;
        }
    }

    private static int CountTrainVariants()
    {
        var trains = new HashSet<(string num, TrainType type, string name)>();
        foreach (var t in GlobData.Trains) 
            trains.Add((t.Number, t.Type, t.Name));

        return trains.Count;
    }

    private void dgvTrains_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex != -1) DataSaved = false;
    }

    private void tsbAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();

    private void tsmiStartupSettings_Click(object sender, EventArgs e) => ShowStartupINISSSettings();

    private void tsmimStartupSettings_Click(object sender, EventArgs e) => ShowStartupINISSSettings();

    private void dgvTrains_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        void SetFromScheme(ColorSetting sett)
        {
            e.CellStyle.ForeColor = sett.ForeColor != SystemColors.ControlText ? sett.ForeColor : Color.White;
            if (sett.BackColor != Color.Transparent) 
                e.CellStyle.BackColor = sett.BackColor;
            e.CellStyle.Font = sett.Bold
                ? new Font(GlobData.UsingStyle.TrainTypeColumnScheme.Font, FontStyle.Bold)
                : GlobData.UsingStyle.TrainTypeColumnScheme.Font;
        }

        if (dgvTrains.Columns["typDataGridViewTextBoxColumn"] != null && e.ColumnIndex == dgvTrains.Columns["typDataGridViewTextBoxColumn"].Index)
            if (e.RowIndex < GlobData.Trains.Count)
            {
                var type = GlobData.Trains[e.RowIndex].Type;
                if (type.IsCustom)
                {
                    var stype = type.CategoryTrain.ToUpper();
                    if (stype.StartsWith("X"))
                        SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.X);
                    else if (stype.StartsWith("R"))
                        SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.R);
                    else if (stype.StartsWith("SL"))
                        SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.Sl);
                    else if (stype.StartsWith("OS")) 
                        SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.Os);
                }
                else
                {
                    switch (type.CategoryTrain)
                    {
                        case "R":
                        case "REX":
                        case "RR":
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.R);
                            break;
                        case "IC":
                        case "EC":
                        case "EN":
                        case "SC":
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.X);
                            break;
                        default:
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.Os);
                            break;
                    }
                }
            }
    }

    private static void GenerateTableTextWhileSaving(GVDDirectory dir)
    {
        foreach (var ttext in GlobData.TableTexts)
        foreach (var tableTextRealization in ttext.Realizations)
        {
            var item = tableTextRealization.Item;
            ttext.Trains.Clear();

            foreach (var vlak in GlobData.Trains)
            {
                var tableTrain = new TableTrain { FontID = -1, Train = vlak };

                if (item.FillSection == TableFillSection.CielovaStanica ||
                    item.FillSection == TableFillSection.CielovaStanicaNastupiste ||
                    item.FillSection == TableFillSection.CielovaStanicaPodchod)
                {
                    if (vlak.Routing == Routing.Prechadzajuci || vlak.Routing == Routing.Vychadzajuci)
                        tableTrain.Text = vlak.StaniceDoSmeru.Last().Name;
                    else
                        tableTrain.Text = dir.GVD.ThisStation.Name;
                }
                else if (item.FillSection == TableFillSection.VychadzajucaStanica)
                {
                    if (vlak.Routing == Routing.Prechadzajuci || vlak.Routing == Routing.Konciaci)
                        tableTrain.Text = vlak.StaniceZoSmeru.First().Name;
                    else
                        tableTrain.Text = dir.GVD.ThisStation.Name;
                }
                else if (item.FillSection == TableFillSection.StaniceDoSmeru || item.FillSection == TableFillSection.StaniceDoSmeruNastupiste)
                {
                    var sb = new StringBuilder();

                    if (vlak.Routing == Routing.Prechadzajuci || vlak.Routing == Routing.Vychadzajuci)
                    {
                        var staniceDo = new List<Station>();

                        foreach (var t in vlak.StaniceDoSmeru)
                            if (t.IsInShortReport)
                                staniceDo.Add(t);

                        staniceDo.RemoveAt(staniceDo.Count - 1);

                        for (var i = 0; i < staniceDo.Count; i++)
                            if (i < staniceDo.Count - 1)
                            {
                                sb.AppendLine(staniceDo[i].Name);
                                sb.Append('#');
                            }
                            else if (i == staniceDo.Count - 1)
                            {
                                sb.AppendLine(staniceDo[i].Name);
                            }

                        tableTrain.Text = sb.ToString();
                    }
                    else
                    {
                        tableTrain.Text = "";
                    }
                }
                else if (item.FillSection == TableFillSection.StaniceZoSmeru)
                {
                    var sb = new StringBuilder();

                    if (vlak.Routing == Routing.Prechadzajuci || vlak.Routing == Routing.Konciaci)
                    {
                        var staniceZo = new List<Station>();

                        foreach (var t in vlak.StaniceZoSmeru)
                            if (t.IsInShortReport)
                                staniceZo.Add(t);

                        for (var i = 1; i < staniceZo.Count; i++)
                            if (i < staniceZo.Count - 1)
                            {
                                sb.AppendLine(staniceZo[i].Name);
                                sb.Append('#');
                            }
                            else if (i == staniceZo.Count - 1)
                            {
                                sb.AppendLine(staniceZo[i].Name);
                            }

                        tableTrain.Text = sb.ToString();
                    }
                    else
                    {
                        tableTrain.Text = "";
                    }
                }
                else
                {
                    return;
                }

                tableTrain.Text = Regex.Replace(tableTrain.Text, @"\t|\n|\r", "");

                ttext.Trains.Add(tableTrain);
            }
        }
    }

    private void SetColumns()
    {
        static void SetCol(DataGridViewColumn column, DesktopColumn format)
        {
            column.Visible = format.Visible;
            column.MinimumWidth = format.MinWidth;
            column.DisplayIndex = format.Order;
        }

        SetCol(cisloDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Number);
        SetCol(typDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Type);
        SetCol(nameDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Name);
        SetCol(LinkaPrichod, GlobData.Config.DesktopCols.LinkaPrichod);
        SetCol(LinkaOdchod, GlobData.Config.DesktopCols.LinkaOdchod);
        SetCol(smerovanieDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Routing);
        SetCol(prichodDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Prichod);
        SetCol(odchodDataGridViewTextBoxColumn, GlobData.Config.DesktopCols.Odchod);
        SetCol(dgvcVychodziaStanica, GlobData.Config.DesktopCols.VychodziaStanica);
        SetCol(dgvcKonecnaStanica, GlobData.Config.DesktopCols.KonecnaStanica);
        SetCol(DatumoveObmedzenieText, GlobData.Config.DesktopCols.DateLimit);
        SetCol(Kolaj, GlobData.Config.DesktopCols.Track);
        SetCol(Dopravca, GlobData.Config.DesktopCols.Operator);
        SetCol(Ostatne, GlobData.Config.DesktopCols.OtherBtn);

        dgvTrains.Columns[dgvTrains.Columns.Count - 1].AutoSizeMode = GlobData.Config.FitLastColumn
            ? DataGridViewAutoSizeColumnMode.Fill
            : DataGridViewAutoSizeColumnMode.None;
    }

    private void SetColumnsAutoWidth()
    {
        for (var i = 0; i < dgvTrains.Columns.Count - 1; i++)
        {
            dgvTrains.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            var widthCol = dgvTrains.Columns[i].Width;
            dgvTrains.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTrains.Columns[i].Width = widthCol;
        }
    }

    private void SetShortcuts()
    {
        var sc = GlobData.Config.Shortcuts;
        tsmiNew.ShortcutKeys = (Keys)sc.New.Shortcut.Value;
        tsmiOpen.ShortcutKeys = (Keys)sc.Open.Shortcut.Value;
        tsmiImportData.ShortcutKeys = (Keys)sc.ImportData.Shortcut.Value;
        tsmiImportGVD.ShortcutKeys = (Keys)sc.ImportGvd.Shortcut.Value;
        tsmiSave.ShortcutKeys = (Keys)sc.Save.Shortcut.Value;
        tsmiAnalyze.ShortcutKeys = (Keys)sc.Analyze.Shortcut.Value;

        tsmimAddTrain.ShortcutKeys = (Keys)sc.AddTrain.Shortcut.Value;
        tsmimEditTrain.ShortcutKeys = (Keys)sc.EditTrain.Shortcut.Value;
        tsmiDeleteTrain.ShortcutKeys = (Keys)sc.DeleteTrains.Shortcut.Value;
        tsmiDuplikovat.ShortcutKeys = (Keys)sc.DuplicateTrain.Shortcut.Value;

        tsmiVlastnostiStanice.ShortcutKeys = (Keys)sc.LocalSettings.Shortcut.Value;
        tsmiGlobalSettings.ShortcutKeys = (Keys)sc.GlobalSettings.Shortcut.Value;
        tsmiAppSettings.ShortcutKeys = (Keys)sc.AppSettings.Shortcut.Value;

        tsmiInformation.ShortcutKeys = (Keys)sc.InfoApp.Shortcut.Value;
        tsmiChangelog.ShortcutKeys = (Keys) sc.UpdateNotes.Shortcut.Value;

        tsmimStartINISS.ShortcutKeys = (Keys)sc.RunINISS.Shortcut.Value;
        tsmimShutdownINISS.ShortcutKeys = (Keys)sc.ShutdownINISS.Shortcut.Value;
        tsmimKillINISS.ShortcutKeys = (Keys)sc.KillINISS.Shortcut.Value;
        tsmimRestartINISS.ShortcutKeys = (Keys)sc.RestartINISS.Shortcut.Value;

        tsmiGrafikon.ShortcutKeys = (Keys)sc.LSGvd.Shortcut.Value;
        tsmiStanice.ShortcutKeys = (Keys)sc.LSStations.Shortcut.Value;
        tsmiDopravcovia.ShortcutKeys = (Keys)sc.LSOperators.Shortcut.Value;
        tsmiPlatforms.ShortcutKeys = (Keys)sc.LSPlatforms.Shortcut.Value;
        tsmiKolaje.ShortcutKeys = (Keys)sc.LSTracks.Shortcut.Value;
        tsmiTPhysical.ShortcutKeys = (Keys)sc.LSPhysicalTables.Shortcut.Value;
        tsmiTLogical.ShortcutKeys = (Keys)sc.LSLogicalsTables.Shortcut.Value;
        tsmiTCatalog.ShortcutKeys = (Keys)sc.LSCatalogTables.Shortcut.Value;
        tsmiTabTab.ShortcutKeys = (Keys)sc.LSTabTab.Shortcut.Value;
        tsmiTTexts.ShortcutKeys = (Keys)sc.LSTTexts.Shortcut.Value;
        tsmiTFonts.ShortcutKeys = (Keys)sc.LSTFonts.Shortcut.Value;
        tsmiTabTabEditor.ShortcutKeys = (Keys)sc.LSTabTabEditor.Shortcut.Value;

        tsmiGrafikony.ShortcutKeys = (Keys)sc.GSGvds.Shortcut.Value;
        tsmiLanguages.ShortcutKeys = (Keys)sc.GSLanguages.Shortcut.Value;
        tsmiMeskania.ShortcutKeys = (Keys)sc.GSDelays.Shortcut.Value;
        tsmiTypyVlakov.ShortcutKeys = (Keys)sc.GSTrainTypes.Shortcut.Value;
        tsmiAudio.ShortcutKeys = (Keys)sc.GSAudio.Shortcut.Value;

        tsmiDatObm.ShortcutKeys = (Keys)sc.DateLimit.Shortcut.Value;
    }

    private static void CheckAutoTrainVariants(int index)
    {
        if (!GlobData.Config.AutoVariant) return;

        var id = 0;
        var seltrains = new List<Train>();
        var dtrain = GlobData.Trains[index];

        foreach (var train in GlobData.Trains)
        {
            if (train.Number == dtrain.Number && string.Equals(train.Name, dtrain.Name) &&
                Equals(train.Type, dtrain.Type) && index != id) seltrains.Add(train);

            id++;
        }

        if (seltrains.Count == 1)
            seltrains[0].Variant = -1;
        else if (seltrains.Count != 0)
            for (var i = 0; i < seltrains.Count; i++)
                seltrains[i].Variant = i + 1;
    }

    private void bWorkerELIS_DoWork(object sender, DoWorkEventArgs e)
    {
        var data = (SendData)e.Argument;

        if (GlobData.Config.DebugModeGUI != DebugMode.AppCrash)
            try
            {
                e.Result = CallELISParser(data);
            }
            catch (Exception exception)
            {
                Log.Exception(exception);

                if (GlobData.Config.DebugModeGUI == DebugMode.OnlyMessage)
                    Utils.ShowError(exception.Message);
                if (GlobData.Config.DebugModeGUI == DebugMode.DetailInfo)
                    Utils.ShowError(exception.ToString());

                _error = true;
                return;
            }
        else
            e.Result = CallELISParser(data);

        _error = false;
    }

    private static List<Train> CallELISParser(SendData data)
    {
        var parser = new ELISParser(data.Path, data.DefTrains, GlobData.TrainsTypes.ToList(), GlobData.Operators.ToList(), data.GVDInfo, data.Track)
        {
            OmitPassingTrains = data.OmitPassingTrains,
            DefinedOperators = data.DefinedOperators,
            DefinedDateLimits = data.DefinedDateLimits,
            ReorderTrains = data.ReorderTrains,
            NewFormat = data.NewFormat
        };
        return parser.ReadELISFile();
    }

    private void bWorkerELIS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        _waitForm.Close();
        Cursor.Current = Cursors.AppStarting;

        if (!_error)
        {
            var imported = (List<Train>)e.Result;
            foreach (var train in imported) GlobData.Trains.Add(train);

            DataSaved = false;
        }
    }

    private static void DeleteTTexts(Train vlak)
    {
        foreach (var tt in GlobData.TableTexts)
            for (var i = tt.Trains.Count - 1; i >= 0; i--)
                if (tt.Trains[i].Train == vlak)
                    tt.Trains.RemoveAt(i);
    }

    private void ExecuteINISS(string fileName)
    {
        _actualINISSProcess = new Process { StartInfo = { FileName = fileName, UseShellExecute = true } };
        if (GlobData.Config.StartupINISSConfig.RunAsAdmin) _actualINISSProcess.StartInfo.Verb = "runas";
        _actualINISSProcess.StartInfo.Arguments = GlobData.Config.StartupINISSConfig.CmdArgs;
        _actualINISSProcess.EnableRaisingEvents = true;
        _actualINISSProcess.Exited += ProcOnExited;
        try
        {
            _actualINISSProcess.Start();

            tsbShutdownINISS.Enabled = true;
            tsmimShutdownINISS.Enabled = true;
            tsbKillINISS.Enabled = true;
            tsmimKillINISS.Enabled = true;
            tsbRestartINISS.Enabled = true;
            tsmimRestartINISS.Enabled = true;
        }
        catch (Exception e)
        {
            Utils.ShowError(string.Format(Resources.FMain_Nepodarilo_sa_spustiť_vybraný_program, e.Message));
        }
    }

    private void ProcOnExited(object sender, EventArgs e)
    {
        _actualINISSProcess?.Dispose();
        _actualINISSProcess = null;

        Invoke(new Action(() =>
        {
            tsbShutdownINISS.Enabled = false;
            tsmimShutdownINISS.Enabled = false;
            tsbKillINISS.Enabled = false;
            tsmimKillINISS.Enabled = false;
            tsbRestartINISS.Enabled = false;
            tsmimRestartINISS.Enabled = false;
        }));
    }

    private void tssbStartINISS_ButtonClick(object sender, EventArgs e)
    {
        if (_actualINISSProcess != null)
        {
            var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
            if (result == DialogResult.Yes) _actualINISSProcess.Kill();
        }
        else
        {
            if (_lastINISSStart != null)
                ExecuteINISS(_lastINISSStart);
            else
                tssbStartINISS.ShowDropDown();
        }
    }

    private void tsmimStartINISS_Click(object sender, EventArgs e)
    {
        if (_actualINISSProcess != null)
        {
            var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
            if (result == DialogResult.Yes) _actualINISSProcess.Kill();
        }
        else
        {
            if (_lastINISSStart != null)
                ExecuteINISS(_lastINISSStart);
            else
                tsmiRun.ShowDropDown();
        }
    }

    private void KillINISS()
    {
        _actualINISSProcess?.Kill();
    }

    private void ShutDownINISS()
    {
        //https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand
        _actualINISSProcess?.SendMessage(0x0112, (IntPtr)0xF060, IntPtr.Zero);
    }

    private void RestartINISS()
    {
        _actualINISSProcess?.Kill();
        ExecuteINISS(_lastINISSStart);
    }

    private void tsbKillINISS_Click(object sender, EventArgs e) => KillINISS();

    private void tsmimKillINISS_Click(object sender, EventArgs e) => KillINISS();

    private void tsbShutdownINISS_Click(object sender, EventArgs e) => ShutDownINISS();

    private void tsmimShutdownINISS_Click(object sender, EventArgs e) => ShutDownINISS();

    private void tsbRestartINISS_Click(object sender, EventArgs e) => RestartINISS();

    private void tsmimRestartINISS_Click(object sender, EventArgs e) => RestartINISS();

    //LOCAL SETTINGS
    private void tsmiGrafikon_Click(object sender, EventArgs e) => ShowLocalSettings(0);

    private void tsmiStanice_Click(object sender, EventArgs e) => ShowLocalSettings(1);

    private void tsmiDopravcovia_Click(object sender, EventArgs e) => ShowLocalSettings(2);

    private void tsmiPlatforms_Click(object sender, EventArgs e) => ShowLocalSettings(3);

    private void tsmiKolaje_Click(object sender, EventArgs e) => ShowLocalSettings(4);

    private void tsmiTPhysical_Click(object sender, EventArgs e) => ShowLocalSettings(5);

    private void tsmiTLogical_Click(object sender, EventArgs e) => ShowLocalSettings(6);

    private void tsmiTCatalog_Click(object sender, EventArgs e) => ShowLocalSettings(7);

    private void tsmiTabTab_Click(object sender, EventArgs e) => ShowLocalSettings(8);

    private void tsmiTTexts_Click(object sender, EventArgs e) => ShowLocalSettings(9);

    private void tsmiTFonts_Click(object sender, EventArgs e) => ShowLocalSettings(10);

    private void tsmiTabTabEditor_Click(object sender, EventArgs e) => ShowLocalSettings(-2);

    //GLOBAL SETTINGS
    private void tsmiGrafikony_Click(object sender, EventArgs e) => ShowGlobalSettings(0);

    private void tsmiLanguages_Click(object sender, EventArgs e) => ShowGlobalSettings(1);

    private void tsmiMeskania_Click(object sender, EventArgs e) => ShowGlobalSettings(2);

    private void tsmiTypyVlakov_Click(object sender, EventArgs e) => ShowGlobalSettings(3);

    private void tsmiAudio_Click(object sender, EventArgs e) => ShowGlobalSettings(4);

    //DATE LIMIT
    private void tsmiDatObm_Click(object sender, EventArgs e) => ShowDatObm();

    private void tsbDatObm_Click(object sender, EventArgs e) => ShowDatObm();

    private static void ShowDatObm()
    {
        var fobm = new FDatObm();
        fobm.ShowDialog();
    }

    private void ChangeEnableMenuItemsLSettings(bool enabled)
    {
        tsmiGrafikon.Enabled = enabled;
        tsmiStanice.Enabled = enabled;
        tsmiDopravcovia.Enabled = enabled;
        tsmiPlatforms.Enabled = enabled;
        tsmiKolaje.Enabled = enabled;
        tsmiTPhysical.Enabled = enabled;
        tsmiTLogical.Enabled = enabled;
        tsmiTCatalog.Enabled = enabled;
        tsmiTabTab.Enabled = enabled;
        tsmiTTexts.Enabled = enabled;
        tsmiTFonts.Enabled = enabled;
        tsmiTabTabEditor.Enabled = enabled;
    }

    private void ChangeEnableMenuItemsGSettings(bool enabled)
    {
        tsmiGrafikony.Enabled = enabled;
        tsmiLanguages.Enabled = enabled;
        tsmiMeskania.Enabled = enabled;
        tsmiTypyVlakov.Enabled = enabled;
        tsmiAudio.Enabled = enabled;
    }

    private class PathAndGVD
    {
        public string Path { get; set; }
        public GVDInfo Gvd { get; set; }
    }

    internal class SendData
    {
        public string Path { get; set; }
        public List<Train> DefTrains { get; set; }
        public GVDInfo GVDInfo { get; set; }
        public Track Track { get; set; }
        public bool OmitPassingTrains { get; set; }
        public bool ReorderTrains { get; set; }
        public bool DefinedOperators { get; set; }
        public bool DefinedDateLimits { get; set; }
        public bool NewFormat { get; set; }
    }
}