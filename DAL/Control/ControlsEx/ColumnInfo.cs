using DevExpress.Data;
using System;
using System.Collections.Generic;

namespace MrDAL.Control.ControlsEx;

internal class ColumnInfo : IDataColumnInfo
{
    public ColumnInfo(string name, string fieldName, Type fieldType, List<IDataColumnInfo> columns, string unboundExpression)
    {
        Name = name;
        FieldName = fieldName;
        FieldType = fieldType;
        Columns = columns;
        UnboundExpression = unboundExpression;
    }

    public string Caption => Caption;

    public List<IDataColumnInfo> Columns { get; }

    public DataControllerBase Controller => null;

    public string FieldName { get; }

    public Type FieldType { get; }

    public string Name { get; }

    public string UnboundExpression { get; set; }
}