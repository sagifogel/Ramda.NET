using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class XProd
    {
        private readonly int[] a = new[] { 1, 2 };
        private readonly string[] b = new[] { "a", "b", "c" };

        [TestMethod]
        public void XProd_Returns_An_Empty_List_If_Either_Input_List_Is_Empty() {
            CollectionAssert.AreEqual(R.XProd(new int[0], new[] { 1, 2, 3 }), new int[0]);
            CollectionAssert.AreEqual(R.XProd(new[] { 1, 2, 3 }, new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("XProd_Creates_The_Collection_Of_All_Cross-Product_Pairs_Of_Its_Parameters")]
        public void XProd_Creates_The_Collection_Of_All_Cross_Product_Pairs_Of_Its_Parameters() {
            NestedCollectionAssert.AreEqual(R.XProd(a, b), new object[] { new object[] { 1, "a" }, new object[] { 1, "b" }, new object[] { 1, "c" }, new object[] { 2, "a" }, new object[] { 2, "b" }, new object[] { 2, "c" } });
        }

        [TestMethod]
        public void XProd_Is_Curried() {
            var something = R.XProd(b);

            NestedCollectionAssert.AreEqual(something(a), new object[] { new object[] { "a", 1 }, new object[] { "a", 2 }, new object[] { "b", 1 }, new object[] { "b", 2 }, new object[] { "c", 1 }, new object[] { "c", 2 } });
        }

        [TestMethod]
        public void XProd_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var something = R.XProd(a);

            Assert.AreEqual(something.Length, 1);
        }
    }
}
