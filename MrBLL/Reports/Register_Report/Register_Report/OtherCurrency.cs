using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class OtherCurrency : MrForm
{
    public OtherCurrency()
    {
        InitializeComponent();
    }

    private void OtherCurrency_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        txt_Currency.Focus();
    }

    private void OtherCurrency_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) Close();
    }

    private void txt_Currency_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Currency, 'E');
    }

    private void txt_Currency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) btn_Currency_Click(sender, e);
        if (e.KeyCode == Keys.Enter)
        {
            if (txt_Currency.Text != string.Empty)
                SendKeys.Send("{TAB}");
            else
                txt_Currency.Focus();
        }
    }

    private void txt_Currency_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Currency, 'L');
    }

    private void txt_Currency_Validated(object sender, EventArgs e)
    {
        Currency_Id = 0;
        if (txt_Currency.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Currency can't be left Blank!");
            txt_Currency.Focus();
            return;
        }

        if (txt_Currency.Text.Trim() != string.Empty)
        {
            Query = "Select * from AMS.Currency WHERE CCode='" + txt_Currency.Text.Trim() + "'";
            dtvalidate.Reset();
            dtvalidate = GetConnection.SelectDataTableQuery(Query);
            if (dtvalidate.Rows.Count > 0)
            {
                Currency_Id = Convert.ToInt32(dtvalidate.Rows[0]["CId"].ToString());
                txt_CurrencyRate.Enabled = true;
            }
            else
            {
                MessageBox.Show("Does Not Exits Currency!");
                txt_Currency.Focus();
            }
        }
        else
        {
            txt_CurrencyRate.Enabled = false;
            txt_CurrencyRate.Text = string.Empty;
            Currency_Id = 0;
        }
    }

    private void btn_Currency_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();

        HeaderCap.Add("Code");
        HeaderCap.Add("Name");
        HeaderCap.Add("Id");
        ColumnWidths.Add(100);
        ColumnWidths.Add(250);
        ColumnWidths.Add(0);
        Query = "SELECT CCode,CName,CId  from AMS.Currency where CId<>'" + ObjGlobal.SysCurrencyId + "'";
        var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Currency List", string.Empty);
        if (PkLst.ShowDialog() == DialogResult.OK)
            if (ClsPickList.PlValue1 != null && ClsPickList.PlValue3 != null &&
                ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue3 != string.Empty)
            {
                Currency_Id = Convert.ToInt32(ClsPickList.PlValue3);
                txt_Currency.Text = ClsPickList.PlValue1;
                txt_Currency.Focus();
            }
    }

    private void txt_CurrencyRate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_CurrencyRate, 'E');
    }

    private void txt_CurrencyRate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_CurrencyRate, 'L');
        txt_CurrencyRate.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(txt_CurrencyRate.Text))
            .ToString(ObjGlobal.SysAmountFormat);
    }

    private void txt_CurrencyRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (txt_CurrencyRate.Text != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Currency Rate is not blank!");
                txt_CurrencyRate.Focus();
            }
        }
    }

    private void txt_CurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_CurrencyRate_Validated(object sender, EventArgs e)
    {
        if (txt_Currency.Text != string.Empty)
            if (Convert.ToDouble(ObjGlobal.ReturnDecimal(txt_CurrencyRate.Text.Trim())) <= 0)
            {
                MessageBox.Show("Currency Rate Canot Zero Value!");
                if (txt_CurrencyRate.Enabled) txt_CurrencyRate.Focus();
            }
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        Cur_Name = txt_Currency.Text;
        Cur_Id = Currency_Id;
        Cur_Rate = Convert.ToDouble(txt_CurrencyRate.Text);
        DialogResult = DialogResult.OK;
        Close();
    }

    #region Global Variable

    private string Query;
    private DataTable dt = new();
    private DataTable dtvalidate = new();
    private int Currency_Id;
    private readonly ArrayList HeaderCap = new();
    private readonly ArrayList ColumnWidths = new();

    public string Cur_Name { get; set; }

    public int Cur_Id { get; set; }

    public double Cur_Rate { get; set; }

    #endregion Global Variable
}