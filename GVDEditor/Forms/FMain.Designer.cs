using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;
using ExControls;

namespace GVDEditor.Forms
{
    partial class FMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle73 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle74 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle75 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle76 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle77 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle78 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle79 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle80 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle81 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle82 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle83 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle84 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle85 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle86 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle87 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle88 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle89 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle90 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle91 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle92 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle93 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle94 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle95 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle96 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle97 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle98 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle99 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle100 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle101 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle102 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle103 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle104 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle105 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle106 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle107 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle108 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle109 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle110 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle111 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle112 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle113 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle114 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle115 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle116 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle117 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle118 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle119 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle120 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle121 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle122 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle123 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle124 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle125 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle126 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle127 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle128 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle129 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle130 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle131 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle132 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle133 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle134 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle135 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle136 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle137 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle138 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle139 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle140 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle141 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle142 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle143 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle144 = new ExControls.ExComboBoxStyle();
            this.hlavneMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiSubor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportGVD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportELIS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimAddTrain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimEditTrain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteTrain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDuplikovat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStanica = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVlastnostiStanice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrafikon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStanice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDopravcovia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPlatforms = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKolaje = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTPhysical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTLogical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTabTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTTexts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTFonts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTabTabEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGlobalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGrafikony = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMeskania = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTypyVlakov = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAppSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRun = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimStartupSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmimStartINISS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimExitINISS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimRestartINISS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNastavenia = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangelog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbImport = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmimImportData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimImportGVD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmimImportELIS = new System.Windows.Forms.ToolStripMenuItem();
            this.tssbRecentDirs = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbAnalyze = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGVD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddTrain = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyTrain = new System.Windows.Forms.ToolStripButton();
            this.tsbEditTrain = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteTrain = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStanica = new System.Windows.Forms.ToolStripButton();
            this.tsbGlobalSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAppSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbInformation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbStanica = new ExControls.ToolStripExComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbObdobie = new ExControls.ToolStripExComboBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbStartINISS = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiStartupSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKillINISS = new System.Windows.Forms.ToolStripButton();
            this.tsbRestartINISS = new System.Windows.Forms.ToolStripButton();
            this.timerTimeAndDate = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgvTrains = new System.Windows.Forms.DataGridView();
            this.cisloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkaPrichod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smerovanieDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.prichodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odchodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcVychodziaStanica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcKonecnaStanica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatumoveObmedzenieText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkaOdchod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kolaj = new ExControls.DataGridViewExComboBoxColumn();
            this.trackBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Dopravca = new ExControls.DataGridViewExComboBoxColumn();
            this.operatorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Ostatne = new System.Windows.Forms.DataGridViewButtonColumn();
            this.vlakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dOpenELIS = new System.Windows.Forms.OpenFileDialog();
            this.bWorkerELIS = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTrainCountWithVariants = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTrainCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSelTrainName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSelTrainVariants = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn1 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn2 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn3 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn4 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn5 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn6 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn7 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn8 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn9 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn10 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn11 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn12 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn13 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn14 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewExComboBoxColumn15 = new ExControls.DataGridViewExComboBoxColumn();
            this.dataGridViewExComboBoxColumn16 = new ExControls.DataGridViewExComboBoxColumn();
            this.hlavneMenu.SuspendLayout();
            this.toolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrains)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operatorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vlakBindingSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // hlavneMenu
            // 
            resources.ApplyResources(this.hlavneMenu, "hlavneMenu");
            this.hlavneMenu.BackColor = System.Drawing.SystemColors.Control;
            this.hlavneMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.hlavneMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSubor,
            this.tsmiUpravit,
            this.tsmiStanica,
            this.tsmiRun,
            this.tsmiNastavenia});
            this.hlavneMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.hlavneMenu.Name = "hlavneMenu";
            this.hlavneMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // tsmiSubor
            // 
            resources.ApplyResources(this.tsmiSubor, "tsmiSubor");
            this.tsmiSubor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.toolStripSeparator6,
            this.tsmiOpen,
            this.tsmiRecent,
            this.toolStripSeparator4,
            this.tsmiImport,
            this.toolStripSeparator3,
            this.tsmiSave,
            this.toolStripSeparator18,
            this.tsmiAnalyze});
            this.tsmiSubor.Name = "tsmiSubor";
            // 
            // tsmiNew
            // 
            resources.ApplyResources(this.tsmiNew, "tsmiNew");
            this.tsmiNew.Image = global::GVDEditor.Properties.Resources.create;
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // tsmiOpen
            // 
            resources.ApplyResources(this.tsmiOpen, "tsmiOpen");
            this.tsmiOpen.Image = global::GVDEditor.Properties.Resources.open;
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiRecent
            // 
            resources.ApplyResources(this.tsmiRecent, "tsmiRecent");
            this.tsmiRecent.Name = "tsmiRecent";
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // tsmiImport
            // 
            resources.ApplyResources(this.tsmiImport, "tsmiImport");
            this.tsmiImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImportData,
            this.tsmiImportGVD,
            this.tsmiImportELIS});
            this.tsmiImport.Image = global::GVDEditor.Properties.Resources.import;
            this.tsmiImport.Name = "tsmiImport";
            // 
            // tsmiImportData
            // 
            resources.ApplyResources(this.tsmiImportData, "tsmiImportData");
            this.tsmiImportData.Name = "tsmiImportData";
            this.tsmiImportData.Click += new System.EventHandler(this.tsmiImportData_Click);
            // 
            // tsmiImportGVD
            // 
            resources.ApplyResources(this.tsmiImportGVD, "tsmiImportGVD");
            this.tsmiImportGVD.Name = "tsmiImportGVD";
            this.tsmiImportGVD.Click += new System.EventHandler(this.tsmiImportGVD_Click);
            // 
            // tsmiImportELIS
            // 
            resources.ApplyResources(this.tsmiImportELIS, "tsmiImportELIS");
            this.tsmiImportELIS.Name = "tsmiImportELIS";
            this.tsmiImportELIS.Click += new System.EventHandler(this.tsmiImportELIS_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // tsmiSave
            // 
            resources.ApplyResources(this.tsmiSave, "tsmiSave");
            this.tsmiSave.Image = global::GVDEditor.Properties.Resources.save;
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // toolStripSeparator18
            // 
            resources.ApplyResources(this.toolStripSeparator18, "toolStripSeparator18");
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            // 
            // tsmiAnalyze
            // 
            resources.ApplyResources(this.tsmiAnalyze, "tsmiAnalyze");
            this.tsmiAnalyze.Image = global::GVDEditor.Properties.Resources.analyze;
            this.tsmiAnalyze.Name = "tsmiAnalyze";
            this.tsmiAnalyze.Click += new System.EventHandler(this.tsmiAnalyze_Click);
            // 
            // tsmiUpravit
            // 
            resources.ApplyResources(this.tsmiUpravit, "tsmiUpravit");
            this.tsmiUpravit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimAddTrain,
            this.tsmimEditTrain,
            this.toolStripSeparator2,
            this.tsmiDeleteTrain,
            this.toolStripSeparator1,
            this.tsmiDuplikovat});
            this.tsmiUpravit.Name = "tsmiUpravit";
            // 
            // tsmimAddTrain
            // 
            resources.ApplyResources(this.tsmimAddTrain, "tsmimAddTrain");
            this.tsmimAddTrain.Image = global::GVDEditor.Properties.Resources.add;
            this.tsmimAddTrain.Name = "tsmimAddTrain";
            this.tsmimAddTrain.Click += new System.EventHandler(this.tsmimAddTrain_Click);
            // 
            // tsmimEditTrain
            // 
            resources.ApplyResources(this.tsmimEditTrain, "tsmimEditTrain");
            this.tsmimEditTrain.Image = global::GVDEditor.Properties.Resources.edit;
            this.tsmimEditTrain.Name = "tsmimEditTrain";
            this.tsmimEditTrain.Click += new System.EventHandler(this.tsmimEditTrain_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tsmiDeleteTrain
            // 
            resources.ApplyResources(this.tsmiDeleteTrain, "tsmiDeleteTrain");
            this.tsmiDeleteTrain.Image = global::GVDEditor.Properties.Resources.delete;
            this.tsmiDeleteTrain.Name = "tsmiDeleteTrain";
            this.tsmiDeleteTrain.Click += new System.EventHandler(this.tsmiDeleteTrain_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // tsmiDuplikovat
            // 
            resources.ApplyResources(this.tsmiDuplikovat, "tsmiDuplikovat");
            this.tsmiDuplikovat.Image = global::GVDEditor.Properties.Resources.copy;
            this.tsmiDuplikovat.Name = "tsmiDuplikovat";
            this.tsmiDuplikovat.Click += new System.EventHandler(this.tsmiDuplikovat_Click);
            // 
            // tsmiStanica
            // 
            resources.ApplyResources(this.tsmiStanica, "tsmiStanica");
            this.tsmiStanica.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVlastnostiStanice,
            this.tsmiGlobalSettings,
            this.toolStripSeparator5,
            this.tsmiAppSettings});
            this.tsmiStanica.Name = "tsmiStanica";
            // 
            // tsmiVlastnostiStanice
            // 
            resources.ApplyResources(this.tsmiVlastnostiStanice, "tsmiVlastnostiStanice");
            this.tsmiVlastnostiStanice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGrafikon,
            this.tsmiStanice,
            this.tsmiDopravcovia,
            this.toolStripSeparator16,
            this.tsmiPlatforms,
            this.tsmiKolaje,
            this.toolStripSeparator15,
            this.tsmiTPhysical,
            this.tsmiTLogical,
            this.tsmiTCatalog,
            this.tsmiTabTab,
            this.tsmiTTexts,
            this.tsmiTFonts,
            this.toolStripSeparator17,
            this.tsmiTabTabEditor});
            this.tsmiVlastnostiStanice.Name = "tsmiVlastnostiStanice";
            this.tsmiVlastnostiStanice.Click += new System.EventHandler(this.tsmiLocalSettings_Click);
            // 
            // tsmiGrafikon
            // 
            resources.ApplyResources(this.tsmiGrafikon, "tsmiGrafikon");
            this.tsmiGrafikon.Name = "tsmiGrafikon";
            this.tsmiGrafikon.Click += new System.EventHandler(this.tsmiGrafikon_Click);
            // 
            // tsmiStanice
            // 
            resources.ApplyResources(this.tsmiStanice, "tsmiStanice");
            this.tsmiStanice.Name = "tsmiStanice";
            this.tsmiStanice.Click += new System.EventHandler(this.tsmiStanice_Click);
            // 
            // tsmiDopravcovia
            // 
            resources.ApplyResources(this.tsmiDopravcovia, "tsmiDopravcovia");
            this.tsmiDopravcovia.Name = "tsmiDopravcovia";
            this.tsmiDopravcovia.Click += new System.EventHandler(this.tsmiDopravcovia_Click);
            // 
            // toolStripSeparator16
            // 
            resources.ApplyResources(this.toolStripSeparator16, "toolStripSeparator16");
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            // 
            // tsmiPlatforms
            // 
            resources.ApplyResources(this.tsmiPlatforms, "tsmiPlatforms");
            this.tsmiPlatforms.Name = "tsmiPlatforms";
            this.tsmiPlatforms.Click += new System.EventHandler(this.tsmiPlatforms_Click);
            // 
            // tsmiKolaje
            // 
            resources.ApplyResources(this.tsmiKolaje, "tsmiKolaje");
            this.tsmiKolaje.Name = "tsmiKolaje";
            this.tsmiKolaje.Click += new System.EventHandler(this.tsmiKolaje_Click);
            // 
            // toolStripSeparator15
            // 
            resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            // 
            // tsmiTPhysical
            // 
            resources.ApplyResources(this.tsmiTPhysical, "tsmiTPhysical");
            this.tsmiTPhysical.Name = "tsmiTPhysical";
            this.tsmiTPhysical.Click += new System.EventHandler(this.tsmiTPhysical_Click);
            // 
            // tsmiTLogical
            // 
            resources.ApplyResources(this.tsmiTLogical, "tsmiTLogical");
            this.tsmiTLogical.Name = "tsmiTLogical";
            this.tsmiTLogical.Click += new System.EventHandler(this.tsmiTLogical_Click);
            // 
            // tsmiTCatalog
            // 
            resources.ApplyResources(this.tsmiTCatalog, "tsmiTCatalog");
            this.tsmiTCatalog.Name = "tsmiTCatalog";
            this.tsmiTCatalog.Click += new System.EventHandler(this.tsmiTCatalog_Click);
            // 
            // tsmiTabTab
            // 
            resources.ApplyResources(this.tsmiTabTab, "tsmiTabTab");
            this.tsmiTabTab.Name = "tsmiTabTab";
            this.tsmiTabTab.Click += new System.EventHandler(this.tsmiTabTab_Click);
            // 
            // tsmiTTexts
            // 
            resources.ApplyResources(this.tsmiTTexts, "tsmiTTexts");
            this.tsmiTTexts.Name = "tsmiTTexts";
            this.tsmiTTexts.Click += new System.EventHandler(this.tsmiTTexts_Click);
            // 
            // tsmiTFonts
            // 
            resources.ApplyResources(this.tsmiTFonts, "tsmiTFonts");
            this.tsmiTFonts.Name = "tsmiTFonts";
            this.tsmiTFonts.Click += new System.EventHandler(this.tsmiTFonts_Click);
            // 
            // toolStripSeparator17
            // 
            resources.ApplyResources(this.toolStripSeparator17, "toolStripSeparator17");
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            // 
            // tsmiTabTabEditor
            // 
            resources.ApplyResources(this.tsmiTabTabEditor, "tsmiTabTabEditor");
            this.tsmiTabTabEditor.Name = "tsmiTabTabEditor";
            this.tsmiTabTabEditor.Click += new System.EventHandler(this.tsmiTabTabEditor_Click);
            // 
            // tsmiGlobalSettings
            // 
            resources.ApplyResources(this.tsmiGlobalSettings, "tsmiGlobalSettings");
            this.tsmiGlobalSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGrafikony,
            this.tsmiLanguages,
            this.tsmiMeskania,
            this.tsmiTypyVlakov,
            this.tsmiAudio});
            this.tsmiGlobalSettings.Image = global::GVDEditor.Properties.Resources.global_settings;
            this.tsmiGlobalSettings.Name = "tsmiGlobalSettings";
            this.tsmiGlobalSettings.Click += new System.EventHandler(this.tsmiGlobalSettings_Click);
            // 
            // tsmiGrafikony
            // 
            resources.ApplyResources(this.tsmiGrafikony, "tsmiGrafikony");
            this.tsmiGrafikony.Name = "tsmiGrafikony";
            this.tsmiGrafikony.Click += new System.EventHandler(this.tsmiGrafikony_Click);
            // 
            // tsmiLanguages
            // 
            resources.ApplyResources(this.tsmiLanguages, "tsmiLanguages");
            this.tsmiLanguages.Name = "tsmiLanguages";
            this.tsmiLanguages.Click += new System.EventHandler(this.tsmiLanguages_Click);
            // 
            // tsmiMeskania
            // 
            resources.ApplyResources(this.tsmiMeskania, "tsmiMeskania");
            this.tsmiMeskania.Name = "tsmiMeskania";
            this.tsmiMeskania.Click += new System.EventHandler(this.tsmiMeskania_Click);
            // 
            // tsmiTypyVlakov
            // 
            resources.ApplyResources(this.tsmiTypyVlakov, "tsmiTypyVlakov");
            this.tsmiTypyVlakov.Name = "tsmiTypyVlakov";
            this.tsmiTypyVlakov.Click += new System.EventHandler(this.tsmiTypyVlakov_Click);
            // 
            // tsmiAudio
            // 
            resources.ApplyResources(this.tsmiAudio, "tsmiAudio");
            this.tsmiAudio.Name = "tsmiAudio";
            this.tsmiAudio.Click += new System.EventHandler(this.tsmiAudio_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // tsmiAppSettings
            // 
            resources.ApplyResources(this.tsmiAppSettings, "tsmiAppSettings");
            this.tsmiAppSettings.Image = global::GVDEditor.Properties.Resources.app_settings;
            this.tsmiAppSettings.Name = "tsmiAppSettings";
            this.tsmiAppSettings.Click += new System.EventHandler(this.tsmiAppSettings_Click);
            // 
            // tsmiRun
            // 
            resources.ApplyResources(this.tsmiRun, "tsmiRun");
            this.tsmiRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimStartupSettings,
            this.toolStripSeparator9,
            this.tsmimStartINISS,
            this.tsmimExitINISS,
            this.tsmimRestartINISS,
            this.toolStripSeparator14});
            this.tsmiRun.Name = "tsmiRun";
            // 
            // tsmimStartupSettings
            // 
            resources.ApplyResources(this.tsmimStartupSettings, "tsmimStartupSettings");
            this.tsmimStartupSettings.Image = global::GVDEditor.Properties.Resources.wrench;
            this.tsmimStartupSettings.Name = "tsmimStartupSettings";
            this.tsmimStartupSettings.Click += new System.EventHandler(this.tsmimStartupSettings_Click);
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // tsmimStartINISS
            // 
            resources.ApplyResources(this.tsmimStartINISS, "tsmimStartINISS");
            this.tsmimStartINISS.Image = global::GVDEditor.Properties.Resources.start;
            this.tsmimStartINISS.Name = "tsmimStartINISS";
            this.tsmimStartINISS.Click += new System.EventHandler(this.tsmimStartINISS_Click);
            // 
            // tsmimExitINISS
            // 
            resources.ApplyResources(this.tsmimExitINISS, "tsmimExitINISS");
            this.tsmimExitINISS.Image = global::GVDEditor.Properties.Resources.stop;
            this.tsmimExitINISS.Name = "tsmimExitINISS";
            this.tsmimExitINISS.Click += new System.EventHandler(this.tsmimExitINISS_Click);
            // 
            // tsmimRestartINISS
            // 
            resources.ApplyResources(this.tsmimRestartINISS, "tsmimRestartINISS");
            this.tsmimRestartINISS.Image = global::GVDEditor.Properties.Resources.restart;
            this.tsmimRestartINISS.Name = "tsmimRestartINISS";
            this.tsmimRestartINISS.Click += new System.EventHandler(this.tsmimRestartINISS_Click);
            // 
            // toolStripSeparator14
            // 
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            // 
            // tsmiNastavenia
            // 
            resources.ApplyResources(this.tsmiNastavenia, "tsmiNastavenia");
            this.tsmiNastavenia.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInformation,
            this.tsmiChangelog});
            this.tsmiNastavenia.Name = "tsmiNastavenia";
            // 
            // tsmiInformation
            // 
            resources.ApplyResources(this.tsmiInformation, "tsmiInformation");
            this.tsmiInformation.Image = global::GVDEditor.Properties.Resources.info_app;
            this.tsmiInformation.Name = "tsmiInformation";
            this.tsmiInformation.Click += new System.EventHandler(this.tsmiInformation_Click);
            // 
            // tsmiChangelog
            // 
            resources.ApplyResources(this.tsmiChangelog, "tsmiChangelog");
            this.tsmiChangelog.Name = "tsmiChangelog";
            this.tsmiChangelog.Click += new System.EventHandler(this.tsmiChangelog_Click);
            // 
            // toolMenu
            // 
            resources.ApplyResources(this.toolMenu, "toolMenu");
            this.toolMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbOpen,
            this.tsbImport,
            this.tssbRecentDirs,
            this.tsbAnalyze,
            this.tsbAddGVD,
            this.toolStripSeparator12,
            this.tsbAddTrain,
            this.tsbCopyTrain,
            this.tsbEditTrain,
            this.tsbDeleteTrain,
            this.toolStripSeparator10,
            this.tsbStanica,
            this.tsbGlobalSettings,
            this.toolStripSeparator11,
            this.tsbAppSettings,
            this.tsbInformation,
            this.toolStripSeparator13,
            this.toolStripLabel1,
            this.tscbStanica,
            this.toolStripLabel2,
            this.tscbObdobie,
            this.toolStripSeparator7,
            this.tssbStartINISS,
            this.tsbKillINISS,
            this.tsbRestartINISS});
            this.toolMenu.Name = "toolMenu";
            // 
            // tsbSave
            // 
            resources.ApplyResources(this.tsbSave, "tsbSave");
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::GVDEditor.Properties.Resources.save;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbOpen
            // 
            resources.ApplyResources(this.tsbOpen, "tsbOpen");
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::GVDEditor.Properties.Resources.open;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbImport
            // 
            resources.ApplyResources(this.tsbImport, "tsbImport");
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmimImportData,
            this.tsmimImportGVD,
            this.tsmimImportELIS});
            this.tsbImport.Image = global::GVDEditor.Properties.Resources.import;
            this.tsbImport.Name = "tsbImport";
            // 
            // tsmimImportData
            // 
            resources.ApplyResources(this.tsmimImportData, "tsmimImportData");
            this.tsmimImportData.Name = "tsmimImportData";
            this.tsmimImportData.Click += new System.EventHandler(this.tsmimImportData_Click);
            // 
            // tsmimImportGVD
            // 
            resources.ApplyResources(this.tsmimImportGVD, "tsmimImportGVD");
            this.tsmimImportGVD.Name = "tsmimImportGVD";
            this.tsmimImportGVD.Click += new System.EventHandler(this.tsmimImportGVD_Click);
            // 
            // tsmimImportELIS
            // 
            resources.ApplyResources(this.tsmimImportELIS, "tsmimImportELIS");
            this.tsmimImportELIS.Name = "tsmimImportELIS";
            this.tsmimImportELIS.Click += new System.EventHandler(this.tsmimImportELIS_Click);
            // 
            // tssbRecentDirs
            // 
            resources.ApplyResources(this.tssbRecentDirs, "tssbRecentDirs");
            this.tssbRecentDirs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbRecentDirs.Name = "tssbRecentDirs";
            // 
            // tsbAnalyze
            // 
            resources.ApplyResources(this.tsbAnalyze, "tsbAnalyze");
            this.tsbAnalyze.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAnalyze.Image = global::GVDEditor.Properties.Resources.analyze;
            this.tsbAnalyze.Name = "tsbAnalyze";
            this.tsbAnalyze.Click += new System.EventHandler(this.tsbAnalyze_Click);
            // 
            // tsbAddGVD
            // 
            resources.ApplyResources(this.tsbAddGVD, "tsbAddGVD");
            this.tsbAddGVD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddGVD.Image = global::GVDEditor.Properties.Resources.create;
            this.tsbAddGVD.Name = "tsbAddGVD";
            this.tsbAddGVD.Click += new System.EventHandler(this.tsbAddGVD_Click);
            // 
            // toolStripSeparator12
            // 
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // tsbAddTrain
            // 
            resources.ApplyResources(this.tsbAddTrain, "tsbAddTrain");
            this.tsbAddTrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTrain.Image = global::GVDEditor.Properties.Resources.add;
            this.tsbAddTrain.Name = "tsbAddTrain";
            this.tsbAddTrain.Click += new System.EventHandler(this.tsbAddTrain_Click);
            // 
            // tsbCopyTrain
            // 
            resources.ApplyResources(this.tsbCopyTrain, "tsbCopyTrain");
            this.tsbCopyTrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopyTrain.Image = global::GVDEditor.Properties.Resources.copy;
            this.tsbCopyTrain.Name = "tsbCopyTrain";
            this.tsbCopyTrain.Click += new System.EventHandler(this.tsbCopyTrain_Click);
            // 
            // tsbEditTrain
            // 
            resources.ApplyResources(this.tsbEditTrain, "tsbEditTrain");
            this.tsbEditTrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditTrain.Image = global::GVDEditor.Properties.Resources.edit;
            this.tsbEditTrain.Name = "tsbEditTrain";
            this.tsbEditTrain.Click += new System.EventHandler(this.tsbEditTrain_Click);
            // 
            // tsbDeleteTrain
            // 
            resources.ApplyResources(this.tsbDeleteTrain, "tsbDeleteTrain");
            this.tsbDeleteTrain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteTrain.Image = global::GVDEditor.Properties.Resources.delete;
            this.tsbDeleteTrain.Name = "tsbDeleteTrain";
            this.tsbDeleteTrain.Click += new System.EventHandler(this.tsbDeleteTrain_Click);
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // tsbStanica
            // 
            resources.ApplyResources(this.tsbStanica, "tsbStanica");
            this.tsbStanica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStanica.Name = "tsbStanica";
            this.tsbStanica.Click += new System.EventHandler(this.tsbLocalSettings_Click);
            // 
            // tsbGlobalSettings
            // 
            resources.ApplyResources(this.tsbGlobalSettings, "tsbGlobalSettings");
            this.tsbGlobalSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGlobalSettings.Image = global::GVDEditor.Properties.Resources.global_settings;
            this.tsbGlobalSettings.Name = "tsbGlobalSettings";
            this.tsbGlobalSettings.Click += new System.EventHandler(this.tsbGlobalSettings_Click);
            // 
            // toolStripSeparator11
            // 
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // tsbAppSettings
            // 
            resources.ApplyResources(this.tsbAppSettings, "tsbAppSettings");
            this.tsbAppSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAppSettings.Name = "tsbAppSettings";
            this.tsbAppSettings.Click += new System.EventHandler(this.tsbAppSettings_Click);
            // 
            // tsbInformation
            // 
            resources.ApplyResources(this.tsbInformation, "tsbInformation");
            this.tsbInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInformation.Image = global::GVDEditor.Properties.Resources.info_app;
            this.tsbInformation.Name = "tsbInformation";
            this.tsbInformation.Click += new System.EventHandler(this.tsbInformation_Click);
            // 
            // toolStripSeparator13
            // 
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // tscbStanica
            // 
            resources.ApplyResources(this.tscbStanica, "tscbStanica");
            this.tscbStanica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbStanica.DropDownWidth = 200;
            this.tscbStanica.Margin = new System.Windows.Forms.Padding(0, 1, 15, 2);
            this.tscbStanica.Name = "tscbStanica";
            this.tscbStanica.SelectedIndexChanged += new System.EventHandler(this.tscbStanica_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel2.Name = "toolStripLabel2";
            // 
            // tscbObdobie
            // 
            resources.ApplyResources(this.tscbObdobie, "tscbObdobie");
            this.tscbObdobie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbObdobie.DropDownWidth = 150;
            this.tscbObdobie.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tscbObdobie.Name = "tscbObdobie";
            this.tscbObdobie.SelectedIndexChanged += new System.EventHandler(this.tscbObdobie_SelectedIndexChanged);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // tssbStartINISS
            // 
            resources.ApplyResources(this.tssbStartINISS, "tssbStartINISS");
            this.tssbStartINISS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStartupSettings,
            this.toolStripSeparator8});
            this.tssbStartINISS.Name = "tssbStartINISS";
            this.tssbStartINISS.ButtonClick += new System.EventHandler(this.tssbStartINISS_ButtonClick);
            // 
            // tsmiStartupSettings
            // 
            resources.ApplyResources(this.tsmiStartupSettings, "tsmiStartupSettings");
            this.tsmiStartupSettings.Image = global::GVDEditor.Properties.Resources.wrench;
            this.tsmiStartupSettings.Name = "tsmiStartupSettings";
            this.tsmiStartupSettings.Click += new System.EventHandler(this.tsmiStartupSettings_Click);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // tsbKillINISS
            // 
            resources.ApplyResources(this.tsbKillINISS, "tsbKillINISS");
            this.tsbKillINISS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbKillINISS.Image = global::GVDEditor.Properties.Resources.stop;
            this.tsbKillINISS.Name = "tsbKillINISS";
            this.tsbKillINISS.Click += new System.EventHandler(this.tsbKillINISS_Click);
            // 
            // tsbRestartINISS
            // 
            resources.ApplyResources(this.tsbRestartINISS, "tsbRestartINISS");
            this.tsbRestartINISS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRestartINISS.Image = global::GVDEditor.Properties.Resources.restart;
            this.tsbRestartINISS.Name = "tsbRestartINISS";
            this.tsbRestartINISS.Click += new System.EventHandler(this.tsbRestartINISS_Click);
            // 
            // timerTimeAndDate
            // 
            this.timerTimeAndDate.Enabled = true;
            this.timerTimeAndDate.Interval = 1000;
            this.timerTimeAndDate.Tick += new System.EventHandler(this.timerTimeAndDate_Tick);
            // 
            // dgvTrains
            // 
            resources.ApplyResources(this.dgvTrains, "dgvTrains");
            this.dgvTrains.AllowUserToAddRows = false;
            this.dgvTrains.AllowUserToDeleteRows = false;
            this.dgvTrains.AllowUserToOrderColumns = true;
            this.dgvTrains.AllowUserToResizeRows = false;
            this.dgvTrains.AutoGenerateColumns = false;
            this.dgvTrains.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTrains.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrains.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTrains.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cisloDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.typDataGridViewTextBoxColumn,
            this.LinkaPrichod,
            this.smerovanieDataGridViewTextBoxColumn,
            this.prichodDataGridViewTextBoxColumn,
            this.odchodDataGridViewTextBoxColumn,
            this.dgvcVychodziaStanica,
            this.dgvcKonecnaStanica,
            this.DatumoveObmedzenieText,
            this.LinkaOdchod,
            this.Kolaj,
            this.Dopravca,
            this.Ostatne});
            this.dgvTrains.DataSource = this.vlakBindingSource;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTrains.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvTrains.Name = "dgvTrains";
            this.dgvTrains.RowTemplate.Height = 24;
            this.dgvTrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrains.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrains_CellClick);
            this.dgvTrains.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTrains_CellFormatting);
            this.dgvTrains.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTrains_CellValidating);
            this.dgvTrains.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrains_CellValueChanged);
            this.dgvTrains.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvTrains_DataError);
            this.dgvTrains.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvTrains_RowsAdded);
            this.dgvTrains.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvTrains_RowsRemoved);
            this.dgvTrains.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTrains_KeyDown);
            // 
            // cisloDataGridViewTextBoxColumn
            // 
            this.cisloDataGridViewTextBoxColumn.DataPropertyName = "NumberVariant";
            resources.ApplyResources(this.cisloDataGridViewTextBoxColumn, "cisloDataGridViewTextBoxColumn");
            this.cisloDataGridViewTextBoxColumn.Name = "cisloDataGridViewTextBoxColumn";
            this.cisloDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typDataGridViewTextBoxColumn
            // 
            this.typDataGridViewTextBoxColumn.DataPropertyName = "Type";
            resources.ApplyResources(this.typDataGridViewTextBoxColumn, "typDataGridViewTextBoxColumn");
            this.typDataGridViewTextBoxColumn.Name = "typDataGridViewTextBoxColumn";
            this.typDataGridViewTextBoxColumn.ReadOnly = true;
            this.typDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LinkaPrichod
            // 
            this.LinkaPrichod.DataPropertyName = "LineArrival";
            resources.ApplyResources(this.LinkaPrichod, "LinkaPrichod");
            this.LinkaPrichod.Name = "LinkaPrichod";
            // 
            // smerovanieDataGridViewTextBoxColumn
            // 
            this.smerovanieDataGridViewTextBoxColumn.DataPropertyName = "RoutingImage";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.smerovanieDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.smerovanieDataGridViewTextBoxColumn, "smerovanieDataGridViewTextBoxColumn");
            this.smerovanieDataGridViewTextBoxColumn.Name = "smerovanieDataGridViewTextBoxColumn";
            this.smerovanieDataGridViewTextBoxColumn.ReadOnly = true;
            this.smerovanieDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // prichodDataGridViewTextBoxColumn
            // 
            this.prichodDataGridViewTextBoxColumn.DataPropertyName = "Arrival";
            dataGridViewCellStyle8.Format = "HH:mm";
            this.prichodDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.prichodDataGridViewTextBoxColumn, "prichodDataGridViewTextBoxColumn");
            this.prichodDataGridViewTextBoxColumn.Name = "prichodDataGridViewTextBoxColumn";
            // 
            // odchodDataGridViewTextBoxColumn
            // 
            this.odchodDataGridViewTextBoxColumn.DataPropertyName = "Departure";
            dataGridViewCellStyle9.Format = "HH:mm";
            this.odchodDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.odchodDataGridViewTextBoxColumn, "odchodDataGridViewTextBoxColumn");
            this.odchodDataGridViewTextBoxColumn.Name = "odchodDataGridViewTextBoxColumn";
            // 
            // dgvcVychodziaStanica
            // 
            this.dgvcVychodziaStanica.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dgvcVychodziaStanica, "dgvcVychodziaStanica");
            this.dgvcVychodziaStanica.Name = "dgvcVychodziaStanica";
            this.dgvcVychodziaStanica.ReadOnly = true;
            // 
            // dgvcKonecnaStanica
            // 
            this.dgvcKonecnaStanica.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dgvcKonecnaStanica, "dgvcKonecnaStanica");
            this.dgvcKonecnaStanica.Name = "dgvcKonecnaStanica";
            this.dgvcKonecnaStanica.ReadOnly = true;
            // 
            // DatumoveObmedzenieText
            // 
            this.DatumoveObmedzenieText.DataPropertyName = "DateremText";
            resources.ApplyResources(this.DatumoveObmedzenieText, "DatumoveObmedzenieText");
            this.DatumoveObmedzenieText.Name = "DatumoveObmedzenieText";
            // 
            // LinkaOdchod
            // 
            this.LinkaOdchod.DataPropertyName = "LinkaDeparture";
            resources.ApplyResources(this.LinkaOdchod, "LinkaOdchod");
            this.LinkaOdchod.Name = "LinkaOdchod";
            // 
            // Kolaj
            // 
            this.Kolaj.DataPropertyName = "Track";
            this.Kolaj.DataSource = this.trackBindingSource;
            this.Kolaj.DisplayMember = "Key";
            this.Kolaj.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.Kolaj, "Kolaj");
            this.Kolaj.Name = "Kolaj";
            exComboBoxStyle73.ArrowColor = null;
            exComboBoxStyle73.BackColor = null;
            exComboBoxStyle73.BorderColor = null;
            exComboBoxStyle73.ButtonBackColor = null;
            exComboBoxStyle73.ButtonBorderColor = null;
            exComboBoxStyle73.ButtonRenderFirst = null;
            exComboBoxStyle73.ForeColor = null;
            this.Kolaj.StyleDisabled = exComboBoxStyle73;
            exComboBoxStyle74.ArrowColor = null;
            exComboBoxStyle74.BackColor = null;
            exComboBoxStyle74.BorderColor = null;
            exComboBoxStyle74.ButtonBackColor = null;
            exComboBoxStyle74.ButtonBorderColor = null;
            exComboBoxStyle74.ButtonRenderFirst = null;
            exComboBoxStyle74.ForeColor = null;
            this.Kolaj.StyleHighlight = exComboBoxStyle74;
            exComboBoxStyle75.ArrowColor = null;
            exComboBoxStyle75.BackColor = null;
            exComboBoxStyle75.BorderColor = null;
            exComboBoxStyle75.ButtonBackColor = null;
            exComboBoxStyle75.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle75.ButtonRenderFirst = true;
            exComboBoxStyle75.ForeColor = null;
            this.Kolaj.StyleNormal = exComboBoxStyle75;
            exComboBoxStyle76.ArrowColor = null;
            exComboBoxStyle76.BackColor = null;
            exComboBoxStyle76.BorderColor = null;
            exComboBoxStyle76.ButtonBackColor = null;
            exComboBoxStyle76.ButtonBorderColor = null;
            exComboBoxStyle76.ButtonRenderFirst = null;
            exComboBoxStyle76.ForeColor = null;
            this.Kolaj.StyleSelected = exComboBoxStyle76;
            this.Kolaj.ValueMember = "This";
            // 
            // trackBindingSource
            // 
            this.trackBindingSource.DataSource = typeof(GVDEditor.Entities.Track);
            // 
            // Dopravca
            // 
            this.Dopravca.DataPropertyName = "Operator";
            this.Dopravca.DataSource = this.operatorBindingSource;
            this.Dopravca.DisplayMember = "Name";
            this.Dopravca.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.Dopravca, "Dopravca");
            this.Dopravca.Name = "Dopravca";
            exComboBoxStyle77.ArrowColor = null;
            exComboBoxStyle77.BackColor = null;
            exComboBoxStyle77.BorderColor = null;
            exComboBoxStyle77.ButtonBackColor = null;
            exComboBoxStyle77.ButtonBorderColor = null;
            exComboBoxStyle77.ButtonRenderFirst = null;
            exComboBoxStyle77.ForeColor = null;
            this.Dopravca.StyleDisabled = exComboBoxStyle77;
            exComboBoxStyle78.ArrowColor = null;
            exComboBoxStyle78.BackColor = null;
            exComboBoxStyle78.BorderColor = null;
            exComboBoxStyle78.ButtonBackColor = null;
            exComboBoxStyle78.ButtonBorderColor = null;
            exComboBoxStyle78.ButtonRenderFirst = null;
            exComboBoxStyle78.ForeColor = null;
            this.Dopravca.StyleHighlight = exComboBoxStyle78;
            exComboBoxStyle79.ArrowColor = null;
            exComboBoxStyle79.BackColor = null;
            exComboBoxStyle79.BorderColor = null;
            exComboBoxStyle79.ButtonBackColor = null;
            exComboBoxStyle79.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle79.ButtonRenderFirst = true;
            exComboBoxStyle79.ForeColor = null;
            this.Dopravca.StyleNormal = exComboBoxStyle79;
            exComboBoxStyle80.ArrowColor = null;
            exComboBoxStyle80.BackColor = null;
            exComboBoxStyle80.BorderColor = null;
            exComboBoxStyle80.ButtonBackColor = null;
            exComboBoxStyle80.ButtonBorderColor = null;
            exComboBoxStyle80.ButtonRenderFirst = null;
            exComboBoxStyle80.ForeColor = null;
            this.Dopravca.StyleSelected = exComboBoxStyle80;
            this.Dopravca.ValueMember = "This";
            // 
            // operatorBindingSource
            // 
            this.operatorBindingSource.DataSource = typeof(GVDEditor.Entities.Operator);
            // 
            // Ostatne
            // 
            this.Ostatne.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Ostatne, "Ostatne");
            this.Ostatne.Name = "Ostatne";
            this.Ostatne.Text = global::GVDEditor.Properties.Resources.REdit;
            this.Ostatne.UseColumnTextForButtonValue = true;
            // 
            // vlakBindingSource
            // 
            this.vlakBindingSource.DataSource = typeof(GVDEditor.Entities.Train);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "VychodziaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "KonecnaStanica";
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dOpenELIS
            // 
            resources.ApplyResources(this.dOpenELIS, "dOpenELIS");
            // 
            // bWorkerELIS
            // 
            this.bWorkerELIS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorkerELIS_DoWork);
            this.bWorkerELIS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorkerELIS_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDate,
            this.tsslTime,
            this.toolStripStatusLabel1,
            this.tsslTrainCountWithVariants,
            this.tsslTrainCount,
            this.tsslSelTrainName,
            this.tsslSelTrainVariants});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            // 
            // tsslDate
            // 
            resources.ApplyResources(this.tsslDate, "tsslDate");
            this.tsslDate.Margin = new System.Windows.Forms.Padding(0, 4, 10, 2);
            this.tsslDate.Name = "tsslDate";
            // 
            // tsslTime
            // 
            resources.ApplyResources(this.tsslTime, "tsslTime");
            this.tsslTime.Name = "tsslTime";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tsslTrainCountWithVariants
            // 
            resources.ApplyResources(this.tsslTrainCountWithVariants, "tsslTrainCountWithVariants");
            this.tsslTrainCountWithVariants.Image = global::GVDEditor.Properties.Resources.icon;
            this.tsslTrainCountWithVariants.Name = "tsslTrainCountWithVariants";
            // 
            // tsslTrainCount
            // 
            resources.ApplyResources(this.tsslTrainCount, "tsslTrainCount");
            this.tsslTrainCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslTrainCount.Name = "tsslTrainCount";
            // 
            // tsslSelTrainName
            // 
            resources.ApplyResources(this.tsslSelTrainName, "tsslSelTrainName");
            this.tsslSelTrainName.Name = "tsslSelTrainName";
            // 
            // tsslSelTrainVariants
            // 
            resources.ApplyResources(this.tsslSelTrainVariants, "tsslSelTrainVariants");
            this.tsslSelTrainVariants.Name = "tsslSelTrainVariants";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn1
            // 
            this.dataGridViewExComboBoxColumn1.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn1.DataSource = this.trackBindingSource;
            this.dataGridViewExComboBoxColumn1.DisplayMember = "Key";
            this.dataGridViewExComboBoxColumn1.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn1, "dataGridViewExComboBoxColumn1");
            this.dataGridViewExComboBoxColumn1.Name = "dataGridViewExComboBoxColumn1";
            exComboBoxStyle81.ArrowColor = null;
            exComboBoxStyle81.BackColor = null;
            exComboBoxStyle81.BorderColor = null;
            exComboBoxStyle81.ButtonBackColor = null;
            exComboBoxStyle81.ButtonBorderColor = null;
            exComboBoxStyle81.ButtonRenderFirst = null;
            exComboBoxStyle81.ForeColor = null;
            this.dataGridViewExComboBoxColumn1.StyleDisabled = exComboBoxStyle81;
            exComboBoxStyle82.ArrowColor = null;
            exComboBoxStyle82.BackColor = null;
            exComboBoxStyle82.BorderColor = null;
            exComboBoxStyle82.ButtonBackColor = null;
            exComboBoxStyle82.ButtonBorderColor = null;
            exComboBoxStyle82.ButtonRenderFirst = null;
            exComboBoxStyle82.ForeColor = null;
            this.dataGridViewExComboBoxColumn1.StyleHighlight = exComboBoxStyle82;
            exComboBoxStyle83.ArrowColor = null;
            exComboBoxStyle83.BackColor = null;
            exComboBoxStyle83.BorderColor = null;
            exComboBoxStyle83.ButtonBackColor = null;
            exComboBoxStyle83.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle83.ButtonRenderFirst = true;
            exComboBoxStyle83.ForeColor = null;
            this.dataGridViewExComboBoxColumn1.StyleNormal = exComboBoxStyle83;
            exComboBoxStyle84.ArrowColor = null;
            exComboBoxStyle84.BackColor = null;
            exComboBoxStyle84.BorderColor = null;
            exComboBoxStyle84.ButtonBackColor = null;
            exComboBoxStyle84.ButtonBorderColor = null;
            exComboBoxStyle84.ButtonRenderFirst = null;
            exComboBoxStyle84.ForeColor = null;
            this.dataGridViewExComboBoxColumn1.StyleSelected = exComboBoxStyle84;
            this.dataGridViewExComboBoxColumn1.ValueMember = "This";
            // 
            // dataGridViewExComboBoxColumn2
            // 
            this.dataGridViewExComboBoxColumn2.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn2.DataSource = this.operatorBindingSource;
            this.dataGridViewExComboBoxColumn2.DisplayMember = "Name";
            this.dataGridViewExComboBoxColumn2.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn2, "dataGridViewExComboBoxColumn2");
            this.dataGridViewExComboBoxColumn2.Name = "dataGridViewExComboBoxColumn2";
            exComboBoxStyle85.ArrowColor = null;
            exComboBoxStyle85.BackColor = null;
            exComboBoxStyle85.BorderColor = null;
            exComboBoxStyle85.ButtonBackColor = null;
            exComboBoxStyle85.ButtonBorderColor = null;
            exComboBoxStyle85.ButtonRenderFirst = null;
            exComboBoxStyle85.ForeColor = null;
            this.dataGridViewExComboBoxColumn2.StyleDisabled = exComboBoxStyle85;
            exComboBoxStyle86.ArrowColor = null;
            exComboBoxStyle86.BackColor = null;
            exComboBoxStyle86.BorderColor = null;
            exComboBoxStyle86.ButtonBackColor = null;
            exComboBoxStyle86.ButtonBorderColor = null;
            exComboBoxStyle86.ButtonRenderFirst = null;
            exComboBoxStyle86.ForeColor = null;
            this.dataGridViewExComboBoxColumn2.StyleHighlight = exComboBoxStyle86;
            exComboBoxStyle87.ArrowColor = null;
            exComboBoxStyle87.BackColor = null;
            exComboBoxStyle87.BorderColor = null;
            exComboBoxStyle87.ButtonBackColor = null;
            exComboBoxStyle87.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle87.ButtonRenderFirst = true;
            exComboBoxStyle87.ForeColor = null;
            this.dataGridViewExComboBoxColumn2.StyleNormal = exComboBoxStyle87;
            exComboBoxStyle88.ArrowColor = null;
            exComboBoxStyle88.BackColor = null;
            exComboBoxStyle88.BorderColor = null;
            exComboBoxStyle88.ButtonBackColor = null;
            exComboBoxStyle88.ButtonBorderColor = null;
            exComboBoxStyle88.ButtonRenderFirst = null;
            exComboBoxStyle88.ForeColor = null;
            this.dataGridViewExComboBoxColumn2.StyleSelected = exComboBoxStyle88;
            this.dataGridViewExComboBoxColumn2.ValueMember = "This";
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "Kolaj";
            resources.ApplyResources(this.dataGridViewComboBoxColumn1, "dataGridViewComboBoxColumn1");
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DataPropertyName = "Dopravca";
            resources.ApplyResources(this.dataGridViewComboBoxColumn2, "dataGridViewComboBoxColumn2");
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn3.DataPropertyName = "Kolaj";
            resources.ApplyResources(this.dataGridViewComboBoxColumn3, "dataGridViewComboBoxColumn3");
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn4.DataPropertyName = "Dopravca";
            resources.ApplyResources(this.dataGridViewComboBoxColumn4, "dataGridViewComboBoxColumn4");
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.DataPropertyName = "Kolaj";
            resources.ApplyResources(this.dataGridViewComboBoxColumn5, "dataGridViewComboBoxColumn5");
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            // 
            // dataGridViewComboBoxColumn6
            // 
            this.dataGridViewComboBoxColumn6.DataPropertyName = "Dopravca";
            resources.ApplyResources(this.dataGridViewComboBoxColumn6, "dataGridViewComboBoxColumn6");
            this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
            // 
            // dataGridViewComboBoxColumn7
            // 
            this.dataGridViewComboBoxColumn7.DataPropertyName = "Kolaj";
            resources.ApplyResources(this.dataGridViewComboBoxColumn7, "dataGridViewComboBoxColumn7");
            this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
            // 
            // dataGridViewComboBoxColumn8
            // 
            this.dataGridViewComboBoxColumn8.DataPropertyName = "Dopravca";
            resources.ApplyResources(this.dataGridViewComboBoxColumn8, "dataGridViewComboBoxColumn8");
            this.dataGridViewComboBoxColumn8.Name = "dataGridViewComboBoxColumn8";
            // 
            // dataGridViewComboBoxColumn9
            // 
            this.dataGridViewComboBoxColumn9.DataPropertyName = "Kolaj";
            resources.ApplyResources(this.dataGridViewComboBoxColumn9, "dataGridViewComboBoxColumn9");
            this.dataGridViewComboBoxColumn9.Name = "dataGridViewComboBoxColumn9";
            // 
            // dataGridViewComboBoxColumn10
            // 
            this.dataGridViewComboBoxColumn10.DataPropertyName = "Dopravca";
            resources.ApplyResources(this.dataGridViewComboBoxColumn10, "dataGridViewComboBoxColumn10");
            this.dataGridViewComboBoxColumn10.Name = "dataGridViewComboBoxColumn10";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn3
            // 
            this.dataGridViewExComboBoxColumn3.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn3.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn3, "dataGridViewExComboBoxColumn3");
            this.dataGridViewExComboBoxColumn3.Name = "dataGridViewExComboBoxColumn3";
            exComboBoxStyle89.ArrowColor = null;
            exComboBoxStyle89.BackColor = null;
            exComboBoxStyle89.BorderColor = null;
            exComboBoxStyle89.ButtonBackColor = null;
            exComboBoxStyle89.ButtonBorderColor = null;
            exComboBoxStyle89.ButtonRenderFirst = null;
            exComboBoxStyle89.ForeColor = null;
            this.dataGridViewExComboBoxColumn3.StyleDisabled = exComboBoxStyle89;
            exComboBoxStyle90.ArrowColor = null;
            exComboBoxStyle90.BackColor = null;
            exComboBoxStyle90.BorderColor = null;
            exComboBoxStyle90.ButtonBackColor = null;
            exComboBoxStyle90.ButtonBorderColor = null;
            exComboBoxStyle90.ButtonRenderFirst = null;
            exComboBoxStyle90.ForeColor = null;
            this.dataGridViewExComboBoxColumn3.StyleHighlight = exComboBoxStyle90;
            exComboBoxStyle91.ArrowColor = null;
            exComboBoxStyle91.BackColor = null;
            exComboBoxStyle91.BorderColor = null;
            exComboBoxStyle91.ButtonBackColor = null;
            exComboBoxStyle91.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle91.ButtonRenderFirst = true;
            exComboBoxStyle91.ForeColor = null;
            this.dataGridViewExComboBoxColumn3.StyleNormal = exComboBoxStyle91;
            exComboBoxStyle92.ArrowColor = null;
            exComboBoxStyle92.BackColor = null;
            exComboBoxStyle92.BorderColor = null;
            exComboBoxStyle92.ButtonBackColor = null;
            exComboBoxStyle92.ButtonBorderColor = null;
            exComboBoxStyle92.ButtonRenderFirst = null;
            exComboBoxStyle92.ForeColor = null;
            this.dataGridViewExComboBoxColumn3.StyleSelected = exComboBoxStyle92;
            // 
            // dataGridViewExComboBoxColumn4
            // 
            this.dataGridViewExComboBoxColumn4.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn4.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn4, "dataGridViewExComboBoxColumn4");
            this.dataGridViewExComboBoxColumn4.Name = "dataGridViewExComboBoxColumn4";
            exComboBoxStyle93.ArrowColor = null;
            exComboBoxStyle93.BackColor = null;
            exComboBoxStyle93.BorderColor = null;
            exComboBoxStyle93.ButtonBackColor = null;
            exComboBoxStyle93.ButtonBorderColor = null;
            exComboBoxStyle93.ButtonRenderFirst = null;
            exComboBoxStyle93.ForeColor = null;
            this.dataGridViewExComboBoxColumn4.StyleDisabled = exComboBoxStyle93;
            exComboBoxStyle94.ArrowColor = null;
            exComboBoxStyle94.BackColor = null;
            exComboBoxStyle94.BorderColor = null;
            exComboBoxStyle94.ButtonBackColor = null;
            exComboBoxStyle94.ButtonBorderColor = null;
            exComboBoxStyle94.ButtonRenderFirst = null;
            exComboBoxStyle94.ForeColor = null;
            this.dataGridViewExComboBoxColumn4.StyleHighlight = exComboBoxStyle94;
            exComboBoxStyle95.ArrowColor = null;
            exComboBoxStyle95.BackColor = null;
            exComboBoxStyle95.BorderColor = null;
            exComboBoxStyle95.ButtonBackColor = null;
            exComboBoxStyle95.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle95.ButtonRenderFirst = true;
            exComboBoxStyle95.ForeColor = null;
            this.dataGridViewExComboBoxColumn4.StyleNormal = exComboBoxStyle95;
            exComboBoxStyle96.ArrowColor = null;
            exComboBoxStyle96.BackColor = null;
            exComboBoxStyle96.BorderColor = null;
            exComboBoxStyle96.ButtonBackColor = null;
            exComboBoxStyle96.ButtonBorderColor = null;
            exComboBoxStyle96.ButtonRenderFirst = null;
            exComboBoxStyle96.ForeColor = null;
            this.dataGridViewExComboBoxColumn4.StyleSelected = exComboBoxStyle96;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn17, "dataGridViewTextBoxColumn17");
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn18, "dataGridViewTextBoxColumn18");
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn5
            // 
            this.dataGridViewExComboBoxColumn5.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn5.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn5, "dataGridViewExComboBoxColumn5");
            this.dataGridViewExComboBoxColumn5.Name = "dataGridViewExComboBoxColumn5";
            exComboBoxStyle97.ArrowColor = null;
            exComboBoxStyle97.BackColor = null;
            exComboBoxStyle97.BorderColor = null;
            exComboBoxStyle97.ButtonBackColor = null;
            exComboBoxStyle97.ButtonBorderColor = null;
            exComboBoxStyle97.ButtonRenderFirst = null;
            exComboBoxStyle97.ForeColor = null;
            this.dataGridViewExComboBoxColumn5.StyleDisabled = exComboBoxStyle97;
            exComboBoxStyle98.ArrowColor = null;
            exComboBoxStyle98.BackColor = null;
            exComboBoxStyle98.BorderColor = null;
            exComboBoxStyle98.ButtonBackColor = null;
            exComboBoxStyle98.ButtonBorderColor = null;
            exComboBoxStyle98.ButtonRenderFirst = null;
            exComboBoxStyle98.ForeColor = null;
            this.dataGridViewExComboBoxColumn5.StyleHighlight = exComboBoxStyle98;
            exComboBoxStyle99.ArrowColor = null;
            exComboBoxStyle99.BackColor = null;
            exComboBoxStyle99.BorderColor = null;
            exComboBoxStyle99.ButtonBackColor = null;
            exComboBoxStyle99.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle99.ButtonRenderFirst = true;
            exComboBoxStyle99.ForeColor = null;
            this.dataGridViewExComboBoxColumn5.StyleNormal = exComboBoxStyle99;
            exComboBoxStyle100.ArrowColor = null;
            exComboBoxStyle100.BackColor = null;
            exComboBoxStyle100.BorderColor = null;
            exComboBoxStyle100.ButtonBackColor = null;
            exComboBoxStyle100.ButtonBorderColor = null;
            exComboBoxStyle100.ButtonRenderFirst = null;
            exComboBoxStyle100.ForeColor = null;
            this.dataGridViewExComboBoxColumn5.StyleSelected = exComboBoxStyle100;
            // 
            // dataGridViewExComboBoxColumn6
            // 
            this.dataGridViewExComboBoxColumn6.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn6.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn6, "dataGridViewExComboBoxColumn6");
            this.dataGridViewExComboBoxColumn6.Name = "dataGridViewExComboBoxColumn6";
            exComboBoxStyle101.ArrowColor = null;
            exComboBoxStyle101.BackColor = null;
            exComboBoxStyle101.BorderColor = null;
            exComboBoxStyle101.ButtonBackColor = null;
            exComboBoxStyle101.ButtonBorderColor = null;
            exComboBoxStyle101.ButtonRenderFirst = null;
            exComboBoxStyle101.ForeColor = null;
            this.dataGridViewExComboBoxColumn6.StyleDisabled = exComboBoxStyle101;
            exComboBoxStyle102.ArrowColor = null;
            exComboBoxStyle102.BackColor = null;
            exComboBoxStyle102.BorderColor = null;
            exComboBoxStyle102.ButtonBackColor = null;
            exComboBoxStyle102.ButtonBorderColor = null;
            exComboBoxStyle102.ButtonRenderFirst = null;
            exComboBoxStyle102.ForeColor = null;
            this.dataGridViewExComboBoxColumn6.StyleHighlight = exComboBoxStyle102;
            exComboBoxStyle103.ArrowColor = null;
            exComboBoxStyle103.BackColor = null;
            exComboBoxStyle103.BorderColor = null;
            exComboBoxStyle103.ButtonBackColor = null;
            exComboBoxStyle103.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle103.ButtonRenderFirst = true;
            exComboBoxStyle103.ForeColor = null;
            this.dataGridViewExComboBoxColumn6.StyleNormal = exComboBoxStyle103;
            exComboBoxStyle104.ArrowColor = null;
            exComboBoxStyle104.BackColor = null;
            exComboBoxStyle104.BorderColor = null;
            exComboBoxStyle104.ButtonBackColor = null;
            exComboBoxStyle104.ButtonBorderColor = null;
            exComboBoxStyle104.ButtonRenderFirst = null;
            exComboBoxStyle104.ForeColor = null;
            this.dataGridViewExComboBoxColumn6.StyleSelected = exComboBoxStyle104;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn19, "dataGridViewTextBoxColumn19");
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn20, "dataGridViewTextBoxColumn20");
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn7
            // 
            this.dataGridViewExComboBoxColumn7.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn7.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn7, "dataGridViewExComboBoxColumn7");
            this.dataGridViewExComboBoxColumn7.Name = "dataGridViewExComboBoxColumn7";
            exComboBoxStyle105.ArrowColor = null;
            exComboBoxStyle105.BackColor = null;
            exComboBoxStyle105.BorderColor = null;
            exComboBoxStyle105.ButtonBackColor = null;
            exComboBoxStyle105.ButtonBorderColor = null;
            exComboBoxStyle105.ButtonRenderFirst = null;
            exComboBoxStyle105.ForeColor = null;
            this.dataGridViewExComboBoxColumn7.StyleDisabled = exComboBoxStyle105;
            exComboBoxStyle106.ArrowColor = null;
            exComboBoxStyle106.BackColor = null;
            exComboBoxStyle106.BorderColor = null;
            exComboBoxStyle106.ButtonBackColor = null;
            exComboBoxStyle106.ButtonBorderColor = null;
            exComboBoxStyle106.ButtonRenderFirst = null;
            exComboBoxStyle106.ForeColor = null;
            this.dataGridViewExComboBoxColumn7.StyleHighlight = exComboBoxStyle106;
            exComboBoxStyle107.ArrowColor = null;
            exComboBoxStyle107.BackColor = null;
            exComboBoxStyle107.BorderColor = null;
            exComboBoxStyle107.ButtonBackColor = null;
            exComboBoxStyle107.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle107.ButtonRenderFirst = true;
            exComboBoxStyle107.ForeColor = null;
            this.dataGridViewExComboBoxColumn7.StyleNormal = exComboBoxStyle107;
            exComboBoxStyle108.ArrowColor = null;
            exComboBoxStyle108.BackColor = null;
            exComboBoxStyle108.BorderColor = null;
            exComboBoxStyle108.ButtonBackColor = null;
            exComboBoxStyle108.ButtonBorderColor = null;
            exComboBoxStyle108.ButtonRenderFirst = null;
            exComboBoxStyle108.ForeColor = null;
            this.dataGridViewExComboBoxColumn7.StyleSelected = exComboBoxStyle108;
            // 
            // dataGridViewExComboBoxColumn8
            // 
            this.dataGridViewExComboBoxColumn8.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn8.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn8, "dataGridViewExComboBoxColumn8");
            this.dataGridViewExComboBoxColumn8.Name = "dataGridViewExComboBoxColumn8";
            exComboBoxStyle109.ArrowColor = null;
            exComboBoxStyle109.BackColor = null;
            exComboBoxStyle109.BorderColor = null;
            exComboBoxStyle109.ButtonBackColor = null;
            exComboBoxStyle109.ButtonBorderColor = null;
            exComboBoxStyle109.ButtonRenderFirst = null;
            exComboBoxStyle109.ForeColor = null;
            this.dataGridViewExComboBoxColumn8.StyleDisabled = exComboBoxStyle109;
            exComboBoxStyle110.ArrowColor = null;
            exComboBoxStyle110.BackColor = null;
            exComboBoxStyle110.BorderColor = null;
            exComboBoxStyle110.ButtonBackColor = null;
            exComboBoxStyle110.ButtonBorderColor = null;
            exComboBoxStyle110.ButtonRenderFirst = null;
            exComboBoxStyle110.ForeColor = null;
            this.dataGridViewExComboBoxColumn8.StyleHighlight = exComboBoxStyle110;
            exComboBoxStyle111.ArrowColor = null;
            exComboBoxStyle111.BackColor = null;
            exComboBoxStyle111.BorderColor = null;
            exComboBoxStyle111.ButtonBackColor = null;
            exComboBoxStyle111.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle111.ButtonRenderFirst = true;
            exComboBoxStyle111.ForeColor = null;
            this.dataGridViewExComboBoxColumn8.StyleNormal = exComboBoxStyle111;
            exComboBoxStyle112.ArrowColor = null;
            exComboBoxStyle112.BackColor = null;
            exComboBoxStyle112.BorderColor = null;
            exComboBoxStyle112.ButtonBackColor = null;
            exComboBoxStyle112.ButtonBorderColor = null;
            exComboBoxStyle112.ButtonRenderFirst = null;
            exComboBoxStyle112.ForeColor = null;
            this.dataGridViewExComboBoxColumn8.StyleSelected = exComboBoxStyle112;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn21, "dataGridViewTextBoxColumn21");
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn22, "dataGridViewTextBoxColumn22");
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn9
            // 
            this.dataGridViewExComboBoxColumn9.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn9.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn9, "dataGridViewExComboBoxColumn9");
            this.dataGridViewExComboBoxColumn9.Name = "dataGridViewExComboBoxColumn9";
            exComboBoxStyle113.ArrowColor = null;
            exComboBoxStyle113.BackColor = null;
            exComboBoxStyle113.BorderColor = null;
            exComboBoxStyle113.ButtonBackColor = null;
            exComboBoxStyle113.ButtonBorderColor = null;
            exComboBoxStyle113.ButtonRenderFirst = null;
            exComboBoxStyle113.ForeColor = null;
            this.dataGridViewExComboBoxColumn9.StyleDisabled = exComboBoxStyle113;
            exComboBoxStyle114.ArrowColor = null;
            exComboBoxStyle114.BackColor = null;
            exComboBoxStyle114.BorderColor = null;
            exComboBoxStyle114.ButtonBackColor = null;
            exComboBoxStyle114.ButtonBorderColor = null;
            exComboBoxStyle114.ButtonRenderFirst = null;
            exComboBoxStyle114.ForeColor = null;
            this.dataGridViewExComboBoxColumn9.StyleHighlight = exComboBoxStyle114;
            exComboBoxStyle115.ArrowColor = null;
            exComboBoxStyle115.BackColor = null;
            exComboBoxStyle115.BorderColor = null;
            exComboBoxStyle115.ButtonBackColor = null;
            exComboBoxStyle115.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle115.ButtonRenderFirst = true;
            exComboBoxStyle115.ForeColor = null;
            this.dataGridViewExComboBoxColumn9.StyleNormal = exComboBoxStyle115;
            exComboBoxStyle116.ArrowColor = null;
            exComboBoxStyle116.BackColor = null;
            exComboBoxStyle116.BorderColor = null;
            exComboBoxStyle116.ButtonBackColor = null;
            exComboBoxStyle116.ButtonBorderColor = null;
            exComboBoxStyle116.ButtonRenderFirst = null;
            exComboBoxStyle116.ForeColor = null;
            this.dataGridViewExComboBoxColumn9.StyleSelected = exComboBoxStyle116;
            // 
            // dataGridViewExComboBoxColumn10
            // 
            this.dataGridViewExComboBoxColumn10.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn10.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn10, "dataGridViewExComboBoxColumn10");
            this.dataGridViewExComboBoxColumn10.Name = "dataGridViewExComboBoxColumn10";
            exComboBoxStyle117.ArrowColor = null;
            exComboBoxStyle117.BackColor = null;
            exComboBoxStyle117.BorderColor = null;
            exComboBoxStyle117.ButtonBackColor = null;
            exComboBoxStyle117.ButtonBorderColor = null;
            exComboBoxStyle117.ButtonRenderFirst = null;
            exComboBoxStyle117.ForeColor = null;
            this.dataGridViewExComboBoxColumn10.StyleDisabled = exComboBoxStyle117;
            exComboBoxStyle118.ArrowColor = null;
            exComboBoxStyle118.BackColor = null;
            exComboBoxStyle118.BorderColor = null;
            exComboBoxStyle118.ButtonBackColor = null;
            exComboBoxStyle118.ButtonBorderColor = null;
            exComboBoxStyle118.ButtonRenderFirst = null;
            exComboBoxStyle118.ForeColor = null;
            this.dataGridViewExComboBoxColumn10.StyleHighlight = exComboBoxStyle118;
            exComboBoxStyle119.ArrowColor = null;
            exComboBoxStyle119.BackColor = null;
            exComboBoxStyle119.BorderColor = null;
            exComboBoxStyle119.ButtonBackColor = null;
            exComboBoxStyle119.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle119.ButtonRenderFirst = true;
            exComboBoxStyle119.ForeColor = null;
            this.dataGridViewExComboBoxColumn10.StyleNormal = exComboBoxStyle119;
            exComboBoxStyle120.ArrowColor = null;
            exComboBoxStyle120.BackColor = null;
            exComboBoxStyle120.BorderColor = null;
            exComboBoxStyle120.ButtonBackColor = null;
            exComboBoxStyle120.ButtonBorderColor = null;
            exComboBoxStyle120.ButtonRenderFirst = null;
            exComboBoxStyle120.ForeColor = null;
            this.dataGridViewExComboBoxColumn10.StyleSelected = exComboBoxStyle120;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn23, "dataGridViewTextBoxColumn23");
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn24, "dataGridViewTextBoxColumn24");
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn11
            // 
            this.dataGridViewExComboBoxColumn11.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn11.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn11, "dataGridViewExComboBoxColumn11");
            this.dataGridViewExComboBoxColumn11.Name = "dataGridViewExComboBoxColumn11";
            exComboBoxStyle121.ArrowColor = null;
            exComboBoxStyle121.BackColor = null;
            exComboBoxStyle121.BorderColor = null;
            exComboBoxStyle121.ButtonBackColor = null;
            exComboBoxStyle121.ButtonBorderColor = null;
            exComboBoxStyle121.ButtonRenderFirst = null;
            exComboBoxStyle121.ForeColor = null;
            this.dataGridViewExComboBoxColumn11.StyleDisabled = exComboBoxStyle121;
            exComboBoxStyle122.ArrowColor = null;
            exComboBoxStyle122.BackColor = null;
            exComboBoxStyle122.BorderColor = null;
            exComboBoxStyle122.ButtonBackColor = null;
            exComboBoxStyle122.ButtonBorderColor = null;
            exComboBoxStyle122.ButtonRenderFirst = null;
            exComboBoxStyle122.ForeColor = null;
            this.dataGridViewExComboBoxColumn11.StyleHighlight = exComboBoxStyle122;
            exComboBoxStyle123.ArrowColor = null;
            exComboBoxStyle123.BackColor = null;
            exComboBoxStyle123.BorderColor = null;
            exComboBoxStyle123.ButtonBackColor = null;
            exComboBoxStyle123.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle123.ButtonRenderFirst = true;
            exComboBoxStyle123.ForeColor = null;
            this.dataGridViewExComboBoxColumn11.StyleNormal = exComboBoxStyle123;
            exComboBoxStyle124.ArrowColor = null;
            exComboBoxStyle124.BackColor = null;
            exComboBoxStyle124.BorderColor = null;
            exComboBoxStyle124.ButtonBackColor = null;
            exComboBoxStyle124.ButtonBorderColor = null;
            exComboBoxStyle124.ButtonRenderFirst = null;
            exComboBoxStyle124.ForeColor = null;
            this.dataGridViewExComboBoxColumn11.StyleSelected = exComboBoxStyle124;
            // 
            // dataGridViewExComboBoxColumn12
            // 
            this.dataGridViewExComboBoxColumn12.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn12.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn12, "dataGridViewExComboBoxColumn12");
            this.dataGridViewExComboBoxColumn12.Name = "dataGridViewExComboBoxColumn12";
            exComboBoxStyle125.ArrowColor = null;
            exComboBoxStyle125.BackColor = null;
            exComboBoxStyle125.BorderColor = null;
            exComboBoxStyle125.ButtonBackColor = null;
            exComboBoxStyle125.ButtonBorderColor = null;
            exComboBoxStyle125.ButtonRenderFirst = null;
            exComboBoxStyle125.ForeColor = null;
            this.dataGridViewExComboBoxColumn12.StyleDisabled = exComboBoxStyle125;
            exComboBoxStyle126.ArrowColor = null;
            exComboBoxStyle126.BackColor = null;
            exComboBoxStyle126.BorderColor = null;
            exComboBoxStyle126.ButtonBackColor = null;
            exComboBoxStyle126.ButtonBorderColor = null;
            exComboBoxStyle126.ButtonRenderFirst = null;
            exComboBoxStyle126.ForeColor = null;
            this.dataGridViewExComboBoxColumn12.StyleHighlight = exComboBoxStyle126;
            exComboBoxStyle127.ArrowColor = null;
            exComboBoxStyle127.BackColor = null;
            exComboBoxStyle127.BorderColor = null;
            exComboBoxStyle127.ButtonBackColor = null;
            exComboBoxStyle127.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle127.ButtonRenderFirst = true;
            exComboBoxStyle127.ForeColor = null;
            this.dataGridViewExComboBoxColumn12.StyleNormal = exComboBoxStyle127;
            exComboBoxStyle128.ArrowColor = null;
            exComboBoxStyle128.BackColor = null;
            exComboBoxStyle128.BorderColor = null;
            exComboBoxStyle128.ButtonBackColor = null;
            exComboBoxStyle128.ButtonBorderColor = null;
            exComboBoxStyle128.ButtonRenderFirst = null;
            exComboBoxStyle128.ForeColor = null;
            this.dataGridViewExComboBoxColumn12.StyleSelected = exComboBoxStyle128;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn25, "dataGridViewTextBoxColumn25");
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn26, "dataGridViewTextBoxColumn26");
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn13
            // 
            this.dataGridViewExComboBoxColumn13.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn13.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn13, "dataGridViewExComboBoxColumn13");
            this.dataGridViewExComboBoxColumn13.Name = "dataGridViewExComboBoxColumn13";
            exComboBoxStyle129.ArrowColor = null;
            exComboBoxStyle129.BackColor = null;
            exComboBoxStyle129.BorderColor = null;
            exComboBoxStyle129.ButtonBackColor = null;
            exComboBoxStyle129.ButtonBorderColor = null;
            exComboBoxStyle129.ButtonRenderFirst = null;
            exComboBoxStyle129.ForeColor = null;
            this.dataGridViewExComboBoxColumn13.StyleDisabled = exComboBoxStyle129;
            exComboBoxStyle130.ArrowColor = null;
            exComboBoxStyle130.BackColor = null;
            exComboBoxStyle130.BorderColor = null;
            exComboBoxStyle130.ButtonBackColor = null;
            exComboBoxStyle130.ButtonBorderColor = null;
            exComboBoxStyle130.ButtonRenderFirst = null;
            exComboBoxStyle130.ForeColor = null;
            this.dataGridViewExComboBoxColumn13.StyleHighlight = exComboBoxStyle130;
            exComboBoxStyle131.ArrowColor = null;
            exComboBoxStyle131.BackColor = null;
            exComboBoxStyle131.BorderColor = null;
            exComboBoxStyle131.ButtonBackColor = null;
            exComboBoxStyle131.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle131.ButtonRenderFirst = true;
            exComboBoxStyle131.ForeColor = null;
            this.dataGridViewExComboBoxColumn13.StyleNormal = exComboBoxStyle131;
            exComboBoxStyle132.ArrowColor = null;
            exComboBoxStyle132.BackColor = null;
            exComboBoxStyle132.BorderColor = null;
            exComboBoxStyle132.ButtonBackColor = null;
            exComboBoxStyle132.ButtonBorderColor = null;
            exComboBoxStyle132.ButtonRenderFirst = null;
            exComboBoxStyle132.ForeColor = null;
            this.dataGridViewExComboBoxColumn13.StyleSelected = exComboBoxStyle132;
            // 
            // dataGridViewExComboBoxColumn14
            // 
            this.dataGridViewExComboBoxColumn14.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn14.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn14, "dataGridViewExComboBoxColumn14");
            this.dataGridViewExComboBoxColumn14.Name = "dataGridViewExComboBoxColumn14";
            exComboBoxStyle133.ArrowColor = null;
            exComboBoxStyle133.BackColor = null;
            exComboBoxStyle133.BorderColor = null;
            exComboBoxStyle133.ButtonBackColor = null;
            exComboBoxStyle133.ButtonBorderColor = null;
            exComboBoxStyle133.ButtonRenderFirst = null;
            exComboBoxStyle133.ForeColor = null;
            this.dataGridViewExComboBoxColumn14.StyleDisabled = exComboBoxStyle133;
            exComboBoxStyle134.ArrowColor = null;
            exComboBoxStyle134.BackColor = null;
            exComboBoxStyle134.BorderColor = null;
            exComboBoxStyle134.ButtonBackColor = null;
            exComboBoxStyle134.ButtonBorderColor = null;
            exComboBoxStyle134.ButtonRenderFirst = null;
            exComboBoxStyle134.ForeColor = null;
            this.dataGridViewExComboBoxColumn14.StyleHighlight = exComboBoxStyle134;
            exComboBoxStyle135.ArrowColor = null;
            exComboBoxStyle135.BackColor = null;
            exComboBoxStyle135.BorderColor = null;
            exComboBoxStyle135.ButtonBackColor = null;
            exComboBoxStyle135.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle135.ButtonRenderFirst = true;
            exComboBoxStyle135.ForeColor = null;
            this.dataGridViewExComboBoxColumn14.StyleNormal = exComboBoxStyle135;
            exComboBoxStyle136.ArrowColor = null;
            exComboBoxStyle136.BackColor = null;
            exComboBoxStyle136.BorderColor = null;
            exComboBoxStyle136.ButtonBackColor = null;
            exComboBoxStyle136.ButtonBorderColor = null;
            exComboBoxStyle136.ButtonRenderFirst = null;
            exComboBoxStyle136.ForeColor = null;
            this.dataGridViewExComboBoxColumn14.StyleSelected = exComboBoxStyle136;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "StartingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn27, "dataGridViewTextBoxColumn27");
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "EndingStation";
            resources.ApplyResources(this.dataGridViewTextBoxColumn28, "dataGridViewTextBoxColumn28");
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            // 
            // dataGridViewExComboBoxColumn15
            // 
            this.dataGridViewExComboBoxColumn15.DataPropertyName = "Track";
            this.dataGridViewExComboBoxColumn15.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn15, "dataGridViewExComboBoxColumn15");
            this.dataGridViewExComboBoxColumn15.Name = "dataGridViewExComboBoxColumn15";
            exComboBoxStyle137.ArrowColor = null;
            exComboBoxStyle137.BackColor = null;
            exComboBoxStyle137.BorderColor = null;
            exComboBoxStyle137.ButtonBackColor = null;
            exComboBoxStyle137.ButtonBorderColor = null;
            exComboBoxStyle137.ButtonRenderFirst = null;
            exComboBoxStyle137.ForeColor = null;
            this.dataGridViewExComboBoxColumn15.StyleDisabled = exComboBoxStyle137;
            exComboBoxStyle138.ArrowColor = null;
            exComboBoxStyle138.BackColor = null;
            exComboBoxStyle138.BorderColor = null;
            exComboBoxStyle138.ButtonBackColor = null;
            exComboBoxStyle138.ButtonBorderColor = null;
            exComboBoxStyle138.ButtonRenderFirst = null;
            exComboBoxStyle138.ForeColor = null;
            this.dataGridViewExComboBoxColumn15.StyleHighlight = exComboBoxStyle138;
            exComboBoxStyle139.ArrowColor = null;
            exComboBoxStyle139.BackColor = null;
            exComboBoxStyle139.BorderColor = null;
            exComboBoxStyle139.ButtonBackColor = null;
            exComboBoxStyle139.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle139.ButtonRenderFirst = true;
            exComboBoxStyle139.ForeColor = null;
            this.dataGridViewExComboBoxColumn15.StyleNormal = exComboBoxStyle139;
            exComboBoxStyle140.ArrowColor = null;
            exComboBoxStyle140.BackColor = null;
            exComboBoxStyle140.BorderColor = null;
            exComboBoxStyle140.ButtonBackColor = null;
            exComboBoxStyle140.ButtonBorderColor = null;
            exComboBoxStyle140.ButtonRenderFirst = null;
            exComboBoxStyle140.ForeColor = null;
            this.dataGridViewExComboBoxColumn15.StyleSelected = exComboBoxStyle140;
            // 
            // dataGridViewExComboBoxColumn16
            // 
            this.dataGridViewExComboBoxColumn16.DataPropertyName = "Operator";
            this.dataGridViewExComboBoxColumn16.DropDownSelectedBackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.dataGridViewExComboBoxColumn16, "dataGridViewExComboBoxColumn16");
            this.dataGridViewExComboBoxColumn16.Name = "dataGridViewExComboBoxColumn16";
            exComboBoxStyle141.ArrowColor = null;
            exComboBoxStyle141.BackColor = null;
            exComboBoxStyle141.BorderColor = null;
            exComboBoxStyle141.ButtonBackColor = null;
            exComboBoxStyle141.ButtonBorderColor = null;
            exComboBoxStyle141.ButtonRenderFirst = null;
            exComboBoxStyle141.ForeColor = null;
            this.dataGridViewExComboBoxColumn16.StyleDisabled = exComboBoxStyle141;
            exComboBoxStyle142.ArrowColor = null;
            exComboBoxStyle142.BackColor = null;
            exComboBoxStyle142.BorderColor = null;
            exComboBoxStyle142.ButtonBackColor = null;
            exComboBoxStyle142.ButtonBorderColor = null;
            exComboBoxStyle142.ButtonRenderFirst = null;
            exComboBoxStyle142.ForeColor = null;
            this.dataGridViewExComboBoxColumn16.StyleHighlight = exComboBoxStyle142;
            exComboBoxStyle143.ArrowColor = null;
            exComboBoxStyle143.BackColor = null;
            exComboBoxStyle143.BorderColor = null;
            exComboBoxStyle143.ButtonBackColor = null;
            exComboBoxStyle143.ButtonBorderColor = System.Drawing.Color.White;
            exComboBoxStyle143.ButtonRenderFirst = true;
            exComboBoxStyle143.ForeColor = null;
            this.dataGridViewExComboBoxColumn16.StyleNormal = exComboBoxStyle143;
            exComboBoxStyle144.ArrowColor = null;
            exComboBoxStyle144.BackColor = null;
            exComboBoxStyle144.BorderColor = null;
            exComboBoxStyle144.ButtonBackColor = null;
            exComboBoxStyle144.ButtonBorderColor = null;
            exComboBoxStyle144.ButtonRenderFirst = null;
            exComboBoxStyle144.ForeColor = null;
            this.dataGridViewExComboBoxColumn16.StyleSelected = exComboBoxStyle144;
            // 
            // FMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTrains);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.hlavneMenu);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.hlavneMenu;
            this.Name = "FMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.hlavneMenu.ResumeLayout(false);
            this.hlavneMenu.PerformLayout();
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrains)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operatorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vlakBindingSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private MenuStrip hlavneMenu;
        private ToolStripMenuItem tsmiNastavenia;
        private ToolStripMenuItem tsmiNew;
        private ToolStripMenuItem tsmiOpen;
        private ToolStripMenuItem tsmiInformation;
        private ToolStripMenuItem tsmiStanica;
        private ToolStrip toolMenu;
        private ToolStripButton tsbAddGVD;
        private ToolStripMenuItem tsmiSubor;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem tsmiSave;
        private ToolStripMenuItem tsmiUpravit;
        private ToolStripMenuItem tsmiDuplikovat;
        private ToolStripMenuItem tsmiVlastnostiStanice;

        #endregion
        private ToolStripButton tsbSave;
        private ToolStripButton tsbOpen;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton tsbGlobalSettings;
        private ToolStripButton tsbStanica;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton tsbInformation;
        private ToolStripSeparator toolStripSeparator12;
        private Timer timerTimeAndDate;
        private ToolStripExComboBox tscbStanica;
        private ToolStripExComboBox tscbObdobie;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripMenuItem tsmiRecent;
        private BackgroundWorker backgroundWorker1;
        private ToolStripMenuItem tsmiGlobalSettings;
        private DataGridView dgvTrains;
        private ToolStripMenuItem tsmimAddTrain;
        private ToolStripMenuItem tsmimEditTrain;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiDeleteTrain;
        private ToolStripButton tsbAddTrain;
        private ToolStripButton tsbEditTrain;
        private ToolStripButton tsbDeleteTrain;
        private ToolStripButton tsbCopyTrain;
        private ToolStripButton tsbAppSettings;
        private BindingSource vlakBindingSource;
        private BindingSource trackBindingSource;
        private BindingSource operatorBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private ToolStripSplitButton tssbRecentDirs;
        private ToolStripMenuItem tsmiChangelog;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn9;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn10;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem tsmiImport;
        private ToolStripSplitButton tsbImport;
        private ToolStripMenuItem tsmiAppSettings;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem tsmiImportData;
        private ToolStripMenuItem tsmiImportGVD;
        private ToolStripMenuItem tsmimImportData;
        private ToolStripMenuItem tsmimImportGVD;
        private ToolStripMenuItem tsmiImportELIS;
        private ToolStripMenuItem tsmimImportELIS;
        private OpenFileDialog dOpenELIS;
        private BackgroundWorker bWorkerELIS;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsslDate;
        private ToolStripStatusLabel tsslTime;
        private ToolStripStatusLabel tsslTrainCountWithVariants;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tsslTrainCount;
        private ToolStripStatusLabel tsslSelTrainName;
        private ToolStripStatusLabel tsslSelTrainVariants;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSplitButton tssbStartINISS;
        private ToolStripMenuItem tsmiStartupSettings;
        private ToolStripSeparator toolStripSeparator8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn1;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn2;
        private ToolStripButton tsbKillINISS;
        private ToolStripMenuItem tsmiRun;
        private ToolStripMenuItem tsmimStartupSettings;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem tsmimStartINISS;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem tsmimExitINISS;
        private ToolStripMenuItem tsmiGrafikon;
        private ToolStripMenuItem tsmiDopravcovia;
        private ToolStripMenuItem tsmiPlatforms;
        private ToolStripMenuItem tsmiKolaje;
        private ToolStripMenuItem tsmiTPhysical;
        private ToolStripMenuItem tsmiTLogical;
        private ToolStripMenuItem tsmiStanice;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripMenuItem tsmiTCatalog;
        private ToolStripMenuItem tsmiTabTab;
        private ToolStripMenuItem tsmiTTexts;
        private ToolStripMenuItem tsmiTFonts;
        private ToolStripMenuItem tsmiGrafikony;
        private ToolStripMenuItem tsmiLanguages;
        private ToolStripMenuItem tsmiMeskania;
        private ToolStripMenuItem tsmiTypyVlakov;
        private ToolStripMenuItem tsmiAudio;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn3;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn5;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn6;
        private ToolStripButton tsbRestartINISS;
        private ToolStripMenuItem tsmimRestartINISS;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn7;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn9;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn11;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn13;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn15;
        private DataGridViewExComboBoxColumn dataGridViewExComboBoxColumn16;
        private DataGridViewTextBoxColumn cisloDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn typDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn LinkaPrichod;
        private DataGridViewImageColumn smerovanieDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn prichodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn odchodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dgvcVychodziaStanica;
        private DataGridViewTextBoxColumn dgvcKonecnaStanica;
        private DataGridViewTextBoxColumn DatumoveObmedzenieText;
        private DataGridViewTextBoxColumn LinkaOdchod;
        private DataGridViewExComboBoxColumn Kolaj;
        private DataGridViewExComboBoxColumn Dopravca;
        private DataGridViewButtonColumn Ostatne;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem tsmiTabTabEditor;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripMenuItem tsmiAnalyze;
        private ToolStripButton tsbAnalyze;
    }
}

