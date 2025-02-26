using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MrDAL.Global.Control;

public class ClsEntryControl
{
    public ClsEntryControl()
    {
        EntryControl = new List<EntryControlViewModel>();
    }

    private List<EntryControlViewModel> EntryControl { get; }

    public void GetEntryControl(string entryModule)
    {
        var cmdString = new StringBuilder();
        cmdString.Append("Select * from EntryControl \n");
        if (!string.IsNullOrEmpty(entryModule)) cmdString.Append($"WHERE EntryModule='{entryModule}' \n");

        var dt = SqlExtensions
            .ExecuteDataSet(cmdString.ToString()).Tables[0];
        if (dt.Rows.Count <= 0) return;
        foreach (DataRow ro in dt.Rows)
        {
            var entryControls = new EntryControlViewModel();
            if (ro["EntryModule"] != DBNull.Value) entryControls.Entry_Type = ro["EntryModule"].ToString();
            if (ro["ControlName"] != DBNull.Value) entryControls.Control_Name = ro["ControlName"].ToString();
            if (ro["ControlValue"] != DBNull.Value) entryControls.Control_Value = ro["ControlValue"].ToString();
            if (ro["MandatoryOpt"] != DBNull.Value) entryControls.Mandatory_opt = ro["MandatoryOpt"].ToString();
            EntryControl.Add(entryControls);
        }
    }
}