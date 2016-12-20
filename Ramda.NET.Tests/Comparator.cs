using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Comparator
    {
        [TestMethod]
        public void Comparator_Builds_A_Comparator_Function_For_Sorting_Out_Of_A_Simple_Predicate_That_Reports_Whether_The_First_Param_Is_Smaller() {
            var arr = new[] { 3, 1, 8, 1, 2, 5 };
            var comparer = R.Comparator(new Func<int, int, bool>((a, b) => a < b));

            CollectionAssert.AreEqual(arr.Sort<int>(new Comparison<int>((x, y) => comparer(x, y))), new[] { 1, 1, 2, 3, 5, 8 });
        }
    }
}
