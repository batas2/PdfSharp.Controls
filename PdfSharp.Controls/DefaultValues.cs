using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace SharpPdf.Controls
{
    public class DocumentDefultValues
    {
        public const string FontFamily = "Arial";
        public static XPdfFontOptions FontOptions = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
    }

    public sealed class LabelDefaultValues : DocumentDefultValues
    {
        public XParagraphAlignment Alignment = XParagraphAlignment.Left;
        public XBrush Brush = XBrushes.Black;

        public int DefaultHeight = 10;
        public int DefaultWidth = 500;
        public XFont Font = new XFont(FontFamily, 12, XFontStyle.Bold, FontOptions);
        public XPen Pen = XPens.Black;
    }

    public sealed class CheckBoxDefaultValues : DocumentDefultValues
    {
        public XBrush Brush = XBrushes.White;
        public int DefaultHeight = 10;
        public int DefaultWidth = 10;
        public XFont Font = new XFont(FontFamily, 9, XFontStyle.Regular, FontOptions);
        public XPen Pen = XPens.Black;
    }

    public sealed class GroupboxDefaultValues : DocumentDefultValues
    {
        public XBrush Brush = XBrushes.LightGray;
        public XFont Font = new XFont(FontFamily, 12, XFontStyle.Bold, FontOptions);
        public int MarginBottom = 1;

        public int MarginLeft = 20;
        public int MarginRight = 1;
        public int MarginTop = 20;
        public XPen Pen = XPens.Black;
    }

    public sealed class InputboxDefaultValues : DocumentDefultValues
    {
        public XBrush Brush = XBrushes.White;
        public int DefaultHeight = 30;
        public int DefaultWidth = 100;
        public XFont Font = new XFont(FontFamily, 9, XFontStyle.Regular, FontOptions);
        public XPen Pen = XPens.Black;
    }

    public sealed class TableCellDefaultValues : DocumentDefultValues
    {
        public XBrush Brush = XBrushes.White;
        public int DefaultHeight = 20;
        public int DefaultWidth = 100;
        public XFont Font = new XFont(FontFamily, 9, XFontStyle.Regular, FontOptions);
        public XPen Pen = XPens.Black;
    }

    public sealed class TableControlDefaultValues : DocumentDefultValues
    {
        public int MarginBottom = 1;
        public int MarginLeft = 20;
        public int MarginRight = 1;
        public int MarginTop = 20;
    }

    public static class DefaultValues
    {
        public static DocumentDefultValues Document = new DocumentDefultValues();
        public static LabelDefaultValues Label = new LabelDefaultValues();
        public static CheckBoxDefaultValues CheckBox = new CheckBoxDefaultValues();
        public static GroupboxDefaultValues Groupbox = new GroupboxDefaultValues();
        public static InputboxDefaultValues Inputbox = new InputboxDefaultValues();
        public static TableCellDefaultValues TableCell = new TableCellDefaultValues();
        public static TableControlDefaultValues TableControl = new TableControlDefaultValues();
    }
}