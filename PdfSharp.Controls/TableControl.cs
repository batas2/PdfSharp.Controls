using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    public class TableControl : IContainerControl
    {
        private readonly GroupBox _backgroundControl;

        private readonly double _left;
        private readonly double _width;
        private ICollection<int> _columnWidths;

        private double _currentY;
        private double _headHight;
        private XRect _rect;

        public ICollection<IContainerControl> HeadRows { get; private set; }
        public ICollection<IContainerControl> Rows { get; private set; }

        public TableControl(int columnCount, ICollection<int> columnPercentWidths, double left, double width,
                            string title)
        {
            ColumnCount = columnCount;

            _columnWidths = columnPercentWidths;
            Rows = new Collection<IContainerControl>();
            HeadRows = new Collection<IContainerControl>();

            _left = left;
            _width = width;

            MarginLeft = DefaultValues.TableControl.MarginLeft;
            MarginRight = DefaultValues.TableControl.MarginRight;
            MarginTop = DefaultValues.TableControl.MarginTop;
            MarginBottom = DefaultValues.TableControl.MarginBottom;

            _backgroundControl = new GroupBox(title);

            Rect = new XRect(left, 0, width, MarginTop + MarginBottom);
        }

        public int ColumnCount { get; private set; }

        #region IContainerControl Members

        public XRect Rect
        {
            get { return _rect; }
            set
            {
                _rect = value;
                _backgroundControl.Rect = value;
            }
        }

        public void Draw(XGraphics gfx)
        {
            Draw(gfx, 0, int.MaxValue);
        }

        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }

        /// <summary>
        /// Draws the Table Control. Will not draw, if height lower than table head height
        /// </summary>
        /// <param name="gfx">The GFX.</param>
        /// <param name="startY">The start Y. Relative to table</param>
        /// <param name="height">The height. Relative to table</param>
        public void Draw(XGraphics gfx, double startY, double height)
        {
            if (startY < 0 || height < 0)
            {
                throw new ArgumentException("Wrong Y range spefified");
            }

            List<IContainerControl> rows = Rows.Where(r => (r.Rect.Y + r.Rect.Height) <= startY + height).ToList();

            //Cannot draw lower table than header or Nothing to draw
            if (height < _headHight || !rows.Any())
            {
                Rect = new XRect(Rect.X, Rect.Y, Rect.Width, 0);
                return;
            }

            rows.InsertRange(0, HeadRows.Clone());
            double curentHeight = rows.Select(x => x.Rect.Height).Sum();

            Rect = new XRect(Rect.X, Rect.Y, Rect.Width, curentHeight + MarginTop + 1);
            _backgroundControl.Brush = XBrushes.LightGray;
            _backgroundControl.Pen = XPens.Black;
            _backgroundControl.Draw(gfx);

            double y = 0;
            foreach (IContainerControl row in rows)
            {
                row.Rect = new XRect(Rect.X + 1, Rect.Y + MarginTop + y, row.Rect.Width - 2,
                                     row.Rect.Height);
                y += row.Rect.Height;
                row.Draw(gfx);
            }
        }

        public object Clone()
        {
            return new TableControl(ColumnCount, _columnWidths, _left, _width, _backgroundControl.Title)
                {
                    Rect = Rect,
                    MarginTop = MarginTop,
                    MarginBottom = MarginBottom,
                    MarginLeft = MarginLeft,
                    MarginRight = MarginRight,
                    _currentY = _currentY,
                    _headHight = _headHight,
                    _rect = _rect,
                    HeadRows = HeadRows.Clone(),
                    Rows = Rows.Clone(),
                    _columnWidths = _columnWidths,
                    ColumnCount = ColumnCount
                };
        }

        #endregion

        public void AddHeadRow(ICollection<IControl> controls, int marginTop = 5, int marginBottom = 5)
        {
            if (controls.Count != ColumnCount)
            {
                throw new ArgumentException("Column count and controls count have to be equal");
            }

            if (Rows.Any())
            {
                throw new Exception("Can not add tableHeader if the table has rows");
            }

            GroupBox headRow = GetNewRow();
            //Each Control box into groupbox with no margins, draw cell border
            GroupBox[] headCells = controls.Select(control => new GroupBox
                {
                    MarginLeft = 0,
                    MarginRight = 0,
                    MarginTop = marginTop,
                    MarginBottom = marginBottom,
                    Controls = new[] { control }
                }).ToArray();
            headRow.AddRow(headCells, _columnWidths);

            HeadRows.Add(headRow);

            _currentY += headRow.Rect.Height;
            _headHight += headRow.Rect.Height;

            Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
        }

        private GroupBox GetNewRow(double left = 0)
        {
            //Headrow container at end of table; _currentY - end of table
            var headRow = new GroupBox(new XRect(_left, _currentY, _width, 20))
                {
                    MarginBottom = 0,
                    MarginTop = 0,
                    Brush = XBrushes.LightGray,
                    Pen = XPens.LightGray
                };
            return headRow;
        }

        public void AddRow(ICollection<IControl> controls, int marginTop = 0, int marginBottom = 0)
        {
            if (controls.Count != ColumnCount)
            {
                throw new ArgumentException("Column count and controls count have to be equal");
            }

            GroupBox row = GetNewRow(_left + MarginLeft);
            ////Each Control box into groupbox with no margins, draw cell border
            var cells = new List<IControl>();
            foreach (IControl control in controls)
            {
                var groupBox = new GroupBox
                    {
                        MarginLeft = 0,
                        MarginRight = 0,
                        MarginTop = marginTop,
                        MarginBottom = marginBottom,
                    };
                groupBox.AddRow(control);
                cells.Add(groupBox);
            }

            row.AddRow(cells, _columnWidths);

            Rows.Add(row);
            _currentY += row.Rect.Height;

            Rect = new XRect(Rect.X, Rect.Y, Rect.Width, _currentY + MarginTop + MarginBottom);
        }

    }
}