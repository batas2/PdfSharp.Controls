using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    public interface ITextControl : IControl
    {
        XFont Font { get; set; }
    }
}