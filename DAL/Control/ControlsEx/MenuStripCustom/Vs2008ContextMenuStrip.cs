using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008ContextMenuStrip : ContextMenuStrip
{
    public Vs2008ContextMenuStrip()
    {
        Renderer = new Vs2008MenuRenderer();
    }
}