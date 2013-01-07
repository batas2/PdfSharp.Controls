using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpPdf.Controls;

namespace PdfSharp.ControlUnitTests
{
    /// <summary>
    ///This is a test class for TableDataTest and is intended
    ///to contain all TableDataTest Unit Tests
    ///</summary>
    [TestClass]
    public class TableDataTest
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
        ///A test for AddRow
        ///</summary>
        [TestMethod]
        public void AddRowTest()
        {
            int columnCount = 4;
            ICollection<int> columnPercentWidths = new Collection<int>() { 25, 25, 25, 25 };
            double left = 10;
            double width = 120;
            string title = "title";
            var target = new TableControl(columnCount, columnPercentWidths, left, width, title);

            ICollection<IControl> controls = new Collection<IControl>()
                {
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar")
                };
            int marginTop = 2;
            int marginBottom = 2;
            target.AddRow(controls, marginTop, marginBottom);
            Assert.AreEqual(target.ColumnCount, columnCount);
            Assert.AreEqual(target.Rows.Count, 1);

        }

        /// <summary>
        ///A test for AddHeadRow
        ///</summary>
        [TestMethod]
        public void AddHeadRowTest()
        {
            int columnCount = 4;
            ICollection<int> columnPercentWidths = new Collection<int>() { 25, 25, 25, 25 };
            double left = 10;
            double width = 120;
            string title = "title";
            var target = new TableControl(columnCount, columnPercentWidths, left, width, title);

            ICollection<IControl> controls = new Collection<IControl>()
                {
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar"),
                    new InputBox("foo", "bar")
                };

            int marginTop = 0;
            int marginBottom = 0;
            target.AddHeadRow(controls, marginTop, marginBottom);
            Assert.AreEqual(target.ColumnCount, columnCount);
            Assert.AreEqual(target.HeadRows.Count, 1);
        }
    }
}