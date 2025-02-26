using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrDAL.Global.Control;

public static class ClsKeyPreview
{
    public static void KeyEvent(KeyEventArgs e, string actionTag, TextBox textBox, Button button)
    {
        var key = (char)e.KeyCode;
        var keyValue = e.KeyValue;
        var keyData = e.KeyData.ToString();
        if ((key >= 113 && key <= 123) || (key >= 33 && key <= 40) || key == 13 || key == 16 || key == 17 ||
            key == 18 || key == 20 || key == 27 || key == 45 || key == 144)
        {
        }
        else if (keyValue == 8 || key == 46)
        {
            textBox.Text = string.Empty;
        }
        else if (keyData is "Keys.O | Keys.Alt" or "Keys.S | Keys.Alt" or "Keys.C | Keys.Alt" or "O, Alt" or "Keys.Alt | Keys.Tab" or "S, Alt")
        {
        }
        else if (keyData == "A, Control")
        {
            textBox.SelectAll();
        }
        else if (keyData == "S, Control")
        {
        }
        else if (keyData == "C, Control")
        {
            textBox.Copy();
        }
        else if (keyData == "V, Control")
        {
            textBox.Paste();
        }
        else if (keyData == "X, Control")
        {
            textBox.Cut();
        }
        else if (keyData == "Z, Control")
        {
            textBox.Undo();
        }
        else if (keyData == "N, Control")
        {
        }
        else
        {
            if ((key == 32 || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 112) && actionTag is "EDIT" or "DELETE")
            {
                var searchKey = key.ToString();
                var val = -1;
                if (keyValue is >= (int)Keys.NumPad0 and <= (int)Keys.NumPad9)
                {
                    val = keyValue - (int)Keys.NumPad0;
                    searchKey = val.ToString();
                }
                else if (keyValue is >= (int)Keys.D0 and <= (int)Keys.D9)
                {
                    val = keyValue - (int)Keys.D0;
                    searchKey = val.ToString();
                }

                ObjGlobal.SearchText = searchKey;
                button.PerformClick();
                ObjGlobal.SearchText = string.Empty;
            }
        }
    }

