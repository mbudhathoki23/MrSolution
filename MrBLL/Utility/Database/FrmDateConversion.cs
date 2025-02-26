using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.Database;

public partial class FrmDateConversion : MrForm
{
    private DataTable dt = new();
    private string Query = string.Empty;

    public FrmDateConversion()
    {
        InitializeComponent();
    }

    public string Date { get; set; }

    private void FrmDateConversion_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        //ObjGlobal.PageloadDateType(msk_FromDate);
        if (ObjGlobal.SysDateType == "M")
        {
            Text = "Miti To Date Conversion";
            lbl_FromDate.Text = "Date";
            lbl_ToDate.Text = "Miti";
            msk_FromDate.Text = ObjGlobal.ReturnEnglishDate(Date);
            msk_ToDate.Text = Date;
        }
        else
        {
            Text = "Date To Miti Conversion";
            lbl_FromDate.Text = "Miti";
            lbl_ToDate.Text = "Date";
            msk_FromDate.Text = ObjGlobal.ReturnNepaliDate(Date);
            msk_ToDate.Text = Date;
        }

        msk_FromDate.Focus();
    }

    private void FrmDateConversion_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
            Close();
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        Date = msk_ToDate.Text;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'E');
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'L');
    }

    private void msk_FromDate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (msk_FromDate.Text != string.Empty || msk_FromDate.Text != "00/00/00000")
            {
                string Date_Type;
                if (ObjGlobal.SysDateType == "M")
                    Date_Type = "D";
                else
                    Date_Type = "M";
                if (ValidDate(msk_FromDate.Text, Date_Type))
                {
                    if (ObjGlobal.SysDateType == "M")
                        msk_ToDate.Text = ObjGlobal.ReturnNepaliDate(msk_FromDate.Text);
                    else
                        msk_ToDate.Text = ObjGlobal.ReturnEnglishDate(msk_FromDate.Text);
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_FromDate.Focus();
                    return;
                }

                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Date is not blank!");
                msk_FromDate.Focus();
            }
        }
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'E');
    }

    private void msk_ToDate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (msk_ToDate.Text != string.Empty || msk_ToDate.Text != "00/00/00000")
            {
                string Date_Type;
                if (ObjGlobal.SysDateType == "M")
                    Date_Type = "D";
                else
                    Date_Type = "M";
                if (ValidDate(msk_ToDate.Text, Date_Type))
                {
                    if (ObjGlobal.SysDateType == "M")
                        msk_FromDate.Text = ObjGlobal.ReturnEnglishDate(msk_ToDate.Text);
                    else
                        msk_FromDate.Text = ObjGlobal.ReturnNepaliDate(msk_ToDate.Text);
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_ToDate.Focus();
                    return;
                }

                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Date is not blank!");
                msk_ToDate.Focus();
            }
        }
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_FromDate_TextChanged(object sender, EventArgs e)
    {
        if (msk_FromDate.Text.Trim() != string.Empty)
        {
        }
    }

    private void msk_ToDate_TextChanged(object sender, EventArgs e)
    {
        if (msk_ToDate.Text.Trim() != string.Empty)
        {
        }
    }

    private static bool ValidDate(string AdDate, string Date_Type)
    {
        var Eng = "01/01/1753";
        string[] split;
        var Query = string.Empty;

        if (ObjGlobal.SysDateType == "M")
        {
            if (Date_Type == "M")
            {
                Eng = AdDate;
                split = Eng.Split('/', ' ');
                Eng = Convert.ToString(split[2] + "/" + split[1] + "/" + split[0]);
                Query = "Select AD_Date from AMS.DateMiti where AD_Date='" + Eng + "'";
            }
            else
            {
                Eng = AdDate;
                Query = "Select AD_Date from AMS.DateMiti where BS_DateDMY='" + Eng + "'";
            }
        }
        else
        {
            if (Date_Type == "D")
            {
                Eng = AdDate;
                split = Eng.Split('/', ' ');
                Eng = Convert.ToString(split[2] + "/" + split[1] + "/" + split[0]);
                Query = "Select AD_Date from AMS.DateMiti where AD_Date='" + Eng + "'";
            }
            else
            {
                Eng = AdDate;
                Query = "Select AD_Date from PIE.DateTable where BS_DateDMY='" + Eng + "'";
            }
        }

        try
        {
            var dt1 = new DataTable();
            dt1.Reset();
            dt1 = GetConnection.SelectDataTableQuery(Query);
            if (dt1.Rows.Count > 0)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}