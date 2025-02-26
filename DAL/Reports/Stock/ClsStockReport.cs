using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Reports.Interface;
using MrDAL.Reports.ViewModule;
using MrDAL.Reports.ViewModule.Object.Inventory;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace MrDAL.Reports.Stock;

public class ClsStockReport : IStockReport
{
    public DataTable GetBillOfMaterialsReports()
    {
        var cmdString = GetBillOfMaterialsScript();
        var dtStock = SqlExtensions.ExecuteDataSet(cmdString);
        if (dtStock.Tables[0].Rows.Count == 0) return dtStock.Tables[0];
        DataTable dtReport;
        if (GetReports.IsSummary)
            dtReport = GetReports.RptMode switch
            {
                "GROUP WISE" => ReturnBillOfMaterialsSummaryReportProductGroupWise(dtStock.Tables[0]),
                "SUBGROUP WISE" => ReturnBillOfMaterialsSummaryReportProductSubGroupWise(dtStock.Tables[0]),
                _ => ReturnBillOfMaterialsSummaryReportVoucherWise(dtStock.Tables[0])
            };
        else
            dtReport = GetReports.RptMode switch
            {
                "GROUP WISE" => ReturnBillOfMaterialsDetailsReportVoucherGroupWise(dtStock),
                "SUBGROUP WISE" => ReturnBillOfMaterialsDetailsReportVoucherSubGroupWise(dtStock),
                _ => ReturnBillOfMaterialsDetailsReportVoucherWise(dtStock)
            };
        return dtReport;
    }

    public DataTable GetProfitabilityReports()
    {
        if (GetReports.RePostValue)
        {
            const string cmdRePost = "AMS.USP_PostStockValue";
            var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        }

        var listResult = GetProfitabilityReportScript();
        var dtStock = SqlExtensions.ExecuteDataSet(listResult).Tables[0];
        if (dtStock.Rows.Count is 0) return dtStock;

        var dtReport = dtStock.Clone();
        if (GetReports.IsSummary)
            dtReport = GetReports.RptMode switch
            {
                "GROUP WISE" => ReturnProfitabilityReportProductGroupWise(dtStock),
                "SUBGROUP WISE" => ReturnProfitabilityReportProductSubGroupWise(dtStock),
                _ => ReturnProfitabilityReportProductWise(dtStock)
            };

        return dtReport;
    }
    

