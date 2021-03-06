﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using SharpPdf.Controls;

namespace PdfSharp.ControlUnitTests
{
    /// <summary>
    ///This is a test class for LabelTest and is intended
    ///to contain all LabelTest Unit Tests
    ///</summary>
    [TestClass]
    public class LabelTest
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
        ///A test for Draw
        ///</summary>
        [TestMethod]
        public void DrawTest()
        {
            var rect = new XRect(0, 0, 10, 40);
            var alignment = new XParagraphAlignment(); 
            string content = "foo"; 
            var target = new Label(alignment, content){Rect = rect};
            PdfDocument pdf = new PdfDocument();
            var page = pdf.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            target.Draw(gfx);
            Assert.AreEqual(target.Rect, rect);
        }
    }
}