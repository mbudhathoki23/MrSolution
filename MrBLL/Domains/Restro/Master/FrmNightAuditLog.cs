using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Data;
using System.Windows.Forms;
using DialogResult = System.Windows.Forms.DialogResult;

namespace MrBLL.Domains.Restro.Master;

public partial class FrmNightAuditLog : Form
{
    public FrmNightAuditLog()
    {
        InitializeComponent();
        RGrid.AutoGenerateColumns = false;
        _salesEntry = new ClsSalesEntry();
    }

    private void FrmNightAuditLog_Load(object sender, EventArgs e)
    {
        BindAuditLogData();
        BindSalesData();
        MskLastDate.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        var result = SaveNightAuditLog();
        if (result == 0)
        {
            CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE NIGHT AUDIT UPDATE..!!");
            return;
        }
        DialogResult = DialogResult.OK;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.Question("DO YOU WANT TO EXIT ACTIVE FORM..??");
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    private int SaveNightAuditLog()
    {
        _salesEntry.AuditLog.LogId = auditLogId;
        _salesEntry.AuditLog.AuditedDate = DateTime.Now;
        _salesEntry.AuditLog.AuditUser = ObjGlobal.LogInUser;
        _salesEntry.AuditLog.BranchId = ObjGlobal.SysBranchId;
        _salesEntry.AuditLog.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
        _salesEntry.AuditLog.AuditedDate = DateTime.Now;
        var result = _salesEntry.SaveNightAuditLog("UPDATE");
        if (result != 0)
        {
            _salesEntry.SaveNightAuditLog("SAVE");
        }
        return result;
    }

    private void BindSalesData()
    {
        var dt = _salesEntry.GetSalesDataReportPaymentType(MskLastDate.Text);
        if (dt.RowsCount() > 0)
        {
            RGrid.DataSource = dt;
        }
    }

    private void BindAuditLogData()
    {
        var dt = _salesEntry.ReturnLastNightAuditLog();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                auditLogId = row["LogId"].GetInt();
                MskLastDate.Text = row["AuditDate"].GetDateString();
            }
        }
    }

    private ISalesEntry _salesEntry;
    private int auditLogId = 0;
}