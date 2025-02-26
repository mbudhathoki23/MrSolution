using DatabaseModule.Setup.LogSetting;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using DataTable = System.Data.DataTable;

namespace MrDAL.Utility.Config;

public class ClsImport : IOnlineSync
{
    // IMPORT FOR THIS CLASS
    #region **----------- IMPORT ------------**
    public ClsImport()
    {
        ImportLog = new ImportLog();
    }
    public ClsImport(string dbConString)
    {
    }

    #endregion ----------------- IMPORT -----------------


    // IMPORT LOCAL TO LOCAL IMPORT
    #region **------------ IMPORT TO LOCAL -------------**


    // IMPORT MASTER
    #region **---------- IMPORT MASTER ----------**
    public int ResetMaster(string value = "", string dbSelect = "", bool auditStart = true)
    {
        var cmdString = value.ToUpper() switch
        {
            "SYSTEM SETTING" => SystemSetting("RESET", dbSelect),
            "BRANCH" => BranchScript("RESET", dbSelect),
            "COMPANY UNIT" => CompanyUnitScript("RESET", dbSelect),
            "CURRENCY" => CurrencyScript("RESET", dbSelect),
            "ACCOUNT GROUP" => AccountGroupScript("RESET", dbSelect),
            "ACCOUNT SUBGROUP" => AccountSubGroupScript("RESET", dbSelect),
            "SENIOR AGENT" or "MAIN AGENT" => SeniorAgentScript("RESET", dbSelect),
            "JUNIOR AGENT" => JuniorAgentScript("RESET", dbSelect),
            "MAIN AREA" => MainAreaScript("RESET", dbSelect),
            "AREA" => AreaScript("RESET", dbSelect),
            "MEMBER TYPE" => MemberTypeScript("RESET", dbSelect),
            "MEMBERSHIP SETUP" => MemberShipSetupScript("RESET", dbSelect),
            "GENERAL LEDGER" => GeneralLedgerScript("RESET", dbSelect),
            "SUB LEDGER" => SubLedgerScript("RESET", dbSelect),
            "DEPARTMENT" => DepartmentScript("RESET", dbSelect),
            "FLOOR" => FloorScript("RESET", dbSelect),
            "COUNTER" => CounterScript("RESET", dbSelect),
            "COST CENTER" => CostCenterScript("RESET", dbSelect),
            "BARCODE LIST" => ProductBarcodeListScript("RESET", dbSelect),
            "PRODUCT GROUP" => ProductGroupScript("RESET", dbSelect),
            "PRODUCT SUBGROUP" => ProductSubGroupScript("RESET", dbSelect),
            "PRODUCT UNIT" => ProductUnitScript("RESET", dbSelect),
            "PRODUCT" => ProductScript("RESET", dbSelect),
            "GODOWN" => GodownScript("RESET", dbSelect),
            "SALES TERM" => SalesTermScript("RESET", dbSelect),
            "PURCHASE TERM" => PurchaseTermScript("RESET", dbSelect),
            "DOCUMENT NUMBERING" => DocumentNumberingScript("RESET", dbSelect),
            "RACK" => RackScript("RESET", dbSelect),
            "TABLE MASTER" => TableMasterScript("RESET", dbSelect),
            _ => "Select 1"
        };
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public int ImportMasterFromLocal(string value, string server = "", string dbSelect = "")
    {
        server = string.IsNullOrEmpty(server) ? GetConnection.ServerDesc : server;
        var cmdString = value.ToUpper() switch
        {
            "BRANCH" => BranchScript("SAVE", dbSelect),
            "COMPANY UNIT" => CompanyUnitScript("SAVE", dbSelect),
            "CURRENCY" => CurrencyScript("SAVE", dbSelect),
            "ACCOUNT GROUP" => AccountGroupScript("SAVE", dbSelect),
            "ACCOUNT SUBGROUP" => AccountSubGroupScript("SAVE", dbSelect),
            "SENIOR AGENT" or "MAIN AGENT" => SeniorAgentScript("SAVE", dbSelect),
            "JUNIOR AGENT" => JuniorAgentScript("SAVE", dbSelect),
            "MAIN AREA" => MainAreaScript("SAVE", dbSelect),
            "AREA" => AreaScript("SAVE", dbSelect),
            "MEMBER TYPE" => MemberTypeScript("SAVE", dbSelect),
            "MEMBERSHIP SETUP" => MemberShipSetupScript("SAVE", dbSelect),
            "GENERAL LEDGER" => GeneralLedgerScript("SAVE", dbSelect),
            "SUB LEDGER" => SubLedgerScript("SAVE", dbSelect),
            "DEPARTMENT" => DepartmentScript("SAVE", dbSelect),
            "FLOOR" => FloorScript("SAVE", dbSelect),
            "COUNTER" => CounterScript("SAVE", dbSelect),
            "COST CENTER" => CostCenterScript("SAVE", dbSelect),
            "PRODUCT GROUP" => ProductGroupScript("SAVE", dbSelect),
            "PRODUCT SUBGROUP" => ProductSubGroupScript("SAVE", dbSelect),
            "PRODUCT UNIT" => ProductUnitScript("SAVE", dbSelect),
            "PRODUCT" => ProductScript("SAVE", dbSelect),
            "BARCODE LIST" => ProductBarcodeListScript("SAVE", dbSelect),
            "GODOWN" => GodownScript("SAVE", dbSelect),
            "SALES TERM" => SalesTermScript("SAVE", dbSelect),
            "PURCHASE TERM" => PurchaseTermScript("SAVE", dbSelect),
            "DOCUMENT NUMBERING" => DocumentNumberingScript("SAVE", dbSelect),
            "RACK" => RackScript("SAVE", dbSelect),
            "TABLE MASTER" => TableMasterScript("SAVE", dbSelect),
            _ => "Select 1"
        };
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public int UpdateMaster(string value, string server = "", string dbSelect = "")
    {
        var cmdString = value.ToUpper() switch
        {
            "BRANCH" => BranchScript("UPDATE", dbSelect),
            "COMPANY UNIT" => CompanyUnitScript("UPDATE", dbSelect),
            "CURRENCY" => CurrencyScript("UPDATE", dbSelect),
            "ACCOUNT GROUP" => AccountGroupScript("UPDATE", dbSelect),
            "ACCOUNT SUBGROUP" => AccountSubGroupScript("UPDATE", dbSelect),
            "SENIOR AGENT" => SeniorAgentScript("UPDATE", dbSelect),
            "JUNIOR AGENT" => JuniorAgentScript("UPDATE", dbSelect),
            "MAIN AREA" => MainAreaScript("UPDATE", dbSelect),
            "AREA" => AreaScript("UPDATE", dbSelect),
            "MEMBERS TYPE" => MemberTypeScript("UPDATE", dbSelect),
            "MEMBERSHIP SETUP" => MemberShipSetupScript("UPDATE", dbSelect),
            "DEPARTMENT" => DepartmentScript("UPDATE", dbSelect),
            "GENERAL LEDGER" => GeneralLedgerScript("UPDATE", dbSelect),
            "SUB LEDGER" => GeneralLedgerScript("UPDATE", dbSelect),
            "FLOOR" => FloorScript("UPDATE", dbSelect),
            "COUNTER" => CounterScript("UPDATE", dbSelect),
            "COST CENTER" => CostCenterScript("UPDATE", dbSelect),
            "PRODUCT GROUP" => ProductGroupScript("UPDATE", dbSelect),
            "PRODUCT SUBGROUP" => ProductSubGroupScript("UPDATE", dbSelect),
            "PRODUCT UNIT" => ProductUnitScript("UPDATE", dbSelect),
            "PRODUCT" => ProductScript("UPDATE", dbSelect),
            "GODOWN" => GodownScript("UPDATE", dbSelect),
            "SALES TERM" => SalesTermScript("UPDATE", dbSelect),
            "PURCHASE TERM" => PurchaseTermScript("UPDATE", dbSelect),
            "RACK" => RackScript("UPDATE", dbSelect),
            "TABLE MASTER" => TableMasterScript("UPDATE", dbSelect),
            _ => "Select 1"
        };
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    #endregion **---------- IMPORT MASTER ----------**


    // IMPORT TRANSACTION
    #region **---------- IMPORT TRANSACTION ----------**
    public int ResetTransaction(string value = "", string dbSelect = "", bool auditStart = true)
    {
        var cmdString = value.ToUpper() switch
        {
            "LEDGER OPENING" or "LOB" => LedgerOpeningScript("RESET", dbSelect),
            "CASH BANK VOUCHER" or "CB" => CashBankVoucherScript("RESET", dbSelect),
            "PRODUCT OPENING" or "POB" => ProductOpeningScript("RESET", dbSelect),
            "POSTDATE CHEQUE" or "PDC" => PostDatedChequeScript("RESET", dbSelect),
            "JOURNAL VOUCHER" or "JV" => JournalVoucherScript("RESET", dbSelect),
            "CREDIT NOTE" or "CN" => CreditNoteScript("RESET", dbSelect),
            "DEBIT NOTE" or "DN" => DebitNoteScript("RESET", dbSelect),
            "BILL OF MATERIALS" or "BOM" => BillOfMaterialsScript("RESET", dbSelect),
            "PURCHASE ORDER" or "PO" => PurchaseOrderScript("RESET", dbSelect),
            "PURCHASE INDENT" or "PIN" => PurchaseIndentScript("RESET", dbSelect),
            "PURCHASE CHALLAN" or "PC" => PurchaseChallanScript("RESET", dbSelect),
            "PURCHASE RETURN" or "PR" => PurchaseReturnScript("RESET", dbSelect),
            "SALES QUOTATION" or "SQ" => SalesQuotationScript("RESET", dbSelect),
            "SALES CHALLAN" or "SC" => SalesChallanScript("RESET", dbSelect),
            "SALES ORDER" or "SO" => SalesOrderScript("RESET", dbSelect),
            "SALES RETURN" or "SR" => SalesReturnScript("RESET", dbSelect),
            "STOCK ADJUSTMENT" or "SA" => StockAdjustmentScript("RESET", dbSelect),
            "SAMPLE COSTING" or "PSC" => SampleCostingScript("RESET", dbSelect),
            "PURCHASE INVOICE" or "PB" => PurchaseInvoiceScript("RESET", dbSelect),
            "SALES INVOICE" or "SB" => SalesInvoiceScript("RESET", dbSelect),
            "PURCHASE ADDITIONAL INVOICE" or "PAB" => PurchaseAdditionalInvoiceScript("RESET", dbSelect),
            "PRODUCTION MEMO" or "IBOM" => ProductionMemoScript("RESET", dbSelect),

            _ => "Select 1"
        };


        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public int ImportTransactionFromLocal(string value, string server = "", string dbSelect = "")
    {
        try
        {
            server = string.IsNullOrEmpty(server) ? GetConnection.ServerDesc : server;
            var cmdString = value.GetUpper() switch
            {
                "LEDGER OPENING" or "LOB" => LedgerOpeningScript("SAVE", dbSelect),
                "CASH BANK VOUCHER" or "CB" => CashBankVoucherScript("SAVE", dbSelect),
                "PRODUCT OPENING" or "POB" => ProductOpeningScript("SAVE", dbSelect),
                "POST DATED CHEQUE" or "PDC" => PostDatedChequeScript("SAVE", dbSelect),
                "JOURNAL VOUCHER" or "JV" => JournalVoucherScript("SAVE", dbSelect),
                "CREDIT NOTE" or "CN" => CreditNoteScript("SAVE", dbSelect),
                "DEBIT NOTE" or "DN" => DebitNoteScript("SAVE", dbSelect),
                "BILL OF MATERIALS" or "BOM" => BillOfMaterialsScript("SAVE", dbSelect),
                "PURCHASE ORDER" or "PO" => PurchaseOrderScript("SAVE", dbSelect),
                "PURCHASE INDENT" or "PIN" => PurchaseIndentScript("SAVE", dbSelect),
                "PURCHASE CHALLAN" or "PC" => PurchaseChallanScript("SAVE", dbSelect),
                "PURCHASE RETURN" or "PR" => PurchaseReturnScript("SAVE", dbSelect),
                "SALES QUOTATION" or "SQ" => SalesQuotationScript("SAVE", dbSelect),
                "SALES CHALLAN" or "SC" => SalesChallanScript("SAVE", dbSelect),
                "SALES ORDER" or "SO" => SalesOrderScript("SAVE", dbSelect),
                "SALES RETURN" or "SR" => SalesReturnScript("SAVE", dbSelect),
                "STOCK ADJUSTMENT" or "SA" => StockAdjustmentScript("SAVE", dbSelect),
                "SAMPLE COSTING" or "PSC" => SampleCostingScript("SAVE", dbSelect),
                "PURCHASE INVOICE" or "PB" => PurchaseInvoiceScript("SAVE", dbSelect),
                "PURCHASE ADDITIONAL INVOICE" or "PAB" => PurchaseAdditionalInvoiceScript("SAVE", dbSelect),
                "SALES INVOICE" or "SB" => SalesInvoiceScript("SAVE", dbSelect),
                "PRODUCTION MEMO" or "IBOM" => ProductionMemoScript("SAVE", dbSelect),
                _ => "Select 1"
            };
            var result = SqlExtensions.ExecuteNonTrans(cmdString);
            return result;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return 0;
        }
    }
    public int UpdateTransaction(string value, string server = "", string dbSelect = "")
    {
        var cmdString = value.ToUpper() switch
        {
            "LEDGER OPENING" or "LOB" => LedgerOpeningScript("UPDATE", dbSelect),
            "CASH BANK VOUCHER" or "CB" => CashBankVoucherScript("UPDATE", dbSelect),
            "PRODUCT OPENING" or "POB" => ProductOpeningScript("UPDATE", dbSelect),
            "POST DATED CHEQUE" or "PDC" => PostDatedChequeScript("UPDATE", dbSelect),
            "JOURNAL VOUCHER" or "JV" => JournalVoucherScript("UPDATE", dbSelect),
            "CREDIT NOTE" or "CN" => CreditNoteScript("UPDATE", dbSelect),
            "DEBIT NOTE" or "DN" => DebitNoteScript("UPDATE", dbSelect),
            "BILL OF MATERIALS" or "BOM" => BillOfMaterialsScript("UPDATE", dbSelect),
            "PURCHASE ORDER" or "PO" => PurchaseOrderScript("UPDATE", dbSelect),
            "PURCHASE INDENT" or "PIN" => PurchaseIndentScript("UPDATE", dbSelect),
            "PURCHASE CHALLAN" or "PC" => PurchaseChallanScript("UPDATE", dbSelect),
            "PURCHASE RETURN" or "PR" => PurchaseReturnScript("UPDATE", dbSelect),
            "SALES QUOTATION" or "SQ" => SalesQuotationScript("UPDATE", dbSelect),
            "SALES CHALLAN" or "SC" => SalesChallanScript("UPDATE", dbSelect),
            "SALES ORDER" or "SO" => SalesOrderScript("UPDATE", dbSelect),
            "SALES RETURN" or "SR" => SalesReturnScript("UPDATE", dbSelect),
            "STOCK ADJUSTMENT" or "SA" => StockAdjustmentScript("UPDATE", dbSelect),
            "SAMPLE COSTING" or "PSC" => SampleCostingScript("UPDATE", dbSelect),
            "PURCHASE INVOICE" or "PB" => PurchaseInvoiceScript("UPDATE", dbSelect),
            "PURCHASE ADDITIONAL INVOICE" or "PAB" => PurchaseAdditionalInvoiceScript("UPDATE", dbSelect),
            "SALES INVOICE" or "SB" => SalesInvoiceScript("UPDATE", dbSelect),
            "PRODUCTION MEMO" or "IBOM" => ProductionMemoScript("UPDATE", dbSelect),
            _ => "Select 1"
        };
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    #endregion **---------- IMPORT TRANSACTION ----------**


    // SAVE SERVER INFO
    public int SaveServerInfo()
    {
        var cmdString = $@"
		INSERT INTO CTRL.ImportLog (ImportType, ImportDate, ServerDesc, ServerUser, ServerPassword, dbInitial, dbCompanyInfo, IsSuccess)
		VALUES (N'{ImportLog.ImportType}', GETDATE(), N'{ImportLog.ServerDesc}', N'{ImportLog.ServerUser}', N'{ImportLog.ServerPassword}', N'{ImportLog.dbInitial}', N'{ImportLog.dbCompanyInfo}', CAST('{ImportLog.IsSuccess}' AS BIT));";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    #endregion **----------------- IMPORT TO LOCAL -----------------**


    // SCRIPT OF MASTER DATABASE
    #region **---------- SCRIPT OF MASTER DATA BASE TABLE ----------**
    private static string SystemSetting(string tag, string initial)
    {
        var cmdString = @"
        TRUNCATE TABLE AMS.SystemSetting;
        TRUNCATE TABLE AMS.SalesSetting;
        TRUNCATE TABLE AMS.InventorySetting;
        TRUNCATE TABLE AMS.FinanceSetting;
        TRUNCATE TABLE AMS.InvoiceSettlement;
        TRUNCATE TABLE AMS.PaymentSetting;
        TRUNCATE TABLE AMS.PurchaseSetting;";
        return cmdString;
    }
    private static string BranchScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.Branch(Branch_ID, NepaliDesc, Branch_Name, Branch_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Created_By, Created_Date, Modify_By, Modify_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, IsDefault, IsActive)
            SELECT Branch_ID, NepaliDesc, Branch_Name, Branch_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Created_By, Created_Date, Modify_By, Modify_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1), ISNULL(IsDefault, 0), ISNULL(IsActive, 1)
            FROM {initial}.AMS.Branch b
            WHERE b.Branch_ID NOT IN(SELECT b1.Branch_ID FROM AMS.Branch b1);";

        }
        else if (tag is "RESET")
        {
            cmdString = @" 
            DELETE AMS.Branch
            WHERE Branch.Branch_ID NOT IN(SELECT Branch_ID FROM AMS.AccountDetails
                                          UNION ALL
                                          SELECT Branch_Id FROM AMS.StockDetails);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
            UPDATE AMS.Branch SET Branch_ID = b.Branch_Id,Branch_Name = b.Branch_Name,Branch_Code = b.Branch_Code,Reg_Date = b.Reg_Date,Address = b.Address,Country = b.Country,State = b.State,City = b.City,PhoneNo = b.PhoneNo,Fax = b.Fax,Email = b.Email,ContactPerson = b.ContactPerson,ContactPersonAdd = b.ContactPersonAdd,ContactPersonPhone = b.ContactPersonPhone,SyncRowVersion = b.SyncRowVersion
			FROM {initial}.AMS.Branch b 
            WHERE AMS.Branch.Branch_ID = b.Branch_Id AND b.SyncRowVersion < AMS.Branch.SyncRowVersion; ";
        }

        return cmdString;
    }
    private static string CompanyUnitScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.CompanyUnit(CmpUnit_ID, CmpUnit_Name, CmpUnit_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Branch_ID, Created_By, Created_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT CmpUnit_ID, CmpUnit_Name, CmpUnit_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Branch_ID, Created_By, Created_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.CompanyUnit cu
            WHERE cu.CmpUnit_ID NOT IN(SELECT cu1.CmpUnit_ID FROM AMS.CompanyUnit cu1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @$"
            DELETE FROM AMS.CompanyUnit
            WHERE CmpUnit_ID NOT IN(SELECT CmpUnit_ID FROM AMS.AccountDetails UNION ALL SELECT CmpUnit_Id FROM AMS.StockDetails);";

        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
             UPDATE AMS.CompanyUnit SET CmpUnit_ID = cu.CmpUnit_Id,CmpUnit_Name = cu.CmpUnit_Name,CmpUnit_Code = cu.CmpUnit_Code,Reg_Date = cu.Reg_Date,Address = cu.Address,Country = cu.Country,State = cu.State,City = cu.City,PhoneNo = cu.PhoneNo,Fax = cu.Fax,Email = cu.Email,ContactPerson = cu.ContactPerson,ContactPersonAdd = cu.ContactPersonAdd,ContactPersonPhone = cu.ContactPersonPhone,SyncRowVersion = cu.SyncRowVersion
			 FROM {initial}.AMS.CompanyUnit cu 
            WHERE AMS.CompanyUnit.CmpUnit_ID = cu.CmpUnit_ID AND cu.SyncRowVersion < AMS.CompanyUnit.SyncRowVersion";
        }

        return cmdString;
    }
    private static string CurrencyScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.Currency(CId, NepaliDesc, CName, CCode, CRate, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, BuyRate)
            SELECT CId, NepaliDesc, CName, CCode, ISNULL(CRate, 1), Branch_ID, Company_Id, ISNULL(Status, 1), ISNULL(IsDefault, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1), ISNULL(BuyRate, 1)
            FROM {initial}.AMS.Currency c
            WHERE c.CId NOT IN(SELECT c.CId FROM AMS.Currency c1); ";
        }
        else if (tag is "RESET")
        {
            cmdString = @$" 
                DELETE FROM AMS.Currency WHERE CId NOT IN (SELECT Currency_ID FROM AMS.AccountDetails)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = @$" 
            UPDATE AMS.Currency SET CId = c.CId,CName = c.CName,CCode = c.CCode,CRate = c.CRate,Status = c.Status,SyncRowVersion = c.SyncRowVersion
            FROM {initial}.AMS.Currency c WHERE AMS.Currency.CId = c.CId AND c.SyncRowVersion < AMS.Currency.SyncRowVersion";
        }

        return cmdString;
    }
    private static string SeniorAgentScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.SeniorAgent(SAgentId, NepaliDesc, SAgent, SAgentCode, PhoneNo, Address, Comm, TagetLimit, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SAgentId, NepaliDesc, SAgent, SAgentCode, PhoneNo, Address, ISNULL(Comm, 0), ISNULL(TagetLimit, 0),NULL GLID, Branch_ID, Company_Id, ISNULL(Status, 1), ISNULL(IsDefault, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.SeniorAgent sa
            WHERE sa.SAgentId NOT IN(SELECT sa1.SAgentId FROM AMS.SeniorAgent sa1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @$"
            DELETE AMS.SeniorAgent WHERE SAgentId NOT IN (SELECT SAgent FROM AMS.JuniorAgent)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
              UPDATE AMS.SeniorAgent SET GLID = NULL;";
        }

        return cmdString;
    }
    private static string JuniorAgentScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.JuniorAgent(AgentId, NepaliDesc, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, CrLimit, CrDays, CrTYpe, TargetLimit, SAgent, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT AgentId, NepaliDesc, AgentName, AgentCode, Address, PhoneNo,NULL GLCode, ISNULL(Commission, 0), ISNULL(CrLimit, 0), ISNULL(CrDays, 0), CrTYpe, ISNULL(TargetLimit, 0), SAgent, Email, Branch_ID, Company_Id, EnterBy, EnterDate, ISNULL(Status, 0), ISNULL(IsDefault, 0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 0)
            FROM {initial}.AMS.JuniorAgent ja
            WHERE ja.AgentId NOT IN(SELECT ja1.AgentId FROM AMS.JuniorAgent ja1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @$"
            DELETE AMS.JuniorAgent WHERE AgentId NOT IN (SELECT Agent_ID FROM AMS.AccountDetails)";

        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
            UPDATE AMS.JuniorAgent SET GLCode = NULL";
        }

        return cmdString;
    }
    private static string MainAreaScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.MainArea(MAreaId, NepaliDesc, MAreaName, MAreaCode, MCountry, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT MAreaId, NepaliDesc, MAreaName, MAreaCode, MCountry, Branch_ID, Company_Id, ISNULL(Status, 1), ISNULL(IsDefault, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.MainArea ma
            WHERE ma.MAreaId NOT IN(SELECT ma1.MAreaId FROM AMS.MainArea ma1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            Delete AMS.MainArea ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.MainArea SET MAreaId = ma.MAreaId,MAreaName = ma.MAreaName,MAreaCode = MA.MAreaCode,MCountry = MA.MCountry,Status = ma.Status,SyncRowVersion = ma.SyncRowVersion
			FROM {initial}.AMS.MainArea ma WHERE AMS.MainArea.MAreaId = ma.MAreaId AND ma.SyncRowVersion < AMS.MainArea.SyncRowVersion";
        }

        return cmdString;
    }
    private static string AreaScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Area(AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, ISNULL(Status, 1), ISNULL(IsDefault, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.Area a
            WHERE a.AreaId NOT IN(SELECT a1.AreaId FROM AMS.Area a1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             Delete AMS.Area";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.Area SET AreaId = A.AreaId,AreaName = A.AreaName,AreaCode = A.AreaCode,Country = A.Country,Main_Area = A.Main_Area,Status = A.Status,SyncRowVersion = a.SyncRowVersion
			FROM {initial}.AMS.Area A WHERE AMS.Area.AreaId= A.AreaId AND a.SyncRowVersion < AMS.Area.SyncRowVersion";
        }

        return cmdString;
    }
    private static string AccountGroupScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
			INSERT INTO AMS.AccountGroup(GrpId, NepaliDesc, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT GrpId, NepaliDesc, GrpName, GrpCode, ISNULL(Schedule, 1), PrimaryGrp, GrpType, Branch_ID, Company_Id, ISNULL(Status, 1), EnterBy, EnterDate, PrimaryGroupId, ISNULL(IsDefault, 0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.AccountGroup ag
            WHERE ag.GrpId NOT IN(SELECT ag1.GrpId FROM AMS.AccountGroup ag1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @" 
            DELETE FROM AMS.AccountGroup WHERE GrpId NOT IN (SELECT GLID FROM AMS.GeneralLedger);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.AccountGroup SET GrpId = ag.GrpId,GrpName = ag.GrpName,GrpCode = ag.GrpCode,Schedule = ag.Schedule,PrimaryGrp = ag.PrimaryGrp,GrpType = ag.GrpType,Status = ag.Status,SyncRowVersion = ag.SyncRowVersion
            FROM AMS.AccountGroup ag
            WHERE {initial}.AMS.AccountGroup.GrpId = ag.GrpId AND ag.SyncRowVersion < AMS.AccountGroup.SyncRowVersion; ";
        }

        return cmdString;
    }
    private static string AccountSubGroupScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.AccountSubGroup(SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, ISNULL(Status, 1), EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, ISNULL(IsDefault, 0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.AccountSubGroup asg
            WHERE asg.SubGrpId NOT IN(SELECT asg1.SubGrpId FROM AMS.AccountSubGroup asg1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @$" 
             DELETE AMS.AccountSubGroup WHERE SubGrpId NOT IN (SELECT SubGrpId FROM AMS.GeneralLedger)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
            UPDATE AMS.AccountSubGroup SET SubGrpId = asg.SubGrpId,SubGrpName = asg.SubGrpName,PrimaryGroupId = asg.PrimaryGroupId,PrimarySubGroupId = asg.PrimarySubGroupId,IsDefault = asg.IsDefault,GrpId = asg.GrpID,SubGrpCode = asg.SubGrpCode,Status = asg.Status,SyncRowVersion = asg.SyncRowVersion
			FROM {initial}.AMS.AccountSubGroup asg WHERE AMS.AccountSubGroup.SubGrpId=asg.SubGrpId AND asg.SyncRowVersion < AMS.AccountSubGroup.SyncRowVersion;";
        }
        return cmdString;
    }
    private static string GeneralLedgerScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, ISNULL(CrDays, 0), ISNULL(CrLimit, 0), CrTYpe, ISNULL(IntRate, 0), GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, ISNULL(Status, 1), ISNULL(IsDefault, 0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.GeneralLedger gl
            WHERE gl.GLID NOT IN(SELECT gl1.GLID FROM AMS.GeneralLedger gl1);
            UPDATE AMS.SeniorAgent SET GLID =a.GLID FROM {initial}.AMS.SeniorAgent a WHERE a.SAgentId = SeniorAgent.SAgentId;
            UPDATE AMS.JuniorAgent SET GLCode =a.GLCode FROM {initial}.AMS.JuniorAgent a WHERE a.GLCode = JuniorAgent.GLCode;";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.GeneralLedger WHERE GLID NOT IN (SELECT Ledger_ID FROM AMS.AccountDetails UNION ALL SELECT Ledger_Id FROM AMS.StockDetails);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.GeneralLedger SET GLID = gl.GLID,GLName = gl.GLName,GLCode = gl.GLCode,ACCode = gl.ACCode,GLType = gl.GLType,GrpId = gl.GrpId,SubGrpId = gl.SubGrpId,PanNo = gl.PanNo,AreaId = gl.AreaId,AgentId = gl.AgentId,CurrId = gl.CurrId,CrDays = gl.CrDays,CrLimit = gl.CrLimit,CrTYpe = gl.CrTYpe,IntRate = gl.IntRate,GLAddress = gl.GLAddress,PhoneNo = gl.PhoneNo,LandLineNo = gl.LandLineNo,OwnerName = gl.OwnerName,OwnerNumber = gl.OwnerNumber,Scheme = gl.Scheme,Email = gl.Email,PrimaryGroupId = gl.PrimaryGroupId,PrimarySubGroupId = gl.PrimarySubGroupId,IsDefault = gl.IsDefault,Status = gl.Status,SyncRowVersion = gl.SyncRowVersion
			FROM {initial}.AMS.GeneralLedger gl WHERE AMS.GeneralLedger.GLID = gl.GLID AND gl.SyncRowVersion < AMS.GeneralLedger.SyncRowVersion;";
        }

        return cmdString;
    }
    private static string SubLedgerScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Subledger(SLId, NepalDesc, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SLId, NepalDesc, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, ISNULL(Status, 1), ISNULL(IsDefault, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.Subledger sl
            WHERE sl.SLId NOT IN(SELECT sl1.SLId FROM AMS.Subledger sl1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.Subledger
            WHERE SLId NOT IN(SELECT Subleder_ID FROM AMS.AccountDetails);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.SubLedger SET SLId = s.SLId,SLName = s.SLName,SLCode = s.SLCode,SLAddress = s.SLAddress,SLPhoneNo = s.SLPhoneNo,GLID = s.GLID,Status = s.Status,SyncRowVersion= s.SyncRowVersion
			FROM {initial}.AMS.SubLedger s WHERE AMS.SubLedger.SLId = s.SLId AND s.SyncRowVersion < AMS.SubLedger.SyncRowVersion";
        }
        return cmdString;
    }
    private static string MemberTypeScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.MemberType(MemberTypeId, NepaliDesc, MemberDesc, MemberShortName, Discount, BranchId, CompanyUnitId, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT MemberTypeId, NepaliDesc, MemberDesc, MemberShortName, ISNULL(Discount, 0), BranchId, CompanyUnitId, EnterBy, EnterDate, ISNULL(ActiveStatus, 1), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.MemberType mt
            WHERE mt.MemberTypeId NOT IN(SELECT mt1.MemberTypeId FROM AMS.MemberType mt1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.MemberType WHERE MemberTypeId NOT IN (SELECT MemberTypeId FROM AMS.MemberShipSetup)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.MemberType SET MemberTypeId = mt.MemberTypeId,MemberDesc = MT.MemberDesc,MemberShortName = mt.MemberShortName,Discount = mt.Discount,ActiveStatus = mt.ActiveStatus,SyncRowVersion = mt.SyncRowVersion
			FROM {initial}.AMS.MemberType mt WHERE AMS.MemberType.MemberTypeId = mt.MemberTypeId AND mt.SyncRowVersion < AMS.MemberType.SyncRowVersion";
        }
        return cmdString;
    }
    private static string MemberShipSetupScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.MemberShipSetup(MShipId, MemberId, NepaliDesc, MShipDesc, MShipShortName, PhoneNo, PriceTag, LedgerId, EmailAdd, MemberTypeId, BranchId, CompanyUnitId, MValidDate, MExpireDate, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT MShipId, MemberId, NepaliDesc, MShipDesc, MShipShortName, PhoneNo, PriceTag, LedgerId, EmailAdd, MemberTypeId, BranchId, CompanyUnitId, MValidDate, MExpireDate, EnterBy, EnterDate, ISNULL(ActiveStatus, 1), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 0)
            FROM {initial}.AMS.MemberShipSetup mss
            WHERE mss.MShipId NOT IN(SELECT mss1.MShipId FROM AMS.MemberShipSetup mss1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.MemberShipSetup WHERE MShipId NOT IN (SELECT MShipId FROM AMS.AccountDetails)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.MemberShipSetup SET MShipId = mss.MShipId,MShipDesc =mss.MShipDesc,MShipShortName = mss.MShipShortName,PhoneNo = mss.PhoneNo,LedgerId = mss.LedgerId,EmailAdd = mss.EmailAdd,MemberTypeId = mss.MemberTypeId,MemberId = mss.MemberId,ActiveStatus = mss.ActiveStatus,SyncRowVersion = mss.SyncRowVersion
			FROM {initial}.AMS.MemberShipSetup mss WHERE AMS.MemberShipSetup.MShipId = mss.MShipId AND mss.SyncRowVersion < AMS.MemberShipSetup.SyncRowVersion";
        }

        return cmdString;
    }
    private static string DepartmentScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Department(DId, NepaliDesc, DName, DCode, Dlevel, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT DId, NepaliDesc, DName, DCode, Dlevel, Branch_ID, Company_Id, ISNULL(Status, 1), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.Department d
            WHERE d.DId NOT IN(SELECT d1.DId FROM AMS.Department d1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.Department WHERE DId NOT IN (SELECT Department_ID1 FROM AMS.AccountDetails)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.Department SET DId = d.DId,DName = d.DId,DCode = d.DCode,DLevel = d.DLevel,Status = d.Status,SyncRowVersion = d.SyncRowVersion
			FROM {initial}.AMS.Department d WHERE AMS.Department.DId = d.DId AND d.SyncRowVersion < AMS.Department.SyncRowVersion";
        }
        return cmdString;
    }
    private static string CounterScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT CId, CName, CCode, Branch_ID, Company_Id, ISNULL(Status, 1), EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.Counter c
            WHERE c.CId NOT IN(SELECT c1.CId FROM AMS.Counter c1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM AMS.Counter WHERE CId NOT IN (SELECT CounterId FROM AMS.SB_Master);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
            UPDATE AMS.Counter SET CId = c.CId,CName = c.CName,CCode = c.CCode,Printer = c.Printer,Status = c.Status,SyncRowVersion = c.SyncRowVersion
			FROM {initial}.AMS.Counter c WHERE AMS.Counter.CId = c.CId AND c.SyncRowVersion < AMS.Counter.SyncRowVersion";
        }
        return cmdString;
    }
    
    private static string FloorScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Floor(FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM {initial}.AMS.Floor 
            WHERE FloorId NOT IN (SELECT FloorId FROM AMS.Floor)";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.Floor WHERE FloorId NOT IN (SELECT tm.FloorId FROM AMS.TableMaster tm)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.Floor SET FloorId = f.FloorId,Description =f.Description,ShortName = f.Description,Type = f.Type,Status = f.Status,SyncRowVersion = f.SyncRowVersion
			FROM {initial}.AMS.Floor f WHERE AMS.Floor.FloorId = f.FloorId AND f.SyncRowVersion < AMS.Floor.SyncRowVersion";
        }
        return cmdString;
    }
    private static string GodownScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Godown(GID, NepaliDesc, GName, GCode, GLocation, Status, EnterBy, EnterDate, BranchUnit, CompUnit, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT GID, NepaliDesc, GName, GCode, GLocation, ISNULL(Status, 1), EnterBy, EnterDate, BranchUnit, CompUnit, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.Godown g
            WHERE g.GID NOT IN(SELECT g1.GID FROM AMS.Godown g1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"    
           DELETE FROM AMS.Godown WHERE GID NOT IN (SELECT sd.Godown_Id FROM AMS.StockDetails sd)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.Godown SET GName = g.GName,GCode = g.GCode,GLocation = g.GLocation,Status = g.Status,SyncRowVersion = g.SyncRowVersion
			FROM {initial}.AMS.Godown g WHERE AMS.Godown.GID = 0 AND g.SyncRowVersion < AMS.Godown.SyncRowVersion ";
        }
        return cmdString;
    }
    private static string CostCenterScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.CostCenter(CCId, CCName, CCcode, GodownId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT CCId, CCName, CCcode, GodownId, Branch_ID, Company_Id, ISNULL(Status, 1), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.CostCenter cc
            WHERE cc.CCId NOT IN(SELECT cc1.CCId FROM AMS.CostCenter cc1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM AMS.CostCenter WHERE CCId NOT IN (SELECT sd.CostCenter_Id FROM AMS.StockDetails sd)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.CostCenter SET CCId = cc.CCId,CCName = cc.CCName,CCCode = cc.CCCode,Status = cc.Status,SyncRowVersion = cc.SyncRowVersion
			FROM {initial}.AMS.CostCenter cc 
            WHERE AMS.CostCenter.CCId = cc.CCId AND cc.SyncRowVersion < AMS.CostCenter.SyncRowVersion";
        }
        return cmdString;
    }
    private static string RackScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.RACK(RID, RName, RCode, Location, Status, EnterBy, EnterDate, BranchId, CompanyUnitId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT RID, RName, RCode, Location, ISNULL(Status, 1), EnterBy, EnterDate, ISNULL(BranchId, 1), CompanyUnitId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.RACK r
            WHERE r.RID NOT IN(SELECT r1.RID FROM AMS.RACK r1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM AMS.RACK";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"   
            UPDATE AMS.RACK SET RID = r.RID,RName = r.RName,RCode = r.RCode,Location = r.Location,Status = r.Status,SyncRowVersion = r.SyncRowVersion
	        FROM {initial}.AMS.RACK r WHERE AMS.RACK.RID = r.RID AND r.SyncRowVersion < AMS.RACK.SyncRowVersion";
        }

        return cmdString;
    }
    private static string ProductGroupScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.ProductGroup(PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, Gprinter, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PGrpId, NepaliDesc, GrpName, GrpCode, ISNULL(GMargin, 0), Gprinter, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId, Branch_ID, Company_Id, ISNULL(Status, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.ProductGroup pg
            WHERE pg.PGrpId NOT IN(SELECT pg1.PGrpId FROM AMS.ProductGroup pg1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE AMS.ProductGroup WHERE PGrpId NOT IN (SELECT PGrpId FROM AMS.Product)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"   
            UPDATE AMS.ProductGroup SET PGrpId = pg.PGrpID,GrpName = pg.GrpName,GrpCode = pg.GrpCode,GMargin = pg.GMargin,Status = pg.Status,SyncRowVersion = pg.SyncRowVersion
			FROM {initial}.AMS.ProductGroup pg WHERE AMS.ProductGroup.PGrpID = pg.PGrpId AND pg.SyncRowVersion < AMS.ProductGroup.SyncRowVersion";
        }
        return cmdString;
    }
    private static string ProductSubGroupScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.ProductSubGroup(PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, IsDefault, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, ISNULL(IsDefault, 0), ISNULL(Status, 1), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.ProductSubGroup psg
            WHERE psg.PSubGrpId NOT IN(SELECT psg1.PSubGrpId FROM AMS.ProductSubGroup psg1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE AMS.ProductSubGroup WHERE PSubGrpId NOT IN (SELECT PSubGrpId FROM AMS.Product)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.ProductSubGroup SET PSubGrpId = psg.PSubGrpId,SubGrpName = psg.SubGrpName,ShortName = psg.ShortName,GrpId = psg.GrpId,Status = psg.Status,SyncRowVersion = psg.SyncRowVersion
			FROM {initial}.AMS.ProductSubGroup psg 
            WHERE AMS.ProductSubGroup.PSubGrpId = psg.PSubGrpId AND psg.SyncRowVersion < AMS.ProductSubGroup.SyncRowVersion";
        }

        return cmdString;
    }
    private static string ProductUnitScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"   
            INSERT INTO AMS.ProductUnit(UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, ISNULL(Status, 1), ISNULL(IsDefault, 0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.ProductUnit pu
            WHERE pu.UID NOT IN(SELECT pu1.UID FROM AMS.ProductUnit pu1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE AMS.ProductUnit WHERE UnitCode NOT IN (SELECT Unit_Id FROM AMS.StockDetails)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.ProductUnit SET UID = pu.UID,UnitName = pu.UnitName,UnitCode = pu.UnitCode,Status = pu.Status,SyncRowVersion = pu.SyncRowVersion
		    FROM {initial}.AMS.ProductUnit pu WHERE AMS.ProductUnit.UID = pu.UID AND pu.SyncRowVersion < AMS.ProductUnit.SyncRowVersion";
        }
        return cmdString;
    }
    private static string ProductScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Product(PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PVehicleWise, PublicationWise, PBuyRate, AltSalesRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, Image, HsCode)
            SELECT PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, ISNULL(PQtyConv, 0), ISNULL(PAltConv, 0), PValTech, ISNULL(PSerialno, 0), ISNULL(PSizewise, 0), ISNULL(PBatchwise, 0), ISNULL(PVehicleWise, 0), ISNULL(PublicationWise, 0), ISNULL(PBuyRate, 0), ISNULL(AltSalesRate, 0), ISNULL(PSalesRate, 0), ISNULL(PMargin1, 0), ISNULL(TradeRate, 0), ISNULL(PMargin2, 0), ISNULL(PMRP, 0), PGrpId, PSubGrpId, ISNULL(PTax, 0), ISNULL(PMin, 0), ISNULL(PMax, 0), CmpId, CmpId1, CmpId2, CmpId3, Branch_ID, CmpUnit_ID, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, ISNULL(BeforeBuyRate, 0), ISNULL(BeforeSalesRate, 0), Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1), Image, HsCode
            FROM {initial}.AMS.Product p
            WHERE p.PID NOT IN(SELECT p1.PID FROM AMS.Product p1);
            INSERT INTO AMS.BookDetails(BookId, NepaliDesc, PrintDesc, ISBNNo, Author, Publisher)
            SELECT BookId, NepaliDesc, PrintDesc, ISBNNo, Author, Publisher FROM {initial}.AMS.BookDetails WHERE BookId NOT IN(SELECT BookId FROM AMS.BookDetails);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.BookDetails;  
            DELETE AMS.Product WHERE PID NOT IN (SELECT Product_Id FROM AMS.StockDetails) ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.Product SET PID = p.PID,PName = P.PName,PAlias = P.PAlias,PShortName = P.PShortName,PType = P.PType,PCategory = P.PCategory,PUnit =P.PUnit,PAltUnit =P.PAltUnit,PQtyConv = P.PQtyConv,PAltConv = P.PAltConv,PValTech = P.PValTech,PSerialno = P.PSerialno,PSizewise = P.PSizewise,PBatchwise = P.PBatchwise,PBuyRate = P.PBuyRate,PSalesRate = P.PSalesRate,PMargin1 = P.PMargin1,TradeRate = P.TradeRate,PMargin2 = P.PMargin2,PMRP = P.PMRP,PGrpId = P.PGrpId,PSubGrpId = P.PSubGrpId,PTax = P.PTax,PMin = P.PMin,PMax = P.PMax,CmpId = P.CmpId,CmpId1 = P.CmpId1,CmpId2 = p.CmpId2,CmpId3 = P.CmpId3,PPL = P.PPL,PPR = P.PPR,PSL = P.PSL,PSR = P.PSR,PL_Opening = P.PL_Opening,PL_Closing = P.PL_Closing,BS_Closing = P.BS_Closing,PImage = P.PImage,Status = P.Status, SyncRowVersion = P.SyncRowVersion
		    FROM {initial}.AMS.Product p WHERE AMS.Product.PID = p.PID ";
        }

        return cmdString;
    }
    private static string ProductBarcodeListScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
            SELECT ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange FROM {initial}.AMS.BarcodeList b WHERE b.Barcode NOT IN(SELECT Barcode COLLATE DATABASE_DEFAULT FROM AMS.BarcodeList);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            TRUNCATE TABLE AMS.BarcodeList;";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.BarcodeList SET 
            SalesRate = bl.SalesRate, MRP = bl.MRP, Trade = bl.Trade, Wholesale = bl.Wholesale, Retail = bl.Retail, Dealer = bl.Dealer, Resellar = bl.Resellar, UnitId =bl.UnitId, AltUnitId = bl.AltUnitId, DailyRateChange = ISNULL(bl.DailyRateChange,0)
            FROM {initial}.AMS.BarcodeList bl WHERE BarcodeList.Barcode = bl.Barcode; ";
        }

        return cmdString;
    }
    private static string DocumentNumberingScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocUser, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT DocId, DocModule, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocUser, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, ISNULL(DocBlank, 0), ISNULL(DocBlankCh, 0), DocBranch, DocUnit, ISNULL(DocStart, 1), ISNULL(DocCurr, 1), ISNULL(DocEnd, 9999), DocDesign, ISNULL(Status, 1), EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.DocumentNumbering dn
            WHERE dn.DocId NOT IN(SELECT dn1.DocId FROM AMS.DocumentNumbering dn1)AND dn.DocDesc NOT IN(SELECT dn1.DocDesc COLLATE DATABASE_DEFAULT FROM AMS.DocumentNumbering dn1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            TRUNCATE TABLE AMS.DocumentNumbering; ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.DocumentNumbering";
        }

