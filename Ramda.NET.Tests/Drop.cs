using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Drop
    {
        [TestMethod]
        [Description("Drop_Skips_The_First_\"n\"_Elements_From_A_List,_Returning_The_Remainder")]
        public void Drop_Skips_The_First_N_Elements_From_A_List_Returning_The_Remainder() {
            CollectionAssert.AreEqual(R.Drop(3, new[] { "a", "b", "c", "d", "e", "f", "g" }), new[] { "d", "e", "f", "g" });
        }

        [TestMethod]
        [Description("Drop_Returns_An_Empty_Array_If_\"n\"_Is_Too_Large")]
        public void Drop_Returns_An_Empty_Array_If_N_Is_Too_Large() {
            CollectionAssert.AreEqual(R.Drop(20, new[] { "a", "b", "c", "d", "e", "f", "g" }), new string[0]);
        }

        [TestMethod]
        [Description("Drop_Returns_An_Equivalent_List_If_\"n\"_Is_<=_0")]
        public void Drop_Returns_An_Equivalent_List_If_N_Is_Lower_Than_Or_Equals_To_0() {
            CollectionAssert.AreEqual(R.Drop(0, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.Drop(-1, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.Drop(int.MinValue, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Drop_Never_Returns_The_Input_Array() {
            var xs = new[] { 1, 2, 3 };
            var res1 = R.Drop(0, xs);
            var res2 = R.Drop(-1, xs);

            Assert.AreNotEqual(res1, xs);
            Assert.AreNotEqual(res2, xs);
            CollectionAssert.AreEqual(res1, xs);
            CollectionAssert.AreEqual(res2, xs);
        }

        [TestMethod]
        public void Drop_Can_Operate_On_Strings() {
            Assert.AreEqual(R.Drop(3, "Ramda"), "da");
            Assert.AreEqual(R.Drop(4, "Ramda"), "a");
            Assert.AreEqual(R.Drop(5, "Ramda"), "");
            Assert.AreEqual(R.Drop(6, "Ramda"), "");
        }
    }
}
