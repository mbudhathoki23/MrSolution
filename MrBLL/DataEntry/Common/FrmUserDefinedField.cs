using MrDAL.Control.WinControl;
using MrDAL.DataEntry.CommonSetup;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Global.Common;
using System;
using System.Data;

namespace MrBLL.DataEntry.Common;

public partial class FrmUserDefinedField : MrForm
{
    private readonly string _actionTag = string.Empty;
    private readonly IUserDefinedFieldRepository _userDefinedFieldRepository = new UserDefinedFieldRepository();
    private DataTable _table = new();
    private string _value = string.Empty;
    public string List = string.Empty;

    public FrmUserDefinedField()
    {
        InitializeComponent();
    }

    private void frmUserDefinedField_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        SourceGrid.Focus();
        ClearControl();
        if (!string.IsNullOrEmpty(List)) LoadMainGrid();
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? "UDF [NEW]" : "User Defined Field";
        TxtFieldName.Clear();
    }

    public void LoadMainGrid()
    {
        SourceGrid.Rows.Clear();
        _table = _userDefinedFieldRepository.GetDataTable();
        if (_table.Rows.Count > 0) SourceGrid.DataSource = _table;
    }
}