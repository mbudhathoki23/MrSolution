using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;

namespace MrDAL.Control.ControlsEx.GridControl;

public class CustomGridView : GridView
{
    protected override string ViewName => "CustomGridView";
    protected internal virtual string GetExtraFilterText => ExtraFilterText;

    protected internal virtual void SetGridControlAccessMethod(DevExpress.XtraGrid.GridControl newControl)
    {
        SetGridControl(newControl);
    }

    /// <inheritdoc />
    [Obsolete]
    protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
    {
        var subStringOperators = new List<CriteriaOperator>();
        foreach (var sString in text.Split(' '))
        {
            var exp = LikeData.CreateContainsPattern(sString);
            var columnsOperators = new List<CriteriaOperator>();
            for (var index = 0; index < Columns.Count; index++)
            {
                var col = Columns[index];
                if (col.Visible && col.ColumnType == typeof(string))
                {
                    columnsOperators.Add(new BinaryOperator(col.FieldName, exp, BinaryOperatorType.Like));
                }
            }

            subStringOperators.Add(new GroupOperator(GroupOperatorType.Or, columnsOperators));
        }

        return new GroupOperator(GroupOperatorType.And, subStringOperators).ToString();
    }
}