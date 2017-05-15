using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Last
    {
        [TestMethod]
        public void Last_Returns_The_First_Element_Of_An_Ordered_Collection() {
            Assert.AreEqual(R.Last(new[] { 1, 2, 3 }), 3);
            Assert.AreEqual(R.Last(new[] { 1, 2 }), 2);
            Assert.AreEqual(R.Last(new[] { 1 }), 1);
            Assert.AreEqual(R.Last(new object[0]), R.@null);
            Assert.AreEqual(R.Last("abc"), "c");
            Assert.AreEqual(R.Last("ab"), "b");
            Assert.AreEqual(R.Last("a"), "a");
            Assert.AreEqual(R.Last(string.Empty), string.Empty);
        }
    }
}
