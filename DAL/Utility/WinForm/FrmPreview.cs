using MrDAL.Global.Common;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MrDAL.Utility.WinForm;

public partial class FrmPreview : Form
{
    public FrmPreview(PictureBox box)
    {
        InitializeComponent();
        RdoStretchImage.Checked = true;
        picDocumentPreview.Image = box.Image;
    }

    private void FrmPreview_Load(object sender, EventArgs e)
    {
        ObjGlobal.FetchPic(picDocumentPreview, string.Empty);
    }

    private void FrmPreview_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) Close();
    }

    private void rdNormal_CheckedChanged(object sender, EventArgs e)
    {
        PicSizeChange();
    }

    private void rdStretchImage_CheckedChanged(object sender, EventArgs e)
    {
        PicSizeChange();
    }

    private void rdAutoSize_CheckedChanged(object sender, EventArgs e)
    {
        PicSizeChange();
    }

    private void rdCenterImage_CheckedChanged(object sender, EventArgs e)
    {
        PicSizeChange();
    }

    private void rdZoom_CheckedChanged(object sender, EventArgs e)
    {
        PicSizeChange();
    }

    private void PicSizeChange()
    {
        picDocumentPreview.Size = new Size(901, 448);
        picDocumentPreview.SizeMode = RdoZoom.Checked ? PictureBoxSizeMode.Zoom
            : RdoCenterImage.Checked ? PictureBoxSizeMode.CenterImage
            : RdoAutoSize.Checked ? PictureBoxSizeMode.AutoSize
            : RdoStretchImage.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Normal;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        var myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
        var myPrinDialog1 = new PrintDialog();
        myPrintDocument1.PrintPage += myPrintDocument2_PrintPage;
        myPrinDialog1.Document = myPrintDocument1;

        if (myPrinDialog1.ShowDialog() == DialogResult.OK) myPrintDocument1.Print();
    }

    private void myPrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        var myBitmap2 = new Bitmap(Width, Height);
        DrawToBitmap(myBitmap2, new Rectangle(0, 0, Width, Height));
        e.Graphics.DrawImage(myBitmap2, 0, 0);
        myBitmap2.Dispose();
    }

    private void myPrintDocument2_PrintPage(object sender, PrintPageEventArgs e)
    {
        var myBitmap1 = new Bitmap(picDocumentPreview.Width, picDocumentPreview.Height);
        picDocumentPreview.DrawToBitmap(myBitmap1,
            new Rectangle(0, 0, picDocumentPreview.Width, picDocumentPreview.Height));
        e.Graphics.DrawImage(myBitmap1, 0, 0);
        myBitmap1.Dispose();
    }

    private void PrintPage(object o, PrintPageEventArgs e)
    {
        var img = picDocumentPreview.Image;
        var loc = new Point(100, 100);
        e.Graphics.DrawImage(img, loc);
    }
}