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

        [TestMethod]
        [Description("CurryN_Supports_R.___Placeholder")]
        public void CurryN_Supports_R___Placeholder() {
            var _ = R.__;
            var f = new Func<int, int, int, int[]>((a, b, c) => new[] { a, b, c });
            var g = R.CurryN(3, f);
            var arr = new[] { 1, 2, 3 };

            CollectionAssert.AreEqual(g(1)(2)(3), arr);
            CollectionAssert.AreEqual(g(1)(2, 3), arr);
            CollectionAssert.AreEqual(g(1, 2)(3), arr);
            CollectionAssert.AreEqual(g(1, 2, 3), arr);

            CollectionAssert.AreEqual(g(_, 2, 3)(1), arr);
            CollectionAssert.AreEqual(g(1, _, 3)(2), arr);
            CollectionAssert.AreEqual(g(1, 2, _)(3), arr);

            CollectionAssert.AreEqual(g(1, _, _)(2)(3), arr);
            CollectionAssert.AreEqual(g(_, 2, _)(1)(3), arr);
            CollectionAssert.AreEqual(g(_, _, 3)(1)(2), arr);

            CollectionAssert.AreEqual(g(1, _, _)(2, 3), arr);
            CollectionAssert.AreEqual(g(_, 2, _)(1, 3), arr);
            CollectionAssert.AreEqual(g(_, _, 3)(1, 2), arr);

            CollectionAssert.AreEqual(g(1, _, _)(_, 3)(2), arr);
            CollectionAssert.AreEqual(g(_, 2, _)(_, 3)(1), arr);
            CollectionAssert.AreEqual(g(_, _, 3)(_, 2)(1), arr);

            CollectionAssert.AreEqual(g(_, _, _)(_, _)(_)(1, 2, 3), arr);
            CollectionAssert.AreEqual(g(_, _, _)(1, _, _)(_, _)(2, _)(_)(3), arr);
        }
    }
}
