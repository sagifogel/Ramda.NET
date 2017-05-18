using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PartialRight
    {
        private Func<int, int, int, int> disc = (a, b, c) => b * b - 4 * a * c;

        [TestMethod]
        public void PartialRight_Caches_The_Initially_Supplied_Arguments() {
            var f = R.PartialRight(disc, new[] { 4 });
            var g = R.PartialRight(disc, new[] { 7, 4 });

            Assert.AreEqual(f(3, 7), 1);
            Assert.AreEqual(g(3), 1);
        }

        [TestMethod]
        public void PartialRight_Correctly_Reports_The_Arity_Of_The_New_Function() {
            var f = R.PartialRight(disc, new[] { 4 });
            var g = R.PartialRight(disc, new[] { 7, 4 });

            Assert.AreEqual(f.Length, 2);
            Assert.AreEqual(g.Length, 1);
        }
    }
}
