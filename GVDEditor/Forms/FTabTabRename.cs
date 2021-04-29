using GVDEditor.Tools;
using System;
using System.Windows.Forms;

namespace GVDEditor.Forms
{
    internal partial class FTabTabRename : Form
    {
        public string NewTabName { get; private set; }

        public FTabTabRename()
        {
            InitializeComponent();

            FormUtils.SetFormFont(this);
            this.ApplyTheme();
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                Utils.ShowError("Nezadaný názov TabTab");
                DialogResult = DialogResult.None;
                return;
            }
            
            NewTabName = tbName.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
