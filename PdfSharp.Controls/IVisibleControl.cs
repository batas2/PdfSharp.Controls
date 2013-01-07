using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    public interface IVisibleControl : IControl
    {
        XPen Pen { get; set; }
        XBrush Brush { get; set; }
    }
}