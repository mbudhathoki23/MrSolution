using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmRawMaterialReturn : MrForm
{
    // RAW MATERIAL RETURN
    public FrmRawMaterialReturn(bool zoom, string txtZoomVno)
    {
        InitializeComponent();
    }

    private void FrmRawMaterialReturn_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmRawMaterialReturn_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }
        if (TxtProduct.Enabled)
        {
            ClearProductDetails();
            EnableGridControl();
            DGrid.Focus();
        }
        else if (!BtnNew.Enabled)
        {
            if (CustomMessageBox.ExitActiveForm().Equals(DialogResult.No))
            {
                return;
            }
            _actionTag = string.Empty;
            EnableControl();
            ClearControl();
            BtnNew.Focus();
        }
        else
        {
            BtnExit.PerformClick();
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No) return;

            var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
            var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
            DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
            if (DGrid.RowCount is 0) DGrid.Rows.Add();

            GetSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            DGrid.CurrentCell = DGrid.CurrentRow.Cells[0];
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                GetTextFromGrid();
                TxtProduct.Focus();
                return;
            }

            GetSerialNo();
            TxtProduct.Focus();
        }
    }

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void GetSerialNo()
    {
        if (DGrid.RowCount <= 0) return;
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    private void GetGridColumn()
    {
        _design.GetBillOfMaterialsEntryDesign(DGrid);
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        if (DGrid.Columns["GTxtGodown"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;

        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;

        DGrid.GotFocus += (sender, e) => { DGrid.Rows[_rowIndex].Cells[0].Selected = true; };
        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
        DGrid.CellEnter += (_, e) => _columnIndex = e.ColumnIndex;
        DGrid.KeyDown += DGrid_KeyDown;

        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                (TxtProduct.Text, _rProductId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                GetProductInfo(_rProductId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _rProductId) = GetMasterList.CreateProduct(true);
                GetProductInfo(_rProductId);
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
                (TxtProduct.Text, _rProductId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                GetProductInfo(_rProductId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _rProductId) = GetMasterList.CreateProduct(true);
                GetProductInfo(_rProductId);
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
                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");

            if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
                if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                    return;
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
        TxtCostCenter = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtCostCenter.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
                OpenCostCenterList();
            else if (e.Shift && e.KeyCode is Keys.Tab)
                SendToBack();
            else if (e.Control && e.KeyCode == Keys.N)
                (TxtCostCenter.Text, _costCenterId) = GetMasterList.CreateCostCenter(true);
            else
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtCostCenter, OpenCostCenterList);
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
                (TxtGodown.Text, _godownId) = GetMasterList.CreateGodown(true);
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
            if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0) TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
        };
        TxtAltQty.TextChanged += (sender, e) =>
        {
            if (!TxtAltQty.Focused) return;
            if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
                TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
            else if (TxtAltQty.GetDecimal() is 0) TxtQty.Text = 1.GetDecimalQtyString();
        };
        TxtAltUnit = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += (sender, e) =>
        {
            TxtQty.Text = TxtQty.GetDecimalQtyString();
            if (!TxtQty.Enabled || !TxtProduct.Enabled) return;
            if (TxtQty.IsBlankOrEmpty()) this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
        };
        TxtQty.TextChanged += (sender, e) =>
        {
            if (!TxtQty.Focused) return;
            TxtBasicAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        };
        TxtUnit = new MrGridTextBox(DGrid);
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
        };
        TxtRate.TextChanged += (sender, e) =>
        {
            if (!TxtRate.Focused) return;
            TxtBasicAmount.Text = TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        };
        TxtRate.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtQty) return;
            if (!TxtProduct.Enabled || !TxtProduct.IsValueExits()) return;
            AddDataToGridDetails(_isGridUpdate);
            TxtProduct.Focus();
        };
        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
        };
        TxtBasicAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtRate) return;
            if (TxtProduct.Enabled && TxtProduct.IsValueExits())
            {
                AddDataToGridDetails(_isGridUpdate);
                TxtProduct.Focus();
            }
        };
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
    }

    private void ReturnVoucherNo()
    {
        var dtCheck = ClsMasterSetup.GetDocumentNumberingSchema("BOM");
        if (dtCheck.Rows.Count == 1)
        {
            _numberSchema = dtCheck.Rows[0]["DocDesc"].ToString();
            TxtVno.GetCurrentVoucherNo("BOM", _numberSchema);
        }
        else if (dtCheck.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme
            {
                Source = "BOM",
                TblName = "INV.BOM_Master",
                FldNAme = "VoucherNo"
            };
            if (wnd.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(wnd.VNo)) return;

            _numberSchema = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void OpenProductList()
    {
        (TxtProduct.Text, _rProductId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        GetProductInfo(_rProductId);
        TxtProduct.Focus();
    }

    private void OpenGodownList()
    {
        (TxtGodown.Text, _godownId) = GetMasterList.GetGodown(_actionTag);
        TxtGodown.Focus();
    }

    private void OpenCostCenterList()
    {
        (TxtCostCenter.Text, _costCenterId) = GetMasterList.GetCostCenterList(_actionTag);
        TxtCostCenter.Focus();
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
        var currentRow = _rowIndex;

        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtShortName"].Index;
        TxtShortName.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtShortName.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtShortName.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtProduct"].Index;
        TxtProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtGodown"].Index;
        TxtGodown.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtGodown.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtGodown.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtCostCenter"].Index;
        TxtCostCenter.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtCostCenter.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtCostCenter.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltQty"].Index;
        TxtAltQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltUOM"].Index;
        TxtAltUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUOM"].Index;
        TxtUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"].Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtBasicAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBasicAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBasicAmount.TabIndex = columnIndex;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrWhiteSpace(_actionTag)
            ? $"BILL OF MATERIALS SETUP [{_actionTag}]"
            : "BILL OF MATERIALS SETUP";
        TxtVno.Clear();
        TxtVno.ReadOnly = !string.IsNullOrWhiteSpace(_actionTag) && _actionTag != "SAVE";
        DGrid.ClearSelection();
        DGrid.Rows.Clear();
        if (DGrid.RowCount is 0) DGrid.Rows.Add();

        if (_actionTag is "SAVE") TxtVno.GetCurrentVoucherNo("BOM", _numberSchema);
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
        }

        TxtRefVno.Clear();
        MskRefDate.Clear();
        _costCenterId = 0;
        TxtCostCenter.Clear();
        _fProductId = 0;
        _departmentId = 0;
        TxtDepartment.Clear();
        TxtRemarks.Clear();
        ClearProductDetails();
        LblTotalAltQty.Text = @"#0.00";
        LblTotalQty.Text = @"#0.00";
        LblTotalAmount.Text = @"#0.00";
        ObjGlobal.DGridColorCombo(DGrid);
    }

    private void ClearProductDetails()
    {
        _gridId = -1;
        _conQty = 0;
        _rProductId = 0;
        TxtProduct.Clear();
        TxtShortName.Clear();
        _godownId = 0;
        TxtGodown.Clear();
        TxtAltQty.Clear();
        _altUnitId = 0;
        TxtAltUnit.Clear();
        TxtQty.Clear();
        _unitId = 0;
        TxtUnit.Clear();
        TxtRate.Clear();
        TxtBasicAmount.Clear();
        _isGridUpdate = false;
        PanelProduct.Visible = false;
        GetSerialNo();
        if (TxtProduct.Enabled) AdjustControlsInDataGrid();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !tagStrings.Contains(_actionTag);
        TxtVno.Enabled = BtnVno.Enabled = isEnable || tagStrings.Contains(_actionTag);
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        TxtRefVno.Enabled = BtnRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtMasterCostCenter.Enabled = BtnCostCenter.Enabled = isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = ObjGlobal.StockDepartmentEnable && isEnable;
        TabProduct.Enabled = _isZoom || isEnable;
        LblTotalQty.Enabled = LblTotalAltQty.Enabled = LblTotalAmount.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.StockRemarksEnable || !tagStrings.Contains(_actionTag);
        BtnSave.Enabled = BtnCancel.Enabled =
            !string.IsNullOrEmpty(_actionTag) && _actionTag is "DELETE" || isEnable;
        EnableGridControl();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;
        TxtProduct.Enabled = TxtProduct.Visible = isEnable;
        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.StockGodownEnable;
        TxtCostCenter.Enabled = TxtCostCenter.Visible = isEnable;
        TxtAltQty.Enabled = false;
        TxtAltQty.Visible = isEnable;
        TxtAltUnit.Enabled = false;
        TxtAltUnit.Visible = isEnable;
        TxtQty.Enabled = TxtQty.Visible = isEnable;
        TxtUnit.Enabled = false;
        TxtUnit.Visible = isEnable;
        TxtRate.Enabled = TxtRate.Visible = isEnable;
        TxtBasicAmount.Enabled = isEnable && ObjGlobal.PurchaseBasicAmountEnable;
        TxtBasicAmount.Visible = isEnable;
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(TxtVno.Text.Trim()))
        {
            MessageBox.Show(@"BOM MEMO NUMBER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVno.Focus();
            return false;
        }

        if (!MskDate.MaskFull)
        {
            MessageBox.Show(@"BOM MEMO DATE IS INVALID..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskDate.Focus();
            return false;
        }

        if (!MskMiti.MaskFull)
        {
            MessageBox.Show(@"BOM MEMO MITI IS INVALID..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskMiti.Focus();
            return false;
        }
        if (DGrid.Rows.Count is 0)
        {
            MessageBox.Show(@"RAW MATERIALS CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(TxtRemarks.Text.Trim()) && ObjGlobal.StockRemarksMandatory)
        {
            MessageBox.Show(@"REMARKS ON THIS VOUCHER IS MANDATORY ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtRemarks.Focus();
            return false;
        }

        return true;
    }

    private void FillBomVoucherDetails(string voucherNo)
    {
        var dsBom = _entry.GetBomVoucherDetails(voucherNo);
        if (dsBom.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsBom.Tables[0].Rows)
            {
                if (_actionTag != "SAVE" && _actionTag != "COPY") TxtVno.Text = dr["VoucherNo"].ToString();
                if (_actionTag is "COPY") ReturnVoucherNo();
                MskMiti.Text = dr["VMiti"].ToString();
                MskDate.Text = Convert.ToDateTime(dr["VDate"].ToString()).ToString("d");
                TxtRefVno.Text = Convert.ToString(dr["OrderNo"].ToString());
                MskRefDate.Text = !string.IsNullOrWhiteSpace(TxtRefVno.Text) ? Convert.ToDateTime(dr["OrderDate"].ToString()).ToString("d") : "";
                TxtDepartment.Text = dr["DName"].ToString();
                _departmentId = ObjGlobal.ReturnInt(dr["DepartmentId"].ToString());
                _costCenterId = ObjGlobal.ReturnInt(dr["CCId"].ToString());
                LblInWords.Text = dr["InWords"].ToString();
                TxtRemarks.Text = dr["Remarks"].ToString();
            }

            if (dsBom.Tables[1].Rows.Count > 0)
            {
                var iRow = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsBom.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsBom.Tables[1].Rows)
                {
                    DGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["SerialNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodownId"].Value = dr["GodownId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenterId"].Value = dr["CCId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenter"].Value = dr["CCName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["OrderNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtOrderSNo"].Value = dr["OrderSNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["AltQty"].GetDecimalQtyString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["AltUnitId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString(true);
                    DGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["UnitId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                    DGrid.Rows[iRow].Cells["GTxtAmount"].Value = dr["Amount"].GetDecimalString();
                    DGrid.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                    DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                    iRow++;
                }
            }
        }

        DetailsTotalCalc();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
        TxtVno.Focus();
    }

    private void FillSalesOrderVoucher(string voucherNo)
    {
    }

    private void AddDataToGridDetails(bool isUpdate)
    {
        if (string.IsNullOrEmpty(TxtCostCenter.Text.Trim()))
        {
            MessageBox.Show(@"COST CENTER IS MANDATORY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCostCenter.Focus();
            return;
        }

        if (string.IsNullOrEmpty(TxtGodown.Text.Trim()) && ObjGlobal.StockGodownMandatory)
        {
            MessageBox.Show(@"GODOWN IS MANDATORY IS MANDATORY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtGodown.Focus();
            return;
        }

        if (ObjGlobal.ReturnDouble(TxtQty.Text) is 0)
        {
            MessageBox.Show(@"QUANTITY CAN'T BE ZERO..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtQty.Focus();
            return;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _rProductId.ToString();
        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtShortName.Text;
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId;
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtCostCenterId"].Value = _costCenterId.ToString();
        DGrid.Rows[iRows].Cells["GTxtCostCenter"].Value = TxtCostCenter.Text;
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = _getGridOrderNo;
        DGrid.Rows[iRows].Cells["GTxtOrderSNo"].Value = _getGridOrderSNo;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.Text;
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.Text;
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.Text;
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.Text;
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = string.Empty;
        _rowIndex = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[_rowIndex].Cells[_columnIndex];
        if (_isGridUpdate)
        {
            EnableGridControl();
            ClearProductDetails();
            DGrid.Focus();
            return;
        }

        _addToGrid = true;
        DGrid.ClearSelection();
        ObjGlobal.DGridColorCombo(DGrid);
        DetailsTotalCalc();
        ClearProductDetails();
        TxtProduct.Focus();
    }

    private void DetailsTotalCalc()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits()) return;
        LblTotalAltQty.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal()).GetDecimalString();
        LblTotalQty.Text = DGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtQty"].Value.GetDecimal())
            .GetDecimalString();
        LblTotalAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
        LblInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalAmount.Text);
    }

    private void GetProductInfo(long selectedId)
    {
        if (selectedId == 0) return;
        var dtProduct = _master.GetMasterProductList(_actionTag, selectedId);
        if (dtProduct.Rows.Count <= 0) return;
        _unitId = dtProduct.Rows[0]["PUnit"].GetInt();
        _altUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        _conQty = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        _conAltQty = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        TxtProduct.Text = dtProduct.Rows[0]["PName"].ToString();
        TxtShortName.Text = dtProduct.Rows[0]["PShortName"].ToString();
        TxtRate.Text = dtProduct.Rows[0]["PBuyRate"].GetDecimalString();
        if (_unitId > 0)
        {
            TxtUnit.Text = dtProduct.Rows[0]["UnitCode"].ToString();
            TxtQty.Text = 1.GetDecimalQtyString(true);
            TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
        }

        if (_altUnitId > 0)
        {
            TxtAltQty.Enabled = true;
            TxtAltUnit.Text = dtProduct.Rows[0]["AltUnitCode"].ToString();
        }

        if (_conQty <= 0) return;
        TxtQty.Text = _conQty.GetDecimalQtyString();
        TxtAltQty.Text = _conAltQty.GetDecimalQtyString();
    }

    private void GetTextFromGrid()
    {
        _gridId = _rowIndex;
        _rProductId = ObjGlobal.ReturnLong(DGrid.Rows[_gridId].Cells["GTxtProductId"].Value.ToString());
        TxtProduct.Text = DGrid.Rows[_gridId].Cells["GTxtProduct"].Value.ToString();
        GetProductInfo(_rProductId);
        TxtShortName.Text = DGrid.Rows[_gridId].Cells["GTxtShortName"].Value.ToString();
        _godownId = ObjGlobal.ReturnInt(DGrid.Rows[_gridId].Cells["GTxtGodownId"].Value.ToString());
        TxtGodown.Text = DGrid.Rows[_gridId].Cells["GTxtGodown"].Value.ToString();
        _costCenterId = ObjGlobal.ReturnInt(DGrid.Rows[_gridId].Cells["GTxtCostCenterId"].Value.ToString());
        TxtCostCenter.Text = DGrid.Rows[_gridId].Cells["GTxtCostCenter"].Value.ToString();
        TxtAltQty.Text = DGrid.Rows[_gridId].Cells["GTxtAltQty"].Value.ToString();
        TxtAltUnit.Text = DGrid.Rows[_gridId].Cells["GTxtAltUOM"].Value.ToString();
        _altUnitId = ObjGlobal.ReturnInt(DGrid.Rows[_gridId].Cells["GTxtAltUOMId"].Value.ToString());
        TxtQty.Text = DGrid.Rows[_gridId].Cells["GTxtQty"].Value.ToString();
        TxtUnit.Text = DGrid.Rows[_gridId].Cells["GTxtUOM"].Value.ToString();
        _unitId = ObjGlobal.ReturnInt(DGrid.Rows[_gridId].Cells["GTxtUOMId"].Value.ToString());
        TxtRate.Text = DGrid.Rows[_gridId].Cells["GTxtRate"].Value.ToString();
        TxtBasicAmount.Text = DGrid.Rows[_gridId].Cells["GTxtAmount"].Value.ToString();
        _isGridUpdate = true;
        TxtProduct.Focus();
    }

    private int SaveBillOfMaterial()
    {
        _entry.NumberSchema = _numberSchema;
        _entry.Module = "BOM";
        _entry.VmBomMaster.VoucherNo = TxtVno.Text;
        _entry.VmBomMaster.VDate = Convert.ToDateTime(MskDate.Text);
        _entry.VmBomMaster.VMiti = MskMiti.Text;
        _entry.VmBomMaster.FinishedGoodsId = _fProductId;
        //_entry.VmBomMaster.FinishedGoodsQty = ObjGlobal.ReturnDecimal(TxtFQty.Text);
        _entry.VmBomMaster.DepartmentId = _departmentId;
        _entry.VmBomMaster.CostCenterId = _costCenterId;
        _entry.VmBomMaster.Amount = ObjGlobal.ReturnDecimal(LblTotalAmount.Text);
        _entry.VmBomMaster.InWords = LblInWords.Text;
        _entry.VmBomMaster.Remarks = TxtRemarks.Text;
        _entry.VmBomMaster.GetView = DGrid;
        _entry.VmBomMaster.SyncRowVersion = _entry.VmBomMaster.SyncRowVersion.ReturnSyncRowNo("BOM", TxtVno.Text);
        if (DGrid.RowCount > 0 && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
        }

        return _entry.SaveBillOfMaterialsSetup(_actionTag);
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global Variable ---------------

    private int _costCenterId;
    private int _masterCostCenterId;
    private int _godownId;
    private int _altUnitId;
    private int _unitId;
    private int _fUnitId;
    private int _fAltUnitId;
    private int _departmentId;
    private int _columnIndex;
    private int _rowIndex;
    private int _gridId;
    private long _rProductId;
    private long _fProductId;

    private readonly bool _isZoom;
    private bool _addToGrid;
    private bool _isGridUpdate;

    private string _numberSchema = string.Empty;
    private string _actionTag = string.Empty;
    private readonly string[] tagStrings = ["DELETE", "REVERSE"];
    private readonly string _zoomVoucher;
    private readonly string _getGridOrderNo = string.Empty;
    private readonly string _getGridOrderSNo = string.Empty;

    private decimal _conQty;
    private decimal _conAltQty = 1;
    private readonly IStockEntryDesign _design;
    private readonly IMasterSetup _master;
    private readonly IStockEntry _entry;

    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtCostCenter { get; set; }
    private MrGridTextBox TxtAltUnit { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtBasicAmount { get; set; }

    #endregion --------------- Global Variable ---------------

    private void BtnSave_Click(object sender, EventArgs e)
    {

    }
}