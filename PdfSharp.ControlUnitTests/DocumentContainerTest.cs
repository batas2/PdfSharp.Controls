using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharp.Pdf;
using SharpPdf.Controls;

namespace PdfSharp.ControlUnitTests
{
    /// <summary>
    ///This is a test class for DocumentContainerTest and is intended
    ///to contain all DocumentContainerTest Unit Tests
    ///</summary>
    [TestClass]
    public class DocumentContainerTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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
        ///A test for Render
        ///</summary>
        [TestMethod]
        public void RenderTest()
        {
            var target = new DocumentContainer(); 
            PdfDocument expected = null; 
            PdfDocument actual;
            actual = target.Render();
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for AddContainer
        ///</summary>
        [TestMethod]
        public void AddContainerTest()
        {
            var target = new DocumentContainer(); 
            IContainerControl container = null; 
            target.AddContainer(container);
            
        }
    }
}