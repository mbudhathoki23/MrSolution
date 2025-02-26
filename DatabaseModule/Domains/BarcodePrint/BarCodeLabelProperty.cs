using System;
using System.Drawing;

namespace DatabaseModule.Domains.BarcodePrint;

[Serializable]
public class BarCodeLabelProperty
{
    public BarCodeLabelProperty()
    {
        FontName = "Arial";
        FontStyle = FontStyle.Regular;
        FontSize = 8.25f;
    }

    public string FontName { get; set; }
    public float FontSize { get; set; }
    public FontStyle FontStyle { get; set; }
}