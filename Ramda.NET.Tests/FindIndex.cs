using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class FindIndex : BaseFind
    {
        [TestMethod]
        public void FindIndex_Returns_The_First_Element_That_Satisfies_The_Predicate() {
            Assert.AreEqual(R.FindIndex(even, a), 1);
            Assert.AreEqual(R.FindIndex(gt100, a), 8);
            Assert.AreEqual(R.FindIndex(isStr, a), 3);
            Assert.AreEqual(R.FindIndex(xGt100, a), 10);
        }

        [TestMethod]
        public void FindIndex_Returns_The_Index_Of_The_First_Element_That_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.FindIndex(even), a), new[] { 1 });
            CollectionAssert.AreEqual(intoArray(R.FindIndex(gt100), a), new[] { 8 });
            CollectionAssert.AreEqual(intoArray(R.FindIndex(isStr), a), new[] { 3 });
            CollectionAssert.AreEqual(intoArray(R.FindIndex(xGt100), a), new[] { 10 });
        }

        [TestMethod]
        [Description("FindIndex_Returns_-1_When_No_Element_Satisfies_The_Predicate")]
        public void FindIndex_Returns_Minus_1_When_No_Element_Satisfies_The_Predicate() {
            Assert.AreEqual(R.FindIndex(even, new[] { "zing" }), -1);
            Assert.AreEqual(R.FindIndex(even, new object[0]), -1);
        }

        [TestMethod]
        [Description("FindIndex_Returns_-1_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array")]
        public void FindIndex_Returns_Minus_1_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.FindIndex(even), new[] { "zing" }), new [] { -1 });
        }

        [TestMethod]
        public void FindIndex_Is_Curried() {
            Assert.IsInstanceOfType(R.FindIndex(even), typeof(DynamicDelegate));
            Assert.AreEqual(R.FindIndex(even)(a), 1);
        }
    }
}
