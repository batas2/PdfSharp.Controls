using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using VATUE2Lib;

namespace PdfSharpControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _xmlFilesPath = "vat_files.xml";
        private const string _tempPdfFilePath = "tmp.pdf";
        private readonly ObservableCollection<string> _pdfFiles;

        public MainWindow()
        {
            InitializeComponent();

            _pdfFiles = new ObservableCollection<string>();
            lstViewPdfFile.ItemsSource = _pdfFiles;

            LoadRecentFiles();
        }

        private void LoadRecentFiles()
        {
            try
            {
                _pdfFiles.Deserialize(_xmlFilesPath);

                if (_pdfFiles.Count > 0)
                {
                    lstViewPdfFile.SelectedIndex = 0;
                    PdfViewer.PdfPath = GeneratePdfFile(_pdfFiles[0]);
                }
            }
            catch (Exception)
            {
                File.Delete(_xmlFilesPath);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                    {
                        CheckFileExists = true,
                        DefaultExt = ".xml",
                        Filter = "xml documents (.xml)|*.xml"
                    };

                bool? result = openFileDialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    PdfViewer.PdfPath = GeneratePdfFile(openFileDialog.FileName);
                    if (!_pdfFiles.Contains(openFileDialog.FileName))
                        _pdfFiles.Add(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string GeneratePdfFile(string filename)
        {
            var c = new VatUE2();
            var output = new byte[1];
            var ex = new Exception();
            var result = c.xml_convert(File.ReadAllText(filename), 1, ref output, ref ex);

            if (result == 0)
            {
                PdfDocument document = PdfReader.Open(new MemoryStream(output, 0, output.Length));

                document.Save(_tempPdfFilePath);

                return _tempPdfFilePath;
            }
            throw ex;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PdfViewer.Print();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstViewPdfFile.SelectedIndex < 0) return;
                string filePath = lstViewPdfFile.SelectedValue.ToString();
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
                else
                {
                    _pdfFiles.Remove(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                    {
                        DefaultExt = ".pdf",
                        Filter = "pdf documents (.pdf)|*.pdf"
                    };

                bool? result = saveFileDialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string destFilePath = saveFileDialog.FileName;
                    File.Copy(_tempPdfFilePath, destFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _pdfFiles.Serialize(_xmlFilesPath);
        }

        private void lstViewPdfFile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (lstViewPdfFile.SelectedIndex < 0) return;
                string filePath = lstViewPdfFile.SelectedValue.ToString();
                if (File.Exists(filePath))
                {
                    PdfViewer.PdfPath = GeneratePdfFile(filePath);
                }
                else
                {
                    _pdfFiles.Remove(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpenPdf_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_tempPdfFilePath);
        }
    }
}