using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.DayClosing;
using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.NotesMaster;
using DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque;
using DatabaseModule.DataEntry.OpeningMaster;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface;

public interface IFinanceEntry
{
    // INSERT , UPDATE , DELETE FUNCTION

    #region --------------- Finance Transction ---------------

    int SaveCashClosing(string actionTag);

    int SaveCashBankVoucher(string actionTag);

    int CashBankAccountPosting();

    int SaveRemittanceVoucher(DataGridView rView, DataGridView pView, string actionTag);

    Task<int> SyncCashBankVoucherAsync(string actionTag);

    Task<int> SyncLedgerOpeningAsync(string actionTag);

    int SaveUnSyncJournalVoucherFromServerAsync(JV_Master journalVoucherModel);

    Task<int> UpdateSyncJournalVoucherColumnInServerAsync(string actionTag);

    int SaveUnSyncCashBankVoucherFromServerAsync(CB_Master cashBankVoucherModel);

    Task<int> UpdateSyncCashBankVoucherColumnInServerAsync(string actionTag);

    int SaveNotesVoucher(string actionTag);

    int UpdateImageFinance(string tableName, string filterColumn);

    short ReturnSyncRowVersionVoucher(string module, string voucherNo);


    // ACCOUNT POSTING DETAILS FUNCTION

    int LedgerOpeningAccountPosting();

    int PdcAccountPosting();

    int UpdateDocumentNumbering(string module, string docDesc);

    int JournalVoucherAccountPosting();

    int NotesVoucherAccountPosting(string mode);

    #endregion --------------- Finance Transction ---------------


    // RETURN VALUE IN DATA TABLE

    #region --------------- DataTable Value Return  ---------------

    DataTable GetCashClosing(int selectedId = 0);

    DataTable GetOpeningLedgerWithBalance(string startDate);

    DataTable GetOpeningVoucher(string module, string voucherNo = "N");

    #endregion --------------- DataTable Value Return  ---------------


    // RETURN VALUE IN DATA SET

    #region --------------- Return Voucher Number  ---------------

    DataSet ReturnCashBankVoucherInDataSet(string voucherNo);

    DataTable IsCheckChequeNoExits(string actionTag, string voucherNo, string txtChequeNo, long ledgerId);

    #endregion --------------- Return Voucher Number  ---------------


    // OBJECT FOR THIS CLASS

    #region --------------- OBJECT ---------------
    JV_Master JvMaster { get; set; }
    CB_Master CbMaster { get; set; }
    CB_Details CbDetails { get; set; }
    Notes_Master NMaster { get; set; }
    LedgerOpening GetOpening { get; set; }
    PostDateCheque PdcMaster { get; set; }
    CashClosing CashMaster { get; set; }
    List<CB_Details> CbDetailsList { get; set; }

    #endregion --------------- OBJECT ---------------











}










