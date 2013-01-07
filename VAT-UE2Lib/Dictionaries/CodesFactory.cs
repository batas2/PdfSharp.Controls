using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace VATUE2Lib.Dictionaries
{
    public interface ICodesFactory
    {
        string ResourceName { get; }
        string GetValue(string key);
        void SetValue(string key, string value);
    }

    public class CodesFactory : ICodesFactory
    {
        private const string StartTag = "xsd:enumeration";

        private readonly string _resourceName;
        private readonly Dictionary<string, string> _dictionary;

        public CodesFactory(string resourceName)
        {
            _dictionary = new Dictionary<string, string>();
            _resourceName = resourceName;

            ParseFile(_resourceName);
        }

        public string ResourceName
        {
            get { return _resourceName; }
        }

        public string GetValue(string key)
        {
            return _dictionary[key];
        }

        public void SetValue(string key, string value)
        {
            _dictionary.Add(key, value);
        }

        private void ParseFile(string resourceName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceStream = assembly.GetManifestResourceStream(resourceName);

                if (resourceStream == null || resourceStream.Length == 0) return;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(resourceStream);


                XmlNodeList usCodes = xmlDoc.GetElementsByTagName(StartTag);

                foreach (XmlNode usCode in usCodes)
                {
                    if (usCode.Attributes != null && usCode.Attributes.Count > 0 && !String.IsNullOrEmpty(usCode.InnerText))
                    {
                        var key = usCode.Attributes[0].Value;
                        var val = usCode.InnerText;
                        _dictionary.Add(key, val);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
