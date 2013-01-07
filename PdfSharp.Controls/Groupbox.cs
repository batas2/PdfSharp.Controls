using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    public class GroupBox : IContainerControl, IAddChildControl, IVisibleControl, ITextControl
    {
        private ICollection<IControl> _controls;
        private double _currentY;

        public GroupBox(string title = "")
        {
            Title = title;
            Controls = new List<IControl>();
            Rect = new XRect(0, 0, 500, MarginTop + MarginBottom);

            Font = DefaultValues.Groupbox.Font;
            Pen = DefaultValues.Groupbox.Pen;
            Brush = DefaultValues.Groupbox.Brush;

            MarginLeft = DefaultValues.Groupbox.MarginLeft;
            MarginRight = DefaultValues.Groupbox.MarginRight;
            MarginTop = DefaultValues.Groupbox.MarginTop;
            MarginBottom = DefaultValues.Groupbox.MarginBottom;
        }

        public GroupBox(double left, double width, string title)
            : this(title)
        {
            Rect = new XRect(left, 2, width, MarginTop + MarginBottom);
        }

        public GroupBox(XRect rect)
            : this("")
        {
            Rect = rect;
        }

        public String Title { get; set; }

        #region IAddChildControl Members

        public void AddChild(IControl control)
        {
            double controlHeight = control.Rect.Height + control.Rect.Y;
            if (controlHeight > _currentY)
            {
                _currentY = controlHeight;
                Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
            }
            Controls.Add(control);
        }

        public ICollection<IControl> Controls
        {
            get { return _controls; }
            set
            {
                if (value != null && value.Any())
                {
                    double maxControlsHeight = value.Select(x => (x.Rect.Height + x.Rect.Y)).Max();
                    if (maxControlsHeight > _currentY)
                    {
                        _currentY = maxControlsHeight;
                        Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
                    }
                }
                _controls = value;
            }
        }

        #endregion

        #region IContainerControl Members

        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public XRect Rect { get; set; }

        public void Draw(XGraphics gfx)
        {
            Draw(gfx, Controls);
        }

        public void Draw(XGraphics gfx, double startY, double height)
        {
            if (startY > _currentY || startY < 0 || height < 0)
            {
                throw new ArgumentException("Wrong Y range spefified");
            }

            //Draw only complete Groupbox
            if (startY != 0)
            {
                Draw(gfx);
            }
        }

        public object Clone()
        {
            return new GroupBox
                {
                    _controls = _controls.Clone(),
                    _currentY = _currentY,
                    Brush = Brush,
                    Font = Font,
                    MarginTop = MarginTop,
                    MarginBottom = MarginBottom,
                    MarginLeft = MarginLeft,
                    MarginRight = MarginRight,
                    Rect = Rect,
                    Title = Title,
                    Pen = Pen
                };
        }

        #endregion

        #region ITextControl Members

        public XFont Font { get; set; }

        #endregion

        #region IVisibleControl Members

        public XPen Pen { get; set; }
        public XBrush Brush { get; set; }

        #endregion

        private void Draw(XGraphics gfx, IEnumerable<IControl> controls)
        {
            gfx.DrawRectangle(Pen, Brush, Rect);

            if (!String.IsNullOrEmpty(Title))
            {
                var titleLabel = new Label(Rect.X + 5, Rect.Y, Title)
                    {Brush = XBrushes.Black, Font = Font, Pen = XPens.Black};
                titleLabel.Draw(gfx);
            }

            if (controls.Any())
            {
                Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
                double maxWidth = Rect.X + Rect.Width;

                //Adding margin for every control in Controls List
                foreach (IControl control in Controls)
                {
                    double x = Rect.X + control.Rect.X + MarginLeft;
                    double y = Rect.Y + control.Rect.Y + MarginTop;
                    double width = x + control.Rect.Width > maxWidth ? maxWidth - x : control.Rect.Width;
                    double height = control.Rect.Height;

                    control.Rect = new XRect(x, y, width, height);
                    control.Draw(gfx);
                }
            }
        }

        public void AddRow(IControl control)
        {
            AddRow(new[] {control}, new[] {100});
        }

        public void AddRow(ICollection<IControl> controls, ICollection<int> percentWidths)
        {
            AddRow(controls, percentWidths, _currentY);
        }

        private void AddRow(ICollection<IControl> controls, ICollection<int> percentWidths, double y)
        {
            if (controls.Count != percentWidths.Count)
            {
                throw new ArgumentException("Control count have to be equal to percentWidths count");
            }

            if (percentWidths.Sum() != 100)
            {
                throw new ArgumentException("Percents sum have to be 100");
            }

            double left = 0;
            double maxWidth = Rect.Width - MarginLeft - MarginRight;

            var controlWidthPairs = controls.Zip(percentWidths, (c, p) => new {Control = c, PercentWidth = p});
            foreach (var pair in controlWidthPairs)
            {
                double x = left;
                double width = maxWidth*pair.PercentWidth/100;
                double height = pair.Control.Rect.Height;

                pair.Control.Rect = new XRect(x, y, width, height);
                left += width;
                Controls.Add(pair.Control);
            }

            _currentY = controls.Select(x => (x.Rect.Height + x.Rect.Y)).Max();
            Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
        }
    }
}