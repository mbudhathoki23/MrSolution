using DatabaseModule.Master.InventorySetup;
using MrDAL.Control.WinControl;
using System;
using System.Collections.Generic;

namespace MrBLL.Domains.POS.Master;

public partial class FrmCounterSelection : MrForm
{
    private bool _accepted;
    private Counter _selectedCounter;

    public FrmCounterSelection(IList<Counter> counters)
    {
        InitializeComponent();
        bsCounters.DataSource = counters ?? new List<Counter>();
        ActiveControl = gridCounters;
    }

    private void FrmCounterSelection_Load(object sender, EventArgs e)
    {
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        if (gridCounters.SelectedRows.Count == 0 ||
            gridCounters.SelectedRows[0].DataBoundItem is not Counter model) return;
        _accepted = true;
        _selectedCounter = model;
        Close();
    }

    private void gridCounters_EnterKeyPressed(object sender, EventArgs e)
    {
        if (gridCounters.SelectedRows.Count == 1) BtnAccept.PerformClick();
    }

    public static (bool Accept, Counter Model) SelectCounter(IList<Counter> counters)
    {
        var form = new FrmCounterSelection(counters);
        form.ShowDialog();
        return (form._accepted, form._selectedCounter);
    }
}