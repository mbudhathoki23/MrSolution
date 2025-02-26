using MrDAL.Global.Common;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public sealed class MrPanel : Panel
{
    public MrPanel()
    {
        BackColor = ObjGlobal.Project.Equals("RUDRALEKHA") ? Color.LightBlue :
            ObjGlobal.Project.Equals("SIYZO") ? Color.MediumAquamarine : SystemColors.InactiveCaption;
        Font = new Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
    }
}