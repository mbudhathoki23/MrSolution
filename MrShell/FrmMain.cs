using System.Windows.Forms;
using MrShell.DbBackup;

namespace MrShell;

public partial class FrmMain : Form
{
    public bool IsLoggedOut { get; private set; }

    public FrmMain()
    {
        InitializeComponent();
    }

    private void FrmMain_Load(object sender, System.EventArgs e)
    {
        this.Controls.Add(new UcDbBackupHome { Dock = DockStyle.Fill });
    }
}