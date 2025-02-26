using DatabaseModule.DataEntry.ProductionSystem.Production;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.ProductionSystem.Production;
using MrDAL.DataEntry.ProductionSystem.Production;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmProduction : MrForm
{
    #region ---------------  FrmBillOfMaterials ---------------

    public FrmProduction(bool isZoom, string voucherNo)
    {
        InitializeComponent();
        _form = new ClsEntryForm(this, BtnExit);
        _design = new GetStockDesign();
        _master = new ClsMasterSetup();
        // _entry = new ClsStockEntry();
        _production = new ProductionRepository();
        _isZoom = isZoom;
        _zoomVoucher = voucherNo;
        DesignGridColumnsAsync();
        AdjustControlsInDataGrid();
        ClearControl();
        EnableControl();
    }

    private void FrmBillOfMaterials_Load(object sender, EventArgs e)
    {
        if (_isZoom)
        {
            FillBomVoucherDetails(_zoomVoucher);
            BtnEdit.Focus();
            return;
        }
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void FrmBillOfMaterials_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TxtRawProduct.Enabled)
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
            TxtFinisehdGoods.Focus();
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
        var frmDp = new FrmDocumentPrint("Crystal", "IBOM", string.Empty, TxtVno.Text, TxtVno.Text, string.Empty, string.Empty, ObjGlobal.SysDefaultInvoicePrinter, ObjGlobal.SysDefaultInvoiceDesign, string.Empty)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
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
        {
            TxtVno.Focus();
        }
        else
        {
            MskMiti.Focus();
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    private void TxtVno_Leave(object sender, EventArgs e)
    {
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag is not "SAVE")
        {
            return;
        }
        var result = TxtVno.IsDuplicate("IBOM");
        if (!result)
        {
            return;
        }
        TxtVno.WarningMessage("PRODUCT VOUCHER NUMBER IS ALREADY EXITS");
        TxtVno.Focus();
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtVno.IsValueExits())
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                TxtVno.WarningMessage($"PRODUCTION VOUCHER NUMBER IS REQUIRED FOR [{_actionTag}]");
                return;
            }
        }
        else if (TxtVno.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var outStanding = !string.IsNullOrEmpty(_actionTag) && _actionTag is "SAVE" ? "NORMAL" : "O";
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "IBOM", outStanding);
        if (result.IsValueExits())
        {
            if (_actionTag != "SAVE")
            {
                TxtVno.Text = result;
                FillProductionVoucherDetails(TxtVno.Text);
            }
        }

        TxtVno.Focus();
    }

    private void TxtBOMVno_Leave(object sender, EventArgs e)
    {
    }

    private void TxtBOMVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnBOMVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtBOMVno.IsBlankOrEmpty())
            {
                TxtBOMVno.WarningMessage("VOUCHER NUMBER IS REQUIRED..!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBOMVno, BtnBOMVno);
        }
    }

    private void BtnBOMVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "BOM", _fProductId.ToString());
        if (result.IsValueExits())
        {
            TxtBOMVno.Text = result;
            FillBomVoucherDetails(TxtBOMVno.Text);
        }
        TxtBOMVno.Focus();
    }

    private void BtnMachine_Click(object sender, EventArgs e)
    {
        using (var getList = new FrmAutoPopList("MIN", "MACHINE", _actionTag, ObjGlobal.SearchText, "", "LIST"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                getList.ShowDialog();
                if (getList.SelectedList.Count > 0)
                {
                    TxtMachine.Text = getList.SelectedList[0]["Description"].ToString().Trim();
                }

                getList.Dispose();
            }
            else
            {
                MessageBox.Show(@"MACHINE LIST NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtMachine.Focus();
                return;
            }
        }

        ObjGlobal.SearchText = string.Empty;
        TxtMachine.Focus();
    }

    private void TxtMachine_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnMachine_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtMachine.IsBlankOrEmpty())
            {
                TxtMachine.WarningMessage("MACHINE NAME IS REQUIRED FOR PRODUCTION..!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtCostingRate_TextChanged(object sender, EventArgs e)
    {
        TxtFQty_TextChanged(sender, e);
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
        if (MskMiti.MaskFull)
        {
            MskDate.Text = ObjGlobal.ReturnEnglishDate(MskMiti.Text);
        }
    }

    private void MskMiti_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            GlobalKeyPress(sender, e);
        }
        if (MskMiti.SelectionLength is 10)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                MskMiti.SelectionStart = 0;
                MskMiti.Select(0, 0);
            });
            SendKeys.SendWait("{HOME}");
        }
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
        if (MskDate.MaskFull)
        {
            MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
        }
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnRefVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnRefVno_Click(object sender, EventArgs e)
    {
        TxtRefVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SO", "");
        FillSalesOrderVoucher(TxtRefVno.Text);
        TxtRefVno.Focus();
    }

    private void MskRefDate_Leave(object sender, EventArgs e)
    {
    }

    private void TxtFinishedGoods_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(_actionTag) && TxtFinisehdGoods.Focused &&
            string.IsNullOrWhiteSpace(TxtFinisehdGoods.Text.Trim()))
        {
            MessageBox.Show(@"FINISHED GOODS CAN'T LEFT BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtFinisehdGoods.Focus();
        }
    }

    private void TxtFinishedGoods_Validating(object sender, CancelEventArgs e)
    {
        if (_fProductId is 0 && !string.IsNullOrWhiteSpace(TxtFinisehdGoods.Text))
        {
        }
    }

    private void TxtFinishedGoods_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnFinishedGoods_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (id > 0)
            {
                TxtFinisehdGoods.Text = description;
                _fProductId = id;
            }
            TxtFinisehdGoods.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtFinisehdGoods.IsValueExits())
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                TxtFinisehdGoods.WarningMessage("PLEASE SELECT FINISHED PRODUCT FOR PRODUCTION..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtFinisehdGoods, BtnFinishedGoods);
        }
    }

    private void BtnFinishedGoods_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetFinishedProduct(_actionTag, MskDate.Text);
        if (description.IsValueExits())
        {
            TxtFinisehdGoods.Text = description;
            _fProductId = id;
            TxtFQty.Text = 1.GetDecimalString();
        }
        TxtFinisehdGoods.Focus();
    }

    private void TxtFQty_Leave(object sender, EventArgs e)
    {
    }

    private void TxtFQty_Validating(object sender, CancelEventArgs e)
    {
        TxtFQty.Text = TxtFQty.GetDecimalQtyString();
    }

    private void TxtFQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            GlobalKeyPress(sender, e);
        }
        else if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtFQty_TextChanged(object sender, EventArgs e)
    {
        if (DGrid.Rows.Count > 0 && TxtFQty.GetDecimal() > 0)
        {
            for (var i = 0; i < DGrid.Rows.Count; i++)
            {
                var actQty = DGrid.Rows[i].Cells["GTxtActualQty"].Value.GetDecimal();
                var fQty = TxtFQty.GetDecimal();
                var qty = fQty * actQty;
                DGrid.Rows[i].Cells["GTxtQty"].Value = qty.GetDecimalQtyString();

                var rate = DGrid.Rows[i].Cells["GTxtRate"].Value.GetDecimal();
                DGrid.Rows[i].Cells["GTxtAmount"].Value = (qty * rate).GetDecimalString();
            }
        }
        VoucherTotalCalculation();
    }

    private void TxtCostingRate_Validating(object sender, CancelEventArgs e)
    {
        TxtCostingRate.Text = TxtCostingRate.GetDecimalString(true);
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var result = GetMasterList.CreateDepartment(true);
            if (result.id > 0)
            {
                TxtDepartment.Text = result.description;
                _departmentId = result.id;
            }
            TxtDepartment.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtMasterCostCenter.Enabled)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                DGrid.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, BtnDepartment);
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        using var getList = new FrmAutoPopList("MIN", "DEPARTMENT", _actionTag, ObjGlobal.SearchText, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            getList.ShowDialog();
            if (getList.SelectedList.Count > 0)
            {
                TxtDepartment.Text = getList.SelectedList[0]["PName"].ToString().Trim();
                _departmentId = ObjGlobal.ReturnInt(getList.SelectedList[0]["Pid"].ToString().Trim());
            }

            getList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DEPARTMENT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDepartment.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDepartment.Focus();
    }

    private void TxtDepartment_Validating(object sender, CancelEventArgs e)
    {
        if (_departmentId is 0 && !string.IsNullOrWhiteSpace(TxtDepartment.Text) &&
            !string.IsNullOrWhiteSpace(_actionTag))
        {
        }
    }

    private void TxtMasterCostCenter_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(_actionTag) && TxtMasterCostCenter.Focused &&
            string.IsNullOrWhiteSpace(TxtMasterCostCenter.Text.Trim()))
        {
            MessageBox.Show(@"COST CENTER CAN'T LEFT BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtMasterCostCenter.Focus();
        }
    }

    private void TxtMasterCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnMasterCostCenter_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var result = GetMasterList.CreateCostCenter(true);
            if (result.id > 0)
            {
                TxtMasterCostCenter.Text = result.description;
                _masterCostCenterId = result.id;
            }

            TxtMasterCostCenter.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            DGrid.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtMasterCostCenter, BtnMasterCostCenter);
        }
    }

    private void BtnMasterCostCenter_Click(object sender, EventArgs e)
    {
        using (var GetList =
               new FrmAutoPopList("MIN", "COSTCENTER", _actionTag, ObjGlobal.SearchText, "R", "LIST"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                GetList.ShowDialog();
                if (GetList.SelectedList.Count > 0)
                {
                    TxtMasterCostCenter.Text = GetList.SelectedList[0]["Description"].ToString().Trim();
                    _masterCostCenterId = ObjGlobal.ReturnInt(GetList.SelectedList[0]["LedgerId"].ToString().Trim());
                }

                GetList.Dispose();
            }
            else
            {
                MessageBox.Show(@"COST CENTER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtMasterCostCenter.Focus();
                return;
            }
        }

        ObjGlobal.SearchText = string.Empty;
        TxtMasterCostCenter.Focus();
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

        if (e.KeyCode is Keys.Enter && !TxtRawProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            DGrid.CurrentCell = DGrid.CurrentRow?.Cells[0];
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                GetTextFromGrid();
                TxtRawProduct.Focus();
                return;
            }

            GetSerialNo();
            TxtRawProduct.Focus();
        }
    }

    private void DGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
    }

    private void DGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
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

        BtnSave.Enabled = false;
        if (SaveProductionVoucher() > 0)
        {
            CustomMessageBox.ActionSuccess(TxtVno.Text, "PRODUCTION", _actionTag);
            ClearControl();
            BtnSave.Enabled = true;
            if (TxtVno.Enabled)
            {
                TxtVno.Focus();
            }
            else
            {
                TxtBOMVno.Focus();
            }
        }
        else
        {
            CustomMessageBox.ActionError(TxtVno.Text, "PRODUCTION", _actionTag);
            BtnSave.Enabled = true;
            if (TxtVno.Enabled)
            {
                TxtVno.Focus();
            }
            else
            {
                MskMiti.Focus();
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        try
        {
            var FileName = string.Empty;
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PAttachment1.ImageLocation = FileName;
            Image myimage = new Bitmap(FileName);
            PAttachment1.Image = myimage;
            PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment1.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (IsFileExists != string.Empty)
            {
                MessageBox.Show("PICTURE FILE FORMAT & " + ex.Message);
            }
        }
    }

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment1);
    }

    private void BtnAttachment2_Click(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        try
        {
            var FileName = string.Empty;
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PAttachment2.ImageLocation = FileName;
            Image myimage = new Bitmap(FileName);
            PAttachment2.Image = myimage;
            PAttachment2.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment2.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (IsFileExists != string.Empty)
            {
                MessageBox.Show("PICTURE FILE FORMAT & " + ex.Message);
            }
        }
    }

    private void LinkAttachment2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment2);
    }

    private void BtnAttachment3_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var fileName = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment3.ImageLocation = fileName;
            Image image = new Bitmap(fileName);
            PAttachment3.Image = image;
            PAttachment3.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment3.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show("PICTURE FILE FORMAT & " + ex.Message);
            }
        }
    }

    private void LinkAttachment3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment3);
    }

    private void BtnAttachment4_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var fileName = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment4.ImageLocation = fileName;
            var image = new Bitmap(fileName);
            PAttachment4.Image = image;
            PAttachment4.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment4.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show(@"PICTURE FILE FORMAT & " + ex.Message);
            }
        }
    }

    private void LinkAttachment4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment4);
    }

    private void BtnAttachment5_Click(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        try
        {
            var FileName = string.Empty;
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PAttachment5.ImageLocation = FileName;
            Image myimage = new Bitmap(FileName);
            PAttachment5.Image = myimage;
            PAttachment5.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment5.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (IsFileExists != string.Empty)
            {
                MessageBox.Show("PICTURE FILE FORMAT & " + ex.Message);
            }
        }
    }

    private void LinkAttachment5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment5);
    }

    private void BtnRemarks_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "NRMASTER", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtRemarks.Text = frmPickList.SelectedList[0]["NRDESC"].ToString().Trim();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CODULDN'T FIND ANY NARRATION OR REMARKS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtRemarks.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtRemarks.Focus();
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnRemarks_Click(sender, e);
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenProductList();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, productId) = GetMasterList.CreateProduct(true);
            if (productId > 0)
            {
                TxtRawProduct.Text = description;
                _rProductId = productId;
                GetProductInfo(_rProductId);
            }
            TxtRawProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRawProduct, OpenProductList);
        }
    }

    private void TxtRawProduct_Validating(object sender, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            TxtRawProduct.WarningMessage("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            return;
        }

        if (DGrid.RowCount == 1 && TxtRawProduct.Enabled && TxtRawProduct.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                TxtRawProduct.WarningMessage("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                return;
            }
        }

        if (DGrid.RowCount >= 1 && TxtRawProduct.Enabled && TxtRawProduct.IsBlankOrEmpty())
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
    }

    private void TxtRawProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenProductList();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, productId) = GetMasterList.CreateProduct(true);
            if (productId > 0)
            {
                TxtRawProduct.Text = description;
                _rProductId = productId;
                GetProductInfo(_rProductId);
            }
            TxtRawProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRawProduct, OpenProductList);
        }
    }

    private void TxtGodown_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.StockGodownItemMandatory)
        {
            if (TxtGodown.IsBlankOrEmpty() && TxtGodown.Enabled)
            {
                TxtGodown.WarningMessage("GODOWN IS MANDATORY FOR PRODUCTION..!!");
                return;
            }
        }
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        var amount = TxtQty.GetDecimal() * TxtRate.GetDecimal();
        TxtAmount.Text = amount.GetDecimalQtyString();
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
        TxtQty.Text = TxtQty.GetDecimalQtyString();
        if (!TxtQty.Enabled || !TxtRawProduct.Enabled) return;
        if (TxtQty.IsBlankOrEmpty())
        {
            TxtQty.WarningMessage("PRODUCT OPENING QTY CANNOT BE ZERO");
            return;
        }
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtRateOnTextChanged(object sender, EventArgs e)
    {
        var amount = TxtRate.GetDecimal() * TxtQty.GetDecimal();
        TxtAmount.Text = amount.GetDecimalString();
    }

    private void TxtAmount_Validating(object sender, CancelEventArgs e)
    {
        if (TxtRawProduct.IsBlankOrEmpty())
        {
            TxtRawProduct.WarningMessage("ENTER THE RAW MATERIALS..!!");
            return;
        }
        if (TxtQty.GetDecimal() is 0)
        {
            TxtQty.WarningMessage("QTY CAN'T BE ZERO..!!");
            return;
        }
        AddDataToGridDetails(_isGridUpdate);
    }

    private void TxtCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCostCenter_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCostCenter.IsValueExits())
            {
                DGrid.Focus();
            }
            else
            {
                TxtCostCenter.WarningMessage("PLEASE SELECT COST CENTER FOR THE PRODUCTION");
            }
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateCostCenter(true);
            if (description.IsValueExits())
            {
                TxtMasterCostCenter.Text = description;
            }
            TxtMasterCostCenter.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtMasterCostCenter, OpenCostCenterList);
        }
    }

    private void BtnCostCenter_Click(object sender, EventArgs e)
    {
    }

    #endregion ---------------  FrmBillOfMaterials ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void ClearControl()
    {
        Text = !string.IsNullOrWhiteSpace(_actionTag)
            ? $" FINISHED GOODS PRODUCTION SETUP [{_actionTag}]"
            : " FINISHED GOODS PRODUCTION SETUP";
        TxtVno.Clear();
        TxtVno.ReadOnly = !string.IsNullOrWhiteSpace(_actionTag) && _actionTag != "SAVE";
        DGrid.ClearSelection();
        DGrid.Rows.Clear();
        if (_actionTag is "SAVE")
        {
            TxtVno.GetCurrentVoucherNo("IBOM", _numberSchema);
        }

        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
        }

        TxtBOMVno.Clear();
        MskBOMDate.Clear();
        TxtRefVno.Clear();
        MskRefDate.Clear();

        _fProductId = 0;
        TxtFinisehdGoods.Clear();
        TxtFQty.Clear();

        _fUnitId = 0;
        TxtFUOM.Clear();

        _departmentId = 0;
        TxtDepartment.Clear();

        _masterCostCenterId = 0;
        TxtMasterCostCenter.Clear();

        TxtCostingRate.Text = 100.GetDecimalString();

        LblTotalAltQty.Text = 0.GetDecimalString();
        LblTotalQty.Text = 0.GetDecimalQtyString();
        LblTotalAmount.Text = 0.GetDecimalString();

        ClearProductDetails();

        ObjGlobal.DGridColorCombo(DGrid);
        TabProduct.SelectedTab = TabRawMaterial;
    }

    private void ClearProductDetails()
    {
        _gridId = -1;
        _altQtyStock = 0;
        _rProductId = 0;

        TxtRawProduct.Clear();
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
        TxtAmount.Clear();
        _isGridUpdate = false;
        PanelProduct.Visible = false;
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable || !tagStrings.Contains(_actionTag);
        TxtVno.Enabled = BtnVno.Enabled = isEnable || tagStrings.Contains(_actionTag);
        TxtBOMVno.Enabled = BtnBOMVno.Enabled = MskBOMDate.Enabled = isEnable;
        TxtMasterCostCenter.Enabled = BtnMasterCostCenter.Enabled = isEnable;

        TxtCostingRate.Enabled = isEnable;
        TxtMachine.Enabled = BtnMachine.Enabled = isEnable;

        MskMiti.Enabled = MskDate.Enabled = isEnable;
        MskMiti.Enabled = MskDate.Enabled = isEnable;

        TxtRefVno.Enabled = BtnRefVno.Enabled = MskRefDate.Enabled = isEnable;

        TxtFinisehdGoods.Enabled = BtnFinishedGoods.Enabled = TxtFQty.Enabled = isEnable;
        TxtFUOM.Enabled = false;

        TxtDepartment.Enabled = BtnDepartment.Enabled = ObjGlobal.StockDepartmentEnable && isEnable;
        TabProduct.Enabled = _isZoom || isEnable;
        LblTotalQty.Enabled = LblTotalAltQty.Enabled = LblTotalAmount.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.StockRemarksEnable || tagStrings.Contains(_actionTag);
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || tagStrings.Contains(_actionTag);
        EnableGridControl();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;
        TxtRawProduct.Enabled = TxtRawProduct.Visible = isEnable;
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
        TxtAmount.Enabled = isEnable;
        TxtAmount.Visible = isEnable;
    }

    private bool IsValidForm()
    {
        if (TxtVno.IsBlankOrEmpty())
        {
            TxtVno.WarningMessage("VOUCHER NUMBER IS BLANK..!!");
            return false;
        }

        if (!MskDate.MaskFull)
        {
            MskDate.WarningMessage(@"BOM MEMO DATE IS INVALID..!!");
            return false;
        }

        if (!MskMiti.MaskFull)
        {
            MskMiti.WarningMessage(@"BOM MEMO MITI IS INVALID..!!");
            return false;
        }

        if (TxtFinisehdGoods.IsBlankOrEmpty())
        {
            TxtFinisehdGoods.WarningMessage(@"FINISHED GOODS IS BLANK PLEASE CHOOSE IT..!!");
            return false;
        }

        if (TxtFQty.GetDecimal() is 0)
        {
            TxtFinisehdGoods.WarningMessage(@"FINISHED GOODS QUANTITY CAN'T BE ZERO..!!");
            return false;
        }

        if (DGrid.Rows.Count is 0 || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetInt() is 0)
        {
            CustomMessageBox.Warning("REMARKS ON THIS VOUCHER IS MANDATORY ..!!");
            TxtRawProduct.Focus();
            return false;
        }

        if (TxtRemarks.IsBlankOrEmpty() && ObjGlobal.StockRemarksMandatory)
        {
            TxtRawProduct.WarningMessage("REMARKS ON THIS VOUCHER IS MANDATORY ..!!");
            return false;
        }

        return true;
    }

    private void FillProductionVoucherDetails(string voucherNo)
    {
        var dsProduction = _production.GetProductionVoucherDetails(voucherNo);
        if (dsProduction.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsProduction.Tables[0].Rows)
            {
                TxtBOMVno.Text = dr["BOMVNo"].ToString();
                MskBOMDate.Text = TxtBOMVno.Text.Length > 0
                    ? dr["BOMDate"].GetDateString()
                    : string.Empty;
                MskBOMDate.Text = MskBOMDate.GetNepaliDate(MskBOMDate.Text);
                MskMiti.Text = dr["VMiti"].ToString();
                MskDate.Text = dr["VDate"].GetDateString();
                TxtRefVno.Text = dr["OrderNo"].ToString();
                MskRefDate.Text = !string.IsNullOrWhiteSpace(TxtRefVno.Text)
                    ? dr["OrderDate"].GetDateString()
                    : "";
                TxtFinisehdGoods.Text = dr["PName"].ToString();
                _fProductId = dr["FinishedGoodsId"].GetLong();
                TxtFQty.Text = ObjGlobal.ReturnDouble(dr["FinishedGoodsQty"].ToString())
                    .ToString(ObjGlobal.SysAmountFormat);
                TxtDepartment.Text = dr["DName"].ToString();
                TxtFUOM.Text = dr["UnitCode"].ToString();
                TxtCostingRate.Text = ObjGlobal.ReturnDouble(dr["Costing"].ToString()).ToString();
                TxtMachine.Text = dr["Machine"].ToString();
                _departmentId = ObjGlobal.ReturnInt(dr["DepartmentId"].ToString());
                _masterCostCenterId = ObjGlobal.ReturnInt(dr["CCId"].ToString());
                TxtMasterCostCenter.Text = dr["CCName"].ToString();
                LblInWords.Text = dr["InWords"].ToString();
                TxtRemarks.Text = dr["Remarks"].ToString();

                var imageData = dr["PAttachment1"] as byte[] ?? null;
                if (imageData is { Length: > 0 })
                {
                    Image newImage = (Bitmap)new ImageConverter().ConvertFrom(imageData);
                    PAttachment1.Image = newImage;
                    PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    PAttachment1.Image = null;
                }

                var imageData2 = dr["PAttachment2"] as byte[] ?? null;
                if (imageData2 is { Length: > 0 })
                {
                    Image newImage = (Bitmap)new ImageConverter().ConvertFrom(imageData2);
                    PAttachment2.Image = newImage;
                    PAttachment2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    PAttachment2.Image = null;
                }

                var imageData3 = dr["PAttachment3"] as byte[] ?? null;
                if (imageData3 is { Length: > 0 })
                {
                    Image newImage = (Bitmap)new ImageConverter().ConvertFrom(imageData3);
                    PAttachment3.Image = newImage;
                    PAttachment3.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    PAttachment3.Image = null;
                }

                var imageData4 = dr["PAttachment4"] as byte[] ?? null;
                if (imageData4 is { Length: > 0 })
                {
                    Image newImage = (Bitmap)new ImageConverter().ConvertFrom(imageData4);
                    PAttachment4.Image = newImage;
                    PAttachment4.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    PAttachment4.Image = null;
                }

                var imageData5 = dr["PAttachment5"] as byte[] ?? null;
                if (imageData5 is { Length: > 0 })
                {
                    Image newImage = (Bitmap)new ImageConverter().ConvertFrom(imageData5);
                    PAttachment5.Image = newImage;
                    PAttachment5.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    PAttachment5.Image = null;
                }
            }

            if (dsProduction.Tables[1].Rows.Count > 0)
            {
                DGrid.Rows.Clear();
                var iRow = 0;
                foreach (DataRow dr in dsProduction.Tables[1].Rows)
                {
                    var rows = DGrid.Rows.Count;
                    DGrid.Rows.Add();
                    DGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["SerialNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodownId"].Value = dr["GodownId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenterId"].Value = dr["CCId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenter"].Value = dr["CCName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtBOMVno"].Value = dr["BOMNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtBOMSNo"].Value = dr["BOMSNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtActualQty"].Value = dr["BOMQty"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtIssueVno"].Value = dr["IssueNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtIssueSNo"].Value = dr["IssueSNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["OrderNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtOrderSNo"].Value = dr["OrderSNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["AltQty"].GetDecimalQtyString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["AltUnitId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
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

        VoucherTotalCalculation();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
        TxtVno.Focus();
    }

    private void FillBomVoucherDetails(string voucherNo)
    {
        var dsBom = _production.GetBomVoucherDetails(voucherNo);
        if (dsBom.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsBom.Tables[0].Rows)
            {
                TxtBOMVno.Text = dr["VoucherNo"].ToString();
                MskBOMDate.Text = dr["VDate"].GetDateString();
                TxtRefVno.Text = dr["OrderNo"].ToString();
                MskRefDate.Text = dr["OrderDate"].GetDateString();
                TxtDepartment.Text = dr["DName"].ToString();
                TxtUnit.Text = dr["UnitCode"].ToString();
                TxtMachine.Text = dr["CCName"].ToString();
                _masterCostCenterId = dr["CostCenterId"].GetInt();
                TxtMasterCostCenter.Text = dr["CCName"].ToString();
                _departmentId = dr["CostCenterId"].GetInt();
                TxtDepartment.Text = dr["DName"].ToString();
                LblInWords.Text = dr["InWords"].ToString();
                TxtRemarks.Text = dr["Remarks"].ToString();
            }

            if (dsBom.Tables[1].Rows.Count > 0)
            {
                DGrid.Rows.Clear();
                var iRow = 0;
                DGrid.Rows.Add(dsBom.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsBom.Tables[1].Rows)
                {
                    var rows = DGrid.Rows.Count;
                    DGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["SerialNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodownId"].Value = dr["GodownId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenterId"].Value = dr["CostCenterId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtCostCenter"].Value = dr["CCName"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtBOMVno"].Value = dr["VoucherNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtBOMSNo"].Value = dr["SerialNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtIssueVno"].Value = string.Empty;
                    DGrid.Rows[iRow].Cells["GTxtIssueSNo"].Value = 0;
                    DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["OrderNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtOrderSNo"].Value = dr["OrderSNo"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["AltQty"].GetDecimalQtyString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["AltUnitId"].ToString();
                    DGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                    if (TxtFQty.GetDecimal() > 1)
                    {
                        var fgQty = TxtFQty.GetDecimal();
                        var issueQty = dr["Qty"].GetDecimal();
                        var singleQty = issueQty / fgQty;

                        DGrid.Rows[iRow].Cells["GTxtActualQty"].Value = singleQty.GetDecimalQtyString();
                    }
                    else
                    {
                        DGrid.Rows[iRow].Cells["GTxtActualQty"].Value = dr["Qty"].GetDecimalQtyString();
                    }

                    DGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
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

        VoucherTotalCalculation();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
        TxtVno.Focus();
    }

    private void FillSalesOrderVoucher(string voucherNo)
    {
    }

    private void AddDataToGridDetails(bool update)
    {
        if (TxtCostCenter.IsBlankOrEmpty() || _costCenterId is 0)
        {
            TxtCostCenter.WarningMessage("COST CENTER IS REQUIRED FOR PRDODUCTION");
            return;
        }

        if (TxtGodown.IsBlankOrEmpty() && ObjGlobal.StockGodownMandatory)
        {
            TxtGodown.WarningMessage(@"GODOWN IS MANDATORY IS MANDATORY..!!");
            TxtGodown.Focus();
            return;
        }

        if (TxtQty.GetDecimal() is 0)
        {
            MessageBox.Show(@"QUANTITY CAN'T BE ZERO..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtQty.Focus();
            return;
        }

        var iRows = 0;
        if (update)
        {
            iRows = _gridId;
        }
        else
        {
            DGrid.Rows.Add();
            iRows = DGrid.Rows.Count - 1;
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _rProductId.ToString();
        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtShortName.Text;
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtRawProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = TxtShortName.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtCostCenterId"].Value = _costCenterId.ToString();
        DGrid.Rows[iRows].Cells["GTxtCostCenter"].Value = TxtCostCenter.Text;
        DGrid.Rows[iRows].Cells["GTxtBOMVno"].Value = TxtBOMVno.Text;
        DGrid.Rows[iRows].Cells["GTxtBOMSNo"].Value = iRows + 1;
        DGrid.Rows[iRows].Cells["GTxtActualQty"].Value = TxtQty.Text;
        DGrid.Rows[iRows].Cells["GTxtIssueVno"].Value = _getGridOrderNo;
        DGrid.Rows[iRows].Cells["GTxtIssueSNo"].Value = _getGridOrderSNo;
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = _getGridOrderNo;
        DGrid.Rows[iRows].Cells["GTxtOrderSNo"].Value = _getGridOrderSNo;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.Text;
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.Text;
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.Text;
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtAmount.Text;
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = string.Empty;
        _addToGrid = true;
        DGrid.ClearSelection();
        ObjGlobal.DGridColorCombo(DGrid);
        VoucherTotalCalculation();
        ClearProductDetails();
        TxtRawProduct.Focus();
    }

    private void VoucherTotalCalculation()
    {
        decimal altQty = 0;
        decimal qty = 0;
        decimal amount = 0;

        var execute = DGrid.Rows.OfType<DataGridViewRow>();
        var rows = execute as DataGridViewRow[] ?? execute.ToArray();

        altQty = rows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal());
        qty = rows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal());
        amount = rows.Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal());

        LblTotalAltQty.Text = altQty.GetDecimalQtyString();
        LblTotalQty.Text = qty.GetDecimalQtyString();
        LblTotalAmount.Text = amount.GetDecimalString();
        LblInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalAmount.Text);
    }

    private void UpdateImageStockVoucher()
    {
        var converter = new ImageConverter();
        var arr1 = (byte[])converter.ConvertTo(PAttachment1.Image, typeof(byte[]));
        _production.VmProductionMaster.PAttachment1 = arr1;

        var arr2 = (byte[])converter.ConvertTo(PAttachment2.Image, typeof(byte[]));
        _production.VmProductionMaster.PAttachment2 = arr2;

        var arr3 = (byte[])converter.ConvertTo(PAttachment3.Image, typeof(byte[]));
        _production.VmProductionMaster.PAttachment3 = arr3;

        var arr4 = (byte[])converter.ConvertTo(PAttachment4.Image, typeof(byte[]));
        _production.VmProductionMaster.PAttachment4 = arr4;

        var arr5 = (byte[])converter.ConvertTo(PAttachment5.Image, typeof(byte[]));
        _production.VmProductionMaster.PAttachment5 = arr5;
    }

    private void GetProductInfo(long selectedId)
    {
        if (selectedId == 0)
        {
            return;
        }

        var dtProduct = _master.GetMasterProductList(_actionTag, selectedId);
        if (dtProduct.Rows.Count <= 0)
        {
            return;
        }

        TxtRawProduct.Text = dtProduct.Rows[0]["PName"].ToString();
        TxtShortName.Text = dtProduct.Rows[0]["PShortName"].ToString();
        TxtRate.Text = dtProduct.Rows[0]["PBuyRate"].ToString();

        _unitId = dtProduct.Rows[0]["PUnit"].GetInt();
        if (_unitId > 0)
        {
            TxtUnit.Enabled = true;
            TxtUnit.Text = dtProduct.Rows[0]["UnitCode"].ToString();
            TxtQty.Text = 1.GetDecimalQtyString();
        }

        _altUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        if (_altUnitId > 0)
        {
            TxtAltQty.Enabled = TxtAltUnit.Enabled = true;
            TxtAltUnit.Text = dtProduct.Rows[0]["UnitCode"].ToString();
        }

        var conQty = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        var conAltQty = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        if (conQty <= 0)
        {
            return;
        }
        TxtQty.Text = conQty.GetDecimalString();
        TxtAltQty.Text = conAltQty.GetDecimalString();
    }

    private void GetTextFromGrid()
    {
        _gridId = _rowIndex;
        _rProductId = DGrid.Rows[_gridId].Cells["GTxtProductId"].Value.GetLong();

        TxtRawProduct.Text = DGrid.Rows[_gridId].Cells["GTxtProduct"].Value.ToString();
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
        TxtAmount.Text = DGrid.Rows[_gridId].Cells["GTxtAmount"].Value.ToString();
        _isGridUpdate = true;
        TxtRawProduct.Focus();
    }

    private void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.Image == null) { return; }

        var fileExt = Path.GetExtension(pictureBox.Image.ToString());
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png"
           ) //&& this.Tag == "SAVE")
        {
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        }
        else
        {
            var path = pictureBox.ImageLocation;
            Process.Start(path);
        }
    }

    private string ReturnVoucherNo()
    {
        var dtCheck = ClsMasterSetup.GetDocumentNumberingSchema("IBOM");
        if (dtCheck.Rows.Count == 1)
        {
            _numberSchema = dtCheck.Rows[0]["DocDesc"].ToString();
            TxtVno.GetCurrentVoucherNo("IBOM", _numberSchema);
        }
        else if (dtCheck.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme
            {
                Source = "IBOM",
                TblName = "INV.Production_Master",
                FldNAme = "VoucherNo"
            };
            if (wnd.ShowDialog() != DialogResult.OK)
            {
                return TxtVno.Text;
            }

            if (string.IsNullOrEmpty(wnd.VNo))
            {
                return TxtVno.Text;
            }

            _numberSchema = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }

        return TxtVno.Text;
    }

    private void DesignGridColumnsAsync()
    {
        _design.GetProductionEntryDesign(DGrid);
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (ObjGlobal.StockGodownEnable)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        }
        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += TxtShortName_KeyDown;
        TxtRawProduct = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtRawProduct.KeyDown += TxtRawProduct_KeyDown;
        TxtRawProduct.Validating += TxtRawProduct_Validating;

        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.Validating += TxtGodown_Validating;
        TxtCostCenter = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtCostCenter.KeyDown += TxtCostCenter_KeyDown;

        TxtAltQty = new MrGridNumericTextBox(DGrid);
        TxtAltQty.TextChanged += TxtAltQty_TextChanged;

        TxtAltUnit = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.TextChanged += TxtQty_TextChanged;
        TxtQty.Validating += TxtQty_Validating;

        TxtUnit = new MrGridTextBox(DGrid);

        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.TextChanged += TxtRateOnTextChanged;

        TxtAmount = new MrGridNumericTextBox(DGrid);
        TxtAmount.Validating += TxtAmount_Validating;
    }

    private void OpenCostCenterList()
    {
        var (description, id) = GetMasterList.GetCostCenterList(_actionTag);
        if (description.IsValueExits())
        {
            TxtCostCenter.Text = description;
            _costCenterId = id;
        }
        TxtCostCenter.Focus();
    }

    private void OpenProductList()
    {
        var (description, productId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        if (productId > 0)
        {
            TxtRawProduct.Text = description;
            _rProductId = productId;
            GetProductInfo(_rProductId);
        }

        TxtRawProduct.Focus();
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
        TxtRawProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRawProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRawProduct.TabIndex = columnIndex;

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
        TxtAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAmount.TabIndex = columnIndex;
    }

    private int SaveProductionVoucher()
    {
        var result = DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtProductId"].Value.GetLong();
        if (result == 0)
        {
            DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
        }
        _production.VmProductionMaster.VoucherNo =
            _actionTag is "SAVE" ? TxtVno.GetCurrentVoucherNo("IBOM", _numberSchema) : TxtVno.Text;
        _production.VmProductionMaster.BOMVNo = TxtBOMVno.Text;
        _production.VmProductionMaster.BOMDate = DateTime.Parse(ObjGlobal.ReturnEnglishDate(MskBOMDate.Text));
        _production.VmProductionMaster.VDate = Convert.ToDateTime(MskDate.Text);
        _production.VmProductionMaster.VMiti = MskMiti.Text;
        _production.VmProductionMaster.FinishedGoodsId = _fProductId;
        _production.VmProductionMaster.FinishedGoodsQty = ObjGlobal.ReturnDecimal(TxtFQty.Text);
        _production.VmProductionMaster.DepartmentId = _departmentId;
        _production.VmProductionMaster.CostCenterId = _masterCostCenterId;
        _production.VmProductionMaster.Machine = TxtMachine.Text.Trim().Length > 0
            ? TxtMachine.Text.ToUpper()
            : TxtMasterCostCenter.Text.Trim().ToUpper();
        _production.VmProductionMaster.Amount = ObjGlobal.ReturnDecimal(LblTotalAmount.Text);
        _production.VmProductionMaster.InWords = LblInWords.Text;
        _production.VmProductionMaster.Remarks = TxtRemarks.Text;
        _production.VmProductionMaster.GetView = DGrid;
        UpdateImageStockVoucher();

        if (DGrid.RowCount <= 0)
        {
            return 0;
        }

        foreach (DataGridViewRow viewRow in DGrid.Rows)
        {
            var list = new Production_Details();
            var detailsProduct = viewRow.Cells["GTxtProductId"].Value.GetLong();
            if (detailsProduct is 0)
            {
                continue;
            }
            list.VoucherNo = TxtVno.Text;
            list.SerialNo = viewRow.Cells["GTxtSNo"].Value.GetInt();
            list.ProductId = viewRow.Cells["GTxtProductId"].Value.GetLong();
            list.GodownId = viewRow.Cells["GTxtGodownId"].Value.GetInt();
            list.CostCenterId = viewRow.Cells["GTxtCostCenterId"].Value.GetInt();
            list.BOMNo = viewRow.Cells["GTxtBOMVno"].Value.GetString();
            list.BOMSNo = viewRow.Cells["GTxtBOMSNo"].Value.GetInt();
            list.BOMQty = viewRow.Cells["GTxtActualQty"].Value.GetDecimal();
            list.IssueNo = viewRow.Cells["GTxtIssueVno"].Value.GetString();
            list.IssueSNo = viewRow.Cells["GTxtIssueSNo"].Value.GetInt();
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
            list.SyncRowVersion = _production.VmProductionMaster.SyncRowVersion;
            _production.DetailsList.Add(list);
        }

        return _production.SaveProductionSetup(_actionTag);
    }

    #endregion --------------- Method ---------------

    // GLOBAL OBJECT

    #region --------------- Global Variable ---------------

    private int _costCenterId;
    private int _masterCostCenterId;
    private int _godownId;
    private int _altUnitId;
    private int _unitId;
    private int _fUnitId;
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

    private double _altQtyStock;
    private readonly IStockEntryDesign _design;
    private readonly IMasterSetup _master;
    // private readonly IStockEntry _entry;
    private readonly IProductionRepository _production;
    private ClsEntryForm _form;

    // GRID CONTROL

    #region ---------- GRID CONTROL ----------

    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtRawProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtCostCenter { get; set; }
    private MrGridTextBox TxtAltUnit { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }

    #endregion ---------- GRID CONTROL ----------

    #endregion --------------- Global Variable ---------------
}