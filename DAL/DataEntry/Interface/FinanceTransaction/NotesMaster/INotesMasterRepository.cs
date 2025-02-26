using DatabaseModule.DataEntry.FinanceTransaction.NotesMaster;
using System.Data;

namespace MrDAL.DataEntry.Interface.FinanceTransaction.NotesMaster;

public interface INotesMasterRepository
{
    // RETURN VALUE IN DATA TABLE
    DataSet ReturnCashBankVoucherInDataSet(string voucherNo);
    DataTable ReturnPdcVoucherInDataTable(string voucherNo);
    DataTable IsCheckVoucherNoExits(string tableName, string tableVoucherNo, string inputVoucherNo);


    // INSERT UPDATE DELETE
    int SaveCashBankVoucher(string actionTag);

    // OBJECT FOR THIS FORM
    Notes_Master NMaster { get; set; }


}