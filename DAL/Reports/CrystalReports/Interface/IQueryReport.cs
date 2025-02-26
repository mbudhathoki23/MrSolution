namespace MrDAL.Reports.CrystalReports.Interface;
public interface IQueryReport
{
    string GetCustomerLedgerBalanceConfirmation(string partyLedgerId, int fiscalYearId, decimal aboveAmount, bool includeVat = false);
    string GetVendorLedgerBalanceConfirmation(string partyLedgerId, int fiscalYearId, decimal aboveAmount, bool includeVat = false);
    string GetFiscalYear(int fiscalYearId);
    string GetPartyLedgerTransaction(long ledgerId, int fiscalYearId);
}