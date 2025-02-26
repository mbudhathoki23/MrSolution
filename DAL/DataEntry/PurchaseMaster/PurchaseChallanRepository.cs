using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Abstractions;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseChallanRepository : IPurchaseChallanRepository
{
    // PURCHASE CHALLAN REPOSITORY
    public PurchaseChallanRepository()
    {
        PcMaster = new PC_Master();
        DetailsList = new List<PC_Details>();
        ChallanTerms = new List<PC_Term>();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }


    // RETURN VALUE IN DATA TABLE
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

    public DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = $" SELECT * FROM AMS.{tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GlID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, SCM.*
			FROM AMS.PC_Master AS SCM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=SCM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=SCM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=SCM.Agent_Id
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=SCM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=SCM.Cls1
			WHERE SCM.PC_Invoice = @voucherNo;
			SELECT SCD.PC_Invoice, SCD.Invoice_SNo, P.PName, P.PShortName, SCD.P_Id, G.GName, G.GCode, SCD.Gdn_Id, SCD.Alt_UnitId, ALTU.UnitCode AS AltUnitId, SCD.Alt_Qty, SCD.Qty, U.UnitCode UnitId, SCD.Unit_Id, SCD.Rate, SCD.B_Amount, SCD.T_Amount, SCD.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, QOT_Sno, QOT_Invoice, PO_Sno, PO_Invoice, SCD.*
			FROM AMS.PC_Details AS SCD
				 INNER JOIN AMS.Product AS P ON P.PID=SCD.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=SCD.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
			WHERE SCD.PC_Invoice=@voucherNo
			ORDER BY SCD.Serial_No;
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PC_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PC_VNo=@voucherNo AND Term_Type='P'
			ORDER BY SIT.SNo ASC
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PC_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PC_VNo = @voucherNo AND Term_Type='B'
			ORDER BY SIT.SNo ASC ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

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


    // INSERT UPDATE DELETE
    public short ReturnMaxSyncRowVersion(string module, string vno)
    {
        var cmdString = module switch
        {
            "PIN" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PIN_Master pm WHERE pm.PIN_Invoice = '{vno}'",
            "PO" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PO_Master pm WHERE pm.PO_Invoice = '{vno}'",
            "PC" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PC_Master pm WHERE pm.PC_Invoice = '{vno}'",
            "GIT" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.GIT_Master pm WHERE pm.GIT_Invoice = '{vno}'",
            "PB" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PB_Master pm WHERE pm.PB_Invoice = '{vno}'",
            "PR" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PR_Master pm WHERE pm.PR_Invoice = '{vno}'",
            "PAB" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.VmPabMaster pm WHERE pm.PAB_Invoice = '{vno}'",
            _ => string.Empty
        };
        return cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();
    }

    public int SavePurchaseChallan(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE" or "UPDATE")
        {
            cmdString.Append($@" 
                DELETE FROM AMS.StockDetails WHERE Voucher_No = '{PcMaster.PC_Invoice}';
				DELETE FROM AMS.PC_Term WHERE PC_VNo = '{PcMaster.PC_Invoice}';
				DELETE FROM AMS.PC_Details WHERE PC_Invoice = '{PcMaster.PC_Invoice}'; ");
            cmdString.Append(" \n");
            if (actionTag is "DELETE")
            {
                cmdString.Append($" DELETE FROM AMS.PC_Master WHERE PC_Invoice = '{PcMaster.PC_Invoice}'; \n");
            }
        }

        if (actionTag.ToUpper() is "NEW" || actionTag is "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.PC_Master(PC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate,ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, PO_Invoice, PO_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append($@" 
                VALUES ( N'{PcMaster.PC_Invoice}','{PcMaster.Invoice_Date.GetSystemDate()}','{PcMaster.Invoice_Miti}',GETDATE(), "); //PC_Invoice
            cmdString.Append(PcMaster.PB_Vno.IsValueExits() ? $"N'{PcMaster.PB_Vno}'," : "NULL,"); //PB_Vno
            cmdString.Append(PcMaster.PB_Vno.IsValueExits()
                ? $"'{PcMaster.Vno_Date.GetSystemDate()}',"
                : "NULL,"); //Vno_Date
            cmdString.Append(PcMaster.PB_Vno.IsValueExits() ? $"'{PcMaster.Vno_Miti}'," : "NULL,"); //Vno_Miti
            cmdString.Append(PcMaster.Vendor_ID > 0 ? $"'{PcMaster.Vendor_ID}'," : "0,"); //Vendor_ID
            if (PcMaster.Party_Name.IsValueExits())
            {
                cmdString.Append(PcMaster.PartyLedgerId.IsValueExits()
                    ? $"N'{PcMaster.PartyLedgerId}',"
                    : "NULL,"); //Party_Name
                cmdString.Append(PcMaster.Party_Name.IsValueExits()
                    ? $"N'{PcMaster.Party_Name}',"
                    : "NULL,"); //Party_Name
                cmdString.Append(PcMaster.Vat_No.IsValueExits() ? $"N'{PcMaster.Vat_No}'," : "NULL,"); //Vat_No
                cmdString.Append(PcMaster.Contact_Person.IsValueExits()
                    ? $"N'{PcMaster.Contact_Person}',"
                    : "NULL,"); //Contact_Person
                cmdString.Append(PcMaster.Mobile_No.IsValueExits()
                    ? $"N'{PcMaster.Mobile_No}',"
                    : "NULL,"); //Mobile_No
                cmdString.Append(PcMaster.Address.IsValueExits() ? $"N'{PcMaster.Address}'," : "NULL,"); //Address
                cmdString.Append(PcMaster.ChqNo.IsValueExits() ? $"N'{PcMaster.ChqNo}'," : "NULL,"); //ChqNo
                cmdString.Append(PcMaster.ChqNo.IsValueExits()
                    ? $"'{PcMaster.ChqDate.GetSystemDate()}',"
                    : "NULL,"); //ChqDate
                cmdString.Append(PcMaster.ChqNo.IsValueExits() ? $"'{PcMaster.ChqDate}'," : "NULL,"); //ChqDate
            }
            else
            {
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
            }

            cmdString.Append(PcMaster.Invoice_Type.IsValueExits()
                ? $"N'{PcMaster.Invoice_Type}',"
                : "'NORMAL',"); //Invoice_Type
            cmdString.Append(PcMaster.Invoice_In.IsValueExits()
                ? $"N'{PcMaster.Invoice_In}',"
                : "'CREDIT',"); //Invoice_In
            cmdString.Append(PcMaster.DueDays.GetInt() > 0 ? $"'{PcMaster.DueDays}'," : "0,"); //DueDays
            cmdString.Append(PcMaster.DueDate != null ? $"'{PcMaster.DueDate:yyyy-MM-dd}'," : "NULL,"); //DueDate
            cmdString.Append(PcMaster.Agent_Id.GetInt() > 0 ? $"'{PcMaster.Agent_Id}'," : "NULL,"); //Agent_Id
            cmdString.Append(PcMaster.Subledger_Id.GetInt() > 0
                ? $"'{PcMaster.Subledger_Id}',"
                : "Null,"); //SubLedger_Id

            if (PcMaster.PO_Invoice.IsValueExits())
            {
                cmdString.Append(PcMaster.PO_Invoice.IsValueExits() ? $"'{PcMaster.PO_Invoice}'," : "NULL,");
                cmdString.Append(PcMaster.PO_Date != null ? $"'{PcMaster.PO_Date.GetSystemDate()}'," : "NULL,");
            }
            else
            {
                cmdString.Append("NULL,NULL,");
            }

            if (PcMaster.QOT_Invoice.IsValueExits())
            {
                cmdString.Append(PcMaster.QOT_Invoice.IsValueExits() ? $"N'{PcMaster.QOT_Invoice}'," : "NULL,");
                cmdString.Append(PcMaster.QOT_Date != null ? $"'{PcMaster.QOT_Date.GetSystemDate()}'," : "NULL,");
            }
            else
            {
                cmdString.Append("NULL,NULL,");
            }

            cmdString.Append(PcMaster.Cls1.GetInt() > 0 ? $"'{PcMaster.Cls1}'," : "Null,");
            cmdString.Append("NULL,NULL,NULL, "); // Cls2, Cls3, Cls4,
            cmdString.Append(PcMaster.Cur_Id.GetInt() > 0 ? $"{PcMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append($"{PcMaster.Cur_Rate.GetDecimal(true)},");
            cmdString.Append(PcMaster.Counter_ID.GetInt() > 0 ? $"'{PcMaster.Counter_ID}'," : "Null,");
            cmdString.Append($"{PcMaster.B_Amount.GetDecimal()},");
            cmdString.Append($"{PcMaster.T_Amount.GetDecimal()},");
            cmdString.Append($"{PcMaster.N_Amount.GetDecimal()},");
            cmdString.Append($"{PcMaster.LN_Amount.GetDecimal()},");
            cmdString.Append($"{PcMaster.Tender_Amount.GetDecimal()},");
            cmdString.Append($"{PcMaster.Change_Amount.GetDecimal()},");
            cmdString.Append($"'{PcMaster.V_Amount.GetDecimal()}',");
            cmdString.Append($"'{PcMaster.Tbl_Amount.GetDecimal()}',");
            cmdString.Append(PcMaster.Action_type.IsValueExits() ? $"N'{PcMaster.Action_type}'," : "'SAVE',");
            cmdString.Append("0,NULL,NULL,NULL,0,");
            cmdString.Append(PcMaster.In_Words.IsValueExits() ? $"N'{PcMaster.In_Words}'," : "NULL,");
            cmdString.Append(PcMaster.Remarks.IsValueExits()
                ? $"N'{PcMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}',GETDATE(),NULL,NULL,NULL,NULL,{ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}" : "NULL,");
            cmdString.Append($"{ObjGlobal.SysFiscalYearId},NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
            cmdString.Append($"{PcMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE [AMS].[PC_Master] SET  \n");
            cmdString.Append($"Invoice_Date = '{PcMaster.Invoice_Date.GetSystemDate()}',");
            cmdString.Append($"Invoice_Miti = '{PcMaster.Invoice_Miti}',");
            cmdString.Append(PcMaster.PB_Vno.IsValueExits() ? $"PB_Vno = N'{PcMaster.PB_Vno}'," : "PB_Vno = NULL,");
            cmdString.Append(PcMaster.Vno_Date != null
                ? $"Vno_Date = '{PcMaster.Vno_Date.GetSystemDate()}',"
                : "NULL,");
            cmdString.Append(PcMaster.Vno_Miti != null ? $"Vno_Miti = '{PcMaster.Vno_Miti}'," : "NULL,");
            cmdString.Append(PcMaster.Vendor_ID.GetLong() > 0
                ? $"Vendor_ID = '{PcMaster.Vendor_ID}',"
                : "Vendor_ID = 0,");
            cmdString.Append(PcMaster.Party_Name.IsValueExits()
                ? $"Party_Name = N'{PcMaster.Party_Name}',"
                : "Party_Name= NULL,");
            cmdString.Append(PcMaster.Vat_No.IsValueExits() ? $"Vat_No = N'{PcMaster.Vat_No}'," : "Vat_No = NULL,");
            cmdString.Append(PcMaster.Contact_Person.IsValueExits()
                ? $"Contact_Person = N'{PcMaster.Contact_Person}',"
                : "Contact_Person= NULL,");
            cmdString.Append(PcMaster.Mobile_No.IsValueExits()
                ? $"Mobile_No = N'{PcMaster.Mobile_No}',"
                : "Mobile_No= NULL,");
            cmdString.Append(PcMaster.Address.IsValueExits()
                ? $"Address = N'{PcMaster.Address}',"
                : "Address= NULL,");
            cmdString.Append(PcMaster.ChqNo.IsValueExits() ? $"ChqNo = N'{PcMaster.ChqNo}'," : "ChqNo= NULL,");
            cmdString.Append(PcMaster.ChqNo.IsValueExits()
                ? $"ChqDate= '{PcMaster.ChqDate.GetSystemDate()}',"
                : "ChqDate= NULL,");
            cmdString.Append(PcMaster.Invoice_Type.IsValueExits()
                ? $"Invoice_Type = N'{PcMaster.Invoice_Type}',"
                : "Invoice_Type= NULL,");
            cmdString.Append(PcMaster.Invoice_In.IsValueExits()
                ? $"Invoice_In = N'{PcMaster.Invoice_In}',"
                : "Invoice_In= NULL,");
            cmdString.Append(PcMaster.DueDays.GetInt() > 0 ? $"DueDays = '{PcMaster.DueDays}'," : "DueDays = 0,");
            cmdString.Append(PcMaster.DueDays.GetInt() > 0
                ? $"DueDate = '{PcMaster.DueDate.GetSystemDate()}',"
                : "DueDate= NULL,");
            cmdString.Append(PcMaster.Agent_Id.GetInt() > 0
                ? $"Agent_Id = '{PcMaster.Agent_Id}',"
                : "Agent_Id= Null,");
            cmdString.Append(PcMaster.Subledger_Id.GetInt() > 0
                ? $"Subledger_Id = '{PcMaster.Subledger_Id}',"
                : "Subledger_Id =Null,");
            cmdString.Append(PcMaster.PO_Invoice.IsValueExits()
                ? $"PO_Invoice = '{PcMaster.PO_Invoice}',"
                : "PO_Invoice = NULL,");
            cmdString.Append(PcMaster.PO_Invoice.IsValueExits()
                ? $"PO_Date = '{PcMaster.PO_Date.GetSystemDate()}',"
                : "PO_Date = NULL,");
            cmdString.Append(PcMaster.QOT_Invoice.IsValueExits()
                ? $"QOT_Invoice = N'{PcMaster.QOT_Invoice}',"
                : "QOT_Invoice = NULL,");
            cmdString.Append(PcMaster.QOT_Invoice.IsValueExits()
                ? $"QOT_Date = '{PcMaster.QOT_Date.GetSystemDate()}',"
                : "QOT_Date = NULL,");
            cmdString.Append(PcMaster.Cls1.GetInt() > 0 ? $"Cls1 = '{PcMaster.Cls1}'," : "Cls1 = Null,");
            cmdString.Append(PcMaster.Cur_Id.GetInt() > 0
                ? $"Cur_Id = '{PcMaster.Cur_Id}',"
                : $"Cur_Id = {ObjGlobal.SysCurrencyId},");
            cmdString.Append(PcMaster.Cur_Rate.GetDecimal(true) > 0
                ? $"Cur_Rate = '{PcMaster.Cur_Rate}',"
                : "Cur_Rate = 1,");
            cmdString.Append(PcMaster.Counter_ID.GetInt() > 0
                ? $"Counter_ID = '{PcMaster.Counter_ID}',"
                : "Counter_ID =Null,");
            cmdString.Append($" B_Amount = '{PcMaster.B_Amount.GetDecimal()}',");
            cmdString.Append($" T_Amount = '{PcMaster.T_Amount.GetDecimal()}',");
            cmdString.Append($" N_Amount = '{PcMaster.N_Amount.GetDecimal()}',");
            cmdString.Append($" Tender_Amount = '{PcMaster.Tender_Amount.GetDecimal()}',");
            cmdString.Append($" Change_Amount = '{PcMaster.Change_Amount.GetDecimal()}',");
            cmdString.Append($" V_Amount = '{PcMaster.V_Amount.GetDecimal()}',");
            cmdString.Append($" LN_Amount= '{PcMaster.LN_Amount.GetDecimal()}',");
            cmdString.Append($" Tbl_Amount = '{PcMaster.Tbl_Amount.GetDecimal()}',");
            cmdString.Append($" Action_type = N'{PcMaster.Action_type}',");
            cmdString.Append(PcMaster.In_Words.IsValueExits()
                ? $" In_Words = N'{PcMaster.In_Words}',"
                : "In_Words = NULL,");
            cmdString.Append(PcMaster.Remarks.IsValueExits()
                ? $" Remarks = N'{PcMaster.Remarks.Trim().Replace("'", "''")}', "
                : "Remarks = NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? " SyncLastPatchedOn = GETDATE(),"
                : "SyncLastPatchedOn = NULL,");
            cmdString.Append($" SyncRowVersion =  {PcMaster.SyncRowVersion.GetInt()},");
            cmdString.Append(" IsSynced =0");
            cmdString.Append($" WHERE PC_Invoice = '{PcMaster.PC_Invoice}' ; \n ");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            if (DetailsList.Count > 0)
            {
                var iRow = 0;
                cmdString.Append(@" 
                    INSERT INTO AMS.PC_Details(PC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                cmdString.Append("\n VALUES \n");
                foreach (var ro in DetailsList)
                {
                    cmdString.Append($" (N'{ro.PC_Invoice}',{ro.Invoice_SNo},{ro.P_Id}, "); //PC_Invoice
                    cmdString.Append(ro.Gdn_Id > 0 ? $" {ro.Gdn_Id}, " : "Null,"); //Gdn_Id
                    cmdString.Append(ro.Alt_Qty > 0 ? $" {ro.Alt_Qty}, " : "0,"); //Alt_Qty
                    cmdString.Append(ro.Alt_UnitId > 0 ? $" {ro.Alt_UnitId}, " : "Null,"); //Alt_UnitId
                    cmdString.Append(ro.Qty > 0 ? $" {ro.Qty}, " : "1,"); //Qty
                    cmdString.Append(ro.Unit_Id > 0 ? $" {ro.Unit_Id}, " : "Null,"); //Unit_Id
                    cmdString.Append($"{ro.Rate}, "); //Rate
                    cmdString.Append($"{ro.B_Amount}, "); //B_Amount
                    cmdString.Append($"{ro.T_Amount},"); //T_Amount
                    cmdString.Append($"{ro.N_Amount}, "); //N_Amount
                    cmdString.Append($"{ro.AltStock_Qty},"); //AltStock_Qty
                    cmdString.Append($"{ro.Stock_Qty}, "); //Stock_Qty
                    cmdString.Append(ro.Narration.IsValueExits() ? $" N'{ro.Narration}', " : "NUll,"); //Narration
                    cmdString.Append(ro.PO_Invoice.IsValueExits()
                        ? $" N'{ro.PO_Invoice}', "
                        : "NUll,"); //PO_Invoice
                    cmdString.Append(ro.PO_Invoice.IsValueExits() ? $" '{ro.PO_Sno}', " : "NUll,"); //PO_Sno
                    cmdString.Append(ro.QOT_Invoice.IsValueExits()
                        ? $" N'{ro.QOT_Invoice}', "
                        : "NUll,"); //PO_Invoice
                    cmdString.Append(ro.QOT_Invoice.IsValueExits() ? $" '{ro.QOT_SNo}', " : "NUll,"); //PO_Sno
                    cmdString.Append($"{ro.Tax_Amount},{ro.V_Amount},"); //Tax_Amount
                    cmdString.Append($"{ro.V_Rate},0,"); //V_Rate
                    cmdString.Append(ro.Free_Unit_Id > 0 ? $" {ro.Free_Unit_Id}, " : "NULL,"); //Free_Unit_Id
                    cmdString.Append($" {ro.Free_Qty},{ro.StockFree_Qty},"); //Free_Qty
                    cmdString.Append(ro.ExtraFree_Unit_Id > 0
                        ? $" {ro.ExtraFree_Unit_Id}, "
                        : "NULL,"); //ExtraFree_Unit_Id
                    cmdString.Append($" {ro.ExtraFree_Qty},0,"); //ExtraFree_Qty
                    cmdString.Append(ro.T_Product ? " 1," : "0,"); //ExtraStockFree_Qty
                    cmdString.Append(ro.P_Ledger == 0 ? " NULL," : $"{ro.P_Ledger},"); //ExtraStockFree_Qty
                    cmdString.Append(ro.PR_Ledger == 0 ? " NULL," : $"{ro.PR_Ledger},"); //ExtraStockFree_Qty
                    cmdString.Append(" NUll,NUll,NUll,NUll,NUll,NUll,NUll,NUll,NUll,NUll,NULL,");
                    cmdString.Append(ro.Batch_No.IsValueExits()
                        ? $" '{ro.Batch_No}', "
                        : "Null,"); //ExtraStockFree_Qty
                    cmdString.Append(ro.Batch_No.IsValueExits()
                        ? $" '{ro.Exp_Date}', "
                        : "NUll,"); //ExtraStockFree_Qty
                    cmdString.Append(ro.Batch_No.IsValueExits()
                        ? $" '{ro.Manu_Date}' "
                        : "NUll, "); //ExtraStockFree_Qty
                    cmdString.Append($"NULL,NUll,NULL,NULL,NULL,{PcMaster.SyncRowVersion.GetDecimal(true)} ");
                    cmdString.Append(iRow == DetailsList.Count - 1 ? " ); \n" : " ), \n");
                    iRow++;
                }
            }

            if (ChallanTerms.Count > 0)
            {
                if (actionTag.Equals("UPDATE"))
                {
                    cmdString.Append(
                        $"DELETE AMS.PC_Term WHERE PC_VNo='{PcMaster.PC_Invoice}' AND Term_Type='B' \n");
                }

                foreach (var dr in ChallanTerms)
                {
                    cmdString.Append(@" 
                        INSERT INTO AMS.PC_Term(PC_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                    cmdString.Append(" \n VALUES \n");
                    cmdString.Append($"('{PcMaster.PC_Invoice}',{dr.PT_Id},{dr.SNo},");
                    cmdString.Append($"'{dr.Term_Type}',");
                    cmdString.Append(dr.Product_Id > 0 ? $"{dr.Product_Id}," : "NULL,");
                    cmdString.Append($"{dr.Rate},{dr.Amount},'{dr.Taxable}',");
                    cmdString.Append($" NULL,NULL,NULL,NULL,NULL,{PcMaster.SyncRowVersion.GetDecimal(true)}); \n");
                }
            }
        }

        var iResult = SaveDataInDatabase(cmdString);
        if (iResult == 0)
        {
            return iResult;
        }

        if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null) Task.Run(() => SyncPurchaseChallanAsync(actionTag));
        if (!_tagStrings.Contains(actionTag))
        {
            if (actionTag is "SAVE")
            {
                AuditLogPurchaseChallan(actionTag);
            }

            PurchaseChallanTermPosting();
            PurchaseChallanStockPosting();
        }

        return iResult;
    }

    private static int SaveDataInDatabase(StringBuilder stringBuilder)
    {
        var isExc = SqlExtensions.ExecuteNonTrans(stringBuilder.ToString());
        return isExc;
    }

    private static int SaveDataInDatabase(string stringBuilder)
    {
        var isExc = SqlExtensions.ExecuteNonTrans(stringBuilder);
        return isExc;
    }

    public async Task<int> SyncPurchaseChallanAsync(string actionTag)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
            return 1;
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}PurchaseChallan/GetPurchaseChallansByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}PurchaseChallan/InsertPurchaseChallanList",
            UpdateUrl = @$"{_configParams.Model.Item2}PurchaseChallan/UpdatePurchaseChallan"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var purchaseChallanRepo = DataSyncProviderFactory.GetRepository<PC_Master>(_injectData);

        // push all new purchase challan data
        var sqlQuery = @"SELECT *FROM AMS.PC_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<PC_Master>(sqlQuery);
        var pcList = queryResponse.List.ToList();
        if (pcList.Count > 0)
        {
            var loopCount = 1;
            if (pcList.Count > 100)
            {
                loopCount = pcList.Count / 100 + 1;
            }

            var newPcList = new List<PC_Master>();
            var cmdString = new StringBuilder();
            var purchaseChallanList = new List<PC_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newPcList.Clear();
                if (i == 0)
                {
                    newPcList.AddRange(pcList.Take(100));
                }
                else
                {
                    newPcList.AddRange(pcList.Skip(100 * i).Take(100));
                }

                purchaseChallanList.Clear();
                foreach (var pcl in newPcList)
                {
                    sqlQuery = $@"SELECT * FROM AMS.PC_Details WHERE PC_Invoice='{pcl.PC_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<PC_Details>(sqlQuery);
                    var pcdList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT * FROM AMS.PC_Term WHERE PC_VNo='{pcl.PC_Invoice}'";
                    var pcTermQueryResponse = await QueryUtils.GetListAsync<PC_Term>(sqlQuery);
                    var pcTermList = pcTermQueryResponse.List.ToList();

                    pcl.DetailsList = pcdList;
                    pcl.ChallanTerms = pcTermList;
                    purchaseChallanList.Add(pcl);
                }

                var pushResponse = await purchaseChallanRepo.PushNewListAsync(purchaseChallanList);
                if (!pushResponse.Value)
                {
                    SplashScreenManager.CloseForm();
                    return 0;
                }
                else
                {
                    cmdString.Clear();
                    foreach (var pcl in newPcList)
                    {
                        cmdString.Append(
                            $"UPDATE AMS.PC_Master SET IsSynced = 1 WHERE PC_Invoice = '{pcl.PC_Invoice}';\n");
                    }

                    var result = await QueryUtils.ExecNonQueryAsync(cmdString.ToString());
                }
            }
        }

        return 1;
    }


    public async Task<bool> SyncPurchaseChallanDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);

        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}PurchaseChallan/GetPurchaseChallansByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}PurchaseChallan/InsertPurchaseChallanList",
            UpdateUrl = @$"{_configParams.Model.Item2}PurchaseChallan/UpdatePurchaseChallan"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var purchaseChallanRepo = DataSyncProviderFactory.GetRepository<PC_Master>(_injectData);

        // pull all new purchase challan data
        if (ObjGlobal.IsPurchaseChallanSync)
        {
            //_frmSyncLogs.AddLog("Pulling new purchase challan data from server...");
            //var result = await PullUnSyncPurchaseChallan(purchaseChallanRepo);
            //if (!result)
            //    return false;

        }
        // push all new purchase challan data
        var sqlQuery = @"SELECT *FROM AMS.PC_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<PC_Master>(sqlQuery);
        var pcList = queryResponse.List.ToList();
        if (pcList.Count > 0)
        {
            var loopCount = 1;
            if (pcList.Count > 100)
            {
                loopCount = pcList.Count / 100 + 1;
            }
            var newPcList = new List<PC_Master>();
            var cmdString = new StringBuilder();
            var purchaseChallanList = new List<PC_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newPcList.Clear();
                if (i == 0)
                {
                    newPcList.AddRange(pcList.Take(100));
                }
                else
                {
                    newPcList.AddRange(pcList.Skip(100 * i).Take(100));
                }
                purchaseChallanList.Clear();
                foreach (var pcl in newPcList)
                {
                    sqlQuery = $@"SELECT * FROM AMS.PC_Details WHERE PC_Invoice='{pcl.PC_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<PC_Details>(sqlQuery);
                    var pcdList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT * FROM AMS.PC_Term WHERE PC_VNo='{pcl.PC_Invoice}'";
                    var pcTermQueryResponse = await QueryUtils.GetListAsync<PC_Term>(sqlQuery);
                    var pcTermList = pcTermQueryResponse.List.ToList();

                    pcl.DetailsList = pcdList;
                    pcl.ChallanTerms = pcTermList;
                    purchaseChallanList.Add(pcl);
                }
                var pushResponse = await purchaseChallanRepo.PushNewListAsync(purchaseChallanList);
                if (!pushResponse.Value)
                {
                    SplashScreenManager.CloseForm(true);
                    return false;
                }
                else
                {
                    cmdString.Clear();
                    foreach (var pcl in newPcList)
                    {
                        cmdString.Append($"UPDATE AMS.PC_Master SET IsSynced = 1 WHERE PC_Invoice = '{pcl.PC_Invoice}';\n");
                    }

                    var result = await QueryUtils.ExecNonQueryAsync(cmdString.ToString());
                }
            }
        }

        return true;
    }
    private int AuditLogPurchaseChallan(string actionTag)
    {
        var cmdString = @$"
			INSERT INTO [AUD].[AUDIT_PC_MASTER] ([PC_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_ID], [Subledger_Id], [PO_Invoice], [PO_Date], [QOT_Invoice], [QOT_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_Type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_Id], [Subledger_Id], [PO_Invoice], [PO_Date], [QOT_Invoice], [QOT_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.[PC_Master]
			WHERE PC_Invoice='{PcMaster.PC_Invoice}';
			INSERT INTO [AUD].[AUDIT_PC_DETAILS] ([PC_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [QOT_Invoice], [QOT_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Issue_Qty], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [QOT_Invoice], [QOT_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Issue_Qty], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.PC_Details
			WHERE [PC_Invoice]='{PcMaster.PC_Invoice}';
			INSERT INTO [AUD].[AUDIT_PC_TERM] ([PC_VNo], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_VNo], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.[PC_Term]
			WHERE [PC_VNo]='{PcMaster.PC_Invoice}'; ";
        return SaveDataInDatabase(cmdString);
    }
    public int PurchaseChallanTermPosting()
    {
        var cmdString = $@"
			DELETE AMS.PC_Term WHERE Term_Type='BT' AND PC_VNo='{PcMaster.PC_Invoice}';
			INSERT INTO AMS.PC_Term(PC_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PC_VNo, PT_Id, ROW_NUMBER() OVER (ORDER BY(SELECT sbt.PC_VNo)) AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PC_Details sd
				 LEFT OUTER JOIN AMS.PC_Master sm ON sm.PC_Invoice=sd.PC_Invoice
				 LEFT OUTER JOIN AMS.PC_Term sbt ON sd.PC_Invoice=sbt.PC_VNo
			WHERE sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount>0 AND sbt.PC_VNo='{PcMaster.PC_Invoice}';";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    public int PurchaseChallanStockPosting()
    {
        var cmdString = @"
			DELETE FROM AMS.StockDetails WHERE Module='PC' ";
        cmdString += PcMaster.PC_Invoice.IsValueExits() ? $@" AND Voucher_No='{PcMaster.PC_Invoice}'; " : " ";
        if (!PcMaster.PC_Invoice.IsValueExits())
            cmdString += @"
				UPDATE AMS.PC_Master SET Invoice_Type ='NORMAL' WHERE PC_Invoice NOT IN ( SELECT PC_Invoice FROM AMS.PB_Master WHERE PC_Invoice IS NOT NULL AND PC_Invoice <> '')
				UPDATE AMS.PC_Master SET Invoice_Type ='POSTED' WHERE PC_Invoice IN ( SELECT PC_Invoice FROM AMS.PB_Master WHERE PC_Invoice IS NOT NULL AND PC_Invoice <> '') AND PC_Master.Invoice_Type <> 'POSTED' ";
        cmdString += @"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, IsNULL(SyncRowVersion,1), SyncBaseId
			FROM(SELECT 'PC' Module, pd.PC_Invoice Voucher_No, Invoice_SNo Serial_No, pm.PB_Vno PurRefVno, pm.Invoice_Date Voucher_Date, pm.Invoice_Miti Voucher_Miti, pm.Invoice_Time Voucher_Time, pm.Vendor_ID Ledger_ID, pm.Subledger_Id Subledger_Id, pm.Agent_ID, pm.Cls1 Department_ID1, pm.Cls2 Department_ID2, pm.Cls3 Department_ID3, pm.Cls4 Department_ID4, pm.Cur_Id Currency_ID, pm.Cur_Rate Currency_Rate, P_Id Product_Id, Gdn_Id Godown_Id, NULL CostCenter_Id, Alt_Qty AltQty, Alt_UnitId AltUnit_Id, Qty, Unit_Id, AltStock_Qty AltStockQty, Stock_Qty StockQty, pd.Free_Qty FreeQty, pd.Free_Unit_Id FreeUnit_Id, pd.StockFree_Qty StockFreeQty, 0 ConvRatio, pd.ExtraFree_Qty ExtraFreeQty, pd.ExtraFree_Unit_Id ExtraFreeUnit_Id, pd.ExtraStockFree_Qty ExtraStockFreeQty, pd.Rate, pd.B_Amount BasicAmt, pd.T_Amount TermAmt, pd.N_Amount NetAmt, 0 BillTermAmt, pd.V_Rate TaxRate, pm.Tbl_Amount TaxableAmt, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) DocVal, 0 ReturnVal, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) StockVal, 0 AddStockVal, pm.PB_Vno PartyInv, 'I' EntryType, pm.Auth_By AuthBy, pm.Auth_Date AuthDate, pm.Reconcile_By RecoBy, pm.Reconcile_Date RecoDate, NULL Counter_ID, NULL RoomNo, pm.CBranch_Id Branch_ID, pm.CUnit_Id CmpUnit_ID, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, 0 SalesRate, pm.FiscalYearId, pm.Enter_By EnterBy, pm.Enter_Date EnterDate, pm.SyncGlobalId, pm.SyncOriginId, pm.SyncCreatedOn, pm.SyncLastPatchedOn, pm.SyncRowVersion, pm.SyncBaseId
				 FROM AMS.PC_Details pd
					  INNER JOIN AMS.Product p ON p.PID = pd.P_Id
					  INNER JOIN AMS.PC_Master pm ON pd.PC_Invoice=pm.PC_Invoice
					  LEFT OUTER JOIN(SELECT pt.Product_Id, pt.PC_VNo, pt.SNo, SUM(CASE WHEN pt1.PT_Sign='-' THEN -pt.Amount ELSE pt.Amount END) StockValue
									  FROM AMS.PC_Term pt
										   LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
									  WHERE pt1.PT_Profitability > 0 AND pt.Term_Type<>'B' ";
        cmdString += PcMaster.PC_Invoice.IsValueExits() ? $@" AND pt.PC_VNo='{PcMaster.PC_Invoice}' " : " ";
        cmdString += @"
									GROUP BY pt.Product_Id, pt.PC_VNo, pt.SNo) AS StockValue ON StockValue.Product_Id=pd.P_Id AND StockValue.PC_VNo=pd.PC_Invoice AND pd.Invoice_SNo=StockValue.SNo
				 WHERE p.PType IN ('I','Inventory') ";
        cmdString += PcMaster.PC_Invoice.IsValueExits() ? $@" AND pm.PC_Invoice='{PcMaster.PC_Invoice}' " : " ";
        cmdString += @" 	AND pm.Invoice_Type <> 'POSTED' AND pm.R_Invoice = 0  and Action_Type <> 'CANCEL') AS Stock; ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    // PULL PURCHASE CHALLAN INVOICE
    #region ---------- PULL PURCHASE CHALLAN / CHALLAN RETURN INVOICE ----------
    private async Task<bool> PullUnSyncPurchaseChallan(IDataSyncRepository<PC_Master> purchaseChallanRepo)
    {
        try
        {
            var pullResponse = await purchaseChallanRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                //_frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                //_formBusy = false;
                //btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_purchaseDataEntry.SaveUnSyncPurchaseChallanFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseChallan(purchaseChallanRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    private async Task<bool> PullUnSyncPurchaseChallanReturn(IDataSyncRepository<PCR_Master> purchaseChallanReturnRepo)
    {
        try
        {
            var pullResponse = await purchaseChallanReturnRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                //_frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                //_formBusy = false;
                //btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_purchaseDataEntry.SaveUnSyncPurchaseChallanReturnFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseChallanReturn(purchaseChallanReturnRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    // OBJECT FOR THIS FORM
    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    public PC_Master PcMaster { get; set; }
    public List<PC_Details> DetailsList { get; set; }
    public List<PC_Term> ChallanTerms { get; set; }

    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
}