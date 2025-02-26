using MrDAL.Core.Utils;
using MrDAL.DataEntry.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.FinanceTransaction.JournalVoucher;
using MrDAL.DataEntry.FinanceTransaction.PostDateCheque;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.Interface.FinanceTransaction.JournalVoucher;
using MrDAL.DataEntry.Interface.FinanceTransaction.PostDateCheque;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.DataEntry.Interface.SalesChallan;
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.Interface.SalesReturn;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.DataEntry.PurchaseMaster;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Windows.Forms;

namespace MrDAL.Utility.Config;

public class ClsRecalculate : IRecalculate
{
    #region --------------- Finance ---------------

    public void UpdateFinanceTransaction(string module, string voucherNo)
    {
        try
        {
            switch (module)
            {
                case "Opening":
                case "OB":
                    {
                        _ledgerOpening.LedgerOpeningAccountPosting();
                        break;
                    }
                case "Cash & Bank":
                case "CB":
                    {
                        _cashBank.CashBankAccountPosting();
                        break;
                    }
                case "Journal Voucher":
                case "JV":
                    {
                        _journalVoucher.JournalVoucherAccountPosting();
                        break;
                    }
                case "Debit Notes":
                case "DN":
                    {
                        //_getFinanceEntry.NotesVoucherAccountPosting("DN");
                        break;
                    }
                case "Credit Notes":
                case "CN":
                    {
                        //_getFinanceEntry.NotesVoucherAccountPosting("CN");
                        break;
                    }
                case "Purchase Invoice":
                case "PB":
                    {
                        _purchaseInvoice.PurchaseInvoiceAccountDetailsPosting();
                        _purchaseInvoice.PurchaseInvoiceTermPosting();
                        break;
                    }
                case "Purchase Return":
                case "PR":
                    {
                        _purchaseReturn.PurchaseReturnInvoiceAccountDetailsPosting();
                        _purchaseReturn.PurchaseReturnInvoiceTermPosting();
                        break;
                    }
                case "Sales Invoice":
                case "SB":
                    {
                        CreateDatabaseTable.DropTrigger();
                        _salesInvoice.SalesInvoiceAccountPosting();
                        _salesInvoice.SalesTermPostingAsync();
                        CreateDatabaseTable.CreateTrigger();
                        break;
                    }
                case "Sales Return":
                case "SR":
                    {

                        CreateDatabaseTable.DropTrigger();
                        _salesReturn.SalesReturnAccountPosting();
                        _salesReturn.SalesReturnTermPosting();
                        CreateDatabaseTable.CreateTrigger();
                        break;
                    }

                case "Purchase Tour & Travel":
                case "PTTB":
                    {
                        break;
                    }
                case "Sales Tour & Travel":
                case "STTB":
                    {
                        break;
                    }
                case "Post Dated Cheque":
                case "PDC":
                    {
                        _postDateCheque.PdcAccountPosting();
                        break;
                    }
            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            MessageBox.Show(e.Message, ObjGlobal.Caption);
        }
    }

    #endregion --------------- Finance ---------------

    #region --------------- Stock ---------------

    public void UpdateStockTransaction(string module, string voucherNo)
    {
        try
        {
            switch (module)
            {
                case "Opening":
                case "OB":
                    {
                        _stockEntry.ProductOpeningStockPosting();
                        break;
                    }
                case "Purchase Challan":
                case "PC":
                    {
                        _purchaseChallan.PurchaseChallanStockPosting();
                        break;
                    }
                case "Purchase Invoice":
                case "PB":
                    {
                        _purchaseInvoice.PurchaseInvoiceStockDetailsPosting();
                        break;
                    }
                case "Purchase Return":
                case "PR":
                    {
                        _purchaseReturn.PurchaseReturnInvoiceStockDetailsPosting();
                        break;
                    }
                case "Sales Challan":
                case "SC":
                    {
                        _salesChallan.SalesChallanStockPosting();
                        break;
                    }
                case "Sales Invoice":
                case "SB":
                    {
                        _salesInvoice.SalesInvoiceStockPosting();
                        break;
                    }
                case "Sales Return":
                case "SR":
                    {
                        _salesReturn.SalesReturnStockPosting();

                        break;
                    }
                case "Stock":
                case "SA":
                    {
                        _stockEntry.StockAdjustmentStockPosting();

                        break;
                    }
                case "Production":
                case "IBOM":
                    {
                        _stockEntry.ProductionStockPosting();
                        break;
                    }
                case "REPOST":
                case "RP":
                    {
                        _salesReturn.UpdateSalesStockValue();
                        _stockEntry.UpdateStockAdjustmentStockValue();
                        break;
                    }

            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            MessageBox.Show(e.Message, ObjGlobal.Caption);
        }
    }

    #endregion --------------- Stock ---------------

    #region --------------- ShrinkDataBase ---------------

    public void ShrinkDatabase(string loginDatabase)
    {
        try
        {
            var cmdString = $@"ALTER DATABASE {loginDatabase} SET RECOVERY SIMPLE; ";
            var i = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);

            var cmdString1 = $@"DBCC SHRINKFILE ({loginDatabase}, 1);";
            var ii = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString1);

            var cmdString2 = $@"ALTER DATABASE {loginDatabase} SET RECOVERY FULL;";
            var iii = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString2);
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
        }
    }

    public void ShrinkDatabaseLog(string loginDatabase)
    {
        try
        {
            var cmdString = $@"ALTER DATABASE {loginDatabase} SET RECOVERY SIMPLE; ";
            var i = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);

            var cmdString1 = $@"DBCC SHRINKFILE ({loginDatabase}_log, 1);";
            var ii = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString1);

            var cmdString2 = $@"ALTER DATABASE {loginDatabase} SET RECOVERY FULL;";
            var iii = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString2);
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
        }
    }

    #endregion --------------- ShrinkDataBase ---------------

    // GLOBAL OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    private readonly ILedgerOpeningRepository _ledgerOpening = new LedgerOpeningRepository();
    private readonly IProductOpeningRepository _productOpening = new ProductOpeningRepository();

    private readonly IPostDateChequeRepository _postDateCheque = new PostDateChequeRepository();
    private readonly ICashBankVoucherRepository _cashBank = new CashBankVoucherRepository();
    private readonly IJournalVoucherRepository _journalVoucher = new JournalVoucherRepository();

    private readonly ISalesChallanRepository _salesChallan = new SalesChallanRepository();
    private readonly ISalesInvoiceRepository _salesInvoice = new SalesInvoiceRepository();
    private readonly ISalesReturn _salesReturn = new SalesReturnRepository();

    private readonly IPurchaseChallanRepository _purchaseChallan = new PurchaseChallanRepository();
    private readonly IPurchaseInvoice _purchaseInvoice = new PurchaseInvoiceRepository();
    private readonly IPurchaseReturn _purchaseReturn = new PurchaseReturnInvoiceRepository();
    private readonly IPurchaseChallanReturn _challanReturn = new PurchaseChallanReturnRepository();

    private readonly IStockEntry _stockEntry = new ClsStockEntry();


    #endregion --------------- Global ---------------
}