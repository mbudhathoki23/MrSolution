using DatabaseModule.CloudSync;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.SystemSetting;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmSystemSettings : MrForm
{
    // SYSTEM SETTING

    #region --------------- SYSTEM SETTING ---------------

    public FrmSystemSettings()
    {
        InitializeComponent();
        _systemSettingRepository = new SystemSettingRepository();
        LoadInitials();
        FillSystemSetting();
        ChkNightAuditApplicable_CheckedChanged(this, null);
        ChkShowPassword_CheckedChanged(this, null);
        CmbFiscalYear_SelectedIndexChanged(this, null);
        CmbCurrency_SelectedValueChanged(this, null);
    }

    private void FrmSystemSettings_Load(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ChkNightAuditApplicable.Enabled = ObjGlobal.SoftwareModule is "RESTRO" or "HOTEL";
        ChkBarcodeSearch.Enabled = ObjGlobal.SoftwareModule.Equals("POS");
        tabSetting.SelectedTab = tbSetting;
        rChkMiti.Focus();
        //if (ObjGlobal.IsOnlineSync)
        //{
        //    Task.Run(() =>
        //    {
        //        GetAndSaveUnsynchronizedSystemSetting();
        //    });
        //}
    }

    private void FrmSystemSettings_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void BtnOrderPrinter_Click(object sender, EventArgs e)
    {
        TxtOrderPrinter.Text = GetMasterList.GetPrinterList("SAVE");
    }

    private void BtnInvoiceNumbering_Click(object sender, EventArgs e)
    {
        var (description, _) = GetMasterList.GetDocumentNumberingList("SAVE", "SB");
        if (description.IsValueExits())
        {
            TxtInvoiceNumbering.Text = description;
        }
        TxtInvoiceNumbering.Focus();
    }

    private void BtnOrderDesign_Click(object sender, EventArgs e)
    {
        TxtOrderDesign.Text = GetMasterList.GetPrintingDesignList("SAVE", "SO");
    }

    private void BtnAbtInvNumbering_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDocumentNumberingList("SAVE", "AVT");
        if (description.IsValueExits())
        {
            TxtAbtInvoiceNumbering.Text = description;
        }
        TxtAbtInvoiceNumbering.Focus();
    }

    private void BtnInvoicePrinter_Click(object sender, EventArgs e)
    {
        TxtInvoicePrinter.Text = GetMasterList.GetPrinterList("SAVE");
    }

    private void BtnOrderNumbering_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDocumentNumberingList("SAVE", "SO");
        if (description.IsValueExits())
        {
            TxtOrderNumbering.Text = description;
        }
        TxtOrderNumbering.Focus();
    }

    private void BtnInvoiceDesign_Click(object sender, EventArgs e)
    {
        TxtInvoiceDesign.Text = GetMasterList.GetPrintingDesignList("SAVE", "SB");
    }

    private void BtnAbtInvoiceDesign_Click(object sender, EventArgs e)
    {
        TxtAbtInvoiceDesign.Text = GetMasterList.GetPrintingDesignList("SAVE", "SB");
    }

    private void BtnSalary_Click(object sender, EventArgs e)
    {
        (TxtSalaryLedger.Text, _salaryLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
    }

    private void BtnTDS_Click(object sender, EventArgs e)
    {
        (TxtTdsLedger.Text, _tdsLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
    }

    private void BtnPF_Click(object sender, EventArgs e)
    {
        (TxtPfLedger.Text, _pfLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
    }

    private void BtnDebtors_Click(object sender, EventArgs e)
    {
        (TxtDebtors.Text, _debtorsGroupId) = GetMasterList.GetAccountGroupList("SAVE");
    }

    private void BtnCreditors_Click(object sender, EventArgs e)
    {
        (TxtCreditors.Text, _creditorsGroupId) = GetMasterList.GetAccountGroupList("SAVE");
    }

    public void Global_EnterKey(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
        BackColor = Color.AliceBlue;
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
        BackColor = Color.Honeydew;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (SaveSystemConfiguration() != 0)
        {
            ObjGlobal.FillSystemConFiguration();
        }
    }

    private void BtnSaveClose_Click(object sender, EventArgs e)
    {
        if (SaveSystemConfiguration() == 0)
        {
            CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE SYSTEM CONFIG SAVE..!!");
            return;
        }
        else
        {
            ObjGlobal.FillSystemConFiguration();
            Close();
            return;
        }
    }

    private void TxtDOPrinter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOrderPrinter_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtOrderPrinter.IsBlankOrEmpty())
            {
                TxtOrderPrinter.WarningMessage("ORDER PRINTER SETUP IS EMPTY");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtOrderPrinter.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOrderPrinter, BtnOrderPrinter);
        }
    }

    private void TxtODNumbering_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnInvoiceNumbering_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtInvoiceNumbering.IsBlankOrEmpty())
            {
                TxtInvoiceNumbering.WarningMessage("INVOICE DOCUMENT NUMBERING SETUP IS REQUIRED");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtOrderPrinter.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtInvoiceNumbering, BtnInvoiceNumbering);
        }
    }

    private void TxtDIDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOrderDesign_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtOrderDesign.IsBlankOrEmpty())
            {
                TxtOrderDesign.WarningMessage("ORDER DESIGN IS REQUIRED TO SETUP");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtOrderPrinter.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOrderDesign, BtnOrderDesign);
        }
    }

    private void TxtDODesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAbtInvNumbering_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtAbtInvoiceNumbering.IsBlankOrEmpty())
            {
                TxtAbtInvoiceNumbering.WarningMessage("ABT SALE INVOICE NUMBERING IS REQUIRED TO SETUP");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtAbtInvoiceNumbering.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAbtInvoiceNumbering, BtnAbtInvNumbering);
        }
    }

    private void TxtDIPrinter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnInvoicePrinter_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtInvoicePrinter.Text.Trim()) && TxtInvoicePrinter.Enabled &&
                TxtInvoicePrinter.Focused)
            {
                MessageBox.Show(@"INVOICE PRINTER IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtInvoicePrinter.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtOrderPrinter.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtInvoicePrinter, BtnInvoicePrinter);
        }
    }

    private void TxtIDNumbering_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOrderNumbering_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtOrderNumbering.Text.Trim()) && TxtOrderNumbering.Enabled &&
                TxtOrderNumbering.Focused)
            {
                MessageBox.Show(@"INVOICE DOC NUMBERING IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtOrderNumbering.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtOrderNumbering.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtOrderNumbering, BtnOrderNumbering);
        }
    }

    private void TxtAPDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnInvoiceDesign_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtInvoiceDesign.Text.Trim()) && TxtInvoiceDesign.Enabled &&
                TxtInvoiceDesign.Focused)
            {
                MessageBox.Show(@"ABT PRINT DESIGN IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtInvoiceDesign.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtInvoiceDesign.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtInvoiceDesign, BtnInvoiceDesign);
        }
    }

    private void TxtRIDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAbtInvoiceDesign_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtAbtInvoiceDesign.IsBlankOrEmpty() && TxtAbtInvoiceDesign.Enabled && TxtAbtInvoiceDesign.Focused)
            {
                TxtAbtInvoiceDesign.WarningMessage(@"ABT PRINT DESIGN IS BLANK ..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtAbtInvoiceDesign.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAbtInvoiceDesign, BtnAbtInvoiceDesign);
        }
    }

    private void TxtSalary_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalary_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtSalaryLedger.IsBlankOrEmpty() && TxtSalaryLedger.Enabled && TxtSalaryLedger.Focused)
            {
                TxtSalaryLedger.WarningMessage("SALARY IS BLANK ..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtSalaryLedger.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalaryLedger, BtnSalary);
        }
    }

    private void TxtTDS_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnTDS_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtTdsLedger.IsBlankOrEmpty() && TxtTdsLedger.Enabled && TxtTdsLedger.Focused)
            {
                TxtTdsLedger.WarningMessage("TDS IS BLANK ..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtTdsLedger.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtTdsLedger, BtnTDS);
        }
    }

    private void TxtPF_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPF_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtPfLedger.IsBlankOrEmpty() && TxtPfLedger.Enabled && TxtPfLedger.Focused)
            {
                TxtPfLedger.WarningMessage("PF LEDGER IS REQUIRED..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtPfLedger.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPfLedger, BtnPF);
        }
    }

    private void TxtDebtors_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDebtors_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDebtors.IsBlankOrEmpty() && TxtDebtors.Enabled && TxtDebtors.Focused)
            {
                TxtDebtors.WarningMessage("DEBTORS ACCOUNT GROUP IS BLANK ..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtDebtors.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDebtors, BtnDebtors);
        }
    }

    private void TxtCreditors_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCreditors_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCreditors.IsBlankOrEmpty() && TxtCreditors.Enabled && TxtCreditors.Focused)
            {
                TxtCreditors.WarningMessage("CREDITORS ACCOUNT GROUP IS BLANK ..!!");
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtCreditors.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCreditors, BtnCreditors);
        }
    }

    private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
    {
        TxtEmailPassword.UseSystemPasswordChar = !ChkShowPassword.Checked;
    }

    private void BtnSavingPath_Click(object sender, EventArgs e)
    {
        if (SaveLocation.ShowDialog() == DialogResult.OK)
        {
            TxtBackupLocation.Text = SaveLocation.SelectedPath;
        }
    }

    private void CmbFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbFiscalYear.SelectedValue != null)
        {
            _fiscalYearId = CmbFiscalYear.SelectedValue.GetHashCode();
        }
    }

    private void CmbCurrency_SelectedValueChanged(object sender, EventArgs e)
    {
        _currencyId = CmbCurrency.SelectedValue.GetInt();
    }

    private void ChkNightAuditApplicable_CheckedChanged(object sender, EventArgs e)
    {
        MskApplicableTime.Enabled = ChkNightAuditApplicable.Checked;
        if (ChkNightAuditApplicable.Enabled)
        {
            return;
        }
        ChkNightAuditApplicable.Checked = false;
        MskApplicableTime.Clear();
    }

    #endregion --------------- SYSTEM SETTING ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void FillSystemSetting()
    {
        var dtSystemSetting = _setup.GetSystemSetting();
        if (dtSystemSetting.RowsCount() is 0)
        {
            return;
        }
        foreach (DataRow dr in dtSystemSetting.Rows)
        {
            rChkDate.Checked = dr["EnglishDate"].GetBool();
            rChkMiti.Checked = !dr["EnglishDate"].GetBool();
            ChkAuditTrial.Checked = dr["AuditTrial"].GetBool();
            ChkEnableUdf.Checked = dr["Udf"].GetBool();
            ChkAutoPopList.Checked = dr["Autopoplist"].GetBool();
            ChkCurrentDate.Checked = dr["CurrentDate"].GetBool();
            ChkConfirmSave.Checked = dr["ConformSave"].GetBool();
            ChkConfirmCancel.Checked = dr["ConformCancel"].GetBool();
            ChkConfirmExits.Checked = dr["ConformExits"].GetBool();
            _currencyId = dr["CurrencyId"].GetInt();
            CmbCurrency.SelectedValue = _currencyId;
            CmbAmountFormat.Text = dr["AmountFormat"].GetString();
            CmbRateFormat.Text = dr["RateFormat"].GetString();
            CmbQtyFormat.Text = dr["QtyFormat"].GetString();
            CmbCurrencyFormat.Text = dr["CurrencyFormatF"].GetString();
            _fiscalYearId = dr["DefaultFiscalYearId"].GetInt();
            CmbFiscalYear.SelectedValue = _fiscalYearId;
            TxtOrderPrinter.Text = dr["DefaultOrderPrinter"].GetString();
            TxtInvoicePrinter.Text = dr["DefaultInvoicePrinter"].GetString();
            TxtOrderNumbering.Text = dr["DefaultOrderNumbering"].GetString();
            TxtInvoiceNumbering.Text = dr["DefaultInvoiceNumbering"].GetString();
            TxtAbtInvoiceNumbering.Text = dr["DefaultAvtInvoiceNumbering"].GetString();
            TxtOrderDesign.Text = dr["DefaultOrderDesign"].GetString();
            ChkOrderPrint.Checked = dr["IsOrderPrint"].GetBool();
            TxtInvoiceDesign.Text = dr["DefaultInvoiceDesign"].GetString();
            ChkInvPrint.Checked = dr["IsInvoicePrint"].GetBool();
            TxtAbtInvoiceDesign.Text = dr["DefaultAvtDesign"].GetString();
            CmbFontName.Text = dr["DefaultFontsName"].GetString();
            CmbFontSize.SelectedValue = dr["DefaultFontsSize"].GetInt();
            CmbPaperSize.Text = dr["DefaultPaperSize"].GetString();
            CmbReportFontStyle.Text = dr["DefaultReportStyle"].GetString();
            ChkPrintDateTime.Checked = dr["DefaultPrintDateTime"].GetBool();
            CmbFormColor.Text = dr["DefaultFormColor"].GetString();
            CmbTextColor.Text = dr["DefaultTextColor"].GetString();
            _debtorsGroupId = dr["DebtorsGroupId"].GetInt();
            TxtDebtors.Text = dr["DebtorsGroup"].GetString();
            _creditorsGroupId = dr["CreditorGroupId"].GetInt();
            TxtCreditors.Text = dr["CreditorGroup"].GetString();
            _salaryLedgerId = dr["SalaryLedgerId"].GetLong();
            TxtSalaryLedger.Text = dr["SalaryLedger"].GetString();
            _tdsLedgerId = dr["TDSLedgerId"].GetLong();
            TxtTdsLedger.Text = dr["TDSLedger"].GetString();
            _pfLedgerId = dr["PFLedgerId"].GetLong();
            TxtPfLedger.Text = dr["PFLedger"].GetString();
            TxtEmailId.Text = dr["DefaultEmail"].GetString();
            TxtEmailPassword.Text = dr["DefaultEmailPassword"].GetString();
            TxtBackupLocation.Text = dr["BackupLocation"].GetString();
            TxtBackupSchIntervaldays.Text = dr["BackupDays"].GetString();
            ChkNightAuditApplicable.Checked = dr["IsNightAudit"].GetBool();
            MskApplicableTime.Text = dr["EndTime"].ToString();
            ChkSearchAlpha.Checked = dr["SearchAlpha"].GetBool();
            ChkBarcodeSearch.Checked = dr["BarcodeAutoSearch"].GetBool();
        }
    }

    private void LoadInitials()
    {
        BindCurrency();
        ObjGlobal.BindFiscalYear(CmbFiscalYear);
        CmbFiscalYear.SelectedIndex = 0;

        ObjGlobal.BindPrinter(CmbDefaultPrinter);
        CmbDefaultPrinter.SelectedIndex = 0;

        BindAmountItem();
        CmbAmountFormat.SelectedIndex = 2;

        BindRateItem();
        CmbRateFormat.SelectedIndex = 2;

        BindCurrencyRateItem();
        CmbCurrencyFormat.SelectedIndex = 2;

        BindQtyItem();
        CmbQtyFormat.SelectedIndex = 2;

        BindFont();
        BindFontColor();
        BindFontSize();
        BindPaperSize();
        BindReportFontStyleItem();
    }

    private void BindCurrency()
    {
        var currency = _pickList.GetCurrency("SAVE");
        if (currency.Rows.Count <= 0)
        {
            return;
        }
        CmbCurrency.DataSource = currency;
        CmbCurrency.DisplayMember = "ShortName";
        CmbCurrency.ValueMember = "LedgerId";
        CmbCurrency.SelectedIndex = 0;
    }

    private void BindQtyItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbQtyFormat.DataSource = items;
    }

    private void BindCurrencyRateItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbCurrencyFormat.DataSource = items;
    }

    private void BindRateItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbRateFormat.DataSource = items;
    }

    private void BindAmountItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbAmountFormat.DataSource = items;
    }

    private void BindReportFontStyleItem()
    {
        var list = new List<string>
        {
            "Upper",
            "Lower",
            "Normal", //First Letter
            "None"
        };
        CmbReportFontStyle.DataSource = list;
        CmbReportFontStyle.SelectedIndex = 0;
    }

    private void BindPaperSize()
    {
        var items = new List<string>
        {
            "A4 Full",
            "A4 Full LandScape",
            "Letter Full"
        };
        CmbPaperSize.DataSource = items;
        CmbPaperSize.SelectedIndex = 0;
    }

    private void BindFontSize()
    {
        for (var i = 6; i <= 20; i++)
        {
            CmbFontSize.Items.Add(i);
        }
        CmbFontSize.SelectedIndex = 11;
    }

    private void BindFontColor()
    {
        var colors = Enum.GetNames(typeof(KnownColor));
        CmbFormColor.DataSource = colors;
        CmbTextColor.DataSource = colors;
        CmbFormColor.SelectedIndex = 0;
        CmbTextColor.SelectedIndex = 0;
    }

    private void BindFont()
    {
        var autoComStr = new AutoCompleteStringCollection();
        foreach (var font in FontFamily.Families)
        {
            CmbFontName.Items.Add(font.Name);
            autoComStr.Add(font.Name);
        }
        CmbFontName.SelectedIndex = 0;
    }

    private int SaveSystemConfiguration()
    {
        try
        {
            _systemSettingRepository.VmSystem.SyId = 1;
            _systemSettingRepository.VmSystem.EnglishDate = rChkDate.Checked;
            _systemSettingRepository.VmSystem.AuditTrial = ChkAuditTrial.Checked;
            _systemSettingRepository.VmSystem.Udf = ChkEnableUdf.Checked;
            _systemSettingRepository.VmSystem.Autopoplist = ChkAutoPopList.Checked;
            _systemSettingRepository.VmSystem.CurrentDate = ChkCurrentDate.Checked;
            _systemSettingRepository.VmSystem.ConformSave = ChkConfirmSave.Checked;
            _systemSettingRepository.VmSystem.ConformCancel = ChkConfirmCancel.Checked;
            _systemSettingRepository.VmSystem.ConformExits = ChkConfirmExits.Checked;
            _systemSettingRepository.VmSystem.CurrencyRate = 1;
            _systemSettingRepository.VmSystem.CurrencyId = _currencyId;
            _systemSettingRepository.VmSystem.DefaultPrinter = TxtInvoicePrinter.Text;
            _systemSettingRepository.VmSystem.AmountFormat = CmbAmountFormat.Text;
            _systemSettingRepository.VmSystem.RateFormat = CmbRateFormat.Text;
            _systemSettingRepository.VmSystem.QtyFormat = CmbQtyFormat.Text;
            _systemSettingRepository.VmSystem.CurrencyFormatF = CmbCurrencyFormat.Text;
            _systemSettingRepository.VmSystem.DefaultFiscalYearId = _fiscalYearId;
            _systemSettingRepository.VmSystem.DefaultOrderPrinter = TxtOrderPrinter.Text;
            _systemSettingRepository.VmSystem.DefaultInvoicePrinter = TxtInvoicePrinter.Text;
            _systemSettingRepository.VmSystem.DefaultOrderNumbering = TxtOrderNumbering.Text;
            _systemSettingRepository.VmSystem.DefaultInvoiceNumbering = TxtInvoiceNumbering.Text;
            _systemSettingRepository.VmSystem.DefaultAvtInvoiceNumbering = TxtAbtInvoiceNumbering.Text;
            _systemSettingRepository.VmSystem.DefaultOrderDesign = TxtOrderDesign.Text;
            _systemSettingRepository.VmSystem.IsOrderPrint = ChkOrderPrint.Checked;
            _systemSettingRepository.VmSystem.IsOrderPrint = ChkOrderPrint.Checked;
            _systemSettingRepository.VmSystem.DefaultInvoiceDesign = TxtInvoiceDesign.Text;
            _systemSettingRepository.VmSystem.IsInvoicePrint = ChkInvPrint.Checked;
            _systemSettingRepository.VmSystem.DefaultAvtDesign = TxtAbtInvoiceDesign.Text;
            _systemSettingRepository.VmSystem.DefaultFontsName = CmbFontName.Text;
            _systemSettingRepository.VmSystem.DefaultFontsSize = CmbFontSize.Text.GetInt();
            _systemSettingRepository.VmSystem.DefaultPrintDateTime = ChkPrintDateTime.Checked;
            _systemSettingRepository.VmSystem.DefaultFormColor = CmbFormColor.Text;
            _systemSettingRepository.VmSystem.DefaultTextColor = CmbTextColor.Text;
            _systemSettingRepository.VmSystem.DebtorsGroupId = _debtorsGroupId;
            _systemSettingRepository.VmSystem.CreditorGroupId = _creditorsGroupId;
            _systemSettingRepository.VmSystem.SalaryLedgerId = _salaryLedgerId;
            _systemSettingRepository.VmSystem.TDSLedgerId = _tdsLedgerId;
            _systemSettingRepository.VmSystem.PFLedgerId = _pfLedgerId;
            _systemSettingRepository.VmSystem.DefaultEmail = TxtEmailId.Text;
            _systemSettingRepository.VmSystem.DefaultEmailPassword = TxtEmailPassword.Text;
            _systemSettingRepository.VmSystem.BackupDays = TxtBackupSchIntervaldays.GetInt();
            _systemSettingRepository.VmSystem.BackupLocation = TxtBackupLocation.Text;

            _systemSettingRepository.VmSystem.IsPrintBranch = ChkPrintBranch.Checked;
            _systemSettingRepository.VmSystem.IsNightAudit = ChkNightAuditApplicable.Checked;
            if (_systemSettingRepository.VmSystem.IsNightAudit.Value)
            {
                _systemSettingRepository.VmSystem.EndTime = TimeSpan.Parse(MskApplicableTime.Text);
            }
            _systemSettingRepository.VmSystem.SearchAlpha = ChkSearchAlpha.Checked;
            _systemSettingRepository.VmSystem.BarcodeAutoSearch = ChkBarcodeSearch.Checked;
            return _systemSettingRepository.SaveSystemSetting("");
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return 0;
        }
    }
    //private async void GetAndSaveUnsynchronizedSystemSetting()
    //{
    //    try
    //    {
    //        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
    //        if (!_configParams.Success || _configParams.Model.Item2 == null)
    //        {
    //            return;
    //        }
    //        var apiConfig = new SyncApiConfig
    //        {
    //            BaseUrl = _configParams.Model.Item2,
    //            Apikey = _configParams.Model.Item3,
    //            Username = ObjGlobal.LogInUser,
    //            BranchId = ObjGlobal.SysBranchId,
    //            GetUrl = @$"{_configParams.Model.Item2}SystemSetting/GetSystemSettingByCallCount",
    //            InsertUrl = @$"{_configParams.Model.Item2}SystemSetting/InsertSystemSettingList",
    //            UpdateUrl = @$"{_configParams.Model.Item2}SystemSetting/UpdateSystemSetting"
    //        };

    //        DataSyncHelper.SetConfig(apiConfig);
    //        _injectData.ApiConfig = apiConfig;
    //        DataSyncManager.SetGlobalInjectData(_injectData);
    //        var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.SystemSetting>(_injectData);

    //        SplashScreenManager.ShowForm(typeof(PleaseWait));
    //        //pull all new table master data
    //        var pullResponse = await _systemSettingRepository.PullSystemSettingServerToClientByRowCounts(settingRepo, 1);
    //        SplashScreenManager.CloseForm();
    //        // push all new table master data
    //        var sqlCrQuery = _systemSettingRepository.GetSystemSettingScript();
    //        var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.SystemSetting>(sqlCrQuery);
    //        var curList = queryResponse.List.ToList();
    //        if (curList.Count > 0)
    //        {
    //            SplashScreenManager.ShowForm(typeof(PleaseWait));
    //            var pushResponse = await settingRepo.PushNewListAsync(curList);
    //            SplashScreenManager.CloseForm();
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        throw new Exception(e.Message);
    //    }
    //}
    #endregion --------------- METHOD ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private string _actionTag;
    private int _fiscalYearId;
    private long _salaryLedgerId;
    private long _tdsLedgerId;
    private long _pfLedgerId;
    private int _debtorsGroupId;
    private int _creditorsGroupId;
    private int _currencyId;
    private readonly ISetup _setup = new ClsSetup();
    private ClsPickList _pickList = new ClsPickList();
    private readonly ISystemSettingRepository _systemSettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- OBJECT FOR THIS FORM ---------------
}