using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Utility.Common;

public partial class FrmRptSearch : MrForm
{
    // OBJECT FOR THIS FORM
    public bool MatchCase;

    public DataGridView RGridView;

    public string SearchValue;

    // SEARCH FORM
    public FrmRptSearch(DataGridView gridView)
    {
        InitializeComponent();
        RGridView = gridView;
    }

    private void FrmRptSearch_Load(object sender, EventArgs e)
    {
        TxtValue.Text = string.Empty;
        BtnSearch.Enabled = false;
        TxtValue.Focus();
    }

    private void FrmRptSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Escape:
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes) Close();
                break;
            }
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
        }
    }

    private void TxtSearchValue_TextChanged(object sender, EventArgs e)
    {
        BtnSearch.Enabled = TxtValue.Text.Trim() != string.Empty;
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        var result = SearchText();
        if (!result)
        {
            CustomMessageBox.Warning($"{TxtValue.Text} NOT FOUND..!!");
        }
    }

    // Method
    private bool SearchText()
    {
        var resultFound = false;
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var rowIndex = 0;
            foreach (var row in RGridView.Rows)
            {
                for (var i = 0; i < RGridView.ColumnCount; i++)
                {
                    var result = RGridView.Rows[rowIndex].Cells[i].Value.GetString();
                    if (!result.Contains(TxtValue.Text))
                    {
                        continue;
                    }
                    if (RGridView.CurrentRow == null)
                    {
                        continue;
                    }
                    RGridView.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    RGridView.CurrentCell = RGridView.Rows[rowIndex].Cells[i];
                    RGridView.Rows[rowIndex].Cells[i].Selected = true;
                    resultFound = true;
                }
                rowIndex++;
            }
            SplashScreenManager.CloseForm(false);
            return resultFound;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            SplashScreenManager.CloseForm(false);
            return resultFound;
        }
    }
}