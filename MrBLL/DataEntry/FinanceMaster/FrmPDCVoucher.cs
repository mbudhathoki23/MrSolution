using DevExpress.Utils.Extensions;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.FinanceTransaction.PostDateCheque;
using MrDAL.DataEntry.Interface.FinanceTransaction.PostDateCheque;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MrBLL.DataEntry.FinanceMaster;

public partial class FrmPDCVoucher : MrForm
{
    #region ------------- Form -------------

    public FrmPDCVoucher(bool zoom, string txtZoomVno)
    {
        InitializeComponent();
        _form = new ClsMasterForm(this, BtnExit);
        // _entry = new ClsFinanceEntry();
        _pDCRepository = new PostDateChequeRepository();
        _master = new ClsMasterSetup();
        _zoomVno = txtZoomVno;
        _isZoom = zoom;
        ClearControl();
        EnableControl();
    }

    private void FrmProvisionPDC_Load(object sender, EventArgs e)
    {
        if (_zoomVno.IsValueExits() && _isZoom)
        {
            BindPdcVoucher(_zoomVno);
        }
        BtnNew.Focus();
    }

    private void FrmProvisionPDC_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
            {
                if (CustomMessageBox.ClearVoucherDetails("PDC") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    EnableControl();
                    ClearControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        ReturnVoucherNumber();
        if (TxtVno.Enabled) TxtVno.Focus();
        else MskMiti.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtRemarks.Enabled = false;
        TxtVno.Focus();
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
            if (SaveProvisionCheque() > 0)
            {
                BtnSave.Enabled = false;
                _pDCRepository.PdcMaster.VoucherNo = TxtVno.Text.Trim();
                CustomMessageBox.ActionSuccess(TxtVno.Text, "POST DATED CHEQUE", _actionTag);
                ClearControl();
                BtnSave.Enabled = true;
                if (string.IsNullOrEmpty(TxtVno.Text.Trim()))
                {
                    TxtVno.Focus();
                }
                else
                {
                    MskMiti.Focus();
                }
            }
            else
            {
                CustomMessageBox.Warning(@"ERROR ON DATA SAVE..!!");
                BtnSave.Enabled = true;
            }
        }
        else
        {
            CustomMessageBox.Warning(@"ERROR ON DATA SAVE..!!");
            BtnSave.Enabled = true;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        BtnExit.PerformClick();
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVno.Text))
        {
            if (_actionTag.ToUpper() == "SAVE")
            {
                using var vnoTable = GetConnection.SelectDataTableQuery($"SELECT DISTINCT pdc.VoucherNo FROM AMS.PostDateCheque pdc WHERE pdc.VoucherNo='{TxtVno.Text.Trim()}'");
                if (vnoTable.Rows.Count > 0)
                {
                    TxtVno.WarningMessage("ENTER VOUCHER NUMBER IS ALREADY EXITS..!!");
                    return;
                }
            }
        }
        if (TxtVno.IsBlankOrEmpty() && TxtVno.ValidControl(ActiveControl))
        {
            TxtVno.WarningMessage("VOUCHER NUMBER IS BLANK PLEASE ENTER VOUCHER NO..!!");
            return;
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MAX", "PDC", "");
        if (_actionTag != "SAVE" && result.IsValueExits())
        {
            TxtVno.Text = result;
            BindPdcVoucher(TxtVno.Text.Trim());
        }
        TxtVno.Focus();
    }

