using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using DatabaseModule.DataEntry.SalesMaster.SalesQuotation;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.Domains.NightAudit;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.TransactionClass;

public class ClsSalesEntry : ISalesEntry
{
    // CONSTRUCTOR

    #region --------------- CONSTRUCTOR ---------------
    public ClsSalesEntry()
    {
        TsbMaster = new Temp_SB_Master();
        SqMaster = new SQ_Master();
        SoMaster = new SO_Master();
        ScMaster = new SC_Master();
        SbMaster = new SB_Master();
        SbDetails = new List<SB_Details>();
        SbTerms = new List<SB_Term>();
        SbOther = new SB_Master_OtherDetails();
        SrMaster = new SR_Master();
        SrDetails = new List<SR_Details>();
        SrTerms = new List<SR_Term>();
        SoDetails = new SO_Details();
        AuditLog = new NightAuditLog();
        IsbDetails = new List<SB_Details>();
    }

    #endregion --------------- CONSTRUCTOR ---------------

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

    // AUDIT LOG OF SALES

    #region --------------- AUDIT_LOG ---------------
    private int AuditLogSalesReturn(string actionTag)
    {
        var cmdString = @$"
		INSERT INTO [AUD].[AUDIT_SR_Master] ([SR_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[SB_Invoice] ,[SB_Date] ,[SB_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Subledger_Id] ,[Agent_Id] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date] ,[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[Cancel_By] ,[Cancel_Date] ,[Cancel_Remarks] ,[IsAPIPosted] ,[IsRealtime] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT  [SR_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[SB_Invoice] ,[SB_Date] ,[SB_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] , [Subledger_Id],[Agent_ID]  ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date],[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[Cancel_By] ,[Cancel_Date] ,[Cancel_Remarks] ,[IsAPIPosted] ,[IsRealtime] ,[CBranch_Id], [CUnit_Id] ,[FiscalYearId] ,'{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SR_Master] where SR_Invoice = '{SrMaster.SR_Invoice}';
		
        INSERT INTO AUD.AUDIT_SR_Details (SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, ModifyAction, ModifyBy, ModifyDate)
		SELECT SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.SR_Details sd where SR_Invoice = '{SrMaster.SR_Invoice}'
		
        INSERT INTO AUD.AUDIT_SR_Term (SR_VNo, ST_Id, SNo, Product_Id, Term_Type, Rate, Amount, Taxable, ModifyAction, ModifyBy, ModifyDate)
		SELECT SR_VNo, ST_Id, SNo, Product_Id, Term_Type, Rate, Amount, Taxable,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.SR_Term st WHERE st.SR_VNo='{SrMaster.SR_Invoice}'";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    private int AuditLogSalesChallan(string actionTag)
    {
        var cmdString = @$"
		INSERT INTO [AUD].[AUDIT_SC_Master] ([SC_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_Vno] ,[Ref_Date] ,[Ref_Miti] ,[Customer_Id] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_Id] ,[Subledger_Id] ,[QOT_Invoice] ,[QOT_Date] ,[SO_Invoice] ,[SO_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId] ,[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_VNo] ,[Ref_Date] ,[Ref_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_ID] ,[Subledger_Id] ,[QOT_Invoice] ,[QOT_Date] ,[SO_Invoice] ,[SO_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId], [Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,'{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.SC_Master where SC_Invoice = '{ScMaster.SC_Invoice}';
		
        INSERT INTO [AUD].[AUDIT_SC_Term] ([SC_Vno] ,[ST_Id] ,[Product_Id] ,[SNo] ,[Term_Type] ,[Rate] ,[Amount] ,[Taxable] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Vno] ,[ST_Id] ,[SNo] ,[Rate] ,[Amount] ,[Term_Type] ,[Product_Id] ,[Taxable]  , ' {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SC_Term] where SC_Vno = '{ScMaster.SC_Invoice}';
		
        INSERT INTO [AUD].[AUDIT_SC_Details] ([SC_Invoice] ,[Invoice_SNo] ,[P_Id] ,[Gdn_Id] ,[Alt_Qty] ,[Alt_UnitId] ,[Qty] ,[Unit_Id] ,[Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[AltStock_Qty] ,[Stock_Qty] ,[Narration] ,[QOT_Invoice] ,[QOT_Sno] ,[SO_Invoice] ,[SO_SNo] ,[Tax_Amount] ,[V_Amount] ,[V_Rate] ,[Issue_Qty] ,[Free_Unit_Id] ,[Free_Qty] ,[StockFree_Qty] ,[ExtraFree_Unit_Id] ,[ExtraFree_Qty] ,[ExtraStockFree_Qty] ,[T_Product] ,[S_Ledger] ,[SR_Ledger] ,[SZ1] ,[SZ2] ,[SZ3] ,[SZ4] ,[SZ5] ,[SZ6] ,[SZ7] ,[SZ8] ,[SZ9] ,[SZ10] ,[Serial_No] ,[Batch_No] ,[Exp_Date] ,[Manu_Date] ,[AltIssue_Qty] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Invoice]	,[Invoice_SNo]	,[P_Id]	,[Gdn_Id]	,[Alt_Qty]	,[Alt_UnitId]	,[Qty]	,[Unit_Id]	,[Rate]	,[B_Amount]	,[T_Amount]	,[N_Amount]	,[AltStock_Qty]	,[Stock_Qty]	,[Narration]	,[QOT_Invoice]	,[QOT_SNo]	,[SO_Invoice]	,[SO_Sno]	,[Tax_Amount]	,[V_Amount]	,[V_Rate]	,[Issue_Qty]	,[Free_Unit_Id]	,[Free_Qty]	,[StockFree_Qty]	,[ExtraFree_Unit_Id]	,[ExtraFree_Qty]	,[ExtraStockFree_Qty]	,[T_Product]	,[S_Ledger]	,[SR_Ledger]	,[SZ1]	,[SZ2]	,[SZ3]	,[SZ4]	,[SZ5]	,[SZ6]	,[SZ7]	,[SZ8]	,[SZ9]	,[SZ10]	,[Serial_No]	,[Batch_No]	,[Exp_Date]	,[Manu_Date]	,[AltIssue_Qty]  , ' {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SC_Details] where SC_Invoice = '{ScMaster.SC_Invoice}';  ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    #endregion --------------- AUDIT_LOG ---------------

    // SAVE SALES DETAILS

    #region --------------- IUD SALES  ---------------

    public int SaveSalesQuotation(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "NEW" || actionTag == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO [AMS].[SQ_Master](SQ_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Expiry_Date, Ref_Vno, Ref_VDate, Ref_VMiti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, FiscalYearId) \n");
            cmdString.Append(" VALUES ");
            cmdString.Append($"('{SqMaster.SQ_Invoice}','{Convert.ToDateTime(SqMaster.Invoice_Date):yyyy-MM-dd}','{SqMaster.Invoice_Miti}', GETDATE(),");
            cmdString.Append(string.IsNullOrEmpty(SqMaster.Expiry_Date.ToString())
                ? $"'{Convert.ToDateTime(SqMaster.Expiry_Date):yyyy-MM-dd}', "
                : "Null,"); //Expiry_Date
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Ref_Vno) ? $"'{SqMaster.Ref_Vno}'," : "Null,"); //refvno
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Ref_VMiti)
                ? $"'{SqMaster.Ref_VMiti}', "
                : "Null,"); //refv
            cmdString.Append(SqMaster.Customer_Id.GetLong() > 0
                ? $"'{SqMaster.Customer_Id}', "
                : "Null,"); //Customer_ID
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Party_Name)
                ? $"'{SqMaster.Party_Name}', "
                : "NUll,"); //Party_Name
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Vat_No) ? $"'{SqMaster.Vat_No}', " : "NUll,"); //Vat_No
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Contact_Person)
                ? $"'{SqMaster.Contact_Person}', "
                : "NUll,"); //Contact_Person
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Mobile_No)
                ? $"'{SqMaster.Mobile_No}', "
                : "NUll,"); //Mobile_No
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Address)
                ? $"'{SqMaster.Address}', "
                : "NUll,"); //Address
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.ChqNo) ? $"'{SqMaster.ChqNo}', " : "NUll,"); //ChqNo
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.ChqNo) ? $"'{SqMaster.ChqDate}', " : "NUll,"); //ChqDate
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Invoice_Type)
                ? $"'{SqMaster.Invoice_Type}',  "
                : "NUll,"); //Invoice_Type
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Invoice_Mode)
                ? $"'{SqMaster.Invoice_Mode}',"
                : "NUll,"); //Invoice_Mode
            cmdString.Append(
                !string.IsNullOrEmpty(SqMaster.Payment_Mode)
                    ? $"'{SqMaster.Payment_Mode}', "
                    : "NUll,"); //Payment_Mode
            cmdString.Append(SqMaster.DueDays.GetDecimal() > 0 ? $"'{SqMaster.DueDays}', " : "0,"); //DueDays
            cmdString.Append(SqMaster.Agent_Id.GetInt() > 0 ? $"'{SqMaster.Agent_Id}', " : "NUll,"); //Agent_Id
            cmdString.Append(SqMaster.Subledger_Id.GetInt() > 0
                ? $"'{SqMaster.Subledger_Id}', "
                : "NUll,"); //Subledger_Id
            cmdString.Append(SqMaster.Cls1.GetInt() > 0 ? $"'{SqMaster.Cls1}', " : "NUll,"); //Cls1
            cmdString.Append("NULL,NULL,NULL, "); // Cls2, Cls3, Cls4,
            cmdString.Append(SqMaster.Cur_Id.GetInt() > 0
                ? $"'{SqMaster.Cur_Id}', "
                : $"'{ObjGlobal.SysCurrencyId}',"); //Cur_Id
            cmdString.Append(SqMaster.Cur_Rate.GetDecimal(true) > 0 ? $"'{SqMaster.Cur_Rate}'," : "1,"); //Cur_Rate
            cmdString.Append($"'{SqMaster.B_Amount.GetDecimal()}',"); //B_Amount
            cmdString.Append($"'{SqMaster.T_Amount.GetDecimal()}',"); //T_Amount
            cmdString.Append($"'{SqMaster.N_Amount.GetDecimal()}',"); //N_Amount
            cmdString.Append($"'{SqMaster.LN_Amount.GetDecimal()}',"); //LN_Amount
            cmdString.Append($"'{SqMaster.V_Amount.GetDecimal()}',"); //V_Amount
            cmdString.Append($"'{SqMaster.Tbl_Amount.GetDecimal()}', "); //Tbl_Amount
            cmdString.Append($"'{SqMaster.Tender_Amount.GetDecimal()}', "); //Tender_Amount
            cmdString.Append($"'{SqMaster.Return_Amount.GetDecimal()}', "); //Return_Amount
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Action_Type)
                ? $"'{SqMaster.Action_Type}', "
                : "'NEW',"); //Action_Type
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.In_Words)
                ? $"'{SqMaster.In_Words}', "
                : "NUll,"); //In_Words
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Remarks) ? $"'{SqMaster.Remarks}', " : "NUll,"); //Remarks
            cmdString.Append(SqMaster.R_Invoice.GetBool() ? "1," : "0,"); //R_Invoice
            cmdString.Append(SqMaster.Is_Printed.GetBool() ? "1," : "0,"); //Is_Printed
            cmdString.Append(SqMaster.No_Print.GetInt() > 0 ? $"'{SqMaster.No_Print}', " : "0,"); //No_Print
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Printed_By)
                ? $"'{SqMaster.Printed_By}', "
                : "Null,"); //Printed_By
            cmdString.Append(SqMaster.Printed_Date != null
                ? $"'{Convert.ToDateTime(SqMaster.Printed_Date):yyyy-MM-dd}', "
                : "Null,"); //Printed_Date
            cmdString.Append(SqMaster.Audit_Lock.GetBool() ? "1," : "0,"); //Audit_Lock
            cmdString.Append($"'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),"); //Enter_By, Enter_Date,
            cmdString.Append(
                "NULL,NULL,NULL,NULL,NULL,NULL, "); //Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date,
            cmdString.Append(!string.IsNullOrEmpty(SqMaster.Cancel_By)
                ? $"'{SqMaster.Cancel_By}', "
                : "Null,"); //Cancel_By
            cmdString.Append(SqMaster.Cancel_Date != null
                ? $"'{Convert.ToDateTime(SqMaster.Cancel_Date):yyyy-MM-dd}', "
                : "Null,"); //Cancel_Date
            cmdString.Append(
                !string.IsNullOrEmpty(SqMaster.Cancel_Remarks)
                    ? $"'{SqMaster.Cancel_Remarks}', "
                    : "Null,"); //Cancel_Remarks
            //cmdString.Append(ObjGlobal.SysCompanyUnitId.GetInt()) > 0 ? $"'{ObjGlobal.SysCompanyUnitId}', " : "Null, ");
            //cmdString.Append(ObjGlobal.SysBranchId.GetInt()) > 0 ? $"'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append($"NUll, NUll, '{ObjGlobal.SysFiscalYearId}'); \n");
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

        if (actionTag.ToUpper() != "DELETE")
        {
            if (SqMaster.GetView != null && SqMaster.GetView.Rows.Count > 0)
            {

            }

            if (SqMaster.dtPTerm != null && SqMaster.dtPTerm.Rows.Count > 0)
            {
            }

            if (SqMaster.dtBTerm != null && SqMaster.dtBTerm.Rows.Count > 0)
            {
            }
        }

        var result = SaveDataInDatabase(cmdString);
        return result;
    }
    public int SaveUnSyncSalesChallanFromServerAsync(SC_Master SC_Master, string actionTag)
    {
        try
        {
            var query = $"SELECT SC_Invoice FROM AMS.SC_Master WHERE SC_Invoice='{SC_Master.SC_Invoice}'";
            var isAlreadExist = GetConnection.GetQueryData(query);
            actionTag = string.IsNullOrEmpty(isAlreadExist) ? "SAVE" : "UPDATE";
            ScMaster.SC_Invoice = SC_Master.SC_Invoice;
            ScMaster.Invoice_Date = SC_Master.Invoice_Date;
            ScMaster.Invoice_Miti = SC_Master.Invoice_Miti;
            ScMaster.Ref_Vno = SC_Master.Ref_Vno;
            ScMaster.Ref_Miti = SC_Master.Ref_Miti;
            ScMaster.Ref_Date = SC_Master.Ref_Date;
            ScMaster.Invoice_Time = SC_Master.Invoice_Time;
            ScMaster.Customer_Id = SC_Master.Customer_Id;

            ScMaster.Party_Name = SC_Master.Party_Name;
            ScMaster.Vat_No = SC_Master.Vat_No;
            ScMaster.Contact_Person = SC_Master.Contact_Person;
            ScMaster.Mobile_No = SC_Master.Mobile_No;
            ScMaster.Address = SC_Master.Address;
            ScMaster.ChqNo = SC_Master.ChqNo;
            ScMaster.ChqDate = SC_Master.ChqDate;

            ScMaster.Invoice_Type = SC_Master.Invoice_Type;
            ScMaster.Invoice_Mode = SC_Master.Invoice_Mode;
            ScMaster.Payment_Mode = SC_Master.Payment_Mode;
            ScMaster.DueDays = SC_Master.DueDays.GetInt();
            ScMaster.DueDate = SC_Master.DueDate;
            ScMaster.Agent_Id = SC_Master.Agent_Id;
            ScMaster.Subledger_Id = SC_Master.Subledger_Id;

            ScMaster.SO_Invoice = SC_Master.SO_Invoice;
            ScMaster.SO_Date = SC_Master.SO_Date;

            ScMaster.QOT_Invoice = SC_Master.QOT_Invoice;
            ScMaster.QOT_Date = SC_Master.QOT_Date;

            ScMaster.Cls1 = SC_Master.Cls1;
            ScMaster.Cls2 = 0;
            ScMaster.Cls3 = 0;
            ScMaster.Cls4 = 0;
            ScMaster.Cur_Id = SC_Master.Cur_Id;
            ScMaster.Cur_Rate = SC_Master.Cur_Rate;
            ScMaster.CounterId = SC_Master.CounterId;
            ScMaster.B_Amount = SC_Master.B_Amount;
            ScMaster.T_Amount = SC_Master.T_Amount;
            ScMaster.N_Amount = SC_Master.N_Amount;
            ScMaster.LN_Amount = SC_Master.LN_Amount;
            ScMaster.Tender_Amount = SC_Master.Tender_Amount;
            ScMaster.Return_Amount = SC_Master.Return_Amount;
            ScMaster.V_Amount = SC_Master.V_Amount;
            ScMaster.Tbl_Amount = SC_Master.Tbl_Amount;
            ScMaster.Action_Type = actionTag;
            ScMaster.R_Invoice = SC_Master.R_Invoice;
            ScMaster.No_Print = SC_Master.No_Print;
            ScMaster.In_Words = SC_Master.In_Words;
            ScMaster.Remarks = SC_Master.Remarks;
            ScMaster.Audit_Lock = SC_Master.Audit_Lock;

            //GetImage.VoucherNo = SC_Master.SC_Invoice;
            //GetImage.PAttachment1 = SC_Master.PAttachment1;
            //GetImage.PAttachment2 = SC_Master.PAttachment2;
            //GetImage.PAttachment3 = SC_Master.PAttachment3;
            //GetImage.PAttachment4 = SC_Master.PAttachment4;
            //GetImage.PAttachment5 = SC_Master.PAttachment5;
            foreach (var sco in SC_Master.OtherDetails)
            {
                SbOther.SB_Invoice = ScMaster.SC_Invoice;
                SbOther.Transport = sco.Transport;
                SbOther.VechileNo = sco.VechileNo;
                SbOther.BiltyNo = sco.BiltyNo;
                SbOther.Package = sco.Package;
                SbOther.BiltyDate = sco.BiltyDate;
                SbOther.BiltyType = sco.BiltyType;
                SbOther.Driver = sco.Driver;
                SbOther.PhoneNo = sco.PhoneNo;
                SbOther.LicenseNo = sco.LicenseNo;
                SbOther.MailingAddress = sco.MailingAddress;
                SbOther.MCity = sco.MCity;
                SbOther.MState = sco.MState;
                SbOther.MCountry = sco.MCountry;
                SbOther.MEmail = sco.MEmail;
                SbOther.ShippingAddress = sco.ShippingAddress;
                SbOther.SCity = sco.SCity;
                SbOther.SState = sco.SState;
                SbOther.SCountry = sco.SCountry;
                SbOther.SEmail = sco.SEmail;
                SbOther.ContractNo = sco.ContractNo;
                SbOther.ContractDate = sco.ContractDate;
                SbOther.ExportInvoice = sco.ExportInvoice;
                SbOther.ExportInvoiceDate = sco.ExportInvoiceDate;
                SbOther.BankDetails = sco.BankDetails;
                SbOther.LcNumber = sco.LcNumber;
                SbOther.CustomDetails = sco.CustomDetails;
            }

            var cmdString = new StringBuilder();
            if (actionTag is "DELETE" or "REVERSE" or "UPDATE")
            {
                if (!actionTag.Equals("UPDATE"))
                {
                    AuditLogSalesChallan(actionTag);
                }

                if (!actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($" Delete from AMS.SC_Term where SC_VNo ='{ScMaster.SC_Invoice}'; \n");
                    cmdString.Append($" Delete from AMS.SC_Details where SC_Invoice ='{ScMaster.SC_Invoice}'; \n");
                }

                cmdString.Append(
                    $" Delete from AMS.StockDetails Where module='SC' and Voucher_No ='{ScMaster.SC_Invoice}'; \n");
                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append(
                        $" Delete from AMS.SC_Master_OtherDetails where SC_Invoice = '{ScMaster.SC_Invoice}'; \n");
                    cmdString.Append($" Delete from AMS.SC_Master where SC_Invoice ='{ScMaster.SC_Invoice}'; \n");
                }
            }

            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(
                    " INSERT INTO AMS.SC_Master(SC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, QOT_Invoice, QOT_Date, SO_Invoice, SO_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)\n");
                cmdString.Append(
                    $" VALUES (N'{ScMaster.SC_Invoice}', '{ScMaster.Invoice_Date:yyyy-MM-dd}', N'{ScMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(ScMaster.Ref_Vno.IsValueExits()
                    ? $" N'{ScMaster.Ref_Vno}', '{ScMaster.Ref_Date:yyyy-MM-dd}','{ScMaster.Ref_Miti}',"
                    : "NULL,NULL,NULL,");
                cmdString.Append($" {ScMaster.Customer_Id},");
                cmdString.Append(ScMaster.PartyLedgerId > 0 ? $" {ScMaster.PartyLedgerId}," : "NULL,");
                cmdString.Append(ScMaster.Party_Name.IsValueExits() ? $" N'{ScMaster.Party_Name}'," : "NULL,");
                cmdString.Append(ScMaster.Vat_No.IsValueExits() ? $" N'{ScMaster.Vat_No}'," : "NULL,");
                cmdString.Append(ScMaster.Contact_Person.IsValueExits() ? $" N'{ScMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(ScMaster.Mobile_No.IsValueExits() ? $" N'{ScMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(ScMaster.Address.IsValueExits() ? $" N'{ScMaster.Address}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" N'{ScMaster.ChqNo}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" '{ScMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" '{ScMaster.ChqMiti}'," : "NULL,");
                cmdString.Append(
                    $"N'{ScMaster.Invoice_Type}', N'{ScMaster.Invoice_Mode}', '{ScMaster.Payment_Mode}', {ScMaster.DueDays.GetInt()},");
                cmdString.Append(ScMaster.DueDays > 0 ? $" '{ScMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(ScMaster.Agent_Id > 0 ? $" {ScMaster.Agent_Id}," : "NULL,");
                cmdString.Append(ScMaster.Subledger_Id > 0 ? $" {ScMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits() ? $" N'{ScMaster.QOT_Invoice}'," : "NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits()
                    ? $" N'{ScMaster.QOT_Date:yyyy-MM-dd}',"
                    : "NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits() ? $" N'{ScMaster.SO_Invoice}'," : "NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits() ? $" N'{ScMaster.SO_Date:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(ScMaster.Cls1 > 0 ? $" {ScMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(ScMaster.CounterId > 0 ? $" {ScMaster.CounterId}," : "NULL,");
                cmdString.Append(ScMaster.Cur_Id > 0 ? $" {ScMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
                cmdString.Append($" {ScMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($" {ScMaster.B_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.T_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.N_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.V_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"'{ScMaster.Action_Type}',");

                cmdString.Append("0,NULL,NULL,NULL,0,");
                cmdString.Append(ScMaster.In_Words.IsValueExits() ? $" N'{ScMaster.In_Words}'," : "NULL,");
                cmdString.Append(ScMaster.Remarks.IsValueExits()
                    ? $" N'{ScMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "NULL,");
                cmdString.Append($"0,'{ObjGlobal.LogInUser}',GETDATE(),NULL,NULL,NULL,NULL,{ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append($" {ObjGlobal.SysFiscalYearId},");
                cmdString.Append(
                    $" NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{ScMaster.SyncRowVersion.GetDecimal(true)}); \n");
            }
            else if (actionTag.Equals("UPDATE"))
            {
                cmdString.Append("UPDATE AMS.SC_Master SET ");
                cmdString.Append(
                    $"Invoice_Date = '{ScMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{ScMaster.Invoice_Miti}',");
                cmdString.Append(ScMaster.Ref_Vno.IsValueExits()
                    ? $" Ref_Vno = N'{ScMaster.Ref_Vno}',Ref_Date = '{ScMaster.Ref_Date:yyyy-MM-dd}',Ref_Miti = '{ScMaster.Ref_Miti}',"
                    : "Ref_Vno = NULL,Ref_Date = NULL,Ref_Miti = NULL,");
                cmdString.Append($" Customer_Id = {ScMaster.Customer_Id},");
                cmdString.Append(ScMaster.Party_Name.IsValueExits()
                    ? $" Party_Name = N'{ScMaster.Party_Name}',"
                    : "Party_Name = NULL,");
                cmdString.Append(ScMaster.Vat_No.IsValueExits()
                    ? $"Vat_No =  N'{ScMaster.Vat_No}',"
                    : "Vat_No = NULL,");
                cmdString.Append(ScMaster.Contact_Person.IsValueExits()
                    ? $" Contact_Person = N'{ScMaster.Contact_Person}',"
                    : "Contact_Person = NULL,");
                cmdString.Append(ScMaster.Mobile_No.IsValueExits()
                    ? $" Mobile_No = N'{ScMaster.Mobile_No}',"
                    : "Mobile_No = NULL,");
                cmdString.Append(ScMaster.Address.IsValueExits()
                    ? $" Address = N'{ScMaster.Address}',"
                    : "Address = NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{ScMaster.ChqNo}'," : "ChqNo = NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits()
                    ? $" ChqDate = N'{ScMaster.ChqDate:yyyy-MM-dd}',"
                    : "ChqDate = NULL,");
                cmdString.Append(
                    $"Invoice_Type = N'{ScMaster.Invoice_Type}', Invoice_Mode = N'{ScMaster.Invoice_Mode}',Payment_Mode =  '{ScMaster.Payment_Mode}',DueDays =  {ScMaster.DueDays},");
                cmdString.Append(ScMaster.DueDays > 0
                    ? $" DueDate = '{ScMaster.DueDate:yyyy-MM-dd}',"
                    : "DueDate = NULL,");
                cmdString.Append(ScMaster.Agent_Id > 0 ? $" Agent_Id = {ScMaster.Agent_Id}," : "Agent_Id = NULL,");
                cmdString.Append(ScMaster.Subledger_Id > 0
                    ? $" Subledger_Id = {ScMaster.Subledger_Id},"
                    : "Subledger_Id = NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits()
                    ? $" SO_Invoice = N'{ScMaster.SO_Invoice}',"
                    : "SO_Invoice = NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits()
                    ? $" SO_Date = N'{ScMaster.SO_Date:yyyy-MM-dd}',"
                    : "SO_Date = NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits()
                    ? $" QOT_Invoice = N'{ScMaster.QOT_Invoice}',"
                    : "QOT_Invoice = NULL,");
                cmdString.Append(ScMaster.QOT_Date.IsValueExits()
                    ? $" QOT_Date = N'{ScMaster.QOT_Date:yyyy-MM-dd}',"
                    : "SC_Date = NULL,");
                cmdString.Append(ScMaster.Cls1 > 0 ? $" Cls1 = {ScMaster.Cls1}," : "Cls1 = NULL,");
                cmdString.Append(ScMaster.CounterId > 0 ? $"CounterId = {ScMaster.CounterId}," : " CounterId = NULL,");
                cmdString.Append(ScMaster.Cur_Id > 0 ? $" Cur_Id = {ScMaster.Cur_Id}," : "Cur_Id = 1,");
                cmdString.Append($"Cur_Rate =  {ScMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($"B_Amount =  {ScMaster.B_Amount.GetDecimal()},");
                cmdString.Append($"T_Amount = {ScMaster.T_Amount.GetDecimal()},");
                cmdString.Append($"N_Amount = {ScMaster.N_Amount.GetDecimal()},");
                cmdString.Append($"LN_Amount = {ScMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($"V_Amount =  {ScMaster.V_Amount.GetDecimal()},");
                cmdString.Append($"Tbl_Amount =  {ScMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($" Tender_Amount = {ScMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($"Return_Amount =  {ScMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"Action_Type = '{ScMaster.Action_Type}',");
                cmdString.Append(ScMaster.In_Words.IsValueExits()
                    ? $"In_Words =  N'{ScMaster.In_Words}',"
                    : "In_Words = NULL,");
                cmdString.Append(ScMaster.Remarks.IsValueExits()
                    ? $" Remarks = N'{ScMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "Remarks = NULL,");
                cmdString.Append("IsSynced=0");
                cmdString.Append($" WHERE SC_Invoice = N'{ScMaster.SC_Invoice}'; \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (SC_Master.DetailsList.Count > 0)
                {
                    var iRows = 0;
                    cmdString.Append(
                        " INSERT INTO AMS.SC_Details(SC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, QOT_Invoice, QOT_Sno, SO_Invoice, SO_SNo, Tax_Amount, V_Amount, V_Rate, AltIssue_Qty, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                    cmdString.Append(" VALUES \n");
                    foreach (var dr in SC_Master.DetailsList)
                    {
                        iRows++;
                        cmdString.Append($"(N'{ScMaster.SC_Invoice}',");
                        cmdString.Append($"{dr.Invoice_SNo.GetInt()},");
                        cmdString.Append($"{dr.P_Id},");
                        cmdString.Append(dr.Gdn_Id.GetInt() > 0 ? $"{dr.Gdn_Id}," : "NULL,");
                        cmdString.Append(dr.Alt_Qty.GetDecimal() > 0 ? $"{dr.Alt_Qty}," : "0,");
                        cmdString.Append(dr.Alt_UnitId.GetInt() > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                        cmdString.Append(dr.Qty.GetDecimal() > 0 ? $"{dr.Qty}," : "1,");
                        cmdString.Append(dr.Unit_Id.GetInt() > 0 ? $"{dr.Unit_Id}," : "NUll,");
                        cmdString.Append($"{dr.Rate.GetDecimal()},");
                        cmdString.Append($"{dr.B_Amount.GetDecimal()},");
                        cmdString.Append($"{dr.T_Amount.GetDecimal()},");
                        cmdString.Append($"{dr.N_Amount.GetDecimal()},");
                        cmdString.Append($"{dr.AltStock_Qty.GetDecimal()},");
                        cmdString.Append($"{dr.Stock_Qty.GetDecimal()},");
                        cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration.GetTrimReplace()}'," : "NULL,");

                        cmdString.Append(dr.QOT_Invoice.IsValueExits() ? $"'{dr.QOT_Invoice}'," : "NULL,");
                        cmdString.Append(dr.QOT_Sno.IsValueExits() ? $"'{dr.QOT_Sno}'," : "0,");

                        cmdString.Append(dr.SO_Invoice.IsValueExits() ? $"'{dr.SO_Invoice}'," : "NULL,");
                        cmdString.Append(dr.SO_SNo.IsValueExits() ? $"'{dr.SO_SNo}'," : "0,");

                        cmdString.Append(
                            "0,0,0,0,0,NULL,0,0, NULL, 0, 0,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
                        cmdString.Append(dr.Serial_No.IsValueExits() ? $"'{dr.Serial_No}'," : "NULL,");
                        cmdString.Append(dr.Batch_No.IsValueExits() ? $"'{dr.Batch_No}'," : "NULL,");
                        cmdString.Append(dr.Batch_No.IsValueExits() ? $"'{dr.Exp_Date.GetSystemDate()}'," : "NULL,");
                        cmdString.Append(dr.Batch_No.IsValueExits() ? $"'{dr.Manu_Date.GetSystemDate()}'," : "NULL,");
                        cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{ScMaster.SyncRowVersion.GetDecimal(true)}");
                        cmdString.Append(iRows == SC_Master.DetailsList.Count ? " );" : "),");
                        cmdString.Append(" \n");
                    }
                }

                if (SC_Master.Terms != null && SC_Master.Terms.Count > 0)
                {
                    if (actionTag.Equals("UPDATE"))
                    {
                        cmdString.Append(
                            $"DELETE AMS.SC_Term WHERE SC_Vno='{ScMaster.SC_Invoice}' AND Term_Type='P' \n");
                    }

                    foreach (var dr in SC_Master.Terms)
                    {
                        if (dr.Amount.GetDecimal() == 0)
                        {
                            continue;
                        }

                        cmdString.Append(@" 
                        INSERT INTO AMS.SC_Term(SC_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                        cmdString.Append($" VALUES ('{ScMaster.SC_Invoice}',");
                        cmdString.Append($"{dr.ST_Id},");
                        cmdString.Append(dr.SNo.GetInt() > 0 ? $"{dr.SNo}," : "1,");
                        cmdString.Append($"'{dr.Term_Type}',");
                        cmdString.Append(dr.Product_Id.GetInt() > 0 ? $"{dr.Product_Id}," : "NULL,");
                        cmdString.Append($"{dr.Rate.GetDecimal()},{dr.Amount.GetDecimal()},");
                        cmdString.Append($"'{dr.Taxable}',");
                        cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{ScMaster.SyncRowVersion.GetInt()}); \n");
                    }
                }
            }

            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            if (_tagStrings.Contains(actionTag))
            {
                return iResult;
            }
            Task.Run(() => UpdateSyncSalesChallanColumnInServerAsync(actionTag));
            SaveSalesOtherDetails("SC", actionTag);
            UpdateImageOnSales("SC");
            _ = SalesChallanTermPosting();
            _ = SalesChallanStockPosting();
            if (ScMaster.SO_Invoice.IsValueExits() || ScMaster.QOT_Invoice.IsValueExits())
            {
                UpdateOrderChallanOnChallan();
            }
            return iResult;
        }
        catch (Exception e)
        {
            CreateDatabaseTable.CreateTrigger();
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
    }
    public async Task<int> UpdateSyncSalesChallanColumnInServerAsync(string actionTag)
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

            getUrl = @$"{configParams.Model.Item2}SalesChallan/GetSalesChallanById";
            insertUrl = @$"{configParams.Model.Item2}SalesChallan/InsertSalesChallan";
            updateUrl = @$"{configParams.Model.Item2}SalesChallan/UpdateSyncSalesChallanColumn";
            deleteUrl = @$"{configParams.Model.Item2}SalesChallan/DeleteSalesChallanAsync?id=" + ScMaster.SC_Invoice;

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

            var salesChallanRepo =
                DataSyncProviderFactory.GetRepository<SC_Master>(DataSyncManager.GetGlobalInjectData());
            var sc = new SC_Master();
            sc.Action_Type = "UPDATE";
            sc.SC_Invoice = ScMaster.SC_Invoice;
            sc.Invoice_Date = ScMaster.Invoice_Date;
            sc.Invoice_Miti = ScMaster.Invoice_Miti;
            sc.Ref_Vno = ScMaster.Ref_Vno;
            sc.Ref_Miti = ScMaster.Ref_Miti;
            sc.Ref_Date = ScMaster.Ref_Date;
            sc.Invoice_Time = ScMaster.Invoice_Time;
            sc.Customer_Id = ScMaster.Customer_Id;

            sc.Party_Name = ScMaster.Party_Name;
            sc.Vat_No = ScMaster.Vat_No;
            sc.Contact_Person = ScMaster.Contact_Person;
            sc.Mobile_No = ScMaster.Mobile_No;
            sc.Address = ScMaster.Address;
            sc.ChqNo = ScMaster.ChqNo;
            sc.ChqDate = ScMaster.ChqDate;

            sc.Invoice_Type = ScMaster.Invoice_Type;
            sc.Invoice_Mode = ScMaster.Invoice_Mode;
            sc.Payment_Mode = ScMaster.Payment_Mode;
            sc.DueDays = ScMaster.DueDays.GetInt();
            sc.DueDate = ScMaster.DueDate;
            sc.Agent_Id = ScMaster.Agent_Id;
            sc.Subledger_Id = ScMaster.Subledger_Id;

            sc.SO_Invoice = ScMaster.SO_Invoice;
            sc.SO_Date = ScMaster.SO_Date;

            sc.QOT_Invoice = ScMaster.QOT_Invoice;
            sc.QOT_Date = ScMaster.QOT_Date;

            sc.Cls1 = ScMaster.Cls1;
            sc.Cls2 = 0;
            sc.Cls3 = 0;
            sc.Cls4 = 0;
            sc.Cur_Id = ScMaster.Cur_Id;
            sc.Cur_Rate = ScMaster.Cur_Rate;
            sc.CounterId = ScMaster.CounterId;
            sc.B_Amount = ScMaster.B_Amount;
            sc.T_Amount = ScMaster.T_Amount;
            sc.N_Amount = ScMaster.N_Amount;
            sc.LN_Amount = ScMaster.LN_Amount;
            sc.Tender_Amount = ScMaster.Tender_Amount;
            sc.Return_Amount = ScMaster.Return_Amount;
            sc.V_Amount = ScMaster.V_Amount;
            sc.Tbl_Amount = ScMaster.Tbl_Amount;
            sc.Action_Type = actionTag;
            sc.R_Invoice = ScMaster.R_Invoice;
            sc.No_Print = ScMaster.No_Print;
            sc.In_Words = ScMaster.In_Words;
            sc.Remarks = ScMaster.Remarks;
            sc.Audit_Lock = ScMaster.Audit_Lock;
            var result = sc.Action_Type.ToUpper() switch
            {
                "SAVE" => await salesChallanRepo?.PushNewAsync(sc),
                "NEW" => await salesChallanRepo?.PushNewAsync(sc),
                "UPDATE" => await salesChallanRepo?.PutNewAsync(sc),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await salesChallanRepo?.PushNewAsync(sc)
            };

            return 1;
        }
        catch (Exception ex)
        {
            //CreateDatabaseTable.CreateTrigger();
            return 1;
        }
    }
    public async Task<int> UpdateSyncSalesReturnColumnInServerAsync(string actionTag)
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

            getUrl = @$"{configParams.Model.Item2}SalesReturn/GetSalesReturnById";
            insertUrl = @$"{configParams.Model.Item2}SalesReturn/InsertSalesReturn";
            updateUrl = @$"{configParams.Model.Item2}SalesReturn/UpdateSyncSalesReturnColumn";
            deleteUrl = @$"{configParams.Model.Item2}SalesReturn/DeleteSalesReturnAsync?id=" + SrMaster.SR_Invoice;

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

            var salesReturnRepo =
                DataSyncProviderFactory.GetRepository<SR_Master>(DataSyncManager.GetGlobalInjectData());
            var sr = new SR_Master();
            sr.Action_Type = "UPDATE";
            sr.SR_Invoice = SrMaster.SR_Invoice;
            sr.Invoice_Date = SrMaster.Invoice_Date;
            sr.Invoice_Miti = SrMaster.Invoice_Miti;
            sr.Invoice_Time = SrMaster.Invoice_Time;
            sr.SB_Invoice = SrMaster.SB_Invoice;
            sr.SB_Date = SrMaster.SB_Date;
            sr.SB_Miti = SrMaster.SB_Miti;
            sr.Customer_ID = SrMaster.Customer_ID;
            sr.Invoice_Type = SrMaster.Invoice_Type;
            sr.Invoice_Mode = SrMaster.Invoice_Mode;
            sr.Payment_Mode = SrMaster.Payment_Mode;
            sr.DueDays = SrMaster.DueDays;
            sr.CounterId = SrMaster.CounterId;
            sr.Cur_Id = SrMaster.Cur_Id;
            sr.Cur_Rate = SrMaster.Cur_Rate;
            sr.B_Amount = SrMaster.B_Amount;
            sr.T_Amount = SrMaster.T_Amount;
            sr.IsSynced = true;
            var result = sr.Action_Type.ToUpper() switch
            {
                "SAVE" => await salesReturnRepo?.PushNewAsync(sr),
                "NEW" => await salesReturnRepo?.PushNewAsync(sr),
                "UPDATE" => await salesReturnRepo?.PutNewAsync(sr),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await salesReturnRepo?.PushNewAsync(sr)
            };

            return 1;
        }
        catch (Exception ex)
        {
            //CreateDatabaseTable.CreateTrigger();
            return 1;
        }
    }
    public int SavePosReturnInvoiceWithQuery(string queryString)
    {
        try
        {
            var cmdString = new StringBuilder();
            cmdString.Append(queryString);
            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }
            _ = SalesReturnTermPosting();
            _ = SalesReturnAccountPosting();
            _ = SalesReturnStockPosting();

            var isIrd = GetConnection.GetQueryData(" SELECT IsIRDRegister FROM AMS.IRDAPISetting;  ");
            int.TryParse(isIrd, out var isIrdRegister);
            if (isIrdRegister <= 0)
            {
                return iResult;
            }
            PostingSalesReturnToApi(SrMaster.SR_Invoice);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }
    public int SavePosReturnInvoice(string actionTag)
    {
        //try
        //{
        //    var invoiceNo = SrMaster.SR_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        {
        //            AuditLogSalesReturn(actionTag);
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($@"
        //            DELETE FROM AMS.SR_Term where SR_VNo ='{SrMaster.SR_Invoice}';");
        //            cmdString.Append($@"
        //            DELETE FROM AMS.SR_Details where SR_Invoice ='{SrMaster.SR_Invoice}';");
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($@"
        //            UPDATE AMS.SR_Master SET Action_Type='CANCEL',R_Invoice =1 WHERE SR_Invoice ='{SrMaster.SR_Invoice}';");
        //        }

        //        cmdString.Append($@"
        //        Delete from AMS.AccountDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}';");
        //        cmdString.Append($@"
        //        Delete from AMS.StockDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}';");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append($@"
        //            Delete from AMS.SR_Master_OtherDetails where SR_Invoice = '{SrMaster.SR_Invoice}';");
        //            cmdString.Append($@"
        //            Delete from AMS.SR_Master where SR_Invoice ='{SrMaster.SR_Invoice}';");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        cmdString.Append(@" 
        //        INSERT INTO AMS.SR_Master(SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, Customer_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, Cancel_By, Cancel_Date, Cancel_Remarks, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
        //        cmdString.Append($@" 
        //        VALUES (N'{SrMaster.SR_Invoice}', '{SrMaster.Invoice_Date:yyyy-MM-dd}', N'{SrMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SrMaster.SB_Invoice.IsValueExits()
        //            ? $" N'{SrMaster.SB_Invoice}', '{SrMaster.SB_Date:yyyy-MM-dd}','{SrMaster.SB_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SrMaster.Customer_ID},");
        //        cmdString.Append(SrMaster.PartyLedgerId > 0 ? $" {SrMaster.PartyLedgerId}," : "NULL,");
        //        cmdString.Append(SrMaster.Party_Name.IsValueExits() ? $" N'{SrMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SrMaster.Vat_No.IsValueExits() ? $" N'{SrMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SrMaster.Contact_Person.IsValueExits() ? $" N'{SrMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SrMaster.Mobile_No.IsValueExits() ? $" N'{SrMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SrMaster.Address.IsValueExits() ? $" N'{SrMaster.Address}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqMiti}'," : "NULL,");
        //        cmdString.Append($" N'{SrMaster.Invoice_Type}', N'{SrMaster.Invoice_Mode}', '{SrMaster.Payment_Mode}', {SrMaster.DueDays},");
        //        cmdString.Append(SrMaster.DueDays > 0 ? $" '{SrMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SrMaster.Agent_Id > 0 ? $" {SrMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SrMaster.Subledger_Id > 0 ? $" {SrMaster.Subledger_Id}," : "NULL,");
        //        cmdString.Append(SrMaster.Cls1 > 0 ? $" {SrMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SrMaster.CounterId > 0 ? $" {SrMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SrMaster.Cur_Id > 0 ? $" {SrMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
        //        cmdString.Append($"{SrMaster.Cur_Rate.GetDecimal(true)},");
        //        cmdString.Append($"{SrMaster.B_Amount.GetDecimal()},{SrMaster.T_Amount.GetDecimal()}, {SrMaster.N_Amount.GetDecimal()},{SrMaster.LN_Amount.GetDecimal()},{SrMaster.V_Amount.GetDecimal()},{SrMaster.Tbl_Amount.GetDecimal()},{SrMaster.Tender_Amount.GetDecimal()},{SrMaster.Return_Amount.GetDecimal()},'{SrMaster.Action_Type}',");
        //        cmdString.Append(SrMaster.In_Words.IsValueExits() ? $" N'{SrMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SrMaster.Remarks.IsValueExits() ? $" N'{SrMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
        //        cmdString.Append($"0,0,NULL,NULL,NULL,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,0,0,{ObjGlobal.SysBranchId},");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append($"NUll,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SrMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(@" 
        //            INSERT INTO AMS.SR_Details (SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //            cmdString.Append(@" 
        //            VALUES ");
        //            foreach (DataGridViewRow dr in SrMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{SrMaster.SR_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtGodownId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtAltQty"].Value},"
        //                    : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtAltUOMId"].Value},"
        //                    : "NULL,");
        //                var qty = dr.Cells["GTxtQty"].Value.GetDecimal() > 0
        //                    ? dr.Cells["GTxtQty"].Value.GetDecimal()
        //                    : 1;
        //                cmdString.Append($"{qty},");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtUOMId"].Value},"
        //                    : "NUll,");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");

        //                var vatAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                var netAmount = dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
        //                var basicAmount = netAmount - vatAmount + pDiscount + bDiscount;
        //                var rate = basicAmount / qty;

        //                var termAmount = vatAmount - pDiscount;

        //                cmdString.Append($"{basicAmount},");
        //                cmdString.Append($"{termAmount},");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtInvoiceNo"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtInvoiceNo"].Value}','{dr.Cells["GTxtInvoiceSNo"].Value}',"
        //                    : "NULL,0,");

        //                var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

        //                cmdString.Append($"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0,");
        //                cmdString.Append(vRate > 0 ? "1," : "0,");

        //                cmdString.Append(@"
        //                NULL,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");

        //                var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                cmdString.Append($"{pDiscountRate},{pDiscount},0,{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
        //                cmdString.Append(iRows == SrMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }

        //            for (var i = 0; i < SrMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SrMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                if (val <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@" 
        //                INSERT INTO AMS.SR_Term (SR_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //                cmdString.Append($" SELECT '{SrMaster.SR_Invoice}',");
        //                cmdString.Append($" {ObjGlobal.SalesDiscountTermId},");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},'P',");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal()},");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal()},");
        //                cmdString.Append(
        //                    $" {SrMaster.GetView.Rows[i].Cells["GTxtProductId"].Value},'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //            }

        //            for (var i = 0; i < SrMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SrMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                if (val is <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@" 
        //                INSERT INTO AMS.SR_Term (SR_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //                cmdString.Append($@" 
        //                SELECT '{SrMaster.SR_Invoice}',{ObjGlobal.SalesVatTermId},");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},'P',");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtTaxPriceRate"].Value.GetDecimal()},");
        //                cmdString.Append(
        //                    $" {SrMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal()},");
        //                cmdString.Append($" {SrMaster.GetView.Rows[i].Cells["GTxtProductId"].Value}, ");
        //                cmdString.Append(SrMaster.GetView.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool() is true
        //                    ? "'Y',"
        //                    : "'N',");
        //                cmdString.Append("NULL,NULL,NULL,NULL,1,NULL \n");
        //            }
        //        }

        //        if (SrMaster.T_Amount > 0)
        //        {
        //            cmdString.Append(@" 
        //            INSERT INTO AMS.SR_Term (SR_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //            cmdString.Append($" VALUES ( N'{SrMaster.SR_Invoice}',{ObjGlobal.SalesSpecialDiscountTermId},");
        //            cmdString.Append($" 1, N'B',{SrMaster.TermRate.GetDecimal()},{SrMaster.T_Amount.GetDecimal()},");
        //            cmdString.Append(" NULL,'N', NULL,NULL,NULL,NULL,1,NULL); \n");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    if (ObjGlobal.IsOnlineSync)
        //    {
        //        Task.Run(() => SyncSalesReturnAsync(true, actionTag));
        //    }

        //    AuditLogSalesReturn(actionTag);
        //    if (actionTag.Equals("REVERSE"))
        //    {
        //        return iResult;
        //    }

        //    _ = SalesReturnTermPosting();
        //    _ = SalesReturnAccountPosting();
        //    _ = SalesReturnStockPosting();
        //    if (SrMaster.SB_Invoice.IsValueExits())
        //    {
        //        UpdateInvoiceTagOnSalesReturn();
        //    }

        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingSalesReturnToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}


        return 0;
    }
    public int SavePosInvoice(string actionTag)
    {
        //try
        //{
        //    var invoiceNo = SbMaster.SB_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        //SalesAuditPosting(actionTag, SbMaster.SB_Invoice);
        //        {
        //            AuditLogSalesInvoice(actionTag);
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append(
        //                $"UPDATE AMS.SB_Master SET Action_Type='CANCEL' ,R_Invoice=1 ,Cancel_Date = '{SbMaster.Cancel_Date.GetSystemDate()}',Cancel_Remarks='{SbMaster.Cancel_Remarks}'  WHERE SB_Invoice='{SbMaster.SB_Invoice}'; \n");
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($"Delete from AMS.SB_Term where SB_VNo ='{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Details where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }

        //        cmdString.Append(
        //            $"Delete from AMS.AccountDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");
        //        cmdString.Append(
        //            $"Delete from AMS.StockDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append(
        //                $"DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append(
        //                $"Delete from AMS.SB_Master_OtherDetails where SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Master where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        if (SbMaster.PB_Vno.IsValueExits())
        //        {
        //            cmdString.Append(
        //                $" UPDATE AMS.temp_SB_Master SET Action_Type='POST' WHERE SB_Invoice='{SbMaster.PB_Vno}' \n");
        //        }

        //        cmdString.Append(@" 
        //        INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //        cmdString.Append(
        //            $"\n VALUES (N'{SbMaster.SB_Invoice}', '{SbMaster.Invoice_Date:yyyy-MM-dd}', N'{SbMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" N'{SbMaster.PB_Vno}', '{SbMaster.Vno_Date:yyyy-MM-dd}','{SbMaster.Vno_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" N'{SbMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $" N'{SbMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" N'{SbMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" N'{SbMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits() ? $" N'{SbMaster.Address}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(
        //            $"N'{SbMaster.Invoice_Type}', N'{SbMaster.Invoice_Mode}', '{SbMaster.Payment_Mode}', {SbMaster.DueDays},");

        //        cmdString.Append(SbMaster.DueDays > 0 ? $" '{SbMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" {SbMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0 ? $" {SbMaster.Subledger_Id}," : "NULL,");

        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Date:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Date:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" {SbMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $" {SbMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" {SbMaster.Cur_Id}," : "1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $" {SbMaster.Cur_Rate}," : "1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $" {SbMaster.B_Amount}," : "0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $" {SbMaster.T_Amount}," : "0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $" {SbMaster.N_Amount}," : "0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" {SbMaster.LN_Amount}," : "0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $" {SbMaster.V_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $" {SbMaster.Tbl_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0 ? $" {SbMaster.Tender_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0 ? $" {SbMaster.Return_Amount}," : "0,");
        //        cmdString.Append($" '{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits() ? $" N'{SbMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" N'{SbMaster.Remarks.Trim().Replace("'", "''")}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.R_Invoice ? $"CAST('{SbMaster.R_Invoice}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.No_Print > 0 ? $" {SbMaster.No_Print}," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_By}'," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits()
        //            ? $" '{SbMaster.Printed_Date: yyyy-MM-dd}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.Audit_Lock is true ? $"CAST('{SbMaster.Audit_Lock}' AS BIT) ," : "0,");
        //        cmdString.Append($" '{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.MShipId > 0 ? $"{SbMaster.MShipId}," : "NULL,");
        //        cmdString.Append(SbMaster.TableId > 0 ? $"{SbMaster.TableId}," : "NULL,");
        //        cmdString.Append("NULL,NUll,NULL,NULL,1,NULL); \n");
        //    }
        //    else if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append("UPDATE AMS.SB_Master SET ");
        //        cmdString.Append(
        //            $"Invoice_Date = '{SbMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{SbMaster.Invoice_Miti}',");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" PB_Vno = N'{SbMaster.PB_Vno}',Vno_Date = '{SbMaster.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{SbMaster.Vno_Miti}',"
        //            : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
        //        cmdString.Append($" Customer_Id = {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits()
        //            ? $" Party_Name = N'{SbMaster.Party_Name}',"
        //            : "Party_Name = NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits()
        //            ? $"Vat_No =  N'{SbMaster.Vat_No}',"
        //            : "Vat_No = NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits()
        //            ? $" Contact_Person = N'{SbMaster.Contact_Person}',"
        //            : "Contact_Person = NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits()
        //            ? $" Mobile_No = N'{SbMaster.Mobile_No}',"
        //            : "Mobile_No = NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits()
        //            ? $" Address = N'{SbMaster.Address}',"
        //            : "Address = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SbMaster.ChqNo}'," : "ChqNo = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits()
        //            ? $" ChqDate = N'{SbMaster.ChqDate:yyyy-MM-dd}',"
        //            : "ChqDate = NULL,");
        //        cmdString.Append(
        //            $"Invoice_Type = N'{SbMaster.Invoice_Type}', Invoice_Mode = N'{SbMaster.Invoice_Mode}',Payment_Mode =  '{SbMaster.Payment_Mode}',DueDays =  {SbMaster.DueDays},");
        //        cmdString.Append(SbMaster.DueDays > 0
        //            ? $" DueDate = '{SbMaster.DueDate:yyyy-MM-dd}',"
        //            : "DueDate = NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" Agent_Id = {SbMaster.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0
        //            ? $" Subledger_Id = {SbMaster.Subledger_Id},"
        //            : "Subledger_Id = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Invoice = N'{SbMaster.SO_Invoice}',"
        //            : "SO_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Date = N'{SbMaster.SO_Date:yyyy-MM-dd}',"
        //            : "SO_Date = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Invoice = N'{SbMaster.SC_Invoice}',"
        //            : "SC_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Date = N'{SbMaster.SC_Date:yyyy-MM-dd}',"
        //            : "SC_Date = NULL,");
        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" Cls1 = {SbMaster.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $"CounterId = {SbMaster.CounterId}," : " CounterId = NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" Cur_Id = {SbMaster.Cur_Id}," : "Cur_Id = 1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $"Cur_Rate =  {SbMaster.Cur_Rate}," : "Cur_Rate = 1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $"B_Amount =  {SbMaster.B_Amount}," : "B_Amount = 0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $"T_Amount = {SbMaster.T_Amount}," : "T_Amount = 0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $"N_Amount = {SbMaster.N_Amount}," : "N_Amount = 0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" LN_Amount = {SbMaster.LN_Amount}," : "LN_Amount = 0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $"V_Amount =  {SbMaster.V_Amount}," : "V_Amount = 0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $"Tbl_Amount =  {SbMaster.Tbl_Amount}," : "Tbl_Amount = 0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0
        //            ? $" Tender_Amount = {SbMaster.Tender_Amount},"
        //            : " Tender_Amount = 0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0
        //            ? $"Return_Amount =  {SbMaster.Return_Amount},"
        //            : "Return_Amount = 0,");
        //        cmdString.Append($"Action_Type = '{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits()
        //            ? $"In_Words =  N'{SbMaster.In_Words}',"
        //            : "In_Words = NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" Remarks = N'{SbMaster.Remarks.Trim().Replace("'", "''")}'"
        //            : "Remarks = NULL");
        //        cmdString.Append($" WHERE SB_Invoice = N'{SbMaster.SB_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SbMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(@" 
        //            INSERT INTO AMS.SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //            cmdString.Append("\n VALUES \n");
        //            foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtGodownId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtAltQty"].Value},"
        //                    : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtAltUOMId"].Value},"
        //                    : "NULL,");
        //                var qty = dr.Cells["GTxtQty"].Value.GetDecimal() > 0
        //                    ? dr.Cells["GTxtQty"].Value.GetDecimal()
        //                    : 1;
        //                cmdString.Append($"{qty},");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtUOMId"].Value},"
        //                    : "NUll,");

        //                cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");

        //                var vatAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                var netAmount = dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
        //                var basicAmount = netAmount - vatAmount + pDiscount;
        //                var rate = basicAmount / qty;

        //                var termAmount = vatAmount - pDiscount;
        //                //cmdString.Append($"{rate},");
        //                cmdString.Append($"{basicAmount},");
        //                cmdString.Append($"{termAmount},");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append("NULL,0,NULL,0,");

        //                var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

        //                cmdString.Append(
        //                    $"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',");

        //                var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                cmdString.Append(
        //                    $"{pDiscountRate},{pDiscount},{SbMaster.TermRate},{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
        //                cmdString.Append(iRows == SbMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }

        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                if (val <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@"
        //                INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //                cmdString.Append($"\n SELECT '{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{ObjGlobal.SalesDiscountTermId},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal()},");
        //                cmdString.Append($"'P',{SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value},'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //            }

        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                if (val is <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@" 
        //                INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //                cmdString.Append($" \n SELECT '{SbMaster.SB_Invoice}',{ObjGlobal.SalesVatTermId},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtTaxPriceRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"'P',{SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value}, ");
        //                cmdString.Append(SbMaster.GetView.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool() is true
        //                    ? "'Y',"
        //                    : "'N',");
        //                cmdString.Append("NULL,NULL,NULL,NULL,1,NULL \n");
        //            }
        //        }

        //        if (SbMaster.T_Amount > 0)
        //        {
        //            cmdString.Append(@"
        //            INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //            cmdString.Append($" \n SELECT '{SbMaster.SB_Invoice}',{ObjGlobal.SalesSpecialDiscountTermId},1,");
        //            cmdString.Append(SbMaster.TermRate > 0 ? $"{SbMaster.TermRate}," : "0,");
        //            cmdString.Append(SbMaster.T_Amount > 0 ? $"{SbMaster.T_Amount}," : "0,");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    if (ObjGlobal.IsOnlineSync)
        //    {
        //        Task.Run(() => SyncSalesAsync(true, actionTag));
        //    }

        //    AuditLogSalesInvoice(actionTag);
        //    if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
        //    {
        //        return iResult;
        //    }
        //    _ = SalesTermPostingAsync();
        //    _ = SalesInvoiceAccountPosting();
        //    _ = SalesInvoiceStockPosting();

        //    if (SbMaster.SC_Invoice.IsValueExits() || SbMaster.SO_Invoice.IsValueExits())
        //    {
        //        UpdateOrderChallanOnInvoice();
        //    }

        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingBillToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }
    public int SaveUnSyncSalesFromServerAsync(SB_Master salesModel, string actionTag)
    {
        //try
        //{
        //    var query = $"SELECT SB_Invoice FROM AMS.SB_Master WHERE SB_Invoice='{salesModel.SB_Invoice}'";
        //    var isAlreadExist = GetConnection.GetQueryData(query);
        //    actionTag = string.IsNullOrEmpty(isAlreadExist) ? "SAVE" : "UPDATE";
        //    SbMaster.SB_Invoice = salesModel.SB_Invoice;
        //    SbMaster.Invoice_Date = salesModel.Invoice_Date;
        //    SbMaster.Invoice_Miti = salesModel.Invoice_Miti;
        //    SbMaster.PB_Vno = salesModel.PB_Vno;
        //    SbMaster.Vno_Miti = salesModel.Vno_Miti;
        //    SbMaster.Vno_Date = salesModel.Vno_Date;
        //    SbMaster.Invoice_Time = salesModel.Invoice_Time;
        //    SbMaster.Customer_Id = salesModel.Customer_Id;
        //    SbMaster.PartyLedgerId = salesModel.PartyLedgerId;
        //    SbMaster.Party_Name = salesModel.Party_Name;
        //    SbMaster.Vat_No = salesModel.Vat_No;
        //    SbMaster.Contact_Person = salesModel.Contact_Person;
        //    SbMaster.Mobile_No = salesModel.Mobile_No;
        //    SbMaster.Address = salesModel.Address;
        //    SbMaster.ChqNo = salesModel.ChqNo;
        //    SbMaster.ChqDate = salesModel.ChqDate;
        //    SbMaster.Invoice_Type = salesModel.Invoice_Type;
        //    SbMaster.Invoice_Mode = salesModel.Invoice_Mode;
        //    SbMaster.Payment_Mode = salesModel.Payment_Mode;
        //    SbMaster.DueDays = salesModel.DueDays;
        //    SbMaster.DueDate = salesModel.DueDate;
        //    SbMaster.Agent_Id = salesModel.Agent_Id;
        //    SbMaster.Subledger_Id = salesModel.Subledger_Id;
        //    SbMaster.SO_Invoice = salesModel.SO_Invoice;
        //    SbMaster.SO_Date = salesModel.SO_Date;
        //    SbMaster.SC_Invoice = salesModel.SC_Invoice;
        //    SbMaster.SC_Date = salesModel.SC_Date;
        //    SbMaster.Cls1 = salesModel.Cls1;
        //    SbMaster.Cls2 = 0;
        //    SbMaster.Cls3 = 0;
        //    SbMaster.Cls4 = 0;
        //    SbMaster.Cur_Id = salesModel.Cur_Id;
        //    SbMaster.Cur_Rate = salesModel.Cur_Rate;
        //    SbMaster.CounterId = salesModel.CounterId;
        //    SbMaster.B_Amount = salesModel.B_Amount;
        //    SbMaster.T_Amount = salesModel.T_Amount;
        //    SbMaster.N_Amount = salesModel.N_Amount;
        //    SbMaster.LN_Amount = salesModel.LN_Amount;
        //    SbMaster.Tender_Amount = salesModel.Tender_Amount;
        //    SbMaster.Return_Amount = salesModel.Return_Amount;
        //    SbMaster.V_Amount = salesModel.V_Amount;
        //    SbMaster.Tbl_Amount = salesModel.Tbl_Amount;
        //    SbMaster.Action_Type = actionTag;
        //    SbMaster.R_Invoice = salesModel.R_Invoice;
        //    SbMaster.No_Print = salesModel.No_Print;
        //    SbMaster.In_Words = salesModel.In_Words;
        //    SbMaster.Remarks = salesModel.Remarks;
        //    SbMaster.Audit_Lock = salesModel.Audit_Lock;
        //    //SbMaster.GetView = DGrid;
        //    //SbMaster.dtBTerm = _dtdtBTerm;
        //    //SbMaster.dtPTerm = _dtdtPTerm;
        //    //SbMaster.ProductBatch = _dtBatchInfo;

        //    //GetImage.VoucherNo = salesModel.SB_Invoice;
        //    //GetImage.PAttachment1 = salesModel.PAttachment1;
        //    //GetImage.PAttachment2 = salesModel.PAttachment2;
        //    //GetImage.PAttachment3 = salesModel.PAttachment3;
        //    //GetImage.PAttachment4 = salesModel.PAttachment4;
        //    //GetImage.PAttachment5 = salesModel.PAttachment5;

        //    foreach (var so in salesModel.OtherDetailsList)
        //    {
        //        SbOther.SB_Invoice = salesModel.SB_Invoice;
        //        SbOther.Transport = so.Transport;
        //        SbOther.VechileNo = so.VechileNo;
        //        SbOther.BiltyNo = so.BiltyNo;
        //        SbOther.Package = so.Package;
        //        SbOther.BiltyDate = so.BiltyDate;
        //        SbOther.BiltyType = so.BiltyType;
        //        SbOther.Driver = so.Driver;
        //        SbOther.PhoneNo = so.PhoneNo;
        //        SbOther.LicenseNo = so.LicenseNo;
        //        SbOther.MailingAddress = so.MailingAddress;
        //        SbOther.MCity = so.MCity;
        //        SbOther.MState = so.MState;
        //        SbOther.MCountry = so.MCountry;
        //        SbOther.MEmail = so.MEmail;
        //        SbOther.ShippingAddress = so.ShippingAddress;
        //        SbOther.SCity = so.SCity;
        //        SbOther.SState = so.SState;
        //        SbOther.SCountry = so.SCountry;
        //        SbOther.SEmail = so.SEmail;
        //        SbOther.ContractNo = so.ContractNo;
        //        SbOther.ContractDate = so.ContractDate;
        //        SbOther.ExportInvoice = so.ExportInvoice;
        //        SbOther.ExportInvoiceDate = so.ExportInvoiceDate;
        //        SbOther.BankDetails = so.BankDetails;
        //        SbOther.LcNumber = so.LcNumber;
        //        SbOther.CustomDetails = so.CustomDetails;
        //    }

        //    var invoiceNo = salesModel.SB_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        //SalesAuditPosting(actionTag, SbMaster.SB_Invoice);
        //        {
        //            AuditLogSalesInvoice(actionTag);
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($@"
        //            UPDATE AMS.SB_Master SET Action_Type='CANCEL' ,R_Invoice=1 ,Cancel_Date = '{salesModel.Cancel_Date.GetSystemDate()}',Cancel_Remarks='{salesModel.Cancel_Remarks}'  WHERE SB_Invoice='{salesModel.SB_Invoice}'; \n");
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($"Delete from AMS.SB_Term where SB_VNo ='{salesModel.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Details where SB_Invoice ='{salesModel.SB_Invoice}'; \n");
        //        }

        //        cmdString.Append($@"
        //        DELETE FROM AMS.AccountDetails Where module='SB' and Voucher_No ='{salesModel.SB_Invoice}'; \n");
        //        cmdString.Append($@"
        //        DELETE FROM AMS.StockDetails Where module='SB' and Voucher_No ='{salesModel.SB_Invoice}'; \n");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append($@"
        //            DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{salesModel.SB_Invoice}'; \n");
        //            cmdString.Append($@"
        //            DELETE FROM AMS.SB_Master_OtherDetails where SB_Invoice = '{salesModel.SB_Invoice}'; \n");
        //            cmdString.Append($@"
        //            DELETE FROM AMS.SB_Master where SB_Invoice ='{salesModel.SB_Invoice}'; \n");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        if (salesModel.PB_Vno.IsValueExits())
        //        {
        //            cmdString.Append($@" 
        //            UPDATE AMS.temp_SB_Master SET Action_Type='POST' WHERE SB_Invoice='{salesModel.PB_Vno}' \n");
        //        }

        //        cmdString.Append(@" 
        //        INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //        cmdString.Append($@" 
        //        VALUES (N'{salesModel.SB_Invoice}', '{salesModel.Invoice_Date:yyyy-MM-dd}', N'{salesModel.Invoice_Miti}', '{salesModel.Invoice_Time}',");
        //        cmdString.Append(salesModel.PB_Vno.IsValueExits()
        //            ? $" N'{salesModel.PB_Vno}', '{salesModel.Vno_Date:yyyy-MM-dd}','{salesModel.Vno_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {salesModel.Customer_Id},");
        //        cmdString.Append(salesModel.PartyLedgerId.GetLong() > 0 ? $" {salesModel.PartyLedgerId}," : "NULL,");
        //        cmdString.Append(salesModel.Party_Name.IsValueExits() ? $" N'{salesModel.Party_Name}'," : "NULL,");
        //        cmdString.Append(salesModel.Vat_No.IsValueExits() ? $" N'{salesModel.Vat_No}'," : "NULL,");
        //        cmdString.Append(salesModel.Contact_Person.IsValueExits()
        //            ? $" N'{salesModel.Contact_Person}',"
        //            : "NULL,");
        //        cmdString.Append(salesModel.Mobile_No.IsValueExits() ? $" N'{salesModel.Mobile_No}'," : "NULL,");
        //        cmdString.Append(salesModel.Address.IsValueExits() ? $" N'{salesModel.Address}'," : "NULL,");
        //        cmdString.Append(salesModel.ChqNo.IsValueExits() ? $" N'{salesModel.ChqNo}'," : "NULL,");
        //        cmdString.Append(salesModel.ChqNo.IsValueExits() ? $" N'{salesModel.ChqDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(salesModel.ChqNo.IsValueExits() ? $" N'{salesModel.ChqMiti}'," : "NULL,");
        //        cmdString.Append(
        //            $"N'{salesModel.Invoice_Type}', N'{salesModel.Invoice_Mode}', '{salesModel.Payment_Mode}', {salesModel.DueDays},");
        //        cmdString.Append(salesModel.DueDays > 0 ? $" '{salesModel.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(salesModel.Agent_Id > 0 ? $" {salesModel.Agent_Id}," : "NULL,");
        //        cmdString.Append(salesModel.Subledger_Id > 0 ? $" {salesModel.Subledger_Id}," : "NULL,");

        //        cmdString.Append(salesModel.SO_Invoice.IsValueExits() ? $" N'{salesModel.SO_Invoice}'," : "NULL,");
        //        cmdString.Append(salesModel.SO_Invoice.IsValueExits()
        //            ? $" N'{salesModel.SO_Date:yyyy-MM-dd}',"
        //            : "NULL,");
        //        cmdString.Append(salesModel.SC_Invoice.IsValueExits() ? $" N'{salesModel.SC_Invoice}'," : "NULL,");
        //        cmdString.Append(salesModel.SC_Invoice.IsValueExits()
        //            ? $" N'{salesModel.SC_Date:yyyy-MM-dd}',"
        //            : "NULL,");

        //        cmdString.Append(salesModel.Cls1 > 0 ? $" {salesModel.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(salesModel.CounterId > 0 ? $" {salesModel.CounterId}," : "NULL,");
        //        cmdString.Append(salesModel.Cur_Id > 0 ? $" {salesModel.Cur_Id}," : "1,");
        //        cmdString.Append(salesModel.Cur_Rate > 0 ? $" {salesModel.Cur_Rate}," : "1,");
        //        cmdString.Append(salesModel.B_Amount > 0 ? $" {salesModel.B_Amount}," : "0,");
        //        cmdString.Append(salesModel.T_Amount > 0 ? $" {salesModel.T_Amount}," : "0,");
        //        cmdString.Append(salesModel.N_Amount > 0 ? $" {salesModel.N_Amount}," : "0,");
        //        cmdString.Append(salesModel.LN_Amount > 0 ? $" {salesModel.LN_Amount}," : "0,");
        //        cmdString.Append(salesModel.V_Amount > 0 ? $" {salesModel.V_Amount}," : "0,");
        //        cmdString.Append(salesModel.Tbl_Amount > 0 ? $" {salesModel.Tbl_Amount}," : "0,");
        //        cmdString.Append(salesModel.Tender_Amount > 0 ? $" {salesModel.Tender_Amount}," : "0,");
        //        cmdString.Append(salesModel.Return_Amount > 0 ? $" {salesModel.Return_Amount}," : "0,");
        //        cmdString.Append($" '{actionTag}',");
        //        cmdString.Append(salesModel.In_Words.IsValueExits() ? $" N'{salesModel.In_Words}'," : "NULL,");
        //        cmdString.Append(salesModel.Remarks.IsValueExits()
        //            ? $" N'{salesModel.Remarks.Trim().Replace("'", "''")}',"
        //            : "NULL,");
        //        cmdString.Append(salesModel.R_Invoice ? $"CAST('{salesModel.R_Invoice}' AS BIT) ," : "0,");
        //        cmdString.Append(salesModel.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
        //        cmdString.Append(salesModel.No_Print > 0 ? $" {salesModel.No_Print}," : "0,");
        //        cmdString.Append(salesModel.Printed_By.IsValueExits() ? $" '{salesModel.Printed_By}'," : "0,");
        //        cmdString.Append(salesModel.Printed_By.IsValueExits()
        //            ? $" '{salesModel.Printed_Date: yyyy-MM-dd}',"
        //            : "NULL,");
        //        cmdString.Append(salesModel.Audit_Lock is true ? $"CAST('{salesModel.Audit_Lock}' AS BIT) ," : "0,");
        //        cmdString.Append($" '{salesModel.Enter_By}','{salesModel.Enter_Date}',");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(salesModel.CUnit_Id > 0 ? $" {salesModel.CUnit_Id}," : "NULL,");
        //        cmdString.Append(salesModel.CBranch_Id > 0 ? $" {salesModel.CBranch_Id}," : "NULL,");
        //        cmdString.Append("NULL,NULL,");
        //        cmdString.Append(salesModel.FiscalYearId > 0 ? $" {salesModel.FiscalYearId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(salesModel.MShipId > 0 ? $"{salesModel.MShipId}," : "NULL,");
        //        cmdString.Append(salesModel.TableId > 0 ? $"{salesModel.TableId}," : "NULL,");
        //        cmdString.Append("NULL,NUll,NULL,NULL,1,NULL); \n");
        //    }
        //    else if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append("UPDATE AMS.SB_Master SET ");
        //        cmdString.Append($"Invoice_Date = '{salesModel.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{salesModel.Invoice_Miti}',");
        //        cmdString.Append(salesModel.PB_Vno.IsValueExits()
        //            ? $" PB_Vno = N'{salesModel.PB_Vno}',Vno_Date = '{salesModel.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{salesModel.Vno_Miti}',"
        //            : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
        //        cmdString.Append($" Customer_Id = {salesModel.Customer_Id},");
        //        cmdString.Append(salesModel.PartyLedgerId.GetLong() > 0
        //            ? $" PartyLedgerId = {salesModel.PartyLedgerId},"
        //            : "PartyLedgerId = NULL,");
        //        cmdString.Append(salesModel.Party_Name.IsValueExits()
        //            ? $" Party_Name = N'{salesModel.Party_Name}',"
        //            : "Party_Name = NULL,");
        //        cmdString.Append(salesModel.Vat_No.IsValueExits()
        //            ? $"Vat_No =  N'{salesModel.Vat_No}',"
        //            : "Vat_No = NULL,");
        //        cmdString.Append(salesModel.Contact_Person.IsValueExits()
        //            ? $" Contact_Person = N'{salesModel.Contact_Person}',"
        //            : "Contact_Person = NULL,");
        //        cmdString.Append(salesModel.Mobile_No.IsValueExits()
        //            ? $" Mobile_No = N'{salesModel.Mobile_No}',"
        //            : "Mobile_No = NULL,");
        //        cmdString.Append(salesModel.Address.IsValueExits()
        //            ? $" Address = N'{salesModel.Address}',"
        //            : "Address = NULL,");
        //        cmdString.Append(salesModel.ChqNo.IsValueExits()
        //            ? $" ChqNo = N'{salesModel.ChqNo}',"
        //            : "ChqNo = NULL,");
        //        cmdString.Append(salesModel.ChqNo.IsValueExits()
        //            ? $" ChqDate = N'{salesModel.ChqDate:yyyy-MM-dd}',"
        //            : "ChqDate = NULL,");
        //        cmdString.Append(
        //            $"Invoice_Type = N'{salesModel.Invoice_Type}', Invoice_Mode = N'{salesModel.Invoice_Mode}',Payment_Mode =  '{salesModel.Payment_Mode}',DueDays =  {salesModel.DueDays},");
        //        cmdString.Append(salesModel.DueDays > 0
        //            ? $" DueDate = '{salesModel.DueDate:yyyy-MM-dd}',"
        //            : "DueDate = NULL,");
        //        cmdString.Append(salesModel.Agent_Id > 0 ? $" Agent_Id = {salesModel.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(salesModel.Subledger_Id > 0
        //            ? $" Subledger_Id = {salesModel.Subledger_Id},"
        //            : "Subledger_Id = NULL,");
        //        cmdString.Append(salesModel.SO_Invoice.IsValueExits()
        //            ? $" SO_Invoice = N'{salesModel.SO_Invoice}',"
        //            : "SO_Invoice = NULL,");
        //        cmdString.Append(salesModel.SO_Invoice.IsValueExits()
        //            ? $" SO_Date = N'{salesModel.SO_Date:yyyy-MM-dd}',"
        //            : "SO_Date = NULL,");
        //        cmdString.Append(salesModel.SC_Invoice.IsValueExits()
        //            ? $" SC_Invoice = N'{salesModel.SC_Invoice}',"
        //            : "SC_Invoice = NULL,");
        //        cmdString.Append(salesModel.SC_Invoice.IsValueExits()
        //            ? $" SC_Date = N'{salesModel.SC_Date:yyyy-MM-dd}',"
        //            : "SC_Date = NULL,");
        //        cmdString.Append(salesModel.Cls1 > 0 ? $" Cls1 = {salesModel.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(salesModel.CounterId > 0
        //            ? $"CounterId = {salesModel.CounterId},"
        //            : " CounterId = NULL,");
        //        cmdString.Append(salesModel.Cur_Id > 0 ? $" Cur_Id = {salesModel.Cur_Id}," : "Cur_Id = 1,");
        //        cmdString.Append(salesModel.Cur_Rate > 0 ? $"Cur_Rate =  {salesModel.Cur_Rate}," : "Cur_Rate = 1,");
        //        cmdString.Append(salesModel.B_Amount > 0 ? $"B_Amount =  {salesModel.B_Amount}," : "B_Amount = 0,");
        //        cmdString.Append(salesModel.T_Amount > 0 ? $"T_Amount = {salesModel.T_Amount}," : "T_Amount = 0,");
        //        cmdString.Append(salesModel.N_Amount > 0 ? $"N_Amount = {salesModel.N_Amount}," : "N_Amount = 0,");
        //        cmdString.Append(salesModel.LN_Amount > 0 ? $" LN_Amount = {salesModel.LN_Amount}," : "LN_Amount = 0,");
        //        cmdString.Append(salesModel.V_Amount > 0 ? $"V_Amount =  {salesModel.V_Amount}," : "V_Amount = 0,");
        //        cmdString.Append(salesModel.Tbl_Amount > 0
        //            ? $"Tbl_Amount =  {salesModel.Tbl_Amount},"
        //            : "Tbl_Amount = 0,");
        //        cmdString.Append(salesModel.Tender_Amount > 0
        //            ? $" Tender_Amount = {salesModel.Tender_Amount},"
        //            : " Tender_Amount = 0,");
        //        cmdString.Append(salesModel.Return_Amount > 0
        //            ? $"Return_Amount =  {salesModel.Return_Amount},"
        //            : "Return_Amount = 0,");
        //        cmdString.Append($"Action_Type = '{actionTag}',");
        //        cmdString.Append(salesModel.In_Words.IsValueExits()
        //            ? $"In_Words =  N'{salesModel.In_Words}',"
        //            : "In_Words = NULL,");
        //        cmdString.Append(salesModel.Remarks.IsValueExits()
        //            ? $" Remarks = N'{salesModel.Remarks.Trim().Replace("'", "''")}'"
        //            : "Remarks = NULL");
        //        cmdString.Append($" WHERE SB_Invoice = N'{salesModel.SB_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (salesModel.DetailsList.Count > 0)
        //        {
        //            var iRows = 0;
        //            cmdString.Append(
        //                " INSERT INTO AMS.SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //            cmdString.Append(" VALUES \n");
        //            foreach (var dr in salesModel.DetailsList)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{dr.SB_Invoice}',");
        //                cmdString.Append($"{dr.Invoice_SNo},");
        //                cmdString.Append($"{dr.P_Id},");
        //                cmdString.Append(dr.Gdn_Id.GetInt() > 0 ? $"{dr.Gdn_Id}," : "NULL,");
        //                cmdString.Append(dr.Alt_Qty.GetDecimal() > 0 ? $"{dr.Alt_Qty}," : "0,");
        //                cmdString.Append(dr.Alt_UnitId.GetInt() > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
        //                var qty = dr.Qty.GetDecimal() > 0 ? dr.Qty.GetDecimal() : 1;
        //                cmdString.Append($"{qty},");
        //                cmdString.Append(dr.Unit_Id.GetInt() > 0 ? $"{dr.Unit_Id}," : "NUll,");
        //                cmdString.Append($"{dr.Rate.GetDecimal()},");
        //                cmdString.Append($"{dr.B_Amount.GetDecimal()},");
        //                cmdString.Append($"{dr.T_Amount.GetDecimal()},");
        //                cmdString.Append($"{dr.N_Amount.GetDecimal()},");
        //                cmdString.Append($"{dr.AltStock_Qty.GetDecimal()},");
        //                cmdString.Append($"{dr.Stock_Qty.GetDecimal()},");
        //                cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
        //                cmdString.Append(dr.SO_Invoice.IsValueExits() ? $"'{dr.SO_Invoice}'," : "NULL,");
        //                cmdString.Append(dr.SO_Sno.GetDecimal() > 0 ? $"{dr.SO_Sno}," : "NULL,");
        //                cmdString.Append(dr.SC_Invoice.IsValueExits() ? $"'{dr.SC_Invoice}'," : "NULL,");
        //                cmdString.Append(dr.SC_SNo.GetDecimal() > 0 ? $"{dr.SC_SNo}," : "NULL,");

        //                var taxAmount = dr.Tax_Amount.GetDecimal();
        //                var vAmount = dr.V_Amount.GetDecimal();
        //                var vRate = dr.V_Rate.GetDecimal();

        //                cmdString.Append(
        //                    $"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',");

        //                var pDiscountRate = dr.PDiscountRate.GetDecimal();
        //                var pDiscount = dr.PDiscount.GetDecimal();
        //                cmdString.Append(
        //                    $"{pDiscountRate},{pDiscount},{dr.BDiscountRate},{dr.BDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
        //                cmdString.Append(iRows == salesModel.DetailsList.Count ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }

        //            foreach (var st in salesModel.Terms)
        //            {
        //                cmdString.Append(
        //                    "INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //                cmdString.Append($"Select '{st.SB_VNo}',");
        //                cmdString.Append($"{st.ST_Id},");
        //                cmdString.Append($"{st.SNo},");
        //                cmdString.Append($"{st.Rate},");
        //                cmdString.Append($"{st.Amount},");
        //                cmdString.Append($"'{st.Term_Type}',");
        //                cmdString.Append(st.Product_Id.GetLong() > 0 ? $"{st.Product_Id}," : "NULL,");
        //                cmdString.Append("'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //            }

        //            if (salesModel.ProductAddInfoModels != null && salesModel.ProductAddInfoModels.Count > 0)
        //            {
        //                cmdString.Append(
        //                    $" DELETE AMS.ProductAddInfo WHERE VoucherNo='{salesModel.SB_Invoice}' AND Module='SB' \n");
        //                cmdString.Append(
        //                    "  INSERT INTO AMS.ProductAddInfo(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)  \n");
        //                cmdString.Append("  VALUES \n");
        //                foreach (var dr in salesModel.ProductAddInfoModels)
        //                {
        //                    cmdString.Append($"('SB','{salesModel.SB_Invoice}','O',");
        //                    cmdString.Append(
        //                        $"{dr.ProductId},{dr.Sno},NULL,NULL,'{dr.BatchNo}',NULL,NULL,NULL,NULL,'{dr.MFDate}','{dr.ExpDate}',{dr.Mrp},{dr.Rate},0,{dr.Qty},");
        //                    cmdString.Append(dr.BranchId > 0 ? $" {dr.BranchId}," : "NULL,");
        //                    cmdString.Append(dr.CompanyUnitId > 0 ? $" {dr.CompanyUnitId}," : "NULL,");
        //                    cmdString.Append($"'{dr.EnterBy}','{dr.EnterDate}',");
        //                    cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{salesModel.SyncRowVersion.GetDecimal(true)}");
        //                    cmdString.Append(salesModel.ProductAddInfoModels.IndexOf(dr) ==
        //                                     salesModel.ProductAddInfoModels.Count - 1
        //                        ? "); \n"
        //                        : " ),\n");
        //                }
        //            }
        //        }
        //    }

        //    CreateDatabaseTable.DropTrigger();
        //    var iResult = SaveDataInDatabase(cmdString);
        //    CreateDatabaseTable.CreateTrigger();
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }
        //    Task.Run(() => UpdateSyncSalesColumnInServerAsync());
        //    AuditLogSalesInvoice(actionTag);
        //    if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    if (salesModel.OtherDetailsList.Count > 0)
        //    {
        //        SaveSalesOtherDetails("SB", actionTag);
        //    }

        //    _ = SalesTermPostingAsync();
        //    _ = SalesInvoiceAccountPosting();
        //    _ = SalesInvoiceStockPosting();

        //    if (SbMaster.SC_Invoice.IsValueExits() || SbMaster.SO_Invoice.IsValueExits())
        //    {
        //        UpdateOrderChallanOnInvoice();
        //    }

        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    CreateDatabaseTable.CreateTrigger();
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }
    public int SaveOpdBillingInvoice(string actionTag)
    {
        //try
        //{
        //    var invoiceNo = SbMaster.SB_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        {
        //            AuditLogSalesInvoice(actionTag);
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append(
        //                $"UPDATE AMS.SB_Master SET Action_Type='CANCEL' ,R_Invoice=1 WHERE SB_Invoice='{SbMaster.SB_Invoice}'; \n");
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($"Delete from AMS.SB_Term where SB_VNo ='{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Details where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }

        //        cmdString.Append(
        //            $"Delete from AMS.AccountDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");
        //        cmdString.Append(
        //            $"Delete from AMS.StockDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append(
        //                $"DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append(
        //                $"Delete from AMS.SB_Master_OtherDetails where SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Master where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        if (SbMaster.PB_Vno.IsValueExits())
        //        {
        //            cmdString.Append(
        //                $" UPDATE AMS.temp_SB_Master SET Action_Type='POST' WHERE SB_Invoice='{SbMaster.PB_Vno}' \n");
        //        }

        //        cmdString.Append(
        //            " INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //        cmdString.Append(
        //            $" VALUES (N'{SbMaster.SB_Invoice}', '{SbMaster.Invoice_Date:yyyy-MM-dd}', N'{SbMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" N'{SbMaster.PB_Vno}', '{SbMaster.Vno_Date:yyyy-MM-dd}','{SbMaster.Vno_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" N'{SbMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $" N'{SbMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" N'{SbMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" N'{SbMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits() ? $" N'{SbMaster.Address}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(
        //            $"N'{SbMaster.Invoice_Type}', N'{SbMaster.Invoice_Mode}', '{SbMaster.Payment_Mode}', {SbMaster.DueDays},");

        //        cmdString.Append(SbMaster.DueDays > 0 ? $" '{SbMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" {SbMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0 ? $" {SbMaster.Subledger_Id}," : "NULL,");

        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Date:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Date:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" {SbMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $" {SbMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" {SbMaster.Cur_Id}," : "1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $" {SbMaster.Cur_Rate}," : "1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $" {SbMaster.B_Amount}," : "0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $" {SbMaster.T_Amount}," : "0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $" {SbMaster.N_Amount}," : "0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" {SbMaster.LN_Amount}," : "0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $" {SbMaster.V_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $" {SbMaster.Tbl_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0 ? $" {SbMaster.Tender_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0 ? $" {SbMaster.Return_Amount}," : "0,");
        //        cmdString.Append($"'{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits() ? $" N'{SbMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" N'{SbMaster.Remarks.Trim().Replace("'", "''")}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.R_Invoice ? $"CAST('{SbMaster.R_Invoice}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.No_Print > 0 ? $" {SbMaster.No_Print}," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_By}'," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits()
        //            ? $" '{SbMaster.Printed_Date: yyyy-MM-dd}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.Audit_Lock is true ? $"CAST('{SbMaster.Audit_Lock}' AS BIT) ," : "0,");
        //        cmdString.Append($" '{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.DoctorId > 0 ? $"{SbMaster.DoctorId}," : "NULL,");
        //        cmdString.Append(SbMaster.PatientId > 0 ? $"{SbMaster.PatientId}," : "NULL,");
        //        cmdString.Append(SbMaster.HDepartmentId > 0 ? $"{SbMaster.HDepartmentId}," : "NULL,");
        //        cmdString.Append(SbMaster.MShipId > 0 ? $"{SbMaster.MShipId}," : "NULL,");
        //        cmdString.Append(SbMaster.TableId > 0 ? $"{SbMaster.TableId}," : "NULL,");
        //        cmdString.Append("NULL,NUll,NULL,NULL,1,NULL); \n");
        //    }
        //    else if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append("UPDATE AMS.SB_Master SET ");
        //        cmdString.Append(
        //            $"Invoice_Date = '{SbMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{SbMaster.Invoice_Miti}',");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" PB_Vno = N'{SbMaster.PB_Vno}',Vno_Date = '{SbMaster.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{SbMaster.Vno_Miti}',"
        //            : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
        //        cmdString.Append($" Customer_Id = {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits()
        //            ? $" Party_Name = N'{SbMaster.Party_Name}',"
        //            : "Party_Name = NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits()
        //            ? $"Vat_No =  N'{SbMaster.Vat_No}',"
        //            : "Vat_No = NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits()
        //            ? $" Contact_Person = N'{SbMaster.Contact_Person}',"
        //            : "Contact_Person = NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits()
        //            ? $" Mobile_No = N'{SbMaster.Mobile_No}',"
        //            : "Mobile_No = NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits()
        //            ? $" Address = N'{SbMaster.Address}',"
        //            : "Address = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SbMaster.ChqNo}'," : "ChqNo = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits()
        //            ? $" ChqDate = N'{SbMaster.ChqDate:yyyy-MM-dd}',"
        //            : "ChqDate = NULL,");
        //        cmdString.Append(
        //            $"Invoice_Type = N'{SbMaster.Invoice_Type}', Invoice_Mode = N'{SbMaster.Invoice_Mode}',Payment_Mode =  '{SbMaster.Payment_Mode}',DueDays =  {SbMaster.DueDays},");
        //        cmdString.Append(SbMaster.DueDays > 0
        //            ? $" DueDate = '{SbMaster.DueDate:yyyy-MM-dd}',"
        //            : "DueDate = NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" Agent_Id = {SbMaster.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0
        //            ? $" Subledger_Id = {SbMaster.Subledger_Id},"
        //            : "Subledger_Id = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Invoice = N'{SbMaster.SO_Invoice}',"
        //            : "SO_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Date = N'{SbMaster.SO_Date:yyyy-MM-dd}',"
        //            : "SO_Date = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Invoice = N'{SbMaster.SC_Invoice}',"
        //            : "SC_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Date = N'{SbMaster.SC_Date:yyyy-MM-dd}',"
        //            : "SC_Date = NULL,");
        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" Cls1 = {SbMaster.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $"CounterId = {SbMaster.CounterId}," : " CounterId = NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" Cur_Id = {SbMaster.Cur_Id}," : "Cur_Id = 1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $"Cur_Rate =  {SbMaster.Cur_Rate}," : "Cur_Rate = 1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $"B_Amount =  {SbMaster.B_Amount}," : "B_Amount = 0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $"T_Amount = {SbMaster.T_Amount}," : "T_Amount = 0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $"N_Amount = {SbMaster.N_Amount}," : "N_Amount = 0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" LN_Amount = {SbMaster.LN_Amount}," : "LN_Amount = 0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $"V_Amount =  {SbMaster.V_Amount}," : "V_Amount = 0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $"Tbl_Amount =  {SbMaster.Tbl_Amount}," : "Tbl_Amount = 0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0
        //            ? $" Tender_Amount = {SbMaster.Tender_Amount},"
        //            : " Tender_Amount = 0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0
        //            ? $"Return_Amount =  {SbMaster.Return_Amount},"
        //            : "Return_Amount = 0,");
        //        cmdString.Append($"Action_Type = '{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.DoctorId > 0 ? $"DoctorId = {SbMaster.DoctorId}," : "DoctorId = NULL,");
        //        cmdString.Append(SbMaster.PatientId > 0 ? $" PatientId= {SbMaster.PatientId}," : "PatientId = NULL,");
        //        cmdString.Append(SbMaster.HDepartmentId > 0
        //            ? $" HDepartmentId={SbMaster.HDepartmentId},"
        //            : "HDepartmentId = NULL,");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits()
        //            ? $"In_Words =  N'{SbMaster.In_Words}',"
        //            : "In_Words = NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" Remarks = N'{SbMaster.Remarks.Trim().Replace("'", "''")}'"
        //            : "Remarks = NULL");
        //        cmdString.Append($" WHERE SB_Invoice = N'{SbMaster.SB_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SbMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(
        //                " INSERT INTO AMS.SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //            cmdString.Append(" VALUES \n");
        //            foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtGodownId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtAltQty"].Value},"
        //                    : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtAltUOMId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtQty"].Value},"
        //                    : "1,");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtUOMId"].Value},"
        //                    : "NUll,");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");
        //                var termAmount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var basicAmount = dr.Cells["GTxtDisplayAmount"].Value.GetDecimal();
        //                basicAmount -= termAmount;
        //                cmdString.Append($"{basicAmount},");
        //                cmdString.Append($"{termAmount},");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append("NULL,0,NULL,0,");

        //                var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

        //                cmdString.Append(
        //                    $"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',");

        //                var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                cmdString.Append(
        //                    $"{pDiscountRate},{pDiscount},{},{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
        //                cmdString.Append(iRows == SbMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }

        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                if (val <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(
        //                    "INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //                cmdString.Append($"Select '{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{ObjGlobal.SalesDiscountTermId},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal()},");
        //                cmdString.Append(
        //                    $"'P',{SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value},'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //            }
        //        }

        //        if (SbMaster.SpecialDiscount > 0)
        //        {
        //            cmdString.Append(
        //                "INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)\n");
        //            cmdString.Append($"SELECT '{SbMaster.SB_Invoice}',{ObjGlobal.SalesSpecialDiscountTermId},1,");
        //            cmdString.Append(
        //                $"{SbMaster.SpecialDiscountRate.GetDecimal()},{SbMaster.SpecialDiscount.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //        }

        //        if (SbMaster.ServiceCharge > 0)
        //        {
        //            cmdString.Append(
        //                "INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)\n");
        //            cmdString.Append($"SELECT '{SbMaster.SB_Invoice}',{ObjGlobal.SalesServiceChargeTermId},1,");
        //            cmdString.Append(
        //                $"{SbMaster.ServiceChargeRate.GetDecimal()},{SbMaster.ServiceCharge.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //        }

        //        if (SbMaster.VatAmount > 0)
        //        {
        //            cmdString.Append(
        //                "INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)\n");
        //            cmdString.Append($"SELECT '{SbMaster.SB_Invoice}',{ObjGlobal.SalesVatTermId},1,");
        //            cmdString.Append($"{SbMaster.VatRate.GetDecimal()},{SbMaster.VatAmount.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL \n");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    AuditLogSalesInvoice(actionTag);
        //    if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    _ = SalesTermPostingAsync();
        //    _ = SalesInvoiceAccountPosting();
        //    _ = SalesInvoiceStockPosting();
        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingBillToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }
    public int UpdateRestroOrder(bool isQty = false)
    {
        var cmdstring = new StringBuilder();
        if (isQty)
        {
            cmdstring.Append(
                @$"UPDATE AMS.SO_Details SET Qty = '{SoDetails.Qty}', Rate = '{SoDetails.Rate}', B_Amount= '{SoDetails.B_Amount}', T_Amount ='{SoDetails.T_Amount}', N_Amount = '{SoDetails.N_Amount}',PDiscountRate='{SoDetails.PDiscountRate}',PDiscount = '{SoDetails.PDiscount}',SyncRowVersion={SoDetails.SyncRowVersion},SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() WHERE SO_Invoice = '{SoDetails.SO_Invoice}' AND P_Id = '{SoDetails.P_Id}' AND Invoice_SNo = '{SoDetails.Invoice_SNo}' ");
        }
        else
        {
            cmdstring.Append(
                " INSERT INTO AMS.SO_Details(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion,MasterKeyId) \n");
            cmdstring.Append(@$"VALUES('{SoDetails.SO_Invoice}', {SoDetails.Invoice_SNo}, {SoDetails.P_Id},");
            cmdstring.Append(SoDetails.Gdn_Id > 0 ? $" {SoDetails.Gdn_Id}," : "NULL,");
            cmdstring.Append(SoDetails.Alt_Qty > 0 ? $"{SoDetails.Alt_Qty}," : "0,");
            cmdstring.Append(SoDetails.Alt_UnitId > 0 ? $" {SoDetails.Alt_UnitId}," : "NULL,");
            cmdstring.Append(SoDetails.Qty > 0 ? $" {SoDetails.Qty}," : "1,");
            cmdstring.Append(SoDetails.Unit_Id > 0 ? $"{SoDetails.Unit_Id} ," : "NULL,");
            cmdstring.Append(
                $"{SoDetails.Rate}, {SoDetails.B_Amount}, {SoDetails.T_Amount}, {SoDetails.N_Amount}, {SoDetails.AltStock_Qty}, {SoDetails.Stock_Qty},");
            cmdstring.Append(SoDetails.Narration.IsValueExits() ? $" '{SoDetails.Narration}'," : "NULL,");
            cmdstring.Append(
                @" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
            cmdstring.Append(
                $" {SoDetails.PDiscountRate}, {SoDetails.PDiscount}, {SoDetails.BDiscountRate}, {SoDetails.BDiscount}, {SoDetails.ServiceChargeRate}, {SoDetails.ServiceCharge},");
            cmdstring.Append($@" NULL, NULL, NULL, NULL, NULL, 1,{SoMaster.MasterKeyId}); ");
        }

        var result = SaveDataInDatabase(cmdstring);
        return result;
    }
    public int SaveRestroOrderCancel(string actionTag)
    {
        try
        {
            var cmdString = new StringBuilder();
            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(
                    " INSERT INTO AMS.OrderCancelMaster(SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, CancelBy, CancelDate, CancelReason, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                cmdString.Append(
                    $" VALUES (N'{SoMaster.SO_Invoice}', '{SoMaster.Invoice_Date:yyyy-MM-dd}', N'{SoMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(SoMaster.Ref_Vno.IsValueExits()
                    ? $" N'{SoMaster.Ref_Vno}', '{SoMaster.Ref_Date:yyyy-MM-dd}','{SoMaster.Ref_Miti}',"
                    : "NULL,NULL,NULL,");
                cmdString.Append($" {SoMaster.Customer_Id},");
                cmdString.Append(SoMaster.PartyLedgerId > 0 ? $" {SoMaster.PartyLedgerId}," : "NULL,");
                cmdString.Append(SoMaster.Party_Name.IsValueExits() ? $" N'{SoMaster.Party_Name}'," : "NULL,");
                cmdString.Append(SoMaster.Vat_No.IsValueExits() ? $" N'{SoMaster.Vat_No}'," : "NULL,");
                cmdString.Append(SoMaster.Contact_Person.IsValueExits() ? $" N'{SoMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(SoMaster.Mobile_No.IsValueExits() ? $" N'{SoMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(SoMaster.Address.IsValueExits() ? $" N'{SoMaster.Address}'," : "NULL,");
                cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqNo}'," : "NULL,");
                cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqMiti}'," : "NULL,");
                cmdString.Append(
                    $"N'{SoMaster.Invoice_Type}', N'{SoMaster.Invoice_Mode}', '{SoMaster.Payment_Mode}', {SoMaster.DueDays},");
                cmdString.Append(SoMaster.DueDays > 0 ? $" '{SoMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(SoMaster.Agent_Id > 0 ? $" {SoMaster.Agent_Id}," : "NULL,");
                cmdString.Append(SoMaster.Subledger_Id > 0 ? $" {SoMaster.Subledger_Id}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,NULL,");
                cmdString.Append(SoMaster.Cls1 > 0 ? $" {SoMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(SoMaster.CounterId > 0 ? $" {SoMaster.CounterId}," : "NULL,");
                cmdString.Append(SoMaster.TableId > 0 ? $" {SoMaster.TableId}," : "NULL,");
                cmdString.Append("NULL,1,");
                cmdString.Append(SoMaster.Cur_Id > 0 ? $" {SoMaster.Cur_Id}," : "1,");
                cmdString.Append(
                    $" {SoMaster.Cur_Rate.GetDecimal(true)},{SoMaster.B_Amount.GetDecimal()},{SoMaster.T_Amount.GetDecimal()},{SoMaster.N_Amount.GetDecimal()},{SoMaster.LN_Amount.GetDecimal()},");
                cmdString.Append(
                    $" {SoMaster.V_Amount.GetDecimal()},{SoMaster.Tbl_Amount.GetDecimal()},{SoMaster.Tender_Amount.GetDecimal()}, {SoMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($" N'{SoMaster.Action_Type}',");
                cmdString.Append(SoMaster.In_Words.IsValueExits() ? $" N'{SoMaster.In_Words}'," : "NULL,");
                cmdString.Append(
                    SoMaster.Remarks.IsValueExits() ? $" N'{SoMaster.Remarks.GetTrimReplace()}'," : "NULL,");
                cmdString.Append(
                    $"0,NULL,NULL,NULL,0,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                cmdString.Append(
                    $"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,GETDATE(),{SoMaster.SyncRowVersion.GetDecimal(true)}); \n");

                cmdString.Append(
                    "INSERT INTO AMS.OrderCancelDetails(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                cmdString.Append(@$"VALUES('{SoDetails.SO_Invoice}', {SoDetails.Invoice_SNo}, {SoDetails.P_Id},");
                cmdString.Append(SoDetails.Gdn_Id > 0 ? $" {SoDetails.Gdn_Id}," : "NULL,");
                cmdString.Append(SoDetails.Alt_Qty > 0 ? $"{SoDetails.Alt_Qty}," : "0,");
                cmdString.Append(SoDetails.Alt_UnitId > 0 ? $" {SoDetails.Alt_UnitId}," : "NULL,");
                cmdString.Append(SoDetails.Qty > 0 ? $" {SoDetails.Qty}," : "1,");
                cmdString.Append(SoDetails.Unit_Id > 0 ? $"{SoDetails.Unit_Id} ," : "NULL,");
                cmdString.Append(
                    $"{SoDetails.Rate}, {SoDetails.B_Amount}, {SoDetails.T_Amount}, {SoDetails.N_Amount}, {SoDetails.AltStock_Qty}, {SoDetails.Stock_Qty},");
                cmdString.Append(SoDetails.Narration.IsValueExits() ? $" '{SoDetails.Narration}'," : "NULL,");
                cmdString.Append(
                    @" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
                cmdString.Append(
                    $" {SoDetails.PDiscountRate}, {SoDetails.PDiscount}, {SoDetails.BDiscountRate}, {SoDetails.BDiscount}, {SoDetails.ServiceChargeRate}, {SoDetails.ServiceCharge},");
                cmdString.Append(@" NULL, NULL, NULL, NULL, NULL, 1); ");
            }

            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
    }
    public int UpdateRestroOrderCancel()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(
            "INSERT INTO AMS.OrderCancelDetails(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        cmdString.Append(@$"VALUES('{SoDetails.SO_Invoice}', {SoDetails.Invoice_SNo}, {SoDetails.P_Id},");
        cmdString.Append(SoDetails.Gdn_Id > 0 ? $" {SoDetails.Gdn_Id}," : "NULL,");
        cmdString.Append(SoDetails.Alt_Qty > 0 ? $"{SoDetails.Alt_Qty}," : "0,");
        cmdString.Append(SoDetails.Alt_UnitId > 0 ? $" {SoDetails.Alt_UnitId}," : "NULL,");
        cmdString.Append(SoDetails.Qty > 0 ? $" {SoDetails.Qty}," : "1,");
        cmdString.Append(SoDetails.Unit_Id > 0 ? $"{SoDetails.Unit_Id} ," : "NULL,");
        cmdString.Append(
            $"{SoDetails.Rate}, {SoDetails.B_Amount}, {SoDetails.T_Amount}, {SoDetails.N_Amount}, {SoDetails.AltStock_Qty}, {SoDetails.Stock_Qty},");
        cmdString.Append(SoDetails.Narration.IsValueExits() ? $" '{SoDetails.Narration}'," : "NULL,");
        cmdString.Append(
            @" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
        cmdString.Append(
            $" {SoDetails.PDiscountRate}, {SoDetails.PDiscount}, {SoDetails.BDiscountRate}, {SoDetails.BDiscount}, {SoDetails.ServiceChargeRate}, {SoDetails.ServiceCharge},");
        cmdString.Append(@" NULL, NULL, NULL, NULL, NULL, 1); ");
        var result = SaveDataInDatabase(cmdString);
        return result;
    }
    public int SaveRestroOrder(string actionTag)
    {
        //try
        //{
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "DELETE", "REVERSE" };
        //    if (strNamesArray.Contains(actionTag))
        //    {
        //        cmdString.Append(
        //            $" UPDATE AMS.SO_Master SET R_Invoice = 1, CancelBy ='{SoMaster.CancelBy}', CancelDate = GETDATE() , CancelReason = '{SoMaster.CancelReason}' WHERE SO_Invoice = '{SoMaster.SO_Invoice}'");
        //    }
        //    else
        //    {
        //        if (actionTag.Equals("SAVE"))
        //        {
        //            cmdString.Append(
        //                " INSERT INTO AMS.SO_Master(MasterKeyId,SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, CancelBy, CancelDate, CancelReason, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //            cmdString.Append(
        //                $" VALUES ({SoMaster.MasterKeyId},N'{SoMaster.SO_Invoice}', '{SoMaster.Invoice_Date:yyyy-MM-dd}', N'{SoMaster.Invoice_Miti}', GETDATE(),");
        //            cmdString.Append(SoMaster.Ref_Vno.IsValueExits()
        //                ? $" N'{SoMaster.Ref_Vno}', '{SoMaster.Ref_Date:yyyy-MM-dd}','{SoMaster.Ref_Miti}',"
        //                : "NULL,NULL,NULL,");
        //            cmdString.Append($" {SoMaster.Customer_Id},");
        //            cmdString.Append(SoMaster.PartyLedgerId > 0 ? $" {SoMaster.PartyLedgerId}," : "NULL,");
        //            cmdString.Append(SoMaster.Party_Name.IsValueExits() ? $" N'{SoMaster.Party_Name}'," : "NULL,");
        //            cmdString.Append(SoMaster.Vat_No.IsValueExits() ? $" N'{SoMaster.Vat_No}'," : "NULL,");
        //            cmdString.Append(SoMaster.Contact_Person.IsValueExits()
        //                ? $" N'{SoMaster.Contact_Person}',"
        //                : "NULL,");
        //            cmdString.Append(SoMaster.Mobile_No.IsValueExits() ? $" N'{SoMaster.Mobile_No}'," : "NULL,");
        //            cmdString.Append(SoMaster.Address.IsValueExits() ? $" N'{SoMaster.Address}'," : "NULL,");
        //            cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqNo}'," : "NULL,");
        //            cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
        //            cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqMiti}'," : "NULL,");
        //            cmdString.Append(
        //                $"N'{SoMaster.Invoice_Type}', N'{SoMaster.Invoice_Mode}', '{SoMaster.Payment_Mode}', {SoMaster.DueDays},");
        //            cmdString.Append(SoMaster.DueDays > 0 ? $" '{SoMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //            cmdString.Append(SoMaster.Agent_Id > 0 ? $" {SoMaster.Agent_Id}," : "NULL,");
        //            cmdString.Append(SoMaster.Subledger_Id > 0 ? $" {SoMaster.Subledger_Id}," : "NULL,");
        //            cmdString.Append("NULL,NULL,NULL,NULL,");
        //            cmdString.Append(SoMaster.Cls1 > 0 ? $" {SoMaster.Cls1}," : "NULL,");
        //            cmdString.Append("NULL,NULL,NULL,");
        //            cmdString.Append(SoMaster.CounterId > 0 ? $" {SoMaster.CounterId}," : "NULL,");
        //            cmdString.Append(SoMaster.TableId > 0 ? $" {SoMaster.TableId}," : "NULL,");
        //            cmdString.Append("NULL,1,");
        //            cmdString.Append(SoMaster.Cur_Id > 0 ? $" {SoMaster.Cur_Id}," : "1,");
        //            cmdString.Append(
        //                $" {SoMaster.Cur_Rate.GetDecimal(true)},{SoMaster.B_Amount.GetDecimal()},{SoMaster.T_Amount.GetDecimal()},{SoMaster.N_Amount.GetDecimal()},{SoMaster.LN_Amount.GetDecimal()},");
        //            cmdString.Append(
        //                $" {SoMaster.V_Amount.GetDecimal()},{SoMaster.Tbl_Amount.GetDecimal()},{SoMaster.Tender_Amount.GetDecimal()}, {SoMaster.Return_Amount.GetDecimal()},");
        //            cmdString.Append($" N'{SoMaster.Action_Type}',");
        //            cmdString.Append(SoMaster.In_Words.IsValueExits() ? $" N'{SoMaster.In_Words}'," : "NULL,");
        //            cmdString.Append(SoMaster.Remarks.IsValueExits()
        //                ? $" N'{SoMaster.Remarks.GetTrimReplace()}',"
        //                : "NULL,");
        //            cmdString.Append(
        //                $"0,NULL,NULL,NULL,0,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,");
        //            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //            cmdString.Append(
        //                $"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,GETDATE(),{SoMaster.SyncRowVersion.GetDecimal(true)}); \n");
        //        }

        //        if (SoMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(
        //                " INSERT INTO AMS.SO_Details(MasterKeyId,SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //            cmdString.Append(" VALUES \n");
        //            foreach (DataGridViewRow dr in SoMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                var dtCheckExitsOrNot =
        //                    $"SELECT SO_Invoice FROM AMS.SO_Details WHERE Invoice_SNo = '{dr.Cells["GTxtSNo"].Value}' AND P_Id = '{dr.Cells["GTxtProductId"].Value}' AND SO_Invoice = '{SoMaster.SO_Invoice}'"
        //                        .GetQueryDataTable();
        //                if (dtCheckExitsOrNot.Rows.Count > 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append($"({SoMaster.MasterKeyId},'{SoMaster.SO_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtGodownId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtAltQty"].Value},"
        //                    : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtAltUOMId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtQty"].Value.GetDecimal() > 0
        //                    ? $"{dr.Cells["GTxtQty"].Value},"
        //                    : "1,");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtUOMId"].Value},"
        //                    : "NUll,");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");
        //                var termAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                termAmount -= dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var basicAmount = dr.Cells["GTxtDisplayAmount"].Value.GetDecimal();
        //                basicAmount -= termAmount;
        //                cmdString.Append($"{basicAmount},");
        //                cmdString.Append($"{termAmount},");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append("NULL,0,NULL,0,");
        //                var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
        //                cmdString.Append($"{taxAmount},{vAmount},{vRate},0,NULL,0,0,NULL,0,0,");
        //                cmdString.Append(vRate > 0 ? "1," : "0,");
        //                cmdString.Append(
        //                    "NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append("0,0,GETDATE(),NULL,0,NULL,");
        //                var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                var serviceCharge = dr.Cells["GTxtValueServiceChange"].Value.GetDecimal();
        //                cmdString.Append(
        //                    $"{pDiscountRate},{pDiscount},{SoMaster.SpecialDiscountRate},{bDiscount},{SoMaster.ServiceChargeRate},{serviceCharge},");
        //                cmdString.Append("NULL,NULL,NULL,NULL,NULL,1");
        //                cmdString.Append(iRows == SoMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }
        //        }
        //        else
        //        {
        //            cmdString.Append(
        //                " INSERT INTO AMS.SO_Details(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion,MasterKeyId) \n");
        //            cmdString.Append(@$"VALUES('{SoDetails.SO_Invoice}', {SoDetails.Invoice_SNo}, {SoDetails.P_Id},");
        //            cmdString.Append(SoDetails.Gdn_Id > 0 ? $" {SoDetails.Gdn_Id}," : "NULL,");
        //            cmdString.Append(SoDetails.Alt_Qty > 0 ? $"{SoDetails.Alt_Qty}," : "0,");
        //            cmdString.Append(SoDetails.Alt_UnitId > 0 ? $" {SoDetails.Alt_UnitId}," : "NULL,");
        //            cmdString.Append(SoDetails.Qty > 0 ? $" {SoDetails.Qty}," : "1,");
        //            cmdString.Append(SoDetails.Unit_Id > 0 ? $"{SoDetails.Unit_Id} ," : "NULL,");
        //            cmdString.Append(
        //                $"{SoDetails.Rate}, {SoDetails.B_Amount}, {SoDetails.T_Amount}, {SoDetails.N_Amount}, {SoDetails.AltStock_Qty}, {SoDetails.Stock_Qty},");
        //            cmdString.Append(SoDetails.Narration.IsValueExits() ? $" '{SoDetails.Narration}'," : "NULL,");
        //            cmdString.Append(
        //                @" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
        //            cmdString.Append(
        //                $" {SoDetails.PDiscountRate}, {SoDetails.PDiscount}, {SoDetails.BDiscountRate}, {SoDetails.BDiscount}, {SoDetails.ServiceChargeRate}, {SoDetails.ServiceCharge},");
        //            cmdString.Append($@" NULL, NULL, NULL, NULL, NULL, 1,{SoMaster.MasterKeyId}); ");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    AuditLogSalesOrder(actionTag);
        //    if (SoMaster.TableId > 0 && actionTag.Equals("SAVE"))
        //    {
        //        var cmdText = $"UPDATE AMS.TableMaster SET TableStatus ='O' WHERE TableId = {SoMaster.TableId}";
        //        var result = ExecuteCommand.ExecuteNonQuery(cmdText);
        //    }

        //    if (SoMaster.TableId > 0 && strNamesArray.Contains(actionTag))
        //    {
        //        var cmdText = $"UPDATE AMS.TableMaster SET TableStatus ='A' WHERE TableId = {SoMaster.TableId}";
        //        var result = ExecuteCommand.ExecuteNonQuery(cmdText);
        //    }

        //    if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    _ = SalesOrderTermPosting();
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }
    public int SaveSalesInvoice(string actionTag)
    {
        //var invoiceNo = SbMaster.SB_Invoice;
        //try
        //{
        //    var cmdString = new StringBuilder();
        //    if (actionTag is "DELETE" or "REVERSE" or "UPDATE")
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        {
        //            AuditLogSalesInvoice(actionTag);
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($@" 
        //            DELETE FROM AMS.SB_Term WHERE SB_VNo ='{SbMaster.SB_Invoice}'; ");
        //            cmdString.Append($@" 
        //            DELETE FROM AMS.SB_Details WHERE SB_Invoice ='{SbMaster.SB_Invoice}';");
        //        }

        //        cmdString.Append($@" 
        //        DELETE FROM AMS.AccountDetails WHERE module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; ");
        //        cmdString.Append($@" 
        //        DELETE FROM AMS.StockDetails WHERE module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; ");
        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append($@" 
        //            DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; ");
        //            cmdString.Append($@" 
        //            DELETE FROM AMS.SB_Master_OtherDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; ");
        //            cmdString.Append($@" 
        //            DELETE FROM AMS.SB_Master WHERE SB_Invoice ='{SbMaster.SB_Invoice}'; ");
        //        }

        //        if (actionTag == "REVERSE")
        //        {
        //            cmdString.Append($@" 
        //            UPDATE AMS.SB_Master SET R_Invoice =1, Cancel_By='{ObjGlobal.LogInUser}', Cancel_Date= GETDATE(),Cancel_Remarks='{SbMaster.Remarks}' WHERE SB_Invoice='{SbMaster.SB_Invoice}' ");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        cmdString.Append(@" 
        //        INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //        cmdString.Append(
        //            $"\n VALUES (N'{SbMaster.SB_Invoice}', '{SbMaster.Invoice_Date:yyyy-MM-dd}', N'{SbMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" N'{SbMaster.PB_Vno}', '{SbMaster.Vno_Date:yyyy-MM-dd}','{SbMaster.Vno_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" N'{SbMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $" N'{SbMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" N'{SbMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" N'{SbMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits() ? $" N'{SbMaster.Address}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append($"N'{SbMaster.Invoice_Type}', N'{SbMaster.Invoice_Mode}','{SbMaster.Payment_Mode}',{SbMaster.DueDays},");
        //        cmdString.Append(SbMaster.DueDays > 0 ? $" '{SbMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" {SbMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0 ? $" {SbMaster.Subledger_Id}," : "NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Date:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Date:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" {SbMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $" {SbMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" {SbMaster.Cur_Id}," : "1,");
        //        cmdString.Append($" {SbMaster.Cur_Rate.GetDecimal(true)},");
        //        cmdString.Append($" {SbMaster.B_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.T_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.N_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.LN_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.V_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.Tbl_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.Tender_Amount.GetDecimal()},");
        //        cmdString.Append($" {SbMaster.Return_Amount.GetDecimal()},");
        //        cmdString.Append($"'{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits() ? $" N'{SbMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits() ? $" N'{SbMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
        //        cmdString.Append(SbMaster.R_Invoice ? $"CAST('{SbMaster.R_Invoice}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.No_Print > 0 ? $" {SbMaster.No_Print}," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_By}'," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_Date: yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Audit_Lock is true ? $"CAST('{SbMaster.Audit_Lock}' AS BIT) ," : "0,");
        //        cmdString.Append($" '{ObjGlobal.LogInUser}',GETDATE(),");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NUll,NULL,NULL,1,NULL); \n");
        //    }
        //    else if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append(" UPDATE AMS.SB_Master SET \n");
        //        cmdString.Append(
        //            $"Invoice_Date = '{SbMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{SbMaster.Invoice_Miti}',");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" PB_Vno = N'{SbMaster.PB_Vno}',Vno_Date = '{SbMaster.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{SbMaster.Vno_Miti}',"
        //            : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
        //        cmdString.Append($" Customer_Id = {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" Party_Name = N'{SbMaster.Party_Name}'," : "Party_Name = NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $"Vat_No =  N'{SbMaster.Vat_No}'," : "Vat_No = NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" Contact_Person = N'{SbMaster.Contact_Person}'," : "Contact_Person = NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" Mobile_No = N'{SbMaster.Mobile_No}'," : "Mobile_No = NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits() ? $" Address = N'{SbMaster.Address}'," : "Address = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SbMaster.ChqNo}'," : "ChqNo = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqDate = N'{SbMaster.ChqDate:yyyy-MM-dd}'," : "ChqDate = NULL,");
        //        cmdString.Append($"Invoice_Type = N'{SbMaster.Invoice_Type}', Invoice_Mode = N'{SbMaster.Invoice_Mode}',Payment_Mode =  '{SbMaster.Payment_Mode}',DueDays =  {SbMaster.DueDays},");
        //        cmdString.Append(SbMaster.DueDays > 0 ? $" DueDate = '{SbMaster.DueDate:yyyy-MM-dd}'," : "DueDate = NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" Agent_Id = {SbMaster.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0 ? $" Subledger_Id = {SbMaster.Subledger_Id}," : "Subledger_Id = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" SO_Invoice = N'{SbMaster.SO_Invoice}'," : "SO_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" SO_Date = N'{SbMaster.SO_Date:yyyy-MM-dd}'," : "SO_Date = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" SC_Invoice = N'{SbMaster.SC_Invoice}'," : "SC_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" SC_Date = N'{SbMaster.SC_Date:yyyy-MM-dd}'," : "SC_Date = NULL,");
        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" Cls1 = {SbMaster.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $"CounterId = {SbMaster.CounterId}," : " CounterId = NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" Cur_Id = {SbMaster.Cur_Id}," : "Cur_Id = 1,");
        //        cmdString.Append($"Cur_Rate =  {SbMaster.Cur_Rate.GetDecimal(true)},");
        //        cmdString.Append($"B_Amount =  {SbMaster.B_Amount.GetDecimal()},");
        //        cmdString.Append($"T_Amount = {SbMaster.T_Amount.GetDecimal()},");
        //        cmdString.Append($"N_Amount = {SbMaster.N_Amount.GetDecimal()},");
        //        cmdString.Append($"LN_Amount = {SbMaster.LN_Amount.GetDecimal()},");
        //        cmdString.Append($"V_Amount =  {SbMaster.V_Amount.GetDecimal()},");
        //        cmdString.Append($"Tbl_Amount =  {SbMaster.Tbl_Amount.GetDecimal()},");
        //        cmdString.Append($"Tender_Amount = {SbMaster.Tender_Amount.GetDecimal()},");
        //        cmdString.Append($"Return_Amount =  {SbMaster.Return_Amount.GetDecimal()},");
        //        cmdString.Append($"Action_Type = '{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits() ? $"In_Words =  N'{SbMaster.In_Words}'," : "In_Words = NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits() ? $" Remarks = N'{SbMaster.Remarks.Trim().Replace("'", "''")}'," : "Remarks = NULL,");
        //        cmdString.Append("IsSynced=0 ");
        //        cmdString.Append($" WHERE SB_Invoice = N'{SbMaster.SB_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SbMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(@" INSERT INTO AMS.SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //            cmdString.Append("\n VALUES \n");
        //            foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //            {
        //                if (dr.Cells["GTxtProductId"].Value.GetLong() is 0)
        //                {
        //                    continue;
        //                }
        //                cmdString.Append($"('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSno"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");

        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0 ? $"{dr.Cells["GTxtGodownId"].Value}," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0 ? $"{dr.Cells["GTxtAltQty"].Value}," : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0 ? $"{dr.Cells["GTxtAltUOMId"].Value}," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtQty"].Value.GetDecimal() > 0 ? $"{dr.Cells["GTxtQty"].Value}," : "1,");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0 ? $"{dr.Cells["GTxtUOMId"].Value}," : "NUll,");

        //                cmdString.Append($"{dr.Cells["GTxtRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtTermAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltStockQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtStockQty"].Value.GetDecimal()},");

        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() ? $"'{dr.Cells["GTxtNarration"].Value.GetTrimReplace()}'," : "NULL,");

        //                cmdString.Append(dr.Cells["GTxtOrderNo"].Value.IsValueExits() ? $"'{dr.Cells["GTxtOrderNo"].Value}'," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtOrderNo"].Value.IsValueExits() ? $"'{dr.Cells["GTxtOrderSno"].Value}'," : "0,");

        //                cmdString.Append(dr.Cells["GTxtChallanNo"].Value.IsValueExits() ? $"'{dr.Cells["GTxtChallanNo"].Value}'," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtChallanNo"].Value.IsValueExits() ? $"'{dr.Cells["GTxtChallanSno"].Value}'," : "0,");

        //                cmdString.Append("0, 0, 0, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',0,0,  0, 0,0,0,NULL,NULL,NULL,NULL,1,NULL),");

        //            }
        //            cmdString.ToString().TrimEnd();
        //            cmdString.Length--;
        //            cmdString.Append("; \n");
        //        }

        //        if (SbMaster.dtPTerm != null && SbMaster.dtPTerm.Rows.Count > 0)
        //        {
        //            if (actionTag.Equals("UPDATE"))
        //            {
        //                cmdString.Append($@"
        //                DELETE AMS.SB_Term WHERE SB_VNo='{SbMaster.SB_Invoice}' AND Term_Type='P'");
        //            }

        //            foreach (DataRow dr in SbMaster.dtPTerm.Rows)
        //            {
        //                if (dr["TermAmt"].GetDecimal() == 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@" 
        //                INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //                cmdString.Append($" VALUES ('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr["TermId"]},");
        //                cmdString.Append(dr["ProductSno"].GetInt() > 0 ? $"{dr["ProductSno"]}," : "1,");
        //                cmdString.Append($"{dr["TermRate"].GetDecimal()},{dr["TermAmt"].GetDecimal()},'P',");
        //                cmdString.Append(dr["ProductId"].GetLong() > 0 ? $"{dr["ProductId"]}," : "NULL,");
        //                cmdString.Append(dr["TermRate"].GetDecimal() > 0 ? "'Y'," : "'N',");
        //                cmdString.Append("NULL,NULL,NULL,NULL,1,NULL); \n");
        //            }
        //        }

        //        if (SbMaster.dtBTerm != null && SbMaster.dtBTerm.Rows.Count > 0)
        //        {
        //            if (actionTag.Equals("UPDATE"))
        //            {
        //                cmdString.Append($@"
        //                DELETE AMS.SB_Term WHERE SB_VNo='{SbMaster.SB_Invoice}' AND Term_Type='B' ");
        //            }

        //            foreach (DataRow dr in SbMaster.dtBTerm.Rows)
        //            {
        //                if (dr["TermAmt"].GetDecimal() == 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@" 
        //                INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
        //                cmdString.Append($" VALUES ('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr["TermId"]},");
        //                cmdString.Append(dr["ProductSno"].GetInt() > 0 ? $"{dr["ProductSno"]}," : "1,");
        //                cmdString.Append($"{dr["TermRate"].GetDecimal()},");
        //                cmdString.Append($"{dr["TermAmt"].GetDecimal()},");
        //                cmdString.Append("'B',");
        //                cmdString.Append(dr["ProductId"].GetInt() > 0 ? $"{dr["ProductId"]}," : "NULL,");
        //                cmdString.Append(dr["TermRate"].GetDecimal() > 0 ? "'Y'," : "'N',");
        //                cmdString.Append($"NULL,NULL,NULL,NULL,{SbMaster.SyncRowVersion.GetInt()},NULL); \n");
        //            }
        //        }

        //        if (SbMaster.ProductBatch != null && SbMaster.ProductBatch.Rows.Count > 0)
        //        {
        //            cmdString.Append($@" 
        //            DELETE AMS.ProductAddInfo WHERE VoucherNo='{SbMaster.SB_Invoice}' AND Module='SB'");
        //            cmdString.Append(@"  
        //            INSERT INTO AMS.ProductAddInfo(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
        //            cmdString.Append("  VALUES \n");
        //            foreach (DataRow dr in SbMaster.ProductBatch.Rows)
        //            {
        //                cmdString.Append($"('SB','{SbMaster.SB_Invoice}','O',");
        //                cmdString.Append($"{dr["ProductId"]},{dr["ProductSno"]},NULL,NULL,'{dr["BatchNo"]}',NULL,NULL,NULL,NULL,'{dr["MfDate"].GetSystemDate()}','{dr["ExpDate"].GetSystemDate()}',{dr["MRP"].GetDecimal()},{dr["Rate"].GetDecimal()},0,{dr["Qty"].GetDecimal()},");
        //                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //                cmdString.Append($"'{ObjGlobal.LogInUser}',GETDATE(),");
        //                cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{SbMaster.SyncRowVersion.GetDecimal(true)}),");
        //            }
        //            cmdString.ToString().TrimEnd(',');
        //            cmdString.Append(";");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    if (ObjGlobal.IsOnlineSync)
        //    {
        //        Task.Run(() => SyncSalesAsync(false, actionTag));
        //    }

        //    if (_tagStrings.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    SaveSalesOtherDetails("SB", actionTag);
        //    UpdateImageOnSales("SB");
        //    if (actionTag.Equals("SAVE"))
        //    {
        //        AuditLogSalesInvoice(actionTag);
        //    }

        //    _ = SalesTermPostingAsync();
        //    _ = SalesInvoiceAccountPosting();
        //    _ = SalesInvoiceStockPosting();
        //    if (SbMaster.SC_Invoice.IsValueExits() || SbMaster.SO_Invoice.IsValueExits())
        //    {
        //        UpdateOrderChallanOnInvoice();
        //    }

        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingBillToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    return 0;
        //}

        return 0;
    }
    public async Task<int> SyncSalesAsync(bool isPosInvoice, string actionTag)
    {
        ////sync
        //try
        //{
        //    var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        //    if (!configParams.Success || configParams.Model.Item1 == null)
        //    //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
        //    //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //configParams.ShowErrorDialog();
        //    {
        //        return 1;
        //    }

        //    var injectData = new DbSyncRepoInjectData
        //    {
        //        ExternalConnectionString = null,
        //        DateTime = DateTime.Now,
        //        LocalOriginId = configParams.Model.Item1,
        //        LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
        //        Username = ObjGlobal.LogInUser,
        //        LocalConnectionString = GetConnection.ConnectionString,
        //        LocalBranchId = ObjGlobal.SysBranchId,
        //        ApiConfig = DataSyncHelper.GetConfig()
        //    };
        //    var deleteUrl = string.Empty;
        //    var getUrl = string.Empty;
        //    var insertUrl = string.Empty;
        //    var updateUrl = string.Empty;

        //    getUrl = @$"{configParams.Model.Item2}Sales/GetSalesById";
        //    insertUrl = @$"{configParams.Model.Item2}Sales/InsertSales";
        //    updateUrl = @$"{configParams.Model.Item2}Sales/UpdateSales";
        //    deleteUrl = @$"{configParams.Model.Item2}Sales/DeleteSalesAsync?id=" + SbMaster.SB_Invoice;

        //    var apiConfig = new SyncApiConfig
        //    {
        //        BaseUrl = configParams.Model.Item2,
        //        Apikey = configParams.Model.Item3,
        //        Username = ObjGlobal.LogInUser,
        //        BranchId = ObjGlobal.SysBranchId,
        //        GetUrl = getUrl,
        //        InsertUrl = insertUrl,
        //        UpdateUrl = updateUrl,
        //        DeleteUrl = deleteUrl
        //    };

        //    DataSyncHelper.SetConfig(apiConfig);
        //    injectData.ApiConfig = apiConfig;
        //    DataSyncManager.SetGlobalInjectData(injectData);

        //    var salesRepo = DataSyncProviderFactory.GetRepository<SB_Master>(DataSyncManager.GetGlobalInjectData());
        //    var sc = new SB_Master
        //    {
        //        SB_Invoice = SbMaster.SB_Invoice,
        //        Invoice_Date = DateTime.Parse(SbMaster.Invoice_Date.ToString("yyyy-MM-dd")),
        //        Invoice_Miti = SbMaster.Invoice_Miti,
        //        Invoice_Time = DateTime.Now,
        //        PB_Vno = SbMaster.PB_Vno.IsValueExits() ? SbMaster.PB_Vno : null,
        //        Vno_Date = SbMaster.PB_Vno.IsValueExits()
        //            ? DateTime.Parse(SbMaster.Vno_Date.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        Vno_Miti = SbMaster.PB_Vno.IsValueExits() ? SbMaster.Vno_Miti : null,
        //        Customer_Id = SbMaster.Customer_Id,
        //        PartyLedgerId = SbMaster.PartyLedgerId.GetLong() > 0 ? SbMaster.PartyLedgerId : null,
        //        Party_Name = SbMaster.Party_Name.IsValueExits() ? SbMaster.Party_Name : null,
        //        Vat_No = ScMaster.Vat_No.IsValueExits() ? SbMaster.Vat_No : null,
        //        Contact_Person = SbMaster.Contact_Person.IsValueExits() ? SbMaster.Contact_Person : null,
        //        Mobile_No = SbMaster.Mobile_No.IsValueExits() ? SbMaster.Mobile_No : null,
        //        Address = SbMaster.Address.IsValueExits() ? SbMaster.Address : null,
        //        ChqNo = SbMaster.ChqNo.IsValueExits() ? SbMaster.ChqNo : null,
        //        ChqDate = SbMaster.ChqNo.IsValueExits()
        //            ? Convert.ToDateTime(SbMaster.ChqDate.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        ChqMiti = SbMaster.ChqNo.IsValueExits() ? SbMaster.ChqMiti : null,
        //        Invoice_Type = SbMaster.Invoice_Type,
        //        Invoice_Mode = SbMaster.Invoice_Mode,
        //        Payment_Mode = SbMaster.Payment_Mode,
        //        DueDays = SbMaster.DueDays.GetInt(),
        //        DueDate = SbMaster.DueDays.GetInt() > 0
        //            ? Convert.ToDateTime(SbMaster.DueDate.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        Agent_Id = SbMaster.Agent_Id > 0 ? SbMaster.Agent_Id : null,
        //        Subledger_Id = SbMaster.Subledger_Id > 0 ? SbMaster.Subledger_Id : null,
        //        SO_Invoice = SbMaster.SO_Invoice.IsValueExits() ? SbMaster.SO_Invoice : null,
        //        SO_Date = SbMaster.SO_Invoice.IsValueExits()
        //            ? Convert.ToDateTime(SbMaster.SO_Date.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        SC_Invoice = SbMaster.SC_Invoice.IsValueExits() ? SbMaster.SC_Invoice : null,
        //        SC_Date = SbMaster.SC_Invoice.IsValueExits()
        //            ? Convert.ToDateTime(SbMaster.SC_Date.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        Cls1 = SbMaster.Cls1 > 0 ? SbMaster.Cls1 : null,
        //        Cls2 = null,
        //        Cls3 = null,
        //        Cls4 = null,
        //        CounterId = SbMaster.CounterId > 0 ? SbMaster.CounterId : null,
        //        Cur_Id = SbMaster.Cur_Id > 0 ? SbMaster.Cur_Id : ObjGlobal.SysCurrencyId,
        //        Cur_Rate = SbMaster.Cur_Rate.GetDecimal(),
        //        B_Amount = SbMaster.B_Amount.GetDecimal(),
        //        T_Amount = SbMaster.T_Amount.GetDecimal(),
        //        N_Amount = SbMaster.N_Amount.GetDecimal(),
        //        LN_Amount = SbMaster.LN_Amount.GetDecimal(),
        //        V_Amount = SbMaster.V_Amount.GetDecimal(),
        //        Tbl_Amount = SbMaster.Tbl_Amount.GetDecimal(),
        //        Tender_Amount = SbMaster.Tender_Amount.GetDecimal(),
        //        Return_Amount = SbMaster.Return_Amount.GetDecimal(),
        //        Action_Type = SbMaster.Action_Type,
        //        In_Words = SbMaster.In_Words.IsValueExits() ? SbMaster.In_Words : null,
        //        Remarks = SbMaster.Remarks.IsValueExits() ? SbMaster.Remarks : null,
        //        R_Invoice = SbMaster.R_Invoice,
        //        Is_Printed = SbMaster.Is_Printed,
        //        No_Print = SbMaster.No_Print,
        //        Printed_By = SbMaster.Printed_By.IsValueExits() ? SbMaster.Printed_By : null,
        //        Printed_Date = SbMaster.Printed_By.IsValueExits() ? SbMaster.Printed_Date : null,
        //        Audit_Lock = SbMaster.Audit_Lock.GetBool() ? true : false,
        //        Enter_By = ObjGlobal.LogInUser,
        //        Enter_Date = DateTime.Now,
        //        Reconcile_By = null,
        //        Reconcile_Date = null,
        //        Auth_By = null,
        //        Auth_Date = null,
        //        Cancel_By = null,
        //        Cancel_Date = null,
        //        Cancel_Remarks = null,
        //        CBranch_Id = ObjGlobal.SysBranchId,
        //        CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
        //        IsAPIPosted = null,
        //        IsRealtime = null,
        //        FiscalYearId = ObjGlobal.SysFiscalYearId,
        //        PAttachment1 = null,
        //        PAttachment2 = null,
        //        PAttachment3 = null,
        //        PAttachment4 = null,
        //        PAttachment5 = null,
        //        DoctorId = null,
        //        PatientId = null,
        //        HDepartmentId = null,
        //        MShipId = SbMaster.MShipId.GetInt() > 0 ? SbMaster.MShipId : null,
        //        TableId = SbMaster.TableId.GetInt() > 0 ? SbMaster.TableId : null,
        //        SyncRowVersion = ScMaster.SyncRowVersion
        //    };
        //    var scDetails = new List<SB_Details>();
        //    var scTerms = new List<SB_Term>();
        //    var productAddInfoList = new List<ProductAddInfo>();
        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SbMaster.GetView is { RowCount: > 0 })
        //        {
        //            if (isPosInvoice)
        //            {
        //                foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //                {
        //                    var vatAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                    var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                    var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                    var netAmount = dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
        //                    var basicAmount = netAmount - vatAmount + pDiscount;

        //                    var termAmount = vatAmount - pDiscount;

        //                    var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                    var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                    var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
        //                    var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();

        //                    var scd = new SB_Details();
        //                    scd.SB_Invoice = SbMaster.SB_Invoice;
        //                    scd.Invoice_SNo = dr.Cells["GTxtSno"].Value.GetInt();
        //                    scd.P_Id = dr.Cells["GTxtProductId"].Value.GetLong();
        //                    scd.Gdn_Id = dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                        ? dr.Cells["GTxtGodownId"].Value.GetInt()
        //                        : null;
        //                    scd.Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                        ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
        //                        : 0;
        //                    scd.Alt_UnitId = dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                        ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
        //                        : null;
        //                    scd.Qty = dr.Cells["GTxtQty"].Value.GetDecimal() > 0
        //                        ? dr.Cells["GTxtQty"].Value.GetDecimal()
        //                        : 1;
        //                    scd.Unit_Id = dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                        ? dr.Cells["GTxtUOMId"].Value.GetInt()
        //                        : null;
        //                    scd.Rate = dr.Cells["GTxtDisplayRate"].Value.GetDecimal();
        //                    scd.B_Amount = basicAmount;
        //                    scd.T_Amount = termAmount;
        //                    scd.N_Amount = netAmount;
        //                    scd.AltStock_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal();
        //                    scd.Stock_Qty = dr.Cells["GTxtQty"].Value.GetDecimal();
        //                    scd.Narration = dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtNarration"].Value.GetTrimReplace()
        //                        : null;
        //                    scd.SO_Invoice = null;
        //                    scd.SO_Sno = 0;
        //                    scd.SC_Invoice = null;
        //                    scd.SC_SNo = 0;
        //                    scd.Tax_Amount = taxAmount;
        //                    scd.V_Amount = vAmount;
        //                    scd.V_Rate = vRate;
        //                    scd.Free_Unit_Id = null;
        //                    scd.Free_Qty = 0;
        //                    scd.StockFree_Qty = 0;
        //                    scd.ExtraFree_Unit_Id = null;
        //                    scd.ExtraFree_Qty = 0;
        //                    scd.ExtraStockFree_Qty = 0;
        //                    scd.T_Product = null;
        //                    scd.S_Ledger = null;
        //                    scd.SR_Ledger = null;
        //                    scd.SZ1 = null;
        //                    scd.SZ2 = null;
        //                    scd.SZ3 = null;
        //                    scd.SZ4 = null;
        //                    scd.SZ5 = null;
        //                    scd.SZ6 = null;
        //                    scd.SZ7 = null;
        //                    scd.SZ8 = null;
        //                    scd.SZ9 = null;
        //                    scd.SZ10 = null;
        //                    scd.Serial_No = null;
        //                    scd.Batch_No = null;
        //                    scd.Exp_Date = null;
        //                    scd.Manu_Date = null;
        //                    scd.MaterialPost = "N";
        //                    scd.PDiscountRate = pDiscountRate;
        //                    scd.PDiscount = pDiscount;
        //                    scd.BDiscountRate = SbMaster.TermRate;
        //                    scd.BDiscount = bDiscount;
        //                    scd.SyncRowVersion = ScMaster.SyncRowVersion;
        //                    scDetails.Add(scd);
        //                }
        //            }
        //            else
        //            {
        //                foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //                {
        //                    var scd = new SB_Details();
        //                    scd.SB_Invoice = SbMaster.SB_Invoice;
        //                    scd.Invoice_SNo = dr.Cells["GTxtSno"].Value.GetInt();
        //                    scd.P_Id = dr.Cells["GTxtProductId"].Value.GetLong();
        //                    scd.Gdn_Id = dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                        ? dr.Cells["GTxtGodownId"].Value.GetInt()
        //                        : null;
        //                    scd.Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                        ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
        //                        : 0;
        //                    scd.Alt_UnitId = dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                        ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
        //                        : null;
        //                    scd.Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
        //                        ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
        //                        : 1;
        //                    scd.Unit_Id = dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                        ? dr.Cells["GTxtUOMId"].Value.GetInt()
        //                        : null;
        //                    scd.Rate = dr.Cells["GTxtRate"].Value.GetDecimal();
        //                    scd.B_Amount = dr.Cells["GTxtAmount"].Value.GetDecimal();
        //                    scd.T_Amount = dr.Cells["GTxtTermAmount"].Value.GetDecimal();
        //                    scd.N_Amount = dr.Cells["GTxtNetAmount"].Value.GetDecimal();
        //                    scd.AltStock_Qty = dr.Cells["GTxtAltStockQty"].Value.GetDecimal();
        //                    scd.Stock_Qty = dr.Cells["GTxtStockQty"].Value.GetDecimal();
        //                    scd.Narration = dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtNarration"].Value.GetTrimReplace()
        //                        : null;
        //                    scd.SO_Invoice = dr.Cells["GTxtOrderNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtOrderNo"].Value.ToString()
        //                        : null;
        //                    scd.SO_Sno = dr.Cells["GTxtOrderNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtOrderSno"].Value.GetDecimal()
        //                        : 0;
        //                    scd.SC_Invoice = dr.Cells["GTxtChallanNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtChallanNo"].Value.ToString()
        //                        : null;
        //                    scd.SC_SNo = dr.Cells["GTxtChallanNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtChallanSno"].Value.GetDecimal()
        //                        : 0;
        //                    scd.Tax_Amount = 0;
        //                    scd.V_Amount = 0;
        //                    scd.V_Rate = 0;
        //                    scd.Free_Unit_Id = null;
        //                    scd.Free_Qty = 0;
        //                    scd.StockFree_Qty = 0;
        //                    scd.ExtraFree_Unit_Id = null;
        //                    scd.ExtraFree_Qty = 0;
        //                    scd.ExtraStockFree_Qty = 0;
        //                    scd.T_Product = null;
        //                    scd.S_Ledger = null;
        //                    scd.SR_Ledger = null;
        //                    scd.SZ1 = null;
        //                    scd.SZ2 = null;
        //                    scd.SZ3 = null;
        //                    scd.SZ4 = null;
        //                    scd.SZ5 = null;
        //                    scd.SZ6 = null;
        //                    scd.SZ7 = null;
        //                    scd.SZ8 = null;
        //                    scd.SZ9 = null;
        //                    scd.SZ10 = null;
        //                    scd.Serial_No = dr.Cells["GTxtSerialNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtSerialNo"].Value.ToString()
        //                        : null;
        //                    scd.Batch_No = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtBatchNo"].Value.ToString()
        //                        : null;
        //                    scd.Exp_Date = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
        //                        ? Convert.ToDateTime(dr.Cells["GTxtExpiryDate"].Value.GetSystemDate())
        //                        : null;
        //                    scd.Manu_Date = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
        //                        ? Convert.ToDateTime(dr.Cells["GTxtMfgDate"].Value.GetSystemDate())
        //                        : null;
        //                    scd.SyncRowVersion = ScMaster.SyncRowVersion;
        //                    scDetails.Add(scd);
        //                }
        //            }
        //        }

        //        if (isPosInvoice)
        //        {
        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                if (val <= 0)
        //                {
        //                    continue;
        //                }

        //                var sct = new SB_Term();
        //                sct.SB_VNo = SbMaster.SB_Invoice;
        //                sct.ST_Id = ObjGlobal.SalesDiscountTermId;
        //                sct.SNo = SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value.GetInt();
        //                sct.Term_Type = "P";
        //                sct.Product_Id = SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value.GetLong();
        //                sct.Rate = SbMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                sct.Amount = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                sct.Taxable = "N";
        //                sct.SyncRowVersion = ScMaster.SyncRowVersion;
        //                scTerms.Add(sct);
        //            }

        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                if (val is <= 0)
        //                {
        //                    continue;
        //                }

        //                var sct = new SB_Term();
        //                sct.SB_VNo = SbMaster.SB_Invoice;
        //                sct.ST_Id = ObjGlobal.SalesVatTermId;
        //                sct.SNo = SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value.GetInt();
        //                sct.Term_Type = "P";
        //                sct.Product_Id = SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value.GetLong();
        //                sct.Rate = SbMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                sct.Amount = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                sct.Taxable = SbMaster.GetView.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool() ? "Y" : "N";
        //                sct.SyncRowVersion = ScMaster.SyncRowVersion;
        //                scTerms.Add(sct);
        //            }

        //            if (SbMaster.T_Amount > 0)
        //            {
        //                var sct = new SB_Term();
        //                sct.SB_VNo = SbMaster.SB_Invoice;
        //                sct.ST_Id = ObjGlobal.SalesSpecialDiscountTermId;
        //                sct.SNo = 1;
        //                sct.Term_Type = "B";
        //                sct.Product_Id = null;
        //                sct.Rate = SbMaster.TermRate;
        //                sct.Amount = SbMaster.T_Amount;
        //                sct.Taxable = "N";
        //                sct.SyncRowVersion = ScMaster.SyncRowVersion;
        //                scTerms.Add(sct);
        //            }
        //        }
        //        else
        //        {
        //            if (SbMaster.dtPTerm != null && SbMaster.dtPTerm.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in SbMaster.dtPTerm.Rows)
        //                {
        //                    if (dr["TermAmt"].GetDecimal() == 0)
        //                    {
        //                        continue;
        //                    }

        //                    var sct = new SB_Term();
        //                    sct.SB_VNo = SbMaster.SB_Invoice;
        //                    sct.ST_Id = dr["TermId"].GetInt();
        //                    sct.SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1;
        //                    sct.Term_Type = "P";
        //                    sct.Product_Id = dr["ProductId"].GetInt() > 0 ? dr["ProductId"].GetLong() : null;
        //                    sct.Rate = dr["TermRate"].GetDecimal();
        //                    sct.Amount = dr["TermAmt"].GetDecimal();
        //                    sct.Taxable = dr["TermRate"].GetDecimal() > 0 &&
        //                                  dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
        //                        ? "Y"
        //                        : "N";
        //                    sct.SyncRowVersion = ScMaster.SyncRowVersion;
        //                    scTerms.Add(sct);
        //                }
        //            }

        //            if (SbMaster.dtBTerm != null && SbMaster.dtBTerm.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in SbMaster.dtBTerm.Rows)
        //                {
        //                    if (dr["TermAmt"].GetDecimal() == 0)
        //                    {
        //                        continue;
        //                    }

        //                    var sct = new SB_Term();
        //                    sct.SB_VNo = SbMaster.SB_Invoice;
        //                    sct.ST_Id = dr["TermId"].GetInt();
        //                    sct.SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1;
        //                    sct.Term_Type = "B";
        //                    sct.Product_Id = dr["ProductId"].GetInt() > 0 ? dr["ProductId"].GetLong() : null;
        //                    sct.Rate = dr["TermRate"].GetDecimal();
        //                    sct.Amount = dr["TermAmt"].GetDecimal();
        //                    sct.Taxable = dr["TermRate"].GetDecimal() > 0 &&
        //                                  dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
        //                        ? "Y"
        //                        : "N";
        //                    sct.SyncRowVersion = ScMaster.SyncRowVersion;
        //                    scTerms.Add(sct);
        //                }
        //            }

        //            if (SbMaster.ProductBatch != null && SbMaster.ProductBatch.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in SbMaster.ProductBatch.Rows)
        //                {
        //                    var pad = new ProductAddInfo();
        //                    pad.Module = "SB";
        //                    pad.VoucherNo = SbMaster.SB_Invoice;
        //                    pad.VoucherType = "O";
        //                    pad.ProductId = dr["ProductId"].GetLong();
        //                    pad.Sno = dr["ProductSno"].GetInt();
        //                    pad.SizeNo = null;
        //                    pad.SerialNo = null;
        //                    pad.BatchNo = dr["BatchNo"].ToString();
        //                    pad.ChasisNo = null;
        //                    pad.EngineNo = null;
        //                    pad.VHModel = null;
        //                    pad.VHColor = null;
        //                    pad.MFDate = Convert.ToDateTime(dr["MfDate"].GetSystemDate());
        //                    pad.ExpDate = Convert.ToDateTime(dr["ExpDate"].GetSystemDate());
        //                    pad.Mrp = dr["MRP"].GetDecimal();
        //                    pad.Rate = dr["Rate"].GetDecimal();
        //                    pad.AltQty = 0;
        //                    pad.Qty = dr["Qty"].GetDecimal();
        //                    pad.BranchId = ObjGlobal.SysBranchId;
        //                    pad.CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0
        //                        ? ObjGlobal.SysCompanyUnitId.GetInt()
        //                        : null;
        //                    pad.EnterBy = ObjGlobal.LogInUser;
        //                    pad.EnterDate = DateTime.Now;
        //                    pad.SyncRowVersion = SbMaster.SyncRowVersion;
        //                    productAddInfoList.Add(pad);
        //                }
        //            }
        //        }
        //    }

        //    sc.DetailsList = scDetails;
        //    sc.Terms = scTerms;
        //    sc.ProductAddInfoModels = productAddInfoList;

        //    var scOtherList = new List<SB_Master_OtherDetails>();
        //    if (actionTag != null)
        //    {
        //        var sco = new SB_Master_OtherDetails
        //        {
        //            SB_Invoice = SbOther.SB_Invoice,
        //            Transport = SbOther.Transport,
        //            VechileNo = SbOther.VechileNo,
        //            BiltyNo = SbOther.BiltyNo,
        //            Package = SbOther.Package,
        //            BiltyDate = !string.IsNullOrEmpty(SbOther.BiltyNo)
        //                ? Convert.ToDateTime(SbOther.BiltyDate.Value.ToString("yyyy-MM-dd"))
        //                : null,
        //            BiltyType = SbOther.BiltyType,
        //            Driver = SbOther.Driver,
        //            PhoneNo = SbOther.PhoneNo,
        //            LicenseNo = SbOther.LicenseNo,
        //            MailingAddress = SbOther.MailingAddress,
        //            MCity = SbOther.MCity,
        //            MState = SbOther.MState,
        //            MCountry = SbOther.MCountry,
        //            MEmail = SbOther.MEmail,
        //            ShippingAddress = SbOther.ShippingAddress,
        //            SCity = SbOther.SCity,
        //            SState = SbOther.SState,
        //            SCountry = SbOther.SCountry,
        //            SEmail = SbOther.SEmail,
        //            ContractNo = SbOther.ContractNo,
        //            ContractDate = !string.IsNullOrEmpty(SbOther.ContractNo)
        //                ? Convert.ToDateTime(SbOther.ContractDate.Value.ToString("yyyy-MM-dd"))
        //                : null,
        //            ExportInvoice = SbOther.ExportInvoice,
        //            ExportInvoiceDate = !string.IsNullOrEmpty(SbOther.ExportInvoice)
        //                ? Convert.ToDateTime(SbOther.ExportInvoiceDate.Value.ToString("yyyy-MM-dd"))
        //                : null,
        //            VendorOrderNo = SbOther.VendorOrderNo,
        //            BankDetails = SbOther.BankDetails,
        //            LcNumber = SbOther.LcNumber,
        //            CustomDetails = SbOther.CustomDetails
        //        };
        //        scOtherList.Add(sco);
        //    }

        //    sc.OtherDetailsList = scOtherList;
        //    var result = actionTag.ToUpper() switch
        //    {
        //        "SAVE" => await salesRepo?.PushNewAsync(sc),
        //        "NEW" => await salesRepo?.PushNewAsync(sc),
        //        "UPDATE" => await salesRepo?.PutNewAsync(sc),
        //        //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
        //        //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
        //        _ => await salesRepo?.PushNewAsync(sc)
        //    };
        //    if (result.Value)
        //    {
        //        CreateDatabaseTable.DropTrigger();
        //        var queryBuilder = new StringBuilder();
        //        queryBuilder.Append($@"
        //        UPDATE AMS.SB_Master SET IsSynced =1 WHERE SB_Invoice='{SbMaster.SB_Invoice}';");
        //        SaveDataInDatabase(queryBuilder);
        //        CreateDatabaseTable.CreateTrigger();
        //    }

        //    return 1;
        //}
        //catch (Exception ex)
        //{
        //    CreateDatabaseTable.CreateTrigger();
        //    return 1;
        //}

        return 0;
    }
    public async Task<int> UpdateSyncSalesColumnInServerAsync()
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

            getUrl = @$"{configParams.Model.Item2}Sales/GetSalesById";
            insertUrl = @$"{configParams.Model.Item2}Sales/InsertSales";
            updateUrl = @$"{configParams.Model.Item2}Sales/UpdateSyncSalesColumn";
            deleteUrl = @$"{configParams.Model.Item2}Sales/DeleteSalesAsync?id=" + SbMaster.SB_Invoice;

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

            var salesRepo = DataSyncProviderFactory.GetRepository<SB_Master>(DataSyncManager.GetGlobalInjectData());
            var sc = new SB_Master();
            sc.Action_Type = "UPDATE";
            sc.SB_Invoice = SbMaster.SB_Invoice;
            sc.Invoice_Date = DateTime.Parse(SbMaster.Invoice_Date.ToString("yyyy-MM-dd"));
            sc.Invoice_Miti = SbMaster.Invoice_Miti;
            sc.Invoice_Time = DateTime.Now;
            sc.PB_Vno = SbMaster.PB_Vno.IsValueExits() ? SbMaster.PB_Vno : null;
            sc.Vno_Date = SbMaster.PB_Vno.IsValueExits()
                ? DateTime.Parse(SbMaster.Vno_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Vno_Miti = SbMaster.PB_Vno.IsValueExits() ? SbMaster.Vno_Miti : null;
            sc.Customer_Id = SbMaster.Customer_Id;
            sc.PartyLedgerId = SbMaster.PartyLedgerId.GetLong() > 0 ? SbMaster.PartyLedgerId : null;
            sc.Party_Name = SbMaster.Party_Name.IsValueExits() ? SbMaster.Party_Name : null;
            sc.Vat_No = ScMaster.Vat_No.IsValueExits() ? SbMaster.Vat_No : null;
            sc.Contact_Person = SbMaster.Contact_Person.IsValueExits() ? SbMaster.Contact_Person : null;
            sc.Mobile_No = SbMaster.Mobile_No.IsValueExits() ? SbMaster.Mobile_No : null;
            sc.Address = SbMaster.Address.IsValueExits() ? SbMaster.Address : null;
            sc.ChqNo = SbMaster.ChqNo.IsValueExits() ? SbMaster.ChqNo : null;
            sc.ChqDate = SbMaster.ChqNo.IsValueExits()
                ? Convert.ToDateTime(SbMaster.ChqDate.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.ChqMiti = SbMaster.ChqNo.IsValueExits() ? SbMaster.ChqMiti : null;
            sc.Invoice_Type = SbMaster.Invoice_Type;
            sc.Invoice_Mode = SbMaster.Invoice_Mode;
            sc.Payment_Mode = SbMaster.Payment_Mode;
            sc.DueDays = SbMaster.DueDays.GetInt();
            sc.DueDate = SbMaster.DueDays.GetInt() > 0
                ? Convert.ToDateTime(SbMaster.DueDate.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Agent_Id = SbMaster.Agent_Id > 0 ? SbMaster.Agent_Id : null;
            sc.Subledger_Id = SbMaster.Subledger_Id > 0 ? SbMaster.Subledger_Id : null;
            sc.SO_Invoice = SbMaster.SO_Invoice.IsValueExits() ? SbMaster.SO_Invoice : null;
            sc.SO_Date = SbMaster.SO_Invoice.IsValueExits()
                ? Convert.ToDateTime(SbMaster.SO_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.SC_Invoice = SbMaster.SC_Invoice.IsValueExits() ? SbMaster.SC_Invoice : null;
            sc.SC_Date = SbMaster.SC_Invoice.IsValueExits()
                ? Convert.ToDateTime(SbMaster.SC_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Cls1 = SbMaster.Cls1 > 0 ? SbMaster.Cls1 : null;
            sc.Cls2 = null;
            sc.Cls3 = null;
            sc.Cls4 = null;
            sc.CounterId = SbMaster.CounterId > 0 ? SbMaster.CounterId : null;
            sc.Cur_Id = SbMaster.Cur_Id > 0 ? SbMaster.Cur_Id : ObjGlobal.SysCurrencyId;
            sc.Cur_Rate = SbMaster.Cur_Rate.GetDecimal();
            sc.B_Amount = SbMaster.B_Amount.GetDecimal();
            sc.T_Amount = SbMaster.T_Amount.GetDecimal();
            sc.N_Amount = SbMaster.N_Amount.GetDecimal();
            sc.LN_Amount = SbMaster.LN_Amount.GetDecimal();
            sc.V_Amount = SbMaster.V_Amount.GetDecimal();
            sc.Tbl_Amount = SbMaster.Tbl_Amount.GetDecimal();
            sc.Tender_Amount = SbMaster.Tender_Amount.GetDecimal();
            sc.Return_Amount = SbMaster.Return_Amount.GetDecimal();
            sc.In_Words = SbMaster.In_Words.IsValueExits() ? SbMaster.In_Words : null;
            sc.Remarks = SbMaster.Remarks.IsValueExits() ? SbMaster.Remarks : null;
            sc.R_Invoice = SbMaster.R_Invoice;
            sc.Is_Printed = SbMaster.Is_Printed;
            sc.No_Print = SbMaster.No_Print;
            sc.Printed_By = SbMaster.Printed_By.IsValueExits() ? SbMaster.Printed_By : null;
            sc.Printed_Date = SbMaster.Printed_By.IsValueExits() ? SbMaster.Printed_Date : null;
            sc.Audit_Lock = SbMaster.Audit_Lock.GetBool() ? true : false;
            sc.Enter_By = ObjGlobal.LogInUser;
            sc.Enter_Date = DateTime.Now;
            sc.Reconcile_By = null;
            sc.Reconcile_Date = null;
            sc.Auth_By = null;
            sc.Auth_Date = null;
            sc.Cancel_By = null;
            sc.Cancel_Date = null;
            sc.Cancel_Remarks = null;
            sc.CBranch_Id = ObjGlobal.SysBranchId;
            sc.CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            sc.IsAPIPosted = null;
            sc.IsRealtime = null;
            sc.FiscalYearId = ObjGlobal.SysFiscalYearId;
            sc.PAttachment1 = null;
            sc.PAttachment2 = null;
            sc.PAttachment3 = null;
            sc.PAttachment4 = null;
            sc.PAttachment5 = null;
            sc.DoctorId = null;
            sc.PatientId = null;
            sc.HDepartmentId = null;
            sc.MShipId = SbMaster.MShipId.GetInt() > 0 ? SbMaster.MShipId : null;
            sc.TableId = SbMaster.TableId.GetInt() > 0 ? SbMaster.TableId : null;
            sc.SyncRowVersion = ScMaster.SyncRowVersion;
            sc.IsSynced = true;

            var result = sc.Action_Type.ToUpper() switch
            {
                "SAVE" => await salesRepo?.PushNewAsync(sc),
                "NEW" => await salesRepo?.PushNewAsync(sc),
                "UPDATE" => await salesRepo?.PutNewAsync(sc),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await salesRepo?.PushNewAsync(sc)
            };

            return 1;
        }
        catch (Exception ex)
        {
            //CreateDatabaseTable.CreateTrigger();
            return 1;
        }
    }

    public int SaveTempSalesInvoice(string actionTag)
    {
        try
        {
            var cmdString = new StringBuilder();
            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(
                    " INSERT INTO AMS.temp_SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
                cmdString.Append(
                    $" VALUES (N'{TsbMaster.SB_Invoice}', '{TsbMaster.Invoice_Date:yyyy-MM-dd}', N'{TsbMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(((object)TsbMaster.PB_Vno).IsValueExits()
                    ? $" N'{TsbMaster.PB_Vno}', '{TsbMaster.Vno_Date:yyyy-MM-dd}','{TsbMaster.Vno_Miti}',"
                    : "NULL,NULL,NULL,");
                cmdString.Append($" {TsbMaster.Customer_Id},");
                cmdString.Append(TsbMaster.Party_Name.IsValueExits() ? $" N'{TsbMaster.Party_Name}'," : "NULL,");
                cmdString.Append(TsbMaster.Vat_No.IsValueExits() ? $" N'{TsbMaster.Vat_No}'," : "NULL,");
                cmdString.Append(TsbMaster.Contact_Person.IsValueExits()
                    ? $" N'{TsbMaster.Contact_Person}',"
                    : "NULL,");
                cmdString.Append(TsbMaster.Mobile_No.IsValueExits() ? $" N'{TsbMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(TsbMaster.Address.IsValueExits() ? $" N'{TsbMaster.Address}'," : "NULL,");
                cmdString.Append(TsbMaster.ChqNo.IsValueExits() ? $" N'{TsbMaster.ChqNo}'," : "NULL,");
                cmdString.Append(TsbMaster.ChqNo.IsValueExits() ? $" N'{TsbMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(
                    $"N'{TsbMaster.Invoice_Type}', N'{TsbMaster.Invoice_Mode}', '{TsbMaster.Payment_Mode}', {TsbMaster.DueDays},");
                cmdString.Append(TsbMaster.DueDays > 0 ? $" '{TsbMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(TsbMaster.Agent_Id > 0 ? $" {TsbMaster.Agent_Id}," : "NULL,");
                cmdString.Append(TsbMaster.Subledger_Id > 0 ? $" {TsbMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(TsbMaster.SO_Invoice.IsValueExits() ? $" N'{TsbMaster.SO_Invoice}'," : "NULL,");
                cmdString.Append(TsbMaster.SO_Invoice.IsValueExits()
                    ? $" N'{TsbMaster.SO_Date:yyyy-MM-dd}',"
                    : "NULL,");
                cmdString.Append(TsbMaster.SC_Invoice.IsValueExits() ? $" N'{TsbMaster.SC_Invoice}'," : "NULL,");
                cmdString.Append(TsbMaster.SC_Invoice.IsValueExits()
                    ? $" N'{TsbMaster.SC_Date:yyyy-MM-dd}',"
                    : "NULL,");
                cmdString.Append(TsbMaster.Cls1 > 0 ? $" {TsbMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(TsbMaster.CounterId > 0 ? $" {TsbMaster.CounterId}," : "NULL,");
                cmdString.Append(TsbMaster.Cur_Id > 0 ? $" {TsbMaster.Cur_Id}," : "1,");
                cmdString.Append(TsbMaster.Cur_Rate > 0 ? $" {TsbMaster.Cur_Rate}," : "1,");
                cmdString.Append(TsbMaster.B_Amount > 0 ? $" {TsbMaster.B_Amount}," : "0,");
                cmdString.Append(TsbMaster.T_Amount > 0 ? $" {TsbMaster.T_Amount}," : "0,");
                cmdString.Append(TsbMaster.N_Amount > 0 ? $" {TsbMaster.N_Amount}," : "0,");
                cmdString.Append(TsbMaster.LN_Amount > 0 ? $" {TsbMaster.LN_Amount}," : "0,");
                cmdString.Append(TsbMaster.V_Amount > 0 ? $" {TsbMaster.V_Amount}," : "0,");
                cmdString.Append(TsbMaster.Tbl_Amount > 0 ? $" {TsbMaster.Tbl_Amount}," : "0,");
                cmdString.Append(TsbMaster.Tender_Amount > 0 ? $" {TsbMaster.Tender_Amount}," : "0,");
                cmdString.Append(TsbMaster.Return_Amount > 0 ? $" {TsbMaster.Return_Amount}," : "0,");
                cmdString.Append($" '{TsbMaster.Action_Type}',");
                cmdString.Append(TsbMaster.In_Words.IsValueExits() ? $" N'{TsbMaster.In_Words}'," : "NULL,");
                cmdString.Append(TsbMaster.Remarks.IsValueExits()
                    ? $" N'{TsbMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "NULL,");
                cmdString.Append(TsbMaster.R_Invoice.HasValue ? $"CAST('{TsbMaster.R_Invoice}' AS BIT) ," : "0,");
                cmdString.Append(TsbMaster.Is_Printed.HasValue ? $"CAST('{TsbMaster.Is_Printed}' AS BIT) ," : "0,");
                cmdString.Append(TsbMaster.No_Print.HasValue ? $" {TsbMaster.No_Print}," : "0,");
                cmdString.Append(TsbMaster.Printed_By.IsValueExits() ? $" '{TsbMaster.Printed_By}'," : "0,");
                cmdString.Append(TsbMaster.Printed_By.IsValueExits()
                    ? $" '{TsbMaster.Printed_Date: yyyy-MM-dd}',"
                    : "NULL,");
                cmdString.Append(TsbMaster.Audit_Lock is true ? $"CAST('{TsbMaster.Audit_Lock}' AS BIT) ," : "0,");
                cmdString.Append($" '{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),");
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                cmdString.Append("NULL,NULL,");
                cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NUll,NULL,NULL,1,NULL); \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (TsbMaster.GetView is { RowCount: > 0 })
                {
                    var iRows = 0;
                    cmdString.Append(
                        " INSERT INTO AMS.temp_SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
                    cmdString.Append(" VALUES \n");
                    foreach (DataGridViewRow dr in TsbMaster.GetView.Rows)
                    {
                        iRows++;
                        cmdString.Append($"('{TsbMaster.SB_Invoice}',");
                        cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
                        cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
                        cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
                            ? $"{dr.Cells["GTxtGodownId"].Value},"
                            : "NULL,");
                        cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
                            ? $"{dr.Cells["GTxtAltQty"].Value},"
                            : "0,");
                        cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
                            ? $"{dr.Cells["GTxtAltUOMId"].Value},"
                            : "NULL,");
                        cmdString.Append(dr.Cells["GTxtQty"].Value.GetDecimal() > 0
                            ? $"{dr.Cells["GTxtQty"].Value},"
                            : "1,");
                        cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
                            ? $"{dr.Cells["GTxtUOMId"].Value},"
                            : "NUll,");
                        cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");
                        var termAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                        termAmount -= dr.Cells["GTxtPDiscount"].Value.GetDecimal();
                        var basicAmount = dr.Cells["GTxtDisplayAmount"].Value.GetDecimal();
                        basicAmount -= termAmount;
                        cmdString.Append($"{basicAmount},");
                        cmdString.Append($"{termAmount},");
                        cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
                        cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
                        cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
                        cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
                            ? $"'{dr.Cells["GTxtNarration"].Value}',"
                            : "NULL,");
                        cmdString.Append("NULL,0,NULL,0,");
                        var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
                        var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                        var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
                        cmdString.Append(
                            $"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',");
                        var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
                        var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
                        var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
                        cmdString.Append(
                            $"{pDiscountRate},{pDiscount},{TsbMaster.TermRate},{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
                        cmdString.Append(iRows == TsbMaster.GetView.RowCount ? " );" : "),");
                        cmdString.Append(" \n");
                    }
                }
            }

            var iResult = SaveDataInDatabase(cmdString);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public int SaveSalesReturn(string actionTag)
    {
        //try
        //{
        //    var invoiceNo = SrMaster.SR_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        {
        //            AuditLogSalesReturn(actionTag);
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($"Delete from AMS.SR_Term where SR_VNo ='{SrMaster.SR_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SR_Details where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append(
        //                $"UPDATE AMS.SR_Master SET Action_Type='CANCEL',R_Invoice =1 WHERE SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
        //        }

        //        cmdString.Append(
        //            $"Delete from AMS.AccountDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");
        //        cmdString.Append(
        //            $"Delete from AMS.StockDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append(
        //                $"Delete from AMS.SR_Master_OtherDetails where SR_Invoice = '{SrMaster.SR_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SR_Master where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        if (SrMaster.SB_Invoice.IsValueExits())
        //        {
        //            cmdString.Append(
        //                $" UPDATE AMS.SB_Master SET Action_Type ='RETURN' WHERE SB_Invoice='{SrMaster.SB_Invoice}' \n");
        //        }

        //        cmdString.Append(
        //            " INSERT INTO AMS.SR_Master(SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, Customer_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, Cancel_By, Cancel_Date, Cancel_Remarks, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //        cmdString.Append(
        //            $" VALUES (N'{SrMaster.SR_Invoice}', '{SrMaster.Invoice_Date:yyyy-MM-dd}', N'{SrMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SrMaster.SB_Invoice.IsValueExits()
        //            ? $" N'{SrMaster.SB_Invoice}', '{SrMaster.SB_Date:yyyy-MM-dd}','{SrMaster.SB_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SrMaster.Customer_ID},");
        //        cmdString.Append(SrMaster.PartyLedgerId > 0 ? $" {SrMaster.PartyLedgerId}," : "NULL,");
        //        cmdString.Append(SrMaster.Party_Name.IsValueExits() ? $" N'{SrMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SrMaster.Vat_No.IsValueExits() ? $" N'{SrMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SrMaster.Contact_Person.IsValueExits() ? $" N'{SrMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SrMaster.Mobile_No.IsValueExits() ? $" N'{SrMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SrMaster.Address.IsValueExits() ? $" N'{SrMaster.Address}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqMiti}'," : "NULL,");
        //        cmdString.Append(
        //            $" N'{SrMaster.Invoice_Type}', N'{SrMaster.Invoice_Mode}', '{SrMaster.Payment_Mode}', {SrMaster.DueDays},");
        //        cmdString.Append(SrMaster.DueDays > 0 ? $" '{SrMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SrMaster.Agent_Id > 0 ? $" {SrMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SrMaster.Subledger_Id > 0 ? $" {SrMaster.Subledger_Id}," : "NULL,");
        //        cmdString.Append(SrMaster.Cls1 > 0 ? $" {SrMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SrMaster.CounterId > 0 ? $" {SrMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SrMaster.Cur_Id > 0 ? $" {SrMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
        //        cmdString.Append($"{SrMaster.Cur_Rate.GetDecimal(true)},");
        //        cmdString.Append(
        //            $"{SrMaster.B_Amount.GetDecimal()},{SrMaster.T_Amount.GetDecimal()}, {SrMaster.N_Amount.GetDecimal()},{SrMaster.LN_Amount.GetDecimal()},{SrMaster.V_Amount.GetDecimal()},{SrMaster.Tbl_Amount.GetDecimal()},{SrMaster.Tender_Amount.GetDecimal()},{SrMaster.Return_Amount.GetDecimal()},'{SrMaster.Action_Type}',");
        //        cmdString.Append(SrMaster.In_Words.IsValueExits() ? $" N'{SrMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SrMaster.Remarks.IsValueExits()
        //            ? $" N'{SrMaster.Remarks.Trim().Replace("'", "''")}',"
        //            : "NULL,");
        //        cmdString.Append(
        //            $"0,0,NULL,NULL,NULL,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,0,0,{ObjGlobal.SysBranchId},");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append($"NUll,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
        //    }

        //    if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append(" UPDATE AMS.SR_Master SET	 \n");
        //        cmdString.Append(
        //            $" Invoice_Date='{SrMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti= N'{SrMaster.Invoice_Miti}',");
        //        cmdString.Append(SrMaster.SB_Invoice.IsValueExits()
        //            ? $"SB_Invoice= N'{SrMaster.SB_Invoice}',SB_Date= '{SrMaster.SB_Date:yyyy-MM-dd}',SB_Miti='{SrMaster.SB_Miti}',"
        //            : "SB_Invoice=NULL,SB_Date=NULL,SB_Miti=NULL,");
        //        cmdString.Append($" Customer_ID= {SrMaster.Customer_ID},");
        //        cmdString.Append(SrMaster.PartyLedgerId > 0
        //            ? $" PartyLedgerId= {SrMaster.PartyLedgerId},"
        //            : "PartyLedgerId= NULL,");
        //        cmdString.Append(SrMaster.Party_Name.IsValueExits()
        //            ? $" Party_Name= N'{SrMaster.Party_Name}',"
        //            : "Party_Name = NULL,");
        //        cmdString.Append(SrMaster.Vat_No.IsValueExits()
        //            ? $" Vat_No = N'{SrMaster.Vat_No}',"
        //            : "Vat_No = NULL,");
        //        cmdString.Append(SrMaster.Contact_Person.IsValueExits()
        //            ? $" Contact_Person = N'{SrMaster.Contact_Person}',"
        //            : "Contact_Person = NULL,");
        //        cmdString.Append(SrMaster.Mobile_No.IsValueExits()
        //            ? $" Mobile_No = N'{SrMaster.Mobile_No}',"
        //            : "Mobile_No = NULL,");
        //        cmdString.Append(SrMaster.Address.IsValueExits()
        //            ? $" Address = N'{SrMaster.Address}',"
        //            : "Address = NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SrMaster.ChqNo}'," : "ChqNo = NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits()
        //            ? $" ChqDate = N'{SrMaster.ChqDate:yyyy-MM-dd}',"
        //            : "ChqDate = NULL,");
        //        cmdString.Append(SrMaster.ChqNo.IsValueExits()
        //            ? $" ChqMiti = N'{SrMaster.ChqMiti}',"
        //            : "ChqMiti = NULL,");
        //        cmdString.Append(
        //            $" Invoice_Type = N'{SrMaster.Invoice_Type}', Invoice_Mode = N'{SrMaster.Invoice_Mode}',Payment_Mode =  '{SrMaster.Payment_Mode}',DueDays =  {SrMaster.DueDays},");
        //        cmdString.Append(SrMaster.DueDays > 0
        //            ? $" DueDate= '{SrMaster.DueDate:yyyy-MM-dd}',"
        //            : "DueDate = NULL,");
        //        cmdString.Append(SrMaster.Agent_Id > 0 ? $"Agent_Id =  {SrMaster.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(SrMaster.Subledger_Id > 0
        //            ? $" Subledger_Id = {SrMaster.Subledger_Id},"
        //            : "Subledger_Id = NULL,");
        //        cmdString.Append(SrMaster.Cls1 > 0 ? $" Cls1 = {SrMaster.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(SrMaster.CounterId > 0 ? $"CounterId =  {SrMaster.CounterId}," : "CounterId = NULL,");
        //        cmdString.Append(SrMaster.Cur_Id > 0
        //            ? $" Cur_Id = {SrMaster.Cur_Id},"
        //            : $"Cur_Id = {ObjGlobal.SysCurrencyId},");
        //        cmdString.Append($"Cur_Rate = {SrMaster.Cur_Rate.GetDecimal(true)},");
        //        cmdString.Append(
        //            $"B_Amount = {SrMaster.B_Amount.GetDecimal()},T_Amount = {SrMaster.T_Amount.GetDecimal()},N_Amount =  {SrMaster.N_Amount.GetDecimal()},LN_Amount = {SrMaster.LN_Amount.GetDecimal()},V_Amount = {SrMaster.V_Amount.GetDecimal()},Tbl_Amount = {SrMaster.Tbl_Amount.GetDecimal()},Tender_Amount = {SrMaster.Tender_Amount.GetDecimal()},Return_Amount = {SrMaster.Return_Amount.GetDecimal()},Action_Type = '{SrMaster.Action_Type}',");
        //        cmdString.Append(SrMaster.In_Words.IsValueExits()
        //            ? $" In_Words = N'{SrMaster.In_Words}',"
        //            : "In_Words = NULL,");
        //        cmdString.Append(SrMaster.Remarks.IsValueExits()
        //            ? $" Remarks = N'{SrMaster.Remarks.Trim().Replace("'", "''")}',"
        //            : "Remarks = NULL,");
        //        cmdString.Append("IsSynced=0");
        //        cmdString.Append(
        //            $"SyncRowVersion = {SrMaster.SyncRowVersion.GetDecimal(true)} WHERE SR_Invoice = '{SrMaster.SR_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SrMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(
        //                " INSERT INTO AMS.SR_Details(SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //            cmdString.Append(" VALUES \n");
        //            foreach (DataGridViewRow dr in SrMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{SrMaster.SR_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSno"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value.GetLong()},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtGodownId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtAltUOMId"].Value},"
        //                    : "NULL,");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal(true)},");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                    ? $"{dr.Cells["GTxtUOMId"].Value},"
        //                    : "NUll,");
        //                cmdString.Append($"{dr.Cells["GTxtRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtTermAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltStockQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtStockQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtNarration"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtInvoiceNo"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtInvoiceNo"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append($"{dr.Cells["GTxtInvoiceSno"].Value.GetInt()},");
        //                cmdString.Append("0,0,0,NULL,0,0,NULL,0,0,0,");
        //                cmdString.Append(dr.Cells["GTxtSBLedgerId"].Value.GetLong() > 0
        //                    ? $"{dr.Cells["GTxtSBLedgerId"].Value.GetLong()},"
        //                    : "NULL, ");
        //                cmdString.Append(dr.Cells["GTxtSRLedgerId"].Value.GetLong() > 0
        //                    ? $"{dr.Cells["GTxtSRLedgerId"].Value.GetLong()},"
        //                    : "NULL, ");
        //                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //                cmdString.Append(dr.Cells["GTxtSerialNo"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtSerialNo"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtBatchNo"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtBatchNo"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtExpiryDate"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtExpiryDate"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtMfgDate"].Value.IsValueExits()
        //                    ? $"'{dr.Cells["GTxtMfgDate"].Value.GetTrimReplace()}',"
        //                    : "NULL,");
        //                cmdString.Append("0,0,0,0,0,0,NULL,NULL,NULL,NULL,NULL,1");
        //                cmdString.Append(iRows == SrMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }
        //        }
        //    }

        //    if (SrMaster.dtPTerm != null && SrMaster.dtPTerm.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in SrMaster.dtPTerm.Rows)
        //        {
        //            if (dr["TermAmt"].GetDecimal() == 0)
        //            {
        //                continue;
        //            }

        //            cmdString.Append(
        //                " INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //            cmdString.Append($" VALUES ('{SrMaster.SR_Invoice}',");
        //            cmdString.Append($"{dr["TermId"]},");
        //            cmdString.Append(dr["ProductSno"].GetInt() > 0 ? $"{dr["ProductSno"]}," : "1,");
        //            cmdString.Append("'P',");
        //            cmdString.Append(dr["ProductId"].GetLong() > 0 ? $"{dr["ProductId"]}," : "NULL,");
        //            cmdString.Append($"{dr["TermRate"].GetDecimal()},{dr["TermAmt"].GetDecimal()},");
        //            cmdString.Append(dr["TermRate"].GetDecimal() > 0 &&
        //                             dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
        //                ? "'Y',"
        //                : "'N',");
        //            cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
        //        }
        //    }

        //    if (SrMaster.dtBTerm != null && SrMaster.dtBTerm.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in SrMaster.dtBTerm.Rows)
        //        {
        //            if (dr["TermAmt"].GetDecimal() == 0)
        //            {
        //                continue;
        //            }

        //            cmdString.Append(
        //                " INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
        //            cmdString.Append($" VALUES ('{SrMaster.SR_Invoice}',");
        //            cmdString.Append($"{dr["TermId"]},");
        //            cmdString.Append(dr["ProductSno"].GetInt() > 0 ? $"{dr["ProductSno"]}," : "1,");
        //            cmdString.Append("'B',");
        //            cmdString.Append(dr["ProductId"].GetLong() > 0 ? $"{dr["ProductId"]}," : "NULL,");
        //            cmdString.Append($"{dr["TermRate"].GetDecimal()},{dr["TermAmt"].GetDecimal()},");
        //            cmdString.Append(dr["TermRate"].GetDecimal() > 0 &&
        //                             dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
        //                ? "'Y',"
        //                : "'N',");
        //            cmdString.Append($"NULL,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    if (ObjGlobal.IsOnlineSync)
        //    {
        //        Task.Run(() => SyncSalesReturnAsync(false, actionTag));
        //    }

        //    if (_tagStrings.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    _ = SalesReturnTermPosting();
        //    _ = SalesReturnAccountPosting();
        //    _ = SalesReturnStockPosting();

        //    if (SrMaster.SB_Invoice.IsValueExits())
        //    {
        //        UpdateInvoiceTagOnSalesReturn();
        //    }

        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingSalesReturnToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }

    public int SaveUnSyncSalesReturnFromServerAsync(SR_Master SR_Master, string actionTag)
    {
        try
        {
            var query = $"SELECT SR_Invoice FROM AMS.SR_Master WHERE SR_Invoice='{SR_Master.SR_Invoice}'";
            var isAlreadExist = GetConnection.GetQueryData(query);
            actionTag = string.IsNullOrEmpty(isAlreadExist) ? "SAVE" : "UPDATE";
            SrMaster.SR_Invoice = SR_Master.SR_Invoice;
            SrMaster.Invoice_Date = SR_Master.Invoice_Date;
            SrMaster.Invoice_Miti = SR_Master.Invoice_Miti;
            SrMaster.Invoice_Time = SR_Master.Invoice_Time;
            SrMaster.SB_Invoice = SR_Master.SB_Invoice;
            SrMaster.SB_Date = SR_Master.SB_Date;
            SrMaster.SB_Miti = SR_Master.SB_Miti;
            SrMaster.Customer_ID = SR_Master.Customer_ID;
            SrMaster.PartyLedgerId = SR_Master.PartyLedgerId;
            SrMaster.Party_Name = SR_Master.Party_Name;
            SrMaster.Vat_No = SR_Master.Vat_No;
            SrMaster.Contact_Person = SR_Master.Contact_Person;
            SrMaster.Mobile_No = SR_Master.Mobile_No;
            SrMaster.Address = SR_Master.Address;
            SrMaster.ChqNo = SR_Master.ChqNo;
            SrMaster.ChqDate = SR_Master.ChqDate;
            SrMaster.ChqMiti = SR_Master.ChqMiti;
            SrMaster.Invoice_Type = SR_Master.Invoice_Type;
            SrMaster.Invoice_Mode = SR_Master.Invoice_Mode;
            SrMaster.Payment_Mode = SR_Master.Payment_Mode;
            SrMaster.DueDays = SR_Master.DueDays;
            SrMaster.DueDate = SR_Master.DueDate;
            SrMaster.Agent_Id = SR_Master.Agent_Id;
            SrMaster.Subledger_Id = SR_Master.Subledger_Id;
            SrMaster.Cls1 = SR_Master.Cls1;
            SrMaster.Cls2 = SR_Master.Cls2;
            SrMaster.Cls3 = SR_Master.Cls3;
            SrMaster.Cls4 = SR_Master.Cls4;
            SrMaster.CounterId = SR_Master.CounterId;
            SrMaster.Cur_Id = SR_Master.Cur_Id;
            SrMaster.Cur_Rate = SR_Master.Cur_Rate;
            SrMaster.B_Amount = SR_Master.B_Amount;
            SrMaster.T_Amount = SR_Master.T_Amount;
            SrMaster.N_Amount = SR_Master.N_Amount;
            SrMaster.LN_Amount = SR_Master.LN_Amount;
            SrMaster.V_Amount = SR_Master.V_Amount;
            SrMaster.Tbl_Amount = SR_Master.Tbl_Amount;
            SrMaster.Tender_Amount = SR_Master.Tender_Amount;
            SrMaster.Return_Amount = SR_Master.Return_Amount;
            SrMaster.In_Words = SR_Master.In_Words;
            SrMaster.Remarks = SR_Master.Remarks;
            SrMaster.R_Invoice = SR_Master.R_Invoice;
            SrMaster.Is_Printed = SR_Master.Is_Printed;
            SrMaster.Cancel_By = SR_Master.Cancel_By;
            SrMaster.Cancel_Date = SR_Master.Cancel_Date;
            SrMaster.Cancel_Remarks = SR_Master.Cancel_Remarks;
            SrMaster.No_Print = SR_Master.No_Print;
            SrMaster.Printed_By = SR_Master.Printed_By;
            SrMaster.Printed_Date = SR_Master.Printed_Date;
            SrMaster.Audit_Lock = SR_Master.Audit_Lock;
            SrMaster.Enter_By = SR_Master.Enter_By;
            SrMaster.Enter_Date = SR_Master.Enter_Date;
            SrMaster.Reconcile_By = SR_Master.Reconcile_By;
            SrMaster.Reconcile_Date = SR_Master.Reconcile_Date;
            SrMaster.Auth_By = SR_Master.Auth_By;
            SrMaster.Auth_Date = SR_Master.Auth_Date;
            SrMaster.Cleared_By = SR_Master.Cleared_By;
            SrMaster.Cleared_Date = SR_Master.Cleared_Date;
            SrMaster.IsAPIPosted = SR_Master.IsAPIPosted;
            SrMaster.IsRealtime = SR_Master.IsRealtime;
            SrMaster.CBranch_Id = SR_Master.CBranch_Id;
            SrMaster.CUnit_Id = SR_Master.CUnit_Id;
            SrMaster.FiscalYearId = SR_Master.FiscalYearId;
            SrMaster.SyncRowVersion = SR_Master.SyncRowVersion;

            var invoiceNo = SrMaster.SR_Invoice;
            var cmdString = new StringBuilder();
            string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
            if (strNamesArray.Any(x => x == actionTag))
            {
                if (!actionTag.Equals("UPDATE"))
                {
                    AuditLogSalesReturn(actionTag);
                }

                if (!actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($"Delete from AMS.SR_Term where SR_VNo ='{SrMaster.SR_Invoice}'; \n");
                    cmdString.Append($"Delete from AMS.SR_Details where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }

                if (actionTag.Equals("REVERSE"))
                {
                    cmdString.Append(
                        $"UPDATE AMS.SR_Master SET Action_Type='CANCEL',R_Invoice =1 WHERE SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }

                cmdString.Append(
                    $"Delete from AMS.AccountDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");
                cmdString.Append(
                    $"Delete from AMS.StockDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");

                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append(
                        $"Delete from AMS.SR_Master_OtherDetails where SR_Invoice = '{SrMaster.SR_Invoice}'; \n");
                    cmdString.Append($"Delete from AMS.SR_Master where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }
            }

            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(
                    " INSERT INTO AMS.SR_Master(SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, Customer_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, Cancel_By, Cancel_Date, Cancel_Remarks, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                cmdString.Append(
                    $" VALUES (N'{SrMaster.SR_Invoice}', '{SrMaster.Invoice_Date:yyyy-MM-dd}', N'{SrMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(SrMaster.SB_Invoice.IsValueExits()
                    ? $" N'{SrMaster.SB_Invoice}', '{SrMaster.SB_Date:yyyy-MM-dd}','{SrMaster.SB_Miti}',"
                    : "NULL,NULL,NULL,");
                cmdString.Append($" {SrMaster.Customer_ID},");
                cmdString.Append(SrMaster.PartyLedgerId > 0 ? $" {SrMaster.PartyLedgerId}," : "NULL,");
                cmdString.Append(SrMaster.Party_Name.IsValueExits() ? $" N'{SrMaster.Party_Name}'," : "NULL,");
                cmdString.Append(SrMaster.Vat_No.IsValueExits() ? $" N'{SrMaster.Vat_No}'," : "NULL,");
                cmdString.Append(SrMaster.Contact_Person.IsValueExits() ? $" N'{SrMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(SrMaster.Mobile_No.IsValueExits() ? $" N'{SrMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(SrMaster.Address.IsValueExits() ? $" N'{SrMaster.Address}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqNo}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqMiti}'," : "NULL,");
                cmdString.Append(
                    $" N'{SrMaster.Invoice_Type}', N'{SrMaster.Invoice_Mode}', '{SrMaster.Payment_Mode}', {SrMaster.DueDays},");
                cmdString.Append(SrMaster.DueDays > 0 ? $" '{SrMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
                cmdString.Append(SrMaster.Agent_Id > 0 ? $" {SrMaster.Agent_Id}," : "NULL,");
                cmdString.Append(SrMaster.Subledger_Id > 0 ? $" {SrMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(SrMaster.Cls1 > 0 ? $" {SrMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(SrMaster.CounterId > 0 ? $" {SrMaster.CounterId}," : "NULL,");
                cmdString.Append(SrMaster.Cur_Id > 0 ? $" {SrMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
                cmdString.Append($"{SrMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append(
                    $"{SrMaster.B_Amount.GetDecimal()},{SrMaster.T_Amount.GetDecimal()}, {SrMaster.N_Amount.GetDecimal()},{SrMaster.LN_Amount.GetDecimal()},{SrMaster.V_Amount.GetDecimal()},{SrMaster.Tbl_Amount.GetDecimal()},{SrMaster.Tender_Amount.GetDecimal()},{SrMaster.Return_Amount.GetDecimal()},'{SrMaster.Action_Type}',");
                cmdString.Append(SrMaster.In_Words.IsValueExits() ? $" N'{SrMaster.In_Words}'," : "NULL,");
                cmdString.Append(SrMaster.Remarks.IsValueExits()
                    ? $" N'{SrMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "NULL,");
                cmdString.Append(
                    $"0,0,NULL,NULL,NULL,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,0,0,{ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                cmdString.Append($"NUll,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (SR_Master.DetailsList.Count > 0)
                {
                    var iRows = 0;
                    cmdString.Append(
                        " INSERT INTO AMS.SR_Details (SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)\n");
                    cmdString.Append(" VALUES \n");

                    foreach (var dr in SR_Master.DetailsList)
                    {
                        iRows++;
                        cmdString.Append($"('{SrMaster.SR_Invoice}',");
                        cmdString.Append($"{dr.Invoice_SNo},");
                        cmdString.Append($"{dr.P_Id},");
                        cmdString.Append(dr.Gdn_Id.GetInt() > 0 ? $"{dr.Gdn_Id}," : "NULL,");
                        cmdString.Append(dr.Alt_Qty.GetDecimal() > 0 ? $"{dr.Alt_Qty}," : "0,");
                        cmdString.Append(dr.Alt_UnitId.GetInt() > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                        var qty = dr.Qty.GetDecimal() > 0 ? dr.Qty.GetDecimal() : 1;
                        cmdString.Append($"{qty},");
                        cmdString.Append(dr.Unit_Id.GetInt() > 0 ? $"{dr.Unit_Id}," : "NUll,");
                        cmdString.Append($"{dr.Rate.GetDecimal()},");

                        var vatAmount = dr.V_Amount.GetDecimal();
                        var pDiscount = dr.PDiscount.GetDecimal();
                        var bDiscount = dr.BDiscount.GetDecimal();
                        var netAmount = dr.N_Amount.GetDecimal();
                        var basicAmount = netAmount - vatAmount + pDiscount + bDiscount;
                        var rate = basicAmount / qty;

                        var termAmount = vatAmount - pDiscount;

                        cmdString.Append($"{basicAmount},");
                        cmdString.Append($"{termAmount},");
                        cmdString.Append($"{dr.N_Amount.GetDecimal()},");
                        cmdString.Append($"{dr.AltStock_Qty.GetDecimal()},");
                        cmdString.Append($"{dr.Stock_Qty.GetDecimal()},");
                        cmdString.Append(dr.Narration.IsValueExits() is true ? $"'{dr.Narration}'," : "NULL,");
                        cmdString.Append(dr.SB_Invoice.IsValueExits() is true
                            ? $"'{dr.SB_Invoice}','{dr.SB_Sno}',"
                            : "NULL,0,");

                        var taxAmount = dr.Tax_Amount.GetDecimal();
                        var vAmount = dr.V_Amount.GetDecimal();
                        var vRate = dr.V_Rate.GetDecimal();

                        cmdString.Append($"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0,");
                        cmdString.Append(vRate > 0 ? "1," : "0,");

                        cmdString.Append(
                            "NULL,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");

                        var pDiscountRate = dr.PDiscountRate.GetDecimal();
                        cmdString.Append($"{pDiscountRate},{pDiscount},0,{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
                        cmdString.Append(iRows == SR_Master.DetailsList.Count ? " );" : "),");
                        cmdString.Append(" \n");
                    }
                }

                if (SR_Master.Terms.Count > 0)
                {
                    foreach (var st in SR_Master.Terms)
                    {
                        cmdString.Append(
                            " INSERT INTO AMS.SR_Term (SR_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
                        cmdString.Append($" Select '{SrMaster.SR_Invoice}',");
                        cmdString.Append($" {st.ST_Id},");
                        cmdString.Append($" {st.SNo},");
                        cmdString.Append($" '{st.Term_Type}',");
                        cmdString.Append($" {st.Rate.GetDecimal()},");
                        cmdString.Append($" {st.Amount.GetDecimal()},");
                        cmdString.Append(st.Product_Id.GetLong() > 0 ? $" {st.Product_Id}," : "NULL,");
                        cmdString.Append(st.Taxable.IsValueExits() ? $" '{st.Taxable}'," : "'N',");
                        cmdString.Append("NULL,NULL,NULL,NULL,1,NULL \n");
                    }
                }
            }

            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            Task.Run(() => UpdateSyncSalesReturnColumnInServerAsync(actionTag));

            AuditLogSalesReturn(actionTag);
            if (actionTag.Equals("REVERSE"))
            {
                return iResult;
            }

            _ = SalesReturnTermPosting();
            _ = SalesReturnAccountPosting();
            _ = SalesReturnStockPosting();
            if (SrMaster.SB_Invoice.IsValueExits())
            {
                UpdateInvoiceTagOnSalesReturn();
            }
            if (!ObjGlobal.IsIrdRegister)
            {
                return iResult;
            }

            PostingSalesReturnToApi(invoiceNo);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
    }

    public async Task<int> SyncSalesReturnAsync(bool isPosReturn, string actionTag)
    {
        //sync
        //try
        //{
        //    var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        //    if (!configParams.Success || configParams.Model.Item1 == null)
        //    //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
        //    //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //configParams.ShowErrorDialog();
        //    {
        //        return 1;
        //    }

        //    var injectData = new DbSyncRepoInjectData
        //    {
        //        ExternalConnectionString = null,
        //        DateTime = DateTime.Now,
        //        LocalOriginId = configParams.Model.Item1,
        //        LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
        //        Username = ObjGlobal.LogInUser,
        //        LocalConnectionString = GetConnection.ConnectionString,
        //        LocalBranchId = ObjGlobal.SysBranchId,
        //        ApiConfig = DataSyncHelper.GetConfig()
        //    };
        //    var deleteUrl = string.Empty;
        //    var getUrl = string.Empty;
        //    var insertUrl = string.Empty;
        //    var updateUrl = string.Empty;

        //    getUrl = @$"{configParams.Model.Item2}SalesReturn/GetSalesReturnById";
        //    insertUrl = @$"{configParams.Model.Item2}SalesReturn/InsertSalesReturn";
        //    updateUrl = @$"{configParams.Model.Item2}SalesReturn/UpdateSalesReturn";
        //    deleteUrl = @$"{configParams.Model.Item2}SalesReturn/DeleteSalesReturnAsync?id=" + SrMaster.SR_Invoice;

        //    var apiConfig = new SyncApiConfig
        //    {
        //        BaseUrl = configParams.Model.Item2,
        //        Apikey = configParams.Model.Item3,
        //        Username = ObjGlobal.LogInUser,
        //        BranchId = ObjGlobal.SysBranchId,
        //        GetUrl = getUrl,
        //        InsertUrl = insertUrl,
        //        UpdateUrl = updateUrl,
        //        DeleteUrl = deleteUrl
        //    };

        //    DataSyncHelper.SetConfig(apiConfig);
        //    injectData.ApiConfig = apiConfig;
        //    DataSyncManager.SetGlobalInjectData(injectData);

        //    var salesReturnRepo = DataSyncProviderFactory.GetRepository<SR_Master>(DataSyncManager.GetGlobalInjectData());

        //    var sr = new SR_Master
        //    {
        //        SR_Invoice = SrMaster.SR_Invoice,
        //        Invoice_Date = DateTime.Parse(SrMaster.Invoice_Date.ToString("yyyy-MM-dd")),
        //        Invoice_Miti = SrMaster.Invoice_Miti,
        //        Invoice_Time = DateTime.Now,
        //        SB_Invoice = SrMaster.SB_Invoice.IsValueExits() ? SrMaster.SB_Invoice : null,
        //        SB_Date = SrMaster.SB_Invoice.IsValueExits()
        //            ? Convert.ToDateTime(SrMaster.SB_Date.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        SB_Miti = SrMaster.SB_Invoice.IsValueExits() ? SrMaster.SB_Miti : null,
        //        Customer_ID = SrMaster.Customer_ID,
        //        PartyLedgerId = SrMaster.PartyLedgerId > 0 ? SrMaster.PartyLedgerId : null,
        //        Party_Name = SrMaster.Party_Name.IsValueExits() ? SrMaster.Party_Name : null,
        //        Vat_No = SrMaster.Vat_No.IsValueExits() ? SrMaster.Vat_No : null,
        //        Contact_Person = SrMaster.Contact_Person.IsValueExits() ? SrMaster.Contact_Person : null,
        //        Mobile_No = SrMaster.Mobile_No.IsValueExits() ? SrMaster.Mobile_No : null,
        //        Address = SrMaster.Address.IsValueExits() ? SrMaster.Address : null,
        //        ChqNo = SrMaster.ChqNo.IsValueExits() ? SrMaster.ChqNo : null,
        //        ChqDate = SrMaster.ChqNo.IsValueExits()
        //            ? Convert.ToDateTime(SrMaster.ChqDate.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        ChqMiti = SrMaster.ChqNo.IsValueExits() ? SrMaster.ChqMiti : null,
        //        Invoice_Type = SrMaster.Invoice_Type,
        //        Invoice_Mode = SrMaster.Invoice_Mode,
        //        Payment_Mode = SrMaster.Payment_Mode,
        //        DueDays = SrMaster.DueDays,
        //        DueDate = SrMaster.DueDays > 0
        //            ? Convert.ToDateTime(SrMaster.DueDate.Value.ToString("yyyy-MM-dd"))
        //            : null,
        //        Agent_Id = SrMaster.Agent_Id > 0 ? SrMaster.Agent_Id : null,
        //        Subledger_Id = SrMaster.Subledger_Id > 0 ? SrMaster.Subledger_Id : null,
        //        Cls1 = SrMaster.Cls1 > 0 ? SrMaster.Cls1 : null,
        //        Cls2 = null,
        //        Cls3 = null,
        //        Cls4 = null,
        //        CounterId = SrMaster.CounterId.GetInt() > 0 ? SrMaster.CounterId : null,
        //        Cur_Id = SrMaster.Cur_Id > 0 ? SrMaster.Cur_Id : ObjGlobal.SysCurrencyId,
        //        Cur_Rate = SrMaster.Cur_Rate.GetDecimal(true),
        //        B_Amount = SrMaster.B_Amount.GetDecimal(),
        //        T_Amount = SrMaster.T_Amount.GetDecimal(),
        //        N_Amount = SrMaster.N_Amount.GetDecimal(),
        //        LN_Amount = SrMaster.LN_Amount.GetDecimal(),
        //        V_Amount = SrMaster.V_Amount.GetDecimal(),
        //        Tbl_Amount = SrMaster.Tbl_Amount.GetDecimal(),
        //        Tender_Amount = SrMaster.Tender_Amount.GetDecimal(),
        //        Return_Amount = SrMaster.Return_Amount.GetDecimal(),
        //        Action_Type = SrMaster.Action_Type,
        //        In_Words = SrMaster.In_Words.IsValueExits() ? SrMaster.In_Words : null,
        //        Remarks = SrMaster.Remarks.IsValueExits() ? SrMaster.Remarks.Trim().Replace("'", "''") : null,
        //        R_Invoice = false,
        //        Is_Printed = false,
        //        Cancel_By = null,
        //        Cancel_Date = null,
        //        Cancel_Remarks = null,
        //        No_Print = 0,
        //        Printed_By = null,
        //        Printed_Date = null,
        //        Audit_Lock = false,
        //        Enter_By = ObjGlobal.LogInUser.ToUpper(),
        //        Enter_Date = DateTime.Now,
        //        Reconcile_By = null,
        //        Reconcile_Date = null,
        //        Auth_By = null,
        //        Auth_Date = null,
        //        Cleared_By = null,
        //        Cleared_Date = null,
        //        IsAPIPosted = false,
        //        IsRealtime = false,
        //        CBranch_Id = ObjGlobal.SysBranchId,
        //        CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
        //        FiscalYearId = ObjGlobal.SysFiscalYearId,
        //        SyncRowVersion = SrMaster.SyncRowVersion
        //    };
        //    var srDetails = new List<SR_Details>();
        //    var srTerms = new List<SR_Term>();
        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SrMaster.GetView is { RowCount: > 0 })
        //        {
        //            if (isPosReturn)
        //            {
        //                foreach (DataGridViewRow dr in SrMaster.GetView.Rows)
        //                {
        //                    var srd = new SR_Details
        //                    {
        //                        SR_Invoice = SrMaster.SR_Invoice,
        //                        Invoice_SNo = dr.Cells["GTxtSno"].Value.GetDecimal(),
        //                        P_Id = dr.Cells["GTxtProductId"].Value.GetLong(),
        //                        Gdn_Id = dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                            ? dr.Cells["GTxtGodownId"].Value.GetInt()
        //                            : null,
        //                        Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal(),
        //                        Alt_UnitId = dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                            ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
        //                            : null,
        //                        Qty = dr.Cells["GTxtQty"].Value.GetDecimal(true),
        //                        Unit_Id = dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                            ? dr.Cells["GTxtUOMId"].Value.GetInt()
        //                            : null,
        //                        Rate = dr.Cells["GTxtDisplayRate"].Value.GetDecimal()
        //                    };

        //                    var vatAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                    var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                    var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //                    var netAmount = dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
        //                    var basicAmount = netAmount - vatAmount + pDiscount + bDiscount;
        //                    var termAmount = vatAmount - pDiscount;

        //                    srd.B_Amount = basicAmount;
        //                    srd.T_Amount = termAmount;
        //                    srd.N_Amount = netAmount;
        //                    srd.AltStock_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal();
        //                    srd.Stock_Qty = dr.Cells["GTxtQty"].Value.GetDecimal();
        //                    srd.Narration = dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtNarration"].Value.GetTrimReplace()
        //                        : null;
        //                    srd.SB_Invoice = dr.Cells["GTxtInvoiceNo"].Value.IsValueExits()
        //                        ? dr.Cells["GTxtInvoiceNo"].Value.GetTrimReplace()
        //                        : null;
        //                    srd.SB_Sno = 0;
        //                    var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                    var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                    var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
        //                    srd.Tax_Amount = taxAmount;
        //                    srd.V_Amount = vAmount;
        //                    srd.V_Rate = vRate;
        //                    srd.Free_Unit_Id = null;
        //                    srd.Free_Qty = 0;
        //                    srd.StockFree_Qty = 0;
        //                    srd.ExtraFree_Unit_Id = null;
        //                    srd.ExtraFree_Qty = 0;
        //                    srd.ExtraStockFree_Qty = 0;
        //                    srd.T_Product = vRate > 0 ? true : false;
        //                    srd.S_Ledger = null;
        //                    srd.SR_Ledger = null;
        //                    srd.SZ1 = null;
        //                    srd.SZ2 = null;
        //                    srd.SZ3 = null;
        //                    srd.SZ4 = null;
        //                    srd.SZ5 = null;
        //                    srd.SZ6 = null;
        //                    srd.SZ7 = null;
        //                    srd.SZ8 = null;
        //                    srd.SZ9 = null;
        //                    srd.SZ10 = null;
        //                    srd.Serial_No = null;
        //                    srd.Batch_No = null;
        //                    srd.Exp_Date = null;
        //                    srd.Manu_Date = null;
        //                    var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                    srd.PDiscountRate = pDiscountRate;
        //                    srd.PDiscount = pDiscount;
        //                    srd.BDiscountRate = 0;
        //                    srd.BDiscount = bDiscount;
        //                    srd.ServiceChargeRate = 0;
        //                    srd.ServiceCharge = 0;
        //                    srd.SyncRowVersion = SrMaster.SyncRowVersion;

        //                    srDetails.Add(srd);
        //                }
        //            }
        //            else
        //            {
        //                srDetails.AddRange(SrMaster.GetView.Rows.Cast<DataGridViewRow>().Select(dr => new SR_Details
        //                {
        //                    SR_Invoice = SrMaster.SR_Invoice,
        //                    Invoice_SNo = dr.Cells["GTxtSno"].Value.GetDecimal(),
        //                    P_Id = dr.Cells["GTxtProductId"].Value.GetLong(),
        //                    Gdn_Id =
        //                            dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
        //                                ? dr.Cells["GTxtGodownId"].Value.GetInt()
        //                                : null,
        //                    Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal(),
        //                    Alt_UnitId =
        //                            dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
        //                                ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
        //                                : null,
        //                    Qty = dr.Cells["GTxtQty"].Value.GetDecimal(true),
        //                    Unit_Id =
        //                            dr.Cells["GTxtUOMId"].Value.GetInt() > 0
        //                                ? dr.Cells["GTxtUOMId"].Value.GetInt()
        //                                : null,
        //                    Rate = dr.Cells["GTxtRate"].Value.GetDecimal(),
        //                    B_Amount = dr.Cells["GTxtAmount"].Value.GetDecimal(),
        //                    T_Amount = dr.Cells["GTxtTermAmount"].Value.GetDecimal(),
        //                    N_Amount = dr.Cells["GTxtNetAmount"].Value.GetDecimal(),
        //                    AltStock_Qty = dr.Cells["GTxtAltStockQty"].Value.GetDecimal(),
        //                    Stock_Qty = dr.Cells["GTxtStockQty"].Value.GetDecimal(),
        //                    Narration =
        //                            dr.Cells["GTxtNarration"].Value.IsValueExits()
        //                                ? dr.Cells["GTxtNarration"].Value.GetTrimReplace()
        //                                : null,
        //                    SB_Invoice =
        //                            dr.Cells["GTxtInvoiceNo"].Value.IsValueExits()
        //                                ? dr.Cells["GTxtInvoiceNo"].Value.GetTrimReplace()
        //                                : null,
        //                    SB_Sno = dr.Cells["GTxtInvoiceSno"].Value.GetInt(),
        //                    Tax_Amount = 0,
        //                    V_Amount = 0,
        //                    V_Rate = 0,
        //                    Free_Unit_Id = null,
        //                    Free_Qty = 0,
        //                    StockFree_Qty = 0,
        //                    ExtraFree_Unit_Id = null,
        //                    ExtraFree_Qty = 0,
        //                    ExtraStockFree_Qty = 0,
        //                    T_Product = false,
        //                    S_Ledger =
        //                            dr.Cells["GTxtSBLedgerId"].Value.GetLong() > 0
        //                                ? dr.Cells["GTxtSBLedgerId"].Value.GetLong()
        //                                : null,
        //                    SR_Ledger =
        //                            dr.Cells["GTxtSRLedgerId"].Value.GetLong() > 0
        //                                ? dr.Cells["GTxtSRLedgerId"].Value.GetLong()
        //                                : null,
        //                    SZ1 = null,
        //                    SZ2 = null,
        //                    SZ3 = null,
        //                    SZ4 = null,
        //                    SZ5 = null,
        //                    SZ6 = null,
        //                    SZ7 = null,
        //                    SZ8 = null,
        //                    SZ9 = null,
        //                    SZ10 = null,
        //                    Serial_No =
        //                            dr.Cells["GTxtSerialNo"].Value.IsValueExits()
        //                                ? dr.Cells["GTxtSerialNo"].Value.GetTrimReplace()
        //                                : null,
        //                    Batch_No =
        //                            dr.Cells["GTxtBatchNo"].Value.IsValueExits()
        //                                ? dr.Cells["GTxtBatchNo"].Value.GetTrimReplace()
        //                                : null,
        //                    Exp_Date =
        //                            dr.Cells["GTxtExpiryDate"].Value.IsValueExits()
        //                                ? Convert.ToDateTime(dr.Cells["GTxtExpiryDate"].Value.GetTrimReplace())
        //                                : null,
        //                    Manu_Date = dr.Cells["GTxtMfgDate"].Value.IsValueExits()
        //                            ? Convert.ToDateTime(dr.Cells["GTxtMfgDate"].Value.GetTrimReplace())
        //                            : null,
        //                    PDiscountRate = 0,
        //                    PDiscount = 0,
        //                    BDiscountRate = 0,
        //                    BDiscount = 0,
        //                    ServiceChargeRate = 0,
        //                    ServiceCharge = 0,
        //                    SyncRowVersion = SrMaster.SyncRowVersion
        //                }));
        //            }
        //        }
        //    }

        //    if (isPosReturn)
        //    {
        //        for (var i = 0; i < SrMaster.GetView.Rows.Count; i++)
        //        {
        //            var val = SrMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //            if (val <= 0)
        //            {
        //                continue;
        //            }

        //            var srt = new SR_Term
        //            {
        //                SR_VNo = SrMaster.SR_Invoice,
        //                ST_Id = ObjGlobal.SalesDiscountTermId,
        //                SNo = SrMaster.GetView.Rows[i].Cells["GTxtSNo"].Value.GetInt(),
        //                Term_Type = "P",
        //                Product_Id = SrMaster.GetView.Rows[i].Cells["GTxtProductId"].Value.GetLong(),
        //                Rate = SrMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal(),
        //                Amount = SrMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal(),
        //                Taxable = "N",
        //                SyncRowVersion = SrMaster.SyncRowVersion
        //            };
        //            srTerms.Add(srt);
        //        }

        //        for (var i = 0; i < SrMaster.GetView.Rows.Count; i++)
        //        {
        //            var val = SrMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //            if (val is <= 0)
        //            {
        //                continue;
        //            }

        //            var srt = new SR_Term
        //            {
        //                SR_VNo = SrMaster.SR_Invoice,
        //                ST_Id = ObjGlobal.SalesVatTermId,
        //                SNo = SrMaster.GetView.Rows[i].Cells["GTxtSNo"].Value.GetInt(),
        //                Term_Type = "P",
        //                Product_Id = SrMaster.GetView.Rows[i].Cells["GTxtProductId"].Value.GetLong(),
        //                Rate = SrMaster.GetView.Rows[i].Cells["GTxtTaxPriceRate"].Value.GetDecimal(),
        //                Amount = SrMaster.GetView.Rows[i].Cells["GTxtValueVatAmount"].Value.GetDecimal(),
        //                Taxable = SrMaster.GetView.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool() is true ? "Y" : "N",
        //                SyncRowVersion = SrMaster.SyncRowVersion
        //            };
        //            srTerms.Add(srt);
        //        }

        //        if (SrMaster.T_Amount > 0)
        //        {
        //            var srt = new SR_Term
        //            {
        //                SR_VNo = SrMaster.SR_Invoice,
        //                ST_Id = ObjGlobal.SalesSpecialDiscountTermId,
        //                SNo = 1,
        //                Term_Type = "B",
        //                Product_Id = null,
        //                Rate = SrMaster.TermRate.GetDecimal(),
        //                Amount = SrMaster.T_Amount.GetDecimal(),
        //                Taxable = "N",
        //                SyncRowVersion = SrMaster.SyncRowVersion
        //            };
        //            srTerms.Add(srt);
        //        }
        //    }
        //    else
        //    {
        //        if (SrMaster.dtPTerm != null && SrMaster.dtPTerm.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in SrMaster.dtPTerm.Rows)
        //            {
        //                if (dr["TermAmt"].GetDecimal() == 0)
        //                {
        //                    continue;
        //                }

        //                var srt = new SR_Term
        //                {
        //                    SR_VNo = SrMaster.SR_Invoice,
        //                    ST_Id = dr["TermId"].GetInt(),
        //                    SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
        //                    Term_Type = "P",
        //                    Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
        //                    Rate = dr["TermRate"].GetDecimal(),
        //                    Amount = dr["TermAmt"].GetDecimal(),
        //                    Taxable = dr["TermRate"].GetDecimal() > 0 && dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId) ? "Y" : "N",
        //                    SyncRowVersion = SrMaster.SyncRowVersion
        //                };
        //                srTerms.Add(srt);
        //            }
        //        }

        //        if (SrMaster.dtBTerm != null && SrMaster.dtBTerm.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in SrMaster.dtBTerm.Rows)
        //            {
        //                if (dr["TermAmt"].GetDecimal() == 0)
        //                {
        //                    continue;
        //                }

        //                var srt = new SR_Term
        //                {
        //                    SR_VNo = SrMaster.SR_Invoice,
        //                    ST_Id = dr["TermId"].GetInt(),
        //                    SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
        //                    Term_Type = "B",
        //                    Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
        //                    Rate = dr["TermRate"].GetDecimal(),
        //                    Amount = dr["TermAmt"].GetDecimal(),
        //                    Taxable = dr["TermRate"].GetDecimal() > 0 && dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId) ? "Y" : "N",
        //                    SyncRowVersion = SrMaster.SyncRowVersion
        //                };
        //                srTerms.Add(srt);
        //            }
        //        }
        //    }

        //    sr.DetailsList = srDetails;
        //    sr.Terms = srTerms;

        //    var result = actionTag.ToUpper() switch
        //    {
        //        "SAVE" => await salesReturnRepo?.PushNewAsync(sr),
        //        "UPDATE" => await salesReturnRepo?.PutNewAsync(sr),
        //        //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
        //        //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
        //        _ => await salesReturnRepo?.PushNewAsync(sr)
        //    };
        //    if (result.Value)
        //    {
        //        CreateDatabaseTable.DropTrigger();
        //        var cmdString = new StringBuilder();
        //        cmdString.Append($@"
        //        UPDATE AMS.SR_Master SET IsSynced = 1 WHERE SR_Invoice = '{SrMaster.SR_Invoice}' ;\n");
        //        SaveDataInDatabase(cmdString);
        //        CreateDatabaseTable.CreateTrigger();
        //    }

        //    return 1;
        //}
        //catch (Exception ex)
        //{
        //    CreateDatabaseTable.CreateTrigger();
        //    return 1;
        //}

        return 0;
    }

    public int SaveSalesOtherDetails(string module, string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "SAVE")
        {
            cmdString.Append($"INSERT INTO AMS.{module}_Master_OtherDetails({module}_Invoice,");
            cmdString.Append(" Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails) \n");
            cmdString.Append(" 	VALUES (  \n");
            cmdString.Append($" N'{SbOther.SB_Invoice}', N'{SbOther.Transport}', N'{SbOther.VechileNo}', N'{SbOther.BiltyNo}', N'{SbOther.Package}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
                ? $" '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append(
                $"N'{SbOther.BiltyType}', N'{SbOther.Driver}', N'{SbOther.PhoneNo}', N'{SbOther.LicenseNo}', N'{SbOther.MailingAddress}', N'{SbOther.MCity}', N'{SbOther.MState}', N'{SbOther.MCountry}', N'{SbOther.MEmail}', N'{SbOther.ShippingAddress}', N'{SbOther.SCity}', N'{SbOther.SState}', N'{SbOther.SCountry}', N'{SbOther.SEmail}', N'{SbOther.ContractNo}',");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
                ? $" '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append($"N'{SbOther.ExportInvoice}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
                ? $" '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append($"N'{SbOther.VendorOrderNo}', N'{SbOther.BankDetails}', N'{SbOther.LcNumber}', N'{SbOther.CustomDetails}'); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append($" UPDATE AMS.{module}_Master_OtherDetails SET ");
            cmdString.Append($" Transport= N'{SbOther.Transport}', VechileNo= N'{SbOther.VechileNo}', BiltyNo = N'{SbOther.BiltyNo}', Package = N'{SbOther.Package}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
                ? $"BiltyDate=  '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "BiltyDate= Null,");
            cmdString.Append($" BiltyType= N'{SbOther.BiltyType}',Driver=  N'{SbOther.Driver}', PhoneNo= N'{SbOther.PhoneNo}',LicenseNo= N'{SbOther.LicenseNo}',MailingAddress=  N'{SbOther.MailingAddress}',MCity=  N'{SbOther.MCity}',MState=  N'{SbOther.MState}',MCountry=  N'{SbOther.MCountry}',MEmail=  N'{SbOther.MEmail}',ShippingAddress=  N'{SbOther.ShippingAddress}', SCity= N'{SbOther.SCity}', SState= N'{SbOther.SState}', SCountry= N'{SbOther.SCountry}',SEmail= N'{SbOther.SEmail}', ContractNo= N'{SbOther.ContractNo}',");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
                ? $"ContractDate=  '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "ContractDate= Null,");
            cmdString.Append($"ExportInvoice= N'{SbOther.ExportInvoice}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
                ? $"ExportInvoiceDate=  '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "ExportInvoiceDate= Null,");
            cmdString.Append($" VendorOrderNo= N'{SbOther.VendorOrderNo}',BankDetails= N'{SbOther.BankDetails}',LcNumber= N'{SbOther.LcNumber}', CustomDetails= N'{SbOther.CustomDetails}' WHERE {module}_Invoice = N'{SbOther.SB_Invoice}' ; ");
        }
        else if (actionTag is "DELETE")
        {
            cmdString.Append($" DELETE AMS.{module}_Master_OtherDetails WHERE {module}_Invoice = '{SbOther.SB_Invoice}'; ");
        }

        if (cmdString.Length is 0)
        {
            return 0;
        }

        var saveData = SaveDataInDatabase(cmdString);
        return saveData;
    }

    public int SaveNightAuditLog(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "SAVE")
        {
            cmdString.Append($@"
            INSERT INTO AMS.NightAuditLog(AuditDate, IsAudited, AuditUser, AuditedDate,BranchId,CompanyUnitId)
            VALUES('{AuditLog.AuditDate.GetSystemDate()}',CAST('{AuditLog.IsAudited}' AS BIT),NULL,NULL,'{AuditLog.BranchId}',");
            cmdString.Append(AuditLog.CompanyUnitId > 0 ? $"'{AuditLog.CompanyUnitId}');" : "NULL);");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append($@" 
            UPDATE AMS.NightAuditLog SET IsAudited = 1,AuditUser='{AuditLog.AuditUser}',AuditedDate ='{AuditLog.AuditedDate.GetSystemDate()}' WHERE LogId = {AuditLog.LogId}");
        }

        var result = ExecuteValueInDatabase(cmdString.ToString());
        return result;
    }

    #endregion --------------- IUD SALES  ---------------

    // SALES TERM POSTING

    #region ---------- TERM_POSTING ----------

    private int QuotationTermPosting()
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

    public int SalesOrderTermPosting()
    {
        var cmdString = $@"
		DELETE AMS.SO_Term WHERE SO_Vno='{SoMaster.SO_Invoice}' AND Term_Type='BT';
		INSERT INTO AMS.SO_Term(SO_Vno, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
        SELECT sbt.SO_Vno, sbt.ST_Id, sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
		FROM AMS.SO_Details sd
			 LEFT OUTER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
			 LEFT OUTER JOIN AMS.SO_Term sbt ON sd.SO_Invoice=sbt.SO_Vno
		WHERE sbt.SO_Vno='{SoMaster.SO_Invoice}' AND sbt.Term_Type='B' AND Product_Id IS NULL;";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int SalesChallanTermPosting()
    {
        var cmdString = $@"
		DELETE AMS.SC_Term WHERE SC_VNo='{ScMaster.SC_Invoice}' AND Term_Type='BT';
		INSERT INTO AMS.SC_Term(SC_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		SELECT sbt.SC_VNo, ST_Id, sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
		FROM AMS.SC_Details sd
			 LEFT OUTER JOIN AMS.SC_Master sm ON sm.SC_Invoice=sd.SC_Invoice
			 LEFT OUTER JOIN AMS.SC_Term sbt ON sd.SC_Invoice=sbt.SC_VNo
		WHERE sbt.SC_VNo='{ScMaster.SC_Invoice}' AND sbt.Term_Type='B' AND Product_Id IS NULL;";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int SalesTermPostingAsync()
    {
        var cmdString = @"
		    DELETE AMS.SB_Term WHERE Term_Type='BT' ";
        if (SbMaster.SB_Invoice.IsValueExits())
        {
            cmdString += $" AND SB_VNo='{SbMaster.SB_Invoice}' ";
        }

        cmdString += $@"
            INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT sbt.SB_VNo SB_VNo, sbt.ST_Id ST_Id, sd.Invoice_SNo AS SNo, 'BT' Term_Type, sd.P_Id Product_Id,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()}  THEN 0 ELSE sbt.Rate END Rate,
            CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()}  THEN 0 ELSE (sbt.Amount / sm.B_Amount)* sd.N_Amount END  Amount, CASE WHEN sd.T_Product=1 THEN 'Y' ELSE 'N' END Taxable, sbt.SyncBaseId, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion
            FROM AMS.SB_Details sd
	            LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
	            LEFT OUTER JOIN AMS.SB_Term sbt ON sd.SB_Invoice=sbt.SB_VNo
	            LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
            WHERE sbt.Term_Type='B' AND Product_Id IS NULL ";
        if (SbMaster.SB_Invoice.IsValueExits())
        {
            cmdString += $" AND sbt.SB_VNo='{SbMaster.SB_Invoice}' ";
        }

        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public int SalesReturnTermPosting()
    {
        var cmdString = @"
		DELETE AMS.SR_Term WHERE Term_Type='BT' ";
        cmdString += SrMaster.SR_Invoice.IsValueExits() ? $@" AND SR_VNo ='{SrMaster.SR_Invoice}'; " : " ";
        cmdString += $@"
		INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		SELECT sbt.SR_VNo, sbt.ST_Id, sd.Invoice_SNo AS SERIAL_NO,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()} THEN 0 ELSE sbt.Rate END Rate,
        CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()} THEN 0 ELSE (sbt.Amount/sm.B_Amount) * sd.N_Amount END Amount, 'BT' Term_Type, sd.P_Id Product_Id, sbt.Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
		FROM AMS.SR_Details sd
			 LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
			 LEFT OUTER JOIN AMS.SR_Term sbt ON sd.SR_Invoice=sbt.SR_VNo
             LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
		WHERE sbt.Term_Type='B' AND Product_Id IS NULL ";
        cmdString += SrMaster.SR_Invoice.IsValueExits() ? $@" AND sbt.SR_VNo='{SrMaster.SR_Invoice}' " : "";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    #endregion ---------- TERM_POSTING ----------

    // METHOD FOR THIS CLASS

    #region --------------- IUD FUNCTION ---------------
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
    public int UpdateImageOnSales(string module)
    {
        var isUpdate = 0;
        var cmdString = new StringBuilder();


        try
        {
            using SqlConnection conn = new SqlConnection(GetConnection.ConnectionString);
            conn.Open();
            if (SbMaster.PAttachment1 != null && SbMaster.PAttachment1.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment1 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment1 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment2 != null && SbMaster.PAttachment2.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment2 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment2 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment3 != null && SbMaster.PAttachment3.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment3 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment3 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment4 != null && SbMaster.PAttachment4.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment4 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment4 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment5 != null && SbMaster.PAttachment5.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment5 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment5 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
        return isUpdate;
    }
    public int UpdateOrderChallanOnChallan()
    {
        var cmdString = new StringBuilder();
        if (ScMaster.QOT_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
            UPDATE AMS.SQ_Master SET Invoice_Type ='POSTED' WHERE SQ_Invoice= '{ScMaster.QOT_Invoice}';");
        }

        if (ScMaster.SO_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
            UPDATE AMS.SO_Master SET Invoice_Type ='POSTED' WHERE SO_Invoice = '{ScMaster.SO_Invoice}'; \n");
        }

        var result = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return result;
    }
    public int UpdateInvoiceTagOnSalesReturn()
    {
        var cmdString = $@" 
        UPDATE AMS.SB_Master SET Action_Type ='RETURN' WHERE SB_Invoice='{SrMaster.SB_Invoice}';";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public int UpdateDocumentNumbering(string module, string documentDesc)
    {
        var cmdString = $@"
        UPDATE AMS.DocumentNumbering SET DocCurr = isNull(DocCurr, 0) + 1  WHERE DocModule = '{module}' AND  DocDesc = '{documentDesc}';";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public void DeleteRestroOrder(string orderNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@" 
        DELETE from [AMS].[SO_Details] Where SO_Invoice = '{orderNo}';");
        var database = SaveDataInDatabase(cmdString);
    }
    public void UpdateReverseBill(string voucherNo, string user, string remarks)
    {
        var cmdString =
            $@"UPDATE AMS.SB_Master SET R_Invoice = 1,Cancel_Date = GETDATE(),Cancel_By = '{user}', Cancel_Remarks = '{remarks}', Action_Type='Cancel' WHERE SB_Invoice =  '{voucherNo}' ";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public void PostingSalesReturnToApi(string voucherNo)
    {
        _ = Task.Run(() => _apiSync.SyncSalesReturnApi(voucherNo));
    }
    public void PostingBillToApi(string voucherNo)
    {
        _ = Task.Run(() => { return _apiSync.SyncSalesBillApiAsync(voucherNo); });
    }
    public void UpdatePostSalesBillToApi(string voucherNo, int isRealtime)
    {
        CreateDatabaseTable.DropTrigger();
        var cmdString = new StringBuilder();
        cmdString.Append($@"
        UPDATE AMS.SB_Master set IsAPIPosted=1, IsRealtime={isRealtime} where SB_Invoice = '{voucherNo}'");
        var iResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        CreateDatabaseTable.CreateTrigger();
    }
    public void UpdatePostSalesCancelToApi(string voucherNo, int isRealtime)
    {
        var cmdString = $@"
        UPDATE AMS.SB_Master set IsAPIPosted= 1, IsRealtime={isRealtime} where SBC_Invoice='{voucherNo}'; ";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public bool UpdatePrintSalesReturnStatus(string voucherNo, string printedBy, DateTime printedDate)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
        UPDATE AMS.SR_Master set No_Print=No_Print+1,Printed_By='{printedBy}' , Printed_Date='{printedDate:MM/dd/yyyy HH:mm:ss}' where SR_Invoice='{voucherNo}' \n");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString()) > 0;
    }
    public static void UpdatePostSalesReturnToApi(string voucherNo, int isRealtime)
    {
        var cmdString = $@"
        UPDATE AMS.SR_Master set IsAPIPosted=1,IsRealtime={isRealtime} Where SR_Invoice = '{voucherNo}'; ";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public (string invoice, string avtInvoice) GetPointOfSalesDesign()
    {
        var cmdString = @" 
            SELECT TOP (1) Paper_Name FROM AMS.DocumentDesignPrint WHERE Module = 'SB'";
        var invoiceDesign = cmdString.GetQueryData();
        cmdString = @" 
            SELECT TOP (1) Paper_Name FROM AMS.DocumentDesignPrint WHERE Module = 'AVT'";
        var avtDesign = cmdString.GetQueryData();
        return (invoiceDesign, avtDesign);
    }
    public string GetInvoicePaymentMode(long ledgerId)
    {
        var cmdString = $@"
            SELECT TOP 1 COUNT(sm.Payment_Mode) CountNo, sm.Payment_Mode
            FROM AMS.SB_Master sm
            WHERE sm.Customer_Id = '{ledgerId}'
            GROUP BY sm.Payment_Mode
            ORDER BY COUNT(sm.Payment_Mode) DESC ";
        var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (result.Rows.Count > 0)
        {
            return result.Rows[0]["Payment_Mode"].GetString();
        }

        return string.Empty;
    }


    #endregion --------------- IUD FUNCTION ---------------

    // ACCOUNT POSTING FUNCTION

    #region ---------- ACCOUNT_POSTING ----------
    public int SalesReturnAccountPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@" DELETE AMS.AccountDetails WHERE module='SR'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" and Voucher_No='{SrMaster.SR_Invoice}' " : string.Empty);
        cmdString.Append($@"
		INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_Id, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_Id, CmpUnit_Id, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_Id, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_Id, CmpUnit_Id, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
		FROM (SELECT 'SR' Module, 1 Serial_No, sm.SR_Invoice Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_ID, {ObjGlobal.SalesReturnLedgerId} CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, 0 Debit_Amt, sm.N_Amount Credit_Amt, 0 LocalDebit_Amt, sm.LN_Amount LocalCredit_Amt, sm.DueDate DueDate, sm.DueDays DueDays, NULL Narration, sm.Remarks Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, sm.SyncGlobalId SyncGlobalId, sm.SyncOriginId SyncOriginId, sm.SyncCreatedOn SyncCreatedOn, SyncLastPatchedOn, sm.SyncRowVersion SyncRowVersion, sm.SyncBaseId SyncBaseId
				FROM AMS.SR_Master sm
				WHERE sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND SM.SR_Invoice ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
		UNION ALL
		SELECT 'SR' Module, sd.Invoice_SNo Serial_No, sd.SR_Invoice Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, CASE WHEN CAST(ISNULL(p.PSR, 0) AS BIGINT)>0 THEN p.PSL ELSE (SELECT SRLedgerId FROM AMS.SalesSetting) END Ledger_ID, sm.Customer_ID CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, SUM(sd.B_Amount) Debit_Amt, 0 Credit_Amt, SUM(sd.B_Amount)* sm.Cur_Rate LocalDebit_Amt, 0 LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, sd.SyncGlobalId SyncGlobalId, sd.SyncOriginId SyncOriginId, sd.SyncCreatedOn SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion SyncRowVersion, sd.SyncBaseId SyncBaseId
			FROM AMS.SR_Details sd
			INNER JOIN AMS.SR_Master sm ON sd.SR_Invoice = sm.SR_Invoice
			INNER JOIN AMS.Product p ON sd.P_Id = p.PID
			WHERE sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND SD.SR_Invoice ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY sd.SR_Invoice, sd.Invoice_SNo, sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time,p.PSR,sm.Customer_Id, sm.Subledger_Id, sm.Agent_ID, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, Narration, Remarks, sm.Enter_By, sm.Enter_Date, sm.SB_Invoice, sm.SB_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId,sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, p.PSL
		UNION ALL
		SELECT 'SR' Module, st.SNo Serial_No, st.SR_VNo Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, st1.Ledger Ledger_ID, sm.Customer_Id CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) Debit_Amt, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END)  Credit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) *  sm.Cur_Rate LocalDebit_Amt,SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END) * sm.Cur_Rate  LocalCredit_Amt, DueDate, DueDays, NULL Narration, sm.Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, st.SyncGlobalId SyncGlobalId, st.SyncOriginId SyncOriginId, st.SyncCreatedOn SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion SyncRowVersion, st.SyncBaseId SyncBaseId
			FROM AMS.SR_Term st
			LEFT OUTER JOIN AMS.SR_Master sm ON st.SR_VNo = sm.SR_Invoice
			LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
		WHERE st.Term_Type <> 'BT' AND sm.R_Invoice=0  AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND st.SR_VNo ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
		GROUP BY st.SR_VNo, st.SNo, sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_ID, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, st1.ST_Sign, Remarks, sm.Enter_By, sm.Enter_Date, sm.SB_Invoice, sm.SB_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, st.SyncGlobalId, st.SyncOriginId, st.SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion, st.SyncBaseId, st1.Ledger) Account ");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }
    public int SalesInvoiceAccountPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.AccountDetails WHERE Module='SB' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits()
            ? $@" AND Voucher_No ='{SbMaster.SB_Invoice}' "
            : string.Empty);
        cmdString.Append($@"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM ( SELECT 'SB' AS Module, ROW_NUMBER() OVER (PARTITION BY sm.SB_Invoice ORDER BY sm.SB_Invoice) AS Serial_No, sm.SB_Invoice AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time,ISNULL(ss.LedgerId, sm.Customer_Id) AS Ledger_ID, {ObjGlobal.SalesLedgerId} AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_Id AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate,ISNULL(ss.Amount,sm.N_Amount) AS Debit_Amt, 0 AS Credit_Amt,ISNULL(ss.Amount, sm.LN_Amount) AS LocalDebit_Amt, 0 AS LocalCredit_Amt, sm.DueDate AS DueDate, sm.DueDays AS DueDays, NULL AS Narration, sm.Remarks AS Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, sm.SyncGlobalId AS SyncGlobalId, sm.SyncOriginId AS SyncOriginId, sm.SyncCreatedOn AS SyncCreatedOn, SyncLastPatchedOn, sm.SyncRowVersion AS SyncRowVersion, sm.SyncBaseId AS SyncBaseId
				 FROM AMS.SB_Master sm
					LEFT OUTER JOIN AMS.InvoiceSettlement ss ON ss.SB_Invoice = sm.SB_Invoice
				 WHERE (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL')");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits()
            ? $@" AND sm.SB_Invoice ='{SbMaster.SB_Invoice}' "
            : string.Empty);
        cmdString.Append(@"
			UNION ALL
			SELECT 'SB' AS Module,ROW_NUMBER() OVER (PARTITION BY sd.SB_Invoice ORDER BY sd.SB_Invoice) AS Serial_No, sd.SB_Invoice AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time, ISNULL(p.PSL, (SELECT SBLedgerId FROM AMS.SalesSetting)) Ledger_ID, sm.Customer_Id AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_Id AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate, 0 AS Debit_Amt, SUM(sd.B_Amount) AS Credit_Amt, 0 AS LocalDebit_Amt, SUM(sd.B_Amount)* sm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, sd.SyncGlobalId AS SyncGlobalId, sd.SyncOriginId AS SyncOriginId, sd.SyncCreatedOn AS SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion AS SyncRowVersion, sd.SyncBaseId AS SyncBaseId
			 FROM AMS.SB_Details sd
				  INNER JOIN AMS.SB_Master sm ON sd.SB_Invoice=sm.SB_Invoice
				  INNER JOIN AMS.Product p ON sd.P_Id=p.PID
			 WHERE (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL') ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND sd.SB_Invoice ='{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY sd.SB_Invoice,sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_Id, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, Narration, Remarks, sm.Enter_By, sm.Enter_Date, sm.PB_Vno, sm.Vno_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, sm.DoctorId, sm.PatientId, sm.HDepartmentId, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, p.PSL
			UNION ALL
			SELECT 'SB' AS Module,ROW_NUMBER() OVER (PARTITION BY st.SB_VNo ORDER BY st.SB_VNo) AS Serial_No, st.SB_VNo AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time, st1.Ledger AS Ledger_ID, sm.Customer_Id AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_ID AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END) AS Debit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) AS Credit_Amt, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END)* sm.Cur_Rate AS LocalDebit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END)* sm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, NULL AS Narration, sm.Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, st.SyncGlobalId AS SyncGlobalId, st.SyncOriginId AS SyncOriginId, st.SyncCreatedOn AS SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion AS SyncRowVersion, st.SyncBaseId AS SyncBaseId
			FROM AMS.SB_Term st
				LEFT OUTER JOIN AMS.SB_Master sm ON st.SB_VNo=sm.SB_Invoice
				LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id=st1.ST_Id
			 WHERE st.Term_Type IN ('B','P') AND (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL') ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND st.SB_VNo ='{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY st.SB_VNo,sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_Id, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, st1.ST_Sign, Remarks, sm.Enter_By, sm.Enter_Date, sm.PB_Vno, sm.Vno_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, sm.DoctorId, sm.PatientId, sm.HDepartmentId, st.SyncGlobalId, st.SyncOriginId, st.SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion, st.SyncBaseId, st1.Ledger ) Account ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return isResult;
    }
    public int SalesTravelAccountPosting()
    {
        var cmdString = @"
			DELETE FROM AMS.AccountDetails WHERE Module='SBT';
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId)
			SELECT 'SBT' module, 0 AS Sno, SBT_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, Customer_ID AS Ledger_Id, SBM.Customer_ID CbLedger_Id, NULL Subledger_Id, NULL Agent_Id, NULL Cls1, NULL Cls2, NULL Cls3, NULL Cls4, Cur_Id, Cur_Rate, N_Amount AS DrAmt, 0 AS CrAmt, N_Amount * ISNULL(Cur_Rate, 1) AS Local_DrAmt, 0 AS Local_CrAmt, SBM.DueDate, SBM.DueDays, '' AS Naration, Remarks, Enter_By AS UserName, Enter_Date, Ref_VNo RefNo, NULL Reconcile_By, NULL Reconcile_Date, SBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, SBM.ChqNo, SBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, SBM.FiscalYearId
			FROM AMS.SBT_Master AS SBM
			UNION ALL
			SELECT 'SBT' module, Invoice_SNo AS Sno, SBM.SBT_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, ISNULL(PSL, Sales_AC) AS Ledger_Id, SBM.Customer_ID CbLedger_Id, Slb_Id Subledger_Id, NULL Agent_Id, NULL Cls1, NULL Cls2, NULL Cls3, NULL Cls4, SBM.Cur_Id, SBM.Cur_Rate, 0 AS DrAmt, ROUND(SUM(ISNULL(PBD.B_Amount, 0)), 2) AS CrAmt, 0 AS Local_DrAmt, SUM(ISNULL(PBD.B_Amount, 0)* ISNULL(SBM.Cur_Rate, 1)) AS Local_CrAmt, SBM.DueDate, SBM.DueDays, '' AS Naration, SBM.Remarks, SBM.Enter_By AS UserName, Enter_Date, Ref_VNo RefNo, NULL Reconcile_By, NULL Reconcile_Date, SBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, SBM.ChqNo, SBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, SBM.FiscalYearId
			FROM AMS.SBT_Master AS SBM, AMS.SBT_Details AS PBD, AMS.SystemConfiguration AS SC, AMS.Product AS P
			WHERE PBD.P_Id=P.PID AND PBD.SBT_Invoice=SBM.SBT_Invoice
			GROUP BY SBM.SBT_Invoice, PSL, Invoice_Date, Invoice_Time, SBM.Cur_Id, SBM.Cur_Rate, CUnit_Id, CBranch_Id, SBM.DueDate, SBM.DueDays, SBM.Remarks, Sales_AC, SBM.Customer_ID, Slb_Id, Enter_By, Enter_Date, Ref_VNo, Invoice_SNo, Invoice_Miti, SBM.Auth_By, Auth_Date, SBM.ChqNo, SBM.ChqDate, Party_Name, Vat_No, SBM.FiscalYearId
			UNION ALL
			SELECT 'SBT' module, 0 AS Sno, SBT.SBT_VNo AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, STM.Ledger Ledger_Id, SBM.Customer_ID CbLedger_Id, NULL Subledger_Id, NULL Agent_Id, NULL Cls1, NULL Cls2, NULL Cls3, NULL Cls4, SBM.Cur_Id, SBM.Cur_Rate, CASE WHEN ST_Sign='-' THEN (SBT.Amount)ELSE 0 END AS DrAmt, CASE WHEN ST_Sign='+' THEN (SBT.Amount)ELSE 0 END AS CrAmt, CASE WHEN ST_Sign='-' THEN ISNULL(SBT.Amount, 0)* ISNULL(SBM.Cur_Rate, 1)ELSE 0 END AS Local_DrAmt, CASE WHEN ST_Sign='+' THEN ISNULL(SBT.Amount, 0)* ISNULL(SBM.Cur_Rate, 1)ELSE 0 END AS Local_CrAmt, SBM.DueDate, SBM.DueDays, '' AS Narration, Remarks, Enter_By, Enter_Date, SBM.Ref_VNo RefNo, NULL Reconcile_By, NULL Reconcile_Date, SBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, SBM.ChqNo, SBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, SBM.FiscalYearId
			FROM AMS.ST_Term AS STM, AMS.SBT_Term AS SBT, AMS.SBT_Master AS SBM
			WHERE STM.ST_Id=SBT.ST_Id AND SBM.SBT_Invoice=SBT.SBT_VNo AND Term_Type<>'BT' AND Term_Type<>'' AND CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END<>0; ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }

    #endregion ---------- ACCOUNT_POSTING ----------

    // STOCK POSTING FUNCTION

    #region ---------- STOCK_POSTING ----------

    public int SalesChallanStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.StockDetails WHERE Module = 'SC' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? $@" AND Voucher_No = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SC' Module, sd.SC_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.Ref_Vno PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.Ref_Vno PartyInv, 'O' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SC_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SC_Master sm ON sd.SC_Invoice = sm.SC_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SC_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SC_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0
					AND st.Term_Type <> 'B' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? @$" AND st.SC_VNo = '{ScMaster.SC_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SC_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SC_Invoice = stockval.SC_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory') AND sm.R_Invoice = 0 AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? @$" AND sd.SC_Invoice = '{ScMaster.SC_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return isResult;
    }

    public int SalesInvoiceStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(" DELETE AMS.StockDetails WHERE module='SB' \n");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $" AND Voucher_No='{SbMaster.SB_Invoice}'; \n" : " ");
        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
            ? $@" DELETE AMS.StockDetails WHERE module ='SC' AND Voucher_No='{SbMaster.SC_Invoice}';"
            : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SB' Module, sd.SB_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.PB_Vno PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.PB_Vno PartyInv, 'O' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SB_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SB_Master sm ON sd.SB_Invoice = sm.SB_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SB_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SB_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0
					AND st.Term_Type <> 'B' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND st.SB_VNo = '{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SB_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SB_Invoice = stockval.SB_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory')  AND sm.R_Invoice = 0 AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? @$" AND sd.SB_Invoice = '{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (isResult == 0)
        {
            return isResult;
        }

        if (SbMaster.SC_Invoice.IsValueExits())
        {
            var update =
                "UPDATE AMS.StockDetails SET Adj_VoucherNo = SD.SC_Invoice, Adj_Qty = sd.Qty, Adj_Module = 'SC' FROM AMS.SB_Details sd WHERE Voucher_No = sd.SB_Invoice AND Product_Id = sd.P_Id ";
            update += SbMaster.SB_Invoice.IsValueExits() ? $" AND Voucher_No='{SbMaster.SB_Invoice}';" : " ";
            var ok = SqlExtensions.ExecuteNonQuery(update);
        }

        return isResult;
    }

    public int SalesReturnStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.StockDetails WHERE Module = 'SR' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND Voucher_No = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SR' Module, sd.SR_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.SB_Invoice PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.SB_Invoice PartyInv, 'I' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SR_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SR_Master sm ON sd.SR_Invoice = sm.SR_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SR_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SR_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0 AND st.Term_Type <> 'B'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND st.SR_VNo = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SR_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SR_Invoice = stockval.SR_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory') AND sm.R_Invoice=0  AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND sd.SR_Invoice = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }

    #endregion ---------- STOCK_POSTING ----------

    // RETURN VALUE IN DATA TABLE

    #region --------------- RETURN DATA TABLE  ---------------

    public DataTable GetTableOrderDetails(int tableId)
    {
        var cmdString = $@"
            SELECT SD.Invoice_SNo ,PD.PName ,Cast(SD.Qty AS DECIMAL( 18,2)) Qty,CAST(SD.Rate AS DECIMAL(18,2)) Rate,CAST(SD.B_Amount AS DECIMAL(18,2)) B_Amount,CAST(SD.T_Amount AS DECIMAL(18,2)) T_Amount,CAST(SD.N_Amount AS DECIMAL(18,2)) N_Amount
            FROM AMS.SO_Details SD
	            INNER JOIN AMS.SO_Master SM ON SM.SO_Invoice = SD.SO_Invoice
                LEFT JOIN AMS.Product PD ON PD.PID=SD.P_Id
	        WHERE SM.TableId = '{tableId}' and SM.Invoice_Type ='ORDER' AND ISNULL(SM.R_Invoice,0) =0
            ORDER BY SD.Invoice_SNo;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetOrderData(string orderNo)
    {
        var cmdString =
            $@"SELECT SD.*,PD.PName FROM AMS.SO_Details  SD left join Ams.Product PD on PD.PId=SD.P_ID WHERE SO_Invoice ='{orderNo}'  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetTodaySales()
    {
        var cmdString =
            @"Select isnull(sum(N_Amount),0) as TodaySales from ams.SB_Master where Invoice_Date = (SELECT CONVERT(VARCHAR(11), getDate(), 100)) and Action_Type<>'Cancel' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesData()
    {
        var cmdString =
            @"SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GLName,N_Amount FROM AMS.SB_Master as PIM Inner Join AMS.GeneralLedger as GL On GL.GlId=PIM.Customer_Id  Where Invoice_Mode='POS' and R_Invoice=0 Order By SB_Invoice,Invoice_Date";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesDataReportPaymentType(string date)
    {
        var cmdString = $@"
            SELECT Payment_Mode InvoiceMode,N_Amount NetAmount
            FROM AMS.SB_Master
            WHERE Invoice_Date='{date.GetSystemDate()}';";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetConsumptionProduct(DateTime fromDate, DateTime toDate)
    {
        var cmdString = $@"
			SELECT* FROM
			(
				Select 'false' as [Tag],[SD].Invoice_SNo as SNo,SM.SB_Invoice as [Voucher No],CONVERT(VARCHAR(10), Invoice_Date, 103) as [Voucher Date],Invoice_Miti as [Voucher Miti],PName as [Product],
				round(Convert(varchar,[SD].Alt_Qty), 2) as [Alt Qty], APU.UnitCode as [Alt Unit], round(Convert(varchar,[SD].Qty), 2) as Qty, pu.UnitCode as [Unit],[SD].P_Id,[SD].Unit_Id,[SD].Alt_UnitId, [SD].Gdn_Id, BOM.CostCenterId,'SB' AS Source
				from Ams.SB_Master as SM
				left join Ams.SB_Details as [SD] on SM.SB_Invoice =[SD].SB_Invoice
				left join Ams.Product as PD on[SD].P_Id = PD.PId
				left join Ams.ProductUnit as APU on APU.UID = [SD].Alt_UnitId
				left join Ams.ProductUnit as PU on PU.UID = [SD].Unit_Id
				left join [AMS].[BillOfMaterial_Master] as BOM on [SD].P_Id = BOM.FGProductId
				where Invoice_Date between '{Convert.ToDateTime(fromDate):yyyy-MM-dd}' and '{Convert.ToDateTime(toDate):yyyy-MM-dd}' and
				([SD].MaterialPost <> 'Y' or [SD].MaterialPost is null) and
				[SD].P_Id in (Select FGProductId from [AMS].[BillOfMaterial_Master])
			)
			order by[Voucher Date],[Voucher No],Sno; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckTableOrderExitsOrNot(int tableId)
    {
        var cmdString =
            $@"SELECT SO_Invoice FROM AMS.SO_Master WHERE TableId = {tableId} AND Invoice_Type ='ORDER' AND (R_Invoice = 0 OR R_Invoice IS NULL);";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckVoucherNoExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = $@" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckPrintDocument(string module)
    {
        var cmdString = $@" Select* from AMS.DocumentDesignPrint Where Is_Online = 1 and module = '{module}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDataForPostSalesReturnToApi()
    {
        const string cmdString =
            @"Select SR_Invoice,convert(varchar(10), Invoice_Date ,103) as Invoice_Date,Invoice_Miti,N_Amount from AMS.SR_Master where IsAPIPosted is null Order by Invoice_Date,SR_Invoice; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDataSalesCancelBillToApi()
    {
        const string cmdString = @"SELECT * from AMS.MaterializedViewSalesCancel; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDataForPostSalesCancelToApi()
    {
        const string cmdString =
            @"Select SBC_Invoice,convert(varchar(10), Invoice_Date ,103) as Invoice_Date,Invoice_Miti,N_Amount from AMS.SBC_Master where IsAPIPosted is null order by Invoice_Date,SBC_Invoice; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDataSalesReturnToApi()
    {
        var cmdString = @$"
			SELECT RIGHT(sm.Invoice_Miti, 4)+'/'+CAST((CAST(RIGHT(RIGHT(sm.Invoice_Miti, 4), 2) AS NUMERIC)+1) AS NVARCHAR) Fiscal_Year, sm.SR_Invoice Bill_No,sm.SB_Invoice RefNo, gl.PanNo Customer_PAN, gl.GLName Customer_Name, sm.Invoice_Miti Bill_Miti, sm.LN_Amount Amount, sm.Tbl_Amount Taxable_Amount, sm.V_Amount Tax_Amount
			FROM AMS.SR_Master sm
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_ID
			WHERE sm.SR_Invoice='{SrMaster.SR_Invoice}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable IsNightAuditExits(string date)
    {
        var cmdString = $"SELECT * FROM AMS.NightAuditLog WHERE AuditDate = '{date.GetSystemDate()}';";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnLastNightAuditLog()
    {
        var cmdString = $@"
            SELECT TOP(1)LogId, AuditDate, IsAudited, AuditUser, AuditedDate
            FROM AMS.NightAuditLog
            WHERE AuditDate<GETDATE() AND AuditDate > '{ObjGlobal.CfStartAdDate.GetSystemDate()}'  AND IsAudited = 0
            ORDER BY LogId DESC;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- RETURN DATA TABLE  ---------------

    // RETURN VALUE IN DATA SET

    #region --------------- RETURN DATASET  ---------------

    private DataSet ReturnSalesInvoiceForApi()
    {
        var cmdString = @$"
			SELECT right(sm.Invoice_Miti,4) + '/'+  CAST( (CAST(RIGHT(RIGHT(sm.Invoice_Miti,4),2) AS NUMERIC)+ 1)  AS NVARCHAR) Fiscal_Year,sm.SB_Invoice Bill_No, gl.PanNo Customer_PAN,gl.GLName Customer_Name,sm.Invoice_Miti Bill_Miti,
			sm.LN_Amount TotalSales,sm.Tbl_Amount Taxable_Amount,sm.V_Amount Tax_Amount  FROM ams.SB_Master sm
			LEFT OUTER JOIN ams.GeneralLedger gl on gl.GLID = sm.Customer_ID
			WHERE sm.SB_Invoice='{SbMaster.SB_Invoice}' ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnSalesQuotationDetailsInDataSet(string voucherNo)
    {
        throw new NotImplementedException();
    }

    public DataSet ReturnPrintKotDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) ='{voucherNo}';
			SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.CCode, C.CId, D.DName,tm.TableName,tm.TableCode, PIM.*
			FROM AMS.SO_Master AS PIM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Customer_Id
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_Id
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
				 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId = PIM.TableId
			WHERE PIM.SO_Invoice=@voucherNo;
			SELECT sd.SO_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName,pg.GrpName,pg.GrpCode,pg.Gprinter,psg.SubGrpName,psg.ShortName SubGroupCode, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration,sd.QOT_Invoice,sd.QOT_SNo, sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.PDiscountRate, sd.PDiscount, sd.BDiscountRate, sd.BDiscount, sd.ServiceChargeRate, sd.ServiceCharge, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax,sd.OrderTime,sd.PrintedItem,sd.PrintKOT
			FROM AMS.SO_Details sd
				 INNER JOIN AMS.Product AS P ON P.PID=sd.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=sd.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
				 LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = P.PSubGrpId
			WHERE sd.SO_Invoice=@voucherNo and ISNULL(sd.Is_Canceled,0) = 0 and ISNULL(sd.PrintKOT,0) = 0 ;
			SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition, PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			FROM AMS.SO_Term AS PIT
				 INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID=PIT.ST_Id
			WHERE PIT.SO_VNo=@voucherNo AND PIT.Term_Type='P'
			ORDER BY PBT.Order_No ASC;
			SELECT PIT.SNo,PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition, PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			FROM AMS.SO_Term AS PIT
				 INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID=PIT.ST_Id
			WHERE PIT.SO_VNo=@voucherNo AND PIT.Term_Type='B'
			ORDER BY PBT.Order_No ASC;
			SELECT smod.SO_Invoice, smod.Transport, smod.VechileNo, smod.BiltyNo, smod.Package, smod.BiltyDate, smod.BiltyType, smod.Driver, smod.PhoneNo, smod.LicenseNo, smod.MailingAddress, smod.MCity, smod.MState, smod.MCountry, smod.MEmail, smod.ShippingAddress, smod.SCity, smod.SState, smod.SCountry, smod.SEmail, smod.ContractNo, smod.ContractDate, smod.ExportInvoice, smod.ExportInvoiceDate, smod.VendorOrderNo, smod.BankDetails, smod.LcNumber, smod.CustomDetails
			FROM AMS.SO_Master_OtherDetails smod
			WHERE smod.SO_Invoice=@voucherNo;";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnSalesOrderDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) ='{voucherNo}';
			SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.CCode, C.CId, D.DName,tm.TableName,tm.TableCode, PIM.*
			FROM AMS.SO_Master AS PIM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Customer_Id
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_Id
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
				 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId = PIM.TableId
			WHERE PIM.SO_Invoice=@voucherNo;

			SELECT sd.SO_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName,pg.GrpName,pg.GrpCode,pg.Gprinter,psg.SubGrpName,psg.ShortName SubGroupCode, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration,sd.QOT_Invoice,sd.QOT_SNo, sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.PDiscountRate, sd.PDiscount, sd.BDiscountRate, sd.BDiscount, sd.ServiceChargeRate, sd.ServiceCharge, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax,sd.OrderTime,sd.PrintedItem,sd.PrintKOT
			FROM AMS.SO_Details sd
				 INNER JOIN AMS.Product AS P ON P.PID=sd.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=sd.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
				 LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = P.PSubGrpId
			WHERE sd.SO_Invoice=@voucherNo;

			SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition, PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			FROM AMS.SO_Term AS PIT
				 INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID=PIT.ST_Id
			WHERE PIT.SO_VNo=@voucherNo AND PIT.Term_Type='P'
			ORDER BY PBT.Order_No ASC;

			SELECT PIT.SNo,PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition, PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			FROM AMS.SO_Term AS PIT
				 INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID=PIT.ST_Id
			WHERE PIT.SO_VNo=@voucherNo AND PIT.Term_Type='B'
			ORDER BY PBT.Order_No ASC;

			SELECT smod.SO_Invoice, smod.Transport, smod.VechileNo, smod.BiltyNo, smod.Package, smod.BiltyDate, smod.BiltyType, smod.Driver, smod.PhoneNo, smod.LicenseNo, smod.MailingAddress, smod.MCity, smod.MState, smod.MCountry, smod.MEmail, smod.ShippingAddress, smod.SCity, smod.SState, smod.SCountry, smod.SEmail, smod.ContractNo, smod.ContractDate, smod.ExportInvoice, smod.ExportInvoiceDate, smod.VendorOrderNo, smod.BankDetails, smod.LcNumber, smod.CustomDetails
			FROM AMS.SO_Master_OtherDetails smod
			WHERE smod.SO_Invoice=@voucherNo;";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnSalesChallanDetailsInDataSet(string voucherNo)
    {
        var cmdString = @$"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}';
			SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			 FROM AMS.SC_Master AS PIM
				  INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID = PIM.Customer_Id
				  LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId = PIM.Subledger_Id
				  LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId = PIM.Agent_Id
				  LEFT OUTER JOIN AMS.Currency AS C ON C.CId = PIM.Cur_Id
				  LEFT OUTER JOIN AMS.Department AS D ON D.DId = PIM.Cls1
			 WHERE PIM.SC_Invoice = @voucherNo;
			SELECT sd.SC_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration, sd.SO_Invoice, sd.SO_Sno, sd.QOT_Invoice,sd.QOT_Sno,sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax
			 FROM AMS.SC_Details sd
				  INNER JOIN AMS.Product AS P ON P.PID = sd.P_Id
				  LEFT OUTER JOIN AMS.Godown AS G ON G.GID = sd.Gdn_Id
				  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				  LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			 WHERE sd.SC_Invoice = @voucherNo;
			SELECT PIT.SNo,PIT.SNo, Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			 FROM AMS.SC_Term AS PIT
				  INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
			 WHERE SC_VNo = @voucherNo AND Term_Type = 'P'
			 ORDER BY Order_No ASC;
			SELECT PIT.SC_Vno,PIT.SNo, Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			 FROM AMS.SC_Term AS PIT
				  INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
			 WHERE SC_VNo = @voucherNo AND Term_Type = 'B'
			 ORDER BY Order_No ASC;
			SELECT smod.SC_Invoice, smod.Transport, smod.VechileNo, smod.BiltyNo, smod.Package, smod.BiltyDate, smod.BiltyType, smod.Driver, smod.PhoneNo, smod.LicenseNo, smod.MailingAddress, smod.MCity, smod.MState, smod.MCountry, smod.MEmail, smod.ShippingAddress, smod.SCity, smod.SState, smod.SCountry, smod.SEmail, smod.ContractNo, smod.ContractDate, smod.ExportInvoice, smod.ExportInvoiceDate, smod.VendorOrderNo, smod.BankDetails, smod.LcNumber, smod.CustomDetails
			 FROM AMS.SC_Master_OtherDetails smod
			 WHERE smod.SC_Invoice = @voucherNo; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnSalesInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}';
			SELECT GL.GLCode, GL.GLName,pm.AccountLedger,pm.ShortName RegNumber, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName,hd.DName HDName, PIM.*
			 FROM AMS.SB_Master AS PIM
				  INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID = PIM.Customer_Id
				  LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId = PIM.Subledger_Id
				  LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId = PIM.Agent_Id
				  LEFT OUTER JOIN AMS.Currency AS C ON C.CId = PIM.Cur_Id
				  LEFT OUTER JOIN AMS.Department AS D ON D.DId = PIM.Cls1
                  LEFT OUTER JOIN HOS.PatientMaster pm ON pm.PaitentId = PIM.PatientId
				  LEFT OUTER JOIN HOS.Department hd ON hd.DId = PIM.HDepartmentId
			 WHERE PIM.SB_Invoice = @voucherNo;

			SELECT sd.SB_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration, sd.SO_Invoice, sd.SO_Sno, sd.SC_Invoice, sd.SC_SNo, sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.MaterialPost, sd.PDiscountRate, sd.PDiscount, sd.BDiscountRate, sd.BDiscount, sd.ServiceChargeRate, sd.ServiceCharge, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax
			 FROM AMS.SB_Details sd
				  INNER JOIN AMS.Product AS P ON P.PID = sd.P_Id
				  LEFT OUTER JOIN AMS.Godown AS G ON G.GID = sd.Gdn_Id
				  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				  LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			 WHERE sd.SB_Invoice = @voucherNo;

			SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			 FROM AMS.SB_Term AS PIT
				  INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
			 WHERE PIT.SB_VNo = @voucherNo AND PIT.Term_Type = 'P'
			 ORDER BY PBT.Order_No ASC;

			SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
			 FROM AMS.SB_Term AS PIT
				  INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
			 WHERE PIT.SB_VNo = @voucherNo AND PIT.Term_Type = 'B'
			 ORDER BY PBT.Order_No ASC;

			SELECT smod.SB_Invoice, smod.Transport, smod.VechileNo, smod.BiltyNo, smod.Package, smod.BiltyDate, smod.BiltyType, smod.Driver, smod.PhoneNo, smod.LicenseNo, smod.MailingAddress, smod.MCity, smod.MState, smod.MCountry, smod.MEmail, smod.ShippingAddress, smod.SCity, smod.SState, smod.SCountry, smod.SEmail, smod.ContractNo, smod.ContractDate, smod.ExportInvoice, smod.ExportInvoiceDate, smod.VendorOrderNo, smod.BankDetails, smod.LcNumber, smod.CustomDetails
			 FROM AMS.SB_Master_OtherDetails smod
			 WHERE smod.SB_Invoice = @voucherNo;  ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnTempSalesInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}';
			SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			 FROM AMS.temp_SB_Master AS PIM
				  INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID = PIM.Customer_Id
				  LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId = PIM.Subledger_Id
				  LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId = PIM.Agent_Id
				  LEFT OUTER JOIN AMS.Currency AS C ON C.CId = PIM.Cur_Id
				  LEFT OUTER JOIN AMS.Department AS D ON D.DId = PIM.Cls1
			 WHERE PIM.SB_Invoice = @voucherNo;
			SELECT sd.SB_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration, sd.SO_Invoice, sd.SO_Sno, sd.SC_Invoice, sd.SC_SNo, sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.MaterialPost, sd.PDiscountRate, sd.PDiscount, sd.BDiscountRate, sd.BDiscount, sd.ServiceChargeRate, sd.ServiceCharge, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax
			 FROM AMS.temp_SB_Details sd
				  INNER JOIN AMS.Product AS P ON P.PID = sd.P_Id
				  LEFT OUTER JOIN AMS.Godown AS G ON G.GID = sd.Gdn_Id
				  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				  LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			 WHERE sd.SB_Invoice = @voucherNo; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet ReturnSalesReturnDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
        DECLARE @voucherNo NVARCHAR(50) =N'{voucherNo}';

        SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.SR_Invoice, PIM.Invoice_Date, PIM.Invoice_Miti, PIM.Invoice_Time, PIM.SB_Invoice, PIM.SB_Date, PIM.SB_Miti, PIM.Customer_ID, PIM.Party_Name, PIM.Vat_No, PIM.Contact_Person, PIM.Mobile_No, PIM.Address, PIM.ChqNo, PIM.ChqDate, PIM.Invoice_Type, PIM.Invoice_Mode, PIM.Payment_Mode, PIM.DueDays, PIM.DueDate, PIM.Agent_Id, PIM.Subledger_Id, PIM.Cls1, PIM.Cls2, PIM.Cls3, PIM.Cls4, PIM.CounterId, PIM.Cur_Id, PIM.Cur_Rate, PIM.B_Amount, PIM.T_Amount, PIM.N_Amount, PIM.LN_Amount, PIM.V_Amount, PIM.Tbl_Amount, PIM.Tender_Amount, PIM.Return_Amount, PIM.Action_Type, PIM.In_Words, PIM.Remarks, PIM.R_Invoice, PIM.Is_Printed, PIM.No_Print, PIM.Printed_By, PIM.Printed_Date, PIM.Audit_Lock, PIM.Enter_By, PIM.Enter_Date, PIM.Reconcile_By, PIM.Reconcile_Date, PIM.Auth_By, PIM.Auth_Date, PIM.Cleared_By, PIM.Cleared_Date, PIM.Cancel_By, PIM.Cancel_Date, PIM.Cancel_Remarks, PIM.CUnit_Id, PIM.CBranch_Id, PIM.IsAPIPosted, PIM.IsRealtime, PIM.FiscalYearId, PIM.SyncGlobalId, PIM.SyncOriginId, PIM.SyncCreatedOn, PIM.SyncLastPatchedOn, PIM.SyncRowVersion, PIM.SyncBaseId
        FROM AMS.SR_Master AS PIM
          INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Customer_ID
          LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
          LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_Id
          LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
          LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
        WHERE PIM.SR_Invoice=@voucherNo;

        SELECT PID.SR_Invoice, PID.Invoice_SNo, P.PName, P.PShortName, PID.P_Id, G.GName, G.GCode, PID.Gdn_Id, P.PAltUnit, PID.Alt_UnitId, ALTU.UnitCode AS AltUnitCode, PID.Alt_Qty, PID.Qty, P.PUnit, U.UnitCode, PID.Unit_Id, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, SB_Sno, SB_Invoice, PID.PDiscountRate, PID.PDiscount, PID.BDiscountRate, PID.BDiscount, PID.ServiceChargeRate, PID.ServiceCharge, P.PTax, PID.Tax_Amount, PID.V_Amount, PID.V_Rate, PID.Free_Qty, PID.ExtraFree_Qty, PID.StockFree_Qty, PID.ExtraFree_Unit_Id, PID.ExtraStockFree_Qty, PID.T_Product, PID.S_Ledger, PID.SR_Ledger
        FROM AMS.SR_Details AS PID
          INNER JOIN AMS.Product AS P ON P.PID=PID.P_Id
          LEFT OUTER JOIN AMS.Godown AS G ON G.GID=PID.Gdn_Id
          LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
          LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
        WHERE PID.SR_Invoice=@voucherNo;
 
        SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
         FROM AMS.SR_Term AS PIT
	          INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
         WHERE PIT.SR_VNo = @voucherNo AND PIT.Term_Type = 'P'
         ORDER BY PBT.Order_No ASC;

        SELECT PIT.SNo, PBT.Order_No orderNo, PBT.ST_ID TermId, PBT.ST_Name TermName, CASE WHEN PBT.ST_Basis = 'V' THEN 'Value' ELSE 'Qty' END Basis, PBT.ST_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.ST_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt, 'SB' Source, '' Formula
         FROM AMS.SR_Term AS PIT
	          INNER JOIN AMS.ST_Term AS PBT ON PBT.ST_ID = PIT.ST_Id
         WHERE PIT.SR_VNo = @voucherNo AND PIT.Term_Type = 'B'
         ORDER BY PBT.Order_No ASC;

        SELECT smod.SR_Invoice, smod.Transport, smod.VechileNo, smod.BiltyNo, smod.Package, smod.BiltyDate, smod.BiltyType, smod.Driver, smod.PhoneNo, smod.LicenseNo, smod.MailingAddress, smod.MCity, smod.MState, smod.MCountry, smod.MEmail, smod.ShippingAddress, smod.SCity, smod.SState, smod.SCountry, smod.SEmail, smod.ContractNo, smod.ContractDate, smod.ExportInvoice, smod.ExportInvoiceDate, smod.VendorOrderNo, smod.BankDetails, smod.LcNumber, smod.CustomDetails
        FROM AMS.SR_Master_OtherDetails smod
        WHERE smod.SR_Invoice=@voucherNo; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    public DataSet GetSalesInvoiceDetailsForPrint(string billNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT SIM.*, GL.GLName, GL.PanNo, GL.GlAddress, CCode, CName, JA.AgentName WaiterName, TableCode, tableName
			FROM AMS.SB_Master SIM
				 LEFT OUTER JOIN AMS.GeneralLedger GL ON SIM.Customer_Id = GL.GlId
				 LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id
				 LEFT OUTER JOIN AMS.Counter AS RC ON RC.CId = SIM.CounterId
				 LEFT OUTER JOIN AMS.TableMaster TM ON TM.TableId = SIM.TableId
			WHERE SIM.SB_Invoice = '{billNo}';

			SELECT SID.*, PName, PShortName, G.GName, APU.UnitCode Alt_UnitCode, APU.UnitName AS Alt_UnitName, PU.UnitCode, PU.UnitName, CASE WHEN PTax > 0 THEN ROUND(SID.B_Amount/SID.Qty, 2) ELSE Rate END AS BeforeVat, CASE WHEN PTax > 0 THEN ROUND(Rate / 1.13 * Qty, 2) ELSE Rate * Qty END AS BAmount, (SID.N_Amount + SID.PDiscount) NetAmount
			FROM AMS.SB_Details AS SID
				 LEFT OUTER JOIN AMS.Product AS P ON P.PID = SID.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID = SID.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS APU ON APU.UID = SID.Alt_UnitId
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = SID.Unit_Id
			WHERE SB_Invoice = '{billNo}';

			SELECT * FROM AMS.SB_Term WHERE SB_VNo = '{billNo}'; ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    public DataSet GetSalesReturnDetailsForPrint(string billNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT	SIM.*,GL.GLName,GL.PanNo,GL.GlAddress,CCode,CName,JA.AgentName WaiterName
			FROM AMS.SR_Master SIM
				LEFT OUTER JOIN AMS.GeneralLedger GL ON SIM.Customer_Id = GL.GlId LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id  LEFT OUTER JOIN AMS.Counter AS RC ON RC.CId = SIM.CounterId
			WHERE SIM.SR_Invoice = '{billNo}';

			SELECT SID.*, PName, PShortName, G.GName, APU.UnitCode Alt_UnitCode, APU.UnitName AS Alt_UnitName, PU.UnitCode,PU.UnitName,
				CASE WHEN PTax > 0 THEN ROUND(Rate / 1.13, 2) ELSE Rate END AS BeforeVat,
				CASE WHEN PTax > 0 THEN ROUND(Rate / 1.13 * Qty, 2)	ELSE Rate * Qty	END AS BAmount
			FROM AMS.SR_Details AS SID
				LEFT OUTER JOIN AMS.Product AS P ON P.PId = SID.P_Id LEFT OUTER JOIN AMS.Godown AS G ON G.GId = SID.Gdn_Id LEFT OUTER JOIN AMS.ProductUnit AS APU ON APU.UID = SID.Alt_UnitId LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = SID.Unit_Id
			WHERE SR_Invoice = '{billNo}';

			SELECT sr.* FROM AMS.SR_Term sr
				WHERE sr.SR_VNo = '{billNo}';");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    public DataSet GetConfirmationSalesDetails(string billNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT SIM.*, GL.GLName, GL.PanNo, GL.GlAddress, CCode, CName, JA.AgentName AS WaiterName, SIM.TableId, tm.TableName, tm.TableType
			FROM AMS.SO_Master SIM
				 LEFT OUTER JOIN AMS.GeneralLedger GL ON SIM.Customer_Id = GL.GlId
				 LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id
				 LEFT OUTER JOIN AMS.Counter AS RC ON RC.CId = SIM.CounterId
				 LEFT OUTER JOIN AMS.TableMaster tm ON SIM.TableId = tm.TableId
			WHERE SIM.SO_Invoice = '{billNo}';

			SELECT SID.*, PName, PShortName, G.GName, APU.UnitCode AS Alt_UnitCode, APU.UnitName AS Alt_UnitName, PU.UnitCode,PU.UnitName,
				CASE WHEN PTax > 0 THEN ROUND(Rate / 1.13, 2) ELSE Rate END AS BeforeVat,
				CASE WHEN PTax > 0 THEN ROUND(Rate / 1.13 * Qty, 2)	ELSE Rate * Qty	END AS BAmount
			FROM AMS.SO_Details AS SID
				LEFT OUTER JOIN AMS.Product AS P ON P.PId = SID.P_Id LEFT OUTER JOIN AMS.Godown AS G ON G.GId = SID.Gdn_Id LEFT OUTER JOIN AMS.ProductUnit AS APU ON APU.UID = SID.Alt_UnitId LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = SID.Unit_Id
			WHERE SID.Is_Canceled = 0  AND  SO_Invoice = '{billNo}';

			SELECT sr.* FROM AMS.SO_Term sr
			WHERE sr.SO_VNo = '{billNo}';");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    private bool UpdateTransferTable(int fromTableId, int tableId)
    {
        var Query = $"  Update AMS.TableMaster set TableStatus='A' where TableId='{fromTableId}' ";
        Query += $" Update AMS.TableMaster set TableStatus='O' where TableId='{tableId}' ";
        var result = SqlExtensions.ExecuteNonTrans(Query);
        return result > 0;
    }

    public bool UpdateTransferTableSalesOrderMaster(string orderNo, int tableId, int fromTableId)
    {
        try
        {
            var Query =
                $"Update AMS.SO_Master set TableId={fromTableId} where SO_Invoice ='{orderNo}' AND TableId = {tableId} ";
            var result = SqlExtensions.ExecuteNonQuery(Query);
            if (result != 0)
            {
                UpdateTransferTable(fromTableId, tableId);
            }

            return result > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    #endregion --------------- RETURN DATASET  ---------------

    // TABLE MASTER

    #region --------------- TABLE MASTER  ---------------
    public bool CheckTableMasterExist(int tableId)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($"SELECT * FROM AMS.TableMaster WHERE TableId = {tableId}");
        var result = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return result > 0;
    }


    #endregion
    // OBJECT FOR THIS CLASS

    #region --------------- GLOBAL VALUE ---------------

    public SO_Details SoDetails { get; set; }
    public SQ_Master SqMaster { get; set; }
    public SO_Master SoMaster { get; set; }
    public SC_Master ScMaster { get; set; }
    public SB_Master SbMaster { get; set; }
    public Temp_SB_Master TsbMaster { get; set; }
    public SB_Master_OtherDetails SbOther { get; set; }
    public List<SB_Details> SbDetails { get; set; }
    public List<SB_Details> IsbDetails { get; set; }
    public List<SB_Term> SbTerms { get; set; }
    public SR_Master SrMaster { get; set; }
    public List<SR_Details> SrDetails { get; set; }
    public List<SR_Term> SrTerms { get; set; }

    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    private readonly ClsIrdApiSync _apiSync = new();
    public NightAuditLog AuditLog { get; set; }

    #endregion --------------- GLOBAL VALUE ---------------
}