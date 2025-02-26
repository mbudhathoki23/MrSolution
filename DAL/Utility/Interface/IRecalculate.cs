namespace MrDAL.Utility.Interface;

public interface IRecalculate
{
    void UpdateFinanceTransaction(string module, string voucherNo = "");

    void UpdateStockTransaction(string module, string voucherNo = "");

    void ShrinkDatabase(string loginDatabase);

    void ShrinkDatabaseLog(string loginDatabase);
}