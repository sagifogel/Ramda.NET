using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Sort
    {
        [TestMethod]
        public void Sort_Sorts_The_Elements_Of_A_List() {
            CollectionAssert.AreEqual(R.Sort((a, b) => a - b, new[] { 3, 1, 8, 1, 2, 5 }), new[] { 1, 1, 2, 3, 5, 8 });
        }

        [TestMethod]
        public void Sort_Does_Not_Affect_The_List_Passed_Supplied() {
            var list = new[] { 3, 1, 8, 1, 2, 5 };

            CollectionAssert.AreEqual(R.Sort((a, b) => a - b, list), new[] { 1, 1, 2, 3, 5, 8 });
            CollectionAssert.AreEqual(list, new[] { 3, 1, 8, 1, 2, 5 });
        }

        [TestMethod]
        public void Sort_Is_Curried() {
            var sortByLength = R.Sort<string>((a, b) => a.Length - b.Length);

            CollectionAssert.AreEqual(sortByLength(new[] { "one", "two", "three", "four", "five", "six" }), new[] { "one", "two", "six", "four", "five", "three" });
        }
    }
}
