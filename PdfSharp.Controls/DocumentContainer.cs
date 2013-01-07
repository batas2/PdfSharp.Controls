using System;
using System.Collections.Generic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SharpPdf.Controls
{
    public class DocumentContainer
    {
        public const string FontFamily = "Arial";
        private readonly ICollection<IContainerControl> _containers;
        private readonly PdfDocument _document;
        private XGraphics _gfx;
        private PdfPage _page;

        public DocumentContainer()
        {
            _containers = new List<IContainerControl>();
            _document = new PdfDocument();

            _document.Info.Title = "Created with PDFsharp";

            AddNewPage();
        }

        public TrimMargins Margins
        {
            get { return _page.TrimMargins; }
            set { _page.TrimMargins = value; }
        }

        public double Width
        {
            get { return _gfx.PageSize.Width; }
        }

        private void AddNewPage()
        {
            _page = _document.AddPage();
            _page.TrimMargins = new TrimMargins {Left = 25, Right = 25, Bottom = 45, Top = 25};
            _gfx = XGraphics.FromPdfPage(_page);
        }

        public void AddContainer(IContainerControl container)
        {
            _containers.Add(container);
        }

        public PdfDocument Render()
        {
            double y = 0;
            double pageHeight = _gfx.PageSize.Height;

            foreach (IContainerControl container in _containers)
            {
                
                var rect = new XRect(container.Rect.X, y, container.Rect.Width, container.Rect.Height);
                container.Rect = rect;

                if (y + container.Rect.Height < pageHeight)
                {
                    try{
                    container.Draw(_gfx);
                        }catch(Exception )
                        {
                            
                        }
                }
                else
                {
                    try
                    {
                        double height = pageHeight - y;
                        double containerHeight = container.Rect.Height;

                        container.Draw(_gfx, 0, height);

                        AddNewPage();

                        container.Rect = new XRect(container.Rect.X, 0, container.Rect.Width, containerHeight);
                        container.Draw(_gfx, height, container.Rect.Height);
                    }
                    catch (Exception ex) { }
                }

                y = container.Rect.Y + container.Rect.Height;
            }
            return _document;
        }
    }
}