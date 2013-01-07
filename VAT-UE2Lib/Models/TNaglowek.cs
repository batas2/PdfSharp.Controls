using System.Xml.Serialization;

namespace VATUE2Lib.Models
{
    public partial class TNaglowek
    {
        [XmlIgnore]
        public ItemChoiceType DateType
        {
            get { return itemElementNameField; }
        }

        [XmlIgnore]
        public sbyte Miesiac
        {
            get { return itemField; }
            set
            {
                itemField = value;
                itemElementNameField = ItemChoiceType.Miesiac;
            }
        }

        [XmlIgnore]
        public sbyte Kwartal
        {
            get { return itemField; }
            set
            {
                itemField = value;
                itemElementNameField = ItemChoiceType.Kwartal;
            }
        }
    }
}