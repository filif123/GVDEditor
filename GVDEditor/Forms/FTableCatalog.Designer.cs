using ExControls;

namespace GVDEditor.Forms
{
    partial class FTableCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTableCatalog));
            this.groupBox1 = new ExGroupBox(this.components);
            this.cbManufacturer = new ExComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tbKey = new ExTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new ExTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.nudMaxRecCount = new ExNumericUpDown(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.bStorno = new System.Windows.Forms.Button();
            this.groupBox2 = new ExGroupBox(this.components);
            this.cbDivType = new ExComboBox(this.components);
            this.nudFont = new ExNumericUpDown(this.components);
            this.nudLine = new ExNumericUpDown(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbTab2 = new ExComboBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.cbTab1 = new ExComboBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.rbRight = new ExRadioButton(this.components);
            this.rbCenter = new ExRadioButton(this.components);
            this.rbLeft = new ExRadioButton(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.nudEnd = new ExNumericUpDown(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.nudStart = new ExNumericUpDown(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.cbColumnFill = new ExComboBox(this.components);
            this.tbColumnKey = new ExTextBox(this.components);
            this.tbColumnName = new ExTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bColumnDelete = new System.Windows.Forms.Button();
            this.bColumnEdit = new System.Windows.Forms.Button();
            this.bColumnAdd = new System.Windows.Forms.Button();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.groupBox3 = new ExGroupBox(this.components);
            this.nudMinHeight = new ExNumericUpDown(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.bSetAll = new System.Windows.Forms.Button();
            this.nudSize = new ExNumericUpDown(this.components);
            this.nudWidth = new ExNumericUpDown(this.components);
            this.nudHeight = new ExNumericUpDown(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.bRowEdit = new System.Windows.Forms.Button();
            this.listRows = new System.Windows.Forms.ListBox();
            this.groupBox4 = new ExGroupBox(this.components);
            this.tbComment = new ExTextBox(this.components);
            this.groupBox5 = new ExGroupBox(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.bSetColumnOrder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRecCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbManufacturer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbManufacturer
            // 
            this.cbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManufacturer.FormattingEnabled = true;
            resources.ApplyResources(this.cbManufacturer, "cbManufacturer");
            this.cbManufacturer.Name = "cbManufacturer";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbKey
            // 
            this.tbKey.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbKey.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbKey, "tbKey");
            this.tbKey.Name = "tbKey";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tbName
            // 
            this.tbName.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbName.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // nudMaxRecCount
            // 
            resources.ApplyResources(this.nudMaxRecCount, "nudMaxRecCount");
            this.nudMaxRecCount.Name = "nudMaxRecCount";
            this.nudMaxRecCount.ValueChanged += new System.EventHandler(this.nudMaxRecCount_ValueChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDivType);
            this.groupBox2.Controls.Add(this.nudFont);
            this.groupBox2.Controls.Add(this.nudLine);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cbTab2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbTab1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.rbRight);
            this.groupBox2.Controls.Add(this.rbCenter);
            this.groupBox2.Controls.Add(this.rbLeft);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudEnd);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nudStart);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbColumnFill);
            this.groupBox2.Controls.Add(this.tbColumnKey);
            this.groupBox2.Controls.Add(this.tbColumnName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.bColumnDelete);
            this.groupBox2.Controls.Add(this.bColumnEdit);
            this.groupBox2.Controls.Add(this.bColumnAdd);
            this.groupBox2.Controls.Add(this.listColumns);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cbDivType
            // 
            this.cbDivType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDivType.FormattingEnabled = true;
            resources.ApplyResources(this.cbDivType, "cbDivType");
            this.cbDivType.Name = "cbDivType";
            // 
            // nudFont
            // 
            resources.ApplyResources(this.nudFont, "nudFont");
            this.nudFont.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudFont.Name = "nudFont";
            this.nudFont.ValueChanged += new System.EventHandler(this.nudFont_ValueChanged);
            // 
            // nudLine
            // 
            resources.ApplyResources(this.nudLine, "nudLine");
            this.nudLine.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudLine.Name = "nudLine";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // cbTab2
            // 
            this.cbTab2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTab2.FormattingEnabled = true;
            resources.ApplyResources(this.cbTab2, "cbTab2");
            this.cbTab2.Name = "cbTab2";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // cbTab1
            // 
            this.cbTab1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTab1.FormattingEnabled = true;
            resources.ApplyResources(this.cbTab1, "cbTab1");
            this.cbTab1.Name = "cbTab1";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // rbRight
            // 
            resources.ApplyResources(this.rbRight, "rbRight");
            this.rbRight.Name = "rbRight";
            this.rbRight.BoxBackColor = System.Drawing.Color.White;
            this.rbRight.UseVisualStyleBackColor = true;
            // 
            // rbCenter
            // 
            resources.ApplyResources(this.rbCenter, "rbCenter");
            this.rbCenter.Checked = true;
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.BoxBackColor = System.Drawing.Color.White;
            this.rbCenter.TabStop = true;
            this.rbCenter.UseVisualStyleBackColor = true;
            // 
            // rbLeft
            // 
            resources.ApplyResources(this.rbLeft, "rbLeft");
            this.rbLeft.Name = "rbLeft";
            this.rbLeft.BoxBackColor = System.Drawing.Color.White;
            this.rbLeft.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // nudEnd
            // 
            resources.ApplyResources(this.nudEnd, "nudEnd");
            this.nudEnd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEnd.Name = "nudEnd";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // nudStart
            // 
            resources.ApplyResources(this.nudStart, "nudStart");
            this.nudStart.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStart.Name = "nudStart";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cbColumnFill
            // 
            this.cbColumnFill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnFill.FormattingEnabled = true;
            resources.ApplyResources(this.cbColumnFill, "cbColumnFill");
            this.cbColumnFill.Name = "cbColumnFill";
            // 
            // tbColumnKey
            // 
            this.tbColumnKey.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbColumnKey.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbColumnKey, "tbColumnKey");
            this.tbColumnKey.Name = "tbColumnKey";
            // 
            // tbColumnName
            // 
            this.tbColumnName.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbColumnName.HighlightColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.tbColumnName, "tbColumnName");
            this.tbColumnName.Name = "tbColumnName";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
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
            // bColumnDelete
            // 
            resources.ApplyResources(this.bColumnDelete, "bColumnDelete");
            this.bColumnDelete.Name = "bColumnDelete";
            this.bColumnDelete.UseVisualStyleBackColor = true;
            this.bColumnDelete.Click += new System.EventHandler(this.bColumnDelete_Click);
            // 
            // bColumnEdit
            // 
            resources.ApplyResources(this.bColumnEdit, "bColumnEdit");
            this.bColumnEdit.Name = "bColumnEdit";
            this.bColumnEdit.UseVisualStyleBackColor = true;
            this.bColumnEdit.Click += new System.EventHandler(this.bColumnEdit_Click);
            // 
            // bColumnAdd
            // 
            resources.ApplyResources(this.bColumnAdd, "bColumnAdd");
            this.bColumnAdd.Name = "bColumnAdd";
            this.bColumnAdd.UseVisualStyleBackColor = true;
            this.bColumnAdd.Click += new System.EventHandler(this.bColumnAdd_Click);
            // 
            // listColumns
            // 
            this.listColumns.FormattingEnabled = true;
            resources.ApplyResources(this.listColumns, "listColumns");
            this.listColumns.Name = "listColumns";
            this.listColumns.SelectedIndexChanged += new System.EventHandler(this.listColumns_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudMinHeight);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.nudMaxRecCount);
            this.groupBox3.Controls.Add(this.bSetAll);
            this.groupBox3.Controls.Add(this.nudSize);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.nudWidth);
            this.groupBox3.Controls.Add(this.nudHeight);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.bRowEdit);
            this.groupBox3.Controls.Add(this.listRows);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // nudMinHeight
            // 
            resources.ApplyResources(this.nudMinHeight, "nudMinHeight");
            this.nudMinHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinHeight.Name = "nudMinHeight";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // bSetAll
            // 
            resources.ApplyResources(this.bSetAll, "bSetAll");
            this.bSetAll.Name = "bSetAll";
            this.bSetAll.UseVisualStyleBackColor = true;
            this.bSetAll.Click += new System.EventHandler(this.bSetAll_Click);
            // 
            // nudSize
            // 
            resources.ApplyResources(this.nudSize, "nudSize");
            this.nudSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            // 
            // nudWidth
            // 
            resources.ApplyResources(this.nudWidth, "nudWidth");
            this.nudWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            // 
            // nudHeight
            // 
            resources.ApplyResources(this.nudHeight, "nudHeight");
            this.nudHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Validating += new System.ComponentModel.CancelEventHandler(this.nudHeight_Validating);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // bRowEdit
            // 
            resources.ApplyResources(this.bRowEdit, "bRowEdit");
            this.bRowEdit.Name = "bRowEdit";
            this.bRowEdit.UseVisualStyleBackColor = true;
            this.bRowEdit.Click += new System.EventHandler(this.bRowEdit_Click);
            // 
            // listRows
            // 
            this.listRows.FormattingEnabled = true;
            resources.ApplyResources(this.listRows, "listRows");
            this.listRows.Name = "listRows";
            this.listRows.SelectedIndexChanged += new System.EventHandler(this.listRows_SelectedIndexChanged);
            this.listRows.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listRows_Format);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.bSetColumnOrder);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // bSetColumnOrder
            // 
            resources.ApplyResources(this.bSetColumnOrder, "bSetColumnOrder");
            this.bSetColumnOrder.Name = "bSetColumnOrder";
            this.bSetColumnOrder.UseVisualStyleBackColor = true;
            this.bSetColumnOrder.Click += new System.EventHandler(this.bSetColumnOrder_Click);
            // 
            // FTableCatalog
            // 
            this.AcceptButton = this.bSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bStorno;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bStorno);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTableCatalog";
            this.ShowInTaskbar = false;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FTableCatalog_HelpButtonClicked);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRecCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExGroupBox groupBox1;
        private ExComboBox cbManufacturer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ExTextBox tbKey;
        private ExTextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bStorno;
        private ExGroupBox groupBox2;
        private ExComboBox cbColumnFill;
        private ExTextBox tbColumnKey;
        private ExTextBox tbColumnName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bColumnDelete;
        private System.Windows.Forms.Button bColumnEdit;
        private System.Windows.Forms.Button bColumnAdd;
        private System.Windows.Forms.ListBox listColumns;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private ExComboBox cbTab2;
        private System.Windows.Forms.Label label11;
        private ExComboBox cbTab1;
        private System.Windows.Forms.Label label10;
        private ExRadioButton rbRight;
        private ExRadioButton rbCenter;
        private ExRadioButton rbLeft;
        private System.Windows.Forms.Label label9;
        private ExNumericUpDown nudEnd;
        private System.Windows.Forms.Label label8;
        private ExNumericUpDown nudStart;
        private System.Windows.Forms.Label label7;
        private ExNumericUpDown nudLine;
        private ExNumericUpDown nudMaxRecCount;
        private System.Windows.Forms.Label label16;
        private ExGroupBox groupBox3;
        private ExNumericUpDown nudSize;
        private ExNumericUpDown nudWidth;
        private ExNumericUpDown nudHeight;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button bRowEdit;
        private System.Windows.Forms.ListBox listRows;
        private ExGroupBox groupBox4;
        private ExTextBox tbComment;
        private ExNumericUpDown nudFont;
        private ExComboBox cbDivType;
        private System.Windows.Forms.Button bSetAll;
        private ExGroupBox groupBox5;
        private System.Windows.Forms.Button bSetColumnOrder;
        private System.Windows.Forms.Label label15;
        private ExNumericUpDown nudMinHeight;
        private System.Windows.Forms.Label label20;
    }
}