    // CONSUMPTION REPORTS
    private DataTable GetBillOfMaterialsReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_Date",
            "dt_Miti",
            "dt_VoucherNo",
            "dt_ProductId",
            "dt_ShortName",
            "dt_ProductName",
            "dt_Qty",
            "dt_Unit",
            "dt_Rate",
            "dt_Balance",
            "dt_SalesRate",
            "dt_CostRatio"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsSummaryReportProductSubGroupWise(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        var dtGroupType = dtStock.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("SubGrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("SubGrpName")).CopyToDataTable();

        foreach (DataRow roSubGroup in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_ShortName"] = roSubGroup["SubGroupCode"].GetString();
            dataRow["dt_ProductName"] = roSubGroup["SubGrpName"].GetString();
            dataRow["IsGroup"] = 2;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var detailsGroup = dtStock.Select($"SubGrpName= '{roSubGroup["SubGrpName"]}'").CopyToDataTable();

            var dtSubGroupType = detailsGroup.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();

            foreach (DataRow roGroup in dtSubGroupType.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dt_ShortName"] = roSubGroup["SubGroupCode"].GetString();
                dataRow["dt_ProductName"] = roSubGroup["SubGrpName"].GetString();
                dataRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                var detailsSubGroup = dtStock
                    .Select($"GrpName= '{roGroup["GrpName"]}' and SubGrpName='{roSubGroup["SubGrpName"]}'")
                    .CopyToDataTable();
                foreach (DataRow roType in detailsSubGroup.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dt_Date"] = roType["VMiti"];
                    dataRow["dt_Miti"] = roType["VDate"].GetDateString();
                    dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
                    dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
                    dataRow["dt_ShortName"] = roType["PShortName"].GetString();
                    dataRow["dt_ProductName"] = roType["PName"].GetString();
                    dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimal();
                    dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
                    dataRow["dt_Balance"] = roType["Amount"].GetDecimal();
                    dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimal();

                    var salesRate = roType["PSalesRate"].GetDecimal();
                    var costRate = roType["Amount"].GetDecimal();
                    var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;

                    dataRow["dt_CostRatio"] = ratio > 0 ? ratio + "%" : string.Empty;
                    dataRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                }

                dataRow = dtReport.NewRow();
                dataRow["dt_ProductName"] = $"[{roGroup["GrpName"]}] GROUP TOTAL :- ";

                var qtySub = detailsSubGroup.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
                var balanceSub = detailsSubGroup.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

                dataRow["dt_Qty"] = qtySub;
                dataRow["dt_Balance"] = balanceSub;
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }

            dataRow = dtReport.NewRow();
            dataRow["dt_ProductName"] = $"[{roSubGroup["SubGroup"]}] SUB GROUP TOTAL :- ";

            var qty = detailsGroup.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
            var balance = detailsGroup.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

            dataRow["dt_Qty"] = qty;
            dataRow["dt_Balance"] = balance;
            dataRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";

        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsSummaryReportProductGroupWise(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        var dtGroupType = dtStock.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_ShortName"] = roGroup["GrpCode"].GetString();
            dataRow["dt_ProductName"] = roGroup["GrpName"].GetString();
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var detailsGroup = dtStock.Select($"GrpName= '{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow roType in detailsGroup.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dt_Date"] = roType["VMiti"];
                dataRow["dt_Miti"] = roType["VDate"].GetDateString();
                dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
                dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
                dataRow["dt_ShortName"] = roType["PShortName"].GetString();
                dataRow["dt_ProductName"] = roType["PName"].GetString();
                dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimal();
                dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
                dataRow["dt_Balance"] = roType["Amount"].GetDecimal();
                dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimal();

                var salesRate = roType["PSalesRate"].GetDecimal();
                var costRate = roType["Amount"].GetDecimal();
                var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;
                dataRow["dt_CostRatio"] = ratio > 0 ? ratio + "%" : string.Empty;
                dataRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }

            dataRow = dtReport.NewRow();
            dataRow["dt_ProductName"] = $"[{roGroup["GrpName"]}] GROUP TOTAL :- ";

            var qty = detailsGroup.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
            var balance = detailsGroup.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

            dataRow["dt_Qty"] = qty;
            dataRow["dt_Balance"] = balance;
            dataRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";

        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsSummaryReportVoucherWise(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        foreach (DataRow roType in dtStock.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_Date"] = roType["VMiti"];
            dataRow["dt_Miti"] = roType["VDate"].GetDateString();
            dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
            dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
            dataRow["dt_ShortName"] = roType["PShortName"].GetString();
            dataRow["dt_ProductName"] = roType["PName"].GetString();
            dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimalQtyString();
            dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
            dataRow["dt_Balance"] = roType["Amount"].GetDecimalString();
            dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimalString();

            var salesRate = roType["PSalesRate"].GetDecimal();
            var costRate = roType["Amount"].GetDecimal();
            var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;
            dataRow["dt_CostRatio"] = ratio > 0 ? ratio + "%" : string.Empty;
            dataRow["IsGroup"] = 0;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";

        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsDetailsReportVoucherSubGroupWise(DataSet dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        var distinctProduct = dtStock.Tables[0];

        var dtSubGroupType = distinctProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("SubGrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("SubGrpName")).CopyToDataTable();
        foreach (DataRow roSubGroup in dtSubGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_ShortName"] = roSubGroup["SubGrpCode"].GetString();
            dataRow["dt_ProductName"] = roSubGroup["SubGrpName"].GetString();
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var detailsSubGroup = distinctProduct.Select($"SubGrpName='{roSubGroup["SubGrpName"]}'").CopyToDataTable();

            var dtGroupType = detailsSubGroup.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
            foreach (DataRow roGroup in dtGroupType.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dt_ShortName"] = roGroup["GrpCode"].GetLong();
                dataRow["dt_ProductName"] = roGroup["GrpName"].GetString();
                dataRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                var groupDetails = distinctProduct
                    .Select($"GrpName= '{roGroup["GrpName"]}' and SubGrpName='{roSubGroup["SubGrpName"]}' ")
                    .CopyToDataTable();
                foreach (DataRow roType in distinctProduct.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dt_Date"] = roType["VMiti"];
                    dataRow["dt_Miti"] = roType["VDate"].GetDateString();
                    dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
                    dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
                    dataRow["dt_ShortName"] = roType["PShortName"].GetString();
                    dataRow["dt_ProductName"] = roType["PName"].GetString();
                    dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimal();
                    dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
                    dataRow["dt_Balance"] = roType["Amount"].GetDecimal();
                    dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimal();

                    var salesRate = roType["PSalesRate"].GetDecimal();
                    var costRate = roType["Amount"].GetDecimal();
                    var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;
                    dataRow["dt_CostRatio"] = ratio > 0 ? ratio.GetDecimalString() + "%" : string.Empty;
                    dataRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                    var details = dtStock.Tables[1].Select($"VoucherNo = '{roType["VoucherNo"]}'")
                        .CopyToDataTable();
                    foreach (DataRow roDetails in details.Rows)
                    {
                        dataRow = dtReport.NewRow();
                        dataRow["dt_ProductId"] = roDetails["ProductId"].GetLong();
                        dataRow["dt_ShortName"] = roDetails["PShortName"].GetString();
                        dataRow["dt_ProductName"] = roDetails["PName"].GetString();
                        dataRow["dt_Qty"] = roDetails["Qty"].GetDecimal();
                        dataRow["dt_Unit"] = roDetails["UnitCode"].GetUpper();
                        dataRow["dt_Rate"] = roDetails["Rate"].GetDecimal();
                        dataRow["dt_Balance"] = roDetails["Amount"].GetDecimal();
                        dataRow["IsGroup"] = 1;
                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                    }

                    dataRow = dtReport.NewRow();
                    dataRow["dt_ProductName"] = "VOUCHER TOTAL =>";
                    dataRow["dt_Qty"] = details.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
                    dataRow["dt_Balance"] = details.AsEnumerable().Sum(x => x["Amount"].GetDecimal());
                    dataRow["IsGroup"] = 22;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                }

                dataRow = dtReport.NewRow();
                dataRow["dt_ProductName"] = $"[{roGroup["GrpName"]}] GROUP TOTAL :- ";

                var qty = groupDetails.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
                var balance = groupDetails.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

                dataRow["dt_Qty"] = qty;
                dataRow["dt_Balance"] = balance;
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }

            dataRow = dtReport.NewRow();
            dataRow["dt_ProductName"] = $"[{roSubGroup["SubGrpName"]}] SUB GROUP TOTAL :- ";

            var qtySub = detailsSubGroup.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
            var balanceSub = detailsSubGroup.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

            dataRow["dt_Qty"] = qtySub;
            dataRow["dt_Balance"] = balanceSub;
            dataRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";
        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsDetailsReportVoucherGroupWise(DataSet dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        var distinctProduct = dtStock.Tables[0];
        var dtGroupType = distinctProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_ShortName"] = roGroup["GrpCode"].GetLong();
            dataRow["dt_ProductName"] = roGroup["GrpName"].GetString();
            dataRow["IsGroup"] = 0;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var groupDetails = distinctProduct.Select($"GrpName= '{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow roType in groupDetails.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dt_Date"] = roType["VMiti"];
                dataRow["dt_Miti"] = roType["VDate"].GetDateString();
                dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
                dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
                dataRow["dt_ShortName"] = roType["PShortName"].GetString();
                dataRow["dt_ProductName"] = roType["PName"].GetString();
                dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimal();
                dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
                dataRow["dt_Balance"] = roType["Amount"].GetDecimal();
                dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimal();

                var salesRate = roType["PSalesRate"].GetDecimal();
                var costRate = roType["Amount"].GetDecimal();
                var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;
                dataRow["dt_CostRatio"] = ratio > 0 ? ratio.GetDecimalString() + "%" : string.Empty;
                dataRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                var details = dtStock.Tables[1].Select($"VoucherNo = '{roType["VoucherNo"]}'").CopyToDataTable();
                foreach (DataRow roDetails in details.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dt_ProductId"] = roDetails["ProductId"].GetLong();
                    dataRow["dt_ShortName"] = roDetails["PShortName"].GetString();
                    dataRow["dt_ProductName"] = roDetails["PName"].GetString();
                    dataRow["dt_Qty"] = roDetails["Qty"].GetDecimal();
                    dataRow["dt_Unit"] = roDetails["UnitCode"].GetUpper();
                    dataRow["dt_Rate"] = roDetails["Rate"].GetDecimal();
                    dataRow["dt_Balance"] = roDetails["Amount"].GetDecimal();
                    dataRow["IsGroup"] = 1;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                }

                dataRow = dtReport.NewRow();
                dataRow["dt_ProductName"] = "VOUCHER TOTAL =>";
                dataRow["dt_Qty"] = details.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
                dataRow["dt_Balance"] = details.AsEnumerable().Sum(x => x["Amount"].GetDecimal());
                dataRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }

            dataRow = dtReport.NewRow();
            dataRow["dt_ProductName"] = $"[{roGroup["GrpName"]}] GROUP TOTAL :- ";

            var qty = groupDetails.AsEnumerable().Sum(x => x["FinishedGoodsQty"].GetDecimal());
            var balance = groupDetails.AsEnumerable().Sum(x => x["Amount"].GetDecimal());

            dataRow["dt_Qty"] = qty;
            dataRow["dt_Balance"] = balance;
            dataRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";
        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnBillOfMaterialsDetailsReportVoucherWise(DataSet dtStock)
    {
        DataRow dataRow;
        var dtReport = GetBillOfMaterialsReportFormat();
        var distinctProduct = dtStock.Tables[0].Copy();
        foreach (DataRow roType in distinctProduct.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dt_Date"] = roType["VMiti"];
            dataRow["dt_Miti"] = roType["VDate"].GetDateString();
            dataRow["dt_VoucherNo"] = roType["VoucherNo"].GetString();
            dataRow["dt_ProductId"] = roType["FinishedGoodsId"].GetLong();
            dataRow["dt_ShortName"] = roType["PShortName"].GetString();
            dataRow["dt_ProductName"] = roType["PName"].GetString();
            dataRow["dt_Qty"] = roType["FinishedGoodsQty"].GetDecimal();
            dataRow["dt_Unit"] = roType["UnitCode"].GetUpper();
            dataRow["dt_Balance"] = roType["Amount"].GetDecimal();
            dataRow["dt_SalesRate"] = roType["PSalesRate"].GetDecimal();

            var salesRate = roType["PSalesRate"].GetDecimal();
            var costRate = roType["Amount"].GetDecimal();
            var ratio = costRate > 0 && salesRate > 0 ? costRate / salesRate * 100 : 0;
            dataRow["dt_CostRatio"] = ratio > 0 ? ratio.GetDecimalString() + "%" : string.Empty;
            dataRow["IsGroup"] = 0;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var details = dtStock.Tables[1].Select($"VoucherNo = '{roType["VoucherNo"]}'").CopyToDataTable();
            foreach (DataRow roDetails in details.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dt_ProductId"] = roDetails["ProductId"].GetLong();
                dataRow["dt_ShortName"] = roDetails["PShortName"].GetString();
                dataRow["dt_ProductName"] = roDetails["PName"].GetString();
                dataRow["dt_Qty"] = roDetails["Qty"].GetDecimal();
                dataRow["dt_Unit"] = roDetails["UnitCode"].GetUpper();
                dataRow["dt_Rate"] = roDetails["Rate"].GetDecimal();
                dataRow["dt_Balance"] = roDetails["Amount"].GetDecimal();
                dataRow["IsGroup"] = 1;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }

            dataRow = dtReport.NewRow();
            dataRow["dt_ProductName"] = "VOUCHER TOTAL =>";
            dataRow["dt_Qty"] = details.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
            dataRow["dt_Balance"] = details.AsEnumerable().Sum(x => x["Amount"].GetDecimal());
            dataRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        var dtGroup = dtReport.Select("dt_Date <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dt_ProductName"] = "GRAND TOTAL :- ";
        var totalOpeningAltQty = dtGroup.AsEnumerable().Sum(x => x["dt_Qty"].GetDecimal());
        var totalOpeningQty = dtGroup.AsEnumerable().Sum(x => x["dt_Balance"].GetDecimal());

        dataRow["dt_Qty"] = totalOpeningAltQty;
        dataRow["dt_Balance"] = totalOpeningQty;
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private string GetBillOfMaterialsScript()
    {
        var cmdString = $@"
            SELECT VoucherNo, VDate, VMiti, FinishedGoodsId, p.PShortName, p.PName,pg.GrpCode,ISNULL(pg.GrpName,'NO GROUP') GrpName,psg.ShortName SubGroupCode,ISNULL(psg.SubGrpName,'NO SUB GROUP') SubGrpName, bm.FinishedGoodsId,bm.FinishedGoodsQty,pu.UnitCode, bm.Amount,p.PSalesRate
            FROM INV.BOM_Master bm
                 LEFT OUTER JOIN AMS.Product p ON p.PID=bm.FinishedGoodsId
	             LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
				 LEFT OUTER JOIN AMS.ProductGroup pg ON  pg.PGrpId = p.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON  psg.PSubGrpId = p.PSubGrpId
            WHERE bm.BranchId = '{ObjGlobal.SysBranchId}' AND bm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' AND bm.VDate BETWEEN  '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'";
        cmdString += GetReports.FilterValue.IsValueExits() ? $" AND bm.VoucherNo= '{GetReports.FilterValue}'" : " ";
        if (!GetReports.IsSummary)
        {
            cmdString += @$"
                SELECT bd.VoucherNo, ProductId,p.PShortName,p.PName, bd.CostCenterId, Qty, UnitId,pu.UnitCode, Rate, bd.Amount
                FROM INV.BOM_Details bd
                     LEFT OUTER JOIN INV.BOM_Master bm ON bm.VoucherNo=bd.VoucherNo
                     LEFT OUTER JOIN AMS.Product p ON p.PID=bd.ProductId
                     LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
                WHERE bm.BranchId = '{ObjGlobal.SysBranchId}' AND bm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' AND bm.VDate BETWEEN  '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'";
            cmdString += GetReports.FilterValue.IsValueExits() ? $" AND bm.VoucherNo= '{GetReports.FilterValue}'" : " ";
        }

        return cmdString;
    }


    // PROFITABILITY REPORT
    private string GetProfitabilityReportScript()
    {
        var cmdString = string.Empty;
        if (GetReports.RptMode is "CUSTOMER WISE")
        {
        }
        else if (GetReports.RptMode is "BILL WISE")
        {
        }
        else
        {
            cmdString = $@"
                WITH Profitability AS
                (
	                SELECT sd.Product_Id, SUM(sd.AltQty) AltQty,SUM(sd.Qty) Qty, SUM(sd.StockVal) Cogs, t.SalesAmount, (ISNULL(t.SalesAmount, 0)-SUM(sd.StockVal)) Profit
	                FROM AMS.StockDetails sd
		                LEFT OUTER JOIN(SELECT P_Id, (SUM(sd1.B_Amount)+ISNULL(t.TemAmount, 0)) SalesAmount
						                FROM AMS.SB_Details sd1
							                INNER JOIN AMS.SB_Master sm1 ON sd1.SB_Invoice=sm1.SB_Invoice
							                LEFT OUTER JOIN(SELECT sbt.Product_Id, SUM(CASE WHEN st.ST_Sign='+' THEN sbt.Amount WHEN st.ST_Sign='-' THEN -sbt.Amount ELSE 0 END) TemAmount
											                FROM AMS.SB_Term sbt
												                INNER JOIN AMS.ST_Term st ON st.ST_ID=sbt.ST_Id
												                INNER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sbt.SB_VNo
											                WHERE sm.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sm.CBranch_Id={ObjGlobal.SysBranchId} AND(sm.CUnit_Id='{ObjGlobal.SysCompanyUnitId}' OR sm.CUnit_Id IS NULL)AND sbt.Term_Type<>'B' AND sbt.ST_Id<>(SELECT SBVatTerm FROM AMS.SalesSetting)  AND sm.Invoice_Date  BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'
											                GROUP BY sbt.Product_Id) AS t ON sd1.P_Id=t.Product_Id
						                WHERE sm1.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sm1.CBranch_Id={ObjGlobal.SysBranchId} AND(sm1.CUnit_Id='{ObjGlobal.SysCompanyUnitId}' OR sm1.CUnit_Id IS NULL) AND sm1.Invoice_Date  BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND sm1.R_Invoice = 0
						                GROUP BY sd1.P_Id, t.TemAmount) AS t ON t.P_Id=sd.Product_Id
	                WHERE sd.Module='SB' AND sd.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sd.Branch_ID = {ObjGlobal.SysBranchId} AND (sd.CmpUnit_ID = '{ObjGlobal.SysCompanyUnitId}' OR sd.CmpUnit_ID IS NULL) AND sd.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'
	                GROUP BY Product_Id, t.SalesAmount
                )
                SELECT p.Product_Id,pd.PName,pd.PShortName, pd.PGrpId,ISNULL(pg.GrpName,'NO GROUP') GrpName, pd.PSubGrpId, ISNULL(psg.SubGrpName,'NO SUB GROUP') SubGrpName,";
            cmdString += ObjGlobal.ServerVersion switch
            {
                < 12 =>
                    $@" CAST( p.AltQty AS DECIMAL(18,{ObjGlobal.SysQtyLength})) AltQty,au.UnitCode AltUOM, CAST( p.Qty AS DECIMAL(18,{ObjGlobal.SysQtyLength})) Qty, pu.UnitCode UOM, CAST( p.Cogs AS DECIMAL(18,{ObjGlobal.SysAmountLength})) Cogs,CAST( p.SalesAmount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) SalesAmount,CAST( p.Profit AS DECIMAL(18,{ObjGlobal.SysAmountLength})) Profit, CASE WHEN ISNULL(p.Profit, 0)<>0 AND ISNULL(p.Cogs, 0)>0 THEN CAST(  ISNULL(p.Profit, 0)/ ISNULL(p.Cogs, 0)* 100 AS DECIMAL(18,{ObjGlobal.SysAmountLength})) ELSE CAST(  0 AS DECIMAL(18,{ObjGlobal.SysAmountLength})) END Ratio,",
                _ =>
                    $@" FORMAT(CAST( p.AltQty AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') AltQty,au.UnitCode AltUOM, FORMAT(CAST( p.Qty AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') Qty, pu.UnitCode UOM, FORMAT(CAST( p.Cogs AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') Cogs, FORMAT(CAST( p.SalesAmount AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') SalesAmount,FORMAT(CAST( p.Profit AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') Profit, CASE WHEN ISNULL(p.Profit, 0)<>0 AND ISNULL(p.Cogs, 0)>0 THEN FORMAT(CAST(  ISNULL(p.Profit, 0)/ ISNULL(p.Cogs, 0)* 100 AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') ELSE FORMAT(CAST(  0 AS DECIMAL(18,6)),'{ObjGlobal.SysAmountCommaFormat}') END Ratio, "
            };
            cmdString += @" 0 IsGroup
                FROM Profitability p
	                INNER JOIN AMS.Product pd ON pd.PID=p.Product_Id
	                LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=pd.PGrpId
	                LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=pd.PSubGrpId
	                LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=pd.PUnit
	                LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = pd.PAltUnit";
            cmdString += GetReports.SortOn switch
            {
                "SHORTNAME" => " ORDER BY pd.PShortName;",
                "QTY" => " ORDER BY P.Qty DESC;",
                "MARGIN" => " ORDER BY P.Profit DESC;",
                _ => " ORDER BY pd.PName;"
            };
        }

        return cmdString;
    }

    private DataTable ReturnProfitabilityReportProductWise(DataTable dtResult)
    {
        var dtReport = dtResult.Copy();

        var newRow = dtResult.NewRow();
        newRow["PName"] = "GRAND TOTAL => ";
        var totalQty = dtResult.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
        var totalCogs = dtResult.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
        var totalSalesAmount = dtResult.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
        var totalProfit = dtResult.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());
        var ratio = totalProfit != 0 && totalCogs > 0 ? totalProfit / totalCogs * 100 : 0;
        if (ObjGlobal.ServerVersion > 12)
        {
            newRow["Qty"] = totalQty;
            newRow["Cogs"] = totalCogs;
            newRow["SalesAmount"] = totalSalesAmount;
            newRow["Profit"] = totalProfit;
            newRow["Ratio"] = ratio.GetDecimal();
        }
        else
        {
            newRow["Qty"] = totalQty.GetDecimalComma();
            newRow["Cogs"] = totalCogs.GetDecimalComma();
            newRow["SalesAmount"] = totalSalesAmount.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Ratio"] = ratio.GetDecimalComma();
        }

        newRow["IsGroup"] = 99;
        dtResult.Rows.InsertAt(newRow, dtResult.Rows.Count + 1);

        return dtResult;
    }

    private DataTable ReturnProfitabilityReportProductGroupWise(DataTable dtResult)
    {
        DataRow newRow;
        var dtReport = dtResult.Clone();
        var dtGroupType = dtResult.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();

        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["PName"] = roGroup["GrpName"];
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var details = dtResult.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();

            details.AsEnumerable().ToList().ForEach(dataRow => dtReport.ImportRow(dataRow));

            newRow = dtReport.NewRow();
            newRow["PName"] = $"[{roGroup["GrpName"]}] GROUP TOTAL => ";

            var qty = details.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
            var cogs = details.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
            var salesAmount = details.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
            var profit = details.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());

            if (ObjGlobal.ServerVersion > 12)
            {
                newRow["Qty"] = qty;
                newRow["Cogs"] = cogs;
                newRow["SalesAmount"] = salesAmount;
                newRow["Profit"] = profit;
            }
            else
            {
                newRow["Qty"] = qty.GetDecimalComma();
                newRow["Cogs"] = cogs.GetDecimalComma();
                newRow["SalesAmount"] = salesAmount.GetDecimalComma();
                newRow["Profit"] = profit.GetDecimalComma();
            }

            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        newRow = dtReport.NewRow();
        newRow["PName"] = "GRAND TOTAL => ";
        var totalQty = dtResult.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
        var totalCogs = dtResult.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
        var totalSalesAmount = dtResult.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
        var totalProfit = dtResult.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());
        var ratio = totalProfit != 0 && totalCogs > 0 ? totalProfit / totalCogs * 100 : 0;
        if (ObjGlobal.ServerVersion > 12)
        {
            newRow["Qty"] = totalQty;
            newRow["Cogs"] = totalCogs;
            newRow["SalesAmount"] = totalSalesAmount;
            newRow["Profit"] = totalProfit;
            newRow["Ratio"] = ratio.GetDecimal();
        }
        else
        {
            newRow["Qty"] = totalQty.GetDecimalComma();
            newRow["Cogs"] = totalCogs.GetDecimalComma();
            newRow["SalesAmount"] = totalSalesAmount.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Ratio"] = ratio.GetDecimalComma();
        }

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtResult;
    }

    private DataTable ReturnProfitabilityReportProductSubGroupWise(DataTable dtResult)
    {
        var newRow = dtResult.NewRow();
        var dtReport = dtResult.Clone();
        var dtGroupType = dtResult.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();

        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            newRow["PName"] = roGroup["GrpName"];
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var details = dtResult.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();

            var dtSubGroupType = details.AsEnumerable().GroupBy(dataRow => new
            {
                grpType = dataRow.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(dataRow => dataRow!.Field<string>("SubGrpName"))
                .CopyToDataTable();
            foreach (DataRow roSubGroup in dtSubGroupType.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["PName"] = roSubGroup["SubGrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var subDetails = dtResult
                    .Select($"GrpName = '{roGroup["GrpName"]}' and SubGrpName = '{roSubGroup["SubGrpName"]}'")
                    .CopyToDataTable();

                subDetails.AsEnumerable().ToList().ForEach(dataRow => dtReport.ImportRow(dataRow));

                newRow = dtReport.NewRow();
                newRow["PName"] = $"[{roSubGroup["SubGrpName"]}] SUB GROUP TOTAL => ";

                var subQty = subDetails.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
                var subCogs = subDetails.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
                var subSalesAmount = subDetails.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
                var subProfit = subDetails.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());

                if (ObjGlobal.ServerVersion > 12)
                {
                    newRow["Qty"] = subQty;
                    newRow["Cogs"] = subCogs;
                    newRow["SalesAmount"] = subSalesAmount;
                    newRow["Profit"] = subProfit;
                }
                else
                {
                    newRow["Qty"] = subQty.GetDecimalComma();
                    newRow["Cogs"] = subCogs.GetDecimalComma();
                    newRow["SalesAmount"] = subSalesAmount.GetDecimalComma();
                    newRow["Profit"] = subProfit.GetDecimalComma();
                }

                newRow["Qty"] = subDetails.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
                newRow["Cogs"] = subDetails.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
                newRow["Profit"] = subDetails.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["PName"] = $"[{roGroup["Group"]}] GROUP TOTAL => ";

            var qty = details.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
            var cogs = details.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
            var salesAmount = details.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
            var profit = details.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());

            if (ObjGlobal.ServerVersion > 12)
            {
                newRow["Qty"] = qty;
                newRow["Cogs"] = cogs;
                newRow["SalesAmount"] = salesAmount;
                newRow["Profit"] = profit;
            }
            else
            {
                newRow["Qty"] = qty.GetDecimalComma();
                newRow["Cogs"] = cogs.GetDecimalComma();
                newRow["SalesAmount"] = salesAmount.GetDecimalComma();
                newRow["Profit"] = profit.GetDecimalComma();
            }

            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        newRow = dtReport.NewRow();
        newRow["PName"] = "GRAND TOTAL => ";
        var totalQty = dtResult.AsEnumerable().Sum(dataRow => dataRow["Qty"].GetDecimal());
        var totalCogs = dtResult.AsEnumerable().Sum(dataRow => dataRow["Cogs"].GetDecimal());
        var totalSalesAmount = dtResult.AsEnumerable().Sum(dataRow => dataRow["SalesAmount"].GetDecimal());
        var totalProfit = dtResult.AsEnumerable().Sum(dataRow => dataRow["Profit"].GetDecimal());
        var ratio = totalProfit != 0 && totalCogs > 0 ? totalProfit / totalCogs * 100 : 0;
        if (ObjGlobal.ServerVersion > 12)
        {
            newRow["Qty"] = totalQty;
            newRow["Cogs"] = totalCogs;
            newRow["SalesAmount"] = totalSalesAmount;
            newRow["Profit"] = totalProfit;
            newRow["Ratio"] = ratio.GetDecimal();
        }
        else
        {
            newRow["Qty"] = totalQty.GetDecimalComma();
            newRow["Cogs"] = totalCogs.GetDecimalComma();
            newRow["SalesAmount"] = totalSalesAmount.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Profit"] = totalProfit.GetDecimalComma();
            newRow["Ratio"] = ratio.GetDecimalComma();
        }

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtResult;
    }

    // STOCK LEDGER REPORT

    #region --------------- GET STOCK REPORT ---------------

    private DataTable ReturnStockLedgerSummaryReportProductGroupWiseIncludeGroup(string cmdString)
    {
        DataRow dataRow;
        var listResult = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var dtReport = listResult.Clone();

        var dtGroupType = listResult.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();

        foreach (DataRow dr in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["PShortName"] = dr["GroupCode"];
            dataRow["PName"] = dr["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var details = listResult.Select($"GrpName='{dr["GrpName"]}'");
            if (details is { Length: > 0 })
            {
                details.AsEnumerable().Take(details.Length).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                dataRow = dtReport.NewRow();
                dataRow["PName"] = $"[{dr["GrpName"]}] GRAND TOTAL >> ";

                dataRow["OpeningAltQty"] =
                    details.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["OpeningQty"] = details.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal()).GetDecimalComma();
                dataRow["OpeningValue"] =
                    details.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal()).GetDecimalComma();

                dataRow["ReceiptAltQty"] =
                    details.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["ReceiptQty"] = details.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal()).GetDecimalComma();
                dataRow["ReceiptValue"] =
                    details.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal()).GetDecimalComma();

                dataRow["IssueAltQty"] =
                    details.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["IssueQty"] = details.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal()).GetDecimalComma();
                dataRow["IssueValue"] = details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

                dataRow["BalanceAltQty"] =
                    details.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["BalanceQty"] = details.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal()).GetDecimalComma();
                dataRow["BalanceValue"] =
                    details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";

        dataRow["OpeningAltQty"] = dtReport.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["OpeningQty"] = dtReport.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal()).GetDecimalComma();
        dataRow["OpeningValue"] = dtReport.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal()).GetDecimalComma();

        dataRow["ReceiptAltQty"] = dtReport.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["ReceiptQty"] = dtReport.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal()).GetDecimalComma();
        dataRow["ReceiptValue"] = dtReport.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal()).GetDecimalComma();

        dataRow["IssueAltQty"] = dtReport.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["IssueQty"] = dtReport.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal()).GetDecimalComma();
        dataRow["IssueValue"] = dtReport.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

        dataRow["BalanceAltQty"] = dtReport.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["BalanceQty"] = dtReport.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal()).GetDecimalComma();
        dataRow["BalanceValue"] = dtReport.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnStockLedgerSummaryReportProductGroupWiseOnly(string cmdString)
    {
        var listResult = QueryUtils.GetList<StockLedgerSummaryReport>(cmdString);

        var finalList = new List<StockLedgerSummaryReport>();
        var productDetails = listResult.List.ToList();

        finalList.AddRange(productDetails);
        finalList.Add(new StockLedgerSummaryReport
        {
            PName = "[GRAND TOTAL] ",
            OpeningAltStockQty = listResult.List.Sum(y => y.OpeningAltStockQty),
            OpeningStockQty = listResult.List.Sum(y => y.OpeningStockQty),
            OpeningStockValue = listResult.List.Sum(y => y.OpeningStockValue),
            ReceiptAltStockQty = listResult.List.Sum(y => y.ReceiptAltStockQty),
            ReceiptStockQty = listResult.List.Sum(y => y.ReceiptStockQty),
            ReceiptStockValue = listResult.List.Sum(y => y.ReceiptStockValue),
            IssueAltStockQty = listResult.List.Sum(y => y.IssueAltStockQty),
            IssueStockQty = listResult.List.Sum(y => y.IssueStockQty),
            IssueStockValue = listResult.List.Sum(y => y.IssueStockValue),
            IsGroup = 99
        });
        var dtReport = finalList.ToDataTable();
        foreach (DataRow ro in dtReport.Rows)
        {
            if (ro["IsGroup"].GetInt() <= 0) continue;
            ro["StringBalanceStockRate"] = "";
        }

        return dtReport;
    }

    private DataTable ReturnStockLedgerSummaryReportProductSubGroupWiseIncludeGroup(string cmdString)
    {
        DataRow dataRow;
        var listResult = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var dtReport = listResult.Clone();

        var dtGroupType = listResult.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).CopyToDataTable();

        foreach (DataRow dr in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["PShortName"] = dr["GroupCode"];
            dataRow["PName"] = dr["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var details = listResult.Select($"GrpName='{dr["GrpName"]}'");
            if (details is { Length: > 0 })
            {
                var dtSubGroup = details.AsEnumerable().GroupBy(row => new
                {
                    group = row.Field<string>("GrpName"),
                    subGroup = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).CopyToDataTable();

                foreach (DataRow ro in dtGroupType.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["PShortName"] = ro["SubGroupCode"];
                    dataRow["PName"] = ro["SubGrpName"];
                    dataRow["IsGroup"] = 2;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                    var detailTable = details.CopyToDataTable();
                    var subDetails = detailTable.Select($"SubGrpName ='{ro["SubGrpName"]}'");
                    if (subDetails is { Length: > 0 })
                    {
                        subDetails.AsEnumerable().Take(subDetails.Length)
                            .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                        dataRow = dtReport.NewRow();
                        dataRow["PName"] = $"[{ro["SubGrpName"]}] GRAND TOTAL >> ";

                        dataRow["OpeningAltQty"] = details.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["OpeningQty"] = details.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["OpeningValue"] = details.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal())
                            .GetDecimalComma();

                        dataRow["ReceiptAltQty"] = details.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["ReceiptQty"] = details.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["ReceiptValue"] = details.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal())
                            .GetDecimalComma();

                        dataRow["IssueAltQty"] = details.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["IssueQty"] = details.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["IssueValue"] = details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal())
                            .GetDecimalComma();

                        dataRow["BalanceAltQty"] = details.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["BalanceQty"] = details.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal())
                            .GetDecimalComma();
                        dataRow["BalanceValue"] = details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal())
                            .GetDecimalComma();

                        dataRow["IsGroup"] = 11;
                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                    }
                }

