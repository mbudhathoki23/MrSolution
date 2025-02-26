using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmCurrencyRateList : Form
{
    public FrmCurrencyRateList()
    {
        InitializeComponent();
    }

    private void FrmCurrencyRateList_Load(object sender, EventArgs e)
    {
        CurrencyConversion();
    }

    public string CurrencyConversion()
    {

        return string.Empty;
    }
}