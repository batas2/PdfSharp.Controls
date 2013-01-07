using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using SharpPdf.Controls;
using VATUE2Lib.Dictionaries;
using VATUE2Lib.Models;

namespace VATUE2Lib
{
    internal class VatUE2v10Template
    {
        private readonly DocumentContainer _document;

        public VatUE2v10Template()
        {
            _document = new DocumentContainer();
        }

        #region Header_Content

        public void AppendHeader(TNaglowek naglowek, string nip)
        {

            var headerBigFont = new XFont(DefaultValues.Groupbox.Font.FontFamily.Name, 12, XFontStyle.Bold, DefaultValues.Groupbox.Font.PdfOptions);

            var nipContent = new GroupBox(0, _document.Width * 3 / 4, "") { MarginLeft = 0, MarginTop = 0, MarginRight = 0, MarginBottom = 0 };
            var percentRowWidths = new[] { 45, 35, 20 };
            var rowControls = new[]
                {
                    new InputBox(Resources.InputTitle1, nip),
                    new InputBox(Resources.InputTitle2) {Brush = XBrushes.LightGray},
                    new InputBox(Resources.InputTitle3) {Brush = XBrushes.LightGray},
                };

            nipContent.AddRow(rowControls, percentRowWidths);
            _document.AddContainer(nipContent);

            var miesiacVal = naglowek.DateType == ItemChoiceType.Miesiac ? naglowek.Miesiac.ToString() : "";
            var kwartalVal = naglowek.ItemElementName == ItemChoiceType.Kwartal ? naglowek.Kwartal.ToString() : "";

            var titleContent = new GroupBox(0, _document.Width, "")
                {
                    MarginBottom = 10,
                    Brush = new XSolidBrush(XColor.FromArgb(128, 128, 255)),
                    Controls = new List<IControl>
                        {
                            new Label(20, 0, Resources.TitleLabel1) {Font = headerBigFont},
                            new Label(150, 0, Resources.TitleLabel2) {Font = headerBigFont},
                            new Label(270, 50, Resources.TitleLabel3) {Font = headerBigFont},
                            new InputBox(new XRect(200, 40, 60, 30), Resources.InputTitle4, miesiacVal),
                            new InputBox(new XRect(300, 40, 60, 30), Resources.InputTitle5, kwartalVal),
                            new InputBox(new XRect(400, 40, 80, 30), Resources.InputTitle6, naglowek.Rok)
                        }
                };
            _document.AddContainer(titleContent);

            var infoFont = new XFont(DocumentContainer.FontFamily, 8);
            var infoContent = new GroupBox(0, _document.Width, "")
                {
                    MarginLeft = 5,
                    MarginTop = 1,
                    MarginRight = 1,
                    MarginBottom = 1,
                    Controls = new List<IControl>
                        {
                            new Label(0, 0, Resources.InfoContent0Label) {Font = infoFont},
                            new Label(0, 10, Resources.InfoContent1Label) {Font = infoFont},
                            new Label(0, 40, Resources.InfoContent2Label) {Font = infoFont},
                            new Label(0, 50, Resources.InfoContent3Label) {Font = infoFont},
                            new Label(new XRect(80, 0, _document.Width, 50), Resources.InfoContent0Label){Font = infoFont},
                            new Label(new XRect(80, 10, _document.Width, 50), Resources.InfoContent1Value){Font = infoFont},
                            new Label(new XRect(80, 40, _document.Width, 50), Resources.InfoContent2Value){Font = infoFont},
                            new Label(new XRect(80, 50, _document.Width, 70), Resources.InfoContent3Value){Font = infoFont},
                        }
                };
            _document.AddContainer(infoContent);
        }

        #endregion

        #region A_Content

        public void AppendAContent(TKodUS kodUs)
        {
            var aContent = new GroupBox(0, _document.Width, Resources.ContainerTitleA);
            aContent.AddChild(new InputBox(new XRect(0, 0, _document.Width, 30))
                {
                    Title = Resources.InputTitle7,
                    Value = KodUSDictionary.GetCode(kodUs),
                });
            _document.AddContainer(aContent);
        }

