using DatabaseModule.DataEntry.OpeningMaster;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.OpeningMaster;

public partial class FrmProductOpeningEntry : MrForm
{
    //EVENTS OF THIS FORM

    #region --------------- PRODUCT OPENING FORM ---------------

    public FrmProductOpeningEntry(bool isZoom = false, string voucherNo = "")
    {
        InitializeComponent();
        _dtBatchInfo = _setup.GetProductBatchFormat();
        _productOpeningRepository = new ProductOpeningRepository();
        GetGridColumns();
        AdjustControlsInDataGrid();
        EnableControl();
        ClearControl();
    }

    private void FrmProductOpeningEntry_Shown(object sender, EventArgs e)
    {
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmProductOpeningEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape) return;
        if (BtnNew.Enabled)
        {
            var result = CustomMessageBox.ExitActiveForm();
            if (result == DialogResult.Yes) Close();
        }
        else
        {
            if (TxtProduct.Enabled)
            {
                ClearLedgerDetails();
                EnableGridControl();
                DGrid.Focus();
                SendToBack();
            }
            else
            {
                _actionTag = string.Empty;
                EnableControl();
                ClearControl();
                BtnNew.Focus();
            }
        }
    }

    private void FrmProductOpeningEntry_Load(object sender, EventArgs e)
    {

        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        BtnSave.Text = @"&" + _actionTag;
        //AddTextToGrid(true);
        TxtVno.Focus();
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        TxtRefNo.Focus();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        TxtRefNo.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _actionTag = "COPY";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        TxtRefNo.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        _actionTag = "PRINT";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        TxtRefNo.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnVno_Click(sender, e);
        else if (TxtVno.ReadOnly)
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, "POP", MskDate.Text);
        if (result.IsValueExits())
        {
            TxtVno.Text = result;
            if (_actionTag != "SAVE")
            {
                FillProductOpeningVoucher(TxtVno.Text);
            }
        }
        TxtVno.Focus();
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtRefNo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtRefNo.IsValueExits())
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (TxtCurrency.Enabled)
                {
                    TxtCurrency.Focus();
                }
                else if (TxtDepartment.Enabled)
                {
                    TxtDepartment.Focus();
                }
                else
                {
                    DGrid.Focus();
                }
            }
        }

    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenCurrency();
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var frm = new FrmCurrency(true);
            frm.ShowDialog();
            TxtCurrency.Text = frm.CurrencyDesc;
            _currencyId = frm.CurrencyId;
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCurrency, OpenCurrency);
        }
    }

    private void TxtCurrency_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidForm())
        {
            if (SaveProductOpening() > 0)
            {
                this.NotifySuccess($@"{TxtVno.Text} VOUCHER NUMBER {_actionTag} SUCCESSFULLY..!!");
                ClearControl();
                BtnSave.Enabled = true;
                if (_actionTag != "SAVE")
                {
                    BtnVno.Enabled = TxtVno.Enabled = true;
                    TxtVno.Clear();
                    TxtVno.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(TxtVno.Text))
                        TxtVno.Focus();
                    else
                        DGrid.Focus();
                }
            }
            else
            {
                this.NotifyValidationError(DGrid, @"ERROR OCCURS WHILE PRODUCT OPENING..!!");
                BtnSave.Enabled = true;
            }
        }
        else
        {
            this.NotifyValidationError(DGrid, @"ERROR OCCURS WHILE PRODUCT OPENING..!!");
            BtnSave.Enabled = true;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Close();
    }

    private void TxtRemarks_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.StockRemarksMandatory && TxtRemarks.IsBlankOrEmpty())
            this.NotifyValidationError(TxtRemarks, "REMARKS IS MANDATORY. PLEASE ENTER REMARKS OF VOUCHER");
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.StockRemarksMandatory && TxtRemarks.IsBlankOrEmpty())
            {
                e.SuppressKeyPress = true;
                return;
            }

            SendKeys.Send("{TAB}");
        }
    }

    private void GlobalEnter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && !TxtProduct.Enabled)
        {
            DGrid.Rows.RemoveAt(_rowIndex);
            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }
            GetSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                TextFromGrid();
                TxtProduct.Focus();
                return;
            }
            GetSerialNo();
            TxtProduct.Focus();
        }
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void DGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (CustomMessageBox.DeleteRow() is DialogResult.No)
        {
            return;
        }
        _isRowDelete = true;
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete)
        {
            return;
        }
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
        }
        GetSerialNo();
    }

    #endregion --------------- PRODUCT OPENING FORM ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD OF THIS FORM ---------------

    private void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("POP");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("POP", _docDesc);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("POP", "AMS.ProductOpening", "Voucher_No");
            if (wnd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(wnd.VNo))
            {
                return;
            }
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_tagStrings.Contains(_actionTag);
        TxtVno.Enabled = BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = false;
        TxtCurrency.Enabled = BtnCurrency.Enabled = isEnable && ObjGlobal.FinanceCurrencyEnable;
        TxtCurrencyRate.Enabled = TxtCurrency.Enabled;
        TxtRefNo.Enabled = MskRefDate.Enabled = isEnable;
        TabLedgerOpening.Enabled = !_tagStrings.Contains(_actionTag) && isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.StockDepartmentEnable;
        TxtRemarks.Enabled = btnRemarks.Enabled = isEnable && ObjGlobal.StockRemarksEnable;
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        EnableGridControl();
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? "PRODUCT OPENING BALANCE ENTRY"
            : $"PRODUCT OPENING BALANCE ENTRY [{_actionTag}]";
        TxtVno.Clear();
        TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("POP", _docDesc) : String.Empty;
        TxtVno.ReadOnly = !_actionTag.Equals("SAVE");
        MskDate.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetDateString();
        MskMiti.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetNepaliDate();
        TxtRefNo.Clear();
        TxtRemarks.Clear();
        LblTotalLocalAmount.Text = String.Empty;
        LblTotalAmount.Text = String.Empty;
        LblTotalQty.Text = String.Empty;
        DGrid.ReadOnly = true;
        DGrid.ClearSelection();
        DGrid.Rows.Clear();
        ClearLedgerDetails();
        DGrid.ClearSelection();
    }

    private void ClearLedgerDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            TxtSno.Text = DGrid.RowCount.ToString();
        }

        _isRowUpdate = false;
        _productId = 0;
        TxtProduct.Clear();
        _godownId = 0;
        TxtGodown.Clear();
        TxtAltQty.Clear();
        _altUnitId = 0;
        TxtAltUom.Clear();
        TxtQty.Clear();
        _unitId = 0;
        TxtUom.Clear();
        TxtRate.Clear();
        TxtAmount.Clear();
        AdjustControlsInDataGrid();
        VoucherTotalCalculation();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;
        TxtProduct.Enabled = TxtProduct.Visible = isEnable;
        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.StockGodownEnable;
        TxtAltQty.Enabled = false;
        TxtAltQty.Visible = isEnable;
        TxtAltUom.Enabled = false;
        TxtAltUom.Visible = isEnable;
        TxtQty.Enabled = TxtQty.Visible = isEnable;
        TxtUom.Enabled = false;
        TxtUom.Visible = isEnable;
        TxtRate.Enabled = TxtRate.Visible = isEnable;
        TxtAmount.Enabled = TxtAmount.Visible = isEnable;
    }

    private void SetProductInfo(long productId)
    {
        if (productId is 0)
        {
            return;
        }
        var dtLedger = _setup.GetMasterProductList(_actionTag, productId);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }
        foreach (DataRow row in dtLedger.Rows)
        {
            //LblShortName.Text = row["PShortName"].ToString();
            LblAltQty.Text = row["AltStockQty"].ToString();
            LblQty.Text = row["StockQty"].ToString();
            _altUnitId = row["PAltUnit"].GetInt();
            _unitId = row["PUnit"].GetInt();
            _conQty = row["PQtyConv"].GetDecimal();
            TxtAltUom.Text = row["AltUnitCode"].GetString();
            TxtUom.Text = row["UnitCode"].GetString();
            TxtAltQty.Enabled = _altUnitId > 0;
            TxtRate.Text = row["PBuyRate"].GetDecimalString();
            LblBuyRate.Text = row["PBuyRate"].GetDecimalString();
            LblSalesRate.Text = row["PSalesRate"].GetDecimalString();
            _isBatch = row["PBatchwise"].GetBool();
            if (_isBatch)
            {
                CallProductBatch();
            }
        }
    }

    private void OpenProductList()
    {
        var result = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        if (result.id > 0)
        {
            TxtProduct.Text = result.description;
            _productId = result.id;
            SetProductInfo(_productId);

        }
        TxtProduct.Focus();
    }

    private void OpenGodownList()
    {
        (TxtGodown.Text, _godownId) = GetMasterList.GetGodown(_actionTag);
    }

    private void OpenCurrency()
    {
        (TxtCurrency.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
    }

    private void GetGridColumns()
    {
        _voucher.GetProductOpeningEntry(DGrid);
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        if (DGrid.Columns["GTxtGodown"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;
        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
        DGrid.CellEnter += (_, e) => _columnIndex = e.ColumnIndex;
        DGrid.UserDeletedRow += DGrid_UserDeletedRow;
        DGrid.UserDeletingRow += DGrid_UserDeletingRow;
        DGrid.KeyDown += DGrid_KeyDown;
        DGrid.EnterKeyPressed += DGrid_EnterKeyPressed;
        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += async (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                if (id > 0)
                {
                    TxtProduct.Text = description;
                    _productId = id;
                    SetProductInfo(_productId);
                }
                TxtProduct.Focus();
            }
            else if (e.KeyCode is Keys.F3)
            {
                if (_productId <= 0)
                {
                    return;
                }
                if (_isBatch)
                {
                    CallProductBatch();
                }
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var (description, id) = GetMasterList.CreateProduct(true);
                if (id > 0)
                {
                    TxtProduct.Text = description;
                    _productId = id;
                    SetProductInfo(_productId);
                }
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtProduct, OpenProductList);
            }
        };
        TxtProduct = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };

        TxtProduct.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                if (id > 0)
                {
                    TxtProduct.Text = description;
                    _productId = id;
                    SetProductInfo(_productId);
                }
                TxtProduct.Focus();
            }
            else if (e.KeyCode is Keys.F3)
            {
                if (_productId <= 0)
                {
                    return;
                }
                if (_isBatch)
                {
                    CallProductBatch();
                }
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var (description, id) = GetMasterList.CreateProduct(true);
                if (id > 0)
                {
                    TxtProduct.Text = description;
                    _productId = id;
                    SetProductInfo(_productId);
                }
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtProduct, OpenProductList);
            }
        };
        TxtProduct.Validating += (_, e) =>
        {
            if (DGrid.RowCount is 0)
            {
                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            }

            if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;
                    this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                    return;
                }
            }

            if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                EnableGridControl();
                DGrid.ClearSelection();
                if (TxtRemarks.Enabled)
                    TxtRemarks.Focus();
                else
                    BtnSave.Focus();
            }
        };
        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenGodownList();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var frm = new FrmGodownName(true);
                frm.ShowDialog();
                TxtGodown.Text = frm.GodownMaster;
                _godownId = frm.GodownId;
                TxtGodown.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtGodown, OpenGodownList);
            }
        };
        TxtAltQty = new MrGridNumericTextBox(DGrid);
        TxtAltQty.Validating += (sender, e) =>
        {
            if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0)
            {
                TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
            }
        };
        TxtAltQty.TextChanged += (sender, e) =>
        {
            if (!TxtAltQty.Focused) return;
            if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
            {
                TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
            }
            else if (TxtAltQty.GetDecimal() is 0)
            {
                TxtQty.Text = 1.GetDecimalQtyString();
            }
        };
        TxtAltUom = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += (sender, e) =>
        {
            TxtQty.Text = TxtQty.GetDecimalQtyString();
            if (!TxtQty.Enabled || !TxtProduct.Enabled)
            {
                return;
            }

            if (TxtQty.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
            }
        };
        TxtQty.TextChanged += (sender, e) =>
        {
            if (!TxtQty.Focused)
            {
                return;
            }
            TxtAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        };
        TxtUom = new MrGridTextBox(DGrid);
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
        };

        TxtRate.TextChanged += (sender, e) =>
        {
            if (!TxtRate.Focused) return;
            TxtAmount.Text = TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        };
        TxtAmount = new MrGridNumericTextBox(DGrid);
        TxtAmount.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
        };
        TxtAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtRate)
            {
                return;
            }
            if (TxtProduct.Enabled && TxtProduct.IsValueExits())
            {
                AddTextToGrid(_isRowUpdate);
                TxtProduct.Focus();
            }
        };
        ObjGlobal.DGridColorCombo(DGrid);
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null) return;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.GetString();
        _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();
        SetProductInfo(_productId);
        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUnitId"].Value.GetInt();
        TxtAltQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltQty"].Value.GetString();
        TxtAltUom.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltUnit"].Value.GetString();
        _unitId = DGrid.Rows[_rowIndex].Cells["GTxtUnitId"].Value.GetInt();
        TxtQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtQty"].Value.GetString();
        TxtUom.Text = DGrid.Rows[_rowIndex].Cells["GTxtUnit"].Value.GetString();
        TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.GetString();
        TxtAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetString();
        _isRowUpdate = true;
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
        var currentRow = _rowIndex;
        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtProduct"].Index;
        TxtProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtGodown"].Index;
        TxtGodown.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtGodown.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtGodown.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltQty"].Index;
        TxtAltQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltUnit"].Index;
        TxtAltUom.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltUom.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltUom.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUnit"].Index;
        TxtUom.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUom.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUom.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"].Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAmount.TabIndex = columnIndex;
    }

    private void AddTextToGrid(bool isUpdate)
    {
        if (ObjGlobal.StockGodownMandatory && TxtGodown.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtGodown, "GODOWN IS MANDATORY..!!");
            return;
        }

        if (ObjGlobal.FinanceCurrencyEnable && TxtCurrency.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY..!!");
            return;
        }

        if (ObjGlobal.StockDepartmentMandatory && TxtDepartment.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtDepartment, "DEPARTMENT IS MANDATORY..!!");
            return;
        }

        if (TxtQty.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtQty, "PRODUCT QTY IS CANNOT BE ZERO..!!");
            return;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSno"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId.ToString();
        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.Text;
        DGrid.Rows[iRows].Cells["GTxtAltUnitId"].Value = _altUnitId;
        DGrid.Rows[iRows].Cells["GTxtAltUnit"].Value = TxtAltUom.Text;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.Text;
        DGrid.Rows[iRows].Cells["GTxtUnitId"].Value = _unitId;
        DGrid.Rows[iRows].Cells["GTxtUnit"].Value = TxtUom.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalAmount"].Value = TxtCurrencyRate.GetDecimal() > 0
            ? (TxtAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString()
            : TxtAmount.GetDecimalString();
        var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnableGridControl();
            ClearLedgerDetails();
            DGrid.Focus();
            return;
        }

        ClearLedgerDetails();
        TxtProduct.AcceptsTab = false;
        GetSerialNo();
    }

    private void FillProductOpeningVoucher(string voucherNo)
    {
        var dsVoucher = _productOpeningRepository.GetProductOpeningVoucherDetails(voucherNo);
        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            if (_actionTag != "SAVE") TxtVno.Text = dsVoucher.Tables[0].Rows[0]["Voucher_No"].ToString();
            MskDate.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetDateString();
            MskMiti.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetNepaliDate();
            TxtCurrency.Text = dsVoucher.Tables[0].Rows[0]["Ccode"].ToString();
            TxtCurrencyRate.Text = dsVoucher.Tables[0].Rows[0]["Currency_Rate"].GetDecimalString();
            TxtRemarks.Text = dsVoucher.Tables[0].Rows[0]["Remarks"].ToString();
            if (dsVoucher.Tables[0].Rows[0]["Cls1"].GetInt() > 0)
            {
                TxtDepartment.Text = dsVoucher.Tables[0].Rows[0]["Department1"].ToString();
                _departmentId = dsVoucher.Tables[0].Rows[0]["Cls1"].GetInt();
            }

            if (dsVoucher.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsVoucher.Tables[1].Rows.Count + 1);
                foreach (DataRow ro in dsVoucher.Tables[1].Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSno"].Value = iRows + 1;
                    DGrid.Rows[iRows].Cells["GTxtProductId"].Value = ro["Product_Id"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtProduct"].Value = ro["PName"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtShortName"].Value = ro["PShortName"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = ro["Godown_Id"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtGodown"].Value = ro["GName"].GetString();

                    DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = ro["AltQty"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAltUnitId"].Value = ro["AltUnit"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtAltUnit"].Value = ro["AltUnitCode"].GetString();

                    DGrid.Rows[iRows].Cells["GTxtQty"].Value = ro["Qty"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtUnitId"].Value = ro["QtyUnit"].GetString();
                    DGrid.Rows[iRows].Cells["GTxtUnit"].Value = ro["QtyUnitCode"].GetString();

                    DGrid.Rows[iRows].Cells["GTxtRate"].Value = ro["Rate"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = ro["Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtLocalAmount"].Value = ro["LocalAmount"].GetDecimal() > 0
                        ? ro["LocalAmount"].GetDecimalString()
                        : ro["Amount"].GetDecimalString();
                    iRows++;
                }
            }
        }

        ObjGlobal.DGridColorCombo(DGrid);
        VoucherTotalCalculation();
        DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
        DGrid.ClearSelection();
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount > 1)
        {
            LblTotalAltQty.Text = DGrid.Rows.OfType<DataGridViewRow>()
                .Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal()).GetDecimalString();
            LblTotalQty.Text = DGrid.Rows.OfType<DataGridViewRow>()
                .Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
            LblTotalAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
                .Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
            LblTotalLocalAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
                .Sum(row => row.Cells["GTxtLocalAmount"].Value.GetDecimal()).GetDecimalString();
        }

        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalAmount.Text);
    }

    private void GetSerialNo()
    {
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    private bool IsValidForm()
    {
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) is DialogResult.No)
            {
                return false;
            }
        }

        if (_actionTag != "DELETE")
        {
            if (TxtVno.IsBlankOrEmpty())
            {
                if (!TxtVno.Enabled) TxtVno.Enabled = true;
                this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS BLANK..!!");
            }

            if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtProductId"].Value.IsBlankOrEmpty())
            {
                this.NotifyValidationError(DGrid, "VOUCHER DETAILS IS BLANK..!!");
            }

            if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
                this.NotifyValidationError(TxtRemarks, "VOUCHER REMARKS IS BLANK..!!");
        }
        return true;
    }

    private int SaveProductOpening()
    {
        try
        {
            const int syncRow = 0;
            var sync = syncRow.ReturnSyncRowNo("POP", _openingId.ToString());
            _productOpeningRepository.VmProductOpening.Details = [];

            if (_actionTag == "SAVE")
            {
                TxtVno.Text = TxtVno.GetCurrentVoucherNo("POP", _docDesc);
                _openingId = _openingId.ReturnMaxIntId("POP", "OpeningId");
            }

            _productOpeningRepository.VmProductOpening.OpeningId = _openingId;
            _productOpeningRepository.VmProductOpening.Voucher_No = TxtVno.Text;
            _productOpeningRepository.VmProductOpening.OP_Date = DateTime.Parse(MskDate.Text);
            _productOpeningRepository.VmProductOpening.OP_Miti = MskMiti.Text.Contains("/") ? MskMiti.Text : MskMiti.Text.Replace("-", "/");
            _productOpeningRepository.VmProductOpening.Godown_Id = _godownId > 0 ? _godownId : null;
            _productOpeningRepository.VmProductOpening.Remarks = TxtRemarks.Text;
            _productOpeningRepository.VmProductOpening.CBranch_Id = ObjGlobal.SysBranchId;
            _productOpeningRepository.VmProductOpening.Cls1 = _departmentId > 0 ? _departmentId : null;
            _productOpeningRepository.VmProductOpening.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _productOpeningRepository.VmProductOpening.CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            _productOpeningRepository.VmProductOpening.Currency_Rate = 1;
            _productOpeningRepository.VmProductOpening.IsReverse = false;
            _productOpeningRepository.VmProductOpening.Enter_Date = DateTime.Now;
            _productOpeningRepository.VmProductOpening.Enter_By = ObjGlobal.LogInUser;
            _productOpeningRepository.VmProductOpening.ProductBatch = _dtBatchInfo;
            _productOpeningRepository.VmProductOpening.GetView = DGrid;

            _productOpeningRepository.Details.Clear();
            foreach (DataGridViewRow item in DGrid.Rows)
            {
                var list = new ProductOpening();
                var result = item.Cells["GTxtProductId"].Value.GetLong();
                if (result == 0)
                {
                    continue;
                }

                list.OpeningId = _openingId;
                list.Voucher_No = TxtVno.Text.Trim();
                list.Serial_No = item.Cells["GTxtSNo"].Value.GetInt();
                list.OP_Date = MskDate.Text.GetDateTime();
                list.OP_Miti = MskMiti.Text.Trim();
                list.Product_Id = item.Cells["GTxtProductId"].Value.GetLong();
                list.Godown_Id = item.Cells["GTxtGodownId"].Value.GetInt() > 0 ? item.Cells["GTxtGodownId"].Value.GetInt() : null;
                list.Cls1 = _departmentId > 0 ? _departmentId : null;
                list.Cls2 = 0;
                list.Cls3 = 0;
                list.Cls4 = 0;
                list.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
                list.Currency_Rate = TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
                list.AltQty = item.Cells["GTxtAltQty"].Value.GetDecimal();
                list.AltUnit = item.Cells["GTxtAltUnit"].Value.GetInt() > 0 ? item.Cells["GTxtAltUnit"].Value.GetInt() : null;
                list.Qty = item.Cells["GTxtQty"].Value.GetDecimal();
                list.QtyUnit = item.Cells["GTxtUnit"].Value.GetInt() > 0 ? item.Cells["GTxtUnit"].Value.GetInt() : null; //to avoid foreign key constrain error while syncing data
                list.Rate = item.Cells["GTxtRate"].Value.GetDecimal();
                list.LocalRate = item.Cells["GTxtRate"].Value.GetDecimal();
                list.Amount = item.Cells["GTxtAmount"].Value.GetDecimal();
                list.LocalAmount = item.Cells["GTxtLocalAmount"].Value.GetDecimal();
                list.IsReverse = false;
                list.CancelRemarks = "";
                list.CancelBy = ObjGlobal.LogInUser;
                list.CancelDate = DateTime.Now;
                list.Remarks = TxtRemarks.Text;
                list.Enter_By = ObjGlobal.LogInUser;
                list.Enter_Date = DateTime.Now;
                list.Reconcile_By = ObjGlobal.LogInUser;
                list.Reconcile_Date = DateTime.Now;
                list.CBranch_Id = ObjGlobal.SysBranchId;
                list.CUnit_Id = _unitId > 0 ? _unitId : null;
                list.FiscalYearId = ObjGlobal.SysFiscalYearId;
                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                    ? ObjGlobal.LocalOriginId.GetValueOrDefault()
                    : Guid.Empty;
                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = sync;

                _productOpeningRepository.Details.Add(list);
            }

            _productOpeningRepository.VmProductOpening.SyncRowVersion = sync;

            return _productOpeningRepository.SaveProductOpeningSetup(_actionTag);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private void CallProductBatch()
    {
        var exitRows = _dtBatchInfo.Select($"ProductId = '{_productId}' and ProductSno='{TxtSno.Text}'");
        var result = new FrmProductBatchList(exitRows is { Length: > 0 } ? exitRows.CopyToDataTable() : new System.Data.DataTable())
        {
            ProductId = _productId,
            ProductSno = TxtSno.GetInt()
        };
        result.ShowDialog();

        if (result.DialogResult == DialogResult.OK)
        {
            TxtQty.Text = result.LblTotalQty.Text;
            TxtAltQty.Text = (TxtQty.GetDecimal() / _conQty).GetDecimalString();
            ProductBatchInfo(result.ProductInfo);
            TxtQty.Enabled = TxtAltQty.Enabled = false;
            //TxtRate.Focus();
            return;
        }
        else
        {
            TxtQty.Enabled = TxtAltQty.Enabled = true;
            if (exitRows is not { Length: > 0 })
            {
                return;
            }
            foreach (var row in exitRows)
            {
                _dtBatchInfo.Rows.Remove(row);
            }
        }
    }

    private void ProductBatchInfo(System.Data.DataTable dt)
    {
        if (DGrid.CurrentRow != null)
        {
            var serialNo = TxtSno.Text;
            var exDetails = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
            if (exDetails)
            {
                var exitAny = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.ToString().Trim()));
                if (exitAny)
                {
                    foreach (DataRow ro in dt.Rows)
                    {
                        foreach (DataRow row in _dtBatchInfo.Rows)
                        {
                            if (row["ProductSno"].GetInt() == ro["ProductSno"].GetInt() && row["ProductId"].GetLong() == ro["ProductId"].GetLong())
                            {
                                var index = _dtBatchInfo.Rows.IndexOf(row);
                                _dtBatchInfo.Rows[index].SetField("BatchNo", ro["BatchNo"]);
                                _dtBatchInfo.Rows[index].SetField("MfDate", ro["MfDate"]);
                                _dtBatchInfo.Rows[index].SetField("ExpDate", ro["ExpDate"]);
                                _dtBatchInfo.Rows[index].SetField("Qty", ro["Qty"]);
                                _dtBatchInfo.Rows[index].SetField("MRP", ro["MRP"]);
                                _dtBatchInfo.AcceptChanges();
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow ro in dt.Rows)
                    {
                        var dataRow = _dtBatchInfo.NewRow();
                        dataRow["SNo"] = ro["SNo"];
                        dataRow["ProductId"] = ro["ProductId"];
                        dataRow["BatchNo"] = ro["BatchNo"];
                        dataRow["MfDate"] = ro["MfDate"];
                        dataRow["ExpDate"] = ro["ExpDate"];
                        dataRow["Qty"] = ro["Qty"];
                        dataRow["MRP"] = ro["MRP"];
                        dataRow["ProductSno"] = serialNo;
                        _dtBatchInfo.Rows.InsertAt(dataRow, _dtBatchInfo.RowsCount() + 1);
                    }
                }
            }
            else
            {
                foreach (DataRow ro in dt.Rows)
                {
                    var dataRow = _dtBatchInfo.NewRow();
                    dataRow["SNo"] = ro["SNo"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["BatchNo"] = ro["BatchNo"];
                    dataRow["MfDate"] = ro["MfDate"];
                    dataRow["ExpDate"] = ro["ExpDate"];
                    dataRow["MRP"] = ro["MRP"];
                    dataRow["Qty"] = ro["Qty"];
                    dataRow["ProductSno"] = ro["ProductSno"];
                    _dtBatchInfo.Rows.InsertAt(dataRow, _dtBatchInfo.RowsCount() + 1);
                }
            }
        }
    }

    #endregion --------------- METHOD OF THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region -------------- OBJECT FOR THIS FORM --------------

    private int _godownId;
    private int _altUnitId;
    private int _unitId;
    private int _departmentId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _rowIndex;
    private int _columnIndex;
    private int _openingId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _isBatch;

    private long _productId;

    private decimal _conQty;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;

    private KeyPressEventArgs _getKeys;
    private System.Data.DataTable _dtBatchInfo;

    // private readonly IStockEntry _entry = new ClsStockEntry();
    private readonly IProductOpeningRepository _productOpeningRepository;
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private readonly IStockEntryDesign _voucher = new GetStockDesign();
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtAltUom { get; set; }
    private MrGridTextBox TxtUom { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }

    #endregion -------------- OBJECT FOR THIS FORM --------------
}