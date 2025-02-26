using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System.Windows.Forms;
using System;
using System.Linq;
using DevExpress.XtraEditors;
using MrDAL.Core.Utils;

namespace MrDAL.Core.Extensions;

public static class BooleanExt
{
    // RETURN VALUE IN BOOLEAN

    #region --------------- RETURN VALUE IN BOOLEAN ---------------
    private static bool CheckDateRange(string adDate, string dateType)
    {
        var eng = dateType.Equals("M") ? adDate.GetEnglishDate() : adDate.GetDateTime().ToShortDateString();
        if (!eng.Contains("/")) eng = eng.Replace("-", "/");
        var split = eng.Split('/', ' ');
        eng = Convert.ToString(split[2] + "/" + split[1] + "/" + split[0]);
        var iEng = eng.GetDateTime();
        try
        {
            return iEng >= ObjGlobal.CfStartAdDate && iEng <= ObjGlobal.CfEndAdDate;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    
    public static bool IsValidDateRange(this MaskedTextBox box, string dateType)
    {
        return IsValidDateRange(box.Text, dateType);
    }
    
    public static bool IsValidDateRange(this string adDate, string dateType)
    {
        return CheckDateRange(adDate, dateType);
    }
    
    public static bool IsValidDate(this string adDate)
    {
        if (adDate.IsBlankOrEmpty()) return false;
        var dateValue = adDate.GetDateTime();
        var eng = dateValue.ToShortDateString();
        var split = eng.Split('/', ' ');
        eng = Convert.ToString($"{split[2]}/{split[1]}/{split[0]}");
        var iEng = DateTime.Parse(Convert.ToString(eng));
        try
        {
            return iEng >= ObjGlobal.CfStartAdDate && iEng <= ObjGlobal.CfEndAdDate;
        }
        catch (Exception ex)
        {
            var exMessage = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    
    public static bool IsDateExits(this string adDate, string dateType)
    {
        string eng;
        string[] split;
        string query;

        if (dateType.Equals("D"))
        {
            eng = adDate;
            if (!eng.Contains("/")) eng = eng.Replace("-", "/");
            split = eng.Split('/', ' ');
            if (split[0].GetInt() > 31) return false;
            if (split[1].GetInt() > 12) return false;
            eng = Convert.ToString($"{split[2]}-{split[1]}-{split[0]}");
            query = $"SELECT AD_DATE FROM AMS.DATEMITI where AD_DATE='{eng}'";
        }
        else
        {
            eng = adDate;
            if (!adDate.Contains("/")) eng = adDate.Replace("-", "/");
            split = eng.Split('/', ' ');
            if (split[0].GetInt() > 32) return false;
            if (split[1].GetInt() > 12) return false;
            query = $"SELECT AD_DATE FROM AMS.DATEMITI where BS_DateDMY='{eng}'";
        }

        try
        {
            var dt = SqlExtensions.ExecuteDataSet(query).Tables[0];
            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    
    public static bool IsDateExits(this MaskedTextBox adDate, string dateType)
    {
        return IsDateExits(adDate.Text, dateType);
    }
    
    public static bool GetBool(this object value)
    {
        if (value == null) return false;
        bool.TryParse(value.ToString(), out var result);
        return result;
    }
    
    public static bool GetBool(this string value)
    {
        if (value == null) return false;
        bool.TryParse(value, out var result);
        return result;
    }
    
    public static bool IsBlankOrEmpty(this string value)
    {
        return value?.Replace("'", "''").Trim().Length is 0;
    }
    
    public static bool IsBlankOrEmpty(this TextBox value)
    {
        return value?.Text.Replace("'", "''").Trim().Length is 0;
    }
    
    public static bool IsBlankOrEmpty(this TextEdit value)
    {
        return value?.Text.Replace("'", "''").Trim().Length is 0;
    }
    
    public static bool ValidControl(this System.Windows.Forms.Control value, System.Windows.Forms.Control activeControl)
    {
        string[] action =
        [
            "BtnExit",
            "BtnCancel"
        ];
        if (value is MaskedTextBox box)
        {
            if (!box.MaskCompleted && activeControl.Name != box.Name && !action.Contains(activeControl.Name))
                return true;
        }
        else if (activeControl.Name != value.Name && !action.Contains(activeControl.Name))
        {
            return true;
        }

        return false;
    }
    
    public static bool IsBlankOrEmpty(this object value)
    {
        return value?.ToString().Replace("'", "''").Trim().Length is 0;
    }
    
    public static bool IsValueExits(this string value, string actionTag)
    {
        string[] action =
        {
            "DELETE", "REVERSE"
        };
        if (action.Contains(actionTag)) return true;
        return value is not null && value.Trim().Replace("'", "''").Length > 0;
    }
    
    public static bool IsValueExits(this string value)
    {
        return value is not null && value.Trim().Replace("'", "''").Length > 0;
    }
   
    public static bool IsContains(this string value)
    {
        string[] tagAction = { "DELETE", "REVERSE" };
        return value is not null && tagAction.Contains(value);
    }
    
    public static bool ActionValid(this string value)
    {
        string[] tagAction = { "DELETE", "REVERSE" };
        return value is not null && !tagAction.Contains(value);
    }
    
    public static bool IsValueExits(this object value)
    {
        return value is not null && value.GetTrimReplace().Length > 0;
    }
    
    public static bool IsGuidExits(this Guid value)
    {
        if (value == Guid.Empty)
        {
            return false;
        }
        return value.GetHashCode() > 0;
    }
    
    public static bool IsGuidExits(this Guid? value)
    {
        if (value == Guid.Empty || value == null)
        {
            return false;
        }
        return value.GetHashCode() > 0;
    }
    
    public static bool IsValueExits(this TextBox value)
    {
        return value.Text is not null && value.Text.Replace("'", "''").Trim().Length > 0;
    }
    
    public static bool IsDecimal(this KeyPressEventArgs e, object sender)
    {
        var result = char.IsControl(e.KeyChar) switch
        {
            false when !char.IsDigit(e.KeyChar) && e.KeyChar != '.' => true,
            _ => e.Handled
        } || (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1);
        return result;
    }
    
    public static bool IsPositiveDecimal(this string value, bool includeZero = true)
    {
        var result = decimal.TryParse(value, out var temp);
        if (result && includeZero) return true;
        return result && temp > 0;
    }

    public static bool IsDuplicate(this string value, string filterColumn, string filterId, string actionTag, string module)
    {
        var (table, column, addColumn) = StringExt.GetTableInfo(module);
        if (filterColumn.IsBlankOrEmpty()) filterColumn = column;
        var cmdString = $"SELECT {column} FROM {table} WHERE {filterColumn} = '{value.GetTrimReplace()}' ";
        cmdString += !actionTag.Equals("SAVE") ? $"AND {column} <> '{filterId}'" : " ";
        var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return result is
        {
            Rows.Count: > 0
        };
    }

    public static bool IsDuplicate(this MaskedTextBox value, string filterColumn, long filterId, string actionTag, string module)
    {
        return IsDuplicate(value.Text, filterColumn, filterId.ToString(), actionTag, module);
    }
    public static bool IsDuplicate(this TextBox value, string filterColumn, long filterId, string actionTag, string module)
    {
        return IsDuplicate(value.Text, filterColumn, filterId.ToString(), actionTag, module);
    }
    
    public static bool IsDuplicate(this TextBox value, string filterColumn, int filterId, string actionTag, string module)
    {
        return IsDuplicate(value.Text, filterColumn, filterId.ToString(), actionTag, module);
    }
    
    public static bool IsDuplicate(this TextBox value, string filterColumn, string filterId, string actionTag, string module)
    {
        return IsDuplicate(value.Text, filterColumn, filterId, actionTag, module);
    }
    
    public static bool IsDuplicate(this TextBox value, string module, string actionTag = "SAVE")
    {
        return IsDuplicate(value.Text, module, actionTag);
    }
    
    private static bool IsDuplicate(string value, string module, string actionTag)
    {
        var (table, column, addColumn) = StringExt.GetTableInfo(module);
        var cmdString = $"SELECT {column} FROM {table} WHERE {addColumn} = '{value}' ";
        cmdString += !actionTag.Equals("SAVE") ? $"AND {column} <> '{value}'" : " ";
        var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return result != null && result.Rows.Count > 0;
    }
    
    public static bool IsDocumentNumberingExits(this TextBox taxBox, string module, string actionTag, int id)
    {
        var cmdString =
            $"SELECT TOP 1 DocId FROM AMS.DocumentNumbering WHERE DocDesc ='{taxBox.Text}' AND DocModule = '{module}'";
        cmdString += !actionTag.Equals("SAVE") ? $"AND DocId <> '{id}'" : " ";
        var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return result != null && result.Rows.Count > 0;
    }

    #endregion
}