using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Zip
    {
        [TestMethod]
        [Description("Zip_Returns_An_Array_Of_\"Tuples\"")]
        public void Zip_Returns_An_Array_Of_Tuples() {
            var a = new[] { 1, 2, 3 };
            var b = new[] { 100, 200, 300 };

            NestedCollectionAssert.AreEqual(R.Zip(a, b), new object[] { new[] { 1, 100 }, new[] { 2, 200 }, new[] { 3, 300 } });
        }

        [TestMethod]
        public void Zip_Returns_A_List_As_Long_As_The_Shorter_Of_The_Lists_Input() {
            var a = new[] { 1, 2, 3 };
            var b = new[] { 100, 200, 300, 400 };
            var c = new[] { 10, 20 };

            NestedCollectionAssert.AreEqual(R.Zip(a, b), new object[] { new[] { 1, 100 }, new[] { 2, 200 }, new[] { 3, 300 } });
            NestedCollectionAssert.AreEqual(R.Zip(a, c), new object[] { new[] { 1, 10 }, new[] { 2, 20 }});
        }
    }
}
