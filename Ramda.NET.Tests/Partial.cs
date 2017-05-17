using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Partial
    {
        private Func<int, int, int, int> disc = (a, b, c) => b * b - 4 * a * c;

        [TestMethod]
        public void Partial_Caches_The_Initially_Supplied_Arguments() {
            var f = R.Partial(disc, new[] { 3 });
            var g = R.Partial(disc, new[] { 3, 7 });

            Assert.AreEqual(f(7, 4), 1);
            Assert.AreEqual(g(4), 1);
        }

        [TestMethod]
        public void Partial_Correctly_Reports_The_Arity_Of_The_New_Function() {
            var f = R.Partial(disc, new[] { 3 });
            var g = R.Partial(disc, new[] { 3, 7 });

            Assert.AreEqual(f.Length, 2);
            Assert.AreEqual(g.Length, 1);
        }
    }
}
