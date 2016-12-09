using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class AddIndex
    {
        private Func<int, int> times2 = x => x * 2;
        private dynamic mapIndexed = R.AddIndex(R.Map(R.__));
        private dynamic reduceIndexed = R.AddIndex(R.Reduce(R.__));
        private Func<int, int, int> addIndexParam = (x, idx) => x + idx;
        private Func<int, int, int, int> timesIndexed = (tot, num, idx) => tot + (num * idx);
        private Func<int, int, IList, int> squareEnds = (x, idx, list) => idx == 0 || idx == list.Count - 1 ? x * x : x;
        private Func<IDictionary<string, object>, string, int, IDictionary<string, object>> objectify = (acc, elem, idx) => {
            acc[elem] = idx;
            return acc;
        };

        [TestMethod]
        public void AddIndex_Unary_Functions_Like_Map_Works_Just_Like_A_Normal_Map() {
            CollectionAssert.AreEqual((ICollection)mapIndexed(times2, new[] { 1, 2, 3, 4 }), new[] { 2, 4, 6, 8 });
        }

        [TestMethod]
        public void AddIndex_Unary_Functions_Like_Map_Passes_The_Index_As_A_Second_Parameter_To_The_Callback() {
            CollectionAssert.AreEqual((ICollection)mapIndexed(addIndexParam, new[] { 8, 6, 7, 5, 3, 0, 9 }), new[] { 8, 7, 9, 8, 7, 5, 15 });
        }

        [TestMethod]
        public void AddIndex_Unary_Functions_Like_Map_Passes_The_Entire_List_As_A_Third_Parameter_To_The_Callback() {
            CollectionAssert.AreEqual((ICollection)mapIndexed(squareEnds, new[] { 8, 6, 7, 5, 3, 0, 9 }), new[] { 64, 6, 7, 5, 3, 0, 81 });
        }

        [TestMethod]
        public void AddIndex_Unary_Functions_Like_Map_Acts_As_A_Curried_Function() {
            var makeSquareEnds = mapIndexed(squareEnds);

            CollectionAssert.AreEqual((ICollection)makeSquareEnds(new[] { 8, 6, 7, 5, 3, 0, 9 }), new[] { 64, 6, 7, 5, 3, 0, 81 });
        }

        [TestMethod]
        public void AddIndex_Binary_Functions_Like_Reduce_Passes_The_Index_As_A_Third_Parameter_To_The_Predicate() {
            ExpandoObject reduced = reduceIndexed(objectify, new ExpandoObject(), new[] { "a", "b", "c", "d", "e" });
            var expected = new { a = 0, b = 1, c = 2, d = 3, e = 4 }.ToExpando();

            Assert.AreEqual((int)reduceIndexed(timesIndexed, 0, new[] { 1, 2, 3, 4, 5 }), 40);
            Assert.IsTrue(reduced.ContentEquals(expected));
        }

        [TestMethod]
        public void AddIndex_Passes_The_Entire_List_As_A_Fourth_Parameter_To_The_Predicate() {
            var list = new[] { 1, 2, 3 };

            reduceIndexed(new Func<object, object, object, object, object>((acc, x, idx, ls) => {
                Assert.AreSame(ls, list);
                return acc;
            }), 0, list);
        }

        [TestMethod]
        public void AddIndex_Works_With_Functions_Like_All_That_Do_Not_Typically_Have_Index_Applied_Passes_The_Index_As_A_Second_Parameter() {
            var allIndexed = R.AddIndex(R.All(R.__));
            var superDiagonal = allIndexed(R.Gt(R.__));

            Assert.IsTrue((bool)superDiagonal(new[] { 8, 6, 5, 4, 9 }));
            Assert.IsFalse((bool)superDiagonal(new[] { 8, 6, 1, 3, 9 }));
        }

        [TestMethod]
        public void AddIndex_Works_With_Arbitrary_User_Defined_Functions_Passes_The_Index_As_An_Additional_Parameter() {
            var mapFilter = new Func<DynamicDelegate, DynamicDelegate, IList, IList>((m, f, list) => R.Filter(R.Compose(f, m), list));
            var mapFilterIndexed = R.AddIndex(mapFilter);

            CollectionAssert.AreEqual((ICollection)mapFilterIndexed(R.Multiply(R.__), R.Gt(R.__, 13), new[] { 8, 6, 7, 5, 3, 0, 9 }), new[] { 7, 5, 9 });
        }
    }
}
