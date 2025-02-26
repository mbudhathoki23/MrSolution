using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmBarcodeList : MrForm
{
    // BARCODE LIST

    #region --------------- FrmBarcodeList ---------------

    public FrmBarcodeList(long productId, DataGridView gridView)
    {
        InitializeComponent();
        InitializeGridComponent();
        _barcodeListRepository = new BarcodeListRepository();
        _GetView = gridView;
        _ProductId = productId;
        KeyDown += FrmBarcodeList_KeyDown;
    }

    private void FrmBarcodeList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Escape)
        {
            return;
        }
        if (TxtBarcode.Enabled)
        {
            EnableControl();
            DGrid.Focus();
        }
        else
        {
            BtnCancel.PerformClick();
        }
    }

    private void FrmBarcodeList_Load(object sender, EventArgs e)
    {
        EnableControl();
        BindProductBarcode();
        ClearDetails();
        TxtBarcode.Focus();
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtMrpBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (DGrid.RowCount > 0)
        {
            if (string.IsNullOrEmpty(TxtBarcode.Text) && DGrid.RowCount > 0)
            {
                var result = DGrid.Rows[DGrid.Rows.Count - 1].Cells["GTxtBarcode"].Value?.ToString();
                if (result.IsBlankOrEmpty())
                {
                    return;
                }
                DGrid.Rows.RemoveAt(DGrid.Rows.Count - 1);
            }
            _GetView = DGrid;
        }

        Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtBarcode.Text) && DGrid.RowCount > 0)
        {
            if (!string.IsNullOrEmpty(DGrid.Rows[DGrid.Rows.Count - 1].Cells["GTxtBarcode"].Value?.ToString()))
            {
                return;
            }

            DGrid.Rows.RemoveAt(DGrid.Rows.Count - 1);
            EnableControl();
        }

        Close();
    }

    private void TxtBarcode_Validated(object sender, EventArgs e)
    {
        if (ActiveControl != null && TxtBarcode.IsValueExits() && !isUpdateGrid)
        {
            if (DGrid == null || DGrid.RowCount - 1 <= 0)
            {
                return;
            }
            //var result = DGrid.Rows.Cast<DataGridViewRow>();
            //var exists = result.Where(row => !row.IsNewRow).Select(row => row.Cells["GTxtBarcode"].Value?.ToString()).Any(x => TxtBarcode.Text == x);
            //if (!exists)
            //{
            //    return;
            //}
            //MessageBox.Show(@"BARCODE ALREADY EXITS ON SELECTED PRODUCT ..!!", ObjGlobal.Caption);
            //TxtBarcode.Focus();
            //return;
        }

        if (TxtBarcode.IsBlankOrEmpty() && DGrid.RowCount > 0)
        {
            if (DGrid.RowCount > 0 && string.IsNullOrEmpty(DGrid.Rows[0].Cells["GTxtBarcode"].Value?.ToString()))
            {
                if (CustomMessageBox.Question(@"BARCODE FOR THIS PRODUCT IS BLANK. DO YOU WANT TO CONTINUE ..!!") is DialogResult.No)
                {
                    TxtBarcode.Focus();
                }
            }
            else
            {
                EnableControl();
                BtnSave.Focus();
            }
        }
    }

    private void TxtBarcode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (!string.IsNullOrEmpty(TxtBarcode.Text))
            {
                SendKeys.Send("{TAB}");
            }
            else BtnSave.Focus();
        }
    }

    private void TxtSalesRate_Validated(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtBarcode.Text) && ObjGlobal.ReturnDecimal(TxtSalesRate.Text) > 0)
        {
            TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
            AddToGridFunction(isUpdateGrid);
            if (TxtBarcode.Enabled)
            {
                TxtBarcode.Focus();
            }
            else DGrid.Focus();
        }

        if (TxtSalesRate.Enabled && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtBarcode"].Value.IsValueExits())
        {
            if (TxtSalesRate.GetDouble() is 0)
            {
                TxtSalesRate.WarningMessage("PRODUCT SALES RATE CANNOT BE ZERO..!!");
                return;
            }
        }
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        colIndex = e.ColumnIndex;
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete)
        {
            if (CustomMessageBox.DeleteRow() == DialogResult.Yes)
            {
                if (DGrid.CurrentRow != null)
                {
                    DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
                }
            }
        }

        if (e.KeyCode == Keys.Enter && DGrid.RowCount > 0 && !TxtBarcode.Enabled) //|| e.KeyCode == Keys.Tab
        {
            DGrid.Rows[rowIndex].Selected = true;
            GridId = rowIndex;
            e.SuppressKeyPress = true;
            EnableControl(true);
            if (!string.IsNullOrEmpty(DGrid.Rows[rowIndex].Cells["GTxtBarcode"].Value?.ToString()))
            {
                GridToTextBox();
            }
            else
            {
                AdjustControlsInDataGrid();
                ClearDetails();
            }

            TxtBarcode.Focus();
        }
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (DGrid.RowCount <= 0)
        {
            return;
        }

        for (var i = 0; i < DGrid.RowCount; i++)
        {
            DGrid.Rows[rowIndex].Cells["GTxtSNo"].Value = i + 1;
        }
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void TxtReSeller_Validating(object sender, CancelEventArgs e)
    {
        TxtReSeller.Text = TxtReSeller.GetDecimalString();
    }

    private void TxtDealer_Validating(object sender, CancelEventArgs e)
    {
        TxtDealer.Text = TxtDealer.GetDecimalString();
    }

    private void TxtRetail_Validating(object sender, CancelEventArgs e)
    {
        TxtRetail.Text = TxtRetail.GetDecimalString();
    }

    private void TxtWholeSales_Validating(object sender, CancelEventArgs e)
    {
        TxtWholeSales.Text = TxtWholeSales.GetDecimalString();
    }

    private void TxtTradePrice_Validating(object sender, CancelEventArgs e)
    {
        TxtTradePrice.Text = TxtTradePrice.GetDecimalString();
    }

    private void TxtMrpBox_Validating(object sender, CancelEventArgs e)
    {
        TxtMrpBox.Text = TxtMrpBox.GetDecimalString();
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenProductUnit();
        }
        else if (e.KeyCode == Keys.F2)
        {
            IsAltUnit = true;
            OpenProductUnit();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmProductUnit(true);
            frm.ShowDialog();
            TxtUnit.Text = frm.ProductUnitName;
            UnitId = frm.ProductUnitId;
            frm.Dispose();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                ObjGlobal.SearchText.Trim(), TxtUnit, OpenProductUnit);
        }
    }

    #endregion --------------- FrmBarcodeList ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void BindProductBarcode()
    {
        if (_GetView != null && _GetView.Rows.Count > 0)
        {
            DGrid.Rows.Add(_GetView.Rows.Count);
            for (var i = 0; i < _GetView.Rows.Count; i++)
            {
                DGrid.Rows[i].Cells["GTxtSNo"].Value = _GetView.Rows[i].Cells["GTxtSNo"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtBarcode"].Value = _GetView.Rows[i].Cells["GTxtBarcode"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtUnitId"].Value = _GetView.Rows[i].Cells["GTxtUnitId"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtUnit"].Value = _GetView.Rows[i].Cells["GTxtUnit"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtMRP"].Value = _GetView.Rows[i].Cells["GTxtMRP"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtTrade"].Value = _GetView.Rows[i].Cells["GTxtTrade"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtWholesales"].Value =
                    _GetView.Rows[i].Cells["GTxtWholesales"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtRetails"].Value = _GetView.Rows[i].Cells["GTxtRetails"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtDealer"].Value = _GetView.Rows[i].Cells["GTxtDealer"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtReseller"].Value =
                    _GetView.Rows[i].Cells["GTxtReseller"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtSalesRate"].Value =
                    _GetView.Rows[i].Cells["GTxtSalesRate"].Value?.ToString();
                DGrid.Rows[i].Cells["GTxtAltUnit"].Value = _GetView.Rows[i].Cells["GTxtAltUnit"].Value?.ToString();
            }

            DGrid.Rows.Add();
            ObjGlobal.DGridColorCombo(DGrid);
        }
        else
        {
            if (_ProductId <= 0)
            {
                return;
            }
            var dtProduct = _barcodeListRepository.GetProductBarcodeList(_ProductId);
            if (dtProduct.Rows.Count <= 0)
            {
                return;
            }
            DGrid.Rows.Clear();
            DGrid.Rows.Add(dtProduct.Rows.Count);
            for (var i = 0; i < dtProduct.Rows.Count; i++)
            {
                DGrid.Rows[i].Cells["GTxtSNo"].Value = i + 1;
                DGrid.Rows[i].Cells["GTxtBarcode"].Value = dtProduct.Rows[i]["Barcode"].ToString();
                DGrid.Rows[i].Cells["GTxtUnitId"].Value =
                    ObjGlobal.ReturnDecimal(dtProduct.Rows[i]["UnitId"].ToString());
                DGrid.Rows[i].Cells["GTxtUnit"].Value = dtProduct.Rows[i]["UOM"].ToString();
                DGrid.Rows[i].Cells["GTxtMRP"].Value = ObjGlobal.ReturnDecimal(dtProduct.Rows[i]["MRP"].ToString())
                    .ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtTrade"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["Trade"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtWholesales"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["Wholesale"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtRetails"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["Retail"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtDealer"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["Dealer"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtReseller"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["Resellar"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                DGrid.Rows[i].Cells["GTxtSalesRate"].Value = ObjGlobal
                    .ReturnDecimal(dtProduct.Rows[i]["SalesRate"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                var iReturn = ObjGlobal.ReturnInt(dtProduct.Rows[i]["isAltUnit"].ToString());
                DGrid.Rows[i].Cells["GTxtAltUnit"].Value = iReturn > 0 ? true : false;
            }

            DGrid.Rows.Add();
            ObjGlobal.DGridColorCombo(DGrid);
        }

        DGrid.ClearSelection();
    }

    private void AddToGridFunction(bool isUpdate)
    {
        if (string.IsNullOrEmpty(TxtBarcode.Text.Trim()))
        {
            MessageBox.Show(@"BARCODE CANNOT BE BLANK PLEASE ENTER PRODUCT BARCODE..!!", ObjGlobal.Caption);
            TxtBarcode.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(TxtBarcode.Text.Trim()))
        {
            if (DGrid != null && DGrid.RowCount - 1 > 0 && !isUpdateGrid)
            {
                var exists = DGrid.Rows.Cast<DataGridViewRow>().Where(row => !row.IsNewRow).Select(row => row.Cells["GTxtBarcode"].Value?.ToString()).Any(x => TxtBarcode.Text == x);
                if (exists)
                {
                    MessageBox.Show(@"BARCODE ALREADY EXITS ON SELECTED PRODUCT ..!!", ObjGlobal.Caption);
                    TxtBarcode.Focus();
                    return;
                }
            }
        }

        if (TxtUnit.IsBlankOrEmpty())
        {
            TxtUnit.WarningMessage("UNIT IS REQUIRED..!!");
            return;
        }
        if (TxtSalesRate.GetDouble() is 0)
        {
            TxtSalesRate.WarningMessage("SALES RATE IS REQUIRED..!!");
            return;
        }

        var iRows = 0;
        if (isUpdate)
        {
            iRows = GridId;
        }
        else if (DGrid != null)
        {
            iRows = DGrid.Rows.Count - 1;
        }

        if (DGrid != null)
        {
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = TxtSNo.Text;
            DGrid.Rows[iRows].Cells["GTxtBarcode"].Value = TxtBarcode.Text;
            DGrid.Rows[iRows].Cells["GTxtUnitId"].Value = UnitId;
            DGrid.Rows[iRows].Cells["GTxtUnit"].Value = TxtUnit.Text;
            DGrid.Rows[iRows].Cells["GTxtMRP"].Value = TxtMrpBox.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtTrade"].Value = TxtTradePrice.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtWholesales"].Value = TxtWholeSales.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtRetails"].Value = TxtRetail.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtDealer"].Value = TxtDealer.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtReseller"].Value = TxtReSeller.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtSalesRate"].Value = TxtSalesRate.GetDecimalString();
            DGrid.Rows[iRows].Cells["GTxtAltUnit"].Value = IsAltUnit;
            if (!isUpdate)
            {
                DGrid.Rows.Add();
            }
            DGrid.CurrentCell = DGrid.Rows[iRows + 1].Cells[colIndex];
            ObjGlobal.DGridColorCombo(DGrid);
        }

        ClearDetails();
        if (isUpdate)
        {
            EnableControl();
        }
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
        var columnIndex = DGrid.Columns["GTxtSNo"].Index;

        TxtSNo.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSNo.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSNo.Tag = columnIndex;
        TxtSNo.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtBarcode"].Index;
        TxtBarcode.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBarcode.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBarcode.Tag = columnIndex;
        TxtBarcode.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUnit"].Index;
        TxtUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.Tag = columnIndex;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtMRP"].Index;
        TxtMrpBox.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtMrpBox.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtMrpBox.TextAlign = HorizontalAlignment.Right;
        TxtMrpBox.Tag = columnIndex;
        TxtMrpBox.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtTrade"].Index;
        TxtTradePrice.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtTradePrice.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtTradePrice.TextAlign = HorizontalAlignment.Right;
        TxtTradePrice.Tag = columnIndex;
        TxtTradePrice.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtWholesales"].Index;
        TxtWholeSales.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtWholeSales.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtWholeSales.TextAlign = HorizontalAlignment.Right;
        TxtWholeSales.Tag = columnIndex;
        TxtWholeSales.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRetails"].Index;
        TxtRetail.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRetail.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRetail.TextAlign = HorizontalAlignment.Right;
        TxtRetail.Tag = columnIndex;
        TxtRetail.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtDealer"].Index;
        TxtDealer.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtDealer.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtDealer.TextAlign = HorizontalAlignment.Right;
        TxtDealer.Tag = columnIndex;
        TxtDealer.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtReseller"].Index;
        TxtReSeller.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtReSeller.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtReSeller.TextAlign = HorizontalAlignment.Right;
        TxtReSeller.Tag = columnIndex;
        TxtReSeller.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtSalesRate"].Index;
        TxtSalesRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSalesRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSalesRate.TextAlign = HorizontalAlignment.Right;
        TxtSalesRate.Tag = columnIndex;
        TxtSalesRate.TabIndex = columnIndex;

        DGrid.Rows[rowIndex].Selected = true;
    }

    private void ClearDetails()
    {
        isUpdateGrid = false;
        IsAltUnit = false;
        UnitId = 0;
        TxtUnit.Clear();
        TxtBarcode.Clear();
        TxtSalesRate.Clear();
        TxtTradePrice.Clear();
        TxtMrpBox.Clear();
        TxtWholeSales.Clear();
        TxtRetail.Clear();
        TxtReSeller.Clear();
        TxtDealer.Clear();
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
        }
        TxtSNo.Text = DGrid.Rows.Count.ToString();
        AdjustControlsInDataGrid();
    }

    private void EnableControl(bool isTrue = false)
    {
        TxtSNo.Visible = TxtSNo.Enabled = isTrue;
        TxtBarcode.Visible = TxtBarcode.Enabled = isTrue;
        TxtUnit.Visible = TxtUnit.Enabled = isTrue;
        TxtSalesRate.Visible = TxtSalesRate.Enabled = isTrue;
        TxtTradePrice.Visible = TxtTradePrice.Enabled = isTrue;
        TxtMrpBox.Visible = TxtMrpBox.Enabled = isTrue;
        TxtWholeSales.Visible = TxtWholeSales.Enabled = isTrue;
        TxtRetail.Visible = TxtRetail.Enabled = isTrue;
        TxtReSeller.Visible = TxtReSeller.Enabled = isTrue;
        TxtDealer.Visible = TxtDealer.Enabled = isTrue;
    }

    private void InitializeGridComponent()
    {
        TxtSNo = new MrGridTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = true
        };

        TxtBarcode = new MrGridTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtBarcode.KeyPress += TxtBarcode_KeyPress;
        TxtBarcode.Validated += TxtBarcode_Validated;

        TxtUnit = new MrGridTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ReadOnly = true
        };
        TxtUnit.KeyDown += TxtUnit_KeyDown;

        TxtMrpBox = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtMrpBox.KeyPress += TxtMrpBox_KeyPress;
        TxtMrpBox.Validating += TxtMrpBox_Validating;

        TxtTradePrice = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtTradePrice.KeyPress += TxtMrpBox_KeyPress;
        TxtTradePrice.Validating += TxtTradePrice_Validating;

        TxtWholeSales = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtWholeSales.KeyPress += TxtMrpBox_KeyPress;
        TxtWholeSales.Validating += TxtWholeSales_Validating;

        TxtRetail = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtRetail.KeyPress += TxtMrpBox_KeyPress;
        TxtRetail.Validating += TxtRetail_Validating;

        TxtDealer = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtDealer.KeyPress += TxtMrpBox_KeyPress;
        TxtDealer.Validating += TxtDealer_Validating;

        TxtReSeller = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtReSeller.KeyPress += TxtMrpBox_KeyPress;
        TxtReSeller.Validating += TxtReSeller_Validating;

        TxtSalesRate = new MrGridNumericTextBox(DGrid)
        {
            Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };
        TxtSalesRate.KeyPress += TxtSalesRate_KeyPress;
        TxtSalesRate.Validated += TxtSalesRate_Validated;
    }

    private void OpenProductUnit()
    {
        var (description, id) = GetMasterList.GetProductUnit("SAVE");
        if (id > 0)
        {
            TxtUnit.Text = description;
            UnitId = id;
        }
        TxtUnit.Focus();
    }

    private void GridToTextBox()
    {
        AdjustControlsInDataGrid();
        TxtSNo.Text = DGrid.Rows[GridId].Cells["GTxtSNo"].Value.ToString();
        TxtBarcode.Text = DGrid.Rows[GridId].Cells["GTxtBarcode"].Value.ToString();
        UnitId = ObjGlobal.ReturnInt(DGrid.Rows[GridId].Cells["GTxtUnitId"].Value?.ToString());
        TxtUnit.Text = DGrid.Rows[GridId].Cells["GTxtUnit"].Value?.ToString();
        TxtMrpBox.Text = DGrid.Rows[GridId].Cells["GTxtMRP"].Value.ToString();
        TxtTradePrice.Text = DGrid.Rows[GridId].Cells["GTxtTrade"].Value.ToString();
        TxtWholeSales.Text = DGrid.Rows[GridId].Cells["GTxtWholesales"].Value.ToString();
        TxtRetail.Text = DGrid.Rows[GridId].Cells["GTxtRetails"].Value.ToString();
        TxtDealer.Text = DGrid.Rows[GridId].Cells["GTxtDealer"].Value.ToString();
        TxtReSeller.Text = DGrid.Rows[GridId].Cells["GTxtReseller"].Value.ToString();
        TxtSalesRate.Text = DGrid.Rows[GridId].Cells["GTxtSalesRate"].Value.ToString();
        IsAltUnit = ObjGlobal.ReturnBool(DGrid.Rows[GridId].Cells["GTxtAltUnit"].Value?.ToString());
        isUpdateGrid = true;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- GLOBAL VALUE ---------------

    public int rowIndex;
    public int colIndex;
    public int GridId;
    public int UnitId;

    public long _ProductId;

    private bool IsAltUnit;
    private bool isUpdateGrid;
    private readonly IBarcodeListRepository _barcodeListRepository;
    public DataGridView _GetView;

    private MrGridTextBox TxtSNo;
    private MrGridTextBox TxtBarcode;
    private MrGridTextBox TxtUnit;

    private MrGridNumericTextBox TxtMrpBox;
    private MrGridNumericTextBox TxtTradePrice;
    private MrGridNumericTextBox TxtWholeSales;
    private MrGridNumericTextBox TxtRetail;
    private MrGridNumericTextBox TxtDealer;
    private MrGridNumericTextBox TxtReSeller;
    private MrGridNumericTextBox TxtSalesRate;

    #endregion --------------- GLOBAL VALUE ---------------

    private void DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}