using System.Xml.Serialization;

namespace VATUE2Lib.Models
{
    public partial class Deklaracja
    {
        public void SetOswiadczenie()
        {
            Oswiadczenie =
                DeklaracjaOswiadczenie.
                    OświadczamżesąmiznaneprzepisyKodeksukarnegoskarbowegooodpowiedzialnościzapodaniedanychniezgodnychzrzeczywistością;
        }
    }
}