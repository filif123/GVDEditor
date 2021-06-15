﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;
using GVDEditor.XML;
using ExControls;
using Microsoft.VisualBasic.FileIO;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Hlavna pracovna plocha
    /// </summary>
    public partial class FMain : Form
    {
        /// <summary>
        ///     Dostupne stanice
        /// </summary>
        public static readonly BindingList<string> Stanice = new();

        /// <summary>
        ///     Vsetky dostupne priecinky s grafikonmi
        /// </summary>
        public static readonly BindingList<GVDDirectory> ObdobiaList = new();

        /// <summary>
        ///     Vsetky vlaky v tomto grafikone
        /// </summary>
        public static SortableBindingList<Train> Trains = new();

        private readonly List<GVDDirectory> GVDDirs = new();

        private bool removingGVD;
        private bool error;
        private bool gvdinfoChanged;
        private GVDDirectory newDir;
        private BindingList<Operator> Operators;
        private bool operatorsChanged;
        private bool prechod;

        private GVDDirectory previousSelectedGVD;
        private bool stationsChanged;
        private bool tableFontsChanged;
        private bool tablesChanged;

        private BindingList<Track> Tracks;
        private bool tracksChanged;

        private bool trainsChanged;

        private FWait waitForm;

        private string lastINISSStart;
        private Process actualINISSProcess;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public FMain()
        {
            InitializeComponent();

            if (AutoUpdater.UpdateAvailable(out var version))
            {
                var res = Utils.ShowInfo(string.Format(Resources.RUpdateInfo, version), Resources.RUpdate, MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes) 
                    Process.Start(LinkConsts.LINK_DOWNLOAD);
            }

            FormUtils.SetFormFont(this);
            hlavneMenu.Renderer = new ToolStripProfessionalRenderer(new FormUtils.LightColorTable());

            tsslDate.Font = GlobData.Config.Fonts.StateRow.Font;
            tsslTime.Font = GlobData.Config.Fonts.StateRow.Font;
            statusStrip.Visible = GlobData.Config.ShowStateRow;
            dgvTrains.RowHeadersVisible = GlobData.Config.ShowRowsHeader;
            tsslDate.Visible = GlobData.Config.ShowDateTimeInStateRow;
            tsslTime.Visible = GlobData.Config.ShowDateTimeInStateRow;
            timerTimeAndDate.Enabled = GlobData.Config.ShowStateRow && GlobData.Config.ShowDateTimeInStateRow;

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
                case Config.DesktopMenu.TS_MS:
                    hlavneMenu.Visible = true;
                    toolMenu.Visible = true;
                    hlavneMenu.Items.Remove(tscbObdobie);
                    hlavneMenu.Items.Remove(tscbStanica);
                    break;
                case Config.DesktopMenu.MS_ONLY:
                    hlavneMenu.Visible = true;
                    toolMenu.Visible = false;
                    hlavneMenu.Items.Add(tscbObdobie);
                    hlavneMenu.Items.Add(tscbStanica);
                    break;
                case Config.DesktopMenu.TS_ONLY:
                    hlavneMenu.Visible = false;
                    toolMenu.Visible = true;
                    hlavneMenu.Items.Remove(tscbObdobie);
                    hlavneMenu.Items.Remove(tscbStanica);
                    break;
            }

            smerovanieDataGridViewTextBoxColumn.DefaultCellStyle.NullValue = new Bitmap(1, 1);
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

            var dirs = AppRegistry.GetRecentDirs();

            foreach (var recentDir in dirs)
            {
                ToolStripItem item1 = new ToolStripMenuItem(recentDir);
                ToolStripItem item2 = new ToolStripMenuItem(recentDir);

                item1.Click += nedavneDirsClick;
                item2.Click += nedavneDirsClick;
                tsmiRecent.DropDownItems.Add(item1);
                tssbRecentDirs.DropDownItems.Add(item2);
            }

            if (dirs.Count == 0 || dirs[0] == "")
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

            this.ApplyTheme();
        }

        /// <inheritdoc />
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (GlobData.Config.DebugModeGUI != Config.DebugMode.APP_CRASH)
                try
                {
                    ProccessData((PathAndGVD) e.Argument);
                }
                catch (Exception exception)
                {
                    Log.Exception(exception);

                    if (GlobData.Config.DebugModeGUI == Config.DebugMode.ONLY_MESSAGE)
                        Utils.ShowError(exception.Message);
                    if (GlobData.Config.DebugModeGUI == Config.DebugMode.DETAILED_INFO)
                        Utils.ShowError(exception.ToString());

                    error = true;
                    return;
                }
            else
                ProccessData((PathAndGVD) e.Argument);

            error = false;
        }

        private void ProccessData(PathAndGVD pathgvd)
        {
            TXTParser.ReadCustomStations(pathgvd.Path, pathgvd.Gvd);

            TXTParser.ReadTables(pathgvd.Path);

            TXTParser.ReadOperators(pathgvd.Path);
            Operators = new BindingList<Operator>(GlobData.Operators);
            
            TXTParser.ReadTracks(pathgvd.Path);
            Tracks = new BindingList<Track>(GlobData.Tracks);

            var nastupistia = new HashSet<Platform>();
            foreach (var kolaj in GlobData.Tracks) 
                nastupistia.Add(kolaj.Nastupiste);
            GlobData.Platforms = nastupistia.ToList();

            TXTParser.ReadLocalCategori(pathgvd.Path);
            InitDruhyReportov();

            var allSounds = new List<FyzZvuk>();
            foreach (var language in GlobData.LocalLanguages)
                allSounds.AddRange(language.IsBasic ? GlobData.Sounds : RawBankReader.ReadFyzZvukFile(GlobData.RAWBANKDir, language));

            try
            {
                TXTParser.ReadRazeni1(pathgvd.Path, allSounds);
            }
            catch (FileNotFoundException)
            {
            }

            Trains = new SortableBindingList<Train>(TXTParser.ReadTrains(pathgvd.Path));

            TXTParser.ReadTTexts(pathgvd.Path, Trains.ToList());
            TXTParser.ReadTableFonts(pathgvd.Path);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            waitForm.Close();

            if (!error)
            {
                Kolaj.DataSource = Tracks;
                Dopravca.DataSource = Operators;

                prechod = true;
                dgvTrains.DataSource = Trains;
                prechod = false;

                SetColumnsAutoWidth();

                //sfunkcnit tlacidla v toolstripe
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
                prechod = true;
                Trains.Clear();
                prechod = false;

                //znefunkcnit tlacidla v toolstripe
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

            UseWaitCursor = false;
        }

        private void DoSave()
        {
            if (GlobData.Config.DebugModeGUI == Config.DebugMode.APP_CRASH)
            {
                DoSaveInternal();
            }
            else
            {
                try
                {
                    DoSaveInternal();
                }
                catch (Exception e)
                {
                    bool monly = GlobData.Config.DebugModeGUI == Config.DebugMode.ONLY_MESSAGE;
                    Utils.ShowError(monly ? e.Message : e.ToString());
                }
            }
            
            void DoSaveInternal()
            {
                var dir = previousSelectedGVD;

                if (trainsChanged || stationsChanged)
                {
                    TXTParser.WriteTrains(dir.Dir.FullPath, Trains.ToList(), dir.GVD, GlobData.ReportVariants);
                    TXTParser.WriteRazeni1(dir.Dir.FullPath, GlobData.Radenia, GlobData.LocalLanguages);

                    if (GlobData.Config.AutoTableText)
                    {
                        tablesChanged = true;

                        GenerateTableTextWhileSaving(dir);
                    }
                }

                if (tablesChanged)
                {
                    TXTParser.WriteTables(dir.Dir.FullPath, GlobData.TabTabs, GlobData.TableCatalogs,
                        GlobData.TablePhysicals, GlobData.TableLogicals);
                    TXTParser.WriteTTexts(dir.Dir.FullPath, GlobData.TableTexts);
                }

                if (tracksChanged) TXTParser.WriteTracks(dir.Dir.FullPath, GlobData.Tracks);

                if (gvdinfoChanged) TXTParser.WriteInfoGVD(dir.Dir.FullPath, dir.GVD);

                if (operatorsChanged) TXTParser.WriteOperators(dir.Dir.FullPath, GlobData.Operators);

                if (tableFontsChanged)
                    TXTParser.WriteModeTabs(dir.Dir.FullPath, GlobData.TableFonts, GlobData.TableFontDir);

                SetChangesAreSaved();
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

                if ((string) tscbStanica.ComboBox.SelectedItem == gvd.ThisStation.Name)
                    ObdobiaList.Add(new GVDDirectory(gvd.StartValidTimeTable.Year + "/" + gvd.EndValidTimeTable.Year, dir, gvd));

                var dirs = TXTParser.ReadDirList();
                dirs.Add(dir);
                TXTParser.WriteDirList(dirs);
                Directory.CreateDirectory(dir.FullPath);
                TXTParser.WriteInfoGVD(dir.FullPath, gvd);

                var dgyv = new GVDDirectory(gvd.StartValidTimeTable.Year + "/" + gvd.EndValidTimeTable.Year, dir, gvd);
                GVDDirs.Add(dgyv);
                newDir = dgyv;

                tscbStanica.ComboBox.SelectedItem = null;
                tscbStanica.ComboBox.SelectedItem = gvd.ThisStation.Name;
            }
        }

        private void ShowImportData()
        {
            var fid = new FImportData(((GVDDirectory) tscbObdobie.ComboBox.SelectedItem).GVD);
            var result = fid.ShowDialog();
            if (result == DialogResult.OK) Trains.ResetBindings();
        }

        private void ShowImportELIS()
        {
            var fimport = new FELISOptions();
            var result = fimport.ShowDialog();
            if (result == DialogResult.OK)
            {
                var data = fimport.ResultOptions;
                data.GVDInfo = ((GVDDirectory) tscbObdobie.ComboBox.SelectedItem).GVD;
                data.Track = GlobData.Tracks.FirstOrDefault();
                data.DefTrains = Trains.ToList();

                error = false;
                UseWaitCursor = true;
                waitForm = new FWait("Prebieha importovanie údajov...");
                waitForm.Show(this);
                if (!bWorkerELIS.IsBusy) bWorkerELIS.RunWorkerAsync(data);
            }
        }

        private void ShowImportGVD()
        {
            var dialog = new ExFolderBrowserDialog {Title = "Vyberte priečinok obsahujúci grafikon"};
            if (!dialog.Show(Handle)) return;

            var selectedPath = dialog.FileName;

            var dirname = Utils.GetDirectoryName(selectedPath);

            var newDirPath = Utils.CombinePath(GlobData.DATADir, dirname);

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
                var gvd = TXTParser.ReadInfoGVD(newDirPath);
                var dirList = new DirList {DirName = dirname, FullPath = selectedPath};
                var dgyv = new GVDDirectory(gvd.StartValidTimeTable.Year + "/" + gvd.EndValidTimeTable.Year, dirList, gvd);

                var dirs = TXTParser.ReadDirList();
                dirs.Add(dirList);
                TXTParser.WriteDirList(dirs);

                GlobData.GVDDirs.Add(dirList);

                if (!Stanice.Contains(gvd.ThisStation.Name)) Stanice.Add(gvd.ThisStation.Name);

                if ((string) tscbStanica.ComboBox.SelectedItem == gvd.ThisStation.Name) ObdobiaList.Add(dgyv);

                GVDDirs.Add(dgyv);
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
            var appSettings = new FAppSettings();
            var result = appSettings.ShowDialog();
            if (result == DialogResult.OK)
            {
                UpdateMainUI();
            }
        }

        private void UpdateMainUI()
        {
            var menu = GlobData.Config.DesktopMenuMode;
            hlavneMenu.Visible = menu is Config.DesktopMenu.TS_MS or Config.DesktopMenu.MS_ONLY;
            toolMenu.Visible = menu is Config.DesktopMenu.TS_MS or Config.DesktopMenu.TS_ONLY;
            statusStrip.Visible = GlobData.Config.ShowStateRow;
            tsslDate.Visible = GlobData.Config.ShowDateTimeInStateRow;
            tsslTime.Visible = GlobData.Config.ShowDateTimeInStateRow;
            timerTimeAndDate.Enabled = GlobData.Config.ShowStateRow && GlobData.Config.ShowDateTimeInStateRow;
            dgvTrains.RowHeadersVisible = GlobData.Config.ShowRowsHeader;

            SetColumns();
            SetColumnsAutoWidth();
            SetShortcuts();
            Refresh();
        }

        private static void ShowInformationApp()
        {
            var iform = new FInformation();
            iform.ShowDialog();
        }

        private void ShowLocalSettings(int startIndex = -1)
        {
            var dir = (GVDDirectory) tscbObdobie.ComboBox.SelectedItem;
            var svform = new FLocalSettings(dir, startIndex);
            var result = svform.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (dir.GVD != svform.ThisDir.GVD)
                {
                    gvdinfoChanged = true;
                    Stanice.ResetBindings();
                }

                tablesChanged = true;

                GlobData.TabTabs = FLocalSettings.TabTabs.ToList();
                GlobData.TableCatalogs = FLocalSettings.TCatalogs.ToList();
                GlobData.TablePhysicals = FLocalSettings.TPhysicals.ToList();
                GlobData.TableLogicals = FLocalSettings.TLogicals.ToList();
                GlobData.TableTexts = FLocalSettings.TTexts.ToList();

                tracksChanged = true;

                GlobData.Tracks = svform.Kolaje.ToList();
                GlobData.Platforms = svform.Nastupistia.ToList();

                Tracks = new BindingList<Track>(GlobData.Tracks);

                operatorsChanged = true;

                GlobData.Operators = svform.Dopravcovia.ToList();
                Operators = new BindingList<Operator>(GlobData.Operators);

                tableFontsChanged = true;

                GlobData.TableFonts = FLocalSettings.TFonts.ToList();
                GlobData.TableFontDir = svform.FontDir;
                
                stationsChanged = true;
                GlobData.CustomStations = svform.CustomStations.ToList();

                if (gvdinfoChanged || tablesChanged || tracksChanged || operatorsChanged || tableFontsChanged || stationsChanged)
                    SetChangesNotSaved();

                Trains.ResetBindings();
            }
        }

        private void ShowGlobalSettings(int startIndex = -1)
        {
            var gf = new FGlobalSettings(GVDDirs.ToList(), startIndex);
            var result = gf.ShowDialog();
            if (result == DialogResult.OK)
            {
                var dirlist = gf.Grafikony.Select(gvd => gvd.Dir).ToList();

                GlobData.GVDDirs = dirlist;
                TXTParser.WriteDirList(dirlist);

                GlobData.TrainsTypes = gf.TrainTypes.ToList();
                TXTParser.WriteTrainTypes(GlobData.TrainsTypes);

                GlobData.Delays = FGlobalSettings.Meskania.ToList();
                TXTParser.WriteZpozdeni(GlobData.Delays);

                GlobData.Audios = gf.Audios.ToList();
                if (GlobData.Audios.Count != 0) TXTParser.WriteAudio(GlobData.Audios);

                removingGVD = true;
                foreach (var gvd in gf.RemovedGVDs)
                {
                    if (GVDDirs.Count == 1)
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

                        prechod = true;
                        Trains.Clear();
                        prechod = false;
                    }

                    GVDDirs.Remove(gvd);
                    ObdobiaList.Remove(gvd);

                    GlobData.GVDDirs.Remove(gvd.Dir);

                    TXTParser.WriteDirList(GlobData.GVDDirs);

                    try
                    {
                        FileSystem.DeleteDirectory(gvd.Dir.FullPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    removingGVD = false;
                }
            }
        }

        private void ShowEditTrain(Train train, int row, bool copy = false)
        {
            var eform = new FEditTrain(train, row, ((GVDDirectory) tscbObdobie.ComboBox.SelectedItem).GVD, copy);
            var result = eform.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (train == null || row == Trains.Count)
                {
                    Trains.Add(eform.ThisTrain);
                    trainsChanged = true;
                    SetChangesNotSaved();
                }
                else
                {
                    Trains.ResetBindings();

                    trainsChanged = true;
                    SetChangesNotSaved();
                }
            }
        }

        private void ShowOpenDir()
        {
            var dialog = new ExFolderBrowserDialog {Title = "Vyberte priečinok s INISS.exe"};
            if (!dialog.Show(Handle)) return;

            var selectedPath = dialog.FileName;

            prechod = true;
            Trains.Clear();
            prechod = false;

            if (GlobData.Config.DebugModeGUI != Config.DebugMode.APP_CRASH)
                try
                {
                    GlobData.PrepareGlobalData(selectedPath);
                }
                catch (Exception e)
                {
                    Log.Exception(e);

                    switch (GlobData.Config.DebugModeGUI)
                    {
                        case Config.DebugMode.ONLY_MESSAGE:
                            Utils.ShowError(e.Message);
                            break;
                        case Config.DebugMode.DETAILED_INFO:
                            Utils.ShowError(e.ToString());
                            break;
                    }

                    return;
                }
            else
                GlobData.PrepareGlobalData(selectedPath);

            AppRegistry.SetNewRecentDir(selectedPath);

            if (InitializeDataList()) InitializeGUI();
        }

        private void ShowStartupINISSSettings()
        {
            var appSettings = new FAppSettings(FAppSettings.STARTUP);
            var result = appSettings.ShowDialog();
            if (result == DialogResult.OK)
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

                    Trains.RemoveAt(row.Index);
                }

                Trains.ResetBindings();
                SetChangesNotSaved();
            }
        }

        private static bool InitializeDataList()
        {
            try
            {
                var stanice = new HashSet<string>();
                var obdobiaList = new List<GVDDirectory>();
                var dirsInData = TXTParser.ReadDirList();
                foreach (var dir in dirsInData)
                {
                    GVDInfo gvd;
                    if (GlobData.Config.DebugModeGUI == Config.DebugMode.APP_CRASH)
                    {
                        gvd = TXTParser.ReadInfoGVD(dir.FullPath);
                        stanice.Add(gvd.ThisStation.Name);
                        obdobiaList.Add(new GVDDirectory(gvd.StartValidTimeTable.Year + "/" + gvd.EndValidTimeTable.Year, dir, gvd));
                    }
                    else
                    {
                        try
                        {
                            gvd = TXTParser.ReadInfoGVD(dir.FullPath);
                            stanice.Add(gvd.ThisStation.Name);
                            obdobiaList.Add(new GVDDirectory(gvd.StartValidTimeTable.Year + "/" + gvd.EndValidTimeTable.Year, dir, gvd));
                        }
                        catch (Exception e)
                        {
                            Utils.ShowError(GlobData.Config.DebugModeGUI == Config.DebugMode.DETAILED_INFO ? e.ToString() : e.Message);
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

            return true;
        }

        private void SetChangesNotSaved()
        {
            Text = Application.ProductName + @" - *" + GlobData.INISSDir;
        }

        private void SetChangesAreSaved()
        {
            trainsChanged = false;
            tablesChanged = false;
            tracksChanged = false;
            gvdinfoChanged = false;
            operatorsChanged = false;
            tableFontsChanged = false;
            stationsChanged = false;
            Text = Text.Replace("*", "");
        }

        private void InitializeGUI()
        {
            //vlozit stanice a obdobia do combo boxov v tool stripe
            GVDDirs.Clear();
            GVDDirs.AddRange(ObdobiaList);

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

            trainsChanged = false;
            tablesChanged = false;
            tracksChanged = false;
            gvdinfoChanged = false;
            operatorsChanged = false;
            tableFontsChanged = false;
            stationsChanged = false;

            foreach (var file in GlobData.INISSExeFiles)
            {
                ToolStripItem item1 = new ToolStripMenuItem(file);
                item1.Click += InissStartItemOnClick;
                tssbStartINISS.DropDownItems.Add(item1);

                ToolStripItem item2 = new ToolStripMenuItem(file);
                item2.Click += InissStartItemOnClick;
                tsmiRun.DropDownItems.Add(item2);
            }

            if (GlobData.INISSExeFiles.Count > 0)
            {
                tsmimStartINISS.Enabled = true;
            }

            foreach (ToolStripItem item in tssbStartINISS.DropDownItems)
            {
                item.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;
            }

            foreach (ToolStripItem item in tsmiRun.DropDownItems)
            {
                item.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;
            }
        }

        private void InissStartItemOnClick(object sender, EventArgs e)
        {
            if (sender is ToolStripItem tsmi)
            {
                if (actualINISSProcess == null)
                {
                    string path = Utils.CombinePath(GlobData.INISSDir, tsmi.Text);
                    ExecuteINISS(path);
                    lastINISSStart = path;
                }
                else
                {
                    var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
                    if (result == DialogResult.Yes)
                    {
                        actualINISSProcess.Kill();
                    }
                }
            }
        }

        private void nedavneDirsClick(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            var menuText = menuItem.Text;

            prechod = true;
            Trains.Clear();
            prechod = false;

            if (GlobData.Config.DebugModeGUI != Config.DebugMode.APP_CRASH)
                try
                {
                    GlobData.PrepareGlobalData(menuText);
                }
                catch (Exception exception)
                {
                    Log.Exception(exception);

                    switch (GlobData.Config.DebugModeGUI)
                    {
                        case Config.DebugMode.ONLY_MESSAGE:
                            Utils.ShowError(exception.Message);
                            break;
                        case Config.DebugMode.DETAILED_INFO:
                            Utils.ShowError(exception.ToString());
                            break;
                    }

                    return;
                }
            else
                GlobData.PrepareGlobalData(menuText);

            if (InitializeDataList()) InitializeGUI();
        }

        private void timerTimeAndDate_Tick(object sender, EventArgs e)
        {
            tsslDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
            tsslTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void tsbInformation_Click(object sender, EventArgs e) => ShowInformationApp();

        private void tscbStanica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbStanica.ComboBox.SelectedItem != null)
            {
                var dirs = new List<GVDDirectory>();
                var stanica = tscbStanica.ComboBox.SelectedItem.ToString();
                foreach (var gvdDir in GVDDirs)
                    if (stanica == gvdDir.GVD.ThisStation.Name)
                        dirs.Add(gvdDir);

                ObdobiaList.Clear();

                foreach (var dir in dirs) ObdobiaList.Add(dir);
                
                if (dirs.Count != 0 && newDir == null)
                {
                    tscbObdobie.ComboBox.SelectedItem = null;
                    tscbObdobie.ComboBox.SelectedIndex = 0;
                }
                else if (newDir != null)
                {
                    tscbObdobie.ComboBox.SelectedItem = null;
                    tscbObdobie.ComboBox.SelectedItem = newDir;
                }

                previousSelectedGVD ??= (GVDDirectory) tscbObdobie.ComboBox.SelectedItem;
            }
        }

        private void tscbObdobie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (removingGVD)
                return;
            
            var dir = (GVDDirectory) tscbObdobie.ComboBox.SelectedItem;

            if (dir == null) return;

            if (!string.IsNullOrEmpty(GlobData.INISSDir) &&
                (trainsChanged || tableFontsChanged || tablesChanged || gvdinfoChanged || tracksChanged ||
                 operatorsChanged || stationsChanged))
            {
                var dialogResult = MessageBox.Show(Resources.FMain_Save_Changes, Resources.RQuestion,
                    MessageBoxButtons.YesNoCancel);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        DoSave();
                        break;
                    case DialogResult.No:
                        SetChangesAreSaved();
                        break;
                    default:
                        return;
                }
            }

            previousSelectedGVD = dir;

            //ak sa jedna o novy grafikon
            if (Equals(dir, newDir))
            {
                GlobData.Tracks = new List<Track> {Track.None};
                GlobData.Platforms = new List<Platform> {Platform.None};

                GlobData.Operators = new List<Operator> {Operator.None};

                GlobData.TabTabs = new List<TableTabTab>();
                GlobData.TableCatalogs = new List<TableCatalog>();
                GlobData.TablePhysicals = new List<TablePhysical>();
                GlobData.TableLogicals = new List<TableLogical>();
                GlobData.TableTexts = new List<TableText>();
                GlobData.TableFonts = new List<TableFont>();

                GlobData.CustomStations = new List<Station>();

                GlobData.Radenia = new List<Radenie>();

                GlobData.ReportTypes = ReportType.GetDefaultValuesSK();
                GlobData.ReportVariants = ReportVariant.GetDefaultValues();

                prechod = true;
                Trains.Clear();
                prechod = false;

                TXTParser.WriteTrains(dir.Dir.FullPath, Trains.ToList(), dir.GVD, GlobData.ReportVariants);

                TXTParser.WriteTables(dir.Dir.FullPath, GlobData.TabTabs, GlobData.TableCatalogs, GlobData.TablePhysicals, GlobData.TableLogicals);
                TXTParser.WriteTTexts(dir.Dir.FullPath, GlobData.TableTexts);

                TXTParser.WriteTracks(dir.Dir.FullPath, GlobData.Tracks);

                TXTParser.WriteInfoGVD(dir.Dir.FullPath, dir.GVD);

                TXTParser.WriteOperators(dir.Dir.FullPath, GlobData.Operators);

                TXTParser.WriteModeTabs(dir.Dir.FullPath, GlobData.TableFonts, GlobData.TableFontDir);

                TXTParser.WriteStateDgm(dir.Dir.FullPath);

                TXTParser.WriteLocalCategori(dir.Dir.FullPath, GlobData.ReportVariants, GlobData.ReportTypes, GlobData.Languages);

                TXTParser.WriteRazeniDefault(dir.Dir.FullPath);
                TXTParser.WriteRazeni1Default(dir.Dir.FullPath);

                newDir = null;
                return;
            }

            prechod = true;
            dgvTrains.DataSource = null;
            Trains.Clear();
            prechod = false;

            error = false;
            UseWaitCursor = true;
            waitForm = new FWait();
            waitForm.Show(this);
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(new PathAndGVD {Path = dir.Dir.FullPath, Gvd = dir.GVD});
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
                if (dgvTrains.CurrentRow != null && e.RowIndex != -1 && Trains[e.RowIndex] != new Train())
                    ShowEditTrain(Trains[e.RowIndex], e.RowIndex);

            if (e.RowIndex != -1)
            {
                tsslSelTrainName.Visible = true;
                tsslSelTrainVariants.Visible = true;
                tsslSelTrainName.Text = $@"{Trains[e.RowIndex].Type} {Trains[e.RowIndex].Number}";
                tsslSelTrainVariants.Text = CountSelTrainVariants(Trains[e.RowIndex]).ToString();
            }
        }

        private static int CountSelTrainVariants(Train train)
        {
            int count = 0;
            foreach (var t in Trains)
            {
                if (t.NumberVariant.Number == train.Number)
                {
                    count++;
                }
            }
            return count;
        }

        private void dgvTrains_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgvTrains.Columns[e.ColumnIndex].Name == "Kolaj" && e.RowIndex != -1 && e.RowIndex < Trains.Count)
                Trains[e.RowIndex].Track = Tracks[0];
            else if (dgvTrains.Columns[e.ColumnIndex].Name == "Dopravca" && e.RowIndex != -1 &&
                     e.RowIndex < Trains.Count)
                Trains[e.RowIndex].Operator = Operators[0];
            else
                Utils.ShowError(Resources.FMain_dgvTrains_DataError_Tabuľka_obsahuje_nesprávny_údaj + e.Exception.Message);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(GlobData.INISSDir) &&
                (trainsChanged || tableFontsChanged || tablesChanged || gvdinfoChanged || tracksChanged ||
                 operatorsChanged || stationsChanged))
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

        private void tsbAddTrain_Click(object sender, EventArgs e) => ShowEditTrain(null, Trains.Count);

        private void tsbCopyTrain_Click(object sender, EventArgs e)
        {
            if (dgvTrains.SelectedRows.Count > 0)
            {
                var index = dgvTrains.SelectedRows[0].Index;
                ShowEditTrain(Trains[index], Trains.Count, true);
            }
        }

        private void tsbEditTrain_Click(object sender, EventArgs e)
        {
            if (dgvTrains.SelectedRows.Count > 0)
            {
                var index = dgvTrains.SelectedRows[0].Index;
                ShowEditTrain(Trains[index], index);
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
            if (dgvTrains.Columns[e.ColumnIndex].Name == "cisloDataGridViewTextBoxColumn" &&
                (string) e.FormattedValue == "") e.Cancel = true;

            if (dgvTrains.Columns[e.ColumnIndex].Name == "odchodDataGridViewTextBoxColumn" &&
                (string) e.FormattedValue == "" &&
                dgvTrains.Rows[e.RowIndex].Cells["prichodDataGridViewTextBoxColumn"].Value == null)
                e.Cancel = true;
            else if (dgvTrains.Columns[e.ColumnIndex].Name == "prichodDataGridViewTextBoxColumn" &&
                     (string) e.FormattedValue == "" &&
                     dgvTrains.Rows[e.RowIndex].Cells["odchodDataGridViewTextBoxColumn"].Value == null) e.Cancel = true;

            if (dgvTrains.Columns[e.ColumnIndex].Name == "DatumoveObmedzenieText")
            {
                var value = e.FormattedValue as string;
                var thistrain = Trains[e.RowIndex];
                var dateRemThis = new DateRem(thistrain.ZaciatokPlatnosti, thistrain.KoniecPlatnosti,
                    bInsertMarks: false);
                try
                {
                    dateRemThis.TxtToBitArray(value);
                }
                catch (Exception exception)
                {
                    Utils.ShowError(exception.Message);
                    e.Cancel = true;
                    return;
                }

                int i = 0;
                foreach (var train in Trains)
                {
                    if (Train.IsSameVariant(train,thistrain) && i != e.RowIndex)
                        if (train.ZaciatokPlatnosti == thistrain.ZaciatokPlatnosti && train.KoniecPlatnosti == thistrain.KoniecPlatnosti)
                            if (dateRemThis.Overlap(thistrain.DateremText, train.DateremText))
                            {
                                var obmand = dateRemThis.TxtAnd(train.DateremText, thistrain.DateremText);
                                var result = MessageBox.Show(string.Format(Resources.FEditTrain_DateRem_zasahuje_do_ineho_vlaku, train.Type,
                                        train.Number, train.Name, obmand), Resources.RWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    var fDateRemEdit = new FDateRemEdit(train, train.DateremText, thistrain.ZaciatokPlatnosti, thistrain.KoniecPlatnosti);
                                    var result2 = fDateRemEdit.ShowDialog();
                                    if (result2 == DialogResult.OK) train.DateremText = fDateRemEdit.DateRemEdited;
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

        private void tsmimAddTrain_Click(object sender, EventArgs e) => ShowEditTrain(null, Trains.Count);

        private void tsmimEditTrain_Click(object sender, EventArgs e)
        {
            if (dgvTrains.SelectedRows.Count > 0)
            {
                var index = dgvTrains.SelectedRows[0].Index;
                ShowEditTrain(Trains[index], index);
            }
        }

        private void tsmiDeleteTrain_Click(object sender, EventArgs e) => DoDeleteTrains();

        private void tsmiDuplikovat_Click(object sender, EventArgs e)
        {
            if (dgvTrains.SelectedRows.Count > 0)
            {
                var index = dgvTrains.SelectedRows[0].Index;
                ShowEditTrain(Trains[index], Trains.Count, true);
            }
        }

        private void tsmiLocalSettings_Click(object sender, EventArgs e) => ShowLocalSettings();

        private void tsmiGlobalSettings_Click(object sender, EventArgs e) => ShowGlobalSettings();

        private void tsmiAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();

        private void tsmiInformation_Click(object sender, EventArgs e) => ShowInformationApp();

        private void tsmiChangelog_Click(object sender, EventArgs e) => Process.Start("http://iniss.6f.sk/gvdeditor/novinky/");

        private void dgvTrains_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (Trains.Count != 0)
            {
                for (var i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    Trains[i].ID = i + 1;

                    if (Trains[i].Routing == null)
                    {
                        var vlak = Trains[i];
                        throw new ArgumentNullException(
                            $"Vlak {vlak.Type} {vlak.NumberVariant} {vlak.Name} nemá definované smerovanie.");
                    }

                    if (Trains[i].Routing == Routing.Prechadzajuci)
                    {
                        dgvTrains.Rows[i].Cells["odchodDataGridViewTextBoxColumn"].ReadOnly = false;
                        dgvTrains.Rows[i].Cells["prichodDataGridViewTextBoxColumn"].ReadOnly = false;
                    }
                    else if (Trains[i].Routing == Routing.Vychadzajuci)
                    {
                        dgvTrains.Rows[i].Cells["odchodDataGridViewTextBoxColumn"].ReadOnly = false;
                        dgvTrains.Rows[i].Cells["prichodDataGridViewTextBoxColumn"].ReadOnly = true;
                    }
                    else
                    {
                        dgvTrains.Rows[i].Cells["odchodDataGridViewTextBoxColumn"].ReadOnly = true;
                        dgvTrains.Rows[i].Cells["prichodDataGridViewTextBoxColumn"].ReadOnly = false;
                    }
                }

                tsslTrainCountWithVariants.Text = dgvTrains.Rows.Count.ToString();
                tsslTrainCount.Text = $@"({CountTrainVariants()})";
            }
        }

        private void dgvTrains_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (var i = 1; i <= Trains.Count; i++) Trains[i - 1].ID = i;

            if (!prechod)
            {
                trainsChanged = true;
                SetChangesNotSaved();
            }

            tsslTrainCountWithVariants.Text = dgvTrains.Rows.Count.ToString();
            tsslTrainCount.Text = $@"({CountTrainVariants()})";

            if (Trains.Count == 0)
            {
                tsslSelTrainName.Visible = false;
                tsslSelTrainVariants.Visible = false;
            }
        }

        private static int CountTrainVariants()
        {
            HashSet<(string num, TrainType type, string name)> trains = new HashSet<(string num, TrainType type, string name)>();
            foreach (var t in Trains)
            {
                trains.Add((t.Number, t.Type, t.Name));
            }

            return trains.Count;
        }

        private void dgvTrains_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                trainsChanged = true;
                SetChangesNotSaved();
            }
        }

        private void tsbAppSettings_Click(object sender, EventArgs e) => ShowAppSettings();

        private void tsmiStartupSettings_Click(object sender, EventArgs e) => ShowStartupINISSSettings();

        private void tsmimStartupSettings_Click(object sender, EventArgs e) => ShowStartupINISSSettings();

        private void dgvTrains_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            void SetFromScheme(ColorSetting sett)
            {
                e.CellStyle.ForeColor = sett.ForeColor != SystemColors.ControlText
                    ? sett.ForeColor
                    : Color.White;
                if (sett.BackColor != Color.White) e.CellStyle.BackColor = sett.BackColor;
                e.CellStyle.Font = sett.Bold
                    ? new Font(GlobData.UsingStyle.TrainTypeColumnScheme.Font, FontStyle.Bold)
                    : GlobData.UsingStyle.TrainTypeColumnScheme.Font;
            }

            if (dgvTrains.Columns["typDataGridViewTextBoxColumn"] != null && e.ColumnIndex == dgvTrains.Columns["typDataGridViewTextBoxColumn"].Index)
                if (e.RowIndex < Trains.Count)
                {
                    var type = Trains[e.RowIndex].Type;
                    if (type.IsCustom)
                    {
                        var stype = type.CategoryTrain.ToUpper();
                        if (stype.StartsWith("X"))
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.X);
                        else if (stype.StartsWith("R"))
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.R);
                        else if (stype.StartsWith("SL"))
                            SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.Sl);
                        else if (stype.StartsWith("OS")) SetFromScheme(GlobData.UsingStyle.TrainTypeColumnScheme.Os);
                    }
                    else
                    {
                        switch (type.Key)
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

                foreach (var vlak in Trains)
                {
                    var tableTrain = new TableTrain {FontId = -1, Train = vlak};

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
            SetCol(DatumoveObmedzenieText, GlobData.Config.DesktopCols.DateRem);
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
            tsmiNew.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.NewGVD.Shortcut.ThisShortcut;
            tsmiOpen.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.OpenGVD.Shortcut.ThisShortcut;
            tsmiImportData.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.ImportData.Shortcut.ThisShortcut;
            tsmiImportGVD.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.ImportGVD.Shortcut.ThisShortcut;
            tsmiSave.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.Save.Shortcut.ThisShortcut;
            tsmiAnalyze.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.Analyze.Shortcut.ThisShortcut;

            tsmimAddTrain.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.AddTrain.Shortcut.ThisShortcut;
            tsmimEditTrain.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.EditTrain.Shortcut.ThisShortcut;
            tsmiDeleteTrain.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.DeleteTrains.Shortcut.ThisShortcut;
            tsmiDuplikovat.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.DuplicateTrain.Shortcut.ThisShortcut;

            tsmiVlastnostiStanice.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LocalSettings.Shortcut.ThisShortcut;
            tsmiGlobalSettings.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GlobalSettings.Shortcut.ThisShortcut;
            tsmiAppSettings.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.AppSettings.Shortcut.ThisShortcut;

            tsmiInformation.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.InfoApp.Shortcut.ThisShortcut;
            tsmiChangelog.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.UpdateNotes.Shortcut.ThisShortcut;

            tsmimStartINISS.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.RunINISS.Shortcut.ThisShortcut;
            tsmimExitINISS.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.KillINISS.Shortcut.ThisShortcut;
            tsmimRestartINISS.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.RestartINISS.Shortcut.ThisShortcut;

            tsmiGrafikon.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSGrafikon.Shortcut.ThisShortcut;
            tsmiStanice.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSStanice.Shortcut.ThisShortcut;
            tsmiDopravcovia.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSDopravcovia.Shortcut.ThisShortcut;
            tsmiPlatforms.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSPlatforms.Shortcut.ThisShortcut;
            tsmiKolaje.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSKolaje.Shortcut.ThisShortcut;
            tsmiTPhysical.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTPhysicals.Shortcut.ThisShortcut;
            tsmiTLogical.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTLogicals.Shortcut.ThisShortcut;
            tsmiTCatalog.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTCatalogs.Shortcut.ThisShortcut;
            tsmiTabTab.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTabTab.Shortcut.ThisShortcut;
            tsmiTTexts.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTTexts.Shortcut.ThisShortcut;
            tsmiTFonts.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTFonts.Shortcut.ThisShortcut;
            tsmiTabTabEditor.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.LSTabTabEditor.Shortcut.ThisShortcut;

            tsmiGrafikony.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GSGrafikony.Shortcut.ThisShortcut;
            tsmiLanguages.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GSLanguages.Shortcut.ThisShortcut;
            tsmiMeskania.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GSMeskania.Shortcut.ThisShortcut;
            tsmiTypyVlakov.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GSTrainTypes.Shortcut.ThisShortcut;
            tsmiAudio.ShortcutKeys = (Keys) GlobData.Config.Shortcuts.GSAudio.Shortcut.ThisShortcut;
        }

        private static void CheckAutoTrainVariants(int index)
        {
            if (!GlobData.Config.AutoVariant) return;

            var id = 0;
            var seltrains = new List<Train>();
            var dtrain = Trains[index];

            foreach (var train in Trains)
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
            var data = (SendData) e.Argument;

            if (GlobData.Config.DebugModeGUI != Config.DebugMode.APP_CRASH)
                try
                {
                    e.Result = ELISParser.ReadELISFile(data.Path, data.DefTrains, GlobData.TrainsTypes, GlobData.Operators,
                        data.GVDInfo, data.Track, data.OmitPassingTrains, data.DefinedOperators, data.DefinedDateRems);
                }
                catch (Exception exception)
                {
                    Log.Exception(exception);

                    if (GlobData.Config.DebugModeGUI == Config.DebugMode.ONLY_MESSAGE)
                        Utils.ShowError(exception.Message);
                    if (GlobData.Config.DebugModeGUI == Config.DebugMode.DETAILED_INFO)
                        Utils.ShowError(exception.ToString());

                    error = true;
                    return;
                }
            else
                e.Result = ELISParser.ReadELISFile(data.Path, data.DefTrains, GlobData.TrainsTypes, GlobData.Operators, data.GVDInfo,
                    data.Track, data.OmitPassingTrains, data.DefinedOperators, data.DefinedDateRems);

            error = false;
        }

        private void bWorkerELIS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            waitForm.Close();
            Cursor.Current = Cursors.AppStarting;

            if (!error)
            {
                var imported = (List<Train>) e.Result;
                foreach (var train in imported) Trains.Add(train);

                trainsChanged = true;
                SetChangesNotSaved();
            }
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
            public bool DefinedOperators { get; set; }
            public bool DefinedDateRems { get; set; }
        }

        private static void DeleteTTexts(Train vlak)
        {
            foreach (var tt in GlobData.TableTexts)
            {
                for (var i = tt.Trains.Count - 1; i >= 0; i--)
                {
                    if (tt.Trains[i].Train == vlak)
                    {
                        tt.Trains.RemoveAt(i);
                    }
                }
            }
        }

        private void ExecuteINISS(string fileName)
        {
            actualINISSProcess = new Process {StartInfo = {FileName = fileName, UseShellExecute = true}};
            if (GlobData.Config.StartupINISSConfig.RunAsAdmin) actualINISSProcess.StartInfo.Verb = "runas";
            actualINISSProcess.StartInfo.Arguments = GlobData.Config.StartupINISSConfig.CmdArgs;
            actualINISSProcess.EnableRaisingEvents = true;
            actualINISSProcess.Exited += ProcOnExited;
            try
            {
                actualINISSProcess.Start();

                tsbKillINISS.Enabled = true;
                tsmimExitINISS.Enabled = true;
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
            actualINISSProcess?.Dispose();
            actualINISSProcess = null;

            Invoke(new Action(() =>
            {
                tsbKillINISS.Enabled = false;
                tsmimExitINISS.Enabled = false;
                tsbRestartINISS.Enabled = false;
                tsmimRestartINISS.Enabled = false;
            }));
        }

        private void tssbStartINISS_ButtonClick(object sender, EventArgs e)
        {
            if (actualINISSProcess != null)
            {
                var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
                if (result == DialogResult.Yes)
                {
                    actualINISSProcess.Kill();
                }
            }
            else
            {
                if (lastINISSStart != null)
                    ExecuteINISS(lastINISSStart);
                else
                    tssbStartINISS.ShowDropDown();
            }
        }

        private void tsmimStartINISS_Click(object sender, EventArgs e)
        {
            if (actualINISSProcess != null)
            {
                var result = Utils.ShowQuestion(Resources.FMain_InissStartItemOnClick);
                if (result == DialogResult.Yes)
                {
                    actualINISSProcess.Kill();
                }
            }
            else
            {
                if (lastINISSStart != null)
                    ExecuteINISS(lastINISSStart);
                else
                    tsmiRun.ShowDropDown();
            }
        }

        private void tsbKillINISS_Click(object sender, EventArgs e) => actualINISSProcess?.Kill();

        private void tsmimExitINISS_Click(object sender, EventArgs e) => actualINISSProcess?.Kill();

        private void tsbRestartINISS_Click(object sender, EventArgs e)
        {
            actualINISSProcess?.Kill();
            ExecuteINISS(lastINISSStart);
        }

        private void tsmimRestartINISS_Click(object sender, EventArgs e)
        {
            actualINISSProcess?.Kill();
            ExecuteINISS(lastINISSStart);
        }


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
    }
}