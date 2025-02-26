using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.ComponentModel;

namespace MrDAL.Control.ControlsEx.GridControl;

[UserRepositoryItem("RegisterCustomGridLookUpEdit")]
public class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
{
    public const string CustomGridLookUpEditName = "CustomGridLookUpEdit";

    static RepositoryItemCustomGridLookUpEdit()
    {
        RegisterCustomGridLookUpEdit();
    }

    public RepositoryItemCustomGridLookUpEdit()
    {
        TextEditStyle = TextEditStyles.Standard;
        AutoComplete = false;
    }

    [Browsable(false)]
    public sealed override TextEditStyles TextEditStyle
    {
        get => base.TextEditStyle;
        set => base.TextEditStyle = value;
    }

    public override string EditorTypeName => CustomGridLookUpEditName;

    public static void RegisterCustomGridLookUpEdit()
    {
        EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomGridLookUpEditName,
            typeof(CustomGridLookUpEdit), typeof(RepositoryItemCustomGridLookUpEdit),
            typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
    }

    protected override DevExpress.XtraGrid.GridControl CreateGrid()
    {
        return new CustomGridControl();
    }
}