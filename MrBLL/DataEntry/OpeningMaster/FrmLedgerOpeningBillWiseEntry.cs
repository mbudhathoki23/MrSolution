using DatabaseModule.DataEntry.OpeningMaster;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace MrBLL.DataEntry.OpeningMaster;

public partial class FrmLedgerOpeningBillWiseEntry : MrForm
{
    private void CbModule_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void GTxtSearch_TextChanged(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        try
        {
            ((DataTable)DGrid.DataSource).DefaultView.RowFilter = $"CompanyName LIKE '{GTxtSearch.Text.Trim()}%'";
            if (DGrid.Rows.Count == 0 && GTxtSearch.Text == string.Empty) LoadLedgerDetails();
        }
        catch
        {
            // ignored
        }
    }

    private void DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void DGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        DGrid.Rows[rowIndex].Selected = true;
        DGrid.Enabled = false;
        LblLedgerDesc.Text = DGrid.Rows[rowIndex].Cells[2].Value.ToString();
        long.TryParse(DGrid.Rows[rowIndex].Cells[1].Value.ToString(), out var _LedgerId);
        LedgerId = _LedgerId;
        EnableControl(true, false);
        BtnNew.Focus();
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        // if (MskMiti.Text.Trim() != "/  /" && MskMiti.Enabled)
        // {
        //     if (ObjGlobal.ValidDate(MskMiti.Text, "M") && MskMiti.Enabled)
        //     {
        //         if (ObjGlobal.ValidDateRange(Convert.ToDateTime(ObjGlobal.ReturnEnglishDate(MskMiti.Text))) ==
        //             false)
        //         {
        //             e.Cancel = true;
        //             MessageBox.Show(
        //                 "DATE MUST BE LESS THEN  " +
        //                 ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.AddDays(-1).ToShortDateString()) +
        //                 " OR EQUAL TO " +
        //                 ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.AddDays(-1).ToShortDateString()) + " ",
        //                 ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //             MskMiti.Focus();
        //         }
        //     }
        //     else
        //     {
        //         e.Cancel = true;
        //         MessageBox.Show(@"PLZ. ENTER VALID DATE !", ObjGlobal.Caption, MessageBoxButtons.OK,
        //             MessageBoxIcon.Warning);
        //         MskMiti.Focus();
        //     }
        // }
        // else if (MskMiti.Text.Trim() == "/  /" && MskMiti.Enabled)
        // {
        //     e.Cancel = true;
        //     MessageBox.Show(@"OPENING MITI CANN'T BE LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Warning);
        //     MskMiti.Focus();
        // }
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        // if (MskDate.Text.Trim() != "/  /" && MskDate.Enabled)
        // {
        //     if (ObjGlobal.ValidDate(MskDate.Text, "D") && MskDate.Enabled)
        //     {
        //         if (ObjGlobal.ValidDateRange(Convert.ToDateTime(MskDate.Text)) == false && MskDate.Enabled)
        //         {
        //             e.Cancel = true;
        //             MessageBox.Show(
        //                 $@"DATE MUST BE LESS THEN {ObjGlobal.CfStartAdDate.AddDays(-1)} OR EQUAL  {ObjGlobal.CfStartAdDate.AddDays(-1)} ",
        //                 ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //             MskDate.Focus();
        //         }
        //     }
        //     else
        //     {
        //         e.Cancel = true;
        //         MessageBox.Show(@"PLZ. ENTER VALID DATE !", ObjGlobal.Caption, MessageBoxButtons.OK,
        //             MessageBoxIcon.Warning);
        //         MskDate.Focus();
        //     }
        // }
        // else if (!MskDate.MaskCompleted && MskDate.Enabled)
        // {
        //     e.Cancel = true;
        //     MessageBox.Show(@"OPENING DATE CANN'T BE LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Warning);
        //     MskDate.Focus();
        // }
    }

    private void DGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void TxtNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            using var frm = new FrmNarrationMaster(false);
            frm.ShowDialog();
            TxtNarration.Text = frm.NarrationMasterDetails;
            frm.Dispose();
            TxtNarration.Focus();
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "DEPARTMENT", _actionTag, ObjGlobal.SearchText, string.Empty,
            "TRANSACTION");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDepartment.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                DepartmentId = Convert.ToInt32(frmPickList.SelectedList[0]["Description"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DEPARTMENT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVno.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDepartment.Focus();
    }

    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        var (description, id, currencyRate) = GetMasterList.GetCurrencyList(_actionTag);
        if (description.IsValueExits())
        {
            TxtCurrency.Text = description;
        }
        TxtCurrency.Focus();

        // var frmPickList = new FrmAutoPopList("MIN", "CURRENCY", _actionTag, ObjGlobal.SearchText, "NORMAL", "LIST");
        // if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        // {
        //     frmPickList.ShowDialog();
        //     if (frmPickList.SelectedList.Count > 0)
        //     {
        //         TxtCurrency.Text = frmPickList.SelectedList[0]["CName"].ToString().Trim();
        //         CurrencyId = Convert.ToInt16(frmPickList.SelectedList[0]["CId"].ToString().Trim());
        //         TxtCurrencyRate.Text = ObjGlobal
        //             .ReturnDouble(frmPickList.SelectedList[0]["CRate"].ToString().Trim())
        //             .ToString(ObjGlobal.SysAmountFormat);
        //     }
        //
        //     frmPickList.Dispose();
        // }
        // else
        // {
        //     MessageBox.Show(@"CURRENCY NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Information);
        //     TxtCurrency.Focus();
        //     return;
        // }
        //
        // ObjGlobal.SearchText = string.Empty;
        // TxtCurrency.Focus();
    }

    private void Btn_Narration_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "NRMASTER", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtNarration.Text = frmPickList.SelectedList[0]["NRDESC"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CODULDN'T FIND ANY NARRATION OR REMARKS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtNarration.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtNarration.Focus();
    }

    #region --------------- Global Variable ---------------

    private int CurrencyId;
    private int DepartmentId;
    private int SubledgerId;
    private int AgentId;
    private int rowIndex;
    private int currentColumn;
    private long LedgerId;
    private int _openingId;

    private bool Date_Change;
    private string Query = string.Empty;
    private string CurCode = string.Empty;
    private string _actionTag = string.Empty;
    public string SearchKeys = string.Empty;
    public string DocDesc = string.Empty;

    private TimeSpan ts;
    private bool btnv = false;
    private Button btnudf = new();
    public DataTable dtPUdf = new("Temp");
    private DataTable dt = new();
    private DataTable dtMain = new();
    private DataTable dtDetl = new("Temp");
    private DataTable dtTemp = new(string.Empty);
    private DataTable dtLedger = new();
    private DataTable dtvalidate = new();
    private DataTable dtMUdf = new("Temp");
    private DataTable dtUdf = new("Temp");
    private DataSet ds = new();
    private readonly Label lblHintsDisplay = new();
    private ObjGlobal Gobj = new();
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
    private StringWriter sw = new();
    private readonly IFinanceEntry ObjEntry = new ClsFinanceEntry();
    private readonly ILedgerOpeningRepository _ledgerOpening = new LedgerOpeningRepository();

    #endregion --------------- Global Variable ---------------

    #region ---------- Form ----------

    public FrmLedgerOpeningBillWiseEntry(bool Zoom, string TxtZoomVno)
    {
        InitializeComponent();
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void BillWiseLedgerOpening_Load(object sender, EventArgs e)
    {
        MskDate.Text = ObjGlobal.CfStartAdDate.AddDays(-1).ToString("dd/MM/yyyy");
        MskMiti.Text = ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.AddDays(-1).ToString("dd/MM/yyyy"));
        ClearControl();
        EnableControl(false, false);
        DGrid.Enabled = true;
        BindStations();
        CmbModule.SelectedIndex = 0;
        DGrid.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void BillWiseLedgerOpening_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl(true, false);
                    BtnNew.Focus();
                }
            }
            else if (DGrid.Enabled == false)
            {
                DGrid.Enabled = true;
                DGrid.Focus();
            }
            else
            {
                btnExit.PerformClick();
            }
        }
    }

    #endregion ---------- Form ----------

    #region ---------- Grid ----------

    private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
        if (!string.IsNullOrEmpty(LblLedgerDesc.Text.Trim())) BtnNew.Focus();
    }

    private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
        if (!string.IsNullOrEmpty(LblLedgerDesc.Text.Trim())) BtnNew.Focus();
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
        {
            DGrid.Rows[rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            DGrid.Enabled = false;
            LblLedgerDesc.Text = DGrid.Rows[rowIndex].Cells[2].Value.ToString();
            long.TryParse(DGrid.Rows[rowIndex].Cells[1].Value.ToString(), out var _LedgerId);
            LedgerId = _LedgerId;
            // LedgerId = SubledgerId;
            EnableControl(true, false);
            BtnNew.Focus();
        }
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
        var columnIndex = DGrid.Columns["GTxtSubLedger"].Index;
        TxtSubledger.TabIndex = columnIndex;
    }

    #endregion ---------- Grid ----------

    #region --------------- Method ---------------

    private void EnableControl(bool Btn, bool Txt)
    {
        BtnNew.Enabled = BtnDelete.Enabled = BtnEdit.Enabled = Btn;

        DGrid.ReadOnly = true;
        CmbModule.Enabled = Txt;
        TxtVno.Enabled = Txt;
        BtnVNo.Enabled = Txt;
        MskDate.Enabled = Txt;
        MskMiti.Enabled = Txt;
        TxtDebitAmount.Enabled = Txt;
        TxtCreditAmount.Enabled = Txt;
        TxtNarration.Enabled = Txt;

        BtnCurrency.Enabled = TxtCurrency.Enabled = Txt;
        TxtCurrencyRate.Enabled = Txt;
        BtnSave.Enabled = Txt;
        BtnCancel.Enabled = Txt;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? "LEDGER OPENING DETAILS [NEW]" : "LEDGER OPENING DETAILS";
        TxtVno.Clear();
        LoadLedgerDetails();

        //Query = "Select * From AMS.Currency";
        //CmbCurrency.DataSource = GetConnection.SelectDataTableQuery(Query);
        //CmbCurrency.DisplayMember = "CCode";
        //CmbCurrency.ValueMember = "CId";
        CurrencyId = ObjGlobal.SysCurrencyId;
        TxtCurrency.Text = ObjGlobal.SysCurrency;

        TxtVno.Text = CmbModule.SelectedIndex == 0 ? TxtVno.GetCurrentVoucherNo("COA", DocDesc) : TxtVno.Text;
        TxtDebitAmount.Clear();
        TxtCreditAmount.Clear();
        SubledgerId = 0;
        AgentId = 0;
        CurrencyId = 0;

        TxtSubledger.Clear();
        TxtDepartment.Clear();
        TxtAgent.Clear();
        TxtDebitAmount.Clear();
        TxtCreditAmount.Clear();
        TxtNarration.Clear();

        TxtSubledger.Enabled = BtnSubledger.Enabled = ObjGlobal.FinanceSubLedgerEnable;
        TxtAgent.Enabled = BtnAgent.Enabled = ObjGlobal.FinanceAgentEnable;
        BtnCurrency.Enabled = TxtCurrency.Enabled = ObjGlobal.FinanceCurrencyEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = ObjGlobal.FinanceDepartmentEnable;
        LblLedgerDesc.Text = string.Empty;
    }

    private void LoadLedgerDetails()
    {
        DGrid.Rows.Clear();
        dt = ObjEntry.GetOpeningLedgerWithBalance(ObjGlobal.CfStartAdDate.AddDays(-1).ToString("yyyy-MM-dd"));
        if (dt.Rows.Count <= 0) return;
        var IRow = 0;
        foreach (DataRow ro in dt.Rows)
        {
            DGrid.Rows.Add();
            DGrid.Rows[IRow].Cells[0].Value = IRow + 1;
            DGrid.Rows[IRow].Cells[1].Value = ro["LedgerId"].ToString();
            DGrid.Rows[IRow].Cells[2].Value = ro["Description"].ToString();
            DGrid.Rows[IRow].Cells[3].Value = ro["Balance"].ToString();
            DGrid.Rows[IRow].Cells[4].Value = ro["AmtType"].ToString();
            IRow++;
        }
    }

    private void BindStations()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("NORMAL","OB"),
            new("PURCHASE INVOICE","OPB"),
            new("SALES INVOICE","OSB"),
            new("CREDIT NOTES","OCN"),
            new("DEBIT NOTES","ODN")
        };
        CmbModule.DataSource = list;
        CmbModule.DisplayMember = "Item1";
        CmbModule.ValueMember = "Item2";
        CmbModule.SelectedIndex = 0;
    }

    private int SaveOpeningDetails()
    {
        try
        {
            _ledgerOpening.GetOpening.Details = [];
            var sync = _ledgerOpening.ReturnSyncRowVersionVoucher("OB", TxtVno.Text);
            var details = new LedgerOpening()
            {
                Opening_Id = _openingId,
                Module = "OB",
                Serial_No = rowIndex,
                Voucher_No = TxtVno.Text.Trim(),
                OP_Date = MskDate.Text.GetDateTime(),
                OP_Miti = MskMiti.Text.Trim(),
                Ledger_Id = LedgerId,
                Subledger_Id = SubledgerId,
                Agent_Id = AgentId,
                Cls1 = DepartmentId,
                Cls2 = 0,
                Cls3 = 0,
                Cls4 = 0,
                Currency_Id = CurrencyId,
                Currency_Rate = TxtCurrencyRate.Text.Trim().GetDouble() is 0 ? 1 : 0,
                Debit = TxtDebitAmount.GetDecimal(),
                LocalDebit = LblLocalDebitAmount.GetDecimal(),
                Credit = TxtCreditAmount.GetDecimal(),
                LocalCredit = LblLocalCreditAmount.GetDecimal(),
                Narration = TxtNarration.Text.Trim(),
                Remarks = "",
                Enter_By = ObjGlobal.LogInUser,
                Enter_Date = DateTime.Now,
                Branch_Id = ObjGlobal.SysBranchId,
                Company_Id = ObjGlobal.SysCompanyUnitId,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                IsReverse = false,
                CancelRemarks = "",
                CancelDate = DateTime.Now,
                SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                    ? ObjGlobal.LocalOriginId.Value
                    : Guid.Empty,
                SyncCreatedOn = DateTime.Now,
                SyncLastPatchedOn = DateTime.Now,
                SyncRowVersion = sync
            };

            _ledgerOpening.GetOpening.Details.Add(details);


            //_ledgerOpening.GetOpening.Voucher_No = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("COA", DocDesc) : TxtVno.Text.Trim();
            //_ledgerOpening.GetOpening.Voucher_No = TxtVno.Text.Trim();
            //_ledgerOpening.GetOpening.Module = CmbModule.SelectedValue.ToString();
            //_ledgerOpening.GetOpening.OP_Date = MskDate.Text.GetDateTime();
            //_ledgerOpening.GetOpening.OP_Miti = MskMiti.Text.Trim();
            //_ledgerOpening.GetOpening.Ledger_Id = LedgerId;
            //_ledgerOpening.GetOpening.Subledger_Id = SubledgerId;
            //_ledgerOpening.GetOpening.Agent_Id = AgentId;
            //_ledgerOpening.GetOpening.Cls1 = DepartmentId;
            //_ledgerOpening.GetOpening.Currency_Id = CurrencyId;

            //if (CurrencyId is 0)
            //{
            //    CurrencyId = ObjGlobal.SysCurrencyId;
            //}
            //if (TxtCurrencyRate.Text.Trim().GetDouble() is 0)
            //{
            //    _ledgerOpening.GetOpening.Currency_Rate = 1;
            //}

            //_ledgerOpening.GetOpening.Currency_Rate = TxtCurrencyRate.Text.GetDecimal();
            //_ledgerOpening.GetOpening.Debit = TxtDebitAmount.GetDecimal();
            //_ledgerOpening.GetOpening.LocalDebit = LblLocalDebitAmount.GetDecimal();
            //_ledgerOpening.GetOpening.Credit = TxtCreditAmount.GetDecimal();
            //_ledgerOpening.GetOpening.LocalCredit = LblLocalCreditAmount.GetDecimal();

            //_ledgerOpening.GetOpening.Branch_Id = ObjGlobal.SysBranchId.ToString().GetInt();
            //_ledgerOpening.GetOpening.Company_Id = ObjGlobal.SysCompanyUnitId.ToString().GetInt();
            //_ledgerOpening.GetOpening.FiscalYearId = ObjGlobal.SysFiscalYearId.ToString().GetInt();
            //_ledgerOpening.GetOpening.GetView = DGrid;

            return _ledgerOpening.SaveLedgerOpening("SAVE");
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    #endregion --------------- Method ---------------

    #region ---------- Botton ----------

    private void btnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(false, true);
        CmbModule.Focus();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(false, true);
        CmbModule.Focus();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl(false, true);
        CmbModule.Focus();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        if (TxtVno.Text != string.Empty || TxtVno.Enabled == false)
        {
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) == DialogResult.Yes) Close();
        }
        else
        {
            ClearControl();
        }
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(TxtVno.Text.Trim()))
        {
            MessageBox.Show(@"Opening Voucher No. Cannot Left Blank !", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVno.Focus();
            return false;
        }

        if (ObjGlobal.FinanceAgentMandatory)
            if (!string.IsNullOrEmpty(TxtAgent.Text.Trim()))
            {
                MessageBox.Show(@"Agent is Mandatory !", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtAgent.Focus();
                return false;
            }

        if (ObjGlobal.FinanceSubLedgerMandatory)
            if (!string.IsNullOrEmpty(TxtSubledger.Text.Trim()))
            {
                MessageBox.Show(@"SubLedger is Mandatory !", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtSubledger.Focus();
                return false;
            }

        if (ObjGlobal.FinanceCurrencyMandatory)
            if (TxtCurrency.Text.Trim() == string.Empty)
            {
                MessageBox.Show(@"Currency is Mandatory !", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtCurrency.Focus();
                return false;
            }

        if (ObjGlobal.FinanceDepartmentMandatory)
            if (string.IsNullOrEmpty(TxtDepartment.Text.Trim()))
            {
                MessageBox.Show(@"DEPARTMENT IS MANDATORY ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDepartment.Focus();
                return false;
            }

        return true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidForm())
        {
            BtnSave.Enabled = false;
            if (SaveOpeningDetails() > 0)
            {
                ObjEntry.GetOpening.Voucher_No = TxtVno.Text.Trim();
                ObjEntry.LedgerOpeningAccountPosting();
                MessageBox.Show($@"{TxtVno.Text} LEDGER OPENING VOUCHER NUMBER {_actionTag} SUCCESSFULLY..!!",
                    ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                BtnSave.Enabled = true;
                EnableControl(false, false);
                LedgerId = 0;

                DGrid.Enabled = true;
                DGrid.Focus();
            }
            else
            {
                MessageBox.Show(@"ERROR OCCURS WHILE LEDGER OPENING ENTRY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                BtnSave.Enabled = true;
            }
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        if (TxtVno.Text != string.Empty)
        {
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..?", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) == DialogResult.Yes) Close();
        }
        else
        {
            ClearControl();
        }
    }

    #endregion ---------- Botton ----------

    #region ---------- Events ----------

    private void CbModule_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbModule, 'E');
    }

    private string ReturnVoucherNo()
    {
        var dtCheck = ClsMasterSetup.GetDocumentNumberingSchema("COA");
        if (dtCheck.Rows.Count == 1)
        {
            DocDesc = dtCheck.Rows[0]["DocDesc"].ToString();
            TxtVno.GetCurrentVoucherNo("COA", DocDesc);
        }
        else if (dtCheck.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme
            {
                Source = "COA",
                TblName = "AMS.LedgerOpening",
                FldNAme = "Voucher_No"
            };
            if (wnd.ShowDialog() != DialogResult.OK) return TxtVno.Text;
            if (string.IsNullOrEmpty(wnd.VNo)) return TxtVno.Text;
            DocDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }

        return TxtVno.Text;
    }

    private void CbModule_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbModule, 'L');
        if (CmbModule.Text is "Normal")
            ReturnVoucherNo();
        else
            TxtVno.Text = string.Empty;
    }

    private void CbModule_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txtVno_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVno, 'E');
    }

    private void txtVno_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVno, 'L');
    }

    private void txtVno_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtVno.Text.Trim()))
        {
            if (CmbModule.Text is "Normal")
                ReturnVoucherNo();
            else
                TxtVno.Text = string.Empty;
        }

        if (TxtVno.Text.Trim() != string.Empty)
            switch (Tag.ToString())
            {
                case "SAVE":
                {
                    Query = $"SELECT Voucher_No FROM AMS.LedgerOpening Where Voucher_No='{TxtVno.Text}'";
                    dt.Reset();
                    dt = GetConnection.SelectDataTableQuery(Query);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(@"OPENING VOUCHER NUMBER ALREADY EXITS..!!", ObjGlobal.Caption);
                        TxtVno.Text = string.Empty;
                        TxtVno.Focus();
                    }

                    break;
                }
                case "UPDATE":
                {
                    Query =
                        $"SELECT Voucher_No FROM AMS.LedgerOpening Where Voucher_No='{TxtVno.Text}' AND Ledger_Id <> '{LedgerId}'";
                    dt = GetConnection.SelectDataTableQuery(Query);
                    if (dt.Rows.Count > 0)
                    {
                        ClearControl();
                        LedgerOpening_Update(dt.Rows[0]["Voucher_No"].ToString());
                        ObjGlobal.DgvBackColor(DGrid);
                        var amount = LblDebitAmount.Text.GetDecimal() + LblLocalDebitAmount.Text.GetDecimal();
                        LblInWords.Text = ClsMoneyConversion.MoneyConversion(amount.ToString());
                        TxtVno.Enabled = false;
                        if (MskDate.Enabled) MskDate.Focus();
                    }

                    break;
                }
            }
    }

    private void txtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtVno.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show(@"LEDGER OPENING VOUCHER NUMBER IS BLANK..!!", ObjGlobal.Caption);
                TxtVno.Focus();
                return;
            }
        }

        if (e.KeyCode == Keys.F1) btnVno_Click(sender, e);
    }

    private void btnVno_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MED", "LEDGEROPENING", SearchKeys, _actionTag,
            CmbModule.SelectedValue.ToString(), "TRANSACTION", string.Empty,
            Convert.ToDateTime(MskDate.Text.Trim()).ToString("yyyy-MM-dd"));
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (_actionTag != "SAVE")
                    LedgerOpening_Update(frmPickList.SelectedList[0]["Voucher_No"].ToString().Trim());
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"OPENING VOUCHER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVno.Focus();
            return;
        }

        SearchKeys = string.Empty;
        TxtVno.Focus();
    }

    protected void LedgerOpening_Update(string txtVoucherNo)
    {
        var dt = GetConnection.SelectDataTableQuery(
            $"SELECT * FROM AMS.LedgerOpening AS lo LEFT OUTER JOIN ams.GeneralLedger AS gl ON lo.Ledger_Id = gl.glId WHERE lo.Voucher_No = '{txtVoucherNo}'");
        if (dt.Rows.Count <= 0) return;
        TxtVno.Text = dt.Rows[0]["Voucher_No"].ToString();
        MskMiti.Text = dt.Rows[0]["OP_Miti"].ToString();
        MskDate.Text = Convert.ToDateTime(dt.Rows[0]["OP_Date"].ToString()).ToString("dd/MM/yyyy");
        TxtCurrency.Text = ObjGlobal.SysCurrency; //"NPR";
        TxtCurrencyRate.Text = "1";
        decimal.TryParse(dt.Rows[0]["Debit"].ToString(), out var _Debit);
        TxtDebitAmount.Text = Convert.ToDecimal(_Debit).ToString(ObjGlobal.SysAmountFormat);
        decimal.TryParse(dt.Rows[0]["Credit"].ToString(), out var _Credit);
        TxtCreditAmount.Text = Convert.ToDecimal(_Credit).ToString(ObjGlobal.SysAmountFormat);
        long.TryParse(dt.Rows[0]["Ledger_Id"].ToString(), out var _LedgerId);
        LedgerId = _LedgerId;
        LblLedgerDesc.Text = dt.Rows[0]["GLName"].ToString();
    }

    private void mskMiti_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMiti, 'E');
        lblHintsDisplay.Text = "Please Enter Nepali Date of you Opening Voucher Number !";
    }

    private void mskMiti_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMiti, 'L');
        if (MskMiti.Text != "  /  /")
            MskDate.Text =
                GetConnection.GetQueryData("Select AD_Date from AMS.DateMiti where BS_DateDMY='" + MskMiti.Text +
                                           "'");

        lblHintsDisplay.Text = string.Empty;
    }

    private void mskMiti_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (MskMiti.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Purchase Invoice Miti Cannot Left  Blank, Please Enter Your Invoice Date!");
                MskMiti.Focus();
            }
        }
    }

    private void dpDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskDate, 'E');
        lblHintsDisplay.Text = "Please Enter English Date of you Opening Voucher Number !";
    }

    private void dpDate_Leave(object sender, EventArgs e)
    {
        if (MskDate.Text != "  /  /")
            MskMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                      Convert.ToDateTime(MskDate.Text).ToString("yyyy-MM-dd") +
                                                      "'");

        ObjGlobal.MskTxtBackColor(MskDate, 'L');
        lblHintsDisplay.Text = string.Empty;
    }

    private void dpDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txtSubledger_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSubledger, 'E');
        lblHintsDisplay.Text = "Please Select SubLedger Name from List Using F1 key Or Use CTRL+N to Create New";
    }

    private void txtSubledger_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSubledger, 'L');
        lblHintsDisplay.Text = string.Empty;
    }

    private void txtSubledger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) btnSubledger_Click(sender, e);

        if (e.Control && e.KeyCode == Keys.N)
        {
            using var frm = new FrmSubLedger(true);
            frm.ShowDialog();
            TxtSubledger.Text = frm.SubLedgerName;
            frm.Dispose();
            TxtSubledger.Focus();
        }

        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txtSubledger_Validating(object sender, CancelEventArgs e)
    {
    }

    private void btnSubledger_Click(object sender, EventArgs e)
    {
        (TxtSubledger.Text, SubledgerId) = GetMasterList.GetSubLedgerList(_actionTag);

        // var frmPickList = new FrmAutoPopList("MED", "SUBLEDGER", _actionTag, ObjGlobal.SearchText, string.Empty,
        //     "LIST");
        // if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        // {
        //     frmPickList.ShowDialog();
        //     if (frmPickList.SelectedList.Count > 0)
        //     {
        //         TxtSubledger.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
        //         SubledgerId = Convert.ToInt32(frmPickList.SelectedList[0]["SubledgerId"].ToString().Trim());
        //     }
        //
        //     frmPickList.Dispose();
        // }
        // else
        // {
        //     MessageBox.Show(@"SUB LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Warning);
        //     TxtSubledger.Focus();
        //     return;
        // }
        //
        // ObjGlobal.SearchText = string.Empty;
        // TxtVno.Focus();
    }

    private void txtClass_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtDepartment, 'E');
    }

    private void txtClass_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtDepartment, 'L');
    }

    private void txtClass_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            btnClass_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmDepartmentSetup();
            frm.ShowDialog();
            TxtDepartment.Text = frm.DepartmentName;
            TxtDepartment.Text = string.Empty;
            TxtDepartment.Focus();
        }
    }

    private void txtClass_Validating(object sender, CancelEventArgs e)
    {
    }

    private void btnClass_Click(object sender, EventArgs e)
    {
    }

    private void txtAgent_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtAgent, 'E');
    }

    private void txtAgent_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtAgent, 'L');
    }

    private void txtAgent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) btnAgent_Click(sender, e);
        if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmJuniorAgent(true);
            frm.ShowDialog();
            TxtAgent.Text = frm.AgentName;
            TxtAgent.Focus();
        }
    }

    private void txtAgent_Validating(object sender, CancelEventArgs e)
    {
    }

    private void btnAgent_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "AGENT", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtAgent.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                AgentId = Convert.ToInt32(frmPickList.SelectedList[0]["Description"].ToString().Trim());
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

    private void TxtCurrency_Leave(object sender, EventArgs e)
    {
        //ObjGlobal.ComboBoxBackColor(TxtCurrency, 'L');
        lblHintsDisplay.Text = string.Empty;
        TxtCurrencyRate.Text = Convert
            .ToDouble(ObjGlobal.ReturnDecimal(GetConnection.GetQueryData(
                "Select isnull(CRate,0) From AMS.Currency Where CId = " + ObjGlobal.SysCurrencyId + " ")))
            .ToString(ObjGlobal.SysCurrencyFormat);
    }

    private void GlobalEnter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCurrency_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmCurrency(true);
            frm.ShowDialog();
            TxtCurrency.Text = frm.CurrencyDesc;
            CurrencyId = frm.CurrencyId;
            TxtCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCurrency, BtnCurrency);
        }
    }

    private void TxtCurrency_Validating(object sender, CancelEventArgs e)
    {
        if (TxtCurrency.Text.Trim() != string.Empty)
        {
            Query = "Select * from AMS.Currency where CCode = '" + TxtCurrency.Text + "'";
            dtvalidate.Reset();
            dtvalidate = GetConnection.SelectDataTableQuery(Query);
            if (dtvalidate.Rows.Count > 0)
            {
                if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(TxtCurrencyRate.Text)) == 0)
                    TxtCurrencyRate.Text = Convert
                        .ToDecimal(ObjGlobal.ReturnDecimal(dtvalidate.Rows[0]["CRate"].ToString()))
                        .ToString(ObjGlobal.SysCurrencyFormat);
            }
            else
            {
                MessageBox.Show("Sorry !!! Currency Doesn't Exit Plz Check Spelling !");
                TxtCurrency.Focus();
            }
        }
        else
        {
            TxtCurrencyRate.Text = string.Empty;
        }
    }

    private void txtCurrencyRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtCurrencyRate.Text.Length == 0) TxtCurrencyRate.Text = "1.00";
            TxtDebitAmount.Focus();
        }
    }

    private void txtCurrencyRate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCurrencyRate, 'L');
        lblHintsDisplay.Text = string.Empty;
    }

    private void txtCurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void txt_Debit_Validated(object sender, EventArgs e)
    {
        TxtDebitAmount.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtDebitAmount.Text))
            .ToString(ObjGlobal.SysAmountFormat);
        LblDifferencAmount.Text =
            (Convert.ToDouble(ObjGlobal.ReturnDecimal(LblDebitAmount.Text)) -
             Convert.ToDouble(ObjGlobal.ReturnDecimal(LblLocalDebitAmount.Text)))
            .ToString(ObjGlobal.SysAmountFormat);
    }

    private void txt_Debit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void txt_Debit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtDebitAmount.Text.Trim())) > 0)
            {
                TxtCreditAmount.Enabled = false;
                SendKeys.Send("{TAB}");
            }
            else
            {
                TxtCreditAmount.Enabled = true;
                TxtCreditAmount.Focus();
            }
        }
    }

    private void txt_Debit_TextChanged(object sender, EventArgs e)
    {
        if (TxtDebitAmount.Text != string.Empty)
        {
            if (decimal.Parse(TxtDebitAmount.Text) > 0) TxtCreditAmount.Text = string.Empty;
            TxtCreditAmount.TabStop = TxtDebitAmount.Text.GetDouble() <= 0;
        }
        else
        {
            TxtCreditAmount.TabStop = true;
        }
    }

    private void txt_Credit_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCreditAmount, 'E');
    }

    private void txt_Credit_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCreditAmount, 'L');
        if (TxtCreditAmount.Text.Trim().GetDouble() > 0) TxtDebitAmount.Text = string.Empty;
    }

    private void txt_Credit_Validating(object sender, CancelEventArgs e)
    {
        TxtCreditAmount.Text = TxtCreditAmount.Text.GetDouble().ToString(ObjGlobal.SysAmountFormat);
        LblDifferencAmount.Text =
            (LblDebitAmount.Text.GetDouble() - LblLocalDebitAmount.Text.GetDouble()).ToString(ObjGlobal
                .SysAmountFormat);
    }

    private void txt_Credit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtCreditAmount.Text.Trim())) == 0 &&
                Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtDebitAmount.Text.Trim())) == 0)
            {
                MessageBox.Show(@"Debit & Credit Amount Cannot Be Zero", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDebitAmount.Focus();
            }

            if (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtCreditAmount.Text.Trim())) > 0)
            {
                TxtDebitAmount.Enabled = false;
                SendKeys.Send("{TAB}");
            }
            else
            {
                TxtDebitAmount.Enabled = true;
                TxtDebitAmount.Focus();
            }
        }
    }

    private void txt_Credit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void txt_Credit_TextChanged(object sender, EventArgs e)
    {
        if (TxtCreditAmount.Text == string.Empty) return;
        if (TxtCreditAmount.Text.GetDouble() > 0) TxtDebitAmount.Text = string.Empty;
    }

    private void txtNarration_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNarration, 'E');
        lblHintsDisplay.Text = string.Empty;
    }

    private void txtNarration_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNarration, 'L');
        lblHintsDisplay.Text = string.Empty;
    }

    private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
        {
            if (TxtCreditAmount.Text.GetDouble().Equals(TxtDebitAmount.Text.GetDouble()))
            {
                MessageBox.Show(@"Debit & Credit Amount Cannot Be Zero", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDebitAmount.Focus();
                return;
            }

            BtnSave.Focus();
        }
    }

    #endregion ---------- Events ----------

    private void label1_Click(object sender, EventArgs e)
    {

    }

}