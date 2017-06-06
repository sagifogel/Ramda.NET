using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Ascend
    {
        [TestMethod]
        public void Ascend_Builds_An_Ascending_Comparator_Function_Out_Of_The_Identity_Function() {
            var ascend = R.Ascend(R.Identity(R.__));
            var array = new[] { 3, 1, 8, 1, 2, 5 };

            CollectionAssert.AreEqual(array.Sort(new Comparison<int>((int a, int b) => ascend(a, b))), new[] { 1, 1, 2, 3, 5, 8 });
        }
    }
}
