using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using System;

namespace MrDAL.Control.Dialogs;

public partial class FrmDeleteConfirm : MrForm
{
    private readonly bool _remarksRequired;
    private bool _accepted;
    private string _reason;

    public FrmDeleteConfirm(string title, string message, bool remarksRequired, string acceptCaption = "Yes",
        string remarks = null)
    {
        InitializeComponent();
        _remarksRequired = remarksRequired;
        base.Text = title;
        lblMessage.Text = message;
        textBox1.Text = remarks;

        lblAsterik.Visible = remarksRequired;
        btnYes.Text = acceptCaption;
    }

    private void btnYes_Click(object sender, EventArgs e)
    {
        if (_remarksRequired && string.IsNullOrWhiteSpace(textBox1.Text))
        {
            this.NotifyError("Enter remarks");
            return;
        }

        _accepted = true;
        _reason = textBox1.Text.Trim();
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    public static (bool Accepted, string Remarks) Confirmed(string title,
        string message, bool remarksRequired, string remarks = null, string acceptCaption = "Yes")
    {
        var form = new FrmDeleteConfirm(title, message, remarksRequired, "Yes", remarks);
        form.ShowDialog();
        return (form._accepted, form._reason);
    }
}