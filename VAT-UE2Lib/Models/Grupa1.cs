using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VATUE2Lib.Models
{
    public partial class Grupa1
    {
        [XmlIgnore]
        public bool IsChecked
        {
            get { return this.P_Dd == 2; }
            set { P_Dd = value ? (sbyte)2 : (sbyte)1; }
        }
    }
}
