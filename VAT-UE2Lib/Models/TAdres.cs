using System.Xml.Serialization;

namespace VATUE2Lib.Models
{
    public partial class TAdres
    {
        [XmlIgnore]
        public bool IsAdresPol
        {
            get { return Item is TAdresPolski; }
        }

        [XmlIgnore]
        public TAdresPolski AdresPolski
        {
            get { return Item as TAdresPolski; }
            set { Item = value; }
        }

        [XmlIgnore]
        public TAdresZagraniczny AdresZagraniczny
        {
            get { return Item as TAdresZagraniczny; }
            set { Item = value; }
        }
    }
}