        #endregion

        #region B_Content

        /// <summary>
        /// Appends the content of the B.
        /// </summary>
        /// <param name="podmiot">The podmiot.</param>
        public void AppendBContent(DeklaracjaPodmiot1 podmiot)
        {
            var bContent = new GroupBox(0, _document.Width, Resources.ContainerTitleB) { MarginBottom = 10 };
            _document.AddContainer(bContent);

            var font = new XFont(DocumentContainer.FontFamily, 12, XFontStyle.Regular);

            var b1Content = new GroupBox(0, _document.Width, Resources.ContainerTitleB1)
                {
                    Font = font,
                    Controls = new List<IControl>
                        {
                            new InputBox(new XRect(0, 0, _document.Width, 30), Resources.InputTitle8),
                            new CheckBox(100, 5) {Title = Resources.InputTitle81, IsChecked = !podmiot.IsOsobaFizyczna},
                            new CheckBox(400, 5) {Title = Resources.InputTitle82, IsChecked = podmiot.IsOsobaFizyczna},
                        }
                };

            if (podmiot.IsOsobaFizyczna)
            {
                AppendB1Content(b1Content, podmiot.OsobaFizyczna);
            }
            else
            {
                AppendB1Content(b1Content, podmiot.OsobaNiefizyczna);
            }

            var b2Content = new GroupBox(0, _document.Width,
                                         podmiot.IsOsobaFizyczna
                                             ? Resources.ContainerTitleB2A
                                             : Resources.ContainerTitleB2B) { Font = font };


            if (podmiot.AdresZamieszkaniaSiedziby.IsAdresPol)
            {
                AppendB2Content(b2Content, podmiot.AdresZamieszkaniaSiedziby.AdresPolski);
            }
            else
            {
                AppendB2Content(b2Content, podmiot.AdresZamieszkaniaSiedziby.AdresZagraniczny);
            }

            _document.AddContainer(b1Content);
            _document.AddContainer(b2Content);
        }

        private void AppendB1Content(GroupBox b1Content, TIdentyfikatorOsobyNiefizycznej osobyNiefizycznej)
        {
            var container = new GroupBox()
            {
                MarginTop = 0,
                MarginBottom = 0,
                MarginLeft = 0,
                MarginRight = 0,
                Brush = XBrushes.White
            };
            var companyDataWidths = new[] { 75, 25 };
            var companyDataControls = new[]
                {
                    new InputBox(Resources.InputTitle9D, osobyNiefizycznej.PelnaNazwa){Pen = XPens.Transparent},
                    new InputBox(Resources.InputTitle9E, osobyNiefizycznej.REGON){Pen = XPens.Transparent},
                };
            container.AddRow(companyDataControls, companyDataWidths);
            b1Content.AddRow(container);
        }

        private void AppendB1Content(GroupBox b1Content, TIdentyfikatorOsobyFizycznej osobyFizycznej)
        {
            var container = new GroupBox()
                {
                    MarginTop = 0,
                    MarginBottom = 0,
                    MarginLeft = 0,
                    MarginRight = 0,
                    Brush = XBrushes.White
                };
            var personDataWidths = new[] { 25, 25, 25, 25 };
            var personDataControls = new[]
                {
                    new InputBox(Resources.InputTitle9, osobyFizycznej.Nazwisko){Pen = XPens.Transparent},
                    new InputBox(Resources.InputTitle9A, osobyFizycznej.ImiePierwsze){Pen = XPens.Transparent},
                    new InputBox(Resources.InputTitle9B, osobyFizycznej.DataUrodzenia.ToString("dd-MM-yyyy")){Pen = XPens.Transparent},
                    new InputBox(Resources.InputTitle9C, osobyFizycznej.PESEL){Pen = XPens.Transparent},
                };
            container.AddRow(personDataControls, personDataWidths);
            b1Content.AddRow(container);
        }

