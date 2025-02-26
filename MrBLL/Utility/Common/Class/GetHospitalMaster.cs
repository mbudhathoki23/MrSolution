using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using System.Windows.Forms;

namespace MrBLL.Utility.Common.Class;

public static class GetHospitalMaster
{
    public static (string description, int id) GetDoctorTypeList(string actionTag)
    {
        var description = new TextBox();
        var descriptionId = 0;
        var frmPickList = new FrmAutoPopList("MED", "DOCTOR_TYPE", actionTag, ObjGlobal.SearchKey, "ALL", "HOS_MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                description.Text = frmPickList.SelectedList[0]["TypeDescription"].GetString();
                descriptionId = frmPickList.SelectedList[0]["DoctorTypeId"].GetInt();
            }

            frmPickList.Dispose();
        }
        else
        {
            description.WarningMessage(@"DOCTOR TYPE LIST ARE NOT AVAILABLE..!!");
            description.Focus();
            return (string.Empty, descriptionId);
        }

        ObjGlobal.SearchText = string.Empty;
        description.Focus();
        return (description.Text, descriptionId);
    }
}