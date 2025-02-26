using System.Runtime.InteropServices;

namespace PrintControl.PrintClass.DirectPrint;

public class TscPrintFunction
{
    [DllImport("TSCLIB.dll", EntryPoint = "about")]
    public static extern int about();

    [DllImport("TSCLIB.dll", EntryPoint = "openport")]
    public static extern int openport(string printername);

    [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
    public static extern int barcode(string x, string y, string type, string height, string readable, string rotation,
        string narrow, string wide, string code);

    [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
    public static extern int clearbuffer();

    [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
    public static extern int closeport();

    [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
    public static extern int downloadpcx(string filename, string image_name);

    [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
    public static extern int formfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
    public static extern int nobackfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
    public static extern int printerfont(string x, string y, string fonttype, string rotation, string xmul, string ymul,
        string text);

    [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
    public static extern int printlabel(string set, string copy);

    [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
    public static extern int sendcommand(string printercommand);

    [DllImport("TSCLIB.dll", EntryPoint = "setup")]
    public static extern int setup(string width, string height, string speed, string density, string sensor,
        string vertical, string offset);

    [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
    public static extern int windowsfont(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline,
        string szFaceName, string content);

    // Open the printer port , And relevant Settings

    public static void OpenPortExt()
    {
        openport("TSC TTP-244 Pro"); // Find the printer port
        sendcommand("SIZE 60 mm,30 mm"); // Set bar code size
        sendcommand("GAP 2 mm,0"); // Set bar code gap
        sendcommand("SPEED 4"); // Set the printing speed
        sendcommand("DENSITY 7"); // Set the ink concentration
        sendcommand("DERECTION 1"); // Set the relative starting point
        sendcommand("REFERENCE 3 mm,3 mm"); // Set the offset border
        sendcommand("CLS"); // Erase memory （ Each time a new barcode is printed, the last printing memory will be cleared ）
    }

    // Turn off the printer port

    public static void ClosePortExt()
    {
        closeport();
    }
}