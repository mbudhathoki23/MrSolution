using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.CommonSetup;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmProductBatchList : Form
{
    #region --------------- PRODUCT BATCH LIST ---------------

    public FrmProductBatchList(DataTable dtBatch, bool isAdd = true)
    {
        InitializeComponent();
        _isAdd = isAdd;
        _productBatchListRepository = new ProductBatchListRepository();
        ProductInfo = _productBatchListRepository.GetProductBatchFormat();
        IniInitializeGrid();
        AdjustControlsInDataGrid();
        ClearControl();
        EnableGridControl();
        GetPrevBatchNo(dtBatch);
    }

    private void FrmProductBatchList_Load(object sender, System.EventArgs e)
    {
        DGrid.Focus();
    }

    private void FrmProductBatchList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!_isAdd)
            {
                return;
            }
            else
            {
                if (TxtBatchNo.Visible)
                {
                    ClearControl();
                    EnableGridControl();
                    DGrid.Focus();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            }
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete)
        {
            DGrid.AllowUserToDeleteRows = true;
            if (DGrid.CurrentRow != null)
            {
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
                _isRowDelete = true;
            }
        }
        if (e.KeyCode is not Keys.Enter || TxtBatchNo.Enabled)
        {
            return;
        }
        e.SuppressKeyPress = true;
        EnableGridControl(true);
        AdjustControlsInDataGrid();
        if (DGrid.Rows[_rowIndex].Cells["GTxtBatchNo"].Value.IsValueExits())
        {
            GetDataFromGrid();
            TxtBatchNo.Focus();
            return;
        }
        TxtBatchNo.Focus();
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _colIndex = e.ColumnIndex;
    }

    private void DGrid_GotFocus(object sender, System.EventArgs e)
    {
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void DGridOnEnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void MskMfDate_Validated(object sender, EventArgs e)
    {
        if (MskMfDate.MaskCompleted && !MskExpDate.MaskCompleted)
        {
            MskExpDate.Text = (MskMfDate.Text.GetDateTime().AddDays(365)).GetDateString();
        }
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (_isRowDelete)
        {
            foreach (DataGridViewRow row in DGrid.Rows)
            {
                row.Cells["GTxtSNo"].Value = row.Index + 1;
            }
        }
    }

    private void DGrid_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
    {
        if (e.Row.IsNewRow)
        {
        }
    }

    private void TxtMrpOnValidating(object sender, CancelEventArgs e)
    {
        TxtMrp.Text = TxtMrp.GetDecimalString();
        if (ActiveControl == TxtSalesRate)
        {
            return;
        }
        if (TxtBatchNo.Enabled)
        {
            AddDataToGrid();
        }
    }

    private void TxtBatchNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            GetBatchList();
        }
        else
        {
            if (TxtBatchNo.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtBatchNo, GetBatchList);
            }
        }
    }

    private void TxtBatchNo_Validating(object sender, CancelEventArgs e)
    {
        if (TxtBatchNo.Enabled && TxtBatchNo.IsBlankOrEmpty())
        {
            if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtBatchNo"].Value.IsValueExits())
            {
                BtnSave.Focus();
            }
            else
            {
                return;
            }
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (DGrid.RowCount == 0)
        {
            DGrid.Focus();
            return;
        }

        if (DGrid.RowCount > 0 && !DGrid.Rows[0].Cells["GTxtBatchNo"].Value.IsValueExits())
        {
            return;
        }
        UpdateProductBatch();
        DialogResult = DialogResult.OK;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    #endregion --------------- PRODUCT BATCH LIST ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void ClearControl()
    {
        if (DGrid.Rows.Count == 0)
        {
            DGrid.Rows.Add();
        }
        GetSerialNumber();
        TxtBatchNo.Clear();
        MskMfDate.Text = DateTime.Now.GetDateString();
        MskExpDate.Text = DateTime.Now.AddDays(365).GetDateString();
        TxtQty.Text = 1.GetDecimalQtyString();
        _isGridUpdate = false;
        _isRowDelete = false;
        TotalQtyCalculation();
    }

    private void GetSerialNumber()
    {
        TxtSno.Text = DGrid.RowCount.ToString();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtBatchNo.Enabled = isEnable;
        TxtBatchNo.Visible = isEnable;

        MskMfDate.Enabled = isEnable;
        MskMfDate.Visible = isEnable;

        MskExpDate.Enabled = isEnable;
        MskExpDate.Visible = isEnable;

        TxtQty.Enabled = isEnable;
        TxtQty.Visible = isEnable;

        TxtMrp.Enabled = isEnable;
        TxtMrp.Visible = isEnable;

        TxtSalesRate.Enabled = isEnable;
        TxtSalesRate.Visible = isEnable;
    }

    private void IniInitializeGrid()
    {
        DGrid.RowEnter += DGrid_RowEnter; ;
        DGrid.GotFocus += DGrid_GotFocus; ;
        DGrid.CellEnter += DGrid_CellEnter;
        DGrid.EnterKeyPressed += DGridOnEnterKeyPressed;
        DGrid.KeyDown += DGrid_KeyDown;
        DGrid.NewRowNeeded += DGrid_NewRowNeeded;
        DGrid.UserDeletedRow += DGrid_UserDeletedRow;
        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtBatchNo = new MrGridTextBox(DGrid)
        {
            ReadOnly = !_isAdd
        };
        TxtBatchNo.KeyDown += TxtBatchNo_KeyDown;
        TxtBatchNo.Validating += TxtBatchNo_Validating;
        MskMfDate = new MrGridMaskedTextBox(DGrid);
        MskMfDate.Validated += MskMfDate_Validated;
        MskExpDate = new MrGridMaskedTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validating += (_, _) => TxtQty.Text = TxtQty.GetDecimalString();
        TxtSalesRate = new MrGridNumericTextBox(DGrid)
        {
        };
        TxtSalesRate.Validating += (_, _) => TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
        TxtMrp = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = !_isAdd
        };
        TxtMrp.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab or Keys.Enter)
            {
                TxtMrpOnValidating(sender, null);
                return;
            }
        };
        TxtMrp.Validating += TxtMrpOnValidating;
        AdjustControlsInDataGrid();
        ObjGlobal.DGridColorCombo(DGrid);
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtBatchNo"].Index;
        TxtBatchNo.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBatchNo.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBatchNo.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtMFDate"].Index;
        MskMfDate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        MskMfDate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        MskMfDate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtExpDate"].Index;
        MskExpDate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        MskExpDate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        MskExpDate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtSalesRate"].Index;
        TxtSalesRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSalesRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSalesRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtMrp"].Index;
        TxtMrp.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtMrp.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtMrp.TabIndex = columnIndex;
    }

    private bool AddDataToGrid()

    {
        if (TxtBatchNo.IsBlankOrEmpty())
        {
            TxtBatchNo.WarningMessage("BATCH NUMBER IS REQUIRED..!!");
            return false;
        }
        if (!MskMfDate.MaskCompleted)
        {
            MskMfDate.WarningMessage("BATCH MF DATE IS REQUIRED..!!");
            return false;
        }
        if (!MskExpDate.MaskCompleted)
        {
            MskExpDate.WarningMessage("BATCH EXPIRY DATE IS REQUIRED..!!");
            return false;
        }
        if (TxtQty.GetDecimal() <= 0)
        {
            TxtQty.WarningMessage("QTY IS REQUIRED..!!");
            return false;
        }
        var iRows = _rowIndex;
        if (!_isGridUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }
        if (DGrid.CurrentRow != null)
        {
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = TxtSno.Text;
            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = ProductId;
            DGrid.Rows[iRows].Cells["GTxtBatchNo"].Value = TxtBatchNo.Text;
            DGrid.Rows[iRows].Cells["GTxtMFDate"].Value = MskMfDate.Text;
            DGrid.Rows[iRows].Cells["GTxtExpDate"].Value = MskExpDate.Text;
            DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.Text;
            DGrid.Rows[iRows].Cells["GTxtMrp"].Value = TxtMrp.GetDecimal() > 0 ? TxtMrp.Text : TxtSalesRate.Text;
            DGrid.Rows[iRows].Cells["GTxtSalesRate"].Value = TxtSalesRate.Text;
            DGrid.Rows[iRows].Cells["GTxtProductSno"].Value = ProductSno;
            _batchRate = TxtSalesRate.Text;
            var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
            DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_colIndex];
            if (_isGridUpdate)
            {
                EnableGridControl();
                ClearControl();
                DGrid.Focus();
                return false;
            }
            ClearControl();
            AdjustControlsInDataGrid();
            TxtBatchNo.AcceptsTab = false;
            TxtBatchNo.Focus();
        }
        else
        {
            return false;
        }

        return true;
    }

    private void GetDataFromGrid()
    {
        if (DGrid.CurrentRow != null)
        {
            _isGridUpdate = true;
            var index = DGrid.CurrentRow.Index;
            TxtSno.Text = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetString();
            TxtBatchNo.Text = DGrid.CurrentRow.Cells["GTxtBatchNo"].Value.GetString();
            MskMfDate.Text = DGrid.CurrentRow.Cells["GTxtMFDate"].Value.GetString();
            MskExpDate.Text = DGrid.CurrentRow.Cells["GTxtExpDate"].Value.GetString();
            TxtQty.Text = DGrid.CurrentRow.Cells["GTxtQty"].Value.GetString();
            TxtMrp.Text = DGrid.CurrentRow.Cells["GTxtMrp"].Value.GetString();
            TxtSalesRate.Text = DGrid.CurrentRow.Cells["GTxtSalesRate"].Value.GetString();
        }
        else
        {
            return;
        }
    }

    private void UpdateProductBatch()
    {
        if (ProductInfo.RowsCount() > 0)
        {
            ProductInfo.Rows.Clear();
        }

        foreach (DataGridViewRow gridRow in DGrid.Rows)
        {
            if (!gridRow.Cells["GTxtProductId"].Value.IsValueExits())
            {
                continue;
            }
            var newRow = ProductInfo.NewRow();
            newRow["SNo"] = gridRow.Cells["GTxtSNo"].Value;
            newRow["ProductId"] = gridRow.Cells["GTxtProductId"].Value;
            newRow["BatchNo"] = gridRow.Cells["GTxtBatchNo"].Value;
            newRow["MfDate"] = gridRow.Cells["GTxtMFDate"].Value;
            newRow["ExpDate"] = gridRow.Cells["GTxtExpDate"].Value;
            newRow["MRP"] = gridRow.Cells["GTxtMrp"].Value;
            newRow["Qty"] = gridRow.Cells["GTxtQty"].Value;
            newRow["Rate"] = gridRow.Cells["GTxtSalesRate"].Value;
            newRow["ProductSno"] = gridRow.Cells["GTxtProductSno"].Value;
            ProductInfo.Rows.InsertAt(newRow, ProductInfo.Rows.Count + 1);
        }
    }

    private void TotalQtyCalculation()
    {
        if (DGrid.Rows.Count is 0)
        {
            return;
        }
        var viewRows = DGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

        LblTotalQty.Text = gridViewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
    }

    private void GetPrevBatchNo(DataTable data)
    {
        if (data.Rows.Count == 0)
        {
            return;
        }
        DGrid.Rows.Add(data.RowsCount());
        foreach (DataRow row in data.Rows)
        {
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtSNo"].Value = row["SNo"];
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtProductId"].Value = row["ProductId"];
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtBatchNo"].Value = row["BatchNo"];
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtMFDate"].Value = row["MfDate"];
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtExpDate"].Value = row["ExpDate"];
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtMrp"].Value = row["MRP"].GetDecimalString();
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtQty"].Value = row["Qty"].GetDecimalString();
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtSalesRate"].Value = row["Rate"].GetDecimalString();
            DGrid.Rows[data.Rows.IndexOf(row)].Cells["GTxtProductSno"].Value = row["ProductSno"];
        }
        DGrid.CurrentCell = DGrid.Rows[data.Rows.Count].Cells[0];
    }

    private void FillProductBatch()
    {
        if (TxtBatchNo.IsValueExits())
        {
            var dt = _productBatchListRepository.GetProductBatchFromBatchRate(TxtBatchNo.Text, TxtSalesRate.Text);
            if (dt.Rows.Count is 0)
            {
                TxtBatchNo.WarningMessage("SELECTED BATCH IS INVALID..!!");
                return;
            }
            else
            {
                var row = dt.Rows[0];
                MskMfDate.Text = row["MFDate"].GetDateString();
                MskExpDate.Text = row["ExpDate"].GetDateString();
                TxtQty.Text = row["Qty"].GetDecimalQtyString();
                TxtMrp.Text = row["Mrp"].GetDecimalString();
            }
        }
    }

    private void GetBatchList()
    {
        var (description, rate) = GetMasterList.GetProductBatchList("SAVE", ProductId);
        if (description.IsValueExits())
        {
            if (_isAdd)
            {
                return;
            }
            TxtBatchNo.Text = description;
            TxtSalesRate.Text = rate.GetDecimalString();
            FillProductBatch();
        }

        TxtBatchNo.Focus();
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public int ProductSno;

    private int _rowIndex;
    private int _colIndex;

    private bool _isGridUpdate;
    private bool _isAdd;
    private bool _isRowDelete;

    public long ProductId;

    public string _batchRate;
    public DataTable ProductInfo;
    private readonly IProductBatchListRepository _productBatchListRepository;

    #region ** ---------- Grid Control  ---------- **

    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtBatchNo { get; set; }
    private MrGridMaskedTextBox MskMfDate { get; set; }
    private MrGridMaskedTextBox MskExpDate { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtMrp { get; set; }
    private MrGridNumericTextBox TxtSalesRate { get; set; }

    #endregion ** ---------- Grid Control  ---------- **

    #endregion --------------- OBJECT ---------------
}