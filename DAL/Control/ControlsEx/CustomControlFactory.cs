using DevExpress.XtraEditors;
using System;

namespace MrDAL.Control.ControlsEx;

internal static class CustomControlFactory
{
    public static System.Windows.Forms.Control CreateControl(Type controlType)
    {
        if (controlType == typeof(SimpleButton)) return new CustomSimpleButton();
        if (controlType == typeof(CheckEdit)) return new CustomCheckEdit();
        if (controlType == typeof(TextEdit)) return new CustomTextEdit();
        if (controlType == typeof(LabelControl)) return new CustomLabelControl();
        throw new ArgumentException("controlType");
    }
}