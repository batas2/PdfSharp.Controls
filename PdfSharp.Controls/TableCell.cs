using System;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace SharpPdf.Controls
{
    public class TableCell : IVisibleControl, ITextControl
    {
        public TableCell(string val = "")
        {
            Value = val;

            Rect = new XRect(0, 0, DefaultValues.TableCell.DefaultWidth, DefaultValues.TableCell.DefaultHeight);
            Brush = DefaultValues.TableCell.Brush;
            Pen = DefaultValues.TableCell.Pen;
            Font = DefaultValues.TableCell.Font;
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

            const double yOffset = 5;

            if (!string.IsNullOrEmpty(Value))
            {
                var valLabel = new Label(new XRect(Rect.X, Rect.Y + yOffset, Rect.Width, Rect.Height), Value)
                    {
                        Alignment = XParagraphAlignment.Center,
                        Brush = XBrushes.Black,
                        Font = new XFont(Font.FontFamily.Name, 12, XFontStyle.Regular, Font.PdfOptions),
                        Pen = XPens.Black,
                    };
                valLabel.Draw(gfx);
            }
        }

        public XPen Pen { get; set; }
        public XBrush Brush { get; set; }

        public object Clone()
        {
            return new TableCell {Rect = Rect, Font = Font, Brush = Brush, Title = Title, Pen = Pen, Value = Value};
        }

        #endregion
    }
}