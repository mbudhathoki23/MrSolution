using MrDAL.Control.WinControl;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmDiscount : MrForm
{
    private readonly decimal NetAmount;
    public decimal DiscPercent, DiscountAmount;

    public FrmDiscount(decimal discPercent, decimal netAmount)
    {
        InitializeComponent();
        DiscPercent = discPercent;
        NetAmount = netAmount;
    }

    private void FrmDiscount_Load(object sender, EventArgs e)
    {
        TxtDiscount.Text = DiscPercent.ToString();
        TxtDiscAmount.Text = DiscPercent > 0 ? (NetAmount * (DiscPercent / 100)).ToString() : "0.00";
    }

    private void TxtDiscount_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDiscount.Text))
        {
            decimal.TryParse(TxtDiscount.Text, out var discpercent);
            DiscPercent = Convert.ToDecimal(discpercent);
            TxtDiscAmount.Text = discpercent > 0 ? (NetAmount * (discpercent / 100)).ToString() : "0.00";
        }
    }

    private void FrmDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        //if (!string.IsNullOrEmpty(TxtDiscount.Text))
        //{
        //    decimal.TryParse(TxtDiscount.Text, out decimal discpercent);
        //    DiscPercent = Convert.ToDecimal(discpercent);
        //}
        // this.Close();

        //}
    }

    private void FrmDiscount_KeyPress(object sender, KeyPressEventArgs e)
    {
        //if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
        //if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
    }

    private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
        {
            e.Handled = true;
            return;
        }

        // checks to make sure only 1 decimal is allowed
        if (e.KeyChar == 46)
            if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                e.Handled = true;
    }

    private void TxtDiscAmount_Validating(object sender, CancelEventArgs e)
    {
        decimal.TryParse(TxtDiscount.Text, out var discpercent);
        var DiscountAmount = discpercent > 0 ? NetAmount * (discpercent / 100) : 0;
        decimal.TryParse(TxtDiscAmount.Text, out var discountTextAmount);
        if (discountTextAmount != DiscountAmount)
            TxtDiscount.Text = (discountTextAmount / NetAmount * 100).ToString("0.00");
    }

    private void TxtDiscAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            decimal.TryParse(TxtDiscount.Text, out var discpercent);
            var DiscAmount = discpercent > 0 ? NetAmount * (discpercent / 100) : 0;
            decimal.TryParse(TxtDiscAmount.Text, out var discountTextAmount);
            if (discountTextAmount != DiscAmount)
            {
                TxtDiscount.Text = (discountTextAmount / NetAmount * 100).ToString("0.00");
                DiscPercent = Convert.ToDecimal(TxtDiscount.Text);
                DiscountAmount = discountTextAmount;
            }
            else
            {
                DiscountAmount = discountTextAmount;
                DiscPercent = Convert.ToDecimal(TxtDiscount.Text);
            }

            Close();
        }
    }
}