        private void AppendB2Content(GroupBox b2Content, TAdresPolski adres)
        {
            var percentRow1Widths = new[] { 20, 45, 35 };
            var row1Controls = new[]
                {
                    new InputBox(Resources.InputTitle10, KodKrajowyDictionary.GetCode(adres.KodKraju)),
                    new InputBox(Resources.InputTitle11, adres.Wojewodztwo),
                    new InputBox(Resources.InputTitle12, adres.Powiat),
                };

            var percentRow2Widths = new[] { 25, 45, 15, 15 };
            var row2Controls = new[]
                {
                    new InputBox(Resources.InputTitle13, adres.Gmina),
                    new InputBox(Resources.InputTitle14, adres.Ulica),
                    new InputBox(Resources.InputTitle15, adres.NrDomu),
                    new InputBox(Resources.InputTitle16, adres.NrLokalu)
                };

            var percentRow3Widths = new[] { 40, 20, 40 };
            var row3Controls = new[]
                {
                    new InputBox(Resources.InputTitle17, adres.Miejscowosc),
                    new InputBox(Resources.InputTitle18, adres.KodPocztowy),
                    new InputBox(Resources.InputTitle19, adres.Poczta)
                };


            b2Content.AddRow(row1Controls, percentRow1Widths);
            b2Content.AddRow(row2Controls, percentRow2Widths);
            b2Content.AddRow(row3Controls, percentRow3Widths);
        }

        private void AppendB2Content(GroupBox b2Content, TAdresZagraniczny adres)
        {
            var percentRow1Widths = new[] { 20, 45, 35 };
            var row1Controls = new[]
                {
                    new InputBox(Resources.InputTitle10, KodKrajowyDictionary.GetCode(adres.KodKraju)),
                    new InputBox(Resources.InputTitle11),
                    new InputBox(Resources.InputTitle12),
                };

            var percentRow2Widths = new[] { 25, 45, 15, 15 };
            var row2Controls = new[]
                {
                    new InputBox(Resources.InputTitle13),
                    new InputBox(Resources.InputTitle14, adres.Ulica),
                    new InputBox(Resources.InputTitle15, adres.NrDomu),
                    new InputBox(Resources.InputTitle16, adres.NrLokalu)
                };

            var percentRow3Widths = new[] { 40, 20, 40 };
            var row3Controls = new[]
                {
                    new InputBox(Resources.InputTitle17, adres.Miejscowosc),
                    new InputBox(Resources.InputTitle18, adres.KodPocztowy),
                    new InputBox(Resources.InputTitle19)
                };


            b2Content.AddRow(row1Controls, percentRow1Widths);
            b2Content.AddRow(row2Controls, percentRow2Widths);
            b2Content.AddRow(row3Controls, percentRow3Widths);
        }

        #endregion

        #region C_Content

