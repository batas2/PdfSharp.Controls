using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace PdfSharpControls
{
    public partial class PdfPreviewControl : UserControl
    {
        private string _pdfFilePath;

        public PdfPreviewControl()
        {
            InitializeComponent();
        }

        public string PdfFilePath
        {
            get { return _pdfFilePath; }
            set
            {
                if (!File.Exists(value))
                {
                    throw new ArgumentException("File not Exits");
                }

                _pdfFilePath = value;
                acroPdfPreview.LoadFile(_pdfFilePath);
                acroPdfPreview.setView("FitH");
            }
        }

        public void Print()
        {
            acroPdfPreview.printWithDialog();
        }

        private void axAcroPDF1_OnError(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString(), "Błąd.", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}