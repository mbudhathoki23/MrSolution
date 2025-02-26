using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.SystemSetting.PayrollSetting;
using MrDAL.SystemSetting.SystemInterface;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.SystemSetting.PayrollSetting;

public partial class FrmIncomeTaxSetup : Form
{
    public FrmIncomeTaxSetup()
    {
        InitializeComponent();
        InitializeGridComponent();
        AdjustControlsInDataGrid();
        _taxSetting = new IncomeTaxSettingRepository();
        EnableGridControl();
    }

    private void FrmIncomeTaxSetup_Load(object sender, EventArgs e)
    {
        FillExitsIncomeTaxData();
        DGrid.Focus();
    }


    // METHOD FOR THIS FORM
    private void FillExitsIncomeTaxData()
    {
        var result = _taxSetting.ReturnIncomeTaxList();
        if (result.Rows.Count > 0)
        {
            var index = 0;
            DGrid.Rows.Add(result.Rows.Count);
            foreach (DataRow row in result.Rows)
            {
                DGrid.Rows[index].Cells[0].Value = row[0];
                DGrid.Rows[index].Cells[1].Value = row[2];
                DGrid.Rows[index].Cells[2].Value = row[3];
                DGrid.Rows[index].Cells[3].Value = row[4];
                DGrid.Rows[index].Cells[4].Value = row[5];
                index++;
            }
        }
    }
    private void InitializeGridComponent()
    {
        TxtSNo = new MrGridTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = true
        };

        TxtDescriptin = new MrGridTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = false
        };

        TxtSingleTaxableAmount = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = false
        };

        TxtMarriedTaxableAmount = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = false
        };

        TxtTaxRate = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = false
        };

    }
    private void AdjustControlsInDataGrid()
    {
        if (DGrid.RowCount <= 0)
        {
            return;
        }

        DGrid.CurrentCell = !isUpdateGrid ? DGrid.Rows[rowIndex].Cells["GTxtSNo"] : DGrid.CurrentCell;
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = rowIndex;
        var columnIndex = DGrid.Columns["GTxtSno"]!.Index;

        TxtSNo.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSNo.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSNo.Tag = columnIndex;
        TxtSNo.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtParticular"]!.Index;
        TxtDescriptin.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtDescriptin.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtDescriptin.Tag = columnIndex;
        TxtDescriptin.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxSingleTaxAmount"]!.Index;
        TxtSingleTaxableAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSingleTaxableAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSingleTaxableAmount.Tag = columnIndex;
        TxtSingleTaxableAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtMarriedTaxAmount"]!.Index;
        TxtMarriedTaxableAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtMarriedTaxableAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtMarriedTaxableAmount.Tag = columnIndex;
        TxtMarriedTaxableAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtTaxChargePercent"]!.Index;
        TxtTaxRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtTaxRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtTaxRate.Tag = columnIndex;
        TxtTaxRate.TabIndex = columnIndex;

        DGrid.Rows[rowIndex].Selected = true;
    }
    private void EnableGridControl(bool isEnable = false)
    {
        TxtSNo.Visible = TxtSNo.Enabled = isEnable;
        TxtDescriptin.Visible = TxtDescriptin.Enabled = isEnable;
        TxtSingleTaxableAmount.Visible = TxtSingleTaxableAmount.Enabled = isEnable;
        TxtMarriedTaxableAmount.Visible = TxtMarriedTaxableAmount.Enabled = isEnable;
        TxtTaxRate.Visible = TxtTaxRate.Enabled = isEnable;

    }


    // OBJECT FOR THIS FORM

    private int rowIndex;
    private int colIndex;

    private bool isUpdateGrid;

    private IIncomeTaxSettingRepository _taxSetting;

    private MrGridTextBox TxtSNo;
    private MrGridTextBox TxtDescriptin;

    private MrGridNumericTextBox TxtSingleTaxableAmount;
    private MrGridNumericTextBox TxtMarriedTaxableAmount;
    private MrGridNumericTextBox TxtTaxRate;

    private void BtnSave_Click(object sender, EventArgs e)
    {

    }
}