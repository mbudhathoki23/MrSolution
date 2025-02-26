using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrDAL.Utility.GridControl;

public class StackedHeaderGenerator : IStackedHeaderGenerator
{
    static StackedHeaderGenerator()
    {
        Instance = new StackedHeaderGenerator();
    }

    private StackedHeaderGenerator()
    {
    }

    public static StackedHeaderGenerator Instance { get; }

    public Header GenerateStackedHeader(DataGridView objGridView)
    {
        var objParentHeader = new Header();
        var objHeaderTree = new Dictionary<string, Header>();
        var iX = 0;
        foreach (DataGridViewColumn objColumn in objGridView.Columns)
        {
            var segments = objColumn.HeaderText.Split('.');
            if (segments.Length > 0)
            {
                var segment = segments[0];
                Header tempHeader, lastTempHeader = null;
                if (objHeaderTree.ContainsKey(segment))
                {
                    tempHeader = objHeaderTree[segment];
                }
                else
                {
                    tempHeader = new Header { Name = segment, X = iX };
                    objParentHeader.Children.Add(tempHeader);
                    objHeaderTree[segment] = tempHeader;
                    tempHeader.ColumnId = objColumn.Index;
                }

                for (var i = 1; i < segments.Length; ++i)
                {
                    segment = segments[i];
                    var found = false;
                    foreach (var child in tempHeader.Children)
                        if (0 == string.Compare(child.Name, segment, StringComparison.InvariantCultureIgnoreCase))
                        {
                            found = true;
                            lastTempHeader = tempHeader;
                            tempHeader = child;
                            break;
                        }

                    if (!found || i == segments.Length - 1)
                    {
                        var temp = new Header { Name = segment, X = iX };
                        temp.ColumnId = objColumn.Index;
                        if (found && i == segments.Length - 1 && null != lastTempHeader)
                            lastTempHeader.Children.Add(temp);
                        else
                            tempHeader.Children.Add(temp);
                        tempHeader = temp;
                    }
                }
            }

            iX += objColumn.Width;
        }

        return objParentHeader;
    }
}