        public void AppendCContent(IEnumerable<Grupa1> grupa1)
        {
            var headFont = new XFont(DocumentContainer.FontFamily, 10, XFontStyle.Bold);
            var head2Font = new XFont(DocumentContainer.FontFamily, 9, XFontStyle.Regular);

            var tableHeadWidths = new[] { 10, 40, 35, 15 };
            var cContent = new TableControl(4, tableHeadWidths, 0, _document.Width,
                                         Resources.ContainerTitleC);
            var tableHeadControls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellA) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellB) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellC) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellD) {Font = headFont},
                };

            var tableHead2Controls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellA) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellB) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellC) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellD) {Font = head2Font},
                };
            cContent.AddHeadRow(tableHeadControls, 5, 15);
            cContent.AddHeadRow(tableHead2Controls, 2, 2);

            foreach (Grupa1 grupa in grupa1)
            {
                var tableRowControls = new Collection<IControl>()
                    {
                        new TableCell(grupa.P_Da.ToString()),
                        new TableCell(grupa.P_Db),
                        new TableCell(grupa.P_Dc.ToString()),
                        new GroupBox(0, 50, "")
                            {
                                MarginTop = 5, MarginBottom = 5, MarginLeft = 30, MarginRight = 1,
                                Controls = new[]{new CheckBox(0, 0){IsChecked = grupa.IsChecked}}, Brush = XBrushes.White,
                            },  
                    };
                cContent.AddRow(tableRowControls);
            }

            _document.AddContainer(cContent);
        }

        #endregion

        #region D_Content

        public void AppendDContent(IEnumerable<Grupa2> grupa2)
        {
            var headFont = new XFont(DocumentContainer.FontFamily, 10, XFontStyle.Bold);
            var head2Font = new XFont(DocumentContainer.FontFamily, 9, XFontStyle.Regular);

            var tableHeadWidths = new[] { 10, 40, 35, 15 };
            var dContent = new TableControl(4, tableHeadWidths, 0, _document.Width,
                                         Resources.ContainerTitleD);
            var tableHeadControls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellA) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellB) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellC) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellD) {Font = headFont},
                };

            var tableHead2Controls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellA) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellB) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellC) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellD) {Font = head2Font},
                };
            dContent.AddHeadRow(tableHeadControls, 5, 15);
            dContent.AddHeadRow(tableHead2Controls, 0, 0);

            foreach (Grupa2 grupa in grupa2)
            {
                var tableRowControls = new Collection<IControl>()
                    {
                        new TableCell(grupa.P_Na.ToString()),
                        new TableCell(grupa.P_Nb),
                        new TableCell(grupa.P_Nc.ToString()),
                        new GroupBox(0, 50, "")
                            {
                                MarginTop = 5, MarginBottom = 5, MarginLeft = 35, MarginRight = 1,
                                Controls = new[]{new CheckBox(0, 0){IsChecked = grupa.IsChecked}}, Brush = XBrushes.White,
                            },   
                    };
                dContent.AddRow(tableRowControls);
            }

            _document.AddContainer(dContent);
        }

        #endregion

        #region E_Content

        public void AppendEContent(IEnumerable<Grupa3> grupa3)
        {
            var headFont = new XFont(DocumentContainer.FontFamily, 10, XFontStyle.Bold);
            var head2Font = new XFont(DocumentContainer.FontFamily, 9, XFontStyle.Regular);

            var tableHeadWidths = new[] { 10, 40, 35, 15 };
            var eContent = new TableControl(4, tableHeadWidths, 0, _document.Width,
                                         Resources.ContainerTitleE);
            var tableHeadControls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellA) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellB) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellC) {Font = headFont},
                    new Label(XParagraphAlignment.Center, Resources.TableHeadCellD) {Font = headFont},
                };

            var tableHead2Controls = new[]
                {
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellA) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellB) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellC) {Font = head2Font},
                    new Label(XParagraphAlignment.Center, Resources.TableHead2CellD) {Font = head2Font},
                };
            eContent.AddHeadRow(tableHeadControls, 5, 15);
            eContent.AddHeadRow(tableHead2Controls, 0, 0);

            foreach (Grupa3 grupa in grupa3)
            {
                var tableRowControls = new[]
                    {
                        new TableCell(grupa.P_Ua.ToString()),
                        new TableCell(grupa.P_Ub),
                        new TableCell(grupa.P_Uc.ToString()),
                        new TableCell()
                    };
                eContent.AddRow(tableRowControls);
            }

            _document.AddContainer(eContent);
        }

        #endregion

        #region F_Content

        public void AppendFContent()
        {
            var fContent = new GroupBox(0, _document.Width,
                                        Resources.ContainerTitleF);

            var percentRow1Widths = new[] { 40, 60 };
            var row1Controls = new[]
                {
                    new InputBox(Resources.InputTitle20),
                    new InputBox(Resources.InputTitle21),
                };

            var percentRow2Widths = new[] { 25, 25, 50 };
            var row2Controls = new[]
                {
                    new InputBox(Resources.InputTitle22),
                    new InputBox(Resources.InputTitle23),
                    new InputBox(Resources.InputTitle24)
                };
            fContent.AddRow(row1Controls, percentRow1Widths);
            fContent.AddRow(row2Controls, percentRow2Widths);

            _document.AddContainer(fContent);
        }

        #endregion

        public PdfDocument Render()
        {
            return _document.Render();
        }
    }
}