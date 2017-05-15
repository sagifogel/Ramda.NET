using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LastIndexOf : BaseIndexOf
    {
        private readonly int[] input = new[] { 1, 2, 3, 4, 5, 1 };
        private readonly object[] list = new object[] { "a", 1, "a" };

        [TestMethod]
        [Description("LastIndexOf_Returns_A_Number_Indicating_An_Object's_Last_Position_In_A_List")]
        public void LastIndexOf_Returns_A_Number_Indicating_An_Objects_Last_Position_In_A_List() {
            var list = new[] { 0, 10, 20, 30, 0, 10, 20, 30, 0, 10 };

            Assert.AreEqual(R.LastIndexOf(30, list), 7);
        }

        [TestMethod]
        [Description("LastIndexOf_Returns_-1_If_The_Object_Is_Not_In_The_List")]
        public void LastIndexOf_Returns_Minus_1_If_The_Object_Is_Not_In_The_List() {
            var list = new[] { 0, 10, 20, 30 };

            Assert.AreEqual(R.LastIndexOf(40, list), -1);
        }

        [TestMethod]
        public void LastIndexOf_Returns_The_Last_Index_Of_The_First_Item() {
            var list = new[] { 0, 10, 20, 30 };

            Assert.AreEqual(R.LastIndexOf(1, input), 5);
        }

        [TestMethod]
        public void LastIndexOf_Returns_The_Index_Of_The_Last_Item() {
            var list = new[] { 0, 10, 20, 30 };

            Assert.AreEqual(R.LastIndexOf(5, input), 4);
        }

        [TestMethod]
        public void LastIndexOf_Finds_A() {
            Assert.AreEqual(R.LastIndexOf("a", list), 2);
        }

        [TestMethod]
        public void LastIndexOf_Does_Not_Find_C() {
            Assert.AreEqual(R.LastIndexOf("c", list), -1);
        }

        [TestMethod]
        [Description("LastIndexOf_Does_Not_Consider_\"1\"_Equal_To_1")]
        public void LastIndexOf_Does_Not_Consider_String_Of_1_Equal_To_1() {
            Assert.AreEqual(R.LastIndexOf("1", list), -1);
        }

        [TestMethod]
        [Description("LastIndexOf_Returns_-1_For_An_Empty_Array")]
        public void LastIndexOf_Returns_Minus_1_For_An_Empty_Array() {
            Assert.AreEqual(R.LastIndexOf("x", new object[0]), -1);
        }

        [TestMethod]
        [Description("LastIndexOf_Has_R.Equals_Semantics")]
        public void LastIndexOf_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.LastIndexOf(0, new[] { -0 }), 0);
            Assert.AreEqual(R.LastIndexOf(-0, new[] { 0 }), 0);
            Assert.AreEqual(R.LastIndexOf(R.@null, new[] { R.@null }), 0);
            Assert.AreEqual(R.LastIndexOf(new Just(new[] { 42 }), new[] { new Just(new[] { 42 }) }), 0);
        }

        [TestMethod]
        [Description("LastIndexOf_Dispatches_To_\"LastIndexOf\"_Method")]
        public void LastIndexOf_Dispatches_To_indexOf_Method() {
            var list = new List("b",
              new List("a",
              new List("n",
              new List("a",
              new List("n",
              new List("a",
              new Empty()))))));

            Assert.AreEqual(R.LastIndexOf("a", "banana"), 5);
            Assert.AreEqual(R.LastIndexOf("x", "banana"), -1);
            Assert.AreEqual(R.LastIndexOf("a", list), 5);
            Assert.AreEqual(R.LastIndexOf("x", list), -1);
        }

        [TestMethod]
        public void LastIndexOf_Is_Curried() {
            var curried = R.LastIndexOf("a");

            Assert.AreEqual(curried(list), 2);
        }

        [TestMethod]
        [Description("LastIndexOf_Finds_Function,_Compared_By_Identity")]
        public void LastIndexOf_Finds_Function_Compared_By_Identity() {
            var f = new Action(() => { });
            var g = new Action(() => { });
            var list = new[] { g, f, g, f };

            Assert.AreEqual(R.LastIndexOf(f, list), 3);
        }

        [TestMethod]
        [Description("LastIndexOf_Does_Not_Find_Function,_Compared_By_Identity")]
        public void LastIndexOf_Does_Not_Find_Function_Compared_By_Identity() {
            var f = new Action(() => { });
            var g = new Action(() => { });
            var h = new Action(() => { });
            var list = new[] { g, f };

            Assert.AreEqual(R.IndexOf(h, list), -1);
        }
    }
}
