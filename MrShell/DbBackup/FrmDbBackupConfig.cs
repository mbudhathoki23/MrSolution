using System.Windows.Forms;

namespace MrShell.DbBackup;

public partial class FrmDbBackupConfig : Form
{
    public FrmDbBackupConfig()
    {
        InitializeComponent();
    }

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
        this.Close();
    }
}