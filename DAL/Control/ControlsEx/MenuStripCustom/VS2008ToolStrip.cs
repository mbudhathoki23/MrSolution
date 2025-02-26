using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008ToolStrip : ToolStrip
{
    public Vs2008ToolStrip()
    {
        Renderer = new Vs2008ToolStripRenderer();
    }
}