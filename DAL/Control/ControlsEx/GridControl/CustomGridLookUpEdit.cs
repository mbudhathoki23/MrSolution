using DevExpress.XtraEditors;
using System.ComponentModel;

namespace MrDAL.Control.ControlsEx.GridControl;

public class CustomGridLookUpEdit : GridLookUpEdit
{
    static CustomGridLookUpEdit()
    {
        RepositoryItemCustomGridLookUpEdit.RegisterCustomGridLookUpEdit();
    }

    public override string EditorTypeName => RepositoryItemCustomGridLookUpEdit.CustomGridLookUpEditName;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new RepositoryItemCustomGridLookUpEdit Properties =>
        base.Properties as RepositoryItemCustomGridLookUpEdit;
}