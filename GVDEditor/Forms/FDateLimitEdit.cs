﻿using GVDEditor.Entities;
using GVDEditor.Tools;
using ToolsCore.Tools;

namespace GVDEditor.Forms;

/// <summary>
///     Dialog - pre upravu datumoveho obmedzenia vlakov.
/// </summary>
public partial class FDateLimitEdit : Form
{
    private readonly DateTime PlatnostOd, PlatnostDo;
    private readonly Train ThisTrain;

    /// <summary>
    ///     Upravene datumove obmedzenie.
    /// </summary>
    public string DateRemEdited;

    /// <summary>
    ///     Vytvori novy formular typu <see cref="FDateLimitEdit"/>.
    /// </summary>
    public FDateLimitEdit(Train thisConflict, string original, DateTime platnostOd, DateTime platnostDo)
    {
        InitializeComponent();
        FormUtils.SetFormFont(this);
        this.ApplyTheme();

        ThisTrain = thisConflict;
        PlatnostOd = platnostOd;
        PlatnostDo = platnostDo;

        tbDateRemOrig.Text = original;
        var dr = new DateLimit(platnostOd, platnostDo, insertMarks: false);
        tbDateRemNew.Text = dr.TextNot(original);
    }

    private void FDateRemEdit_Load(object sender, EventArgs e) => 
        Text += $@" {ThisTrain.Type} {ThisTrain.Number} {ThisTrain.Name}";

    private void bSave_Click(object sender, EventArgs e)
    {
        var daterems = tbDateRemNew.Text;
        try
        {
            var dom = new DateLimit(PlatnostOd.Date, PlatnostDo.Date);
            dom.TextToBitArray(daterems);
        }
        catch (Exception exception)
        {
            Utils.ShowError(exception.Message);
            DialogResult = DialogResult.None;
            return;
        }

        DateRemEdited = daterems;

        DialogResult = DialogResult.OK;
    }

    private void bStorno_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
}