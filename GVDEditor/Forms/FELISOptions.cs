﻿using System;
using System.IO;
using System.Windows.Forms;
using GVDEditor.Properties;
using GVDEditor.Tools;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     
    /// </summary>
    public partial class FELISOptions : Form
    {
        internal FMain.SendData ResultOptions;

        /// <summary>
        ///     Constructor
        /// </summary>
        public FELISOptions()
        {
            InitializeComponent();

            FormUtils.SetFormFont(this);
            this.ApplyTheme();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            var result = dOpenELIS.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbPath.Text = dOpenELIS.FileName;
            }
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbPath.Text))
            {
                Utils.ShowError(Resources.FELISOptions_Zadaný_súbor_neexistuje);
                DialogResult = DialogResult.None;
                return;
            }

            ResultOptions = new FMain.SendData()
            {
                DefinedDateRems = cbDateRems.Checked,
                DefinedOperators = cbOperators.Checked,
                OmitPassingTrains = cbSkipPassingTrains.Checked,
                Path = tbPath.Text
            };

            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
