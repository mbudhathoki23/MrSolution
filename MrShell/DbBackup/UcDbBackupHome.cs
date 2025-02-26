using System.Windows.Forms;

namespace MrShell.DbBackup;

public partial class UcDbBackupHome : UserControl
{
    public UcDbBackupHome()
    {
        InitializeComponent();
    }

    private void llConfigure_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        new FrmDbBackupConfig().ShowDialog();
    }
}