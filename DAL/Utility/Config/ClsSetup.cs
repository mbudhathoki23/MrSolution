using DatabaseModule.Setup.IrdConfig;
using DatabaseModule.Setup.PrintSetting;
using DatabaseModule.Setup.SystemSetting;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System.Data;
using System.Text;

namespace MrDAL.Utility.Config;

public class ClsSetup : ISetup
{
    // SETUP CONFIG

    #region --------------- SETUP ---------------

    public ClsSetup()
    {
        VmDocument = new DocumentDesignPrint();
        VmIrd = new IRDAPISetting();
    }

    #endregion --------------- SETUP ---------------

    // I_U_D_FUNCTION

    #region --------------- IUD FUNCTION ---------------

    public int SaveDocumentDesignPrint()
    {
        var cmdString = new StringBuilder();
        if (!VmDocument.ActionTag.Equals("SAVE")) return 0;
        cmdString.Append($" DELETE AMS.DocumentDesignPrint WHERE Module='{VmDocument.Module}' \n");
        cmdString.Append(@" INSERT INTO AMS.DocumentDesignPrint (DDP_Id, Module, Paper_Name, Is_Online, NoOfPrint, Notes, DesignerPaper_Name, Created_By, Created_Date, Status, Branch_Id, CmpUnit_Id) ");
        cmdString.Append(" \n VALUES ");
        if (VmDocument.SGridView.RowCount > 0)
        {
            for (var i = 0; i < VmDocument.SGridView.RowCount; i++)
            {
                if (VmDocument.SGridView.RowCount > 1 && i > 0) VmDocument.DDP_Id += 1;
                cmdString.Append($@"({VmDocument.DDP_Id},'{VmDocument.Module}','{VmDocument.SGridView.Rows[i].Cells[0].Value}',");
                cmdString.Append($" CAST('{VmDocument.Is_Online}' AS BIT),{VmDocument.NoOfPrint},");
                cmdString.Append(VmDocument.Notes.IsValueExits() ? $" N'{VmDocument.Notes}'," : "NULL,");
                cmdString.Append($"'{VmDocument.SGridView.Rows[i].Cells[1].Value}',N'{ObjGlobal.LogInUser}',GetDate(),1,");
                cmdString.Append(VmDocument.Branch_Id > 0 ? $" {VmDocument.Branch_Id}," : $"{ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}" : "NULL");
                cmdString.Append(i == VmDocument.SGridView.RowCount - 1 ? "); \n" : "), \n");
            }
        }
        else
        {
            cmdString = new StringBuilder();
            cmdString.Append($" DELETE AMS.DocumentDesignPrint WHERE Module='{VmDocument.Module}'");
        }
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }

    public int SaveIrdApiSetting()
    {
        var cmdString = $@"
		TRUNCATE TABLE AMS.IRDAPISetting;
		INSERT INTO AMS.IRDAPISetting ([IRDAPI], [IrdUser], [IrdUserPassword], [IrdCompanyPan], [IsIRDRegister])
		VALUES(N'{VmIrd.IRDAPI}',N'{VmIrd.IrdUser}',N'{VmIrd.IrdUserPassword}',N'{VmIrd.IrdCompanyPan}',{VmIrd.IsIRDRegister.GetInt()}); ";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);
        if (result == 0)
        {
            return result;
        }

        cmdString = $"UPDATE AMS.CompanyInfo SET IsTaxRegister=CAST('{VmIrd.IsIRDRegister}' AS BIT);";
        result = SqlExtensions.ExecuteNonQuery(cmdString);
        if (result != 0)
        {
            ObjGlobal.IsIrdRegister = VmIrd.IsIRDRegister > 0;
        }
        return result;
    }

    public int GenerateSqlAuditLog(string location)
    {
        var cmdString = $@"
		CREATE SERVER AUDIT [{ObjGlobal.InitialCatalog}_Ird_Sql_Audit]
        TO FILE
        (	FILEPATH = N'{location}'
	        ,MAXSIZE = 0 MB
	        ,MAX_ROLLOVER_FILES = 2147483647
	        ,RESERVE_DISK_SPACE = OFF
        )
        WITH
        (	QUEUE_DELAY = 1000
	        ,ON_FAILURE = CONTINUE
        ) ";
        var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);

