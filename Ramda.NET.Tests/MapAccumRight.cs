using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MapAccumRight
    {
        private readonly Func<dynamic, dynamic, Tuple<object, object>> add = (a, b) => R.Tuple.Create(a + b, a + b);
        private readonly Func<int, int, Tuple<object, object>> mult = (a, b) => R.Tuple.Create(a * b, a * b);

        [TestMethod]
        public void MapAccumRight_Map_And_Accumulate_Simple_Functions_Over_Arrays_With_The_Supplied_Accumulator() {
            Tuple<IList, object> tuple1 = R.MapAccumRight(add, 0, new[] { 1, 2, 3, 4 });
            Tuple<IList, object> tuple2 = R.MapAccumRight(mult, 1, new[] { 1, 2, 3, 4 });

            Assert.AreEqual(tuple1.Item2, 10);
            Assert.AreEqual(tuple2.Item2, 24);
            CollectionAssert.AreEqual(tuple1.Item1, new[] { 10, 9, 7, 4 });
            CollectionAssert.AreEqual(tuple2.Item1, new[] { 24, 24, 12, 4 });
        }

        [TestMethod]
        public void MapAccumRight_Returns_The_List_And_Accumulator_For_An_Empty_Array() {
            Tuple<IList, object> tuple1 = R.MapAccumRight(add, 0, new int[0]);
            Tuple<IList, object> tuple2 = R.MapAccumRight(mult, 1, new int[0]);

            Assert.AreEqual(tuple1.Item2, 0);
            Assert.AreEqual(tuple2.Item2, 1);
            CollectionAssert.AreEqual(tuple1.Item1, new object[0]);
            CollectionAssert.AreEqual(tuple2.Item1, new object[0]);
        }

        [TestMethod]
        public void MapAccumRight_Is_Curried() {
            var addOrConcat = R.MapAccumRight(add);
            var sum = addOrConcat(0);
            var cat = addOrConcat(string.Empty);
            Tuple<IList, object> summedTuple = sum(new[] { 1, 2, 3, 4 });
            Tuple<IList, object> catTuple = cat(new[] { "1", "2", "3", "4" });

            Assert.AreEqual(summedTuple.Item2, 10);
            Assert.AreEqual(catTuple.Item2, "1234");
            CollectionAssert.AreEqual(summedTuple.Item1, new[] { 10, 9, 7, 4 });
            CollectionAssert.AreEqual(catTuple.Item1, new[] { "1234", "234", "34", "4" });
        }

        [TestMethod]
        public void MapAccumRight_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var sum = R.MapAccumRight(add, 0);

            Assert.AreEqual(sum.Length, 1);
        }
    }
}
