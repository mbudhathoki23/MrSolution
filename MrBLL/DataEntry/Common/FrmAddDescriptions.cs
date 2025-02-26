using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmAddDescriptions : MrForm
{
    public FrmAddDescriptions()
    {
        InitializeComponent();
    }

    // OBJECT
    public string Descriptions { get; set; }

    private void FrmAddDescriptions_Load(object sender, EventArgs e)
    {
        TxtDescription.Text = Descriptions;
        TxtDescription.SelectionStart = 0;
        TxtDescription.Focus();
    }

    private void FrmAddDescriptions_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) Close();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        Descriptions = TxtDescription.Text;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void TxtDescriptions_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtDescription, 'E');
    }

    private void TxtDescriptions_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                BtnSave.Focus();
            }
        }
    }

    private void TxtDescriptions_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtDescription, 'L');
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}