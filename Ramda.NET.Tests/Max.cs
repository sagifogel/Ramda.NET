using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Max
    {
        [TestMethod]
        public void Max_Returns_The_Larger_Of_Its_Two_Arguments() {
            Assert.AreEqual(R.Max(-7, 7), 7);
            Assert.AreEqual(R.Max(7, -7), 7);
        }

        [TestMethod]
        public void Max_Works_For_Any_Orderable_Type() {
            var d1 = new DateTime(2001, 1, 1);
            var d2 = new DateTime(2002, 2, 2);

            Assert.AreEqual(R.Max(d1, d2), d2);
            Assert.AreEqual(R.Max(d2, d1), d2);
            Assert.AreEqual(R.Max('a', 'b'), 'b');
            Assert.AreEqual(R.Max('b', 'a'), 'b');
            Assert.AreEqual(R.Max("a", "b"), "b");
            Assert.AreEqual(R.Max("b", "a"), "b");
        }
    }
}
