using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesQuotation;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.SalesQuotation;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MrDAL.DataEntry.SalesMaster;

public class SalesQuotationRepository : ISalesQuotationRepository
{
    public SalesQuotationRepository()
    {
        SqMaster = new SQ_Master();
        DetailsList = new List<SQ_Details>();
        Terms = new List<SQ_Term>();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // SALES QUOTATION INSERT UPDATE DELETE
    public int SaveSalesQuotation(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "NEW" || actionTag == "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.SQ_Master(SQ_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Expiry_Date, Ref_Vno, Ref_VDate, Ref_VMiti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Cancel_By, Cancel_Date, Cancel_Remarks, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CUnit_Id, CBranch_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($"('{SqMaster.SQ_Invoice}','{SqMaster.Invoice_Date.GetSystemDate()}','{SqMaster.Invoice_Miti}', GETDATE(),");
            cmdString.Append($"'{SqMaster.Expiry_Date.GetSystemDate()}', "); //Expiry_Date
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Ref_Vno) ? $"'{SqMaster.Ref_Vno}'," : "Null,"); //Ref_Vno
            cmdString.Append(SqMaster.Ref_VDate != null ? $"'{SqMaster.Ref_VDate.GetSystemDate()}'," : "Null,"); //Ref_VDate
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Ref_VMiti) ? $"N'{SqMaster.Ref_VMiti}', " : "Null,"); //Ref_VMiti
            cmdString.Append(SqMaster.Customer_Id.GetLong() > 0 ? $"{SqMaster.Customer_Id}, " : "0,"); //Customer_ID
            cmdString.Append(SqMaster.PartyLedgerId.GetLong() > 0 ? $"{SqMaster.PartyLedgerId}," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Party_Name) ? $"'{SqMaster.Party_Name}', " : "NUll,"); //Party_Name
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Vat_No) ? $"'{SqMaster.Vat_No}', " : "NUll,"); //Vat_No
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Contact_Person) ? $"'{SqMaster.Contact_Person}', " : "NUll,"); //Contact_Person
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Mobile_No) ? $"'{SqMaster.Mobile_No}', " : "NUll,"); //Mobile_No
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Address) ? $"'{SqMaster.Address}', " : "NUll,"); //Address
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.ChqNo) ? $"'{SqMaster.ChqNo}', " : "NUll,"); //ChqNo
            cmdString.Append($"'{SqMaster.ChqDate.GetSystemDate()}', "); //ChqDate
            cmdString.Append($"'{SqMaster.ChqMiti}',");
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Invoice_Type) ? $"'{SqMaster.Invoice_Type}',  " : "NUll,"); //Invoice_Type
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Invoice_Mode) ? $"'{SqMaster.Invoice_Mode}'," : "NUll,"); //Invoice_Mode
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Payment_Mode) ? $"'{SqMaster.Payment_Mode}', " : "NUll,"); //Payment_Mode
            cmdString.Append(SqMaster.DueDays.GetInt() > 0 ? $"{SqMaster.DueDays}, " : "0,"); //DueDays
            cmdString.Append(SqMaster.DueDate != null ? $"'{SqMaster.DueDate.GetSystemDate()}', " : "NULL,"); //DueDate
            cmdString.Append(SqMaster.Agent_Id.GetInt() > 0 ? $"{SqMaster.Agent_Id} , " : "NUll,"); //Agent_Id
            cmdString.Append(SqMaster.Subledger_Id.GetInt() > 0 ? $"{SqMaster.Subledger_Id}, " : "NUll,"); //Subledger_Id
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.IND_Invoice) ? $"'{SqMaster.IND_Invoice}', " : "NUll,"); //IND_Invoice
            cmdString.Append(SqMaster.IND_Date != null ? $"'{SqMaster.IND_Date.GetSystemDate()}', " : "Null,"); //IND_Date
            cmdString.Append(SqMaster.Cls1.GetInt() > 0 ? $"{SqMaster.Cls1} , " : "NUll,"); //Cls1
            cmdString.Append("NULL,NULL,NULL, "); // Cls2, Cls3, Cls4,
            cmdString.Append(SqMaster.Cur_Id.GetInt() > 0 ? $"{SqMaster.Cur_Id} , " : $"'{ObjGlobal.SysCurrencyId}',"); //Cur_Id
            cmdString.Append(SqMaster.Cur_Rate.GetDecimal(true) > 0 ? $"'{SqMaster.Cur_Rate}'," : "1,"); //Cur_Rate
            cmdString.Append($"'{SqMaster.B_Amount.GetDecimal()}',"); //B_Amount
            cmdString.Append($"'{SqMaster.T_Amount.GetDecimal()}',"); //T_Amount
            cmdString.Append($"'{SqMaster.N_Amount.GetDecimal()}',"); //N_Amount
            cmdString.Append($"'{SqMaster.LN_Amount.GetDecimal()}',"); //LN_Amount
            cmdString.Append($"'{SqMaster.V_Amount.GetDecimal()}',"); //V_Amount
            cmdString.Append($"'{SqMaster.Tbl_Amount.GetDecimal()}', "); //Tbl_Amount
            cmdString.Append($"'{SqMaster.Tender_Amount.GetDecimal()}', "); //Tender_Amount
            cmdString.Append($"'{SqMaster.Return_Amount.GetDecimal()}', "); //Return_Amount
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Action_Type) ? $"'{SqMaster.Action_Type}', " : "'NEW',"); //Action_Type
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.In_Words) ? $"'{SqMaster.In_Words}', " : "NUll,"); //In_Words
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Remarks) ? $"'{SqMaster.Remarks}', " : "NUll,"); //Remarks
            cmdString.Append(SqMaster.R_Invoice.GetBool() ? "1," : "0,"); //R_Invoice
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Cancel_By) ? $"'{SqMaster.Cancel_By}', " : "Null,"); //Cancel_By
            cmdString.Append(SqMaster.Cancel_Date != null ? $"'{SqMaster.Cancel_Date.GetSystemDate()}', " : "Null,"); //Cancel_Date
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Cancel_Remarks) ? $"'{SqMaster.Cancel_Remarks}', " : "Null,"); //Cancel_Remarks
            cmdString.Append(SqMaster.Is_Printed.GetBool() ? "1," : "0,"); //Is_Printed
            cmdString.Append(SqMaster.No_Print.GetInt() > 0 ? $"'{SqMaster.No_Print}', " : "0,"); //No_Print
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Printed_By) ? $"'{SqMaster.Printed_By}', " : "Null,"); //Printed_By
            cmdString.Append(SqMaster.Printed_Date != null ? $"'{SqMaster.Printed_Date.GetSystemDate()}', " : "Null,"); //Printed_Date
            cmdString.Append(SqMaster.Audit_Lock.GetBool() ? "1," : "0,"); //Audit_Lock
            cmdString.Append($"'{ObjGlobal.LogInUser}',GETDATE(),"); //Enter_By, Enter_Date,
            cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL, "); //Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date,
            cmdString.Append($"NUll, {SqMaster.CBranch_Id}, '{ObjGlobal.SysFiscalYearId}',");
            cmdString.Append($"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,GETDATE(),{SqMaster.SyncRowVersion.GetDecimal(true)}); \n");

        }
        else if (actionTag == "DELETE" || actionTag == "UPDATE")
        {
            cmdString.Append($" Delete from AMS.SQ_Term where SQ_VNo = '{SqMaster.SQ_Invoice}'; \n");
            cmdString.Append($" Delete from AMS.SQ_Details where SQ_Invoice = '{SqMaster.SQ_Invoice}'; \n");
            if (actionTag == "DELETE")
            {
                cmdString.Append($" Delete from AMS.SQ_Master where SQ_Invoice = '{SqMaster.SQ_Invoice}'; \n");
            }
        }

        if (actionTag.ToUpper() != "DELETE" && !actionTag.Equals("REVERSE"))
        {
            if (DetailsList.Count > 0)
            {
                cmdString.Append(@" 
                    INSERT INTO AMS.SQ_Details(SQ_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, PG_Id, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date ,Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append(" \n VALUES \n");

                foreach (var dr in DetailsList)
                {
                    var index = DetailsList.IndexOf(dr);
                    cmdString.Append($"('{dr.SQ_Invoice}', {dr.Invoice_SNo}, {dr.P_Id},");
                    cmdString.Append(dr.Gdn_Id > 0 ? $@"{dr.Gdn_Id}," : "NULL,");
                    cmdString.Append($"{dr.Alt_Qty},");
                    cmdString.Append(dr.Alt_UnitId > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                    cmdString.Append($"{dr.Qty},");
                    cmdString.Append(dr.Unit_Id > 0 ? $"{dr.Unit_Id}," : "NULL,");
                    cmdString.Append($" {dr.Rate}, {dr.B_Amount}, {dr.T_Amount}, {dr.N_Amount},");
                    cmdString.Append($"{dr.AltStock_Qty}, {dr.Stock_Qty},");
                    cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
                    cmdString.Append(dr.IND_Invoice.IsValueExits() ? $"'{dr.IND_Invoice}', {dr.IND_Sno}, " : "NULL,0,");
                    cmdString.Append($"{dr.Tax_Amount},{dr.V_Amount} , {dr.V_Rate}, {dr.Issue_Qty}, ");
                    cmdString.Append(dr.Free_Unit_Id > 0 ? $"{dr.Free_Unit_Id}," : "NULL,");
                    cmdString.Append($"{dr.Free_Qty}, {dr.StockFree_Qty},");
                    cmdString.Append(dr.ExtraFree_Unit_Id > 0 ? $"{dr.ExtraFree_Unit_Id}," : "NULL,");
                    cmdString.Append($"{dr.ExtraFree_Qty}, {dr.ExtraStockFree_Qty},");
                    cmdString.Append(dr.PG_Id > 0 ? $"{dr.PG_Id}," : "NULL, ");
                    cmdString.Append($"CAST('{dr.T_Product}' AS BIT), ");
                    cmdString.Append(dr.S_Ledger > 0 ? $"{dr.S_Ledger}," : "NULL,");
                    cmdString.Append(dr.SR_Ledger > 0 ? $"{dr.SR_Ledger}," : "NULL,");
                    cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                    cmdString.Append($"NULL, NULL, NULL, NULL, ");
                    cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'," : "NULL,");
                    cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                    cmdString.Append($"'{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}',");
                    cmdString.Append($"{dr.SyncRowVersion}");
                    cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");

                }

                if (Terms.Count > 0)
                {
                    if (actionTag.Equals("UPDATE"))
                    {
                        cmdString.Append($"DELETE AMS.SQ_Term WHERE SQ_VNo='{SqMaster.SQ_Invoice}' AND Term_Type='P' \n");
                    }

                    cmdString.Append(@"INSERT INTO AMS.SQ_Term(SQ_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append(" \n VALUES \n");

                    foreach (var dr in Terms)
                    {
                        var index = Terms.IndexOf(dr);
                        cmdString.Append($" ('{dr.SQ_Vno}',");
                        cmdString.Append($"{dr.ST_Id.GetInt()},");
                        cmdString.Append(dr.SNo.GetInt() > 0 ? $"{dr.SNo}," : "1,");
                        cmdString.Append($"N'{dr.Term_Type}',");
                        cmdString.Append(dr.Product_Id.GetLong() > 0 ? $"{dr.Product_Id}," : "NULL,");
                        cmdString.Append(dr.Rate.GetDecimal() > 0 ? $"{dr.Rate.GetDecimal()}," : "0,");
                        cmdString.Append(dr.Amount.GetLong() > 0 ? $"{dr.Amount}," : "0,");
                        cmdString.Append($"N'{dr.Taxable}',");
                        cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'" : "NULL,");
                        cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                        cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                        cmdString.Append($"'{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}',");
                        cmdString.Append($"{dr.SyncRowVersion}");
                        cmdString.Append(index == Terms.Count - 1 ? "); \n" : "),\n");
                    }
                }
            }
        }

        var result = SaveDataInDatabase(cmdString);
        return result;
    }
    public int SalesQuotationTermPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
        DELETE AMS.SQ_Vno WHERE SQ_Vno='{SqMaster.SQ_Invoice}' AND Term_Type='BT';
		INSERT INTO AMS.SQ_Term(SQ_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
        SELECT sbt.SQ_Vno, sbt.ST_Id, sd.Invoice_SNo AS SNo, 'BT' Term_Type, sd.P_Id Product_Id, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, sbt.Taxable, sbt.SyncBaseId, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion
        FROM AMS.SQ_Details sd
		        LEFT OUTER JOIN AMS.SQ_Master sm ON sm.SQ_Invoice=sd.SQ_Invoice
		        LEFT OUTER JOIN AMS.SQ_Term sbt ON sd.SQ_Invoice=sbt.SQ_Vno
        WHERE sbt.SQ_Vno='{SqMaster.SQ_Invoice}' AND sbt.Term_Type='B' AND Product_Id IS NULL;");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }
    private int SaveDataInDatabase(StringBuilder query)
    {
        return ExecuteValueInDatabase(query.ToString());
    }

    private int ExecuteValueInDatabase(string query)
    {
        try
        {
            var iRows = SqlExtensions.ExecuteNonTrans(query);
            return iRows;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            return 0;
        }
    }
    // RETURN VALUE IN SHORT 
    #region  --------------- SHORT ---------------
    public short ReturnSyncRowVersionVoucher(string module, string voucherNo)
    {
        var cmdString = module switch
        {
            "PIN" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PIN_Master pm WHERE pm.PIN_Invoice = '{voucherNo}'",
            "PO" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PO_Master pm WHERE pm.PO_Invoice = '{voucherNo}'",
            "PC" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PC_Master pm WHERE pm.PC_Invoice = '{voucherNo}'",
            "GIT" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.GIT_Master pm WHERE pm.GIT_Invoice = '{voucherNo}'",
            "PB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PB_Master pm WHERE pm.PB_Invoice = '{voucherNo}'",
            "PR" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PR_Master pm WHERE pm.PR_Invoice = '{voucherNo}'",
            "PAB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.VmPabMaster pm WHERE pm.PAB_Invoice = '{voucherNo}'",
            _ => string.Empty
        };
        var result = cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();

        return result.GetHashCode() > 0 ? result : (short)1;
    }
    #endregion

    // DATA TABLE FUNCTION
    public DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo)
    {
        var cmdString = $@" 
            SELECT PB_Vno FROM AMS.PB_Master where Vendor_ID = '{ledgerId}' and PB_Vno = '{txtRefVno}'  AND FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        if (action != "SAVE") cmdString += $" AND PB_Invoice <> '{voucherNo}' and Vendor_ID <> '{ledgerId}'  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public SQ_Master SqMaster { get; set; }
    public List<SQ_Details> DetailsList { get; set; }
    public List<SQ_Term> Terms { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}