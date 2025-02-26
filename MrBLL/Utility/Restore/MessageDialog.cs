using MrBLL.Utility.Restore.Properties;
using MrDAL.Control.WinControl;
using System.Windows.Forms;

namespace MrBLL.Utility.Restore;

public partial class MessageDialog : MrForm
{
    public MessageDialog()
    {
        InitializeComponent();
    }

    public static DialogResult ShowQuestion(Form owner, string text, string title, bool needUac)
    {
        var messageDialog = new MessageDialog { Text = title, messageBox = { Text = text } };
        if (needUac) UserAccount.SetUacShield(messageDialog.yesButton);
        return messageDialog.ShowDialog(owner);
    }
}