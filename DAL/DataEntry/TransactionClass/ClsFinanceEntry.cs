using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.DayClosing;
using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.NotesMaster;
using DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque;
using DatabaseModule.DataEntry.OpeningMaster;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.TransactionClass;

public class ClsFinanceEntry : IFinanceEntry
{
    // FINANCE CLASS

    #region --------------- FINANCE ENTRY ---------------

    public ClsFinanceEntry()
    {
        CashMaster = new CashClosing();
        JvMaster = new JV_Master();
        CbMaster = new CB_Master();
        GetOpening = new LedgerOpening();
        PdcMaster = new PostDateCheque();
        NMaster = new Notes_Master();
        CbDetails = new CB_Details();
    }

    #endregion --------------- FINANCE ENTRY ---------------


    // UPDATE IMAGE ON FINANCE VOUCHER

    #region --------------- UPDATE IMAGE ---------------

    public int UpdateImageFinance(string tableName, string filterColumn)
    {
        var isOk = 0;
        //if (GetImage.PAttachment1 != null || GetImage.PAttachment2 != null || GetImage.PAttachment3 != null ||
        //    GetImage.PAttachment4 != null || GetImage.PAttachment5 != null)
        //{
        //    var cmdString = new StringBuilder();
        //    cmdString.Append(
        //        $"UPDATE {tableName} SET PAttachment1 = @PImage1,PAttachment2 = @PImage2,PAttachment3 = @PImage3,PAttachment4 = @PImage4,PAttachment5 = @PImage5 WHERE {filterColumn} = N'{GetImage.VoucherNo}';");
        //    var cmd2 = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection());
        //    if (GetImage.PAttachment1 != null)
        //    {
        //        cmd2.Parameters.Add("@PImage1", SqlDbType.Image).Value = GetImage.PAttachment1;
        //    }

        //    if (GetImage.PAttachment2 != null)
        //    {
        //        cmd2.Parameters.Add("@PImage2", SqlDbType.Image).Value = GetImage.PAttachment2;
        //    }

        //    if (GetImage.PAttachment3 != null)
        //    {
        //        cmd2.Parameters.Add("@PImage3", SqlDbType.Image).Value = GetImage.PAttachment3;
        //    }

        //    if (GetImage.PAttachment4 != null)
        //    {
        //        cmd2.Parameters.Add("@PImage4", SqlDbType.Image).Value = GetImage.PAttachment4;
        //    }

        //    if (GetImage.PAttachment5 != null)
        //    {
        //        cmd2.Parameters.Add("@PImage5", SqlDbType.Image).Value = GetImage.PAttachment5;
        //    }

        //    isOk = cmd2.ExecuteNonQuery();
        //}

        return isOk;
    }
    #endregion --------------- UPDATE IMAGE ---------------


    // INSERT, UPDATE , DELETE FUNCTION

    #region --------------- IUD EVENT  ---------------

    public int SaveCashBankVoucher(string actionTag)
    {
        var cmdTxt = new StringBuilder();
        if (_tagStrings.Contains(actionTag))
        {
            AuditLogCashBankVoucher();
            cmdTxt.Append($@"
                DELETE FROM AMS.AccountDetails WHERE Voucher_No='{CbMaster.Voucher_No}' AND Module='CB';");
            if (!actionTag.Equals("REVERSE"))
            {
                cmdTxt.Append($@"
                    DELETE FROM AMS.CB_Details WHERE Voucher_No='{CbMaster.Voucher_No}';");
            }

            if (actionTag is "DELETE")
            {
                cmdTxt.Append($@"
                    DELETE FROM AMS.CB_Master WHERE Voucher_No='{CbMaster.Voucher_No}';");
            }
        }

        if (actionTag == "SAVE")
        {
            cmdTxt.Append(@" 
                INSERT INTO AMS.CB_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, IsReverse, CancelBy, CancelDate, CancelRemarks, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdTxt.Append("VALUES ( \n");
            cmdTxt.Append(
                $" N'{CbMaster.VoucherMode}',N'{CbMaster.Voucher_No}',N'{CbMaster.Voucher_Date:yyyy-MM-dd}','{CbMaster.Voucher_Miti}',GETDATE(),");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits()
                ? $" '{CbMaster.Ref_VNo}','{CbMaster.Ref_VDate:yyyy-MM-dd}',"
                : "Null,Null,");
            cmdTxt.Append(CbMaster.VoucherType.IsValueExits() ? $" '{CbMaster.VoucherType}'," : "N'CONTRA'");
            cmdTxt.Append($" '{CbMaster.Ledger_Id}',");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits()
                ? $" '{CbMaster.CheqNo}','{CbMaster.CheqDate:yyyy-MM-dd}','{CbMaster.CheqMiti}',"
                : "Null,NULL,NULL,");
            cmdTxt.Append(CbMaster.Currency_Id > 0 ? $" '{CbMaster.Currency_Id}'," : $"{ObjGlobal.SysCurrencyId},");
            cmdTxt.Append(CbMaster.Currency_Rate > 0 ? $" '{CbMaster.Currency_Rate}'," : "1,");
            cmdTxt.Append(CbMaster.Cls1 > 0 ? $" {CbMaster.Cls1}," : "NUll ,");
            cmdTxt.Append("NUll, NUll, NUll,");
            cmdTxt.Append(CbMaster.Remarks.IsValueExits() ? $" N'{CbMaster.Remarks}'," : "Null,");
            cmdTxt.Append(CbMaster.Action_Type.IsValueExits() ? $" N'{CbMaster.Action_Type}'," : "'SAVE',");
            cmdTxt.Append($"'{ObjGlobal.LogInUser}' ,GETDATE(),NULL,NULL,0,NULL,NULL,0,{ObjGlobal.SysBranchId}, ");
            cmdTxt.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" '{ObjGlobal.SysCompanyUnitId}'," : "NUll ,");
            cmdTxt.Append($"{ObjGlobal.SysFiscalYearId},NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,");
            cmdTxt.Append(CbMaster.In_Words.IsValueExits() ? $" N'{CbMaster.In_Words}'," : "Null,");
            cmdTxt.Append($"NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdTxt.Append(" UPDATE AMS.CB_Master SET ");
            cmdTxt.Append(
                $" Voucher_Date = '{CbMaster.Voucher_Date:yyyy-MM-dd}',Voucher_Miti = N'{CbMaster.Voucher_Miti}', ");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits() ? $" Ref_VNo = '{CbMaster.Ref_VNo}'," : "Ref_VNo = Null,");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits()
                ? $" Ref_VDate = '{CbMaster.Ref_VDate:yyyy-MM-dd}',"
                : "Ref_VDate = Null,");
            cmdTxt.Append(CbMaster.VoucherType.IsValueExits()
                ? $" VoucherType = '{CbMaster.VoucherType}',"
                : "VoucherType = Null,");
            cmdTxt.Append(CbMaster.Ledger_Id > 0 ? $" Ledger_Id = '{CbMaster.Ledger_Id}'," : "Ledger_Id= Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits() ? $" CheqNo= '{CbMaster.CheqNo}'," : "CheqNo = Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits()
                ? $" CheqDate = '{CbMaster.CheqDate:yyyy-MM-dd}',"
                : "CheqDate = Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits() ? $"CheqMiti=  '{CbMaster.CheqMiti}'," : "CheqMiti = Null,");
            cmdTxt.Append(CbMaster.Currency_Id > 0 ? $"Currency_Id= '{CbMaster.Currency_Id}'," : "Currency_Id= 1,");
            cmdTxt.Append(CbMaster.Currency_Rate > 0
                ? $" Currency_Rate = '{CbMaster.Currency_Rate}',"
                : "Currency_Rate = 1,");
            cmdTxt.Append(CbMaster.Cls1 > 0 ? $" Cls1 = '{CbMaster.Cls1}'," : " Cls1 = NUll ,");
            cmdTxt.Append(CbMaster.Remarks.IsValueExits() ? $"Remarks = N'{CbMaster.Remarks}'," : " Remarks = Null,");
            cmdTxt.Append(CbMaster.Action_Type.IsValueExits()
                ? $"Action_Type=N'{CbMaster.Action_Type}', "
                : " Action_Type = 'UPDATE', ");
            cmdTxt.Append($"SyncRowVersion =  {CbMaster.SyncRowVersion.GetDecimal(true)} ,");
            cmdTxt.Append(" IsSynced =0");
            cmdTxt.Append($" WHERE Voucher_No = N'{CbMaster.Voucher_No}'; \n");
        }
        else if (actionTag.Equals("REVERSE"))
        {
            cmdTxt.Append(
                $"UPDATE	AMS.CB_Master SET CancelBy = '{ObjGlobal.LogInUser}', CancelDate = GETDATE(), CancelRemarks = '{CbMaster.Remarks}' WHERE Voucher_No = '{CbMaster.Voucher_No}'; \n");
        }

        if (actionTag != "DELETE" && !actionTag.Equals("POST"))
        {
            cmdTxt.Append(
                " INSERT INTO AMS.CB_Details(Voucher_No, SNo, CBLedgerId, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdTxt.Append(" VALUES \n");
            if (CbMaster.GetView is { RowCount: > 0 })
            {
                var iRows = 0;
                foreach (DataGridViewRow drCel in CbMaster.GetView.Rows)
                {
                    iRows++;
                    var subLedgerId = drCel.Cells["GTxtSubledgerId"].Value.GetInt();
                    var currencyId = drCel.Cells["GTxtCurrencyId"].Value.GetDecimal();
                    var agentId = drCel.Cells["GTxtAgentId"].Value.GetDecimal();
                    var departmentId = drCel.Cells["GTxtDepartmentId"].Value.GetDecimal();
                    var currencyRate = drCel.Cells["GTxtExchangeRate"].Value.GetDecimal();

                    cmdTxt.Append($"(N'{CbMaster.Voucher_No}',");
                    cmdTxt.Append($"{drCel.Cells["GTxtSNo"].Value},");
                    cmdTxt.Append($"{CbMaster.Ledger_Id},{drCel.Cells["GTxtLedgerId"].Value.GetLong()},");
                    cmdTxt.Append(subLedgerId > 0 ? $"{subLedgerId}," : "NULL,");
                    cmdTxt.Append(agentId > 0 ? $"{agentId}," : "NULL,");
                    cmdTxt.Append(departmentId > 0 ? $"{departmentId}," : "NULL,");
                    cmdTxt.Append("NULL,NULL,NULL,");
                    cmdTxt.Append(currencyId > 0 ? $"{drCel.Cells["GTxtCurrencyId"].Value.GetInt()}," :
                        CbMaster.Currency_Id > 0 ? $"{CbMaster.Currency_Id}," : "NULL,");
                    cmdTxt.Append(currencyRate > 0
                        ? $"{drCel.Cells["GTxtExchangeRate"].Value.GetDecimal(true)} ,"
                        : "1,");
                    cmdTxt.Append($"{drCel.Cells["GTxtPayment"].Value.GetDecimal()},");
                    cmdTxt.Append($"{drCel.Cells["GTxtReceipt"].Value.GetDecimal()},");
                    cmdTxt.Append($"{drCel.Cells["GTxtLocalPayment"].Value.GetDecimal()},");
                    cmdTxt.Append($"{drCel.Cells["GTxtLocalReceipt"].Value.GetDecimal()},0,0,");
                    cmdTxt.Append(drCel.Cells["GTxtNarration"].Value.IsValueExits()
                        ? $"N'{drCel.Cells["GTxtNarration"].Value}',"
                        : "NULL,");
                    cmdTxt.Append(
                        $"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetInt()}");
                    cmdTxt.Append(iRows == CbMaster.GetView.RowCount ? " );" : "),");
                    cmdTxt.Append(" \n");
                }
            }
            else
            {
                cmdTxt.Append($" ('{CbDetails.Voucher_No}', 1,{CbDetails.CBLedgerId}, {CbDetails.Ledger_Id},");
                cmdTxt.Append(CbDetails.Subledger_Id > 0 ? $"{CbDetails.Subledger_Id}, " : "NULL,");
                cmdTxt.Append(CbDetails.Agent_Id > 0 ? $"{CbDetails.Agent_Id}, " : "NULL,");
                cmdTxt.Append(CbDetails.Cls1 > 0 ? $"{CbDetails.Cls1}, " : "NULL,");
                cmdTxt.Append("NULL, NULL, NULL, ");
                cmdTxt.Append(CbDetails.CurrencyId > 0 ? $"{CbDetails.CurrencyId}, " : $"{ObjGlobal.SysCurrencyId},");
                cmdTxt.Append(CbDetails.CurrencyRate > 0 ? $"{CbDetails.CurrencyRate}," : "1,");
                cmdTxt.Append(
                    $"{CbDetails.Debit}, {CbDetails.Credit},{CbDetails.LocalDebit}, {CbDetails.LocalCredit},{CbDetails.Tbl_Amount.GetDecimal()},{CbDetails.V_Amount.GetDecimal()},");
                cmdTxt.Append(CbDetails.Narration.IsValueExits() ? $"N'{CbDetails.Narration}', " : "NULL,");
                cmdTxt.Append(CbDetails.Party_No.IsValueExits() ? $"'{CbDetails.Party_No}', " : "NULL,");
                cmdTxt.Append(CbDetails.Party_No.IsValueExits() ? $"'{CbDetails.Invoice_Date:yyyy-MM-dd}', " : "NULL,");
                cmdTxt.Append(CbDetails.Party_No.IsValueExits() ? $"'{CbDetails.Invoice_Miti}', " : "NULL,");
                cmdTxt.Append(CbDetails.VatLedger_Id > 0 ? $"{CbDetails.VatLedger_Id}," : "NULL,");
                cmdTxt.Append(CbDetails.PanNo.IsValueExits() ? $"'{CbDetails.PanNo}', " : "NULL,");
                cmdTxt.Append(CbDetails.Vat_Reg.IsValueExits() ? $"CAST('{CbDetails.Vat_Reg}' AS BIT), " : "NUll,");
                cmdTxt.Append($"NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetDecimal(true)}); \n");
            }
        }

        if (actionTag is "POST")
        {
            cmdTxt.Append($"UPDATE AMS.CB_Master SET VoucherMode=N'POST',ReconcileBy = '{ObjGlobal.LogInUser}',ReconcileDate= GETDATE(),Audit_Lock = 1 WHERE Voucher_No='{CbMaster.Voucher_No}' ");
        }

        var iUpdate = SqlExtensions.ExecuteNonTrans(cmdTxt);
        if (iUpdate > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncCashBankVoucherAsync(actionTag));
            }
        }

        if (iUpdate == 0 || actionTag == "REVERSE")
        {
            return iUpdate;
        }

        if (!CbMaster.VoucherMode.Equals("PROV") && !actionTag.Equals("DELETE"))
        {
            CashBankAccountPosting();
        }

        if (string.IsNullOrEmpty(CbMaster.Ref_VNo))
        {
            return iUpdate;
        }

        var cmd = $"Delete from AMS.AccountDetails Where Module = 'PDC' AND  Voucher_No = '{CbMaster.Ref_VNo}'; ";
        if (actionTag is "SAVE")
        {
            cmd += $" Update AMS.PostDateCheque set Status = 'Deposit' WHERE VoucherNo = '{CbMaster.Ref_VNo}'; ";
        }
        else if (actionTag is "DELETE")
        {
            cmd += $" Update AMS.PostDateCheque set Status = 'Due' WHERE VoucherNo = '{CbMaster.Ref_VNo}'; ";
        }

        var result1 = SqlExtensions.ExecuteNonQuery(cmd);
        return iUpdate;
    }