    private void TxtVno_Leave(object sender, EventArgs e)
    {
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnVno_Click(sender, e);
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
        if (MskMiti.MaskCompleted)
        {
            var result = MskMiti.IsDateExits("M");
            if (result)
            {
                var valid = MskMiti.IsValidDateRange("M");
                if (valid)
                {
                    MskDate.Text = MskDate.GetEnglishDate(MskMiti.Text);
                    return;
                }
                MskMiti.WarningMessage($"VOUCHER MITI MUST BE BETWEEN {ObjGlobal.CfStartBsDate} TO {ObjGlobal.CfEndBsDate}..!!");
                return;
            }
            MskMiti.WarningMessage("ENTER MITI IS INVALID..!!");
            return;
        }
        e.Cancel = true;
        MskMiti.WarningMessage(@"MITI CAN NOT BE LEFT BLANK..!!");
        return;
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
        if (MskDate.MaskCompleted)
        {
            var exits = MskDate.IsDateExits("D");
            if (exits)
            {
                var valid = MskDate.IsValidDateRange("D");
                if (valid)
                {
                    MskMiti.Text = MskMiti.GetNepaliDate(MskDate.Text);
                    return;
                }
                MskDate.WarningMessage($"VOUCHER DATE RANGE MUST BE {ObjGlobal.CfStartAdDate.GetDateString()} TO {ObjGlobal.CfEndAdDate.GetDateString()}..!!");
                return;
            }
            MskDate.WarningMessage("PLEASE ENTER THE VALID DATE..!!");
            return;
        }
        MskDate.WarningMessage("PLEASE ENTER THE VALID DATE..!!");
        return;
    }

    private void CmbType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void CmbStatus_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void TxtBank_Enter(object sender, EventArgs e)
    {
    }

