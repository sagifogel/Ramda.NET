using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MapAccum
    {
        private readonly Func<dynamic, dynamic, Tuple<object, object>> add = (a, b) => R.Tuple.Create(a + b, a + b);
        private readonly Func<int, int, Tuple<object, object>> mult = (a, b) => R.Tuple.Create(a * b, a * b);
        private readonly Func<int[], int[], Tuple<object, object>> concat = (a, b) => R.Tuple.Create(a.Concat(b), a.Concat(b));

        [TestMethod]
        public void MapAccum_Map_And_Accumulate_Simple_Functions_Over_Arrays_With_The_Supplied_Accumulator() {
            Tuple<object, IList> tuple1 = R.MapAccum(add, 0, new[] { 1, 2, 3, 4 });
            Tuple<object, IList> tuple2 = R.MapAccum(mult, 1, new[] { 1, 2, 3, 4 });

            Assert.AreEqual(tuple1.Item1, 10);
            Assert.AreEqual(tuple2.Item1, 24);
            CollectionAssert.AreEqual(tuple1.Item2, new[] { 1, 3, 6, 10 });
            CollectionAssert.AreEqual(tuple2.Item2, new[] { 1, 2, 6, 24 });
        }

        [TestMethod]
        public void MapAccum_Returns_The_List_And_Accumulator_For_An_Empty_Array() {
            Tuple<object, IList> tuple1 = R.MapAccum(add, 0, new int[0]);
            Tuple<object, IList> tuple2 = R.MapAccum(mult, 1, new int[0]);
            Tuple<object, IList> tuple3 = R.MapAccum(concat, new int[0], new int[0]);

            Assert.AreEqual(tuple1.Item1, 0);
            Assert.AreEqual(tuple2.Item1, 1);
            CollectionAssert.AreEqual((IList)tuple3.Item1, new object[0]);
            CollectionAssert.AreEqual(tuple1.Item2, new object[0]);
            CollectionAssert.AreEqual(tuple2.Item2, new object[0]);
            CollectionAssert.AreEqual(tuple3.Item2, new object[0]);
        }

        [TestMethod]
        public void MapAccum_Is_Curried() {
            var addOrConcat = R.MapAccum(add);
            var sum = addOrConcat(0);
            var cat = addOrConcat(string.Empty);
            Tuple<object, IList> summedTuple = sum(new[] { 1, 2, 3, 4 });
            Tuple<object, IList> catTuple = cat(new[] { "1", "2", "3", "4" });

            Assert.AreEqual(summedTuple.Item1, 10);
            Assert.AreEqual(catTuple.Item1, "1234");
            CollectionAssert.AreEqual(summedTuple.Item2, new[] { 1, 3, 6, 10 });
            CollectionAssert.AreEqual(catTuple.Item2, new[] { "1", "12", "123", "1234" });
        }

        [TestMethod]
        public void MapAccum_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var sum = R.MapAccum(add, 0);

            Assert.AreEqual(sum.Length, 1);
        }
    }
}
