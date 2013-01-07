using System;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace SharpPdf.Controls
{
    public class InputBox : IVisibleControl, ITextControl
    {
        public InputBox(string title, string val = "")
            : this(
                new XRect(0, 0, DefaultValues.Inputbox.DefaultWidth, DefaultValues.Inputbox.DefaultHeight), title, val)
        {
        }

        public InputBox(XRect rect, string title, string val = "")
            : this(rect)
        {
            Title = title;
            Value = val;
        }

        public InputBox(XRect rect)
        {
            Rect = rect;

            Brush = DefaultValues.Inputbox.Brush;
            Pen = DefaultValues.Inputbox.Pen;
            Font = DefaultValues.Inputbox.Font;
        }

        public String Title { get; set; }

        public String Value { get; set; }

        #region ITextControl Members

        public XFont Font { get; set; }

        #endregion

        #region IVisibleControl Members

        public XRect Rect { get; set; }

        public void Draw(XGraphics gfx)
        {
            gfx.DrawRectangle(Pen, Brush, Rect);

            double yOffset = 5;

            //Print InputBox Title, Left Top corner
            if (!string.IsNullOrEmpty(Title))
            {
                var titleLabel = new Label(Rect.X + 5, Rect.Y, Title)
                    {
                        Brush = XBrushes.Black,
                        Font = new XFont(Font.FontFamily.Name, 8, XFontStyle.Bold, Font.PdfOptions),
                        Pen = XPens.Black
                    };
                titleLabel.Draw(gfx);
                yOffset = 15;
            }

            //Print InputBox Value
            if (!string.IsNullOrEmpty(Value))
            {
                var valLabel = new Label(new XRect(Rect.X + 10, Rect.Y + yOffset, Rect.Width, Rect.Height), Value)
                    {
                        Alignment = XParagraphAlignment.Left,
                        Brush = XBrushes.Black,
                        Font = new XFont(Font.FontFamily.Name, 12, XFontStyle.Regular, Font.PdfOptions),
                        Pen = XPens.Black,
                    };
                valLabel.Draw(gfx);
                yOffset += valLabel.Rect.Height;
            }
        }

        public XPen Pen { get; set; }
        public XBrush Brush { get; set; }

        public object Clone()
        {
            return new InputBox(Title, Value)
                {Value = Value, Rect = Rect, Font = Font, Brush = Brush, Title = Title, Pen = Pen};
        }

        #endregion
    }
}