using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Utility.Server;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrDAL.DataEntry.CommonSetup;

public class TermCalculationRepository : ITermCalculationRepository
{
    // RETURN VALUE IN DATA TABLE

    public DataGridView GetBillingTermDesign(DataGridView getView)
    {
        if (getView.ColumnCount > 0)
        {
            getView.DataSource = null;
            getView.Columns.Clear();
        }

        getView.AddColumn("GTxtSno", "SNO", "SNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        getView.AddColumn("GTxtTermId", "TermId", "TermId", 0, 2, false);
        getView.AddColumn("GTxtOrderNo", "OrderNo", "OrderNo", 0, 2, false);
        getView.AddColumn("GTxtDescription", "DESCRIPTION", "TermDesc", 200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        getView.AddColumn("GTxtBasic", "BASIC", "TermBasic", 90, 50, true, DataGridViewContentAlignment.MiddleCenter);
        getView.AddColumn("GTxtSign", "SIGN", "TermSign", 75, 45, true, DataGridViewContentAlignment.MiddleCenter);
        getView.AddColumn("GTxtRate", "RATE", "TermRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        getView.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        getView.AddColumn("GTxtValueAmount", "ValueAmount", "GTxtValueAmount", 0, 2, false);
        getView.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        getView.AddColumn("GTxtProductSno", "ProductSno", "GTxtProductSno", 0, 2, false);
        getView.AddColumn("GTxtTermType", "TermType", "TermType", 0, 2, false);
        getView.AddColumn("GTxtTermCondition", "TermCondition", "TermCondition", 0, 2, false);
        return getView;
    }
    public DataTable GetTermCalculationForVoucher(string module, string termType = "B")
    {
        string[] salesStrings = { "SQ", "SO", "SC", "SB", "SR" };
        string cmdString;
        if (salesStrings.Contains(module))
        {
            cmdString = $@" 
                Select ST_Id TermId, Order_No OrderNo,ST_Name TermDesc,ST_Type TermType, ST_Condition TermCondition,CASE WHEN ST_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, ST_Sign TermSign, ST_Rate TermRate from [AMS].ST_Term WHERE ST_Condition='{termType}' AND Module='SB' AND ST_Type <> 'A' ORDER BY Order_No";
        }
        else if (module.Equals("SAB"))
        {
            cmdString = $@"
                Select ST_Id TermId, Order_No OrderNo,ST_Name TermDesc,ST_Type TermType, ST_Condition TermCondition,CASE WHEN ST_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, ST_Sign TermSign, ST_Rate TermRate from [AMS].ST_Term WHERE ST_Condition='{termType}' AND Module='SB' AND ST_Type = 'A' ORDER BY Order_No";
        }
        else if (module.Equals("PAB"))
        {
            cmdString = $@" 
                Select PT_Id TermId, Order_No OrderNo,PT_Name TermDesc,PT_Type TermType, PT_Condition TermCondition,Ledger,PT_Basis TermBasic, PT_Sign TermSign, PT_Rate TermRate from [AMS].PT_Term WHERE PT_Condition='{termType}' AND Module='PB' AND PT_Type = 'A' ORDER BY Order_No ";
        }
        else
        {
            cmdString = $@" 
                Select PT_Id TermId, Order_No OrderNo,PT_Name TermDesc,PT_Type TermType, PT_Condition TermCondition,CASE WHEN PT_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, PT_Sign TermSign, PT_Rate TermRate from [AMS].PT_Term WHERE PT_Condition='{termType}' AND Module='PB' AND PT_Type <> 'A' ORDER BY Order_No ";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
}