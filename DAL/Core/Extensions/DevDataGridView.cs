using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;

namespace MrDAL.Core.Extensions;

public static class DevDataGridView
{
    public static GridColumn AddGridColumn(this GridView gridView1, int width, string caption, string fieldName,
        bool isVisible = true, UnboundColumnType columnType = UnboundColumnType.String)
    {
        var index = gridView1.Columns.Count;
        var column = new GridColumn
        {
            Caption = caption,
            FieldName = fieldName,
            Visible = isVisible,
            Width = width,
            AbsoluteIndex = index,
            UnboundType = columnType
        };
        gridView1.Columns.Add(column);
        return column;
    }

    public static GridColumn AddGridColumn(this GridView gridView1, string caption, string fieldName, string name,
        int width, bool isVisible = true, int index = 0, UnboundColumnType columnType = UnboundColumnType.String)
    {
        index = gridView1.Columns.Count;
        var column = new GridColumn
        {
            Caption = caption,
            FieldName = fieldName,
            Name = name,
            Width = width,
            Visible = isVisible,
            VisibleIndex = index,
            UnboundType = columnType,
            UnboundDataType = columnType switch
            {
                UnboundColumnType.Decimal => typeof(decimal),
                UnboundColumnType.DateTime => typeof(DateTime),
                UnboundColumnType.Boolean => typeof(bool),
                _ => typeof(string)
            },
            DisplayFormat =
            {
                FormatType = columnType switch
                {
                    UnboundColumnType.Decimal => FormatType.Numeric,
                    UnboundColumnType.DateTime => FormatType.DateTime,
                    _ => FormatType.None
                },
                FormatString = columnType switch
                {
                    UnboundColumnType.Decimal => "#.00",
                    _ => ""
                }
            }
        };
        gridView1.Columns.Add(column);
        return column;
    }
}