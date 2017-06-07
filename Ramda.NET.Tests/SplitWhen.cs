using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SplitWhen
    {
        [TestMethod]
        public void SplitWhen_Splits_An_Array_At_The_First_Instance_To_Satisfy_The_Predicate() {
            NestedCollectionAssert.AreEqual(R.SplitWhen(R.Equals(2), new[] { 1, 2, 3 }), new object[] { new[] { 1 }, new object[] { 2, 3 } });
        }

        [TestMethod]
        public void SplitWhen_Retains_All_Original_Elements() {
            NestedCollectionAssert.AreEqual(R.SplitWhen(R.T, new[] { 1, 1, 1 }), new object[] { new int[0], new[] { 1, 1, 1 } });
        }

        [TestMethod]
        public void SplitWhen_Is_Curried() {
            var splitWhenFoo = R.SplitWhen(R.Equals("foo"));

            NestedCollectionAssert.AreEqual(splitWhenFoo(new[] { "foo", "bar", "baz" }), new object[] { new string[0], new[] { "foo", "bar", "baz" } });
        }

        [TestMethod]
        public void SplitWhen_Only_Splits_Once() {
            NestedCollectionAssert.AreEqual(R.SplitWhen(R.Equals(2), new[] { 1, 2, 3, 1, 2, 3 }), new[] { new[] { 1 }, new[] { 2, 3, 1, 2, 3 } });
        }
    }
}
