using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class FindLastIndex : BaseFind
    {
        [TestMethod]
        public void FindLastIndex_Returns_The_Index_Of_The_Last_Element_That_Satisfies_The_Predicate_Into_An_Array() {
            Assert.AreEqual(R.FindLastIndex(even, a), 15);
            Assert.AreEqual(R.FindLastIndex(gt100, a), 9);
            Assert.AreEqual(R.FindLastIndex(isStr, a), 3);
            Assert.AreEqual(R.FindLastIndex(xGt100, a), 10);
        }

        [TestMethod]
        [Description("FindLastIndex_Returns_-1_When_No_Element_Satisfies_The_Predicate")]
        public void FindLastIndex_Returns_Minus_1_When_No_Element_Satisfies_The_Predicate() {
            Assert.AreEqual(R.FindLastIndex(even, new[] { "zing" }), -1);
        }

        [TestMethod]
        public void FindLastIndex_Returns_The_Index_Of_The_Last_Element_Into_An_Array_That_Satisfies_The_Predicate() {
            CollectionAssert.AreEqual(intoArray(R.FindLastIndex(even), a), new[] { 15 });
            CollectionAssert.AreEqual(intoArray(R.FindLastIndex(gt100), a), new[] { 9 });
            CollectionAssert.AreEqual(intoArray(R.FindLastIndex(isStr), a), new[] { 3 });
            CollectionAssert.AreEqual(intoArray(R.FindLastIndex(xGt100), a), new[] { 10 });
        }

        [TestMethod]
        [Description("FindLastIndex_Returns_-1_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array")]
        public void FindLastIndex_Returns_Minus_1_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.FindLastIndex(even), new[] { "zing" }), new[] { -1 });
        }

        [TestMethod]
        public void FindLastIndex_Works_When_The_First_Element_Matches() {
            Assert.AreEqual(R.FindLastIndex(even, new[] { 2, 3, 5 }), 0);
        }

        [TestMethod]
        public void FindLastIndex_Does_Not_Go_Into_An_Infinite_Loop_On_An_Empty_Array() {
            Assert.AreEqual(R.FindLastIndex(even, new object[] { }), -1);
        }

        [TestMethod]
        public void FindLastIndex_Is_Curried() {
            Assert.IsInstanceOfType(R.FindLastIndex(even), typeof(DynamicDelegate));
            Assert.AreEqual(R.FindLastIndex(even)(a), 15);
        }
    }
}
