using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.ProductionSystem.BillOfMaterials;
using MrDAL.DataEntry.ProductionSystem.BillOfMaterials;
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
using System.Text;
using System.Windows.Forms;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmBillOfMaterials : MrForm
{
    // BILL OF MATERIALS

    #region ---------------  FrmBillOfMaterials ---------------

    public FrmBillOfMaterials(bool isZoom, string voucherNo)
    {
        InitializeComponent();
        _design = new GetStockDesign();
        _master = new ClsMasterSetup();
        _billOfMaterialsRepository = new BillOfMaterialsRepository();
        _isZoom = isZoom;
        _zoomVoucher = voucherNo;
        GetGridColumn();
        ClearControl();
        EnableControl();
    }

    private void FrmBillOfMaterials_Load(object sender, EventArgs e)
    {
        if (_isZoom)
        {
            FillBomVoucherDetails(_zoomVoucher);
            BtnEdit.Focus();
        }
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], Tag);
    }

    private void FrmBillOfMaterials_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
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
                TxtMasterCostCenter.Clear();
                EnableControl();
                ClearControl();
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
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableControl(true);
        ClearControl();
        ReturnVoucherNo();
        if (string.IsNullOrEmpty(TxtVno.Text))
        {
            TxtVno.Focus();
        }
        else
        {
            MskMiti.Focus();
        }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        EnableControl(true);
        ClearControl();
        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        EnableControl();
        ClearControl();
        TxtVno.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        var print = new FrmDocumentPrint("Crystal", "IBOM", string.Empty, TxtVno.Text, TxtVno.Text, string.Empty,
            string.Empty, ObjGlobal.SysDefaultInvoicePrinter, ObjGlobal.SysDefaultInvoiceDesign, string.Empty)
        {
            Owner = ActiveForm
        };
        print.ShowDialog();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        EnableControl();
        ClearControl();
        TxtVno.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableControl(true);
        ClearControl();
        ReturnVoucherNo();
        BtnVno_Click(sender, e);
        if (string.IsNullOrEmpty(TxtVno.Text))
            TxtVno.Focus();
        else
            MskMiti.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes) Close();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (!IsValidForm()) return;
        BtnSave.Enabled = false;
        if (SaveBillOfMaterial() > 0)
        {
            MessageBox.Show($@"{TxtVno.Text} BOM VOUCHER NUMBER {_actionTag} SUCCESSFULLY ..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearControl();
            BtnSave.Enabled = true;
            if (TxtVno.Enabled) TxtVno.Focus();
            else MskMiti.Focus();
        }
        else
        {
            MessageBox.Show($@"ERROR OCCURS WHILE DATA {_actionTag} ..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnSave.Enabled = true;
            if (TxtVno.Enabled) TxtVno.Focus();
            else MskMiti.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void BtnRemarks_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetNarrationRemarks(_actionTag);
        if (result.IsValueExits())
        {
            TxtRemarks.Text = result;
        }
        TxtRemarks.Focus();
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnRemarks_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var result = GetMasterList.CreateNarration(true);
            if (result.IsValueExits())
            {
                TxtRemarks.Text = result.description;
            }
            TxtRemarks.Focus();
        }
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag is "SAVE" && TxtVno.IsValueExits())
        {
            var dtCheck = _billOfMaterialsRepository.CheckVoucherNo("INV.BOM_Master", "VoucherNo", TxtVno.Text);
            if (dtCheck.Rows.Count > 0)
            {
                TxtVno.NotifyWarning(@"BOM MEMO NUMBER ALREADY EXITS..!!");
                TxtVno.Focus();
            }
        }

        if (_actionTag.IsValueExits() && ActiveControl != null && ActiveControl.Name != "TxtVno" && TxtVno.IsBlankOrEmpty())
        {
            TxtVno.WarningMessage("VOUCHER NUMBER IS REQUIRED");
        }
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtVno.IsBlankOrEmpty())
            {
                TxtVno.WarningMessage("VOUCHER NUMBER IS REQUIRED..!!");
            }
        }
        else if (TxtVno.ReadOnly || !_actionTag.Equals("SAVE"))
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, "BOM", MskDate.Text);
        if (result.Length > 0)
        {
            TxtVno.Text = result;
            if (!_actionTag.Equals("SAVE"))
            {
                FillBomVoucherDetails(result);
            }
        }
        TxtVno.Focus();
    }

    private void MskMiti_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
        var messageBoxCS = new StringBuilder();
        messageBoxCS.AppendFormat("{0} = {1}", "Position", e.Position);
        messageBoxCS.AppendLine();
        messageBoxCS.AppendFormat("{0} = {1}", "RejectionHint", e.RejectionHint);
        messageBoxCS.AppendLine();
    }

    private void MskMiti_Leave(object sender, EventArgs e)
    {
        if (!MskMiti.MaskFull)
        {
            MskMiti.BeepOnError = true;
            MessageBox.Show(@"BOM MITI IS INVALID USER THE SYSTEM FORMAT..!!", ObjGlobal.Caption);
            MskMiti.Focus();
        }
    }

    private void MskMiti_Enter(object sender, EventArgs e)
    {
        BeginInvoke((MethodInvoker)delegate
        {
            MskMiti.SelectionStart = 0;
            MskMiti.Select(0, 0);
        });
        SendKeys.SendWait("{HOME}");
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskMiti.MaskFull) MskDate.Text = ObjGlobal.ReturnEnglishDate(MskMiti.Text);
    }

    private void MskDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
    }

    private void MskDate_Enter(object sender, EventArgs e)
    {
        BeginInvoke((MethodInvoker)delegate
        {
            MskDate.SelectionStart = 0;
            MskDate.Select(0, 0);
        });
        SendKeys.SendWait("{HOME}");
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskDate.MaskFull) MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnRefVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtRefVno.IsValueExits())
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            else
                TxtFinisehdGoods.Focus();
        }
    }

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter) GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void BtnRefVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, "SO", MskDate.Text);
        if (result.IsValueExits())
        {
            TxtRefVno.Text = result;
            FillSalesOrderVoucher(result);
        }
        BtnVno.Focus();
    }

    private void TxtFinishedGoods_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && ActiveControl.Name != "TxtFinisehdGoods" && TxtFinisehdGoods.IsBlankOrEmpty())
        {
            TxtFinisehdGoods.WarningMessage("FINISHED GOODS IS REQUIRED");
        }
    }

    private void TxtFinishedGoods_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnFinishedGoods_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtFinisehdGoods.IsBlankOrEmpty())
            {
                TxtFinisehdGoods.WarningMessage("FINISHED GOODS IS REQUIRED");
                return;
            }
            GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (description.IsValueExits())
            {
                TxtFinisehdGoods.Text = description;
                _fProductId = id;
            }
            TxtFinisehdGoods.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtFinisehdGoods, BtnFinishedGoods);
        }
    }

    private void BtnFinishedGoods_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProduct(_actionTag, MskDate.Text, "F");
        if (description.IsValueExits())
        {
            TxtFinisehdGoods.Text = description;
            _fProductId = id;
            TxtFQty.Text = 1.GetDecimalString();
            GetFinishedGoodsInfo();
        }
    }

    private void TxtFAltQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            GlobalKeyPress(sender, e);
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtFQty_Validating(object sender, CancelEventArgs e)
    {
        TxtFQty.Text = TxtFQty.GetDecimalQtyString();
        if (TxtRate.GetDecimal() <= 0)
        {
            return;
        }
        TxtRate.Text = TxtRate.GetDecimalString();
        TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
    }

    private void TxtFQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtDepartment.Enabled)
            {
                GlobalKeyPress(sender, e);
            }
            else
            {
                TxtMasterCostCenter.Focus();
            }
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.KeyData is Keys.Enter)
        {
            GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateDepartment(true);
            if (description.IsValueExits())
            {
                TxtDepartment.Text = description;
                _departmentId = id;
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
        if (description.IsValueExits())
        {
            TxtDepartment.Text = description;
            _departmentId = id;
        }
        TxtDepartment.Focus();
    }

    private void BtnCostCenter_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetCostCenterList(_actionTag);
        if (description.IsValueExits())
        {
            TxtMasterCostCenter.Text = description;
        }
        TxtMasterCostCenter.Focus();
    }

    private void TxtMasterCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCostCenter_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtMasterCostCenter.IsValueExits())
            {
                DGrid.Focus();
            }
            else
            {
                this.NotifyValidationError(TxtMasterCostCenter, "PLEASE SELECT COST CENTER FOR THE BOM");
            }
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateCostCenter(true);
            if (description.IsValueExits())
            {
                TxtMasterCostCenter.Text = description;
                _masterCostCenterId = id;
            }
            TxtMasterCostCenter.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtMasterCostCenter, BtnCostCenter);
        }
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }

            if (DGrid.CurrentRow != null)
            {
                var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
                var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
                if (DGrid.RowCount is 0)
                {
                    DGrid.Rows.Add();
                }
                GetSerialNo();
            }
        }

        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            DGrid.CurrentCell = DGrid.CurrentRow?.Cells[0];
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

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void OnTxtBasicAmountOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
    }

    private void OnTxtRateOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtRate.Focused) return;
        TxtBasicAmount.Text = TxtRate.GetDecimal() > 0
            ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
            : 0.GetDecimalString();
    }

    private void OnTxtRateOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab) SendToBack();
    }

    private void OnTxtQtyOnValidated(object sender, EventArgs e)
    {
        TxtQty.Text = TxtQty.GetDecimalQtyString();
        if (!TxtQty.Enabled || !TxtProduct.Enabled) return;
        if (TxtQty.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
        }
    }

    private void OnTxtAltQtyOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtAltQty.Focused) return;
        if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
            TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
        else if (TxtAltQty.GetDecimal() is 0) TxtQty.Text = 1.GetDecimalQtyString();
    }

    private void OnTxtGodownOnKeyDown(object _, KeyEventArgs e)
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
    }

    private void OnTxtProductOnKeyDown(object _, KeyEventArgs e)
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
    }

    #endregion ---------------  FrmBillOfMaterials ---------------

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

    private string ReturnVoucherNo()
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
            if (wnd.ShowDialog() != DialogResult.OK) return TxtVno.Text;

            if (string.IsNullOrEmpty(wnd.VNo)) return TxtVno.Text;

            _numberSchema = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }

        return TxtVno.Text;
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
        TxtProduct.KeyDown += OnTxtProductOnKeyDown;
        // TxtProduct.Validating += OnTxtProductOnValidating;

        TxtCostCenter = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtCostCenter.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenCostCenterList();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var result = GetMasterList.CreateCostCenter(true);
                if (result.id > 0)
                {
                    TxtCostCenter.Text = result.description;
                    _costCenterId = result.id;
                }
                TxtCostCenter.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtCostCenter, OpenCostCenterList);
            }
        };
        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.KeyDown += OnTxtGodownOnKeyDown;
        TxtAltQty = new MrGridNumericTextBox(DGrid);
        //TxtAltQty.Validating += OnTxtAltQtyOnValidating;
        TxtAltQty.TextChanged += OnTxtAltQtyOnTextChanged;
        TxtAltUnit = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += OnTxtQtyOnValidated;
        TxtQty.TextChanged += (sender, e) =>
        {
            if (!TxtQty.Focused) return;
            TxtBasicAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        };
        TxtUnit = new MrGridTextBox(DGrid);
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += OnTxtRateOnKeyDown;
        TxtRate.TextChanged += OnTxtRateOnTextChanged;
        //  TxtRate.Validating += OnTxtRateOnValidating;
        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += OnTxtBasicAmountOnKeyDown;
        //TxtBasicAmount.Validating += OnTxtBasicAmountOnValidating;
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
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
        _masterCostCenterId = 0;
        _fProductId = 0;
        TxtFinisehdGoods.Clear();
        TxtFQty.Clear();
        TxtFUOM.Clear();
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
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_tagStrings.Contains(_actionTag);
        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        TxtRefVno.Enabled = BtnRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtFinisehdGoods.Enabled = BtnFinishedGoods.Enabled = TxtFQty.Enabled = isEnable;
        TxtFUOM.Enabled = false;
        TxtMasterCostCenter.Enabled = BtnCostCenter.Enabled = isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = ObjGlobal.StockDepartmentEnable && isEnable;
        TabProduct.Enabled = _isZoom || isEnable;
        LblTotalQty.Enabled = LblTotalAltQty.Enabled = LblTotalAmount.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.StockRemarksEnable || !_tagStrings.Contains(_actionTag);
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
        TxtBasicAmount.Enabled = isEnable;
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

        if (string.IsNullOrWhiteSpace(TxtFinisehdGoods.Text.Trim()))
        {
            MessageBox.Show(@"FINISHED GOODS IS BLANK PLEASE CHOOSE IT..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtFinisehdGoods.Focus();
            return false;
        }

        if (ObjGlobal.ReturnDouble(TxtFQty.Text.Trim()) is 0)
        {
            MessageBox.Show(@"FINISHED GOODS QUANTITY CAN'T BE ZERO..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtFinisehdGoods.Focus();
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
        var dsBom = _billOfMaterialsRepository.GetBomVoucherDetails(voucherNo);
        if (dsBom.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsBom.Tables[0].Rows)
            {
                if (_actionTag != "SAVE" && _actionTag != "COPY") TxtVno.Text = dr["VoucherNo"].ToString();
                if (_actionTag is "COPY") ReturnVoucherNo();
                MskMiti.Text = dr["VMiti"].ToString();
                MskDate.Text = Convert.ToDateTime(dr["VDate"].ToString()).ToString("d");
                TxtRefVno.Text = Convert.ToString(dr["OrderNo"].ToString());
                MskRefDate.Text = !string.IsNullOrWhiteSpace(TxtRefVno.Text)
                    ? Convert.ToDateTime(dr["OrderDate"].ToString()).ToString("d")
                    : "";
                TxtFinisehdGoods.Text = dr["PName"].ToString();
                _fProductId = dr["FinishedGoodsId"].GetLong();
                GetFinishedGoodsInfo();
                TxtFQty.Text = dr["FinishedGoodsQty"].GetDecimalString();
                TxtDepartment.Text = dr["DName"].ToString();
                _departmentId = dr["DepartmentId"].GetInt();
                _masterCostCenterId = dr["CostCenterId"].GetInt();
                TxtMasterCostCenter.Text = dr["CCName"].GetString();
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
                    DGrid.Rows[iRow].Cells["GTxtCostCenterId"].Value = dr["CostCenterId"].ToString();
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

    private void GetFinishedGoodsInfo()
    {
        var dtProduct = _master.GetMasterProductList(_actionTag, _fProductId);
        if (dtProduct.Rows.Count <= 0) return;
        dtProduct.Rows[0]["PUnit"].GetInt();
        _fAltUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        TxtFUOM.Text = dtProduct.Rows[0]["UnitCode"].GetString();
        TxtFAltUOM.Text = dtProduct.Rows[0]["AltUnitCode"].GetString();
        if (_fAltUnitId > 0)
        {
            TxtFAltQty.Enabled = true;
        }
    }

    private void GetTextFromGrid()
    {
        _gridId = _rowIndex;
        _rProductId = DGrid.Rows[_gridId].Cells["GTxtProductId"].Value.GetLong();
        TxtProduct.Text = DGrid.Rows[_gridId].Cells["GTxtProduct"].Value.ToString();
        GetProductInfo(_rProductId);
        TxtShortName.Text = DGrid.Rows[_gridId].Cells["GTxtShortName"].Value.ToString();
        _godownId = DGrid.Rows[_gridId].Cells["GTxtGodownId"].Value.GetInt();
        TxtGodown.Text = DGrid.Rows[_gridId].Cells["GTxtGodown"].Value.ToString();
        _costCenterId = DGrid.Rows[_gridId].Cells["GTxtCostCenterId"].Value.GetInt();
        TxtCostCenter.Text = DGrid.Rows[_gridId].Cells["GTxtCostCenter"].Value.ToString();
        TxtAltQty.Text = DGrid.Rows[_gridId].Cells["GTxtAltQty"].Value.ToString();
        TxtAltUnit.Text = DGrid.Rows[_gridId].Cells["GTxtAltUOM"].Value.ToString();
        _altUnitId = DGrid.Rows[_gridId].Cells["GTxtAltUOMId"].Value.GetInt();
        TxtQty.Text = DGrid.Rows[_gridId].Cells["GTxtQty"].Value.ToString();
        TxtUnit.Text = DGrid.Rows[_gridId].Cells["GTxtUOM"].Value.ToString();
        _unitId = DGrid.Rows[_gridId].Cells["GTxtUOMId"].Value.GetInt();
        TxtRate.Text = DGrid.Rows[_gridId].Cells["GTxtRate"].Value.ToString();
        TxtBasicAmount.Text = DGrid.Rows[_gridId].Cells["GTxtAmount"].Value.ToString();
        _isGridUpdate = true;
        TxtProduct.Focus();
    }

    private int SaveBillOfMaterial()
    {
        const int syncRow = 0;

        _billOfMaterialsRepository.NumberSchema = _numberSchema;
        _billOfMaterialsRepository.Module = "BOM";
        _billOfMaterialsRepository.VmBomMaster.VoucherNo = TxtVno.Text;
        _billOfMaterialsRepository.VmBomMaster.VDate = Convert.ToDateTime(MskDate.Text);
        _billOfMaterialsRepository.VmBomMaster.VMiti = MskMiti.Text;
        _billOfMaterialsRepository.VmBomMaster.FinishedGoodsId = _fProductId;
        _billOfMaterialsRepository.VmBomMaster.FinishedGoodsQty = ObjGlobal.ReturnDecimal(TxtFQty.Text);
        _billOfMaterialsRepository.VmBomMaster.DepartmentId = _departmentId;
        _billOfMaterialsRepository.VmBomMaster.CostCenterId = _masterCostCenterId;
        _billOfMaterialsRepository.VmBomMaster.Amount = ObjGlobal.ReturnDecimal(LblTotalAmount.Text);
        _billOfMaterialsRepository.VmBomMaster.InWords = LblInWords.Text;
        _billOfMaterialsRepository.VmBomMaster.Remarks = TxtRemarks.Text;
        _billOfMaterialsRepository.VmBomMaster.GetView = DGrid;
        _billOfMaterialsRepository.VmBomMaster.SyncRowVersion = syncRow.ReturnSyncRowNo("BOM", TxtVno.Text);

        if (DGrid.RowCount <= 0)
        {
            return 0;
        }
        foreach (DataGridViewRow viewRow in DGrid.Rows)
        {
            var list = new BOM_Details();
            var ProductId = viewRow.Cells["GTxtProductId"].Value.GetLong();
            if (ProductId is 0)
            {
                continue;
            }
            list.VoucherNo = TxtVno.Text;
            list.SerialNo = viewRow.Cells["GTxtSNo"].Value.GetInt();
            list.ProductId = viewRow.Cells["GTxtProductId"].Value.GetLong();
            list.GodownId = viewRow.Cells["GTxtGodownId"].Value.GetInt();
            list.CostCenterId = viewRow.Cells["GTxtCostCenterId"].Value.GetInt();
            list.OrderNo = viewRow.Cells["GTxtOrderNo"].Value.GetString();
            list.OrderSNo = viewRow.Cells["GTxtOrderSNo"].Value.GetInt();
            list.AltQty = viewRow.Cells["GTxtAltQty"].Value.GetDecimal();
            list.AltUnitId = viewRow.Cells["GTxtAltUOMId"].Value.GetInt();
            list.Qty = viewRow.Cells["GTxtQty"].Value.GetDecimal();
            list.UnitId = viewRow.Cells["GTxtUOMId"].Value.GetInt();
            list.Rate = viewRow.Cells["GTxtRate"].Value.GetDecimal();
            list.Amount = viewRow.Cells["GTxtAmount"].Value.GetDecimal();
            list.Narration = viewRow.Cells["GTxtNarration"].Value.GetString();

            list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.Value : Guid.Empty;
            list.SyncCreatedOn = DateTime.Now;
            list.SyncLastPatchedOn = DateTime.Now;
            list.SyncRowVersion = _billOfMaterialsRepository.VmBomMaster.SyncRowVersion;
            _billOfMaterialsRepository.DetailsList.Add(list);
        }


        if (DGrid.RowCount > 0 && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
        }

        return _billOfMaterialsRepository.SaveBillOfMaterialsSetup(_actionTag);


    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global Variable ---------------

    private int _costCenterId;
    private int _masterCostCenterId;
    private int _godownId;
    private int _altUnitId;
    private int _unitId;
    private int _fAltUnitId;
    private int _departmentId;
    private int _columnIndex;
    private int _rowIndex;
    private int _gridId;
    private long _rProductId;
    private long _fProductId;

    private readonly bool _isZoom;
    private bool _isGridUpdate;

    private string _numberSchema = string.Empty;
    private string _actionTag = string.Empty;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private readonly string _zoomVoucher;
    private readonly string _getGridOrderNo = string.Empty;
    private readonly string _getGridOrderSNo = string.Empty;

    private decimal _conQty;
    private decimal _conAltQty = 1;
    private readonly IStockEntryDesign _design;
    private readonly IMasterSetup _master;
    // private readonly IStockEntry _entry;
    private readonly IBillOfMaterialsRepository _billOfMaterialsRepository;

    #region -------------- Custom Control --------------

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

    #endregion -------------- Custom Control --------------

    #endregion --------------- Global Variable ---------------
}