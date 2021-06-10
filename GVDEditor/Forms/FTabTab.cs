using GVDEditor.Entities;
using GVDEditor.Properties;
using GVDEditor.Tools;
using ScintillaNET;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ExControls;

namespace GVDEditor.Forms
{
    /// <summary>
    ///     Dialog - Nastavenie TabTabs
    /// </summary>
    public partial class FTabTab : Form
    {
        private static readonly string[] operatory = {"AND", "OR", "NOT", "ODD"};

        private readonly TabTabLexer cSharpLexer = new(
            TabTabACItems.GetFunctionItems().Select(item => item.FunctionName),
            TabTabACItems.GetEventItems().Select(item => item.MenuText),
            TabTabACItems.GetConstantItems().Select(item => item.ConstName),
            operatory);

        private int lastCaretPos;
        private int maxLineNumberCharLength;

        internal readonly BindingList<TabTabDoc> documents = new();

        private readonly TableTabTab SelectedTab;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public FTabTab(TableTabTab tab = null)
        {
            InitializeComponent();
            FormUtils.SetFormFont(this);

            if (GlobData.UsingStyle.DarkTitleBar)
            {
                ExTools.SetImmersiveDarkMode(Handle, true);
            }

            acMenu.TargetControlWrapper = new ScintillaWrapper(scText);

            foreach (var tabTab in FLocalSettings.TabTabs)
            {
                documents.Add(new TabTabDoc{ Document = CreateDocument(tabTab.Text) , TabTab = tabTab});
            }

            lbTabTabs.DataSource = documents;

            scText.EmptyUndoBuffer();

            acMenu.SetAutocompleteItems(TabTabACItems.GetItems());

            tsbUndo.Enabled = false;
            tsbRedo.Enabled = false;

            ShowNumberLines();

            SelectedTab = tab;
        }

