using DatabaseModule.Setup.SystemSetting;
using MrDAL.Global.Common;
using MrDAL.SystemSetting.SystemInterface;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.SystemSetting.PayrollSetting;

public class IncomeTaxSettingRepository : IIncomeTaxSettingRepository
{
    public IncomeTaxSettingRepository()
    {
        TaxSetting = new List<IncomeTaxSetting>();
    }

    public int SaveIncomeTaxSetting()
    {
        var cmdString = @$"
            DELETE HR.IncomeTaxSetting WHERE FiscalYearId ='{ObjGlobal.SysFiscalYearId}';
            INSERT INTO HR.IncomeTaxSetting (SerialNo,FiscalYearId,IncomeTaxTitle,SingleTaxAmount,MarriedTaxAmount,TaxRate)";
        if (TaxSetting.Count > 0)
        {
            cmdString += "\n VALUES( ";
            var index = 0;
            foreach (var setting in TaxSetting)
            {
                index++;
                cmdString += $"{setting.SerialNo},{setting.FiscalYearId},{setting.IncomeTaxTitle},{setting.SingleTaxAmount},{setting.MarriedTaxAmount},{setting.TaxRate}";
                cmdString += index == TaxSetting.Count ? ");" : "),";
            }
        }
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }

    public DataTable ReturnIncomeTaxList()
    {
        var cmdString = $"SELECT * FROM HR.IncomeTaxSetting its WHERE its.FiscalYearId ='{ObjGlobal.SysFiscalYearId}'";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables.Count > 0 ? result.Tables[0] : new DataTable();
    }

    public List<IncomeTaxSetting> TaxSetting { get; set; }
}