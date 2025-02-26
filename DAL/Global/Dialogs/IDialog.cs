namespace MrDAL.Global.Dialogs;

public interface IDialog
{
    bool SaveConfirmed();

    bool ShowMessage();

    bool Confirm(string message, string title);
}