        private void FTabTab_Load(object sender, EventArgs e)
        {
            lbTabTabs.Font = GlobData.Config.Fonts.Menu; //GlobData.UsingStyle.TabTabEditorScheme.Font;

            scText.StyleResetDefault();
            scText.Styles[Style.Default].Font = GlobData.UsingStyle.TabTabEditorScheme.Font.Name;
            scText.Styles[Style.Default].SizeF = GlobData.UsingStyle.TabTabEditorScheme.Font.Size;
            scText.StyleClearAll();

            this.ApplyTheme();
            FormUtils.ChangeColorContextMenu(GlobData.UsingStyle, conMenuScText);
            FormUtils.ChangeColorContextMenu(GlobData.UsingStyle, conMenuLbTabs);
            acMenu.Colors.BackColor = GlobData.UsingStyle.ControlsColorScheme.Panel.BackColor;
            acMenu.Colors.ForeColor = GlobData.UsingStyle.ControlsColorScheme.Panel.ForeColor;
            acMenu.Colors.SelectedForeColor = GlobData.UsingStyle.ControlsColorScheme.Panel.ForeColor;

            scText.Styles[TabTabStyle.Default].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Default.ForeColor;
            scText.Styles[TabTabStyle.Default].Bold = GlobData.UsingStyle.TabTabEditorScheme.Default.Bold;

            scText.Styles[TabTabStyle.Function].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Function.ForeColor;
            scText.Styles[TabTabStyle.Function].Bold = GlobData.UsingStyle.TabTabEditorScheme.Function.Bold;

            scText.Styles[TabTabStyle.Identifier].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Identifier.ForeColor;
            scText.Styles[TabTabStyle.Identifier].Bold = GlobData.UsingStyle.TabTabEditorScheme.Identifier.Bold;

            scText.Styles[TabTabStyle.Number].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Number.ForeColor;
            scText.Styles[TabTabStyle.Number].Bold = GlobData.UsingStyle.TabTabEditorScheme.Number.Bold;

            scText.Styles[TabTabStyle.String].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.String.ForeColor;
            scText.Styles[TabTabStyle.String].Bold = GlobData.UsingStyle.TabTabEditorScheme.String.Bold;

            scText.Styles[TabTabStyle.Comment].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Comment.ForeColor;
            scText.Styles[TabTabStyle.Comment].Bold = GlobData.UsingStyle.TabTabEditorScheme.Comment.Bold;

            scText.Styles[TabTabStyle.Var].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Var.ForeColor;
            scText.Styles[TabTabStyle.Var].Bold = GlobData.UsingStyle.TabTabEditorScheme.Var.Bold;

            scText.Styles[TabTabStyle.Event].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Event.ForeColor;
            scText.Styles[TabTabStyle.Event].Bold = GlobData.UsingStyle.TabTabEditorScheme.Event.Bold;

            scText.Styles[TabTabStyle.OnNewLine].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.OnNewLine.ForeColor;
            scText.Styles[TabTabStyle.OnNewLine].Bold = GlobData.UsingStyle.TabTabEditorScheme.OnNewLine.Bold;

            scText.Styles[TabTabStyle.Operator].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Operator.ForeColor;
            scText.Styles[TabTabStyle.Operator].Bold = GlobData.UsingStyle.TabTabEditorScheme.Operator.Bold;

            scText.Styles[TabTabStyle.Constant].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.Constant.ForeColor;
            scText.Styles[TabTabStyle.Constant].Bold = GlobData.UsingStyle.TabTabEditorScheme.Constant.Bold;

            scText.Lexer = Lexer.Container;

            //highlight active braces
            scText.IndentationGuides = IndentView.LookBoth;

            scText.Styles[Style.BraceLight].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.SelBraces.ForeColor;
            scText.Styles[Style.BraceLight].BackColor = GlobData.UsingStyle.TabTabEditorScheme.SelBraces.BackColor;
            scText.Styles[Style.BraceLight].Bold = GlobData.UsingStyle.TabTabEditorScheme.SelBraces.Bold;

            scText.Styles[Style.BraceBad].ForeColor = GlobData.UsingStyle.TabTabEditorScheme.SelBraceBad.ForeColor;
            scText.Styles[Style.BraceBad].BackColor = GlobData.UsingStyle.TabTabEditorScheme.SelBraceBad.BackColor;
            scText.Styles[Style.BraceBad].Bold = GlobData.UsingStyle.TabTabEditorScheme.SelBraceBad.Bold;

            if (SelectedTab is not null)
            {
                for (int i = 0; i < documents.Count; i++)
                {
                    if (documents[i].TabTab == SelectedTab)
                    {
                        lbTabTabs.SelectedIndex = i;
                    }
                }
            }
        }

        private void FTabTab_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool unsaved = documents.Any(doc => doc.Unsaved);

