using DevExpress.XtraEditors;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.UserAccessControl.Models;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using MrDAL.Utility.Licensing;
using MrDAL.Utility.Server;
using MrDAL.Utility.WinForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrDAL.Global.Common;

public class ObjGlobal
{
    // BIND SQL TABLE IN COMBO BOX

    #region ------------------ SQL TABLE ------------------

    public static DataTable BindSqlTableName()
    {
        const string sql = "Select Distinct 'AMS.'+name AS TableName from sysobjects where xtype='u' ";
        return SqlExtensions.ExecuteDataSet(sql).Tables[0];
    }

    #endregion ------------------ SQL TABLE ------------------

    // BACKUP AND RESTORE

    #region ------------------ BACKUP & RESTORE ------------------

    public static int DataBaseBackup(string initialCatalog, string backupPath, bool isCompress = false)
    {
        var isBackup = 0;
        var date = DateTime.Now;
        var backUpFile =
            $"{backupPath}\\{initialCatalog}_{SysBranchName.Replace(" ", "").ToUpper()}_{date.Day.ToString().PadLeft(2, '0')}_{date.Month.ToString().PadLeft(2, '0')}_{date.Year.ToString().PadLeft(2, '0')}.bak";
        try
        {
            if (!isCompress)
            {
                var cmdString = $@"
                    DECLARE @fileName VARCHAR(256);
                    DECLARE @name VARCHAR(256);
                    DECLARE db_cursor CURSOR READ_ONLY FOR
                    SELECT name
                     FROM master.sys.databases
                     WHERE name = '{initialCatalog}'
                    AND    state = 0
                    AND    is_in_standby = 0;
                    OPEN db_cursor;
                    FETCH NEXT FROM db_cursor
                    INTO @name;
                    WHILE @@FETCH_STATUS = 0
                      BEGIN
                        SET @fileName = '{backUpFile}';
                        BACKUP DATABASE @name
                        TO DISK = @fileName;
                        FETCH NEXT FROM db_cursor
                        INTO @name;
                      END;
                    CLOSE db_cursor;
                    DEALLOCATE db_cursor;";
                isBackup = SqlExtensions.ExecuteNonQuery(cmdString);
            }
            else
            {
                var cmdString = $@"
                    DECLARE @fileName VARCHAR(256);
                    DECLARE @name VARCHAR(256);
                    DECLARE db_cursor CURSOR READ_ONLY FOR
                    SELECT name
                     FROM master.sys.databases
                     WHERE name = '{initialCatalog}'
                    AND    state = 0
                    AND    is_in_standby = 0;
                    OPEN db_cursor;
                    FETCH NEXT FROM db_cursor
                    INTO @name;
                    WHILE @@FETCH_STATUS = 0
                      BEGIN
                        SET @fileName = '{backUpFile}';
                        BACKUP DATABASE @name
                        TO DISK = @fileName with COMPRESSION;
                        FETCH NEXT FROM db_cursor
                        INTO @name;
                      END;
                    CLOSE db_cursor;
                    DEALLOCATE db_cursor;";
                isBackup = SqlExtensions.ExecuteNonQuery(cmdString);
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }

        return isBackup;
    }

    #endregion ------------------ BACKUP & RESTORE ------------------

    // GENERATE DOCUMENT NUMBERING

    #region ------------------ Document Numbering ------------------

    public static string ReturnDocumentNumbering(string table, string column, string module, string schemaDesc)
    {
        var pRifix = string.Empty;
        var suffix = string.Empty;
        var txtVoucherNo = new TextBox();
        var cmdString =
            $"Select * from AMS.DocumentNumbering where DocDesc = '{schemaDesc}' And DocModule = '{module}' and FiscalYearId={SysFiscalYearId} and  DocBranch='{SysBranchId}' OR DocBranch IS NULL ";
        var rsScheme = SqlExtensions.ExecuteDataSet(cmdString)
            .Tables[0];
        if (rsScheme.Rows.Count <= 0)
        {
            return txtVoucherNo.Text;
        }

        pRifix = string.IsNullOrEmpty(rsScheme.Rows[0]["DocPrefix"].ToString()) switch
        {
            false => rsScheme.Rows[0]["DocPrefix"].ToString(),
            _ => pRifix
        };
        pRifix = pRifix.GetString();
        suffix = string.IsNullOrEmpty(rsScheme.Rows[0]["DocSufix"].ToString()) switch
        {
            false => rsScheme.Rows[0]["DocSufix"].ToString(),
            _ => suffix
        };
        suffix = suffix.GetString();
        var blankCharLength = rsScheme.Rows[0]["DocBodyLength"].GetInt();
        cmdString =
            $"SELECT  ISNULL(MAX(CAST(REPLACE(REPLACE({column},'{pRifix}',''),'{suffix}','') AS DECIMAL)),0) + 1 VoucherNo FROM {table} WHERE {column} LIKE '{pRifix}%{suffix}'";
        var dtCurrentNo = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];

        var voucherLength = dtCurrentNo.Rows[0]["VoucherNo"].ToString().Length;
        blankCharLength -= voucherLength;
        var blankCharArray = Enumerable.Repeat(0, blankCharLength);

        var voucherNo = string.Join(string.Empty, blankCharArray) + dtCurrentNo.Rows[0]["VoucherNo"];
        txtVoucherNo.Text = pRifix + voucherNo + suffix;
        return txtVoucherNo.Text;
    }

    #endregion ------------------ Document Numbering ------------------

    // BIND PAPER SIZE

    #region ------------------ Bind Paper Size ------------------

    public static void BindPaperSize(ComboBox paperSize, string station, string invoiceType = null)
    {
        var table = SqlExtensions
            .ExecuteDataSet($"SELECT * FROM ams.DllPrintDesigList dpdl WHERE dpdl.Module='{station}'").Tables[0];
        if (table.Rows.Count <= 0)
        {
            return;
        }

        paperSize.DataSource = table;
        paperSize.DisplayMember = "DesignDesc";
        paperSize.ValueMember = "Module";
    }

    #endregion ------------------ Bind Paper Size ------------------

    // GLOBAL METHOD FOR FORM

    #region ------------------ METHOD FOR FORM ------------------

    public static void BindPeriodicDate(MaskedTextBox fromTextBox, MaskedTextBox toTextBox)
    {
        if (SysDateType == "M")
        {
            fromTextBox.Text = CfStartBsDate;
            toTextBox.Text = CfEndBsDate;
        }
        else
        {
            fromTextBox.Text = CfStartAdDate.GetDateString();
            toTextBox.Text = CfEndAdDate.GetDateString();
        }
    }

    public static void LoadDate(string sType, int index, MaskedTextBox mskFrom, MaskedTextBox mskToDate)
    {
        var startAdDate = DateTime.Now;
        var endAdDate = DateTime.Now;
        switch (index)
        {
            case 1:
                {
                    startAdDate = DateTime.Now;
                    endAdDate = DateTime.Now;
                    break;
                }
            case 2:
                {
                    startAdDate = DateTime.Now.AddDays(-1);
                    endAdDate = DateTime.Now.AddDays(-1);
                    break;
                }
            case 3:
                {
                    startAdDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                    endAdDate = DateTime.Now.AddDays(6 - (int)DateTime.Now.DayOfWeek);
                    break;
                }
            case 4:
                {
                    endAdDate = DateTime.Now.AddDays(-1 - (int)DateTime.Now.DayOfWeek);
                    startAdDate = DateTime.Now.AddDays(-7 - (int)DateTime.Now.DayOfWeek);
                    break;
                }
            //Current Month
            case 5 when SysDateType == "D":
                {
                    startAdDate = Convert.ToDateTime(GetConnection.GetQueryData(
                        "SELECT CONVERT(nvarchar(11),DATEADD(Month, DATEDIFF(Month, 0, GETDATE()), 0),105) "));
                    endAdDate = DateTime.Now;
                    break;
                }
            case 5:
                {
                    var cmdString =
                        $" Select Top 1 AD_Date From MASTER.AMS.DateMiti Where BS_Months = (Select Distinct BS_Months from AMS.DateMiti Where AD_Date = '{DateTime.Now:yyyy-MM-dd}'  ) ";
                    cmdString +=
                        $" And Substring(BS_DateDMY,7,4)= (Select Distinct Substring(BS_DateDMY,7,4) MYear from MASTER.AMS.DateMiti Where AD_Date = '{DateTime.Now:yyyy-MM-dd}' ) ";
                    cmdString += " Order By AD_Date asc ";
                    startAdDate = Convert.ToDateTime(GetConnection.GetQueryData(cmdString));
                    cmdString =
                        $" Select Top 1 AD_Date From AMS.DateMiti Where BS_Months = (Select Distinct BS_Months from AMS.DateMiti Where AD_Date = '{DateTime.Now:yyyy-MM-dd}'  ) ";
                    cmdString +=
                        $" And Substring(BS_DateDMY,7,4)= (Select Distinct Substring(BS_DateDMY,7,4) MYear from MASTER.AMS.DateMiti Where AD_Date = '{DateTime.Now:yyyy-MM-dd}' ) ";
                    cmdString += " Order By AD_Date desc ";
                    endAdDate = Convert.ToDateTime(GetConnection.GetQueryData(cmdString));
                    break;
                }
            //Last Month
            case 6 when SysDateType == "D":
                {
                    startAdDate = Convert.ToDateTime(GetConnection.GetQueryData(
                        "Select CONVERT(nvarchar(11), DATEADD(month,-1, DATEADD(Month, DATEDIFF(Month, 0, GETDATE()), 0)),105)"));
                    endAdDate = Convert.ToDateTime(GetConnection.GetQueryData(
                        "Select CONVERT(nvarchar(11),DATEADD(Month, DATEDIFF(Month, 0, GETDATE()), -1),105) "));
                    break;
                }
            case 6:
                {
                    var getDate =
                        $@"SELECT FORMAT ( DATEADD ( MONTH, -1, CONVERT ( DATE, BS_Date )), 'yyyy/MM' ) LastMonth FROM AMS.DateMiti WHERE AD_Date = '{Convert.ToDateTime(DateTime.Now):yyyy-MM-dd}';";
                    var valueDate = GetConnection.GetQueryData(getDate);

                    var cmdString = $@"
                        SELECT TOP 1 AD_Date StartDate FROM AMS.DateMiti WHERE BS_Date LIKE '%{valueDate}%';
                        SELECT TOP 1 AD_Date EndDate FROM AMS.DateMiti WHERE BS_Date LIKE '%{valueDate}%' ORDER BY BS_Date DESC;";
                    var ds = SqlExtensions.ExecuteDataSet(cmdString);
                    if (ds.Tables.Count > 0)
                    {
                        startAdDate = ds.Tables[0].Rows[0]["StartDate"].ToString().GetDateTime();
                        endAdDate = ds.Tables[1].Rows[0]["EndDate"].ToString().GetDateTime();
                    }

                    break;
                }
            case 7:
                {
                    startAdDate = CfStartAdDate;
                    endAdDate = DateTime.Now;
                    break;
                }
            case 8:
                {
                    startAdDate = CfStartAdDate;
                    endAdDate = CfEndAdDate;
                    break;
                }
            case 0:
                {
                    startAdDate = CfStartAdDate;
                    endAdDate = CfEndAdDate;
                    break;
                }
            case 9:
                {
                    startAdDate = CfStartAdDate;
                    endAdDate = DateTime.Now > CfEndAdDate ? CfEndAdDate : DateTime.Now;
                    break;
                }
        }

        if (index == -1)
        {
            if (sType != "R")
            {
                return;
            }

            mskFrom.Text = string.Empty;
            mskToDate.Text = string.Empty;
            mskFrom.Mask = @"99/99/9999";
            mskToDate.Mask = @"99/99/9999";
            mskFrom.Enabled = true;
            mskToDate.Enabled = true;
        }
        else
        {
            if (sType != "R")
            {
                return;
            }

            if (SysDateType == "M")
            {
                mskFrom.Text = mskFrom.GetNepaliDate(startAdDate.GetDateString());
                mskToDate.Text = mskToDate.GetNepaliDate(endAdDate.GetDateString());
            }
            else
            {
                mskFrom.Text = startAdDate.GetDateString();
                mskToDate.Text = endAdDate.GetDateString();
            }

            if (index is -1 or 0)
            {
                mskFrom.Enabled = true;
                mskToDate.Enabled = true;
            }
            else
            {
                if (index is 9)
                {
                    mskFrom.Visible = false;
                    mskFrom.Enabled = false;
                    mskToDate.Enabled = true;
                    mskToDate.Location = index switch
                    {
                        9 => mskFrom.Location,
                        _ => mskToDate.Location
                    };
                }
                else
                {
                    mskFrom.Enabled = index is 0;
                    mskToDate.Enabled = index is 0;
                }
            }
        }
    }

