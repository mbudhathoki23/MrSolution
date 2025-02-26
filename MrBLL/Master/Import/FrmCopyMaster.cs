using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Master.Import;

public partial class FrmCopyMaster : MrForm
{
    // IMPORT FROM LOCAL
    #region --------------- IMPORT DATA ---------------
    public FrmCopyMaster()
    {
        InitializeComponent();
        _objUtility = new ClsImport();
    }

    private void FrmImportData_Load(object sender, EventArgs e)
    {
        BindMasterList();
    }

    private void BtnCompany_Click(object sender, EventArgs e)
    {

        var cmdTxt = $"SELECT gc.Database_Name LedgerId, gc.Company_Name Description FROM MASTER.AMS.GlobalCompany gc WHERE gc.Database_Name <> '{ObjGlobal.InitialCatalog}'";
        var fmComList = new FrmAutoPopList(cmdTxt);
        if (FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
        fmComList.ShowDialog();
        if (fmComList.SelectedList.Count <= 0) return;
        TxtCompany.Text = fmComList.SelectedList[0]["Description"].ToString().Trim();
        TxtDatabase.Text = fmComList.SelectedList[0]["LedgerId"].ToString().Trim();
    }

    private void BtnImport_Click(object sender, EventArgs e)
    {
        if (CopyMasterListFromOldOne())
        {
            CustomMessageBox.Information("COPY DATA SUCCESSFULLY..!!");
        }
        else
        {
            CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE DATA COPY DO YOU WANT TO UPDATE..??");
        }
    }

    private void TxtDatabase_KeyDown(object sender, KeyEventArgs e)
    {
        ClsKeyPreview.KeyEvent(e, "DELETE", TxtDatabase, BtnCompany);
    }

    private void TxtCompany_KeyDown(object sender, KeyEventArgs e)
    {
        ClsKeyPreview.KeyEvent(e, "DELETE", TxtDatabase, BtnCompany);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void CkbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        TagMaster();
    }

    #endregion --------------- IMPORT DATA ---------------


    // METHOD FOR THIS FORM
    #region ---------------  METHOD ---------------

    private bool CopyMasterListFromOldOne()
    {
        try
        {

            for (var i = 0; i < ChkListMaster.Items.Count; i++)
            {
                ChkListMaster.SelectedIndex = i;
                if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
                {
                    var result = _objUtility.ImportMasterFromLocal(ChkListMaster.SelectedValue.ToString(), "", TxtDatabase.Text);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    private void BindMasterList()
    {
        var table = new DataTable();
        table = table.MasterImportModule();
        ChkListMaster.DataSource = table;
        ChkListMaster.DisplayMember = "DESCRIPTION";
        ChkListMaster.ValueMember = "DESCRIPTION";
    }
    private void TagMaster()
    {
        if (ChkListMaster.Items.Count <= 0)
        {
            return;
        }
        for (var i = 0; i < ChkListMaster.Items.Count; i++)
        {
            ChkListMaster.SetItemChecked(i, ckbSelectAll.Checked);
        }
    }
    #endregion ---------------  METHOD ---------------


    // OBJECT FOR THIS FORM
    #region --------------- Class ---------------
    private readonly IOnlineSync _objUtility;
    #endregion -------------- OBJECT --------------

}