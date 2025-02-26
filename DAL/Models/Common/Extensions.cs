using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace MrDAL.Models.Common;

/// <summary>
///     Hold Extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    ///     Extension method to print all the "ImbeddedImages" in a provided list
    /// </summary>
    /// <typeparam name="?"></typeparam>
    /// <param name="list"></param>
    /// <param name="g"></param>
    /// <param name="pagewidth"></param>
    /// <param name="pageheight"></param>
    /// <param name="margins"></param>
    public static void DrawImbeddedImage<T>(this IEnumerable<T> list,
        Graphics g, int pagewidth, int pageheight, Margins margins)
    {
        foreach (var t in list)
            if (t is PrinterFunction.ImbeddedImage)
            {
                var ii = (PrinterFunction.ImbeddedImage)Convert.ChangeType(t, typeof(PrinterFunction.ImbeddedImage));
                // Fix - DrawImageUnscaled was actually scaling the images!!?! Oh well...
                //g.DrawImageUnscaled(ii.theImage, ii.upperleft(pagewidth, pageheight, margins));
                g.DrawImage(ii.theImage,
                    new Rectangle(ii.upperleft(pagewidth, pageheight, margins),
                        new Size(ii.theImage.Width, ii.theImage.Height)));
            }
    }
}