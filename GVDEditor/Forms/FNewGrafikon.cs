﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Dialog - Novy grafikon
    /// </summary>
    public partial class FNewGrafikon : Form
    {
        /// <summary>
        ///     Novy grafikon
        /// </summary>
        public GVDInfo GvdInfo;

        /// <summary>
        ///     Novy priecinok s grafikonom
        /// </summary>
        public DirList NewDir;

        private Color vybrataFarba = Color.White;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public FNewGrafikon()
        {
            InitializeComponent();
            FormUtils.SetFormFont(this);
            this.ApplyTheme();

            if (!string.IsNullOrEmpty(GlobData.INISSDir)) tbDirIniss.Text = GlobData.INISSDir;

            if (FMain.Stanice.Count == 0) rbNewObd.Enabled = false;

            nudIDStation.Enabled = false;
            tbStationName.Enabled = false;
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            var gvd = new GVDInfo();

            DateTime odD = dtpDataOd.Value.Date;
            DateTime doD = dtpDataDo.Value.Date;
            if (doD.CompareTo(odD) <= 0)
            {
                Utils.ShowError(Resources.FNewGrafikon_Čas_konca_platnosti_dát_má_byť_neskôr_ako_začiatok_platnosti);
                DialogResult = DialogResult.None;
                return;
            }

            DateTime odG = dtpDataOd.Value.Date;
            DateTime doG = dtpDataDo.Value.Date;
            if (doG.CompareTo(odG) <= 0)
            {
                Utils.ShowError(Resources.FNewGrafikon_Čas_konca_platnosti_grafikonu_má_byť_neskôr_ako_začiatok_platnosti);
                DialogResult = DialogResult.None;
                return;
            }

            if (cbCustomStation.Checked)
            {
                var id = decimal.ToInt32(nudIDStation.Value);
                var name = tbStationName.Text;

                if (string.IsNullOrEmpty(name))
                {
                    Utils.ShowError(Resources.FNewGrafikon_Nie_je_zadaná_žiadna_stanica);
                    DialogResult = DialogResult.None;
                    return;
                }

                foreach (Station stanica in GlobData.Stations)
                    if (stanica.ID == id.ToString())
                    {
                        Utils.ShowError(Resources.FNewGrafikon_Zadané_ID_vlastnej_stanice_už_patrí_inej_stanici);
                        DialogResult = DialogResult.None;
                        return;
                    }

                gvd.ThisStation = new Station(id.ToString(), name, isCustom: true);
            }
            else
            {
                if (cbStationName.SelectedIndex == -1)
                {
                    Utils.ShowError(Resources.FNewGrafikon_Nie_je_vybratá_stanica);
                    DialogResult = DialogResult.None;
                    return;
                }

                gvd.ThisStation = new Station(((Station) cbStationName.SelectedItem).ID,
                    ((Station) cbStationName.SelectedItem).Name);
            }

            gvd.StartValidData = odD;
            gvd.EndValidData = doD;
            gvd.StartValidTimeTable = odG;
            gvd.EndValidTimeTable = doG;
            gvd.CreateData = DateTime.Today;

            gvd.IsRegionText = true;
            gvd.Category = 1;
            gvd.VLIndex = -1;
            gvd.OnlyCityVLIndex = -999;

            NewDir = new DirList
            {
                DirName = tbDirName.Text,
                FullPath = GlobData.DATADir + Path.DirectorySeparatorChar + tbDirName.Text,
                TablePort = decimal.ToInt32(nudTabPort.Value),
                HlaseniePort = decimal.ToInt32(nudHlaseniePort.Value),
                Farba = vybrataFarba
            };

            GvdInfo = gvd;

            if (rbNewObd.Checked)
            {
                var o = false;
                var interval = new Interval(odG, doG);

                foreach (GVDDirectory obd in FMain.ObdobiaList)
                {
                    var compare = new Interval(obd.GVD.StartValidTimeTable, obd.GVD.EndValidTimeTable);

                    if (obd.GVD.ThisStation.Name == gvd.ThisStation.Name && interval.Overlaps(compare)) o = true;
                }

                if (o)
                {
                    Utils.ShowError(Resources.FNewGrafikon_Zadané_obdobie_platnosti_tejto_stanice_už_existuje);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            else
            {
                foreach (GVDDirectory obd in FMain.ObdobiaList)
                    if (obd.GVD.ThisStation.Name == gvd.ThisStation.Name)
                    {
                        Utils.ShowError(Resources.FNewGrafikon_Zadaná_stanica_pre_tento_INISS_už_existuje);
                        DialogResult = DialogResult.None;
                        return;
                    }
            }

            if (Directory.Exists(NewDir.FullPath))
            {
                Utils.ShowError(Resources.Priečinok_s_týmto_názvom_už_existuje__Zmeňte_jeho_názov);
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void bZrusit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cbStationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbDirName.Text = cbStationName.SelectedItem + @"." + dtpDataOd.Value.Year;
        }

        private void dtpZacPlatnosti_ValueChanged(object sender, EventArgs e)
        {
            if (cbCustomStation.Checked)
                tbDirName.Text = tbStationName.Text + @"." + dtpDataOd.Value.Year;
            else
                tbDirName.Text = cbStationName.SelectedItem + @"." + dtpDataOd.Value.Year;
        }

        private void tbDirIniss_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var path = tbDirIniss.Text + Path.DirectorySeparatorChar + "RAWBANK";
                RawBankReader.ReadFyzZvukFile(path, Language.GetBasicLanguage(GlobData.Languages));
                GlobData.Stations = Station.GetStations();
                GlobData.Stations.Sort();
                cbStationName.DataSource = GlobData.Stations;
            }
            catch (Exception)
            {
                Utils.ShowError(Resources.FNewGrafikon_Priečinok_neobsahuje_priečinok_RAWBANK);
            }
        }

        private void rbNewStation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewStation.Checked)
            {
                cbStationName.DataSource = null;
                var st = new List<Station>(GlobData.Stations);
                st.Sort();
                cbStationName.DataSource = st;
            }
            else
            {
                cbStationName.DataSource = null;
                var st = new List<Station>();
                foreach (Station s in GlobData.Stations)
                foreach (var ss in FMain.Stanice)
                    if (ss == s.Name)
                        st.Add(s);
                st.Sort();
                cbStationName.DataSource = st;
            }
        }

        private void bEditColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogFarba.ShowDialog();
            if (result == DialogResult.OK)
            {
                vybrataFarba = colorDialogFarba.Color;
                pbColor.BackColor = vybrataFarba;
            }
        }

        private void cbCustomStation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustomStation.Checked)
            {
                cbStationName.Enabled = false;
                nudIDStation.Enabled = true;
                tbStationName.Enabled = true;
            }
            else
            {
                cbStationName.Enabled = true;
                nudIDStation.Enabled = false;
                tbStationName.Enabled = false;
                tbDirName.Text = cbStationName.SelectedItem + @"." + dtpDataOd.Value.Year;
            }
        }

        private void tbStationName_TextChanged(object sender, EventArgs e)
        {
            tbDirName.Text = tbStationName.Text + @"." + dtpDataOd.Value.Year;
        }

        private void FNewGrafikon_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(LinkConsts.LINK_NEW_GVD);
        }
    }
}