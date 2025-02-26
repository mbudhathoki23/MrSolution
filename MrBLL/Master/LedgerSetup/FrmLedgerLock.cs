using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmLedgerLock : MrForm
{
    public FrmLedgerLock()
    {
        InitializeComponent();
        TxtSearch.Visible = false;
        _setup = new ClsPickList();
    }

    private void FrmLedgerLock_Load(object sender, EventArgs e)
    {
        BindLedgerDetails();
        TxtSearch.Focus();
    }

    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        if (TxtSearch.IsValueExits())
        {
            Search();
        }
        else
        {
            BindLedgerDetails();
        }
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.Enter)
        {
        }
    }

    private void Search()
    {
        try
        {
            ((DataTable)RGrid.DataSource).DefaultView.RowFilter = $"GlName LIKE '{TxtSearch.Text.Trim()}%'";
            if (RGrid.Rows.Count != 0 || TxtSearch.Text != string.Empty)
            {
                return;
            }
            BindLedgerDetails();
            ObjGlobal.DGridColorCombo(RGrid);
        }
        catch
        {
            // ignored
        }
    }

    private void BindLedgerDetails()
    {
        RGrid.AutoGenerateColumns = false;
        var dt = _setup.GetGeneralLedgerList("UPDATE", "", DateTime.Now.GetDateString(), true);

        RGrid.AddColumn("GTxtLedgerId", "LedgerId", "LedgerId", 5, 2, false);
        RGrid.AddColumn("GTxtLedger", "PARTICULAR", "Description", 400, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("GTxtShortName", "SHORT NAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("GTxtGroup", "ACCOUNT GROUP", "GroupDesc", 120, 2, true, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
        RGrid.AddCheckColumn("GTxtStatus", "ACTIVE", "Status", 120, 2, true);
        if (dt.RowsCount() > 0)
        {
            RGrid.DataSource = dt;
        }
        ObjGlobal.DGridColorCombo(RGrid);
    }

    private IPickList _setup;
    private MrGridMaskedTextBox TxtLedger { get; set; }
    private MrGridMaskedTextBox TxtShortName { get; set; }
    private MrGridMaskedTextBox TxtGroup { get; set; }
    private MrGridComboBox CmbActive { get; set; }
}