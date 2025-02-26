using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.MIS;

public partial class FrmSoftwareModule : MrForm
{
    private int currentColumn;
    private int rowIndex;
    public string SoftwareModule = string.Empty;

    public FrmSoftwareModule()
    {
        InitializeComponent();
    }

    private void FrmSoftwareModule_Load(object sender, EventArgs e)
    {
        ObjGlobal.DGridColorCombo(DGrid);
        BindData();
    }

    private void FrmSoftwareModule_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void SoftwareModuleUpdate()
    {
        if (DGrid.Rows.Count == 0)
        {
            return;
        }
        SoftwareModule = DGrid.CurrentRow?.Cells["MODULE"].Value.ToString();
        if (string.IsNullOrEmpty(SoftwareModule))
        {
            return;
        }

        var cmdString = $"UPDATE AMS.CompanyInfo set SoftModule = N'{SoftwareModule}', LoginDate = GETDATE();";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        if (result == 0)
        {
            return;
        }

        var system = SoftwareModule.ToUpper() switch
        {
            "AIMS" => AlterDatabaseTable.DefaultTermValueForTrading(),
            "POS" => AlterDatabaseTable.DefaultValuePointOfSales(),
            "RESTRO" => AlterDatabaseTable.DefaultValueRestaurant(),
            "HOSPITAL" => AlterDatabaseTable.DefaultValuePointOfSales(),
            "HOTEL" => AlterDatabaseTable.DefaultValueRestaurant(),
            _ => AlterDatabaseTable.DefaultTermValueForTrading()
        };
        if (system != 0)
        {
            AlterDatabaseTable.SaveSystemConfiguration();
            AlterDatabaseTable.UpdateSystemConfiguration(SoftwareModule);
        }
        Close();
        return;
    }

    private void BindData()
    {
        var data = new DataTable();
        DGrid.DataSource = data.GetSoftwareModule();
        DGrid.Columns["MODULE"].Width = 120;
        DGrid.Columns["DESCRIPTION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    private void DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        SoftwareModuleUpdate();
        BindData();
    }

    private void DGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        SoftwareModuleUpdate();
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
        {
            if (DGrid.Rows.Count != 0)
            {
                DGrid.Rows[rowIndex].Selected = true;
                e.SuppressKeyPress = true;
                SoftwareModuleUpdate();
            }
        }
    }
}