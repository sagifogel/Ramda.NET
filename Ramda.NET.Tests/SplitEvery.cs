using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SplitEvery
    {
        [TestMethod]
        public void SplitEvery_Splits_A_Collection_Into_Slices_Of_The_Specified_Length() {
            NestedCollectionAssert.AreEqual(R.SplitEvery(1, new[] { 1, 2, 3, 4 }), new object[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 } });
            NestedCollectionAssert.AreEqual(R.SplitEvery(2, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2 }, new[] { 3, 4 } });
            NestedCollectionAssert.AreEqual(R.SplitEvery(3, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2, 3 }, new[] { 4 } });
            NestedCollectionAssert.AreEqual(R.SplitEvery(4, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2, 3, 4 } });
            NestedCollectionAssert.AreEqual(R.SplitEvery(5, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2, 3, 4 } });
            CollectionAssert.AreEqual(R.SplitEvery(3, new int[0]), new int[0]);
            CollectionAssert.AreEqual(R.SplitEvery(1, "abcd"), new[] { "a", "b", "c", "d" });
            CollectionAssert.AreEqual(R.SplitEvery(2, "abcd"), new[] { "ab", "cd" });
            CollectionAssert.AreEqual(R.SplitEvery(3, "abcd"), new[] { "abc", "d" });
            CollectionAssert.AreEqual(R.SplitEvery(4, "abcd"), new[] { "abcd" });
            CollectionAssert.AreEqual(R.SplitEvery(5, "abcd"), new[] { "abcd" });
            CollectionAssert.AreEqual(R.SplitEvery(3, string.Empty), new string[0]);
        }

        [TestMethod]
        public void SplitEvery_Throws_If_First_Argument_Is_Not_Positive() {
            Assert.IsTrue(Run(0, new object[0]));
            Assert.IsTrue(Run(0, string.Empty));
            Assert.IsTrue(Run(-1, new object[0]));
            Assert.IsTrue(Run(-1, string.Empty));
        }

        private bool Run(int n, IEnumerable list) {
            try {
                R.SplitEvery(0, new object[0]);
            }
            catch (ArgumentOutOfRangeException ex) {
                return ex.Message.Equals("First argument to splitEvery must be a positive integer\r\nParameter name: n");
            }

            return false;
        }
    }
}
