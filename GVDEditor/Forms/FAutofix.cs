using GVDEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GVDEditor.Entities;

namespace GVDEditor.Forms
{
    public partial class FAutofix : Form
    {
        public AutofixResult Result { get; private set; }

        public IFixable ResultItem { get; private set; }

        private readonly string _wrongInput;
        private readonly AutofixOutputType _type;

        public FAutofix(AutofixOutputType type, string errorText, IEnumerable<IFixable> selection, string wrongInput)
        {
            InitializeComponent();
            
            FormUtils.SetFormFont(this);
            this.ApplyTheme();
            flowLayoutPanel1.BackColor = GlobData.UsingStyle.ControlsColorScheme.Box.BackColor;

            pbIcon.Image = SystemIcons.Error.ToBitmap();
            tbErrorText.Text = errorText;
            _type = type;
            _wrongInput = wrongInput;
            cbItems.DataSource = selection;
        }

        private void bContinue_Click(object sender, EventArgs e)
        {
            switch (Result)
            {
                case AutofixResult.NONE:
                    break;
                case AutofixResult.CREATE_NEW:
                    if (ResultItem is null)
                    {
                        Utils.ShowError("Nebol vytvorený žiadny nový objekt.");
                        DialogResult = DialogResult.None;
                        return;
                    }
                    break;
                case AutofixResult.SELECTION:
                    if (cbItems.SelectedItem is null)
                    {
                        Utils.ShowError("Nebol vybraný žiadny objekt.");
                        DialogResult = DialogResult.None;
                        return;
                    }

                    ResultItem = cbItems.SelectedItem as IFixable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            Result = AutofixResult.NONE;
            DialogResult = DialogResult.Cancel;
        }

        private void rbNewObject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewObject.Checked)
            {
                Result = AutofixResult.CREATE_NEW;
                ResultItem = default;
                bCreate.Enabled = true;
                cbItems.Enabled = false;
            }
        }

        private void rbSelectObject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSelectObject.Checked)
            {
                Result = AutofixResult.SELECTION;
                ResultItem = default;
                bCreate.Enabled = false;
                cbItems.Enabled = true;
            }
        }

        private void rbDoNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDoNothing.Checked)
            {
                Result = AutofixResult.NONE;
                ResultItem = default;
                bCreate.Enabled = false;
                cbItems.Enabled = false;
            }
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case AutofixOutputType.TRAIN_TYPE:
                    //ResultItem = new TrainType();
                    break;
            }
        }
    }

    public enum AutofixResult
    {
        NONE,
        CREATE_NEW,
        SELECTION
    }
}
