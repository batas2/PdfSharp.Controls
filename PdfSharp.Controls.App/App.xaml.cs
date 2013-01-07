using System;
using System.IO;
using System.Windows;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using VATUE2Lib;

namespace PdfSharpControls
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += Application_Startup;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 2)
            {
                var vatUe2 = new VatUE2();
                var output = new byte[1];
                var ex = new Exception();
                var result = vatUe2.xml_convert(File.ReadAllText(e.Args[0]), 1, ref output, ref ex);

                if (result == 0)
                {
                    PdfDocument document = PdfReader.Open(new MemoryStream(output, 0, output.Length));

                    document.Save(e.Args[1]);
                }
                else
                {
                    Console.Error.WriteLine(PdfSharpControls.Properties.Resources.CommandLineException, ex.Message);
                }
                Current.Shutdown();
            }
            else if (e.Args.Length > 0)
            {
                Console.Error.WriteLine(PdfSharpControls.Properties.Resources.CommandLineUsage);
            }
        }
    }
}