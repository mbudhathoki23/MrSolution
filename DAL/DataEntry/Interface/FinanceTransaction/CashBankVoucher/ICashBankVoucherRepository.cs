using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface.FinanceTransaction.CashBankVoucher;

public interface ICashBankVoucherRepository
{

    int SaveCashBankVoucher(string actionTag);
    int SaveRemittanceVoucher(DataGridView rView, DataGridView pView, string actionTag);
    Task<int> SyncCashBankVoucherAsync(string actionTag);
    int CashBankAccountPosting();

    DataSet ReturnCashBankVoucherInDataSet(string voucherNo);
    CB_Master CbMaster { get; set; }
    CB_Details CbDetails { get; set; }
    List<CB_Details> CbDetailsList { get; set; }
    DataTable IsCheckVoucherNoExits(string tableName, string tableVoucherNo, string inputVoucherNo);

    // RETURN VALUE IN DATA TABLE
    DataTable IsCheckChequeNoExits(string actionTag, string voucherNo, string txtChequeNo, long ledgerId);
    DataTable ReturnPdcVoucherInDataTable(string voucherNo);

}