    private int AuditLogCashBankVoucher()
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_CB_MASTER(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, '{CbMaster.Action_Type}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.CB_Master
            WHERE Voucher_No ='{CbMaster.Voucher_No}';

            INSERT INTO AUD.AUDIT_CB_DETAILS(Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, CBLedgerId, CurrencyId, CurrencyRate, ModifyAction, ModifyBy, ModifyDate)
            SELECT Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, CBLedgerId, CurrencyId, CurrencyRate, '{CbMaster.Action_Type}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.CB_Details
            WHERE Voucher_No ='{CbMaster.Voucher_No}'; ";

        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int SaveRemittanceVoucher(DataGridView rView, DataGridView pView, string actionTag)
    {
        var cmdTxt = new StringBuilder();
        if (_tagStrings.Contains(actionTag))
        {
            AuditLogCashBankVoucher();
            cmdTxt.Append(
                $"DELETE FROM AMS.AccountDetails WHERE Voucher_No='{CbMaster.Voucher_No}' AND Module='CB'; \n");
            if (!actionTag.Equals("REVERSE"))
            {
                cmdTxt.Append($"DELETE FROM AMS.CB_Details WHERE Voucher_No='{CbMaster.Voucher_No}'; \n");
            }

            if (actionTag is "DELETE")
            {
                cmdTxt.Append($"DELETE FROM AMS.CB_Master WHERE Voucher_No='{CbMaster.Voucher_No}'; \n");
            }
        }

        if (actionTag == "SAVE")
        {
            cmdTxt.Append(
                " INSERT INTO AMS.CB_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, IsReverse, CancelBy, CancelDate, CancelRemarks, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdTxt.Append("VALUES ( \n");
            cmdTxt.Append(
                $" N'{CbMaster.VoucherMode}',N'{CbMaster.Voucher_No}',N'{CbMaster.Voucher_Date:yyyy-MM-dd}','{CbMaster.Voucher_Miti}',GETDATE(),");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits()
                ? $" '{CbMaster.Ref_VNo}','{CbMaster.Ref_VDate:yyyy-MM-dd}',"
                : "Null,Null,");
            cmdTxt.Append(CbMaster.VoucherType.IsValueExits() ? $" '{CbMaster.VoucherType}'," : "N'CONTRA'");
            cmdTxt.Append($" '{CbMaster.Ledger_Id}',");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits()
                ? $" '{CbMaster.CheqNo}','{CbMaster.CheqDate:yyyy-MM-dd}','{CbMaster.CheqMiti}',"
                : "Null,NULL,NULL,");
            cmdTxt.Append(CbMaster.Currency_Id > 0 ? $" '{CbMaster.Currency_Id}'," : $"{ObjGlobal.SysCurrencyId},");
            cmdTxt.Append(CbMaster.Currency_Rate > 0 ? $" '{CbMaster.Currency_Rate}'," : "1,");
            cmdTxt.Append(CbMaster.Cls1 > 0 ? $" {CbMaster.Cls1}," : "NUll ,");
            cmdTxt.Append("NUll, NUll, NUll,");
            cmdTxt.Append(CbMaster.Remarks.IsValueExits() ? $" N'{CbMaster.Remarks}'," : "Null,");
            cmdTxt.Append(CbMaster.Action_Type.IsValueExits() ? $" N'{CbMaster.Action_Type}'," : "'SAVE',");
            cmdTxt.Append($"'{ObjGlobal.LogInUser}' ,GETDATE(),NULL,NULL,0,NULL,NULL,0,{ObjGlobal.SysBranchId}, ");
            cmdTxt.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" '{ObjGlobal.SysCompanyUnitId}'," : "NUll ,");
            cmdTxt.Append($"{ObjGlobal.SysFiscalYearId},NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,");
            cmdTxt.Append(CbMaster.In_Words.IsValueExits() ? $" N'{CbMaster.In_Words}'," : "Null,");
            cmdTxt.Append($"NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdTxt.Append(" UPDATE AMS.CB_Master SET ");
            cmdTxt.Append(
                $" Voucher_Date = '{CbMaster.Voucher_Date:yyyy-MM-dd}',Voucher_Miti = N'{CbMaster.Voucher_Miti}', ");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits() ? $" Ref_VNo = '{CbMaster.Ref_VNo}'," : "Ref_VNo = Null,");
            cmdTxt.Append(CbMaster.Ref_VNo.IsValueExits()
                ? $" Ref_VDate = '{CbMaster.Ref_VDate:yyyy-MM-dd}',"
                : "Ref_VDate = Null,");
            cmdTxt.Append(CbMaster.VoucherType.IsValueExits()
                ? $" VoucherType = '{CbMaster.VoucherType}',"
                : "VoucherType = Null,");
            cmdTxt.Append(CbMaster.Ledger_Id > 0 ? $" Ledger_Id = '{CbMaster.Ledger_Id}'," : "Ledger_Id= Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits() ? $" CheqNo= '{CbMaster.CheqNo}'," : "CheqNo = Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits()
                ? $" CheqDate = '{CbMaster.CheqDate:yyyy-MM-dd}',"
                : "CheqDate = Null,");
            cmdTxt.Append(CbMaster.CheqNo.IsValueExits() ? $"CheqMiti=  '{CbMaster.CheqMiti}'," : "CheqMiti = Null,");
            cmdTxt.Append(CbMaster.Currency_Id > 0 ? $"Currency_Id= '{CbMaster.Currency_Id}'," : "Currency_Id= 1,");
            cmdTxt.Append(CbMaster.Currency_Rate > 0
                ? $" Currency_Rate = '{CbMaster.Currency_Rate}',"
                : "Currency_Rate = 1,");
            cmdTxt.Append(CbMaster.Cls1 > 0 ? $" Cls1 = '{CbMaster.Cls1}'," : " Cls1 = NUll ,");
            cmdTxt.Append(CbMaster.Remarks.IsValueExits() ? $"Remarks = N'{CbMaster.Remarks}'," : " Remarks = Null,");
            cmdTxt.Append(CbMaster.Action_Type.IsValueExits()
                ? $"Action_Type=N'{CbMaster.Action_Type}', "
                : " Action_Type = 'UPDATE', ");
            cmdTxt.Append($"SyncRowVersion =  {CbMaster.SyncRowVersion.GetDecimal(true)} ,");
            cmdTxt.Append(" IsSynced =0");
            cmdTxt.Append($" WHERE Voucher_No = N'{CbMaster.Voucher_No}'; \n");
        }

        if (actionTag != "DELETE")
        {
            if (rView is { RowCount: > 0 })
            {
                foreach (DataGridViewRow drCel in rView.Rows)
                {
                    var ledgerId = drCel.Cells["GTxtRLedgerId"].Value.GetInt();
                    if (ledgerId is 0)
                    {
                        continue;
                    }

                    cmdTxt.Append(
                        " INSERT INTO AMS.CB_Details(Voucher_No, SNo, CBLedgerId, Ledger_Id, SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                    cmdTxt.Append(" VALUES \n");

                    var currencyId = drCel.Cells["GTxtRCurrencyId"].Value.GetDecimal();
                    var currencyRate = drCel.Cells["GTxtRExchangeRate"].Value.GetDecimal();

                    cmdTxt.Append($" (N'{CbMaster.Voucher_No}',");
                    cmdTxt.Append($"{drCel.Cells["GTxtRSNo"].Value},");
                    cmdTxt.Append($"{CbMaster.Ledger_Id},{drCel.Cells["GTxtRLedgerId"].Value.GetLong()},");
                    cmdTxt.Append("NULL,NULL,NULL,NULL,NULL,NULL,");
                    cmdTxt.Append(currencyId > 0 ? $"{currencyId}," :
                        CbMaster.Currency_Id > 0 ? $"{CbMaster.Currency_Id}," : "NULL,");
                    cmdTxt.Append(currencyRate > 0 ? $"{currencyRate.GetDecimal(true)} ," : "1,");
                    cmdTxt.Append($"0,{drCel.Cells["GTxtReceipt"].Value.GetDecimal()},");
                    cmdTxt.Append($"0,{drCel.Cells["GTxtLocalReceipt"].Value.GetDecimal()},0,0,");
                    cmdTxt.Append(drCel.Cells["GTxtRNarration"].Value.IsValueExits()
                        ? $"N'{drCel.Cells["GTxtRNarration"].Value}',"
                        : "NULL,");
                    cmdTxt.Append(
                        $" NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetInt()} ); \n");
                }
            }

            if (pView is { RowCount: > 0 })
            {
                foreach (DataGridViewRow drCel in pView.Rows)
                {
                    var ledgerId = drCel.Cells["GTxtPLedgerId"].Value.GetInt();
                    if (ledgerId is 0)
                    {
                        continue;
                    }

                    cmdTxt.Append(
                        " INSERT INTO AMS.CB_Details(Voucher_No, SNo, CBLedgerId, Ledger_Id, SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                    cmdTxt.Append(" VALUES \n");

                    var currencyId = drCel.Cells["GTxtPCurrencyId"].Value.GetDecimal();
                    var currencyRate = drCel.Cells["GTxtPExchangeRate"].Value.GetDecimal();

                    cmdTxt.Append($" (N'{CbMaster.Voucher_No}',");
                    cmdTxt.Append($"{drCel.Cells["GTxtPSNo"].Value},");
                    cmdTxt.Append($"{CbMaster.Ledger_Id},{drCel.Cells["GTxtPLedgerId"].Value.GetLong()},");
                    cmdTxt.Append("NULL,NULL,NULL,NULL,NULL,NULL,");
                    cmdTxt.Append(currencyId > 0 ? $"{currencyId}," :
                        CbMaster.Currency_Id > 0 ? $"{CbMaster.Currency_Id}," : "NULL,");
                    cmdTxt.Append(currencyRate > 0 ? $"{currencyRate.GetDecimal(true)} ," : "1,");
                    cmdTxt.Append($"{drCel.Cells["GTxtPayment"].Value.GetDecimal()},0,");
                    cmdTxt.Append($"{drCel.Cells["GTxtLocalPayment"].Value.GetDecimal()},0,0,0,");
                    cmdTxt.Append(drCel.Cells["GTxtPNarration"].Value.IsValueExits()
                        ? $"N'{drCel.Cells["GTxtPNarration"].Value}',"
                        : "NULL,");
                    cmdTxt.Append(
                        $" NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{CbMaster.SyncRowVersion.GetInt()} ); \n");
                }
            }
        }

        var iUpdate = SqlExtensions.ExecuteNonTrans(cmdTxt);
        if (iUpdate > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncCashBankVoucherAsync(actionTag));
            }
        }

        if (iUpdate == 0 || actionTag == "REVERSE")
        {
            return iUpdate;
        }

        if (actionTag != null && !CbMaster.VoucherMode.Equals("PROV") &&
            !actionTag.Equals("DELETE"))
        {
            CashBankAccountPosting();
        }

        return iUpdate;
    }

    public async Task<int> SyncCashBankVoucherAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}CashBankVoucher/GetCashBankVoucherById";
            insertUrl = @$"{configParams.Model.Item2}CashBankVoucher/InsertCashBankVoucher";
            updateUrl = @$"{configParams.Model.Item2}CashBankVoucher/UpdateCashBankVoucher";
            deleteUrl = @$"{configParams.Model.Item2}CashBankVoucher/DeleteCashBankVoucherAsync?id=" +
                        CbMaster.Voucher_No;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var cbRepo = DataSyncProviderFactory.GetRepository<CB_Master>(DataSyncManager.GetGlobalInjectData());
            var cb = new CB_Master
            {
                VoucherMode = actionTag == "POST" ? "POST" : CbMaster.VoucherMode,
                Voucher_No = CbMaster.Voucher_No,
                Voucher_Date = Convert.ToDateTime(CbMaster.Voucher_Date.GetSystemDate()),
                Voucher_Miti = CbMaster.Voucher_Miti,
                Voucher_Time = DateTime.Now,
                Ref_VNo = CbMaster.Ref_VNo.IsValueExits() ? CbMaster.Ref_VNo : null,
                Ref_VDate = CbMaster.Ref_VNo.IsValueExits()
                    ? Convert.ToDateTime(CbMaster.Ref_VDate.GetSystemDate())
                    : null,
                VoucherType = CbMaster.VoucherType.IsValueExits() ? CbMaster.VoucherType : "CONTRA",
                Ledger_Id = CbMaster.Ledger_Id,
                CheqNo = CbMaster.CheqNo.IsValueExits() ? CbMaster.CheqNo : null,
                CheqDate = CbMaster.CheqNo.IsValueExits() ? Convert.ToDateTime(CbMaster.CheqDate.GetSystemDate()) : null,
                CheqMiti = CbMaster.CheqNo.IsValueExits() ? CbMaster.CheqMiti : null,
                Currency_Id = CbMaster.Currency_Id > 0 ? CbMaster.Currency_Id : ObjGlobal.SysCurrencyId,
                Currency_Rate = CbMaster.Currency_Rate > 0 ? CbMaster.Currency_Rate : 1,
                Cls1 = CbMaster.Cls1 > 0 ? CbMaster.Cls1 : null,
                Cls2 = null,
                Cls3 = null,
                Cls4 = null,
                Remarks = CbMaster.Remarks.IsValueExits() ? CbMaster.Remarks : null,
                Action_Type = actionTag,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                ReconcileBy = actionTag == "POST" ? ObjGlobal.LogInUser : null,
                ReconcileDate = actionTag == "POST" ? DateTime.Now : null,
                Audit_Lock = actionTag == "POST",
                ClearingBy = null,
                ClearingDate = null,
                PrintValue = null,
                CBranch_Id = ObjGlobal.SysBranchId,
                CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                PAttachment1 = CbMaster.PAttachment1,
                PAttachment2 = null,
                PAttachment3 = null,
                PAttachment4 = null,
                PAttachment5 = null,
                IsReverse = actionTag == "REVERSE" ? true : false,
                CancelBy = actionTag == "REVERSE" ? ObjGlobal.LogInUser : null,
                CancelDate = actionTag == "REVERSE" ? DateTime.Now : null,
                CancelRemarks = actionTag == "REVERSE" ? CbMaster.Remarks : null,
                In_Words = CbMaster.In_Words.IsValueExits() ? CbMaster.In_Words : null,
                SyncRowVersion = CbMaster.SyncRowVersion
            };
            var cbDetails = new List<CB_Details>();
            if (actionTag != "DELETE" && !actionTag.Equals("POST"))
            {
                if (CbMaster.GetView is { RowCount: > 0 })
                {
                    foreach (DataGridViewRow drCel in CbMaster.GetView.Rows)
                    {
                        var subLedgerId = drCel.Cells["GTxtSubledgerId"].Value.GetInt();
                        var currencyId = drCel.Cells["GTxtCurrencyId"].Value.GetDecimal();
                        var agentId = drCel.Cells["GTxtAgentId"].Value.GetInt();
                        var departmentId = drCel.Cells["GTxtDepartmentId"].Value.GetInt();
                        var currencyRate = drCel.Cells["GTxtExchangeRate"].Value.GetDecimal();

                        var cbd = new CB_Details
                        {
                            Voucher_No = CbMaster.Voucher_No,
                            SNo = drCel.Cells["GTxtSNo"].Value.GetInt(),
                            CBLedgerId = CbMaster.Ledger_Id,
                            Ledger_Id = drCel.Cells["GTxtLedgerId"].Value.GetLong(),
                            Subledger_Id = subLedgerId > 0 ? subLedgerId : null,
                            Agent_Id = agentId > 0 ? agentId : null,
                            Cls1 = departmentId > 0 ? departmentId : null,
                            Cls2 = null,
                            Cls3 = null,
                            Cls4 = null,
                            CurrencyId = currencyId > 0 ? drCel.Cells["GTxtCurrencyId"].Value.GetInt() :
                                CbMaster.Currency_Id > 0 ? CbMaster.Currency_Id : null,
                            CurrencyRate = currencyRate > 0 ? drCel.Cells["GTxtExchangeRate"].Value.GetDecimal(true) : 1,
                            Debit = drCel.Cells["GTxtPayment"].Value.GetDecimal(),
                            Credit = drCel.Cells["GTxtReceipt"].Value.GetDecimal(),
                            LocalDebit = drCel.Cells["GTxtLocalPayment"].Value.GetDecimal(),
                            LocalCredit = drCel.Cells["GTxtLocalReceipt"].Value.GetDecimal(),
                            Tbl_Amount = 0,
                            V_Amount = 0,
                            Narration = drCel.Cells["GTxtNarration"].Value.IsValueExits()
                                ? drCel.Cells["GTxtNarration"].Value.ToString()
                                : null,
                            Party_No = null,
                            Invoice_Date = null,
                            Invoice_Miti = null,
                            VatLedger_Id = null,
                            PanNo = null,
                            Vat_Reg = null,
                            SyncRowVersion = CbMaster.SyncRowVersion
                        };
                        cbDetails.Add(cbd);
                    }
                }
            }

            cb.DetailsList = cbDetails;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await (cbRepo?.PushNewAsync(cb)),
                "UPDATE" => await (cbRepo?.PutNewAsync(cb)),
                "REVERSE" => await (cbRepo?.PutNewAsync(cb)),
                "POST" => await (cbRepo?.PutNewAsync(cb)),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (cbRepo?.PushNewAsync(cb))
            };
            if (result.Value)
            {
                var cmd = $@"
                    UPDATE AMS.CB_Master SET IsSynced = 1 WHERE Voucher_No = '{CbMaster.Voucher_No}';";
                SqlExtensions.ExecuteNonQuery(cmd);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int CashBankAccountPosting()
    {
        var query = @"
			Delete from  AMS.AccountDetails Where Module in ('CB') ";
        query += CbMaster.Voucher_No.IsValueExits() ? $" and Voucher_No ='{CbMaster.Voucher_No}'; " : " ";
        query += @"
            INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Ledger_ID, Voucher_Date, Voucher_Miti, Voucher_Time, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, Branch_ID, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT Module, ROW_NUMBER() OVER (PARTITION BY Voucher_No ORDER BY Voucher_No) AS Sno, Voucher_No, Ledger_Id, Voucher_Date, Voucher_Miti, Voucher_Time, CbLedger_ID, Subleder_ID, Agent_Id, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_Id, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, Branch_ID, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM(SELECT 'CB' AS Module, 1 AS Serial_No, cm2.Voucher_No AS Voucher_No, cm2.Ledger_Id AS Ledger_Id, cm2.Voucher_Date AS Voucher_Date, cm2.Voucher_Miti AS Voucher_Miti, cm2.Voucher_Time AS Voucher_Time, cdm.Ledger_Id AS CbLedger_ID, cdm.Subledger_Id AS Subleder_ID, cdm.Agent_Id AS Agent_Id, cdm.Cls1 AS Department_ID1, NULL AS Department_ID2, NULL AS Department_ID3, NULL AS Department_ID4,ISNULL(cdm.CurrencyId, cm2.Currency_Id) AS Currency_Id, ISNULL(cdm.CurrencyRate, cm2.Currency_Rate) AS Currency_Rate, ISNULL(cdm.Debit_Amt, 0) AS Debit_Amt, ISNULL(cdm.Credit_Amt, 0) AS Credit_Amt, ISNULL(cdm.LocalDebit_Amt, 0) AS LocalDebit_Amt, ISNULL(cdm.LocalCredit_Amt, 0) AS LocalCredit_Amt, NULL AS DueDate, 0 AS DueDays, NULL AS Narration, cm2.Remarks AS Remarks, cm2.Ref_VNo AS RefNo, cm2.Ref_VDate AS RefDate, cm2.ReconcileBy AS Reconcile_By, cm2.ReconcileDate AS Reconcile_Date, NULL AS Authorize_By, NULL AS Authorize_Date, cm2.ClearingBy AS Clearing_By, cm2.ClearingDate AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, cm2.CheqNo AS Cheque_No, cm2.CheqDate AS Cheque_Date, cm2.CheqMiti AS Cheque_Miti, NULL AS PartyName, NULL AS PartyLedger_Id, NULL AS Party_PanNo, cm2.CUnit_Id AS CmpUnit_ID, cm2.FiscalYearId AS FiscalYearId, NULL AS DoctorId, NULL AS PatientId, NULL AS HDepartmentId, cm2.CBranch_Id AS Branch_ID, cm2.EnterBy AS EnterBy, cm2.EnterDate AS EnterDate, cm2.SyncBaseId, cm2.SyncGlobalId, cm2.SyncOriginId, cm2.SyncCreatedOn, cm2.SyncLastPatchedOn, ISNULL(cm2.SyncRowVersion, 1) SyncRowVersion
                FROM AMS.CB_Master AS cm2
                INNER JOIN(SELECT cd2.Voucher_No, cd2.Ledger_Id, cd2.Subledger_Id, cd2.Agent_Id, cd2.Cls1,cd2.CurrencyId,cd2.CurrencyRate, SUM(cd2.Credit) AS Debit_Amt, SUM(cd2.Debit) AS Credit_Amt, SUM(cd2.LocalCredit) AS LocalDebit_Amt, SUM(cd2.LocalDebit) AS LocalCredit_Amt
                        FROM AMS.CB_Details AS cd2 ";
        query += CbMaster.Voucher_No.IsValueExits() ? $" WHERE cd2.Voucher_No ='{CbMaster.Voucher_No}'  " : " ";
        query += @"
            GROUP BY cd2.Voucher_No, cd2.Ledger_Id, cd2.Subledger_Id, cd2.Agent_Id, cd2.Cls1,cd2.CurrencyId,cd2.CurrencyRate) AS cdm ON cdm.Voucher_No=cm2.Voucher_No
            WHERE (cm2.IsReverse=0 OR cm2.IsReverse IS NULL)";
        query += CbMaster.Voucher_No.IsValueExits() ? $" AND cm2.Voucher_No ='{CbMaster.Voucher_No}' " : " ";
        query += @"
            UNION ALL
            SELECT 'CB' AS Module, cd.SNo AS Serial_No, cd.Voucher_No AS Voucher_No, cd.Ledger_Id AS Ledger_Id, cm.Voucher_Date, cm.Voucher_Miti, cm.Voucher_Time, cm.Ledger_Id AS CbLedger_ID, cd.Subledger_Id AS Subleder_ID, cd.Agent_Id, cd.Cls1 AS Department_ID1, cd.Cls2 AS Department_ID2, cd.Cls3 AS Department_ID3, cd.Cls4 AS Department_ID4, ISNULL(cd.CurrencyId, cm.Currency_Id) AS Currency_Id, ISNULL(cd.CurrencyRate, cm.Currency_Rate) AS Currency_Rate, cd.Debit AS Debit_Amt, cd.Credit AS Credit_Amt, cd.LocalDebit AS LocalDebit_Amt, cd.LocalCredit AS LocalCredit_Amt, NULL AS DueDate, 0 AS DueDays, Narration, Remarks, cm.Ref_VNo AS RefNo, cm.Ref_VDate AS RefDate, cm.ReconcileBy AS Reconcile_By, cm.ReconcileDate AS Reconcile_Date, NULL AS Authorize_By, NULL AS Authorize_Date, cm.ClearingBy AS Clearing_By, cm.ClearingDate AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, cm.CheqNo AS Cheque_No, cm.CheqDate AS Cheque_Date, cm.CheqMiti AS Cheque_Miti, NULL AS PartyName, NULL AS PartyLedger_Id, NULL AS Party_PanNo, cm.CUnit_Id AS CmpUnit_ID, cm.FiscalYearId AS FiscalYearId, NULL AS DoctorId, NULL AS PatientId, NULL AS HDepartmentId, cm.CBranch_Id AS Branch_ID, cm.EnterBy AS EnterBy, cm.EnterDate AS EnterDate, cd.SyncBaseId, cd.SyncGlobalId, cd.SyncOriginId, cd.SyncCreatedOn, cd.SyncLastPatchedOn, ISNULL(cd.SyncRowVersion, 1) AS SyncRowVersion
            FROM AMS.CB_Details AS cd
            INNER JOIN AMS.CB_Master AS cm ON cm.Voucher_No = cd.Voucher_No
            WHERE (cm.IsReverse=0 OR cm.IsReverse IS NULL)";
        query += CbMaster.Voucher_No.IsValueExits() ? $" AND cd.Voucher_No ='{CbMaster.Voucher_No}'  " : " ";
        query += @"
                    ) AS ACCTran;";
        var exe = SqlExtensions.ExecuteNonTrans(query);
        return exe;
    }

    #endregion --------------- IUD EVENT  ---------------


    //FINANCE TRANSACTION POSTING

    #region --------------- FINANCE TRANSACTION POSTING  ---------------

    public async Task<int> SyncLedgerOpeningAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;
            if (GetOpening.GetView != null)
            {
                getUrl = @$"{configParams.Model.Item2}LedgerOpening/GetLedgerOpeningById";
                insertUrl = @$"{configParams.Model.Item2}LedgerOpening/InsertLedgerOpeningList";
                updateUrl = @$"{configParams.Model.Item2}LedgerOpening/UpdateLedgerOpeningList";
                deleteUrl = @$"{configParams.Model.Item2}LedgerOpening/DeleteLedgerOpeningAsync?id=" +
                            GetOpening.Opening_Id;
            }
            else
            {
                getUrl = @$"{configParams.Model.Item2}LedgerOpening/GetLedgerOpeningById";
                insertUrl = @$"{configParams.Model.Item2}LedgerOpening/InsertLedgerOpening";
                updateUrl = @$"{configParams.Model.Item2}LedgerOpening/UpdateLedgerOpening";
                deleteUrl = @$"{configParams.Model.Item2}LedgerOpening/DeleteLedgerOpeningAsync?id=" +
                            GetOpening.Opening_Id;
            }

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var ledgerOpeningRepo = DataSyncProviderFactory.GetRepository<LedgerOpening>(DataSyncManager.GetGlobalInjectData());
            if (actionTag is "SAVE" or "UPDATE")
            {
                if (GetOpening.GetView != null)
                {
                    if (GetOpening.GetView != null)
                    {
                        var loList = new List<LedgerOpening>();
                        for (var i = 0; i < GetOpening.GetView.Rows.Count; i++)
                        {
                            var ledgerId = GetOpening.GetView.Rows[i].Cells["GTxtLedgerId"].Value.GetLong();
                            if (ledgerId is 0)
                            {
                                continue;
                            }

                            var subLedgerId = GetOpening.GetView.Rows[i].Cells["GTxtSubledgerId"].Value.GetInt();
                            var agentId = GetOpening.GetView.Rows[i].Cells["GTxtAgentId"].Value.GetInt();
                            var departmentId = GetOpening.GetView.Rows[i].Cells["GTxtDepartmentId"].Value.GetInt();
                            var currencyId = GetOpening.GetView.Rows[i].Cells["GTxtCurrencyId"].Value.GetInt();
                            var currencyRate = GetOpening.GetView.Rows[i].Cells["GTxtExchangeRate"].Value.GetDecimal();
                            var narration = GetOpening.GetView.Rows[i].Cells["GTxtNarration"].Value.GetTrimReplace();

                            var lo = new LedgerOpening
                            {
                                Opening_Id = GetOpening.Opening_Id,
                                Module = GetOpening.Module,
                                Serial_No = GetOpening.GetView.Rows[i].Cells["GTxtSNo"].Value.GetInt(),
                                Voucher_No = GetOpening.Voucher_No,
                                OP_Date = Convert.ToDateTime(GetOpening.OP_Date.ToString("yyyy-MM-dd")),
                                OP_Miti = GetOpening.OP_Miti,
                                Ledger_Id = ledgerId,
                                Subledger_Id = subLedgerId > 0 ? subLedgerId : null,
                                Agent_Id = agentId > 0 ? agentId : null,
                                Cls1 = departmentId > 0 ? departmentId : null,
                                Cls2 = null,
                                Cls3 = null,
                                Cls4 = null,
                                Currency_Id = currencyId > 0 ? currencyId : ObjGlobal.SysCurrencyId,
                                Currency_Rate = currencyRate > 0 ? currencyRate : 1,
                                Debit = GetOpening.GetView.Rows[i].Cells["GTxtDebit"].Value.GetDecimal(),
                                LocalDebit = GetOpening.GetView.Rows[i].Cells["GTxtLocalDebit"].Value.GetDecimal(),
                                Credit = GetOpening.GetView.Rows[i].Cells["GTxtCredit"].Value.GetDecimal(),
                                LocalCredit = GetOpening.GetView.Rows[i].Cells["GTxtLocalCredit"].Value.GetDecimal(),
                                Narration = narration.IsValueExits() ? narration : null,
                                Remarks = GetOpening.Remarks.IsValueExits()
                                    ? GetOpening.Remarks.Trim().Replace("'", "''")
                                    : null,
                                Enter_By = ObjGlobal.LogInUser,
                                Enter_Date = DateTime.Now,
                                Reconcile_By = null,
                                Reconcile_Date = null,
                                Branch_Id = ObjGlobal.SysBranchId,
                                Company_Id = ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? ObjGlobal.SysCompanyUnitId : null,
                                FiscalYearId = ObjGlobal.SysFiscalYearId,
                                IsReverse = false,
                                SyncRowVersion = GetOpening.SyncRowVersion
                            };
                            loList.Add(lo);
                        }

                        var result = actionTag.ToUpper() switch
                        {
                            "SAVE" => await (ledgerOpeningRepo?.PushNewListAsync(loList)),
                            "UPDATE" => await (ledgerOpeningRepo?.PutNewListAsync(loList)),
                            "DELETE" => await (ledgerOpeningRepo?.DeleteNewAsync()),
                            _ => await (ledgerOpeningRepo?.PushNewListAsync(loList))
                        };
                    }
                    else
                    {
                        var lo = new LedgerOpening
                        {
                            Opening_Id = GetOpening.Opening_Id,
                            Module = GetOpening.Module,
                            Serial_No = 1,
                            Voucher_No = GetOpening.Voucher_No,
                            OP_Date = Convert.ToDateTime(GetOpening.OP_Date.ToString("yyyy-MM-dd")),
                            OP_Miti = GetOpening.OP_Miti,
                            Ledger_Id = GetOpening.Ledger_Id,
                            Subledger_Id = GetOpening.Subledger_Id > 0 ? GetOpening.Subledger_Id : null,
                            Agent_Id = GetOpening.Agent_Id > 0 ? GetOpening.Agent_Id : null,
                            Cls1 = GetOpening.Cls1 > 0 ? GetOpening.Cls1 : null,
                            Cls2 = null,
                            Cls3 = null,
                            Cls4 = null,
                            Currency_Id = GetOpening.Currency_Id > 0 ? GetOpening.Currency_Id : ObjGlobal.SysCurrencyId,
                            Currency_Rate = GetOpening.Currency_Rate > 0 ? GetOpening.Currency_Rate : 1,
                            Debit = GetOpening.Debit.GetDecimal(),
                            LocalDebit = GetOpening.LocalDebit.GetDecimal(),
                            Credit = GetOpening.Credit.GetDecimal(),
                            LocalCredit = GetOpening.LocalCredit.GetDecimal(),
                            Narration = GetOpening.Narration.Trim().Replace("'", "''"),
                            Remarks = null,
                            Enter_By = ObjGlobal.LogInUser,
                            Enter_Date = DateTime.Now,
                            Reconcile_By = null,
                            Reconcile_Date = null,
                            Branch_Id = ObjGlobal.SysBranchId,
                            Company_Id = ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? ObjGlobal.SysCompanyUnitId : null,
                            FiscalYearId = ObjGlobal.SysFiscalYearId,
                            IsReverse = false,
                            SyncRowVersion = GetOpening.SyncRowVersion
                        };

                        var result = actionTag.ToUpper() switch
                        {
                            "SAVE" => await ledgerOpeningRepo?.PushNewAsync(lo)!,
                            "UPDATE" => await ledgerOpeningRepo?.PutNewAsync(lo)!,
                            "DELETE" => await ledgerOpeningRepo?.DeleteNewAsync()!,
                            _ => await ledgerOpeningRepo?.PushNewAsync(lo)!
                        };
                    }
                }
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveCashClosing(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(@" 
            INSERT INTO AMS.CashClosing (EnterBy, EnterDate, EnterMiti, EnterTime, CB_Balance, Cash_Sales, Cash_Purchase, TotalCash, ThauQty, ThouVal, FivHunQty, FivHunVal, HunQty, HunVal, FiFtyQty, FiftyVal, TwenteyFiveQty, TwentyFiveVal, TwentyQty, TwentyVal, TenQty, TenVal, FiveQty, FiveVal, TwoQty, TwoVal, OneQty, OneVal, Cash_Diff, Module, HandOverUser, Remarks)");
            cmdString.Append($@" 
            VALUES('{CashMaster.EnterBy}', GETDATE(), '{CashMaster.EnterMiti}', GETDATE(), N'{CashMaster.CB_Balance}', {CashMaster.Cash_Sales}, {CashMaster.Cash_Purchase},{CashMaster.TotalCash},");
            cmdString.Append($"{CashMaster.ThauQty}, {CashMaster.ThouVal},");
            cmdString.Append($"{CashMaster.FivHunQty}, {CashMaster.FivHunVal},");
            cmdString.Append($"{CashMaster.HunQty}, {CashMaster.HunVal}, ");
            cmdString.Append($"{CashMaster.FiFtyQty}, {CashMaster.FiftyVal},");
            cmdString.Append($"{CashMaster.TwenteyFiveQty}, {CashMaster.TwentyFiveVal},");
            cmdString.Append($"{CashMaster.TwentyQty}, {CashMaster.TwentyVal},");
            cmdString.Append($"{CashMaster.TenQty}, {CashMaster.TenVal},");
            cmdString.Append($"{CashMaster.FiveQty}, {CashMaster.FiveVal},");
            cmdString.Append($"{CashMaster.TwoQty},{CashMaster.TwoVal},");
            cmdString.Append($"{CashMaster.OneQty},{CashMaster.OneVal},");
            cmdString.Append($"{CashMaster.Cash_Diff},'{CashMaster.Module}', N'{CashMaster.HandOverUser}', '{CashMaster.Remarks}'); \n");
        }
        else if (actionTag == "UPDATE")
        {
        }
        else if (actionTag == "DELETE")
        {
        }

        return SaveDataInDatabase(cmdString);
    }

    private int SaveDataInDatabase(string getQuery)
    {
        var exe = SqlExtensions.ExecuteNonTrans(getQuery.ToString());
        return exe;
    }

    private int SaveDataInDatabase(StringBuilder getQuery)
    {
        return SaveDataInDatabase(getQuery.ToString());
    }

    #region *---------- AUDIT LOG ----------*

    private void AuditLogJournalVoucher()
    {
        var cmdString = $@"
        INSERT INTO AUD.AUDIT_JV_MASTER(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks, Action_Type, Audit_Lock, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
        SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks,N'{JvMaster.Action_Type}' Action_Type, Audit_Lock, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, N'{JvMaster.Action_Type}' ModifyAction, N'{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
        FROM AMS.JV_Master jm
        WHERE jm.Voucher_No = '{JvMaster.Voucher_No}';

        INSERT INTO AUD.AUDIT_JV_DETAILS(Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CBLedger_Id, Chq_No, Chq_Date, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, CurrencyId, CurrencyRate, ModifyAction, ModifyBy, ModifyDate)
        SELECT Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CBLedger_Id, Chq_No, Chq_Date, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, CurrencyId, CurrencyRate, N'{JvMaster.Action_Type}' ModifyAction,N'{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
        FROM AMS.JV_Details jd
        WHERE jd.Voucher_No ='{JvMaster.Voucher_No}'; ";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }

    #endregion *---------- AUDIT LOG ----------*

    public int SaveUnSyncJournalVoucherFromServerAsync(JV_Master journalVoucherModel)
    {
        var query = $"SELECT Voucher_No FROM AMS.JV_Master WHERE Voucher_No='{journalVoucherModel.Voucher_No}'";
        var queryData = GetConnection.GetQueryData(query);
        JvMaster.Action_Type = journalVoucherModel.Action_Type;
        var actionTag = string.IsNullOrEmpty(queryData) ? "SAVE" : "UPDATE";
        JvMaster.VoucherMode = journalVoucherModel.VoucherMode;
        JvMaster.Voucher_No = journalVoucherModel.Voucher_No;
        JvMaster.Voucher_Date = journalVoucherModel.Voucher_Date;
        JvMaster.Voucher_Miti = journalVoucherModel.Voucher_Miti;
        JvMaster.Voucher_Time = journalVoucherModel.Voucher_Time;
        JvMaster.Ref_VNo = journalVoucherModel.Ref_VNo;
        JvMaster.Ref_VDate = journalVoucherModel.Ref_VDate;
        JvMaster.Currency_Id = journalVoucherModel.Currency_Id;
        JvMaster.Currency_Rate = journalVoucherModel.Currency_Rate;
        JvMaster.Cls1 = journalVoucherModel.Cls1;
        JvMaster.Cls2 = journalVoucherModel.Cls2;
        JvMaster.Cls3 = journalVoucherModel.Cls3;
        JvMaster.Cls4 = journalVoucherModel.Cls4;
        JvMaster.N_Amount = journalVoucherModel.N_Amount;
        JvMaster.Remarks = journalVoucherModel.Remarks;
        JvMaster.EnterBy = journalVoucherModel.EnterBy;
        JvMaster.EnterDate = journalVoucherModel.EnterDate;
        JvMaster.Audit_Lock = journalVoucherModel.Audit_Lock;
        JvMaster.ReconcileBy = journalVoucherModel.ReconcileBy;
        JvMaster.ReconcileDate = journalVoucherModel.ReconcileDate;
        JvMaster.ClearingBy = journalVoucherModel.ClearingBy;
        JvMaster.ClearingDate = journalVoucherModel.ClearingDate;
        JvMaster.PrintValue = journalVoucherModel.PrintValue;
        JvMaster.CBranch_Id = journalVoucherModel.CBranch_Id;
        JvMaster.CUnit_Id = journalVoucherModel.CUnit_Id;
        JvMaster.FiscalYearId = journalVoucherModel.FiscalYearId;
        JvMaster.PAttachment1 = journalVoucherModel.PAttachment1;
        JvMaster.PAttachment2 = journalVoucherModel.PAttachment2;
        JvMaster.PAttachment3 = journalVoucherModel.PAttachment3;
        JvMaster.PAttachment4 = journalVoucherModel.PAttachment4;
        JvMaster.PAttachment5 = journalVoucherModel.PAttachment5;
        JvMaster.SyncRowVersion = journalVoucherModel.SyncRowVersion;
        var cmdString = new StringBuilder();
        if (_tagStrings.Contains(actionTag))
        {
            cmdString.Append($"DELETE AMS.AccountDetails WHERE Voucher_No = '{JvMaster.Voucher_No}' AND Module = 'JV' \n");
            if (actionTag != "REVERSE")
            {
                cmdString.Append($"DELETE AMS.JV_Details WHERE Voucher_No = '{JvMaster.Voucher_No}' \n");
            }

            if (actionTag is "DELETE")
            {
                cmdString.Append($"DELETE AMS.JV_Master WHERE Voucher_No = '{JvMaster.Voucher_No}' \n");
            }

            if (actionTag != "UPDATE")
            {
                AuditLogJournalVoucher();
            }
        }

        if (actionTag is "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.JV_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks, Action_Type, EnterBy, EnterDate, Audit_Lock, IsReverse, CancelBy, CancelDate, CancelReason, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append("VALUES ( ");
            cmdString.Append(JvMaster.VoucherMode.IsValueExits() ? $"'{JvMaster.VoucherMode}', " : "'MDMC'");
            cmdString.Append(
                $"N'{JvMaster.Voucher_No}', '{JvMaster.Voucher_Date.GetSystemDate()}', N'{JvMaster.Voucher_Miti}', GETDATE(),");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"'{JvMaster.Ref_VNo}', " : "NULL,");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"'{JvMaster.Ref_VDate.GetSystemDate()}', " : "NULL,");
            cmdString.Append(JvMaster.Currency_Id > 0 ? $"'{JvMaster.Currency_Id}', " : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append(JvMaster.Currency_Rate > 0 ? $"'{JvMaster.Currency_Rate}', " : "1,");
            cmdString.Append(JvMaster.Cls1 > 0 ? $"'{JvMaster.Cls1}', " : "NULL,");
            cmdString.Append($" NULL, NULL, NULL, {JvMaster.N_Amount.GetDecimal()},");
            cmdString.Append(JvMaster.Remarks.IsValueExits()
                ? $"'{JvMaster.Remarks.Trim().GetTrimReplace()}',"
                : " NULL,");
            cmdString.Append(
                $"'SAVE', N'{ObjGlobal.LogInUser.ToUpper()}', GETDATE(), 0,0, NULL,NULL, NULL, NULL,NULL, NULL, NULL, 0, {ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($" {ObjGlobal.SysFiscalYearId}, NULL, NULL, NULL, NULL, NULL, \n");
            cmdString.Append($" NULL, NULL, NULL, NULL, NULL,{JvMaster.SyncRowVersion.GetDecimal(true)} ); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append("UPDATE AMS.JV_Master SET ");
            cmdString.Append(
                $"Voucher_Date = '{JvMaster.Voucher_Date.GetSystemDate()}', Voucher_Miti = N'{JvMaster.Voucher_Miti}',");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"Ref_VNo = '{JvMaster.Ref_VNo}', " : "Ref_VNo = NULL,");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits()
                ? $"Ref_VDate = '{JvMaster.Ref_VDate.GetSystemDate()}', "
                : "Ref_VDate = NULL,");
            cmdString.Append(JvMaster.Currency_Id > 0
                ? $"Currency_Id = '{JvMaster.Currency_Id}', "
                : $"Currency_Id = {ObjGlobal.SysCurrencyId},");
            cmdString.Append(JvMaster.Currency_Rate > 0
                ? $"Currency_Rate = '{JvMaster.Currency_Rate}', "
                : "Currency_Rate= 1,");
            cmdString.Append(JvMaster.Cls1 > 0 ? $"Cls1 = '{JvMaster.Cls1}', " : "Cls1 = NULL,");
            cmdString.Append(JvMaster.Remarks.IsValueExits() ? $"Remarks = '{JvMaster.Remarks}', " : "Remarks = NULL,");
            cmdString.Append($"SyncRowVersion =  {JvMaster.SyncRowVersion},");
            cmdString.Append($"Action_Type = 'UPDATE', IsSynced=0 WHERE Voucher_No = N'{JvMaster.Voucher_No}' ; \n");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            cmdString.Append(
                " INSERT INTO AMS.JV_Details(Voucher_No, SNo, CBLedger_Id, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Chq_No, Chq_Date, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append("VALUES  \n");
            var iRows = 0;
            foreach (var drCel in journalVoucherModel.DetailsList)
            {
                iRows++;
                cmdString.Append($"(N'{JvMaster.Voucher_No}',");
                cmdString.Append($"'{drCel.SNo}',NULL,"); //SNo
                cmdString.Append($"'{drCel.Ledger_Id}',"); //Ledger_Id
                cmdString.Append(drCel.Subledger_Id.GetInt() > 0 ? $"'{drCel.Subledger_Id}'," : "NULL,"); //Ledger_Id
                cmdString.Append(drCel.Agent_Id.GetInt() > 0 ? $"'{drCel.Agent_Id}'," : "NULL,"); //Ledger_Id
                cmdString.Append(drCel.Cls1.GetInt() > 0 ? $"'{drCel.Cls1}'," : "NULL,"); //Ledger_Id
                cmdString.Append(" NULL,NULL,NULL,NULL,NULL,"); //Ledger_Id
                cmdString.Append(drCel.CurrencyId.GetInt() > 0
                    ? $" {drCel.CurrencyId}, "
                    : $"{ObjGlobal.SysCurrencyId},"); //Ledger_Id
                cmdString.Append($" {drCel.CurrencyRate.GetDecimalString(true)}, "); //Ledger_Id
                cmdString.Append($" {drCel.Debit.GetDecimal()}, "); //Ledger_Id
                cmdString.Append($" {drCel.Credit.GetDecimal()}, "); //Ledger_Id
                cmdString.Append($" {drCel.LocalDebit.GetDecimal()}, "); //Ledger_Id
                cmdString.Append($" {drCel.LocalCredit.GetDecimal()} ,"); //Ledger_Id
                cmdString.Append($" N'{drCel.Narration.Replace("'", "''")}', "); //Ledger_Id
                cmdString.Append(
                    $" 0,0,0,NULL,NULL,NULL,NUll,NUll,NULL,NULL,NULL,NUll,NUll, {JvMaster.SyncRowVersion.GetDecimal(true)}"); //Ledger_Id
                cmdString.Append(iRows == journalVoucherModel.DetailsList.Count ? " ); \n" : "), \n");
            }
        }

        var saveData = SaveDataInDatabase(cmdString);
        if (saveData <= 0)
        {
            return saveData;
        }

        Task.Run(() => UpdateSyncJournalVoucherColumnInServerAsync(actionTag));
        if (actionTag.Equals("DELETE"))
        {
            return saveData;
        }

        AuditLogJournalVoucher();
        if (JvMaster.VoucherMode != "PROV")
        {
            JournalVoucherAccountPosting();
        }

        return saveData;
    }

    public async Task<int> UpdateSyncJournalVoucherColumnInServerAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}JournalVoucher/GetJournalVoucherById";
            insertUrl = @$"{configParams.Model.Item2}JournalVoucher/InsertJournalVoucher";
            updateUrl = @$"{configParams.Model.Item2}JournalVoucher/UpdateSyncJournalVoucherColumn";
            deleteUrl = @$"{configParams.Model.Item2}JournalVoucher/DeleteJournalVoucherAsync?id=" +
                        JvMaster.Voucher_No;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var journalVoucherRepo =
                DataSyncProviderFactory.GetRepository<JV_Master>(DataSyncManager.GetGlobalInjectData());
            var jv = new JV_Master
            {
                Action_Type = "UPDATE",
                Voucher_No = JvMaster.Voucher_No,
                Voucher_Date = JvMaster.Voucher_Date,
                Voucher_Time = JvMaster.Voucher_Time
            };
            var result = jv.Action_Type.ToUpper() switch
            {
                "SAVE" => await (journalVoucherRepo?.PushNewAsync(jv)),
                "NEW" => await (journalVoucherRepo?.PushNewAsync(jv)),
                "UPDATE" => await (journalVoucherRepo?.PutNewAsync(jv)),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (journalVoucherRepo?.PushNewAsync(jv))
            };

            return 1;
        }
        catch (Exception ex)
        {
            //CreateDatabaseTable.CreateTrigger();
            return 1;
        }
    }

    public async Task<int> UpdateSyncCashBankVoucherColumnInServerAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}CashBankVoucher/GetCashBankVoucherById";
            insertUrl = @$"{configParams.Model.Item2}CashBankVoucher/InsertCashBankVoucher";
            updateUrl = @$"{configParams.Model.Item2}CashBankVoucher/UpdateSyncCashBankVoucherColumn";
            deleteUrl = @$"{configParams.Model.Item2}CashBankVoucher/DeleteCashBankVoucherAsync?id=" +
                        CbMaster.Voucher_No;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var cashBankVoucherRepo =
                DataSyncProviderFactory.GetRepository<CB_Master>(DataSyncManager.GetGlobalInjectData());
            var cv = new CB_Master
            {
                Action_Type = "UPDATE",
                Voucher_No = JvMaster.Voucher_No,
                Voucher_Date = JvMaster.Voucher_Date,
                Voucher_Time = JvMaster.Voucher_Time
            };
            var result = cv.Action_Type.ToUpper() switch
            {
                "SAVE" => await (cashBankVoucherRepo?.PushNewAsync(cv)),
                "NEW" => await (cashBankVoucherRepo?.PushNewAsync(cv)),
                "UPDATE" => await (cashBankVoucherRepo?.PutNewAsync(cv)),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (cashBankVoucherRepo?.PushNewAsync(cv))
            };

            return 1;
        }
        catch (Exception ex)
        {
            //CreateDatabaseTable.CreateTrigger();
            return 1;
        }
    }

    public int SaveNotesVoucher(string actionTag)
    {
        //var cmdString = new StringBuilder();
        //if (VmMaster.ActionTag is "DELETE" or "UPDATE")
        //{
        //    cmdString.Append(
        //        $"DELETE FROM AMS.AccountDetails WHERE Voucher_No='{VmMaster.TxtVoucherNo}' AND Module='CB'; \n");
        //    cmdString.Append($"DELETE FROM AMS.CB_Details WHERE Voucher_No='{VmMaster.TxtVoucherNo}'; \n");
        //    if (VmMaster.ActionTag is "DELETE")
        //    {
        //        cmdString.Append($"DELETE FROM AMS.CB_Master WHERE Voucher_No='{VmMaster.TxtVoucherNo}'; \n");
        //    }
        //}

        //if (VmMaster.ActionTag is "SAVE")
        //{
        //    cmdString.Append(
        //        "INSERT INTO AMS.CB_Master (VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id,CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId) \n");
        //    cmdString.Append("VALUES ( \n");
        //    cmdString.Append(VmMaster.TxtVoucherMode.IsValueExits() ? $" N'{VmMaster.TxtVoucherMode}', " : "N'N',");
        //    cmdString.Append(VmMaster.TxtVoucherNo.IsValueExits() ? $" N'{VmMaster.TxtVoucherNo}', " : "Null,");
        //    cmdString.Append(VmMaster.MskVoucherDate.IsValueExits()
        //        ? $" N'{VmMaster.MskVoucherDate}', "
        //        : "GETDATE(),");
        //    cmdString.Append(VmMaster.MskVoucherMiti.IsValueExits() ? $" N'{VmMaster.MskVoucherMiti}'," : "Null,");
        //    cmdString.Append(" GETDATE(), ");
        //    cmdString.Append(VmMaster.TxtRefVoucherNo.IsValueExits() ? $" '{VmMaster.TxtRefVoucherNo}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtRefVoucherNo.IsValueExits() ? $" '{VmMaster.MskRefVoucherDate}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtVoucherType.IsValueExits() ? $" '{VmMaster.TxtVoucherType}'," : "Null,");
        //    cmdString.Append(VmMaster.LedgerId > 0 ? $" '{VmMaster.LedgerId}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits() ? $" '{VmMaster.TxtChequeNo}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits() ? $" '{VmMaster.MskChequeDate}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits() ? $" '{VmMaster.MskChequeMiti}'," : "Null,");
        //    cmdString.Append(VmMaster.CurrencyId > 0 ? $" '{VmMaster.CurrencyId}'," : "Null,");
        //    cmdString.Append(VmMaster.CurrencyRate > 0 ? $" '{VmMaster.CurrencyRate}'," : "1,");
        //    cmdString.Append(VmMaster.Department > 0 ? $" '{VmMaster.Department}'," : "NUll ,");
        //    cmdString.Append("NUll, NUll, NUll,");
        //    cmdString.Append(VmMaster.TxtRemarks.IsValueExits() ? $" '{VmMaster.TxtRemarks}'," : "Null,");
        //    cmdString.Append(VmMaster.TxtActionType.IsValueExits() ? $" '{VmMaster.TxtActionType}'," : "'NEW',");
        //    cmdString.Append(VmMaster.TxtEnterBy.IsValueExits() ? $" '{VmMaster.TxtEnterBy}'," : "'MrSolution',");
        //    cmdString.Append(" GETDATE(), ");
        //    cmdString.Append(VmMaster.TxtReconcileBy.IsValueExits() ? $" '{VmMaster.TxtReconcileBy}'," : "NUll,");
        //    cmdString.Append(VmMaster.TxtReconcileBy.IsValueExits() ? $" '{VmMaster.MskReconcileDate}'," : "NUll,");
        //    cmdString.Append(VmMaster.AuditLock ? " 1 ," : "0,");
        //    cmdString.Append(VmMaster.TxtClearedBy.IsValueExits() ? $" '{VmMaster.TxtClearedBy}'," : "NUll,");
        //    cmdString.Append(VmMaster.TxtClearedBy.IsValueExits() ? $" '{VmMaster.MskClearedDate}'," : "NUll,");
        //    cmdString.Append("0,");
        //    cmdString.Append(VmMaster.BranchId > 0 ? $" '{VmMaster.BranchId}'," : "NUll ,");
        //    cmdString.Append(VmMaster.CompanyUnitId > 0 ? $" '{VmMaster.CompanyUnitId}'," : "NUll ,");
        //    cmdString.Append(VmMaster.FiscalYearId > 0 ? $" '{VmMaster.FiscalYearId}' \n" : "NUll \n");
        //    cmdString.Append(" );");
        //}
        //else if (VmMaster.ActionTag is "UPDATE")
        //{
        //    cmdString.Append(" UPDATE AMS.CB_Master SET ");
        //    cmdString.Append(VmMaster.MskVoucherDate.IsValueExits()
        //        ? $" Voucher_Date = '{VmMaster.MskVoucherDate}' "
        //        : "Voucher_Date = NULL,  ");
        //    cmdString.Append(VmMaster.MskVoucherMiti.IsValueExits()
        //        ? $" Voucher_Miti = N'{VmMaster.MskVoucherMiti}',"
        //        : "Voucher_Miti = Null,");
        //    cmdString.Append(VmMaster.TxtRefVoucherNo.IsValueExits()
        //        ? $" Ref_VNo = '{VmMaster.TxtRefVoucherNo}',"
        //        : "Ref_VNo = Null,");
        //    cmdString.Append(VmMaster.TxtRefVoucherNo.IsValueExits()
        //        ? $" Ref_VDate = '{VmMaster.MskRefVoucherDate}',"
        //        : "Ref_VDate = Null,");
        //    cmdString.Append(VmMaster.TxtVoucherType.IsValueExits()
        //        ? $" VoucherType = '{VmMaster.TxtVoucherType}',"
        //        : "VoucherType = Null,");
        //    cmdString.Append(VmMaster.LedgerId > 0 ? $" Ledger_Id = '{VmMaster.LedgerId}'," : "Ledger_Id= Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits()
        //        ? $" CheqNo= '{VmMaster.TxtChequeNo}',"
        //        : "CheqNo = Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits()
        //        ? $" CheqDate = '{VmMaster.MskChequeDate}',"
        //        : "CheqDate = Null,");
        //    cmdString.Append(VmMaster.TxtChequeNo.IsValueExits()
        //        ? $"CheqMiti=  '{VmMaster.MskChequeMiti}',"
        //        : "CheqMiti = Null,");
        //    cmdString.Append(VmMaster.CurrencyId > 0
        //        ? $"Currency_Id=  '{VmMaster.CurrencyId}',"
        //        : "Currency_Id= Null,");
        //    cmdString.Append(VmMaster.CurrencyRate > 0
        //        ? $" Currency_Rate = '{VmMaster.CurrencyRate}',"
        //        : "Currency_Rate = 1,");
        //    cmdString.Append(VmMaster.Department > 0 ? $" Cls1 = '{VmMaster.Department}'," : " Cls1 = NUll ,");
        //    cmdString.Append(VmMaster.TxtRemarks.IsValueExits()
        //        ? $"Remarks = N'{VmMaster.TxtRemarks}',"
        //        : " Remarks = Null,");
        //    cmdString.Append(VmMaster.TxtActionType.IsValueExits()
        //        ? $"Action_Type=  '{VmMaster.TxtActionType}' \n"
        //        : " Action_Type = 'UPDATE' \n");
        //    cmdString.Append($"WHERE Voucher_No = N'{VmMaster.TxtVoucherNo}';");
        //}

        //if (VmMaster.ActionTag != "DELETE")
        //{
        //    cmdString.Append(
        //        "INSERT INTO AMS.CB_Details (Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg)  \n");
        //    cmdString.Append(" VALUES  \n");

        //    for (var i = 0; i < ObjDetails.Count; i++)
        //    {
        //        var j = ObjDetails.Count - 1;
        //        foreach (var item in ObjDetails)
        //        {
        //            cmdString.Append("(  \n");
        //            if (i is 0 && ObjDetails.Count is 1)
        //            {
        //                cmdString.Append(" ) \n");
        //            }

        //            if (i is 0 && ObjDetails.Count > 1 && i <= j)
        //            {
        //                cmdString.Append(" ), \n");
        //            }
        //            else
        //            {
        //                cmdString.Append(" ); \n");
        //            }
        //        }
        //    }
        //}

        return SaveDataInDatabase("");
    }

    public int UpdateDocumentNumbering(string module, string docDesc)
    {
        var cmdTxt =
            $"Update AMS.DocumentNumbering set DocCurr= isNull(DocCurr,0) +1  where DocModule='{module}' and  DocDesc='{docDesc}'";
        var iUpdate = SqlExtensions.ExecuteNonQuery(cmdTxt);
        return iUpdate;
    }

    #endregion --------------- FINANCE TRANSACTION POSTING  ---------------


    //FINANCE ACCOUNT POSTING

    #region --------------- ACCOUNT DETAILS POSTING  ---------------

    public int PdcAccountPosting()
    {
        var query = @"
			DELETE FROM  AMS.AccountDetails WHERE Module='PDC' ";
        query += PdcMaster.VoucherNo.IsValueExits() ? $" AND Voucher_No= '{PdcMaster.VoucherNo}'" : " ";
        query += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId,SyncRowVersion)
            SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId,PDC.SyncRowVersion
            FROM(SELECT 'PDC' Module, 0 Serial_No, pdc.VoucherNo Voucher_No, pdc.VoucherDate Voucher_Date, pdc.VoucherMiti Voucher_Miti, pdc.VoucherTime Voucher_Time, pdc.BankLedgerId Ledger_ID, pdc.LedgerId CbLedger_ID, pdc.SubLedgerId Subleder_ID, pdc.AgentId Agent_ID, pdc.Cls1 Department_ID1, NULL Department_ID2, NULL Department_ID3, NULL Department_ID4, 1 Currency_ID, 1 Currency_Rate, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS Debit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS Credit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS LocalDebit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS LocalCredit_Amt, NULL DueDate, 0 DueDays, pdc.Remarks Narration, pdc.Remarks Remarks, pdc.EnterBy EnterBy, pdc.EnterDate EnterDate, NULL RefNo, NULL RefDate, NULL Reconcile_By, NULL Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, pdc.ChequeNo Cheque_No, pdc.ChqDate Cheque_Date, pdc.ChqMiti Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, pdc.BranchId Branch_ID, pdc.CompanyUnitId CmpUnit_ID, pdc.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, pdc.SyncRowVersion
                 FROM AMS.PostDateCheque pdc
                 UNION ALL
                 SELECT 'PDC' Module, 1 Serial_No, pdc.VoucherNo Voucher_No, pdc.VoucherDate Voucher_Date, pdc.VoucherMiti Voucher_Miti, pdc.VoucherTime Voucher_Time, pdc.LedgerId Ledger_ID, pdc.BankLedgerId CbLedger_ID, pdc.SubLedgerId Subleder_ID, pdc.AgentId Agent_ID, pdc.Cls1 Department_ID1, NULL Department_ID2, NULL Department_ID3, NULL Department_ID4, 1 Currency_ID, 1 Currency_Rate, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS Debit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS Credit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS LocalDebit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS LocalCredit_Amt, NULL DueDate, 0 DueDays, pdc.Remarks Narration, pdc.Remarks Remarks, pdc.EnterBy EnterBy, pdc.EnterDate EnterDate, NULL RefNo, NULL RefDate, NULL Reconcile_By, NULL Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, pdc.ChequeNo Cheque_No, pdc.ChqDate Cheque_Date, pdc.ChqMiti Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, pdc.BranchId Branch_ID, pdc.CompanyUnitId CmpUnit_ID, pdc.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId,pdc.SyncRowVersion
                 FROM AMS.PostDateCheque pdc) AS PDC";
        query += PdcMaster.VoucherNo.IsValueExits() ? $" WHERE PDC.Voucher_No = '{PdcMaster.VoucherNo}'  \n" : " ";
        query += @" 
        DELETE FROM AMS.AccountDetails WHERE Module='PDC' and Voucher_No IN (SELECT cm.Ref_VNo FROM AMS.CB_Master cm WHERE cm.Ref_VNo IS NOT NULL AND cm.Ref_VNo <> '') ";
        query += PdcMaster.VoucherNo.IsValueExits()
            ? ""
            : "UPDATE AMS.PostDateCheque SET Status = 'Due' where Status = 'Deposit' and VoucherNo NOT IN (SELECT cm.Ref_VNo FROM AMS.CB_Master cm WHERE cm.Ref_VNo IS NOT NULL AND cm.Ref_VNo <> '')";
        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
    }

    public int LedgerOpeningAccountPosting()
    {
        var query = @"
			Delete from [AMS].[AccountDetails]  Where [Module] IN ('N','OB','LOB') ";
        query += GetOpening.Voucher_No.IsValueExits() ? $" and [Voucher_No] = '{GetOpening.Voucher_No}' " : " ";
        query += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Ledger_ID, Voucher_Date, Voucher_Miti, Voucher_Time, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, Branch_ID, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            Select 'LOB' Module, Serial_No, Voucher_No, Ledger_ID,LOB.OP_Date Voucher_Date,LOB.OP_Miti Voucher_Miti,LOB.Enter_Date Voucher_Time,Ledger_ID CbLedger_ID,LOB.Subledger_Id Subleder_ID,LOB.Agent_Id Agent_ID,LOB.Cls1 Department_ID1,LOB.Cls2 Department_ID2,LOB.Cls3 Department_ID3,LOB.Cls4 Department_ID4,LOB.Currency_Id Currency_ID,LOB.Currency_Rate Currency_Rate,LOB.Debit Debit_Amt,LOB.Credit Credit_Amt,LOB.LocalDebit LocalDebit_Amt,LOB.LocalCredit LocalCredit_Amt,NULL DueDate,0 DueDays, Narration, Remarks,NULL RefNo,NULL RefDate, Reconcile_By, Reconcile_Date,NULL Authorize_By,NULL Authorize_Date,NULL Clearing_By,NULL Clearing_Date,NULL Posted_By,NULL Posted_Date,NULL Cheque_No,NULL Cheque_Date,NULL Cheque_Miti,NULL PartyName,NULL PartyLedger_Id,NULL Party_PanNo,LOB.Company_Id CmpUnit_ID, FiscalYearId,NULL DoctorId,NULL PatientId,NULL HDepartmentId, Branch_ID,LOB.Enter_By EnterBy,LOB.Enter_Date EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM AMS.LedgerOpening as LOB ";
        query += GetOpening.Voucher_No.IsValueExits() ? $@"where LOB.Voucher_No = '{GetOpening.Voucher_No}'" : "";
        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
    }

    public int JournalVoucherAccountPosting()
    {
        var query = @"Delete from  AMS.AccountDetails Where Module in ('JV') ";
        if (((object)JvMaster.Voucher_No).IsValueExits())
        {
            query += $" and Voucher_No='{JvMaster.Voucher_No}' ";
        }

        query += @"
			INSERT INTO AMS.AccountDetails (Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId)
             SELECT 'JV' AS Module, SNo Serial_No, JVM.Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, JVD.Ledger_Id, CBLedger_Id, Subledger_Id, Agent_Id, ISNULL(JVM.Cls1, JVD.Cls1), ISNULL(JVM.Cls2, JVD.Cls2), ISNULL(JVM.Cls3, JVD.Cls3), ISNULL(JVM.Cls4, JVD.Cls4), ISNULL(JVD.CurrencyId,JVM.Currency_Id) Currency_Id,ISNULL(JVD.CurrencyRate,JVM.Currency_Rate) Currency_Rate, Debit, Credit, CASE WHEN LocalDebit=0 THEN Debit ELSE LocalDebit END, CASE WHEN LocalCredit=0 THEN Credit ELSE LocalCredit END, NULL DueDate, NULL DueDays, Narration, Remarks, EnterBy, EnterDate, Ref_VNo, Ref_VDate, ReconcileBy, ReconcileDate, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL PostedDate, Chq_No, Chq_Date, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, CBranch_Id, CUnit_Id, JVM.FiscalYearId
              FROM AMS.JV_Master JVM, AMS.JV_Details JVD
              WHERE JVM.Voucher_No = JVD.Voucher_No AND JVM.Action_Type <> 'Cancel' ";
        if (((object)JvMaster.Voucher_No).IsValueExits())
        {
            query += $" and JVM.Voucher_No = '{JvMaster.Voucher_No}' ";
        }

        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
    }

    public int NotesVoucherAccountPosting(string mode)
    {
        var cmdString = string.Empty;
        if (mode is "DN")
        {
            cmdString = @"
				Delete from  AMS.AccountDetails Where Module in ('DN') ";
            if (NMaster.Voucher_No != null && !string.IsNullOrEmpty(NMaster.Voucher_No))
            {
                cmdString += $" AND Voucher_No='{NMaster.Voucher_No}'; ";
            }

            cmdString += @"
				INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId)
				SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId FROM
				(
					Select Nm1.[VoucherMode] Module, 1 as Serial_No, Nm1.[Voucher_No] Voucher_No,[Voucher_Date] Voucher_Date,[Voucher_Miti] Voucher_Miti, [Voucher_Time] Voucher_Time, Nm1.[Ledger_ID] Ledger_ID, NULL CbLedger_Id, NM1.[Subledger_Id] Subleder_ID, NM1.[Agent_ID], Nm1.[Cls1] Department_ID1, Nm1.[Cls2] Department_ID2, Nm1.[Cls3] Department_ID3, Nm1.[Cls4] Department_ID4, [Currency_Id] Currency_ID, [Currency_Rate] Currency_Rate, SUM(ND1.[Debit]) Debit_Amt, SUM(ND1.[Credit]) Credit_Amt, SUM(ND1.LocalDebit) LocalDebit_Amt, SUM(ND1.LocalCredit) LocalCredit_Amt, Null DueDate, Null DueDays, ND1.Narration Narration, NM1.Remarks,[EnterBy] EnterBy, [EnterDate] EnterDate, [Ref_VNo] RefNo, [Ref_VDate] RefDate, [ReconcileBy] Reconcile_By, [ReconcileDate] Reconcile_Date, Null Authorize_By, Null Authorize_Date,[ClearingBy] Clearing_By, ClearingDate Clearing_Date, Null Posted_By, Null Posted_Date,[CheqNo] Cheque_No, [CheqMiti] Cheque_Miti, [CheqDate] Cheque_Date, Null PartyName, NULL PartyLedger_Id, Null Party_PanNo,[CBranch_Id] Branch_ID,[CUnit_Id] CmpUnit_ID,[NM1].[FiscalYearId] FiscalYearId, NULL DoctorId, NUll PatientId, NULL HDepartmentId
					FROM AMS.Notes_Master NM1 Left Outer Join AMS.Notes_Details ND1 on NM1.Voucher_No = ND1.Voucher_No  where Nm1.[VoucherMode] = 'DN'
					Group By Nm1.[VoucherMode], Nm1.[Voucher_No], [Voucher_Date], [Voucher_Miti], [Voucher_Time], Nm1.[Ledger_Id], NM1.[Subledger_ID], NM1.[Agent_ID], Nm1.[Cls1], Nm1.[Cls2], Nm1.[Cls3], Nm1.[Cls4], [Currency_Id], [Currency_Rate], ND1.[Debit], ND1.[Credit], ND1.LocalDebit, ND1.LocalCredit, ND1.Narration, NM1.Remarks, [EnterBy], [EnterDate], [Ref_VNo], [Ref_VDate], [ReconcileBy], [ReconcileDate], [ClearingBy], ClearingDate, [CheqNo], [CheqMiti], [CheqDate], [CBranch_Id], [CUnit_Id], FiscalYearId
						Union All
					Select ND2.VoucherMode Module, ND2.SNo Serial_No, ND2.[Voucher_No] Voucher_No, NM2.[Voucher_Date] Voucher_Date, NM2.[Voucher_Miti] Voucher_Miti, NM2.[Voucher_Time] Voucher_Time, ND2.[Ledger_ID] Ledger_ID, NULL CbLedger_ID, ND2.Subledger_Id Subleder_ID, ND2.[Agent_ID] Agent_ID, ND2.[Cls1] Department_ID1, ND2.[Cls2] Department_ID2, ND2.[Cls3] Department_ID3, ND2.[Cls4] Department_ID4, NM2.[Currency_ID] Currency_ID, NM2.[Currency_Rate] Currency_Rate, ND2.[Credit] Debit_Amt, ND2.[Debit] Credit_Amt, ND2.LocalCredit LocalDebit_Amt, ND2.LocalDebit LocalCredit_Amt, NULL DueDate, NULL DueDays, ND2.Narration Narration, NM2.Remarks Remarks, [EnterBy] EnterBy, [EnterDate] EnterDate, [Ref_VNo] RefNo, [Ref_VDate] RefDate, [ReconcileBy] Reconcile_By, [ReconcileDate] Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, [ClearingBy] Clearing_By, ClearingDate Clearing_Date, NULL Posted_By, NULL Posted_Date, [CheqNo] Cheque_No, [CheqDate] Cheque_Date, [CheqMiti] Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo,[CBranch_Id] Branch_ID, [CUnit_Id] CmpUnit_ID,[NM2].[FiscalYearId] FiscalYearId, NULL DoctorId, NUll PatientId, NULL HDepartmentId
					from AMS.Notes_Details ND2 Left Outer Join AMS.Notes_Master as NM2 on NM2.Voucher_No = ND2.Voucher_No where ND2.[VoucherMode] = 'DN'
				) AcTran ";
            if (NMaster.Voucher_No != null && !string.IsNullOrEmpty(NMaster.Voucher_No))
            {
                cmdString += $" WHERE AcTran.Voucher_No='{NMaster.Voucher_No}'; ";
            }
        }
        else if (mode is "CN")
        {
            cmdString = @"
				Delete from  AMS.AccountDetails Where Module in ('CN') ";
            if (NMaster.Voucher_No != null && !string.IsNullOrEmpty(NMaster.Voucher_No))
            {
                cmdString += $" AND Voucher_No='{NMaster.Voucher_No}'; ";
            }

            cmdString += @"
				INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId)
				SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId
				FROM(
				Select Nm1.[VoucherMode] Module, 0 as Serial_No, Nm1.[Voucher_No] Voucher_No,[Voucher_Date] Voucher_Date,[Voucher_Miti] Voucher_Miti, [Voucher_Time] Voucher_Time, Nm1.[Ledger_ID] Ledger_ID, NULL CbLedger_Id, NM1.[Subledger_Id] Subleder_ID, NM1.[Agent_ID], Nm1.[Cls1] Department_ID1, Nm1.[Cls2] Department_ID2, Nm1.[Cls3] Department_ID3, Nm1.[Cls4] Department_ID4, [Currency_Id] Currency_ID, [Currency_Rate] Currency_Rate, SUM(ND1.[Debit]) Debit_Amt, SUM(ND1.[Credit]) Credit_Amt, SUM(ND1.LocalDebit) LocalDebit_Amt, SUM(ND1.LocalCredit) LocalCredit_Amt, Null DueDate, Null DueDays, ND1.Narration Narration, NM1.Remarks,[EnterBy] EnterBy, [EnterDate] EnterDate, [Ref_VNo] RefNo, [Ref_VDate] RefDate, [ReconcileBy] Reconcile_By, [ReconcileDate] Reconcile_Date, Null Authorize_By, Null Authorize_Date,[ClearingBy] Clearing_By, ClearingDate Clearing_Date, Null Posted_By, Null Posted_Date,[CheqNo] Cheque_No, [CheqMiti] Cheque_Miti, [CheqDate] Cheque_Date, Null PartyName, NULL PartyLedger_Id, Null Party_PanNo,[CBranch_Id] Branch_ID,[CUnit_Id] CmpUnit_ID,[NM1].[FiscalYearId] FiscalYearId, NULL DoctorId, NUll PatientId, NULL HDepartmentId
				FROM AMS.Notes_Master NM1 Left Outer Join AMS.Notes_Details ND1 on NM1.Voucher_No = ND1.Voucher_No  where Nm1.[VoucherMode] = 'CN'
				Group By Nm1.[VoucherMode], Nm1.[Voucher_No], [Voucher_Date], [Voucher_Miti], [Voucher_Time], Nm1.[Ledger_Id], NM1.[Subledger_ID], NM1.[Agent_ID], Nm1.[Cls1], Nm1.[Cls2], Nm1.[Cls3], Nm1.[Cls4], [Currency_Id], [Currency_Rate], ND1.[Debit], ND1.[Credit], ND1.LocalDebit, ND1.LocalCredit, ND1.Narration, NM1.Remarks, [EnterBy], [EnterDate], [Ref_VNo], [Ref_VDate], [ReconcileBy], [ReconcileDate], [ClearingBy], ClearingDate, [CheqNo], [CheqMiti], [CheqDate], [CBranch_Id], [CUnit_Id], FiscalYearId
				Union All
				Select ND2.VoucherMode Module, ND2.SNo Serial_No, ND2.[Voucher_No] Voucher_No, NM2.[Voucher_Date] Voucher_Date, NM2.[Voucher_Miti] Voucher_Miti, NM2.[Voucher_Time] Voucher_Time, ND2.[Ledger_ID] Ledger_ID, NULL CbLedger_ID, ND2.Subledger_Id Subleder_ID, ND2.[Agent_ID] Agent_ID, ND2.[Cls1] Department_ID1, ND2.[Cls2] Department_ID2, ND2.[Cls3] Department_ID3, ND2.[Cls4] Department_ID4, NM2.[Currency_ID] Currency_ID, NM2.[Currency_Rate] Currency_Rate, ND2.[Credit] Debit_Amt, ND2.[Debit] Credit_Amt, ND2.LocalCredit LocalDebit_Amt, ND2.LocalDebit LocalCredit_Amt, NULL DueDate, NULL DueDays, ND2.Narration Narration, NM2.Remarks Remarks, [EnterBy] EnterBy, [EnterDate] EnterDate, [Ref_VNo] RefNo, [Ref_VDate] RefDate, [ReconcileBy] Reconcile_By, [ReconcileDate] Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, [ClearingBy] Clearing_By, ClearingDate Clearing_Date, NULL Posted_By, NULL Posted_Date, [CheqNo] Cheque_No, [CheqDate] Cheque_Date, [CheqMiti] Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo,[CBranch_Id] Branch_ID, [CUnit_Id] CmpUnit_ID,[NM2].[FiscalYearId] FiscalYearId, NULL DoctorId, NUll PatientId, NULL HDepartmentId
				from AMS.Notes_Details ND2 Left Outer Join AMS.Notes_Master as NM2 on NM2.Voucher_No = ND2.Voucher_No where ND2.[VoucherMode] = 'CN'
				) AcTran ";
            if (NMaster.Voucher_No != null && !string.IsNullOrEmpty(NMaster.Voucher_No))
            {
                cmdString += $" WHERE AcTran.Voucher_No='{NMaster.Voucher_No}'; ";
            }
        }

        var nonQuery = SqlExtensions.ExecuteNonQuery(cmdString);
        return nonQuery;
    }

    public short ReturnSyncRowVersionVoucher(string module, string voucherNo)
    {
        var cmdString = module switch
        {
            "CB" =>
                $" SELECT  MAX(ISNULL(cm.SyncRowVersion,0) +1) SyncRowVersion FROM ams.CB_Master cm WHERE cm.Voucher_No = '{voucherNo}'",
            "JV" =>
                $" SELECT  MAX(ISNULL(cm.SyncRowVersion,0) +1) SyncRowVersion FROM ams.JV_Master cm WHERE cm.Voucher_No = '{voucherNo}'",
            "LOB" =>
                $" SELECT  MAX(ISNULL(cm.SyncRowVersion,0) +1) SyncRowVersion FROM ams.LedgerOpening cm WHERE cm.Voucher_No = '{voucherNo}'",
            "DN" or "CN" =>
                $"SELECT  MAX(ISNULL(cm.SyncRowVersion,0) +1) SyncRowVersion FROM ams.Notes_Master  cm WHERE cm.VoucherMode='{module}' AND cm.Voucher_No = '{voucherNo}'",
            _ => string.Empty
        };
        return cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();
    }

    #endregion --------------- ACCOUNT DETAILS POSTING  ---------------


    // RETURN VALUE IN DATA TABLE

    #region --------------- GET DATA IN DATATABLE ---------------

    public DataTable GetOpeningLedgerWithBalance(string startDate)
    {
        var cmdString = new StringBuilder();
        if (string.IsNullOrEmpty(startDate))
        {
            startDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        cmdString.Append($@"
			SELECT LedgerOpening.LedgerId,LedgerOpening.ShortName,LedgerOpening.[Description],ABS(SUM(LedgerOpening.Balance)) Balance ,
			CASE WHEN Convert(Decimal(18,2),(SUM(LedgerOpening.Balance))) > 0 THEN 'Dr'
			WHEN Convert(Decimal(18,2),(SUM(LedgerOpening.Balance))) < 0 THEN 'Cr'
			ELSE '' END AmtType
			FROM (
				Select GL.GLID LedgerId, GlCode ShortName, GlName Description,
				Convert(Decimal(18,2),(SUM(LocalDebit_Amt) - SUM(LocalCredit_Amt))) as Balance
				from AMS.AccountDetails as AT
				Inner Join AMS.GeneralLedger as GL On GL.GLId = AT.Ledger_Id left Outer Join AMS.AccountGroup as AG on GL.GrpId = AG.GrpId
				Where Module <>'PDC' and Voucher_Date <= '{startDate}' and Module in ('OB', 'LOB', 'N', 'OBSB', 'OBPB', 'OBSR', 'OBPR', 'OBJV', 'OBCB') AND GrpType  not in ('Expenses', 'Income')
				Group By GrpType,GLID,GlCode,GlName
			UNION ALL
			SELECT GLID,gl.GLCode,gl.GLName,0 FROM AMS.GeneralLedger AS gl left Outer Join AMS.AccountGroup as AG on gl.GrpId = AG.GrpId
			where GrpType  not in ('Expenses', 'Income')
			) LedgerOpening
			GROUP BY LedgerOpening.LedgerId,LedgerOpening.ShortName,LedgerOpening.[Description]
			ORDER BY LedgerOpening.Description ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetCashClosing(int selectedId = 0)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(selectedId != 0
            ? $"Select * from AMS.CashClosing where CC_Id='{selectedId}'"
            : "Select * from AMS.CashClosing ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetOpeningVoucher(string module = "N", string voucherNo = "")
    {
        return new DataTable();
    }

    public DataTable IsCheckChequeNoExits(string actionTag, string voucherNo, string txtChequeNo, long ledgerId)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
        Select * From AMS.CB_Master where CheqNo='{txtChequeNo.GetTrimReplace()}' and Ledger_Id = '{ledgerId}' ");
        if (actionTag is "UPDATE")
        {
            cmdString.Append($" AND Voucher_No <> '{voucherNo}' ");
        }

        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }


    #endregion --------------- GET DATA IN DATATABLE ---------------


    // RETURN VALUE IN DATA SET

    #region --------------- RETURN VOUCHER NO ---------------

    public DataSet ReturnCashBankVoucherInDataSet(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT CBM.Voucher_No,CONVERT(varchar(10),Voucher_Date,103) Voucher_Date ,Voucher_Miti,GL.GLID,GL.GLCode,GLName,CBM.Ref_VNo,CONVERT(varchar(10),CBM.Ref_VDate,103) Ref_VDate,CheqNo,CONVERT(varchar(10),CheqDate,103)CheqDate,CheqMiti,CBM.Currency_Id,CUR.Ccode,CName,CBM.Currency_Rate,CBM.Cls1,D1.DName Department1,CBM.Cls2,D2.DName Department2, CBM.Cls3,D3.DName Department3 ,CBM.Cls4,D4.DName Department4,Remarks,CBM.PAttachment1,CBM.PAttachment2,CBM.PAttachment3,CBM.PAttachment4,CBM.PAttachment5 FROM AMS.CB_Master as CBM Inner Join AMS.GeneralLedger as GL On GL.GLID=CBM.Ledger_Id Left Outer Join AMS.Currency as CUR ON Cur.CId=CBM.Currency_Id Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4 Where CBM.Voucher_No='{voucherNo}'
			SELECT CBM.Voucher_No, GL.GLID, GL.GLCode, GLName, CBD.Subledger_Id, SL.SLName, SL.SLCode, CBD.Agent_ID, AG.AgentName, AG.AgentCode, CBD.Cls1, D1.DName Department1, CBD.Cls2, D2.DName Department2, CBD.Cls3, D3.DName Department3, CBD.Cls4, D4.DName Department4,CBD.CurrencyId,C.Ccode, CBD.CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, CBD.PanNo	FROM AMS.CB_Master as CBM  Inner Join AMS.CB_Details as CBD On CBD.Voucher_No=CBM.Voucher_No Inner Join AMS.GeneralLedger as GL On GL.GLID=CBD.Ledger_Id Left Outer Join AMS.SubLedger as SL On SL.SLId=CBD.Subledger_Id Left Outer Join AMS.JuniorAgent as AG ON AG.AgentId=CBD.Agent_ID Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4 LEFT OUTER JOIN AMS.Currency c ON CBD.CurrencyId = c.CId Where CBM.Voucher_No='{voucherNo}' ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    public int SaveUnSyncCashBankVoucherFromServerAsync(CB_Master cashBankVoucherModel)
    {
        throw new NotImplementedException();
    }

    #endregion --------------- RETURN VOUCHER NO ---------------


    // OBJECT FOR THIS FORM

    #region --------------- GLOBAL OBJECT VALUE ---------------

    public JV_Master JvMaster { get; set; }
    public CB_Master CbMaster { get; set; }
    public CB_Details CbDetails { get; set; }
    public Notes_Master NMaster { get; set; }
    public LedgerOpening GetOpening { get; set; }
    public CashClosing CashMaster { get; set; }
    public PostDateCheque PdcMaster { get; set; }
    public List<CB_Details> CbDetailsList { get; set; }

    private readonly string[] _tagStrings = { "DELETE", "UPDATE", "REVERSE" };

    #endregion --------------- GLOBAL OBJECT VALUE ---------------














}









