using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Slice
    {
        public object[] Arguments(params object[] args) => args;

        [TestMethod]
        public void Slice_Retrieves_The_Proper_Sublist_Of_A_List() {
            var list = new[] { 8, 6, 7, 5, 3, 0, 9 };

            CollectionAssert.AreEqual(R.Slice(2, 5, list), new[] { 7, 5, 3 });
        }

        [TestMethod]
        [Description("Slice_Handles_Array-Like_Object")]
        public void Slice_Handles_Array_Like_Object() {
            CollectionAssert.AreEqual(R.Slice(1, 4, Arguments(1, 2, 3, 4, 5)), new[] { 2, 3, 4 });
        }

        [TestMethod]
        public void Slice_Can_Operate_On_Strings() {
            Assert.AreEqual(R.Slice(0, 0, "abc"), "");
            Assert.AreEqual(R.Slice(0, 1, "abc"), "a");
            Assert.AreEqual(R.Slice(0, 2, "abc"), "ab");
            Assert.AreEqual(R.Slice(0, 3, "abc"), "abc");
            Assert.AreEqual(R.Slice(0, 4, "abc"), "abc");
            Assert.AreEqual(R.Slice(1, 0, "abc"), "");
            Assert.AreEqual(R.Slice(1, 1, "abc"), "");
            Assert.AreEqual(R.Slice(1, 2, "abc"), "b");
            Assert.AreEqual(R.Slice(1, 3, "abc"), "bc");
            Assert.AreEqual(R.Slice(1, 4, "abc"), "bc");
            Assert.AreEqual(R.Slice(0, -4, "abc"), "");
            Assert.AreEqual(R.Slice(0, -3, "abc"), "");
            Assert.AreEqual(R.Slice(0, -2, "abc"), "a");
            Assert.AreEqual(R.Slice(0, -1, "abc"), "ab");
            Assert.AreEqual(R.Slice(0, -0, "abc"), "");
            Assert.AreEqual(R.Slice(-2, -4, "abc"), "");
            Assert.AreEqual(R.Slice(-2, -3, "abc"), "");
            Assert.AreEqual(R.Slice(-2, -2, "abc"), "");
            Assert.AreEqual(R.Slice(-2, -1, "abc"), "b");
            Assert.AreEqual(R.Slice(-2, -0, "abc"), "");
        }
    }
}
