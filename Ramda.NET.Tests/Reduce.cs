using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Ramda.NET.R;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Reduce
    {
        private readonly Func<dynamic, dynamic, dynamic> add = (a, b) => (dynamic)a + (dynamic)b;
        private readonly Func<int, int, int> mult = (a, b) => a * b;

        class Reduicable : IReducible
        {
            public int[] X => new[] { 1, 2, 3 };

            public object Reduce(Func<object, object, object> step, object acc) {
                return "override";
            }
        }

        [TestMethod]
        public void Reduce_Folds_Simple_Functions_Over_Arrays_With_The_Supplied_Accumulator() {
            Assert.AreEqual(R.Reduce<object, object, object>(add, 0, new object[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(R.Reduce(mult, 1, new[] { 1, 2, 3, 4 }), 24);
        }

        [TestMethod]
        [Description("Reduce_Dispatches_To_Objects_That_Implement_\"Reduce\"")]
        public void Reduce_Dispatches_To_Objects_That_Implement_Reduce() {
            var obj = new Reduicable();

            Assert.AreEqual(R.Reduce<object, object, object>(add, 0, obj), "override");
            Assert.AreEqual(R.Reduce<object, object, object>(add, 10, obj), "override");
        }

        [TestMethod]
        public void Reduce_Returns_The_Accumulator_For_An_Empty_Array() {
            Assert.AreEqual(R.Reduce<object, object, object>(add, 0, new object[0]), 0);
            Assert.AreEqual(R.Reduce(mult, 1, new int[0]), 1);
            CollectionAssert.AreEqual(R.Reduce(R.Concat(R.__), new int[0], new int[0]), new int[0]);
        }

        [TestMethod]
        public void Reduce_Is_Curried() {
            var addOrConcat = R.Reduce(add);
            var sum = addOrConcat(0);
            var cat = addOrConcat(string.Empty);

            Assert.AreEqual(sum(new[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(cat(new[] { "1", "2", "3", "4" }), "1234");
        }

        [TestMethod]
        public void Reduce_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var sum = R.Reduce<object, object, object>(add, 0);

            Assert.AreEqual(sum.Length, 1);
        }
    }
}
