using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Tail
    {
        [TestMethod]
        public void Tail_Returns_The_Tail_Of_An_Ordered_Collection() {
            CollectionAssert.AreEqual(R.Tail(new[] { 1, 2, 3 }), new[] { 2, 3 });
            CollectionAssert.AreEqual(R.Tail(new[] { 2, 3 }), new[] { 3 });
            CollectionAssert.AreEqual(R.Tail(new[] { 3 }), new int[0]);
            CollectionAssert.AreEqual(R.Tail(new int[0]), new int[0]);
            Assert.AreEqual(R.Tail("abc"), "bc");
            Assert.AreEqual(R.Tail("bc"), "c");
            Assert.AreEqual(R.Tail("c"), "");
            Assert.AreEqual(R.Tail(""), "");
        }
    }
}