    public static bool ValidDate(string adDate, string dateType)
    {
        string eng;
        string[] split;
        string cmdString;

        if (dateType == "D")
        {
            eng = adDate;
            split = eng.Split('/', ' ');
            if (split[0].GetInt() > 31)
            {
                return false;
            }

            if (split[1].GetInt() > 12)
            {
                return false;
            }

            eng = Convert.ToString($"{split[2]}-{split[1]}-{split[0]}");
            cmdString = $"SELECT AD_DATE FROM AMS.DATEMITI where AD_DATE='{eng}'";
        }
        else
        {
            eng = adDate;
            split = eng.Split('/', ' ');
            if (split[0].GetInt() > 31)
            {
                return false;
            }

            if (split[1].GetInt() > 12)
            {
                return false;
            }

            cmdString = $"SELECT AD_DATE FROM AMS.DATEMITI where BS_DateDMY='{eng}'";
        }

        try
        {
            var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            var exMessage = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    public static bool ValidDateRange(DateTime adDate)

    {
        var eng = "01/01/1753";
        eng = adDate.ToShortDateString();
        var split = "01/01/1753".Split('/', ' ');
        eng = Convert.ToString($"{split[2]}/{split[1]}/{split[0]}");
        var iEng = DateTime.Parse(Convert.ToString($"{split[0]}/{split[1]}/{split[2]}"));
        var dt = new DataTable();
        try
        {
            return iEng >= CfStartAdDate && iEng <= CfEndAdDate;
        }
        catch (Exception ex)
        {
            var Message = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    public static string CurrentDate(string date)
    {
        string[] split;
        split = date.Split('/', ' ');
        date = Convert.ToString($"{split[2]}/{split[1]}/{split[0]}");
        return date;
    }

    public static string SystemCurrentDate(string date)
    {
        var splitStrings = date.Split('/', ' ');
        date = Convert.ToString($"{splitStrings[2]}-{splitStrings[1]}-{splitStrings[0]}");
        return date;
    }

    public static string ReturnNepaliDate(string date)
    {
        if (date == "  /  /")
        {
            return date;
        }

        if (string.IsNullOrEmpty(date))
        {
            return date;
        }

        date = SystemCurrentDate(date);
        var sql = $"Select BS_DateDMY from AMS.DateMiti Where AD_Date='{date}' ";
        date = GetConnection.GetQueryData(sql);
        return date;
    }

    public static string ReturnEnglishDate(string date)
    {
        if (date != "  /  /")
        {
            if (string.IsNullOrEmpty(date))
            {
                return date;
            }

            var cmdString = $"Select AD_Date from AMS.DateMiti Where BS_DateDMY='{date}' ";
            var dtDate = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            date = dtDate.Rows.Count != 0 ? dtDate.Rows[0]["AD_Date"].GetDateString() : DateTime.Now.GetDateString();
        }
        else
        {
            date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        return date;
    }

    public static string EntryDateType(string date)
    {
        if (date.Length > 0 && !string.IsNullOrEmpty(date) && date != "  /  /")
        {
            string[] split;
            if (SysDateType == "M")
            {
                date = ReturnEnglishDate(date);
                if (string.IsNullOrWhiteSpace(date))
                {
                    date = DateTime.Today.ToString("d");
                }

                split = date.Split('/', ' ');
                date = Convert.ToString($"{split[2]}-{split[1]}-{split[0]}");
            }
            else
            {
                split = date.Split('/', ' ');
                date = Convert.ToString($"{split[2]}/{split[1]}/{split[0]}");
            }
        }
        else
        {
            date = DateTime.Today.ToString("d");
        }

        return date;
    }

    public static string FillDateType(string date)
    {
        if (date.Length <= 0 || string.IsNullOrEmpty(date) || date == "  /  /")
        {
            return date;
        }

        if (SysDateType != "M")
        {
            return date;
        }

        date = ReturnNepaliDate(date);
        return date;
    }

    public static string PageLoadDateType(MaskedTextBox textBox)
    {
        textBox.Text = SysDateType == "D"
            ? ReturnNepaliDate(DateTime.Now.ToShortDateString())
            : DateTime.Now.ToShortDateString();
        return textBox.Text;
    }

    public static string FindString(DateTime adDate)
    {
        return $"{adDate.Month.ToString().PadLeft(2, '0')}/{adDate.Day.ToString().PadLeft(2, '0')}/{adDate.Year}";
    }

    public static string GetMacAddress()
    {
        const string cmd = @"
            declare @t table (i uniqueidentifier default newsequentialid(),  m as cast(i as char(36)))
            insert into @t default values;
            select substring(m,25,2) + '-' + substring(m,27,2) + '-' + substring(m,29,2) + '-' + 
            substring(m,31,2) + '-' + substring(m,33,2) + '-' +  substring(m,35,2) AS MacAddress FROM @t ";
        var macAddresses = cmd.GetQueryMasterData().Replace("-", "");
        ObjGlobal.SystemMacAddress = macAddresses;
        return macAddresses;
    }

    public static Task<string> GetSerialNo()
    {
        try
        {
            if (ObjGlobal.SystemSerialNo.IsValueExits())
            {
                return Task.FromResult(ObjGlobal.SystemSerialNo);
            }

            var cpuInfo = string.Empty;
            var mc = new ManagementClass("win32_processor");
            var instances = mc.GetInstances();

            foreach (var o in instances)
            {
                var mo = (ManagementObject)o;
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }

            var serial = string.Empty;
            var mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
            var moc = mos.Get();

            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                serial = mo["SerialNumber"].ToString();
            }

            const string drive = "C";
            var dsk = new ManagementObject($@"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            var volumeSerial = dsk["VolumeSerialNumber"].ToString();

            var uniqueId = cpuInfo + volumeSerial;

            ObjGlobal.SystemSerialNo = uniqueId;
            return Task.FromResult(serial);
        }
        catch (Exception e)
        {
            ObjGlobal.SystemSerialNo = "M$R$V$2018";
            // Ignore
        }

        return Task.FromResult(ObjGlobal.SystemSerialNo);
    }

    [Obsolete]
    public static string GetIpAddress()
    {
        var hostName = Dns.GetHostName();
        Console.WriteLine(hostName);
        var ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        ObjGlobal.SystemHostName = hostName;
        return ipAddress;
    }
    public static string GetServerName()
    {
        var computerName = Environment.MachineName;
        Console.WriteLine(computerName);
        ObjGlobal.ComputerName = computerName;
        return computerName;
    }
    public static string GetLocalMacAddress()
    {
        var macAddresses =
        (
            from nic in NetworkInterface.GetAllNetworkInterfaces()
            where nic.OperationalStatus == OperationalStatus.Up
            select nic.GetPhysicalAddress().ToString()
        ).FirstOrDefault();
        ObjGlobal.SystemMacAddress = macAddresses;
        return macAddresses;
    }

    public static string MenuUserAccessControlData { get; set; }

    public static List<UserAccessFormControl> FormUserAccessControlData { get; set; }

    public static void GetFormAccessControl(SimpleButton btn, object frmName)
    {
        if (frmName == null || FormUserAccessControlData == null)
        {
            return;
        }

        var data = FormUserAccessControlData.FirstOrDefault(x => x.FormName == frmName.ToString());
        if (data != null)
        {
            btn.Visible = btn.Tag.ToString() switch
            {
                "NewButtonCheck" => data.NewButtonCheck,
                "EditButtonCheck" => data.EditButtonCheck,
                "DeleteButtonCheck" => data.DeleteButtonCheck,
                "ViewButtonCheck" => data.ViewButtonCheck,
                _ => btn.Visible
            };
        }
    }

    public static void GetFormAccessControl(List<SimpleButton> btn, object frmName)
    {
        if (frmName == null || FormUserAccessControlData == null)
        {
            return;
        }
        var data = FormUserAccessControlData.FirstOrDefault(x => x.FormName == frmName.ToString());
        foreach (var b in btn.Where(b => b.Tag != null))
        {
            if (data != null)
            {
                b.Visible = b.Tag.ToString() switch
                {
                    "NewButtonCheck" => data.NewButtonCheck,
                    "EditButtonCheck" => data.EditButtonCheck,
                    "DeleteButtonCheck" => data.DeleteButtonCheck,
                    "ViewButtonCheck" => data.ViewButtonCheck,
                    _ => b.Visible
                };
            }
        }
    }

    public static UserAccessFormControl GetFormAccessControl(string frmName)
    {
        if (FormUserAccessControlData == null || string.IsNullOrEmpty(frmName))
        {
            return null;
        }

        var data = FormUserAccessControlData.FirstOrDefault(x => x.FormName == frmName);
        return data;
    }

    #endregion ------------------ METHOD FOR FORM ------------------

    // SET DEFAULT VALUE IN DATABASE -- FILL SYSTEM CONFIGURATION

    #region ------------------ GLOBAL VALIABLE FOR SYSTEM CONFIGURATION ------------------

    public static void FillSystemConFiguration()
    {
        try
        {
            const string cmdString = @"
                SELECT * FROM AMS.SystemSetting ss;
                SELECT * FROM AMS.PurchaseSetting ps;
                SELECT * FROM AMS.SalesSetting ss;
                SELECT * FROM AMS.InventorySetting ivs;
                SELECT * FROM AMS.FinanceSetting fs;
                SELECT * FROM AMS.IRDAPISetting i;
                SELECT * FROM AMS.PaymentSetting;
                SELECT * FROM master.AMS.SoftwareRegistration;
                SELECT * FROM AMS.SyncTable; 
                SELECT TOP (1) net_address MachineName FROM master.dbo.SysProcesses where hostname = (SELECT SERVERPROPERTY('MachineName')) and net_library='LPC' and nt_domain =(SELECT SERVERPROPERTY('MachineName')) 
                ORDER BY net_address;";

            var dsSetting = SqlExtensions.ExecuteDataSet(cmdString);
            var dtSystem = dsSetting.Tables[0];
            var dtPurchase = dsSetting.Tables[1];
            var dtSales = dsSetting.Tables[2];
            var dtStock = dsSetting.Tables[3];
            var dtFinance = dsSetting.Tables[4];
            var dtIrdConfig = dsSetting.Tables[5];
            var dtPaymentSetting = dsSetting.Tables[6];
            var dtLicense = dsSetting.Tables[7];
            var dtDataSync = dsSetting.Tables[8];
            var dtSystemAddress = dsSetting.Tables[9];

            if (dtSystem is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtSystem.Rows)
                {
                    SysIsDateWise = dr["EnglishDate"].GetBool();
                    SysDateType = SysIsDateWise ? "D" : "M";
                    SysAuditTrial = dr["AuditTrial"].GetBool();
                    SysUdf = dr["Udf"].GetBool();
                    SysAutoPopup = dr["Autopoplist"].GetBool();
                    SysCurrentDate = dr["CurrentDate"].GetBool();
                    SysConfirmSave = dr["ConformSave"].GetBool();
                    SysConfirmCancel = dr["ConformCancel"].GetBool();
                    SysConfirmExit = dr["ConformExits"].GetBool();
                    SysCurrencyRate = dr["CurrencyRate"].GetDecimalString(true);
                    SysCurrencyId = dr["CurrencyId"].GetInt();
                    SysConfirmExit = dr["ConformExits"].GetBool();
                    SysDefaultPrinter = dr["DefaultPrinter"].GetString();
                    SysAmountFormat = dr["AmountFormat"].GetString();
                    SysAmountLength = Math.Abs(SysAmountFormat.Length - 2);
                    SysRateFormat = dr["RateFormat"].GetString();
                    SysRateLength = Math.Abs(SysRateFormat.Length - 2);
                    SysQtyFormat = dr["QtyFormat"].GetString();
                    SysQtyLength = Math.Abs(SysQtyFormat.Length - 2);
                    SysCurrencyFormat = dr["CurrencyFormatF"].GetString();

                    SysQtyCommaFormat = SysQtyLength switch
                    {
                        0 => "##,##,##0",
                        1 => "##,##,##0.0",
                        2 => "##,##,##0.00",
                        3 => "##,##,##0.000",
                        4 => "##,##,##0.0000",
                        5 => "##,##,##0.00000",
                        6 => "##,##,##0.000000",
                        _ => "##,##,##0.00"
                    };
                    SysRateCommaFormat = SysAmountLength switch
                    {
                        0 => "##,##,##0",
                        1 => "##,##,##0.0",
                        2 => "##,##,##0.00",
                        3 => "##,##,##0.000",
                        4 => "##,##,##0.0000",
                        5 => "##,##,##0.00000",
                        6 => "##,##,##0.000000",
                        _ => "##,##,##0.00"
                    };
                    SysAmountCommaFormat = SysAmountLength switch
                    {
                        0 => "##,##,##0",
                        1 => "##,##,##0.0",
                        2 => "##,##,##0.00",
                        3 => "##,##,##0.000",
                        4 => "##,##,##0.0000",
                        5 => "##,##,##0.00000",
                        6 => "##,##,##0.000000",
                        _ => "##,##,##0.00"
                    };
                    SysFiscalYearId = dr["DefaultFiscalYearId"].GetInt();
                    SysDefaultOrderPrinter = dr["DefaultOrderPrinter"].GetString();
                    SysDefaultInvoicePrinter = dr["DefaultInvoicePrinter"].GetString();
                    SysDefaultOrderDocNumbering = dr["DefaultOrderNumbering"].GetString();
                    SysDefaultInvoiceDocNumbering = dr["DefaultInvoiceNumbering"].GetString();
                    SysDefaultAbtInvoiceDocNumbering = dr["DefaultAvtInvoiceNumbering"].GetString();
                    SysDefaultOrderDesign = dr["DefaultOrderDesign"].GetString();
                    SysIsOrderPrint = dr["IsOrderPrint"].GetBool();
                    SysDefaultInvoiceDesign = dr["DefaultInvoiceDesign"].GetString();
                    SysIsInvoicePrint = dr["IsInvoicePrint"].GetBool();
                    SysIsBranchPrint = dr["IsPrintBranch"].GetBool();
                    SysDefaultAbtInvoiceDesign = dr["DefaultAvtDesign"].GetString();
                    SysFontName = dr["DefaultFontsName"].GetString();
                    SysFontSize = dr["DefaultFontsSize"].GetInt();
                    SysPaperSize = dr["DefaultPaperSize"].GetString();
                    SysReportStyle = dr["DefaultReportStyle"].GetString();
                    SysPrintingDateTime = dr["DefaultPrintDateTime"].GetBool();
                    SysDefaultFormColor = dr["DefaultFormColor"].GetString();
                    SysDefaultTextColor = dr["DefaultTextColor"].GetString();
                    SysDebtorGroupId = dr["DebtorsGroupId"].GetInt();
                    SysCreditorGroupId = dr["CreditorGroupId"].GetInt();
                    SysSalaryLedgerId = dr["SalaryLedgerId"].GetLong();
                    SysTdsLedgerId = dr["TDSLedgerId"].GetLong();
                    SysPfLedgerId = dr["PFLedgerId"].GetLong();
                    SysEmailAddress = dr["DefaultEmail"].GetString();
                    SysEmailPassword = dr["DefaultEmailPassword"].GetString();
                    SysBackupPath = dr["BackupLocation"].GetString();
                    SysBackupDays = dr["BackupDays"].GetInt();

                    NightAuditEndTime = dr["EndTime"].GetDateTime().TimeOfDay;
                    SysIsBranchPrint = dr["IsPrintBranch"].GetBool();
                    SysIsNightAudit = dr["IsNightAudit"].GetBool();
                    SysIsAlphaSearch = dr["SearchAlpha"].GetBool();
                    SysIsBarcodeSearch = dr["BarcodeAutoSearch"].GetBool();
                }
            }

            if (dtPurchase is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtPurchase.Rows)
                {
                    PurchaseLedgerId = dr["PBLedgerId"].GetLong();
                    PurchaseReturnLedgerId = dr["PRLedgerId"].GetLong();
                    PurchaseVatTermId = dr["PBVatTerm"].GetInt();
                    PurchaseDiscountTermId = dr["PBDiscountTerm"].GetInt();
                    PurchaseProductDiscountTermId = dr["PBProductDiscountTerm"].GetInt();
                    PurchaseAdditionalVatTermId = dr["PBAdditionalTerm"].GetInt();
                    PurchaseInvoiceDateEnable = dr["PBDateChange"].GetBool();
                    PurchaseCreditDaysWarning = dr["PBCreditDays"].GetInt();
                    PurchaseCreditBalanceWarning = dr["PBCreditLimit"].GetInt();
                    PurchaseCarryRate = dr["PBCarryRate"].GetBool();
                    PurchaseChangeRate = dr["PBChangeRate"].GetBool();
                    PurchaseLastRate = dr["PBLastRate"].GetBool();
                    PurchaseOrderEnable = dr["POEnable"].GetBool();
                    PurchaseOrderMandatory = dr["POMandetory"].GetBool();
                    PurchaseChallanEnable = dr["PCEnable"].GetBool();
                    PurchaseChallanMandatory = dr["PCMandetory"].GetBool();
                    PurchaseSubLedgerEnable = dr["PBSublegerEnable"].GetBool();
                    PurchaseSubLedgerMandatory = dr["PBSubledgerMandetory"].GetBool();
                    PurchaseAgentEnable = dr["PBAgentEnable"].GetBool();
                    PurchaseAgentMandatory = dr["PBAgentMandetory"].GetBool();
                    PurchaseDepartmentEnable = dr["PBDepartmentEnable"].GetBool();
                    PurchaseDepartmentMandatory = dr["PBDepartmentMandetory"].GetBool();
                    PurchaseCurrencyEnable = dr["PBCurrencyEnable"].GetBool();
                    PurchaseCurrencyMandatory = dr["PBCurrencyMandetory"].GetBool();
                    PurchaseCurrencyRateChange = dr["PBCurrencyRateChange"].GetBool();
                    PurchaseGodownEnable = dr["PBGodownEnable"].GetBool();
                    PurchaseGodownMandatory = dr["PBGodownMandetory"].GetBool();
                    PurchaseAltUnitEnable = dr["PBAlternetUnitEnable"].GetBool();
                    PurchaseAltUnitMandatory = dr["PBAlternetUnitEnable"].GetBool();
                    PurchaseIndentEnable = dr["PBIndent"].GetBool();
                    PurchaseIndentMandatory = dr["PBIndentMandatory"].GetBool();
                    PurchaseBasicAmountEnable = dr["PBBasicAmount"].GetBool();
                    PurchaseRemarksEnable = dr["PBRemarksEnable"].GetBool();
                    PurchaseRemarksMandatory = dr["PBRemarksMandatory"].GetBool();
                    PurchaseDescriptionsEnable = dr["PBNarration"].GetBool();
                }
            }

            if (dtSales is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtSales.Rows)
                {
                    SalesLedgerId = dr["SBLedgerId"].GetLong();
                    SalesReturnLedgerId = dr["SRLedgerId"].GetLong();
                    SalesVatTermId = dr["SBVatTerm"].GetInt();
                    SalesDiscountTermId = dr["SBProductDiscountTerm"].GetInt();
                    SalesSpecialDiscountTermId = dr["SBDiscountTerm"].GetInt();
                    SalesAdditionalTerm = dr["SBAdditionalTerm"].GetInt();
                    SalesServiceChargeTermId = dr["SBServiceCharge"].GetInt();
                    SalesInvoiceDateEnable = dr["SBDateChange"].GetBool();
                    SalesCreditDaysWarning = dr["SBCreditDays"].GetInt();
                    SalesCreditDaysWarning = dr["SBCreditLimit"].GetInt();
                    SalesCarryRate = dr["SBCarryRate"].GetBool();
                    SalesChangeRate = dr["SBChangeRate"].GetBool();
                    SalesLastRate = dr["SBLastRate"].GetBool();
                    SalesQuotationEnable = dr["SBQuotationEnable"].GetBool();
                    SalesQuotationMandatory = dr["SBQuotationMandetory"].GetBool();
                    SalesDispatchOrderEnable = dr["SBDispatchOrderEnable"].GetBool();
                    SalesDispatchOrderMandatory = dr["SBDispatchMandetory"].GetBool();
                    SalesOrderEnable = dr["SOEnable"].GetBool();
                    SalesOrderMandatory = dr["SOMandetory"].GetBool();
                    SalesChallanEnable = dr["SCEnable"].GetBool();
                    SalesChallanMandatory = dr["SCMandetory"].GetBool();
                    SalesSubLedgerEnable = dr["SBSublegerEnable"].GetBool();
                    SalesSubLedgerMandatory = dr["SBSubledgerMandetory"].GetBool();
                    SalesAgentEnable = dr["SBAgentEnable"].GetBool();
                    SalesAgentMandatory = dr["SBAgentMandetory"].GetBool();
                    SalesDepartmentEnable = dr["SBDepartmentEnable"].GetBool();
                    SalesDepartmentMandatory = dr["SBDepartmentMandetory"].GetBool();
                    SalesCurrencyEnable = dr["SBCurrencyEnable"].GetBool();
                    SalesCurrencyMandatory = dr["SBCurrencyMandetory"].GetBool();
                    SalesCurrencyRateChange = dr["SBCurrencyRateChange"].GetBool();
                    SalesGodownEnable = dr["SBGodownEnable"].GetBool();
                    SalesGodownMandatory = dr["SBGodownMandetory"].GetBool();
                    SalesAltUnitEnable = dr["SBAlternetUnitEnable"].GetBool();
                    SalesAltUnitMandatory = dr["SBAlternetUnitEnable"].GetBool();
                    SalesIndentEnable = dr["SBIndent"].GetBool();
                    SalesDescriptionsEnable = dr["SBNarration"].GetBool();
                    SalesBasicAmountEnable = dr["SBBasicAmount"].GetBool();
                    SalesAvailableStock = dr["SBAviableStock"].GetBool();
                    SalesStockValueInSalesReturn = dr["SBReturnValue"].GetBool();
                    SalesPartyName = dr["PartyInfo"].GetBool();
                    SalesRemarksEnable = dr["SBRemarksEnable"].GetBool();
                    SalesRemarksMandatory = dr["SBRemarksMandatory"].GetBool();
                    SalesIgnoreTenderAmount = dr["IgnoreTenderAmount"].GetBool();
                }
            }

            if (dtStock is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtStock.Rows)
                {
                    StockOpeningStockLedgerId = dr["OPLedgerId"].GetLong();
                    StockClosingStockLedgerId = dr["CSPLLedgerId"].GetLong();
                    StockStockInHandLedgerId = dr["CSBSLedgerId"].GetLong();
                    StockNegativeStockWarning = dr["NegativeStock"].Equals("W");
                    StockNegativeStockBlock = dr["NegativeStock"].Equals("B");
                    StockAltUnitEnable = dr["AlternetUnit"].GetBool();
                    StockCostCenterEnable = dr["CostCenterEnable"].GetBool();
                    StockCostCenterMandatory = dr["CostCenterMandetory"].GetBool();
                    StockCostCenterItemEnable = dr["CostCenterItemEnable"].GetBool();
                    StockCostCenterItemMandatory = dr["CostCenterItemMandetory"].GetBool();
                    StockUnitEnable = dr["ChangeUnit"].GetBool();
                    StockGodownEnable = dr["GodownEnable"].GetBool();
                    StockGodownMandatory = dr["GodownMandetory"].GetBool();
                    StockRemarksEnable = dr["RemarksEnable"].GetBool();
                    StockGodownItemEnable = dr["GodownItemEnable"].GetBool();
                    StockGodownItemMandatory = dr["GodownItemMandetory"].GetBool();
                    StockDescriptionsEnable = dr["NarrationEnable"].GetBool();
                    StockShortNameWise = dr["ShortNameWise"].GetBool();
                    StockCarryBatchQty = dr["BatchWiseQtyEnable"].GetBool();
                    StockExpiryDate = dr["ExpiryDate"].GetBool();
                    StockFreeQty = dr["FreeQty"].GetBool();
                    StockGroupWiseFilter = dr["GroupWiseFilter"].GetBool();
                    StockGodownWiseStockFilter = dr["GodownWiseStock"].GetBool();
                }
            }

            if (dtFinance is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtFinance.Rows)
                {
                    FinanceProfitLossLedgerId = dr["ProfiLossId"].GetLong();
                    FinanceCashLedgerId = dr["CashId"].GetLong();
                    FinanceVatLedgerId = dr["VATLedgerId"].GetLong();
                    FinanceBankLedgerId = dr["PDCBankLedgerId"].GetLong();
                    FinanceShortNameWise = dr["ShortNameWisTransaction"].GetBool();
                    FinanceNegativeWarning = dr["WarngNegativeTransaction"].GetBool();
                    FinanceNegativeTransaction = dr["NegativeTransaction"].GetString();
                    FinanceVoucherDateEnable = dr["VoucherDate"].GetBool();
                    FinanceAgentEnable = dr["AgentEnable"].GetBool();
                    FinanceAgentMandatory = dr["AgentMandetory"].GetBool();
                    FinanceDepartmentEnable = dr["DepartmentEnable"].GetBool();
                    FinanceDepartmentEnable = dr["DepartmentMandetory"].GetBool();
                    FinanceRemarksEnable = dr["RemarksEnable"].GetBool();
                    FinanceRemarksMandatory = dr["RemarksMandetory"].GetBool();
                    FinanceNarrationMandatory = dr["NarrationMandetory"].GetBool();
                    FinanceCurrencyEnable = dr["CurrencyEnable"].GetBool();
                    FinanceCurrencyMandatory = dr["CurrencyMandetory"].GetBool();
                    FinanceSubLedgerEnable = dr["SubledgerEnable"].GetBool();
                    FinanceSubLedgerMandatory = dr["SubledgerMandetory"].GetBool();
                    FinanceDepartmentItemEnable = dr["DetailsClassEnable"].GetBool();
                    FinanceDepartmentItemMandatory = dr["DetailsClassMandetory"].GetBool();
                }
            }

            if (dtPaymentSetting is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtPaymentSetting.Rows)
                {
                    FinanceCashLedgerId = dr["CashLedgerId"].GetLong() > 0
                        ? dr["CashLedgerId"].GetLong()
                        : FinanceCashLedgerId;

                    FinanceBankLedgerId = dr["BankLedgerId"].GetLong() > 0
                        ? dr["BankLedgerId"].GetLong()
                        : FinanceBankLedgerId;

                    FinanceCardLedgerId = dr["CardLedgerId"].GetLong();
                    FinanceFonePayLedgerId = dr["PhonePayLedgerId"].GetLong();
                    FinanceEsewaLedgerId = dr["EsewaLedgerId"].GetLong();
                    FinanceKhaltiLedgerId = dr["KhaltiLedgerId"].GetLong();
                    FinanceRemitLedgerId = dr["RemitLedgerId"].GetLong();
                    FinanceConnectIpsLedgerId = dr["ConnectIpsLedgerId"].GetLong();
                    FinancePartialLedgerId = dr["PartialLedgerId"].GetLong();
                    FinanaceGiftVoucherLedgerId = dr["GiftVoucherLedgerId"].GetLong();
                }
            }

            var licenseAsync = ReadLicenseAsync();
            if (!licenseAsync)
            {
                if (dtLicense is { Rows.Count: > 0 })
                {
                    if (dtLicense.Rows.Count > 1)
                    {
                        var macAddress = GetMacAddress();
                        macAddress = Encrypt(macAddress);

                        var decrypt = Encrypt(SystemSerialNo);
                        var license =
                            dtLicense.Select($"ClientSerialNo = '{decrypt}' or Server_MacAdd ='{macAddress}' ");

                        if (license.Length > 0)
                        {
                            foreach (var dr in license)
                            {
                                LicenseRegistrationId = dr["RegistrationId"].GetGuid();
                                RegisterLicenseNodes = Decrypt(dr["NoOfNodes"].ToString()).GetInt();
                                ExpiryLicenseDate = Decrypt(dr["ExpiredDate"].ToString()).GetDateTime();
                                RemainingDays = (ExpiryLicenseDate - DateTime.Now).Days;

                                IsLicenseExpire = RemainingDays < 0;
                                IsIrdApproved = Decrypt(dr["Module"].ToString()).GetString();
                                IsOnlineMode = dr["IsOnline"].GetBool();
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dtLicense.Rows)
                        {
                            LicenseRegistrationId = dr["RegistrationId"].GetGuid();
                            RegisterLicenseNodes = Decrypt(dr["NoOfNodes"].ToString()).GetInt();
                            ExpiryLicenseDate = Decrypt(dr["ExpiredDate"].ToString()).GetDateTime();
                            RemainingDays = (ExpiryLicenseDate - DateTime.Now).Days;

                            IsLicenseExpire = RemainingDays < 0;
                            IsIrdApproved = Decrypt(dr["Module"].ToString()).GetString();
                            IsOnlineMode = dr["IsOnline"].GetBool();
                        }
                    }
                }
                else
                {
                    const string cmdExpire = @" 
                    SELECT create_date FROM master.sys.tables WHERE name = 'SoftwareRegistration';";
                    var installDate = cmdExpire.GetQueryData().GetDateTime();
                    var differenceDays = (DateTime.Now - installDate).Days;
                    IsLicenseExpire = differenceDays > 30;
                    RemainingDays = differenceDays;
                    IsNewInstallation = differenceDays < 30;
                }
            }

            if (dtIrdConfig is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtIrdConfig.Rows)
                {
                    var address = dr["IRDAPI"].GetString();
                    if (!ObjGlobal.IrdApiAddress.Equals(address))
                    {
                        var msg = "API SAVE IN DATABASE & SOFTWARE IS DIFFRENCE.. DO YOU WANT TO UPDATE..??";
                        var question = CustomMessageBox.Question(msg);
                        if (question == DialogResult.Yes)
                        {
                            break;
                        }
                    }
                    IrdApiAddress = address;
                    IrdUser = dr["IrdUser"].GetString();
                    IrdUserPassword = dr["IrdUserPassword"].GetString();
                    IrdCompanyPan = dr["IrdCompanyPan"].GetString();
                    var isTaxRegister = dr["IsIRDRegister"].GetInt() > 0;
                    IsIrdApproved = isTaxRegister ? "YES" : "NO";
                }
            }

            if (dtDataSync is { Rows.Count: > 0 })
            {
                foreach (DataRow dr in dtDataSync.Rows)
                {
                    IsBranchSync = dr["IsBranch"].GetBool();
                    IsGeneralLedgerSync = dr["IsGeneralLedger"].GetBool();
                    IsTableIdSync = dr["IsTableId"].GetBool();
                    IsAreaSync = dr["IsArea"].GetBool();
                    IsBillingTermSync = dr["IsBillingTerm"].GetBool();
                    IsAgentSync = dr["IsAgent"].GetBool();
                    IsProductSync = dr["IsProduct"].GetBool();
                    IsCostCenterSync = dr["IsCostCenter"].GetBool();
                    IsMemberSync = dr["IsMember"].GetBool();
                    IsCashBankSync = dr["IsCashBank"].GetBool();
                    IsJournalVoucherSync = dr["IsJournalVoucher"].GetBool();
                    IsNotesRegisterSync = dr["IsNotesRegister"].GetBool();
                    IsPDCVoucherSync = dr["IsPDCVoucher"].GetBool();
                    IsLedgerOpeningSync = dr["IsLedgerOpening"].GetBool();
                    IsProductOpeningSync = dr["IsProductOpening"].GetBool();
                    IsSalesQuotationSync = dr["IsSalesQuotation"].GetBool();
                    IsSalesOrderSync = dr["IsSalesOrder"].GetBool();
                    IsSalesChallanSync = dr["IsSalesChallan"].GetBool();
                    IsSalesInvoiceSync = dr["IsSalesInvoice"].GetBool();
                    IsSalesReturnSync = dr["IsSalesReturn"].GetBool();
                    IsSalesAdditionalSync = dr["IsSalesAdditional"].GetBool();
                    IsPurchaseIndentSync = dr["IsPurchaseIndent"].GetBool();
                    IsPurchaseOrderSync = dr["IsPurchaseOrder"].GetBool();
                    IsPurchaseChallanSync = dr["IsPurchaseChallan"].GetBool();
                    IsPurchaseInvoiceSync = dr["IsPurchaseInvoice"].GetBool();
                    IsPurchaseReturnSync = dr["IsPurchaseReturn"].GetBool();
                    IsPurchaseAdditionalSync = dr["IsPurchaseAdditional"].GetBool();
                    IsStockAdjustmentSync = dr["IsStockAdjustment"].GetBool();
                    SyncAPISync = dr["SyncAPI"].ToString();
                    SyncOrginIdSync = Decrypt(dr["SyncOriginId"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
        }
    }

    private static bool ReadLicenseAsync()
    {
        var fileName = Application.StartupPath + @"\MrLicense.lix";
        if (!File.Exists(fileName))
        {
            return false;
        }

        var strContent = File.ReadAllText(fileName);
        var json = Decrypt(strContent);
        try
        {
            var model = JsonConvert.DeserializeObject<LicDetail>(json);
            var modelLicenseTo = model?.LicenseTo;
            if (model != null)
            {
                var nudVersion = model.Version;
            }

            if (model != null)
            {
                var cbxEditions = model.Edition;
            }

            if (model == null)
            {
                return false;
            }

            var originGroupId = model.OriginGroupId.ToString();
            var hwIds = model.HwIds;

            foreach (var t in model.Branches)
            {
                TotalUser = t.MaxUsers.GetInt();
                RegisterLicenseNodes = t.MaxPc.GetInt();
                LicenseRegistrationId = t.OutletUqId;
                var expDate = t.ExpDate;
                var today = DateTime.Now.GetDateTime();
                var difference = expDate - today;
                IsLicenseExpire = difference.Days.GetInt() <= 0;
            }

            return true;
        }
        catch (JsonReaderException ex)
        {
            MessageBox.Show(@"Invalid Json" + Environment.NewLine + ex.Message);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }

        return false;
    }

    #endregion ------------------ GLOBAL VALIABLE FOR SYSTEM CONFIGURATION ------------------

    // VALID DATE TIME

    #region ------------------ date/days validation ------------------

    public static int ReturnInt(string value)
    {
        int.TryParse(value, out var returnInt);
        return returnInt;
    }

    public static bool ReturnBool(string value)
    {
        bool.TryParse(value, out var returnBool);
        return returnBool;
    }

    public static long ReturnLong(string value)
    {
        long.TryParse(value, out var returnLong);
        return returnLong;
    }

    public static double ReturnDouble(string value)
    {
        double.TryParse(value, out var returnDouble);
        return returnDouble;
    }

    public static decimal ReturnDecimal(string value)
    {
        decimal.TryParse(value, out var returnDecimal);
        return returnDecimal;
    }

    public static string ReturnString(string value)
    {
        var con = Convert.ToString(value);
        return con;
    }

    #endregion ------------------ date/days validation ------------------

    // DATA GRID CUSTOMIZED

    #region ------------------ Control Color ------------------

    public static DataGridView DgvBackColor(DataGridView grid)
    {
        try
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Color.White;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.HeaderCell.Style.BackColor = SystemColors.GradientInactiveCaption;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Bold);
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FloralWhite; // Rows Cell Style
                row.HeaderCell.Style.BackColor = Color.DarkSlateBlue;
                row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            }
        }
        catch
        {
            // ignored
        }

        return grid;
    }

