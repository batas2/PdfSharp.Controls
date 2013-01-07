using System;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace SharpPdf.Controls
{
    public class Label : IVisibleControl, ITextControl
    {
        public Label(XParagraphAlignment alignment, string content)
            : this(new XRect(0, 0, DefaultValues.Label.DefaultWidth, DefaultValues.Label.DefaultHeight), content)
        {
            Alignment = alignment;
        }

        public Label(double x, double y, string content)
            : this(new XRect(x, y, DefaultValues.Label.DefaultWidth, DefaultValues.Label.DefaultHeight), content)
        {
        }

        public Label(XRect rect, string content)
        {
            Content = content;
            Rect = rect;

            Font = DefaultValues.Label.Font;
            Pen = DefaultValues.Label.Pen;
            Brush = DefaultValues.Label.Brush;
            Alignment = DefaultValues.Label.Alignment;
        }

        public String Content { get; set; }

        public XParagraphAlignment Alignment { get; set; }

        #region ITextControl Members

        public XFont Font { get; set; }

        #endregion

        #region IVisibleControl Members

        public XRect Rect { get; set; }

        public void Draw(XGraphics gfx)
        {
            var tf = new XTextFormatter(gfx) {Alignment = Alignment};

            XSize size = gfx.MeasureString(Content, Font);
            Rect = new XRect(Rect.X, Rect.Y, Rect.Width, size.Height*(Math.Ceiling(size.Width/Rect.Width) + 1));
            tf.DrawString(Content, Font, Brush, Rect, XStringFormats.TopLeft);
        }

        public XPen Pen { get; set; }
        public XBrush Brush { get; set; }

        public object Clone()
        {
            return new Label(Rect, Content)
                {Alignment = Alignment, Rect = Rect, Font = Font, Brush = Brush, Pen = Pen, Content = Content};
        }

        #endregion
    }
}