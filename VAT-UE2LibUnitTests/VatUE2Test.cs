using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using VATUE2Lib;

namespace DeloitteLibUnitTests
{
    /// <summary>
    ///This is a test class for VatUE2Test and is intended
    ///to contain all VatUE2Test Unit Tests
    ///</summary>
    [TestClass]
    public class VatUE2Test
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            //    SampleDocGenerator.Sample1(@"Samples\sample1.xml");
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        /// <summary>
        ///A test for xml_convert
        ///</summary>
        [TestMethod]
        [DeploymentItem(@"Samples\sample1_output.pdf", "Samples")]
        [DeploymentItem(@"Samples\sample1.xml", "Samples")]
        public void xml_convertTest()
        {
            var target = new VatUE2(); // TODO: Initialize to an appropriate value
            string xml = File.ReadAllText(@"Samples\sample1.xml"); // TODO: Initialize to an appropriate value
            short output_type = 0; // TODO: Initialize to an appropriate value

            PdfDocument document = PdfReader.Open(@"Samples\sample1_output.pdf");
            var stream = new MemoryStream();
            document.Save(stream, false);
            byte[] outputExpected = stream.ToArray();
            stream.Close();


            var output = new byte[1];

            var error = new Exception(); // TODO: Initialize to an appropriate value
            var errorExpected = new Exception(); // TODO: Initialize to an appropriate value

            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            actual = target.xml_convert(xml, output_type, ref output, ref error);

            //var d = PdfReader.Open(new MemoryStream(output, 0, output.Length));

            //const string filename = @"Samples\sample1_output.pdf";
            //document.Save(filename);


            Assert.AreEqual(expected, actual);
        }
    }
}