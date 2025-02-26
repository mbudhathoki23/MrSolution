using MrDAL.Global.Control;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class FrmListView : Form
{
    public FrmListView()
    {
        InitializeComponent();
        KeyPress += FrmListView_KeyPress;
        CenterToScreen();
    }

    private void FrmListView_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    public int AddToList(string value, bool isSuccess)
    {
        var item = new ListViewItem
        {
            Text = value,
            ForeColor = isSuccess ? Color.Black : Color.Red,
        };
        ReportView.Items.Add(item);
        return item.Index;
    }
}