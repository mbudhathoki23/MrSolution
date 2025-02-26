using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MrDAL.Control.WinControl;

public partial class MrForm : Form
{
    int x = 0, y = 0;
    Thread t;
    public MrForm()
    {
        InitializeComponent();
        KeyPreview = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        CausesValidation = false;
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    private void MrForm_Load(object sender, EventArgs e)
    {
        DoubleBuffered = true;
        //Icon = ObjGlobal.Caption is "SIYZO" ? Resources.SiyZo : Resources.MrLogo;
    }
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
            return handleParam;
        }
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        var p = new Pen(Color.Orange);
        using Brush b = new SolidBrush(Color.Red);
        g.FillEllipse(b, x, y, 0, 0);
    }
}