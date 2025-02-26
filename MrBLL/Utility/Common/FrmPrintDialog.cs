using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Utility.Common;

public partial class FrmPrintDialog : MrForm
{
    #region --------------- Property ---------------

    public string Pr_Device;
    public long _Page_No;

    public long Page_No
    {
        get => _Page_No;
        set => _Page_No = value;
    }

    public long _No_Copy;

    public long No_Copy
    {
        get => _No_Copy;
        set => _No_Copy = value;
    }

    public int _Start_Page;

    public int Start_Page
    {
        get => _Start_Page;
        set => _Start_Page = value;
    }

    public int _End_Page;

    public int End_Page
    {
        get => _End_Page;
        set => _End_Page = value;
    }

    public string _Paper_Size;

    public string Paper_Size
    {
        get => _Paper_Size;
        set => _Paper_Size = value;
    }

    public long _N_Line;

    public long N_Line
    {
        get => _N_Line;
        set => _N_Line = value;
    }

    public long _Page_Width;

    public long Page_Width
    {
        get => _Page_Width;
        set => _Page_Width = value;
    }

    public long _Page_Height;

    public long Page_Height
    {
        get => _Page_Height;
        set => _Page_Height = value;
    }

    public long _Row_Height;

    public long Row_Height
    {
        get => _Row_Height;
        set => _Row_Height = value;
    }

    public long _Left_Margin;

    public long Left_Margin
    {
        get => _Left_Margin;
        set => _Left_Margin = value;
    }

    public long _Right_Margin;

    public long Right_Margin
    {
        get => _Right_Margin;
        set => _Right_Margin = value;
    }

    public long _Tbl_HeaderHeight;

    public long Tbl_HeaderHeight
    {
        get => _Tbl_HeaderHeight;
        set => _Tbl_HeaderHeight = value;
    }

    public long _ColHedGap;

    public bool Page_Break { get; set; }

    public bool Landscape { get; set; }

    #endregion --------------- Property ---------------

    #region --------------- Form ---------------

    public FrmPrintDialog()
    {
        InitializeComponent();
        ObjGlobal.BindPrinter(cmb_Printer);
    }

    private void PrintDialog_Load(object sender, EventArgs e)
    {
        No_Copy = Start_Page = End_Page = 0;
        Paper_Size = string.Empty;
        N_Line = 0;

        cmb_PaperSize.Items.Add("A4 Full");
        cmb_PaperSize.Items.Add("Letter Full");
        cmb_PaperSize.Items.Add("8 1/2 * 12");
        cmb_PaperSize.SelectedIndex = 0;

        cmb_PaperSize.SelectedIndex = ObjGlobal.SysPaperSize == "A4 Full" ? 0 : 1;
    }

    private void PrintDialog_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape)
        {
            return;
        }

        if (MessageBox.Show(@"Are you sure want to Close Form..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion --------------- Form ---------------

    #region --------------- Event ---------------

    private void rb_All_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_All.Checked)
        {
            txt_From.Enabled = false;
            txt_To.Enabled = false;
        }
    }

    private void rb_Pages_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_Pages.Checked)
        {
            txt_From.Enabled = true;
            txt_To.Enabled = true;
        }
    }

    private void btn_Print_Click(object sender, EventArgs e)
    {
        int.TryParse(txt_NoOfCopies.Text, out var noOfCopy);
        No_Copy = noOfCopy;
        Start_Page = 0;
        End_Page = 0;
        Page_No = 0;
        if (rb_All.Checked == false)
        {
            Start_Page = Convert.ToInt16(ObjGlobal.ReturnDecimal(txt_From.Text));
            End_Page = Convert.ToInt16(ObjGlobal.ReturnDecimal(txt_To.Text));
            Page_No = Page_No - 1;
        }
        else
        {
            End_Page = 0;
        }

        foreach (var printer in PrinterSettings.InstalledPrinters.Cast<string>().Where(printer => string.Equals(printer, cmb_Printer.Text, StringComparison.CurrentCultureIgnoreCase)))
        {
            Pr_Device = printer;
        }

        Paper_Size = cmb_PaperSize.Text;
        if (No_Copy < 1) No_Copy = 1;
        if (rb_Portrait.Checked) Landscape = false;
        else if (rb_Landscape.Checked) Landscape = true;
        DialogResult = DialogResult.OK;
        Close();
    }

    #endregion --------------- Event ---------------
}