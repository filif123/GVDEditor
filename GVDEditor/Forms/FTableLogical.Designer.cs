using ExControls;
using GVDEditor.Entities;

namespace GVDEditor.Forms
{
    partial class FTableLogical
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTableLogical));
            this.bSave = new System.Windows.Forms.Button();
            this.bStorno = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLogicalZostavaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox4 = new ExControls.ExGroupBox(this.components);
            this.tbComment = new ExControls.ExTextBox(this.components);
            this.gboxSimple = new ExControls.ExGroupBox(this.components);
            this.dgvZostava = new System.Windows.Forms.DataGridView();
            this.startRowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endRowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bRemoveTab = new System.Windows.Forms.Button();
            this.bAddTab = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listFyzTab = new System.Windows.Forms.ListBox();
            this.groupBox1 = new ExControls.ExGroupBox(this.components);
            this.nudCountRecords = new ExControls.ExNumericUpDown(this.components);
            this.cbTypeView = new ExControls.ExComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.tbKey = new ExControls.ExTextBox(this.components);
            this.tbName = new ExControls.ExTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tablePhysicalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tableLogicalZostavaBindingSource)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.gboxSimple.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZostava)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePhysicalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bSave.Name = "bSave";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bStorno
            // 
            resources.ApplyResources(this.bStorno, "bStorno");
            this.bStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bStorno.Name = "bStorno";
            this.bStorno.UseVisualStyleBackColor = true;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Table";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // tableLogicalZostavaBindingSource
            // 
            this.tableLogicalZostavaBindingSource.DataSource = typeof(GVDEditor.Forms.FTableLogical.TableLogicalZostava);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbComment);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // tbComment
            // 
            this.tbComment.AcceptsReturn = true;
            this.tbComment.AcceptsTab = true;
            this.tbComment.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.tbComment, "tbComment");
            this.tbComment.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.tbComment.Name = "tbComment";
            // 
            // gboxSimple
            // 
            this.gboxSimple.Controls.Add(this.dgvZostava);
            this.gboxSimple.Controls.Add(this.bRemoveTab);
            this.gboxSimple.Controls.Add(this.bAddTab);
            this.gboxSimple.Controls.Add(this.label5);
            this.gboxSimple.Controls.Add(this.label4);
            this.gboxSimple.Controls.Add(this.listFyzTab);
            resources.ApplyResources(this.gboxSimple, "gboxSimple");
            this.gboxSimple.Name = "gboxSimple";
            this.gboxSimple.TabStop = false;
            // 
            // dgvZostava
            // 
            this.dgvZostava.AllowUserToAddRows = false;
            this.dgvZostava.AllowUserToDeleteRows = false;
            this.dgvZostava.AllowUserToResizeColumns = false;
            this.dgvZostava.AllowUserToResizeRows = false;
            this.dgvZostava.AutoGenerateColumns = false;
            this.dgvZostava.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvZostava.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZostava.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.startRowDataGridViewTextBoxColumn,
            this.endRowDataGridViewTextBoxColumn,
            this.tableDataGridViewTextBoxColumn});
            this.dgvZostava.DataSource = this.tableLogicalZostavaBindingSource;
            resources.ApplyResources(this.dgvZostava, "dgvZostava");
            this.dgvZostava.Name = "dgvZostava";
            this.dgvZostava.RowHeadersVisible = false;
            this.dgvZostava.RowTemplate.Height = 24;
            this.dgvZostava.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZostava.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvZostava_CellValidating);
            // 
            // startRowDataGridViewTextBoxColumn
            // 
            this.startRowDataGridViewTextBoxColumn.DataPropertyName = "StartRow";
            resources.ApplyResources(this.startRowDataGridViewTextBoxColumn, "startRowDataGridViewTextBoxColumn");
            this.startRowDataGridViewTextBoxColumn.Name = "startRowDataGridViewTextBoxColumn";
            // 
            // endRowDataGridViewTextBoxColumn
            // 
            this.endRowDataGridViewTextBoxColumn.DataPropertyName = "EndRow";
            resources.ApplyResources(this.endRowDataGridViewTextBoxColumn, "endRowDataGridViewTextBoxColumn");
            this.endRowDataGridViewTextBoxColumn.Name = "endRowDataGridViewTextBoxColumn";
            // 
            // tableDataGridViewTextBoxColumn
            // 
            this.tableDataGridViewTextBoxColumn.DataPropertyName = "Table";
            resources.ApplyResources(this.tableDataGridViewTextBoxColumn, "tableDataGridViewTextBoxColumn");
            this.tableDataGridViewTextBoxColumn.Name = "tableDataGridViewTextBoxColumn";
            this.tableDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bRemoveTab
            // 
            resources.ApplyResources(this.bRemoveTab, "bRemoveTab");
            this.bRemoveTab.Name = "bRemoveTab";
            this.bRemoveTab.UseVisualStyleBackColor = true;
            this.bRemoveTab.Click += new System.EventHandler(this.bRemoveTab_Click);
            // 
            // bAddTab
            // 
            resources.ApplyResources(this.bAddTab, "bAddTab");
            this.bAddTab.Name = "bAddTab";
            this.bAddTab.UseVisualStyleBackColor = true;
            this.bAddTab.Click += new System.EventHandler(this.bAddTab_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // listFyzTab
            // 
            this.listFyzTab.FormattingEnabled = true;
            resources.ApplyResources(this.listFyzTab, "listFyzTab");
            this.listFyzTab.Name = "listFyzTab";
            this.listFyzTab.DoubleClick += new System.EventHandler(this.listFyzTab_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudCountRecords);
            this.groupBox1.Controls.Add(this.cbTypeView);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbKey);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // nudCountRecords
            // 
            resources.ApplyResources(this.nudCountRecords, "nudCountRecords");
            this.nudCountRecords.Name = "nudCountRecords";
            // 
            // cbTypeView
            // 
            this.cbTypeView.DropDownSelectedRowBackColor = System.Drawing.SystemColors.Highlight;
            this.cbTypeView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeView.FormattingEnabled = true;
            resources.ApplyResources(this.cbTypeView, "cbTypeView");
            this.cbTypeView.Name = "cbTypeView";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // tbKey
            // 
            this.tbKey.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbKey.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbKey, "tbKey");
            this.tbKey.Name = "tbKey";
            // 
            // tbName
            // 
            this.tbName.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbName.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tablePhysicalBindingSource
            // 
            this.tablePhysicalBindingSource.DataSource = typeof(GVDEditor.Entities.TablePhysical);
            // 
            // FTableLogical
            // 
            this.AcceptButton = this.bSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bStorno;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gboxSimple);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bStorno);
            this.Controls.Add(this.bSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTableLogical";
            this.ShowInTaskbar = false;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FTableLogical_HelpButtonClicked);
            ((System.ComponentModel.ISupportInitialize)(this.tableLogicalZostavaBindingSource)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gboxSimple.ResumeLayout(false);
            this.gboxSimple.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZostava)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePhysicalBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bStorno;
        private ExGroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ExComboBox cbTypeView;
        private ExTextBox tbKey;
        private ExTextBox tbName;
        private System.Windows.Forms.Label label3;
        private ExGroupBox gboxSimple;
        private System.Windows.Forms.Button bRemoveTab;
        private System.Windows.Forms.Button bAddTab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listFyzTab;
        private ExNumericUpDown nudCountRecords;
        private System.Windows.Forms.Label label7;
        private ExGroupBox groupBox4;
        private ExTextBox tbComment;
        private System.Windows.Forms.BindingSource tablePhysicalBindingSource;
        private System.Windows.Forms.DataGridView dgvZostava;
        private System.Windows.Forms.BindingSource tableLogicalZostavaBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn startRowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endRowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableDataGridViewTextBoxColumn;
    }
}