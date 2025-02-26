using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class TreeViewEx : TreeView
{
    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
        string pszSubIdList);

    protected override void CreateHandle()
    {
        base.CreateHandle();
        SetWindowTheme(Handle, "explorer", null);
    }

    public void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
    {
        foreach (TreeNode node in treeNode.Nodes)
        {
            node.Checked = nodeChecked;
            if (node.Nodes.Count > 0)
                // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                CheckAllChildNodes(node, nodeChecked);
        }
    }

    protected override void OnAfterCheck(TreeViewEventArgs e)
    {
        if (e.Action != TreeViewAction.Unknown)
            if (e.Node.Nodes.Count > 0)
                CheckAllChildNodes(e.Node, e.Node.Checked);

        if (e.Node.Parent != null)
        {
            var n = e.Node;
            while (n.Parent != null)
            {
                if (n.Checked) n.Parent.Checked = true;
                n = n.Parent;
            }
        }
    }

    public List<T> EnumerateAllTreeNodes<T>(TreeView tree, T parentNode = null) where T : TreeNode
    {
        if (parentNode != null && parentNode.Nodes.Count == 0)
            return new List<T>();

        var nodes = parentNode != null ? parentNode.Nodes : tree.Nodes;
        var childList = nodes.Cast<T>().ToList();

        var result = new List<T>(1024); //Preallocate space for children
        result.AddRange(childList); //Level first

        //Recursion on each child node
        childList.ForEach(n => result.AddRange(EnumerateAllTreeNodes(tree, n)));

        return result;
    }
}