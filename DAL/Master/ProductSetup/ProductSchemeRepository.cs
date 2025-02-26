using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.Master.ProductSetup;

public class ProductSchemeRepository : IProductSchemeRepository
{
    public ProductSchemeRepository()
    {
        SchemeMaster = new Scheme_Master();
    }

    // INSERT UPDATE DELETE
    public int SaveProductScheme(string actionTag)
    {
        var cmdString = new StringBuilder();
        SchemeMaster.SchemeTime = DateTime.Now;
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO AMS.Scheme_Master (SchemeId, SchemeDate, SchemeMiti, SchemeTime, SchemeDesc, ValidFrom, ValidFromMiti, ValidTo, ValidToMiti, EnterBy, EnterDate, IsActive, BranchId, CompanyUnitId, Remarks, FiscalYearId) \n");
            cmdString.Append(" VALUES ( ");
            cmdString.Append(
                $" {SchemeMaster.SchemeId},'{SchemeMaster.SchemeDate:yyyy-MM-dd}','{SchemeMaster.SchemeMiti}','{SchemeMaster.SchemeTime}',N'{SchemeMaster.SchemeDesc}','{SchemeMaster.ValidFrom:yyyy-MM-dd}','{SchemeMaster.ValidFromMiti}','{SchemeMaster.ValidTo:yyyy-MM-dd}','{SchemeMaster.ValidToMiti}', ");
            cmdString.Append($" '{ObjGlobal.LogInUser}',GETDATE(),1,{ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($" NULL,{ObjGlobal.SysFiscalYearId}); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Scheme_Master SET  \n");
            cmdString.Append($"SchemeDesc = N'{SchemeMaster.SchemeDesc}', ");
            cmdString.Append(
                $"ValidFromMiti = '{SchemeMaster.ValidFromMiti}',ValidToMiti ='{SchemeMaster.ValidToMiti}',ValidFrom = '{SchemeMaster.ValidFrom:yyyy-MM-dd}',ValidTo = '{SchemeMaster.ValidTo:yyyy-MM-dd}'  ");
            cmdString.Append($" WHERE SchemeId ='{SchemeMaster.SchemeId}';  \n");
        }

        if (actionTag.ToUpper() == "DELETE" || actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append($@"  DELETE AMS.Scheme_Details WHERE SchemeId ='{SchemeMaster.SchemeId}'");
            if (actionTag.ToUpper() == "DELETE")
            {
                SaveProductSchemeAuditLog(actionTag);
                cmdString.Append($@" DELETE AMS.Scheme_Master WHERE SchemeId = '{SchemeMaster.SchemeId}'");
            }
        }

