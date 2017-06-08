using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Take
    {
        [TestMethod]
        [Description("Take_Takes_Only_The_First_\"N\"_Elements_From_A_List")]
        public void Take_Takes_Only_The_First_N_Elements_From_A_List() {
            CollectionAssert.AreEqual(R.Take(3, new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' }), new[] { 'a', 'b', 'c' });
        }

        [TestMethod]
        public void Take_Returns_Only_As_Many_As_The_Array_Can_Provide() {
            CollectionAssert.AreEqual(R.Take(3, new int[] { 1, 2 }), new int[] { 1, 2 });
            CollectionAssert.AreEqual(R.Take(3, new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("Take_Returns_An_Equivalent_List_If_\"N\"_Is_<_0")]
        public void Take_Returns_An_Equivalent_List_If_N_Is_Lower_Than_Zero() {
            CollectionAssert.AreEqual(R.Take(-1, new[] { 1, 2, 3 }), new int[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.Take(int.MinValue, new int[] { 1, 2, 3 }), new int[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Take_Never_Returns_The_Input_Array() {
            var xs = new[] { 1, 2, 3 };

            CollectionAssert.AreEqual(R.Take(3, xs), xs);
            CollectionAssert.AreEqual(R.Take(int.MaxValue, xs), xs);
            CollectionAssert.AreEqual(R.Take(-1, xs), xs);
        }

        [TestMethod]
        public void Take_Can_Operate_On_Strings() {
            Assert.AreEqual(R.Take(3, "Ramda"), "Ram");
            Assert.AreEqual(R.Take(2, "Ramda"), "Ra");
            Assert.AreEqual(R.Take(1, "Ramda"), "R");
            Assert.AreEqual(R.Take(0, "Ramda"), "");
        }

        [TestMethod]
        public void Take_Handles_Zero_Correctly() {
            CollectionAssert.AreEqual(R.Into(new int[0], R.Take(0), new[] { 1, 2, 3 }), new int[0]);
        }
    }
}
