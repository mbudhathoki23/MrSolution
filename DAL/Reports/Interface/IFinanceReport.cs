using MrDAL.Reports.ViewModule;
using System.Data;

namespace MrDAL.Reports.Interface;

public interface IFinanceReport
{
    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    VmFinanceReports GetReports { get; set; }

    #endregion --------------- OBJECT ---------------

    // TRIAL BALANCE

    #region --------------- TRIAL BALANCE ---------------

    DataTable GetOpeningTrialBalanceReport();

    DataTable GetNormalTrialBalanceReport();

    DataTable GetPeriodicTrialBalanceReport();

    #endregion --------------- TRIAL BALANCE ---------------

    // PROFIT & LOSS

    #region --------------- PROFIT & LOSS ---------------

    DataTable GetNormalProfitLossReport();

    DataTable GetPeriodicProfitLossReport();

    #endregion --------------- PROFIT & LOSS ---------------

    // BALANCE SHEET

    #region --------------- BALANCE SHEET ---------------

    DataTable GetNormalBalanceSheetReport();

    DataTable GetPeriodicBalanceSheetReport();

    #endregion --------------- BALANCE SHEET ---------------

    // PROVISION VOUCHER LIST

    #region --------------- PROVISION VOUCHER ---------------

    DataTable GetProvisionCashBankVoucher();

    DataTable GetProvisionJournalVoucher();

    #endregion --------------- PROVISION VOUCHER ---------------

    // GENERAL LEDGER REPORTS

    #region --------------- GENERA LEDGER REPORT ---------------

    DataTable GetGeneralLedgerDetailsReport();

    DataTable GetGeneralLedgerSummaryReport();

    #endregion --------------- GENERA LEDGER REPORT ---------------

    // CASH & BANK REPORTS

    #region --------------- DAY BOOK & CASH & BANK REPORTS ---------------

    DataTable GetDayBookReport();

    DataTable GetJournalVoucherReport();

    DataTable GetCashBankDetailsReports();

    DataTable GetCashBankSummaryReports();

    #endregion --------------- DAY BOOK & CASH & BANK REPORTS ---------------

    // MEMBER SHIP REPORT

    #region --------------- MEMBERSHIP REPORTS ---------------

    DataTable GetMemberShipListSummary();

    DataTable GetMemberShipListDetails();

    #endregion --------------- MEMBERSHIP REPORTS ---------------

    // LIST OF MASTER

    #region --------------- LIST OF MASTER ---------------

    DataTable GetGeneralLedgerListMaster();
    DataTable GetAccountGroupListMaster();
    DataTable GetAccountSubGroupListMaster();
    DataTable GetAreaListMaster();
    DataTable GetAgentListMaster();
    DataTable GetSubLedgerListMaster();
    DataTable GetLedgerSubLedgerListMaster();
    DataTable GetDepartmentListMaster();
    DataTable GetDepartmentLedgerListMaster();

    #endregion --------------- LIST OF MASTER ---------------
}