using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VATUE2Lib.Models;

namespace VATUE2Lib.Dictionaries
{
    public static class KodUSDictionary
    {
        public const string ResourceName = "VATUE2Lib.Dictionaries.KodyUrzedowSkarbowych_v3-0.xsd";
        private static readonly ICodesFactory CodesFactory = new CodesFactory(ResourceName);

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^.0-9]", "", RegexOptions.Compiled);
        }

        public static string GetCode(TKodUS code)
        {
            try
            {
                var s = code.ToString();
                var codeStr = RemoveSpecialCharacters(s);
                return CodesFactory.GetValue(codeStr);
            }
            catch
            {
                throw new Exception("Coulnd not find code:" + code);
            }
        }
    }
}
