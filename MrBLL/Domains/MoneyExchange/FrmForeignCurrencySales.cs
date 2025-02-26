using MrDAL.Control.ControlsEx.GridControl;
using System;
using System.Windows.Forms;

namespace MrBLL.Domains.MoneyExchange;

public partial class FrmForeignCurrencySales : Form
{
    public FrmForeignCurrencySales()
    {
        InitializeComponent();
    }

    private void FrmForeignCurrencySales_Load(object sender, EventArgs e)
    {

    }

    // METHOD FOR THIS FORM



    // GRID CONTROL
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtCurrency { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtNetAmount { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }

}