using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Descend
    {
        [TestMethod]
        public void Descend_Builds_An_Descending_Comparator_Function_Out_Of_The_Identity_Function() {
            var ascend = R.Descend(R.Identity(R.__));
            var array = new[] { 3, 1, 8, 1, 2, 5 };

            CollectionAssert.AreEqual(array.Sort(new Comparison<int>((int a, int b) => ascend(a, b))), new[] { 8, 5, 3, 2, 1, 1 });
        }
    }
}
