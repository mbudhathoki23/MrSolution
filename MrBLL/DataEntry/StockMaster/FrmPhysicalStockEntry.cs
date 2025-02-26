using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.StockMaster;

public partial class FrmPhysicalStockEntry : MrForm
{
    // STOCK ADJUSTMENT

    #region --------------- STOCK ADJUSTMENT ---------------

    public FrmPhysicalStockEntry(bool zoomFrm, string txtZoomVno)
    {
        InitializeComponent();

        _objEntry = new ClsStockEntry();
        _getDesign = new GetStockDesign();
        _master = new ClsMasterSetup();

        _actionTag = string.Empty;

        _isZoom = zoomFrm;
        _txtZoomVno = txtZoomVno;

        GetGridColumn();
        ControlEnable();
        ControlClear();
    }

    private void FrmPhysicalStockEntry_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmPhysicalStockEntry_Shown(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            TxtVNo.Text = _txtZoomVno;
            FillStockAdjustmentVoucherData(TxtVNo.Text);
            BtnEdit.Focus();
        }
        else
        {
            BtnNew.Focus();
        }
    }

    private void FrmPhysicalStockEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 27)
        {
            if (TxtProduct.Enabled)
            {
                ClearDetails();
                EnableGridControl();
                DGrid.Focus();
            }
            else if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.No)
                {
                    return;
                }
                _actionTag = string.Empty;
                ControlEnable();
                ControlClear();
                BtnNew.Focus();
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    private void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ControlEnable(true);
        ControlClear();
        if (TxtVNo.Text.Trim() == string.Empty) TxtVNo.Focus();
        else MskMiti.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        if (_isZoom == false)
        {
            ControlClear();
            ControlEnable(true);
        }

        TxtVNo.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        if (_isZoom == false)
        {
            ControlEnable();
            ControlClear();
        }

        TxtVNo.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        var frmDp = new FrmDocumentPrint("Document Printing - Stock Adjustment", "SA", string.Empty, TxtVNo.Text,
            TxtVNo.Text, string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty, string.Empty)
        {
            Owner = this
        };
        frmDp.ShowDialog();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        if (_isZoom == false)
        {
            ControlEnable();
            TxtVNo.Enabled = true;
        }

        TxtVNo.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _actionTag = "COPY";
        BtnVno.PerformClick();
        if (TxtVNo.Text.IsValueExits())
            TxtVNo.Focus();
        else MskMiti.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (!IsValidForm())
        {
            return;
        }
        if (SaveStockAdjustment() != 0)
        {
            MessageBox.Show($@"{TxtVNo.Text}STOCK ADJUSTMENT VOUCHER [{_actionTag}] SUCCESSFULLY..!!",
                ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ControlClear();
            if (TxtVNo.Enabled)
                TxtVNo.Focus();
            else
                MskMiti.Focus();
        }
        else
        {
            MessageBox.Show($@"ERROR OCCURS WHILE VOUCHER NUMBER [{_actionTag}]..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            BtnSave.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ControlClear();
        if (TxtVNo.Enabled)
            TxtVNo.Focus();
        else if (MskMiti.Enabled) MskMiti.Focus();
    }

    private void TxtVNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) BtnVno_Click(sender, e);
    }

    private void TxtVNo_Validated(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(TxtVNo.Text.Trim()) && TxtVNo.Focused &&
            _actionTag is "SAVE")
        {
            var dtVno = _objEntry.CheckVoucherNo("AMS.STA_Master", "StockAdjust_No", TxtVNo.Text.Trim());
            if (dtVno == null || dtVno.Rows.Count <= 0) return;
            MessageBox.Show(@"STOCK ADJUSTMENT NUMBER ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVNo.Focus();
        }
    }

    private void TxtVNo_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtVNo.Text.Trim()) && TxtVNo.Focused &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"STOCK ADJUSTMENT NUMBER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVNo.Focus();
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MED", @"SA", ObjGlobal.Caption, _actionTag, "Normal", "TRANSACTION");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (!string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE")
                {
                    TxtVNo.Text = frmPickList.SelectedList[0]["VoucherNo"].ToString().Trim();
                    FillStockAdjustmentVoucherData(TxtVNo.Text);
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"STOCK ADJUSTMENT VOUCHER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVNo.Focus();
            return;
        }

        ObjGlobal.Caption = string.Empty;
        TxtVNo.Focus();
    }

    private void MskMiti_Enter(object sender, EventArgs e)
    {
        BeginInvoke((MethodInvoker)delegate
        {
            MskMiti.Select(0, 0);
        });
        SendKeys.SendWait("{HOME}");
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskMiti.MaskCompleted && MskMiti.Enabled)
        {
            MskDate.Text = MskDate.GetEnglishDate(MskMiti.Text);
            var isValid = ObjGlobal.ValidDate(MskMiti.Text, "M");
            if (!isValid)
            {
                MskMiti.WarningMessage(@"THE ENTER MITI IS NOT VALID..!!");
                return;
            }
            var isRange = MskMiti.IsValidDateRange("M");
            if (!isRange)
            {
                MskMiti.WarningMessage($@"MITI MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate} ");
                return;
            }
        }
        else if (!MskMiti.MaskCompleted && MskMiti.Enabled && MskMiti.ValidControl(ActiveControl))
        {
            MskMiti.WarningMessage(@"THE ENTER VALID VOUCHER MITI..!!");
            return;
        }
    }

    private void MskDate_Validated(object sender, EventArgs e)
    {
        if (MskDate.MaskCompleted && MskDate.Enabled)
        {
            MskMiti.Text = MskMiti.GetNepaliDate(MskDate.Text);
            var isValid = ObjGlobal.ValidDate(MskDate.Text, "D");
            if (!isValid)
            {
                MskDate.WarningMessage(@"THE ENTER MITI IS NOT VALID..!!");
                return;
            }
            var isRange = MskDate.IsValidDateRange("D");
            if (!isRange)
            {
                MskDate.WarningMessage($@"MITI MUST BE BETWEEN {ObjGlobal.CfStartAdDate.GetDateString()} AND {ObjGlobal.CfEndAdDate.GetDateString()} ");
                return;
            }
        }
        else if (!MskDate.MaskCompleted && MskDate.Enabled && MskDate.ValidControl(ActiveControl))
        {
            MskDate.WarningMessage(@"THE ENTER VALID VOUCHER DATE..!!");
            return;
        }
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var result = GetMasterList.CreateDepartment(true);
            if (result.id > 0)
            {
                TxtDepartment.Text = result.description;
                _departmentId = result.id;
            }
            TxtDepartment.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, BtnDepartment);
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDepartmentList(_actionTag);
        if (id > 0)
        {
            TxtDepartment.Text = description;
            _departmentId = id;
        }
        TxtDepartment.Focus();
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.Enter)
        {
            if (TxtDepartment.Enabled)
            {
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
            else
            {
                DGrid.Focus();
            }
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }
            var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
            var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
            DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
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
            if (DGrid.CurrentRow != null)
            {
                DGrid.CurrentCell = DGrid.CurrentRow.Cells[0];
            }
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                SetDataToText();
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

    #endregion --------------- STOCK ADJUSTMENT ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void GetTransactionType()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("In", "I"),
            new("Out", "O")
        };
        CmbType.DataSource = list;
        CmbType.DisplayMember = "Item1";
        CmbType.ValueMember = "Item2";
    }

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
        _getDesign.GetPhysicalStockEntry(DGrid);
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        if (DGrid.Columns["GTxtGodown"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        }
        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;
        }
        DGrid.GotFocus += (_, _) =>
        {
            DGrid.Rows[_rowIndex].Cells[0].Selected = true;
        };
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
                var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                if (id > 0)
                {
                    TxtProduct.Text = description;
                    _productId = id;
                    SetProductInfo(_productId);
                }
                TxtProduct.Focus();
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
                TxtProduct.WarningMessage("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                return;
            }

            if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    TxtProduct.WarningMessage("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                    return;
                }
            }
            if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                EnableGridControl();
                DGrid.ClearSelection();
                if (TxtRemarks.Enabled)
                {
                    TxtRemarks.Focus();
                }
                else
                {
                    BtnSave.Focus();
                }
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
                (TxtGodown.Text, _godownId) = GetMasterList.CreateGodown(true);
                TxtGodown.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtGodown, OpenGodownList);
            }
        };
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
        AdjustControlsInDataGrid();
    }

    private void OpenProductList()
    {
        var (description, id) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        if (id > 0)
        {
            TxtProduct.Text = description;
            _productId = id;
        }
        SetProductInfo(_productId);
        TxtProduct.Focus();
    }

    private void OpenGodownList()
    {
        (TxtGodown.Text, _godownId) = GetMasterList.GetGodown(_actionTag);
        TxtGodown.Focus();
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

    private bool IsValidForm()
    {
        if (_actionTag != "SAVE")
            if (MessageBox.Show($@"ARE YOU SURE WANT TO {_actionTag.ToUpper()} ..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

        if (_actionTag is "SAVE" && ObjGlobal.SysConfirmSave)
            if (MessageBox.Show($@"ARE YOU SURE WANT TO {_actionTag.ToUpper()} ..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

        if (string.IsNullOrEmpty(TxtVNo.Text))
        {
            MessageBox.Show(@"PLZ. ENTER STOCK ADJUSTMENT VOUCHER NO..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVNo.Focus();
            return false;
        }

        if (DGrid.RowCount <= 0)
        {
            MessageBox.Show(@"PLZ. ENTER ITEMS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return false;
        }

        if (ObjGlobal.StockDepartmentMandatory && string.IsNullOrEmpty(TxtDepartment.Text.Trim()))
        {
            MessageBox.Show(@"DEPARTMENT IS MANDATORY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDepartment.Focus();
            return false;
        }

        if (ObjGlobal.StockRemarksMandatory && string.IsNullOrEmpty(TxtRemarks.Text.Trim()))
        {
            MessageBox.Show(@"REMARKS IS MANDATORY !", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtRemarks.Focus();
            return false;
        }

        if ((_actionTag.Equals("REVERSE") || _actionTag.Equals("DELETE")) &&
            string.IsNullOrEmpty(TxtRemarks.Text.Trim()) && TxtRemarks.Enabled)
        {
            MessageBox.Show($@"PLEASE ENTER REMARKS FOR [{_actionTag}] !", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtRemarks.Focus();
            return false;
        }

        return true;
    }

    private string ReturnVoucherNo()
    {
        var dtCheck = ClsMasterSetup.GetDocumentNumberingSchema("SA");
        if (dtCheck.Rows.Count == 1)
        {
            _numberSchema = dtCheck.Rows[0]["DocDesc"].ToString();
            TxtVNo.GetCurrentVoucherNo("SA", _numberSchema);
        }
        else if (dtCheck.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme
            {
                Source = "SA",
                TblName = "AMS.STA_Master",
                FldNAme = "StockAdjust_No"
            };
            if (wnd.ShowDialog() != DialogResult.OK) return TxtVNo.Text;
            if (string.IsNullOrEmpty(wnd.VNo)) return TxtVNo.Text;
            _numberSchema = wnd.Description;
            TxtVNo.Text = wnd.VNo;
        }

        return TxtVNo.Text;
    }

    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_tagStrings.Contains(_actionTag);
        TxtVNo.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        TxtRefVno.Enabled = BtnRefVno.Enabled = isEnable;
        BtnVno.Enabled = TxtVNo.Enabled;
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = ObjGlobal.StockDepartmentEnable && isEnable;
        DGrid.Enabled = isEnable;
        TabProduct.Enabled = isEnable;
        DGrid.ReadOnly = true;
        TxtRemarks.Enabled = isEnable && ObjGlobal.StockRemarksEnable || _tagStrings.Contains(_actionTag);
        BtnSave.Enabled = BtnCancel.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        EnableGridControl();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;
        TxtProduct.Enabled = TxtProduct.Visible = isEnable;
        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.StockGodownEnable;
        TxtQty.Enabled = TxtQty.Visible = isEnable;
        TxtUnit.Enabled = false;
        TxtUnit.Visible = isEnable;
        TxtRate.Enabled = TxtRate.Visible = isEnable;
        TxtBasicAmount.Enabled = isEnable && ObjGlobal.PurchaseBasicAmountEnable;
        TxtBasicAmount.Visible = isEnable;
    }

    private void ControlClear()
    {
        Text = _actionTag.IsValueExits()
            ? $@"PHYSICAL STOCK ENTRY [{_actionTag}]"
            : @"PHYSICAL STOCK ENTRY";
        TxtVNo.Text = _actionTag.Equals("SAVE") ? TxtVNo.GetCurrentVoucherNo("SA", _numberSchema) : string.Empty;
        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.GetNepaliDate(MskDate.Text);
        }
        TxtRefVno.Clear();
        _departmentId = 0;
        TxtDepartment.Clear();
        LblTotalQty.Text = string.Empty;
        LblNumInWords.Text = string.Empty;
        LblTotalAmount.Text = string.Empty;
        TxtRemarks.Clear();
        DGrid.Rows.Clear();
        ClearDetails();
        DGrid.ClearSelection();
    }

    private void ClearDetails()
    {
        _isGridUpdate = false;
        _productId = 0;
        TxtProduct.Text = string.Empty;
        PanelProduct.Visible = true;
        _godownId = 0;
        TxtGodown.Clear();

        TxtQty.Clear();

        _unitId = 0;
        TxtUnit.Clear();

        TxtRate.Clear();
        TxtBasicAmount.Clear();
        TxtShortName.Clear();
        lblStockQty.Text = string.Empty;
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
        }
        AdjustControlsInDataGrid();
    }

    private void SetProductInfo(long getProductId)
    {
        if (getProductId == 0)
        {
            return;
        }
        var dtProduct = _master.GetMasterProductList(_actionTag, getProductId);
        if (dtProduct.Rows.Count <= 0)
        {
            return;
        }
        _unitId = dtProduct.Rows[0]["PUnit"].GetInt();
        _altUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        TxtProduct.Text = dtProduct.Rows[0]["PName"].ToString();
        TxtShortName.Text = dtProduct.Rows[0]["PShortName"].ToString();
        TxtRate.Text = dtProduct.Rows[0]["PBuyRate"].ToString();
        if (_unitId > 0)
        {
            TxtUnit.Text = dtProduct.Rows[0]["UnitCode"].ToString();
            TxtQty.Text = 1.ToString(ObjGlobal.SysAmountFormat);
        }

        if (_altUnitId > 0)
        {
            TxtAltQty.Enabled = TxtAltUnit.Enabled = true;
            TxtAltUnit.Text = dtProduct.Rows[0]["AltUnitCode"].ToString();
        }

        _conAltQty = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        TxtQty.Text = _conQty.ToString(ObjGlobal.SysAmountFormat);
        PanelProduct.Visible = true;
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits())
        {
            return;
        }

        var viewRows = DGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

        LblTotalAltQty.Text = gridViewRows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal()).GetDecimalQtyString();
        LblTotalQty.Text = gridViewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalQtyString();
        LblTotalAmount.Text = gridViewRows.Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
        LblNumInWords.GetNumberInWords(LblTotalAmount.Text);
    }

    private void AddDataToGridDetails(bool isGridUpdate)
    {
        if (string.IsNullOrEmpty(TxtProduct.Text) || _productId is 0)
        {
            MessageBox.Show(@"PRODUCT NAME CAN'T BE BLANK..!!", ObjGlobal.Caption);
            TxtProduct.Focus();
            return;
        }

        if (string.IsNullOrEmpty(TxtQty.Text))
        {
            MessageBox.Show(@"PRODUCT QTY CAN'T BE BLANK..!!", ObjGlobal.Caption);
            TxtQty.Focus();
            return;
        }

        var iRows = _rowIndex;
        if (!_isGridUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId.ToString();
        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtShortName.Text;
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        var stock = lblStockQty.GetDecimal();
        var adjStock = TxtQty.GetDecimal();
        var result = stock - adjStock;

        DGrid.Rows[iRows].Cells["GTxtType"].Value = result > 0 ? "I" : "O";

        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;

        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtActualQty"].Value = Math.Abs(result);
        DGrid.Rows[iRows].Cells["GTxtActualAmount"].Value = Math.Abs(result) * TxtRate.GetDecimal();

        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = _additionalDescription;
        _rowIndex = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[_rowIndex].Cells[_columnIndex];
        if (_isGridUpdate)
        {
            EnableGridControl();
            ClearDetails();
            DGrid.Focus();
            return;
        }

        ObjGlobal.DGridColorCombo(DGrid);
        VoucherTotalCalculation();
        ClearDetails();
        GetSerialNo();
        TxtProduct.Focus();
    }

    private void SetDataToText()
    {
        if (!DGrid.Rows[_rowIndex].Selected) return;
        if (DGrid.CurrentRow != null)
        {
            _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
            _unitId = DGrid.Rows[_rowIndex].Cells["GTxtUOMId"].Value.GetInt();
            _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();
            _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
            SetProductInfo(_productId);
            TxtShortName.Text = DGrid.Rows[_rowIndex].Cells["GTxtShortName"].Value.ToString();
            TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.ToString();
            TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.ToString();
            CmbType.SelectedIndex = DGrid.Rows[_rowIndex].Cells["GTxtType"].Value.ToString() == "In" ? 0 : 1;
            TxtQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtQty"].Value.ToString();
            TxtAltUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltUOM"].Value.ToString();
            TxtUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtUOM"].Value.ToString();
            TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.ToString();
            TxtBasicAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.ToString();
            _additionalDescription = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value?.ToString();
        }

        _isGridUpdate = true;
        TxtProduct.Focus();
    }

    private int SaveStockAdjustment()
    {
        if (_actionTag is "SAVE") TxtVNo.Text = TxtVNo.GetCurrentVoucherNo("SA", _numberSchema);

        _objEntry.VmStaMaster.StockAdjust_No = TxtVNo.Text;
        _objEntry.VmStaMaster.VDate = DateTime.Parse(MskDate.Text);
        _objEntry.VmStaMaster.VMiti = MskMiti.Text;
        _objEntry.VmStaMaster.Vtime = DateTime.Now;
        _objEntry.VmStaMaster.DepartmentId = _departmentId;
        _objEntry.VmStaMaster.BarCode = ObjGlobal.SysBranchId.ToString();
        _objEntry.VmStaMaster.PhyStockNo = TxtRefVno.Text;
        _objEntry.VmStaMaster.Remarks = TxtRemarks.Text;
        if (DGrid.RowCount > 0 && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
        }
        _objEntry.VmStaMaster.GetView = DGrid;
        return _objEntry.SaveStockAdjustment(_actionTag);
    }

    private void FillStockAdjustmentVoucherData(string voucherNo)
    {
        var dsStock = _objEntry.GetStockAdjustmentVoucherDetails(voucherNo);
        if (dsStock.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow dr in dsStock.Tables[0].Rows)
        {
            if (_actionTag != "SAVE") TxtVNo.Text = dr["StockAdjust_No"].ToString();

            MskMiti.Text = dr["VMiti"].ToString();
            MskDate.Text = Convert.ToDateTime(dr["VDate"].ToString()).ToString("dd/MM/yyyy");
            TxtRefVno.Text = dr["PhyStockNo"].IsValueExits()
                ? Convert.ToString(dr["PhyStockNo"].ToString())
                : string.Empty;
            TxtDepartment.Text = dr["DepartmentId"].IsValueExits() ? dr["DName"].ToString() : string.Empty;
            _departmentId = ObjGlobal.ReturnInt(dr["DepartmentId"].ToString());
            TxtRemarks.Text = dr["Remarks"].IsValueExits()
                ? Convert.ToString(dr["Remarks"].ToString())
                : string.Empty;
        }

        if (dsStock.Tables[1].Rows.Count <= 0) return;
        {
            DGrid.Rows.Clear();
            DGrid.Rows.Add(dsStock.Tables[1].Rows.Count + 1);
            var rows = 0;
            foreach (DataRow dr in dsStock.Tables[1].Rows)
            {
                DGrid.Rows[rows].Cells["GTxtSNo"].Value = dr["SNo"].ToString();
                DGrid.Rows[rows].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                DGrid.Rows[rows].Cells["GTxtShortName"].Value = dr["PName"].ToString();
                DGrid.Rows[rows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                DGrid.Rows[rows].Cells["GTxtGodownId"].Value = dr["GodownId"].ToString();
                DGrid.Rows[rows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                DGrid.Rows[rows].Cells["GTxtType"].Value = dr["AdjType"].ToString().Trim() == "I" ? "In" : "Out";
                DGrid.Rows[rows].Cells["GTxtAltQty"].Value = dr["AltQty"].GetDecimalQtyString();
                DGrid.Rows[rows].Cells["GTxtAltUOMId"].Value = dr["AltUnitId"].ToString();
                DGrid.Rows[rows].Cells["GTxtAltUOM"].Value = dr["AltUnit"].ToString();
                DGrid.Rows[rows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString(true);
                DGrid.Rows[rows].Cells["GTxtUOMId"].Value = dr["UnitId"].ToString();
                DGrid.Rows[rows].Cells["GTxtUOM"].Value = dr["Unit"].ToString();
                DGrid.Rows[rows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtAmount"].Value = dr["NetAmount"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtNarration"].Value = dr["AddDesc"].ToString();
                rows++;
            }

            DGrid.CurrentCell = DGrid.Rows[rows - 1].Cells[_columnIndex];
        }
        VoucherTotalCalculation();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- GLOBAL VARIABLE ---------------

    private long _productId;
    private int _columnIndex;
    private int _rowIndex;
    private int _godownId;
    private int _altUnitId;
    private int _unitId;
    private int _departmentId;

    public bool RowsDelete;
    private readonly bool _isZoom;
    private bool _isGridUpdate;

    private readonly string _txtZoomVno;
    private string _actionTag;
    private string _numberSchema;
    private string _additionalDescription;

    private readonly string[] _tagStrings =
    [
        "DELETE", "REVERSE"
    ];

    private decimal _conQty;
    private decimal _conAltQty = 1;

    private readonly IStockEntry _objEntry;
    private readonly IMasterSetup _master;
    private readonly IStockEntryDesign _getDesign;
    private MrGridComboBox CmbType { get; set; }
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtAltUnit { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtBasicAmount { get; set; }

    #endregion --------------- GLOBAL VARIABLE ---------------
}