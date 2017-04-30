using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Head
    {
        [TestMethod]
        public void Head_Returns_The_First_Element_Of_An_Ordered_Collection() {
            Assert.AreEqual(R.Head(new[] { 1, 2, 3 }), 1);
            Assert.AreEqual(R.Head(new[] { 2, 3 }), 2);
            Assert.AreEqual(R.Head(new[] { 3 }), 3);
            Assert.AreEqual(R.Head(new object[0]), R.Null);
            Assert.AreEqual(R.Head("abc"), "a");
            Assert.AreEqual(R.Head("bc"), "b");
            Assert.AreEqual(R.Head("c"), "c");
            Assert.AreEqual(R.Head(""), "");
        }
    }
}
