using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008MenuStrip : MenuStrip
{
    public Vs2008MenuStrip()
    {
        Renderer = new Vs2008MenuRenderer();
    }
}