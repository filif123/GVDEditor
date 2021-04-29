using ExControls;
using GVDEditor.Entities;

namespace GVDEditor.Forms
{
    partial class FTableText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTableText));
            this.bSave = new System.Windows.Forms.Button();
            this.bStorno = new System.Windows.Forms.Button();
            this.groupBox1 = new ExGroupBox();
            this.tbKey = new ExTextBox();
            this.tbName = new ExTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new ExGroupBox();
            this.cbCatalogItem = new ExComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCatalogTable = new ExComboBox();
            this.bReDelete = new System.Windows.Forms.Button();
            this.bReEdit = new System.Windows.Forms.Button();
            this.bReAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listRealisations = new System.Windows.Forms.ListBox();
            this.groupBox3 = new ExGroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudFont = new ExNumericUpDown();
            this.bGenerate = new System.Windows.Forms.Button();
            this.bTextEdit = new System.Windows.Forms.Button();
            this.tbTrainText = new ExTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listTrains = new System.Windows.Forms.ListBox();
            this.groupBox4 = new ExGroupBox();
            this.tbComment = new ExTextBox();
            this.tableTextRealizationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableTextRealizationBindingSource)).BeginInit();
            this.SuspendLayout();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbKey);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tbKey
            // 
            resources.ApplyResources(this.tbKey, "tbKey");
            this.tbKey.Name = "tbKey";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCatalogItem);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbCatalogTable);
            this.groupBox2.Controls.Add(this.bReDelete);
            this.groupBox2.Controls.Add(this.bReEdit);
            this.groupBox2.Controls.Add(this.bReAdd);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listRealisations);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cbCatalogItem
            // 
            this.cbCatalogItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatalogItem.FormattingEnabled = true;
            resources.ApplyResources(this.cbCatalogItem, "cbCatalogItem");
            this.cbCatalogItem.Name = "cbCatalogItem";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cbCatalogTable
            // 
            this.cbCatalogTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatalogTable.FormattingEnabled = true;
            resources.ApplyResources(this.cbCatalogTable, "cbCatalogTable");
            this.cbCatalogTable.Name = "cbCatalogTable";
            this.cbCatalogTable.SelectedIndexChanged += new System.EventHandler(this.cbCatalogTable_SelectedIndexChanged);
            // 
            // bReDelete
            // 
            resources.ApplyResources(this.bReDelete, "bReDelete");
            this.bReDelete.Name = "bReDelete";
            this.bReDelete.UseVisualStyleBackColor = true;
            this.bReDelete.Click += new System.EventHandler(this.bReDelete_Click);
            // 
            // bReEdit
            // 
            resources.ApplyResources(this.bReEdit, "bReEdit");
            this.bReEdit.Name = "bReEdit";
            this.bReEdit.UseVisualStyleBackColor = true;
            this.bReEdit.Click += new System.EventHandler(this.bReEdit_Click);
            // 
            // bReAdd
            // 
            resources.ApplyResources(this.bReAdd, "bReAdd");
            this.bReAdd.Name = "bReAdd";
            this.bReAdd.UseVisualStyleBackColor = true;
            this.bReAdd.Click += new System.EventHandler(this.bReAdd_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // listRealisations
            // 
            this.listRealisations.FormattingEnabled = true;
            resources.ApplyResources(this.listRealisations, "listRealisations");
            this.listRealisations.Name = "listRealisations";
            this.listRealisations.SelectedIndexChanged += new System.EventHandler(this.listRealisations_SelectedIndexChanged);
            this.listRealisations.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listRealisations_Format);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.nudFont);
            this.groupBox3.Controls.Add(this.bGenerate);
            this.groupBox3.Controls.Add(this.bTextEdit);
            this.groupBox3.Controls.Add(this.tbTrainText);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.listTrains);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // nudFont
            // 
            resources.ApplyResources(this.nudFont, "nudFont");
            this.nudFont.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudFont.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudFont.Name = "nudFont";
            this.nudFont.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudFont.ValueChanged += new System.EventHandler(this.nudFont_ValueChanged);
            // 
            // bGenerate
            // 
            resources.ApplyResources(this.bGenerate, "bGenerate");
            this.bGenerate.Name = "bGenerate";
            this.bGenerate.UseVisualStyleBackColor = true;
            this.bGenerate.Click += new System.EventHandler(this.bGenerate_Click);
            // 
            // bTextEdit
            // 
            resources.ApplyResources(this.bTextEdit, "bTextEdit");
            this.bTextEdit.Name = "bTextEdit";
            this.bTextEdit.UseVisualStyleBackColor = true;
            this.bTextEdit.Click += new System.EventHandler(this.bTextEdit_Click);
            // 
            // tbTrainText
            // 
            resources.ApplyResources(this.tbTrainText, "tbTrainText");
            this.tbTrainText.Name = "tbTrainText";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // listTrains
            // 
            this.listTrains.FormattingEnabled = true;
            resources.ApplyResources(this.listTrains, "listTrains");
            this.listTrains.Name = "listTrains";
            this.listTrains.SelectedIndexChanged += new System.EventHandler(this.listTrains_SelectedIndexChanged);
            this.listTrains.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listTrains_Format);
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
            resources.ApplyResources(this.tbComment, "tbComment");
            this.tbComment.Name = "tbComment";
            // 
            // tableTextRealizationBindingSource
            // 
            this.tableTextRealizationBindingSource.DataSource = typeof(GVDEditor.Entities.TableTextRealization);
            // 
            // FTableText
            // 
            this.AcceptButton = this.bSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bStorno;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bStorno);
            this.Controls.Add(this.bSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTableText";
            this.ShowInTaskbar = false;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FTableText_HelpButtonClicked);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableTextRealizationBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bStorno;
        private ExGroupBox groupBox1;
        private ExTextBox tbKey;
        private ExTextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ExGroupBox groupBox2;
        private System.Windows.Forms.Button bReDelete;
        private System.Windows.Forms.Button bReEdit;
        private System.Windows.Forms.Button bReAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listRealisations;
        private ExComboBox cbCatalogItem;
        private System.Windows.Forms.Label label5;
        private ExComboBox cbCatalogTable;
        private ExGroupBox groupBox3;
        private System.Windows.Forms.Button bTextEdit;
        private ExTextBox tbTrainText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listTrains;
        private ExGroupBox groupBox4;
        private ExTextBox tbComment;
        private System.Windows.Forms.Button bGenerate;
        private System.Windows.Forms.BindingSource tableTextRealizationBindingSource;
        private System.Windows.Forms.Label label8;
        private ExNumericUpDown nudFont;
    }
}