        return cmdString;
    }
    private static string PurchaseTermScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, ISNULL(PT_Rate, 0), PT_Branch, PT_CompanyUnit, ISNULL(PT_Profitability, 0), ISNULL(PT_Supess, 0), ISNULL(PT_Status, 1), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 1)
            FROM {initial}.AMS.PT_Term pt
            WHERE pt.PT_Id NOT IN(SELECT pt1.PT_Id FROM AMS.PT_Term pt1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.PT_Term
            WHERE PT_Id NOT IN(SELECT PT_Id FROM AMS.PB_Term
                               UNION ALL
                               SELECT PT_Id FROM AMS.PR_Term
                               UNION ALL
                               SELECT PT_Id FROM AMS.PAB_Details
                               UNION ALL
                               SELECT PT_Id FROM AMS.PC_Term
                               UNION ALL
                               SELECT PT_Id FROM AMS.PCR_Term
                               UNION ALL
                               SELECT PT_Id FROM AMS.PO_Term);";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.PT_Term SET PT_ID = pt.PT_ID,Order_No = pt.Order_No,Module = pt.Module,PT_Name = pt.PT_Name,PT_Type = pt.PT_Type,Ledger = pt.Ledger,PT_Basis = pt.PT_Basis,PT_Sign = pt.PT_Sign,PT_Condition = pt.PT_Condition,PT_Rate = pt.PT_Rate,PT_Profitability = pt.PT_Profitability,PT_Supess = pt.PT_Supess,PT_Status = pt.PT_Status,SyncRowVersion = pt.SyncRowVersion
			FROM {initial}.AMS.PT_Term pt WHERE AMS.PT_Term.PT_ID = pt.PT_ID AND pt.SyncRowVersion < AMS.PT_Term.SyncRowVersion ";
        }

        return cmdString;
    }
    private static string SalesTermScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ISNULL(ST_Rate, 0), ST_Branch, ST_CompanyUnit, ISNULL(ST_Profitability, 0), ISNULL(ST_Supess, 0), ISNULL(ST_Status, 0), EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion, 0)
            FROM {initial}.AMS.ST_Term st
            WHERE st.ST_ID NOT IN(SELECT st1.ST_ID FROM AMS.ST_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM AMS.ST_Term WHERE ST_ID NOT IN (SELECT ST_Id FROM AMS.SB_Term UNION ALL SELECT ST_Id FROM AMS.SR_Term UNION ALL SELECT ST_Id FROM AMS.SC_Term UNION ALL SELECT ST_Id FROM AMS.SO_Term UNION ALL SELECT ST_Id FROM AMS.SQ_Term) ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.ST_Term SET ST_ID = st.ST_ID,Order_No = st.Order_No,Module = st.Module,ST_Name = st.ST_Name,ST_Type = st.ST_Type,Ledger = st.Ledger,ST_Basis = st.ST_Basis,ST_Sign = st.ST_Sign,ST_Condition = st.ST_Condition,ST_Rate = st.ST_Rate,ST_Profitability = st.ST_Profitability,ST_Supess = st.ST_Supess,ST_Status = st.ST_Status,SyncRowVersion = st.SyncRowVersion
			FROM {initial}.AMS.ST_Term st WHERE AMS.ST_Term.ST_ID = st.ST_ID AND ST.SyncRowVersion < AMS.ST_Term.SyncRowVersion";
        }

        return cmdString;
    }
   
    
    private static string TableMasterScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.TableMaster(TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, TableStatus, TableType, IsPrePaid, Status, EnterBy, EnterDate, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, ISNULL(TableStatus,'A'), ISNULL(TableType,'D'), ISNULL(IsPrePaid,0), ISNULL(Status,1), EnterBy, EnterDate, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.TableMaster 
            WHERE TableId NOT IN (SELECT TableId FROM AMS.TableMaster)";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM AMS.TableMaster WHERE TableId NOT IN (SELECT sm.TableId FROM AMS.SB_Master sm)";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.TableMaster SET TableId = tm.TableId,TableName = tm.TableName,TableCode = tm.TableCode,FloorId = tm.FloorId,TableStatus = tm.TableStatus,Status = tm.Status,TableType = tm.TableType,SyncRowVersion = tm.SyncRowVersion
			FROM {initial}.AMS.TableMaster tm 
            WHERE AMS.TableMaster.TableId = tm.TableId AND tm.SyncRowVersion < AMS.TableMaster.SyncRowVersion";
        }
        return cmdString;
    }
    #endregion


    //SCRIPT OF TRANSACTION DATABASE
    #region ** ---------- SCRIPT OF TRANSACTION DATA BASE TABLE ----------**
    private static string LedgerOpeningScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.LedgerOpening(Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT Opening_Id, Module, ISNULL(Serial_No,1),Voucher_No, OP_Date, OP_Miti,Ledger_Id,SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Currency_Rate,1), ISNULL(Debit,0), ISNULL(LocalDebit,0), ISNULL(Credit,0), ISNULL(LocalCredit,0), Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, ISNULL(Branch_Id,{ObjGlobal.SysBranchId}), Company_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), ISNULL(IsReverse,1), CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.LedgerOpening lo
            WHERE lo.Voucher_No NOT IN (SELECT lo1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.LedgerOpening lo1); ";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.LedgerOpening; ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.LedgerOpening SET Serial_No = lo.Serial_No,OP_Date = lo.OP_Date,OP_Miti = lo.OP_Miti,Ledger_Id = lo.Ledger_Id,SubLedger_Id = lo.SubLedger_Id,Agent_Id = lo.Agent_Id,Cls1 = lo.Cls1,Cls2 = lo.Cls2,Cls3 = lo.Cls3,Cls4 = lo.Cls4,Currency_Id = ISNULL(lo.Currency_Id,{ObjGlobal.SysCurrencyId}),Currency_Rate = ISNULL(lo.Currency_Rate,1),Debit = ISNULL(lo.Debit,0),LocalDebit = ISNULL(lo.LocalDebit,0),Credit = ISNULL(lo.Credit,0),LocalCredit = ISNULL(lo.LocalCredit,0),Narration = lo.Narration,Remarks = lo.Remarks,Enter_By = ISNULL(lo.Enter_By,'MrSolution'),Enter_Date = ISNULL(lo.Enter_Date,GETDATE()),Reconcile_By = lo.Reconcile_By,Reconcile_Date = lo.Reconcile_Date,Branch_Id = ISNULL(lo.Branch_Id,{ObjGlobal.SysBranchId}),Company_Id = lo.Company_Id,FiscalYearId = IsNULL(lo.FiscalYearId,{ObjGlobal.SysFiscalYearId}),IsReverse = ISNULL(lo.IsReverse,0),CancelRemarks = lo.CancelRemarks,CancelBy = lo.CancelBy,CancelDate = lo.CancelDate,SyncBaseId = lo.SyncBaseId,SyncGlobalId = lo.SyncGlobalId,SyncOriginId = lo.SyncOriginId,SyncCreatedOn = lo.SyncCreatedOn,SyncLastPatchedOn = lo.SyncLastPatchedOn,SyncRowVersion = ISNULL(lo.SyncRowVersion,1)
            FROM {initial}.AMS.LedgerOpening lo
            WHERE lo.Voucher_No = LedgerOpening.Voucher_No AND lo.Opening_Id = LedgerOpening.Opening_Id; ";
        }

        return cmdString;
    }
    private static string CashBankVoucherScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.CB_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, IsReverse, CancelBy, CancelDate, CancelReason, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, CancelRemarks)
            SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Currency_Rate,1), Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, ISNULL(Audit_Lock,0), ClearingBy, ClearingDate, PrintValue, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, ISNULL(IsReverse,1), CancelBy, CancelDate, CancelReason, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1), CancelRemarks 
            FROM {initial}.AMS.CB_Master cbm
            WHERE cbm.Voucher_No NOT IN (SELECT cbm1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.CB_Master cbm1);

            INSERT INTO AMS.CB_Details(Voucher_No, SNo, CBLedgerId, Ledger_Id, SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT Voucher_No, ISNULL(SNo,1), CBLedgerId, Ledger_Id, SubLedger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, ISNULL(CurrencyId,{ObjGlobal.SysCurrencyId}), ISNULL(CurrencyRate,1), ISNULL(Debit,0), ISNULL(Credit,0), ISNULL(LocalDebit,0), ISNULL(LocalCredit,0), ISNULL(Tbl_Amount,0), ISNULL(V_Amount,0), Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.CB_Details cbd
            WHERE cbd.Voucher_No NOT IN (SELECT cbd1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.CB_Details cbd1);";
        }
        else if (tag is "RESET")
        {
            cmdString = @$"
             DELETE FROM {initial}.AMS.CB_Details;
             DELETE FROM {initial}.AMS.CB_Master; ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.CB_Master SET VoucherMode = cm.VoucherMode,Voucher_Date = cm.Voucher_Date,Voucher_Miti = cm.Voucher_Miti,Voucher_Time = cm.Voucher_Time,Ref_VNo = cm.Ref_VNo,Ref_VDate = cm.Ref_VDate,VoucherType = cm.VoucherType,Ledger_Id = cm.Ledger_Id,CheqNo = cm.CheqNo,CheqDate = cm.CheqDate,CheqMiti = cm.CheqMiti,Currency_Id = ISNULL(cm.Currency_Id,1),Currency_Rate = ISNULL(cm.Currency_Rate,1),Cls1 = cm.Cls1,Cls2 = cm.Cls2,Cls3 = cm.Cls3,Cls4 = cm.Cls4,Remarks = cm.Remarks,Action_Type = cm.Action_Type,EnterBy = cm.EnterBy,EnterDate = cm.EnterDate,ReconcileBy = cm.ReconcileBy,ReconcileDate = cm.ReconcileDate,Audit_Lock = cm.Audit_Lock,ClearingBy = cm.ClearingBy,ClearingDate = cm.ClearingDate,PrintValue = cm.PrintValue,CBranch_Id = cm.CBranch_Id,CUnit_Id = cm.CUnit_Id,FiscalYearId = cm.FiscalYearId,PAttachment1 = cm.PAttachment1,PAttachment2 = cm.PAttachment2,PAttachment3 = cm.PAttachment3,PAttachment4 = cm.PAttachment4,PAttachment5 = cm.PAttachment5,IsReverse = cm.IsReverse,CancelBy = cm.CancelBy,CancelDate = cm.CancelDate,CancelReason = cm.CancelReason,In_Words = cm.In_Words,SyncBaseId = cm.SyncBaseId,SyncGlobalId = cm.SyncGlobalId,SyncOriginId = cm.SyncOriginId,SyncCreatedOn = cm.SyncCreatedOn,SyncLastPatchedOn= cm.SyncLastPatchedOn,SyncRowVersion = ISNULL(cm.SyncRowVersion,1) ,CancelRemarks = cm.CancelRemarks,IsSynced = cm.IsSynced
            FROM {initial}.AMS.CB_Master cm
            WHERE CB_Master.Voucher_No = cm.Voucher_No;

            DELETE AMS.CB_Details
            WHERE Voucher_No IN (SELECT cd.Voucher_No FROM {initial}.AMS.CB_Details cd);
            INSERT INTO AMS.CB_Details (Voucher_No,SNo,CBLedgerId,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,CurrencyId,CurrencyRate,Debit,Credit,LocalDebit,LocalCredit,Tbl_Amount,V_Amount,Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT Voucher_No,SNo,CBLedgerId,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,ISNULL(CurrencyId,{ObjGlobal.SysCurrencyId}),ISNULL(CurrencyRate,1),ISNULL(Debit,0),ISNULL(Credit,0),ISNULL(LocalDebit,0),ISNULL(LocalCredit,0),ISNULL(Tbl_Amount,0),ISNULL(V_Amount,0),Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.CB_Details cbd
            WHERE cbd.Voucher_No NOT IN (SELECT cbd1.Voucher_No FROM AMS.CB_Details cbd1);";
        }

        return cmdString;
    }
    private static string ProductOpeningScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT OpeningId, Voucher_No, ISNULL(Serial_No,1), OP_Date, OP_Miti,Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Currency_Rate,1), ISNULL(AltQty,0),AltUnit, ISNULL(Qty,1),QtyUnit, ISNULL(Rate,0), ISNULL(LocalRate,0), ISNULL(Amount,0), ISNULL(LocalAmount,0), ISNULL(IsReverse,0), CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.ProductOpening pob
            WHERE pob.Voucher_No NOT IN (SELECT pob1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.ProductOpening pob1); ";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.ProductOpening; ";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.ProductOpening SET Serial_No=pob.Serial_No, OP_Date=pob.OP_Date, OP_Miti =pob.OP_Miti, Product_Id=pob.Product_Id, Godown_Id=pob.Godown_Id, Cls1 = pob.Cls1,Cls2 = pob.Cls2,Cls3 = pob.Cls3,Cls4 = pob.Cls4, Currency_Id=ISNULL(pob.Currency_Id,{ObjGlobal.SysCurrencyId}, ISNULL(pob.Currency_Rate,1), AltQty=pob.AltQty, AltUnit=pob.AltUnit, Qty=pob.Qty, QtyUnit=pob.QtyUnit, ISNULL(pob.Rate,0), ISNULL(pob.LocalRate,0), Amount=pob.Amount, LocalAmount=pob.LocalAmount, ISNULL(IsReverse,0), CancelRemarks=pob.CancelRemarks, CancelBy=pob.CancelBy, CancelDate=pob.CancelDate, Remarks=pob.Remarks, Enter_By = ISNULL(pob.Enter_By,'MrSolution'), Enter_Date = ISNULL(pob.Enter_Date,GETDATE()), Reconcile_By=pob.Reconcile_By, Reconcile_Date=pob.Reconcile_Date, ISNULL(pob.Branch_Id,{ObjGlobal.SysBranchId}, CUnit_Id=pob.CUnit_Id, ISNULL(pob.FiscalYearId,{ObjGlobal.SysFiscalYearId}, SyncBaseId = pob.SyncBaseId,SyncGlobalId = pob.SyncGlobalId,SyncOriginId = pob.SyncOriginId,SyncCreatedOn = pob.SyncCreatedOn,SyncLastPatchedOn = pob.SyncLastPatchedOn,SyncRowVersion = ISNULL(pob.SyncRowVersion,1)
            FROM {initial}.AMS.ProductOpening pob
            WHERE pob.Voucher_No = ProductOpening.Voucher_No AND pob.OpeningId = ProductOpening.OpeningId";
        }

        return cmdString;
    }
    private static string PostDatedChequeScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.PostDateCheque(VoucherNo, VoucherDate, VoucherTime, VoucherMiti, BankLedgerId, VoucherType, Status, BankName, BranchName, ChequeNo, ChqDate, ChqMiti, DrawOn, Amount, LedgerId, SubLedgerId, AgentId, Remarks, DepositedBy, DepositeDate, IsReverse, CancelReason, CancelBy, CancelDate, Cls1, Cls2, Cls3, Cls4, BranchId, CompanyUnitId, FiscalYearId, EnterBy, EnterDate, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT VoucherNo, VoucherDate, VoucherTime, VoucherMiti, BankLedgerId, VoucherType, ISNULL(Status,1), BankName, BranchName, ChequeNo, ChqDate, ChqMiti, ISNULL(DrawOn,0), ISNULL(Amount,0), LedgerId, SubLedgerId, AgentId, Remarks, DepositedBy, DepositeDate, ISNULL(IsReverse,1), CancelReason, CancelBy, CancelDate, Cls1, Cls2, Cls3, Cls4, ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), EnterBy, EnterDate, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PostDateCheque pdc
            WHERE pdc.PDCId NOT IN (SELECT pdc1.PDCId FROM AMS.PostDateCheque pdc1)";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PostDateCheque";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.PostDateCheque SET  VoucherDate=pdc.VoucherDate, VoucherTime=pdc.VoucherTime, VoucherMiti=pdc.VoucherMiti, BankLedgerId=pdc.BankLedgerId, VoucherType=pdc.VoucherType, Status=pdc.Status, BankName=pdc.BankName, BranchName=pdc.BranchName, ChequeNo=pdc.ChequeNo, ChqDate=pdc.ChqDate, ChqMiti=pdc.ChqMiti, DrawOn=pdc.DrawOn, Amount=pdc.Amount, LedgerId=pdc.LedgerId, SubLedgerId=pdc.SubLedgerId, AgentId=pdc.AgentId, Remarks=pdc.Remarks, DepositedBy=pdc., DepositeDate=pdc.DepositeDate, ISNULLIs(pdc.IsReverse,0), CancelReason=pdc.CancelReason, CancelBy=pdc.CancelBy, CancelDate=pdc.CancelDate, Cls1=pdc.Cls1, Cls2=pdc.Cls2, Cls3=pdc.Cls3, Cls4=pdc.Cls4, ISNULL(pdc.BranchId,0), CompanyUnitId=pdc.CompanyUnitId, ISNULL(pdc.FiscalYearId,{ObjGlobal.SysFiscalYearId}), EnterBy=ISNULL(pdc.EnterBy,'MrSolution), EnterDate=ISNULL(pdc.EnterDate,GETDATE()), PAttachment1=pdc.PAttachment1, PAttachment2=pdc.PAttachment2, PAttachment3=pdc.PAttachment3, PAttachment4=pdc.PAttachment4, PAttachment5=pdc.PAttachment5, SyncBaseId=pdc.SyncBaseId, SyncGlobalId=pdc.SyncGlobalId, SyncOriginId=pdc.OriginId, SyncCreatedOn=pdc.CreatedOn, SyncLastPatchedOn=pdc.SyncLastPatchedOn, SyncRowVersion=ISNULL(pdc.SyncRowVersion,1)
            FROM {initial}.AMS.PostDateCheque pdc
            WHERE pdc.VoucherNo = PostDateCheque.VoucherNo AND pdc.PDCId= PostDateCheque.PDCId";
        }

        return cmdString;
    }
    private static string JournalVoucherScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.JV_Master(VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,Currency_Id,Currency_Rate,Cls1,Cls2,Cls3,Cls4,N_Amount,Remarks,Action_Type,EnterBy,EnterDate,Audit_Lock,IsReverse,CancelBy,CancelDate,CancelReason,ReconcileBy,ReconcileDate,ClearingBy,ClearingDate,PrintValue,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,CancelRemarks,IsSynced)
            SELECT VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}),ISNULL(Currency_Rate,1),Cls1,Cls2,Cls3,Cls4,ISNULL(N_Amount,0),Remarks,Action_Type,EnterBy,EnterDate,isnull(Audit_Lock,0),isnull(IsReverse,1),CancelBy,CancelDate,CancelReason,ReconcileBy,ReconcileDate,ClearingBy,ClearingDate, PrintValue,ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}),CUnit_Id,ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}),PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1),CancelRemarks,ISNULL(IsSynced,0)
            FROM {initial}.AMS.JV_Master jm
            WHERE jm.Voucher_No NOT IN (SELECT jm1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.JV_Master jm1)

            INSERT INTO AMS.JV_Details (Voucher_No,SNo,CBLedger_Id,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,Chq_No,Chq_Date,CurrencyId,CurrencyRate,Debit,Credit,LocalDebit,LocalCredit,Narration,Tbl_Amount,V_Amount,Vat_Reg,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT Voucher_No,SNo,CBLedger_Id,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,Chq_No,Chq_Date,ISNULL(CurrencyId,{ObjGlobal.SysCurrencyId}),ISNULL(CurrencyRate,1),ISNULL(Debit,0),ISNULL(Credit,0),ISNULL(LocalDebit,0),ISNULL(LocalCredit,0),Narration,ISNULL(Tbl_Amount,0),ISNULL(V_Amount,0),Vat_Reg,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.JV_Details jd
            WHERE jd.Voucher_No NOT IN (SELECT jd1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.JV_Details jd1)
";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM {initial}.AMS.JV_Details; 
             DELETE FROM {initial}.AMS.JV_Master";
        }
        else if (tag is "UPDATE")
        {
            cmdString = $@"
            ";
        }

        return cmdString;
    }
    private static string CreditNoteScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.Notes_Master(VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,VoucherType,Ledger_Id,CheqNo,CheqDate,CheqMiti,SubLedger_Id,Agent_Id,Currency_Id,Currency_Rate,Cls1,Cls2,Cls3,Cls4,Remarks,Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,Audit_Lock,ClearingBy,ClearingDate,PrintValue,IsReverse,CancelBy,CancelDate,CancelReason,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,VoucherType,Ledger_Id,CheqNo,CheqDate,CheqMiti,SubLedger_Id,Agent_Id,ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}),ISNULL(Currency_Rate,1),Cls1,Cls2,Cls3,Cls4,Remarks,Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,isnull(Audit_Lock,0),ClearingBy,ClearingDate, ISNULL(PrintValue,0),ISNULL(IsReverse,1),CancelBy,CancelDate,CancelReason,ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}),CUnit_Id,ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}),PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1) 
            FROM {initial}.AMS.Notes_Master nm
            WHERE nm.Voucher_No NOT IN (SELECT nm1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.Notes_Master nm1 WHERE nm1.VoucherMode = 'CN') AND nm.VoucherMode = 'CN'

            INSERT INTO AMS.Notes_Details(VoucherMode,Voucher_No,SNo,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,Debit,Credit,LocalDebit,LocalCredit,Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,T_Amount,V_Amount,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT VoucherMode,Voucher_No,SNo,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,ISNULL(Debit,0),ISNULL(Credit,0),ISNULL(LocalDebit,0),ISNULL(LocalCredit,0),Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,ISNULL(T_Amount,0),ISNULL(V_Amount,0),SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.Notes_Details nd
            WHERE nd.Voucher_No NOT IN (SELECT nd1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.Notes_Details nd1 WHERE nd1.VoucherMode = 'CN') AND nd.VoucherMode = 'CN'";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.Notes_Details WHERE VoucherMode = 'CN'; 
            DELETE FROM {initial}.AMS.Notes_Master WHERE VoucherMode = 'CN';";
        }
        else if (tag is "UPDATE")
        {

        }

        return cmdString;
    }
    private static string DebitNoteScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.Notes_Master(VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,VoucherType,Ledger_Id,CheqNo,CheqDate,CheqMiti,Subledger_Id,Agent_Id,Currency_Id,Currency_Rate,Cls1,Cls2,Cls3,Cls4,Remarks,Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,Audit_Lock,ClearingBy,ClearingDate,PrintValue,IsReverse,CancelBy,CancelDate,CancelReason,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,VoucherType,Ledger_Id,CheqNo,CheqDate,CheqMiti,SubLedger_Id,Agent_Id,ISNULL(Currency_Id,{ObjGlobal.SysCurrencyId}),ISNULL(Currency_Rate,1),Cls1,Cls2,Cls3,Cls4,Remarks,Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,isnull(Audit_Lock,0),ClearingBy,ClearingDate, ISNULL(PrintValue,0),ISNULL(IsReverse,1),CancelBy,CancelDate,CancelReason,ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}),CUnit_Id,ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}),PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1) 
            FROM {initial}.AMS.Notes_Master nm
            WHERE nm.Voucher_No NOT IN (SELECT nm1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.Notes_Master nm1 WHERE nm1.VoucherMode = 'DN') AND nm.VoucherMode = 'DN'

            INSERT INTO AMS.Notes_Details(VoucherMode,Voucher_No,SNo,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,Debit,Credit,LocalDebit,LocalCredit,Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,T_Amount,V_Amount,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT VoucherMode,Voucher_No,SNo,Ledger_Id,SubLedger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,ISNULL(Debit,0),ISNULL(Credit,0),ISNULL(LocalDebit,0),ISNULL(LocalCredit,0),Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,ISNULL(T_Amount,0),ISNULL(V_Amount,0),SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.Notes_Details nd
            WHERE nd.Voucher_No NOT IN (SELECT nd1.Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.Notes_Details nd1 WHERE nd1.VoucherMode = 'DN') AND nd.VoucherMode = 'DN'";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.Notes_Details WHERE VoucherMode = 'DN'; 
            DELETE FROM {initial}.AMS.Notes_Master WHERE VoucherMode = 'DN';";
        }

        return cmdString;
    }
    private static string BillOfMaterialsScript(string tag, string initial)
    {
        var script = string.Empty;
        if (tag is "SAVE")
        {
            script = @$"
			INSERT INTO INV.BOM_Master (VoucherNo, VDate, VMiti, VTime, FinishedGoodsId, FinishedGoodsQty, DepartmentId, CostCenterId, Amount, InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, OrderNo, OrderDate, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT VoucherNo, VDate, VMiti, VTime, FinishedGoodsId, ISNULL(FinishedGoodsQty,0), DepartmentId, CostCenterId, ISNULL(Amount,0), InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, OrderNo, OrderDate, EnterBy, EnterDate, ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.INV.BOM_Master bom 
            WHERE bom.VoucherNo NOT IN (SELECT bom1.VoucherNo COLLATE DATABASE_DEFAULT FROM INV.BOM_Master bom1);
            
            INSERT INTO INV.BOM_Details (VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT VoucherNo, ISNULL(SerialNo,1), ProductId, GodownId, CostCenterId, OrderNo, OrderSNo, ISNULL(AltQty,0), AltUnitId, ISNULL(Qty,0), UnitId, ISNULL(Rate,0), ISNULL(Amount,0), Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.INV.BOM_Details bod 
            WHERE bod.VoucherNo NOT IN (SELECT bod1.VoucherNo COLLATE DATABASE_DEFAULT FROM INV.BOM_Details bod1 );";

        }
        else if (tag is "RESET")
        {
            script = $@"
             DELETE FROM {initial}.AMS.BillOfMaterial_Details; 
             DELETE FROM {initial}.AMS.BillOfMaterial_Master;";
        }

        return script;
    }
    private static string PurchaseOrderScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.PO_Master (PO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Vendor_ID,PartyLedgerId,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,ChqMiti,Invoice_Type,Invoice_In,DueDays,DueDate,Agent_Id,SubLedger_Id,PIN_Invoice,PIN_Date,PQT_Invoice,PQT_Date,Cls1,Cls2,Cls3,Cls4,Cur_Id,Cur_Rate,B_Amount,T_Amount,N_Amount,LN_Amount,V_Amount,Tbl_Amount,Action_type,R_Invoice,CancelBy,CancelDate,CancelRemarks,No_Print,In_Words,Remarks,Audit_Lock,Enter_By,Enter_Date,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,IsReverse,IsSynced)
            SELECT PO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Vendor_ID,PartyLedgerId,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,ChqMiti,Invoice_Type,Invoice_In,DueDays,DueDate,Agent_Id,SubLedger_Id,PIN_Invoice,PIN_Date,PQT_Invoice,PQT_Date,Cls1,Cls2,Cls3,Cls4,ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}),ISNULL(Cur_Rate,1),ISNULL(B_Amount,0),ISNULL(T_Amount,0),ISNULL(N_Amount,0),ISNULL(LN_Amount,0),ISNULL(V_Amount,0),ISNULL(Tbl_Amount,0),Action_type,ISNULL(R_Invoice,0),CancelBy,CancelDate,CancelRemarks,ISNULL(No_Print,0),In_Words,Remarks,ISNULL(Audit_Lock,0),Enter_By,Enter_Date,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}),CUnit_Id,ISNULL(FiscalYearId,1),PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1),ISNULL(IsReverse,0),ISNULL(IsSynced,0)
            FROM {initial}.AMS.PO_Master pm 
            WHERE pm.PO_Invoice NOT IN (SELECT pm1.PO_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PO_Master pm1);

            INSERT INTO AMS.PO_Details (PO_Invoice,Invoice_SNo,P_Id,Gdn_Id,Alt_Qty,Alt_UnitId,Qty,Unit_Id,Rate,B_Amount,T_Amount,N_Amount,AltStock_Qty,Stock_Qty,Narration,PIN_Invoice,PIN_Sno,PQT_Invoice,PQT_SNo,Tax_Amount,V_Amount,V_Rate,Issue_Qty,Free_Unit_Id,Free_Qty,StockFree_Qty,ExtraFree_Unit_Id,ExtraFree_Qty,ExtraStockFree_Qty,T_Product,P_Ledger,PR_Ledger,SZ1,SZ2,SZ3,SZ4,SZ5,SZ6,SZ7,SZ8,SZ9,SZ10,Serial_No,Batch_No,Exp_Date,Manu_Date,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT PO_Invoice,ISNULL(Invoice_SNo,1),P_Id,Gdn_Id,ISNULL(Alt_Qty,0),Alt_UnitId,ISNULL(Qty,0),Unit_Id,ISNULL(Rate,0),ISNULL(B_Amount,0),ISNULL(T_Amount,0),ISNULL(N_Amount,0),ISNULL(AltStock_Qty,0),ISNULL(Stock_Qty,0),Narration,PIN_Invoice,PIN_Sno,PQT_Invoice,PQT_SNo,ISNULL(Tax_Amount,0),ISNULL(V_Amount,0),ISNULL(V_Rate,0),ISNULL(Issue_Qty,0),Free_Unit_Id,ISNULL(Free_Qty,0),ISNULL(StockFree_Qty,0),ExtraFree_Unit_Id,ISNULL(ExtraFree_Qty,0),ISNULL(ExtraStockFree_Qty,0),T_Product,P_Ledger,PR_Ledger,SZ1,SZ2,SZ3,SZ4,SZ5,SZ6,SZ7,SZ8,SZ9,SZ10,Serial_No,Batch_No,Exp_Date,Manu_Date,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1) 
            FROM {initial}.AMS.PO_Details pd
            WHERE pd.PO_Invoice NOT IN (SELECT pd1.PO_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PO_Details pd1);

            INSERT INTO AMS.PO_Term (PO_VNo,PT_Id,SNo,Term_Type,Product_Id,Rate,Amount,Taxable,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
            SELECT PO_VNo,PT_Id,SNo,Term_Type,Product_Id,ISNULL(Rate,0),ISNULL(Amount,0),Taxable,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PO_Term pt 
            WHERE pt.PO_VNo NOT IN ( SELECT pt1.PO_VNo COLLATE DATABASE_DEFAULT FROM AMS.PO_Term pt1); ";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PO_Term;
            DELETE FROM {initial}.AMS.PO_Details;
            DELETE FROM {initial}.AMS.PO_Master;";
        }

        return cmdString;
    }
    private static string PurchaseIndentScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
			INSERT INTO AMS.PIN_Master (PIN_Invoice, PIN_Date, PIN_Miti, Person, Sub_Ledger, Cls1, Cls2, Cls3, Cls4, EnterBY, EnterDate, ActionType, Remarks, Print_value, CancelBy, CancelDate, CancelRemarks, FiscalYearId, BranchId, CompanyUnitId)
			SELECT PIN_Invoice, PIN_Date, PIN_Miti, Person, Sub_Ledger, Cls1, Cls2, Cls3, Cls4, EnterBY, EnterDate, ActionType, Remarks, ISNULL(Print_value,0), CancelBy, CancelDate, CancelRemarks, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId
			FROM {initial}.AMS.PIN_Master pm
            WHERE pm.PIN_Invoice NOT IN (SELECT pm1.PIN_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PIN_Master pm1);

			INSERT INTO AMS.PIN_Details (PIN_Invoice, SNo, P_Id, Gdn_Id, Alt_Qty, Alt_Unit, Qty, Unit, AltStock_Qty, StockQty, Issue_Qty, Narration)
			SELECT PIN_Invoice, SNo, P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_Unit, ISNULL(Qty,0), Unit, ISNULL(AltStock_Qty,0), ISNULL(StockQty,0), ISNULL(Issue_Qty,0), Narration
			FROM {initial}.AMS.PIN_Details pd
            WHERE pd.PIN_Invoice NOT IN (SELECT pd1.PIN_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PIN_Details pd1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PIN_Details;
            DELETE FROM {initial}.AMS.PIN_Master;";
        }

        return cmdString;
    }
    private static string PurchaseChallanScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
			INSERT INTO AMS.PC_Master (PC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, PO_Invoice, PO_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT PC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, PO_Invoice, PO_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
			FROM {initial}.AMS.PC_Master pm
            WHERE pm.PC_Invoice NOT IN (SELECT pm1.PC_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PC_Master pm1);

			INSERT INTO AMS.PC_Details (PC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT PC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
			FROM {initial}.AMS.PC_Details pd
            WHERE pd.PC_Invoice NOT IN (SELECT pd1.PC_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PC_Details pd1);

			INSERT INTO AMS.PC_Term (PC_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT PC_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
			FROM {initial}.AMS.PC_Term pt
            WHERE pt.PC_VNo NOT IN (SELECT pt1.PC_VNo COLLATE DATABASE_DEFAULT FROM AMS.PC_Term pt1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PC_Term;
            DELETE FROM {initial}.AMS.PC_Details;
            DELETE FROM {initial}.AMS.PC_Master;";
        }

        return cmdString;
    }
    private static string PurchaseReturnScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.PR_Master(PR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, SubLedger_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, R_Invoice, CancelBy, CancelDate, CancelRemarks, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, SubLedger_Id, Cls1, Cls2, Cls3, Cls4, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Change_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), Action_type, ISNULL(No_Print,0), In_Words, Remarks, ISNULL(Audit_Lock,0), Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, R_Invoice, CancelBy, CancelDate, CancelRemarks, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PR_Master pm
            WHERE pm.PR_Invoice NOT IN(SELECT pm1.PR_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PR_Master pm1)
            
            INSERT INTO AMS.PR_Details(PR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PB_Invoice, PB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PR_Invoice, ISNULL(Invoice_SNo,1), P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, PB_Invoice, ISNULL(PB_Sno,0), ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PR_Details pd
            WHERE pd.PR_Invoice NOT IN(SELECT pd1.PR_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PR_Details pd1)

            INSERT INTO AMS.PR_Term(PR_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PR_VNo, PT_Id, ISNULL(SNo,0), Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0), Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PR_Term pt
            WHERE pt.PR_VNo NOT IN(SELECT pt1.PR_VNo COLLATE DATABASE_DEFAULT FROM AMS.PR_Term pt1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PR_Term;
           DELETE FROM {initial}.AMS.PR_Details;
           DELETE FROM {initial}.AMS.PR_Master;";
        }
        return cmdString;
    }
    private static string SalesQuotationScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
            INSERT INTO AMS.SQ_Master(SQ_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Expiry_Date, Ref_Vno, Ref_VDate, Ref_VMiti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, IND_Invoice, IND_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Cancel_By, Cancel_Date, Cancel_Remarks, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CUnit_Id, CBranch_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SQ_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Expiry_Date, Ref_Vno, Ref_VDate, Ref_VMiti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, IND_Invoice, IND_Date, Cls1, Cls2, Cls3, Cls4, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Return_Amount,0), Action_Type, In_Words, Remarks, ISNULL(R_Invoice,0), Cancel_By, Cancel_Date, Cancel_Remarks, ISNULL(Is_Printed,0), ISNULL(No_Print,0), Printed_By, Printed_Date, ISNULL(Audit_Lock,0), Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CUnit_Id, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)	
            FROM {initial}.AMS.SQ_Master sm
            WHERE sm.SQ_Invoice NOT IN (SELECT sm1.SQ_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SQ_Master sm1);

			INSERT INTO AMS.SQ_Details (SQ_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, PG_Id, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SQ_Invoice, ISNULL(Invoice_SNo,0), P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, IND_Invoice, IND_Sno, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), ISNULL(Issue_Qty,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), PG_Id, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SQ_Details sd
            WHERE sd.SQ_Invoice NOT IN (SELECT sd1.SQ_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SQ_Details sd1);

			INSERT INTO AMS.SQ_Term (SQ_Vno, ST_Id, SNo, Term_Type, Product_Id,Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SQ_Vno, ST_Id, ISNULL(SNo,0), Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0) , Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1) 
			FROM {initial}.AMS.SQ_Term st 
            WHERE st.SQ_Vno NOT IN (SELECT st1.SQ_Vno COLLATE DATABASE_DEFAULT FROM AMS.SQ_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"         
           DELETE FROM {initial}.AMS.SQ_Term;
           DELETE FROM {initial}.AMS.SQ_Details;
           DELETE FROM {initial}.AMS.SQ_Master;";
        }

        return cmdString;
    }
    private static string SalesChallanScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
		    INSERT INTO AMS.SC_Master(SC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, QOT_Invoice, QOT_Date, SO_Invoice, SO_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, IsSynced)
            SELECT SC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, ISNULL(Customer_Id,1), PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, QOT_Invoice, QOT_Date, SO_Invoice, SO_Date, Cls1, Cls2, Cls3, Cls4, CounterId, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Return_Amount,0), Action_Type, ISNULL(R_Invoice,0), CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1), ISNULL(IsSynced,0)
            FROM {initial}.AMS.SC_Master sm
            WHERE sm.SC_Invoice NOT IN (SELECT sm1.SC_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SC_Master sm1);

            INSERT INTO AMS.SC_Master_OtherDetails(SC_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails)
            SELECT SC_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails
            FROM {initial}.AMS.SC_Master_OtherDetails smd
            WHERE smd.SC_Invoice NOT IN(SELECT smd1.SC_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SC_Master_OtherDetails smd1);

			INSERT INTO AMS.SC_Details(SC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, QOT_Invoice, QOT_Sno, SO_Invoice, SO_SNo, Tax_Amount, V_Amount, V_Rate, AltIssue_Qty, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SC_Invoice, ISNULL(Invoice_SNo,1), P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, QOT_Invoice, QOT_Sno, SO_Invoice, SO_SNo, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), ISNULL(AltIssue_Qty,0), ISNULL(Issue_Qty,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SC_Details sd
            WHERE sd.SC_Invoice NOT IN(SELECT sd1.SC_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SC_Details sd1);

			INSERT INTO AMS.SC_Term(SC_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SC_Vno, ST_Id, ISNULL(SNo,1), Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0), Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SC_Term st
            WHERE st.SC_Vno NOT IN(SELECT st1.SC_Vno COLLATE DATABASE_DEFAULT FROM AMS.SC_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.SC_Term;
            DELETE FROM {initial}.AMS.SC_Details;
            DELETE FROM {initial}.AMS.SC_Master_OtherDetails;
            DELETE FROM {initial}.AMS.SC_Master;";
        }

        return cmdString;

    }
    private static string SalesOrderScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = @$"
			INSERT INTO AMS.SO_Master (SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_VNo, Ref_Date, Ref_Miti, Customer_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_ID, SubLedger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date,  CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_VNo, Ref_Date, Ref_Miti, ISNULL(Customer_ID,1), Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_ID, SubLedger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, CounterId, TableId, CombineTableId, NoOfPerson, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Return_Amount,0), Action_Type, In_Words, Remarks, R_Invoice, ISNULL(Is_Printed,0), ISNULL(No_Print,0), Printed_By, Printed_Date, ISNULL(Audit_Lock,0), Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}),CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SO_Master sm 
            WHERE sm.SO_Invoice NOT IN (SELECT sm1.SO_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SO_Master sm1);

            INSERT INTO AMS.SO_Master_OtherDetails(SO_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails)
            SELECT SO_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails
            FROM {initial}.AMS.SO_Master_OtherDetails sod
            WHERE sod.SO_Invoice NOT IN(SELECT sod1.SO_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SO_Master_OtherDetails sod1);

			INSERT INTO AMS.SO_Details (SO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SO_Invoice, ISNULL(Invoice_SNo,1), P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, IND_Invoice, IND_Sno, QOT_Invoice, QOT_SNo, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), ISNULL(Issue_Qty,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, Notes, PrintedItem, PrintKOT, OrderTime, Print_Time, Is_Canceled, CancelNotes, ISNULL(PDiscountRate,0), ISNULL(PDiscount,0), ISNULL(BDiscountRate,0), ISNULL(BDiscount,0), ISNULL(ServiceChargeRate,0), ISNULL(ServiceCharge,0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SO_Details sd
            WHERE sd.SO_Invoice NOT IN (SELECT sd1.SO_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SO_Details sd1);

			INSERT INTO AMS.SO_Term (SO_Vno, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SO_Vno, ST_Id, ISNULL(SNo,1), Term_Type, ISNULL(Rate,0), ISNULL(Amount,0), Product_Id, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SO_Term st 
            WHERE st.SO_Vno NOT IN (SELECT st1.SO_Vno COLLATE DATABASE_DEFAULT FROM AMS.SO_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"     
            DELETE FROM {initial}.AMS.SO_Term;
            DELETE FROM {initial}.AMS.SO_Details;
            DELETE FROM {initial}.AMS.SO_Master_OtherDetails;
            DELETE FROM {initial}.AMS.SO_Master;";
        }
        return cmdString;
    }
    private static string SalesReturnScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.SR_Master (SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, Customer_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, Cancel_By, Cancel_Date, Cancel_Remarks, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, ISNULL(Customer_ID,1), PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Return_Amount,0), Action_Type, In_Words, Remarks, ISNULL(R_Invoice,0), ISNULL(Is_Printed,0), Cancel_By, Cancel_Date, Cancel_Remarks, ISNULL(No_Print,0), Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SR_Master sm
            WHERE sm.SR_Invoice NOT IN (SELECT sm1.SR_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SR_Master sm1);

            INSERT INTO AMS.SR_Master_OtherDetails (SR_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails)
			SELECT SR_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails
			FROM {initial}.AMS.SR_Master_OtherDetails smod 
            WHERE smod.SR_Invoice NOT IN (SELECT smod1.SR_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SR_Master_OtherDetails smod1);

			INSERT INTO AMS.SR_Details (SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, SB_Invoice, SB_Sno, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, ISNULL(PDiscountRate,0), ISNULL(PDiscount,0), ISNULL(BDiscountRate,0), ISNULL(BDiscount,0), ISNULL(ServiceChargeRate,0), ISNULL(ServiceCharge,0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SR_Details sd 
            WHERE sd.SR_Invoice NOT IN (SELECT sd1.SR_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SR_Details sd1);

			INSERT INTO AMS.SR_Term (SR_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
			SELECT SR_VNo, ST_Id, ISNULL(SNo,1), Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0), Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.SR_Term st
            WHERE st.SR_VNo NOT IN (SELECT st1.SR_VNo COLLATE DATABASE_DEFAULT FROM AMS.SR_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.SR_Term;
            DELETE FROM {initial}.AMS.SR_Details; 
            DELETE FROM {initial}.AMS.SR_Master_OtherDetails; 
            DELETE FROM {initial}.AMS.SR_Master;";
        }

        return cmdString;
    }
    private static string StockAdjustmentScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.STA_Master (StockAdjust_No, VDate, VMiti, Vtime, DepartmentId, BarCode, PhyStockNo, Posting, Export, ReconcileBy, AuditBy, AuditDate, AuthorizeBy, AuthorizeDate, AuthorizeRemarks, PostedBy, PostedDate, Remarks, Status, EnterBy, EnterDate, PrintValue, IsReverse, CancelBy, CancelDate, CancelReason, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT StockAdjust_No, VDate, VMiti, Vtime, DepartmentId, BarCode, PhyStockNo, Posting, Export, ReconcileBy, AuditBy, AuditDate, AuthorizeBy, AuthorizeDate, AuthorizeRemarks, PostedBy, PostedDate, Remarks, ISNULL(Status,1), EnterBy, EnterDate, PrintValue, ISNULL(IsReverse,1), CancelBy, CancelDate, CancelReason, ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
			FROM {initial}.AMS.STA_Master sm
            WHERE sm.StockAdjust_No NOT IN (SELECT sm1.StockAdjust_No COLLATE DATABASE_DEFAULT FROM AMS.STA_Master sm1)

			INSERT INTO AMS.STA_Details (StockAdjust_No, Sno, ProductId, GodownId, AdjType, AltQty, AltUnitId, Qty, UnitId, AltStockQty, StockQty, Rate, NetAmount, AddDesc, DepartmentId, StConvRatio, PhyStkNo, PhyStkSno)
			SELECT StockAdjust_No, ISNULL(Sno,1), ProductId, GodownId, AdjType, ISNULL(AltQty,0), AltUnitId, ISNULL(Qty,0), UnitId, ISNULL(AltStockQty,0), ISNULL(StockQty,0), ISNULL(Rate,0), ISNULL(NetAmount,0), AddDesc, DepartmentId, StConvRatio, PhyStkNo, PhyStkSno
			FROM {initial}.AMS.STA_Details sd 
            WHERE sd.StockAdjust_No NOT IN (SELECT sd1.StockAdjust_No COLLATE DATABASE_DEFAULT FROM AMS.STA_Details sd1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.STA_Details; 
            DELETE FROM {initial}.AMS.STA_Master;";
        }

        return cmdString;
    }
    private static string SampleCostingScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.SampleCosting_Master(CostingNo, CostingDate, CostingMiti, CostingTime, CostCenterExpenseVno, CostCenterExpenseDate, FinalProductId, UnitId, GodownId, CostCenterId, DepartmentId, TotalQty, CostRatio, NetAmount, Remarks, ActionType, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT CostingNo, CostingDate, CostingMiti, CostingTime, CostCenterExpenseVno, CostCenterExpenseDate, FinalProductId, UnitId, GodownId, CostCenterId, DepartmentId, ISNULL(TotalQty,0), ISNULL(CostRatio,0), ISNULL(NetAmount,0), Remarks, ActionType, EnterBy, EnterDate, ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SampleCosting_Master scm
            WHERE scm.CostingNo NOT IN(SELECT scm1.CostingNo COLLATE DATABASE_DEFAULT FROM AMS.SampleCosting_Master scm1);

            INSERT INTO AMS.SampleCosting_Details(CostingNo, SerialNo, ProductId, GodownId, CostCenterId, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, CostCenterExpenseVno, CostCenterExpenseSno)
            SELECT CostingNo, ISNULL(SerialNo,1), ProductId, GodownId, CostCenterId, ISNULL(AltQty,0), AltUnitId, ISNULL(Qty,0), UnitId, ISNULL(Rate,0), ISNULL(Amount,0), Narration, CostCenterExpenseVno, CostCenterExpenseSno
            FROM {initial}.AMS.SampleCosting_Details scd
            WHERE scd.CostingNo NOT IN(SELECT scd1.CostingNo COLLATE DATABASE_DEFAULT FROM AMS.SampleCosting_Details scd1);";
        }
        else if (tag is ":RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.SampleCosting_Details;
            DELETE FROM {initial}.AMS.SampleCosting_Master;";
        }

        return cmdString;
    }
    private static string PurchaseInvoiceScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.PB_Master(PB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, SubLedger_Id, PO_Invoice, PO_Date, PC_Invoice, PC_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelRemarks, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, SubLedger_Id, PO_Invoice, PO_Date, PC_Invoice, PC_Date, Cls1, Cls2, Cls3, Cls4, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), Counter_ID, ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Change_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), Action_type, R_Invoice, CancelBy, CancelDate, CancelRemarks, ISNULL(No_Print,0), In_Words, Remarks, ISNULL(Audit_Lock,0), Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PB_Master pm
            WHERE pm.PB_Invoice NOT IN(SELECT pm1.PB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PB_Master pm1);

            INSERT INTO AMS.PB_OtherMaster(PAB_Invoice, PPNo, PPDate, TaxableAmount, VatAmount, CustomAgent, Transportation, VechileNo, Cn_No, Cn_Date, BankDoc)
            SELECT PAB_Invoice, PPNo, PPDate, TaxableAmount, VatAmount, CustomAgent, Transportation, VechileNo, Cn_No, Cn_Date, BankDoc
            FROM {initial}.AMS.PB_OtherMaster po
            WHERE po.PAB_Invoice NOT IN(SELECT po1.PAB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PB_OtherMaster po1);

            INSERT INTO ams.PB_Details(PB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, PC_Invoice, PC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, TaxExempted_Amount, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PB_Invoice, ISNULL(Invoice_SNo,1), P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, PO_Invoice, PO_Sno, PC_Invoice, PC_SNo, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, ISNULL(TaxExempted_Amount,0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PB_Details pd
            WHERE pd.PB_Invoice NOT IN(SELECT pd1.PB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PB_Details pd1);

            INSERT INTO AMS.PB_Term(PB_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PB_VNo, PT_Id, ISNULL(SNo,1), Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0), Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PB_Term pt
            WHERE pt.PB_VNo NOT IN(SELECT pt1.PB_VNo COLLATE DATABASE_DEFAULT FROM AMS.PB_Term pt1); ";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.PB_Term;
            DELETE FROM {initial}.AMS.PB_Details; 
            DELETE FROM {initial}.AMS.PB_Master_OtherDetails; 
            DELETE FROM {initial}.AMS.PB_Master;";
        }

        return cmdString;
    }
    private static string SalesInvoiceScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.SB_Master(SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Cancel_By, Cancel_Date, Cancel_Remarks, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, DoctorId, PatientId, HDepartmentId, MShipId, TableId, CBranch_Id, CUnit_Id, FiscalYearId, IsAPIPosted, IsRealtime, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, ISNULL(Customer_Id,1), PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, SubLedger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(LN_Amount,0), ISNULL(V_Amount,0), ISNULL(Tbl_Amount,0), ISNULL(Tender_Amount,0), ISNULL(Return_Amount,0), Action_Type, In_Words, Remarks, ISNULL(R_Invoice,0), Cancel_By, Cancel_Date, Cancel_Remarks, ISNULL(Is_Printed,0), ISNULL(No_Print,0), Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, DoctorId, PatientId, HDepartmentId, MShipId, TableId, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), IsAPIPosted, IsRealtime, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SB_Master sm
            WHERE sm.SB_Invoice NOT IN(SELECT sm1.SB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SB_Master sm1);

            INSERT INTO AMS.SB_ExchangeDetails(SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, ExchangeGLD, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, N_Amount)
            SELECT SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, ExchangeGLD, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(N_Amount,0)
            FROM {initial}.AMS.SB_ExchangeDetails se
            WHERE se.SB_Invoice NOT IN(SELECT se1.SB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SB_ExchangeDetails se1);

            INSERT INTO  AMS.SB_Master_OtherDetails(SB_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails)
            SELECT SB_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails
            FROM {initial}.AMS.SB_Master_OtherDetails so
            WHERE so.SB_Invoice NOT IN(SELECT so1.SB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SB_Master_OtherDetails so1);

            INSERT INTO ams.SB_Details(SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, ISNULL(Alt_Qty,0), Alt_UnitId, ISNULL(Qty,0), Unit_Id, ISNULL(Rate,0), ISNULL(B_Amount,0), ISNULL(T_Amount,0), ISNULL(N_Amount,0), ISNULL(AltStock_Qty,0), ISNULL(Stock_Qty,0), Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, ISNULL(Tax_Amount,0), ISNULL(V_Amount,0), ISNULL(V_Rate,0), Free_Unit_Id, ISNULL(Free_Qty,0), ISNULL(StockFree_Qty,0), ExtraFree_Unit_Id, ISNULL(ExtraFree_Qty,0), ISNULL(ExtraStockFree_Qty,0), T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, ISNULL(PDiscountRate,0), ISNULL(PDiscount,0), ISNULL(BDiscountRate,0), ISNULL(BDiscount,0), ISNULL(ServiceChargeRate,0), ISNULL(ServiceCharge,0), SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SB_Details sd
            WHERE sd.SB_Invoice NOT IN(SELECT sd1.SB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.SB_Details sd1);
           
            INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT SB_VNo, ST_Id, SNo, Term_Type, Product_Id, ISNULL(Rate,0), ISNULL(Amount,0), Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.SB_Term st
            WHERE st.SB_VNo NOT IN(SELECT st1.SB_VNo COLLATE DATABASE_DEFAULT FROM AMS.SB_Term st1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.AMS.SB_Term;
            DELETE FROM {initial}.AMS.SB_Details;
            DELETE FROM {initial}.AMS.SB_Master_OtherDetails;
            DELETE FROM {initial}.AMS.SB_ExchangeDetails;
            DELETE FROM {initial}.AMS.SB_Master;";
        }

        return cmdString;
    }
    private static string PurchaseAdditionalInvoiceScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO AMS.PAB_Master(PAB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, PB_Qty, PB_Amount, LocalAmount, Cls1, Cls2, Cls3, Cls4, Agent_Id, Cur_Id, Cur_Rate, T_Amount, Remarks, Action_Type, No_Print, In_Words, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, R_Invoice, Cancel_By, Cancel_Date, Cancel_Remarks, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT PAB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, ISNULL(PB_Qty,0), ISNULL(PB_Amount,0), ISNULL(LocalAmount,0), Cls1, Cls2, Cls3, Cls4, Agent_Id, ISNULL(Cur_Id,{ObjGlobal.SysCurrencyId}), ISNULL(Cur_Rate,1), ISNULL(T_Amount,0), Remarks, Action_Type, ISNULL(No_Print,0), In_Words, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, R_Invoice, Cancel_By, Cancel_Date, Cancel_Remarks, ISNULL(CBranch_Id,{ObjGlobal.SysBranchId}), CUnit_Id, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.AMS.PAB_Master pm
            WHERE pm.PAB_Invoice NOT IN (SELECT pm1.PAB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PAB_Master pm1);

            INSERT INTO AMS.PAB_Details(PAB_Invoice, SNo, PT_Id, Ledger_Id, CBLedger_Id, SubLedger_Id, Agent_Id, DepartmentId, Product_Id, Percentage, Amount, N_Amount, Term_Type, PAB_Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, FiscalYearId)
            SELECT PAB_Invoice, SNo, PT_Id, Ledger_Id, CBLedger_Id, SubLedger_Id, Agent_Id, DepartmentId, Product_Id, Percentage, Amount, N_Amount, Term_Type, PAB_Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1), ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId})
            FROM {initial}.AMS.PAB_Details pd
            WHERE pd.PAB_Invoice NOT IN (SELECT pd1.PAB_Invoice COLLATE DATABASE_DEFAULT FROM AMS.PAB_Details pd1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
             DELETE FROM {initial}.AMS.PAB_Details;
             DELETE FROM {initial}.AMS.PAB_Master;";
        }

        return cmdString;
    }
    private static string ProductionMemoScript(string tag, string initial)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE")
        {
            cmdString = $@"
            INSERT INTO INV.Production_Master(VoucherNo, VDate, VMiti, VTime, BOMVNo, BOMDate, FinishedGoodsId, FinishedGoodsQty, Costing, Machine, DepartmentId, CostCenterId, Amount, InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsCancel, CancelBy, CancelDate, CancelReason, IsReturn, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, IssueNo, IssueDate, OrderNo, OrderDate, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, Source, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncGlobalId, SyncOriginId, SyncBaseId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT VoucherNo, VDate, VMiti, VTime, BOMVno, BOMDate, FinishedGoodsId, FinishedGoodsQty, ISNULL(Costing,0), Machine, DepartmentId, CostCenterId, ISNULL(Amount,0), InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, ISNULL(IsCancel,0), CancelBy, CancelDate, CancelReason, ISNULL(IsReturn,0), IsReconcile, ReconcileBy, ReconcileDate, ISNULL(IsPosted,0), PostedBy, PostedDate, IssueNo, IssueDate, OrderNo, OrderDate, EnterBy, EnterDate, ISNULL(BranchId,{ObjGlobal.SysBranchId}), CompanyUnitId, ISNULL(FiscalYearId,{ObjGlobal.SysFiscalYearId}), Source, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncGlobalId, SyncOriginId, SyncBaseId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.INV.Production_Master pm
            WHERE pm.VoucherNo NOT IN(SELECT pm1.VoucherNo COLLATE DATABASE_DEFAULT FROM INV.Production_Master pm1);

            INSERT INTO INV.Production_Details(VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, BOMNo, BOMSno, BOMQty, IssueNo, IssueSNo, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT VoucherNo, ISNULL(SerialNo,1), ProductId, GodownId, CostCenterId, BOMNo, BOMSno, ISNULL(BOMQty,0), IssueNo, IssueSNo, OrderNo, OrderSNo, ISNULL(AltQty,0), AltUnitId, ISNULL(Qty,0), UnitId, ISNULL(Rate,0), ISNULL(Amount,0), Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
            FROM {initial}.INV.Production_Details pd
            WHERE pd.VoucherNo NOT IN(SELECT pd1.VoucherNo COLLATE DATABASE_DEFAULT FROM INV.Production_Details pd1);";
        }
        else if (tag is "RESET")
        {
            cmdString = $@"
            DELETE FROM {initial}.INV.Production_Details;
            DELETE FROM {initial}.INV.Production_Master;";
        }

        return cmdString;
    }
    #endregion



    // RETURN VALUE IN DATA TABLE
    #region --------------- RETURN DATATABLE ---------------
    public DataTable GetCompanyList(string source, string userId, string userName)
    {
        var dtReturn = new DataTable();
        return dtReturn;
    }
    public DataTable GetServerInfo(string source)
    {
        var maxIdData = 0;
        source = ObjGlobal.Encrypt(source);
        var dtMaxId = SqlExtensions.ExecuteDataSet($"SELECT MAX(il.LogId) LogId FROM CTRL.ImportLog il WHERE il.ImportType='{source}'").Tables[0];
        if (dtMaxId.Rows.Count > 0)
        {
            maxIdData = ObjGlobal.ReturnInt(dtMaxId.Rows[0][0].ToString());
        }

        var cmdTxt = $"SELECT il.ServerDesc,il.ServerUser,il.ServerPassword,il.dbInitial,il.dbCompanyInfo FROM CTRL.ImportLog il WHERE il.LogId={maxIdData}; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }
    #endregion --------------- RETURN DATATABLE ---------------



    // OBJECT FOR THIS FORM
    #region ---------------- OBJECT -----------------
    public ImportLog ImportLog { get; set; }
    #endregion ----------------- OBJECT -----------------

}