using System;
using System.Collections.Generic;
using VATUE2Lib.Models;

namespace DeloitteLibUnitTests
{
    public class SampleDocGenerator
    {
        public static void Sample1(string fileName)
        {
            var deklaracja = new Deklaracja();
            var naglowek = new TNaglowek
                {
                    CelZlozenia = 3,
                    Rok = DateTime.Now.Year.ToString(),
                    WariantFormularza = 2,
                    KodUrzedu = TKodUS.Item1813,
                    KodFormularza = new TNaglowekKodFormularza(),
                    Item = 8,
                    ItemElementName = ItemChoiceType.Miesiac
                };

            var podmiot = new DeklaracjaPodmiot1
                {
                    OsobaFizyczna = new TIdentyfikatorOsobyFizycznej
                        {
                            DataUrodzenia = DateTime.Now,
                            ImiePierwsze = "Bartosz",
                            Nazwisko = "Frąckowiak",
                            NIP = "123-456-32-18",
                            PESEL = "89082606238"
                        },
                    AdresZamieszkaniaSiedziby = new TPodmiotDowolnyAdresZamieszkaniaSiedziby
                        {
                            AdresPolski = new TAdresPolski
                                {
                                    Gmina = "Bydgoszcz",
                                    KodKraju = TKodKraju.PL,
                                    KodPocztowy = "85-200",
                                    Miejscowosc = "Bydgoszcz",
                                    NrDomu = "33",
                                    NrLokalu = "3",
                                    Poczta = "Polska",
                                    Powiat = "Bydgoski",
                                    Ulica = "Łokietka 33",
                                    Wojewodztwo = "Kuj-Pom"
                                }
                        },
                };
            var pozycjeSzczegulowe = new PozycjeSzczegolowe
                {
                    Grupa1 =
                        new List<Grupa1>
                            {
                                new Grupa1 {P_Da = TKodKrajuUE.EE, P_Db = "a", P_Dc = 2, P_Dd = 2},
                                new Grupa1 {P_Da = TKodKrajuUE.CY, P_Db = "b", P_Dc = 3, P_Dd = 3}
                            },
                    Grupa2 =
                        new List<Grupa2>
                            {
                                new Grupa2 {P_Na = TKodKrajuUE.ES, P_Nb = "c", P_Nc = 4, P_Nd = 4},
                                new Grupa2 {P_Na = TKodKrajuUE.EE, P_Nb = "c", P_Nc = 5, P_Nd = 5}
                            },
                    Grupa3 = new List<Grupa3>
                        {
                            new Grupa3 {P_Ua = TKodKrajuUE.IT, P_Ub = "e", P_Uc = 6},
                            new Grupa3 {P_Ua = TKodKrajuUE.ES, P_Ub = "f", P_Uc = 7}
                        }
                };
            deklaracja.Podmiot1 = podmiot;
            deklaracja.PozycjeSzczegolowe = pozycjeSzczegulowe;
            deklaracja.SetOswiadczenie();
            deklaracja.Naglowek = naglowek;

            deklaracja.SaveToFile(fileName);
        }
    }
}