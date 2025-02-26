using DevExpress.XtraEditors;
using MrDAL.Core.Extensions;
using System;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmCalculator : Form
{

    public FrmCalculator()
    {
        InitializeComponent();
    }
    private void FrmCalculator_Load(object sender, EventArgs e)
    {
        LblResult.Focus();
    }
    private void FrmCalculator_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == (char)Keys.D1)
        {
            TxtOne.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D2)
        {
            TxtTwo.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D3)
        {
            TxtThree.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D4)
        {
            TxtFour.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D5)
        {
            TxtFive.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D6)
        {
            TxtSix.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D7)
        {
            TxtSeven.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D8)
        {
            TxtEight.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D9)
        {
            TxtNine.PerformClick();
        }
        else if (e.KeyValue == (char)Keys.D0)
        {
            TxtZero.PerformClick();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtEqual.PerformClick();
        }

    }
    private void FrmCalculator_KeyPress(object sender, KeyPressEventArgs e)
    {

    }
    private void CalculateValue(object sender, EventArgs e)
    {

    }
    private void NumEvent(object sender, EventArgs e)
    {
        if (LblResult.Text == "0" || operandPerformed)
        {
            LblResult.Text = string.Empty;
        }
        SimpleButton btn = (SimpleButton)sender;
        if (btn.Text.Equals("."))
        {
            if (LblResult.Text.Contains("."))
            {
                return;
            }
        }
        LblResult.Text += btn.Text;
        operandPerformed = false;
    }
    private void OperandEvent(object sender, EventArgs e)
    {
        operandPerformed = true;
        SimpleButton btn = (SimpleButton)sender;
        string newOperand = btn.Text;

        var resultValue = LblResult.Text + " " + newOperand;
        if (newOperand.IsBlankOrEmpty())
        {
            return;
        }
        switch (operand)
        {
            case "+": LblResult.Text = (result + Double.Parse(LblResult.Text)).ToString(); break;
            case "-": LblResult.Text = (result - Double.Parse(LblResult.Text)).ToString(); break;
            case "X": LblResult.Text = (result * Double.Parse(LblResult.Text)).ToString(); break;
            case "/": LblResult.Text = (result / Double.Parse(LblResult.Text)).ToString(); break;
            default: break;
        }

        result = Double.Parse(LblResult.Text);
        operand = newOperand;

    }
    private void BCE_Click(object sender, EventArgs e)
    {
        LblResult.Text = "0";
    }
    private void BC_Click(object sender, EventArgs e)
    {
        LblResult.Text = "0";
        result = 0;
        operand = "";
    }
    private void BEq_Click(object sender, EventArgs e)
    {
        if (!LblResult.IsValueExits() || operand.IsBlankOrEmpty())
        {
            return;
        }
        operandPerformed = true;

        switch (operand)
        {
            case "+": LblResult.Text = (result + Double.Parse(LblResult.Text)).ToString(); break;
            case "-": LblResult.Text = (result - Double.Parse(LblResult.Text)).ToString(); break;
            case "X": LblResult.Text = (result * Double.Parse(LblResult.Text)).ToString(); break;
            case "/": LblResult.Text = (result / Double.Parse(LblResult.Text)).ToString(); break;
            default: break;
        }

        result = Double.Parse(LblResult.Text);
        LblResult.Text = result.ToString();
        result = 0;
        operand = "";
    }
    private void Button15_Click(object sender, EventArgs e)
    {
        if (!operandPerformed && !LblResult.Text.Contains(","))
        {
            LblResult.Text += ",";
        }
        else if (operandPerformed)
        {
            LblResult.Text = "0";
        }

        if (!LblResult.Text.Contains(","))
        {
            LblResult.Text += ",";
        }

        operandPerformed = false;
    }

    string keyPressValue = string.Empty;
    bool operandPerformed = false;
    string operand = "";
    double result = 0;
}