using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Curry
    {
        [TestMethod]
        public void Curry_Curries_A_Single_Value() {
            var f = R.Curry(new Func<int, int, int, int, int>((a, b, c, d) => (a + b * c) / d));
            var g = f(12);

            Assert.AreEqual(g(3, 6, 2), 15);
        }

        [TestMethod]
        public void Curry_Curries_Multiple_Values() {
            var f = R.Curry(new Func<int, int, int, int, int>((a, b, c, d) => (a + b * c) / d));
            var g = f(12, 3);
            var h = f(12, 3, 6);

            Assert.AreEqual(g(6, 2), 15);
            Assert.AreEqual(h(2), 15);
        }

        [TestMethod]
        public void Curry_Allows_Further_Currying_Of_A_Curried_Function() {
            var f = R.Curry(new Func<int, int, int, int, int>((a, b, c, d) => (a + b * c) / d));
            var g = f(12);
            var h = g(3);

            Assert.AreEqual(g(3, 6, 2), 15);
            Assert.AreEqual(h(6, 2), 15);
            Assert.AreEqual(g(3, 6)(2), 15);
        }

        [TestMethod]
        public void Curry_Properly_Reports_The_Length_Of_The_Curried_Function() {
            var f = R.Curry(new Func<int, int, int, int, int>((a, b, c, d) => (a + b * c) / d));
            var g = f(12);
            var h = g(3);

            Assert.AreEqual(f.Length, 4);
            Assert.AreEqual(g.Length, 3);
            Assert.AreEqual(h.Length, 2);
            Assert.AreEqual(g(3, 6).Length, 1);
        }

        [TestMethod]
        [Description("Curry_Supports_R.___Placeholder")]
        public void Curry_Supports_R___Placeholder() {
            var _ = R.__;
            var f = new Func<int, int, int, int[]>((a, b, c) => new[] { a, b, c });
            var g = R.Curry(f);
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
