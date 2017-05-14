using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Min
    {
        [TestMethod]
        public void Min_Returns_The_Smaller_Of_Its_Two_Arguments() {
            Assert.AreEqual(R.Min(-7, 7), -7);
            Assert.AreEqual(R.Min(7, -7), -7);
        }

        [TestMethod]
        public void Min_Works_For_Any_Orderable_Type() {
            var d1 = new DateTime(2001, 1, 1);
            var d2 = new DateTime(2002, 2, 2);

            Assert.AreEqual(R.Min(d1, d2), d1);
            Assert.AreEqual(R.Min(d2, d1), d1);
            Assert.AreEqual(R.Min('a', 'b'), 'a');
            Assert.AreEqual(R.Min('b', 'a'), 'a');
            Assert.AreEqual(R.Min("a", "b"), "a");
            Assert.AreEqual(R.Min("b", "a"), "a");
        }
    }
}
