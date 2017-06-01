using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ReduceWhile
    {
        private readonly Func<int, int, bool> isOdd = (_, x) => x % 2 == 1;

        [TestMethod]
        public void ReduceWhile_Reduces_Until_Its_Predicate_Fails() {
            Assert.AreEqual(R.ReduceWhile(isOdd, R.Add(R.__), 0, new[] { 1, 3, 1, 5, 20, 7, 7, 7 }), 10);
        }

        [TestMethod]
        public void ReduceWhile_Returns_Its_Accumulator_When_Given_An_Empty_Array() {
            Assert.AreEqual(R.ReduceWhile(isOdd, R.Add(R.__), 101, new int[0]), 101);
        }

        [TestMethod]
        public void ReduceWhile_Is_Curried() {
            var reduceWhileOdd = R.ReduceWhile(isOdd);

            Assert.AreEqual(reduceWhileOdd(R.Add(R.__), 101, new int[0]), 101);
            Assert.AreEqual(reduceWhileOdd(R.Add(R.__), 0, new[] { 1, 2, 3, 4 }), 1);
        }

        [TestMethod]
        public void ReduceWhile_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var reduceWhileOdd = R.ReduceWhile(isOdd);

            Assert.AreEqual(reduceWhileOdd.Length, 3);
        }
    }
}
