using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class FrmApiKeyList : Form
{
    // API KEY LIST FORM
    public FrmApiKeyList()
    {
        InitializeComponent();
        _master = new ClsMasterSetup();
        BindCompanyDetail();
    }

    private void FrmApiKeyList_Load(object sender, EventArgs e)
    {
    }

    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ((DataTable)RGrid.DataSource).DefaultView.RowFilter = $"Description LIKE '%{TxtSearch.Text.Trim()}%' or PanNo LIKE '%{TxtSearch.Text.Trim()}%'  ";
            if (RGrid.Rows.Count != 0 || TxtSearch.Text != string.Empty)
            {
                return;
            }
            BindCompanyDetail();
            ObjGlobal.DGridColorCombo(RGrid);
        }
        catch
        {
            // ignored
        }
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.C)
        {
            if (RGrid.CurrentRow != null)
            {
                var copy = RGrid.CurrentRow.Cells["ApiKey"].Value.ToString();
                Clipboard.SetText(copy.GetUpper());
                isCopyMode = true;
            }
        }
        else
        {
            isCopyMode = false;
        }
    }

    private void FrmApiKeyList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)8)
        {
            if (TxtSearch.Text.Length > 0)
            {
                TxtSearch.Text = TxtSearch.Text.Substring(0, TxtSearch.Text.Length - 1);
            }
        }
        else if (e.KeyChar == (char)27)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
                return;
            }
        }
        else
        {
            if (isCopyMode)
            {
                return;
            }
            TxtSearch.Text += e.KeyChar.ToString();
        }
    }

    // METHOD FOR THIS FORM
    private void BindCompanyDetail()
    {
        var dt = _master.GetCompanyList();
        if (RGrid.Columns.Count > 0)
        {
            RGrid.Columns.Clear();
        }
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("GComp_Id", "GComp_Id", "GComp_Id", 0, 2, false);
        RGrid.AddColumn("Database_Name", "Database_Name", "Database_Name", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 250, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PanNo", "PAN NO", "PanNo", 150, 100, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 150, 100, true);
        RGrid.AddColumn("ApiKey", "API KEY", "ApiKey", 150, 100, true, DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
        RGrid.DataSource = dt;
    }

    // OBJECT FOR THIS FORM
    private readonly IMasterSetup _master;

    private bool isCopyMode = false;
}