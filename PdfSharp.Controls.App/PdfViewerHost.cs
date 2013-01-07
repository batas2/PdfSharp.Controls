using System.Windows;
using System.Windows.Forms.Integration;

namespace PdfSharpControls
{
    public class PdfViewerHost : WindowsFormsHost
    {
        public static readonly DependencyProperty PdfPathProperty = DependencyProperty.Register(
            "PdfFilePath", typeof (string), typeof (PdfViewerHost), new PropertyMetadata(PdfPathPropertyChanged));

        private readonly PdfPreviewControl _wrappedControl;

        public PdfViewerHost()
        {
            _wrappedControl = new PdfPreviewControl();
            Child = _wrappedControl;
        }

        public string PdfPath
        {
            get { return (string) GetValue(PdfPathProperty); }

            set { SetValue(PdfPathProperty, value); }
        }

        public void Print()
        {
            _wrappedControl.Print();
        }

        private static void PdfPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var host = (PdfViewerHost) d;
            host._wrappedControl.PdfFilePath = (string) e.NewValue;
        }
    }
}