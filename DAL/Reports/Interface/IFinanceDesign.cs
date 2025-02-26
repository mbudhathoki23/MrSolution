using System.Windows.Forms;

namespace MrDAL.Reports.Interface;

public interface IFinanceDesign
{
    #region ---------------- CASH & BANK / JOURNAL VOUCHER ----------------

    DataGridView GetCashBankJournalVoucherDesign(DataGridView rGrid);

    DataGridView GetCashBankJournalVoucherTFormatDesign(DataGridView rGrid);

    DataGridView GetCashBankSummaryJournalVoucherDesign(DataGridView rGrid);

    DataGridView GetCashBankSummaryJournalVoucherTFormatDesign(DataGridView rGrid);

    #endregion ---------------- CASH & BANK / JOURNAL VOUCHER ----------------

    #region ---------------- TRIAL BALANCE / BALANCE SHEET ----------------

    DataGridView GetTrialBalanceNormalDesign(DataGridView rGrid);

    DataGridView GetTrialBalanceNormalTFormatDesign(DataGridView rGrid);

    DataGridView GetTrialBalancePeriodicDesign(DataGridView rGrid);

    DataGridView GetTrialBalancePeriodicTFormatDesign(DataGridView rGrid);

    #endregion ---------------- TRIAL BALANCE / BALANCE SHEET ----------------

    #region ---------------- PROFIT & LOSS ----------------

    DataGridView GetProfitLossBalanceSheetPeriodicDesign(DataGridView rGrid);

    DataGridView GetProfitLossBalanceSheetPeriodicTFormatDesign(DataGridView rGrid);

    DataGridView GetProfitLossBalanceSheetNormalDesign(DataGridView rGrid);

    DataGridView GetProfitLossBalanceSheetNormalTFormatDesign(DataGridView rGrid);

    #endregion ---------------- PROFIT & LOSS ----------------

    #region ---------------- LEDGER REPORT DESIGN ----------------

    DataGridView GetGeneralLedgerNormalDesign(DataGridView rGrid);

    DataGridView GetGeneralLedgerNormalTFormatDesign(DataGridView rGrid);

    DataGridView GetGeneralLedgerSummaryDesign(DataGridView rGrid);

    DataGridView GetGeneralLedgerSummaryTFormatDesign(DataGridView rGrid);

    DataGridView GetPartyConfirmationDesign(DataGridView rGrid, bool includeReturn);

    #endregion ---------------- LEDGER REPORT DESIGN ----------------

    #region ---------------- DAY BOOK ----------------

    DataGridView GetDayBookNormalReportDesign(DataGridView rGrid);

    DataGridView GetDayBookTFormatReportDesign(DataGridView rGrid);

    #endregion ---------------- DAY BOOK ----------------
}