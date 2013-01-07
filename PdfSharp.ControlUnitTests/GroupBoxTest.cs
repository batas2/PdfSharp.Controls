using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharp.Drawing;
using SharpPdf.Controls;

namespace PdfSharp.ControlUnitTests
{
    /// <summary>
    ///This is a test class for GroupBoxTest and is intended
    ///to contain all GroupBoxTest Unit Tests
    ///</summary>
    [TestClass]
    public class GroupBoxTest
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
            var percentWidths = new[] { 20, 30, 50 };
            var controls = new[]
                {
                    new InputBox(new XRect(0, 0, 10, 20), ""),
                    new InputBox(new XRect(0, 0, 10, 30), ""),
                    new InputBox(new XRect(0, 0, 10, 50), ""),
                };

            var rect = new XRect(0, 0, 100 + DefaultValues.Groupbox.MarginLeft + DefaultValues.Groupbox.MarginRight, 10);
            var target = new GroupBox(rect);

            target.AddRow(controls, percentWidths);
            foreach (var control in controls.Zip(target.Controls, (i, o) => new { Input = i, Output = o }))
            {
                Assert.AreSame(control.Input, control.Output);
            }

            Assert.AreEqual(target.Controls.ElementAt(0).Rect.Left, 0);
            Assert.AreEqual(target.Controls.ElementAt(1).Rect.Left, 20);
            Assert.AreEqual(target.Controls.ElementAt(2).Rect.Left, 50);

            Assert.AreEqual(target.Controls.ElementAt(0).Rect.Width, 20);
            Assert.AreEqual(target.Controls.ElementAt(1).Rect.Width, 30);
            Assert.AreEqual(target.Controls.ElementAt(2).Rect.Width, 50);

            Assert.AreEqual(target.Rect.Height, target.MarginBottom + target.MarginTop + 50);
        }

        /// <summary>
        ///A test for AddChild
        ///</summary>
        [TestMethod]
        public void AddChildTest()
        {
            var rect = new XRect();
            var target = new GroupBox(rect);
            IControl control = new Label(0, 0, "foo");
            target.AddChild(control);
            Assert.AreEqual(target.Controls.Count, 1);
        }

        /// <summary>
        ///A test for AddRow
        ///</summary>
        [TestMethod]
        public void AddRowTest1()
        {
            var rect = new XRect(0, 0, 50, 50);
            var target = new GroupBox(rect);
            IControl control = new InputBox("foo", "bar");
            target.AddRow(control);
            Assert.AreEqual(target.Controls.Contains(control), true);
        }

        /// <summary>
        ///A test for AddRow
        ///</summary>
        [TestMethod]
        public void AddRowTest2()
        {
            var rect = new XRect(0, 0, 100, 20);
            var target = new GroupBox(rect);
            ICollection<IControl> controls = new Collection<IControl> { new Label(0, 0, "foo"), new Label(0, 0, "bar") };
            ICollection<int> percentWidths = new Collection<int>() { 50, 50 };
            target.AddRow(controls, percentWidths);
            Assert.AreEqual(target.Controls.Count, 2);
        }
    }
}