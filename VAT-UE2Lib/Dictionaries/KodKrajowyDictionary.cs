using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VATUE2Lib.Models;

namespace VATUE2Lib.Dictionaries
{
    public class KodKrajowyDictionary
    {
        public const string ResourceName = "VATUE2Lib.Dictionaries.KodyKrajowUE_v1-0.xsd";
        private static readonly ICodesFactory CodesFactory = new CodesFactory(ResourceName);

        static KodKrajowyDictionary()
        {
            CodesFactory.SetValue("PL", "Polska");
        }

        public static string GetCode(TKodKraju code)
        {
            try
            {
                return CodesFactory.GetValue(code.ToString());
            }
            catch 
            {
                throw new Exception("Coulnd not find code:" + code);
            }
        }
    }
}
