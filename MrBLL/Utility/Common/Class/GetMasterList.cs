using MrBLL.DataEntry.Common;
using MrBLL.Domains.Hospital.Master;
using MrBLL.Domains.POS.Master;
using MrBLL.Domains.Restro.Master;
using MrBLL.Domains.SchoolTime.Stationary;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrBLL.Setup.BranchSetup;
using MrBLL.Setup.BusinessUnit;
using MrBLL.Setup.CompanySetup;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.Common.Class;

public static class GetMasterList
{
    // SELECT MASTER LIST AND GET VALUE

    public static (string description, int id) GetCompanyList(string actionTag)
    {
        string[] value = ["MED", "COMPANY", actionTag, ObjGlobal.SearchText, string.Empty, "LIST"];
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList(value);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["CompanyName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["CompanyId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning("COMPANY LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    public static (string description, int id) GetUserInfoList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "USER", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["User_Name"].GetString();
                descriptionId = frmPickList.SelectedList[0]["User_Id"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"LOGIN USER LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }


    // GENERAL LEDGER DETAILS
    public static (string description, int id) GetBranchList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "BRANCH", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["ValueName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["ValueId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            description.WarningMessage(@"BRANCH LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetFiscalYearList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "FISCALYEAR", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["ValueName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["ValueId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            description.WarningMessage(@"BRANCH LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    
    public static (string description, int id) GetSalesTermList(string actionTag, string category)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "SALESTERM", actionTag, ObjGlobal.SearchKey, category, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES BILLING TERM LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetPurchaseTermList(string actionTag, string category)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList =
            new FrmAutoPopList("MED", "PURCHASETERM", actionTag, ObjGlobal.SearchKey, category, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PURCHASE BILLING TERM LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetDepartmentList(string action, string type = "DEPARTMENT")
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "DEPARTMENT", action, ObjGlobal.SearchKey, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@$"{type} LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetHospitalDepartmentList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "HDEPARTMENT", action, ObjGlobal.SearchKey, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@$"DEPARTMENT LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetDoctorList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "DOCTOR", action, ObjGlobal.SearchKey, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@$"DOCTOR LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetAccountGroupList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "ACCOUNTGROUP", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ACCOUNT GROUP LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetAccountSubGroupList(string action, int groupId)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "ACCOUNTSUBGROUP", action, ObjGlobal.SearchText,
            groupId.ToString(), "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ACCOUNT SUB GROUP LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    public static (string description, int id) GetAccountSubGroupLists(string action, int groupId)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "ACCOUNTSUBGROUPLIST", action, ObjGlobal.SearchText,
            groupId.ToString(), "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ACCOUNT SUB GROUP LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    public static (string description, int id) GetSubLedgerList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "SUBLEDGER", action, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SUB LEDGER LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetSeniorAgentList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "MAINAGENT", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"AGENT LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetAgentList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "JUNIORAGENT", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"AGENT LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id, string country) GetMainAreaList(string actionTag)
    {
        var description = new TextBox();
        var country = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "MAINAREA", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                country.Text = frmPickList.SelectedList[0]["MCountry"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"AREA LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId, country.Text);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId, country.Text);
    }

    public static (string description, int id) GetAreaList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "AREA", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"AREA LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetSchemeList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "SCHEME", actionTag, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SCHEME LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id, string currencyRate) GetCurrencyList(string actionTag)
    {
        var description = new TextBox();
        var exchangeRate = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "CURRENCY", actionTag, ObjGlobal.SearchText, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["ShortName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
                exchangeRate.Text = frmPickList.SelectedList[0]["CRate"].GetDecimalString();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CURRENCY LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId, 1.GetDecimalString());
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId, exchangeRate.Text);
    }

    public static (string description, long id) GetGeneralLedger(string action, string category = "", string module = "MASTER")
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", action, ObjGlobal.SearchText, category, module, DateTime.Now.GetDateString(), true);
        if (FrmAutoPopList.GetListTable is { Rows: { Count: > 0 } })
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GENERAL LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetPartyLedger(string action, string category = "")
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "PARTYLEDGER", action, ObjGlobal.SearchText, category, "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"GENERAL LEDGER ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }
        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetGeneralLedger(string action, string category, string mskDate, string module)
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", action, ObjGlobal.SearchText, category, module, mskDate, true);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GENERAL LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }
        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetOpeningGeneralLedger(string action, string mskDate)
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "OPENINGLEDGER", action, ObjGlobal.SearchKey, "ALL", "MASTER",
            mskDate.GetSystemDate(), string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GENERAL LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetPatient(string action)
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "PATIENT", action, ObjGlobal.SearchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["ShortName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PATIENT LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    // PRODUCT DETAILS
    public static (string description, int id) GetGodown(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "GODOWN", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GODOWN LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetTableList(string action, string category)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "TABLE", action, ObjGlobal.SearchText, category, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["TableName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["TableId"].GetInt();
            }
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"TABLE LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetCounterList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "COUNTER", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COUNTER / TERMINAL LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetFloorList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "FLOOR", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"FLOOR LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetCostCenterList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "COSTCENTER", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COST CENTER LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id, decimal margin) GetProductGroup(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        decimal margin = 0;
        var nepaliDesc = new TextBox();
        var frmPickList = new FrmAutoPopList("MED", "PRODUCTGROUP", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
                margin = frmPickList.SelectedList[0]["Margin"].GetInt();
                //nepaliDesc = frmPickList.SelectedList[0]["NapaliDesc"].GetString();
            }
            frmPickList.Dispose();
        }
        else
        {
            description.WarningMessage(@"PRODUCT GROUP ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId, 0);
        }
        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId, margin);
    }

    public static (string description, int id) GetProductSubGroup(string action, int groupId)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "PRODUCTSUBGROUP", action, ObjGlobal.SearchText, groupId.ToString(), "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT SUB GROUP ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    public static (string description, int id) GetProductSubGroups(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "PRODUCTSUBGROUP", action, ObjGlobal.SearchText, "", "DETAILS");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT SUB GROUP ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
    public static (string description, int id) GetProductUnit(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MIN", "PRODUCTUNIT", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT UNIT ARE NOT FOUND..!!");
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetProduct(string action, string mskDate, string category = "ALL", string reportMode = "")
    {
        var description = new TextBox();
        long descriptionId = 0;

        var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", ObjGlobal.SearchText, action, category, "MASTER", "", mskDate);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT ARE NOT FOUND..!!");
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetRestroProduct(string action, string mskDate, string category = "ALL", string reportMode = "")
    {
        var description = new TextBox();
        long descriptionId = 0;

        var frmPickList = new FrmAutoPopList("MAX", "RESTROPRODUCT", ObjGlobal.SearchText, action, category,
            "MASTER", mskDate, string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT ARE NOT FOUND..!!");
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string Text, long descriptionId) GetFinishedProduct(string action, string mskDate, string category = "ALL", string reportMode = "")
    {
        var description = new TextBox();
        long descriptionId = 0;

        var frmPickList = new FrmAutoPopList("MAX", "FINISEDPRODUCT", ObjGlobal.SearchText, action, category, "MASTER", "", mskDate);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT ARE NOT FOUND..!!");
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string Text, long descriptionId) GetRawProduct(string action, string mskDate, string category = "ALL", string reportMode = "")
    {
        var description = new TextBox();
        long descriptionId = 0;

        var frmPickList = new FrmAutoPopList("MAX", "RAWPRODUCT", ObjGlobal.SearchText, action, category, "MASTER", "", mskDate);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT ARE NOT FOUND..!!");
            description.Focus();
            return (description.Text, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetMasterProduct(string action)
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "MASTERPRODUCT", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable is { Rows: { Count: > 0 } })
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT ARE NOT FOUND..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) GetCounterProduct(string txtBarcode)
    {
        var description = new TextBox();
        long descriptionId = 0;
        var frm = new FrmSearchCProduct("CounterProduct", "Product List", txtBarcode, "('Inventory','Service','Counter')", true);
        frm.ShowDialog();
        if (frm.SelectList.Count > 0)
        {
            description.Text = frm.SelectList[0]["PName"].GetString();
            descriptionId = frm.SelectList[0]["PID"].GetLong();
            description.Focus();
        }
        frm.Dispose();
        return (description.Text, descriptionId);
    }

    public static (string description, string rate) GetProductBatchList(string action, long productId)
    {
        var description = new TextBox();
        var rate = new TextBox();
        var frmPickList = new FrmAutoPopList("MED", "PRODUCTBATCH", action, ObjGlobal.SearchText, productId.ToString(), "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                var days = frmPickList.SelectedList[0]["Days"].GetInt();
                if (days < 0)
                {
                    description.WarningMessage("SELECTED BATCH IS EXPIRED..!!");
                    return (description.Text, rate.Text);
                }
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                rate.Text = frmPickList.SelectedList[0]["Rate"].GetString();
            }
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRODUCT BATCH ARE NOT AVAILABLE..!!");
            description.Focus();
            return (description.Text, rate.Text);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, rate.Text);
    }

    public static (string description, int id) GetMemberShipList(string action)
    {
        var description = new TextBox();
        int descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "MEMBERSHIP", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"MEMBER LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetMemberShipTypeList(string action)
    {
        var description = new TextBox();
        int descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "MEMBERTYPE", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"MEMBER LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) GetGiftVoucherList(string action)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "GIFT VOUCHER", action, ObjGlobal.SearchKey, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["ShortName"].GetString();
                descriptionId = frmPickList.SelectedList[0]["LedgerId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@$"GIFT VOUCHER LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    // OTHERS
    public static string GetNarrationOfProduct(string actionTag, string existingNarration)
    {
        var description = new TextBox();
        var frm = new FrmAddDescriptions
        {
            Descriptions = existingNarration
        };
        frm.ShowDialog();
        description.Text = frm.Descriptions;
        description.Focus();
        return description.Text;
    }

    public static string GetPrinterList(string actionTag)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "PRINTER", actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"INSTALL PRINTER ARE NOT AVAILABLE..!!");
            description.Focus();
            return string.Empty;
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return description.Text;
    }

    public static string GetPdcBankDescription()
    {
        var bankDescription = string.Empty;
        const string script = "SELECT DISTINCT pdc.BankName Description,0 LedgerId  FROM AMS.PostDateCheque pdc";
        var frmPickList = new FrmAutoPopList(script);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            bankDescription = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => bankDescription
            };
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"BANK LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return bankDescription;
        }

        ObjGlobal.SearchText = string.Empty;
        return bankDescription;
    }

    public static string GetPdcBankBranch()
    {
        var bankDescription = string.Empty;
        const string script = "SELECT DISTINCT pdc.BranchName Description,0 LedgerId FROM AMS.PostDateCheque pdc";
        var frmPickList = new FrmAutoPopList(script);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            bankDescription = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => bankDescription
            };
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"BANK BRANCH NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return bankDescription;
        }

        ObjGlobal.SearchText = string.Empty;
        return bankDescription;
    }

    public static string GetPrintingDesignList(string actionTag, string module)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "DESIGN", actionTag, ObjGlobal.SearchText, module, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"PRINTING DESIGN LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return string.Empty;
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return description.Text;
    }

    public static string GetPublicationList(string action)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "PUBLICATION", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].ToString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"BOOK PUBLICATION LIST NOT FOUND..!!");
            description.Focus();
            return description.Text;
        }
        ObjGlobal.SearchText = string.Empty;
        return description.Text;
    }

    public static string GetBookAuthorList(string action)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "AUTHOR", action, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].ToString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"BOOK AUTHOR LIST NOT FOUND..!!");
            description.Focus();
            return description.Text;
        }
        ObjGlobal.SearchText = string.Empty;
        return description.Text;
    }

    public static string GetNarrationRemarks(string actionTag)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "NRMASTER", actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["NRDESC"].ToString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"NARRATION LIST NOT FOUND..!!");
            description.Focus();
            return description.Text;
        }
        ObjGlobal.SearchText = string.Empty;
        return description.Text;
    }

    public static (string description, string module) GetDocumentSchemaList(string actionTag)
    {
        var description = new TextBox();
        var module = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "DOCUMENTSCHEMA", actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => description.Text
            };
            module.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["ShortName"].GetString(),
                _ => description.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"DOCUMENT SCHEMA LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (description.Text, module.Text);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, module.Text);
    }

    public static (string description, int id) GetDocumentNumberingList(string actionTag, string module)
    {
        var id = 0;
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "DOCUMENTNUMBERING", actionTag, ObjGlobal.SearchText, module, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            description.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["Description"].GetString(),
                _ => description.Text
            };
            id = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["LedgerId"].GetInt(),
                _ => id
            };
            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.Warning(@"DOCUMENT NUMBERING LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (description.Text, id);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, id);
    }


    /// <summary>
    /// CREATE MASTER LIST AND GET VALUE
    /// </summary>
    /// <param name="CREATE MASTER LIST AND GET VALUE"></param>
    /// <returns></returns>
    // CREATE MASTER LIST AND GET VALUE

    #region --------------- CREATE MASTER LIST AND RETURN VALUE) ---------------

    // GENERAL LEDGER
    public static (string description, int id) CreateCompanySetup(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmCompanySetup(isZoom);
        frm.ShowDialog();
        description.Text = frm.CompanyName;
        const int descriptionId = 0;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateBranch(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmBranchSetup(isZoom);
        frm.ShowDialog();
        description.Text = frm.BranchName;
        const int descriptionId = 0;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateCompanyUnit(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmCompanyUnitSetup();
        frm.ShowDialog();
        description.Text = frm.CompanyName;
        const int descriptionId = 0;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateAccountGroup(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmAccountGroup(isZoom);
        frm.ShowDialog();
        description.Text = frm.GroupDesc;
        var descriptionId = frm.GroupId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateAccountSubGroup(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmAccountSubGroup(isZoom);
        frm.ShowDialog();
        description.Text = frm.AccountSubGrpDesc;
        var descriptionId = frm.SubGroupId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateDepartment(bool isZoom = false, string text = "DEPARTMENT")
    {
        var description = new TextBox();
        var frm = new FrmDepartmentSetup(isZoom, text);
        frm.ShowDialog();
        description.Text = frm.DepartmentDesc;
        var descriptionId = frm.DepartmentId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateSubLedger(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmSubLedger(isZoom);
        frm.ShowDialog();
        description.Text = frm.SubLedgerName;
        var descriptionId = frm.SubLedgerId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateMainAgent(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmSeniorAgent(isZoom);
        frm.ShowDialog();
        description.Text = frm.AgentDesc;
        var descriptionId = frm.AgentId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateAgent(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmJuniorAgent(isZoom);
        frm.ShowDialog();
        description.Text = frm.AgentName;
        var descriptionId = frm.AgentId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id, string country) CreateMainArea(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmMainArea(isZoom);
        frm.ShowDialog();
        description.Text = frm.MainAreaDesc;
        var descriptionId = frm.MainAreaId;
        var country = frm.CountryDesc;
        description.Focus();
        return (description.Text, descriptionId, country);
    }

    public static (string description, int id) CreateMemberType(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmMemberType(isZoom);
        frm.ShowDialog();
        description.Text = frm.MemberDesc;
        var descriptionId = frm.MemberId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateMemberShip(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmMemberType(isZoom);
        frm.ShowDialog();
        description.Text = frm.MemberDesc;
        var descriptionId = frm.MemberId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateArea(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmSubArea(isZoom);
        frm.ShowDialog();
        description.Text = frm.AreaName;
        var descriptionId = frm.AreaId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateCounter(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmCounterName();
        frm.ShowDialog();
        description.Text = frm.CounterDesc;
        var descriptionId = frm.CounterId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateTable(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmTable(isZoom);
        frm.ShowDialog();
        description.Text = frm.SelectedTable;
        var descriptionId = frm.TableId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateGodown(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmGodownName(isZoom);
        frm.ShowDialog();
        description.Text = frm.GodownMaster;
        var descriptionId = frm.GodownId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateNarration(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmNarrationMaster(isZoom);
        frm.ShowDialog();
        description.Text = frm.NarrationMasterDetails;
        var descriptionId = 0;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateCostCenter(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmCostCentre(isZoom);
        frm.ShowDialog();
        description.Text = frm.CostCenterDesc;
        var descriptionId = frm.CCId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id, string exchangeRate) CreateCurrency(bool isZoom = false)
    {
        var description = new TextBox();
        var exchangeRate = new TextBox();
        var frm = new FrmCurrency(isZoom);
        frm.ShowDialog();
        description.Text = frm.CurrencyDesc;
        var descriptionId = frm.CurrencyId;
        exchangeRate.Text = frm.CurrencyRate.GetDecimalString(true);
        description.Focus();
        return (description.Text, descriptionId, exchangeRate.Text);
    }

    public static (string description, int id) CreateDoctorType(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmDoctorType(isZoom);
        frm.ShowDialog();
        description.Text = frm.DoctorTypeDesc;
        var descriptionId = frm.DoctorTypeId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateDoctor(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmAccountGroup(isZoom);
        frm.ShowDialog();
        description.Text = frm.GroupDesc;
        var descriptionId = frm.GroupId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateHospitalDepartment(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmHDepartment(isZoom);
        frm.ShowDialog();
        description.Text = frm.DepartmentName;
        var descriptionId = frm.DepartmentId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateWard(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmWard(isZoom);
        frm.ShowDialog();
        description.Text = frm.WardDesc;
        var descriptionId = frm.WardId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateBedType(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmBedType(isZoom);
        frm.ShowDialog();
        description.Text = frm.BedTypeDesc;
        var descriptionId = frm.BedTypeId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateBedNumber(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmBedNo(isZoom);
        frm.ShowDialog();
        description.Text = frm.BedNoDesc;
        var descriptionId = frm.BedNoId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) CreateGeneralLedger(string category = "", bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmGeneralLedger(category, isZoom);
        frm.ShowDialog();
        description.Text = frm.LedgerDesc;
        var descriptionId = frm.LedgerId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) CreateHospitalItem(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmHospitalServices(isZoom);
        frm.ShowDialog();
        description.Text = frm.ProductDesc;
        var descriptionId = frm.ProductId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    // PRODUCT LEDGER
    public static (string description, int id) CreateProductUnit(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmProductUnit(isZoom);
        frm.ShowDialog();
        description.Text = frm.ProductUnitName;
        var descriptionId = frm.ProductUnitId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateProductGroup(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmProductGroup(true);
        frm.ShowDialog();
        description.Text = frm.ProductGroupDesc;
        var descriptionId = frm.GroupId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, int id) CreateProductSubGroup(bool isZoom = false, int groupId = 0)
    {
        var description = new TextBox();
        var frm = new FrmProductSubGroup(true, groupId);
        frm.ShowDialog();
        description.Text = frm.ProductSubGroupName;
        var descriptionId = frm.PSubGrpId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) CreateProduct(bool isZoom = false)
    {
        long descriptionId;
        var description = new TextBox();
        if (ObjGlobal.SoftwareModule.Equals("POS"))
        {
            var frm = new FrmPosProduct(isZoom);
            frm.ShowDialog();
            description.Text = frm.ProductDesc;
            descriptionId = frm.ProductId;
        }
        else if (ObjGlobal.SoftwareModule.Equals("RESTRO"))
        {
            var frm = new FrmRestaurantProduct(isZoom);
            frm.ShowDialog();
            description.Text = frm.ProductDesc;
            descriptionId = frm.ProductId;
        }
        else if (ObjGlobal.SoftwareModule.Equals("STATIONARY") || ObjGlobal.SoftwareModule.Equals("SCHOOL-TIME"))
        {
            var frm = new FrmBook("SAVE", isZoom);
            frm.ShowDialog();
            description.Text = frm.ProductDesc;
            descriptionId = frm.ProductId;
        }
        else if (ObjGlobal.SoftwareModule.Equals("HOSPITAL"))
        {
            var frm = new FrmHospitalServices(isZoom);
            frm.ShowDialog();
            description.Text = frm.ProductDesc;
            descriptionId = frm.ProductId;
        }
        else
        {
            var frm = new FrmProduct(isZoom);
            frm.ShowDialog();
            description.Text = frm.ProductDesc;
            descriptionId = frm.ProductId;
        }

        description.Focus();
        return (description.Text, descriptionId);
    }

    public static (string description, long id) CreatePatient(bool isZoom = false)
    {
        var description = new TextBox();
        var frm = new FrmPatient(isZoom);
        frm.ShowDialog();
        description.Text = frm.PatientDesc;
        var descriptionId = frm.PatientId;
        description.Focus();
        return (description.Text, descriptionId);
    }

    #endregion --------------- CREATE MASTER LIST AND RETURN VALUE) ---------------

    // GET TAG MASTER LIST
    public static string GetTagMasterList(string reportDesc, string category = "")
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = reportDesc,
            Category = category,
            BranchId = ObjGlobal.SysBranchId.GetIntString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.GetIntString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.GetIntString()
        };
        frm2.ShowDialog();
        return ClsTagList.PlValue1;
    }
}