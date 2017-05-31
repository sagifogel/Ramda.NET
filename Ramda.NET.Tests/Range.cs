using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Range
    {
        [TestMethod]
        public void Range_Returns_List_Of_Numbers() {
            CollectionAssert.AreEqual(R.Range(0, 5), new[] { 0, 1, 2, 3, 4 });
            CollectionAssert.AreEqual(R.Range(4, 7), new[] { 4, 5, 6 });
        }

        [TestMethod]
        public void Range_Returns_The_Empty_List_If_The_First_Parameter_Is_Not_Larger_Than_The_Second() {
            CollectionAssert.AreEqual(R.Range(7, 3), new int[0]);
            CollectionAssert.AreEqual(R.Range(5, 5), new int[0]);
        }

        [TestMethod]
        public void Range_Is_Curried() {
            var from10 = R.Range(10);

            CollectionAssert.AreEqual(from10(15), new[] { 10, 11, 12, 13, 14 });
        }
    }
}
