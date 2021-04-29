using System.Windows.Forms;
using ScintillaNET;

namespace GVDEditor.Controls
{
    internal class MyScintilla : Scintilla
    {
        private bool _hScrollBar;
        private bool _vScrollBar;

        public VScrollBar VScrollBarControl { get; }

        public HScrollBar HScrollBarControl { get; }

        public new bool VScrollBar
        {
            get => _vScrollBar;
            set
            {
                _vScrollBar = value;
                VScrollBarControl.Visible = value;
            }
        }

        public new bool HScrollBar
        {
            get => _hScrollBar;
            set
            {
                _hScrollBar = value;
                _hScrollBar = false;
                HScrollBarControl.Visible = false;
            }
        }

        public MyScintilla()
        {
            base.HScrollBar = false;
            base.VScrollBar = false;
            
            VScrollBarControl = new VScrollBar();
            HScrollBarControl = new HScrollBar {Visible = false}; //TODO Horizontal scrollbar NOT WORKING

            VScrollBarControl.Scroll += VScrollBarControlOnScroll;
            HScrollBarControl.Scroll += HScrollBarControlOnScroll;

            Controls.Add(VScrollBarControl);
            Controls.Add(HScrollBarControl);

            VScrollBarControl.Dock = DockStyle.Right;
            HScrollBarControl.Dock = DockStyle.Bottom;

            VScrollBarControl.Cursor = Cursors.Default;
            HScrollBarControl.Cursor = Cursors.Default;

            Padding padding = HScrollBarControl.Padding;
            padding.Right = HScrollBarControl.Height;
            HScrollBarControl.Margin = padding;
        }

        private void VScrollBarControlOnScroll(object sender, ScrollEventArgs e)
        {
            FirstVisibleLine = VScrollBarControl.Value;
        }

        private void HScrollBarControlOnScroll(object sender, ScrollEventArgs e)
        {
            XOffset = HScrollBarControl.Value;
        }

        /// <summary>
        /// Raises the <see cref="E:ScintillaNET.Scintilla.UpdateUI" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:ScintillaNET.UpdateUIEventArgs" /> that contains the event data.</param>
        protected override void OnUpdateUI(UpdateUIEventArgs e)
        {
            base.OnUpdateUI(e);

            if ((e.Change & UpdateChange.VScroll) != 0)
            {
                VScrollBarControl.Minimum = 0;
                VScrollBarControl.Maximum = Lines.Count;
                VScrollBarControl.LargeChange = LinesOnScreen;
                VScrollBarControl.Value = FirstVisibleLine;
            }
            if ((e.Change & UpdateChange.HScroll) != 0)  //TODO Horizontal scrollbar doesnt work properly
            {
                /*int largest = LargestLine();
                int larW = Lines[largest].Length;
                int textareaW = GetColumn(Lines[largest].EndPosition - 1);

                HScrollBarControl.Minimum = 0;
                HScrollBarControl.Maximum = larW - textareaW;
                HScrollBarControl.Value = XOffset;*/
            }

            if ((e.Change & UpdateChange.Content) != 0)
            {
                if (Lines.Count < LinesOnScreen)
                {
                    VScrollBarControl.Visible = false;
                }
                else
                {
                    VScrollBarControl.Visible = true;
                    VScrollBarControl.Minimum = 0;
                    VScrollBarControl.Maximum = Lines.Count;
                    VScrollBarControl.LargeChange = LinesOnScreen;
                    VScrollBarControl.Value = FirstVisibleLine;
                }

                /*int largest = LargestLine();
                int larW = Lines[largest].Length;
                int textareaW = GetColumn(Lines[largest].EndPosition - 1);

                if (larW < textareaW)
                {
                    HScrollBarControl.Visible = false;
                }
                else
                {
                    HScrollBarControl.Visible = true;
                    HScrollBarControl.Minimum = 0;
                    HScrollBarControl.Maximum = larW - textareaW;
                    
                    HScrollBarControl.Value = XOffset;
                }*/
            }
        }

        public int LargestLine()
        {
            int index = 0;
            int len = 0;
            foreach (Line line in Lines)
            {
                if (line.Length > len)
                {
                    index = line.Index;
                    len = line.Length;
                }
            }

            return index;
        }
    }
}
