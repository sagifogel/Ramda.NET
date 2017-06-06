using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SplitAt
    {
        [TestMethod]
        public void SplitAt_Splits_An_Array_At_A_Given_Index() {
            NestedCollectionAssert.AreEqual(R.SplitAt(1, new[] { 1, 2, 3 }), new object[] { new[] { 1 }, new[] { 2, 3 } });
        }

        [TestMethod]
        public void SplitAt_Splits_A_String_At_A_Given_Index() {
            CollectionAssert.AreEqual(R.SplitAt(5, "hello world"), new[] { "hello", " world" });
        }

        [TestMethod]
        public void SplitAt_Is_Curried() {
            var splitAtThree = R.SplitAt(3);

            CollectionAssert.AreEqual(splitAtThree("foobar"), new[] { "foo", "bar" });
        }

        [TestMethod]
        public void SplitAt_Can_Handle_Index_Greater_Than_Array_Length() {
            NestedCollectionAssert.AreEqual(R.SplitAt(4, new[] { 1, 2 }), new object[] { new[] { 1, 2 }, new int[0] });
        }

        [TestMethod]
        public void SplitAt_Can_Support_Negative_Index() {
            CollectionAssert.AreEqual(R.SplitAt(-1, "foobar"), new[] { "fooba", "r" });
        }
    }
}
