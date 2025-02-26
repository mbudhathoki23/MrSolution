using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.NotesMaster;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.FinanceTransaction.NotesMaster;
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

namespace MrDAL.DataEntry.FinanceTransaction.NotesMaster;

public class NotesMasterRepository : INotesMasterRepository
{
    public NotesMasterRepository()
    {
        NMaster = new Notes_Master();
    }

    // RETURN VALUE IN DATA TABLE
    public DataSet ReturnCashBankVoucherInDataSet(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT CBM.Voucher_No,CONVERT(varchar(10),Voucher_Date,103) Voucher_Date ,Voucher_Miti,GL.GLID,GL.GLCode,GLName,CBM.Ref_VNo,CONVERT(varchar(10),CBM.Ref_VDate,103) Ref_VDate,CheqNo,CONVERT(varchar(10),CheqDate,103)CheqDate,CheqMiti,CBM.Currency_Id,CUR.Ccode,CName,CBM.Currency_Rate,CBM.Cls1,D1.DName Department1,CBM.Cls2,D2.DName Department2, CBM.Cls3,D3.DName Department3 ,CBM.Cls4,D4.DName Department4,Remarks,CBM.PAttachment1,CBM.PAttachment2,CBM.PAttachment3,CBM.PAttachment4,CBM.PAttachment5 FROM AMS.CB_Master as CBM Inner Join AMS.GeneralLedger as GL On GL.GLID=CBM.Ledger_Id Left Outer Join AMS.Currency as CUR ON Cur.CId=CBM.Currency_Id Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4 Where CBM.Voucher_No='{voucherNo}'
			SELECT CBM.Voucher_No, GL.GLID, GL.GLCode, GLName, CBD.Subledger_Id, SL.SLName, SL.SLCode, CBD.Agent_ID, AG.AgentName, AG.AgentCode, CBD.Cls1, D1.DName Department1, CBD.Cls2, D2.DName Department2, CBD.Cls3, D3.DName Department3, CBD.Cls4, D4.DName Department4,CBD.CurrencyId,C.Ccode, CBD.CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, CBD.PanNo	FROM AMS.CB_Master as CBM  Inner Join AMS.CB_Details as CBD On CBD.Voucher_No=CBM.Voucher_No Inner Join AMS.GeneralLedger as GL On GL.GLID=CBD.Ledger_Id Left Outer Join AMS.SubLedger as SL On SL.SLId=CBD.Subledger_Id Left Outer Join AMS.JuniorAgent as AG ON AG.AgentId=CBD.Agent_ID Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4 LEFT OUTER JOIN AMS.Currency c ON CBD.CurrencyId = c.CId Where CBM.Voucher_No='{voucherNo}' ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }
    public DataTable ReturnPdcVoucherInDataTable(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT CBM.voucherNo,CONVERT(varchar(10),VoucherDate,103)VoucherDate ,VoucherMiti,GL.GLID,GL.GLCode,GL.GLName,Bnk.GLID as BankLedgerId,Bnk.GLName as BankName, CONVERT(varchar(10),CBM.VoucherDate,103) RefDate, CBM.ChequeNo,CONVERT(varchar(10),CBM.ChqDate,103)CheqDate,CBM.ChqMiti,CBM.Cls1,D1.DName Department1,CBM.Cls2,D2.DName Department2, CBM.Cls3,D3.DName Department3 ,CBM.Cls4,D4.DName Department4,Remarks,Amount,VoucherType,CBM.PAttachment1 FROM  [AMS].[PostDateCheque] as CBM  Inner Join AMS.GeneralLedger as GL On GL.GLID=CBM.LedgerId Left Outer Join AMS.GeneralLedger as Bnk ON Bnk.GLID=CBM.BankLedgerId Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4  Where CBM.voucherNo='{voucherNo}' ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString())
            .Tables[0];
    }
    public DataTable IsCheckVoucherNoExits(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    // INSERT UPDATE DELETE
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


    // OBJECT FOR THIS FORM
    public Notes_Master NMaster { get; set; }
    private readonly string[] _tagStrings = { "DELETE", "UPDATE", "REVERSE" };
    public CB_Master CbMaster { get; set; }
    public CB_Details CbDetails { get; set; }

}