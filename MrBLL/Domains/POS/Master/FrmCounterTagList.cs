using Microsoft.VisualBasic;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmCounterTagList : MrForm
{
    #region --------------- COUNTER TAG LIST ---------------

    public FrmCounterTagList()
    {
        InitializeComponent();
        BackColor = ObjGlobal.FrmBackColor();
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void FrmCounterTagList_Load(object sender, EventArgs e)
    {
        ObjGlobal.DGridColorCombo(SGrid);
        LoadData();
        SGrid.Focus();
    }

    private void FrmCounterTagList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            Close();
        }
        else
        {
            if (e.KeyChar == 6)
                if (e.KeyChar == 19)
                    SGrid.Focus();

            if (ActiveControl.Name == "TxtFindValue") return;

            if (Strings.InStr(
                    "ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwxy z`~!@#$%^&*()_+1234567890-=/ <>.",
                    e.KeyChar.ToString().ToUpper()) > 0) CSearch += e.KeyChar.ToString().ToUpper();
            if (e.KeyChar == 8)
                if (CSearch.Length > 0)
                    CSearch = CSearch.Substring(0, CSearch.Length - 1);
            if (CSearch.Trim() != string.Empty) lbl_SearchValue.Text = CSearch;

            if (e.KeyChar == 39) e.KeyChar = '0';
        }
    }

    #endregion --------------- COUNTER TAG LIST ---------------

    #region --------------- GRID VIEW ---------------

    private void SGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void SGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _currentColumn = e.ColumnIndex;
    }

    private void SGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        SGrid.Rows[_rowIndex].Selected = true;
        e.SuppressKeyPress = true;
        if (!string.IsNullOrEmpty(SGrid.CurrentRow?.Cells["GTxtLedgerId"].Value.ToString()))
        {
            SelectedCounter = SGrid.CurrentRow.Cells["GTxtShortName"].Value.ToString();
            SelectedCounterId = SGrid.CurrentRow.Cells["GTxtLedgerId"].Value.GetInt();
            CounterPrinter = SGrid.CurrentRow.Cells["GTxtPrinter"].Value.ToString();
        }

        Close();
    }

    private void SGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 6) return;

        if (e.KeyChar == 16) return;
    }

    private void SGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (_currentColumn != 0) return;
            ClsTagList.PlValue1 = string.Empty;
            if (SGrid.Rows.Count <= 0) return;
            foreach (DataGridViewRow dr in SGrid.Rows)
            {
                if (dr.Cells[0].Selected != true) continue;
                SelectedCounter = ClsTagList.PlValue1 =
                    ClsTagList.PlValue1 + dr.Cells[0].Value.ToString().Trim() + string.Empty;
                CounterPrinter = SGrid.CurrentRow?.Cells[2].Value.ToString();
                Close();
            }
        }
        catch (Exception exc)
        {
            this.NotifyError(exc.Message);
        }
    }

    #endregion --------------- GRID VIEW ---------------

    #region --------------- EVENT ---------------

    private void CmdCancel_Click(object sender, EventArgs e)
    {
        ClsTagList.PlValue1 = string.Empty;
        ClsTagList.PlValue2 = string.Empty;
        ClsTagList.PlValue3 = string.Empty;
        Close();
    }

    private void CmdOk_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(SGrid.CurrentRow?.Cells["GTxtLedgerId"].Value.ToString()))
        {
            SelectedCounter = SGrid.CurrentRow.Cells["GTxtShortName"].Value.ToString();
            SelectedCounterId = SGrid.CurrentRow.Cells["GTxtLedgerId"].Value.GetInt();
            CounterPrinter = SGrid.CurrentRow.Cells["GTxtPrinter"].Value.ToString();
        }

        Close();
    }

    private void LoadData()
    {
        _dt = PickList.GetCounterList("SAVE", true);
        if (_dt.Rows.Count > 0)
        {
            SGrid.DataSource = _dt;
        }
        else
        {
            MessageBox.Show(@"NO DATA FOUND TO DISPLAY...!!");
            Close();
        }
    }

    private void lbl_SearchValue_TextChanged(object sender, EventArgs e)
    {
    }

    #endregion --------------- EVENT ---------------

    #region --------------- OBJECT ---------------

    private int _rowIndex;
    private int _currentColumn;
    private DataTable _dt = new();
    private readonly string _query = string.Empty;
    public string CSearch = string.Empty;
    public string SelectedCounter = string.Empty;
    public string CounterPrinter = string.Empty;
    public int SelectedCounterId;
    public IPickList PickList = new ClsPickList();

    #endregion --------------- OBJECT ---------------
}