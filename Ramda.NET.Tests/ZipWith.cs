using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ZipWith
    {
        private readonly int[] a = new[] { 1, 2, 3 };
        private readonly int[] b = new[] { 100, 200, 300 };
        private readonly int[] c = new[] { 10, 20, 30, 40, 50, 60 };
        private readonly Func<int, int, int> x = (a, b) => a * b;
        private readonly Func<int, int, int> add = (a, b) => a + b;
        private readonly Func<int, int, string> s = (a, b) => $"{a} cow {b}";

        [TestMethod]
        [Description("ZipWith_Returns_An_Array_Created_By_Applying_Its_Passed-In_Function_Pair-Wise_On_Its_Passed_In_Arrays")]
        public void ZipWith_Returns_An_Array_Created_By_Applying_Its_Passed_In_Function_Pair_Wise_On_Its_Passed_In_Arrays() {
            CollectionAssert.AreEqual(R.ZipWith(add, a, b), new[] { 101, 202, 303 });
            CollectionAssert.AreEqual(R.ZipWith(x, a, b), new[] { 100, 400, 900 });
            CollectionAssert.AreEqual(R.ZipWith(s, a, b), new[] { "1 cow 100", "2 cow 200", "3 cow 300" });
        }

        [TestMethod]
        public void ZipWith_Returns_An_Array_Whose_Length_Is_Equal_To_The_Shorter_Of_Its_Input_Arrays() {
            Assert.AreEqual(R.ZipWith(add, a, c).Length, a.Length);
        }
    }
}