    public static DataGridView DGridColorCombo(DataGridView rGrid)
    {
        try
        {
            if (rGrid.ColumnCount <= 0)
            {
                return rGrid;
            }

            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            rGrid.EnableHeadersVisualStyles = false;
            rGrid.BackgroundColor = Color.AliceBlue;
            rGrid.GridColor = Color.DarkSlateBlue;
            rGrid.RowHeadersWidth = rGrid.RowHeadersVisible ? 25 : 4;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);

            foreach (DataGridViewColumn col in rGrid.Columns)
            {
                col.HeaderCell.Style.BackColor = Color.DarkSlateBlue;
                col.HeaderCell.Style.ForeColor = Color.White;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderText = col.HeaderText.ToUpper();
                col.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
                col.DefaultCellStyle.Font =
                    new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }

            rGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            rGrid.RowTemplate.DefaultCellStyle.Font = new Font("Bookman Old Style", 11.25F);
            return rGrid;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            e.ToNonQueryErrorResult(e.StackTrace);
            return rGrid;
        }
    }

    #endregion ------------------ Control Color ------------------

    // CONTROL CUSTOMIZED

    #region ------------------ Control Color ------------------

    public static TextBox TxtBackColor(TextBox txt, char type)
    {
        txt.BackColor = type switch
        {
            'E' => Color.Thistle,
            'L' => Color.Wheat,
            _ => txt.BackColor
        };

        txt.ForeColor = Color.DarkBlue;
        return txt;
    }

    public static ComboBox ComboBoxBackColor(ComboBox cb, char type)
    {
        cb.BackColor = type switch
        {
            'E' => Color.Thistle,
            'L' => Color.Wheat,
            _ => cb.BackColor
        };

        cb.ForeColor = Color.DarkBlue;
        return cb;
    }

    public static MaskedTextBox MskTxtBackColor(MaskedTextBox txt, char type)
    {
        txt.BackColor = type switch
        {
            'E' => Color.Thistle,
            'L' => Color.Wheat,
            _ => txt.BackColor
        };

        txt.ForeColor = Color.DarkBlue;
        return txt;
    }

    public static Color FrmBackColor()
    {
        return SystemColors.InactiveCaption;
    }

    public static Color GetEnterBackColor()
    {
        return SystemColors.InactiveCaption;
    }

    public static Color GetEnterForeColor()
    {
        return Color.Black;
    }

    public static Color GetLeaveBackColor()
    {
        return Color.White;
    }

    public static Color GetLeaveForeColor()
    {
        return Color.Black;
    }

    public static bool CheckOpened()
    {
        var fc = Application.OpenForms;
        return fc.Count > 1;
    }

    #endregion ------------------ Control Color ------------------

    // RETURN COMBO BOX

    #region ------------------ Bind Combo Box ------------------

    public static void BindFiscalYear(ComboBox cmbFiscalYear)
    {
        var cmdString = SysDateType == "D"
            ? "SELECT FY_Id,AD_FY Fiscal_Year FROM AMS.FiscalYear order BY Fiscal_Year desc "
            : "SELECT FY_Id,BS_FY Fiscal_Year FROM AMS.FiscalYear order BY Fiscal_Year desc ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return;
        }

        cmbFiscalYear.DataSource = dt;
        cmbFiscalYear.DisplayMember = "Fiscal_Year";
        cmbFiscalYear.ValueMember = "FY_Id";
        cmbFiscalYear.SelectedItem = SysFiscalYearId;
    }

    public static void BindMasterFiscalYear(ComboBox cbFiscalYear)
    {
        var cmdString = "SELECT fy.FiscalYearId,fy.BSFiscalYear FROM MASTER.AMS.FiscalYear fy";
        var dt = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return;
        }

        cbFiscalYear.DataSource = dt;
        cbFiscalYear.DisplayMember = "BSFiscalYear";
        cbFiscalYear.ValueMember = "FiscalYearId";
    }

    public static ComboBox BindBranch(ComboBox cbBranch)
    {
        var cmdString = @"
        SELECT 0 Branch_Id,'--Select--' Branch_Name Union All SELECT Branch_Id,Branch_Name FROM AMS.Branch";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return cbBranch;
        }

        cbBranch.DataSource = dt;
        cbBranch.DisplayMember = "Branch_Name";
        cbBranch.ValueMember = "Branch_Id";
        return cbBranch;
    }

    public static ComboBox BindDateType(ComboBox comboBox, int typeId = 8)
    {
        var list = typeId <= 8
            ? new List<ValueModel<string, string>>
            {
                new("Custom Date", "Custom Date"),
                new("Today", "Today"),
                new("Yesterday", "Yesterday"),
                new("Current Week", "Current Week"),
                new("Last Week", "Last Week"),
                new("Current Month", "Current Month"),
                new("Last Month", "Last Month"),
                new("Up to Date", "Up to Date"),
                new("Accounting Period", "Accounting Period")
            }
            : new List<ValueModel<string, string>>
            {
                new("Custom Date", "Custom Date"),
                new("Today", "Today"),
                new("Yesterday", "Yesterday"),
                new("Current Week", "Current Week"),
                new("Last Week", "Last Week"),
                new("Current Month", "Current Month"),
                new("Last Month", "Last Month"),
                new("Up to Date", "Up to Date"),
                new("Accounting Period", "Accounting Period"),
                new("As on", "As on")
            };
        if (list.Count <= 0)
        {
            return comboBox;
        }

        comboBox.DataSource = list;
        comboBox.DisplayMember = "Item1";
        comboBox.ValueMember = "Item2";
        comboBox.SelectedIndex = typeId is 9 ? 9 : 8;
        return comboBox;
    }

    public static ComboBox BindUserRoleAsync(ComboBox userType)
    {
        const string cmdString = "select Role_Id,Role from AMS.User_Role";
        var dt = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return userType;
        }

        userType.DataSource = dt;
        userType.DisplayMember = "Role";
        userType.ValueMember = "Role_Id";
        return userType;
    }

    public static ComboBox BindUser(ComboBox cbUserInfo)
    {
        const string cmdString = "Select  User_Name,Full_Name from AMS.UserInfo";
        var dt = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return cbUserInfo;
        }

        cbUserInfo.DataSource = dt;
        cbUserInfo.DisplayMember = "User_Name";
        cbUserInfo.ValueMember = "Full_Name";
        return cbUserInfo;
    }

    public static void BindAdvanceReport(ComboBox advanceReport, string station)
    {
        var cmdString =
            $"SELECT 0 DocDesigner_Id,'--Select--' DesignerPaper_Name Union All SELECT DocDesigner_Id,DesignerPaper_Name FROM Master.AMS.PrintDocument_Designer Where Type = 'Report' and Station = '{station}' ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return;
        }

        advanceReport.DataSource = dt;
        advanceReport.DisplayMember = "DesignerPaper_Name";
        advanceReport.ValueMember = "DocDesigner_Id";
    }

    public static void BindPrinter(ComboBox printerName)
    {
        printerName.Items.Clear();
        if (PrinterSettings.InstalledPrinters.Count <= 0)
        {
            MessageBox.Show(@"PRINTER NOT FOUND..!!");
            return;
        }

        foreach (string printer in PrinterSettings.InstalledPrinters) printerName.Items.Add(printer);
        printerName.SelectedIndex = 0;
    }

    #endregion ------------------ Bind Combo Box ------------------

    // RETURN VALUE IN STRING

    #region ------------------ Return datatable value & other ------------------

    public static string BindAutoIncrementCode(string type, string autoIncrementCode)
    {
        string cmdString;
        DataTable dt;
        autoIncrementCode = autoIncrementCode.PadRight(2, 'A').ToUpper();
        switch (type) // Ward
        {
            case "BT":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(BShortName,1,2))+1 BShortName  FROM HOS.BedType  where SUBSTRING(BShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["BShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "WD":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(WShortName,1,2))+1 WShortName  FROM HOS.WardMaster  where SUBSTRING(WShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["WShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "HD":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(DCode,1,2))+1 DCode  FROM HOS.Department  where SUBSTRING(DCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) + dt.Rows[0]["DCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "DR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(DrShortName,1,2))+1 DrShortName  FROM HOS.Doctor  where SUBSTRING(DrShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["DrShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "DRT":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(DrTypeShortName,1,2))+1 DrTypeShortName  FROM HOS.DoctorType  where SUBSTRING(DrTypeShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["DrTypeShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "BR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(Branch_Code,1,2))+1 Branch_Code  FROM AMS.Branch  where SUBSTRING(Branch_Code,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["Branch_Code"].ToString().PadLeft(5, '0');
                    break;
                }

            case "CMU":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(CmpUnit_Code,1,2))+1 CmpUnit_Code  FROM AMS.CompanyUnit  where SUBSTRING(CmpUnit_Code,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["CmpUnit_Code"].ToString().PadLeft(5, '0');
                    break;
                }

            case "AG":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(GrpCode,1,2))+1 GrpCode  FROM AMS.AccountGroup  where SUBSTRING(GrpCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode =
                        autoIncrementCode.Substring(0, 2) + dt.Rows[0]["GrpCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "ASG":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(SubGrpCode,1,2))+1 SubGrpCode  FROM AMS.AccountSubGroup  where SUBSTRING(SubGrpCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["SubGrpCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "GL":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(GlCode,1,2))+1 GlCode  FROM AMS.GeneralLedger  where SUBSTRING(GlCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) + dt.Rows[0]["GlCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "SL":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(SlCode,1,2))+1 SlCode  FROM AMS.SubLedger  where SUBSTRING(SlCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) + dt.Rows[0]["SlCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "Agent":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(AgentCode,1,2))+1 AgentCode  FROM AMS.JuniorAgent  where SUBSTRING(AgentCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["AgentCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "UR":
                {
                    cmdString =
                        $"SELECT COUNT(SUBSTRING(Role,1,2))+1 Role_Id  FROM MASTER.AMS.User_Role  where SUBSTRING(Role,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode =
                        autoIncrementCode.Substring(0, 2) + dt.Rows[0]["Role_Id"].ToString().PadLeft(5, '0');
                    break;
                }

            case "SAgent":
                {
                    try
                    {
                        cmdString =
                            $"SELECT  COUNT(SUBSTRING(SAgentCode,1,2))+1 SAgentCode  FROM AMS.SeniorAgent  where SUBSTRING(SAgentCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                        dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                        autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                            dt.Rows[0]["SAgentCode"].ToString().PadLeft(5, '0');
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }

                    break;
                }

            case "MAR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(MAreaCode,1,2))+1 MAreaCode  FROM AMS.MainArea  where SUBSTRING(MAreaCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["MAreaCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "AR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(AreaCode,1,2))+1 AreaCode  FROM AMS.Area  where SUBSTRING(AreaCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["AreaCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "PG":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(GrpCode,1,2))+1 GrpCode  FROM AMS.ProductGroup  where SUBSTRING(GrpCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["GrpCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "PR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(PShortName,1,2))+1 PShortName  FROM AMS.Product  where SUBSTRING(PShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["PShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "PSG":
                {
                    try
                    {
                        cmdString =
                            $"SELECT  COUNT(SUBSTRING(ShortName,1,2))+1 ShortName  FROM AMS.ProductSubGroup  where SUBSTRING(ShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                        dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                        autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                            dt.Rows[0]["ShortName"].ToString().PadLeft(5, '0');
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }

                    break;
                }

            case "GD":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(GCode,1,2))+1 GCode  FROM AMS.Godown  where SUBSTRING(GCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["GCode"].ToString().PadLeft(5, '0');
                    break;
                }
            case "RK":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(RCode,1,2))+1 RCode  FROM AMS.RACK  where SUBSTRING(RCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["RCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "CC":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(CCCode,1,2))+1 CCCode  FROM AMS.CostCenter  where SUBSTRING(CCCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["CCCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "CR":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(CCode,1,2))+1 CCode  FROM AMS.Currency  where SUBSTRING(CCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["CCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "EC":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(EmployeeCode,1,2))+1 EmployeeCode  FROM AMS.Employee  where SUBSTRING(EmployeeCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["EmployeeCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "TM":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(Code,1,2))+1 Code  FROM AMS.Transport  where SUBSTRING(Code,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["Code"].ToString().PadLeft(5, '0');
                    break;
                }

            case "LM":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(Code,1,2))+1 Code  FROM AMS.Location  where SUBSTRING(Code,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["Code"].ToString().PadLeft(5, '0');
                    break;
                }

            case "MM":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(Code,1,2))+1 Code  FROM AMS.Machine  where SUBSTRING(Code,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["Code"].ToString().PadLeft(5, '0');
                    break;
                }

            case "FL":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(ShortName,1,2))+1 ShortName  FROM AMS.Floor  where SUBSTRING(ShortName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["ShortName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "TB":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(TableCode,1,2))+1 TableCode  FROM AMS.TableMaster  where SUBSTRING(TableCode,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["TableCode"].ToString().PadLeft(5, '0');
                    break;
                }

            case "MT":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(MemberDesc,1,2))+1 MemberDesc  FROM AMS.MemberType  where SUBSTRING(MemberDesc,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["MemberDesc"].ToString().PadLeft(5, '0');
                    break;
                }

            case "MS":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(MShipDesc,1,2))+1 MShipDesc  FROM AMS.MemberShipSetup  where SUBSTRING(MShipDesc,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["MShipDesc"].ToString().PadLeft(5, '0');
                    break;
                }

            case "CT":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(CName,1,2))+1 CName  FROM AMS.Counter  where SUBSTRING(CName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["CName"].ToString().PadLeft(5, '0');
                    break;
                }

            case "DEP":
                {
                    cmdString =
                        $"SELECT  COUNT(SUBSTRING(DName,1,2))+1 DName  FROM AMS.Department  where SUBSTRING(DName,1,2)='{autoIncrementCode.Substring(0, 2)}'";
                    dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    autoIncrementCode = autoIncrementCode.Substring(0, 2) +
                                        dt.Rows[0]["DName"].ToString().PadLeft(5, '0');
                    break;
                }

                //case "UN":
                //	cmdString = "SELECT  COUNT(SUBSTRING(UnitName,1,2))+1 UnitName  FROM AMS.ProductUnit  where SUBSTRING(UnitName,1,2)='" + AutoIncrementCode.Substring(0, 2) + "'";
                //	dt = SelectDataTablecmdString(cmdString);
                //	AutoIncrementCode = AutoIncrementCode.Substring(0, 2) + dt.Rows[0]["UnitName"].ToString().PadLeft(5, '0');
                //	break;
        }

        return autoIncrementCode;
    }

    public static void GetCompanyInfoDetails()
    {
        const string cmdString = "Select  * from AMS.CompanyInfo ";
        var table = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (table.Rows.Count <= 0)
        {
            return;
        }

        foreach (DataRow dr in table.Rows)
        {
            CompanyPrintDesc = dr["PrintDesc"].ToString();
            LogInCompany = dr["Company_Name"].ToString();
            CompanyAddress = dr["Address"].ToString();
            CompanyCountry = dr["Country"].ToString();
            CompanyPhoneNo = dr["PhoneNo"].ToString();
            CompanyPanNo = dr["Pan_No"].ToString();
            SyncBaseSync = dr["ApiKey"].GetGuid();
        }
    }

    public static void GetFiscalYearDetails()
    {
        try
        {
            DataTable dtFiscal;
            string cmdString;
            if (SysFiscalYearId is 0)
            {
                cmdString =
                    @"SELECT TOP 1 FY_Id FiscalYearId,Current_FY, Start_ADDate,End_ADDate,Start_BSDate,End_BsDate,AD_FY,BS_FY FROM AMS.FiscalYear WHERE Current_FY = 1";
                dtFiscal = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                if (dtFiscal.Rows.Count is 0)
                {
                    cmdString =
                        @"SELECT FiscalYearId,DefaultYear Current_FY,StartADDate Start_ADDate,EndADDate End_ADDate,StartBSDate Start_BSDate,EndBSDate End_BsDate,ADFiscalYear AD_FY,BSFiscalYear BS_FY  FROM AMS.FiscalYear WHERE GETDATE() BETWEEN StartADDate AND EndADDate";
                    dtFiscal = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
                    if (dtFiscal.Rows.Count is 0)
                    {
                        return;
                    }
                }
            }
            else
            {
                cmdString =
                    $@"SELECT TOP 1 FiscalYearId ,DefaultYear Current_FY,StartADDate Start_ADDate,EndADDate End_ADDate,StartBSDate Start_BSDate,EndBSDate End_BsDate,ADFiscalYear AD_FY,BSFiscalYear BS_FY FROM master.AMS.FiscalYear WHERE FiscalYearId ={SysFiscalYearId} ";
                dtFiscal = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                if (dtFiscal.Rows.Count is 0)
                {
                    var cmdText =
                        $@"SELECT TOP 1 FY_Id FiscalYearId,Current_FY, Start_ADDate,End_ADDate,Start_BSDate,End_BsDate,AD_FY,BS_FY FROM AMS.FiscalYear WHERE FY_Id ={SysFiscalYearId}  ";
                    dtFiscal = SqlExtensions.ExecuteDataSetOnMaster(cmdText).Tables[0];
                }
            }

            if (SysFiscalYearId is 0)
            {
                SysFiscalYearId = dtFiscal.Rows[0]["FiscalYearId"].GetInt();
            }
            CfStartAdDate = dtFiscal.Rows[0]["Start_ADDate"].GetDateTime();
            CfEndAdDate = dtFiscal.Rows[0]["End_ADDate"].GetDateTime();
            CfStartBsDate = dtFiscal.Rows[0]["Start_BSDate"].ToString();
            CfEndBsDate = dtFiscal.Rows[0]["End_BSDate"].ToString();
            SysFiscalYear = dtFiscal.Rows[0]["AD_FY"].GetString();
            SysBsFiscalYear = dtFiscal.Rows[0]["BS_FY"].GetString();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
        }
    }

    #endregion ------------------ Return datatable value & other ------------------

    // ENCRYPT AND DECRYPT

    #region --------------- Encrypt and Decrypt ---------------

    public static string Encrypt(string toBeEncrypted)
    {
        var rhinelandCipher = new RijndaelManaged();
        const string password = "@A@M@S-Acc#";
        var plainText = Encoding.Unicode.GetBytes(toBeEncrypted);
        var salt = Encoding.ASCII.GetBytes(password.Length.ToString());
        var secretKey = new PasswordDeriveBytes(password, salt);
        var encrypt = rhinelandCipher.CreateEncryptor(secretKey?.GetBytes(32), secretKey?.GetBytes(16));
        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(memoryStream, encrypt, CryptoStreamMode.Write);
        cryptoStream.Write(plainText, 0, plainText.Length);
        cryptoStream.FlushFinalBlock();
        var cipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        var encryptedData = Convert.ToBase64String(cipherBytes);
        return encryptedData;
    }

    public static string Decrypt(string toBeDecrypted)
    {
        var rhinelandCipher = new RijndaelManaged();
        const string password = "@A@M@S-Acc#";
        var decryptedData = string.Empty;
        if (string.IsNullOrEmpty(toBeDecrypted))
        {
            return decryptedData;
        }

        try
        {
            var encryptedData = Convert.FromBase64String(toBeDecrypted);
            var salt = Encoding.ASCII.GetBytes(password.Length.ToString());
            var secretKey = new PasswordDeriveBytes(password, salt);
            var decryption = rhinelandCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey?.GetBytes(16));
            var memoryStream = new MemoryStream(encryptedData);
            var cryptoStream = new CryptoStream(memoryStream, decryption, CryptoStreamMode.Read);
            var plainText = new byte[encryptedData.Length];
            var decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
            memoryStream.Close();
            cryptoStream.Close();
            decryptedData = Encoding.Unicode.GetString(plainText, 0, decryptedCount);
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            decryptedData = toBeDecrypted;
        }
        return decryptedData;
    }

    #endregion --------------- Encrypt and Decrypt ---------------

    // PICTURE CUSTOM

    #region ------------- Picture ---------------

    public static PictureBox Box;
    public static string Title;
    public static string SearchKey;

    public static PictureBox FetchPic(PictureBox pic, string title)
    {
        Box = new PictureBox();
        if (pic?.Image != null)
        {
            Box.Image = pic.Image;
        }
        else if (pic?.BackgroundImage != null)
        {
        }

        return pic;
    }

    public byte[] GetPic(Image img, PictureBox picture)
    {
        byte[] data = null;
        try
        {
            using var stream = new MemoryStream();
            var bmp = new Bitmap(picture.Image);
            bmp.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return data;
        }
    }

    public static byte[] ReadFile(string sPath)
    {
        byte[] data = null;
        if (string.IsNullOrEmpty(sPath))
        {
            return null;
        }

        try
        {
            var fInfo = new FileInfo(sPath);
            var numBytes = fInfo.Length;
            var fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return data;
        }

        return data;
    }

    public static void PreviewPicture(PictureBox pic, string title)
    {
        Box = null;
        Box = pic;
        Title = title;
        var fp = new FrmPreview(pic);
        fp.ShowDialog();
    }

    #endregion ------------- Picture ---------------

    // OBJECT FOR THIS CLASS

    #region --------------- Project ---------------

    public static string Caption { get; set; } = @"MrSolution";
    public static string Project { get; set; } = @"MRSOLUTION"; // @"SIYZO" //  @"RUDRALEKHA" // MRSOLUTION // ;

    #endregion --------------- Project ---------------

    // IRD CONFIGURATION

    #region --------------- IRD Config ---------------

    public static bool IsIrdRegister { get; set; }
    public static bool IsOnlineMode { get; set; }
    public static string IrdApiAddress { get; set; } = "https://cbapi.ird.gov.np";
    public static string CloudSyncBaseUrl { get; set; } = "https://api.mrsolution.com.np/api/";
    public static string IrdUser { get; set; } = "Test_CBMS";
    public static string IrdUserPassword { get; set; } = "test@321";
    public static string IrdCompanyPan { get; set; } = "999999999";

    public static string IsIrdApproved { get; set; } = string.Empty;

    public static Guid RegistrationId { get; set; }

    #endregion --------------- IRD Config ---------------

    // SERVER CONFIGURATION

    #region --------------- ServerConfig ---------------

    public static string DataSource { get; set; } = string.Empty;
    public static string ServerUser { get; set; } = string.Empty;
    public static int ServerVersion { get; set; } = 8;
    public static string InitialCatalog { get; set; } = string.Empty;
    public static Guid? LocalOriginId { get; set; }
    public static string ServerPassword { get; set; } = string.Empty;
    public static bool MultiServerOption { get; set; } = true;
    public static bool MultiServerEdit { get; set; } = false;
    public static string ConnectionString { get; set; }

    #endregion --------------- ServerConfig ---------------

    //SYSTEM CONFIGURATION

    #region ------------------ SYSTEM CONFIGURATION ------------------

    // LOGIN USER INFO

    #region *----- LoginUserInfo -----*

    public static int LogInUserId { get; set; }
    public static int LogInUserValidDays { get; set; }
    public static int RoleId { get; set; }
    public static int TotalUser { get; set; } = 1;

    public static long UserLedgerId { get; set; } = 0;

    public static bool UserAllowPosting { get; set; }
    public static bool UserAuditLog { get; set; }
    public static bool ChangeQty { get; set; }
    public static bool UserAuthorized { get; set; }
    public static bool UserPdcDashBoard { get; set; }
    public static bool UserChangeRate { get; set; }
    public static bool UserModify { get; set; }
    public static bool UserDelete { get; set; }

    public static string LogInUser { get; set; }

    public static string[] DomainLoginUser { get; set; } =
    [
        "AMSADMIN",
        "SYSADMIN",
    ];

    public static string LogInUserPassword { get; set; }
    public static string LogInUserFullName { get; set; }
    public static string LogInUserCategory { get; set; }

    public static DateTime LogInUserPostingStartDate { get; set; }
    public static DateTime LogInUserPostingEndDate { get; set; }
    public static DateTime LogInUserModifyStartDate { get; set; }
    public static DateTime LogInUserModifyEndDate { get; set; }
    public static IList<UserPermissionModel> UacPermissions { get; set; }

    public static bool IsPermissionAllowed(UacAccessFeature feature, UacAction? action)
    {
        if (UacPermissions == null)
        {
            throw new NoNullAllowedException("Global permissions not initialized. Please check ObjGlobal.");
        }

        if (action == null)
        {
            return UacPermissions.FirstOrDefault(x => x.Feature == feature) != null;
        }

        try
        {
            var model = UacPermissions.FirstOrDefault(x => x.Feature == feature);
            if (model == null)
            {
                return false;
            }

            var config = XmlUtils.XmlDeserialize<dynamic>(model.ConfigXml);
            return config.Actions is IList<UacAction> actions && actions.Contains(action.Value);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("MrDAL.Domains.Shared.UserAccessControl");
            return false;
        }
    }

    #endregion *----- LoginUserInfo -----*

    // COMPANY INFO

    #region *----- CompanyInfo -----*

    public static int CompanyId { get; set; }
    public static int SysBranchId { get; set; }
    public static string CompanyVersion { get; set; } = "2025.0110";
    public static bool FCompanySetup { get; set; }
    public static bool IsOnlineSync { get; set; } = false;

    public static string CompanyPrintDesc { get; set; }
    public static string LogInCompany { get; set; }
    public static string CompanyAddress { get; set; }
    public static string CompanyCountry { get; set; }
    public static string CompanyState { get; set; }
    public static string CompanyCity { get; set; }
    public static string CompanyPanNo { get; set; }
    public static string CompanyEmailAddress { get; set; }
    public static string CompanyWebSites { get; set; }
    public static string CompanyPhoneNo { get; set; }
    public static string CompanyFaxNo { get; set; }
    public static string SysBranchName { get; set; }
    public static string CompanyUnitName { get; set; }

    public static string BsCurrentDate { get; set; }
    public static string SysFiscalYear { get; set; }
    public static string SysBsFiscalYear { get; set; }
    public static string CfStartBsDate { get; set; }
    public static string CfEndBsDate { get; set; }
    public static string SoftwareModule { get; set; }
    public static DateTime CompanyLoginDateTime { get; set; }
    public static DateTime RegistrationDate { get; set; }
    public static byte CompanyLogo { get; set; }
    public static Guid? SyncBaseSync { get; set; }

    #endregion *----- CompanyInfo -----*

    public int Sn { get; set; }
    public static int RegisterLicenseNodes { get; set; }
    public static int SysFontSize { get; set; }
    public static int SysOpenLoginPage { get; set; }
    public static int SysOperatingCounter { get; set; }
    public static int SystemSettingId { get; set; }
    public static int SysCurrencyId { get; set; }
    public static int SysNoOfCopy { get; set; } = 1;
    public static int SysAmountLength { get; set; }
    public static int SysRateLength { get; set; }
    public static int SysCurrencyLength { get; set; }
    public static int SysQtyLength { get; set; }
    public static int SysTotalTermAmt { get; set; } = 0;
    public static int SysFiscalYearId { get; set; }
    public static int SysCompanyUnitId { get; set; }
    public static int SysBackupDays { get; set; }
    public static int RemainingDays { get; set; }
    public static bool IsNewInstallation { get; set; }

    public static long SysDebtorGroupId { get; set; }
    public static long SysCreditorGroupId { get; set; }
    public static long SysSalaryLedgerId { get; set; }
    public static long SysTdsLedgerId { get; set; }
    public static long SysPfLedgerId { get; set; }
    public static bool SysConfirmSave { get; set; }
    public static bool SysConfirmCancel { get; set; }
    public static bool SysConfirmExit { get; set; }
    public static bool SysConfirmUpdate { get; set; }
    public static bool SysConfirmDelete { get; set; }
    public static bool IsLicenseExpire { get; set; } = true;
    public static bool SysIsNightAudit { get; set; }

    public static bool SysIsAlphaSearch { get; set; }
    public static bool SysIsBarcodeSearch { get; set; }
    public static bool SysCheckProfile { get; set; } = false;
    public static bool SysAlarm { get; set; } = false;
    public static bool IsKotBotPrint { get; set; } = true;
    public static bool SingleColumnSearch { get; set; } = false;
    public static bool AllowPosting { get; set; }
    public static bool IsFirstTime { get; set; } = false;
    public static bool SysTaxBilling { get; set; }
    public static bool SysCurrentFiscalYear { get; set; }
    public static bool SysAuditTrial { get; set; }
    public static bool SysUdf { get; set; }
    public static bool SysIsOrderPrint { get; set; }
    public static bool SysIsInvoicePrint { get; set; }
    public static bool SysIsBranchPrint { get; set; }
    public static bool SysAutoPopup { get; set; } = true;
    public static bool SysFormCloseConfirmation { get; set; } = false;
    public static bool SysPrintingDateTime { get; set; }
    public static bool SysCurrentDate { get; set; } = true;
    public static bool SysIsDateWise { get; set; }

    public static TimeSpan? NightAuditEndTime { get; set; }

    public static string SearchText { get; set; } = string.Empty;
    public static string ZoomTableId { get; set; }
    public static string SysCurrency { get; set; }
    public static string SysCurrencyRate { get; set; }
    public static string SysDateType { get; set; }
    public static string SysDefaultPrinter { get; set; }
    public static string SysDefaultOrderPrinter { get; set; }
    public static string SysDefaultOrderDesign { get; set; }
    public static string SysDefaultPreInvoiceDesign { get; set; }
    public static string SysDefaultOrderDocNumbering { get; set; } = "RESTRO ORDER";
    public static string SysDefaultInvoicePrinter { get; set; }
    public static string SysDefaultAbtInvoiceDesign { get; set; }
    public static string SysDefaultInvoiceDesign { get; set; }
    public static string SysDefaultInvoiceDocNumbering { get; set; } = "SALES INVOICE";
    public static string SysDefaultAbtInvoiceDocNumbering { get; set; } = "SALES INVOICE";
    public static string SysBackupPath { get; set; }
    public static string SysAmountFormat { get; set; } = "0.00";
    public static string SysRateFormat { get; set; } = "0.00";

    public static string SysAmountCommaFormat { get; set; }
    public static string SysRateCommaFormat { get; set; }
    public static string SysQtyCommaFormat { get; set; }
    public static string SysQtyFormat { get; set; } = "0.00";
    public static string SysCurrencyFormat { get; set; } = "0.00";
    public static string SysFontName { get; set; }
    public static string SysPaperSize { get; set; }
    public static string SysReportStyle { get; set; }
    public static string SysDefaultFormColor { get; set; }
    public static string SysDefaultTextColor { get; set; }
    public static string SysTermsSnRatePercentAmount { get; set; } = string.Empty;
    public static string SysExistBillTermData { get; set; } = string.Empty;
    public static string SysDebtorAc { get; set; }
    public static string SysCreditorAc { get; set; }
    public static string SysSalaryAc { get; set; }
    public static string SysPfAc { get; set; }
    public static string SysTdsAc { get; set; }
    public static string SysEmailAddress { get; set; }
    public static string SysEmailPassword { get; set; }
    public static string SystemSerialNo { get; set; }
    public static string SystemMacAddress { get; set; }
    public static string SystemHostName { get; set; }
    public static string ComputerName { get; set; }
    public static string ComputerAddress { get; set; }

    public string DisplayMember { get; set; }
    public string ValueMember { get; set; }

    public static Guid? LicenseRegistrationId { get; set; }
    public static DateTime ExpiryLicenseDate { get; set; }
    public static DateTime SysLicenseRenewalDate { get; set; }
    public static DateTime SysLicenseValidDate { get; set; }
    public static DateTime SysLicenseIssueDate { get; set; }
    public static DateTime SysPostingStartDate { get; set; } = Convert.ToDateTime("01/01/1753");
    public static DateTime SysPostingEndDate { get; set; } = Convert.ToDateTime("01/01/1753");

    public static DateTime CfStartAdDate { get; set; }
    public static DateTime CfEndAdDate { get; set; }
    public static DateTime NightAuditDate { get; set; }

    #endregion ------------------ SYSTEM CONFIGURATION ------------------

    // PURCHASE CONFIGURATION

    #region ------------------ PURCHASE CONFIGURATION  ------------------

    public static int PurchaseVatTermId { get; set; }
    public static int PurchaseProductDiscountTermId { get; set; }
    public static int PurchaseDiscountTermId { get; set; }
    public static int PurchaseAdditionalVatTermId { get; set; }
    public static int PurchaseCreditBalanceWarning { get; set; }
    public static int PurchaseCreditDaysWarning { get; set; }

    public static long PurchaseLedgerId { get; set; }
    public static long PurchaseReturnLedgerId { get; set; }

    public static bool PurchaseOrderEnable { get; set; }
    public static bool PurchaseOrderMandatory { get; set; }
    public static bool PurchaseChallanEnable { get; set; }
    public static bool PurchaseChallanMandatory { get; set; }
    public static bool PurchaseSubLedgerEnable { get; set; }
    public static bool PurchaseSubLedgerMandatory { get; set; }
    public static bool PurchaseAgentEnable { get; set; }
    public static bool PurchaseAgentMandatory { get; set; }
    public static bool PurchaseCurrencyEnable { get; set; }
    public static bool PurchaseCurrencyRateChange { get; set; }
    public static bool PurchaseCurrencyMandatory { get; set; }
    public static bool PurchaseGodownEnable { get; set; }
    public static bool PurchaseGodownMandatory { get; set; }
    public static bool PurchaseUnitEnable { get; set; }
    public static bool PurchaseUnitMandatory { get; set; }
    public static bool PurchaseAltUnitEnable { get; set; }
    public static bool PurchaseAltUnitMandatory { get; set; }
    public static bool PurchaseBasicAmountEnable { get; set; }
    public static bool PurchaseBasicAmountMandatory { get; set; }
    public static bool PurchaseDescriptionsEnable { get; set; }
    public static bool PurchaseDescriptionsMandatory { get; set; }
    public static bool PurchaseRemarksEnable { get; set; }
    public static bool PurchaseRemarksMandatory { get; set; }
    public static bool PurchaseDepartmentEnable { get; set; }
    public static bool PurchaseDepartmentMandatory { get; set; }
    public static bool PurchaseIndentEnable { get; set; }
    public static bool PurchaseIndentMandatory { get; set; }
    public static bool PurchaseQuotationEnable { get; set; }
    public static bool PurchaseQuotationMandatory { get; set; }
    public static bool PurchaseInvoiceDateEnable { get; set; }
    public static bool PurchaseUpdateRateEnable { get; set; }
    public static bool PurchaseCarryRate { get; set; }
    public static bool PurchaseChangeRate { get; set; }
    public static bool PurchaseLastRate { get; set; }
    public static bool PurchaseBeforeVat { get; set; }
    public static bool PurchaseBatchRate { get; set; }
    public static bool PurchaseQualityControl { get; set; }
    public static bool PurchaseProductGrpWiseBilling { get; set; }

    #endregion ------------------ PURCHASE CONFIGURATION  ------------------

    // SALES CONFIGURATION

    #region ------------------ SALES CONFIGURATION ------------------

    public static int SalesVatTermId { get; set; }
    public static int SalesDiscountTermId { get; set; }
    public static int SalesSpecialDiscountTermId { get; set; }
    public static int SalesAdditionalTerm { get; set; }
    public static int SalesServiceChargeTermId { get; set; }
    public static int SalesCreditBalanceWarning { get; set; }
    public static int SalesCreditDaysWarning { get; set; }
    public static int SalesCancellationProductId { get; set; }

    public static long SalesLedgerId { get; set; }
    public static long SalesReturnLedgerId { get; set; }
    public static long SalesCancellationCustomerId { get; set; }

    public static bool SalesChangeRate { get; set; }
    public static bool SalesLastRate { get; set; }
    public static bool SalesBeforeVat { get; set; }
    public static bool SalesCarryRate { get; set; }
    public static bool SalesStockValueInSalesReturn { get; set; }
    public static bool SalesAvailableStock { get; set; }
    public static bool SalesPartyName { get; set; }
    public static bool SalesProductGrpWiseBilling { get; set; }
    public static bool SalesDispatchOrderMandatory { get; set; }
    public static bool SalesTenderAmount { get; set; }
    public static bool SalesOrderEnable { get; set; }
    public static bool SalesOrderMandatory { get; set; }
    public static bool SalesChallanEnable { get; set; }
    public static bool SalesChallanMandatory { get; set; }
    public static bool SalesSubLedgerEnable { get; set; }
    public static bool SalesSubLedgerMandatory { get; set; }
    public static bool SalesAgentEnable { get; set; }
    public static bool SalesAgentMandatory { get; set; }
    public static bool SalesCurrencyEnable { get; set; }
    public static bool SalesCurrencyMandatory { get; set; }
    public static bool SalesCurrencyRateChange { get; set; }
    public static bool SalesGodownEnable { get; set; }
    public static bool SalesGodownMandatory { get; set; }
    public static bool SalesUnitEnable { get; set; }
    public static bool SalesUnitMandatory { get; set; }
    public static bool SalesAltUnitEnable { get; set; }
    public static bool SalesAltUnitMandatory { get; set; }
    public static bool SalesBasicAmountEnable { get; set; }
    public static bool SalesBasicAmountMandatory { get; set; }
    public static bool SalesDescriptionsEnable { get; set; }
    public static bool SalesDescriptionsMandatory { get; set; }
    public static bool SalesRemarksEnable { get; set; }
    public static bool SalesRemarksMandatory { get; set; }
    public static bool SalesDepartmentEnable { get; set; }
    public static bool SalesDepartmentMandatory { get; set; }
    public static bool SalesQuotationEnable { get; set; }
    public static bool SalesQuotationMandatory { get; set; }
    public static bool SalesDispatchOrderEnable { get; set; }
    public static bool SalesInvoiceDateEnable { get; set; }
    public static bool SalesIndentEnable { get; set; }
    public static bool SalesUpdateRate { get; set; }
    public static bool SalesIgnoreTenderAmount { get; set; }

    #endregion ------------------ SALES CONFIGURATION ------------------

    // STOCK CONFIGURATION

    #region ------------------ STOCK CONFIGURATION ------------------

    public static long StockOpeningStockLedgerId { get; set; }
    public static long StockClosingStockLedgerId { get; set; }
    public static long StockStockInHandLedgerId { get; set; }

    public static bool StockNegativeStockWarning { get; set; }
    public static bool StockNegativeStockBlock { get; set; }
    public static bool StockGodownWiseCategory { get; set; }
    public static bool StockGroupWiseCategory { get; set; }
    public static bool StockSubGroupWiseCategory { get; set; }
    public static bool StockShortNameWise { get; set; }
    public static bool StockAltQtyAlteration { get; set; }
    public static bool StockAlterationPart { get; set; }
    public static bool StockCarryBatchQty { get; set; }
    public static bool StockBreakupQty { get; set; }
    public static bool StockMfgDate { get; set; }
    public static bool StockExpiryDate { get; set; }
    public static bool StockMfgDateValidation { get; set; }
    public static bool StockExpiryDateValidation { get; set; }
    public static bool StockFreeQty { get; set; }
    public static bool StockExtraFreeQty { get; set; }
    public static bool StockGroupWiseFilter { get; set; }
    public static bool StockGodownWiseStockFilter { get; set; }
    public static bool StockFinishedQty { get; set; }
    public static bool StockEqualQty { get; set; }
    public static bool StockItemGodownWiseFilter { get; set; }
    public static bool StockCostCenterEnable { get; set; }
    public static bool StockCostCenterMandatory { get; set; }
    public static bool StockCostCenterItemEnable { get; set; }
    public static bool StockCostCenterItemMandatory { get; set; }
    public static bool StockGodownEnable { get; set; }
    public static bool StockGodownMandatory { get; set; }
    public static bool StockGodownItemEnable { get; set; }
    public static bool StockGodownItemMandatory { get; set; }
    public static bool StockUnitEnable { get; set; }
    public static bool StockUnitMandatory { get; set; }
    public static bool StockAltUnitEnable { get; set; }
    public static bool StockAltUnitMandatory { get; set; }
    public static bool StockRemarksEnable { get; set; }
    public static bool StockRemarksMandatory { get; set; }
    public static bool StockDescriptionsEnable { get; set; }
    public static bool StockDescriptionsMandatory { get; set; }
    public static bool StockDepartmentEnable { get; set; }
    public static bool StockDepartmentMandatory { get; set; }
    public static bool StockAvailableStock { get; set; }

    #endregion ------------------ STOCK CONFIGURATION ------------------

    // FINANCE CONFIGURATION

    #region ------------------ FINANCE CONFIGURATION ------------------

    public static long FinanceProfitLossLedgerId { get; set; }
    public static long FinanceCashLedgerId { get; set; }
    public static long FinanceBankLedgerId { get; set; }
    public static long FinanceCardLedgerId { get; set; }
    public static long FinanceFonePayLedgerId { get; set; }
    public static long FinanceEsewaLedgerId { get; set; }
    public static long FinanceKhaltiLedgerId { get; set; }
    public static long FinanceRemitLedgerId { get; set; }
    public static long FinanceConnectIpsLedgerId { get; set; }
    public static long FinancePartialLedgerId { get; set; }
    public static long FinanaceGiftVoucherLedgerId { get; set; }
    public static long FinanceVatLedgerId { get; set; }

    public static bool FinanceShortNameWise { get; set; }
    public static bool FinanceNegativeWarning { get; set; }
    public static bool FinancePrintingDateTime { get; set; }
    public static bool FinanceCurrencyEnable { get; set; }
    public static bool FinanceCurrencyMandatory { get; set; }
    public static bool FinanceAgentEnable { get; set; }
    public static bool FinanceAgentMandatory { get; set; }
    public static bool FinanceDepartmentEnable { get; set; }
    public static bool FinanceDepartmentItemEnable { get; set; }
    public static bool FinanceDepartmentItemMandatory { get; set; }
    public static bool FinanceDepartmentMandatory { get; set; }
    public static bool FinanceSubLedgerEnable { get; set; }
    public static bool FinanceSubLedgerMandatory { get; set; }
    public static bool FinanceRemarksEnable { get; set; }
    public static bool FinanceRemarksMandatory { get; set; }
    public static bool FinanceVoucherDateEnable { get; set; }
    public static bool FinanceNarrationMandatory { get; set; }
    public static string FinanceNegativeTransaction { get; set; }

    #endregion ------------------ FINANCE CONFIGURATION ------------------

    // FINANCE CONFIGURATION

    #region ------------------ FINANCE CONFIGURATION ------------------

    public static bool IsBranchSync { get; set; }
    public static bool IsGeneralLedgerSync { get; set; }
    public static bool IsTableIdSync { get; set; }
    public static bool IsAreaSync { get; set; }
    public static bool IsBillingTermSync { get; set; }
    public static bool IsAgentSync { get; set; }
    public static bool IsProductSync { get; set; }
    public static bool IsCostCenterSync { get; set; }
    public static bool IsMemberSync { get; set; }
    public static bool IsCashBankSync { get; set; }
    public static bool IsJournalVoucherSync { get; set; }
    public static bool IsNotesRegisterSync { get; set; }
    public static bool IsPDCVoucherSync { get; set; }
    public static bool IsLedgerOpeningSync { get; set; }
    public static bool IsProductOpeningSync { get; set; }
    public static bool IsSalesQuotationSync { get; set; }
    public static bool IsSalesOrderSync { get; set; }
    public static bool IsSalesChallanSync { get; set; }
    public static bool IsSalesInvoiceSync { get; set; }
    public static bool IsSalesReturnSync { get; set; }
    public static bool IsSalesAdditionalSync { get; set; }
    public static bool IsPurchaseIndentSync { get; set; }
    public static bool IsPurchaseOrderSync { get; set; }
    public static bool IsPurchaseChallanSync { get; set; }
    public static bool IsPurchaseInvoiceSync { get; set; }
    public static bool IsPurchaseReturnSync { get; set; }
    public static bool IsPurchaseAdditionalSync { get; set; }
    public static bool IsStockAdjustmentSync { get; set; }
    public static string SyncAPISync { get; set; }
    public static string SyncOrginIdSync { get; set; }

    #endregion ------------------ FINANCE CONFIGURATION ------------------
}