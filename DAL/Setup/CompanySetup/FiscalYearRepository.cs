using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Setup.CompanySetup;

public class FiscalYearRepository : IFiscalYear
{
    public FiscalYearRepository()
    {
        Fiscal = new FiscalYear();

    }

    public int SaveFiscalYear(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("INSERT INTO AMS.FiscalYear(FY_Id, AD_FY, BS_FY, Current_FY, Start_ADDate, End_ADDate, Start_BSDate, End_BSDate) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($"({Fiscal.FY_Id}, '{Fiscal.AD_FY}', '{Fiscal.BS_FY}', {Fiscal.Current_FY}, {Fiscal.Start_ADDate}, {Fiscal.End_ADDate}, '{Fiscal.Start_BSDate}', '{Fiscal.End_BSDate}') \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.FiscalYear set ");
            cmdString.Append($" AD_FY = {Fiscal.AD_FY}, BS_FY = {Fiscal.BS_FY}, ");
            cmdString.Append(Fiscal.Current_FY is true ? $"Current_FY =CAST('{Fiscal.Current_FY}' AS BIT) ," : "Current_FY=0,");
            cmdString.Append($"Start_ADDate = '{Fiscal.Start_ADDate.GetSystemDate()}', End_ADDate = '{Fiscal.End_ADDate.GetSystemDate()}', Start_BSDate = {Fiscal.Start_BSDate}, End_BSDate = {Fiscal.End_BSDate}");
            cmdString.Append($" WHERE FY_Id = {Fiscal.FY_Id} ;");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        return exe;
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterFiscalYear()
    {
        const string cmdString = @"
			SELECT DISTINCT fy.FY_Id FiscalYearId, fy.AD_FY, fy.BS_FY, fy.Current_FY, fy.Start_ADDate, fy.End_ADDate, fy.Start_BSDate, fy.End_BSDate
			FROM AMS.AccountDetails ad
				 LEFT OUTER JOIN AMS.FiscalYear fy ON ad.FiscalYearId = fy.FY_Id
			ORDER BY fy.Current_FY DESC, fy.AD_FY;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public string GetFiscalYearScript(int yearId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.FiscalYear";
        cmdString += yearId > 0 ? $" WHERE FY_Id= {yearId} " : "";
        return cmdString;
    }
    public async Task<bool> PullFiscalYearServerToClientByRowCount(IDataSyncRepository<FiscalYear> fiscalYearRepo, int callCount)
    {
        try
        {
            //var pullResponse = await fiscalYearRepo.GetUnSynchronizedDataAsync();
            //if (!pullResponse.Success)
            //{
            //    return false;
            //}

            //var query = GetFiscalYearScript();
            //var alldata = ExecuteCommand.ExecuteDataSetSql(query);

            //foreach (var fiscalYearData in pullResponse.List)
            //{
            //    Fiscal = fiscalYearData;

            //    var alreadyExistData = alldata.Select("FY_Id= '" + fiscalYearData.FY_Id + "'");
            //    if (alreadyExistData.Length > 0)
            //    {
            //        var result = SaveFiscalYear("UPDATE");
            //    }
            //    else
            //    {
            //        var result = SaveFiscalYear("SAVE");
            //    }
            //}

            //if (pullResponse.IsReCall)
            //{
            //    callCount++;
            //    await PullFiscalYearServerToClientByRowCount(fiscalYearRepo, callCount);
            //}

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    // OBJECT FOR THIS FORM
    public FiscalYear Fiscal { get; set; }

}