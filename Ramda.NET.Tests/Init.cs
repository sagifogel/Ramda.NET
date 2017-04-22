using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Init
    {
        [TestMethod]
        public void Init_Returns_All_But_The_Last_Element_Of_An_Ordered_Collection() {
            CollectionAssert.AreEqual(R.Init(new[] { 1, 2, 3 }), new[] { 1, 2 });
            CollectionAssert.AreEqual(R.Init(new[] { 2, 3 }), new[] { 2 });
            CollectionAssert.AreEqual(R.Init(new[] { 3 }), new int[0]);
            CollectionAssert.AreEqual(R.Init(new object[0]), new object[0]);
            Assert.AreEqual(R.Init("abc"), "ab");
            Assert.AreEqual(R.Init("bc"), "b");
            Assert.AreEqual(R.Init("c"), "");
            Assert.AreEqual(R.Init(""), "");
        }

        [TestMethod]
        [Description("Init_Handles_Array-Like_Object")]
        public void Init_Handles_Lists() {
            CollectionAssert.AreEqual(R.Init(new List<int> { 1, 2, 3 }), new[] { 1, 2 });
        }
    }
}
