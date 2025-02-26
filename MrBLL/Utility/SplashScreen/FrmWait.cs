using DevExpress.XtraWaitForm;

namespace MrBLL.Utility.SplashScreen;

public partial class FrmWait : WaitForm
{
    #region ------------- WAIT FORM -------------

    public FrmWait()
    {
        InitializeComponent();
        progressPanel1.AutoHeight = true;
    }

    #endregion ------------- WAIT FORM -------------

    #region -------------- EVENTS --------------

    public override void SetCaption(string caption)
    {
        base.SetCaption(caption);
        progressPanel1.Caption = caption;
    }

    public override void SetDescription(string description)
    {
        base.SetDescription(description);
        progressPanel1.Description = description;
    }

    #endregion -------------- EVENTS --------------
}