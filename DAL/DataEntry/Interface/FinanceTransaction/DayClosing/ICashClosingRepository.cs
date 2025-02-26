using DatabaseModule.DataEntry.FinanceTransaction.DayClosing;
using System.Data;

namespace MrDAL.DataEntry.Interface.FinanceTransaction.DayClosing;

public interface ICashClosingRepository
{
    //RETURN VALUE OF PRODUCT IN DATA TABLE

    DataTable IsExitsCheckDocumentNumbering(string module);


    // RETURN VALUE OF MASTER LIST IN DATA TABLE

    long GetUserLedgerIdFromUser(string userInfo);

    // OBJECT FOR THIS FORM
    CashClosing ObjCashClosing { get; set; }
}