        cmdString = @$"
        CREATE DATABASE AUDIT SPECIFICATION [{ObjGlobal.InitialCatalog}_Database_Sql_Audit]
        FOR SERVER AUDIT [{ObjGlobal.InitialCatalog}_Ird_Sql_Audit]
        ADD (DELETE ON DATABASE::{ObjGlobal.InitialCatalog} BY [dbo]),
        ADD (EXECUTE ON DATABASE::{ObjGlobal.InitialCatalog} BY [dbo]),
        ADD (INSERT ON DATABASE::{ObjGlobal.InitialCatalog} BY [dbo]),
        ADD (SELECT ON DATABASE::{ObjGlobal.InitialCatalog} BY [dbo]),
        ADD (UPDATE ON DATABASE::{ObjGlobal.InitialCatalog} BY [dbo])";
        result = SqlExtensions.ExecuteNonTrans(cmdString);
        return result;
        ;
    }

    public int GetReturnMaxValue(string table)
    {
        var tableId = table switch
        {
            "DocumentDesignPrint" => "DDP_Id",
            _ => string.Empty
        };
        var cmdString = $"SELECT MAX(ISNULL({tableId},0)) +1 MaxId FROM AMS.{table}";
        var dtMax = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtMax.Rows.Count > 0 ? dtMax.Rows[0].GetInt() : 0;
    }

    #endregion --------------- IUD FUNCTION ---------------

    // RETURN VALUE IN DATA TABLE

    #region --------------- Return Value in DataTable ---------------

    public DataTable GetSystemSetting()
    {
        const string cmdString = @"
		SELECT ss.SyId, ss.EnglishDate, ss.AuditTrial, ss.UDF, ss.Autopoplist, ss.CurrentDate, ss.ConformSave, ss.ConformCancel, ss.ConformExits, ss.CurrencyRate,c.CRate, ss.CurrencyId,c.Ccode, ss.DefaultPrinter, ss.AmountFormat, ss.RateFormat, ss.QtyFormat, ss.CurrencyFormatF, ss.DefaultFiscalYearId,fy.BS_FY, ss.DefaultOrderPrinter, ss.DefaultInvoicePrinter, ss.DefaultOrderNumbering, ss.DefaultInvoiceNumbering, ss.DefaultAvtInvoiceNumbering, ss.DefaultOrderDesign, ss.IsOrderPrint, ss.DefaultInvoiceDesign, ss.IsInvoicePrint, ss.DefaultAvtDesign, ss.DefaultFontsName, ss.DefaultFontsSize, ss.DefaultPaperSize, ss.DefaultReportStyle, ss.DefaultPrintDateTime, ss.DefaultFormColor, ss.DefaultTextColor, ss.DebtorsGroupId,debtor.GrpName DebtorsGroup, ss.CreditorGroupId,creditor.GrpName CreditorGroup, ss.SalaryLedgerId,salary.GLName SalaryLedger, ss.TDSLedgerId,tds.GLName TDSLedger, ss.PFLedgerId,pf.GLName PFLedger, ss.DefaultEmail, ss.DefaultEmailPassword, ss.BackupDays, ss.BackupLocation,ss.IsNightAudit,ss.EndTime,ss.SearchAlpha,ss.BarcodeAutoSearch
		FROM AMS.SystemSetting ss
			 LEFT OUTER JOIN AMS.AccountGroup debtor ON debtor.GrpId = ss.DebtorsGroupId
			 LEFT OUTER JOIN AMS.AccountGroup creditor ON creditor.GrpId = ss.CreditorGroupId
			 LEFT OUTER JOIN AMS.GeneralLedger salary ON salary.GLID = ss.SalaryLedgerId
			 LEFT OUTER JOIN AMS.GeneralLedger pf ON pf.GLID = ss.PFLedgerId
			 LEFT OUTER JOIN AMS.GeneralLedger tds ON tds.GLID = ss.TDSLedgerId
			 LEFT OUTER JOIN AMS.Currency c ON c.CId = ss.CurrencyId
			 LEFT OUTER JOIN AMS.FiscalYear fy ON fy.FY_Id = ss.DefaultFiscalYearId";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetIrdApiSetting()
    {
        const string cmdString = @"
            SELECT i.IRDAPI, i.IrdUser, i.IrdUserPassword, i.IrdCompanyPan, i.IsIRDRegister
            FROM AMS.IRDAPISetting i";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPurchaseSetting()
    {
        const string cmdString = @"
			SELECT ps.PurId, ps.PBLedgerId,pb.GLName PBLedger, ps.PRLedgerId,pr.GLName PRLedger, ps.PBVatTerm,vat.PT_Name VatTerm, ps.PBDiscountTerm,dis.PT_Name DiscountTerm, ps.PBProductDiscountTerm,pdis.PT_Name ProductDiscountTerm, ps.PBAdditionalTerm,addvat.PT_Name AdditionalVatTerm, ps.PBDateChange, ps.PBCreditDays, ps.PBCreditLimit, ps.PBCarryRate, ps.PBChangeRate, ps.PBLastRate, ps.POEnable, ps.POMandetory, ps.PCEnable, ps.PCMandetory, ps.PBSublegerEnable, ps.PBSubledgerMandetory, ps.PBAgentEnable, ps.PBAgentMandetory, ps.PBDepartmentEnable, ps.PBDepartmentMandetory, ps.PBCurrencyEnable, ps.PBCurrencyMandetory, ps.PBCurrencyRateChange, ps.PBGodownEnable, ps.PBGodownMandetory, ps.PBAlternetUnitEnable, ps.PBIndent, ps.PBNarration, ps.PBBasicAmount, ps.PBRemarksEnable, ps.PBRemarksMandatory
			FROM AMS.PurchaseSetting ps
			    LEFT OUTER JOIN AMS.GeneralLedger pb ON pb.GLID = ps.PBLedgerId
			    LEFT OUTER JOIN AMS.GeneralLedger pr ON pr.GLID = ps.PRLedgerId
			    LEFT OUTER JOIN AMS.PT_Term  vat ON vat.PT_ID = ps.PBVatTerm
			    LEFT OUTER JOIN AMS.PT_Term  dis ON dis.PT_ID = ps.PBDiscountTerm
			    LEFT OUTER JOIN AMS.PT_Term  pdis ON pdis.PT_ID = ps.PBProductDiscountTerm
			    LEFT OUTER JOIN AMS.PT_Term  addvat ON addvat.PT_ID = ps.PBAdditionalTerm";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesSetting()
    {
        const string cmdString = @"
			SELECT sb.GLName SBLedger,sr.GLName SRLedger, vat.ST_Name VatTerm, dis.ST_Name DiscountTerm, pdis.ST_Name ProductDiscountTerm,  addvat.ST_Name AddtionalVatTerm,  sDis.ST_Name ServiceChargeTerm, ss.*
            FROM AMS.SalesSetting ss
	            LEFT OUTER JOIN AMS.GeneralLedger sb ON sb.GLID=ss.SBLedgerId
	            LEFT OUTER JOIN AMS.GeneralLedger sr ON sr.GLID=ss.SRLedgerId
	            LEFT OUTER JOIN AMS.ST_Term vat ON vat.ST_ID=ss.SBVatTerm
	            LEFT OUTER JOIN AMS.ST_Term dis ON dis.ST_ID=ss.SBDiscountTerm
	            LEFT OUTER JOIN AMS.ST_Term pdis ON pdis.ST_ID=ss.SBProductDiscountTerm
	            LEFT OUTER JOIN AMS.ST_Term addvat ON addvat.ST_ID=ss.SBAdditionalTerm
	            LEFT OUTER JOIN AMS.ST_Term sDis ON sDis.ST_ID=ss.SBServiceCharge;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetInventorySetting()
    {
        const string cmdString = @"
			SELECT op.GLName OPLedger,pl.GLName PLLedger,bs.GLName BSLedger, ss.*
            FROM AMS.InventorySetting ss
	            LEFT OUTER JOIN AMS.GeneralLedger op ON op.GLID=ss.OPLedgerId
	            LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=ss.CSPLLedgerId
	            LEFT OUTER JOIN AMS.GeneralLedger bs ON bs.GLID=ss.CSBSLedgerId;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetFinanceSetting()
    {
        const string cmdString = @"
			SELECT  pl.GLName PlLedger, cash.GLName CashLedger,  vat.GLName VatLedger,pdc.GLName PDCLedger, fs.*
            FROM AMS.FinanceSetting fs
	            LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=fs.ProfiLossId
	            LEFT OUTER JOIN AMS.GeneralLedger cash ON cash.GLID=fs.CashId
	            LEFT OUTER JOIN AMS.GeneralLedger vat ON vat.GLID=fs.VATLedgerId
	            LEFT OUTER JOIN AMS.GeneralLedger pdc ON pdc.GLID=fs.PDCBankLedgerId;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPaymentSetting()
    {
        const string cmdString = @"
			SELECT p.PaymentId, p.CashLedgerId,cash.GLName CashLedger, p.CardLedgerId,card.GLName CardLedger, p.BankLedgerId,bank.GLName BankLedger, p.PhonePayLedgerId,phonePay.GLName PhonePayLedger, p.EsewaLedgerId,esewa.GLName EsewaLedger, p.KhaltiLedgerId,khalti.GLName KhaltiLedger, p.RemitLedgerId,remit.GLName RemitLedger, p.ConnectIpsLedgerId,ips.GLName IpsLedger,p.PartialLedgerId, par.GLName PartialLedger
			FROM AMS.PaymentSetting p
				LEFT OUTER JOIN AMS.GeneralLedger cash ON cash.GLID=p.CashLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger card ON card.GLID=p.CardLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger bank ON bank.GLID=p.BankLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger phonePay ON phonePay.GLID=p.PhonePayLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger esewa ON esewa.GLID = p.EsewaLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger khalti ON khalti.GLID = p.KhaltiLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger remit ON remit.GLID = p.RemitLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger ips ON ips.GLID = p.ConnectIpsLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger par ON par.GLID = p.PartialLedgerId";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- Return Value in DataTable ---------------

    //OBJECT FOR THIS CLASS

    #region --------------- OBJECT FOR THIS CLASS ---------------

    public DocumentDesignPrint VmDocument { get; set; }
    public PurchaseSetting VmPurchase { get; set; }
    public SalesSetting VmSales { get; set; }
    public FinanceSetting VmFinance { get; set; }
    public DatabaseModule.Setup.SystemSetting.SystemSetting VmSystem { get; set; }
    public InventorySetting VmStock { get; set; }
    public IRDAPISetting VmIrd { get; set; }
    public PaymentSetting VmPayment { get; set; }

    #endregion --------------- OBJECT FOR THIS CLASS ---------------
}