            if (unsaved)
            {
                var result = Utils.ShowQuestion(Resources.FMain_Save_Changes, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        DoSaveAll();
                        e.Cancel = false;
                        DialogResult = DialogResult.OK;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        DialogResult = DialogResult.Cancel;
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void DoSave()
        {
            if (lbTabTabs.SelectedIndex != -1)
            {
                documents[lbTabTabs.SelectedIndex].TabTab.Text = scText.Text;
                documents[lbTabTabs.SelectedIndex].Unsaved = false;
                documents.ResetBindings();
            }
        }

        private void DoSaveAll()
        {
            var current = scText.Document;

            foreach (var doc in documents)
            {
                SwitchDocument(doc.Document);
                doc.TabTab.Text = scText.Text;
                doc.Unsaved = false;
            }

            scText.Document = current;
            documents.ResetBindings();
        }

        private void DoUndo()
        {
            scText.Undo();

            if (!scText.CanUndo && lbTabTabs.SelectedIndex != -1)
            {
                documents[lbTabTabs.SelectedIndex].Unsaved = false;
                documents.ResetBindings();
            }
        }

        private void DoRedo() => scText.Redo();

        private void DoAddTab()
        {
            var frtt = new FTabTabRename();
            if (frtt.ShowDialog() == DialogResult.OK)
            {
                documents.Add(new TabTabDoc { Unsaved = true, Document = CreateDocument(""), TabTab = new TableTabTab { Key = frtt.NewTabName, Text = "" } });
                lbTabTabs.SelectedIndex = documents.Count - 1;
            }
        }

        private void DoRemoveTab()
        {
            if (lbTabTabs.SelectedIndex != -1)
            {
                bool delete = true;
                string where = " ";
                int index = lbTabTabs.SelectedIndex;
                var tab = FLocalSettings.TabTabs[index];

                foreach (var tc in FLocalSettings.TCatalogs)
                {
                    foreach (var ti in tc.Items)
                    {
                        if (ti.Tab1 == tab)
                        {
                            delete = false;
                            where += $"Katalógová tabuľa {tc.Name}, položka {ti.Name}, TAB1";
                            break;
                        }

                        if (ti.Tab2 == tab)
                        {
                            delete = false;
                            where += $"Katalógová tabuľa {tc.Name}, položka {ti.Name}, TAB2";
                            break;
                        }
                    }

                    if (!delete)
                        break;
                }

                if (delete)
                    documents.RemoveAt(index);
                else
                    Utils.ShowError(Resources.SelectedItemRemoveCancel + where);
            }
        }

        private void DoRenameTab()
        {
            var frtt = new FTabTabRename();
            if (frtt.ShowDialog() == DialogResult.OK && lbTabTabs.SelectedIndex != -1)
            {
                documents[lbTabTabs.SelectedIndex].TabTab.Key = frtt.NewTabName;
                documents.ResetBindings();
            }
        }

        private void DoFindReplace()
        {
            var ffar = new FTabTabFindReplace(scText);
            ffar.Show();
        }

        private void DoReformat()
        {
            scText.BeginUndoAction();

            foreach (var f in TabTabACItems.GetFunctionItems().Select(item => item.FunctionName))
                scText.Text = scText.Text.ExReplace(f, f, StringComparison.CurrentCultureIgnoreCase);

            foreach (var c in TabTabACItems.GetConstantItems().Select(item => item.ConstName))
                scText.Text = scText.Text.ExReplace(c, c, StringComparison.CurrentCultureIgnoreCase);

            AddSpaces("=");
            AddSpaces(@"\|\|");
            AddSpaces("&&");

            scText.EndUndoAction();
        }

        private void AddSpaces(string replc)
        {
            
            char fc = replc[0];
            char lc = replc[replc.Length - 1];

            scText.Text = Regex.Replace(scText.Text,$@"[^ ]{replc}[^ \r\n]|[^ ]{replc}.?|.?{replc}[^ \r\n]", delegate (Match match)
            {
                char fmc = match.Value[0];
                char lmc = match.Value[match.Value.Length - 1];

                string first = fmc != fc && fmc != ' ' ? fmc.ToString() : "";
                string  last = lmc != lc && lmc != ' ' ? lmc.ToString() : "";

                return $"{first} {replc} {last}".Replace("\\","");
            });
        }

        private void tsbSave_Click(object sender, EventArgs e) => DoSave();
        private void tsbSaveAll_Click(object sender, EventArgs e) => DoSaveAll();
        private void tsbStorno_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
        private void tsbUndo_Click(object sender, EventArgs e) => DoUndo();
        private void tsbRedo_Click(object sender, EventArgs e) => DoRedo();
        private void tsbAddTab_Click(object sender, EventArgs e) => DoAddTab();
        private void tsbRemoveTab_Click(object sender, EventArgs e) => DoRemoveTab();
        private void tsbRename_Click(object sender, EventArgs e) => DoRenameTab();
        private void tsbFindReplace_Click(object sender, EventArgs e) => DoFindReplace();
        private void tsbReformat_Click(object sender, EventArgs e) => DoReformat();
        private void tsmiSave_Click(object sender, EventArgs e) => DoSave();
        private void tsmiSaveAll_Click(object sender, EventArgs e) => DoSaveAll();
        private void tsmiUndo_Click(object sender, EventArgs e) => DoUndo();
        private void tsmiRedo_Click(object sender, EventArgs e) => DoRedo();
        private void tsmiCut_Click(object sender, EventArgs e) => scText.Cut();
        private void tsmiCopy_Click(object sender, EventArgs e) => scText.Copy();
        private void tsmiPaste_Click(object sender, EventArgs e) => scText.Paste();
        private void tsmiDelete_Click(object sender, EventArgs e) => scText.ReplaceSelection("");
        private void tsmiSelectAll_Click(object sender, EventArgs e) => scText.SelectAll();
        private void tsmiAddTabTab_Click(object sender, EventArgs e) => DoAddTab();
        private void tsmiDeleteTabTab_Click(object sender, EventArgs e) => DoRemoveTab();
        private void tsmiRenameTabTab_Click(object sender, EventArgs e) => DoRenameTab();
        private void scText_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            var startPos = scText.GetEndStyled();
            var endPos = e.Position;

            cSharpLexer.Style(scText, startPos, endPos);
        }

        private void scText_TextChanged(object sender, EventArgs e)
        {
            tsbUndo.Enabled = scText.CanUndo;
            tsbRedo.Enabled = scText.CanRedo;

            if (lbTabTabs.SelectedIndex != -1 && !documents[lbTabTabs.SelectedIndex].Unsaved)
            {
                documents[lbTabTabs.SelectedIndex].Unsaved = true;
                tsbSave.Enabled = true;
                documents.ResetBindings();
            }

            ShowNumberLines();
        }

        private void ShowNumberLines()
        {
            var maxLength = scText.Lines.Count.ToString().Length;
            if (maxLength == maxLineNumberCharLength)
                return;

            const int padding = 2;
            scText.Margins[0].Width = scText.TextWidth(Style.LineNumber, new string('9', maxLength + 1)) + padding;
            maxLineNumberCharLength = maxLength;
        }

        private void FTabTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.S) 
                DoSaveAll();
            if (e.Control && e.KeyCode == Keys.S) 
                DoSave();
        }

