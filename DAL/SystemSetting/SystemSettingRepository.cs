using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Server;
using System;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting;

public class SystemSettingRepository : ISystemSettingRepository
{
    public SystemSettingRepository()
    {
        VmSystem = new DatabaseModule.Setup.SystemSetting.SystemSetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SaveSystemSetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.SystemSetting;
			INSERT INTO AMS.SystemSetting (SyId, EnglishDate, AuditTrial, Udf, Autopoplist, CurrentDate, ConformSave, ConformCancel, ConformExits, CurrencyRate, CurrencyId, DefaultPrinter, AmountFormat, RateFormat, QtyFormat, CurrencyFormatF, DefaultFiscalYearId, DefaultOrderPrinter, DefaultInvoicePrinter, DefaultOrderNumbering, DefaultInvoiceNumbering, DefaultAvtInvoiceNumbering, DefaultOrderDesign, IsOrderPrint, DefaultInvoiceDesign, IsInvoicePrint, IsPrintBranch, DefaultAvtDesign, DefaultFontsName, DefaultFontsSize, DefaultPaperSize, DefaultReportStyle, DefaultPrintDateTime, DefaultFormColor, DefaultTextColor, DebtorsGroupId, CreditorGroupId, SalaryLedgerId, TDSLedgerId, PFLedgerId, DefaultEmail, DefaultEmailPassword, BackupDays, BackupLocation,IsNightAudit, EndTime, SearchAlpha, BarcodeAutoSearch)
			VALUES({VmSystem.SyId},CAST('{VmSystem.EnglishDate}' AS BIT), CAST('{VmSystem.AuditTrial}' AS BIT), CAST('{VmSystem.Udf}' AS BIT),CAST('{VmSystem.Autopoplist}' AS BIT),CAST('{VmSystem.CurrentDate}' AS BIT),CAST('{VmSystem.ConformSave}' AS BIT), CAST('{VmSystem.ConformCancel}' AS BIT),CAST('{VmSystem.ConformExits}' AS BIT), {VmSystem.CurrencyRate},{VmSystem.CurrencyId}, N'{VmSystem.DefaultPrinter}', {VmSystem.AmountFormat}, {VmSystem.RateFormat}, {VmSystem.QtyFormat}, {VmSystem.CurrencyFormatF}, {VmSystem.DefaultFiscalYearId}, N'{VmSystem.DefaultOrderPrinter}', N'{VmSystem.DefaultInvoicePrinter}', N'{VmSystem.DefaultOrderNumbering}', N'{VmSystem.DefaultInvoiceNumbering}', N'{VmSystem.DefaultAvtInvoiceNumbering}', N'{VmSystem.DefaultOrderDesign}', CAST('{VmSystem.IsOrderPrint}' AS BIT), N'{VmSystem.DefaultInvoiceDesign}', CAST('{VmSystem.IsInvoicePrint}' AS BIT), CAST('{VmSystem.IsInvoicePrint}' AS BIT), N'{VmSystem.DefaultAvtDesign}', N'{VmSystem.DefaultFontsName}', {VmSystem.DefaultFontsSize}, N'{VmSystem.DefaultPaperSize}', N'{VmSystem.DefaultReportStyle}', CAST('{VmSystem.DefaultPrintDateTime}' AS BIT), N'{VmSystem.DefaultFormColor}', N'{VmSystem.DefaultTextColor}',";
        cmdString += VmSystem.DebtorsGroupId > 0 ? $"{VmSystem.DebtorsGroupId}, " : "NULL,";
        cmdString += VmSystem.CreditorGroupId > 0 ? $"{VmSystem.CreditorGroupId}," : "NULL,";
        cmdString += VmSystem.SalaryLedgerId > 0 ? $"{VmSystem.SalaryLedgerId}, " : "NULL,";
        cmdString += VmSystem.TDSLedgerId > 0 ? $"{VmSystem.TDSLedgerId}, " : "NULL,";
        cmdString += VmSystem.PFLedgerId > 0 ? $" {VmSystem.PFLedgerId}," : "NULL,";
        cmdString +=
            $@"'{VmSystem.DefaultEmail}', N'{VmSystem.DefaultEmailPassword}', {VmSystem.BackupDays}, N'{VmSystem.BackupLocation}', ";
        cmdString +=
            $" CAST('{VmSystem.IsNightAudit}' AS BIT),CAST('{VmSystem.EndTime}' AS TIME),CAST('{VmSystem.SearchAlpha}' AS BIT),CAST('{VmSystem.BarcodeAutoSearch}' AS BIT) ); ";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);
        if (result == 0) return result;
        var cmd = $@"
			UPDATE AMS.FiscalYear SET Current_FY = 0;
			UPDATE AMS.FiscalYear SET Current_FY =1 WHERE FY_Id ='{VmSystem.DefaultFiscalYearId}'; ";
        return SqlExtensions.ExecuteNonTrans(cmd);
    }

    public string GetSystemSettingScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.SystemSetting s";
        cmdString += settingId > 0 ? $" WHERE s.SyId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullSystemSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.SystemSetting> systemSettingRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}SystemSetting/GetSystemSettingByCallCount?callCount={callCount}";
        var pullResponse = await systemSettingRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetSystemSettingScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmSystem = settingData;

                var alreadyExistData = alldata.Select("SyId='" + settingData.SyId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SaveSystemSetting("UPDATE");
                }
                else
                {
                    var result = SaveSystemSetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullSystemSettingServerToClientByRowCounts(systemSettingRepo, callCount);
        }

        return true;
    }

    // OBJECT FOR THIS FORM
    public DatabaseModule.Setup.SystemSetting.SystemSetting VmSystem { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}