﻿using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using GVDEditor.Tools;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Dialog - Informacie o aplikacii
    /// </summary>
    public partial class FInformation : Form
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public FInformation()
        {
            InitializeComponent();
            FormUtils.SetFormFont(this);
            this.ApplyTheme();

            lAppVersion.Text = Application.ProductVersion;
            lAppName.Text = Application.ProductName;
            lAppName.Font = new Font(lAppName.Font, FontStyle.Bold);

            linkWeb.Text = LinkConsts.LINK_INFO_APP;
            linkEmail.Text = LinkConsts.EMAIL;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(LinkConsts.LINK_INFO_APP);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:" + LinkConsts.EMAIL);
        }

        private void InformationForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(LinkConsts.LINK_INFO_APP);
        }
    }
}