using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class DataGridViewExt
{
    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText)
    {
        return AddColumn(gridView, name, headerText, name, 110, 90, true, DataGridViewContentAlignment.MiddleLeft,
            DataGridViewColumnSortMode.NotSortable);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, 2, DataGridViewAutoSizeColumnMode.None);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, mimWidth,
            DataGridViewAutoSizeColumnMode.None);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2, bool visible = true)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, mimWidth, visible,
            DataGridViewContentAlignment.MiddleLeft, DataGridViewColumnSortMode.NotSortable);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2, bool visible = true,
        DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, mimWidth, visible, alignment,
            DataGridViewColumnSortMode.NotSortable);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2, bool visible = true,
        DataGridViewAutoSizeColumnMode columnMode = DataGridViewAutoSizeColumnMode.None)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, mimWidth, visible,
            DataGridViewContentAlignment.MiddleLeft, DataGridViewColumnSortMode.NotSortable, columnMode);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2,
        DataGridViewAutoSizeColumnMode columnMode = DataGridViewAutoSizeColumnMode.None)
    {
        return AddColumn(gridView, name, headerText, dataProperty, width, mimWidth, true,
            DataGridViewContentAlignment.MiddleLeft, DataGridViewColumnSortMode.NotSortable, columnMode);
    }

    public static DataGridViewTextBoxColumn AddColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2, bool visible = true,
        DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft,
        DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.NotSortable,
        DataGridViewAutoSizeColumnMode columnMode = DataGridViewAutoSizeColumnMode.None)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = name,
            HeaderText = headerText,
            DataPropertyName = dataProperty.IsBlankOrEmpty() ? name : dataProperty,
            Width = width ?? 100,
            MinimumWidth = mimWidth > 0 ? (int)mimWidth : 2,
            Visible = visible,
            DefaultCellStyle = { Alignment = alignment },
            AutoSizeMode = columnMode,
            SortMode = sortMode,
            ValueType = typeof(string)
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        gridView.Columns.Add(column);
        return column;
    }

    public static DataGridViewCheckBoxColumn AddCheckColumn(this DataGridView gridView, string name, string headerText,
        string dataProperty, int? width = 100, int? mimWidth = 2, bool visible = true)
    {
        var column = new DataGridViewCheckBoxColumn
        {
            Name = name,
            HeaderText = headerText,
            DataPropertyName = dataProperty.IsBlankOrEmpty() ? name : dataProperty,
            Width = width ?? 100,
            MinimumWidth = mimWidth ?? 50,
            Visible = visible,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
            SortMode = DataGridViewColumnSortMode.NotSortable
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        gridView.Columns.Add(column);
        return column;
    }
}