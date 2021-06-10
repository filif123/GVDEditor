
namespace GVDEditor.Forms
{
    partial class FAnalyzer
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
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bAnalyze = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.lStatus = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bFixSelected = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bgWorkAnalyze = new System.ComponentModel.BackgroundWorker();
            this.iFixBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProblemType = new System.Windows.Forms.DataGridViewImageColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FixType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solutionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iFixBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Spustiť analýzu:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lStatus, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.bOK, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvResults, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.bAnalyze);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 31);
            this.panel1.TabIndex = 4;
            // 
            // bAnalyze
            // 
            this.bAnalyze.AutoSize = true;
            this.bAnalyze.Location = new System.Drawing.Point(124, 1);
            this.bAnalyze.Name = "bAnalyze";
            this.bAnalyze.Size = new System.Drawing.Size(88, 27);
            this.bAnalyze.TabIndex = 3;
            this.bAnalyze.Text = "Analyzovať";
            this.bAnalyze.UseVisualStyleBackColor = true;
            this.bAnalyze.Click += new System.EventHandler(this.bAnalyze_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbStatus.Enabled = false;
            this.pbStatus.Location = new System.Drawing.Point(233, 12);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(7);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(474, 23);
            this.pbStatus.TabIndex = 7;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lStatus.Location = new System.Drawing.Point(717, 5);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(75, 37);
            this.lStatus.TabIndex = 8;
            this.lStatus.Text = "0%";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bOK
            // 
            this.bOK.AutoSize = true;
            this.bOK.Location = new System.Drawing.Point(717, 409);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 33);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.bFixSelected);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 409);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(703, 33);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // bFixSelected
            // 
            this.bFixSelected.AutoSize = true;
            this.bFixSelected.Location = new System.Drawing.Point(3, 3);
            this.bFixSelected.Name = "bFixSelected";
            this.bFixSelected.Size = new System.Drawing.Size(131, 27);
            this.bFixSelected.TabIndex = 9;
            this.bFixSelected.Text = "Opraviť označené";
            this.bFixSelected.UseVisualStyleBackColor = true;
            this.bFixSelected.Click += new System.EventHandler(this.bFixSelected_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoGenerateColumns = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProblemType,
            this.textDataGridViewTextBoxColumn,
            this.FixType,
            this.solutionDataGridViewTextBoxColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvResults, 3);
            this.dgvResults.DataSource = this.iFixBindingSource;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(8, 45);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(784, 358);
            this.dgvResults.TabIndex = 11;
            this.dgvResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResults_CellFormatting);
            this.dgvResults.DoubleClick += new System.EventHandler(this.dgvResults_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // bgWorkAnalyze
            // 
            this.bgWorkAnalyze.WorkerReportsProgress = true;
            this.bgWorkAnalyze.WorkerSupportsCancellation = true;
            this.bgWorkAnalyze.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkAnalyze_DoWork);
            this.bgWorkAnalyze.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkAnalyze_ProgressChanged);
            this.bgWorkAnalyze.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkAnalyze_RunWorkerCompleted);
            // 
            // iFixBindingSource
            // 
            this.iFixBindingSource.DataSource = typeof(GVDEditor.Tools.IProblem);
            // 
            // ProblemType
            // 
            this.ProblemType.HeaderText = "Typ problému";
            this.ProblemType.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ProblemType.MinimumWidth = 6;
            this.ProblemType.Name = "ProblemType";
            this.ProblemType.ReadOnly = true;
            this.ProblemType.Width = 125;
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            this.textDataGridViewTextBoxColumn.HeaderText = "Problém";
            this.textDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            this.textDataGridViewTextBoxColumn.ReadOnly = true;
            this.textDataGridViewTextBoxColumn.Width = 89;
            // 
            // FixType
            // 
            this.FixType.HeaderText = "Typ riešenia";
            this.FixType.MinimumWidth = 6;
            this.FixType.Name = "FixType";
            this.FixType.ReadOnly = true;
            this.FixType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FixType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FixType.Width = 125;
            // 
            // solutionDataGridViewTextBoxColumn
            // 
            this.solutionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.solutionDataGridViewTextBoxColumn.DataPropertyName = "Solution";
            this.solutionDataGridViewTextBoxColumn.HeaderText = "Riešenie";
            this.solutionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.solutionDataGridViewTextBoxColumn.Name = "solutionDataGridViewTextBoxColumn";
            this.solutionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FAnalyzer
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.Name = "FAnalyzer";
            this.ShowIcon = false;
            this.Text = "Analyzátor grafikonu";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iFixBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bAnalyze;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Button bFixSelected;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.BindingSource iFixBindingSource;
        private System.ComponentModel.BackgroundWorker bgWorkAnalyze;
        private System.Windows.Forms.DataGridViewImageColumn ProblemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FixType;
        private System.Windows.Forms.DataGridViewTextBoxColumn solutionDataGridViewTextBoxColumn;
    }
}