using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseIndent;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.PurchaseMaster;
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

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseIndentRepository : IPurchaseIndentRepository
{
    public PurchaseIndentRepository()
    {
        PinMaster = new PIN_Master();
        DetailsList = new List<PIN_Details>();
        CbMaster = new CB_Master();
        CbDetails = new CB_Details();
        PbMaster = new PB_Master();
    }

    // INSERT UPDATE DELETE
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
            if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
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
    public int SavePurchaseInvoice(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (!actionTag.Equals("SAVE"))
        {
            AuditLogPurchaseInvoice(actionTag);
        }
        if (actionTag is "DELETE" or "UPDATE" or "REVERSE")
        {
            if (actionTag is "DELETE" || actionTag != "REVERSE")
            {
                cmdString.Append($" DELETE AMS.PIN_Master WHERE PIN_Invoice ='{PinMaster.PIN_Invoice}'; \n");
            }
        }

        if (actionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.PIN_Master(PIN_Invoice, PIN_Date, PIN_Miti, Person, Sub_Ledger, AgentId, Cls1, Cls2, Cls3, Cls4, EnterBY, EnterDate, ActionType, Remarks, Print_value, R_Invoice, CancelBy, CancelDate, CancelRemarks, BranchId, CompanyUnitId, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($" (N'{PinMaster.PIN_Invoice}','{PinMaster.PIN_Date.GetSystemDate()}',N'{PinMaster.PIN_Miti}','{PinMaster.Person}', ");
            cmdString.Append(PinMaster.Sub_Ledger > 0 ? $"{PinMaster.Sub_Ledger} ," : "NULL,");
            cmdString.Append(PinMaster.AgentId > 0 ? $"{PinMaster.AgentId} ," : "NULL,");
            cmdString.Append(PinMaster.Cls1 > 0 ? $"{PinMaster.Cls1} ," : "NULL,");
            cmdString.Append($"NULL,NULL,NULL,");
            cmdString.Append($"'{PinMaster.EnterBY}','{PinMaster.EnterDate.GetSystemDate()}','{PinMaster.ActionType}',");
            cmdString.Append(PinMaster.Remarks.Trim().Length > 0 ? $"'{PinMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
            cmdString.Append($"{PinMaster.Print_value},{PinMaster.R_Invoice},'{PinMaster.CancelBy}','{PinMaster.CancelDate.GetSystemDate()}','{PinMaster.CancelRemarks}',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
            cmdString.Append("NULL,NULL,NULL,NULL,NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $"'{PinMaster.SyncBaseId}'," : "NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $" '{PinMaster.SyncGlobalId}'," : "NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $" '{PinMaster.SyncOriginId}'," : "NULL,");
            cmdString.Append($" '{PinMaster.SyncCreatedOn.GetSystemDate()}',");
            cmdString.Append($" '{PinMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" {PinMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.PIN_Master SET \n");
            cmdString.Append($" PIN_Invoice ='{PinMaster.PIN_Invoice}',PIN_Date ='{PinMaster.PIN_Date.GetSystemDate()}',PIN_Miti =N'{PinMaster.PIN_Miti}',Person ='{PinMaster.Person}', ");
            cmdString.Append(PinMaster.Sub_Ledger > 0 ? $"Sub_Ledger ={PinMaster.Sub_Ledger} ," : "Sub_Ledger =NULL,");
            cmdString.Append(PinMaster.AgentId > 0 ? $"AgentId ={PinMaster.AgentId} ," : "AgentId =NULL,");
            cmdString.Append(PinMaster.Cls1 > 0 ? $"Cls1 ={PinMaster.Cls1} ," : "Cls1 =NULL,");
            cmdString.Append($"EnterBY ='{PinMaster.EnterBY}',EnterDate ='{PinMaster.EnterDate.GetSystemDate()}',ActionType ='{PinMaster.ActionType}',");
            cmdString.Append(PinMaster.Remarks.Trim().Length > 0 ? $"Remarks ='{PinMaster.Remarks}'," : "Remarks =NULL,");
            cmdString.Append($"Print_value ={PinMaster.Print_value},R_Invoice ={PinMaster.R_Invoice},CancelBy ='{PinMaster.CancelBy}',CancelDate ='{PinMaster.CancelDate.GetSystemDate()}',CancelRemarks ='{PinMaster.CancelRemarks}',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" SysBranchId={ObjGlobal.SysBranchId}," : "SysBranchId=NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" SysCompanyUnitId ={ObjGlobal.SysCompanyUnitId}," : "SysCompanyUnitId =NULL,");
            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" SysFiscalYearId ={ObjGlobal.SysFiscalYearId}," : "SysFiscalYearId =NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $" SyncBaseId ='{PinMaster.SyncBaseId}'," : "SyncBaseId =NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $" SyncGlobalId ='{PinMaster.SyncGlobalId}'," : "SyncGlobalId =NULL,");
            cmdString.Append(PinMaster.SyncBaseId.IsGuidExits() ? $" SyncOriginId ='{PinMaster.SyncOriginId}'," : "SyncOriginId =NULL,");
            cmdString.Append($" SyncCreatedOn ='{PinMaster.SyncCreatedOn.GetSystemDate()}',");
            cmdString.Append($" SyncLastPatchedOn ='{PinMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" SyncRowVersion ={PinMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }

        if (actionTag.ToUpper() != "DELETE" && !actionTag.Equals("REVERSE"))
        {
            if (DetailsList.Count > 0)
            {
                var iRows = 0;
                cmdString.Append(@" 
                    INSERT INTO AMS.PIN_Details(PIN_Invoice, SNo, P_Id, Gdn_Id, Alt_Qty, Alt_Unit, Qty, Unit, AltStock_Qty, StockQty, Issue_Qty, Narration) ");
                cmdString.Append("\n VALUES \n");

                foreach (var dr in DetailsList)
                {
                    var index = DetailsList.IndexOf(dr);
                    cmdString.Append($"('{dr.PIN_Invoice}', {dr.SNo}, {dr.P_Id},");
                    cmdString.Append(dr.Gdn_Id > 0 ? $"{dr.Gdn_Id}, " : "NULL,");
                    cmdString.Append($"{dr.Alt_Qty}, ");
                    cmdString.Append(dr.Alt_Unit > 0 ? $"{dr.Alt_Unit}, " : "NULL,");
                    cmdString.Append($"{dr.Qty}, ");
                    cmdString.Append($"{dr.Unit}, ");
                    cmdString.Append($"{dr.AltStock_Qty}, {dr.StockQty}, {dr.Issue_Qty}, '{dr.Narration}'");
                    cmdString.Append(index == DetailsList.Count - 1 ? " ); \n" : "),\n");
                }
            }
        }

        var isOk = SaveDataInDatabase(cmdString);
        if (isOk == 0)
        {
            return isOk;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncPurchaseInvoiceAsync(actionTag));
        }
        if (!_tagStrings.Contains(actionTag))
        {
            if (actionTag is "SAVE")
            {
                AuditLogPurchaseInvoice(actionTag);
            }
            PurchaseInvoiceTermPosting();
            if (PbMaster.PC_Invoice.IsValueExits())
            {
                var cmdTxt = $@"
					UPDATE AMS.PC_Master SET Invoice_Type = 'POSTED' WHERE PC_Invoice='{PbMaster.PC_Invoice}'; ";
                var i = SqlExtensions.ExecuteNonQuery(cmdTxt);
            }

            PurchaseInvoiceAccountDetailsPosting();
            PurchaseInvoiceStockDetailsPosting();
        }

        return isOk;
    }
    private int AuditLogPurchaseInvoice(string actionTag)
    {
        var cmdString = @$"
			INSERT INTO [AUD].[AUDIT_PB_Master] ([PB_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[PB_Vno] ,[Vno_Date] ,[Vno_Miti] ,[Vendor_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_In] ,[DueDays] ,[DueDate] ,[Agent_ID] ,[Subledger_Id] ,[PO_Invoice] ,[PO_Date] ,[PC_Invoice] ,[PC_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[Cur_Id] ,[Cur_Rate] ,[Counter_ID] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[Tender_Amount] ,[Change_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[Audit_Lock] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[CancelBy] ,[CancelDate] ,[CancelRemarks] ,[CUnit_Id] ,[CBranch_Id] ,[FiscalYearId] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,[Enter_By] ,[Enter_Date] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
			SELECT [PB_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[PB_Vno] ,[Vno_Date] ,[Vno_Miti] ,[Vendor_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_In] ,[DueDays] ,[DueDate] ,[Agent_ID] ,[Subledger_Id] ,[PO_Invoice] ,[PO_Date] ,[PC_Invoice] ,[PC_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[Cur_Id] ,[Cur_Rate] ,[Counter_ID] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[Tender_Amount] ,[Change_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[Audit_Lock] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[CancelBy] ,[CancelDate] ,[CancelRemarks] ,[CUnit_Id] ,[CBranch_Id] ,[FiscalYearId] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,[Enter_By] ,[Enter_Date] ,'   {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[PB_Master] where PB_Invoice = '{PbMaster.PB_Invoice}';

			INSERT INTO [AUD].[AUDIT_PB_Details] ([PB_Invoice] ,[Invoice_SNo] ,[P_Id] ,[Gdn_Id] ,[Alt_Qty] ,[Alt_UnitId] ,[Qty] ,[Unit_Id] ,[Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[AltStock_Qty] ,[Stock_Qty] ,[Narration] ,[PO_Invoice] ,[PO_Sno] ,[PC_Invoice] ,[PC_SNo] ,[Tax_Amount] ,[V_Amount] ,[V_Rate] ,[Free_Unit_Id] ,[Free_Qty] ,[StockFree_Qty] ,[ExtraFree_Unit_Id] ,[ExtraFree_Qty] ,[ExtraStockFree_Qty] ,[T_Product] ,[P_Ledger] ,[PR_Ledger] ,[SZ1] ,[SZ2] ,[SZ3] ,[SZ4] ,[SZ5] ,[SZ6] ,[SZ7] ,[SZ8] ,[SZ9] ,[SZ10] ,[Serial_No] ,[Batch_No] ,[Exp_Date] ,[Manu_Date] ,[TaxExempted_Amount] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
			SELECT [PB_Invoice]	,[Invoice_SNo]	,[P_Id]	,[Gdn_Id]	,[Alt_Qty]	,[Alt_UnitId]	,[Qty]	,[Unit_Id]	,[Rate]	,[B_Amount]	,[T_Amount]	,[N_Amount]	,[AltStock_Qty]	,[Stock_Qty],[Narration] ,[PO_Invoice]	,[PO_Sno]	,[PC_Invoice]	,[PC_SNo]	,[Tax_Amount]	,[V_Amount]	,[V_Rate]	,[Free_Unit_Id]	,[Free_Qty]	,[StockFree_Qty]	,[ExtraFree_Unit_Id]	,[ExtraFree_Qty]	,[ExtraStockFree_Qty]	,[T_Product]	,[P_Ledger]	,[PR_Ledger]	,[SZ1]	,[SZ2]	,[SZ3]	,[SZ4]	,[SZ5]	,[SZ6]	,[SZ7]	,[SZ8]	,[SZ9]	,[SZ10]	,[Serial_No]	,[Batch_No]	,[Exp_Date]	,[Manu_Date]	,[TaxExempted_Amount] ,'   {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[PB_Details] where PB_Invoice = '{PbMaster.PB_Invoice}';

			INSERT INTO[AUD].[AUDIT_PB_Term] ([PB_Vno],[PT_Id],[SNo],[Rate],[Amount],[Term_Type],[Product_Id],[Taxable],[ModifyAction],[ModifyBy],[ModifyDate])
			SELECT[PB_Vno] ,[PT_Id] ,[SNo] ,[Rate] ,[Amount] ,[Term_Type] ,[Product_Id] ,[Taxable]  ,'{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[PB_Term] where[PB_Vno] = '{PbMaster.PB_Invoice}'; ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public int PurchaseInvoiceAccountDetailsPosting()
    {
        var cmdString = @" DELETE FROM AMS.AccountDetails WHERE Module = 'PB' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND Voucher_No = '{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId,SyncRowVersion)
			SELECT 'PB' Module,Sno, VNo, VDate, VMiti, VTime, Ledger_Id, CbLedger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, DrAmt, CrAmt, Local_DrAmt, Local_CrAmt, DueDate, DueDays, Naration, Remarks, UserName, Enter_Date, RefNo, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, ChqNo, ChqDate, Party_Name, NULL PartyLedger_Id, Vat_No, CBranch_Id, CUnit_Id, FiscalYearId,IsNUll(ACCTran.SyncRowVersion,1) SyncRowVersion
			FROM(SELECT 'PB' Module, ROW_NUMBER() OVER (PARTITION BY PBM.PB_Invoice ORDER BY PBM.PB_Invoice) AS Sno, PB_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, Vendor_ID AS Ledger_Id, SC.PBLedgerId CbLedger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, Cur_Rate, 0 AS DrAmt, N_Amount AS CrAmt, 0 AS Local_DrAmt, N_Amount * ISNULL(Cur_Rate, 1) AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Naration, Remarks, Enter_By AS UserName, Enter_Date, PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, FiscalYearId,PBM.SyncRowVersion
				 FROM AMS.PB_Master AS PBM, AMS.PurchaseSetting AS SC WHERE (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL)";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PBM.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
				 UNION ALL
				 SELECT 'PB' Module,ROW_NUMBER() OVER (PARTITION BY PBM.PB_Invoice ORDER BY PBM.PB_Invoice) Sno, PBM.PB_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, ISNULL(PPL, PBLedgerId) AS Ledger_Id, PBM.Vendor_ID CbLedger_Id, Subledger_Id, PBM.Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, PBM.Cur_Rate, ROUND(SUM(ISNULL(PBD.B_Amount, 0)), 2) AS DrAmt, 0 AS CrAmt, SUM(ISNULL(PBD.B_Amount, 0)* ISNULL(PBM.Cur_Rate, 1)) AS Local_DrAmt, 0 AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Naration, PBM.Remarks, PBM.Enter_By AS UserName, Enter_Date, PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, PBM.FiscalYearId, PBM.SyncRowVersion
				 FROM AMS.PB_Master AS PBM, AMS.PB_Details AS PBD, AMS.PurchaseSetting AS SC, AMS.Product AS P
				 WHERE PBD.P_Id=P.PID AND PBD.PB_Invoice=PBM.PB_Invoice AND (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL) ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $@"AND PBM.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
				 GROUP BY PBM.PB_Invoice, PPL, Invoice_Date, Invoice_Time, PBM.Cur_Id, PBM.Cur_Rate, Cls1, Cls2, Cls3, Cls4, CUnit_Id, CBranch_Id, PBM.DueDate, PBM.DueDays, PBM.Remarks, PBLedgerId, PBM.Vendor_ID, Subledger_Id, Enter_By, Enter_Date, PB_Vno, PBM.Agent_Id,Invoice_Miti, PBM.Auth_By, Auth_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, PBM.FiscalYearId, PBM.SyncRowVersion
				 UNION ALL
				 SELECT 'PB' Module, ROW_NUMBER() OVER (PARTITION BY PBM.PB_Vno ORDER BY PBM.PB_Vno) AS Sno, PBT.PB_VNo AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, PTM.Ledger Ledger_Id, PBM.Vendor_ID CbLedger_Id, Subledger_Id, PBM.Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, PBM.Cur_Rate, CASE WHEN PT_Sign='+' THEN (PBT.Amount)ELSE 0 END AS DrAmt, CASE WHEN PT_Sign='-' THEN SUM(PBT.Amount)ELSE 0 END AS CrAmt, CASE WHEN PT_Sign='+' THEN SUM(ISNULL(PBT.Amount, 0) * ISNULL(PBM.Cur_Rate, 1))ELSE 0 END AS Local_DrAmt, CASE WHEN PT_Sign='-' THEN SUM(ISNULL(PBT.Amount, 0)* ISNULL(PBM.Cur_Rate, 1))ELSE 0 END AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Narration, Remarks, Enter_By, Enter_Date, PBM.PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, PBM.FiscalYearId,PBM.SyncRowVersion
				 FROM AMS.PT_Term AS PTM, AMS.PB_Term AS PBT, AMS.PB_Master AS PBM
				 WHERE PTM.PT_ID=PBT.PT_Id AND PBM.PB_Invoice=PBT.PB_VNo AND (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL) AND Term_Type<>'BT' AND Term_Type<>'' AND CASE WHEN PT_Sign='+' THEN Amount ELSE -Amount END<>0  ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PBM.PB_Invoice='{PbMaster.PB_Invoice}'" : " ";
        cmdString += @"
				GROUP BY CASE WHEN PT_Sign='+' THEN (PBT.Amount)ELSE 0 END, PBT.PB_VNo, PBM.Invoice_Date, PBM.Invoice_Miti, PBM.Invoice_Time, PTM.Ledger, PBM.Vendor_ID, PBM.Subledger_Id, PBM.Agent_Id, PBM.Cls1, PBM.Cls2, PBM.Cls3, PBM.Cls4, PBM.Cur_Id, PBM.Cur_Rate, PTM.PT_Sign, PBM.DueDate, PBM.DueDays, PBM.Enter_By, PBM.Enter_Date, PBM.PB_Vno, PBM.Auth_By, PBM.Auth_Date, PBM.ChqNo, PBM.ChqDate, PBM.Party_Name, PBM.Vat_No, PBM.CUnit_Id, PBM.CBranch_Id, PBM.FiscalYearId, PBM.SyncRowVersion,PBM.Remarks
				) AS ACCTran;   ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    public int PurchaseInvoiceStockDetailsPosting()
    {
        var cmdString = @"
			DELETE FROM AMS.StockDetails WHERE Module='PB' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND Voucher_No='{PbMaster.PB_Invoice}';" : " ";
        cmdString += @"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM(SELECT 'PB' Module, pd.PB_Invoice Voucher_No, Invoice_SNo Serial_No, pm.PB_Vno PurRefVno, pm.Invoice_Date Voucher_Date, pm.Invoice_Miti Voucher_Miti, pm.Invoice_Time Voucher_Time, pm.Vendor_ID Ledger_ID, pm.Subledger_Id Subledger_Id, pm.Agent_ID, pm.Cls1 Department_ID1, pm.Cls2 Department_ID2, pm.Cls3 Department_ID3, pm.Cls4 Department_ID4, pm.Cur_Id Currency_ID, pm.Cur_Rate Currency_Rate, P_Id Product_Id, Gdn_Id Godown_Id, NULL CostCenter_Id, Alt_Qty AltQty, Alt_UnitId AltUnit_Id, Qty, Unit_Id, AltStock_Qty AltStockQty, Stock_Qty StockQty, pd.Free_Qty FreeQty, pd.Free_Unit_Id FreeUnit_Id, pd.StockFree_Qty StockFreeQty, 0 ConvRatio, pd.ExtraFree_Qty ExtraFreeQty, pd.ExtraFree_Unit_Id ExtraFreeUnit_Id, pd.ExtraStockFree_Qty ExtraStockFreeQty, pd.Rate, pd.B_Amount BasicAmt, pd.T_Amount TermAmt, pd.N_Amount NetAmt, 0 BillTermAmt, pd.V_Rate TaxRate, pm.Tbl_Amount TaxableAmt, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) DocVal, 0 ReturnVal, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) StockVal, 0 AddStockVal, pm.PB_Vno PartyInv, 'I' EntryType, pm.Auth_By AuthBy, pm.Auth_Date AuthDate, pm.Reconcile_By RecoBy, pm.Reconcile_Date RecoDate, NULL Counter_ID, NULL RoomNo, pm.CBranch_Id Branch_ID, pm.CUnit_Id CmpUnit_ID, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, 0 SalesRate, pm.FiscalYearId, pm.Enter_By EnterBy, pm.Enter_Date EnterDate, pm.SyncGlobalId, pm.SyncOriginId, pm.SyncCreatedOn, pm.SyncLastPatchedOn, pm.SyncRowVersion, pm.SyncBaseId
				 FROM AMS.PB_Details pd
					  INNER JOIN AMS.Product p ON p.PID = pd.P_Id
					  INNER JOIN AMS.PB_Master pm ON pd.PB_Invoice=pm.PB_Invoice
					  LEFT OUTER JOIN(SELECT pt.Product_Id, pt.PB_VNo, pt.SNo, SUM(CASE WHEN pt1.PT_Sign='-' THEN -pt.Amount ELSE pt.Amount END) StockValue
									  FROM AMS.PB_Term pt
										   LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
									  WHERE pt1.PT_Profitability>0 AND pt.Term_Type<>'B' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND pt.PB_VNo='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
									  GROUP BY pt.Product_Id, pt.PB_VNo, pt.SNo) AS StockValue ON StockValue.Product_Id = pd.P_Id AND StockValue.PB_VNo = pd.PB_Invoice AND pd.Invoice_SNo = StockValue.SNo
				 WHERE p.PType IN ('I','Inventory') ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND pm.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += " ) AS Stock; ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    public int PurchaseInvoiceTermPosting()
    {
        var cmdString = @"
			DELETE AMS.PB_Term WHERE Term_Type='BT' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PB_VNo='{PbMaster.PB_Invoice}';" : "";
        cmdString += @"
			INSERT INTO AMS.PB_Term(PB_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PB_VNo, PT_Id,  sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion,1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PB_Details sd
					LEFT OUTER JOIN AMS.PB_Master sm ON sm.PB_Invoice=sd.PB_Invoice
					LEFT OUTER JOIN AMS.PB_Term sbt ON sd.PB_Invoice=sbt.PB_VNo
			WHERE  sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount > 0 ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND sbt.PB_VNo = '{PbMaster.PB_Invoice}';" : "";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    private static int SaveDataInDatabase(StringBuilder stringBuilder)
    {
        var isExc = SqlExtensions.ExecuteNonTrans(stringBuilder.ToString());
        return isExc;
    }
    public async Task<int> SyncPurchaseInvoiceAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                return 1;

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

            getUrl = @$"{configParams.Model.Item2}PurchaseInvoice/GetPurchaseInvoiceById";
            insertUrl = @$"{configParams.Model.Item2}PurchaseInvoice/InsertPurchaseInvoice";
            updateUrl = @$"{configParams.Model.Item2}PurchaseInvoice/UpdatePurchaseInvoice";
            deleteUrl = @$"{configParams.Model.Item2}PurchaseInvoice/DeletePurchaseInvoiceAsync?id=" +
                        PbMaster.PB_Invoice;

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

            var purchaseInvoiceRepo =
                DataSyncProviderFactory.GetRepository<PB_Master>(DataSyncManager.GetGlobalInjectData());
            var pi = new PB_Master
            {
                PB_Invoice = PbMaster.PB_Invoice,
                Invoice_Date = Convert.ToDateTime(PbMaster.Invoice_Date.ToString("yyyy-MM-dd")),
                Invoice_Miti = PbMaster.Invoice_Miti,
                Invoice_Time = DateTime.Now,
                PB_Vno = PbMaster.PB_Vno.Trim().Length > 0 ? PbMaster.PB_Vno : null,
                Vno_Date = PbMaster.PB_Vno.Trim().Length > 0
                    ? Convert.ToDateTime(PbMaster.Vno_Date.Value.ToString("yyyy-MM-dd"))
                    : null,
                Vno_Miti = PbMaster.PB_Vno.Trim().Length > 0 ? PbMaster.Vno_Miti : null,
                Vendor_ID = PbMaster.Vendor_ID,
                PartyLedgerId = PbMaster.PartyLedgerId > 0 ? PbMaster.PartyLedgerId : null,
                Party_Name = PbMaster.Party_Name.Trim().Length > 0 ? PbMaster.Party_Name : null,
                Vat_No = PbMaster.Vat_No.Trim().Length > 0 ? PbMaster.Vat_No : null,
                Contact_Person = PbMaster.Contact_Person.Trim().Length > 0 ? PbMaster.Contact_Person : null,
                Mobile_No = PbMaster.Mobile_No.Trim().Length > 0 ? PbMaster.Mobile_No : null,
                Address = PbMaster.Address.Trim().Length > 0 ? PbMaster.Address : null,
                ChqNo = PbMaster.ChqNo.Trim().Length > 0 ? PbMaster.ChqNo : null,
                ChqDate = PbMaster.ChqNo.Trim().Length > 0 ? PbMaster.ChqDate : null,
                ChqMiti = PbMaster.ChqNo.Trim().Length > 0 ? PbMaster.ChqMiti : null,
                Invoice_Type = PbMaster.Invoice_Type.Trim().Length > 0 ? PbMaster.Invoice_Type : "NORMAL",
                Invoice_In = PbMaster.Invoice_In.Trim().Length > 0 ? PbMaster.Invoice_In : "CREDIT",
                DueDays = PbMaster.DueDays.GetInt(),
                DueDate = PbMaster.DueDays.GetInt() > 0
                    ? Convert.ToDateTime(PbMaster.DueDate.Value.ToString("yyyy-MM-dd"))
                    : null,
                Agent_Id = PbMaster.Agent_Id > 0 ? PbMaster.Agent_Id : null,
                Subledger_Id = PbMaster.Subledger_Id > 0 ? PbMaster.Subledger_Id : null,
                PO_Invoice = PbMaster.PO_Invoice.Trim().Length > 0 ? PbMaster.PO_Invoice : null,
                PO_Date = PbMaster.PO_Invoice.Trim().Length > 0
                    ? Convert.ToDateTime(PbMaster.PO_Date.Value.ToString("yyyy-MM-dd"))
                    : null,
                PC_Invoice = PbMaster.PC_Invoice.Trim().Length > 0 ? PbMaster.PC_Invoice : null,
                PC_Date = PbMaster.PC_Invoice.Trim().Length > 0
                    ? Convert.ToDateTime(PbMaster.PC_Date.Value.ToString("yyyy-MM-dd"))
                    : null,
                Cls1 = PbMaster.Cls1 > 0 ? PbMaster.Cls1 : null,
                Cls2 = null,
                Cls3 = null,
                Cls4 = null,
                Cur_Id = PbMaster.Cur_Id > 0 ? PbMaster.Cur_Id : ObjGlobal.SysCurrencyId,
                Cur_Rate = PbMaster.Cur_Rate.GetDecimal(true),
                Counter_ID = PbMaster.Counter_ID > 0 ? PbMaster.Counter_ID : null,
                B_Amount = PbMaster.B_Amount.GetDecimal(),
                T_Amount = PbMaster.T_Amount.GetDecimal(),
                N_Amount = PbMaster.N_Amount.GetDecimal(),
                LN_Amount = PbMaster.LN_Amount.GetDecimal(),
                Tender_Amount = PbMaster.Tender_Amount.GetDecimal(),
                Change_Amount = PbMaster.Change_Amount.GetDecimal(),
                V_Amount = PbMaster.V_Amount.GetDecimal(),
                Tbl_Amount = PbMaster.Tbl_Amount.GetDecimal(),
                Action_type = PbMaster.Action_type.Trim().Length > 0 ? PbMaster.Action_type : "SAVE",
                R_Invoice = false,
                CancelBy = null,
                CancelDate = null,
                CancelRemarks = null,
                No_Print = 0,
                In_Words = PbMaster.In_Words.Trim().Length > 0 ? PbMaster.In_Words.Trim().Replace("'", "''") : null,
                Remarks = PbMaster.Remarks.Trim().Length > 0 ? PbMaster.Remarks.Trim().Replace("'", "''") : null,
                Audit_Lock = false,
                Enter_By = ObjGlobal.LogInUser,
                Enter_Date = DateTime.Now,
                Reconcile_By = null,
                Reconcile_Date = null,
                Auth_By = null,
                Auth_Date = null,
                Cleared_By = null,
                Cleared_Date = null,
                CBranch_Id = ObjGlobal.SysBranchId,
                CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                PAttachment1 = null,
                PAttachment2 = null,
                PAttachment3 = null,
                PAttachment4 = null,
                PAttachment5 = null,
                SyncRowVersion = PbMaster.SyncRowVersion
            };
            var piDetails = new List<PB_Details>();
            if (PbMaster.GetView is { RowCount: > 0 })
                foreach (DataGridViewRow dr in PbMaster.GetView.Rows)
                {
                    var pid = new PB_Details
                    {
                        PB_Invoice = PbMaster.PB_Invoice,
                        Invoice_SNo = dr.Cells["GTxtSno"].Value.GetInt(),
                        P_Id = dr.Cells["GTxtProductId"].Value.GetInt(),
                        Gdn_Id = dr.Cells["GTxtGodownId"].Value.GetInt() > 0
                            ? dr.Cells["GTxtGodownId"].Value.GetInt()
                            : null,
                        Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
                            ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
                            : 0,
                        Alt_UnitId = dr.Cells["GTxtAltUOMId"].Value.GetDecimal() > 0
                            ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
                            : null,
                        Qty = dr.Cells["GTxtQty"].Value.GetDecimal() > 0 ? dr.Cells["GTxtQty"].Value.GetDecimal() : 1,
                        Unit_Id =
                            dr.Cells["GTxtUOMId"].Value.GetInt() > 0 ? dr.Cells["GTxtUOMId"].Value.GetInt() : null,
                        Rate = dr.Cells["GTxtRate"].Value.GetDecimal(),
                        B_Amount = dr.Cells["GTxtAmount"].Value.GetDecimal(),
                        T_Amount = dr.Cells["GTxtTermAmount"].Value.GetDecimal(),
                        N_Amount = dr.Cells["GTxtNetAmount"].Value.GetDecimal(),
                        AltStock_Qty = dr.Cells["GTxtAltStockQty"].Value.GetDecimal(),
                        Stock_Qty = dr.Cells["GTxtStockQty"].Value.GetDecimal(),
                        Narration = dr.Cells["GTxtNarration"].Value.IsValueExits()
                            ? dr.Cells["GTxtNarration"].Value.ToString()
                            : null,
                        PO_Invoice = dr.Cells["GTxtOrderNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtOrderNo"].Value.ToString()
                            : null,
                        PO_Sno = dr.Cells["GTxtOrderSno"].Value.GetInt(),
                        PC_Invoice = dr.Cells["GTxtChallanNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtChallanNo"].Value.ToString()
                            : null,
                        PC_SNo = dr.Cells["GTxtChallanSno"].Value.GetInt(),
                        Tax_Amount = 0,
                        V_Amount = 0,
                        V_Rate = 0,
                        Free_Unit_Id = 0,
                        Free_Qty = 0,
                        StockFree_Qty = 0,
                        ExtraFree_Unit_Id = 0,
                        ExtraFree_Qty = 0,
                        ExtraStockFree_Qty = 0,
                        T_Product = dr.Cells["IsTaxable"].Value.GetBool() is true ? true : false,
                        P_Ledger = dr.Cells["GTxtPBLedgerId"].Value.GetLong() > 0
                            ? dr.Cells["GTxtPBLedgerId"].Value.GetLong()
                            : null,
                        PR_Ledger = dr.Cells["GTxtPRLedgerId"].Value.GetLong() > 0
                            ? dr.Cells["GTxtPRLedgerId"].Value.GetLong()
                            : null,
                        SZ1 = null,
                        SZ2 = null,
                        SZ3 = null,
                        SZ4 = null,
                        SZ5 = null,
                        SZ6 = null,
                        SZ7 = null,
                        SZ8 = null,
                        SZ9 = null,
                        SZ10 = null,
                        Serial_No = null,
                        Batch_No = null,
                        Exp_Date = null,
                        Manu_Date = null,
                        TaxExempted_Amount = 0,
                        SyncRowVersion = PbMaster.SyncRowVersion
                    };

                    piDetails.Add(pid);
                }

            var piTerms = new List<PB_Term>();

            if (PbMaster.ProductTerm != null && PbMaster.ProductTerm.Rows.Count > 0)
                foreach (DataRow dr in PbMaster.ProductTerm.Rows)
                {
                    if (dr["TermAmt"].GetDecimal() is 0) continue;
                    var pit = new PB_Term
                    {
                        PB_VNo = PbMaster.PB_Invoice,
                        PT_Id = dr["TermId"].GetInt(),
                        SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
                        Term_Type = "P",
                        Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
                        Rate = dr["TermRate"].GetDecimal(),
                        Amount = dr["TermAmt"].GetDecimal(),
                        Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                  dr["TermId"].GetInt().Equals(ObjGlobal.PurchaseVatTermId)
                            ? "Y"
                            : "N",
                        SyncRowVersion = PbMaster.SyncRowVersion
                    };

                    piTerms.Add(pit);
                }

            if (PbMaster.BillTerm != null && PbMaster.BillTerm.Rows.Count > 0)
                foreach (DataRow dr in PbMaster.BillTerm.Rows)
                {
                    if (dr["TermAmt"].GetDecimal() is 0) continue;
                    var pit = new PB_Term
                    {
                        PB_VNo = PbMaster.PB_Invoice,
                        PT_Id = dr["TermId"].GetInt(),
                        SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
                        Term_Type = "B",
                        Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
                        Rate = dr["TermRate"].GetDecimal(),
                        Amount = dr["TermAmt"].GetDecimal(),
                        Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                  dr["TermId"].GetInt().Equals(ObjGlobal.PurchaseVatTermId)
                            ? "Y"
                            : "N",
                        SyncRowVersion = PbMaster.SyncRowVersion
                    };

                    piTerms.Add(pit);
                }

            var prdAddInfos = new List<ProductAddInfo>();
            if (PbMaster.ProductBatch != null && PbMaster.ProductBatch.Rows.Count > 0)
                foreach (DataRow dr in PbMaster.ProductBatch.Rows)
                {
                    var prdAddInfo = new ProductAddInfo
                    {
                        Module = "PB",
                        VoucherNo = PbMaster.PB_Invoice,
                        VoucherType = "I",
                        ProductId = dr["ProductId"].GetLong(),
                        Sno = dr["ProductSno"].GetInt(),
                        SizeNo = null,
                        SerialNo = null,
                        BatchNo = dr["BatchNo"].ToString(),
                        ChasisNo = null,
                        EngineNo = null,
                        VHModel = null,
                        VHColor = null,
                        MFDate = Convert.ToDateTime(dr["MfDate"].GetSystemDate()),
                        ExpDate = Convert.ToDateTime(dr["ExpDate"].GetSystemDate()),
                        Mrp = dr["MRP"].GetDecimal(),
                        Rate = dr["Rate"].GetDecimal(),
                        AltQty = 0,
                        Qty = dr["Qty"].GetDecimal(),
                        BranchId = ObjGlobal.SysBranchId,
                        CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                        EnterBy = ObjGlobal.LogInUser,
                        EnterDate = DateTime.Now,
                        SyncRowVersion = PbMaster.SyncRowVersion
                    };
                    prdAddInfos.Add(prdAddInfo);
                }

            pi.DetailsList = piDetails;
            pi.Terms = piTerms;
            pi.ProductAddInfos = prdAddInfos;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await purchaseInvoiceRepo?.PushNewAsync(pi),
                "NEW" => await purchaseInvoiceRepo?.PushNewAsync(pi),
                "UPDATE" => await purchaseInvoiceRepo?.PutNewAsync(pi),
                //"REVERSE" => await purchaseChallanReturnRepo?.PutNewAsync(pcr),
                //"DELETE" => await purchaseChallanReturnRepo?.DeleteNewAsync(),
                _ => await purchaseInvoiceRepo?.PushNewAsync(pi)
            };
            if (result.Value)
            {
                var queryBuilder = new StringBuilder();
                queryBuilder.Append($"UPDATE AMS.PB_Master SET IsSynced =1 WHERE PB_Invoice='{PbMaster.PB_Invoice}'");
                SaveDataInDatabase(queryBuilder);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }


    // RETURN VALUE IN DATA TABLE
    public DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GLID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			FROM AMS.PB_Master AS PIM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_ID
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
			WHERE PIM.PB_Invoice = @voucherNo;
			SELECT PID.PB_Invoice, PID.Invoice_SNo, P.PName, PAlias, P.PShortName, PID.P_Id, G.GName, G.GCode, PID.Gdn_Id, P.PAltUnit, PID.Alt_UnitId, ALTU.UnitCode AS AltUnitCode, PID.Alt_Qty, PID.Qty, P.PUnit, U.UnitCode, PID.Unit_Id, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, PO_Sno, PO_Invoice, PC_SNo, PC_Invoice
			FROM AMS.PB_Details AS PID
				 INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID = PID.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			WHERE PID.PB_Invoice = @voucherNo
			ORDER BY Invoice_SNo
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
			FROM AMS.PB_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PB_Vno=@voucherNo AND Term_Type='P' AND PIT.Product_Id IN (SELECT P_Id FROM AMS.PB_Details WHERE PB_Invoice=@voucherNo)
			ORDER BY PIT.SNo ASC
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
			FROM AMS.PB_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PB_Vno = @voucherNo AND Term_Type='B'
			ORDER BY PIT.SNo ASC
            SELECT * FROM AMS.ProductAddInfo
            WHERE VoucherNo = @voucherNo AND Module ='PB'";

        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = $" SELECT * FROM AMS.{tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // OBJECT FOR THIS FORM
    public PIN_Master PinMaster { get; set; }
    public CB_Master CbMaster { get; set; }
    public CB_Details CbDetails { get; set; }
    private readonly string[] _tagStrings = { "DELETE", "UPDATE", "REVERSE" };
    public PB_Master PbMaster { get; set; }

    public List<PIN_Details> DetailsList { get; set; }
}