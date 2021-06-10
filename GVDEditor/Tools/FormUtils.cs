using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using ExControls;
using GVDEditor.Controls;
using ScintillaNET;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda obsahujúca metódy na správu Formu
    /// </summary>
    internal static class FormUtils
    {
        /// <summary>
        ///     Zmena farebnej schémy dialogu alebo ovladacieho prvku
        /// </summary>
        /// <param name="c">form alobo ovladaci prvok</param>
        public static void ApplyTheme(this Control c)
        {
            var style = GlobData.UsingStyle;
            c.BackColor = style.ControlsColorScheme.Panel.BackColor;
            if (c is Form f && style.DarkTitleBar)
            {
                f.SetImmersiveDarkMode(true);
            }

            ChangeColorComponents(style, c.Controls);
        }

        public static void ChangeColorContextMenu(XML.Style style, ContextMenuStrip strip)
        {
            if (style.ControlsDefaultStyle)
            {
                strip.RenderMode = ToolStripRenderMode.Professional;
                strip.Renderer = new ToolStripProfessionalRenderer(new LightColorTable());
                return;
            }

            strip.RenderMode = ToolStripRenderMode.Professional;
            strip.Renderer = new MyMenuRenderer(new MyColorTable());

            strip.BackColor = style.ControlsColorScheme.Panel.BackColor;
            strip.ForeColor = style.ControlsColorScheme.Panel.ForeColor;

            SetColorMenuItems(strip.Items);

            void SetColorMenuItems(IEnumerable coll)
            {
                foreach (ToolStripItem toolStripItem in coll)
                {
                    toolStripItem.BackColor = style.ControlsColorScheme.Panel.BackColor;
                    toolStripItem.ForeColor = style.ControlsColorScheme.Panel.ForeColor;
                    if (toolStripItem is ToolStripMenuItem tsmi) SetColorMenuItems(tsmi.DropDown.Items);
                }
            }
        }

        public static void ChangeColorComponents(XML.Style style, IEnumerable collection)
        {
            var scheme = style.ControlsColorScheme;
            foreach (Control component in collection)
                switch (component)
                {
                    case MyScintilla sc:
                    {
                        sc.StyleResetDefault();
                        sc.Styles[Style.Default].Font = GlobData.UsingStyle.TabTabEditorScheme.Font.Name;
                        sc.Styles[Style.Default].SizeF = GlobData.UsingStyle.TabTabEditorScheme.Font.Size;
                        sc.Styles[Style.Default].BackColor = scheme.Box.BackColor;
                        sc.StyleClearAll();
                        sc.Styles[Style.LineNumber].BackColor = scheme.Button.BackColor;
                        sc.Styles[Style.LineNumber].ForeColor = scheme.Button.ForeColor;
                        sc.CaretForeColor = scheme.Box.ForeColor;
                        sc.SetSelectionBackColor(true,scheme.Border.ForeColor);
                        if (!style.ControlsDefaultStyle)
                        {
                            sc.BorderStyle = BorderStyle.None;
                        }
                        if (style.DarkScrollBar)
                        {
                            sc.VScrollBarControl.SetTheme(WindowsTheme.DarkExplorer);
                            sc.HScrollBarControl.SetTheme(WindowsTheme.DarkExplorer);
                        }

                        break;
                    }
                    case ExCheckedListBox dclb:
                    {
                        dclb.DefaultStyle = style.ControlsDefaultStyle;
                        if (!style.ControlsDefaultStyle)
                        {
                            dclb.BackColor = scheme.Box.BackColor;
                            dclb.ForeColor = scheme.Box.ForeColor;
                            dclb.BorderColor = scheme.Border.ForeColor;
                            dclb.SquareBackColor = scheme.Box.BackColor;
                            dclb.MarkColor = scheme.Mark.ForeColor;
                            dclb.BorderStyle = BorderStyle.None;
                            dclb.FocusedBackColor = scheme.Border.ForeColor;
                            dclb.HighlightColor = scheme.Highlight.BackColor;
                        }
                        
                        if (style.DarkScrollBar)
                        {
                            dclb.SetTheme(WindowsTheme.DarkExplorer);
                        }
                        break;
                    }
                    case ExGroupBox dgb:
                    {
                        ChangeColorComponents(style, dgb.Controls);
                        dgb.DefaultStyle = style.ControlsDefaultStyle;
                        dgb.BackColor = scheme.Panel.BackColor;
                        dgb.ForeColor = scheme.Panel.ForeColor;
                        dgb.BorderThickness = 1;
                        dgb.BorderColor = scheme.Border.ForeColor;
                        break;
                    }
                    case Panel:
                    {
                        ChangeColorComponents(style, component.Controls);
                        component.BackColor = scheme.Panel.BackColor;
                        component.ForeColor = scheme.Panel.ForeColor;
                        break;
                    }
                    case SplitContainer sc:
                    {
                        component.BackColor = scheme.Panel.BackColor;
                        component.ForeColor = scheme.Panel.ForeColor;
                        ChangeColorComponents(style, sc.Panel1.Controls);
                        ChangeColorComponents(style, sc.Panel2.Controls);
                        break;
                    }
                    case ExTabControl tab:
                    {
                        ChangeColorComponents(style, component.Controls);
                        tab.DefaultStyle = style.ControlsDefaultStyle;
                        component.BackColor = scheme.Panel.BackColor;
                        component.ForeColor = scheme.Panel.ForeColor;
                        tab.HeaderBackColor = scheme.Box.BackColor;
                        tab.HeaderForeColor = scheme.Box.ForeColor;
                        tab.ActiveHeaderBackColor = scheme.Panel.BackColor;
                        tab.ActiveHeaderForeColor = scheme.Panel.ForeColor;
                        tab.BorderColor = scheme.Border.ForeColor;
                        tab.HighlightBackColor = scheme.Button.BackColor;
                        tab.HighlightForeColor = scheme.Button.ForeColor;
                        break;
                    }
                    case Button b:
                    {
                        if (!style.ControlsDefaultStyle)
                        {
                            component.BackColor = scheme.Button.BackColor;
                            component.ForeColor = scheme.Button.ForeColor;
                            b.FlatStyle = FlatStyle.Flat;
                            b.FlatAppearance.BorderColor = scheme.Border.ForeColor;
                            b.FlatAppearance.MouseOverBackColor = scheme.Highlight.BackColor;
                            b.FlatAppearance.MouseDownBackColor = scheme.Highlight.BackColor;
                        }
                        break;
                    }
                    case ExLineSeparator dls:
                    {
                        dls.LineColor = scheme.Border.ForeColor;
                        break;
                    }
                    case ExMaskedTextBox tb:
                    {
                        tb.DefaultStyle = style.ControlsDefaultStyle;
                        if (!style.ControlsDefaultStyle)
                        {
                            tb.BackColor = scheme.Box.BackColor;
                            tb.ForeColor = scheme.Box.ForeColor;
                            tb.BorderColor = scheme.Border.ForeColor;
                            tb.HighlightColor = scheme.Highlight.BackColor;
                            tb.DisabledBorderColor = scheme.Button.BackColor;
                        }
                        break;
                    }
                    case ExTextBox tb:
                    {
                        tb.BackColor = scheme.Box.BackColor;
                        tb.ForeColor = scheme.Box.ForeColor;
                        tb.BorderColor = scheme.Border.ForeColor;
                        tb.HighlightColor = scheme.Highlight.BackColor;
                        tb.DefaultStyle = style.ControlsDefaultStyle;
                        tb.DisabledBorderColor = scheme.Button.BackColor;
                        tb.DisabledBackColor = scheme.Panel.BackColor;
                        break;
                    }
                    case RichTextBox tb:
                    {
                        if (!style.ControlsDefaultStyle)
                        {
                            tb.BackColor = scheme.Box.BackColor;
                            tb.ForeColor = scheme.Box.ForeColor;
                            tb.BorderStyle = BorderStyle.None;
                        }
                        break;
                    }
                    case ExNumericUpDown nud:
                    {
                        nud.DefaultStyle = style.ControlsDefaultStyle;
                        if (!style.ControlsDefaultStyle)
                        {
                            nud.BackColor = scheme.Box.BackColor;
                            nud.ForeColor = scheme.Box.ForeColor;
                            nud.ArrowsColor = scheme.Button.ForeColor;
                            nud.BorderColor = scheme.Border.ForeColor;
                            nud.HighlightColor = scheme.Highlight.BackColor;
                            nud.SelectedButtonColor = scheme.Highlight.BackColor;
                        }
                        break;
                    }
                    case ExDateTimePicker dtp:
                    {
                        dtp.DefaultStyle = style.ControlsDefaultStyle;
                        dtp.BackColor = scheme.Button.BackColor;
                        dtp.ForeColor = scheme.Button.ForeColor;
                        dtp.BorderColor = scheme.Border.ForeColor;
                        dtp.ArrowColor = scheme.Box.ForeColor;
                        dtp.HighlightColor = scheme.Highlight.BackColor;
                        break;
                    }
                    case DateTimePicker dtp:
                    {
                        dtp.BackColor = scheme.Box.BackColor;
                        dtp.ForeColor = scheme.Box.ForeColor;
                        dtp.CalendarMonthBackground = scheme.Box.BackColor;
                        dtp.CalendarForeColor = scheme.Box.ForeColor;
                        break;
                    }
                    case LinkLabel ll:
                    {
                        ll.LinkColor = scheme.Highlight.BackColor;
                        break;
                    }
                    case Label l:
                    {
                        l.ForeColor = scheme.Label.ForeColor;
                        break;
                    }
                    case TreeView tv:
                    {
                        component.BackColor = scheme.Box.BackColor;
                        component.ForeColor = scheme.Box.ForeColor;
                        if (!style.ControlsDefaultStyle)
                        {
                            tv.BorderStyle = BorderStyle.None;
                        }
                        if (style.DarkScrollBar)
                        {
                            tv.SetTheme(WindowsTheme.DarkExplorer);
                        }
                        break;
                    }
                    case ListBox lb:
                    {
                        component.BackColor = scheme.Box.BackColor;
                        component.ForeColor = scheme.Box.ForeColor;
                        if (!style.ControlsDefaultStyle)
                        {
                            lb.BorderStyle = BorderStyle.None;
                        }
                        if (style.DarkScrollBar)
                        {
                            lb.SetTheme(WindowsTheme.DarkExplorer);
                        }
                        break;
                    }
                    case ExCheckBox cb:
                    {
                        cb.DefaultStyle = style.ControlsDefaultStyle;
                        if (!style.ControlsDefaultStyle)
                        {
                            cb.BorderColor = scheme.Border.ForeColor;
                            cb.BoxBackColor = scheme.Box.BackColor;
                            cb.MarkColor = scheme.Mark.ForeColor;
                            cb.HighlightColor = scheme.Highlight.BackColor;
                        }
                        break;
                    }
                    case ExComboBox dcombo:
                    {
                        dcombo.DefaultStyle = dcombo.DefaultStyle && style.ControlsDefaultStyle;
                        if (!dcombo.DefaultStyle)
                        {
                            dcombo.BackColor = scheme.Box.BackColor;
                            dcombo.ForeColor = scheme.Box.ForeColor;
                            dcombo.DropDownSelectedRowBackColor = scheme.Highlight.BackColor;
                            dcombo.DropDownBackColor = scheme.Panel.BackColor;
                            dcombo.StyleNormal.BorderColor = scheme.Border.ForeColor;
                            dcombo.StyleNormal.ArrowColor = scheme.Box.ForeColor;
                            dcombo.StyleNormal.ButtonBorderColor = scheme.Box.BackColor;
                            dcombo.StyleSelected.BorderColor = scheme.Highlight.BackColor;
                            dcombo.StyleSelected.ButtonRenderFirst = false;
                            dcombo.StyleSelected.ButtonBackColor = scheme.Highlight.BackColor;
                            dcombo.StyleSelected.ButtonBorderColor = scheme.Highlight.BackColor;
                            dcombo.StyleHighlight.BorderColor = scheme.Highlight.BackColor;
                            dcombo.StyleHighlight.ButtonBorderColor = scheme.Highlight.BackColor;
                        }
                        break;
                    }
                    case ExRadioButton rb:
                    {
                        rb.DefaultStyle = style.ControlsDefaultStyle;
                        if (!style.ControlsDefaultStyle)
                        {
                            rb.BorderColor = scheme.Mark.ForeColor;
                            rb.BoxBackColor = scheme.Panel.BackColor;
                            rb.MarkColor = scheme.Mark.ForeColor;
                            rb.HighlightColor = scheme.Highlight.BackColor;
                        }
                        break;
                    }
                    case MenuStrip ms:
                    {
                        if (style.ControlsDefaultStyle)
                        {
                            ms.RenderMode = ToolStripRenderMode.Professional;
                            ms.Renderer = new ToolStripProfessionalRenderer(new LightColorTable());
                        }
                        else
                        {
                            ms.BackColor = scheme.Panel.BackColor;
                            ms.ForeColor = scheme.Panel.ForeColor;
                            ms.RenderMode = ToolStripRenderMode.Professional;
                            ms.Renderer = new MyMenuRenderer(new MyColorTable());
                        }

                        SetColorMenuItems(ms.Items);

                        void SetColorMenuItems(ToolStripItemCollection coll)
                        {
                            foreach (ToolStripItem toolStripItem in coll)
                            {
                                toolStripItem.BackColor = scheme.Panel.BackColor;
                                toolStripItem.ForeColor = scheme.Panel.ForeColor;
                                switch (toolStripItem)
                                {
                                    case ToolStripMenuItem tsmi:
                                        SetColorMenuItems(tsmi.DropDown.Items);
                                        break;
                                    case ToolStripExComboBox tcb:
                                    {
                                        tcb.ComboBox.DefaultStyle = style.ControlsDefaultStyle;
                                        if (!style.ControlsDefaultStyle)
                                        {
                                            tcb.ComboBox.BackColor = scheme.Button.BackColor;
                                            tcb.ComboBox.ForeColor = scheme.Button.ForeColor;
                                            tcb.ComboBox.DropDownSelectedRowBackColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.DropDownBackColor = scheme.Panel.BackColor;
                                            tcb.ComboBox.StyleNormal.BorderColor = scheme.Border.ForeColor;
                                            tcb.ComboBox.StyleNormal.ArrowColor = scheme.Box.ForeColor;
                                            tcb.ComboBox.StyleSelected.BorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleHighlight.BorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleSelected.ButtonRenderFirst = false;
                                            tcb.ComboBox.StyleSelected.ButtonBackColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleSelected.ButtonBorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleHighlight.ButtonBorderColor = scheme.Highlight.BackColor;
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                    }
                    case ToolStrip ts:
                    {
                        if (style.ControlsDefaultStyle)
                        {
                            ts.RenderMode = ToolStripRenderMode.Professional;
                            ts.Renderer = new ToolStripProfessionalRenderer(new LightColorTable());
                        }
                        else
                        {
                            ts.BackColor = scheme.Panel.BackColor;
                            ts.ForeColor = scheme.Panel.ForeColor;
                            ts.RenderMode = ToolStripRenderMode.Professional;
                            ts.Renderer = new MyMenuRenderer(new MyColorTable());
                        }
                        

                        SetColorMenuItems(ts.Items);

                        void SetColorMenuItems(ToolStripItemCollection coll)
                        {
                            foreach (ToolStripItem toolStripItem in coll)
                            {
                                toolStripItem.BackColor = scheme.Panel.BackColor;
                                toolStripItem.ForeColor = scheme.Panel.ForeColor;
                                switch (toolStripItem)
                                {
                                    case ToolStripMenuItem tsmi:
                                        SetColorMenuItems(tsmi.DropDown.Items);
                                        break;
                                    case ToolStripSplitButton tssb:
                                        SetColorMenuItems(tssb.DropDownItems);
                                        break;
                                    case ToolStripExComboBox tcb:
                                        tcb.ComboBox.DefaultStyle = style.ControlsDefaultStyle;
                                        if (!style.ControlsDefaultStyle)
                                        {
                                            tcb.ComboBox.BackColor = scheme.Button.BackColor;
                                            tcb.ComboBox.ForeColor = scheme.Button.ForeColor;
                                            tcb.ComboBox.DropDownSelectedRowBackColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.DropDownBackColor = scheme.Panel.BackColor;
                                            tcb.ComboBox.StyleNormal.BorderColor = scheme.Border.ForeColor;
                                            tcb.ComboBox.StyleNormal.ArrowColor = scheme.Box.ForeColor;
                                            tcb.ComboBox.StyleSelected.BorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleHighlight.BorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleSelected.ButtonRenderFirst = false;
                                            tcb.ComboBox.StyleSelected.ButtonBackColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleSelected.ButtonBorderColor = scheme.Highlight.BackColor;
                                            tcb.ComboBox.StyleHighlight.ButtonBorderColor = scheme.Highlight.BackColor;
                                        }
                                        break;
                                }
                            }
                        }

                        break;
                    }
                    case DataGridView dgv:
                    {
                        dgv.EnableHeadersVisualStyles = style.ControlsDefaultStyle;

                        if (!style.ControlsDefaultStyle)
                        {
                            dgv.ColumnHeadersDefaultCellStyle.BackColor = scheme.Button.BackColor;
                            dgv.ColumnHeadersDefaultCellStyle.ForeColor = scheme.Button.ForeColor;
                            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                            dgv.RowHeadersDefaultCellStyle.BackColor = scheme.Button.BackColor;
                            dgv.RowHeadersDefaultCellStyle.ForeColor = scheme.Button.ForeColor;
                            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                            dgv.DefaultCellStyle.BackColor = scheme.Box.BackColor;
                            dgv.DefaultCellStyle.ForeColor = scheme.Box.ForeColor;

                            dgv.ForeColor = scheme.Panel.ForeColor;
                            dgv.BackColor = scheme.Panel.BackColor;
                            dgv.BackgroundColor = scheme.Box.BackColor;
                            dgv.GridColor = scheme.Border.ForeColor;

                            dgv.BorderStyle = BorderStyle.None;
                        }

                        foreach (DataGridViewColumn column in dgv.Columns)
                            switch (column)
                            {
                                case DataGridViewButtonColumn cbutton:
                                {
                                    if (!style.ControlsDefaultStyle)
                                    {
                                        cbutton.FlatStyle = FlatStyle.Flat;
                                        cbutton.DefaultCellStyle.ForeColor = scheme.Button.ForeColor;
                                        cbutton.DefaultCellStyle.BackColor = scheme.Button.BackColor;
                                        cbutton.DefaultCellStyle.SelectionBackColor = scheme.Highlight.BackColor;
                                        cbutton.DefaultCellStyle.SelectionForeColor = scheme.Highlight.ForeColor;
                                    }
                                    break;
                                }
                                case DataGridViewExComboBoxColumn dccb:
                                {
                                    dccb.DefaultStyle = style.ControlsDefaultStyle;
                                    if (!style.ControlsDefaultStyle)
                                    {
                                        dccb.DropDownBackColor = scheme.Panel.BackColor;
                                        dccb.DropDownSelectedBackColor = scheme.Highlight.BackColor;
                                        dccb.StyleNormal.ForeColor = scheme.Button.ForeColor;
                                        dccb.StyleNormal.BackColor = scheme.Button.BackColor;
                                        dccb.StyleNormal.BorderColor = scheme.Border.ForeColor;
                                        dccb.StyleNormal.ArrowColor = scheme.Box.ForeColor;
                                        dccb.StyleNormal.ButtonBackColor = scheme.Button.BackColor;
                                        dccb.StyleNormal.ButtonBorderColor = scheme.Button.BackColor;
                                        dccb.StyleSelected.BorderColor = scheme.Highlight.BackColor;
                                        dccb.StyleSelected.ButtonRenderFirst = false;
                                        dccb.StyleSelected.ButtonBackColor = scheme.Highlight.BackColor;
                                        dccb.StyleSelected.ButtonBorderColor = scheme.Highlight.BackColor;
                                        dccb.StyleHighlight.BorderColor = scheme.Highlight.BackColor;
                                        dccb.StyleHighlight.ButtonBorderColor = scheme.Highlight.BackColor;
                                    }
                                    break;
                                }
                                case DataGridViewExCheckBoxColumn dccb:
                                {
                                    dccb.DefaultStyle = style.ControlsDefaultStyle;
                                    if (!style.ControlsDefaultStyle)
                                    {
                                        dccb.BorderColor = scheme.Border.ForeColor;
                                        dccb.MarkColor = scheme.Mark.ForeColor;
                                        dccb.SquareBackColor = scheme.Panel.BackColor;
                                        dccb.HighlightColor = scheme.Highlight.BackColor;
                                    }
                                    break;
                                }
                                case DataGridViewCheckBoxColumn ccb:
                                {
                                    var template = new DataGridViewExCheckBoxCell
                                    {
                                        DefaultStyle = style.ControlsDefaultStyle,
                                        BorderColor = scheme.Border.ForeColor,
                                        MarkColor = scheme.Mark.ForeColor,
                                        SquareBackColor = scheme.Panel.BackColor,
                                        HighlightColor = scheme.Highlight.BackColor
                                    };
                                    ccb.CellTemplate = template;
                                    break;
                                }
                            }

                        if (style.DarkScrollBar)
                        {
                            foreach (Control dgvc in dgv.Controls)
                            {
                                if (dgvc is ScrollBar sc)
                                {
                                    sc.SetTheme(WindowsTheme.DarkExplorer);
                                }
                            }
                        }

                        break;
                    }
                }
        }

        /// <summary>
        ///     Zmení písmo Formu
        /// </summary>
        /// <param name="form">upravovaný Form</param>
        public static void SetFormFont(Form form)
        {
            form.Font = GlobData.Config.Fonts.Labels.Font;
            ChangeComponentsFont(form.Controls);

            form.AutoSize = true;
        }

        private static void ChangeComponentsFont(Control.ControlCollection collection)
        {
            foreach (Control component in collection)
                switch (component)
                {
                    case GroupBox:
                        ChangeComponentsFont(component.Controls);
                        component.Font = GlobData.Config.Fonts.Labels.Font;
                        break;
                    case Panel:
                        ChangeComponentsFont(component.Controls);
                        component.Font = GlobData.Config.Fonts.Labels.Font;
                        break;
                    case TabControl:
                        ChangeComponentsFont(component.Controls);
                        component.Font = GlobData.Config.Fonts.Labels.Font;
                        break;
                    case Button:
                        component.Font = GlobData.Config.Fonts.Buttons.Font;
                        break;
                    case MenuStrip ms:
                    {
                        component.Font = GlobData.Config.Fonts.Menu.Font;

                        SetFontMenuItems(ms.Items);

                        static void SetFontMenuItems(ToolStripItemCollection coll)
                        {
                            foreach (ToolStripItem toolStripItem in coll)
                            {
                                toolStripItem.Font = GlobData.Config.Fonts.Menu.Font;
                                if (toolStripItem is ToolStripMenuItem tsmi) SetFontMenuItems(tsmi.DropDown.Items);
                            }
                        }

                        break;
                    }
                    case ToolStrip ts:
                    {
                        component.Font = GlobData.Config.Fonts.Menu.Font;
                        var size = GlobData.Config.Fonts.Menu.Font.Height;
                        ts.ImageScalingSize = new Size(size, size);

                        SetFontStripItems(ts.Items);

                        void SetFontStripItems(ToolStripItemCollection coll)
                        {
                            foreach (ToolStripItem toolStripItem in coll)
                            {
                                toolStripItem.Font = GlobData.Config.Fonts.Menu.Font;
                                if (toolStripItem is ToolStripMenuItem tsmi)
                                    SetFontStripItems(tsmi.DropDown.Items);
                                else if (toolStripItem is ToolStripSeparator sep) sep.Size = new Size(6, size + 10);
                            }
                        }

                        break;
                    }
                    case DataGridView dgv:
                        dgv.ColumnHeadersDefaultCellStyle.Font = GlobData.Config.Fonts.ColsHeader.Font;
                        dgv.DefaultCellStyle.Font = GlobData.Config.Fonts.TableCells.Font;
                        break;
                }
        }

        public class ColorScheme
        {
            public Color PanelBG { get; set; }
            public Color PanelFG { get; set; }

            public Color ButtonBG { get; set; }
            public Color ButtonFG { get; set; }

            public Color LabelFG { get; set; }
            public Color BorderBG { get; set; }

            public Color BoxBG { get; set; }
            public Color BoxFG { get; set; }
            public Color ComponentFG { get; set; }
            public Color Highlight { get; set; }
        }

        private class MyColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color MenuBorder => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;
            public override Color ToolStripBorder => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;
            public override Color SeparatorDark => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;
            public override Color SeparatorLight => Color.Transparent;
            public override Color MenuItemBorder => Color.Transparent;
            public override Color GripDark => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;
            public override Color GripLight => Color.Transparent;
            public override Color MenuStripGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;
            public override Color MenuStripGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Border.ForeColor;

            public override Color ToolStripDropDownBackground => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            public override Color ToolStripGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            public override Color ToolStripGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            public override Color ToolStripGradientMiddle => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;

            public override Color MenuItemSelectedGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color MenuItemSelectedGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;

            public override Color MenuItemPressedGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color MenuItemPressedGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;

            public override Color ImageMarginGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            public override Color ImageMarginGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            public override Color ImageMarginGradientMiddle => GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;

            public override Color ButtonSelectedHighlight => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonSelectedHighlightBorder => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;

            public override Color ButtonSelectedGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonSelectedGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonSelectedGradientMiddle => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonSelectedBorder => Color.Transparent;

            public override Color ButtonPressedGradientBegin => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonPressedGradientEnd => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonPressedGradientMiddle => GlobData.UsingStyle.ControlsColorScheme.Highlight.BackColor;
            public override Color ButtonPressedBorder => Color.Transparent;

            public override Color CheckBackground => GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;
        }

        public class LightColorTable : ProfessionalColorTable
        {
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
        }

        public class MyMenuRenderer : ToolStripProfessionalRenderer
        {
            public MyMenuRenderer(ProfessionalColorTable table) : base(table)
            {
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                if (e.Item.Enabled) e.ArrowColor = GlobData.UsingStyle.ControlsColorScheme.Button.ForeColor;

                base.OnRenderArrow(e);
            }
        }
    }
}