using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class XtraLayoutPanelViewBase : XtraForm
{
    private static readonly object ControlSelectRequestEventKey = new();
    private static readonly object ControlDeleteRequestEventKey = new();

    private static readonly IntPtr DeleteKeyWParam = new(0x2E);
    private static readonly IntPtr SysCommandMaximizeWParam = new(0xF032);

    public XtraLayoutPanelViewBase()
    {
        TopLevel = false;
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
    }

    public virtual XtraLayoutPanelBase LayoutPanel => null;

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
        base.OnClosing(e);
    }

    public void Initialize()
    {
        InitializeControls();
        LayoutPanel.ForEachChildControlIncludeItself(x =>
        {
            if (x is MaskBox) return;
            x.Click += OnChildControlMouseClick;
        });
        LayoutPanel.ControlAdded += OnChildControlAdded;
        LayoutPanel.ControlRemoved += OnChildControlRemoved;
    }

    public virtual void ResetLayout()
    {
        LayoutPanel.SuspendLayout();
        try
        {
            var controlsCollection = LayoutPanel.Controls;
            for (var n = controlsCollection.Count - 1; n >= 0; n--)
            {
                if (controlsCollection[n] is ICustomControl control)
                {
                    controlsCollection[n].Dispose();
                }
            }
        }
        finally
        {
            LayoutPanel.ResumeLayout();
        }
    }

    protected virtual void InitializeControls()
    {
    }

    protected override bool ProcessKeyPreview(ref Message msg)
    {
        const int wmKeyup = 257;
        if (msg.Msg == wmKeyup && msg.WParam == DeleteKeyWParam) OnControlDeleteRequest(new ControlEventArgs(null));
        return base.ProcessKeyPreview(ref msg);
    }

    protected override void WndProc(ref Message msg)
    {
        const int wmSysCommand = 274;
        if (msg.Msg == wmSysCommand)
            if (msg.WParam == SysCommandMaximizeWParam)
                return;
        base.WndProc(ref msg);
    }

    private void OnChildControlMouseClick(object sender, EventArgs e)
    {
        if (e is MouseEventArgs ee && ee.Button != MouseButtons.Left) return;
        OnControlSelectRequest(new ControlEventArgs((System.Windows.Forms.Control)sender));
    }

    #region Event Handlers

    private void OnChildControlAdded(object sender, ControlEventArgs e)
    {
        e.Control.Click += OnChildControlMouseClick;
    }

    private void OnChildControlRemoved(object sender, ControlEventArgs e)
    {
        e.Control.Click -= OnChildControlMouseClick;
    }

    #endregion Event Handlers

    #region Events

    public event EventHandler<ControlEventArgs> ControlSelectRequest
    {
        add => Events.AddHandler(ControlSelectRequestEventKey, value);
        remove => Events.RemoveHandler(ControlSelectRequestEventKey, value);
    }

    public event EventHandler<ControlEventArgs> ControlDeleteRequest
    {
        add => Events.AddHandler(ControlDeleteRequestEventKey, value);
        remove => Events.RemoveHandler(ControlDeleteRequestEventKey, value);
    }

    private void OnControlSelectRequest(ControlEventArgs e)
    {
        var handler = (EventHandler<ControlEventArgs>)Events[ControlSelectRequestEventKey];
        handler?.Invoke(this, e);
    }

    private void OnControlDeleteRequest(ControlEventArgs e)
    {
        var handler = (EventHandler<ControlEventArgs>)Events[ControlDeleteRequestEventKey];
        handler?.Invoke(this, e);
    }

    #endregion Events

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // XtraLayoutPanelViewBase
        // 
        this.ClientSize = new System.Drawing.Size(820, 511);
        this.Name = "XtraLayoutPanelViewBase";
        this.ResumeLayout(false);

    }
}