using System;
using System.Xml.Serialization;

namespace VATUE2Lib.Models
{
    public partial class TPodmiotDowolnyBezAdresu
    {
        [XmlIgnore]
        public bool IsOsobaFizyczna
        {
            get
            {
                if (Item == null)
                {
                    throw new Exception();
                }
                return Item is TIdentyfikatorOsobyFizycznej;
            }
        }

        [XmlIgnore]
        public TIdentyfikatorOsobyFizycznej OsobaFizyczna
        {
            get { return Item as TIdentyfikatorOsobyFizycznej; }
            set { Item = value; }
        }

        [XmlIgnore]
        public TIdentyfikatorOsobyNiefizycznej OsobaNiefizyczna
        {
            get { return Item as TIdentyfikatorOsobyNiefizycznej; }
            set { Item = value; }
        }
    }
}