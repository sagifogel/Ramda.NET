using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DropLast
    {
        [TestMethod]
        [Description("DropLast_Skips_The_Last_\"n\"_Elements_From_A_List,_Returning_The_Remainder")]
        public void DropLast_Skips_The_Last_N_Elements_From_A_List_Returning_The_Remainder() {
            CollectionAssert.AreEqual(R.DropLast(3, new[] { "a", "b", "c", "d", "e", "f", "g" }), new[] { "a", "b", "c", "d" });
        }

        [TestMethod]
        [Description("DropLast_Returns_An_Empty_Array_If_\"n\"_Is_Too_Large")]
        public void DropLast_Returns_An_Empty_Array_If_N_Is_Too_Large() {
            CollectionAssert.AreEqual(R.DropLast(20, new[] { "a", "b", "c", "d", "e", "f", "g" }), new string[0]);
        }

        [TestMethod]
        [Description("DropLast_Returns_An_Equivalent_List_If_\"n\"_Is_<=_0")]
        public void DropLast_Returns_An_Equivalent_List_If_N_Is_Lower_Than_Or_Equals_To_0() {
            CollectionAssert.AreEqual(R.DropLast(0, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.DropLast(-1, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(R.DropLast(int.MinValue, new[] { 1, 2, 3 }), new[] { 1, 2, 3 });
        }

        [TestMethod]
        public void DropLast_Never_Returns_The_Input_Array() {
            var xs = new[] { 1, 2, 3 };
            var res1 = R.DropLast(0, xs);
            var res2 = R.DropLast(-1, xs);

            Assert.AreNotEqual(res1, xs);
            Assert.AreNotEqual(res2, xs);
            CollectionAssert.AreEqual(res1, xs);
            CollectionAssert.AreEqual(res2, xs);
        }

        [TestMethod]
        public void DropLast_Can_Operate_On_Strings() {
            Assert.AreEqual(R.DropLast(3, "Ramda"), "Ra");
        }

        [TestMethod]
        public void DropLast_Is_Curried() {
            var dropLast2 = R.DropLast(2);

            CollectionAssert.AreEqual(dropLast2(new[] { "a", "b", "c", "d", "e" }), new[] { "a", "b", "c" });
            CollectionAssert.AreEqual(dropLast2(new[] { "x", "y", "z" }), new[] { "x" });
        }

        [TestMethod]
        public void DropLast_Can_Act_As_A_Transducer() {
            var dropLast2 = R.DropLast(2);

            CollectionAssert.AreEqual(R.Into(new int[0], dropLast2, new[] { 1, 3, 5, 7, 9, 1, 2 }), new[] { 1, 3, 5, 7, 9 });
            CollectionAssert.AreEqual(R.Into(new int[0], dropLast2, new[] { 1 }), new int[0]);
        }
    }
}
