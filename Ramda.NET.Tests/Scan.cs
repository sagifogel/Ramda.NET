using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Scan
    {
        private readonly Func<int, int, int> mult = (a, b) => a * b;
        private readonly Func<dynamic, dynamic, dynamic> add = (a, b) => a + b;

        [TestMethod]
        public void Scan_Scans_Simple_Functions_Over_Arrays_With_The_Supplied_Accumulator() {
            CollectionAssert.AreEqual(R.Scan(add, 0, new[] { 1, 2, 3, 4 }), new[] { 0, 1, 3, 6, 10 });
            CollectionAssert.AreEqual(R.Scan(mult, 1, new[] { 1, 2, 3, 4 }), new[] { 1, 1, 2, 6, 24 });
        }

        [TestMethod]
        public void Scan_Returns_The_Accumulator_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.Scan(add, 0, new int[0]), new[] { 0 });
            CollectionAssert.AreEqual(R.Scan(mult, 1, new int[0]), new[] { 1 });
        }

        [TestMethod]
        public void Scan_Is_Curried() {
            var addOrConcat = R.Scan(add);
            var sum = addOrConcat(0);
            var cat = addOrConcat(string.Empty);

            CollectionAssert.AreEqual(sum(new[] { 1, 2, 3, 4 }), new[] { 0, 1, 3, 6, 10 });
            CollectionAssert.AreEqual(cat(new[] { "1", "2", "3", "4" }), new[] { "", "1", "12", "123", "1234" });
        }

        [TestMethod]
        public void Scan_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var sum = R.Scan<dynamic, dynamic, dynamic>(add, 0);

            Assert.AreEqual(sum.Length, 1);
        }
    }
}
