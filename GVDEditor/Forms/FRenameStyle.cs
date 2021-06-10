﻿using GVDEditor.Tools;
using GVDEditor.XML;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GVDEditor.Properties;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Dialóg - Zmena názvu štýlu
    /// </summary>
    public partial class FRenameStyle : Form
    {
        /// <summary>
        ///     Vrateny novy nazov stylu
        /// </summary>
        public string NewName;

        private readonly List<Style> styles;
        private readonly int currentIndex;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="curIndex">index tohto stylu v zozname</param>
        /// <param name="pname">predosly nazov stylu</param>
        /// <param name="styles">vsetky dosial definovane styly</param>
        public FRenameStyle(List<Style> styles, int curIndex, string pname)
        {
            InitializeComponent();
            FormUtils.SetFormFont(this);
            this.ApplyTheme();

            tbStyleName.Text = pname;
            this.styles = styles;
            currentIndex = curIndex;
        }

        private void bRename_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbStyleName.Text))
            {
                Utils.ShowError(Resources.FRenameStyle_Neplatný_nový_názov_štýlu);
                DialogResult = DialogResult.None;
                return;
            }

            for (var i = 0; i < styles.Count; i++)
            {
                var style = styles[i];
                if (style.Name == tbStyleName.Text && i != currentIndex)
                {
                    Utils.ShowError(Resources.FRenameStyle_Zadaný_názov_štýlu_už_existuje);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            NewName = tbStyleName.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
