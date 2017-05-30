using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Prepend
    {
        [TestMethod]
        public void Prepend_Adds_The_Element_To_The_Beginning_Of_The_List() {
            CollectionAssert.AreEqual(R.Prepend("x", new[] { "y", "z" }), new[] { "x", "y", "z" });
            NestedCollectionAssert.AreEqual(R.Prepend(new[] { "a", "z" }, new[] { "x", "y" }), new object[] { new[] { "a", "z" }, "x", "y" });
        }

        [TestMethod]
        public void Prepend_Works_On_Empty_List() {
            CollectionAssert.AreEqual(R.Prepend(1, new int[0]), new[] { 1 });
        }

        [TestMethod]
        public void Prepend_Is_Curried() {
            Assert.IsInstanceOfType(R.Prepend(4), typeof(DynamicDelegate));
            CollectionAssert.AreEqual(R.Prepend(4)(new[] { 3, 2, 1 }), new[] { 4, 3, 2, 1 });
        }
    }
}
