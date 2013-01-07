using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace PdfSharpControls
{
    public static class SerializationExtensions
    {
        public static void Serialize(this ObservableCollection<string> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof (List<string>));
            using (FileStream stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list.ToList());
            }
        }

        public static void Deserialize(this ObservableCollection<string> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof (List<string>));
            using (FileStream stream = File.Open(fileName, FileMode.OpenOrCreate))
            {
                if (stream.Length == 0)
                    return;

                var newList = (List<string>) (serializer.Deserialize(stream));
                foreach (string item in newList)
                {
                    list.Add(item);
                }
            }
        }
    }
}