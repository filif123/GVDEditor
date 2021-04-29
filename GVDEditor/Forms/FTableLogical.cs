﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Dialog - Nastavenie logickej tabule
    /// </summary>
    public partial class FTableLogical : Form
    {
        private readonly bool copy;
        private readonly BindingList<TableLogicalZostava> TVybrane;

        /// <summary>
        ///     Tato logicka tabula
        /// </summary>
        public TableLogical ThisTable;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="table">tato tabula</param>
        /// <param name="tables">dostupne fyzicke tabule</param>
        /// <param name="copy">ci sa jedna o kopiu</param>
        public FTableLogical(TableLogical table, List<TablePhysical> tables, bool copy = false)
        {
            InitializeComponent();
            FormUtils.SetFormFont(this);
            this.ApplyTheme();

            ThisTable = table;
            this.copy = copy;

            var vybrane = new HashSet<TablePhysical>();
            var zostava = new List<TableLogicalZostava>();

            foreach (TableRecord record in ThisTable.Records)
            foreach (TablePosition position in record)
                if (vybrane.Contains(position.TablePhysical))
                {
                    foreach (TableLogicalZostava logicalZostava in zostava.Where(logicalZostava =>
                        logicalZostava.Table.Equals(position.TablePhysical)))
                        logicalZostava.EndRow = position.Position + 1;
                }
                else
                {
                    vybrane.Add(position.TablePhysical);
                    zostava.Add(new TableLogicalZostava
                    {
                        Table = position.TablePhysical, StartRow = position.Position + 1, EndRow = position.Position + 1
                    });
                }

            TVybrane = new BindingList<TableLogicalZostava>(zostava);

            cbTypeView.DataSource = TableViewType.GetValues();
            if (ThisTable.TypeView != null) cbTypeView.SelectedItem = ThisTable.TypeView;

            listFyzTab.DataSource = tables;
            dgvZostava.DataSource = TVybrane;

            tbName.Text = ThisTable.Name;
            tbKey.Text = ThisTable.Key;

            tbComment.Text = table.Comment;

            nudCountRecords.Value = ThisTable.Records.Count;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            TableLogical table = copy ? new TableLogical() : ThisTable;

            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbKey.Text))
            {
                Utils.ShowError(Resources.Tables_Nie_sú_vyplnené_všetky_povinné_polia);
                DialogResult = DialogResult.None;
                return;
            }

            foreach (TableLogical t in FLocalSettings.TLogicals)
                if (t.Key == tbKey.Text && table != t)
                {
                    Utils.ShowError(Resources.Tables_Zadaný_kľúč_tabule_už_existuje);
                    DialogResult = DialogResult.None;
                    return;
                }

            table.Key = tbKey.Text;
            table.Name = tbName.Text;
            table.TypeView = (TableViewType) cbTypeView.SelectedItem;

            var records = new List<TableRecord>();

            for (var i = 0; i < nudCountRecords.Value; i++)
            {
                var positions = new List<TablePosition>();
                foreach (TableLogicalZostava zostava in TVybrane)
                    if (i >= zostava.StartRow - 1 && i <= zostava.EndRow - 1)
                        positions.Add(new TablePosition
                        {
                            TablePhysical = zostava.Table, Position = i,
                            TypeView = (TableViewType) cbTypeView.SelectedItem
                        });
                
                records.Add(new TableRecord {Positions = positions});
            }

            table.Records = records;

            table.Comment = tbComment.Text;

            if (copy) ThisTable = table;

            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void bAddTab_Click(object sender, EventArgs e)
        {
            if (listFyzTab.SelectedIndex != -1)
            {
                var fyztab = (TablePhysical) listFyzTab.SelectedItem;
                var found = false;
                foreach (TableLogicalZostava zostava in TVybrane)
                    if (zostava.Table.Equals(fyztab))
                        found = true;

                if (!found)
                    TVybrane.Add(new TableLogicalZostava
                        {Table = fyztab, StartRow = 0, EndRow = decimal.ToInt32(nudCountRecords.Value)});
            }
        }

        private void bRemoveTab_Click(object sender, EventArgs e)
        {
            if (dgvZostava.SelectedRows.Count != 0) TVybrane.RemoveAt(dgvZostava.SelectedRows[0].Index);
        }

        private void listFyzTab_DoubleClick(object sender, EventArgs e)
        {
            if (listFyzTab.SelectedIndex != -1)
            {
                var fyztab = (TablePhysical) listFyzTab.SelectedItem;
                var found = false;
                foreach (TableLogicalZostava zostava in TVybrane)
                    if (zostava.Table.Equals(fyztab))
                        found = true;

                if (!found)
                    TVybrane.Add(new TableLogicalZostava
                        {Table = fyztab, StartRow = 0, EndRow = decimal.ToInt32(nudCountRecords.Value)});
            }
        }

        private void dgvZostava_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex != -1)
                switch (e.ColumnIndex)
                {
                    case 0:
                    {
                        var num = int.Parse((string) e.FormattedValue);
                        if (num < 1) e.Cancel = true;

                        break;
                    }
                    case 1:
                    {
                        var num = int.Parse((string) e.FormattedValue);
                        if (num > nudCountRecords.Value) e.Cancel = true;

                        break;
                    }
                }
        }

        private void FTableLogical_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(LinkConsts.LINK_TLOGICAL);
        }

        private class TableLogicalZostava
        {
            public TablePhysical Table { get; set; }
            public int StartRow { get; set; }
            public int EndRow { get; set; }
        }
    }
}