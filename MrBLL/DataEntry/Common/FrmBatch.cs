using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmBatch : MrForm
{
    public string _BatchNo;
    public DateTime _EXPDate;
    public DateTime _MFGDate;

    public FrmBatch()
    {
        InitializeComponent();
    }

    public string Datetime { get; private set; }

    private void FrmBatch_Load(object sender, EventArgs e)
    {
        MskMFGDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        MskMFGMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                     Convert.ToDateTime(MskMFGDate.Text).ToString("yyyy-MM-dd") +
                                                     "'");
        MskEXPDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        MskEXPMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                     Convert.ToDateTime(MskEXPDate.Text).ToString("yyyy-MM-dd") +
                                                     "'");
        TxtBatchNo.Focus();
    }

    private void FrmBatch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (TxtBatchNo.Text.Trim() == string.Empty && TxtBatchNo.TextLength == 0)
        {
            MessageBox.Show(@"Batch No is Empty..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBatchNo.Focus();
            return;
        }

        if (MskMFGDate.Text.Trim() == "  /  /    " && MskMFGDate.TextLength == 0)
        {
            MessageBox.Show(@"MFG Date is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskMFGDate.Focus();
            return;
        }

        if (MskEXPDate.Text.Trim() == "  /  /    " && MskEXPDate.TextLength == 0)
        {
            MessageBox.Show(@"EXP Date is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskMFGDate.Focus();
            return;
        }

        _BatchNo = TxtBatchNo.Text.Trim();
        _MFGDate = Convert.ToDateTime(MskMFGDate.Text);
        _EXPDate = Convert.ToDateTime(MskEXPDate.Text);
        ControlClear();
        Close();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ControlClear();
    }

    private void TxtBatchNo_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBatchNo, 'E');
    }

    private void TxtBatchNo_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBatchNo, 'L');
    }

    private void TxtBatchNo_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtBatchNo && TxtBatchNo.TextLength == 0)
        {
            MessageBox.Show(@"Batch No is Empty..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtBatchNo.Focus();
        }
    }

    private void ControlClear()
    {
        TxtBatchNo.Clear();
        MskEXPDate.Text = string.Empty;
        MskMFGDate.Text = string.Empty;
    }

    private void MskMFGDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMFGDate, 'E');
    }

    private void MskMFGDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMFGDate, 'L');
    }

    private void MskMFGDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskMFGDate.Text != "  /  /")
        {
            MskMFGMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                         Convert.ToDateTime(MskMFGDate.Text)
                                                             .ToString("yyyy-MM-dd") + "'");
        }
        else if (MskMFGDate.Text.Trim() == "/  /" && MskMFGDate.Enabled)
        {
            MessageBox.Show(@"Invoice Miti Cann't be Left Blank..!!", ObjGlobal.Caption);
            MskMFGDate.Focus();
        }
    }

    private void MskMFGMiti_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMFGMiti, 'E');
    }

    private void MskMFGMiti_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskEXPMiti, 'L');
    }

    private void MskMFGMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskEXPMiti.Text != "  /  /")
        {
            MskMFGDate.Text =
                GetConnection.GetQueryData("Select AD_Date from AMS.DateMiti where BS_DateDMY='" + MskEXPMiti.Text +
                                           "'");
        }
        else if (MskEXPMiti.Text.Trim() == "/  /" && MskEXPMiti.Enabled)
        {
            MessageBox.Show(@"Invoice Miti Cann't be Left Blank..!!", ObjGlobal.Caption);
            MskEXPMiti.Focus();
        }
    }

    private void MskEXPDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskEXPDate, 'E');
    }

    private void MskEXPDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskEXPDate, 'L');
    }

    private void MskEXPDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskEXPDate.Text != "  /  /")
        {
            MskEXPMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                         Convert.ToDateTime(MskEXPDate.Text)
                                                             .ToString("yyyy-MM-dd") + "'");
        }
        else if (MskEXPDate.Text.Trim() == "/  /" && MskEXPDate.Enabled)
        {
            e.Cancel = true;
            MessageBox.Show(@"EXP Date Cann't be Left Blank..!!", ObjGlobal.Caption);
            MskEXPDate.Focus();
        }
    }

    private void MskEXPMiti_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskEXPMiti, 'E');
    }

    private void MskEXPMiti_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskEXPMiti, 'L');
    }

    private void MskEXPMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskEXPMiti.Text != "  /  /")
        {
            MskEXPDate.Text =
                GetConnection.GetQueryData("Select AD_Date from AMS.DateMiti where BS_DateDMY='" + MskEXPMiti.Text +
                                           "'");
        }
        else if (MskEXPMiti.Text.Trim() == "/  /" && MskEXPMiti.Enabled)
        {
            e.Cancel = true;
            MessageBox.Show(@"Invoice Miti Cann't be Left Blank..!!", ObjGlobal.Caption);
            MskEXPMiti.Focus();
        }
    }
}