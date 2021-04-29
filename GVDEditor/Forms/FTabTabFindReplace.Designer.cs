﻿
using ExControls;

namespace GVDEditor.Forms
{
    partial class FTabTabFindReplace
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
            ExControls.ExComboBoxStyle exComboBoxStyle9 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle10 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle11 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle12 = new ExControls.ExComboBoxStyle();
            this.tabControl1 = new ExControls.ExTabControl(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbFind = new ExControls.ExComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.bFind = new System.Windows.Forms.Button();
            this.bCount = new System.Windows.Forms.Button();
            this.bFindClose = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboxSearchingCyclic = new ExControls.ExCheckBox(this.components);
            this.cboxSearchingCaseSensitive = new ExControls.ExCheckBox(this.components);
            this.cboxSearchingWholeWord = new ExControls.ExCheckBox(this.components);
            this.cboxBackSearching = new ExControls.ExCheckBox(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.exGroupBox1 = new ExControls.ExGroupBox(this.components);
            this.rbRegExSearching = new ExControls.ExRadioButton(this.components);
            this.rbAdvancedSearching = new ExControls.ExRadioButton(this.components);
            this.rbNormalSearching = new ExControls.ExRadioButton(this.components);
            this.exGroupBox2 = new ExControls.ExGroupBox(this.components);
            this.rbTransAlways = new ExControls.ExRadioButton(this.components);
            this.rbTransOnlyOnFocusLost = new ExControls.ExRadioButton(this.components);
            this.cboxTransparency = new ExControls.ExCheckBox(this.components);
            this.barTransparency = new System.Windows.Forms.TrackBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.exGroupBox1.SuspendLayout();
            this.exGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.tabControl1.HighlightBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(611, 279);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(603, 250);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hľadať";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cbFind, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bFind, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.bCount, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.bFindClose, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(597, 244);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // cbFind
            // 
            this.cbFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFind.DropDownSelectedRowBackColor = System.Drawing.SystemColors.Highlight;
            this.cbFind.FormattingEnabled = true;
            this.cbFind.Location = new System.Drawing.Point(64, 3);
            this.cbFind.Name = "cbFind";
            this.cbFind.Size = new System.Drawing.Size(429, 24);
            exComboBoxStyle9.ArrowColor = null;
            exComboBoxStyle9.BackColor = null;
            exComboBoxStyle9.BorderColor = null;
            exComboBoxStyle9.ButtonBackColor = null;
            exComboBoxStyle9.ButtonBorderColor = null;
            exComboBoxStyle9.ButtonRenderFirst = null;
            exComboBoxStyle9.ForeColor = null;
            this.cbFind.StyleDisabled = exComboBoxStyle9;
            exComboBoxStyle10.ArrowColor = null;
            exComboBoxStyle10.BackColor = null;
            exComboBoxStyle10.BorderColor = null;
            exComboBoxStyle10.ButtonBackColor = null;
            exComboBoxStyle10.ButtonBorderColor = null;
            exComboBoxStyle10.ButtonRenderFirst = null;
            exComboBoxStyle10.ForeColor = null;
            this.cbFind.StyleHighlight = exComboBoxStyle10;
            exComboBoxStyle11.ArrowColor = null;
            exComboBoxStyle11.BackColor = null;
            exComboBoxStyle11.BorderColor = null;
            exComboBoxStyle11.ButtonBackColor = null;
            exComboBoxStyle11.ButtonBorderColor = null;
            exComboBoxStyle11.ButtonRenderFirst = null;
            exComboBoxStyle11.ForeColor = null;
            this.cbFind.StyleNormal = exComboBoxStyle11;
            exComboBoxStyle12.ArrowColor = null;
            exComboBoxStyle12.BackColor = null;
            exComboBoxStyle12.BorderColor = null;
            exComboBoxStyle12.ButtonBackColor = null;
            exComboBoxStyle12.ButtonBorderColor = null;
            exComboBoxStyle12.ButtonRenderFirst = null;
            exComboBoxStyle12.ForeColor = null;
            this.cbFind.StyleSelected = exComboBoxStyle12;
            this.cbFind.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hľadať:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bFind
            // 
            this.bFind.AutoSize = true;
            this.bFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bFind.Location = new System.Drawing.Point(499, 3);
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(95, 27);
            this.bFind.TabIndex = 2;
            this.bFind.Text = "Hľadať ďalší";
            this.bFind.UseVisualStyleBackColor = true;
            this.bFind.Click += new System.EventHandler(this.bFind_Click);
            // 
            // bCount
            // 
            this.bCount.AutoSize = true;
            this.bCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCount.Location = new System.Drawing.Point(499, 36);
            this.bCount.Name = "bCount";
            this.bCount.Size = new System.Drawing.Size(95, 27);
            this.bCount.TabIndex = 3;
            this.bCount.Text = "Spočítať";
            this.bCount.UseVisualStyleBackColor = true;
            this.bCount.Click += new System.EventHandler(this.bCount_Click);
            // 
            // bFindClose
            // 
            this.bFindClose.AutoSize = true;
            this.bFindClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.bFindClose.Location = new System.Drawing.Point(499, 69);
            this.bFindClose.Name = "bFindClose";
            this.bFindClose.Size = new System.Drawing.Size(95, 27);
            this.bFindClose.TabIndex = 4;
            this.bFindClose.Text = "Zatvoriť";
            this.bFindClose.UseVisualStyleBackColor = true;
            this.bFindClose.Click += new System.EventHandler(this.bFindClose_Click);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.cboxSearchingCyclic);
            this.flowLayoutPanel1.Controls.Add(this.cboxSearchingCaseSensitive);
            this.flowLayoutPanel1.Controls.Add(this.cboxSearchingWholeWord);
            this.flowLayoutPanel1.Controls.Add(this.cboxBackSearching);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 69);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(490, 172);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // cboxSearchingCyclic
            // 
            this.cboxSearchingCyclic.AutoSize = true;
            this.cboxSearchingCyclic.BoxBackColor = System.Drawing.Color.White;
            this.cboxSearchingCyclic.Checked = true;
            this.cboxSearchingCyclic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxSearchingCyclic.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxSearchingCyclic.Location = new System.Drawing.Point(3, 148);
            this.cboxSearchingCyclic.Name = "cboxSearchingCyclic";
            this.cboxSearchingCyclic.Size = new System.Drawing.Size(80, 21);
            this.cboxSearchingCyclic.TabIndex = 3;
            this.cboxSearchingCyclic.Text = "Cyklicky";
            this.cboxSearchingCyclic.UseVisualStyleBackColor = true;
            // 
            // cboxSearchingCaseSensitive
            // 
            this.cboxSearchingCaseSensitive.AutoSize = true;
            this.cboxSearchingCaseSensitive.BoxBackColor = System.Drawing.Color.White;
            this.cboxSearchingCaseSensitive.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxSearchingCaseSensitive.Location = new System.Drawing.Point(3, 121);
            this.cboxSearchingCaseSensitive.Name = "cboxSearchingCaseSensitive";
            this.cboxSearchingCaseSensitive.Size = new System.Drawing.Size(235, 21);
            this.cboxSearchingCaseSensitive.TabIndex = 2;
            this.cboxSearchingCaseSensitive.Text = "Rozlišovať malé/VEĽKÉ písmená";
            this.cboxSearchingCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // cboxSearchingWholeWord
            // 
            this.cboxSearchingWholeWord.AutoSize = true;
            this.cboxSearchingWholeWord.BoxBackColor = System.Drawing.Color.White;
            this.cboxSearchingWholeWord.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxSearchingWholeWord.Location = new System.Drawing.Point(3, 94);
            this.cboxSearchingWholeWord.Name = "cboxSearchingWholeWord";
            this.cboxSearchingWholeWord.Size = new System.Drawing.Size(163, 21);
            this.cboxSearchingWholeWord.TabIndex = 1;
            this.cboxSearchingWholeWord.Text = "Hľadať iba celé slová";
            this.cboxSearchingWholeWord.UseVisualStyleBackColor = true;
            // 
            // cboxBackSearching
            // 
            this.cboxBackSearching.AutoSize = true;
            this.cboxBackSearching.BoxBackColor = System.Drawing.Color.White;
            this.cboxBackSearching.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxBackSearching.Location = new System.Drawing.Point(3, 67);
            this.cboxBackSearching.Name = "cboxBackSearching";
            this.cboxBackSearching.Size = new System.Drawing.Size(133, 21);
            this.cboxBackSearching.TabIndex = 0;
            this.cboxBackSearching.Text = "Smerom dozadu";
            this.cboxBackSearching.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(603, 250);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nahradiť";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 391);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(63, 20);
            this.tsslStatus.Text = "Nothing";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.exGroupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.exGroupBox2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 391);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // exGroupBox1
            // 
            this.exGroupBox1.Controls.Add(this.rbRegExSearching);
            this.exGroupBox1.Controls.Add(this.rbAdvancedSearching);
            this.exGroupBox1.Controls.Add(this.rbNormalSearching);
            this.exGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exGroupBox1.Location = new System.Drawing.Point(3, 288);
            this.exGroupBox1.Name = "exGroupBox1";
            this.exGroupBox1.Size = new System.Drawing.Size(302, 100);
            this.exGroupBox1.TabIndex = 1;
            this.exGroupBox1.TabStop = false;
            this.exGroupBox1.Text = "Režim vyhľadávania";
            // 
            // rbRegExSearching
            // 
            this.rbRegExSearching.AutoSize = true;
            this.rbRegExSearching.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbRegExSearching.Location = new System.Drawing.Point(10, 73);
            this.rbRegExSearching.Name = "rbRegExSearching";
            this.rbRegExSearching.Size = new System.Drawing.Size(132, 21);
            this.rbRegExSearching.TabIndex = 2;
            this.rbRegExSearching.Text = "Regulárny výraz";
            this.rbRegExSearching.UseVisualStyleBackColor = true;
            this.rbRegExSearching.CheckedChanged += new System.EventHandler(this.rbRegExSearching_CheckedChanged);
            // 
            // rbAdvancedSearching
            // 
            this.rbAdvancedSearching.AutoSize = true;
            this.rbAdvancedSearching.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbAdvancedSearching.Location = new System.Drawing.Point(10, 46);
            this.rbAdvancedSearching.Name = "rbAdvancedSearching";
            this.rbAdvancedSearching.Size = new System.Drawing.Size(167, 21);
            this.rbAdvancedSearching.TabIndex = 1;
            this.rbAdvancedSearching.Text = "Rozšírený (\\n, \\r, \\0...)";
            this.rbAdvancedSearching.UseVisualStyleBackColor = true;
            // 
            // rbNormalSearching
            // 
            this.rbNormalSearching.AutoSize = true;
            this.rbNormalSearching.Checked = true;
            this.rbNormalSearching.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbNormalSearching.Location = new System.Drawing.Point(10, 21);
            this.rbNormalSearching.Name = "rbNormalSearching";
            this.rbNormalSearching.Size = new System.Drawing.Size(89, 21);
            this.rbNormalSearching.TabIndex = 0;
            this.rbNormalSearching.TabStop = true;
            this.rbNormalSearching.Text = "Normálny";
            this.rbNormalSearching.UseVisualStyleBackColor = true;
            // 
            // exGroupBox2
            // 
            this.exGroupBox2.Controls.Add(this.rbTransAlways);
            this.exGroupBox2.Controls.Add(this.rbTransOnlyOnFocusLost);
            this.exGroupBox2.Controls.Add(this.cboxTransparency);
            this.exGroupBox2.Controls.Add(this.barTransparency);
            this.exGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exGroupBox2.Location = new System.Drawing.Point(311, 288);
            this.exGroupBox2.Name = "exGroupBox2";
            this.exGroupBox2.Size = new System.Drawing.Size(303, 100);
            this.exGroupBox2.TabIndex = 2;
            this.exGroupBox2.TabStop = false;
            this.exGroupBox2.Text = " ";
            // 
            // rbTransAlways
            // 
            this.rbTransAlways.AutoSize = true;
            this.rbTransAlways.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbTransAlways.Location = new System.Drawing.Point(144, 62);
            this.rbTransAlways.Name = "rbTransAlways";
            this.rbTransAlways.Size = new System.Drawing.Size(60, 21);
            this.rbTransAlways.TabIndex = 3;
            this.rbTransAlways.Text = "Vždy";
            this.rbTransAlways.UseVisualStyleBackColor = true;
            // 
            // rbTransOnlyOnFocusLost
            // 
            this.rbTransOnlyOnFocusLost.AutoSize = true;
            this.rbTransOnlyOnFocusLost.Checked = true;
            this.rbTransOnlyOnFocusLost.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbTransOnlyOnFocusLost.Location = new System.Drawing.Point(6, 62);
            this.rbTransOnlyOnFocusLost.Name = "rbTransOnlyOnFocusLost";
            this.rbTransOnlyOnFocusLost.Size = new System.Drawing.Size(132, 21);
            this.rbTransOnlyOnFocusLost.TabIndex = 2;
            this.rbTransOnlyOnFocusLost.TabStop = true;
            this.rbTransOnlyOnFocusLost.Text = "Pri strate fokusu";
            this.rbTransOnlyOnFocusLost.UseVisualStyleBackColor = true;
            this.rbTransOnlyOnFocusLost.CheckedChanged += new System.EventHandler(this.rbTransOnlyOnFocusLost_CheckedChanged);
            // 
            // cboxTransparency
            // 
            this.cboxTransparency.AutoSize = true;
            this.cboxTransparency.BoxBackColor = System.Drawing.Color.White;
            this.cboxTransparency.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.cboxTransparency.Location = new System.Drawing.Point(6, 0);
            this.cboxTransparency.Name = "cboxTransparency";
            this.cboxTransparency.Size = new System.Drawing.Size(111, 21);
            this.cboxTransparency.TabIndex = 0;
            this.cboxTransparency.Text = "Priehľadnosť";
            this.cboxTransparency.UseVisualStyleBackColor = true;
            this.cboxTransparency.CheckedChanged += new System.EventHandler(this.cboxTransparency_CheckedChanged);
            // 
            // barTransparency
            // 
            this.barTransparency.Enabled = false;
            this.barTransparency.LargeChange = 10;
            this.barTransparency.Location = new System.Drawing.Point(6, 27);
            this.barTransparency.Maximum = 100;
            this.barTransparency.Minimum = 20;
            this.barTransparency.Name = "barTransparency";
            this.barTransparency.Size = new System.Drawing.Size(290, 56);
            this.barTransparency.SmallChange = 5;
            this.barTransparency.TabIndex = 1;
            this.barTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.barTransparency.Value = 100;
            this.barTransparency.Scroll += new System.EventHandler(this.barTransparency_Scroll);
            // 
            // FTabTabFindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 417);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(635, 464);
            this.Name = "FTabTabFindReplace";
            this.ShowInTaskbar = false;
            this.Text = "Hľadať a nahradiť";
            this.TopMost = true;
            this.Leave += new System.EventHandler(this.FTabTabFindReplace_Leave);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.exGroupBox1.ResumeLayout(false);
            this.exGroupBox1.PerformLayout();
            this.exGroupBox2.ResumeLayout(false);
            this.exGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ExGroupBox exGroupBox1;
        private ExGroupBox exGroupBox2;
        private ExCheckBox cboxTransparency;
        private System.Windows.Forms.TrackBar barTransparency;
        private ExRadioButton rbTransOnlyOnFocusLost;
        private ExRadioButton rbTransAlways;
        private ExRadioButton rbNormalSearching;
        private ExRadioButton rbAdvancedSearching;
        private ExRadioButton rbRegExSearching;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private ExComboBox cbFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bFind;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Button bCount;
        private System.Windows.Forms.Button bFindClose;
        private ExCheckBox cboxBackSearching;
        private ExCheckBox cboxSearchingWholeWord;
        private ExCheckBox cboxSearchingCaseSensitive;
        private ExCheckBox cboxSearchingCyclic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}