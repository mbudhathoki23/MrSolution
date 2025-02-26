using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class EntryGridViewEx : DataGridView
{
    public int MaxHeight = 300;
    public new int Width = 200;

    public EntryGridViewEx()
    {
        base.DoubleBuffered = true;
        BlockNavigationOnNextRowOnEnter = true;
        BackColor = SystemColors.ActiveCaption;
        BackgroundColor = SystemColors.ActiveCaption;
        GridColor = Color.DarkSlateBlue;
        ReadOnly = true;
        AllowUserToDeleteRows = false;
        AllowUserToOrderColumns = false;
        AllowUserToResizeRows = false;
        ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        RowHeadersDefaultCellStyle.Font = new Font("Bookman Old Style", 9);
        DefaultCellStyle.Font = new Font("Bookman Old Style", 9);
    }

    [Browsable(true)]
    public bool DoubleBufferEnabled
    {
        get => base.DoubleBuffered;
        set => base.DoubleBuffered = value;
    }

    [Browsable(true)]
    [DefaultValue(false)]
    public bool BlockNavigationOnNextRowOnEnter { get; set; }

    [Browsable(true)]
    [DefaultValue(false)]
    public bool DisplayColumnChooser { get; set; }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    public event EventHandler<EventArgs> EnterKeyPressed;

    protected virtual void OnEnterKeyPressed()
    {
        EnterKeyPressed?.Invoke(this, EventArgs.Empty);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (keyData == Keys.Enter && BlockNavigationOnNextRowOnEnter) return base.ProcessDialogKey(Keys.Tab);
        return base.ProcessDialogKey(keyData);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Delete && SelectionMode == DataGridViewSelectionMode.FullRowSelect)
            return base.ProcessCmdKey(ref msg, keyData);

        // Check if Enter is pressed
        if (keyData != Keys.Enter || SelectionMode != DataGridViewSelectionMode.FullRowSelect)
            return base.ProcessCmdKey(ref msg, keyData);
        // If there isn't any selected row, do nothing
        if (CurrentRow == null) return true;

        // Display first cell's value
        OnEnterKeyPressed();
        return true;
    }

    public static void InvokeIfRequired2<T>(T control, Action<T> action) where T : System.Windows.Forms.Control
    {
        if (control.InvokeRequired)
            control.Invoke(new Action(() => action(control)));
        else
            action(control);
    }
}