using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

/// <summary>A special custom rounding GroupBox with many painting features.</summary>
[ToolboxBitmap(typeof(MrGroup), "CodeVendor.Controls.Grouper.bmp")]
[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
public class MrGroup : UserControl
{
    #region Enumerations

    /// <summary>A special gradient enumeration.</summary>
    public enum GroupBoxGradientMode
    {
        /// <summary>Specifies no gradient mode.</summary>
        None = 4,

        /// <summary>Specifies a gradient from upper right to lower left.</summary>
        BackwardDiagonal = 3,

        /// <summary>Specifies a gradient from upper left to lower right.</summary>
        ForwardDiagonal = 2,

        /// <summary>Specifies a gradient from left to right.</summary>
        Horizontal = 0,

        /// <summary>Specifies a gradient from top to bottom.</summary>
        Vertical = 1
    }

    #endregion Enumerations

    #region Constructor

    /// <summary>This method will construct a new GroupBox control.</summary>
    public MrGroup()
    {
        InitializeStyles();
        InitializeGroupBox();
    }

    #endregion Constructor

    #region DeConstructor

    /// <summary>This method will dispose of the GroupBox control.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing) _components?.Dispose();
        base.Dispose(disposing);
    }

    #endregion DeConstructor

    #region Protected Methods

    /// <summary>Overrides the OnPaint method to paint control.</summary>
    /// <param name="e">The paint event arguments.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        PaintBack(e.Graphics);
        PaintGroupText(e.Graphics);
    }

    #endregion Protected Methods

    #region Variables

    private Container _components;
    private int _vRoundCorners = 10;
    private string _vGroupTitle = "MrGroup";
    private Color _vBorderColor = Color.White;
    private float _vBorderThickness = 1;
    private bool _vShadowControl;
    private Color _vBackgroundColor = SystemColors.InactiveCaption;
    private Color _vBackgroundGradientColor = Color.White;
    private GroupBoxGradientMode _vBackgroundGradientMode = GroupBoxGradientMode.Vertical;
    private Color _vShadowColor = Color.DarkGray;
    private int _vShadowThickness = 3;
    private Image _vGroupImage;
    private Color _vCustomGroupBoxColor = Color.White;
    private bool _vPaintGroupBox;
    private Color _vBackColor = Color.Transparent;

    #endregion Variables

    #region Properties

    /// <summary>This feature will paint the background color of the control.</summary>
    [Category("Appearance")]
    [Description("This feature will paint the background color of the control.")]
    public override Color BackColor
    {
        get => _vBackColor;
        set
        {
            _vBackColor = value;
            Refresh();
        }
    }

    /// <summary>This feature will paint the group title background to the specified color if PaintGroupBox is set to true.</summary>
    [Category("Appearance")]
    [Description(
        "This feature will paint the group title background to the specified color if PaintGroupBox is set to true.")]
    public Color CustomGroupBoxColor
    {
        get => _vCustomGroupBoxColor;
        set
        {
            _vCustomGroupBoxColor = value;
            Refresh();
        }
    }

    /// <summary>This feature will paint the group title background to the CustomGroupBoxColor.</summary>
    [Category("Appearance")]
    [Description("This feature will paint the group title background to the CustomGroupBoxColor.")]
    public bool PaintGroupBox
    {
        get => _vPaintGroupBox;
        set
        {
            _vPaintGroupBox = value;
            Refresh();
        }
    }

    /// <summary>This feature can add a 16 x 16 image to the group title bar.</summary>
    [Category("Appearance")]
    [Description("This feature can add a 16 x 16 image to the group title bar.")]
    public Image GroupImage
    {
        get => _vGroupImage;
        set
        {
            _vGroupImage = value;
            Refresh();
        }
    }

    /// <summary>This feature will change the control's shadow color.</summary>
    [Category("Appearance")]
    [Description("This feature will change the control's shadow color.")]
    public Color ShadowColor
    {
        get => _vShadowColor;
        set
        {
            _vShadowColor = value;
            Refresh();
        }
    }

    /// <summary>This feature will change the size of the shadow border.</summary>
    [Category("Appearance")]
    [Description("This feature will change the size of the shadow border.")]
    public int ShadowThickness
    {
        get => _vShadowThickness;
        set
        {
            _vShadowThickness = value switch
            {
                > 10 => 10,
                < 1 => 1,
                _ => value
            };

            Refresh();
        }
    }

    /// <summary>
    ///     This feature will change the group control color. This color can also be used in combination with
    ///     BackgroundGradientColor for a gradient paint.
    /// </summary>
    [Category("Appearance")]
    [Description(
        "This feature will change the group control color. This color can also be used in combination with BackgroundGradientColor for a gradient paint.")]
    public Color BackgroundColor
    {
        get => _vBackgroundColor;
        set
        {
            _vBackgroundColor = value;
            Refresh();
        }
    }

    /// <summary>This feature can be used in combination with BackgroundColor to create a gradient background.</summary>
    [Category("Appearance")]
    [Description("This feature can be used in combination with BackgroundColor to create a gradient background.")]
    public Color BackgroundGradientColor
    {
        get => _vBackgroundGradientColor;
        set
        {
            _vBackgroundGradientColor = value;
            Refresh();
        }
    }

    /// <summary>This feature turns on background gradient painting.</summary>
    [Category("Appearance")]
    [Description("This feature turns on background gradient painting.")]
    public GroupBoxGradientMode BackgroundGradientMode
    {
        get => _vBackgroundGradientMode;
        set
        {
            _vBackgroundGradientMode = value;
            Refresh();
        }
    }

    /// <summary>This feature will round the corners of the control.</summary>
    [Category("Appearance")]
    [Description("This feature will round the corners of the control.")]
    public int RoundCorners
    {
        get => _vRoundCorners;
        set
        {
            _vRoundCorners = value switch
            {
                > 25 => 25,
                < 1 => 1,
                _ => value
            };

            Refresh();
        }
    }

    /// <summary>This feature will add a group title to the control.</summary>
    [Category("Appearance")]
    [Description("This feature will add a group title to the control.")]
    public string GroupTitle
    {
        get => _vGroupTitle;
        set
        {
            _vGroupTitle = value;
            Refresh();
        }
    }

    /// <summary>This feature will allow you to change the color of the control's border.</summary>
    [Category("Appearance")]
    [Description("This feature will allow you to change the color of the control's border.")]
    public Color BorderColor
    {
        get => _vBorderColor;
        set
        {
            _vBorderColor = value;
            Refresh();
        }
    }

    /// <summary>This feature will allow you to set the control's border size.</summary>
    [Category("Appearance")]
    [Description("This feature will allow you to set the control's border size.")]
    public float BorderThickness
    {
        get => _vBorderThickness;
        set
        {
            _vBorderThickness = value switch
            {
                > 3 => 3,
                < 1 => 1,
                _ => value
            };
            Refresh();
        }
    }

    /// <summary>This feature will allow you to turn on control shadowing.</summary>
    [Category("Appearance")]
    [Description("This feature will allow you to turn on control shadowing.")]
    public bool ShadowControl
    {
        get => _vShadowControl;
        set
        {
            _vShadowControl = value;
            Refresh();
        }
    }

    #endregion Properties

    #region Initialization

    /// <summary>This method will initialize the controls custom styles.</summary>
    private void InitializeStyles()
    {
        //Set the control styles----------------------------------
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        //--------------------------------------------------------
    }

    /// <summary>This method will initialize the GroupBox control.</summary>
    private void InitializeGroupBox()
    {
        _components = new Container();
        Resize += GroupBox_Resize;
        DockPadding.All = 20;
        Name = "GroupBox";
        Size = new Size(372, 101);
    }

    #endregion Initialization

    #region Private Methods

    /// <summary>This method will paint the group title.</summary>
    /// <param name="g">The paint event graphics object.</param>
    private void PaintGroupText(Graphics g)
    {
        //Check if string has something-------------
        if (GroupTitle == string.Empty) return;
        //------------------------------------------

        //Set Graphics smoothing mode to Anit-Alias--
        g.SmoothingMode = SmoothingMode.AntiAlias;
        //-------------------------------------------

        //Declare Variables------------------
        var stringSize = g.MeasureString(GroupTitle, Font);
        var stringSize2 = stringSize.ToSize();
        if (GroupImage != null) stringSize2.Width += 18;
        var arcWidth = RoundCorners;
        var arcHeight = RoundCorners;
        const int arcX1 = 20;
        var arcX2 = stringSize2.Width + 34 - (arcWidth + 1);
        const int arcY1 = 0;
        var arcY2 = 24 - (arcHeight + 1);
        var path = new GraphicsPath();
        Brush borderBrush = new SolidBrush(BorderColor);
        var borderPen = new Pen(borderBrush, BorderThickness);
        LinearGradientBrush backgroundGradientBrush = null;
        Brush backgroundBrush =
            PaintGroupBox ? new SolidBrush(CustomGroupBoxColor) : new SolidBrush(BackgroundColor);
        var textColorBrush = new SolidBrush(ForeColor);
        SolidBrush shadowBrush = null;
        GraphicsPath shadowPath = null;
        //-----------------------------------

        //Check if shadow is needed----------
        if (ShadowControl)
        {
            shadowBrush = new SolidBrush(ShadowColor);
            shadowPath = new GraphicsPath();
            shadowPath.AddArc(arcX1 + (ShadowThickness - 1), arcY1 + (ShadowThickness - 1), arcWidth, arcHeight,
                180, GroupBoxConstants.SweepAngle); // Top Left
            shadowPath.AddArc(arcX2 + (ShadowThickness - 1), arcY1 + (ShadowThickness - 1), arcWidth, arcHeight,
                270, GroupBoxConstants.SweepAngle); //Top Right
            shadowPath.AddArc(arcX2 + (ShadowThickness - 1), arcY2 + (ShadowThickness - 1), arcWidth, arcHeight,
                360, GroupBoxConstants.SweepAngle); //Bottom Right
            shadowPath.AddArc(arcX1 + (ShadowThickness - 1), arcY2 + (ShadowThickness - 1), arcWidth, arcHeight, 90,
                GroupBoxConstants.SweepAngle); //Bottom Left
            shadowPath.CloseAllFigures();

            //Paint Rounded Rectangle------------
            g.FillPath(shadowBrush, shadowPath);
            //-----------------------------------
        }
        //-----------------------------------

        //Create Rounded Rectangle Path------
        path.AddArc(arcX1, arcY1, arcWidth, arcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
        path.AddArc(arcX2, arcY1, arcWidth, arcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
        path.AddArc(arcX2, arcY2, arcWidth, arcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
        path.AddArc(arcX1, arcY2, arcWidth, arcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
        path.CloseAllFigures();
        //-----------------------------------

        //Check if Gradient Mode is enabled--
        if (PaintGroupBox)
        {
            //Paint Rounded Rectangle------------
            g.FillPath(backgroundBrush, path);
            //-----------------------------------
        }
        else
        {
            if (BackgroundGradientMode == GroupBoxGradientMode.None)
            {
                //Paint Rounded Rectangle------------
                g.FillPath(backgroundBrush, path);
                //-----------------------------------
            }
            else
            {
                backgroundGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height),
                    BackgroundColor, BackgroundGradientColor, (LinearGradientMode)BackgroundGradientMode);

                //Paint Rounded Rectangle------------
                g.FillPath(backgroundGradientBrush, path);
                //-----------------------------------
            }
        }
        //-----------------------------------

        //Paint Boarded-----------------------
        g.DrawPath(borderPen, path);
        //-----------------------------------

        //Paint Text-------------------------
        var customStringWidth = GroupImage != null ? 44 : 28;
        g.DrawString(GroupTitle, Font, textColorBrush, customStringWidth, 5);
        //-----------------------------------

        //Draw GroupImage if there is one----
        if (GroupImage != null) g.DrawImage(GroupImage, 28, 4, 16, 16);
        //-----------------------------------

        //Destroy Graphic Objects------------
        path?.Dispose();
        borderBrush?.Dispose();
        borderPen?.Dispose();
        backgroundGradientBrush?.Dispose();
        backgroundBrush?.Dispose();
        textColorBrush?.Dispose();
        shadowBrush?.Dispose();
        shadowPath?.Dispose();
        //-----------------------------------
    }

    /// <summary>This method will paint the control.</summary>
    /// <param name="g">The paint event graphics object.</param>
    private void PaintBack(Graphics g)
    {
        //Set Graphics smoothing mode to Anit-Alias--
        g.SmoothingMode = SmoothingMode.AntiAlias;
        //-------------------------------------------

        //Declare Variables------------------
        var arcWidth = RoundCorners * 2;
        var arcHeight = RoundCorners * 2;
        const int arcX1 = 0;
        var arcX2 = ShadowControl ? Width - (arcWidth + 1) - ShadowThickness : Width - (arcWidth + 1);
        const int arcY1 = 10;
        var arcY2 = ShadowControl ? Height - (arcHeight + 1) - ShadowThickness : Height - (arcHeight + 1);
        var path = new GraphicsPath();
        Brush borderBrush = new SolidBrush(BorderColor);
        var borderPen = new Pen(borderBrush, BorderThickness);
        LinearGradientBrush backgroundGradientBrush = null;
        Brush backgroundBrush = new SolidBrush(BackgroundColor);
        SolidBrush shadowBrush = null;
        GraphicsPath shadowPath = null;
        //-----------------------------------

        //Check if shadow is needed----------
        if (ShadowControl)
        {
            shadowBrush = new SolidBrush(ShadowColor);
            shadowPath = new GraphicsPath();
            shadowPath.AddArc(arcX1 + ShadowThickness, arcY1 + ShadowThickness, arcWidth, arcHeight, 180,
                GroupBoxConstants.SweepAngle); // Top Left
            shadowPath.AddArc(arcX2 + ShadowThickness, arcY1 + ShadowThickness, arcWidth, arcHeight, 270,
                GroupBoxConstants.SweepAngle); //Top Right
            shadowPath.AddArc(arcX2 + ShadowThickness, arcY2 + ShadowThickness, arcWidth, arcHeight, 360,
                GroupBoxConstants.SweepAngle); //Bottom Right
            shadowPath.AddArc(arcX1 + ShadowThickness, arcY2 + ShadowThickness, arcWidth, arcHeight, 90,
                GroupBoxConstants.SweepAngle); //Bottom Left
            shadowPath.CloseAllFigures();

            //Paint Rounded Rectangle------------
            g.FillPath(shadowBrush, shadowPath);
            //-----------------------------------
        }
        //-----------------------------------

        //Create Rounded Rectangle Path------
        path.AddArc(arcX1, arcY1, arcWidth, arcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
        path.AddArc(arcX2, arcY1, arcWidth, arcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
        path.AddArc(arcX2, arcY2, arcWidth, arcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
        path.AddArc(arcX1, arcY2, arcWidth, arcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
        path.CloseAllFigures();
        //-----------------------------------

        //Check if Gradient Mode is enabled--
        if (BackgroundGradientMode == GroupBoxGradientMode.None)
        {
            //Paint Rounded Rectangle------------
            g.FillPath(backgroundBrush, path);
            //-----------------------------------
        }
        else
        {
            backgroundGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BackgroundColor,
                BackgroundGradientColor, (LinearGradientMode)BackgroundGradientMode);

            //Paint Rounded Rectangle------------
            g.FillPath(backgroundGradientBrush, path);
            //-----------------------------------
        }
        //-----------------------------------

        //Paint Boarded-----------------------
        g.DrawPath(borderPen, path);
        //-----------------------------------

        //Destroy Graphic Objects------------
        path?.Dispose();
        borderBrush?.Dispose();
        borderPen?.Dispose();
        backgroundGradientBrush?.Dispose();
        backgroundBrush?.Dispose();
        shadowBrush?.Dispose();
        shadowPath?.Dispose();
        //-----------------------------------
    }

    /// <summary>This method fires when the GroupBox resize event occurs.</summary>
    /// <param name="sender">The object the sent the event.</param>
    /// <param name="e">The event arguments.</param>
    private void GroupBox_Resize(object sender, EventArgs e)
    {
        Refresh();
    }

    #endregion Private Methods
}