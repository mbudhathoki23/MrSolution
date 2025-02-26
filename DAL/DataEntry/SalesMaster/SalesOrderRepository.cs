using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.SalesOrder;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.SalesMaster;

public class SalesOrderRepository : ISalesOrderRepository
{
    public int SaveSalesOrder(string actionTag)
    {
        try
        {
            var cmdString = new StringBuilder();
            string[] strNamesArray = { "DELETE", "REVERSE" };
            if (strNamesArray.Contains(actionTag))
            {
                AuditLogSalesOrder(actionTag);
                if (actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($@" 
                        UPDATE AMS.SO_Master SET R_Invoice = 1, CancelBy ='{SoMaster.CancelBy}', CancelDate = GETDATE() , CancelReason = '{SoMaster.CancelReason}' WHERE SO_Invoice = '{SoMaster.SO_Invoice}'");
                }

                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append($@"
                        DELETE AMS.SO_Term WHERE SO_Vno='{SoMaster.SO_Invoice}';
                        DELETE	AMS.SO_Details WHERE SO_Invoice='{SoMaster.SO_Invoice}';
                        DELETE AMS.SO_Master WHERE	SO_Invoice = '{SoMaster.SO_Invoice}'; ");
                }
            }
            else
            {
                if (actionTag.Equals("SAVE"))
                {
                    cmdString.Append(@" 
                        INSERT INTO AMS.SO_Master(SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, CancelBy, CancelDate, CancelReason, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, MasterKeyId) ");
                    cmdString.Append($"\n VALUES \n");
                    cmdString.Append($" (N'{SoMaster.SO_Invoice}', '{SoMaster.Invoice_Date.GetSystemDate()}', N'{SoMaster.Invoice_Miti}', GETDATE(),");
                    cmdString.Append(SoMaster.Ref_Vno.IsValueExits() ? $" N'{SoMaster.Ref_Vno}'," : "NULL, ");
                    cmdString.Append(SoMaster.Ref_Date != null ? $" '{SoMaster.Ref_Date.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(SoMaster.Ref_Miti.IsValueExits() ? $" '{SoMaster.Ref_Miti}'," : "NULL,");
                    cmdString.Append($" {SoMaster.Customer_Id},");
                    cmdString.Append(SoMaster.PartyLedgerId > 0 ? $" {SoMaster.PartyLedgerId}," : "NULL,");
                    cmdString.Append(SoMaster.Party_Name.IsValueExits() ? $" N'{SoMaster.Party_Name}'," : "NULL,");
                    cmdString.Append(SoMaster.Vat_No.IsValueExits() ? $" N'{SoMaster.Vat_No}'," : "NULL,");
                    cmdString.Append(SoMaster.Contact_Person.IsValueExits() ? $" N'{SoMaster.Contact_Person}'," : "NULL,");
                    cmdString.Append(SoMaster.Mobile_No.IsValueExits() ? $" N'{SoMaster.Mobile_No}'," : "NULL,");
                    cmdString.Append(SoMaster.Address.IsValueExits() ? $" N'{SoMaster.Address}'," : "NULL,");
                    cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" N'{SoMaster.ChqNo}'," : "NULL,");
                    cmdString.Append(SoMaster.ChqDate != null ? $" N'{SoMaster.ChqDate.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(SoMaster.ChqMiti.IsValueExits() ? $" N'{SoMaster.ChqMiti}'," : "NULL,");
                    cmdString.Append($"N'{SoMaster.Invoice_Type}', N'{SoMaster.Invoice_Mode}', '{SoMaster.Payment_Mode}', {SoMaster.DueDays},");
                    cmdString.Append(SoMaster.DueDate != null ? $" '{SoMaster.DueDate.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(SoMaster.Agent_Id.GetInt() > 0 ? $" {SoMaster.Agent_Id}," : "NULL,");
                    cmdString.Append(SoMaster.Subledger_Id.GetInt() > 0 ? $" {SoMaster.Subledger_Id}," : "NULL,");
                    cmdString.Append(SoMaster.IND_Invoice.IsValueExits() ? $" '{SoMaster.IND_Invoice}'," : "NULL,");
                    cmdString.Append(SoMaster.IND_Date != null ? $" '{SoMaster.IND_Date.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(SoMaster.QOT_Invoice.IsValueExits() ? $" '{SoMaster.QOT_Invoice}'," : "NULL,");
                    cmdString.Append(SoMaster.QOT_Date != null ? $" '{SoMaster.QOT_Date.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(SoMaster.Cls1 > 0 ? $" {SoMaster.Cls1}," : "NULL,");
                    cmdString.Append("NULL,NULL,NULL,");
                    cmdString.Append(SoMaster.CounterId.GetInt() > 0 ? $" {SoMaster.CounterId}," : "NULL,");
                    cmdString.Append(SoMaster.TableId.GetInt() > 0 ? $" {SoMaster.TableId}," : "NULL,");
                    cmdString.Append("NULL,1,");
                    cmdString.Append(SoMaster.Cur_Id > 0 ? $" {SoMaster.Cur_Id}," : "1,");
                    cmdString.Append($" {SoMaster.Cur_Rate.GetDecimal(true)},{SoMaster.B_Amount.GetDecimal()},{SoMaster.T_Amount.GetDecimal()},{SoMaster.N_Amount.GetDecimal()},{SoMaster.LN_Amount.GetDecimal()},");
                    cmdString.Append($" {SoMaster.V_Amount.GetDecimal()},{SoMaster.Tbl_Amount.GetDecimal()},{SoMaster.Tender_Amount.GetDecimal()}, {SoMaster.Return_Amount.GetDecimal()},");
                    cmdString.Append($" N'{SoMaster.Action_Type}',");
                    cmdString.Append(SoMaster.In_Words.IsValueExits() ? $" N'{SoMaster.In_Words}'," : "NULL,");
                    cmdString.Append(SoMaster.Remarks.IsValueExits() ? $" N'{SoMaster.Remarks.GetTrimReplace()}'," : "NULL,");
                    cmdString.Append($"0,NULL,NULL,NULL,0,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,");
                    cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                    cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                    cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                    cmdString.Append($"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,GETDATE(),{SoMaster.SyncRowVersion.GetDecimal(true)},{SoMaster.MasterKeyId}); \n");
                }
                else if (actionTag.Equals("UPDATE"))
                {
                    cmdString.Append($@" 
                        UPDATE AMS.SO_Master SET   Invoice_Date ='{SoMaster.Invoice_Date:yyyy-MM-dd}',Invoice_Miti = N'{SoMaster.Invoice_Miti}',");
                    cmdString.Append(SoMaster.Ref_Vno.IsValueExits() ? $" Ref_Vno = N'{SoMaster.Ref_Vno}', Ref_Date = '{SoMaster.Ref_Date:yyyy-MM-dd}',Ref_Miti ='{SoMaster.Ref_Miti}'," : "Ref_Vno = NULL,Ref_Date = NULL,Ref_Miti = NULL,");
                    cmdString.Append($" Customer_Id= {SoMaster.Customer_Id},");
                    cmdString.Append(SoMaster.PartyLedgerId > 0 ? $" PartyLedgerId = {SoMaster.PartyLedgerId}," : "PartyLedgerId = NULL,");
                    cmdString.Append(SoMaster.Party_Name.IsValueExits() ? $"Party_Name =  N'{SoMaster.Party_Name}'," : "Party_Name = NULL,");
                    cmdString.Append(SoMaster.Vat_No.IsValueExits() ? $" Vat_No = N'{SoMaster.Vat_No}'," : "Vat_No = NULL,");
                    cmdString.Append(SoMaster.Contact_Person.IsValueExits() ? $" Contact_Person = N'{SoMaster.Contact_Person}'," : "Contact_Person = NULL,");
                    cmdString.Append(SoMaster.Mobile_No.IsValueExits() ? $" Mobile_No = N'{SoMaster.Mobile_No}'," : "Mobile_No =NULL,");
                    cmdString.Append(SoMaster.Address.IsValueExits() ? $" Address = N'{SoMaster.Address}'," : "Address = NULL,");
                    cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SoMaster.ChqNo}'," : "ChqNo = NULL,");
                    cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" ChqDate = N'{SoMaster.ChqDate:yyyy-MM-dd}'," : "ChqDate =NULL,");
                    cmdString.Append(SoMaster.ChqNo.IsValueExits() ? $" ChqMiti = N'{SoMaster.ChqMiti}'," : "ChqMiti = NULL,");
                    cmdString.Append($"Invoice_Type = N'{SoMaster.Invoice_Type}', Invoice_Mode = N'{SoMaster.Invoice_Mode}', Payment_Mode ='{SoMaster.Payment_Mode}', DueDays = {SoMaster.DueDays.GetInt()},");
                    cmdString.Append(SoMaster.DueDays > 0 ? $" DueDate = '{SoMaster.DueDate:yyyy-MM-dd}'," : "DueDate = NULL,");
                    cmdString.Append(SoMaster.Agent_Id > 0 ? $" Agent_Id = {SoMaster.Agent_Id}," : "Agent_Id =NULL,");
                    cmdString.Append(SoMaster.Subledger_Id > 0 ? $" Subledger_Id = {SoMaster.Subledger_Id}," : "Subledger_Id = NULL,");
                    cmdString.Append(SoMaster.IND_Invoice.IsValueExits() ? $" IND_Invoice = '{SoMaster.IND_Invoice}'," : "IND_Invoice = NULL,");
                    cmdString.Append(SoMaster.IND_Invoice.IsValueExits() ? $" IND_Date = '{SoMaster.IND_Date.GetSystemDate()}'," : "IND_Date = NULL,");
                    cmdString.Append(SoMaster.QOT_Invoice.IsValueExits() ? $" QOT_Invoice = '{SoMaster.QOT_Invoice}'," : "QOT_Invoice = NULL,");
                    cmdString.Append(SoMaster.QOT_Invoice.IsValueExits() ? $" QOT_Date = '{SoMaster.QOT_Date.GetSystemDate()}'," : "QOT_Date = NULL,");
                    cmdString.Append(SoMaster.Cls1 > 0 ? $" Cls1 = {SoMaster.Cls1}," : "Cls1 = NULL,");
                    cmdString.Append(SoMaster.CounterId > 0 ? $" CounterId = {SoMaster.CounterId}," : "CounterId = NULL,");
                    cmdString.Append(SoMaster.TableId > 0 ? $" TableId = {SoMaster.TableId}," : "TableId = NULL,");
                    cmdString.Append(SoMaster.Cur_Id > 0 ? $" Cur_Id = {SoMaster.Cur_Id}," : "Cur_Id = 1,");
                    cmdString.Append($" Cur_Rate = {SoMaster.Cur_Rate.GetDecimal(true)},B_Amount = {SoMaster.B_Amount.GetDecimal()},T_Amount = {SoMaster.T_Amount.GetDecimal()},N_Amount = {SoMaster.N_Amount.GetDecimal()},LN_Amount = {SoMaster.LN_Amount.GetDecimal()},");
                    cmdString.Append($" V_Amount = {SoMaster.V_Amount.GetDecimal()},Tbl_Amount = {SoMaster.Tbl_Amount.GetDecimal()},Tender_Amount = {SoMaster.Tender_Amount.GetDecimal()}, Return_Amount = {SoMaster.Return_Amount.GetDecimal()},");
                    cmdString.Append($" Action_Type = N'{SoMaster.Action_Type}',");
                    cmdString.Append(SoMaster.In_Words.IsValueExits() ? $" In_Words = N'{SoMaster.In_Words}'," : "In_Words = NULL,");
                    cmdString.Append(SoMaster.Remarks.IsValueExits() ? $"Remarks =  N'{SoMaster.Remarks.GetTrimReplace()}' " : "Remarks = NULL");
                    cmdString.Append($" WHERE SO_Invoice ='{SoMaster.SO_Invoice}' ; ");
                }

                if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
                {
                    if (DetailsList.Count > 0)
                    {
                        if (actionTag.Equals("UPDATE"))
                        {
                            cmdString.Append($"\n DELETE AMS.SO_Details WHERE SO_Invoice='{SoMaster.SO_Invoice}' \n");
                        }
                        cmdString.Append(@" 
                            INSERT INTO AMS.SO_Details(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, MasterKeyId, DisplayName) ");
                        cmdString.Append("\n VALUES \n");

                        foreach (var dr in DetailsList)
                        {
                            var index = DetailsList.IndexOf(dr);
                            cmdString.Append($"('{dr.SO_Invoice}', {dr.Invoice_SNo}, {dr.P_Id},");
                            cmdString.Append(dr.Gdn_Id > 0 ? $@"{dr.Gdn_Id}," : "NULL,");
                            cmdString.Append($"{dr.Alt_Qty},");
                            cmdString.Append(dr.Alt_UnitId > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                            cmdString.Append($"{dr.Qty}, {dr.Unit_Id}, {dr.Rate}, {dr.B_Amount}, {dr.T_Amount}, {dr.N_Amount},");
                            cmdString.Append($"{dr.AltStock_Qty}, {dr.Stock_Qty},");
                            cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
                            cmdString.Append(dr.IND_Invoice.IsValueExits() ? $"'{dr.SO_Invoice}', {dr.IND_Sno}, " : "NULL,0,");
                            cmdString.Append(dr.QOT_Invoice.IsValueExits() ? $"'{dr.QOT_Invoice}', {dr.QOT_SNo}," : "NULL,0,");
                            cmdString.Append($"{dr.Tax_Amount},{dr.V_Amount} , {dr.V_Rate}, ");
                            cmdString.Append($"{dr.Issue_Qty.GetDecimal()},");
                            cmdString.Append(dr.Free_Unit_Id > 0 ? $"{dr.Free_Unit_Id}," : "NULL,");
                            cmdString.Append($"{dr.Free_Qty}, {dr.StockFree_Qty},");
                            cmdString.Append(dr.ExtraFree_Unit_Id > 0 ? $"{dr.ExtraFree_Unit_Id}," : "NULL,");
                            cmdString.Append($"{dr.ExtraFree_Qty}, {dr.ExtraStockFree_Qty}, CAST('{dr.T_Product}' AS BIT), ");
                            cmdString.Append(dr.S_Ledger > 0 ? $"{dr.S_Ledger}," : "NULL,");
                            cmdString.Append(dr.SR_Ledger > 0 ? $"{dr.SR_Ledger}," : "NULL,");
                            cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                            cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                            cmdString.Append($"{dr.PDiscountRate}, {dr.PDiscount}, {dr.BDiscountRate}, {dr.BDiscount}, {dr.ServiceChargeRate}, {dr.ServiceCharge},");
                            cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'," : "NULL,");
                            cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                            cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                            cmdString.Append($"'{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}',");
                            cmdString.Append($"{dr.SyncRowVersion},");
                            cmdString.Append($"{SoMaster.MasterKeyId},");
                            cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'" : "NULL");

                            cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");

                        }

                        if (Terms.Count > 0)
                        {
                            if (actionTag.Equals("UPDATE"))
                            {
                                cmdString.Append($"\n DELETE AMS.SO_Term WHERE SO_Vno='{SoMaster.SO_Invoice}' AND Term_Type='B' \n");
                            }
                            cmdString.Append(@"
                                INSERT INTO AMS.SO_Term(SO_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                            cmdString.Append($"\n VALUES \n");

                            foreach (var dr in Terms)
                            {
                                var index = Terms.IndexOf(dr);
                                cmdString.Append($" ('{dr.SO_Vno}',");
                                cmdString.Append($"{dr.ST_Id},");
                                cmdString.Append(dr.SNo.GetInt() > 0 ? $"{dr.SNo}," : "1,");
                                cmdString.Append($"N'{dr.Term_Type}',");
                                cmdString.Append(dr.Product_Id.GetLong() > 0 ? $"{dr.Product_Id.GetLong()}," : "NULL,");
                                cmdString.Append($"{dr.Rate.GetDecimal()},{dr.Amount.GetDecimal()},");
                                cmdString.Append($"N'{dr.Taxable}',");
                                cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'," : "NULL,");
                                cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                                cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                                cmdString.Append($"'{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}',");
                                cmdString.Append($"{dr.SyncRowVersion}");
                                cmdString.Append(index == Terms.Count - 1 ? "); \n" : "),\n");
                            }
                        }
                    }

                }

            }

            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            if (ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId != null)
            {
                Task.Run(() => SyncSalesOrderAsync(actionTag));
            }

            AuditLogSalesOrder(actionTag);
            if (SoMaster.TableId > 0 && actionTag.Equals("SAVE"))
            {
                var cmdText = $"UPDATE AMS.TableMaster SET TableStatus ='O' WHERE TableId = {SoMaster.TableId}";
                var result = SqlExtensions.ExecuteNonQuery(cmdText);
            }

            if (SoMaster.TableId > 0 && strNamesArray.Contains(actionTag))
            {
                var cmdText = $"UPDATE AMS.TableMaster SET TableStatus ='A' WHERE TableId = {SoMaster.TableId}";
                var result = SqlExtensions.ExecuteNonQuery(cmdText);
            }

            if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
            {
                return iResult;
            }

            _ = SalesOrderTermPosting();
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
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
    public int UpdateImageOnSales(string module)
    {
        var isUpdate = 0;
        var cmdString = new StringBuilder();


        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            conn.Open();
            if (SoMaster.PAttachment1 != null && SoMaster.PAttachment1.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment1 = @PImage1 WHERE {module}_Invoice = '{SoMaster.SO_Invoice}'  \n");
                using var cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SoMaster.PAttachment1 ?? Array.Empty<byte>();
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SoMaster.PAttachment2 != null && SoMaster.PAttachment2.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment2 = @PImage1 WHERE {module}_Invoice = '{SoMaster.SO_Invoice}'  \n");
                using var cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SoMaster.PAttachment2 ?? Array.Empty<byte>();
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SoMaster.PAttachment3 != null && SoMaster.PAttachment3.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment3 = @PImage1 WHERE {module}_Invoice = '{SoMaster.SO_Invoice}'  \n");
                using var cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SoMaster.PAttachment3 ?? Array.Empty<byte>();
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SoMaster.PAttachment4 != null && SoMaster.PAttachment4.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment4 = @PImage1 WHERE {module}_Invoice = '{SoMaster.SO_Invoice}'  \n");
                using var cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SoMaster.PAttachment4 ?? Array.Empty<byte>();
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SoMaster.PAttachment5 != null && SoMaster.PAttachment5.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment5 = @PImage1 WHERE {module}_Invoice = '{SoMaster.SO_Invoice}'  \n");
                using var cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SoMaster.PAttachment5 ?? Array.Empty<byte>();
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

    public async Task<int> SyncSalesOrderAsync(string actionTag)
    {
        //sync
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item1 == null)
            {
                return 1;
            }
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}SalesOrder/GetAllSalesOrder",
                InsertUrl = @$"{_configParams.Model.Item2}SalesOrder/InsertSalesOrderList",
                UpdateUrl = @$"{_configParams.Model.Item2}SalesOrder/UpdateSalesOrderList",
                DeleteUrl = $@"{_configParams.Model.Item2}SalesOrder?id='{SoMaster.SO_Invoice}'"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var sqlQuery = @"SELECT *FROM AMS.SO_Master WHERE ISNULL(IsSynced,0)=0";
            var queryResponse = await QueryUtils.GetListAsync<SO_Master>(sqlQuery);
            var salesOrderRepo = DataSyncProviderFactory.GetRepository<SO_Master>(_injectData);

            var saList = queryResponse.List.ToList();
            if (saList.Count > 0)
            {
                var loopCount = 1;
                if (saList.Count > 100)
                {
                    loopCount = saList.Count / 100 + 1;
                }
                var newSaList = new List<SO_Master>();
                var cmdString = new StringBuilder();
                var salesList = new List<SO_Master>();
                for (var i = 0; i < loopCount; i++)
                {
                    newSaList.Clear();
                    newSaList.AddRange(i == 0 ? saList.Take(100) : saList.Skip(100 * i).Take(100));
                    salesList.Clear();
                    foreach (var sa in newSaList)
                    {
                        sqlQuery = $@"SELECT *FROM AMS.SO_Details WHERE SO_Invoice='{sa.SO_Invoice}'";
                        var dtlQueryResponse = await QueryUtils.GetListAsync<SO_Details>(sqlQuery);
                        var sadList = dtlQueryResponse.List.ToList();

                        sqlQuery = $@"SELECT *FROM AMS.SO_Term WHERE SO_Vno='{sa.SO_Invoice}'";
                        var pcTermQueryResponse = await QueryUtils.GetListAsync<SO_Term>(sqlQuery);
                        var saTermList = pcTermQueryResponse.List.ToList();

                        var sbMaster = new SO_Master();
                        sbMaster = sa;
                        sbMaster.DetailsList = sadList;
                        sbMaster.Terms = saTermList;
                        salesList.Add(sbMaster);
                    }
                    SplashScreenManager.ShowForm(typeof(PleaseWait));
                    var pushResponse = await salesOrderRepo.PushNewListAsync(salesList);
                    if (!pushResponse.Value)
                    {
                        SplashScreenManager.CloseForm();
                        pushResponse.ShowErrorDialog();
                        return 0;
                    }
                    else
                    {
                        try
                        {
                            cmdString.Clear();
                            foreach (var sa in newSaList)
                            {
                                cmdString.Append($"UPDATE AMS.SO_Master SET IsSynced = 1 WHERE SO_Invoice = '{sa.SO_Invoice}'; \n");
                            }
                            var query = cmdString.ToString();
                            CreateDatabaseTable.DropTrigger();
                            await QueryUtils.ExecNonQueryAsync(query);
                            CreateDatabaseTable.CreateTrigger();
                        }
                        catch
                        {
                            CreateDatabaseTable.CreateTrigger();
                        }
                        SplashScreenManager.CloseForm();
                    }
                }
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public int SaveSalesOtherDetails(string module, string actionTag)
    {
        //var cmdString = new StringBuilder();
        //if (actionTag is "SAVE")
        //{
        //    cmdString.Append($"INSERT INTO AMS.{module}_Master_OtherDetails({module}_Invoice,");
        //    cmdString.Append(" Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails) \n");
        //    cmdString.Append(" 	VALUES \n");
        //    cmdString.Append($" (N'{SbOther.SB_Invoice}', N'{SbOther.Transport}', N'{SbOther.VechileNo}', N'{SbOther.BiltyNo}', N'{SbOther.Package}', ");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
        //        ? $" '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "Null,");
        //    cmdString.Append(
        //        $"N'{SbOther.BiltyType}', N'{SbOther.Driver}', N'{SbOther.PhoneNo}', N'{SbOther.LicenseNo}', N'{SbOther.MailingAddress}', N'{SbOther.MCity}', N'{SbOther.MState}', N'{SbOther.MCountry}', N'{SbOther.MEmail}', N'{SbOther.ShippingAddress}', N'{SbOther.SCity}', N'{SbOther.SState}', N'{SbOther.SCountry}', N'{SbOther.SEmail}', N'{SbOther.ContractNo}',");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
        //        ? $" '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "Null,");
        //    cmdString.Append($"N'{SbOther.ExportInvoice}', ");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
        //        ? $" '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "Null,");
        //    cmdString.Append($"N'{SbOther.VendorOrderNo}', N'{SbOther.BankDetails}', N'{SbOther.LcNumber}', N'{SbOther.CustomDetails}'); \n");
        //}
        //else if (actionTag is "UPDATE")
        //{
        //    cmdString.Append($" UPDATE AMS.{module}_Master_OtherDetails SET ");
        //    cmdString.Append($" Transport= N'{SbOther.Transport}', VechileNo= N'{SbOther.VechileNo}', BiltyNo = N'{SbOther.BiltyNo}', Package = N'{SbOther.Package}', ");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
        //        ? $"BiltyDate=  '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "BiltyDate= Null,");
        //    cmdString.Append($" BiltyType= N'{SbOther.BiltyType}',Driver=  N'{SbOther.Driver}', PhoneNo= N'{SbOther.PhoneNo}',LicenseNo= N'{SbOther.LicenseNo}',MailingAddress=  N'{SbOther.MailingAddress}',MCity=  N'{SbOther.MCity}',MState=  N'{SbOther.MState}',MCountry=  N'{SbOther.MCountry}',MEmail=  N'{SbOther.MEmail}',ShippingAddress=  N'{SbOther.ShippingAddress}', SCity= N'{SbOther.SCity}', SState= N'{SbOther.SState}', SCountry= N'{SbOther.SCountry}',SEmail= N'{SbOther.SEmail}', ContractNo= N'{SbOther.ContractNo}',");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
        //        ? $"ContractDate=  '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "ContractDate= Null,");
        //    cmdString.Append($"ExportInvoice= N'{SbOther.ExportInvoice}', ");
        //    cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
        //        ? $"ExportInvoiceDate=  '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "ExportInvoiceDate= Null,");
        //    cmdString.Append($" VendorOrderNo= N'{SbOther.VendorOrderNo}',BankDetails= N'{SbOther.BankDetails}',LcNumber= N'{SbOther.LcNumber}', CustomDetails= N'{SbOther.CustomDetails}' WHERE {module}_Invoice = N'{SbOther.SB_Invoice}' ; ");
        //}
        //else if (actionTag is "DELETE")
        //{
        //    cmdString.Append($" DELETE AMS.{module}_Master_OtherDetails WHERE {module}_Invoice = '{SbOther.SB_Invoice}'; ");
        //}

        //if (cmdString.Length is 0)
        //{
        //    return 0;
        //}

        var saveData = 0; //SaveDataInDatabase(cmdString);
        return saveData;
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
    public string GetSalesOrderScript(string voucherNo = "")
    {
        var cmdString = $@"SELECT * FROM AMS.SO_Master so ";
        cmdString += (!string.IsNullOrEmpty(voucherNo) ? $" WHERE so.SyncGlobalId IS NULL AND so.SO_Invoice= '{voucherNo}' " : "");
        return cmdString;
    }
    public async Task<bool> PullSalesOrderServerToClientByRowCounts(IDataSyncRepository<SO_Master> salesOrderRepository, int callCount)
    {
        try
        {
            var pullResponse = await salesOrderRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetSalesOrderScript();
            var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var order in pullResponse.List)
            {
                SoMaster = order;

                var existData = dataSetSql.Select($"SO_Invoice= '{order.SO_Invoice}'");
                if (existData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    var rowVersionId = existData[0]["SyncRowVersion"].GetShort();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (order.SyncRowVersion > rowVersionId)
                    {
                        SoMaster = order;
                        var result = SaveSalesOrder("UPDATE");
                    }
                }
                else
                {
                    var result = SaveSalesOrder("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullSalesOrderServerToClientByRowCounts(salesOrderRepository, callCount);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    // RESTRO ORDER
    #region ----------- RESTRO ORDER ------------
    public int UpdateRestroOrder(bool isQty = false)
    {
        var cmdString = new StringBuilder();
        foreach (var details in DetailsList)
        {
            if (isQty)
            {
                cmdString.Append(@$"
                    UPDATE AMS.SO_Details SET Qty = '{details.Qty}', Rate = '{details.Rate}', B_Amount= '{details.B_Amount}', T_Amount ='{details.T_Amount}', N_Amount = '{details.N_Amount}',PDiscountRate='{details.PDiscountRate}',PDiscount = '{details.PDiscount}',SyncRowVersion={details.SyncRowVersion},SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() WHERE SO_Invoice = '{details.SO_Invoice}' AND P_Id = '{details.P_Id}' AND Invoice_SNo = '{details.Invoice_SNo}' ");
            }
            else
            {
                cmdString.Append(@" 
                    INSERT INTO AMS.SO_Details(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, MasterKeyId, DisplayName) ");
                cmdString.Append("\n VALUES \n");

                foreach (var dr in DetailsList)
                {
                    var index = DetailsList.IndexOf(dr);
                    cmdString.Append($"('{dr.SO_Invoice}', {dr.Invoice_SNo}, {dr.P_Id},");
                    cmdString.Append(dr.Gdn_Id > 0 ? $@"{dr.Gdn_Id}," : "NULL,");
                    cmdString.Append($"{dr.Alt_Qty},");
                    cmdString.Append(dr.Alt_UnitId > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                    cmdString.Append($"{dr.Qty}, {dr.Unit_Id}, {dr.Rate}, {dr.B_Amount}, {dr.T_Amount}, {dr.N_Amount},");
                    cmdString.Append($"{dr.AltStock_Qty}, {dr.Stock_Qty},");
                    cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
                    cmdString.Append(dr.IND_Invoice.IsValueExits() ? $"'{dr.SO_Invoice}', {dr.IND_Sno}, " : "NULL,0,");
                    cmdString.Append(dr.QOT_Invoice.IsValueExits() ? $"'{dr.QOT_Invoice}', {dr.QOT_SNo}," : "NULL,0,");
                    cmdString.Append($"{dr.Tax_Amount},{dr.V_Amount} , {dr.V_Rate}, ");
                    cmdString.Append($"{dr.Issue_Qty.GetDecimal()},");
                    cmdString.Append(dr.Free_Unit_Id > 0 ? $"{dr.Free_Unit_Id}," : "NULL,");
                    cmdString.Append($"{dr.Free_Qty}, {dr.StockFree_Qty},");
                    cmdString.Append(dr.ExtraFree_Unit_Id > 0 ? $"{dr.ExtraFree_Unit_Id}," : "NULL,");
                    cmdString.Append($"{dr.ExtraFree_Qty}, {dr.ExtraStockFree_Qty}, CAST('{dr.T_Product}' AS BIT), ");
                    cmdString.Append(dr.S_Ledger > 0 ? $"{dr.S_Ledger}," : "NULL,");
                    cmdString.Append(dr.SR_Ledger > 0 ? $"{dr.SR_Ledger}," : "NULL,");
                    cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                    cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                    cmdString.Append($"{dr.PDiscountRate}, {dr.PDiscount}, {dr.BDiscountRate}, {dr.BDiscount}, {dr.ServiceChargeRate}, {dr.ServiceCharge},");
                    cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'," : "NULL,");
                    cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                    cmdString.Append($"'{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}',");
                    cmdString.Append($"{dr.SyncRowVersion},");
                    cmdString.Append($"{dr.MasterKeyId},");
                    cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'" : "NULL");

                    cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");

                }
            }

        }
        ;


        var result = SaveDataInDatabase(cmdString);
        return result;
    }

    public int UpdateRestaurantOrderCancel()
    {
        var cmdString = new StringBuilder();
        foreach (var details in DetailsList)
        {
            cmdString.Append(@"
                INSERT INTO AMS.OrderCancelDetails(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append($" \n VALUES('{details.SO_Invoice}', {details.Invoice_SNo}, {details.P_Id},");
            cmdString.Append(details.Gdn_Id > 0 ? $" {details.Gdn_Id}," : "NULL,");
            cmdString.Append(details.Alt_Qty > 0 ? $"{details.Alt_Qty}," : "0,");
            cmdString.Append(details.Alt_UnitId > 0 ? $" {details.Alt_UnitId}," : "NULL,");
            cmdString.Append(details.Qty > 0 ? $" {details.Qty}," : "1,");
            cmdString.Append(details.Unit_Id > 0 ? $"{details.Unit_Id} ," : "NULL,");
            cmdString.Append($"{details.Rate}, {details.B_Amount}, {details.T_Amount}, {details.N_Amount}, {details.AltStock_Qty}, {details.Stock_Qty},");
            cmdString.Append(details.Narration.IsValueExits() ? $" '{details.Narration}'," : "NULL,");
            cmdString.Append(@" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
            cmdString.Append($" {details.PDiscountRate}, {details.PDiscount}, {details.BDiscountRate}, {details.BDiscount}, {details.ServiceChargeRate}, {details.ServiceCharge},");
            cmdString.Append(@" NULL, NULL, NULL, NULL, NULL, 1); ");
        }

        var result = SaveDataInDatabase(cmdString);
        return result;
    }

    public int SaveRestroOrderCancel(string actionTag)
    {
        try
        {
            var cmdString = new StringBuilder();
            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(@" 
                    INSERT INTO AMS.OrderCancelMaster(SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, CancelBy, CancelDate, CancelReason, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                cmdString.Append($"\n VALUES (N'{SoMaster.SO_Invoice}', '{SoMaster.Invoice_Date:yyyy-MM-dd}', N'{SoMaster.Invoice_Miti}', GETDATE(),");
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


                foreach (var details in DetailsList)
                {
                    cmdString.Append(@"
                        INSERT INTO AMS.OrderCancelDetails(SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append($" \n VALUES('{details.SO_Invoice}', {details.Invoice_SNo}, {details.P_Id},");
                    cmdString.Append(details.Gdn_Id > 0 ? $" {details.Gdn_Id}," : "NULL,");
                    cmdString.Append(details.Alt_Qty > 0 ? $"{details.Alt_Qty}," : "0,");
                    cmdString.Append(details.Alt_UnitId > 0 ? $" {details.Alt_UnitId}," : "NULL,");
                    cmdString.Append(details.Qty > 0 ? $" {details.Qty}," : "1,");
                    cmdString.Append(details.Unit_Id > 0 ? $"{details.Unit_Id} ," : "NULL,");
                    cmdString.Append($"{details.Rate}, {details.B_Amount}, {details.T_Amount}, {details.N_Amount}, {details.AltStock_Qty}, {details.Stock_Qty},");
                    cmdString.Append(details.Narration.IsValueExits() ? $" '{details.Narration}'," : "NULL,");
                    cmdString.Append(@" NULL, NULL, NULL, NULL, 0, 0, 0, 0, NULL, 0, 0, NULL, 0, 0,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,GETDATE(), NULL, 0, NULL,");
                    cmdString.Append($" {details.PDiscountRate}, {details.PDiscount}, {details.BDiscountRate}, {details.BDiscount}, {details.ServiceChargeRate}, {details.ServiceCharge},");
                    cmdString.Append(@" NULL, NULL, NULL, NULL, NULL, 1); ");
                }
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
    #endregion
    //AUDIT LOG OF SALES
    private int AuditLogSalesOrder(string actionTag)
    {
        var cmdString = @$"
		    INSERT INTO [AUD].[AUDIT_SO_Master] ([SO_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_Vno] ,[Ref_Date] ,[Ref_Miti] ,[Customer_Id] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_Id] ,[Subledger_Id] ,[IND_Invoice] ,[IND_Date] ,[QOT_Invoice] ,[QOT_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[TableId] ,[CombineTableId] ,[NoOfPerson] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId] ,[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		    SELECT [SO_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_VNo] ,[Ref_Date] ,[Ref_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_ID] ,[Subledger_Id] ,[IND_Invoice] ,[IND_Date] ,[QOT_Invoice] ,[QOT_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[TableId] ,[CombineTableId] ,[NoOfPerson] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date] ,[CBranch_Id], [CUnit_Id], [FiscalYearId], [Audit_Lock] , [Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,  [PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] , '{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SO_Master] where SO_Invoice = '{SoMaster.SO_Invoice}';  ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
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
    public SO_Master SoMaster { get; set; } = new();
    public List<SO_Details> DetailsList { get; set; } = new();
    public List<SO_Term> Terms { get; set; } = new();
    public SO_Master_OtherDetails SoOther { get; set; } = new();

    private readonly DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}