        private void scText_CharAdded(object sender, CharAddedEventArgs e) => InsertMatchedChars(e);

        private void InsertMatchedChars(CharAddedEventArgs e)
        {
            var caretPos = scText.CurrentPosition;
            var docStart = caretPos == 1;
            var docEnd = caretPos == scText.Text.Length;

            var charPrev = docStart ? scText.GetCharAt(caretPos) : scText.GetCharAt(caretPos - 2);
            var charNext = scText.GetCharAt(caretPos);

            var isCharPrevBlank = charPrev == ' ' || charPrev == '\t' || charPrev == '\n' || charPrev == '\r';

            var isCharNextBlank = charNext == ' ' || charNext == '\t' || charNext == '\n' || charNext == '\r' || docEnd;

            var isEnclosed = charPrev == '(' && charNext == ')' || charPrev == '{' && charNext == '}' || charPrev == '[' && charNext == ']';

            var isSpaceEnclosed = charPrev == '(' && isCharNextBlank || isCharPrevBlank && charNext == ')' ||
                                  charPrev == '{' && isCharNextBlank || isCharPrevBlank && charNext == '}' ||
                                  charPrev == '[' && isCharNextBlank || isCharPrevBlank && charNext == ']';

            var isCharOrString = isCharPrevBlank && isCharNextBlank || isEnclosed || isSpaceEnclosed;

            var charNextIsCharOrString = charNext == '"' || charNext == '\'';

            switch (e.Char)
            {
                case '(':
                    if (charNextIsCharOrString) return;
                    scText.InsertText(caretPos, ")");
                    break;
                case '{':
                    if (charNextIsCharOrString) return;
                    scText.InsertText(caretPos, "}");
                    break;
                case '[':
                    if (charNextIsCharOrString) return;
                    scText.InsertText(caretPos, "]");
                    break;
                case '"':
                    // 0x22 = "
                    if (charPrev == 0x22 && charNext == 0x22)
                    {
                        scText.DeleteRange(caretPos, 1);
                        scText.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scText.InsertText(caretPos, "\"");
                    break;
                case '\'':
                    // 0x27 = '
                    if (charPrev == 0x27 && charNext == 0x27)
                    {
                        scText.DeleteRange(caretPos, 1);
                        scText.GotoPosition(caretPos);
                        return;
                    }

                    if (isCharOrString)
                        scText.InsertText(caretPos, "'");
                    break;
            }
        }

        private static bool IsBrace(int c)
        {
            return c switch
            {
                '(' => true,
                ')' => true,
                '[' => true,
                ']' => true,
                '{' => true,
                '}' => true,
                _ => false
            };
        }

        private void scText_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = scText.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(scText.GetCharAt(caretPos - 1)))
                    bracePos1 = caretPos - 1;
                else if (IsBrace(scText.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    var bracePos2 = scText.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                    {
                        scText.BraceBadLight(bracePos1);
                        scText.HighlightGuide = 0;
                    }
                    else
                    {
                        scText.BraceHighlight(bracePos1, bracePos2);
                        scText.HighlightGuide = scText.GetColumn(bracePos1);
                    }
                }
                else
                {
                    // Turn off brace matching
                    scText.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                    scText.HighlightGuide = 0;
                }
            }

