using MrDAL.Models.Common;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class ComboBoxExt
{
    // RETURN VALUE IN COMBO BOX
    public static ComboBox BindTermModule(this ComboBox box, string module)
    {
        var list = new List<ValueModel<string, string>>
        {
            new(module.Equals("PB") ? "Purchase Invoice" : "Sales Invoice", module),
            new(module.Equals("PR") ? "Purchase Return" : "Sales Return", module)
        };
        if (list.Count <= 0) return box;
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
        return box;
    }
    public static ComboBox BindTermType(this ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("General", "G"),
            new("Additional", "A"),
            new("RoundOff", "R")
        };
        if (list.Count > 0)
        {
            box.DataSource = list;
            box.DisplayMember = "Item1";
            box.ValueMember = "Item2";
            box.SelectedIndex = 0;
        }

        return box;
    }
    public static ComboBox BindTermBasis(this ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Value", "V"),
            new("Quantity", "Q")
        };
        if (list.Count > 0)
        {
            box.DataSource = list;
            box.DisplayMember = "Item1";
            box.ValueMember = "Item2";
            box.SelectedIndex = 0;
        }

        return box;
    }
    public static ComboBox BindTermCondition(this ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Bill Wise", "B"),
            new("Product Wise", "P")
        };
        if (list.Count <= 0) return box;
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
        return box;
    }
    public static ComboBox BindPrinterList(this ComboBox box)
    {
        box.Items.Clear();
        if (PrinterSettings.InstalledPrinters.Count <= 0)
        {
            MessageBox.Show(@"PRINTER NOT FOUND..!!");
            return box;
        }
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            box.Items.Add(printer);
        }
        box.SelectedIndex = 0;
        return box;
    }
}