    private void TxtBank_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtBank.Text.Trim()) && !string.IsNullOrEmpty(_actionTag) && TxtBank.Focused)
        {
            MessageBox.Show(@"PLEASE SELECT LEDGER ..??", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBank.Focus();
        }
    }

    private void TxtBank_Validating(object sender, CancelEventArgs e)
    {
        _bankLedgerId = _bankLedgerId switch
        {
            0 when !string.IsNullOrEmpty(TxtBank.Text.Trim()) => _master.ReturnLongValueFromTable(
                "AMS.GeneralLedger", "GLId", "GLName", TxtBank.Text.Trim()),
            _ => _bankLedgerId
        };
    }

    private void BtnBank_Click(object sender, EventArgs e)
    {
        (TxtBank.Text, _bankLedgerId) = GetMasterList.GetGeneralLedger(_actionTag, "BANK");
        TxtDrawnon.Text = CmbType.Text switch
        {
            "Payment" => TxtBank.Text,
            _ => TxtDrawnon.Text
        };
    }

    private void TxtChequeNo_Leave(object sender, EventArgs e)
    {
        if (TxtChequeNo.IsBlankOrEmpty() && _actionTag.IsValueExits() && TxtChequeNo.Focused)
        {
            if (CustomMessageBox.Question(@"CHEQUE NO IS BLANK..DO YOU WANT TO CONTINUE..??") is DialogResult.No)
            {
                TxtChequeNo.Focus();
            }
        }
    }

    private void TxtChequeNo_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag is "SAVE")
        {
            var dt = GetConnection.SelectDataTableQuery($"SELECT pdc.ChequeNo FROM AMS.PostDateCheque pdc WHERE pdc.BankName='{TxtClientBank.Text.Trim()}' AND pdc.BranchName='{TxtBankBranch.Text.Trim()}' and pdc.ChequeNo='{TxtChequeNo.Text}'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show(@"CHEQUE NO IS ALREADY EXITS ..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtChequeNo.Focus();
            }
        }
        else if (_actionTag is "UPDATE")
        {
            var dt = GetConnection.SelectDataTableQuery(
                $"SELECT pdc.ChequeNo FROM AMS.PostDateCheque pdc WHERE pdc.BankName='{TxtClientBank.Text.Trim()}' AND pdc.BranchName='{TxtBankBranch.Text.Trim()}' AND VoucherNo <> '{TxtVno.Text}' and pdc.ChequeNo='{TxtChequeNo.Text}'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show(@"CHEQUE NO IS ALREAD EXITS ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                TxtChequeNo.Focus();
            }
        }
    }

    private void TxtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void MskChequeMiti_Enter(object sender, EventArgs e)
    {
    }

    private void MskChequeMiti_Leave(object sender, EventArgs e)
    {
    }

    private void MskChequeMiti_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void MskChequeDate_Enter(object sender, EventArgs e)
    {
    }

    private void MskChequeDate_Leave(object sender, EventArgs e)
    {
    }

    private void MskChequeDate_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void MskChequeDate_Validating(object sender, CancelEventArgs e)
    {
        MskChequeMiti.Text = MskChequeDate.MaskCompleted switch
        {
            true => ObjGlobal.ReturnNepaliDate(MskChequeDate.Text),
            _ => MskChequeMiti.Text
        };
    }

    private void TxtDrawnon_Enter(object sender, EventArgs e)
    {
    }

    private void TxtDrawnon_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDrawnon.Text.Trim()) && !string.IsNullOrEmpty(_actionTag) && TxtDrawnon.Focused)
        {
            MessageBox.Show(@"PLEASE THE VALUE ON DRAW ON...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDrawnon.Focus();
        }
    }

    private void TxtDrawnon_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtDrawnon_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) BtnDrawOn_Click(sender, e);
    }

    private void TxtDepartment_Enter(object sender, EventArgs e)
    {
    }

    private void TxtDepartment_Leave(object sender, EventArgs e)
    {
    }

    private void TxtDepartment_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtDepartment_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.F1) BtnDepartment.PerformClick();
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        (TxtDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
        TxtDepartment.Focus();
    }

    private void BtnBankDesc_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetPdcBankDescription();
        if (result.IsValueExits())
        {
            TxtClientBank.Text = result;
        }
        TxtClientBank.Focus();
    }

    private void TxtClientBank_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) BtnBankDesc_Click(sender, e);
    }

    private void TxtClientBank_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtClientBank_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtClientBank.Text.Trim()) &&
            !string.IsNullOrEmpty(_actionTag) && TxtClientBank.Focused)
        {
            MessageBox.Show(@"PLEASE THE VALUE ON BANK DESCRIPTION ...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtClientBank.Focus();
        }
    }

    private void TxtBankBranch_Enter(object sender, EventArgs e)
    {
    }

    private void TxtBankBranch_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtClientBank.Text.Trim()) &&
            !string.IsNullOrEmpty(_actionTag) && TxtClientBank.Focused)
        {
            MessageBox.Show(@"PLEASE THE VALUE ON BRANCH OF CHEQUE ...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBankBranch.Focus();
        }
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "BOTH");
        TxtDrawnon.Text = TxtLedger.Text;
        TxtLedger.Focus();
    }

    private void MskChequeMiti_Validating(object sender, CancelEventArgs e)
    {
        MskChequeDate.Text = MskChequeMiti.MaskCompleted switch
        {
            true => ObjGlobal.ReturnEnglishDate(MskChequeMiti.Text),
            _ => MskChequeDate.Text
        };
    }

    private void MskMiti_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
        var messageBoxCs = new StringBuilder();
        messageBoxCs.AppendFormat("{0} = {1}", "Position", e.Position);
        messageBoxCs.AppendLine();
        messageBoxCs.AppendFormat("{0} = {1}", "RejectionHint", e.RejectionHint);
        messageBoxCs.AppendLine();
    }

    private void TxtBank_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnBank.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtLedger.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtLedger.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtBank.Text.Trim()) && !string.IsNullOrEmpty(_actionTag) && TxtBank.Focused)
            {
                MessageBox.Show(@"PLEASE SELECT LEDGER ..??", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtBank.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBank, BtnBank);
        }
    }

    private void BtnSubledger_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MED", "SUBLEDGER", _actionTag, ObjGlobal.SearchText, string.Empty,
            "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSubledger.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                _subLedgerId = Convert.ToInt32(frmPickList.SelectedList[0]["SubLedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SUB LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVno.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSubledger.Focus();
    }

    private void BtnAgent_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "AGENT", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtAgent.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                _agentId = Convert.ToInt32(frmPickList.SelectedList[0]["Description"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES AGENT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtAgent.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtAgent.Focus();
    }

    private void BtnBranch_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetPdcBankBranch();
        if (result.IsValueExits())
        {
            TxtBankBranch.Text = result;
        }
        TxtBankBranch.Focus();
    }

    private void BtnDrawOn_Click(object sender, EventArgs e)
    {
        const string script = "SELECT  DISTINCT pdc.DrawOn Description,0 ID  FROM AMS.PostDateCheque pdc";
        using var frmPickList = new FrmAutoPopList(script);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            TxtDrawnon.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].ToString().Trim(),
                _ => TxtDrawnon.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DRAW ON LEDGER ARE NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBankBranch.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDrawnon.Focus();
    }

    private void MskDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
        var messageBoxCS = new StringBuilder();
        messageBoxCS.AppendFormat("{0} = {1}", "Position", e.Position);
        messageBoxCS.AppendLine();
        messageBoxCS.AppendFormat("{0} = {1}", "RejectionHint", e.RejectionHint);
        messageBoxCS.AppendLine();
    }

    private void TxtBankBranch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) BtnBranch_Click(sender, e);
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtLedger.Text.Trim()) && !string.IsNullOrEmpty(_actionTag) && TxtLedger.Focused)
        {
            MessageBox.Show(@"PLEASE SELECT THE LEDGER..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtLedger.Focus();
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmGeneralLedger("CUSTOMER", true);
            frm.ShowDialog();
            TxtLedger.Text = frm.LedgerDesc;
            _ledgerId = frm.LedgerId;
            frm.Dispose();
        }
        else if (e.KeyCode is Keys.Enter or Keys.Tab)
        {
            if (string.IsNullOrEmpty(TxtLedger.Text.Trim())) BtnLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtLedger, BtnLedger);
        }
    }

    private void TxtAmount_Validated(object sender, EventArgs e)
    {
        double.TryParse(TxtAmount.Text, out var _Amount);
        if (_Amount is 0 & TxtAmount.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"PLEASE ENTER THE VOUCHER AMOUNT...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtAmount.Focus();
            return;
        }

        TxtAmount.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtAmount.Text))
            .ToString(ObjGlobal.SysAmountFormat);
    }

    private void TxtAmount_Leave(object sender, EventArgs e)
    {
        double.TryParse(TxtAmount.Text, out var _Amount);
        if (_Amount is 0 & TxtAmount.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"PLEASE ENTER THE VOUCHER AMOUNT...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtAmount.Focus();
        }
    }

    private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar is (char)Keys.Enter)
        {
            double.TryParse(TxtAmount.Text, out var _Amount);
            if (_Amount is 0 & TxtAmount.Focused && !string.IsNullOrEmpty(_actionTag))
            {
                MessageBox.Show(@"PLEASE ENTER THE VOUCHER AMOUNT...!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtAmount.Focus();
            }
        }
    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
        _ledgerId = _ledgerId switch
        {
            0 when !string.IsNullOrEmpty(TxtLedger.Text.Trim()) => _master.ReturnLongValueFromTable(
                "AMS.GeneralLedger", "GLId", "GLName", TxtLedger.Text.Trim()),
            _ => _ledgerId
        };
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        try
        {
            var FileName = string.Empty;
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PAttachment1.ImageLocation = FileName;
            var myimage = new Bitmap(FileName);
            PAttachment1.Image = myimage;
            PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment1.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (IsFileExists != string.Empty) MessageBox.Show(@"PICTURE FILE FORMAT & " + ex.Message);
        }
    }

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment1);
    }

    private void TxtRemarks_Validating(object sender, CancelEventArgs e)
    {
        if (ChkAttachment.Checked && _actionTag != "DELETE" && !string.IsNullOrEmpty(_actionTag))
        {
            if (MessageBox.Show(@"DO YOU WANT TO ATTACH FILES ON THIS VOUCHER", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.Yes)
            {
                TbControl.SelectedTab = TbAttachment;
                TbAttachment.IsOwnerFormActive();
                TbAttachment.Focus();
                BtnAttachment1.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
        else
        {
            BtnSave.Focus();
        }
    }

    private void BtnRemarks_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "NRMASTER", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            TxtRemarks.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["NRDESC"].ToString().Trim(),
                _ => TxtRemarks.Text
            };
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
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmNarrationMaster(true);
            frm.ShowDialog();
            TxtRemarks.Text = frm.NarrationMasterDetails;
            TxtRemarks.Focus();
        }
    }

    private void PAttachment1_DoubleClick(object sender, EventArgs e)
    {
    }

    #endregion ------------- Form -------------

    // METHOD FOR THIS FORM

    #region ------------- Method -------------

    internal void ReturnVoucherNumber()
    {
        var ObjMaster = new ClsMasterSetup();
        var dt = ObjMaster.IsExitsCheckDocumentNumbering("PDC");
        if (dt != null && dt.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.GetCurrentVoucherNo("PDC", _docDesc);
        }
        else if (dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("PDC", "AMS.PostDateCheque", "VoucherNo");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    internal void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.Image == null) return;
        var fileExt = Path.GetExtension(pictureBox.ImageLocation);
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png") //&& this.Tag == "SAVE")
        {
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        }
        else
        {
            var location = pictureBox.ImageLocation;
            Process.Start(location ?? string.Empty);
        }
    }

    internal void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TbControl.Enabled = !string.IsNullOrEmpty(_actionTag);

        BtnVno.Enabled = TxtVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskMiti.Enabled = MskDate.Enabled = isEnable;
        CmbType.Enabled = TxtBank.Enabled = BtnBank.Enabled = isEnable;
        TxtChequeNo.Enabled = MskChequeDate.Enabled = MskChequeMiti.Enabled = isEnable;
        TxtDrawnon.Enabled = isEnable;
        CmbStatus.Enabled = _actionTag != "SAVE" && isEnable;
        TxtClientBank.Enabled = TxtBankBranch.Enabled = isEnable;
        TxtLedger.Enabled = BtnLedger.Enabled = isEnable;
        TxtAmount.Enabled = TxtRemarks.Enabled = isEnable;
        BtnCancel.Enabled = BtnSave.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
    }

    internal void ClearControl()
    {
        Text = string.IsNullOrEmpty(_actionTag)
            ? "POST DATED CHEQUE DETAILS"
            : $"POST DATED CHEQUE DETAILS [{_actionTag}]";
        TbControl.SelectedTab = TbVoucherDetails;
        foreach (Control ctrl in TbControl.SelectedTab.Controls)
            ctrl.Text = ctrl switch
            {
                TextBox => string.Empty,
                _ => ctrl.Text
            };
        if (BtnNew.Enabled || BtnEdit.Enabled || BtnDelete.Enabled)
        {
            MskDate.Text = MskChequeDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = MskChequeMiti.Text = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"));
        }

        TxtBank.Text =
            GetConnection.GetQueryData(
                $"Select GlName From AMS.Generalledger Where GLID = {ObjGlobal.FinanceBankLedgerId} ");
        _bankLedgerId = ObjGlobal.FinanceBankLedgerId;
        if (_actionTag is "SAVE")
            TxtVno.GetCurrentVoucherNo("PDC", _docDesc);
        else
            TxtVno.Clear();

        CmbStatus.SelectedIndex = 0;
        CmbType.SelectedIndex = 0;
        BtnAgent.Enabled = TxtAgent.Enabled = ObjGlobal.FinanceAgentEnable;
        BtnDepartment.Enabled = TxtDepartment.Enabled = ObjGlobal.FinanceDepartmentEnable;
        BtnSubledger.Enabled = TxtSubledger.Enabled = ObjGlobal.FinanceSubLedgerEnable;
        PAttachment1.Image = null;
        TxtRemarks.Enabled = true;
    }

    internal int SaveProvisionCheque()
    {
        const int syncRow = 0;

        _pDCRepository.PdcMaster.VoucherNo = _actionTag.Equals("SAVE")
            ? TxtVno.GetCurrentVoucherNo("PDC", _docDesc)
            : TxtVno.Text.Trim();
        _pDCRepository.PdcMaster.VoucherDate = MskDate.GetDateTime();
        _pDCRepository.PdcMaster.VoucherMiti = MskMiti.Text.Trim();
        _pDCRepository.PdcMaster.BankLedgerId = ObjGlobal.ReturnLong(_bankLedgerId.ToString());
        _pDCRepository.PdcMaster.VoucherType = CmbType.Text.Trim();
        _pDCRepository.PdcMaster.Status = CmbStatus.Text.Trim();

        _pDCRepository.PdcMaster.BankName = TxtClientBank.Text.Trim();
        _pDCRepository.PdcMaster.BranchName = TxtBankBranch.Text.Trim();
        _pDCRepository.PdcMaster.ChequeNo = TxtChequeNo.Text.Trim();
        _pDCRepository.PdcMaster.ChqDate = MskChequeDate.GetDateTime();
        _pDCRepository.PdcMaster.ChqMiti = MskChequeMiti.Text.Trim();
        _pDCRepository.PdcMaster.DrawOn = TxtDrawnon.Text.Trim().Replace("'", "''");
        _pDCRepository.PdcMaster.Amount = TxtAmount.GetDecimal();
        _pDCRepository.PdcMaster.LedgerId = _ledgerId;
        _pDCRepository.PdcMaster.SubLedgerId = _subLedgerId;
        _pDCRepository.PdcMaster.AgentId = _agentId;
        _pDCRepository.PdcMaster.Cls1 = _departmentId;
        _pDCRepository.PdcMaster.BranchId = ObjGlobal.SysBranchId;
        _pDCRepository.PdcMaster.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
        _pDCRepository.PdcMaster.Remarks = TxtRemarks.Text.Trim();
        _pDCRepository.PdcMaster.EnterBy = ObjGlobal.LogInUser.Trim();
        _pDCRepository.PdcMaster.FiscalYearId = ObjGlobal.SysFiscalYearId;
        _pDCRepository.PdcMaster.SyncRowVersion = syncRow.ReturnSyncRowNo("PDC", TxtVno.Text);
        _pDCRepository.PdcMaster.PAttachment1 = PAttachment1?.Image.ConvertImage();
        return _pDCRepository.SaveProvisionCheque(_actionTag, TxtLedger.Text);
    }

    internal bool IsValidForm()
    {
        if (TxtVno.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVno, @"PLEASE ENTER THE VOUCHER NUMBER IN VOUCHER BOX..!!");
            return false;
        }

        if (_tagStrings.Contains(_actionTag)) return true;

        if (!MskDate.MaskCompleted)
        {
            this.NotifyValidationError(MskDate, @"PLEASE ENTER THE DATE IN DATE BOX..!!");
            return false;
        }

        if (!MskMiti.MaskCompleted)
        {
            this.NotifyValidationError(MskMiti, @"PLEASE ENTER THE MITI IN DATE BOX..!!");
            return false;
        }

        if (TxtClientBank.IsBlankOrEmpty())
        {
            MessageBox.Show(@"BANK DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBank.Focus();
            return false;
        }

        if (TxtBankBranch.IsBlankOrEmpty())
        {
            MessageBox.Show(@"BANK BRANCH IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBank.Focus();
            return false;
        }

        _bankLedgerId = _bankLedgerId switch
        {
            0 when !string.IsNullOrEmpty(TxtBank.Text.Trim()) => _master.ReturnLongValueFromTable(
                "AMS.GeneralLedger", "GLId", "GLName", TxtBank.Text.Trim()),
            _ => _bankLedgerId
        };
        _ledgerId = _ledgerId switch
        {
            0 when !string.IsNullOrEmpty(TxtLedger.Text.Trim()) => _master.ReturnLongValueFromTable(
                "AMS.GeneralLedger", "GLId", "GLName", TxtLedger.Text.Trim()),
            _ => _ledgerId
        };
        _subLedgerId = _subLedgerId switch
        {
            0 when !string.IsNullOrEmpty(TxtSubledger.Text.Trim()) => _master.ReturnIntValueFromTable(
                "AMS.SubLedger", "SLId", "SLName", TxtSubledger.Text.Trim()),
            _ => _subLedgerId
        };
        _departmentId = _departmentId switch
        {
            0 when !string.IsNullOrEmpty(TxtDepartment.Text.Trim()) => _master.ReturnIntValueFromTable(
                "AMS.Department", "DId", "DName", TxtDepartment.Text.Trim()),
            _ => _departmentId
        };
        _agentId = _agentId switch
        {
            0 when !string.IsNullOrEmpty(TxtAgent.Text.Trim()) => _master.ReturnIntValueFromTable("AMS.JuniorAgent",
                "AgentId", "AgentName", TxtAgent.Text.Trim()),
            _ => _agentId
        };
        if (string.IsNullOrEmpty(TxtDrawnon.Text))
        {
            MessageBox.Show(@"DRAWN ON NUMBER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDrawnon.Focus();
            return false;
        }

        return true;
    }

    internal void BindPdcVoucher(string voucherNo)
    {
        var dt = _pDCRepository.GetPostDatedChequeVoucher(voucherNo);
        if (dt.Rows.Count <= 0) return;
        foreach (DataRow ro in dt.Rows)
        {
            if (_actionTag != "SAVE") TxtVno.Text = ro["VoucherNo"].ToString();
            MskDate.Text = DateTime.Now.GetDateString();
            MskMiti.Text = MskMiti.GetNepaliDate(MskDate.Text);
            _bankLedgerId = ro["BankLedgerId"].GetLong();
            TxtBank.Text = ro["BankLedger"].ToString();
            TxtChequeNo.Text = ro["ChequeNo"].ToString();
            MskChequeDate.Text = ro["ChqDate"].GetDateString();
            MskChequeMiti.Text = ro["ChqMiti"].ToString();
            TxtDrawnon.Text = ro["DrawOn"].ToString();
            TxtClientBank.Text = ro["BankName"].ToString();
            TxtBankBranch.Text = ro["BranchName"].ToString();
            _ledgerId = ro["LedgerId"].GetLong();
            TxtLedger.Text = ro["LedgerDesc"].ToString();
            _departmentId = ro["Cls1"].GetInt();
            TxtDepartment.Text = ro["Department"].ToString();
            _subLedgerId = ro["SubLedgerId"].GetInt();
            TxtSubledger.Text = ro["SubLedger"].ToString();
            _agentId = ro["AgentId"].GetInt();
            TxtAgent.Text = ro["SalesMan"].ToString();
            TxtAmount.Text = ro["Amount"].GetDecimalString();
            TxtRemarks.Text = ro["Remarks"].ToString();
            CmbType.Text = ro["VoucherType"].ToString();
            PAttachment1.Image = dt.Rows[0]["PAttachment1"].GetImage();
        }
    }

    #endregion ------------- Method -------------

    // OBJECT FOR THIS FORM

    #region ------------- Global Function -------------

    private int _departmentId;
    private int _subLedgerId;
    private int _agentId;

    private long _ledgerId;
    private long _bankLedgerId;

    private bool _isZoom;

    private string _actionTag = string.Empty;
    private string _searchKey = string.Empty;
    private string _query = string.Empty;
    private string _zoomVno;

    private string _docDesc = string.Empty;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private readonly IMasterSetup _master;
    // private readonly IFinanceEntry _entry;
    private readonly IPostDateChequeRepository _pDCRepository;
    private ClsMasterForm _form;

    #endregion ------------- Global Function -------------
}