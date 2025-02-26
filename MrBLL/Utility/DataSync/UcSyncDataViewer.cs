using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class UcSyncDataViewer<T> : XtraUserControl
{
    public UcSyncDataViewer(IList<T> datasource, string title, int initWidth)
    {
        InitializeComponent();

        if (datasource == null)
            throw new ArgumentNullException(nameof(datasource));

        lblTitle.Text = title;
        LoadData(gridView, datasource, initWidth);
    }

    public static void LoadData<T>(DataGridView gridView, IList<T> list, int initWidth)
    {
        var origAuto = gridView.AutoGenerateColumns;
        gridView.AutoGenerateColumns = true;
        gridView.DataSource = list;
        gridView.BindingContext = new BindingContext();
        gridView.AutoGenerateColumns = origAuto;

        var colWidth = gridView.Columns
            .GetColumnsWidth(DataGridViewElementStates.None);

        if (colWidth < initWidth)
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
}