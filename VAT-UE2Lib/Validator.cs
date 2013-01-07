using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace VATUE2Lib
{
    public interface IValidator
    {
        IList<string> ValidationErrors { get; }
        bool ValidateStream(Stream dataStream, string schemaResource);
    }

    public class Validator : IValidator
    {
        public IList<string> ValidationErrors { get; private set; }

        public Validator()
        {
            ValidationErrors = new List<string>();
        }


        public bool ValidateStream(Stream dataStream, string schemaResource)
        {

            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(schemaResource);

            if (resourceStream == null || resourceStream.Length == 0) return false;

            XmlReaderSettings settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            settings.Schemas.Add("", XmlReader.Create(resourceStream));

            XmlReader reader = XmlReader.Create(dataStream, settings);

            while (reader.Read()) ;

            return !ValidationErrors.Any();
        }


        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                ValidationErrors.Add("Warning: Matching schema not found. No validation occurred." + args.Message);
            else
                ValidationErrors.Add("Validation error: " + args.Message);

        }
    }
}
