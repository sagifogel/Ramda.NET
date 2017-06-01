using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ReduceRight
    {
        private readonly Func<int, int, int> avg = (a, b) => (a + b) / 2;

        [TestMethod]
        public void ReduceRight_Folds_Lists_In_The_Right_Order() {
            Assert.AreEqual(R.ReduceRight((string a, string b) => a + b, string.Empty, new[] { "a", "b", "c", "d" }), "abcd");
        }

        [TestMethod]
        public void ReduceRight_Folds_Subtract_Over_Arrays_In_The_Right_Order() {
            Assert.AreEqual(R.ReduceRight((int a, int b) => a - b, 0, new[] { 1, 2, 3, 4 }), -2);
        }

        [TestMethod]
        public void ReduceRight_Folds_Simple_Functions_Over_Arrays_With_The_Supplied_Accumulator() {
            Assert.AreEqual(R.ReduceRight(avg, 54, new[] { 12, 4, 10, 6 }), 12);
        }

        [TestMethod]
        public void ReduceRight_Returns_The_Accumulator_For_An_Empty_Array() {
            Assert.AreEqual(R.ReduceRight(avg, 0, new int[0]), 0);
        }

        [TestMethod]
        public void ReduceRight_Is_Curried() {
            var something = R.ReduceRight(avg, 54);
            var rcat = R.ReduceRight(R.Concat(R.__), string.Empty);

            Assert.AreEqual(something(new[] { 12, 4, 10, 6 }), 12);
            Assert.AreEqual(rcat(new[] { "1", "2", "3", "4" }), "1234");
        }

        [TestMethod]
        public void ReduceRight_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var something = R.ReduceRight(avg, 0);

            Assert.AreEqual(something.Length, 1);
        }
    }
}
