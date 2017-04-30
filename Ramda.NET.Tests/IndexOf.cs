using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IndexOf : BaseIndexOf
    {
        private readonly int[] list2 = new[] { 1, 2, 3 };
        private readonly int[] list = new[] { 0, 10, 20, 30 };
        private readonly int[] input = new[] { 1, 2, 3, 4, 5 };

        [TestMethod]
        [Description("IndexOf_Returns_A_Number_Indicating_An_Object\"s_Position_In_A_List")]
        public void IndexOf_Returns_A_Number_Indicating_An_Objects_Position_In_A_List() {
            Assert.AreEqual(R.IndexOf(30, list), 3);
        }

        [TestMethod]
        [Description("IndexOf_Returns_-1_If_The_Object_Is_Not_In_The_List")]
        public void IndexOf_Returns_Minus_One_If_The_Object_Is_Not_In_The_List() {
            Assert.AreEqual(R.IndexOf(40, list), -1);
        }

        [TestMethod]
        public void IndexOf_Returns_The_Index_Of_The_First_Item() {
            Assert.AreEqual(R.IndexOf(1, input), 0);
        }

        [TestMethod]
        public void IndexOf_Returns_The_Index_Of_The_Last_Item() {
            Assert.AreEqual(R.IndexOf(5, input), 4);
        }

        [TestMethod]
        public void IndexOf_Finds_1() {
            Assert.AreEqual(R.IndexOf(1, list2), 0);
        }

        [TestMethod]
        public void IndexOf_Finds_1_And_Is_Result_Strictly_It() {
            Assert.AreEqual(R.IndexOf(1, list2), 0);
        }

        [TestMethod]
        public void IndexOf_Does_Not_Find_4() {
            Assert.AreEqual(R.IndexOf(4, list2), -1);
        }

        [TestMethod]
        [Description("IndexOf_Returns_-1_For_An_Empty_Array")]
        public void IndexOf_Returns_Minus_1_For_An_Empty_Array() {
            Assert.AreEqual(R.IndexOf(4, new int[0]), -1);
        }

        [TestMethod]
        [Description("IndexOf_Has_R.Equals_Semantics")]
        public void IndexOf_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.IndexOf(0, new[] { -0 }), 0);
            Assert.AreEqual(R.IndexOf(-0, new[] { 0 }), 0);
            Assert.AreEqual(R.IndexOf(R.Null, new[] { R.Null }), 0);
            Assert.AreEqual(R.IndexOf(new Just(new[] { 42 }), new[] { new Just(new[] { 42 }) }), 0);
        }

        [TestMethod]
        [Description("IndexOf_Dispatches_To_\"IndexOf\"_Method")]
        public void IndexOf_Dispatches_To_indexOf_Method() {
            var list = new List("b",
              new List("a",
              new List("n",
              new List("a",
              new List("n",
              new List("a",
              new Empty()))))));

            Assert.AreEqual(R.IndexOf("a", "banana"), 1);
            Assert.AreEqual(R.IndexOf("x", "banana"), -1);
            Assert.AreEqual(R.IndexOf("a", list), 1);
            Assert.AreEqual(R.IndexOf("x", list), -1);
        }

        [TestMethod]
        public void IndexOf_Is_Curried() {
            var curried = R.IndexOf(3);

            Assert.AreEqual(curried(list2), 2);
        }

        [TestMethod]
        [Description("IndexOf_Finds_Function,_Compared_By_Identity")]
        public void IndexOf_Finds_Function_Compared_By_Identity() {
            var f = new Action(() => { });
            var g = new Action(() => { });
            var list = new[] { g, f, g, f };

            Assert.AreEqual(R.IndexOf(f, list), 1);
        }

        [TestMethod]
        [Description("IndexOf_Does_Not_Find_Function,_Compared_By_Identity")]
        public void IndexOf_Does_Not_Find_Function_Compared_By_Identity() {
            var f = new Action(() => { });
            var g = new Action(() => { });
            var h = new Action(() => { });
            var list = new[] { g, f };

            Assert.AreEqual(R.IndexOf(h, list2), -1);
        }
    }
}
