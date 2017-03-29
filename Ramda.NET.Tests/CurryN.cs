using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class CurryN
    {
        public int Source(int a, int b, int c, int d) {
            return a * b * c;
        }

        [TestMethod]
        public void CurryN_Accepts_An_Arity() {
            var curried = R.CurryN(3, new Func<int, int, int, int, int>(Source));

            Assert.AreEqual(curried(1)(2)(3), 6);
            Assert.AreEqual(curried(1, 2)(3), 6);
            Assert.AreEqual(curried(1)(2, 3), 6);
            Assert.AreEqual(curried(1, 2, 3), 6);
        }

        [TestMethod]
        public void CurryN_Can_Be_Partially_Applied() {
            var curry3 = R.CurryN(3);
            var curried = curry3(new Func<int, int, int, int, int>(Source));

            Assert.AreEqual(curried.Length, 3);
            Assert.AreEqual(curried(1)(2)(3), 6);
            Assert.AreEqual(curried(1, 2)(3), 6);
            Assert.AreEqual(curried(1)(2, 3), 6);
            Assert.AreEqual(curried(1, 2, 3), 6);
        }
    }
}
