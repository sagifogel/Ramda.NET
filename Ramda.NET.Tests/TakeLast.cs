using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class TakeLast
    {
        [TestMethod]
        [Description("TakeLast_TakeLasts_Only_The_First_\"N\"_Elements_From_A_List")]
        public void TakeLast_TakeLasts_Only_The_First_N_Elements_From_A_List() {
            CollectionAssert.AreEqual(R.TakeLast(3, new[] { "a", "b", "c", "d", "e", "f", "g" }), new[] { "e", "f", "g" });
        }

        [TestMethod]
        public void TakeLast_Returns_Only_As_Many_As_The_Array_Can_Provide() {
            CollectionAssert.AreEqual(R.TakeLast(3, new int[] { 1, 2 }), new int[] { 1, 2 });
            CollectionAssert.AreEqual(R.TakeLast(3, new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("TakeLast_Returns_An_Equivalent_List_If_\"N\"_Is_<_0")]
        public void TakeLast_Returns_An_Equivalent_List_If_N_Is_Lower_Than_Zero() {
            CollectionAssert.AreEqual(R.TakeLast(-1, new[] { 1, 2, 3 }), new int[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.TakeLast(int.MinValue, new int[] { 1, 2, 3 }), new int[] { 1, 2, 3 });
        }

        [TestMethod]
        public void TakeLast_Never_Returns_The_Input_Array() {
            var xs = new[] { 1, 2, 3 };

            CollectionAssert.AreEqual(R.TakeLast(3, xs), xs);
            CollectionAssert.AreEqual(R.TakeLast(int.MaxValue, xs), xs);
            CollectionAssert.AreEqual(R.TakeLast(-1, xs), xs);
        }

        [TestMethod]
        public void TakeLast_Can_Operate_On_Strings() {
            Assert.AreEqual(R.TakeLast(3, "Ramda"), "mda");
        }

        [TestMethod]
        public void TakeLast_Handles_Zero_Correctly() {
            CollectionAssert.AreEqual(R.TakeLast(0, new[] { 1, 2, 3 }), new int[0]);
        }

        [TestMethod]
        public void TakeLast_Is_Curried() {
            var takeLast3 = R.TakeLast(3);

            CollectionAssert.AreEqual(takeLast3(new[] { "a", "b", "c", "d", "e", "f", "g" }), new[] { "e", "f", "g" });
            CollectionAssert.AreEqual(takeLast3(new[] { "w", "x", "y", "z" }), new[] { "x", "y", "z" });
        }
    }
}