            if ((e.Change & UpdateChange.Selection) > 0)
            {
                var currentPos = scText.CurrentPosition;
                var anchorPos = scText.AnchorPosition;
                if (anchorPos - currentPos == 0)
                {
                    tsslPosText.Text = @"Pos:";
                    tsslPos.Text = currentPos.ToString();
                }
                else
                {
                    tsslPosText.Text = @"Sel:";
                    tsslPos.Text = Math.Abs(anchorPos - currentPos).ToString();
                }

                tsslRow.Text = (scText.LineFromPosition(currentPos) + 1).ToString();
                tsslCol.Text = (scText.GetColumn(currentPos) + 1).ToString();
            }
        }

        private void SwitchDocument(Document nextDocument)
        {
            var prevDocument = scText.Document;
            scText.AddRefDocument(prevDocument);

            scText.Document = nextDocument;
            scText.ReleaseDocument(nextDocument);
        }

        private Document CreateDocument(string text)
        {
            var l = scText.CreateLoader(256);
            char[] chars = text.ToCharArray();
            if (chars.Length != 0) l.AddData(chars, text.Length);
            return l.ConvertToDocument();
        }

        private void lbTabTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTabTabs.SelectedIndex != -1)
            {
                SwitchDocument(documents[lbTabTabs.SelectedIndex].Document);

                tsbSave.Enabled = documents[lbTabTabs.SelectedIndex].Unsaved;
                tsbUndo.Enabled = scText.CanUndo;
                tsbRedo.Enabled = scText.CanRedo;
                tsslTabTabName.Text = documents[lbTabTabs.SelectedIndex].TabTab.Key;
                tsslLen.Text = scText.Text.Length.ToString();
                tsslLines.Text = scText.Lines.Count.ToString();
            }
        }

        internal class TabTabDoc
        {
            public TableTabTab TabTab { get; set; }
            public Document Document { get; set; }
            public bool Unsaved { get; set; }

            /// <summary>Returns a string that represents the current object.</summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return Unsaved ? "* " + TabTab.Key : TabTab.Key;
            }
        }

        private void scText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void scText_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tsmiSave.Enabled = tsbSave.Enabled;
                tsmiUndo.Enabled = scText.CanUndo;
                tsmiRedo.Enabled = scText.CanRedo;
                tsmiCut.Enabled = scText.SelectedText.Length > 0;
                tsmiCopy.Enabled = scText.SelectedText.Length > 0;
                tsmiPaste.Enabled = scText.CanPaste;
                tsmiDelete.Enabled = scText.SelectedText.Length > 0;
                tsmiSelectAll.Enabled = scText.Text.Length > 0 && scText.Text.Length != scText.SelectedText.Length;
            }
        }
    }
}