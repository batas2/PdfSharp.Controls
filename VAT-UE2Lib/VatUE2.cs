using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using PdfSharp.Pdf;
using VATUE2Lib.Models;

namespace VATUE2Lib
{
    public class VatUE2
    {

        private const string SchemaResourceName = "VATUE2Lib.Models.VAT-UE(2)_v1-0.xsd";
        /// <summary>
        /// Xml_converts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="output_type">The output_type.</param>
        /// <param name="output">The output.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        /// 0 - Success
        /// 1 - Output Type not suported; Suported type == 1 -> PDF
        /// 2 - Input string null or empty
        /// 3 - Xml deseialization error.
        /// 4 - Error while generating template fields
        /// 5 - Document rendering error
        /// </returns>
        public short xml_convert(string xml, short output_type, ref byte[] output, ref Exception error)
        {
            if (output_type == 2)
            {
                error = new ArgumentException("output_type not supported");
                return 1;
            }

            if (String.IsNullOrEmpty(xml))
            {
                error = new ArgumentException("Input string 'xml' cannot be empty.");
                return 2;
            }

            //Validation
            Deklaracja deklaracja = null;
            try
            {
                deklaracja = Validate(xml);
                //deklaracja = Deklaracja.Deserialize(xml);
            }
            catch (Exception ex)
            {
                error = ex;
                return 3;
            }

            //Template filling with xml values
            var vat = new VatUE2v10Template();
            try
            {
                FillTemplate(vat, deklaracja);
            }
            catch (Exception ex)
            {
                error = ex;
                return 4;
            }

            //Rendering output
            try
            {
                output = Render(vat);
            }
            catch (Exception ex)
            {
                error = ex;
                return 5;
            }

            return 0;
        }

        private static void FillTemplate(VatUE2v10Template vat, Deklaracja deklaracja)
        {
            var nip = deklaracja.Podmiot1.IsOsobaFizyczna
                          ? deklaracja.Podmiot1.OsobaFizyczna.NIP
                          : deklaracja.Podmiot1.OsobaNiefizyczna.NIP;

            vat.AppendHeader(deklaracja.Naglowek, nip);
            vat.AppendAContent(deklaracja.Naglowek.KodUrzedu);
            vat.AppendBContent(deklaracja.Podmiot1);
            vat.AppendCContent(deklaracja.PozycjeSzczegolowe.Grupa1);
            vat.AppendDContent(deklaracja.PozycjeSzczegolowe.Grupa2);
            vat.AppendEContent(deklaracja.PozycjeSzczegolowe.Grupa3);
            vat.AppendFContent();
        }

        private static byte[] Render(VatUE2v10Template vat)
        {
            PdfDocument document = vat.Render();

            var stream = new MemoryStream();
            document.Save(stream, false);
            byte[] output = stream.ToArray();
            stream.Close();

            return output;
        }

        private static Deklaracja Validate(string xml)
        {

            Deklaracja deklaracja = null;
            IValidator validator = new Validator();

            bool validateResult = validator.ValidateStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)),
                                                           SchemaResourceName);
            if (validateResult)
            {
                deklaracja = Deklaracja.Deserialize(xml);
            }
            else
            {
                throw new Exception("Validation Error: " +
                                    String.Join(System.Environment.NewLine, validator.ValidationErrors));
            }
            return deklaracja;
        }
    }
}