        if (actionTag.ToUpper() != "DELETE")
        {
            int[] value = { 501, 1001, 1501, 2001, 2501, 3001, 3501, 4001 };
            int[] value2 = { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
            if (SchemeMaster.GetView is { RowCount: > 0 })
            {
                var rowId = SchemeMaster.GetView.RowCount - 1;
                if (!SchemeMaster.GetView.Rows[rowId].Cells["GTxtProductId"].Value.IsValueExits())
                {
                    SchemeMaster.GetView.Rows.RemoveAt(rowId);
                }

                cmdString.Append(
                    " INSERT INTO AMS.Scheme_Details (SchemeId, SNo, ProductId, GroupId, SubGroupId, Qty, DiscountPercent, DiscountValue, MinValue, MaxValue, SalesRate, MrpRate, OfferRate) \n");
                cmdString.Append(" VALUES \n");
                foreach (DataGridViewRow row in SchemeMaster.GetView.Rows)
                {
                    if (value.Contains(SchemeMaster.GetView.RowCount))
                    {
                        cmdString.Append(" VALUES \n");
                    }

                    cmdString.Append(
                        $" ( {SchemeMaster.SchemeId},{row.Cells["GTxtSno"].Value.GetInt()},{row.Cells["GTxtProductId"].Value.GetLong()},");
                    cmdString.Append(row.Cells["GTxtGroupId"].Value.GetInt() > 0
                        ? $"{row.Cells["GTxtGroupId"].Value.GetInt()},"
                        : "NULL,");
                    cmdString.Append(row.Cells["GTxtSubGroupId"].Value.GetInt() > 0
                        ? $"{row.Cells["GTxtSubGroupId"].Value.GetInt()},"
                        : "NULL,");
                    cmdString.Append(
                        $"1,0,0,0,0,{row.Cells["GTxtSalesRate"].Value},{row.Cells["GTxtMrp"].Value},0)");
                    if (value2.Contains(SchemeMaster.GetView.RowCount))
                    {
                        cmdString.Append("; \n");
                    }
                    else
                    {
                        cmdString.Append(row.Index < SchemeMaster.GetView.RowCount - 1 ? ",\n" : "; \n");
                    }
                }
            }
        }

        var exe = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncProductSchemeAsync(actionTag));
        }

        return exe;
    }
    public async Task<int> SyncProductSchemeAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
                //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //configParams.ShowErrorDialog();
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUri = @$"{configParams.Model.Item2}ProductScheme/DeleteProductSchemeAsync?id=" +
                            SchemeMaster.SchemeId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}ProductScheme/GetProductSchemeById",
                InsertUrl = @$"{configParams.Model.Item2}ProductScheme/InsertProductScheme",
                UpdateUrl = @$"{configParams.Model.Item2}ProductScheme/UpdateProductScheme",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var productSchemeRepo =
                DataSyncProviderFactory.GetRepository<Scheme_Master>(DataSyncManager.GetGlobalInjectData());

            var psg = new Scheme_Master
            {
                SchemeId = SchemeMaster.SchemeId,
                SchemeDate = Convert.ToDateTime(SchemeMaster.SchemeDate.ToString("yyy-MM-dd")),
                SchemeMiti = SchemeMaster.SchemeMiti,
                SchemeTime = SchemeMaster.SchemeTime,
                SchemeDesc = SchemeMaster.SchemeDesc,
                ValidFrom = Convert.ToDateTime(SchemeMaster.ValidFrom.Value.ToString("yyyy-MM-dd")),
                ValidFromMiti = SchemeMaster.ValidFromMiti,
                ValidTo = Convert.ToDateTime(SchemeMaster.ValidTo.Value.ToString("yyyy-MM-dd")),
                ValidToMiti = SchemeMaster.ValidToMiti,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                IsActive = true,
                BranchId = ObjGlobal.SysBranchId,
                CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                Remarks = SchemeMaster.Remarks,
                FiscalYearId = ObjGlobal.SysFiscalYearId
            };

            var result = actionTag switch
            {
                "SAVE" => await productSchemeRepo?.PushNewAsync(psg),
                "UPDATE" => await productSchemeRepo?.PutNewAsync(psg),
                "DELETE" => await productSchemeRepo?.DeleteNewAsync(),
                _ => await productSchemeRepo?.PushNewAsync(psg)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    public int SaveProductSchemeAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_SCHEME_MASTER(Scheme_Name, FromDate, FromTo, RatedAs, Basis, MannualOveride, ConsiderReturn, FromMiti, ToMiti, SchemeDate, SchemeMiti, ProductTerm, ProdUnit, BillTerm, Term_Basis, ApplyFor, BranchId, CompanyUnitId, Remarks, FiscalYearId, ModifyAction, ModifyBy, ModifyDate)
            SELECT Scheme_Name, FromDate, FromTo, RatedAs, Basis, MannualOveride, ConsiderReturn, FromMiti, ToMiti, SchemeDate, SchemeMiti, ProductTerm, ProdUnit, BillTerm, Term_Basis, ApplyFor, BranchId, CompanyUnitId, Remarks, FiscalYearId,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.Scheme_Master
            WHERE SchemeId='';

            INSERT INTO AUD.AUDIT_SCHEME_DETAILS(Scheme_Id, SNo, P_Code, P_Group, P_SubGroup, Qty, PercentageValue, DiscountValue, MinValue, MaxValue, FreeQty, ProductRate, LessPercentage, DisPercentage, ModifyAction, ModifyBy, ModifyDate)
            SELECT Scheme_Id, SNo, P_Code, P_Group, P_SubGroup, Qty, PercentageValue, DiscountValue, MinValue, MaxValue, FreeQty, ProductRate, LessPercentage, DisPercentage,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.Scheme_Details
            WHERE SchemeId=''
            ";
        var exe = SqlExtensions.ExecuteNonTrans(cmdString);
        return exe;

    }

    // RETURN DATA IN DATA TABLE
    public DataTable GetProductListWithCheckbox(string productId)
    {
        var dtProduct = new DataTable();
        var cmdString = @$"
			SELECT p.PID ProductId, p.PName Product, ISNULL(bl.Barcode,p.PShortName) Barcode, pu.UnitCode UOM, p.PMRP, ISNULL(bl.SalesRate,p.PSalesRate) SalesRate, ISNULL ( pg.GrpName, 'NO GROUP' ) PGroup, ISNULL ( psg.SubGrpName, 'NO SUBGROUP' ) SubGrpName
			 FROM AMS.Product p
				  LEFT OUTER HASH JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
				  LEFT OUTER HASH JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
				  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
				  LEFT OUTER JOIN AMS.BarcodeList bl ON bl.ProductId = p.PID AND bl.Barcode = p.Barcode AND bl.SalesRate <> p.PSalesRate
			 WHERE p.Status = 1  and p.PID in ({productId})";
        cmdString += " ORDER BY p.PName, bl.Barcode ;";
        dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtProduct;
    }

    // OBJECT FOR THIS FORM
    public Scheme_Master SchemeMaster { get; set; }
}