using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using System.Windows.Forms;

namespace MrDAL.Domains.CRM.Common;

public class GetCrmMasterList
{
    public static (string description, long id) GetClientCollection(string category, bool isActive)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MAX", "ClientCollection", category, isActive);
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
            description.WarningMessage("CLIENT INFORMATION RECORDS NOT FOUNDS..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }

    public static string GetClientSource(string category, bool isActive)
    {
        var description = new TextBox();
        var frmPickList = new FrmAutoPopList("MIN", "ClientSource", category, isActive);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                description.Text = frmPickList.SelectedList[0]["Description"].GetString();
            frmPickList.Dispose();
        }
        else
        {
            description.WarningMessage("CLIENT SOURCE RECORDS NOT FOUNDS..!!");
            description.Focus();
            return description.Text;
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return description.Text;
    }
}