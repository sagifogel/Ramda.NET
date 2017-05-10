using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MapObjIndexed
    {
        private readonly Func<int, int> times2 = x => x * 2;
        private readonly Func<int, string, string> addIndexed = (x, key) => x + key;
        private readonly object input = new { A = 8, B = 6, C = 7, D = 5, E = 3, F = 0, G = 9 };
        private readonly object squaredVowels = new { A = 64, B = 6, C = 7, D = 5, E = 9, F = 0, G = 9 };
        private readonly Func<int, string, int> squareVowels = (x, key) => R.Contains(key, new[] { "A", "E", "I", "O", "U" }) ? x * x : x;

        [TestMethod]
        public void MapObjIndexed_Works_Just_Like_A_Normal_MapObj() {
            DynamicAssert.AreEqual(R.MapObjIndexed(times2, new { A = 1, B = 2, C = 3, D = 4 }), new { A = 2, B = 4, C = 6, D = 8 });
        }

        [TestMethod]
        public void MapObjIndexed_Passes_The_Index_As_A_Second_Parameter_To_The_Callback() {
            var expected = new { A = "8A", B = "6B", C = "7C", D = "5D", E = "3E", F = "0F", G = "9G" };

            DynamicAssert.AreEqual(R.MapObjIndexed(addIndexed, input), expected);
        }

        [TestMethod]
        public void MapObjIndexed_Passes_The_Entire_List_As_A_Third_Parameter_To_The_Callback() {
            DynamicAssert.AreEqual(R.MapObjIndexed(squareVowels, input), squaredVowels);
        }

        [TestMethod]
        public void MapObjIndexed_Is_Curried() {
            var makeSquareVowels = R.MapObjIndexed(squareVowels);

            DynamicAssert.AreEqual(makeSquareVowels(input), squaredVowels);
        }
    }
}
