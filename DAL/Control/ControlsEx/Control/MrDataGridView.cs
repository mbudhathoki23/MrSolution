using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class MrDataGridView : DataGridView
{
    private readonly CheckedListBox _mCheckedListBox;
    private readonly ToolStripDropDown _mPopup;
    public int MaxHeight = 300;
    public new int Width = 200;

    public MrDataGridView()
    {
        base.DoubleBuffered = true;
        BackColor = SystemColors.ActiveCaption;
        BackgroundColor = SystemColors.ActiveCaption;
        GridColor = Color.DarkSlateBlue;
        ReadOnly = true;
        AllowUserToDeleteRows = false;

        _mCheckedListBox = new CheckedListBox
        {
            CheckOnClick = true
        };
        _mCheckedListBox.ItemCheck += MCheckedListBox_ItemCheck;
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
    }

    [Browsable(true)]
    public bool DoubleBufferEnabled
    {
        get => base.DoubleBuffered;
        set => base.DoubleBuffered = value;
    }

    [Browsable(true)]
    [DefaultValue(false)]
    public bool DisplayColumnChooser { get; set; }

    public override sealed Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
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

    private void MCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
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
}