                dataRow = dtReport.NewRow();
                dataRow["PName"] = $"[{dr["GrpName"]}] GRAND TOTAL >> ";

                dataRow["OpeningAltQty"] =
                    details.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["OpeningQty"] = details.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal()).GetDecimalComma();
                dataRow["OpeningValue"] =
                    details.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal()).GetDecimalComma();

                dataRow["ReceiptAltQty"] =
                    details.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["ReceiptQty"] = details.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal()).GetDecimalComma();
                dataRow["ReceiptValue"] =
                    details.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal()).GetDecimalComma();

                dataRow["IssueAltQty"] =
                    details.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["IssueQty"] = details.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal()).GetDecimalComma();
                dataRow["IssueValue"] = details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

                dataRow["BalanceAltQty"] =
                    details.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["BalanceQty"] = details.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal()).GetDecimalComma();
                dataRow["BalanceValue"] =
                    details.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";

        dataRow["OpeningAltQty"] = dtReport.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["OpeningQty"] = dtReport.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal()).GetDecimalComma();
        dataRow["OpeningValue"] = dtReport.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal()).GetDecimalComma();

        dataRow["ReceiptAltQty"] = dtReport.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["ReceiptQty"] = dtReport.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal()).GetDecimalComma();
        dataRow["ReceiptValue"] = dtReport.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal()).GetDecimalComma();

        dataRow["IssueAltQty"] = dtReport.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["IssueQty"] = dtReport.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal()).GetDecimalComma();
        dataRow["IssueValue"] = dtReport.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

        dataRow["BalanceAltQty"] = dtReport.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["BalanceQty"] = dtReport.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal()).GetDecimalComma();
        dataRow["BalanceValue"] = dtReport.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal()).GetDecimalComma();

        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnStockLedgerSummaryReportProductSubGroupWiseOnly(string cmdString)
    {
        var listResult = QueryUtils.GetList<StockLedgerSummaryReport>(cmdString);

        var finalList = new List<StockLedgerSummaryReport>();
        var productDetails = listResult.List.ToList();
        finalList.AddRange(productDetails);
        finalList.Add(new StockLedgerSummaryReport
        {
            PName = "[GRAND TOTAL] ",
            OpeningAltStockQty = listResult.List.Sum(y => y.OpeningAltStockQty),
            OpeningStockQty = listResult.List.Sum(y => y.OpeningStockQty),
            OpeningStockValue = listResult.List.Sum(y => y.OpeningStockValue),
            ReceiptAltStockQty = listResult.List.Sum(y => y.ReceiptAltStockQty),
            ReceiptStockQty = listResult.List.Sum(y => y.ReceiptStockQty),
            ReceiptStockValue = listResult.List.Sum(y => y.ReceiptStockValue),
            IssueAltStockQty = listResult.List.Sum(y => y.IssueAltStockQty),
            IssueStockQty = listResult.List.Sum(y => y.IssueStockQty),
            IssueStockValue = listResult.List.Sum(y => y.IssueStockValue),
            IsGroup = 99
        });
        var dtReport = finalList.ToDataTable();
        foreach (DataRow ro in dtReport.Rows)
        {
            if (ro["IsGroup"].GetInt() <= 0) continue;
            ro["StringBalanceStockRate"] = "";
        }

        return dtReport;
    }

    private DataTable ReturnStockLedgerSummaryReportProductWise(string cmdString)
    {
        var listResult = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var dtReport = listResult.Copy();

        var dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";

        var openingAltQty = dtReport.AsEnumerable().Sum(x => x["OpeningAltQty"].GetDecimal());
        var openingQty = dtReport.AsEnumerable().Sum(x => x["OpeningQty"].GetDecimal());
        var openingValue = dtReport.AsEnumerable().Sum(x => x["OpeningValue"].GetDecimal());

        var receiptAltQty = dtReport.AsEnumerable().Sum(x => x["ReceiptAltQty"].GetDecimal());
        var receiptQty = dtReport.AsEnumerable().Sum(x => x["ReceiptQty"].GetDecimal());
        var receiptValue = dtReport.AsEnumerable().Sum(x => x["ReceiptValue"].GetDecimal());

        var issueAltQty = dtReport.AsEnumerable().Sum(x => x["IssueAltQty"].GetDecimal());
        var issueQty = dtReport.AsEnumerable().Sum(x => x["IssueQty"].GetDecimal());
        var issueValue = dtReport.AsEnumerable().Sum(x => x["IssueValue"].GetDecimal());
        var salesValue = dtReport.AsEnumerable().Sum(x => x["SalesValue"].GetDecimal());

        var balanceAltQty = dtReport.AsEnumerable().Sum(x => x["BalanceAltQty"].GetDecimal());
        var balanceQty = dtReport.AsEnumerable().Sum(x => x["BalanceQty"].GetDecimal());
        var balanceValue = dtReport.AsEnumerable().Sum(x => x["BalanceValue"].GetDecimal());

        if (ObjGlobal.ServerVersion < 10)
        {
            dataRow["OpeningAltQty"] = openingAltQty;
            dataRow["OpeningQty"] = openingQty;
            dataRow["OpeningValue"] = openingValue;

            dataRow["ReceiptAltQty"] = receiptAltQty;
            dataRow["ReceiptQty"] = receiptQty;
            dataRow["ReceiptValue"] = receiptValue;

            dataRow["IssueAltQty"] = issueAltQty;
            dataRow["IssueQty"] = issueQty;
            dataRow["IssueValue"] = issueValue;
            dataRow["SalesValue"] = salesValue;

            dataRow["BalanceAltQty"] = balanceAltQty;
            dataRow["BalanceQty"] = balanceQty;
            dataRow["BalanceValue"] = balanceValue;
        }
        else
        {
            dataRow["OpeningAltQty"] = openingAltQty.GetDecimalComma();
            dataRow["OpeningQty"] = openingQty.GetDecimalComma();
            dataRow["OpeningValue"] = openingValue.GetDecimalComma();

            dataRow["ReceiptAltQty"] = receiptAltQty.GetDecimalComma();
            dataRow["ReceiptQty"] = receiptQty.GetDecimalComma();
            dataRow["ReceiptValue"] = receiptValue.GetDecimalComma();

            dataRow["IssueAltQty"] = issueAltQty.GetDecimalComma();
            dataRow["IssueQty"] = issueQty.GetDecimalComma();
            dataRow["IssueValue"] = issueValue.GetDecimalComma();
            dataRow["SalesValue"] = salesValue.GetDecimalComma();

            dataRow["BalanceAltQty"] = balanceAltQty.GetDecimalComma();
            dataRow["BalanceQty"] = balanceQty.GetDecimalComma();
            dataRow["BalanceValue"] = balanceValue.GetDecimalComma();
        }

        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    public DataTable GetStockLedgerSummaryReport()
    {
        if (GetReports.RePostValue)
        {
            const string cmdRePost = @"[AMS].[USP_PostStockValue]";
            var nonQuery = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        }

        var cmdString = $@"
            WITH StockSummaryReport AS (SELECT p.PID, p.PName, p.PShortName,au.UnitCode AltUom,u.UnitCode Uom,p.PGrpId,pg.GrpCode GroupCode,ISNULL(pg.GrpName,'NO GROUP') GrpName,p.PSubGrpId,psg.ShortName SubGroupCode,ISNULL(psg.SubGrpName,'NO SUB-GROUP') SubGrpName, ob.OpeningAltQty, ob.OpeningQty, ob.OpeningValue, rp.ReceiptAltQty, rp.ReceiptQty, rp.ReceiptValue, iss.IssueAltQty, iss.IssueQty, iss.IssueValue,i.SalesAmount SalesValue,  bl.BalanceAltQty, bl.BalanceQty, CASE WHEN ISNULL(bl.BalanceValue, 0)>0 AND ISNULL(bl.BalanceQty, 0)>0 THEN bl.BalanceValue ELSE 0 END BalanceValue
                            FROM AMS.Product p
                                 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId=pg.PGrpId
                                 LEFT OUTER JOIN AMS.ProductUnit au ON p.PAltUnit=au.UID
                                 LEFT OUTER JOIN AMS.ProductUnit u ON p.PUnit=u.UID
                                 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId=psg.PSubGrpId
                                 LEFT OUTER JOIN(SELECT sd.Product_Id,
													SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) OpeningAltQty,
													SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) OpeningQty,
													SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) OpeningValue
                                                 FROM AMS.StockDetails sd
                                                 WHERE sd.Branch_Id={ObjGlobal.SysBranchId} AND sd.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}'";
        cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.Product_Id in ({GetReports.ProductId})" : "";
        cmdString += $@"
                                                 GROUP BY sd.Product_Id) AS ob ON ob.Product_Id=p.PID
                                 LEFT OUTER JOIN(SELECT sd.Product_Id,SUM(sd.AltQty) ReceiptAltQty, SUM(sd.Qty) ReceiptQty, SUM(sd.StockVal) ReceiptValue
                                                 FROM AMS.StockDetails sd
                                                 WHERE  sd.EntryType = 'I' AND sd.Branch_Id={ObjGlobal.SysBranchId} AND sd.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'";
        cmdString += GetReports.ProductId.IsValueExits() ? $"AND  sd.Product_Id in ({GetReports.ProductId})" : "";
        cmdString += $@"
                                                 GROUP BY sd.Product_Id) AS rp ON rp.Product_Id=p.PID
                                 LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(sd.AltQty) IssueAltQty, SUM(sd.Qty) IssueQty, SUM(sd.StockVal) IssueValue
                                                 FROM AMS.StockDetails sd
                                                 WHERE sd.EntryType = 'O' AND sd.Branch_Id={ObjGlobal.SysBranchId} AND sd.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'";
        cmdString += GetReports.ProductId.IsValueExits() ? $"AND  sd.Product_Id in ({GetReports.ProductId})" : "";
        cmdString += $@"
                                                 GROUP BY sd.Product_Id) AS iss ON iss.Product_Id=p.PID
                                 LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) BalanceAltQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) BalanceQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) BalanceValue
                                                 FROM AMS.StockDetails sd
                                                 WHERE  sd.Branch_Id={ObjGlobal.SysBranchId} AND sd.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}'"; // AND sd.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'
        cmdString += GetReports.ProductId.IsValueExits() ? $" AND  sd.Product_Id in ({GetReports.ProductId})" : "";
        cmdString += @"
                                                 GROUP BY sd.Product_Id) AS bl ON bl.Product_Id=p.PID
                                 LEFT OUTER JOIN( SELECT P_Id, SUM(sd1.B_Amount) + ISNULL(t.TemAmount, 0) SalesAmount FROM AMS.SB_Details sd1
                                                        LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice = sd1.SB_Invoice
                                                        LEFT OUTER JOIN(SELECT sbt.Product_Id, SUM(CASE WHEN st.ST_Sign='+' THEN sbt.Amount WHEN st.ST_Sign='-' THEN -sbt.Amount ELSE 0 END) TemAmount FROM AMS.SB_Term sbt
                                                                                                           INNER JOIN AMS.ST_Term st ON st.ST_ID=sbt.ST_Id
                                                                                                      WHERE sbt.Term_Type<>'B' AND sbt.ST_Id<>(SELECT SBVatTerm FROM AMS.SalesSetting) ";
        cmdString += GetReports.ProductId.IsValueExits() ? $" AND sbt.Product_Id in ({GetReports.ProductId})" : "";
        cmdString += $@"
                                                                                                      GROUP BY sbt.Product_Id) AS t ON sd1.P_Id=t.Product_Id
                                                                                 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND (sm.R_Invoice =0 OR sm.R_Invoice IS NULL) ";
        cmdString += GetReports.ProductId.IsValueExits() ? $@" AND sd1.P_Id IN({GetReports.ProductId}) " : "";
        cmdString += @"
                                                                                 GROUP BY sd1.P_Id,ISNULL(t.TemAmount, 0))i ON i.P_Id=p.PID
                                WHERE 1=1 ";
        cmdString += GetReports.ProductId.IsValueExits() ? $@" AND p.PID IN  ({GetReports.ProductId}) " : " ";
        if (GetReports.NegativeStockOnly && !GetReports.IncludeZeroBalance)
            cmdString += " AND (ISNULL(bl.BalanceQty, 0)) < 0  ";
        else if (GetReports.ExcludeNegative && !GetReports.IncludeZeroBalance)
            cmdString += " AND (ISNULL(bl.BalanceQty, 0)) > 0   ";
        else if (!GetReports.IncludeZeroBalance) cmdString += " AND (ISNULL(bl.BalanceQty, 0)) <> 0   ";
        cmdString += @" )
            SELECT ";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => " ROW_NUMBER() OVER (PARTITION BY st.GrpName ORDER BY st.PName) Sno",
            "SUBGROUP WISE" => " ROW_NUMBER() OVER (PARTITION BY st.GrpName,st.SubGrpName ORDER BY st.PName) Sno",
            _ => " ROW_NUMBER() OVER (ORDER BY st.PName) Sno"
        };
        cmdString +=
            $@" ,st.PID, st.PName, st.PShortName,st.AltUom,st.Uom,st.PGrpId, st.GroupCode, st.GrpName, st.PSubGrpId, st.SubGroupCode, st.SubGrpName,
            CASE WHEN ABS(ISNULL(st.OpeningAltQty, 0))>0 THEN FORMAT(ISNULL(st.OpeningAltQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningAltQty,
            CASE WHEN ABS(ISNULL(st.OpeningQty, 0))>0 THEN FORMAT(ISNULL(st.OpeningQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningQty,
            CASE WHEN ISNULL(st.OpeningQty, 0)>0 THEN FORMAT(ISNULL(st.OpeningValue, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningValue,
            CASE WHEN ISNULL(st.ReceiptAltQty, 0)>0 THEN FORMAT(ISNULL(st.ReceiptAltQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ReceiptAltQty,
            CASE WHEN ISNULL(st.ReceiptQty, 0)>0 THEN FORMAT(ISNULL(st.ReceiptQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ReceiptQty,
            CASE WHEN ISNULL(st.ReceiptValue, 0)>0 THEN FORMAT(ISNULL(st.ReceiptValue, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ReceiptValue,
            CASE WHEN ISNULL(st.IssueAltQty, 0)>0 THEN FORMAT(ISNULL(st.IssueAltQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END IssueAltQty,
            CASE WHEN ISNULL(st.IssueQty, 0)>0 THEN FORMAT(ISNULL(st.IssueQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END IssueQty,
            CASE WHEN ISNULL(st.IssueValue, 0)>0 THEN FORMAT(ISNULL(st.IssueValue, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END IssueValue,
            CASE WHEN ISNULL(st.SalesValue, 0)>0 THEN FORMAT(ISNULL(st.SalesValue, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END SalesValue,
            CASE WHEN ABS(ISNULL(st.BalanceAltQty, 0))>0 THEN FORMAT(ISNULL(st.BalanceAltQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END BalanceAltQty,
            CASE WHEN ABS(ISNULL(st.BalanceQty, 0))>0 THEN FORMAT(ISNULL(st.BalanceQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END BalanceQty,
            CASE WHEN ISNULL(st.BalanceQty, 0)>0 THEN FORMAT(ISNULL(st.BalanceValue, 0)/ ISNULL(st.BalanceQty, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END BalanceRate,
            CASE WHEN ISNULL(st.BalanceQty, 0)>0 THEN FORMAT(ISNULL(st.BalanceValue, 0), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END BalanceValue, 0 IsGroup";
        cmdString += @"
            FROM StockSummaryReport st ";
        if (GetReports.IncludeZeroBalance)
            cmdString += @"
            WHERE st.PID IN (SELECT Product_Id FROM AMS.StockDetails) ";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => @"
            ORDER BY st.GrpName;",
            "SUBGROUP WISE" => @"
            ORDER BY st.GrpName,os.SubGrpName; ",
            _ => @"
            ORDER BY st.PName;"
        };
        var dtReport = GetReports.RptMode switch
        {
            "GROUP ONLY" => ReturnStockLedgerSummaryReportProductGroupWiseOnly(cmdString),
            "GROUP WISE" => ReturnStockLedgerSummaryReportProductGroupWiseIncludeGroup(cmdString),
            "SUBGROUP ONLY" => ReturnStockLedgerSummaryReportProductSubGroupWiseOnly(cmdString),
            "SUBGROUP WISE" => ReturnStockLedgerSummaryReportProductSubGroupWiseIncludeGroup(cmdString),
            _ => ReturnStockLedgerSummaryReportProductWise(cmdString)
        };
        return dtReport;
    }

    
    // DETAILS REPORTS
    private DataTable ReturnStockLedgerDetailsReportProductIncludeProductGroupWise(DataTable dtStock)
    {
        return dtStock;
    }

    private DataTable ReturnStockLedgerDetailsReportProductIncludeProductSubGroupWise(DataTable dtStock)
    {
        return dtStock;
    }

    private DataTable ReturnStockLedgerDetailsReportProductWise(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = dtStock.Clone();

        var dtGroupType = dtStock.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("PName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("PName")).CopyToDataTable();

        foreach (DataRow p in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["VoucherNo"] = p["PShortName"];
            dataRow["Ledger"] = p["PName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
            var productId = p["Product_Id"].GetLong();
            var details = dtStock.Select($"Product_Id = {productId} ").CopyToDataTable();
            if (details.Rows.Count > 0)
            {
                details.AsEnumerable().Take(details.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                var detailsOpeningAltQty = details.AsEnumerable().Sum(row => row["OpeningAltQty"].GetDecimal());
                var detailsOpeningQty = details.AsEnumerable().Sum(row => row["OpeningQty"].GetDecimal());
                var detailsOpeningValue = details.AsEnumerable().Sum(row => row["OpeningVal"].GetDecimal());
                var detailsReceiptAltQty = details.AsEnumerable().Sum(row => row["ReceiptAltQty"].GetDecimal());
                var detailsReceiptQty = details.AsEnumerable().Sum(row => row["ReceiptQty"].GetDecimal());
                var detailsReceiptValue = details.AsEnumerable().Sum(row => row["ReceiptValue"].GetDecimal());
                var detailsIssueAltQty = details.AsEnumerable().Sum(row => row["IssueAltQty"].GetDecimal());
                var detailsIssueQty = details.AsEnumerable().Sum(row => row["IssueQty"].GetDecimal());
                var detailsIssueValue = details.AsEnumerable().Sum(row => row["IssueValue"].GetDecimal());

                var detailsBalanceAltQty = detailsOpeningAltQty + detailsReceiptAltQty - detailsIssueAltQty;
                var detailsBalanceQty = detailsOpeningQty + detailsReceiptQty - detailsIssueQty;
                var detailsBalanceValue = detailsOpeningValue + detailsReceiptValue - detailsIssueValue;

                dataRow = dtReport.NewRow();
                dataRow["Ledger"] = $"[{p["PName"]}] TOTAL >>";

                dataRow["OpeningAltQty"] = detailsOpeningAltQty.GetDecimalComma();
                dataRow["OpeningQty"] = detailsOpeningQty.GetDecimalComma();
                dataRow["OpeningVal"] = detailsOpeningValue.GetDecimalComma();

                dataRow["ReceiptAltQty"] = detailsReceiptAltQty.GetDecimalComma();
                dataRow["ReceiptQty"] = detailsReceiptQty.GetDecimalComma();
                dataRow["ReceiptValue"] = detailsReceiptValue.GetDecimalComma();

                dataRow["IssueAltQty"] = detailsIssueAltQty.GetDecimalComma();
                dataRow["IssueQty"] = detailsIssueQty.GetDecimalComma();
                dataRow["IssueValue"] = detailsIssueValue.GetDecimalComma();

                dataRow["BalanceAlt"] = detailsBalanceQty != 0 ? detailsBalanceAltQty.GetDecimalComma() : "NIL";
                dataRow["BalanceQty"] = detailsBalanceQty != 0 ? detailsBalanceQty.GetDecimalComma() : "NIL";
                dataRow["BalanceValue"] = detailsBalanceQty != 0 ? detailsBalanceValue.GetDecimalComma() : "NIL";

                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
            }
        }

        dataRow = dtReport.NewRow();
        dataRow["Ledger"] = "GRAND TOTAL >>";

        var totalOpeningAltQty = dtStock.AsEnumerable().Sum(row => row["OpeningAltQty"].GetDecimal());
        var totalOpeningQty = dtStock.AsEnumerable().Sum(row => row["OpeningQty"].GetDecimal());
        var totalOpeningValue = dtStock.AsEnumerable().Sum(row => row["OpeningVal"].GetDecimal());
        var totalReceiptAltQty = dtStock.AsEnumerable().Sum(row => row["ReceiptAltQty"].GetDecimal());
        var totalReceiptQty = dtStock.AsEnumerable().Sum(row => row["ReceiptQty"].GetDecimal());
        var totalReceiptValue = dtStock.AsEnumerable().Sum(row => row["ReceiptValue"].GetDecimal());
        var totalIssueAltQty = dtStock.AsEnumerable().Sum(row => row["IssueAltQty"].GetDecimal());
        var totalIssueQty = dtStock.AsEnumerable().Sum(row => row["IssueQty"].GetDecimal());
        var totalIssueValue = dtStock.AsEnumerable().Sum(row => row["IssueValue"].GetDecimal());

        var totalBalanceAltQty = totalOpeningAltQty + totalReceiptAltQty - totalIssueAltQty;
        var totalBalanceQty = totalOpeningQty + totalReceiptQty - totalIssueQty;
        var totalBalanceValue = totalOpeningValue + totalReceiptValue - totalIssueValue;

        dataRow["OpeningAltQty"] = totalOpeningAltQty.GetDecimalComma();
        dataRow["OpeningQty"] = totalOpeningQty.GetDecimalComma();
        dataRow["OpeningVal"] = totalOpeningValue.GetDecimalComma();
        dataRow["ReceiptAltQty"] = totalReceiptAltQty.GetDecimalComma();
        dataRow["ReceiptQty"] = totalReceiptQty.GetDecimalComma();
        dataRow["ReceiptValue"] = totalReceiptValue.GetDecimalComma();
        dataRow["IssueAltQty"] = totalIssueAltQty.GetDecimalComma();
        dataRow["IssueQty"] = totalIssueQty.GetDecimalComma();
        dataRow["IssueValue"] = totalIssueValue.GetDecimalComma();

        dataRow["BalanceAlt"] = totalBalanceQty != 0 ? totalBalanceAltQty.GetDecimalComma() : "NIL";
        dataRow["BalanceQty"] = totalBalanceQty != 0 ? totalBalanceQty.GetDecimalComma() : "NIL";
        dataRow["BalanceValue"] = totalBalanceQty != 0 ? totalBalanceValue.GetDecimalComma() : "NIL";

        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
        return dtReport;
    }

    public DataTable GetStockLedgerDetailsReport()
    {
        var cmdString = new StringBuilder();
        if (GetReports.RePostValue)
        {
            const string cmdRePost = "AMS.USP_PostStockValue";
            var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        }

        cmdString.Append($@"
			WITH StockDetails AS
            (
	            SELECT trn.Product_Id, trn.PName, trn.PShortName,trn.Module, trn.VoucherNo,trn.Serial_No,trn.VoucherDate, trn.VoucherMiti, trn.Ledger, trn.OpeningAltQty, trn.OpeningQty, trn.OpeningVal, trn.ReceiptAltQty, trn.ReceiptQty, trn.ReceiptValue, trn.IssueAltQty, trn.IssueQty, trn.IssueValue,
                SUM(ISNULL(trn.OpeningAltQty, 0)+ISNULL(trn.ReceiptAltQty, 0)-ISNULL(trn.IssueAltQty, 0)) OVER (PARTITION BY trn.PName ORDER BY trn.PName, trn.VoucherDate,trn.ReceiptQty DESC, trn.VoucherNo ROWS UNBOUNDED PRECEDING) AS BalanceAlt,
                SUM(ISNULL(trn.OpeningQty, 0)+ISNULL(trn.ReceiptQty, 0)-ISNULL(trn.IssueQty, 0)) OVER (PARTITION BY trn.PName ORDER BY trn.PName, trn.VoucherDate,trn.ReceiptQty DESC, trn.VoucherNo ROWS UNBOUNDED PRECEDING) AS BalanceQty,
                CASE WHEN  SUM(ISNULL(trn.OpeningQty, 0)+ISNULL(trn.ReceiptQty, 0)-ISNULL(trn.IssueQty, 0)) OVER (PARTITION BY trn.PName ORDER BY trn.PName, trn.VoucherDate,trn.ReceiptQty DESC, trn.VoucherNo ROWS UNBOUNDED PRECEDING) > 0 then
                SUM(ISNULL(trn.OpeningVal, 0)+ISNULL(trn.ReceiptValue, 0)-ISNULL(trn.IssueValue, 0)) OVER (PARTITION BY trn.PName ORDER BY trn.PName, trn.VoucherDate,trn.ReceiptQty DESC, trn.VoucherNo  ROWS UNBOUNDED PRECEDING) ELSE 0 END AS BalanceValue
	            FROM(SELECT fy.Product_Id, p.PName, p.PShortName, fy.Module, fy.VoucherNo,fy.Serial_No, fy.VoucherDate, fy.VoucherMiti, fy.Ledger, fy.OpeningAltQty, fy.OpeningQty, fy.OpeningVal, fy.ReceiptAltQty, fy.ReceiptQty, fy.ReceiptValue, fy.IssueAltQty, fy.IssueQty, fy.IssueValue
		             FROM AMS.Product p
			            LEFT OUTER JOIN(SELECT 'OB' Module, sd.Product_Id, '' VoucherNo,0 Serial_No, '' VoucherDate, '' VoucherMiti, 'OPENING BALANCE B/F' Ledger, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) OpeningAltQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) OpeningQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) OpeningVal, 0 ReceiptAltQty, 0 ReceiptQty, 0 ReceiptValue, 0 IssueAltQty, 0 IssueQty, 0 IssueValue
							            FROM AMS.StockDetails sd
							            WHERE sd.Branch_Id={ObjGlobal.SysBranchId} AND sd.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}'");
        if (GetReports.ProductId.IsValueExits())
            cmdString.Append($"  AND sd.Product_Id IN ({GetReports.ProductId}) \n");
        cmdString.Append(@$"
                                        GROUP BY sd.Product_Id
                                        HAVING  SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) <> 0
							            UNION ALL
							            SELECT sd.Module,sd.Product_Id,sd.Voucher_No VoucherNo,sd.Serial_No,sd.Voucher_Date VoucherDate, sd.Voucher_Miti VoucherMiti,CASE WHEN sd.Ledger_Id IS NULL THEN md.ModuleType  ELSE gl.GLName END Ledger, 0 OpeningAltQty, 0 OpeningQty, 0 OpeningValue, CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE 0 END ReceiptAltQty, CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE 0 END ReceiptQty, CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE 0 END ReceiptStockVal, CASE WHEN sd.EntryType='O' THEN sd.AltQty ELSE 0 END IssueAltQty, CASE WHEN sd.EntryType='O' THEN sd.Qty ELSE 0 END IssueQty, CASE WHEN sd.EntryType='O' THEN sd.StockVal ELSE 0 END IssueStockVal
							            FROM AMS.StockDetails sd
								            LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sd.Ledger_ID
                                            LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module COLLATE DATABASE_DEFAULT = sd.Module
							            WHERE sd.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}'");
        if (GetReports.ProductId.IsValueExits())
            cmdString.Append($"  AND sd.Product_Id IN ({GetReports.ProductId}) \n");
        cmdString.Append(@"
                                        ) AS fy ON fy.Product_Id = p.PID
                    WHERE 1 = 1 ");
        if (GetReports.ProductGroupId.IsValueExits())
            cmdString.Append($"  AND P.PGrpID IN ({GetReports.ProductGroupId}) \n");
        if (GetReports.ProductSubGroupId.IsValueExits())
            cmdString.Append($"  AND p.PSubGrpId IN ({GetReports.ProductSubGroupId}) \n");
        if (GetReports.ProductId.IsValueExits()) cmdString.Append($" AND fy.Product_Id IN ({GetReports.ProductId}) \n");
        cmdString.Append($@"
                    ) AS trn
	            WHERE trn.Ledger IS NOT NULL
	            GROUP BY trn.Product_Id, trn.PName, trn.PShortName,trn.Module, trn.VoucherNo,trn.Serial_No, trn.VoucherDate, trn.VoucherMiti, trn.Ledger, trn.OpeningAltQty, trn.OpeningQty, trn.OpeningVal, trn.ReceiptAltQty, trn.ReceiptQty, trn.ReceiptValue, trn.IssueAltQty, trn.IssueQty, trn.IssueValue
            ) SELECT st.Product_Id, st.PName, st.PShortName,p.PGrpId,pg.GrpCode,ISNULL(pg.GrpName,'NO_GROUP') GrpName,p.PSubGrpId,psg.ShortName SubGrpCode,ISNULL(psg.SubGrpName,'NO SUB_GROUP') SubGrpName,st.Module, st.VoucherNo, CONVERT(NVARCHAR,st.VoucherDate,103) VoucherDate, st.VoucherMiti, st.Ledger,
            CASE WHEN ISNULL(OpeningAltQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.OpeningAltQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END OpeningAltQty,
			CASE WHEN ISNULL(OpeningQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.OpeningQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END OpeningQty,
			CASE WHEN ISNULL(OpeningVal, 0)>0 THEN FORMAT(CAST(ISNULL(st.OpeningVal, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningVal,
			CASE WHEN ISNULL(ReceiptAltQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.ReceiptAltQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END ReceiptAltQty,
			CASE WHEN ISNULL(ReceiptQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.ReceiptQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END ReceiptQty,
			CASE WHEN ISNULL(ReceiptValue, 0)>0 THEN FORMAT(CAST(ISNULL(st.ReceiptValue, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ReceiptValue,
			CASE WHEN ISNULL(IssueAltQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.IssueAltQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END IssueAltQty,
			CASE WHEN ISNULL(IssueQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.IssueQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END IssueQty,
			CASE WHEN ISNULL(IssueValue, 0)>0 THEN FORMAT(CAST(ISNULL(st.IssueValue, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END IssueValue,
			CASE WHEN ISNULL(BalanceAlt, 0)>0 THEN FORMAT(CAST(ISNULL(st.BalanceAlt, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END BalanceAlt,
			CASE WHEN ISNULL(BalanceQty, 0)<>0 THEN FORMAT(CAST(ISNULL(st.BalanceQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE '' END BalanceQty,
			CASE WHEN ISNULL(BalanceQty, 0)>0 THEN  FORMAT(CAST(ISNULL(st.BalanceValue, 0)/ ISNULL(st.BalanceQty, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}') ELSE '' END BalanceRate,
			CASE WHEN ISNULL(BalanceQty, 0)>0 THEN FORMAT(CAST(ISNULL(st.BalanceValue, 0) AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END BalanceValue ");
        cmdString.Append(@"
            ,0 IsGroup FROM StockDetails st
                LEFT OUTER JOIN AMS.Product p ON p.PID = st.Product_Id
	            LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
	            LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
	            LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
	            LEFT OUTER JOIN AMS.ProductUnit u ON u.UID = p.PUnit
            ORDER BY st.PName,CAST(st.VoucherDate AS DATE) ASC,st.ReceiptQty DESC,st.VoucherNo ");
        var dtStock = SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
        if (dtStock.Rows.Count < 0)
        {
            return new DataTable();
        }
        var dtReport = GetReports.RptMode switch
        {
            "GROUP WISE" => ReturnStockLedgerDetailsReportProductIncludeProductGroupWise(dtStock),
            "SUBGROUP WISE" => ReturnStockLedgerDetailsReportProductIncludeProductSubGroupWise(dtStock),
            _ => ReturnStockLedgerDetailsReportProductWise(dtStock)
        };
        return dtReport;
    }

    #endregion --------------- GET STOCK REPORT ---------------

    //PRODUCT OPENING REPORT

    #region ** ----- PRODUCT OPENING ---- **

    private DataTable GetStockOpeningLedgerSummaryReport(DataTable dtStock)
    {
        var index = 1;
        DataRow dataRow;
        var dtReport = GetStockValueReportFormat();
        foreach (DataRow item in dtStock.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dtSNo"] = index;
            dataRow["dtProductId"] = item["Product_Id"].ToString();
            dataRow["dtShortName"] = item["PShortName"].ToString();
            dataRow["dtProduct"] = item["PName"].ToString();
            dataRow["dtStockAltUnit"] = item["AltUnitCode"].ToString();
            dataRow["dtStockUnit"] = item["UnitCode"].ToString();

            var openingAltQty = ObjGlobal.ReturnDouble(item["AltQty"].ToString());
            var openingQty = ObjGlobal.ReturnDouble(item["Qty"].ToString());
            var openingValue = ObjGlobal.ReturnDouble(item["Amount"].ToString());
            var openingRate = openingQty > 0 ? openingValue / openingQty : 0;

            dataRow["dtStockAltQty"] = openingAltQty;
            dataRow["dtStockQty"] = openingQty;
            dataRow["dtRate"] = openingRate;
            dataRow["dtAmount"] = openingQty > 0 ? openingValue : 0.ToString();
            dataRow["IsGroup"] = "0";
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            index++;
        }

        var dtGrand = dtReport.Select("dtProductId <> ''").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["dtProduct"] = "GRAND TOTAL :- ";
        dataRow["dtStockAltQty"] = dtGrand.AsEnumerable().Sum(x => x["dtStockAltQty"].GetDecimal());
        dataRow["dtStockQty"] = dtGrand.AsEnumerable().Sum(x => x["dtStockQty"].GetDecimal());
        dataRow["dtAmount"] = dtGrand.AsEnumerable().Sum(x => x["dtAmount"].GetDecimal());
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    public DataTable GetProductOpeningLedgerWise()
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			WITH OpeningProduct  AS
			(
				Select Product_Id,Godown_Id,Department_Id1,AltQty,AltUnit_Id,Case when EntryType='O' then (-StockQty) else StockQty end Qty,Unit_Id,Case when EntryType='O' then (-StockVal) else StockVal end  Amount from ams.StockDetails SD
				WHERE SD.FiscalYearId <={ObjGlobal.SysFiscalYearId}
				UNION All
				SELECT Product_Id,Godown_Id,po.Cls1,AltQty,po.AltUnit,po.Qty Qty,po.QtyUnit,Amount  FROM AMS.ProductOpening po WHERE po.FiscalYearId = {ObjGlobal.SysFiscalYearId}
			) SELECT po.Product_Id,p.PShortName,P.PName,po.Godown_Id,G.GName,ISNULL(P.PGrpId,0) GrpId,ISNULL(pG.GrpName,'NO GROUP') GrpName,ISNULL(P.PSubGrpId,0) PSubGrpId,ISNULL(psG.SubGrpName,'NO SUBGROUP') SubGrpName,po.Department_Id1,D.DName,SUM(po.AltQty) AltQty, P.PAltUnit,AU.UnitCode AltUnitCode, SUM(po.Qty) Qty,P.PUnit,U.UnitCode ,CASE WHEN SUM(po.Qty) > 0 THEN SUM(po.Amount) ELSE  0 END Amount  FROM OpeningProduct po
			inner join AMS.Product P on po.Product_Id=p.PID
			left Outer join AMS.ProductGroup pG ON pg.PGrpID = p.PGrpID
			left Outer join AMS.ProductSubGroup psG ON psg.PSubGrpId = p.PSubGrpId
			left Outer join AMS.Godown G on po.Godown_Id=G.GID
			left Outer join AMS.Department D on po.Department_Id1=D.DId
			left outer Join AMS.ProductUnit AU  on P.PAltUnit= AU.UID
			left outer Join AMS.ProductUnit U on P.PUnit = U.UID ");
        if (!string.IsNullOrWhiteSpace(GetReports.ProductId))
            cmdString.Append($" WHERE po.Product_Id in ({GetReports.ProductId}) \n");
        cmdString.Append(@"
			Group By Product_Id,P.PAltUnit,AU.UnitCode,P.PUnit,U.UnitCode,P.PShortName,PName,P.PGrpId,P.PSubGrpId,pG.GrpName,psG.SubGrpName,Godown_Id,G.GName,po.Department_Id1,D.DName
			having Sum(Qty) <> 0
			Order By PName ");
        var dtOpening = SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
        return GetStockOpeningLedgerSummaryReport(dtOpening);
    }

    public DataTable GetProductOpeningOnly()
    {
        const string cmdRePost = "AMS.USP_PostStockValue";
        var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        var cmdString = $@"
            WITH OpeningStock AS (SELECT p.PID ProductId, p.PName, p.PShortName, psg.GrpId,pg.GrpCode GrpShortName,ISNULL(pg.GrpName,'NO-GROUP') GrpName,p.PSubGrpId,psg.ShortName SubGroupShortName,ISNULL(psg.SubGrpName,'NO SUB-GROUP') SubGrpName,(ISNULL(fy.StockAltQty, 0)+ISNULL(ob.StockAltQty, 0)) StockAltQty, au.UnitCode AltUom, (ISNULL(fy.StockQty, 0)+ISNULL(ob.StockQty, 0)) StockQty, u.UnitCode Uom, (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0)) StockVal, CASE WHEN p.PTax>0 THEN (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))+(ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))* p.PTax / 100 ELSE (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))END StockValueWithVat
                  FROM AMS.Product p
                       LEFT OUTER JOIN AMS.ProductUnit u ON u.UID=p.PUnit
                       LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
	                   LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
					   LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
                       LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) StockAltQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) StockQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) StockVal
                                       FROM AMS.StockDetails sd
                                       WHERE sd.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND sd.Branch_Id = {ObjGlobal.SysBranchId}
                                       GROUP BY sd.Product_Id
                                       HAVING SUM(CASE WHEN sd.EntryType = 'I' THEN sd.StockVal ELSE -sd.StockVal END) > 0 ) AS fy ON fy.Product_Id=p.PID
                       LEFT OUTER JOIN(SELECT sd1.Product_Id, SUM(sd1.AltQty) StockAltQty, SUM(sd1.Qty) StockQty, SUM(sd1.StockVal) StockVal
                                       FROM AMS.StockDetails sd1
                                       WHERE sd1.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sd1.Module='POB' AND sd1.Branch_Id = {ObjGlobal.SysBranchId}
                                       GROUP BY sd1.Product_Id
                                       HAVING SUM(CASE WHEN sd1.EntryType = 'I' THEN sd1.StockVal ELSE -sd1.StockVal END) > 0 )ob ON ob.Product_Id=p.PID)
            SELECT ";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => " ROW_NUMBER() OVER (PARTITION BY os.GrpName ORDER BY os.GrpName,os.PName) Sno",
            "SUBGROUP WISE" =>
                " ROW_NUMBER() OVER (PARTITION BY os.GrpName,os.SubGroupShortName ORDER BY os.GrpName,os.SubGroupShortName,os.PName)Sno",
            _ => " ROW_NUMBER() OVER (ORDER BY os.PName) Sno"
        };
        cmdString +=
            @" ,os.ProductId, os.PName, os.PShortName,os.GrpId,os.GrpShortName,os.GrpName,os.PSubGrpId,os.SubGroupShortName,os.SubGrpName,";
        if (ObjGlobal.ServerVersion < 10)
            cmdString +=
                $@" CAST(os.StockAltQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})) StockAltQty, os.AltUom, CAST(os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})) StockQty, os.Uom, CASE WHEN StockVal>0 THEN CAST(os.StockVal / os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE CAST(0 AS DECIMAL(18, {ObjGlobal.SysAmountLength})) END StockRate,CASE WHEN StockVal > 0 THEN CAST(os.StockVal AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE FORMAT(0, '##,##,##0.00') END StockVal,CASE WHEN StockVal > 0 THEN CAST(os.StockValueWithVat AS DECIMAL(18,{ObjGlobal.SysAmountLength})) ELSE FORMAT(0, '##,##,##0.00') END StockValueWithVat,0 IsGroup ";
        else
            cmdString +=
                $@" FORMAT(CAST(os.StockAltQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})), '{ObjGlobal.SysQtyCommaFormat}') StockAltQty, os.AltUom, FORMAT(CAST(os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})), '{ObjGlobal.SysQtyCommaFormat}') StockQty, os.Uom, CASE WHEN StockVal>0 THEN FORMAT(CAST(os.StockVal / os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})), '{ObjGlobal.SysQtyCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysQtyCommaFormat}')END StockRate, FORMAT(CAST(os.StockVal AS DECIMAL(18, {ObjGlobal.SysAmountLength})), '{ObjGlobal.SysQtyCommaFormat}') StockVal, FORMAT(CAST(os.StockValueWithVat AS DECIMAL(18, {ObjGlobal.SysAmountLength})), '{ObjGlobal.SysQtyCommaFormat}') StockValueWithVat,0 IsGroup ";
        cmdString += @"
            FROM OpeningStock os
            WHERE os.StockQty<>0 ";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => " ORDER BY os.GrpName,os.PName;",
            "SUBGROUP WISE" => " ORDER BY os.GrpName,os.SubGrpName,os.PName; ",
            _ => " ORDER BY os.PName;"
        };
        var dtStock = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtStock.Rows.Count is 0)
        {
            return new DataTable();
        }
        var dtReport = GetReports.RptMode switch
        {
            "GROUP WISE" => ReturnStockValueWithProductGroupIncludeProduct(dtStock),
            "SUBGROUP WISE" => ReturnStockValueWithProductSubGroupIncludeProduct(dtStock),
            _ => ReturnStockValueWithProduct(dtStock)
        };
        return dtReport;
    }

    #endregion ** ----- PRODUCT OPENING ---- **

    //STOCK VALUATION REPORT

    #region ** ----- STOCK VALUE ---- **

    private DataTable GetStockValueReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dtSNo",
            "dtProductId",
            "dtShortName",
            "dtProduct",
            "dtStockAltQty",
            "dtStockAltUnit",
            "dtStockQty",
            "dtStockUnit",
            "dtRate",
            "dtAmount",
            "dtFilter"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnStockValueWithProduct(DataTable dtStock)
    {
        var dtReport = dtStock.Copy();

        var dtGrand = dtReport.Select("IsGroup=0").CopyToDataTable();
        var dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";
        dataRow["StockAltQty"] = dtGrand.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockQty"] = dtGrand.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockVal"] = dtGrand.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnStockValueWithProductGroupIncludeProduct(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = dtStock.Clone();

        var dtGroupType = dtStock.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow row in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["ProductId"] = row["GrpId"];
            dataRow["PShortName"] = row["GrpShortName"];
            dataRow["PName"] = row["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var details = dtStock.Select($"GrpName= '{row["GrpName"]}'");
            if (details is { Length: > 0 })
            {
                details.AsEnumerable().Take(details.Length).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
                dataRow = dtReport.NewRow();
                dataRow["PName"] = $"[{row["GrpName"].GetUpper()}] GRAND TOTAL >> ";
                dataRow["StockAltQty"] = details.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["StockQty"] = details.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
                dataRow["StockVal"] = details.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        var dtGrand = dtReport.Select("IsGroup=0").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";
        dataRow["StockAltQty"] = dtGrand.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockQty"] = dtGrand.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockVal"] = dtGrand.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnStockValueWithProductSubGroupIncludeProduct(DataTable dtStock)
    {
        DataRow dataRow;
        var dtReport = dtStock.Clone();

        var dtGroupType = dtStock.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow row in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["ProductId"] = row["GrpId"];
            dataRow["PShortName"] = row["GrpShortName"];
            dataRow["PName"] = row["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

            var details = dtStock.Select($"GrpName= '{row["GrpName"]}'");
            if (details is { Length: > 0 })
            {
                var dtSubGroup = details.AsEnumerable().GroupBy(row => new
                {
                    group = row.Field<string>("GrpName"),
                    subGroup = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("SubGrpName"))
                    .CopyToDataTable();
                foreach (DataRow dr in dtSubGroup.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["ProductId"] = dr["PSubGrpId"];
                    dataRow["PShortName"] = dr["SubGroupShortName"];
                    dataRow["PName"] = dr["SubGrpName"];
                    dataRow["IsGroup"] = 2;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                    var subDetails =
                        dtStock.Select($"GrpName= '{row["GrpName"]}' and SubGrpName = '{dr["SubGrpName"]}'");
                    if (subDetails is { Length: > 0 })
                    {
                        subDetails.AsEnumerable().Take(subDetails.Length)
                            .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                        dataRow = dtReport.NewRow();
                        dataRow["PName"] = $"[{dr["SubGrpName"]}] GRAND TOTAL >> ";
                        dataRow["StockAltQty"] = details.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
                        dataRow["StockQty"] = details.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
                        dataRow["StockVal"] = details.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
                        dataRow["IsGroup"] = 22;
                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                    }
                }

                dataRow = dtReport.NewRow();
                dataRow["PName"] = $"[{row["GrpName"].GetUpper()}] GRAND TOTAL >> ";
                dataRow["StockAltQty"] = details.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
                dataRow["StockQty"] = details.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
                dataRow["StockVal"] = details.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        var dtGrand = dtReport.Select("IsGroup=0").CopyToDataTable();
        dataRow = dtReport.NewRow();
        dataRow["PName"] = "GRAND TOTAL >> ";
        dataRow["StockAltQty"] = dtGrand.AsEnumerable().Sum(x => x["StockAltQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockQty"] = dtGrand.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
        dataRow["StockVal"] = dtGrand.AsEnumerable().Sum(x => x["StockVal"].GetDecimal()).GetDecimalComma();
        dataRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    public DataTable GetClosingStockValueLedgerWise()
    {
        if (GetReports.RePostValue)
        {
            const string cmdRePost = "AMS.USP_PostStockValue";
            var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        }

        var cmdString = $@"
			WITH StockValuation AS (SELECT p.PID ProductId, p.PName, p.PShortName, psg.GrpId,pg.GrpCode GrpShortName,ISNULL(pg.GrpName,'NO-GROUP') GrpName,p.PSubGrpId,psg.ShortName SubGroupShortName,(ISNULL(fy.StockAltQty, 0)+ISNULL(ob.StockAltQty, 0)) StockAltQty, au.UnitCode AltUom, (ISNULL(fy.StockQty, 0)+ISNULL(ob.StockQty, 0)) StockQty, u.UnitCode Uom, (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0)) StockVal, CASE WHEN p.PTax>0 THEN (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))+(ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))* p.PTax / 100 ELSE (ISNULL(fy.StockVal, 0)+ISNULL(ob.StockVal, 0))END StockValueWithVat
                      FROM AMS.Product p
                           LEFT OUTER JOIN AMS.ProductUnit u ON u.UID=p.PUnit
                           LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					       LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
					       LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
                           LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) StockAltQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) StockQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) StockVal
                                           FROM AMS.StockDetails sd
                                           WHERE sd.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' AND sd.Branch_Id ={ObjGlobal.SysBranchId}
                                           GROUP BY sd.Product_Id) AS fy ON fy.Product_Id=p.PID
                           LEFT OUTER JOIN(SELECT sd1.Product_Id, SUM(sd1.AltQty) StockAltQty, SUM(sd1.Qty) StockQty, SUM(sd1.StockVal) StockVal
                                           FROM AMS.StockDetails sd1
                                           WHERE sd1.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sd1.Branch_Id ={ObjGlobal.SysBranchId} AND sd1.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' --AND sd1.Module='POB'
                                           GROUP BY sd1.Product_Id)ob ON ob.Product_Id=p.PID)
            SELECT ROW_NUMBER() OVER (ORDER BY os.PName) Sno, os.ProductId, os.PName, os.PShortName,os.GrpId,os.GrpShortName,os.GrpName,os.PSubGrpId,os.SubGroupShortName,os.SubGrpName,";
        cmdString += ObjGlobal.ServerVersion switch
        {
            < 12 =>
                $@" CAST(os.StockAltQty AS DECIMAL(18, {ObjGlobal.SysQtyLength})) StockAltQty, os.AltUom, CAST(os.StockQty AS DECIMAL(18, {ObjGlobal.SysQtyLength})) StockQty, os.Uom, CASE WHEN StockVal>0 THEN CAST(os.StockVal / os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE CAST(0 AS DECIMAL(18,{ObjGlobal.SysAmountLength})) END StockRate, CAST(os.StockVal AS DECIMAL(18, {ObjGlobal.SysAmountLength}))  StockVal, CAST(os.StockValueWithVat AS DECIMAL(18, {ObjGlobal.SysAmountLength})) StockValueWithVat,0 IsGroup",
            _ =>
                $@" FORMAT(CAST(os.StockAltQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockAltQty, os.AltUom, FORMAT(CAST(os.StockQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockQty, os.Uom, CASE WHEN StockVal>0 THEN FORMAT(CAST(os.StockVal / os.StockQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysQtyCommaFormat}')END StockRate, FORMAT(CAST(os.StockVal AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockVal, FORMAT(CAST(os.StockValueWithVat AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockValueWithVat,0 IsGroup"
        };

        cmdString += @"
            FROM StockValuation os WHERE 1=1 ";
        cmdString += GetReports.ExcludeNegative
            ? @"
            AND os.StockQty > 0"
            : @"
            AND os.StockQty <> 0";
        cmdString += GetReports.ProductId.IsValueExits()
            ? $@"
            AND os.ProductId in ({GetReports.ProductId})"
            : "";
        cmdString += GetReports.SortOn switch
        {
            "SHORTNAME" => @" ORDER BY os.PShortName; ",
            "QTY" => @" ORDER BY os.StockQty DESC; ",
            "AMOUNT" => @" ORDER BY os.StockVal DESC; ",
            _ => @" ORDER BY os.PName; "
        };
        var dtQuery = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtQuery.Rows.Count <= 0) return new DataTable();
        var dtReport = ReturnStockValueWithProduct(dtQuery);
        return dtReport;
    }

    public DataTable GetClosingStockValue()
    {
        if (GetReports.RePostValue)
        {
            const string cmdRePost = "AMS.USP_PostStockValue";
            var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", GetReports.ProductId));
        }

        var cmdString = $@"
			WITH StockValuation AS
            (
                SELECT p.PID ProductId, p.PName, p.PShortName,psg.GrpId,pg.GrpCode GrpShortName,ISNULL(pg.GrpName,'NO-GROUP') GrpName,p.PSubGrpId,psg.ShortName SubGroupShortName,ISNULL(psg.SubGrpName,'NO SUB-GROUP') SubGrpName,  ISNULL(ob.StockAltQty, 0) StockAltQty, au.UnitCode AltUom, ISNULL(ob.StockQty, 0) StockQty, u.UnitCode Uom, ISNULL(ob.StockVal, 0) StockVal, CASE WHEN p.PTax>0 THEN ISNULL(ob.StockVal, 0) + ISNULL(ob.StockVal, 0)* p.PTax / 100 ELSE ISNULL(ob.StockVal, 0)END StockValueWithVat
                FROM AMS.Product p
                   LEFT OUTER JOIN AMS.ProductUnit u ON u.UID=p.PUnit
                   LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
                   LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
				   LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
                   LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltQty ELSE -sd.AltQty END) StockAltQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty ELSE -sd.Qty END) StockQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) StockVal
                       FROM AMS.StockDetails sd
                       WHERE sd.FiscalYearId <= {ObjGlobal.SysFiscalYearId} AND sd.Branch_Id ={ObjGlobal.SysBranchId} AND sd.Voucher_Date <= '{GetReports.ToDate.GetSystemDate()}'
                       GROUP BY sd.Product_Id)ob ON ob.Product_Id=p.PID)
            SELECT ";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => " ROW_NUMBER() OVER (PARTITION BY os.GrpName ORDER BY os.GrpName,os.PName)Sno ",
            "SUBGROUP WISE" =>
                " ROW_NUMBER() OVER (PARTITION BY os.GrpName,os.SubGroupShortName ORDER BY os.GrpName,os.SubGroupShortName,os.PName)Sno ",
            _ => " ROW_NUMBER() OVER (ORDER BY os.PName)Sno "
        };
        cmdString +=
            @",os.ProductId, os.PName, os.PShortName,os.GrpId,os.GrpShortName,os.GrpName,os.PSubGrpId,os.SubGroupShortName,os.SubGrpName,";
        cmdString += ObjGlobal.ServerVersion < 10
            ? $@" CAST(os.StockAltQty AS DECIMAL(18, {ObjGlobal.SysQtyLength})) StockAltQty, os.AltUom, CAST(os.StockQty AS DECIMAL(18, {ObjGlobal.SysQtyLength})) StockQty, os.Uom, CASE WHEN StockVal>0 THEN CAST(os.StockVal / os.StockQty AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE CAST(0 AS DECIMAL(18, {ObjGlobal.SysAmountLength})) END StockRate, CASE WHEN ISNULL(os.StockVal, 0)>0 THEN CAST(os.StockVal AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE CAST(0  AS DECIMAL(18,{ObjGlobal.SysAmountLength})) END StockVal, CASE WHEN ISNULL(os.StockValueWithVat, 0)>0 THEN CAST(os.StockValueWithVat AS DECIMAL(18, {ObjGlobal.SysAmountLength})) ELSE CAST(0 AS DECIMAL(18, {ObjGlobal.SysAmountLength}) )END StockValueWithVat,"
            : $@" FORMAT(CAST(os.StockAltQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockAltQty, os.AltUom,
                        FORMAT(CAST(os.StockQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}') StockQty, os.Uom,
                        CASE WHEN StockVal>0 THEN FORMAT(CAST(os.StockVal / os.StockQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysQtyCommaFormat}')END StockRate,
                        CASE WHEN ISNULL(os.StockVal, 0)>0 THEN FORMAT(CAST(os.StockVal AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysAmountCommaFormat}')END StockVal,
                        CASE WHEN StockVal>0 THEN FORMAT(CAST(os.StockValueWithVat / os.StockQty AS DECIMAL(18, 6)), '{ObjGlobal.SysQtyCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysQtyCommaFormat}')END StockRateWithVat,
                        CASE WHEN ISNULL(os.StockValueWithVat, 0)>0 THEN FORMAT(CAST(os.StockValueWithVat AS DECIMAL(18, 6)), '{ObjGlobal.SysAmountCommaFormat}')ELSE FORMAT(0, '{ObjGlobal.SysAmountCommaFormat}')END StockValueWithVat,";
        cmdString += @" 0 IsGroup
            FROM StockValuation os WHERE 1=1 ";
        if (GetReports.NegativeStockOnly)
            cmdString += @" AND os.StockQty < 0";
        else if (GetReports.ExcludeNegative)
            cmdString += @" AND os.StockQty > 0";
        else if (!GetReports.IncludeZeroBalance) cmdString += @" AND os.StockQty <> 0";
        cmdString += GetReports.ProductId.IsValueExits()
            ? $@"
            AND os.ProductId in ({GetReports.ProductId})"
            : "";
        cmdString += GetReports.RptMode switch
        {
            "GROUP WISE" => "\n ORDER BY os.GrpName,PName;",
            "SUBGROUP WISE" => "\n ORDER BY os.GrpName,os.SubGrpName,PName; ",
            _ => "\n ORDER BY os.PName;"
        };
        var dtQuery = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtQuery.Rows.Count <= 0) return new DataTable();
        var dtReport = GetReports.RptMode switch
        {
            "GROUP WISE" => ReturnStockValueWithProductGroupIncludeProduct(dtQuery),
            "SUBGROUP WISE" => ReturnStockValueWithProductSubGroupIncludeProduct(dtQuery),
            _ => ReturnStockValueWithProduct(dtQuery)
        };
        return dtReport;
    }

    #endregion ** ----- STOCK VALUE ---- **

    // LIST OF MASTER OF PRODUCT

    #region --------------- LIST OF MASTER ---------------

    private DataTable GetProductListReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dtSNo",
            "dtProductId",
            "dtShortName",
            "dtProduct",
            "dtAltUnit",
            "dtUnit",
            "dtMrpRate",
            "dtBuyRate",
            "dtSalesRate",
            "dtStatus",
            "dtType",
            "dtCategory",
            "dtGrpName",
            "dtSubGrpName",
            "dtPTax",
            "dtTaxable",
            "dtPMax",
            "dtPMin",
            "dtSalesLedger",
            "dtSalesReturnLedger",
            "dtPurchaseLedger",
            "dtPurchaseReturnLedger",
            "dtOpeningLedger",
            "dtClosingLedger",
            "dtStockInHandLedger"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnProductList(DataTable dtProduct)
    {
        var dtReport = GetProductListReportFormat();
        foreach (DataRow ro in dtProduct.Rows)
        {
            var dataRow = dtReport.NewRow();
            dataRow["dtSNo"] = ro["SerialNo"];
            dataRow["dtProductId"] = ro["PID"];
            dataRow["dtShortName"] = ro["PShortName"];
            dataRow["dtProduct"] = ro["PName"];
            dataRow["dtAltUnit"] = ro["AltUnit"];
            dataRow["dtUnit"] = ro["Unit"];
            dataRow["dtMrpRate"] = ro["PMRP"];
            dataRow["dtBuyRate"] = ro["PBuyRate"];
            dataRow["dtSalesRate"] = ro["PSalesRate"];
            dataRow["dtType"] = ro["PType"];
            dataRow["dtCategory"] = ro["PCategory"];
            dataRow["dtStatus"] = ro["Status"];
            dataRow["dtGrpName"] = ro["GrpName"];
            dataRow["dtSubGrpName"] = ro["SubGrpName"];
            dataRow["dtPTax"] = ro["PTax"];
            dataRow["dtTaxable"] = ro["Tax_Type"];
            dataRow["dtPMax"] = ro["PMax"];
            dataRow["dtPMin"] = ro["PMin"];
            dataRow["dtSalesLedger"] = ro["SalesLedger"];
            dataRow["dtSalesReturnLedger"] = ro["SalesReturnLedger"];
            dataRow["dtPurchaseLedger"] = ro["PurchaseLedger"];
            dataRow["dtPurchaseReturnLedger"] = ro["PurchaseReturnLedger"];
            dataRow["dtOpeningLedger"] = ro["OpeningLedger"];
            dataRow["dtClosingLedger"] = ro["ClosingLedger"];
            dataRow["dtStockInHandLedger"] = ro["StockInHandLedger"];
            dataRow["IsGroup"] = 0;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProductListWithProductType(DataTable dtProduct)
    {
        DataRow dataRow;
        var dtReport = GetProductListReportFormat();
        var dtGroupType = dtProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("PType")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("PType")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dtProduct"] = roGroup["PType"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProduct.Select($"PType='{roGroup["PType"]}'").CopyToDataTable();
            foreach (DataRow ro in dtGroupDetails.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dtSNo"] = ro["SerialNo"];
                dataRow["dtProductId"] = ro["PID"];
                dataRow["dtShortName"] = ro["PShortName"];
                dataRow["dtProduct"] = ro["PName"];
                dataRow["dtAltUnit"] = ro["AltUnit"];
                dataRow["dtUnit"] = ro["Unit"];
                dataRow["dtMrpRate"] = ro["PMRP"];
                dataRow["dtBuyRate"] = ro["PBuyRate"];
                dataRow["dtSalesRate"] = ro["PSalesRate"];
                dataRow["dtType"] = ro["PType"];
                dataRow["dtCategory"] = ro["PCategory"];
                dataRow["dtStatus"] = ro["Status"];
                dataRow["dtGrpName"] = ro["GrpName"];
                dataRow["dtSubGrpName"] = ro["SubGrpName"];
                dataRow["dtPTax"] = ro["PTax"];
                dataRow["dtTaxable"] = ro["Tax_Type"];
                dataRow["dtPMax"] = ro["PMax"];
                dataRow["dtPMin"] = ro["PMin"];
                dataRow["dtSalesLedger"] = ro["SalesLedger"];
                dataRow["dtSalesReturnLedger"] = ro["SalesReturnLedger"];
                dataRow["dtPurchaseLedger"] = ro["PurchaseLedger"];
                dataRow["dtPurchaseReturnLedger"] = ro["PurchaseReturnLedger"];
                dataRow["dtOpeningLedger"] = ro["OpeningLedger"];
                dataRow["dtClosingLedger"] = ro["ClosingLedger"];
                dataRow["dtStockInHandLedger"] = ro["StockInHandLedger"];
                dataRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    private DataTable ReturnProductListWithProductTaxable(DataTable dtProduct)
    {
        var dtReport = GetProductListReportFormat();
        var dtGroupType = dtProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("Tax_Type")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("Tax_Type")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            var dataRow = dtReport.NewRow();
            dataRow["dtProduct"] = roGroup["Tax_Type"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProduct.Select($"Tax_Type='{roGroup["Tax_Type"]}'").CopyToDataTable();
            foreach (DataRow ro in dtGroupDetails.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dtSNo"] = ro["SerialNo"];
                dataRow["dtProductId"] = ro["PID"];
                dataRow["dtShortName"] = ro["PShortName"];
                dataRow["dtProduct"] = ro["PName"];
                dataRow["dtAltUnit"] = ro["AltUnit"];
                dataRow["dtUnit"] = ro["Unit"];
                dataRow["dtMrpRate"] = ro["PMRP"];
                dataRow["dtBuyRate"] = ro["PBuyRate"];
                dataRow["dtSalesRate"] = ro["PSalesRate"];
                dataRow["dtType"] = ro["PType"];
                dataRow["dtCategory"] = ro["PCategory"];
                dataRow["dtStatus"] = ro["Status"];
                dataRow["dtGrpName"] = ro["GrpName"];
                dataRow["dtSubGrpName"] = ro["SubGrpName"];
                dataRow["dtPTax"] = ro["PTax"];
                dataRow["dtTaxable"] = ro["Tax_Type"];
                dataRow["dtPMax"] = ro["PMax"];
                dataRow["dtPMin"] = ro["PMin"];
                dataRow["dtSalesLedger"] = ro["SalesLedger"];
                dataRow["dtSalesReturnLedger"] = ro["SalesReturnLedger"];
                dataRow["dtPurchaseLedger"] = ro["PurchaseLedger"];
                dataRow["dtPurchaseReturnLedger"] = ro["PurchaseReturnLedger"];
                dataRow["dtOpeningLedger"] = ro["OpeningLedger"];
                dataRow["dtClosingLedger"] = ro["ClosingLedger"];
                dataRow["dtStockInHandLedger"] = ro["StockInHandLedger"];
                dataRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    private DataTable ReturnProductListWithGroup(DataTable dtProduct)
    {
        var dtReport = GetProductListReportFormat();
        var dtGroupType = dtProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            var dataRow = dtReport.NewRow();
            dataRow["dtShortName"] = roGroup["GrpCode"];
            dataRow["dtProduct"] = roGroup["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProduct.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow ro in dtGroupDetails.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dtSNo"] = ro["SerialNo"];
                dataRow["dtProductId"] = ro["PID"];
                dataRow["dtShortName"] = ro["PShortName"];
                dataRow["dtProduct"] = ro["PName"];
                dataRow["dtAltUnit"] = ro["AltUnit"];
                dataRow["dtUnit"] = ro["Unit"];
                dataRow["dtMrpRate"] = ro["PMRP"];
                dataRow["dtBuyRate"] = ro["PBuyRate"];
                dataRow["dtSalesRate"] = ro["PSalesRate"];
                dataRow["dtType"] = ro["PType"];
                dataRow["dtCategory"] = ro["PCategory"];
                dataRow["dtStatus"] = ro["Status"];
                dataRow["dtGrpName"] = ro["GrpName"];
                dataRow["dtSubGrpName"] = ro["SubGrpName"];
                dataRow["dtPTax"] = ro["PTax"];
                dataRow["dtTaxable"] = ro["Tax_Type"];
                dataRow["dtPMax"] = ro["PMax"];
                dataRow["dtPMin"] = ro["PMin"];
                dataRow["dtSalesLedger"] = ro["SalesLedger"];
                dataRow["dtSalesReturnLedger"] = ro["SalesReturnLedger"];
                dataRow["dtPurchaseLedger"] = ro["PurchaseLedger"];
                dataRow["dtPurchaseReturnLedger"] = ro["PurchaseReturnLedger"];
                dataRow["dtOpeningLedger"] = ro["OpeningLedger"];
                dataRow["dtClosingLedger"] = ro["ClosingLedger"];
                dataRow["dtStockInHandLedger"] = ro["StockInHandLedger"];
                dataRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    private DataTable ReturnProductListWithSubGroup(DataTable dtProduct)
    {
        DataRow dataRow;
        var dtReport = GetProductListReportFormat();
        var dtGroupType = dtProduct.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpName")
        }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow roGroup in dtGroupType.Rows)
        {
            dataRow = dtReport.NewRow();
            dataRow["dtShortName"] = roGroup["GrpCode"];
            dataRow["dtProduct"] = roGroup["GrpName"];
            dataRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProduct.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
            var dtSubGroupType = dtProduct.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("SubGrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row!.Field<string>("SubGrpName")).CopyToDataTable();
            foreach (DataRow roSubGroup in dtGroupType.Rows)
            {
                dataRow = dtReport.NewRow();
                dataRow["dtShortName"] = roSubGroup["SubGroupCode"];
                dataRow["dtProduct"] = roSubGroup["SubGrpName"];
                dataRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                var dtSubGroupDetails = dtProduct
                    .Select($"GrpName='{roGroup["GrpName"]}' and SubGrpName='{roSubGroup["SubGrpName"]}'")
                    .CopyToDataTable();
                foreach (DataRow ro in dtSubGroupDetails.Rows)
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dtSNo"] = ro["SerialNo"];
                    dataRow["dtProductId"] = ro["PID"];
                    dataRow["dtShortName"] = ro["PShortName"];
                    dataRow["dtProduct"] = ro["PName"];
                    dataRow["dtAltUnit"] = ro["AltUnit"];
                    dataRow["dtUnit"] = ro["Unit"];
                    dataRow["dtMrpRate"] = ro["PMRP"];
                    dataRow["dtBuyRate"] = ro["PBuyRate"];
                    dataRow["dtSalesRate"] = ro["PSalesRate"];
                    dataRow["dtType"] = ro["PType"];
                    dataRow["dtCategory"] = ro["PCategory"];
                    dataRow["dtStatus"] = ro["Status"];
                    dataRow["dtGrpName"] = ro["GrpName"];
                    dataRow["dtSubGrpName"] = ro["SubGrpName"];
                    dataRow["dtPTax"] = ro["PTax"];
                    dataRow["dtTaxable"] = ro["Tax_Type"];
                    dataRow["dtPMax"] = ro["PMax"];
                    dataRow["dtPMin"] = ro["PMin"];
                    dataRow["dtSalesLedger"] = ro["SalesLedger"];
                    dataRow["dtSalesReturnLedger"] = ro["SalesReturnLedger"];
                    dataRow["dtPurchaseLedger"] = ro["PurchaseLedger"];
                    dataRow["dtPurchaseReturnLedger"] = ro["PurchaseReturnLedger"];
                    dataRow["dtOpeningLedger"] = ro["OpeningLedger"];
                    dataRow["dtClosingLedger"] = ro["ClosingLedger"];
                    dataRow["dtStockInHandLedger"] = ro["StockInHandLedger"];
                    dataRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                }
            }
        }

        return dtReport;
    }

    public DataTable GetMasterProductList()
    {
        var cmdString = @"
            SELECT ROW_NUMBER() OVER (ORDER BY p.PName) AS SerialNo, p.PID, p.PName, p.PShortName, CASE WHEN p.PType='S' THEN 'SERVICE ITEM' WHEN p.PType='I' THEN 'INVENTORY' WHEN p.PType='A' THEN 'ASSETS' ELSE UPPER(p.PType)END PType, CASE WHEN p.PCategory='FG' THEN 'FINISEHD GOODS' WHEN p.PCategory='RM' THEN 'RAW MATERIALS' ELSE UPPER(p.PCategory)END PCategory, UPPER(pu.UnitCode) AS AltUnit, UPPER(pu1.UnitCode) AS Unit, p.PMRP, p.PSalesRate, p.PBuyRate, CASE WHEN p.Status=1 THEN 'Active' ELSE 'InActive' END Status, pg.GrpCode, ISNULL(pg.GrpName, 'NO-GROUP') AS GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO-SUB GROUP') AS SubGrpName, p.PTax,CASE WHEN p.PTax > 0 THEN 'TAXABLE' ELSE 'NON-TAXABLE' END Tax_Type, p.PMax, p.PMin, PSL.GLName AS SalesLedger, PSR.GLName AS SalesReturnLedger, PPL.GLName AS PurchaseLedger, PPR.GLName AS PurchaseReturnLedger, PL_Opening.GLName AS OpeningLedger, PL_Closing.GLName AS ClosingLedger, BS_Closing.GLName AS StockInHandLedger
            FROM AMS.Product p
	             LEFT OUTER JOIN AMS.ProductUnit pu ON p.PAltUnit=pu.UID
	             LEFT OUTER JOIN AMS.ProductUnit pu1 ON p.PUnit=pu1.UID
	             LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId=pg.PGrpId
	             LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId=psg.PSubGrpId
	             LEFT OUTER JOIN AMS.GeneralLedger PSL ON p.PSL=PSL.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger PSR ON p.PSR=PSR.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger PPL ON p.PPL=PPL.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger PPR ON p.PPR=PPR.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger PL_Opening ON p.PL_Opening=PL_Opening.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger PL_Closing ON p.PL_Closing=PL_Closing.GLID
	             LEFT OUTER JOIN AMS.GeneralLedger BS_Closing ON p.BS_Closing=BS_Closing.GLID
            WHERE 1=1";
        cmdString += GetReports.ProductId.IsValueExits() ? $"  AND p.PID in ({GetReports.ProductId}) " : "";
        cmdString += GetReports.ProductGroupId.IsValueExits()
            ? $"  AND p.PGrpID in ({GetReports.ProductGroupId}) "
            : " ";
        cmdString += GetReports.ProductSubGroupId.IsValueExits()
            ? $" AND p.PSubGrpId in ({GetReports.ProductSubGroupId}) "
            : " ";
        cmdString += "  ORDER BY p.PName; ";
        var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtProduct.Rows.Count is 0) return dtProduct;
        var dtReport = GetReports.RptName switch
        {
            "PRODUCT GROUP/PRODUCT" => ReturnProductListWithGroup(dtProduct),
            "PRODUCT SUB GROUP/PRODUCT" => ReturnProductListWithSubGroup(dtProduct),
            "TAXABLE/NON TAXABLE" => ReturnProductListWithProductTaxable(dtProduct),
            "PRODUCT TYPE" => ReturnProductListWithProductType(dtProduct),
            _ => ReturnProductList(dtProduct)
        };
        return dtReport;
    }

    #endregion --------------- LIST OF MASTER ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public VmStockReports GetReports { get; set; } = new();

    #endregion --------------- OBJECT ---------------
}