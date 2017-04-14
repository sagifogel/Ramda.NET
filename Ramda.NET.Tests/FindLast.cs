using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class FindLast : BaseFind
    {
        [TestMethod]
        public void FindLast_Returns_The_Last_Element_That_Satisfies_The_Predicate() {
            Assert.AreEqual(R.FindLast(even, a), 0);
            Assert.AreEqual(R.FindLast(gt100, a), 300);
            Assert.AreEqual(R.FindLast(isStr, a), "cow");
            Assert.AreEqual(R.FindLast(xGt100, a), obj2);
        }

        [TestMethod]
        public void FindLast_Returns_The_Index_Of_The_Last_Element_That_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.FindLast(even), a), new[] { 0 });
            CollectionAssert.AreEqual(intoArray(R.FindLast(gt100), a), new[] { 300 });
            CollectionAssert.AreEqual(intoArray(R.FindLast(isStr), a), new[] { "cow" });
            CollectionAssert.AreEqual(intoArray(R.FindLast(xGt100), a), new[] { obj2 });
        }

        [TestMethod]
        [Description("FindLast_Returns_\"Undefined\"_When_No_Element_Satisfies_The_Predicate")]
        public void FindLast_Returns_Null_When_No_Element_Satisfies_The_Predicate() {
            Assert.IsNull(R.FindLast(even, new[] { "zing" }));
        }

        [TestMethod]
        [Description("FindLast_Returns_\"Undefined\"_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array")]
        public void FindLast_Returns_Null_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.FindLast(even), new[] { "zing" }), new object[] { null });
        }

        [TestMethod]
        public void FindLast_Works_When_The_First_Element_Matches() {
            Assert.AreEqual(R.FindLast(even, new[] { 2, 3, 5 }), 2);
        }

        [TestMethod]
        public void FindLast_Does_Not_Go_Into_An_Infinite_Loop_On_An_Empty_Array() {
            Assert.IsNull(R.FindLast(even, new object[] { }));
        }

        [TestMethod]
        public void FindLast_Is_Curried() {
            Assert.IsInstanceOfType(R.FindLast(even), typeof(DynamicDelegate));
            Assert.AreEqual(R.FindLast(even)(a), 0);
        }
    }
}
