
namespace GVDEditor.Forms
{
    partial class FAutofix
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
            ExControls.ExComboBoxStyle exComboBoxStyle1 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle2 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle3 = new ExControls.ExComboBoxStyle();
            ExControls.ExComboBoxStyle exComboBoxStyle4 = new ExControls.ExComboBoxStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bStorno = new System.Windows.Forms.Button();
            this.bContinue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.tbErrorText = new ExControls.ExTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.rbNewObject = new ExControls.ExRadioButton(this.components);
            this.bCreate = new System.Windows.Forms.Button();
            this.rbSelectObject = new ExControls.ExRadioButton(this.components);
            this.cbItems = new ExControls.ExComboBox(this.components);
            this.exLineSeparator1 = new ExControls.ExLineSeparator();
            this.rbDoNothing = new ExControls.ExRadioButton(this.components);
            this.exLineSeparator2 = new ExControls.ExLineSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lCreateResult = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.bStorno);
            this.flowLayoutPanel1.Controls.Add(this.bContinue);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 303);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(555, 53);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // bStorno
            // 
            this.bStorno.AutoSize = true;
            this.bStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bStorno.Location = new System.Drawing.Point(467, 13);
            this.bStorno.Name = "bStorno";
            this.bStorno.Size = new System.Drawing.Size(75, 27);
            this.bStorno.TabIndex = 0;
            this.bStorno.Text = "Zrušiť";
            this.bStorno.UseVisualStyleBackColor = true;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // bContinue
            // 
            this.bContinue.AutoSize = true;
            this.bContinue.Location = new System.Drawing.Point(371, 13);
            this.bContinue.Name = "bContinue";
            this.bContinue.Size = new System.Drawing.Size(90, 27);
            this.bContinue.TabIndex = 1;
            this.bContinue.Text = "Pokračovať";
            this.bContinue.UseVisualStyleBackColor = true;
            this.bContinue.Click += new System.EventHandler(this.bContinue_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Program našiel chybu pri otváraní grafikonu:";
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(6, 36);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(66, 60);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbIcon.TabIndex = 2;
            this.pbIcon.TabStop = false;
            // 
            // tbErrorText
            // 
            this.tbErrorText.BorderColor = System.Drawing.Color.DimGray;
            this.tbErrorText.DisabledBackColor = System.Drawing.SystemColors.Control;
            this.tbErrorText.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbErrorText.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            this.tbErrorText.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.tbErrorText.HintForeColor = System.Drawing.SystemColors.GrayText;
            this.tbErrorText.HintText = null;
            this.tbErrorText.Location = new System.Drawing.Point(86, 37);
            this.tbErrorText.Multiline = true;
            this.tbErrorText.Name = "tbErrorText";
            this.tbErrorText.ReadOnly = true;
            this.tbErrorText.Size = new System.Drawing.Size(457, 59);
            this.tbErrorText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Vyberte akciu:";
            // 
            // rbNewObject
            // 
            this.rbNewObject.AutoSize = true;
            this.rbNewObject.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbNewObject.Location = new System.Drawing.Point(16, 131);
            this.rbNewObject.Name = "rbNewObject";
            this.rbNewObject.Size = new System.Drawing.Size(220, 21);
            this.rbNewObject.TabIndex = 5;
            this.rbNewObject.Text = "Vytvoriť nový objekt tohto typu";
            this.rbNewObject.UseVisualStyleBackColor = true;
            this.rbNewObject.CheckedChanged += new System.EventHandler(this.rbNewObject_CheckedChanged);
            // 
            // bCreate
            // 
            this.bCreate.AutoSize = true;
            this.bCreate.Enabled = false;
            this.bCreate.Location = new System.Drawing.Point(35, 158);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(75, 27);
            this.bCreate.TabIndex = 6;
            this.bCreate.Text = "Vytvoriť";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // rbSelectObject
            // 
            this.rbSelectObject.AutoSize = true;
            this.rbSelectObject.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbSelectObject.Location = new System.Drawing.Point(15, 200);
            this.rbSelectObject.Name = "rbSelectObject";
            this.rbSelectObject.Size = new System.Drawing.Size(369, 21);
            this.rbSelectObject.TabIndex = 7;
            this.rbSelectObject.Text = "Vybrať z dosiaľ načítaných objektov rovnakého druhu:";
            this.rbSelectObject.UseVisualStyleBackColor = true;
            this.rbSelectObject.CheckedChanged += new System.EventHandler(this.rbSelectObject_CheckedChanged);
            // 
            // cbItems
            // 
            this.cbItems.DropDownSelectedRowBackColor = System.Drawing.SystemColors.Highlight;
            this.cbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItems.Enabled = false;
            this.cbItems.FormattingEnabled = true;
            this.cbItems.Location = new System.Drawing.Point(35, 227);
            this.cbItems.Name = "cbItems";
            this.cbItems.Size = new System.Drawing.Size(349, 24);
            exComboBoxStyle1.ArrowColor = null;
            exComboBoxStyle1.BackColor = null;
            exComboBoxStyle1.BorderColor = null;
            exComboBoxStyle1.ButtonBackColor = null;
            exComboBoxStyle1.ButtonBorderColor = null;
            exComboBoxStyle1.ButtonRenderFirst = null;
            exComboBoxStyle1.ForeColor = null;
            this.cbItems.StyleDisabled = exComboBoxStyle1;
            exComboBoxStyle2.ArrowColor = null;
            exComboBoxStyle2.BackColor = null;
            exComboBoxStyle2.BorderColor = null;
            exComboBoxStyle2.ButtonBackColor = null;
            exComboBoxStyle2.ButtonBorderColor = null;
            exComboBoxStyle2.ButtonRenderFirst = null;
            exComboBoxStyle2.ForeColor = null;
            this.cbItems.StyleHighlight = exComboBoxStyle2;
            exComboBoxStyle3.ArrowColor = null;
            exComboBoxStyle3.BackColor = null;
            exComboBoxStyle3.BorderColor = null;
            exComboBoxStyle3.ButtonBackColor = null;
            exComboBoxStyle3.ButtonBorderColor = null;
            exComboBoxStyle3.ButtonRenderFirst = null;
            exComboBoxStyle3.ForeColor = null;
            this.cbItems.StyleNormal = exComboBoxStyle3;
            exComboBoxStyle4.ArrowColor = null;
            exComboBoxStyle4.BackColor = null;
            exComboBoxStyle4.BorderColor = null;
            exComboBoxStyle4.ButtonBackColor = null;
            exComboBoxStyle4.ButtonBorderColor = null;
            exComboBoxStyle4.ButtonRenderFirst = null;
            exComboBoxStyle4.ForeColor = null;
            this.cbItems.StyleSelected = exComboBoxStyle4;
            this.cbItems.TabIndex = 8;
            // 
            // exLineSeparator1
            // 
            this.exLineSeparator1.Location = new System.Drawing.Point(35, 193);
            this.exLineSeparator1.Name = "exLineSeparator1";
            this.exLineSeparator1.Size = new System.Drawing.Size(495, 1);
            this.exLineSeparator1.TabIndex = 9;
            this.exLineSeparator1.Text = null;
            // 
            // rbDoNothing
            // 
            this.rbDoNothing.AutoSize = true;
            this.rbDoNothing.Checked = true;
            this.rbDoNothing.HighlightColor = System.Drawing.SystemColors.Highlight;
            this.rbDoNothing.Location = new System.Drawing.Point(15, 264);
            this.rbDoNothing.Name = "rbDoNothing";
            this.rbDoNothing.Size = new System.Drawing.Size(349, 21);
            this.rbDoNothing.TabIndex = 10;
            this.rbDoNothing.TabStop = true;
            this.rbDoNothing.Text = "Nerobiť nič (program ukončí načítavanie grafikonu)";
            this.rbDoNothing.UseVisualStyleBackColor = true;
            this.rbDoNothing.CheckedChanged += new System.EventHandler(this.rbDoNothing_CheckedChanged);
            // 
            // exLineSeparator2
            // 
            this.exLineSeparator2.Location = new System.Drawing.Point(35, 257);
            this.exLineSeparator2.Name = "exLineSeparator2";
            this.exLineSeparator2.Size = new System.Drawing.Size(495, 1);
            this.exLineSeparator2.TabIndex = 11;
            this.exLineSeparator2.Text = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbIcon);
            this.panel1.Controls.Add(this.tbErrorText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 107);
            this.panel1.TabIndex = 12;
            // 
            // lCreateResult
            // 
            this.lCreateResult.AutoSize = true;
            this.lCreateResult.Location = new System.Drawing.Point(116, 163);
            this.lCreateResult.Name = "lCreateResult";
            this.lCreateResult.Size = new System.Drawing.Size(197, 17);
            this.lCreateResult.TabIndex = 13;
            this.lCreateResult.Text = "Kliknutím vytvoríte nový objekt";
            // 
            // FAutofix
            // 
            this.AcceptButton = this.bContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.bStorno;
            this.ClientSize = new System.Drawing.Size(555, 356);
            this.Controls.Add(this.lCreateResult);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.exLineSeparator2);
            this.Controls.Add(this.rbDoNothing);
            this.Controls.Add(this.exLineSeparator1);
            this.Controls.Add(this.cbItems);
            this.Controls.Add(this.rbSelectObject);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.rbNewObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAutofix";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Rýchla oprava";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bStorno;
        private System.Windows.Forms.Button bContinue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbIcon;
        private ExControls.ExTextBox tbErrorText;
        private System.Windows.Forms.Label label2;
        private ExControls.ExRadioButton rbNewObject;
        private System.Windows.Forms.Button bCreate;
        private ExControls.ExRadioButton rbSelectObject;
        private ExControls.ExComboBox cbItems;
        private ExControls.ExLineSeparator exLineSeparator1;
        private ExControls.ExRadioButton rbDoNothing;
        private ExControls.ExLineSeparator exLineSeparator2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lCreateResult;
    }
}