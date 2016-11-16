using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Append
    {
        [TestMethod]
        public void Append_Adds_The_Element_To_The_End_Of_The_List() {
            NestedCollectionAssert.AreEqual(R.Append("z", new[] { "x", "y" }), new[] { "x", "y", "z" });
            NestedCollectionAssert.AreEqual((IList)R.Append<object>(new string[] { "a", "z" }, new[] { "x", "y" }), new object[] { "x", "y", new object[] { "a", "z" } });
        }

        [TestMethod]
        public void Append_Works_On_Empty_List() {
            CollectionAssert.AreEqual(R.Append(1, new object[0]), new[] { 1 });
        }

        [TestMethod]
        public void Append_Is_Curried() {
            Assert.IsInstanceOfType(R.Append(4), typeof(DynamicDelegate));
            CollectionAssert.AreEqual((ICollection)R.Append(1)(new[] { 4, 3, 2 }), new[] { 4, 3, 2, 1 });
        }
    }
}
