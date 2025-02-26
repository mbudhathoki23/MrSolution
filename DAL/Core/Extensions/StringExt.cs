using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class StringExt
{
    public static string ReturnMaxPatientId(this TextBox value)
    {
        var (table, column) = ("HOS.PatientMaster", "ShortName");
        var cmdString = $" SELECT ISNULL(MAX(CAST({column} AS BIGINT)), 0)+1 MaxId FROM {table};";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dt.Rows.Count == 0)
            value.Text = DateTime.Now.Year + @"01";
        else
            value.Text = dt.Rows[0]["MaxId"].GetString();
        return dt.RowsCount() > 0 && value.Text.GetInt() > 0 ? value.Text : string.Empty;
    }
    public static string GetSystemDate(this string value)
    {
        DateTime.TryParse(value, out var dateTime);
        return Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
    }
    public static string GetSystemDate(this object value)
    {
        value ??= DateTime.Now;
        DateTime.TryParse(value.ToString(), out var dateTime);
        return Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
    }
    public static string GetSystemDate(this MaskedTextBox value)
    {
        if (!value.MaskCompleted) value.Text = DateTime.Now.GetDateString();
        DateTime.TryParse(value.Text, out var dateTime);
        return Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
    }
    public static string GetLastVoucherDate(this MaskedTextBox value, string module)
    {
        var cmdString = @$"
            SELECT TOP 1 ad.Voucher_Date FROM AMS.AccountDetails ad WHERE ad.Module='{module}' ORDER BY ad.Voucher_Date DESC";
        value.Text = cmdString.GetQueryData().GetDateString();
        if (!value.MaskCompleted) value.Text = DateTime.Now.GetDateString();
        DateTime.TryParse(value.Text, out var dateTime);
        return Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy");
    }
    public static (string table, string columnId, string column) GetTableInfo(string module)
    {
        module = module.ToUpper();
        var table = module switch
        {
            "BRANCH" or "B" => "AMS.Branch",
            "CURRENCY" or "C" => "AMS.Currency",
            "COUNTER" or "CC" => "AMS.Counter",
            "PRODUCT" or "P" => "AMS.Product",
            "BARCODELIST" or "BL" => "AMS.BarcodeList",
            "PRODUCTUNIT" or "PU" => "AMS.ProductUnit",
            "RACK" or "RK" => "AMS.Rack",
            "PRODUCTGROUP" or "PG" => "AMS.ProductGroup",
            "PRODUCTSUBGROUP" or "PSG" => "AMS.ProductSubGroup",
            "ACCOUNTGROUP" or "AG" => "AMS.AccountGroup",
            "ACCOUNTSUBGROUP" or "ASG" => "AMS.AccountSubGroup",
            "GENERALLEDGER" or "GL" => "AMS.GeneralLedger",
            "SUBLEDGER" or "SL" => "AMS.SubLedger",
            "NRMASTER" or "NRM" => "AMS.NR_Master",
            "AREA" or "AR" => "AMS.Area",
            "MAINAREA" or "MA" => "AMS.MainArea",
            "SENIORAGENT" or "SS" => "AMS.SeniorAgent",
            "JUNIORAGENT" or "JS" => "AMS.JuniorAgent",
            "DOCUMENTNUMBERING" or "NUM" => "AMS.DocumentNumbering",
            "DOCUMENTDESIGNPRINT" or "DESIGN" => "AMS.DocumentDesignPrint",
            "MEMBERSHIPSETUP" or "MSP" => "AMS.MemberShipSetup",
            "MEMBERTYPE" or "MT" => "AMS.MemberType",
            "GIFTVOUCHER" or "GV" => "AMS.GiftVoucherList",
            "TABLE" or "T" => "AMS.TableMaster",
            "FLOOR" or "F" => "AMS.Floor",
            "COSTCENTER" or "CS" => "AMS.CostCenter",
            "GODOWN" or "GD" => "AMS.Godown",
            "PATIENT" or "PA" => "HOS.PatientMaster",
            "DOCTORTYPE" or "DRTYPE" => "HOS.DoctorType",
            "BEDMASTER" or "BED" => "HOS.BedMaster",
            "BEDTYPE" or "BTYPE" => "HOS.BedType",
            "WARDMASTER" or "WMASTER" => "HOS.WardMaster",
            "ST_TERM" or "ST" => "AMS.ST_Term",
            "PT_TERM" or "PT" => "AMS.PT_Term",
            "COA" => "AMS.LedgerOpening",
            "POP" => "AMS.ProductOpening",
            "CB" => "AMS.CB_Master",
            "CV" => "AMS.CB_Master",
            "JV" => "AMS.JV_Master",
            "DN" or "CN" => "AMS.Notes_Master",
            "PIN" => "AMS.PIN_Master",
            "PO" => "AMS.PO_Master",
            "GIT" => "AMS.GIT_Master",
            "PC" => "AMS.PC_Master",
            "PCR" => "AMS.PCR_Master",
            "PB" => "AMS.PB_Master",
            "PR" => "AMS.PR_Master",
            "PAB" => "AMS.PAB_Master",
            "PEB" => "AMS.PEB_Master",
            "SQ" => "AMS.SQ_Master",
            "SO" or "RSO" => "AMS.SO_Master",
            "SC" => "AMS.SC_Master",
            "SR" => "AMS.SR_Master",
            "SAB" => "AMS.SAB_Master",
            "SEB" => "AMS.SEB_Master",
            "PDC" => "AMS.PostDateCheque",
            "PBT" => "AMS.PBT_Master",
            "SB" or "POS" or "AVT" or "RSB" => "AMS.SB_Master",
            "SBT" => "AMS.SBT_Master",
            "TSB" => "AMS.Temp_SB_Master",
            "BOM" => "INV.BOM_Master",
            "IBOM" => "INV.Production_Master",
            "SA" => "AMS.STA_Master",
            "MI" => "INV.StockIssue_Master",
            _ => string.Empty
        };
        var columnId = module switch
        {
            "BRANCH" or "B" => "Branch_Id",
            "CURRENCY" or "COUNTER" or "C" or "CC" => "CId",
            "PRODUCTUNIT" or "PU" => "UID",
            "PRODUCT" or "P" => "PID",
            "BARCODELIST" or "BL" => "Barcode",
            "PRODUCTGROUP" or "PG" => "PGrpId",
            "PRODUCTSUBGROUP" or "PSG" => "PSubGrpId",
            "RACK" or "RK" => "RID",
            "ACCOUNTGROUP" or "AG" => "GrpId",
            "ACCOUNTSUBGROUP" or "ASG" => "SubGrpId",
            "GENERALLEDGER" or "GL" => "GLID",
            "SUBLEDGER" or "SL" => "SLId",
            "NRMASTER" or "NRM" => "NRID",
            "AREA" or "AR" => "AreaId",
            "MAINAREA" or "MA" => "MAreaId",
            "SENIORAGENT" or "SS" => "SAgentId",
            "JUNIORAGENT" or "JS" => "AgentId",
            "DOCUMENTNUMBERING" or "NUM" => "DocId",
            "DOCUMENTDESIGNPRINT" or "DESIGN" => "DDP_Id",
            "MEMBERSHIPSETUP" or "MSP" => "MShipId",
            "MEMBERTYPE" or "MT" => "MemberTypeId",
            "GIFTVOUCHER" or "GV" => "VoucherId ",
            "TABLE" or "T" => "TableId",
            "FLOOR" or "F" => "FloorId",
            "COSTCENTER" or "CS" => "CCId",
            "GODOWN" or "GD" => "GID",
            "PATIENT" or "PA" => "PaitentId",
            "DOCTORTYPE" or "DRTYPE" => "DtID",
            "BEDMASTER" or "BED" => "BID",
            "BEDTYPE" or "BTYPE" => "HOS.BedType",
            "WARDMASTER" or "WMASTER" => "HOS.WardMaster",
            "ST_TERM" or "ST" => "ST_ID ",
            "PT_TERM" or "PT" => "PT_ID ",
            "COA" => "Voucher_No ",
            "POP" => "Voucher_No ",
            "CB" or "CV" or "JV" or "DN" or "CN" => "Voucher_No ",
            "PIN" => "PIN_Invoice",
            "PO" => "PO_Invoice",
            "GIT" => "GIT_Invoice",
            "PC" => "PC_Invoice",
            "PCR" => "PCR_Invoice",
            "PB" => "PB_Invoice",
            "PR" => "PR_Invoice",
            "PAB" => "PAB_Invoice",
            "PEB" => "PEB_Invoice",
            "SQ" => "SQ_Invoice",
            "SO" or "RSO" => "SO_Invoice",
            "SC" => "SC_Invoice",
            "SB" or "POS" or "AVT" or "RSB" or "TSB" => "SB_Invoice",
            "SR" => "SR_Invoice",
            "SAB" => "SAB_Invoice",
            "SEB" => "SEB_Invoice",
            "PDC" or "BOM" or "IBOM" => "VoucherNo",
            "SA" => "StockAdjust_No",
            "MI" => "VoucherNo",
            _ => string.Empty
        };
        var column = module switch
        {
            "BRANCH" or "B" => "Branch_Name",
            "CURRENCY" or "COUNTER" or "C" or "CC" => "CName",
            "PRODUCTUNIT" or "PU" => "UnitName",
            "PRODUCT" or "P" => "PName",
            "BARCODELIST" or "BL" => "ProductId",
            "PRODUCTGROUP" or "ACCOUNTGROUP" or "PG" or "AG" => "GrpName",
            "PRODUCTSUBGROUP" or "ACCOUNTSUBGROUP" or "PSG" or "ASG" => "SubGrpName",
            "GENERALLEDGER" or "GL" => "GLName",
            "SUBLEDGER" or "SL" => "SLName",
            "NRMASTER" or "NRM" => "NRDESC",
            "AREA" or "AR" => "AreaName",
            "MAINAREA" or "MA" => "MAreaName",
            "SENIORAGENT" or "SS" => "SAgent",
            "JUNIORAGENT" or "JS" => "AgentName",
            "DOCUMENTNUMBERING" or "NUM" => "DocDesc",
            "DOCUMENTDESIGNPRINT" or "DESIGN" => "DesignerPaper_Name",
            "MEMBERSHIPSETUP" or "MSP" => "MShipDesc",
            "MEMBERTYPE" or "MT" => "MemberDesc",
            "GIFTVOUCHER" or "GV" => "CardNo",
            "TABLE" or "T" => "TableName",
            "FLOOR" or "F" => "Description",
            "COSTCENTER" or "CS" => "CCName",
            "GODOWN" or "GD" => "GName",
            "PATIENT" or "PA" => "PaitentDesc",
            "DOCTORTYPE" or "DRTYPE" => "DrTypeDesc",
            "DOCTOR" or "DR" => "DrName",
            "HOSDEPARTMENT" or "HOSDEP" => "DName",
            "BEDMASTER" or "BED" => "BID",
            "BEDTYPE" or "BTYPE" => "HOS.BedType",
            "WARDMASTER" or "WMASTER" => "HOS.WardMaster",
            "ST_TERM" or "ST" => "ST_Name ",
            "PT_TERM" or "PT" => "PT_Name ",
            "COA" => "Voucher_No ",
            "POP" => "Voucher_No ",
            "CB" or "CV" or "JV" or "DN" or "CN" => "Voucher_No ",
            "PIN" => "PIN_Invoice",
            "PO" => "PO_Invoice",
            "GIT" => "GIT_Invoice",
            "PC" => "PC_Invoice",
            "PCR" => "PCR_Invoice",
            "PB" => "PB_Invoice",
            "PR" => "PR_Invoice",
            "PAB" => "PAB_Invoice",
            "PEB" => "PEB_Invoice",
            "SQ" => "SQ_Invoice",
            "SO" or "RSO" => "SO_Invoice",
            "SC" => "SC_Invoice",
            "SB" or "POS" or "AVT" or "RSB" => "SB_Invoice",
            "SR" => "SR_Invoice",
            "SAB" => "SAB_Invoice",
            "SEB" => "SEB_Invoice",
            "PDC" or "BOM" or "IBOM" => "VoucherNo",
            "SA" => "StockAdjust_No",
            _ => string.Empty
        };
        return (table, columnId, column);
    }
    public static string GetUpper(this TextBox value)
    {
        return GetUpper(value.Text);
    }
    public static string GetUpper(this string value)
    {
        return value == null ? string.Empty : value.Trim().Replace("'", "''").ToUpper();
    }
    public static string GetUpper(this ComboBox value)
    {
        return GetUpper(value.Text);
    }
    public static string GetUpper(this object value)
    {
        return value == null ? string.Empty : value.ToString().Trim().Replace("'", "''").ToUpper();
    }
    public static string GetProperCase(this Label value)
    {
        return value.Text == null ? string.Empty : GetProperCase(value.Text);
    }
    public static string GetProperCase(this ComboBox value)
    {
        return value.Text == null ? string.Empty : GetProperCase(value.Text);
    }
    public static string GetProperCase(this TextBox value)
    {
        return value.Text == null ? string.Empty : GetProperCase(value.Text);
    }
    public static string GetProperCase(this object value)
    {
        return value == null ? string.Empty : GetProperCase(value.ToString());
    }
    public static string GetProperCase(this string value)
    {
        var result = string.Empty;
        if (value != null) result = Regex.Replace(value.ToLower(), @"\b[a-z]", m => m.Value.ToUpper());

        return result;
    }
    public static string GetString(this string value)
    {
        return value == null ? string.Empty : value.Trim().Replace("'", "''");
    }
    public static string GetString(this TextBox value)
    {
        return GetString(value.Text);
    }
    public static string GetString(this object value)
    {
        return value == null ? string.Empty : GetString(value.GetTrimReplace());
    }
    public static string GetIntString(this object value)
    {
        if (value == null) return string.Empty;
        var result = int.TryParse(value.ToString(), out var intResult);
        return result ? intResult.ToString() : string.Empty;
    }
    public static string GetDateString(this object value)
    {
        if (value is null || value.IsBlankOrEmpty()) value = DateTime.Now;
        var mskDate = Convert.ToDateTime(value).ToString("dd/MM/yyyy");
        return mskDate;
    }
    public static string GetIrdDateString(this object value)
    {
        if (value is null) return DateTime.Now.GetDateString();
        var date = value.ToString();
        if (date.Length > 0 && !string.IsNullOrEmpty(date) && date != "  /  /")
        {
            var split = date.Split('/', ' ');
            date = Convert.ToString($"{split[2]}.{split[1]}.{split[0]}");
        }

        return date;
    }
    public static string SplitCamelCase(this Enum value)
    {
        var input = value.ToString();
        return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return null;
        var field = type.GetField(name);
        if (field == null) return null;
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
            return attr.Description;
        return value.ToString();
    }
    public static string GetTrimReplace(this string value)
    {
        return value?.Trim().Replace("'", "''");
    }
    public static string GetTrimReplace(this object value)
    {
        return value?.ToString().Trim().Replace("'", "''");
    }
    public static string GetTrimReplace(this TextBox value)
    {
        return GetTrimReplace(value.Text);
    }
    public static string GetTrimReplace(this StringBuilder value)
    {
        return GetTrimReplace(value.ToString());
    }
    public static string GetTrimApostrophe(this string value)
    {
        var input = value;
        input = input.Replace("''", "'");
        return input;
    }
    public static string GetTrimApostrophe(this object value)
    {
        if (value == null) return string.Empty;
        return GetTrimApostrophe(value.ToString());
    }
    public static string GetTrimApostrophe(this TextBox value)
    {
        return GetTrimApostrophe(value.Text);
    }
    public static string GetTrimApostrophe(this Label value)
    {
        return GetTrimApostrophe(value.Text);
    }
    public static string GetTrimApostrophe(this ComboBox value)
    {
        return GetTrimApostrophe(value.Text);
    }
    public static string GetDecimalQtyString(this string value)
    {
        var result = decimal.TryParse(value, out var decimalValue);
        return result switch
        {
            true => decimalValue.ToString(ObjGlobal.SysQtyFormat),
            _ => string.Empty
        };
    }
    public static string GetDecimalQtyString(this object value, bool isQtyRequired = false)
    {
        var resultDecimal = value.GetDecimal();
        var result = resultDecimal != 0 ? resultDecimal.ToString(ObjGlobal.SysQtyFormat) : string.Empty;

        return result;
    }
    public static string GetDecimalQtyString(this TextBox value, bool isQtyRequired = false)
    {
        decimal.TryParse(value.Text, out var result);
        return Math.Abs(result) switch
        {
            0 when isQtyRequired => 1.ToString(ObjGlobal.SysQtyFormat),
            _ => Math.Abs(result) > 0 ? result.ToString(ObjGlobal.SysQtyFormat) : string.Empty
        };
    }
    public static string GetDecimalQtyString(this Label value)
    {
        decimal.TryParse(value.Text, out var decimalQty);
        return Math.Abs(decimalQty) > 0 ? decimalQty.ToString(ObjGlobal.SysQtyFormat) : string.Empty;
    }
    public static string GetDecimalComma(this object value)
    {
        return value switch
        {
            null => string.Empty,
            "NIL" => value.ToString(),
            _ => value.ToString().GetDecimalComma()
        };
    }
    public static string GetRateComma(this object value)
    {
        return value switch
        {
            null => string.Empty,
            "NIL" => value.ToString(),
            _ => value.ToString().GetRateComma()
        };
    }
    public static string GetDecimalComma(this string value)
    {
        decimal.TryParse(value, out var result);
        return Math.Abs(result) > 0 ? result.ToString(ObjGlobal.SysAmountCommaFormat) : string.Empty;
    }
    public static string GetRateComma(this string value)
    {
        decimal.TryParse(value, out var result);
        return Math.Abs(result) > 0 ? result.ToString(ObjGlobal.SysRateCommaFormat) : string.Empty;
    }
    public static string GetDecimalComma(this TextBox value)
    {
        var result = decimal.TryParse(value.Text, out var decimalValue);
        return result ? decimalValue.ToString(ObjGlobal.SysAmountCommaFormat) : string.Empty;
    }
    public static string GetDecimalComma(this Label value)
    {
        var result = decimal.TryParse(value.Text, out var decimalValue);
        return result ? decimalValue.ToString(ObjGlobal.SysAmountCommaFormat) : string.Empty;
    }
    public static string GetDecimalString(this string value, bool isCurrency = false)
    {
        if (value is null) return isCurrency ? 1.ToString(ObjGlobal.SysCurrencyFormat) : string.Empty;
        decimal.TryParse(value, out var result);
        return Math.Abs(result) > 0
            ? result.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysAmountFormat)
            : string.Empty;
    }
    public static string GetRateDecimalString(this Label value, bool isCurrency = false)
    {
        if (value == null) return isCurrency ? 1.ToString(ObjGlobal.SysCurrencyFormat) : string.Empty;
        decimal.TryParse(value.Text, out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysRateFormat)
            : isCurrency
                ? 1.ToString(ObjGlobal.SysCurrencyFormat)
                : string.Empty;
    }
    public static string GetRateDecimalString(this TextBox value, bool isCurrency = false)
    {
        if (value == null) return isCurrency ? 1.ToString(ObjGlobal.SysCurrencyFormat) : string.Empty;
        decimal.TryParse(value.Text, out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysRateFormat)
            : isCurrency
                ? 1.ToString(ObjGlobal.SysCurrencyFormat)
                : string.Empty;
    }
    public static string GetRateDecimalString(this object value, bool isCurrency = false)
    {
        if (value == null) return isCurrency ? 1.ToString(ObjGlobal.SysCurrencyFormat) : string.Empty;
        decimal.TryParse(value.ToString(), out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysRateFormat)
            : isCurrency
                ? 1.ToString(ObjGlobal.SysCurrencyFormat)
                : string.Empty;
    }
    public static string GetDecimalString(this object value, bool isCurrency = false)
    {
        if (value == null) return isCurrency ? 1.ToString(ObjGlobal.SysCurrencyFormat) : string.Empty;
        decimal.TryParse(value.ToString(), out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysAmountFormat)
            : isCurrency
                ? 1.ToString(ObjGlobal.SysCurrencyFormat)
                : string.Empty;
    }
    public static string GetDecimalString(this TextBox value, bool isCurrency = false)
    {
        if (value.Text is null) return string.Empty;
        decimal.TryParse(value.Text, out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysAmountFormat)
            : string.Empty;
    }
    public static string GetDecimalString(this Label value, bool isCurrency = false)
    {
        if (value.Text is null) return string.Empty;
        decimal.TryParse(value.Text, out var decimalValue);
        return Math.Abs(decimalValue) > 0
            ? decimalValue.ToString(isCurrency ? ObjGlobal.SysCurrencyFormat : ObjGlobal.SysAmountFormat)
            : string.Empty;
    }
    public static string GetEnglishDate(this object date)
    {
        if (date.IsValueExits())
        {
            var query = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{date}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(query).Tables[0];
            if (dtDate.Rows.Count != 0)
            {
                date = dtDate.Rows[0]["AD_Date"].GetDateString();
            }
        }
        else
        {
            date = DateTime.Now.GetDateString();
        }

        return date.ToString();
    }
    public static string GetEnglishDate(this string date)
    {
        if (date.IsValueExits())
        {
            if (!date.Contains("/")) date = date.Replace("-", "/");
            var query = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{date}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(query).Tables[0];
            if (dtDate.Rows.Count != 0)
            {
                date = dtDate.Rows[0]["AD_Date"].GetDateString();
            }
        }
        else
        {
            date = DateTime.Now.GetDateString();
        }

        return date;
    }
    public static string GetEnglishDate(this MaskedTextBox date)
    {
        if (date.MaskCompleted)
        {
            var query = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{date.Text}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(query).Tables[0];
            if (dtDate.Rows.Count != 0)
            {
                date.Text = dtDate.Rows[0]["AD_Date"].GetDateString();
            }
        }
        else
        {
            date.Text = DateTime.Now.GetDateString();
        }

        return date.Text;
    }

    public static string GetEnglishDate(this MaskedTextBox date, string miti)
    {
        if (date.MaskCompleted)
        {
            var query = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{miti}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(query).Tables[0];
            if (dtDate.Rows.Count != 0) date.Text = dtDate.Rows[0]["AD_Date"].GetDateString();
        }
        else
        {
            date.Text = DateTime.Now.GetDateString();
        }

        return date.Text;
    }

    public static string GetEnglishDate(this object date, string miti)
    {
        if (miti.IsValueExits())
        {
            var query = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{miti}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(query).Tables[0];
            if (dtDate.Rows.Count != 0)
            {
                date = dtDate.Rows[0]["AD_Date"].GetDateString();
            }
        }
        else
        {
            date = DateTime.Now.GetDateString();
        }

        return date.ToString();
    }

    public static string GetQueryMasterData(this string query)
    {
        try
        {
            if (query.IsBlankOrEmpty())
            {
                query = "SELECT 1";
            }
            var table = SqlExtensions.ExecuteDataSetOnMaster(query).Tables[0];
            return table?.Rows.Count > 0 ? table.Rows[0][0].ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return string.Empty;
        }
    }

    public static string GetQueryData(this string query)
    {
        try
        {
            if (query.IsBlankOrEmpty())
            {
                query = "SELECT 1";
            }
            var table = SqlExtensions.ExecuteDataSet(query).Tables[0];
            return table?.Rows.Count > 0 ? table.Rows[0][0].ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return string.Empty;
        }
    }

    public static string GetNepaliDate(this object date)
    {
        date ??= DateTime.Now;
        date = date.ToString().GetNepaliDate();
        return date.GetString();
    }

    public static string GetNepaliDate(this string date)
    {
        if (!date.IsValueExits()) return date;

        var sDateText = Convert.ToDateTime(date);
        var nDate = sDateText.ToString("yyyy-MM-dd");
        var cmdString = $"Select BS_DateDMY from AMS.DateMiti Where AD_Date='{nDate}' ";
        var dtDate = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtDate.Rows.Count != 0)
        {
            date = dtDate.Rows[0]["BS_DateDMY"].ToString();
        }
        return date;
    }

    public static string GetNepaliDate(this string date, string engDate)
    {
        if (engDate.IsBlankOrEmpty())
        {
            engDate = DateTime.Now.GetDateString();
        }
        var sDateText = Convert.ToDateTime(engDate);
        var nDate = sDateText.ToString("yyyy-MM-dd");
        var cmdString = $"Select BS_DateDMY from AMS.DateMiti Where AD_Date='{nDate}' ";
        var dtDate = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtDate.Rows.Count != 0)
        {
            date = dtDate.Rows[0]["BS_DateDMY"].ToString();
        }
        return date;
    }

    public static string GetNepaliDate(this MaskedTextBox date, string engDate)
    {
        if (engDate.IsBlankOrEmpty())
        {
            engDate = DateTime.Now.GetDateString();
        }
        var sDateText = Convert.ToDateTime(engDate);
        var nDate = sDateText.ToString("yyyy-MM-dd");
        var cmdString = $"Select BS_DateDMY from AMS.DateMiti Where AD_Date='{nDate}' ";
        var dtDate = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtDate.Rows.Count != 0)
        {
            date.Text = dtDate.Rows[0]["BS_DateDMY"].ToString();
        }
        return date.Text;
    }

    public static string NullableTrimmedText(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public static string GetCurrentVoucherNo(this TextBox txtVoucherNo, string module, string schemaDesc)
    {
        txtVoucherNo.Text = GetCurrentVoucherNo(txtVoucherNo.Text, module, schemaDesc);
        return txtVoucherNo.Text;
    }

    public static string GetCurrentVoucherNo(this string txtVoucherNo, string module, string schemaDesc)
    {
        var (table, column, _) = StringExt.GetTableInfo(module);
        var prifix = string.Empty;
        var suffix = string.Empty;
        var cmdString = $@" 
        SELECT * FROM AMS.DocumentNumbering 
        WHERE DocDesc = '{schemaDesc}' AND DocModule = '{module}' AND FiscalYearId={ObjGlobal.SysFiscalYearId} AND  (DocBranch ='{ObjGlobal.SysBranchId}' OR DocBranch IS NULL); ";
        var rsScheme = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (rsScheme.Rows.Count <= 0)
        {
            return txtVoucherNo;
        }
        if (rsScheme.Rows[0]["DocPrefix"].IsValueExits())
        {
            prifix = rsScheme.Rows[0]["DocPrefix"].GetString();
        }
        if (rsScheme.Rows[0]["DocSufix"].IsValueExits())
        {
            suffix = rsScheme.Rows[0]["DocSufix"].GetString();
        }
        var blankCharLength = rsScheme.Rows[0]["DocBodyLength"].GetInt();
        var cmdNumber = string.Empty;
        if (prifix.IsValueExits() && suffix.IsValueExits())
        {
            cmdNumber += $@" 
            SELECT ISNULL(MAX(CAST(REPLACE(REPLACE({column}, '{prifix}', ''), '{suffix}', '') AS BIGINT)), 0)+1 VoucherNo
            FROM   ( SELECT   TOP(1) TRY_CAST(ISNULL(REPLACE(REPLACE({column}, '{prifix}', ''), '{suffix}', ''), 0) AS BIGINT) {column}
                     FROM     {table}
                     WHERE    {column} LIKE '{prifix}%{suffix}'
                     ORDER BY TRY_CAST(ISNULL(REPLACE(REPLACE( {column}, '{prifix}', ''), '{suffix}', ''), 0) AS BIGINT) DESC) doc;";
        }
        else if (prifix.IsValueExits() && !suffix.IsValueExits())
        {
            cmdNumber += $@"
            SELECT ISNULL(MAX(CAST(REPLACE({column},'{prifix.Trim()}','') AS DECIMAL)),0) + 1 VoucherNo 
            FROM ( SELECT   TOP(1) CAST(ISNULL(REPLACE({column}, '{prifix}', ''), 0) AS BIGINT) {column}
                     FROM     {table}
                     WHERE    {column} LIKE '{prifix}%'
                     ORDER BY CAST(ISNULL(REPLACE({column}, '{prifix}', ''),0) AS BIGINT) DESC) doc; ";
        }
        else if (!prifix.IsValueExits() && suffix.IsValueExits())
        {
            cmdNumber += $@"
            SELECT ISNULL(MAX(CAST(REPLACE({column}, '{suffix}', '') AS BIGINT)), 0)+1 VoucherNo
            FROM   ( SELECT   TOP(1) TRY_CAST(ISNULL(REPLACE({column},'{suffix}', ''), 0) AS BIGINT) {column}
                     FROM     {table}
                     WHERE    {column} LIKE '%{suffix}'
                     ORDER BY TRY_CAST(ISNULL(REPLACE({column},'{suffix}', ''), 0) AS BIGINT) DESC) doc; ";
        }
        else if (!prifix.IsValueExits() && suffix.IsValueExits())
        {
            cmdNumber += $@"
            SELECT MAX(CAST(ISNULL({column},0)) +1 VoucherNo
            FROM   ( SELECT   TOP(1) TRY_CAST(ISNULL({column},0) AS BIGINT) {column}
                     FROM     {table}
                     ORDER BY TRY_CAST(ISNULL({column},0) AS BIGINT) DESC) doc; ";
        }
        cmdNumber += $" ";
        var dtCurrentNo = SqlExtensions.ExecuteDataSet(cmdNumber).Tables[0];

        var voucherLength = dtCurrentNo.Rows[0]["VoucherNo"].GetIntString().Length;
        if (blankCharLength > voucherLength)
        {
            var diff = blankCharLength - voucherLength;
            blankCharLength = diff;
        }
        else
        {
            blankCharLength = 0;
        }

        var blankCharArray = Enumerable.Repeat(0, blankCharLength);

        var voucherNo = string.Join(string.Empty, blankCharArray) + dtCurrentNo.Rows[0]["VoucherNo"];
        txtVoucherNo = prifix.Trim() + voucherNo + suffix.Trim();
        return txtVoucherNo;
    }

    public static string IsClear(this Label val)
    {
        return IsClear(val.Text);
    }

    public static string IsClear(this string val)
    {
        val = string.Empty;
        return val;
    }

    public static string IsClear(this object val)
    {
        return IsClear(val == null ? string.Empty : val.ToString());
    }

    public static string IsClear(this ToolStripStatusLabel value)
    {
        return IsClear(value.Text);
    }

    public static string GetNumberInWords(this Label numberInWords, string val)
    {
        return numberInWords.Text = ClsMoneyConversion.MoneyConversion(val);
    }

    public static string GenerateShortName(this TextBox description, string module, string filterColumn = "")
    {
        var shortName = new TextBox();
        var (table, column, _) = StringExt.GetTableInfo(module);
        var blankCharLength = 5;
        if (filterColumn.IsBlankOrEmpty()) filterColumn = column;
        var sortOn = description.TextLength switch
        {
            1 => description.Text.GetTrimReplace().Substring(0, 1).GetUpper(),
            _ => description.Text.GetTrimReplace().Substring(0, 2).GetUpper()
        };
        var cmdNumber = $@"
            SELECT ISNULL(MAX(CAST( AMS.GetNumericValue({column}) AS BIGINT)),0) + 1 Number
            FROM {table} WHERE {filterColumn} LIKE '%{sortOn}%'";
        var dtCurrentNo = SqlExtensions.ExecuteDataSet(cmdNumber).Tables[0];
        var voucherLength = dtCurrentNo.Rows[0]["Number"].ToString().Length;
        if (blankCharLength > voucherLength)
        {
            blankCharLength -= voucherLength;
            var blankCharArray = Enumerable.Repeat(0, blankCharLength);
            var voucherNo = string.Join(string.Empty, blankCharArray) + dtCurrentNo.Rows[0]["Number"];
            shortName.Text = sortOn + voucherNo;
        }
        else
        {
            shortName.Text = (dtCurrentNo.Rows[0]["Number"].GetLong() + 1).ToString();
        }

        return shortName.Text;
    }

    public static string GenerateShortName(this TextBox shortName, string module, string value, string column = "")
    {
        var (table, filterColumn, _) = StringExt.GetTableInfo(module);
        var blankCharLength = 5;
        if (column.IsValueExits())
        {
            filterColumn = column;
        }
        var sortOn = value.GetTrimReplace().Substring(0, 2).GetUpper();
        var cmdNumber = $@"
            SELECT ISNULL(MAX(CAST( AMS.GetNumericValue({filterColumn}) AS BIGINT)),0) + 1 Number
            FROM {table} WHERE {filterColumn} LIKE '%{sortOn}%'";
        var dtCurrentNo = SqlExtensions.ExecuteDataSet(cmdNumber).Tables[0];
        var voucherLength = dtCurrentNo.Rows[0]["Number"].ToString().Length;
        if (blankCharLength > voucherLength)
        {
            blankCharLength -= voucherLength;
            var blankCharArray = Enumerable.Repeat(0, blankCharLength);
            var voucherNo = string.Join(string.Empty, blankCharArray) + dtCurrentNo.Rows[0]["Number"];
            shortName.Text = sortOn + voucherNo;
        }
        else
        {
            shortName.Text = (dtCurrentNo.Rows[0]["Number"].GetLong() + 1).ToString();
        }

        return shortName.Text;
    }

    public static string GenerateAccountingCode(this TextBox textBox, int groupId)
    {
        long accountCode = 0;
        var cmdString = $@"
        SELECT * FROM AMS.AccountGroup WHERE GrpId={groupId};

        SELECT MAX(AMS.GetNumericValue(gl.ACCode))+1 ACCode
        FROM AMS.GeneralLedger gl
        WHERE gl.GrpId='{groupId}';";

        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString);
        var table1 = dtLedger.Tables[0];
        var table2 = dtLedger.Tables[1];
        if (table1.Rows.Count > 0 || table1.RowsCount() == 0)
        {

            if (table2.Rows.Count > 0)
            {
                accountCode = table2.Rows[0]["ACCode"].ToString().GetLong();
            }

            if (accountCode > 0)
            {
                return textBox.Text = accountCode.ToString();
            }
            else
            {
                if (table1.Rows.Count == 0)
                {
                    return string.Empty;
                }
                var schedule = table1.Rows[0]["Schedule"].GetInt();
                textBox.Text = schedule.ToString().Length is 2
                    ? schedule + @"0001"
                    : schedule + @"00001";
            }

        }
        return textBox.Text;
    }
    public static string GetMaxValue(this string value, string actionTag, string module, string filterColumn)
    {
        try
        {
            var (table, column, addColumn) = StringExt.GetTableInfo(module);
            var cmdString = $"Select MAX(AMS.GetNumericValue({filterColumn})) From {table} WHERE  1=1 ";
            if (!actionTag.Equals("SAVE"))
            {
                cmdString += $" AND {column} like '%{value}%'";
            }
            var dtRecords = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            return dtRecords.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return string.Empty;
        }
    }
    public static string GetMaxValue(this TextBox value, string actionTag, string module, string filterColumn)
    {
        try
        {
            return GetMaxValue(value.Text, actionTag, module, filterColumn);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return string.Empty;
        }
    }
    public static string ConvertToNepali(this object value)
    {
        var ascii = Encoding.GetEncoding(0);
        var japanese = Encoding.GetEncoding(57002);
        var unicodeBytes = ascii.GetBytes(value.ToString());
        var bytes = Encoding.Convert(ascii, japanese, unicodeBytes);
        var chars = new char[japanese.GetCharCount(bytes, 0, bytes.Length)];
        japanese.GetChars(bytes, 0, bytes.Length, chars, 0);
        var convertToNepali = new string(chars);
        return convertToNepali;
    }
    public static string ReverseString(this string str)
    {
        var chars = str.ToCharArray();

        for (var i = 0; i < str.Length / 2; i++)
            (chars[i], chars[str.Length - i - 1]) = (chars[str.Length - i - 1], chars[i]);

        return new string(chars);
    }

}