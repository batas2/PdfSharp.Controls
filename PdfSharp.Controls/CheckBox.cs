using System;
using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    public class CheckBox : IVisibleControl, ITextControl
    {
        public CheckBox(double x, double y)
            : this(new XRect(x, y, DefaultValues.CheckBox.DefaultWidth, DefaultValues.CheckBox.DefaultHeight))
        {
        }

        public CheckBox(XRect rect)
        {
            Rect = rect;
            Brush = DefaultValues.CheckBox.Brush;
            Pen = DefaultValues.CheckBox.Pen;
            Font = DefaultValues.CheckBox.Font;
        }

        public String Title { get; set; }
        public bool IsChecked { get; set; }

        #region ITextControl Members

        public XFont Font { get; set; }

        #endregion

        #region IVisibleControl Members

        public XRect Rect { get; set; }

        public void Draw(XGraphics gfx)
        {
            var inputBox = new InputBox(Rect);
            inputBox.Draw(gfx);

            if (!string.IsNullOrEmpty(Title))
            {
                var label = new Label(Rect.X + Rect.Width + 5, Rect.Y, Title)
                    {
                        Brush = XBrushes.Black,
                        Font = new XFont(Font.FontFamily.Name, 9, XFontStyle.Regular, Font.PdfOptions),
                        Pen = XPens.Black
                    };

                label.Draw(gfx);
            }

            if (IsChecked)
            {
                gfx.DrawLine(Pen, Rect.X, Rect.Y, Rect.X + Rect.Width, Rect.Y + Rect.Height);
                gfx.DrawLine(Pen, Rect.X + Rect.Width, Rect.Y, Rect.X, Rect.Y + Rect.Height);
            }
        }

        public XPen Pen { get; set; }
        public XBrush Brush { get; set; }

        public object Clone()
        {
            return new CheckBox(Rect)
                {
                    Brush = Brush,
                    Pen = Pen,
                    Title = Title,
                    Rect = Rect,
                    Font = Font,
                    IsChecked = IsChecked
                };
        }

        #endregion
    }
}