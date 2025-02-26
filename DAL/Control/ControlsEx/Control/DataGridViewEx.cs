using MrDAL.Core.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class DataGridViewEx : DataGridView
{
    private readonly CheckedListBox _mCheckedListBox;
    private readonly ToolStripDropDown _mPopup;
    public int MaxHeight = 300;
    public new int Width = 200;

    public DataGridViewEx()
    {
        base.DoubleBuffered = true;
        BackColor = SystemColors.ActiveCaption;
        BackgroundColor = SystemColors.ActiveCaption;
        GridColor = Color.DarkSlateBlue;
        ReadOnly = true;
        AllowUserToDeleteRows = false;
        AllowUserToOrderColumns = false;
        AllowUserToResizeRows = false;

        _mCheckedListBox = new CheckedListBox
        {
            CheckOnClick = true
        };
        _mCheckedListBox.ItemCheck += mCheckedListBox_ItemCheck;
        var mControlHost = new ToolStripControlHost(_mCheckedListBox)
        {
            Padding = Padding.Empty,
            Margin = Padding.Empty,
            AutoSize = true
        };
        _mPopup = new ToolStripDropDown
        {
            Padding = Padding.Empty
        };
        _mPopup.Items.Add(mControlHost);
        CellFormatting += RGrid_CellFormatting;
        ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

        DataBindingComplete += OnDataBindingComplete;
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

    public override sealed Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        foreach (DataGridViewColumn column in Columns)
        {
            var col = column.Name;
            if (column.ValueType == typeof(decimal)) column.DefaultCellStyle.Format = "N2";
        }
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

    protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
    {
        base.OnColumnHeaderMouseClick(e);
        if (!DisplayColumnChooser) return;
        if (e.Button != MouseButtons.Right) return;

        if (_mCheckedListBox.Items.Count > 0)
        {
            var preferredHeight = _mCheckedListBox.Items.Count * 16 + 7;
            _mCheckedListBox.Height = preferredHeight < MaxHeight ? preferredHeight : MaxHeight;
            _mCheckedListBox.Width = Width;
            _mCheckedListBox.Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _mPopup.Show(PointToScreen(new Point(e.X, e.Y)));
            _mPopup.BringToFront();
            return;
        }

        _mCheckedListBox.Items.Clear();
        foreach (DataGridViewColumn c in Columns)
            if (c.Visible)
                _mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
    }

    private void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (!_mCheckedListBox.Focused) return;
        var row = _mCheckedListBox.SelectedItem != null
            ? _mCheckedListBox.SelectedItem.ToString()
            : _mCheckedListBox.Items[0].ToString();
        var rowIndex = 0;
        foreach (DataGridViewColumn c in Columns)
        {
            var col = c.HeaderText;
            if (col != row) continue;
            rowIndex = Columns.IndexOf(c);
        }

        if (rowIndex > 0) Columns[rowIndex].Visible = Columns[rowIndex].Visible is false;
    }

    private void RGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (Columns["IsGroup"] == null)
        {
            return;
        }

        using var row = Rows[e.RowIndex];
        if (row.Cells[e.ColumnIndex].ValueType == typeof(decimal) && row.Cells[e.ColumnIndex].Value.GetDecimal() == 0)
        {
            e.Value = string.Empty;
            e.FormattingApplied = true;
        }

        row.DefaultCellStyle.ForeColor = row.Cells["IsGroup"].Value.GetInt() switch
        {
            1 or 11 => Color.Blue,
            2 or 22 => Color.BlueViolet,
            3 or 33 => Color.IndianRed,
            12 => Color.Red,
            13 => Color.Blue,
            _ => Color.Black
        };

        row.DefaultCellStyle.Font = row.Cells["IsGroup"].Value.GetInt() switch
        {
            11 or 22 or 33 or 99 => new Font("Bookman Old Style", 11, FontStyle.Bold),
            111 => new Font("Bookman Old Style", 14, FontStyle.Bold),
            88 or 10 => new Font("Bookman Old Style", 11, FontStyle.Italic),
            _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
        };

        var result = row.DefaultCellStyle.Alignment;
        row.DefaultCellStyle.Alignment = row.Cells["IsGroup"].Value.GetInt() switch
        {
            11 or 13 or 22 or 33 or 88 or 99 => DataGridViewContentAlignment.MiddleRight,
            111 => DataGridViewContentAlignment.MiddleCenter,
            _ => DataGridViewContentAlignment.NotSet
        };
    }

    public static void InvokeIfRequired2<T>(T control, Action<T> action) where T : System.Windows.Forms.Control
    {
        if (control.InvokeRequired)
            control.Invoke(new Action(() => action(control)));
        else
            action(control);
    }
}