    public static void KeyEvent(KeyEventArgs e, string actionTag, TextBox textBox, Action inputMethod)
    {
        var key = (char)e.KeyCode;
        var keyValue = e.KeyValue;
        var keyData = e.KeyData.ToString();
        if ((key >= 113 && key <= 123) || (key >= 33 && key <= 40) || key == 13 || key == 16 || key == 17 ||
            key == 18 || key == 20 || key == 27 || key == 45 || key == 144)
        {
        }
        else if (keyValue == 8 || key == 46)
        {
            textBox.Text = string.Empty;
        }
        else if (keyData is "Keys.O | Keys.Alt" or "Keys.S | Keys.Alt" or "Keys.C | Keys.Alt" or "O, Alt" or "Keys.Alt | Keys.Tab")
        {
        }
        else if (keyData == "A, Control")
        {
            textBox.SelectAll();
        }
        else if (keyData == "S, Control")
        {
        }
        else if (keyData == "S, Alt")
        {
        }
        else if (keyData == "C, Control")
        {
            textBox.Copy();
        }
        else if (keyData == "V, Control")
        {
            textBox.Paste();
        }
        else if (keyData == "X, Control")
        {
            textBox.Cut();
        }
        else if (keyData == "Z, Control")
        {
            textBox.Undo();
        }
        else if (keyData == "N, Control")
        {
        }
        else
        {
            if ((key == 32 || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) ||
                 key == 112) && actionTag is "EDIT" or "DELETE")
            {
                var searchKey = key.ToString();
                var val = -1;
                if (keyValue is >= (int)Keys.NumPad0 and <= (int)Keys.NumPad9)
                {
                    val = keyValue - (int)Keys.NumPad0;
                    searchKey = val.ToString();
                }
                else if (keyValue is >= (int)Keys.D0 and <= (int)Keys.D9)
                {
                    val = keyValue - (int)Keys.D0;
                    searchKey = val.ToString();
                }

                ObjGlobal.SearchText = searchKey;
                inputMethod();
                ObjGlobal.SearchText = string.Empty;
            }
        }
    }
    
    public static void KeyEvent(KeyEventArgs e, string actionTag, MaskedTextBox textBox, Button button)
    {
        var key = (char)e.KeyCode;
        var keyValue = e.KeyValue;
        var keyData = e.KeyData.ToString();
        if ((key >= 113 && key <= 123) || (key >= 33 && key <= 40) || key == 13 || key == 16 || key == 17 ||
            key == 18 || key == 20 || key == 27 || key == 45 || key == 144)
        {
        }
        else if (keyValue == 8 || key == 46)
        {
            textBox.Text = string.Empty;
        }
        else if (keyData is "Keys.O | Keys.Alt" or "Keys.S | Keys.Alt" or "Keys.C | Keys.Alt" or "O, Alt" or "Keys.Alt | Keys.Tab" or "S, Alt")
        {
        }
        else if (keyData == "A, Control")
        {
            textBox.SelectAll();
        }
        else if (keyData == "S, Control")
        {
        }
        else if (keyData == "C, Control")
        {
            textBox.Copy();
        }
        else if (keyData == "V, Control")
        {
            textBox.Paste();
        }
        else if (keyData == "X, Control")
        {
            textBox.Cut();
        }
        else if (keyData == "Z, Control")
        {
            textBox.Undo();
        }
        else if (keyData == "N, Control")
        {
        }
        else
        {
            if ((key == 32 || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 112) && actionTag is "EDIT" or "DELETE")
            {
                var searchKey = key.ToString();
                var val = -1;
                if (keyValue is >= (int)Keys.NumPad0 and <= (int)Keys.NumPad9)
                {
                    val = keyValue - (int)Keys.NumPad0;
                    searchKey = val.ToString();
                }
                else if (keyValue is >= (int)Keys.D0 and <= (int)Keys.D9)
                {
                    val = keyValue - (int)Keys.D0;
                    searchKey = val.ToString();
                }

                ObjGlobal.SearchText = searchKey;
                button.PerformClick();
                ObjGlobal.SearchText = string.Empty;
            }
        }
    }
    
    public static void KeyEvent(char key, int keyValue, string keyData, string searchText, TextBox textBox, Button button)
    {
        KeyEvent(key, keyValue, keyData, "DELETE", searchText, textBox, button);
    }
    
    public static void KeyEvent(char key, int keyValue, string keyData, string actionTag, string searchText, TextBox textBox, Button Btn)
    {
        if ((key >= 113 && key <= 123) || (key >= 33 && key <= 40) || key == 13 || key == 16 || key == 17 ||
            key == 18 || key == 20 || key == 27 || key == 45 || key == 144)
        {
        }
        else if (keyValue == 8 || key == 46)
        {
            textBox.Text = string.Empty;
        }
        else if (keyData is "Keys.O | Keys.Alt" or "Keys.S | Keys.Alt" or "Keys.C | Keys.Alt" or "O, Alt"
                 or "Keys.Alt | Keys.Tab")
        {
        }
        else if (keyData == "A, Control")
        {
            textBox.SelectAll();
        }
        else if (keyData == "S, Control")
        {
        }
        else if (keyData == "S, Alt")
        {
        }
        else if (keyData == "C, Control")
        {
            textBox.Copy();
        }
        else if (keyData == "V, Control")
        {
            textBox.Paste();
        }
        else if (keyData == "X, Control")
        {
            textBox.Cut();
        }
        else if (keyData == "Z, Control")
        {
            textBox.Undo();
        }
        else if (keyData == "N, Control")
        {
        }
        else
        {
            if ((key == 32 || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) ||
                 key == 112) && actionTag is "EDIT" or "DELETE")
            {
                searchText = key.ToString();
                var val = -1;
                if (keyValue is >= (int)Keys.NumPad0 and <= (int)Keys.NumPad9)
                {
                    val = keyValue - (int)Keys.NumPad0;
                    searchText = val.ToString();
                }
                else if (keyValue is >= (int)Keys.D0 and <= (int)Keys.D9)
                {
                    val = keyValue - (int)Keys.D0;
                    searchText = val.ToString();
                }

                ObjGlobal.SearchText = searchText;
                Btn.PerformClick();
                ObjGlobal.SearchText = string.Empty;
            }
        }
    }
    
    public static void KeyEvent(char key, int keyValue, string keyData, string actionTag, TextBox textBox, Action inputMethod)
    {
        KeyEvent(key, keyValue, keyData, actionTag, string.Empty, textBox, inputMethod);
    }
    
    public static void KeyEvent(char key, int keyValue, string keyData, string actionTag, string searchKey, TextBox textBox, Action inputMethod)
    {
        if ((key >= 113 && key <= 123) || (key >= 33 && key <= 40) || key == 13 || key == 16 || key == 17 ||
            key == 18 || key == 20 || key == 27 || key == 45 || key == 144)
        {
        }
        else if (keyValue == 8 || key == 46)
        {
            textBox.Text = string.Empty;
        }
        else if (keyData is "Keys.O | Keys.Alt" or "Keys.S | Keys.Alt" or "Keys.C | Keys.Alt" or "O, Alt"
                 or "Keys.Alt | Keys.Tab")
        {
        }
        else if (keyData == "A, Control")
        {
            textBox.SelectAll();
        }
        else if (keyData == "S, Control")
        {
        }
        else if (keyData == "S, Alt")
        {
        }
        else if (keyData == "C, Control")
        {
            textBox.Copy();
        }
        else if (keyData == "V, Control")
        {
            textBox.Paste();
        }
        else if (keyData == "X, Control")
        {
            textBox.Cut();
        }
        else if (keyData == "Z, Control")
        {
            textBox.Undo();
        }
        else if (keyData == "N, Control")
        {
        }
        else
        {
            if ((key == 32 || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) ||
                 key == 112) && actionTag is "EDIT" or "DELETE")
            {
                searchKey = key.ToString();
                var val = -1;
                if (keyValue is >= (int)Keys.NumPad0 and <= (int)Keys.NumPad9)
                {
                    val = keyValue - (int)Keys.NumPad0;
                    searchKey = val.ToString();
                }
                else if (keyValue is >= (int)Keys.D0 and <= (int)Keys.D9)
                {
                    val = keyValue - (int)Keys.D0;
                    searchKey = val.ToString();
                }

                ObjGlobal.SearchText = searchKey;
                inputMethod();
                ObjGlobal.SearchText = string.Empty;
            }
        }
    }
}