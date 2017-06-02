using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Reverse
    {
        [TestMethod]
        public void Reverse_Reverses_Arrays() {
            CollectionAssert.AreEqual(R.Reverse(new int[0]), new int[0]);
            CollectionAssert.AreEqual(R.Reverse(new int[] { 1 }), new int[] { 1 });
            CollectionAssert.AreEqual(R.Reverse(new int[] { 1, 2 }), new int[] { 2, 1 });
            CollectionAssert.AreEqual(R.Reverse(new int[] { 1, 2, 3 }), new int[] { 3, 2, 1 });
        }

        [TestMethod]
        public void Reverse_Reverses_Strings() {
            Assert.AreEqual(R.Reverse(string.Empty), string.Empty);
            Assert.AreEqual(R.Reverse("a"), "a");
            Assert.AreEqual(R.Reverse("ab"), "ba");
            Assert.AreEqual(R.Reverse("abc"), "cba");
        }
    }
}
